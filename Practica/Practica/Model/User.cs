using System;
using System.Collections.Generic;

namespace Practica.Model {

    public enum UserState { Active, Unactive, Blocked };

    public class User {

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        private string password;
        public string Password { get { return this.password; } set { this.password = Utils.Password.EncriptPassword(value); } }
        public bool Is_Subscription { get; set; }
        public bool Is_superuser { get; set; }
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
            this.State = UserState.Unactive;
            this.Last_login = DateTime.Now;
        }

        public void ChangePassword(string existingPassword, string newPassword) {
            if (string.IsNullOrEmpty(existingPassword) || string.IsNullOrEmpty(newPassword)) {
                throw new ArgumentException("Las contraseñas no pueden estar vacías.");
            }

            if (!Utils.Password.VerifyPassword(existingPassword, this.password)) {
                throw new InvalidOperationException("La contraseña actual no es correcta.");
            }

            if (!Utils.Password.CheckPassword(newPassword)) {
                throw new ArgumentException("La nueva contraseña no cumple los requisitos de seguridad.");
            }

            Password = newPassword;
        }

        public void ChangeDetails(String name, String lastName, String email) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email)) {
                throw new ArgumentException("El nombre, apellidos y email no pueden estar vacíos.");
            }
            // Ahora lanza una excepción si el formato es inválido
            if (!Utils.Email.IsValidFormat(email)) {
                throw new ArgumentException("El formato del email no es válido.");
            }

            // Si todo está bien, actualiza las propiedades
            Name = name;
            LastName = lastName;
            Email = email;
        }

        public override bool Equals(object obj) {
            return obj is User user &&
                    Id == user.Id &&
                    Name == user.Name &&
                    LastName == user.LastName &&
                    Email == user.Email &&
                    password == user.password;
        }

        public override int GetHashCode() {
            int hashCode = 1734723601;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LastName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            return hashCode;
        }
    }
}