using System;
using System.Data;
using System.Data.SqlClient;
using Checkpoint.Core.Entities;

namespace Checkpoint.Data.Repositories
{
 public class MovimientoRepository
 {
 private readonly string _cs;
 public MovimientoRepository()
 {
 _cs = System.Configuration.ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(_cs)) throw new InvalidOperationException("Cadena 'App' no configurada.");
 }

 // Registrar Ingreso
 public void RegistrarIngreso(Movimiento m)
 {
 if (m == null) throw new ArgumentNullException(nameof(m));
 if (m.Cantidad <=0) throw new InvalidOperationException("Cantidad debe ser mayor que0.");

 using (var conn = new SqlConnection(_cs))
 {
 conn.Open();
 using (var tran = conn.BeginTransaction())
 {
 try
 {
 // Insertar movimiento
 using (var cmd = new SqlCommand(@"INSERT INTO Movimiento (Id, LoteId, SedeId, OrigenUbicacionId, DestinoUbicacionId, Tipo, Cantidad, Unidad, Fecha, UsuarioId, Motivo)
VALUES (@Id,@LoteId,@SedeId,@OrigenUbicacionId,@DestinoUbicacionId,@Tipo,@Cantidad,@Unidad,@Fecha,@UsuarioId,@Motivo)", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@SedeId", m.SedeId);
 cmd.Parameters.AddWithValue("@OrigenUbicacionId", (object)m.OrigenUbicacionId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@DestinoUbicacionId", (object)m.DestinoUbicacionId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Tipo", m.Tipo);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.Parameters.AddWithValue("@Fecha", m.Fecha);
 cmd.Parameters.AddWithValue("@UsuarioId", (object)m.UsuarioId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Motivo", (object)m.Motivo ?? DBNull.Value);
 cmd.ExecuteNonQuery();
 }

 // Actualizar stock: sumar a destino (si existe actualizar, si no insertar)
 if (m.DestinoUbicacionId.HasValue)
 {
 using (var cmd = new SqlCommand(@"IF EXISTS (SELECT1 FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId)
 UPDATE Stock SET Cantidad = Cantidad + @Cantidad, ActualizadoEn = GETDATE() WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId
ELSE
 INSERT INTO Stock (Id, LoteId, UbicacionId, Cantidad, Unidad, ActualizadoEn) VALUES (NEWID(), @LoteId, @UbicacionId, @Cantidad, @Unidad, GETDATE())", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.DestinoUbicacionId.Value);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.ExecuteNonQuery();
 }
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

 // Registrar Salida: valida existencia en origen y estado de lote
 public void RegistrarSalida(Movimiento m)
 {
 if (m == null) throw new ArgumentNullException(nameof(m));
 if (m.Cantidad <=0) throw new InvalidOperationException("Cantidad debe ser mayor que0.");
 if (!m.OrigenUbicacionId.HasValue) throw new InvalidOperationException("OrigenUbicacionId es requerido para Salida.");

 using (var conn = new SqlConnection(_cs))
 {
 conn.Open();
 using (var tran = conn.BeginTransaction())
 {
 try
 {
 // Verificar estado del lote (debe ser Liberado)
 using (var cmd = new SqlCommand("SELECT Estado FROM Lote WHERE Id = @LoteId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 var estadoObj = cmd.ExecuteScalar();
 var estado = estadoObj == null || estadoObj == DBNull.Value ? null : estadoObj.ToString();
 if (string.IsNullOrEmpty(estado) || !estado.Equals("Liberado", StringComparison.OrdinalIgnoreCase))
 throw new InvalidOperationException("Solo lotes 'Liberado' pueden realizar salidas.");
 }

 // Verificar stock en origen
 decimal disponible =0m;
 using (var cmd = new SqlCommand("SELECT Cantidad FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.OrigenUbicacionId.Value);
 var obj = cmd.ExecuteScalar();
 if (obj == null) throw new InvalidOperationException("No hay stock en la ubicación de origen.");
 disponible = Convert.ToDecimal(obj);
 if (disponible < m.Cantidad) throw new InvalidOperationException("Cantidad insuficiente en origen.");
 }

 // Insertar movimiento
 using (var cmd = new SqlCommand(@"INSERT INTO Movimiento (Id, LoteId, SedeId, OrigenUbicacionId, DestinoUbicacionId, Tipo, Cantidad, Unidad, Fecha, UsuarioId, Motivo)
VALUES (@Id,@LoteId,@SedeId,@OrigenUbicacionId,@DestinoUbicacionId,@Tipo,@Cantidad,@Unidad,@Fecha,@UsuarioId,@Motivo)", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@SedeId", m.SedeId);
 cmd.Parameters.AddWithValue("@OrigenUbicacionId", m.OrigenUbicacionId.Value);
 cmd.Parameters.AddWithValue("@DestinoUbicacionId", (object)m.DestinoUbicacionId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Tipo", m.Tipo);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.Parameters.AddWithValue("@Fecha", m.Fecha);
 cmd.Parameters.AddWithValue("@UsuarioId", (object)m.UsuarioId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Motivo", (object)m.Motivo ?? DBNull.Value);
 cmd.ExecuteNonQuery();
 }

 // Restar stock en origen
 using (var cmd = new SqlCommand(@"UPDATE Stock SET Cantidad = Cantidad - @Cantidad, ActualizadoEn = GETDATE() WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.OrigenUbicacionId.Value);
 cmd.ExecuteNonQuery();
 }

 // Verificar no negativo (double-check)
 using (var cmd = new SqlCommand("SELECT Cantidad FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.OrigenUbicacionId.Value);
 var obj = cmd.ExecuteScalar();
 var after = Convert.ToDecimal(obj);
 if (after <0) throw new InvalidOperationException("Operación generó stock negativo.");
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

 // Movimiento interno: resta en origen y suma en destino, misma sede
 public void RegistrarMovimientoInterno(Movimiento m)
 {
 if (m == null) throw new ArgumentNullException(nameof(m));
 if (m.Cantidad <=0) throw new InvalidOperationException("Cantidad debe ser mayor que0.");
 if (!m.OrigenUbicacionId.HasValue || !m.DestinoUbicacionId.HasValue) throw new InvalidOperationException("Origen y Destino son requeridos para movimiento interno.");
 if (m.OrigenUbicacionId == m.DestinoUbicacionId) throw new InvalidOperationException("Origen y destino no pueden ser iguales.");

 using (var conn = new SqlConnection(_cs))
 {
 conn.Open();
 using (var tran = conn.BeginTransaction())
 {
 try
 {
 // Verificar lote Liberado
 using (var cmd = new SqlCommand("SELECT Estado FROM Lote WHERE Id = @LoteId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 var estadoObj = cmd.ExecuteScalar();
 var estado = estadoObj == null || estadoObj == DBNull.Value ? null : estadoObj.ToString();
 if (string.IsNullOrEmpty(estado) || !estado.Equals("Liberado", StringComparison.OrdinalIgnoreCase))
 throw new InvalidOperationException("Solo lotes 'Liberado' pueden moverse internamente.");
 }

 // Verificar que ambas ubicaciones pertenezcan a la misma sede
 Guid sedeOrigen, sedeDestino;
 using (var cmd = new SqlCommand("SELECT SedeId FROM Ubicacion WHERE Id=@Id", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Id", m.OrigenUbicacionId.Value);
 sedeOrigen = (Guid)cmd.ExecuteScalar();
 }
 using (var cmd = new SqlCommand("SELECT SedeId FROM Ubicacion WHERE Id=@Id", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Id", m.DestinoUbicacionId.Value);
 sedeDestino = (Guid)cmd.ExecuteScalar();
 }
 if (sedeOrigen != sedeDestino) throw new InvalidOperationException("Movimiento interno requiere que origen y destino estén en la misma sede.");

 // Verificar stock en origen
 decimal disponible =0m;
 using (var cmd = new SqlCommand("SELECT Cantidad FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.OrigenUbicacionId.Value);
 var obj = cmd.ExecuteScalar();
 if (obj == null) throw new InvalidOperationException("No hay stock en la ubicación de origen.");
 disponible = Convert.ToDecimal(obj);
 if (disponible < m.Cantidad) throw new InvalidOperationException("Cantidad insuficiente in origen.");
 }

 // Insertar movimiento
 using (var cmd = new SqlCommand(@"INSERT INTO Movimiento (Id, LoteId, SedeId, OrigenUbicacionId, DestinoUbicacionId, Tipo, Cantidad, Unidad, Fecha, UsuarioId, Motivo)
VALUES (@Id,@LoteId,@SedeId,@OrigenUbicacionId,@DestinoUbicacionId,@Tipo,@Cantidad,@Unidad,@Fecha,@UsuarioId,@Motivo)", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@SedeId", m.SedeId);
 cmd.Parameters.AddWithValue("@OrigenUbicacionId", m.OrigenUbicacionId.Value);
 cmd.Parameters.AddWithValue("@DestinoUbicacionId", m.DestinoUbicacionId.Value);
 cmd.Parameters.AddWithValue("@Tipo", m.Tipo);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.Parameters.AddWithValue("@Fecha", m.Fecha);
 cmd.Parameters.AddWithValue("@UsuarioId", (object)m.UsuarioId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Motivo", (object)m.Motivo ?? DBNull.Value);
 cmd.ExecuteNonQuery();
 }

 // Restar origen
 using (var cmd = new SqlCommand(@"UPDATE Stock SET Cantidad = Cantidad - @Cantidad, ActualizadoEn = GETDATE() WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.OrigenUbicacionId.Value);
 cmd.ExecuteNonQuery();
 }

 // Sumar destino (upsert)
 using (var cmd = new SqlCommand(@"IF EXISTS (SELECT1 FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId)
 UPDATE Stock SET Cantidad = Cantidad + @Cantidad, ActualizadoEn = GETDATE() WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId
ELSE
 INSERT INTO Stock (Id, LoteId, UbicacionId, Cantidad, Unidad, ActualizadoEn) VALUES (NEWID(), @LoteId, @UbicacionId, @Cantidad, @Unidad, GETDATE())", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.DestinoUbicacionId.Value);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.ExecuteNonQuery();
 }

 // Verificar no negativo
 using (var cmd = new SqlCommand("SELECT Cantidad FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.OrigenUbicacionId.Value);
 var obj = cmd.ExecuteScalar();
 var after = Convert.ToDecimal(obj);
 if (after <0) throw new InvalidOperationException("Operación generó stock negativo.");
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

 // Registrar Devolución: suma en destino (no exige lote Liberado)
 public void RegistrarDevolucion(Movimiento m)
 {
 if (m == null) throw new ArgumentNullException(nameof(m));
 if (m.Cantidad <=0) throw new InvalidOperationException("Cantidad debe ser mayor que0.");
 if (!m.DestinoUbicacionId.HasValue) throw new InvalidOperationException("DestinoUbicacionId es requerido para Devolución.");

 using (var conn = new SqlConnection(_cs))
 {
 conn.Open();
 using (var tran = conn.BeginTransaction())
 {
 try
 {
 using (var cmd = new SqlCommand(@"INSERT INTO Movimiento (Id, LoteId, SedeId, OrigenUbicacionId, DestinoUbicacionId, Tipo, Cantidad, Unidad, Fecha, UsuarioId, Motivo)
VALUES (@Id,@LoteId,@SedeId,@OrigenUbicacionId,@DestinoUbicacionId,@Tipo,@Cantidad,@Unidad,@Fecha,@UsuarioId,@Motivo)", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@SedeId", m.SedeId);
 cmd.Parameters.AddWithValue("@OrigenUbicacionId", (object)m.OrigenUbicacionId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@DestinoUbicacionId", m.DestinoUbicacionId.Value);
 cmd.Parameters.AddWithValue("@Tipo", m.Tipo);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.Parameters.AddWithValue("@Fecha", m.Fecha);
 cmd.Parameters.AddWithValue("@UsuarioId", (object)m.UsuarioId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Motivo", (object)m.Motivo ?? DBNull.Value);
 cmd.ExecuteNonQuery();
 }

 // Upsert stock destino
 using (var cmd = new SqlCommand(@"IF EXISTS (SELECT1 FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId)
 UPDATE Stock SET Cantidad = Cantidad + @Cantidad, ActualizadoEn = GETDATE() WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId
ELSE
 INSERT INTO Stock (Id, LoteId, UbicacionId, Cantidad, Unidad, ActualizadoEn) VALUES (NEWID(), @LoteId, @UbicacionId, @Cantidad, @Unidad, GETDATE())", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", m.DestinoUbicacionId.Value);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
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

 // Registrar Ajuste: suma/resta en misma ubicación y exige Motivo
 public void RegistrarAjuste(Movimiento m)
 {
 if (m == null) throw new ArgumentNullException(nameof(m));
 if (m.Cantidad ==0) throw new InvalidOperationException("Cantidad de ajuste debe ser distinta de0.");
 if (string.IsNullOrWhiteSpace(m.Motivo)) throw new InvalidOperationException("Motivo es requerido para ajustes.");
 if (!m.DestinoUbicacionId.HasValue && !m.OrigenUbicacionId.HasValue) throw new InvalidOperationException("Ubicación de ajuste es requerida en Origen o Destino.");

 // Definir ubicacion objetivo: preferir DestinoUbicacionId
 var ubicacionId = m.DestinoUbicacionId ?? m.OrigenUbicacionId.Value;

 using (var conn = new SqlConnection(_cs))
 {
 conn.Open();
 using (var tran = conn.BeginTransaction())
 {
 try
 {
 // Insertar movimiento
 using (var cmd = new SqlCommand(@"INSERT INTO Movimiento (Id, LoteId, SedeId, OrigenUbicacionId, DestinoUbicacionId, Tipo, Cantidad, Unidad, Fecha, UsuarioId, Motivo)
VALUES (@Id,@LoteId,@SedeId,@OrigenUbicacionId,@DestinoUbicacionId,@Tipo,@Cantidad,@Unidad,@Fecha,@UsuarioId,@Motivo)", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Id", Guid.NewGuid());
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@SedeId", m.SedeId);
 cmd.Parameters.AddWithValue("@OrigenUbicacionId", (object)m.OrigenUbicacionId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@DestinoUbicacionId", (object)m.DestinoUbicacionId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Tipo", m.Tipo);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.Parameters.AddWithValue("@Fecha", m.Fecha);
 cmd.Parameters.AddWithValue("@UsuarioId", (object)m.UsuarioId ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Motivo", (object)m.Motivo ?? DBNull.Value);
 cmd.ExecuteNonQuery();
 }

 // Si la cantidad es positiva -> sumar, si negativa -> restar
 if (m.Cantidad >0)
 {
 using (var cmd = new SqlCommand(@"IF EXISTS (SELECT1 FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId)
 UPDATE Stock SET Cantidad = Cantidad + @Cantidad, ActualizadoEn = GETDATE() WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId
ELSE
 INSERT INTO Stock (Id, LoteId, UbicacionId, Cantidad, Unidad, ActualizadoEn) VALUES (NEWID(), @LoteId, @UbicacionId, @Cantidad, @Unidad, GETDATE())", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", ubicacionId);
 cmd.Parameters.AddWithValue("@Cantidad", m.Cantidad);
 cmd.Parameters.AddWithValue("@Unidad", m.Unidad);
 cmd.ExecuteNonQuery();
 }
 }
 else
 {
 var qty = Math.Abs(m.Cantidad);
 // Verificar stock suficiente
 decimal disponible =0m;
 using (var cmd = new SqlCommand("SELECT Cantidad FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", ubicacionId);
 var obj = cmd.ExecuteScalar();
 if (obj == null) throw new InvalidOperationException("No hay stock en la ubicación para ajustar.");
 disponible = Convert.ToDecimal(obj);
 if (disponible < qty) throw new InvalidOperationException("Cantidad insuficiente para ajuste.");
 }
 using (var cmd = new SqlCommand(@"UPDATE Stock SET Cantidad = Cantidad - @Cantidad, ActualizadoEn = GETDATE() WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@Cantidad", qty);
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", ubicacionId);
 cmd.ExecuteNonQuery();
 }
 }

 // Verificar no negativo
 using (var cmd = new SqlCommand("SELECT Cantidad FROM Stock WHERE LoteId=@LoteId AND UbicacionId=@UbicacionId", conn, tran))
 {
 cmd.Parameters.AddWithValue("@LoteId", m.LoteId);
 cmd.Parameters.AddWithValue("@UbicacionId", ubicacionId);
 var obj = cmd.ExecuteScalar();
 var after = Convert.ToDecimal(obj);
 if (after <0) throw new InvalidOperationException("Operación generó stock negativo.");
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
