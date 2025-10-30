
using Database;
using Practica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seeder {
    /// <summary>
    /// Clase para inicializar la base de datos con datos de prueba (seeding).
    /// </summary>
    public class DatabaseSeeder {

        // Campo privado para almacenar una instancia de la capa de datos.
        private readonly ICapaDatos _capaDatos;

        /// <summary>
        /// Constructor que recibe la capa de datos para interactuar con la base de datos.
        /// </summary>
        /// <param name="capaDatos">La implementación de la interfaz de la capa de datos.</param>
        public DatabaseSeeder(ICapaDatos capaDatos) {
            _capaDatos = capaDatos;
        }

        /// <summary>
        /// Método principal para ejecutar el proceso de "seeding".
        /// </summary>
        public void Seed() {
            // Comprueba si ya existen usuarios en la base de datos para no duplicar datos.
            if (_capaDatos.NumUsers() > 0) {
                return; // Si hay usuarios, no hace nada y sale del método.
            }

            // Obtiene la lista de usuarios iniciales.
            var users = GetInitialUsers();
            // Guarda cada usuario de la lista en la base de datos.
            foreach (var user in users) {
                _capaDatos.GuardaUser(user);
            }

            // Obtiene la lista de actividades iniciales.
            var activities = GetInitialActivities();
            // Guarda cada actividad de la lista en la base de datos.
            foreach (var activity in activities) {
                _capaDatos.GuardaActivity(activity);
            }
        }

        /// <summary>
        /// Crea y devuelve una lista de usuarios predefinidos.
        /// </summary>
        /// <returns>Una lista de objetos User.</returns>
        private List<User> GetInitialUsers() {
            var users = new List<User> {
                // Crea un usuario administrador.
                new User("Admin", "User", "admin@example.com", "Admin123456!") { Id = 1, Is_superuser = true },
                // Crea usuarios normales.
                new User("Juan", "Pérez", "juan.perez@example.com", "Juan1234567!") { Id = 2 },
                new User("Ana", "García", "ana.garcia@example.com", "Ana12345678!") { Id = 3 }
            };
            return users;
        }

        /// <summary>
        /// Crea y devuelve una lista de actividades predefinidas asociadas a los usuarios.
        /// </summary>
        /// <returns>Una lista de objetos Activity.</returns>
        private List<Activity> GetInitialActivities() {
            // Lee los usuarios de la base de datos para asociarlos a las actividades.
            var userJuan = _capaDatos.LeeUser("juan.perez@example.com");
            var userAna = _capaDatos.LeeUser("ana.garcia@example.com");

            // Si alguno de los usuarios no se encuentra, devuelve una lista vacía para evitar errores.
            if (userJuan == null || userAna == null) {
                return new List<Activity>();
            }

            // Crea una lista de diferentes tipos de actividades para cada usuario.
            var activities = new List<Activity> {
                // Actividades para Juan
                new ActivityRunning(userJuan, "Carrera por el río", null, new DateTime(2025, 10, 10, 8, 0, 0), 45, "Mañana fresca, buen ritmo.", "Paseo de la Isla", 8.5f, 25),
                new ActivitySwimming(userJuan, "Nado en piscina", null, new DateTime(2025, 10, 11, 19, 30, 0), 60, "Piscina concurrida.", "Piscina Municipal San Amaro", 2000),
                new ActividadCycling(userJuan, "Ruta a Fuentes Blancas", null, new DateTime(2025, 10, 12, 10, 0, 0), 120, "Día soleado perfecto para rodar.", "Fuentes Blancas", 40.0f, 150),
                new ActivityGym(userJuan, "Sesión de pierna", null, new DateTime(2025, 10, 13, 18, 0, 0), 75, "Entrenamiento intenso.", 550, "Tren inferior"),

                // Actividades para Ana
                new ActivityRunning(userAna, "Carrera por el río", null, new DateTime(2025, 10, 10, 8, 0, 0), 45, "Mañana fresca, buen ritmo.", "Paseo de la Isla", 8.5f, 25),
                new ActivitySwimming(userAna, "Nado en piscina", null, new DateTime(2025, 10, 11, 19, 30, 0), 60, "Piscina concurrida.", "Piscina Municipal San Amaro", 2000),
                new ActividadCycling(userAna, "Ruta a Fuentes Blancas", null, new DateTime(2025, 10, 12, 10, 0, 0), 120, "Día soleado perfecto para rodar.", "Fuentes Blancas", 40.0f, 150),
                new ActivityGym(userAna, "Sesión de pierna", null, new DateTime(2025, 10, 13, 18, 0, 0), 75, "Entrenamiento intenso.", 550, "Tren inferior")
            };

            // Devuelve la lista de actividades creada.
            return activities;
        }
    }
}