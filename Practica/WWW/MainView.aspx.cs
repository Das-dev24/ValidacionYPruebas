using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Practica.Model;

namespace WWW {
    public partial class WebForm1 : System.Web.UI.Page {
        private User user;

        protected void Page_Load(object sender, EventArgs e) {
            user = Session["user"] as User;

            if (user != null) {
                lblUser.Text = $"Bienvenido de nuevo {user.Name} {user.LastName}";
            } else {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnProfile_Click(object sender, EventArgs e) {
            if (user.Is_superuser) {
                Response.Redirect("AdminUsers.aspx");
            } else {
                Response.Redirect("Profile.aspx");
            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e) {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}