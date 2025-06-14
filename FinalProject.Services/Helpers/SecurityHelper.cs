﻿using System.Security.Cryptography;
using System.Text;

namespace FinalProject.Services.Helpers
{
    public static class SecurityHelper
    {
        // DEFF PASS FOR EACH USER IS - pass123
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToUpper();
            }
        }
    }
}
