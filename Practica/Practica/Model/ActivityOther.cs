using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model

{
    public class ActivityOther : Activity
    {
        private string otherActivity;
        private string place;


        public ActivityOther(User user, string name, string typeActivity, DateTime start, int duration, string notes, string place, string otherActivity)
            : base(user, name, typeActivity, start, duration, notes)
        {
            base.TypeActivity = "Other";
            this.place = place;
            this.otherActivity = otherActivity;


            if(duration <= 0f)
            {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }

        }
        public override string ObtainActivity()
        {
            return "Entrenamiento de " + this.otherActivity + " en " + base.Duration + " mins en " + this.place + " .";
        }
    }
}