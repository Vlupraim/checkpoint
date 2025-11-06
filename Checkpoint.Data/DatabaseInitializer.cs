using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Checkpoint.Core.Security;
using Hasher = Checkpoint.Core.Security.PasswordHasher;

namespace Checkpoint.Data
{
 public static class DatabaseInitializer
 {
 private static readonly string LogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dbinit.log");

 // Busca recursos embebidos schema.sql y seed.sql y los ejecuta si la BD no existe o está vacía.
 public static void EnsureDatabase()
 {
 // limpiar log anterior
 TryWriteLog("--- DatabaseInitializer started at " + DateTime.Now.ToString("s") + " ---\n");

 var csMaster = global::System.Configuration.ConfigurationManager.ConnectionStrings["AppMaster"]?.ConnectionString;
 var csApp = global::System.Configuration.ConfigurationManager.ConnectionStrings["App"]?.ConnectionString;
 if (string.IsNullOrEmpty(csApp)) throw new InvalidOperationException("Cadena 'App' no configurada.");

 TryWriteLog("Using App connection string: " + (csApp ?? "(null)"));
 TryWriteLog("Using AppMaster connection string: " + (csMaster ?? "(null)"));

 // Si se proporciona AppMaster se usa para crear la base si no existe
 if (!string.IsNullOrEmpty(csMaster))
 {
 // Ejemplo: crear la base de datos si no existe (solo para entornos de desarrollo con localdb)
 try
 {
 var builder = new SqlConnectionStringBuilder(csApp);
 var dbName = builder.InitialCatalog;
 TryWriteLog("Ensuring database exists: " + dbName);
 var masterBuilder = new SqlConnectionStringBuilder(csMaster);
 using (var conn = new SqlConnection(csMaster))
 {
 conn.Open();
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = $"IF DB_ID('{dbName}') IS NULL CREATE DATABASE [{dbName}]";
 cmd.ExecuteNonQuery();
 }
 }
 TryWriteLog("Database creation check executed.");
 }
 catch (Exception ex)
 {
 // no bloquear la app, solo log
 Console.WriteLine("No se pudo crear la base: " + ex.Message);
 TryWriteLog("No se pudo crear la base: " + ex.Message + "\n" + ex.StackTrace);
 }
 }

 // Ejecutar schema y seed: primero intentar recurso embebido, si no existe, buscar archivo en disco (ruta relativa)
 ExecuteSqlResourceOrFile("Checkpoint.Data.Scripts.schema.sql", new[] { "Checkpoint.Data", "Scripts", "schema.sql" }, csApp);
 ExecuteSqlResourceOrFile("Checkpoint.Data.Scripts.seed.sql", new[] { "Checkpoint.Data", "Scripts", "seed.sql" }, csApp);

 // After seed, ensure admin password hash matches expected (useful in dev)
 try
 {
 EnsureAdminPassword(csApp);
 }
 catch (Exception ex)
 {
 TryWriteLog("EnsureAdminPassword error: " + ex.Message + "\n" + ex.StackTrace);
 }

 // Informar al desarrollador
 Console.WriteLine("DatabaseInitializer: ejecución completada.");
 TryWriteLog("DatabaseInitializer: ejecución completada.\n");
 }

 private static void EnsureAdminPassword(string connectionString)
 {
 if (string.IsNullOrEmpty(connectionString)) return;
 try
 {
 using (var conn = new SqlConnection(connectionString))
 using (var cmd = new SqlCommand("SELECT Id, PasswordHash FROM [Usuario] WHERE Email = @email", conn))
 {
 cmd.Parameters.AddWithValue("@email", "admin@local");
 conn.Open();
 using (var rdr = cmd.ExecuteReader())
 {
 if (rdr.Read())
 {
 var id = rdr.GetGuid(0);
 var stored = rdr.IsDBNull(1) ? null : rdr.GetString(1);
 // If stored is empty or doesn't verify for 'admin', update it to a fresh hash for password 'admin'
 var ok = false;
 if (!string.IsNullOrEmpty(stored))
 {
 try { var newHash = PasswordHasher.CreateHash("admin"); } catch { ok = false; }
 }
 if (!ok)
 {
 var newHash = PasswordHasher.CreateHash("admin");
 TryWriteLog("Updating admin password hash to a new generated value.");
 rdr.Close();
 using (var upd = new SqlCommand("UPDATE [Usuario] SET PasswordHash = @ph WHERE Id = @id", conn))
 {
 upd.Parameters.AddWithValue("@ph", newHash);
 upd.Parameters.AddWithValue("@id", id);
 upd.ExecuteNonQuery();
 }
 TryWriteLog("Admin password hash updated.");
 }
 else
 {
 TryWriteLog("Admin password hash already valid.");
 }
 }
 else
 {
 TryWriteLog("Admin user not found (email=admin@local).");
 }
 }
 }
 }
 catch (Exception ex)
 {
 TryWriteLog("EnsureAdminPassword exception: " + ex.Message + "\n" + ex.StackTrace);
 }
 }

