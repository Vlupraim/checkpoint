using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Checkpoint.Core.Entities;

namespace Checkpoint.Data.Repositories
{
 public class UsuarioRepository
 {
 private readonly string _cs;
 public UsuarioRepository()
 {
 _cs = System.Configuration.ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(_cs)) throw new InvalidOperationException("Cadena 'App' no configurada.");
 }

 public Usuario GetByEmail(string email)
 {
 if (string.IsNullOrWhiteSpace(email)) return null;
 using (var conn = new SqlConnection(_cs))
 using (var cmd = new SqlCommand("SELECT Id, Nombre, Email, PasswordHash, Activo FROM [Usuario] WHERE Email = @email", conn))
 {
 cmd.Parameters.AddWithValue("@email", email);
 conn.Open();
 using (var rdr = cmd.ExecuteReader())
 {
 if (rdr.Read())
 {
 return new Usuario
 {
 Id = rdr.GetGuid(0),
 Nombre = rdr.IsDBNull(1)? null : rdr.GetString(1),
 Email = rdr.IsDBNull(2)? null : rdr.GetString(2),
 PasswordHash = rdr.IsDBNull(3)? null : rdr.GetString(3),
 Activo = rdr.IsDBNull(4)? false : rdr.GetBoolean(4)
 };
 }
 }
 }
 return null;
 }

 public IEnumerable<string> GetRoles(Guid usuarioId)
 {
 var list = new List<string>();
 using (var conn = new SqlConnection(_cs))
 using (var cmd = new SqlCommand("SELECT R.Nombre FROM Rol R JOIN [UserRole] UR ON R.Id = UR.RolId WHERE UR.UsuarioId = @uid", conn))
 {
 cmd.Parameters.AddWithValue("@uid", usuarioId);
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
