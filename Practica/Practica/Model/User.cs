using System;
using System.Collections.Generic;

namespace Practica.Model {

    public enum UserState { Active, Unactive, Blocked };

    // Mirar a futuro Notificación de Cambios (implementar la interfaz INotifyPropertyChanged)
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

        public void Login(string email, string password) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException("Email and password cannot be empty.");
            }

            if (!Utils.Password.VerifyPassword(password, this.password) || !this.Email.Equals(email)) {
                throw new InvalidOperationException("Incorrect email or password.");
            }

            this.State = UserState.Active;
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

        public void Logout() {
            if (this.State != UserState.Active) {
                throw new InvalidOperationException("Cannot log out when the user is not active.");
            }
            this.State = UserState.Unactive;
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

        public void Subscribe() {
            if (this.Is_Subscription) {
                throw new InvalidOperationException("The user is already subscribed.");
            }
            this.Is_Subscription = true;
        }

        public void Unsubscribe() {
            this.Is_Subscription = false;
        }

        public void Block() {
            if (this.State == UserState.Blocked) {
                throw new InvalidOperationException("El usuario ya se encuentra bloqueado.");
            }
            this.State = UserState.Blocked;
        }

        public void Unblock() {
            if (this.State != UserState.Blocked) {
                throw new InvalidOperationException("El usuario no está bloqueado.");
            }
            this.State = UserState.Unactive;
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