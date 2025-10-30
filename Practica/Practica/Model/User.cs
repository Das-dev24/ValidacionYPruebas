using System;
using System.Collections.Generic;

namespace Practica.Model {

    // Enumeración que define los posibles estados de un usuario.
    public enum UserState { Active, Unactive, Blocked };

    /// <summary>
    /// Representa a un usuario del sistema con sus datos y propiedades.
    /// </summary>
    public class User {

        // Identificador único del usuario.
        public int Id { get; set; }
        // Nombre del usuario.
        public string Name { get; set; }
        // Apellidos del usuario.
        public string LastName { get; set; }
        // Correo electrónico, usado también para el login.
        public string Email { get; set; }
        // Campo privado para guardar la contraseña ya encriptada.
        private string password;
        // Propiedad pública para la contraseña. Al asignarle un valor, lo encripta automáticamente.
        public string Password { get { return this.password; } set { this.password = Utils.Password.EncriptPassword(value); } }
        // Indica si el usuario tiene una suscripción de pago activa.
        public bool Is_Subscription { get; set; }
        // Indica si el usuario es un administrador con permisos especiales.
        public bool Is_superuser { get; set; }
        // Estado actual del usuario (Activo, Inactivo o Bloqueado).
        public UserState State { get; set; }
        // Fecha y hora del último inicio de sesión.
        public DateTime Last_login { get; set; }
        // Lista que contiene todas las actividades registradas por el usuario.
        public List<Activity> Activities { get; } = new List<Activity>();


        /// <summary>
        /// Constructor para crear un nuevo objeto User.
        /// </summary>
        public User(string name, string lastName, string email, string password) {
            this.Name = name;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password; // La contraseña se encripta al asignarse aquí.
            // Valores por defecto para un nuevo usuario.
            this.Is_Subscription = false;
            this.Is_superuser = false;
            this.State = UserState.Unactive; // Un usuario nuevo empieza como inactivo.
            this.Last_login = DateTime.Now;
        }

        /// <summary>
        /// Permite al usuario cambiar su contraseña de forma segura.
        /// </summary>
        public void ChangePassword(string existingPassword, string newPassword) {
            // Comprueba que ninguna de las contraseñas esté vacía.
            if (string.IsNullOrEmpty(existingPassword) || string.IsNullOrEmpty(newPassword)) {
                throw new ArgumentException("Las contraseñas no pueden estar vacías.");
            }

            // Verifica que la contraseña actual introducida coincida con la almacenada.
            if (!Utils.Password.VerifyPassword(existingPassword, this.password)) {
                throw new InvalidOperationException("La contraseña actual no es correcta.");
            }

            // Valida que la nueva contraseña cumpla con los requisitos de seguridad.
            if (!Utils.Password.CheckPassword(newPassword)) {
                throw new ArgumentException("La nueva contraseña no cumple los requisitos de seguridad.");
            }

            // Si todo es correcto, asigna la nueva contraseña (se encriptará automáticamente).
            Password = newPassword;
        }

        /// <summary>
        /// Permite cambiar los datos personales del usuario (nombre, apellidos, email).
        /// </summary>
        public void ChangeDetails(String name, String lastName, String email) {
            // Valida que los campos no estén vacíos.
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email)) {
                throw new ArgumentException("El nombre, apellidos y email no pueden estar vacíos.");
            }
            // Valida que el formato del email sea correcto.
            if (!Utils.Email.IsValidFormat(email)) {
                throw new ArgumentException("El formato del email no es válido.");
            }

            // Si las validaciones son correctas, actualiza los datos.
            Name = name;
            LastName = lastName;
            Email = email;
        }

        /// <summary>
        /// Sobrescribe el método Equals para comparar dos objetos User por sus propiedades.
        /// </summary>
        public override bool Equals(object obj) {
            return obj is User user &&
                   Id == user.Id &&
                   Email == user.Email;
        }

        /// <summary>
        /// Sobrescribe el método GetHashCode para generar un código hash basado en las propiedades.
        /// Es necesario al sobrescribir Equals para el correcto funcionamiento en colecciones.
        /// </summary>
        public override int GetHashCode() {
            // Calcula un código hash combinando el Id y el Email del usuario.
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