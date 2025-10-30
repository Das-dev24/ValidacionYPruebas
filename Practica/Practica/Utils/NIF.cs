using System;

namespace Practica.Utils {
    /// <summary>
    /// Clase para validar un Número de Identificación Fiscal (NIF) español.
    /// </summary>
    public class NIF {
        /// <summary>
        /// Comprueba si una cadena de texto es un NIF válido.
        /// </summary>
        /// <param name="nif">El NIF a validar.</param>
        /// <returns>True si el NIF es válido, de lo contrario False.</returns>
        public static bool validar_NIF(string nif) {
            // Convierte el NIF a mayúsculas para unificar el formato.
            string nifMayusculas = nif?.ToUpper();

            // Comprueba si el NIF es nulo, está vacío o no tiene exactamente 9 caracteres.
            if (string.IsNullOrWhiteSpace(nifMayusculas) || nifMayusculas.Length != 9)
                return false;

            // Separa la parte numérica (los primeros 8 caracteres) y la letra.
            string numeros = nifMayusculas.Substring(0, 8);
            char letra = nifMayusculas[8];

            // Intenta convertir la parte numérica a un entero. Si no se puede, el NIF no es válido.
            if (!int.TryParse(numeros, out int numero))
                return false;

            // Cadena de letras para el cálculo del NIF. La posición de cada letra es clave.
            const string letras = "TRWAGMYFPDXBNJZSQVHLCKE";

            // Calcula cuál debería ser la letra correcta usando el algoritmo del módulo 23.
            char letraCorrecta = letras[numero % 23];

            // Devuelve true solo si la letra del NIF coincide con la letra calculada.
            return letra == letraCorrecta;
        }
    }
}