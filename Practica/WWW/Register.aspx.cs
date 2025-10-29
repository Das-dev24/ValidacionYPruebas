using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Practica.Model;
using Datos;

namespace WWW 
{
    public partial class Register : System.Web.UI.Page
    {
        private CapaDatos Data
        {
            get
            {
                CapaDatos data = (CapaDatos)Application["datos"];
                if (data == null)
                {
                    data = new CapaDatos();
                    Application["datos"] = data;
                }
                return data;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessage.Visible = false;
            }
        }

        protected void btnRegistro_Click(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            List<string> errores = new List<string>();

            if (string.IsNullOrEmpty(nombre))
            {
                errores.Add("El nombre es obligatorio.");
            }

            if (string.IsNullOrEmpty(apellido))
            {
                errores.Add("El apellido es obligatorio.");
            }

            if (string.IsNullOrEmpty(email))
            {
                errores.Add("El correo electrónico es obligatorio.");
            }
            if (string.IsNullOrEmpty(password))
            {
                errores.Add("La contraseña es obligatoria.");
            }

            if (!string.IsNullOrEmpty(email) && !IsValidEmail(email))
            {
                errores.Add("El formato del correo electrónico no es válido.");
            }

            if (password.Length < 8)
            {
                errores.Add("La contraseña debe tener al menos 8 caracteres.");
            }

            if (password != confirmPassword)
            {
                errores.Add("Las contraseñas no coinciden.");
            }

            if (errores.Count > 0)
            {
                ShowMessage(string.Join("<br>", errores), "danger");
            }
            else
            {
                try { 
                    User registeredUser = new User(nombre, apellido, email, password);
                    Data.GuardaUser(registeredUser);
                    ShowMessage("¡Registro completado con éxito!", "success");
                    Response.AppendHeader("Refresh", "2;url=LogIn.aspx");
                }
                catch (Exception ex)
                {
                    ShowMessage("Error al registrar el usuario: " + ex.Message, "danger");
                }
             }
        }

        private void ShowMessage(string message, string type)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "alert alert-" + type;
            lblMessage.Visible = true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}