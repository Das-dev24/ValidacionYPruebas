using Datos;
using Practica.Model;
using System;
using System.Globalization;
using System.Web.UI;

namespace WWW {
    public partial class AddActivity : System.Web.UI.Page {
        // Propiedad para acceder a la capa de datos (igual que en la página principal)
        private CapaDatos Data {
            get {
                CapaDatos data = (CapaDatos)Application["datos"];
                if (data == null) {
                    data = new CapaDatos();
                    Application["datos"] = data;
                }
                return data;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            // Se asegura de que el usuario haya iniciado sesión antes de poder añadir una actividad
            if (Session["user"] as User == null) {
                Response.Redirect("Login.aspx");
            }
        }

        /// <summary>
        /// Se ejecuta cada vez que el usuario cambia el tipo de actividad en el desplegable.
        /// Muestra u oculta los paneles con los campos específicos de cada deporte.
        /// </summary>
        protected void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e) {
            // Primero, oculta todos los paneles para empezar de cero
            pnlRunningCycling.Visible = false;
            pnlSwimming.Visible = false;
            pnlGym.Visible = false;
            pnlOther.Visible = false;

            // Habilita/deshabilita los validadores correspondientes
            SetValidators(false); // Deshabilita todos primero

            // Muestra el panel correspondiente a la selección del usuario
            string selectedType = ddlActivityType.SelectedValue;
            switch (selectedType) {
                case "Carrera":
                case "Ciclismo":
                    pnlRunningCycling.Visible = true;
                    SetValidators(true, "RunningCycling");
                    break;
                case "Natacion":
                    pnlSwimming.Visible = true;
                    SetValidators(true, "Swimming");
                    break;
                case "Gimnasio":
                    pnlGym.Visible = true;
                    SetValidators(true, "Gym");
                    break;
                case "Otro":
                    pnlOther.Visible = true;
                    SetValidators(true, "Other");
                    break;
            }
        }

        /// <summary>
        /// Se ejecuta al pulsar el botón de Guardar.
        /// Valida los datos y, si son correctos, crea el objeto de actividad y lo guarda.
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e) {
            // Comprueba si todos los validadores de la página se han cumplido
            if (!Page.IsValid) {
                return;
            }

            User user = Session["user"] as User;
            if (user == null) {
                Response.Redirect("Login.aspx"); // Seguridad extra
                return;
            }

            // --- 1. Recoger datos comunes ---
            string name = txtName.Text;
            DateTime startTime = Convert.ToDateTime(txtStartTime.Text);
            int duration = Convert.ToInt32(txtDuration.Text);
            string notes = txtNotes.Text;
            string selectedType = ddlActivityType.SelectedValue;

            Activity newActivity = null;

            // --- 2. Crear el objeto de actividad específico ---
            try {
                switch (selectedType) {
                    case "Carrera":
                        float runDistance = float.Parse(txtDistance.Text, CultureInfo.InvariantCulture);
                        int runSlope = string.IsNullOrEmpty(txtSlope.Text) ? 0 : int.Parse(txtSlope.Text);
                        string runPlace = txtPlaceRunBike.Text;
                        newActivity = new ActivityRunning(user, name, selectedType, startTime, duration, notes, runPlace, runDistance, runSlope);
                        break;

                    case "Ciclismo":
                        float bikeDistance = float.Parse(txtDistance.Text, CultureInfo.InvariantCulture);
                        int bikeSlope = string.IsNullOrEmpty(txtSlope.Text) ? 0 : int.Parse(txtSlope.Text);
                        string bikePlace = txtPlaceRunBike.Text;
                        newActivity = new ActividadCycling(user, name, selectedType, startTime, duration, notes, bikePlace, bikeDistance, bikeSlope);
                        break;

                    case "Natacion":
                        int swimDistance = int.Parse(txtSwimDistance.Text);
                        string swimPlace = txtSwimPlace.Text;
                        newActivity = new ActivitySwimming(user, name, selectedType, startTime, duration, notes, swimPlace, swimDistance);
                        break;

                    case "Gimnasio":
                        int calories = string.IsNullOrEmpty(txtCalories.Text) ? 0 : int.Parse(txtCalories.Text);
                        string bodyPart = txtBodyPart.Text;
                        newActivity = new ActivityGym(user, name, selectedType, startTime, duration, notes, calories, bodyPart);
                        break;

                    case "Otro":
                        string otherActivityName = txtOtherActivity.Text;
                        string otherPlace = txtOtherPlace.Text;
                        newActivity = new ActivityOther(user, name, selectedType, startTime, duration, notes, otherPlace, otherActivityName);
                        break;
                }

                // --- 3. Guardar la actividad y redirigir ---
                if (newActivity != null) {
                    Data.GuardaActivity(newActivity); // Llama a tu método en CapaDatos para guardar
                    Response.Redirect("MainView.aspx"); // Vuelve a la lista principal
                }
            } catch (Exception ex) {
                // Opcional: Mostrar un mensaje de error al usuario si algo falla (ej: formato de número incorrecto)
                // lblError.Text = "Ha ocurrido un error al guardar: " + ex.Message;
                // lblError.Visible = true;
            }
        }

        /// <summary>
        /// Se ejecuta al pulsar el botón Cancelar. Devuelve al usuario a la página principal.
        /// </summary>
        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("MainView.aspx");
        }

        /// <summary>
        /// Método auxiliar para habilitar o deshabilitar los validadores requeridos de cada panel.
        /// </summary>
        private void SetValidators(bool isEnabled, string panelType = "") {
            // Validadores de Carrera/Ciclismo
            rfvDistance.Enabled = (panelType == "RunningCycling" && isEnabled);

            // Validadores de Natación
            rfvSwimDistance.Enabled = (panelType == "Swimming" && isEnabled);

            // Validadores de "Otro"
            rfvOtherActivity.Enabled = (panelType == "Other" && isEnabled);
        }
    }
}