using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Practica.Utils;

namespace Practica.Model {

    public enum UserState { Active, Unactive, Blocked };

    public class User {

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        private string password;
        public string Password { set { this.password = Utils.Password.EncriptPassword(value); } }
        public bool Is_Subscription { get; set; }
        public bool Is_superuser { get; set; }
        public bool Is_active { get; set; }
        public UserState State { get; set; }
        public DateTime Last_login { get; set; }
        public List<Activity> Activities { get; } = new List<Activity>();


        public User(string name, string lastName, string email, string password) {
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
            this.Is_Subscription = false;
            this.Is_superuser = false;
            this.Is_active = false;
            this.State = UserState.Unactive;
            this.Last_login = DateTime.Now;
        }
        public User() {
            this.Id = 1;
            this.Name = "Admin";
            this.LastName = "";
            this.Email = "example@example.com";
            this.Password = "Admin_123456";
            this.Is_Subscription = false;
            this.Is_superuser = true;
            this.Is_active = false;
            this.State = UserState.Unactive;
            this.Last_login = DateTime.Now;
        }

        public bool Register(string name, string lastName, string email, string password) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ) { 
                return false;
            }
            if (!Utils.Password.CheckPassword(password) || email != Email) {
                //Modificar luego el tema del email para comparar con la bd
                return false;
            }

            Name = name;
            LastName = lastName;
            Email = email;
            Password = password;

            return true;
        }

        public bool Login(string email, string password) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                return false;
            }

            if (Utils.Password.VerifyPassword(password, this.password) && this.Email.Equals(email)) {
                this.State = UserState.Active;
                this.Last_login = DateTime.Now;
                return true;
            }

            return false;
        }

        public bool ChangePassword(string existingPassword, string newPassword) {
            if (string.IsNullOrEmpty(existingPassword) || string.IsNullOrEmpty(newPassword)){
                return false;
            }
            
            if (!Utils.Password.VerifyPassword(existingPassword, this.password) && !Utils.Password.CheckPassword(newPassword)) {
                return false;
            }

            Password = newPassword;
            return true;
        }

        public bool Logout() {
            if (this.State == UserState.Active) {
                this.State = UserState.Unactive;
                return true;
            }
            return false;
        }

        public bool ChangeDetails(String name, String lastName, String email) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email)) {
                return false;
            }

            //Crear funcion en utils que compruebe las cuentas de correo electronico
            //if(!checkEmail(email))
            Name = name;
            LastName = lastName;
            Email = email;

            return false;
        }

        public bool Subscribe() {
            if (!this.Is_Subscription) {
                this.Is_Subscription = true;
                return true;
            }
            return false;
        }

        public void Unsubscribe() {
            this.Is_Subscription = false;
        }

        public override bool Equals(object obj) {
            return obj is User user &&
                   Id == user.Id &&
                   Name == user.Name &&
                   LastName == user.LastName &&
                   Email == user.Email &&
                   password == user.password &&
                   Is_Subscription == user.Is_Subscription &&
                   Is_superuser == user.Is_superuser &&
                   Is_active == user.Is_active &&
                   State == user.State &&
                   Last_login == user.Last_login &&
                   EqualityComparer<List<Activity>>.Default.Equals(Activities, user.Activities);
        }

        public override int GetHashCode() {
            int hashCode = 1460853562;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            hashCode = hashCode * -1521134295 + Is_Subscription.GetHashCode();
            hashCode = hashCode * -1521134295 + Is_superuser.GetHashCode();
            hashCode = hashCode * -1521134295 + Is_active.GetHashCode();
            hashCode = hashCode * -1521134295 + State.GetHashCode();
            hashCode = hashCode * -1521134295 + Last_login.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Activity>>.Default.GetHashCode(Activities);
            return hashCode;
        }
    }
}
