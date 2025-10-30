using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practica.Utils {
    /// <summary>
    /// Clase para manejar la validación y encriptación de contraseñas.
    /// </summary>
    public class Password {
        /// <summary>
        /// Comprueba si una contraseña cumple con los requisitos de seguridad.
        /// </summary>
        /// <param name="password">La contraseña a verificar.</param>
        /// <returns>True si la contraseña es segura, de lo contrario False.</returns>
        public static bool CheckPassword(string password) {
            // La contraseña no puede ser nula y debe tener al menos 12 caracteres.
            if (string.IsNullOrEmpty(password) || password.Length < 12)
                return false;

            // Define la lista de caracteres especiales permitidos.
            string specialChars = "!@#$%^&*()_[]{};:'\",.<>/?`~";

            // Comprueba que la contraseña contenga al menos:
            return password.Any(char.IsUpper)      // una letra mayúscula
                && password.Any(char.IsLower)      // una letra minúscula
                && password.Any(char.IsDigit)      // un número
                && password.Any(c => specialChars.Contains(c)); // y un carácter especial.
        }

        /// <summary>
        /// Encripta una contraseña usando el algoritmo de hash SHA256.
        /// </summary>
        /// <param name="password">La contraseña en texto plano.</param>
        /// <returns>La contraseña encriptada como una cadena en formato Base64.</returns>
        public static String EncriptPassword(String password) {
            // Crea una instancia del algoritmo SHA256.
            using (var sha256 = System.Security.Cryptography.SHA256.Create()) {
                // Convierte la contraseña de texto a un array de bytes.
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                // Calcula el hash a partir de los bytes.
                var hash = sha256.ComputeHash(bytes);
                // Convierte el hash (array de bytes) a una cadena de texto Base64 para poder guardarla.
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Verifica si una contraseña ingresada coincide con un hash almacenado.
        /// </summary>
        /// <param name="password">La contraseña en texto plano que introduce el usuario.</param>
        /// <param name="storedHash">El hash de la contraseña que está guardado en la base de datos.</param>
        /// <returns>True si las contraseñas coinciden, de lo contrario False.</returns>
        public static bool VerifyPassword(string password, string storedHash) {
            // Encripta la contraseña que el usuario acaba de ingresar.
            string hashIngresado = EncriptPassword(password);
            // Compara el nuevo hash con el que estaba guardado.
            return hashIngresado.Equals(storedHash);
        }

    }
}
