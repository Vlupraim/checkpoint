using System;
using System.Security.Cryptography;

namespace Checkpoint.Core.Security
{
 // PBKDF2 password hasher: format => {iterations}.{saltBase64}.{hashBase64}
 public static class PasswordHasher
 {
 private const int SaltSize =16; //128 bit
 private const int HashSize =32; //256 bit
 private const int DefaultIterations =100000;

 public static string CreateHash(string password, int iterations = DefaultIterations)
 {
 if (password == null) throw new ArgumentNullException(nameof(password));
 using (var rng = new RNGCryptoServiceProvider())
 {
 var salt = new byte[SaltSize];
 rng.GetBytes(salt);
 using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
 {
 var hash = pbkdf2.GetBytes(HashSize);
 return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
 }
 }
 }

 public static bool VerifyHash(string password, string storedHash)
 {
 if (password == null) throw new ArgumentNullException(nameof(password));
 if (string.IsNullOrEmpty(storedHash)) return false;
 // expected format iterations.salt.hash
 var parts = storedHash.Split('.');
 if (parts.Length !=3) return false;
 if (!int.TryParse(parts[0], out int iterations)) return false;
 // tolerate accidental whitespace/newlines in stored values
 var saltB64 = parts[1]?.Trim();
 var hashB64 = parts[2]?.Trim();
 byte[] salt;
 byte[] hash;
 try
 {
 salt = Convert.FromBase64String(saltB64);
 hash = Convert.FromBase64String(hashB64);
 }
 catch (FormatException)
 {
 // stored value not a valid base64 -> treat as invalid credentials
 return false;
 }
 using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
 {
 var computed = pbkdf2.GetBytes(hash.Length);
 return AreEqualSlow(computed, hash);
 }
 }

 private static bool AreEqualSlow(byte[] a, byte[] b)
 {
 if (a.Length != b.Length) return false;
 var diff =0;
 for (int i =0; i < a.Length; i++) diff |= a[i] ^ b[i];
 return diff ==0;
 }
 }
}
