using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica_1.Model;

namespace Practica.Model

{
    public class ActivitySwimming : Activity
    {
        private int distance;

        private string place;


        public ActivitySwimming(int idActivivty, int user, string name, string typeActivity, DateTime start, int duration, string notes, string place, int distance)
            : base(idActivivty, user, name, typeActivity, start, duration, notes)
        {
            base.TypeActivity = "Natación";
            this.place = place;
            this.distance = distance;
        }
        public override string ObtainActivity()
        {
            return "Natación en " + this.place + " de " + this.distance + " m en " + base.Duration + " mins";
        }
    }
}