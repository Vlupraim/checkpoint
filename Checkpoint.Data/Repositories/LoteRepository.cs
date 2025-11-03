using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Checkpoint.Core.Entities;

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
 CodigoLote = rdr.IsDBNull(2)? null : rdr.GetString(2),
 FechaIngreso = rdr.GetDateTime(3),
 FechaVencimiento = rdr.IsDBNull(4)? (DateTime?)null : rdr.GetDateTime(4),
 OrdenCompra = rdr.IsDBNull(5)? null : rdr.GetString(5),
 GuiaRecepcion = rdr.IsDBNull(6)? null : rdr.GetString(6),
 TempIngreso = rdr.IsDBNull(7)? (decimal?)null : rdr.GetDecimal(7),
 Estado = rdr.IsDBNull(8)? null : rdr.GetString(8)
 });
 }
 }
 }
 return list;
 }
 }
}
