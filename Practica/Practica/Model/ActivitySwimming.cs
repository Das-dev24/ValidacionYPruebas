using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    /// <summary>
    /// Clase específica para actividades de natación, que hereda de la clase base Activity.
    /// </summary>
    public class ActivitySwimming : Activity {
        // Distancia nadada, generalmente en metros.
        public int Distance { get; set; }
        // Lugar donde se realizó la actividad (ej. "Piscina Municipal", "Mar Cantábrico").
        public string Place { get; set; }

        // Campos privados para almacenar los datos internamente.
        private int distance;
        private string place;

        /// <summary>
        /// Constructor para crear una nueva actividad de natación.
        /// </summary>
        public ActivitySwimming(User user, string name, string typeActivity, DateTime start, int duration, string notes, string place, int distance)
            // Llama al constructor de la clase base (Activity) para inicializar las propiedades comunes.
            : base(user, name, typeActivity, start, duration, notes) {

            // Asigna "Natación" como tipo de actividad fijo para esta clase.
            base.TypeActivity = "Natación";
            // Inicializa las propiedades específicas de una actividad de natación.
            this.place = place;
            this.distance = distance;

            // Valida que la distancia no sea un valor negativo.
            if (distance < 0f) {
                throw new ArgumentException("La distancia en natación no puede ser negativa.");
            }

            // Valida que la duración sea un valor positivo.
            if (duration <= 0f) {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }
        }

        /// <summary>
        /// Sobrescribe el método de la clase base para devolver un resumen específico de la actividad de natación.
        /// </summary>
        /// <returns>Una cadena de texto con los detalles de la actividad.</returns>
        public override string ObtainActivity() {
            return "Natación en " + this.place + " de " + this.distance + " m en " + base.Duration + " mins";
        }
    }
}