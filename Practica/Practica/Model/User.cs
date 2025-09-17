using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica_1.Utils;

namespace Practica_1.Model
{
    public class User
    {
        private int id;
        private string name;
        private string lastName;
        private string email;
        private string password;
        private bool subscription;
        private bool is_superuser;
        private bool is_active;
        private DateTime last_login;

        public User(int id, string name, string lastName, string email, string password, bool subscription)
        {
            id = id;
            name = name;
            lastName = lastName;
            email = email;
            password = password;
            subscription = subscription;
        }

        public String Name { get { return this.name; } set { this.name = value;  } }

        public String LastName { get { return this.lastName; } set { this.lastName = value; } }

        public String Password { set { this.password = EncriptPassword.EncriptPasswordMethod(value); } }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   id == user.id &&
                   name == user.name &&
                   lastName == user.lastName &&
                   email == user.email &&
                   password == user.password &&
                   subscription == user.subscription;
        }

        public override int GetHashCode()
        {
            int hashCode = 1427793515;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            hashCode = hashCode * -1521134295 + subscription.GetHashCode();
            return hashCode;
        }
    }
}
