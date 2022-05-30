﻿using System.Security.Cryptography;

namespace JwtAuthentication.Utils
{
    public static class EncryptionUtils
    {
        public static byte[] GenerateSalt(int length)
        {
            var bytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return bytes;
        }

        public static byte[] GenerateHash(byte[] password, byte[] salt, int iterations, int length)
        {
            using var deriveBytes = new Rfc2898DeriveBytes(password, salt, iterations);
            return deriveBytes.GetBytes(length);
        }
    }
}