 private static void ExecuteSqlResourceOrFile(string resourceName, string[] relativePathParts, string connectionString)
 {
 var asm = Assembly.GetExecutingAssembly();
 try
 {
 using (var stream = asm.GetManifestResourceStream(resourceName))
 {
 if (stream != null)
 {
 using (var reader = new StreamReader(stream))
 {
 var sql = reader.ReadToEnd();
 TryWriteLog($"Found embedded resource: {resourceName}, executing...");
 ExecuteSqlScript(sql, connectionString);
 Console.WriteLine($"DatabaseInitializer: ejecutado recurso embebido {resourceName}.");
 TryWriteLog($"DatabaseInitializer: ejecutado recurso embebido {resourceName}.");
 return;
 }
 }
 }

 // Si no hay recurso embebido, buscar archivo en disco subiendo por directorios desde el bin
 var relPath = Path.Combine(relativePathParts);
 TryWriteLog($"Looking for script file: {relPath}");
 var file = FindFileInParents(AppDomain.CurrentDomain.BaseDirectory, relPath,6);
 if (!string.IsNullOrEmpty(file))
 {
 TryWriteLog($"Found script file at: {file}");
 var sql = File.ReadAllText(file);
 ExecuteSqlScript(sql, connectionString);
 Console.WriteLine($"DatabaseInitializer: ejecutado script desde archivo {file}.");
 TryWriteLog($"DatabaseInitializer: ejecutado script desde archivo {file}.");
 return;
 }

 Console.WriteLine($"DatabaseInitializer: no se encontró ni recurso ni archivo para {resourceName} (buscado '{relPath}').");
 TryWriteLog($"DatabaseInitializer: no se encontró ni recurso ni archivo para {resourceName} (buscado '{relPath}').");
 }
 catch (Exception ex)
 {
 Console.WriteLine($"DatabaseInitializer: error ejecutando {resourceName}: {ex.Message}");
 TryWriteLog($"DatabaseInitializer: error ejecutando {resourceName}: {ex.Message}\n{ex.StackTrace}");
 }
 }

 private static string FindFileInParents(string startDirectory, string relativePath, int maxLevels)
 {
 try
 {
 var dir = new DirectoryInfo(startDirectory);
 for (int i =0; i < maxLevels && dir != null; i++)
 {
 var candidate = Path.Combine(dir.FullName, relativePath);
 if (File.Exists(candidate)) return candidate;
 // also try directly filename in this folder
 var fileNameOnly = Path.GetFileName(relativePath);
 var candidate2 = Path.Combine(dir.FullName, fileNameOnly);
 if (File.Exists(candidate2)) return candidate2;
 dir = dir.Parent;
 }
 }
 catch (Exception ex)
 {
 // ignore
 TryWriteLog("FindFileInParents error: " + ex.Message);
 }
 return null;
 }

 private static void ExecuteSqlScript(string sql, string connectionString)
 {
 if (string.IsNullOrWhiteSpace(sql)) return;

 // SQL Server tools use 'GO' to separate batches; SqlCommand does not understand GO, so split by lines with only GO
 var batches = Regex.Split(sql, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

 using (var conn = new SqlConnection(connectionString))
 {
 conn.Open();
 TryWriteLog("Connected to database for script execution.");
 int batchIndex =0;
 foreach (var batch in batches)
 {
 batchIndex++;
 var trimmed = batch.Trim();
 if (string.IsNullOrEmpty(trimmed)) continue;
 TryWriteLog($"Executing batch #{batchIndex} (length={trimmed.Length})...");
 using (var cmd = conn.CreateCommand())
 {
 cmd.CommandText = trimmed;
 cmd.CommandType = System.Data.CommandType.Text;
 try
 {
 cmd.ExecuteNonQuery();
 TryWriteLog($"Batch #{batchIndex} executed successfully.");
 }
 catch (Exception ex)
 {
 // Log and rethrow with context so caller sees useful message
 var message = $"Error ejecutando batch de SQL: {ex.Message}\nBatch:\n{(trimmed.Length >400 ? trimmed.Substring(0,400) + "..." : trimmed)}";
 Console.WriteLine(message);
 TryWriteLog(message + "\n" + ex.StackTrace);
 throw new InvalidOperationException(message, ex);
 }
 }
 }
 }
 }

 private static void TryWriteLog(string message)
 {
 try
 {
 var entry = $"[{DateTime.Now:s}] {message}{Environment.NewLine}";
 // append to file
 File.AppendAllText(LogFile, entry);
 // also write to console (visible in VS Output when debugging)
 Console.WriteLine(entry);
 }
 catch
 {
 // ignore logging failures
 }
 }
 }
}
