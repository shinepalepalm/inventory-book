using InventoryBook.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace InventoryBook.BLL.Services.Implementations
{
    public class CryptographerService : ICryptographerService
    {
        public byte[] Encrypt(string input, out byte[] salt)
        {
            var random = new Random();
            var saltSize = random.Next(4, 8);
            salt = new byte[saltSize];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Encrypt(input, salt);
        }

        public byte[] Encrypt(string input, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 16);
        }
    }
}
