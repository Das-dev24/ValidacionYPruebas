using System;
using System.Linq; // Para usar métodos como .Last() en colecciones.
using System.Net.Mail; // Contiene la clase MailAddress para analizar direcciones de correo.
using System.Text.RegularExpressions; // Para trabajar con expresiones regulares.

namespace Practica.Utils {
    /// <summary>
    /// Clase para validar formatos de correo electrónico.
    /// </summary>
    public class Email {
        /// <summary>
        /// Comprueba si una cadena de texto tiene un formato de email válido.
        /// </summary>
        /// <param name="email">El correo electrónico a validar.</param>
        /// <returns>True si el formato es válido, de lo contrario False.</returns>
        public static bool IsValidFormat(string email) {
            // Si el email está vacío, nulo o solo contiene espacios en blanco, no es válido.
            if (string.IsNullOrWhiteSpace(email)) {
                return false;
            }

            // Define un patrón de expresión regular para la estructura básica: texto@texto.texto
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            // Comprueba si el email cumple con el patrón básico.
            if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase)) {
                return false;
            }

            // Utiliza la clase MailAddress de .NET para un análisis más detallado.
            var mailAddress = new MailAddress(email);
            // Comprueba si el dominio (la parte después de @) tiene formatos inválidos
            // como puntos seguidos ("..") o si empieza o termina con un punto.
            if (mailAddress.Host.Contains("..") || mailAddress.Host.StartsWith(".") || mailAddress.Host.EndsWith(".")) {
                return false;
            }

            // Comprueba si el dominio de nivel superior (ej: .com, .es) tiene al menos 2 caracteres.
            if (mailAddress.Host.Split('.').Last().Length < 2) {
                return false;
            }

            // Si ha pasado todas las comprobaciones, el formato es válido.
            return true;
        }
    }
}