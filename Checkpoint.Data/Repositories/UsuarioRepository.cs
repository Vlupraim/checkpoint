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
                            Nombre = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                            Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            PasswordHash = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                            Activo = rdr.IsDBNull(4) ? false : rdr.GetBoolean(4)
                        };
                    }
                }
            }
            return null;
        }

        public Usuario GetById(Guid id)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT Id, Nombre, Email, PasswordHash, Activo FROM [Usuario] WHERE Id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Usuario
                        {
                            Id = rdr.GetGuid(0),
                            Nombre = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                            Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            PasswordHash = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                            Activo = rdr.IsDBNull(4) ? false : rdr.GetBoolean(4)
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<Usuario> GetAll()
        {
            var list = new List<Usuario>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT Id, Nombre, Email, Activo FROM [Usuario] ORDER BY Nombre", conn))
            {
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        list.Add(new Usuario
                        {
                            Id = rdr.GetGuid(0),
                            Nombre = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                            Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                            Activo = rdr.IsDBNull(3) ? false : rdr.GetBoolean(3)
                        });
                    }
                }
            }
            return list;
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
                    while (rdr.Read()) list.Add(rdr.IsDBNull(0) ? null : rdr.GetString(0));
                }
            }
            return list;
        }

        public IEnumerable<Guid> GetRoleIds(Guid usuarioId)
        {
            var list = new List<Guid>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT RolId FROM [UserRole] WHERE UsuarioId = @uid", conn))
            {
                cmd.Parameters.AddWithValue("@uid", usuarioId);
                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read()) list.Add(rdr.GetGuid(0));
                }
            }
            return list;
        }

        public bool ExistsEmail(string email, Guid? excludeId = null)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(
                excludeId.HasValue
                    ? "SELECT 1 FROM [Usuario] WHERE Email=@Email AND Id<>@Id"
                    : "SELECT 1 FROM [Usuario] WHERE Email=@Email", conn))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                if (excludeId.HasValue) cmd.Parameters.AddWithValue("@Id", excludeId.Value);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o != null;
            }
        }

        public void Insert(Usuario u, List<Guid> roles)
        {
            if (u == null) throw new ArgumentNullException(nameof(u));
            if (u.Id == Guid.Empty) u.Id = Guid.NewGuid();

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(
                            @"INSERT INTO [Usuario] (Id, Nombre, Email, PasswordHash, Activo) 
                              VALUES (@Id, @Nombre, @Email, @PasswordHash, @Activo)", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", u.Id);
                            cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                            cmd.Parameters.AddWithValue("@Email", u.Email);
                            cmd.Parameters.AddWithValue("@PasswordHash", (object)u.PasswordHash ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Activo", u.Activo);
                            cmd.ExecuteNonQuery();
                        }

                        if (roles != null)
                        {
                            foreach (var rolId in roles)
                            {
                                using (var cmd = new SqlCommand(
                                    @"INSERT INTO [UserRole] (UsuarioId, RolId, AsignadoEn, AsignadoPorUsuarioId) 
                                      VALUES (@UsuarioId, @RolId, GETDATE(), @AsignadoPor)", conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@UsuarioId", u.Id);
                                    cmd.Parameters.AddWithValue("@RolId", rolId);
                                    cmd.Parameters.AddWithValue("@AsignadoPor",
                                        (object)Core.Security.CurrentSession.UsuarioActual?.Id ?? DBNull.Value);
                                    cmd.ExecuteNonQuery();
                                }
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

        public void Update(Usuario u, List<Guid> roles)
        {
            if (u == null) throw new ArgumentNullException(nameof(u));

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(
                            @"UPDATE [Usuario] 
                              SET Nombre=@Nombre, Email=@Email, PasswordHash=@PasswordHash, Activo=@Activo 
                              WHERE Id=@Id", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", u.Id);
                            cmd.Parameters.AddWithValue("@Nombre", u.Nombre);
                            cmd.Parameters.AddWithValue("@Email", u.Email);
                            cmd.Parameters.AddWithValue("@PasswordHash", (object)u.PasswordHash ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Activo", u.Activo);
                            cmd.ExecuteNonQuery();
                        }

                        using (var cmd = new SqlCommand(
                            "DELETE FROM [UserRole] WHERE UsuarioId=@UsuarioId", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@UsuarioId", u.Id);
                            cmd.ExecuteNonQuery();
                        }

                        if (roles != null)
                        {
                            foreach (var rolId in roles)
                            {
                                using (var cmd = new SqlCommand(
                                    @"INSERT INTO [UserRole] (UsuarioId, RolId, AsignadoEn, AsignadoPorUsuarioId) 
                                      VALUES (@UsuarioId, @RolId, GETDATE(), @AsignadoPor)", conn, tran))
                                {
                                    cmd.Parameters.AddWithValue("@UsuarioId", u.Id);
                                    cmd.Parameters.AddWithValue("@RolId", rolId);
                                    cmd.Parameters.AddWithValue("@AsignadoPor",
                                        (object)Core.Security.CurrentSession.UsuarioActual?.Id ?? DBNull.Value);
                                    cmd.ExecuteNonQuery();
                                }
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

        // 🔴 Eliminación dura (borra roles y luego usuario)
        public void Delete(Guid usuarioId)
        {
            if (usuarioId == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(usuarioId));
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DELETE FROM [UserRole] WHERE UsuarioId=@Id", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", usuarioId);
                            cmd.ExecuteNonQuery();
                        }
                        using (var cmd = new SqlCommand("DELETE FROM [Usuario] WHERE Id=@Id", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Id", usuarioId);
                            var rows = cmd.ExecuteNonQuery();
                            if (rows == 0) throw new InvalidOperationException("Usuario no encontrado.");
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

        // 🟡 Soft delete (desactivar)
        public void SetActivo(Guid usuarioId, bool activo)
        {
            if (usuarioId == Guid.Empty) throw new ArgumentException("Id inválido.", nameof(usuarioId));
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("UPDATE [Usuario] SET Activo=@Activo WHERE Id=@Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", usuarioId);
                cmd.Parameters.AddWithValue("@Activo", activo);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
