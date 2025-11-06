using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        var pw = "admin";
        if (args.Length > 0) pw = args[0];
        var hash = PasswordHasher.CreateHash(pw);
        Console.WriteLine(hash);
    }
}

// Local copy of PasswordHasher to make this tool self-contained
public static class PasswordHasher
{
    private const int SaltSize = 16; //128 bit
    private const int HashSize = 32; //256 bit
    private const int DefaultIterations = 100000;

    public static string CreateHash(string password, int iterations = DefaultIterations)
    {
        if (password == null) throw new ArgumentNullException(nameof(password));
        using (var rng = RandomNumberGenerator.Create())
        {
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA1))
            {
                var hash = pbkdf2.GetBytes(HashSize);
                return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
            }
        }
    }
}