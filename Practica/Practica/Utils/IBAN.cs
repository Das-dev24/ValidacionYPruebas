using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practica.Utils {
    /// <summary>
    /// Clase para validar un número de cuenta bancaria internacional (IBAN).
    /// </summary>
    public class IBAN {
        /// <summary>
        /// Valida si una cadena de texto corresponde a un IBAN español válido.
        /// </summary>
        /// <param name="iban">El IBAN a validar.</param>
        /// <returns>True si el IBAN es válido, de lo contrario False.</returns>
        public static bool validar_IBAN(string iban) {
            // Si el IBAN está vacío o solo contiene espacios, no es válido.
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            // Limpia el IBAN: quita espacios y lo convierte a mayúsculas.
            iban = iban.Replace(" ", "").ToUpper();

            // Comprueba con una expresión regular si el formato es correcto (ES + 22 dígitos).
            if (!Regex.IsMatch(iban, @"^ES\d{22}$"))
                return false;

            // --- Algoritmo de validación del IBAN ---

            // 1. Mueve los 4 primeros caracteres (código de país y dígitos de control) al final.
            string reformulado = iban.Substring(4) + iban.Substring(0, 4);

            // 2. Convierte las letras a su valor numérico (A=10, B=11, ..., S=28, etc.).
            string numerico = "";
            foreach (char c in reformulado) {
                if (char.IsLetter(c)) {
                    // Si es una letra, calcula su valor.
                    numerico += (c - 'A' + 10).ToString();
                } else {
                    // Si es un número, lo añade directamente.
                    numerico += c;
                }
            }

            // 3. Calcula el módulo 97 del número gigante. Si el resto es 1, el IBAN es válido.
            return crear_Modulo97(numerico) == 1;
        }

        /// <summary>
        /// Calcula el módulo 97 de un número muy grande representado como una cadena de texto.
        /// Se hace por fragmentos para evitar desbordamiento numérico.
        /// </summary>
        /// <param name="input">El número como cadena.</param>
        /// <returns>El resultado de la operación módulo 97.</returns>
        private static int crear_Modulo97(string input) {
            string fragmento = "";
            // Recorre cada dígito del número.
            foreach (char c in input) {
                // Añade el dígito actual al fragmento que se está procesando.
                fragmento += c;
                // Si el fragmento es lo suficientemente largo (9 dígitos caben en un 'int')...
                if (fragmento.Length >= 9) {
                    // ...lo convierte a número y calcula su módulo 97.
                    int num = int.Parse(fragmento);
                    // El resto se convierte en el inicio del siguiente fragmento.
                    fragmento = (num % 97).ToString();
                }
            }
            // Calcula el módulo del último fragmento restante.
            return int.Parse(fragmento) % 97;
        }
    }
}