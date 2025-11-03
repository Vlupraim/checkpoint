using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Checkpoint.Core.Entities;

namespace Checkpoint.Data.Repositories
{
 public class ProductoRepository
 {
 private readonly string _connectionString;

 public ProductoRepository()
 {
 _connectionString = ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(_connectionString))
 {
 throw new InvalidOperationException("Cadena de conexión 'App' no configurada.");
 }
 }

 // Obtener todos los productos
 public IEnumerable<Producto> GetAll()
 {
 var list = new List<Producto>();
 using (var conn = new SqlConnection(_connectionString))
 using (var cmd = new SqlCommand("SELECT Id, Sku, Nombre, Unidad, VidaUtilDias, TempMin, TempMax, StockMinimo, Activo FROM Producto ORDER BY Nombre", conn))
 {
 conn.Open();
 using (var rdr = cmd.ExecuteReader())
 {
 while (rdr.Read())
 {
 list.Add(new Producto
 {
 Id = rdr.GetGuid(0),
 Sku = rdr.IsDBNull(1) ? null : rdr.GetString(1),
 Nombre = rdr.IsDBNull(2) ? null : rdr.GetString(2),
 Unidad = rdr.IsDBNull(3) ? null : rdr.GetString(3),
 VidaUtilDias = rdr.IsDBNull(4) ?0 : rdr.GetInt32(4),
 TempMin = rdr.IsDBNull(5) ? (decimal?)null : rdr.GetDecimal(5),
 TempMax = rdr.IsDBNull(6) ? (decimal?)null : rdr.GetDecimal(6),
 StockMinimo = rdr.IsDBNull(7) ?0 : rdr.GetDecimal(7),
 Activo = rdr.IsDBNull(8) ? false : rdr.GetBoolean(8)
 });
 }
 }
 }
 return list;
 }

 public Producto GetById(Guid id)
 {
 using (var conn = new SqlConnection(_connectionString))
 using (var cmd = new SqlCommand("SELECT Id, Sku, Nombre, Unidad, VidaUtilDias, TempMin, TempMax, StockMinimo, Activo FROM Producto WHERE Id = @id", conn))
 {
 cmd.Parameters.AddWithValue("@id", id);
 conn.Open();
 using (var rdr = cmd.ExecuteReader())
 {
 if (rdr.Read())
 {
 return new Producto
 {
 Id = rdr.GetGuid(0),
 Sku = rdr.IsDBNull(1) ? null : rdr.GetString(1),
 Nombre = rdr.IsDBNull(2) ? null : rdr.GetString(2),
 Unidad = rdr.IsDBNull(3) ? null : rdr.GetString(3),
 VidaUtilDias = rdr.IsDBNull(4) ?0 : rdr.GetInt32(4),
 TempMin = rdr.IsDBNull(5) ? (decimal?)null : rdr.GetDecimal(5),
 TempMax = rdr.IsDBNull(6) ? (decimal?)null : rdr.GetDecimal(6),
 StockMinimo = rdr.IsDBNull(7) ?0 : rdr.GetDecimal(7),
 Activo = rdr.IsDBNull(8) ? false : rdr.GetBoolean(8)
 };
 }
 }
 }
 return null;
 }

 public void Insert(Producto p)
 {
 if (p == null) throw new ArgumentNullException(nameof(p));
 p.Id = Guid.NewGuid();
 using (var conn = new SqlConnection(_connectionString))
 using (var cmd = new SqlCommand(@"INSERT INTO Producto (Id, Sku, Nombre, Unidad, VidaUtilDias, TempMin, TempMax, StockMinimo, Activo) VALUES (@Id,@Sku,@Nombre,@Unidad,@VidaUtilDias,@TempMin,@TempMax,@StockMinimo,@Activo)", conn))
 {
 cmd.Parameters.AddWithValue("@Id", p.Id);
 cmd.Parameters.AddWithValue("@Sku", (object)p.Sku ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Nombre", (object)p.Nombre ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Unidad", (object)p.Unidad ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@VidaUtilDias", p.VidaUtilDias);
 cmd.Parameters.AddWithValue("@TempMin", (object)p.TempMin ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@TempMax", (object)p.TempMax ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@StockMinimo", p.StockMinimo);
 cmd.Parameters.AddWithValue("@Activo", p.Activo);
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 }

 public void Update(Producto p)
 {
 if (p == null) throw new ArgumentNullException(nameof(p));
 using (var conn = new SqlConnection(_connectionString))
 using (var cmd = new SqlCommand(@"UPDATE Producto SET Sku=@Sku, Nombre=@Nombre, Unidad=@Unidad, VidaUtilDias=@VidaUtilDias, TempMin=@TempMin, TempMax=@TempMax, StockMinimo=@StockMinimo, Activo=@Activo WHERE Id=@Id", conn))
 {
 cmd.Parameters.AddWithValue("@Id", p.Id);
 cmd.Parameters.AddWithValue("@Sku", (object)p.Sku ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Nombre", (object)p.Nombre ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@Unidad", (object)p.Unidad ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@VidaUtilDias", p.VidaUtilDias);
 cmd.Parameters.AddWithValue("@TempMin", (object)p.TempMin ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@TempMax", (object)p.TempMax ?? DBNull.Value);
 cmd.Parameters.AddWithValue("@StockMinimo", p.StockMinimo);
 cmd.Parameters.AddWithValue("@Activo", p.Activo);
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 }

 public void Delete(Guid id)
 {
 using (var conn = new SqlConnection(_connectionString))
 using (var cmd = new SqlCommand("DELETE FROM Producto WHERE Id = @id", conn))
 {
 cmd.Parameters.AddWithValue("@id", id);
 conn.Open();
 cmd.ExecuteNonQuery();
 }
 }
 }
}
