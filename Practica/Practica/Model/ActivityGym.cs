using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    public class ActivityGym : Activity {
        private string bodyPart;
        private int calories;

        public ActivityGym(User user, string name, string typeActivity, DateTime start, int duration, string notes, int calories, string bodyPart)
            : base(user, name, typeActivity, start, duration, notes) {
            base.TypeActivity = "Gimnasio";
            this.calories = calories;
            this.bodyPart = bodyPart;

            if (calories < 0f) {
                throw new ArgumentException("Las calorias quemadas no pueden ser negativas.");
            }

            if (duration <= 0f) {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }

        }

        public override string ObtainActivity() {
            return "Entrenamiento de " + this.bodyPart + ", quemando " + this.calories + " calorías en " + base.Duration + " mins";
        }
    }
}