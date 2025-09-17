using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica_1.Model;

namespace Practica.Model
{
    public abstract class Activity
    {
        // Atributos comunes (pueden ser protected para que los hijos los vean)
        protected int IdActivity { get; set; }
        protected int IdUser { get; set; }
        protected string Name { get; set; }
        protected string TypeActivity { get; set; } // 1: Carrera, 2: Ciclismo, 3: Natacion, 4: Triatlon
        protected DateTime StartTime { get; set; }
        protected int Duration { get; set; }
        protected string Notes { get; set; }

        // Constructor de la clase base
        public Activity(int idActivity, int idUser, string name, string type, DateTime startTime, int duration, string notes)
        {
            this.IdActivity = idActivity;
            this.IdUser = idUser;
            this.Name = name;
            this.TypeActivity = type;
            this.StartTime = startTime;
            this.Duration = duration;
            this.Notes = notes;
        }


        public abstract string ObtainActivity();
    }
}
