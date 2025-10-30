using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    /// <summary>
    /// Clase específica para actividades de ciclismo, que hereda de la clase base Activity.
    /// </summary>
    public class ActividadCycling : Activity {

        // Propiedades públicas para la distancia, desnivel y lugar.
        public float Distance { get; set; }
        public int Slope { get; set; }
        public string Place { get; set; }

        // Campos privados para almacenar los datos internamente.
        private float distance;
        private int slope;
        private string place;

        /// <summary>
        /// Constructor para crear una nueva actividad de ciclismo.
        /// </summary>
        public ActividadCycling(User user, string name, string typeActivity, DateTime start, int duration, string notes, string place, float distance, int slope)
            // Llama al constructor de la clase base (Activity) para inicializar las propiedades comunes.
            : base(user, name, typeActivity, start, duration, notes) {
            // Asigna "Ciclismo" como tipo de actividad fijo para esta clase.
            base.TypeActivity = "Ciclismo";
            // Inicializa las propiedades específicas de una actividad de ciclismo.
            this.place = place;
            this.distance = distance;
            this.slope = slope;

            // Valida que la distancia no sea negativa.
            if (distance < 0f) {
                throw new ArgumentException("La distancia debe ser mayor que cero.");
            }

            // Valida que la duración sea un valor positivo.
            if (duration <= 0f) {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }
        }

        /// <summary>
        /// Sobrescribe el método de la clase base para devolver un resumen específico de la actividad de ciclismo.
        /// </summary>
        /// <returns>Una cadena de texto con los detalles de la actividad.</returns>
        public override string ObtainActivity() {
            return "Ciclismo en " + this.place + " de " + this.distance + " a un ritmo de: " + this.Rythm() + " mins/km. Y una velocidad media de: " + this.Speed() + " km/h, y un desnivel de " + this.slope;
        }

        /// <summary>
        /// Calcula el ritmo de la actividad en minutos por kilómetro.
        /// </summary>
        /// <returns>El ritmo formateado a dos decimales.</returns>
        public string Rythm() {
            // Evita la división por cero si la distancia es cero.
            if (distance == 0) {
                throw new DivideByZeroException("La distancia no puede ser cero al calcular el ritmo.");
            }
            // Fórmula para calcular el ritmo (minutos / km).
            float rythm = base.Duration / this.distance;
            // Devuelve el ritmo formateado a dos decimales.
            return rythm.ToString("0.00");
        }

        /// <summary>
        /// Calcula la velocidad media en kilómetros por hora.
        /// </summary>
        /// <returns>La velocidad formateada a dos decimales.</returns>
        public string Speed() {
            // Fórmula para calcular la velocidad (km / h).
            float speed = this.distance / (base.Duration / 60f);
            // Devuelve la velocidad formateada a dos decimales.
            return speed.ToString("0.00");
        }
    }
}