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
            throw new NotImplementedException();
        }

        public int NumUsers()
        {
            throw new NotImplementedException();
        }

        public int NumUsersActivos()
        {
            throw new NotImplementedException();
        }

        public bool ValidaUser(string email, string password){
            throw new NotImplementedException();
        }
    }
}
