using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica_1.Model;

namespace Practica.Model

{
    public class ActividadCiclismo : Activity
    {
        private int distance;
        private int slope; //Desnivel
        private string place;


        public ActividadCiclismo(int idActivivty, int user, string name, string typeActivity, DateTime start, int duration, string notes, string place, int distance, int slope)
            : base(idActivivty, user, name, typeActivity, start, duration, notes)
        {
            base.TypeActivity = "Ciclismo";
            this.place = place;
            this.distance = distance;
            this.slope = slope;
        }
        public override string ObtainActivity()
        {
            return "Ciclismo en " + this.place + " de " + this.distance + " a un ritmo de: " + this.Rythm() + " mins/km." + "Y una velocidad media de :" + this.Speed() + " km/h";
        }
        public int Rythm()
        {
            return base.Duration / this.distance;
        }
        public int Speed()
        {
            return this.distance / (base.Duration / 60);
        }
    }
}
