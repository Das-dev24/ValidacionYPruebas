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

        public List<User> GetAllUsers() {
            return Users;
        }

        private void SeedData() {
            var seeder = new DatabaseSeeder(this);
            seeder.Seed();
        }

        public bool GuardaUser(User u) {
            if (Users.Contains(u)) {
                return false;
            } else {
                Users.Add(u);
                return true;
            }
        }

        public User LeeUser(string email) {
            return Users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public int NumUsers() {
            return Users.Count;
        }

        public int NumUsersActivos() {
            int contador = 0;
            for (int i = 0; i < Users.Count; i++) {
                if (Users[i].State == UserState.Active) {
                    contador++;
                }
            }
            return contador;
        }

        public bool ValidaUser(string email, string password) {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException("El email y la contraseña no pueden estar vacíos.");
            }

            User usuario = LeeUser(email);

            if (usuario == null || !Password.VerifyPassword(password, usuario.Password)) {
                throw new InvalidOperationException("La combinación de email y contraseña es incorrecta." + password + "\t" + usuario.Password);
            }

            usuario.State = UserState.Active;
            usuario.Last_login = DateTime.Now;
            return true;
        }

        public void Register(string name, string lastName, string email, string password) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException("All fields must be filled out for registration.");
            }
            if (!Password.CheckPassword(password) || LeeUser(email) != null  || !Email.IsValidFormat(email)) {
                throw new ArgumentException("Password does not meet requirements or email is invalid.");
            }

            User usuario = new User(name, lastName, email, password);
            usuario.Id = NumUsers() + 1;
            GuardaUser(usuario);
        }


        public bool GuardaActivity(Activity e) {
            if (Activities.Contains(e)) {
                return false;
            } else {
                Activities.Add(e);
                return true;
            }
        }

        public Activity LeeActivity(int idElemento) {
            if (idElemento < 0 || idElemento >= Activities.Count) {
                return null;
            } else {
                return Activities[idElemento];
            }
        }

        public int NumActivityes(int idUser) {
            int contador = 0;

            for (int i = 0; i < Activities.Count; i++) {
                if (Activities[i].GetUser().Id == idUser) {
                    contador++;
                }
            }

            return contador;
        }
    }
}
