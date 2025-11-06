using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Checkpoint.Core.Entities;
using Checkpoint.Core.Security; // CurrentSession

namespace Checkpoint.Data.Repositories
{
    public class LoteRepository
    {
        private readonly string _cs;

        public LoteRepository()
        {
            _cs = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
            if (string.IsNullOrEmpty(_cs)) throw new InvalidOperationException("Cadena 'App' no configurada.");
        }

        // ===================== CONSULTAS BÁSICAS =====================

        public IEnumerable<Lote> GetAll()
        {
            var list = new List<Lote>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"SELECT Id, ProductoId, CodigoLote, FechaIngreso, 
                                                     FechaVencimiento, OrdenCompra, GuiaRecepcion, 
                                                     TempIngreso, Estado 
                                              FROM Lote 
                                              ORDER BY FechaIngreso DESC", conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Lote
                        {
                            Id = rdr.GetGuid(0),
                            ProductoId = rdr.GetGuid(1),
                            CodigoLote = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            FechaIngreso = rdr.GetDateTime(3),
                            FechaVencimiento = rdr.IsDBNull(4) ? (DateTime?)null : rdr.GetDateTime(4),
                            OrdenCompra = rdr.IsDBNull(5) ? null : rdr.GetString(5),
                            GuiaRecepcion = rdr.IsDBNull(6) ? null : rdr.GetString(6),
                            TempIngreso = rdr.IsDBNull(7) ? (decimal?)null : rdr.GetDecimal(7),
                            Estado = rdr.IsDBNull(8) ? null : rdr.GetString(8)
                        });
                    }
                }
            }
            return list;
        }

        public Lote GetById(Guid id)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"SELECT Id, ProductoId, CodigoLote, FechaIngreso, 
                                                     FechaVencimiento, Estado 
                                              FROM Lote WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Lote
                        {
                            Id = rdr.GetGuid(0),
                            ProductoId = rdr.GetGuid(1),
                            CodigoLote = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            FechaIngreso = rdr.GetDateTime(3),
                            FechaVencimiento = rdr.IsDBNull(4) ? (DateTime?)null : rdr.GetDateTime(4),
                            Estado = rdr.IsDBNull(5) ? null : rdr.GetString(5)
                        };
                    }
                }
            }
            return null;
        }

        // ===================== CAMBIO DE ESTADO + AUDITORÍA =====================

        public void ActualizarEstadoLote(Guid loteId, string nuevoEstado, string observacion)
        {
            if (nuevoEstado != "Liberado" && nuevoEstado != "Bloqueado")
                throw new ArgumentException("El estado solo puede ser 'Liberado' o 'Bloqueado'.");

            var usuarioId = CurrentSession.UsuarioActual?.Id
                            ?? throw new InvalidOperationException("No hay un usuario autenticado.");

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(
                            "UPDATE Lote SET Estado = @Estado WHERE Id = @LoteId", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                            cmd.Parameters.AddWithValue("@LoteId", loteId);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand(@"
                            INSERT INTO CalidadLiberacion (Id, LoteId, UsuarioId, Fecha, Estado, Observacion)
                            VALUES (NEWID(), @LoteId, @UsuarioId, @Fecha, @Estado, @Observacion)", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@LoteId", loteId);
                            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                            cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                            cmd.Parameters.AddWithValue("@Observacion", (object)observacion ?? DBNull.Value);
                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // ===================== KPI / ALERTAS =====================

        /// <summary>
        /// Lotes con vencimiento en los próximos 'dias' (Pendiente o Liberado).
        /// </summary>
        public int GetLotesPorVencerCount(int dias)
        {
            const string sql = @"
SELECT COUNT(*) 
FROM Lote 
WHERE Estado IN ('Pendiente','Liberado')
  AND FechaVencimiento IS NOT NULL
  AND DATEDIFF(day, GETDATE(), FechaVencimiento) BETWEEN 0 AND @Dias;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            { cmd.Parameters.AddWithValue("@Dias", dias); conn.Open(); return (int)cmd.ExecuteScalar(); }
        }

        /// <summary>
        /// Top N lotes por vencer (ascendente por fecha).
        /// </summary>
        public IEnumerable<Lote> GetLotesPorVencer(int dias, int top = 10)
        {
            var list = new List<Lote>();
            string sql = $@"
SELECT TOP({top}) Id, ProductoId, CodigoLote, FechaIngreso, FechaVencimiento, 
       OrdenCompra, GuiaRecepcion, TempIngreso, Estado
FROM Lote
WHERE Estado IN ('Pendiente','Liberado')
  AND FechaVencimiento IS NOT NULL
  AND DATEDIFF(day, GETDATE(), FechaVencimiento) BETWEEN 0 AND @Dias
ORDER BY FechaVencimiento ASC;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Dias", dias);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Lote
                        {
                            Id = rdr.GetGuid(0),
                            ProductoId = rdr.GetGuid(1),
                            CodigoLote = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            FechaIngreso = rdr.GetDateTime(3),
                            FechaVencimiento = rdr.IsDBNull(4) ? (DateTime?)null : rdr.GetDateTime(4),
                            OrdenCompra = rdr.IsDBNull(5) ? null : rdr.GetString(5),
                            GuiaRecepcion = rdr.IsDBNull(6) ? null : rdr.GetString(6),
                            TempIngreso = rdr.IsDBNull(7) ? (decimal?)null : rdr.GetDecimal(7),
                            Estado = rdr.IsDBNull(8) ? null : rdr.GetString(8)
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Cantidad de lotes con estado 'Pendiente' (recepciones por cerrar).
        /// </summary>
        public int GetPendientesRecepcionCount()
        {
            const string sql = @"SELECT COUNT(*) FROM Lote WHERE Estado = 'Pendiente';";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            { conn.Open(); return (int)cmd.ExecuteScalar(); }
        }
    }
}
