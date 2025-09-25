using Database;
using Practica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos {
    internal class CapaDatos : ICapaDatos {

        List<User> Users = new List<User>();
        List<Activity> Activities = new List<Activity>();

        public bool GuardaActivity(Activity e) {
            if (Activities.Contains(e)) {
                return false;
            } else
            {
                Activities.Add(e);
                return true;
            }
        }

        public bool GuardaUser(User u) {
            if (Users.Contains(u))
            {
                return false;
            }
            else
            {
                Users.Add(u);
                return true;
            }
        }

        public Activity LeeActivity(int idElemento) {

            if (idElemento < 0 || idElemento >= Activities.Count)
            {
                return null;
            } else
            {
                return Activities[idElemento];
            }
        }

        public User LeeUser(string email)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Email == email)
                {
                    return Users[i];
                }
            }
            return null;

        }

        public int NumActivityes(int idUser)
        {
            int contador = 0;

            for (int i = 0; i < Activities.Count; i++)
            {
                if (Activities[i].GetUser().getId() == idUser) //Hay que implementar getId en User
                {
                    contador++;
                }
            }

            return contador;
        }

        public int NumUsers()
        {
            int numUsers = Users.Count;
            return numUsers;
        }

        public int NumUsersActivos()
        {
            int contador = 0;
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Is_active)
                {
                    contador++;
                }
            }
            return contador;
        }

        public bool ValidaUser(string email, string password){
            throw new NotImplementedException();
        }
    }
}
