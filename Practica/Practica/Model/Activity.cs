using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica.Model;

namespace Practica.Model {
    /// <summary>
    /// Clase base abstracta para todas las actividades deportivas.
    /// No se pueden crear objetos de esta clase directamente, solo de sus clases hijas.
    /// </summary>
    public abstract class Activity {
        // Identificador único de la actividad.
        public int Id { get; set; }
        // Usuario que realizó la actividad.
        public User User { get; set; }
        // Nombre o título de la actividad (ej. "Carrera matutina").
        public string Name { get; set; }
        // Tipo de actividad (Carrera, Ciclismo, Natación, etc.).
        public string TypeActivity { get; set; }
        // Fecha y hora de inicio de la actividad.
        public DateTime StartTime { get; set; }
        // Duración de la actividad en minutos.
        public int Duration { get; set; }
        // Notas o comentarios adicionales sobre la actividad.
        public string Notes { get; set; }

        /// <summary>
        /// Constructor para inicializar las propiedades comunes de una actividad.
        /// </summary>
        public Activity(User user, string name, string type, DateTime startTime, int duration, string notes) {
            this.User = user;
            this.Name = name;
            this.TypeActivity = type;
            this.StartTime = startTime;
            this.Duration = duration;
            this.Notes = notes;
        }

        /// <summary>
        /// Método abstracto. Obliga a las clases que hereden de Activity
        /// a implementar su propia lógica para obtener un resumen de la actividad.
        /// </summary>
        /// <returns>Una cadena de texto con los detalles de la actividad.</returns>
        public abstract string ObtainActivity();

        /// <summary>
        /// Devuelve el objeto User asociado a esta actividad.
        /// </summary>
        /// <returns>El usuario que realizó la actividad.</returns>
        public User GetUser() {
            return User;
        }
    }
}