using Datos;
using Practica.Model; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WWW {
    public partial class AdminUsers : System.Web.UI.Page {
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
            User user = Session["user"] as User;
            if (user == null) {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack) {
                BindUsers();
            }
        }

        private void BindUsers() {
            List<User> userList = Data.GetAllUsers();
            rptUsers.DataSource = userList;
            rptUsers.DataBind();
        }

        protected void btnAddUser_Click(object sender, EventArgs e) {
            Response.Redirect("AddUser.aspx");
        }

        protected void rptUsers_ItemCommand(object source, RepeaterCommandEventArgs e) {
            int userId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Edit") {
                Response.Redirect($"EditUser.aspx?id={userId}");
            } else if (e.CommandName == "Delete") {

                User userToDelete = Data.GetAllUsers().FirstOrDefault(u => u.Id == userId);

                if (userToDelete != null) {
                    Data.DeleteUser(userToDelete);
                }

                BindUsers();
            }
        }
    }
}