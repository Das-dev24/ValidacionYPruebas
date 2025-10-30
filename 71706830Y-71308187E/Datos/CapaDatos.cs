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
    public class CapaDatos : ICapaDatos {

        private List<User> Users = new List<User>();
        private List<Activity> Activities = new List<Activity>();

        public CapaDatos() {
            SeedData();
        }

        private void SeedData() {
            var seeder = new DatabaseSeeder(this);
            seeder.Seed();
        }

        public List<User> GetAllUsers() {
            return Users;
        }

        public bool GuardaUser(User u) {
            if (Users.Contains(u)) {
                return false;
            } else {
                Users.Add(u);
                return true;
            }
        }

        public bool DeleteUser(User userToDelete) {
            if (userToDelete == null) {
                return false;
            }

            Activities.RemoveAll(activity => activity.GetUser() == userToDelete);

            return Users.Remove(userToDelete);
        }

        public User LeeUser(string email) {
            return Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public User LeeUserPorId(int userId) {
            return Users.FirstOrDefault(u => u.Id == userId);
        }

        public int NumUsers() {
            return Users.Count;
        }

        public bool ValidaUser(string email, string password) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException("El email o la contraseña no pueden estar vacíos.");
            }

            User usuario = LeeUser(email);

            if (usuario == null || !Password.VerifyPassword(password, usuario.Password)) {
                throw new InvalidOperationException("La combinación de email y contraseña es incorrecta.");
            }

            usuario.State = UserState.Active;
            usuario.Last_login = DateTime.Now;
            return true;
        }

        public void Register(string name, string lastName, string email, string password, bool isSuperUser = false) {
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

            User usuario = new User(name, lastName, email, password);
            usuario.Id = NumUsers() + 1;
            usuario.Is_superuser = isSuperUser;

            GuardaUser(usuario);
        }

        public List<Activity> GetActivitiesForUser(int userId) {
            return Activities.Where(a => a.GetUser() != null && a.GetUser().Id == userId).ToList();
        }

        public bool GuardaActivity(Activity e) {
            if (Activities.Contains(e)) {
                return false;
            } else {
                Activities.Add(e);
                return true;
            }
        }
    }
}
