using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static bool VerifyPassword(string newpassword, string storedHash){
            string hashIngresado = EncriptPassword(newpassword);
            return hashIngresado.Equals(storedHash);
        }

    }
}
