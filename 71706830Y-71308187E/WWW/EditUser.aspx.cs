using Datos;
using Practica.Model;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace WWW {
    public partial class EditUser : System.Web.UI.Page {
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
            if (!IsPostBack) {
                LoadUserData();
            }
        }

        private void LoadUserData() {
            string userIdString = Request.QueryString["id"];
            int userId;

            if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out userId)) {
                User userToEdit = Data.LeeUserPorId(userId);
                if (userToEdit != null) {
                    PopulateForm(userToEdit);
                } else {
                    Response.Redirect("AdminUsers.aspx");
                }
            } else {
                Response.Redirect("AdminUsers.aspx");
            }
        }

        private void PopulateForm(User user) {
            hdnUserId.Value = user.Id.ToString();
            lblUserId.Text = user.Id.ToString();
            lblLastLogin.Text = user.Last_login.ToString("dd/MM/yyyy HH:mm");
            txtName.Text = user.Name;
            txtLastName.Text = user.LastName;
            txtEmail.Text = user.Email;
            chkIsSubscribed.Checked = user.Is_Subscription;
            chkIsSuperuser.Checked = user.Is_superuser;
            ddlState.DataSource = Enum.GetValues(typeof(UserState));
            ddlState.DataBind();
            ddlState.SelectedValue = user.State.ToString();
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e) {
            try {
                int userId = int.Parse(hdnUserId.Value);
                User userToUpdate = Data.LeeUserPorId(userId);

                if (userToUpdate != null) {
                    userToUpdate.Name = txtName.Text;
                    userToUpdate.LastName = txtLastName.Text;
                    userToUpdate.Email = txtEmail.Text;
                    userToUpdate.Is_Subscription = chkIsSubscribed.Checked;
                    userToUpdate.Is_superuser = chkIsSuperuser.Checked;
                    userToUpdate.State = (UserState)Enum.Parse(typeof(UserState), ddlState.SelectedValue);

                    //Data.UpdateUser(userToUpdate);

                    ShowMessage("¡Usuario actualizado correctamente!", isError: false);
                    Response.AppendHeader("Refresh", "2;url=AdminUsers.aspx");
                }
            } catch (Exception ex) {
                ShowMessage("Error al guardar: " + ex.Message, isError: true);
            }
        }

        // --- NUEVA FUNCIÓN ---
        protected void btnResetPassword_Click(object sender, EventArgs e) {
            try {
                if (string.IsNullOrEmpty(txtNewPassword.Text)) {
                    ShowMessage("El campo de la nueva contraseña no puede estar vacío.", isError: true);
                    return;
                }

                // Valida si la nueva contraseña cumple los requisitos
                if (!Practica.Utils.Password.CheckPassword(txtNewPassword.Text)) {
                    throw new ArgumentException("La contraseña no cumple los requisitos de seguridad.");
                }

                int userId = int.Parse(hdnUserId.Value);
                User userToUpdate = Data.LeeUserPorId(userId);

                if (userToUpdate != null) {
                    // Al asignar al campo 'Password', el 'set' se encarga de encriptarlo
                    userToUpdate.Password = txtNewPassword.Text;

                    //Data.UpdateUser(userToUpdate);

                    ShowMessage("¡Contraseña del usuario restablecida con éxito!", isError: false);
                    txtNewPassword.Text = string.Empty; // Limpia el campo
                }
            } catch (Exception ex) {
                ShowMessage("Error al restablecer contraseña: " + ex.Message, isError: true);
            }
        }

        // Función de ayuda para mostrar mensajes
        private void ShowMessage(string message, bool isError) {
            lblMessage.Text = message;
            lblMessage.CssClass = isError ? "alert alert-danger" : "alert alert-success";
            lblMessage.Visible = true;
        }
    }
}