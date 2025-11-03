using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Checkpoint.Core.Entities;

namespace Checkpoint.Data.Repositories
{
 public class UbicacionRepository
 {
 private readonly string _cs;
 public UbicacionRepository()
 {
 _cs = System.Configuration.ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(_cs)) throw new InvalidOperationException("Cadena 'App' no configurada.");
 }

 public IEnumerable<Ubicacion> GetAll()
 {
 var list = new List<Ubicacion>();
 using (var conn = new SqlConnection(_cs))
 using (var cmd = new SqlCommand("SELECT Id, SedeId, Codigo, Tipo, Capacidad FROM Ubicacion ORDER BY Codigo", conn))
 {
 conn.Open();
 using (var rdr = cmd.ExecuteReader())
 {
 while (rdr.Read())
 {
 list.Add(new Ubicacion
 {
 Id = rdr.GetGuid(0),
 SedeId = rdr.GetGuid(1),
 Codigo = rdr.IsDBNull(2)? null : rdr.GetString(2),
 Tipo = rdr.IsDBNull(3)? null : rdr.GetString(3),
 Capacidad = rdr.IsDBNull(4)? (decimal?)null : rdr.GetDecimal(4)
 });
 }
 }
 }
 return list;
 }
 }
}
