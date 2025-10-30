using System;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Practica.Utils {
    public class Email {
        public static bool IsValidFormat(string email) {
            if (string.IsNullOrWhiteSpace(email)) {
                return false;
            }

            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase)) {
                return false;
            }

            var mailAddress = new MailAddress(email);
            if (mailAddress.Host.Contains("..") || mailAddress.Host.StartsWith(".") || mailAddress.Host.EndsWith(".")) {
                return false;
            }

            if (mailAddress.Host.Split('.').Last().Length < 2) {
                return false;
            }

            return true;
        }
    }
}