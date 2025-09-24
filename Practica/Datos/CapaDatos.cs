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

        public bool GuardaActivity(Activity e) {
            throw new NotImplementedException();
        }

        public bool GuardaUser(User u) {
            throw new NotImplementedException();
        }

        public Activity LeeActivity(int idElemento) {
            throw new NotImplementedException();
        }

        public User LeeUser(string email)
        {
            throw new NotImplementedException();
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
