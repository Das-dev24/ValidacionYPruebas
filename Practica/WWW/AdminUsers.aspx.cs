using Datos; // Y a tu capa de datos
using Practica.Model; // Asegúrate de tener la referencia a tu modelo
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WWW {
    public partial class AdminUsers : System.Web.UI.Page {
        // Asume que tienes acceso a tu capa de datos aquí
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
            // 1. Se añade la verificación de sesión al inicio del método.
            User user = Session["user"] as User;
            if (user == null) {
                // Si no hay usuario en la sesión, se redirige inmediatamente a la página de Login.
                Response.Redirect("Login.aspx");
                return; // Detiene la ejecución del resto del código de la página.
            }

            // 2. Si la verificación es exitosa, el resto del código se ejecuta con normalidad.
            if (!IsPostBack) {
                BindUsers();
            }
        }

        private void BindUsers() {
            // Obtiene todos los usuarios de la capa de datos
            List<User> userList = Data.GetAllUsers();
            rptUsers.DataSource = userList;
            rptUsers.DataBind();
        }

        protected void btnAddUser_Click(object sender, EventArgs e) {
            // Redirige a una página para crear un nuevo usuario
            Response.Redirect("CreateUser.aspx");
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e) {
            // 1. Obtener el ID del usuario desde CommandArgument (forma correcta).
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit") {
                // La lógica de editar sigue funcionando con el ID.
                Response.Redirect($"EditUser.aspx?id={userId}");
            } else if (e.CommandName == "Delete") {
                // 2. Para llamar a DeleteUser(User user), primero obtenemos el objeto User completo.
                //    Lo buscamos en la lista de usuarios usando el ID que obtuvimos.
                User userToDelete = Data.GetAllUsers().FirstOrDefault(u => u.Id == userId);

                // 3. Verificamos que el usuario fue encontrado antes de intentar borrarlo.
                if (userToDelete != null) {
                    // 4. Ahora sí, llamamos al método DeleteUser pasándole el objeto completo.
                    Data.DeleteUser(userToDelete);
                }

                // 5. Recargamos la lista para mostrar los cambios.
                BindUsers();
            }
        }
    }
}