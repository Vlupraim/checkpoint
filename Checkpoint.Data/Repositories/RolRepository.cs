using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Checkpoint.Core.Entities;

namespace Checkpoint.Data.Repositories
{
 public class RolRepository
 {
 private readonly string _cs;
 public RolRepository()
 {
 _cs = System.Configuration.ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(_cs)) throw new InvalidOperationException("Cadena 'App' no configurada.");
 }

 public IEnumerable<string> GetAllNames()
 {
 var list = new List<string>();
 using (var conn = new SqlConnection(_cs))
 using (var cmd = new SqlCommand("SELECT Nombre FROM Rol ORDER BY Nombre", conn))
 {
 conn.Open();
 using (var rdr = cmd.ExecuteReader())
 {
 while (rdr.Read()) list.Add(rdr.IsDBNull(0)? null : rdr.GetString(0));
 }
 }
 return list;
 }
 }
}
