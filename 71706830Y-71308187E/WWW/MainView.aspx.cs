using Datos;
using Practica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WWW {
    public partial class MainView : System.Web.UI.Page {
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
            User user = Session["user"] as User;
            if (user == null) {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack) {
                lblUser.Text = $"{user.Name} {user.LastName}";
                BindActivities();
            }
        }

        private void BindActivities() {
            User user = Session["user"] as User;
            if (user != null) {
                // Obtenemos las actividades y las ordenamos por fecha de inicio descendente
                List<Activity> userActivities = Data.GetActivitiesForUser(user.Id)
                                                    .OrderByDescending(a => a.StartTime)
                                                    .ToList();

                if (userActivities.Any()) {
                    rptActivities.DataSource = userActivities;
                    rptActivities.DataBind();
                    rptActivities.Visible = true;
                    pnlNoActivities.Visible = false;
                } else {
                    rptActivities.Visible = false;
                    pnlNoActivities.Visible = true;
                }
            }
        }

        // El manejador de eventos para clics dentro de la lista
        protected void rptActivities_ItemCommand(object source, RepeaterCommandEventArgs e) {
            if (e.CommandName == "ViewDetails") {
                int activityId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"ActivityDetails.aspx?id={activityId}");
            }
        }

        protected void btnProfile_Click(object sender, EventArgs e) {
            User user = Session["user"] as User;
            if (user != null) {
                Response.Redirect(user.Is_superuser ? "AdminUsers.aspx" : "Profile.aspx");
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e) {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        protected void btnAddActivity_Click(object sender, EventArgs e) {
            // Redirige al usuario a la página del formulario para crear una nueva actividad
            Response.Redirect("AddActivity.aspx");
        }
    }
}