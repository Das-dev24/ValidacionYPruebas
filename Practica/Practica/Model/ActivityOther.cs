using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    /// <summary>
    /// Clase para registrar otros tipos de actividades no definidas específicamente.
    /// Hereda de la clase base Activity.
    /// </summary>
    public class ActivityOther : Activity {
        // Nombre del tipo de actividad (ej: "Yoga", "Pádel").
        public string OtherActivity { get; set; }
        // Lugar donde se realizó la actividad.
        public string Place { get; set; }

        // Campos privados para almacenar los datos internamente.
        private string otherActivity;
        private string place;

        /// <summary>
        /// Constructor para crear una nueva actividad de tipo "Otro".
        /// </summary>
        public ActivityOther(User user, string name, string typeActivity, DateTime start, int duration, string notes, string place, string otherActivity)
            // Llama al constructor de la clase base (Activity) para inicializar las propiedades comunes.
            : base(user, name, typeActivity, start, duration, notes) {

            // Asigna "Other" como tipo de actividad fijo para esta clase.
            base.TypeActivity = "Other";
            // Inicializa las propiedades específicas de esta clase.
            this.place = place;
            this.otherActivity = otherActivity;

            // Valida que la duración sea un valor positivo.
            if (duration <= 0f) {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }
        }

        /// <summary>
        /// Sobrescribe el método de la clase base para devolver un resumen de la actividad.
        /// </summary>
        /// <returns>Una cadena de texto con los detalles de la actividad.</returns>
        public override string ObtainActivity() {
            return "Entrenamiento de " + this.otherActivity + " en " + base.Duration + " mins en " + this.place + " .";
        }
    }
}