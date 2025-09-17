using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica_1.Model;

namespace Practica.Model
    
{
    public class ActivityRunning: Activity
    {
        private int distance;
        private int slope; //Desnivel
        private string place;
       

        public ActivityRunning(int idActivivty, int user, string name, string typeActivity, DateTime start, int duration, string notes, string place, int distance, int slope) 
            : base(idActivivty, user, name, typeActivity, start, duration, notes)
        {
            base.TypeActivity = "Carrera";
            this.place = place;
            this.distance = distance;
            this.slope = slope;
        }
        public override string ObtainActivity()
        {
            return "Carrera en " + this.place + " de " + this.distance + " a un ritmo de: " + this.Rythm() + " mins/km";
        }
        public int Rythm()
        {
            return base.Duration / this.distance;
        }
    }
}
