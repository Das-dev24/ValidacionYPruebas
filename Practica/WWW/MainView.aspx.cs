using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Practica.Model;

namespace WWW
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = new User("Diego", "Alonso","pepe@ubu.es","jaudajna32");
            lblUser.Text = user.Name + " " + user.LastName;
        }
    }
}