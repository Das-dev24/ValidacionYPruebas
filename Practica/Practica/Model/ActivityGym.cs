using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    /// <summary>
    /// Clase específica para actividades de gimnasio, que hereda de la clase base Activity.
    /// </summary>
    public class ActivityGym : Activity {
        // Parte del cuerpo trabajada (ej. "Tren superior", "Pierna").
        public string BodyPart { get; set; }
        // Calorías quemadas durante el entrenamiento.
        public int Calories { get; set; }

        // Campos privados para almacenar los datos internamente.
        private string bodyPart;
        private int calories;

        /// <summary>
        /// Constructor para crear una nueva actividad de gimnasio.
        /// </summary>
        public ActivityGym(User user, string name, string typeActivity, DateTime start, int duration, string notes, int calories, string bodyPart)
            // Llama al constructor de la clase base (Activity) para inicializar las propiedades comunes.
            : base(user, name, typeActivity, start, duration, notes) {

            // Asigna "Gimnasio" como tipo de actividad fijo para esta clase.
            base.TypeActivity = "Gimnasio";
            // Inicializa las propiedades específicas de una actividad de gimnasio.
            this.calories = calories;
            this.bodyPart = bodyPart;

            // Valida que las calorías no sean un valor negativo.
            if (calories < 0f) {
                throw new ArgumentException("Las calorias quemadas no pueden ser negativas.");
            }

            // Valida que la duración sea un valor positivo.
            if (duration <= 0f) {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }
        }

        /// <summary>
        /// Sobrescribe el método de la clase base para devolver un resumen específico de la actividad de gimnasio.
        /// </summary>
        /// <returns>Una cadena de texto con los detalles del entrenamiento.</returns>
        public override string ObtainActivity() {
            return "Entrenamiento de " + this.bodyPart + ", quemando " + this.calories + " calorías en " + base.Duration + " mins";
        }
    }
}