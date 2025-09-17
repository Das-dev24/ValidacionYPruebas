using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Practica_1.Utils;

namespace Practica_1.Model {
    public class User {
        private int id;
        private string name;
        private string lastName;
        private string email;
        private string password;
        private bool subscription;
        private bool is_superuser;
        private bool is_active;
        private DateTime last_login;

        public User(string name, string lastName, string email, string password) {
            this.id = 1;
            this.name = name;
            this.lastName = lastName;
            this.email = email;
            this.password = Utils.Password.EncriptPassword(password); ;
            this.subscription = false;
            this.is_superuser = false;
            this.is_active = false;
            this.last_login = DateTime.Now;
        }

        public User() {
            this.id = 1;
            this.name = "Pedro";
            this.lastName = "Gonzalez";
            this.email = "example@example.com";
            this.password = Utils.Password.EncriptPassword("admin");
            this.subscription = true;
            this.is_superuser = true;
            this.is_active = false;
            this.last_login = DateTime.Now;
        }

        public int Id { get { return this.id; } set { this.id = value; } }
        public String Name { get { return this.name; } set { this.name = value;  } }
        public String LastName { get { return this.lastName; } set { this.lastName = value; } }
        public String Email { get { return this.email; } set { this.email = value; } }
        public String Password { set { this.password = Utils.Password.EncriptPassword(value); } }
        public bool Subscription { get { return this.subscription; } set { this.subscription = value; } }
        public bool Is_superuser { get { return this.is_superuser; } set { this.is_superuser = value; } }
        public bool Is_active { get { return this.is_active; } set { this.is_active = value; } }
        public DateTime Last_login { get { return this.last_login; } set { this.last_login = value; } }

        public bool Login (String email, String password){
            if (email == null | password == null){
                return false;
            } else if (Utils.Password.VerifyPassword(password, this.password) && Email.Equals(email)){
                return true;
            } else {
                return false;
            }
        }

        public bool ChangePassword (String exist_password, String new_password){
            if (exist_password == null | new_password == null){
                return false;
            } else if (Utils.Password.VerifyPassword(exist_password, this.password)){
                Password = new_password;
                return true; 
            } else {
                return false;
            }
        }

        public override bool Equals(object obj) {
            return obj is User user &&
                   id == user.id &&
                   name == user.name &&
                   lastName == user.lastName &&
                   email == user.email &&
                   password == user.password &&
                   subscription == user.subscription &&
                   is_superuser == user.is_superuser &&
                   is_active == user.is_active &&
                   last_login == user.last_login &&
                   Id == user.Id &&
                   Name == user.Name &&
                   LastName == user.LastName &&
                   Email == user.Email &&
                   Subscription == user.Subscription &&
                   Is_superuser == user.Is_superuser &&
                   Is_active == user.Is_active &&
                   Last_login == user.Last_login;
        }

        public override int GetHashCode() {
            int hashCode = 58874072;
            hashCode = hashCode * -1521134295 + id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            hashCode = hashCode * -1521134295 + subscription.GetHashCode();
            hashCode = hashCode * -1521134295 + is_superuser.GetHashCode();
            hashCode = hashCode * -1521134295 + is_active.GetHashCode();
            hashCode = hashCode * -1521134295 + last_login.GetHashCode();
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + Subscription.GetHashCode();
            hashCode = hashCode * -1521134295 + Is_superuser.GetHashCode();
            hashCode = hashCode * -1521134295 + Is_active.GetHashCode();
            hashCode = hashCode * -1521134295 + Last_login.GetHashCode();
            return hashCode;
        }
    }
}
