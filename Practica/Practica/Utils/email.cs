using System;
using System.Linq;
using System.Net.Mail;

namespace Practica.Utils {
    public class Email {
        public static bool IsValidFormat(string email) {
            if (string.IsNullOrWhiteSpace(email) || email.Contains(" ")) {
                return false;
            }

            try {
                var mailAddress = new MailAddress(email);

                return !mailAddress.Host.Contains("..") &&
                       mailAddress.Host.Split('.').Last().Length > 1;
            } catch (FormatException) {
                return false;
            }
        }
    }
}