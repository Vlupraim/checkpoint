using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Checkpoint.Core.Entities;

namespace Checkpoint.Data.Repositories
{
 public class SedeRepository
 {
 private readonly string _cs;
 public SedeRepository()
 {
 _cs = System.Configuration.ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(_cs)) throw new InvalidOperationException("Cadena 'App' no configurada.");
 }

 public IEnumerable<Sede> GetAll()
 {
 var list = new List<Sede>();
 using (var conn = new SqlConnection(_cs))
 using (var cmd = new SqlCommand("SELECT Id, Nombre, Codigo, Direccion, Activa FROM Sede ORDER BY Nombre", conn))
 {
 conn.Open();
 using (var rdr = cmd.ExecuteReader())
 {
 while (rdr.Read())
 {
 list.Add(new Sede
 {
 Id = rdr.GetGuid(0),
 Nombre = rdr.IsDBNull(1)? null : rdr.GetString(1),
 Codigo = rdr.IsDBNull(2)? null : rdr.GetString(2),
 Direccion = rdr.IsDBNull(3)? null : rdr.GetString(3),
 Activa = rdr.IsDBNull(4)? false : rdr.GetBoolean(4)
 });
 }
 }
 }
 return list;
 }
 }
}
