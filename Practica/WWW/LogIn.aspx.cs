using Datos;
using System;

namespace WWW {
    public partial class LogIn : System.Web.UI.Page {

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
            lblErrorMessage.Visible = false;
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            try {
                // Usamos la instancia Singleton de CapaDatos
                if (Data.ValidaUser(txtEmail.Text, txtPassword.Text)) {
                    Session["user"] = Data.LeeUser(txtEmail.Text);
                    Response.Redirect("MainView.aspx");
                }
            } catch (Exception ex) {
                lblErrorMessage.Text = ex.Message;
                lblErrorMessage.CssClass = "alert alert-danger";
                lblErrorMessage.Visible = true;
            }
        }
    }
}
