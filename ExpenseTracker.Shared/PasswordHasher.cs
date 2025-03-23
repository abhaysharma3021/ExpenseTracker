using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace ExpenseTracker.Shared;

public class PasswordHasher
{
    private const int SaltSize = 16; // Size of the salt in bytes
    private const int KeySize = 32;  // Size of the hash in bytes
    private const int Iterations = 10000; // Number of iterations for the hash function

    /// <summary>
    /// Hashes a plain text password with a generated salt.
    /// </summary>
    /// <param name="password">The plain text password to hash.</param>
    /// <returns>The hashed password with the salt appended.</returns>
    public static string HashPassword(string password)
    {
        // Generate a salt
        var salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Generate the hash
        var hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: Iterations,
            numBytesRequested: KeySize
        );

        // Combine salt and hash in a single array for storage
        var hashBytes = new byte[SaltSize + KeySize];
        Buffer.BlockCopy(salt, 0, hashBytes, 0, SaltSize);
        Buffer.BlockCopy(hash, 0, hashBytes, SaltSize, KeySize);

        // Convert the combined salt and hash to a Base64 string
        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Verifies a password against a hashed password with salt.
    /// </summary>
    /// <param name="password">The plain text password to verify.</param>
    /// <param name="hashedPassword">The hashed password with salt.</param>
    /// <returns>True if the password matches; otherwise, false.</returns>
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Decode the Base64 string to get the salt and hash
        var hashBytes = Convert.FromBase64String(hashedPassword);

        // Extract the salt from the hashBytes array
        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, SaltSize);

        // Hash the input password with the extracted salt
        var hash = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: Iterations,
            numBytesRequested: KeySize
        );

        // Compare the hashed input password with the stored hash
        for (int i = 0; i < KeySize; i++)
        {
            if (hashBytes[i + SaltSize] != hash[i])
            {
                return false;
            }
        }

        return true;
    }
}
