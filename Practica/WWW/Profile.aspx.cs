using System;
using System.Web.UI;
using Practica.Model;
using Practica.Utils;

namespace WWW {
    public partial class Profile : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                LoadUserProfile();
            }
            // Asegurarse de que los mensajes estén ocultos al cargar la página
            lblMessage.Visible = false;
            lblPasswordMessage.Visible = false;
        }

        private void LoadUserProfile() {
            User user = Session["user"] as User;
            if (user == null) {
                Response.Redirect("Login.aspx");
                return;
            }

            lblFullName.Text = $"{user.Name} {user.LastName}";
            lblEmailInfo.Text = user.Email;
            lblSubscriptionStatus.Text = user.Is_Subscription ? "Activa" : "Inactiva";
            lblAccountStatus.Text = user.State.ToString();
            lblLastLogin.Text = user.Last_login.ToString("dd/MM/yyyy HH:mm");

            txtName.Text = user.Name;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.Email;
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e) {
            User user = Session["user"] as User;
            if (user == null) { Response.Redirect("Login.aspx"); return; }

            lblMessage.Visible = false;
            lblPasswordMessage.Visible = false;

            try {
                user.ChangeDetails(txtName.Text, txtLastName.Text, txtEmail.Text);
                Session["user"] = user;

                lblMessage.Text = "¡Datos actualizados correctamente!";
                lblMessage.CssClass = "alert alert-success mt-3";
                lblMessage.Visible = true;
                LoadUserProfile();
            } catch (ArgumentException ex){
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "alert alert-danger mt-3";
                lblMessage.Visible = true;
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e) {
            User user = Session["user"] as User;
            if (user == null) {
                Response.Redirect("Login.aspx");
                return;
            }

            lblPasswordMessage.Visible = false;
            lblMessage.Visible = false;

            if (txtNewPassword.Text != txtConfirmPassword.Text) {
                lblPasswordMessage.Text = "Error: La nueva contraseña y la confirmación no coinciden.";
                lblPasswordMessage.CssClass = "alert alert-danger";
                lblPasswordMessage.Visible = true;
                return;
            }

            try {
                user.ChangePassword(txtOldPassword.Text, txtNewPassword.Text);
                Session["user"] = user;

                lblPasswordMessage.Text = "¡Contraseña cambiada con éxito!";
                lblPasswordMessage.CssClass = "alert alert-success";
                lblPasswordMessage.Visible = true;
            } catch (Exception ex) {
                lblPasswordMessage.Text = "Error: " + ex.Message;
                lblPasswordMessage.CssClass = "alert alert-danger";
                lblPasswordMessage.Visible = true;
            } finally {
                txtOldPassword.Text = string.Empty;
                txtNewPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
            }
        }
    }
}