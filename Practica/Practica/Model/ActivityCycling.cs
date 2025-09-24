using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model

{
    public class ActividadCycling : Activity
    {
        private float distance;
        private int slope; //Desnivel
        private string place;


        public ActividadCycling(int idActivivty, int user, string name, string typeActivity, DateTime start, int duration, string notes, string place, float distance, int slope)
            : base(idActivivty, user, name, typeActivity, start, duration, notes)
        {
            base.TypeActivity = "Ciclismo";
            this.place = place;
            this.distance = distance;
            this.slope = slope;

            if (distance < 0f)
            {
                throw new ArgumentException("La distancia debe ser mayor que cero.");
            }

            if (duration <= 0f)
            {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }
        }



        public override string ObtainActivity()
        {
            return "Ciclismo en " + this.place + " de " + this.distance + " a un ritmo de: " + this.Rythm() + " mins/km. Y una velocidad media de: " + this.Speed() + " km/h, y un desnivel de " + this.slope;
        }
        public string Rythm()
        {
            if (distance == 0)
            {
                throw new DivideByZeroException("La distancia no puede ser cero al calcular el ritmo.");
            }
            float rythm = base.Duration / this.distance;
            return rythm.ToString("0.00");
        }
        public string Speed()
        {
            float speed = this.distance / (base.Duration / 60f);
            return speed.ToString("0.00");
        }
    }
}
