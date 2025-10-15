using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    public abstract class Activity {
        // Atributos comunes (pueden ser protected para que los hijos los vean)
        protected User User { get; set; }
        protected string Name { get; set; }
        public string TypeActivity { get; set; } // 1: Carrera, 2: Ciclismo, 3: Natacion, 4: Triatlon
        protected DateTime StartTime { get; set; }
        protected int Duration { get; set; }
        protected string Notes { get; set; }

        // Constructor de la clase base
        public Activity(User user, string name, string type, DateTime startTime, int duration, string notes) {
            this.User = User;
            this.Name = name;
            this.TypeActivity = type;
            this.StartTime = startTime;
            this.Duration = duration;
            this.Notes = notes;
        }

        public abstract string ObtainActivity();

        public User GetUser() {
            return User;
        }
    }
}
