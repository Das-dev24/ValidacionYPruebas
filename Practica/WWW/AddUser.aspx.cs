using Datos;
using Practica.Model;
using System;
using System.Web.UI;

namespace WWW {
    public partial class AddUser : System.Web.UI.Page {
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
            // Proteger la página para que solo administradores puedan entrar
            User user = Session["user"] as User;
            if (user == null || !user.Is_superuser) {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e) {
            if (!Page.IsValid) {
                return;
            }

            string name = txtName.Text;
            string lastName = txtLastName.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            bool isSuperUser = chkIsSuperUser.Checked;

            try {
                Data.Register(name, lastName, email, password, isSuperUser);
                Response.Redirect("AdminUsers.aspx");
            } catch (ArgumentException ex) {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            } catch (Exception ex) {
                lblError.Text = "Ha ocurrido un error inesperado al crear el usuario.";
                lblError.Visible = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e) {
            Response.Redirect("AdminUsers.aspx");
        }
    }
}