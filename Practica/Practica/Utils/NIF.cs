using System;

namespace Practica.Utils {
    public class NIF {
        public static bool validar_NIF(string nif) {
            string nifMayusculas = nif?.ToUpper();

            if (string.IsNullOrWhiteSpace(nifMayusculas) || nifMayusculas.Length != 9)
                return false;

            string numeros = nifMayusculas.Substring(0, 8);
            char letra = nifMayusculas[8];

            if (!int.TryParse(numeros, out int numero))
                return false;

            const string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
            char letraCorrecta = letras[numero % 23];

            return letra == letraCorrecta;
        }
    }
}