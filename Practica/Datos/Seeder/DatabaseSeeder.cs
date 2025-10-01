using Database;
using Practica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Seeder {
    public class DatabaseSeeder {

        private readonly ICapaDatos _capaDatos;

        public DatabaseSeeder(ICapaDatos capaDatos) {
            _capaDatos = capaDatos;
        }

        public void Seed() {
            if (_capaDatos.NumUsers() > 0) {
                return;
            }

            var users = GetInitialUsers();
            foreach (var user in users) {
                _capaDatos.GuardaUser(user);
            }

            var activities = GetInitialActivities();
            foreach (var activity in activities) {
                _capaDatos.GuardaActivity(activity);
            }
        }

        private List<User> GetInitialUsers() {
            var users = new List<User> {
                new User("Admin", "User", "admin@example.com", "Admin123456!") { Id = 1, Is_superuser = true },
                new User("Juan", "Pérez", "juan.perez@example.com", "Juan1234567!") { Id = 2 },
                new User("Ana", "García", "ana.garcia@example.com", "Ana12345678!") { Id = 3 }
            };
            return users;
        }

        private List<Activity> GetInitialActivities() {
            var userJuan = _capaDatos.LeeUser("juan.perez@example.com");
            var userAna = _capaDatos.LeeUser("ana.garcia@example.com");

            if (userJuan == null || userAna == null) {
                return new List<Activity>();
            }

            var activities = new List<Activity> {
            new ActivityRunning(userJuan, "Carrera por el río", null, new DateTime(2025, 10, 10, 8, 0, 0), 45, "Mañana fresca, buen ritmo.", "Paseo de la Isla", 8.5f, 25),
            new ActivitySwimming(userJuan, "Nado en piscina", null, new DateTime(2025, 10, 11, 19, 30, 0), 60, "Piscina concurrida.", "Piscina Municipal San Amaro", 2000),

            new ActividadCycling(userAna, "Ruta a Fuentes Blancas", null, new DateTime(2025, 10, 12, 10, 0, 0), 120, "Día soleado perfecto para rodar.", "Fuentes Blancas", 40.0f, 150),
            new ActivityGym(userAna, "Sesión de pierna", null, new DateTime(2025, 10, 13, 18, 0, 0), 75, "Entrenamiento intenso.", 550, "Tren inferior")
            };

            return activities;
        }
    }
}
