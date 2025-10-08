using Datos;
using Practica.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WWW
{
    public partial class LogIn : System.Web.UI.Page
    {
        CapaDatos data;
        User AuthorizedUser;
        protected void Page_Load(object sender, EventArgs e)
        {

            data = (CapaDatos)Application["datos"];
            if (data == null)
            {
                data = new CapaDatos();
            }
            AuthorizedUser = null;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if( data.ValidaUser(tbxUser.Text, tbxPassword.Text))
            {
                Session["user"] = data.LeeUser(tbxUser.Text);
                Server.Transfer("MainView.aspx");
            }
            else
            {
                lblFailedLogIn.Visible = true;
            }
        }
    }
}