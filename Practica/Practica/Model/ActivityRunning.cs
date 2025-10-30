using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    /// <summary>
    /// Clase específica para actividades de carrera a pie (running), que hereda de la clase base Activity.
    /// </summary>
    public class ActivityRunning : Activity {
        // Propiedades públicas para la distancia, desnivel y lugar.
        public float Distance { get; set; }
        public int Slope { get; set; }
        public string Place { get; set; }

        // Campos privados para almacenar los datos internamente.
        private float distance;
        private int slope; // Desnivel acumulado en la carrera.
        private string place;

        /// <summary>
        /// Constructor para crear una nueva actividad de carrera.
        /// </summary>
        public ActivityRunning(User user, string name, string typeActivity, DateTime start, int duration, string notes, string place, float distance, int slope)
            // Llama al constructor de la clase base (Activity) para inicializar las propiedades comunes.
            : base(user, name, typeActivity, start, duration, notes) {

            // Asigna "Carrera" como tipo de actividad fijo para esta clase.
            base.TypeActivity = "Carrera";
            // Inicializa las propiedades específicas de una actividad de carrera.
            this.place = place;
            this.distance = distance;
            this.slope = slope;

            // Valida que la distancia no sea un valor negativo.
            if (distance < 0f) {
                throw new ArgumentException("La distancia debe ser mayor que cero.");
            }

            // Valida que la duración sea un valor positivo.
            if (duration <= 0) {
                throw new ArgumentException("La duración debe ser mayor que cero.");
            }
        }

        /// <summary>
        /// Sobrescribe el método de la clase base para devolver un resumen específico de la carrera.
        /// </summary>
        /// <returns>Una cadena de texto con los detalles de la actividad.</returns>
        public override string ObtainActivity() {
            return "Carrera en " + this.place + " de " + this.distance + " a un ritmo de: " + this.Rythm() + " mins/km";
        }

        /// <summary>
        /// Calcula el ritmo de la carrera en minutos por kilómetro.
        /// </summary>
        /// <returns>El ritmo formateado a dos decimales.</returns>
        public string Rythm() {
            // Evita un error de división por cero si la distancia es cero.
            if (distance == 0) {
                throw new DivideByZeroException("La distancia no puede ser cero al calcular el ritmo.");
            }
            // Fórmula para calcular el ritmo (total de minutos / total de kilómetros).
            float rythm = base.Duration / this.distance;
            // Devuelve el resultado como texto, formateado a dos decimales.
            return rythm.ToString("0.00");
        }