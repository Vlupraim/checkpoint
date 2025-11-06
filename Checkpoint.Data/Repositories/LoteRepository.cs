using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Checkpoint.Core.Entities;
using Checkpoint.Core.Security; // Necesario para CurrentSession

namespace Checkpoint.Data.Repositories
{
    public class LoteRepository
    {
        private readonly string _cs;
        public LoteRepository()
        {
            _cs = System.Configuration.ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
            if (string.IsNullOrEmpty(_cs)) throw new InvalidOperationException("Cadena 'App' no configurada.");
        }

        public IEnumerable<Lote> GetAll()
        {
            var list = new List<Lote>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT Id, ProductoId, CodigoLote, FechaIngreso, FechaVencimiento, OrdenCompra, GuiaRecepcion, TempIngreso, Estado FROM Lote ORDER BY FechaIngreso DESC", conn))
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

        // NUEVO: Necesario para verificar el estado actual antes de actualizar
        public Lote GetById(Guid id)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT Id, ProductoId, CodigoLote, FechaIngreso, FechaVencimiento, Estado FROM Lote WHERE Id = @Id", conn))
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


        // NUEVO: Método transaccional para actualizar el estado del lote y registrar la auditoría
        public void ActualizarEstadoLote(Guid loteId, string nuevoEstado, string observacion)
        {
            // Validar que el estado sea uno de los permitidos
            if (nuevoEstado != "Liberado" && nuevoEstado != "Bloqueado")
            {
                throw new ArgumentException("El estado solo puede ser 'Liberado' o 'Bloqueado'.");
            }

            var usuarioId = CurrentSession.UsuarioActual?.Id;
            if (usuarioId == null)
            {
                throw new InvalidOperationException("No hay un usuario autenticado para registrar la auditoría.");
            }

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // 1. Actualizar el estado en la tabla Lote
                        using (var cmd = new SqlCommand("UPDATE Lote SET Estado = @Estado WHERE Id = @LoteId", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Estado", nuevoEstado);
                            cmd.Parameters.AddWithValue("@LoteId", loteId);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Insertar el registro de auditoría en CalidadLiberacion
                        using (var cmd = new SqlCommand(@"INSERT INTO CalidadLiberacion (Id, LoteId, UsuarioId, Fecha, Estado, Observacion)
                                                        VALUES (NEWID(), @LoteId, @UsuarioId, @Fecha, @Estado, @Observacion)", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@LoteId", loteId);
                            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId.Value);
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
    }
}