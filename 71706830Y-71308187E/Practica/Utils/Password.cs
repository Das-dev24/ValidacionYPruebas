using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practica.Utils {
    public class Password {
        public static bool CheckPassword(string password) {
            if (string.IsNullOrEmpty(password) || password.Length < 12)
                return false;

            string specialChars = "!@#$%^&*()_[]{};:'\",.<>/?`~";

            return password.Any(char.IsUpper)
                && password.Any(char.IsLower)
                && password.Any(char.IsDigit)
                && password.Any(c => specialChars.Contains(c));
        }

        public static String EncriptPassword(String password) {
            // Encriptar la contraseña usando un algoritmo de hash (SHA256)
            using (var sha256 = System.Security.Cryptography.SHA256.Create()) {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string storedHash){
            string hashIngresado = EncriptPassword(password);
            return hashIngresado.Equals(storedHash);
        }

    }
}
