using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practica_1.Utils {
    public class Password {
        public static String EncriptPassword(String password) {
            // Encriptar la contraseña usando un algoritmo de hash (SHA256)
            using (var sha256 = System.Security.Cryptography.SHA256.Create()) {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        public static bool CheckPassword(string password) {
            if (string.IsNullOrEmpty(password) || password.Length < 12) {
                return false;
            }

            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;

            string specialChars = "!@#$%^&*()_+-=[]{};:'\",.<>/?`~";

            foreach (char c in password) {
                if (char.IsUpper(c)) {
                    hasUppercase = true;
                } else if (char.IsLower(c)) {
                    hasLowercase = true;
                } else if (char.IsDigit(c)) {
                    hasDigit = true;
                } else if (specialChars.Contains(c)) {
                    hasSpecialChar = true;
                }

                if (hasUppercase && hasLowercase && hasDigit && hasSpecialChar) {
                    break;
                }
            }

            return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
        }

        public static bool VerifyPassword(string newpassword, string storedHash){
            string hashIngresado = EncriptPassword(newpassword);
            return hashIngresado.Equals(storedHash);
        }

    }
}
