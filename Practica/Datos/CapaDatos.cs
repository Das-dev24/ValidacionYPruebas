using Database;
using Practica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Practica.Utils;

namespace Datos {
    internal class CapaDatos : ICapaDatos {

        private List<User> Users = new List<User>();
        private List<Activity> Activities = new List<Activity>();

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

        public bool ValidaUser(string email, string password){
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException("Email and password cannot be empty.");
            }

            if (!Password.VerifyPassword(password, LeeUser(email).Password) || LeeUser(email) == null) {
                throw new InvalidOperationException("Incorrect combination of email and password.");
            }
            User usuario = LeeUser(email);

            usuario.State = UserState.Active;
            usuario.Last_login = DateTime.Now;
            return true;
        }

        public void Register(string name, string lastName, string email, string password) {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) {
                throw new ArgumentNullException("All fields must be filled out for registration.");
            }
            if (!Password.CheckPassword(password) || LeeUser(email) != null  || !Email.IsValidFormat(email)) {
                // Modificar luego el tema del email para comparar con la bd
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
