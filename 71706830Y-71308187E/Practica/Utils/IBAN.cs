using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practica.Utils {
    public class IBAN {
        public static bool validar_IBAN(string iban) {
            if (string.IsNullOrWhiteSpace(iban))
                return false;

            iban = iban.Replace(" ", "").ToUpper();

            if (!Regex.IsMatch(iban, @"^ES\d{22}$"))
                return false;

            // Mover los 4 primeros caracteres al final
            string reformulado = iban.Substring(4) + iban.Substring(0, 4);

            // Convertir letras a números (A=10, B=11, ..., Z=35)
            string numerico = "";
            foreach (char c in reformulado) {
                if (char.IsLetter(c))
                    numerico += (c - 'A' + 10).ToString();
                else
                    numerico += c;
            }

            // Validar con módulo 97
            return crear_Modulo97(numerico) == 1;
        }

        private static int crear_Modulo97(string input) {
            string fragmento = "";
            foreach (char c in input) {
                fragmento += c;
                if (fragmento.Length >= 9) {
                    int num = int.Parse(fragmento);
                    fragmento = (num % 97).ToString();
                }
            }
            return int.Parse(fragmento) % 97;
        }
    }
}
