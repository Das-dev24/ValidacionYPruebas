using Database;
using Datos.Seeder;
using Practica.Model;
using Practica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Datos {
    /// <summary>
    /// Implementación de la capa de datos que maneja la lógica de negocio y el acceso a los datos en memoria.
    /// </summary>
    public class CapaDatos : ICapaDatos {

        // Lista para almacenar los usuarios en memoria (simulando una base de datos).
        private List<User> Users = new List<User>();
        // Lista para almacenar las actividades en memoria.
        private List<Activity> Activities = new List<Activity>();

        /// <summary>
        /// Constructor de la clase. Llama al método para inicializar los datos de prueba.
        /// </summary>
        public CapaDatos() {
            SeedData();
        }

        /// <summary>
        /// Inicializa la base de datos con datos de prueba si está vacía.
        /// </summary>
        private void SeedData() {
            var seeder = new DatabaseSeeder(this); // Crea una instancia del seeder.
            seeder.Seed(); // Ejecuta el método para poblar los datos.
        }

        /// <summary>
        /// Devuelve la lista completa de usuarios.
        /// </summary>
        public List<User> GetAllUsers() {
            return Users;
        }

        /// <summary>
        /// Guarda un usuario en la lista si no existe previamente.
        /// </summary>
        /// <returns>True si se guardó, false si ya existía.</returns>
        public bool GuardaUser(User u) {
            if (Users.Contains(u)) {
                return false; // El usuario ya existe.
            } else {
                Users.Add(u); // Añade el nuevo usuario.
                return true;
            }
        }

        /// <summary>
        /// Elimina un usuario y todas sus actividades asociadas.
        /// </summary>
        /// <returns>True si se eliminó, false si no se encontró.</returns>
        public bool DeleteUser(User userToDelete) {
            if (userToDelete == null) {
                return false; // No se puede borrar un usuario nulo.
            }

            // Elimina todas las actividades asociadas a este usuario.
            Activities.RemoveAll(activity => activity.GetUser() == userToDelete);

            // Elimina el usuario de la lista de usuarios.
            return Users.Remove(userToDelete);
        }

        /// <summary>
        /// Busca y devuelve un usuario por su dirección de email.
        /// </summary>
        public User LeeUser(string email) {
            // Usa FirstOrDefault para encontrar el primer usuario que coincida con el email (ignorando mayúsculas/minúsculas).
            return Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Busca y devuelve un usuario por su ID.
        /// </summary>
        public User LeeUserPorId(int userId) {
            return Users.FirstOrDefault(u => u.Id == userId);
        }

        /// <summary>
        /// Devuelve el número total de usuarios registrados.
        /// </summary>
        public int NumUsers() {
            return Users.Count;
        }

        /// <summary>
        /// Valida las credenciales de un usuario.
        /// </summary>
        /// <returns>True si las credenciales son correctas.</returns>
        public bool ValidaUser(string email, string password) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException("El email o la contraseña no pueden estar vacíos.");
            }

            User usuario = LeeUser(email); // Busca al usuario por su email.

            // Comprueba si el usuario existe y si la contraseña es correcta.
            if (usuario == null || !Password.VerifyPassword(password, usuario.Password)) {
                throw new InvalidOperationException("La combinación de email y contraseña es incorrecta.");
            }

            // Si es válido, actualiza el estado y la fecha del último login.
            usuario.State = UserState.Active;
            usuario.Last_login = DateTime.Now;
            return true;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema con validaciones previas.
        /// </summary>
        public void Register(string name, string lastName, string email, string password, bool isSuperUser = false) {
            // Validaciones de los datos de entrada.
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentException("Todos los campos son obligatorios.");
            }
            if (LeeUser(email) != null) {
                throw new ArgumentException("El correo electrónico ya está en uso.");
            }
            if (!Email.IsValidFormat(email)) {
                throw new ArgumentException("El formato del correo electrónico no es válido.");
            }
            if (!Password.CheckPassword(password)) {
                throw new ArgumentException("La contraseña no cumple con los requisitos de seguridad.");
            }

            // Crea el nuevo objeto User.
            User usuario = new User(name, lastName, email, password);
            usuario.Id = NumUsers() + 1; // Asigna un ID simple.
            usuario.Is_superuser = isSuperUser;

            // Guarda el nuevo usuario.
            GuardaUser(usuario);
        }

        /// <summary>
        /// Obtiene todas las actividades de un usuario específico por su ID.
        /// </summary>
        public List<Activity> GetActivitiesForUser(int userId) {
            // Filtra la lista de actividades por el ID del usuario.
            return Activities.Where(a => a.GetUser() != null && a.GetUser().Id == userId).ToList();
        }

        /// <summary>
        /// Guarda una nueva actividad en la lista.
        /// </summary>
        /// <returns>True si se guardó, false si ya existía.</returns>
        public bool GuardaActivity(Activity e) {
            if (Activities.Contains(e)) {
                return false; // La actividad ya existe.
            } else {
                e.Id = (Activities.Count + 1); // Asigna un nuevo ID.
                Activities.Add(e); // Añade la actividad a la lista.
                return true;
            }
        }

        /// <summary>
        /// Busca y devuelve una actividad por su ID.
        /// </summary>
        public Activity GetActivityById(int activityId) {
            return Activities.FirstOrDefault(a => a.Id == activityId);
        }
    }
}
