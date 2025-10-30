using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Practica.Utils {
    /// <summary>
    /// Clase de utilidad para operaciones relacionadas con correos electrónicos.
    /// </summary>
    public class Email {
        /// <summary>
        /// Comprueba si una cadena de texto tiene el formato de un correo electrónico válido.
        /// </summary>
        /// <param name="email">El correo electrónico a validar.</param>
        /// <returns>True si el formato es válido, de lo contrario False.</returns>
        public static bool IsValidFormat(string email) {
            // Comprueba si el email es nulo, vacío o solo contiene espacios en blanco.
            if (string.IsNullOrWhiteSpace(email)) {
                return false;
            }

            try {
                // Intenta crear un objeto MailAddress. Si el formato es inválido, lanzará una excepción.
                var mailAddress = new MailAddress(email);

                // Comprueba que el dominio (la parte después de la '@') no tenga formatos inválidos,
                // como puntos dobles, o que empiece o termine con un punto.
                if (mailAddress.Host.Contains("..") || mailAddress.Host.StartsWith(".") || mailAddress.Host.EndsWith(".")) {
                    return false;
                }

                // Verifica que el dominio de nivel superior (ej: .com, .es) tenga al menos 2 caracteres.
                if (mailAddress.Host.Split('.').Last().Length < 2) {
                    return false;
                }

                // Si todas las comprobaciones son correctas, el formato es válido.
                return true;
            } catch (FormatException) {
                // Si MailAddress no puede interpretar el email, su formato es incorrecto.
                return false;
            }
        }
    }
}