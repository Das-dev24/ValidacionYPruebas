using Practica.Model;
using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace Practica.Utils {
    public class Email {

        public static bool IsValidFormat(string email) {
            if (string.IsNullOrWhiteSpace(email)) {
                return false;
            }

            try {
                var mailAddress = new MailAddress(email);
                return true;
            } catch (FormatException) {
                return false;
            }
        }
    }
}