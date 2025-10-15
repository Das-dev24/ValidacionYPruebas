using Datos;
using Practica.Model;
using Practica.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WWW {
    public partial class LogIn : System.Web.UI.Page {
        private User AuthorizedUser;

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
            AuthorizedUser = null;
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
                lblErrorMessage.Visible = true;
            }
        }
    }
}
