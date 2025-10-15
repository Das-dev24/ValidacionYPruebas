<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminUsers.aspx.cs" Inherits="WWW.AdminUsers" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Administrar Usuarios</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: flex-start;
            padding: 40px 20px;
        }
        .admin-card {
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 900px;
        }
        .admin-header {
            margin-bottom: 10px;
            border-bottom: 1px solid #eee;
            padding-bottom: 15px;
        }
        .admin-header h2 { color: #333; font-size: 28px; }
        .add-button-container {
            text-align: left; /* Alineamos el botón de añadir a la derecha */
            margin-bottom: 20px;
        }
        .user-list { list-style: none; padding: 0; }
        
        /* --- AJUSTE 1 --- */
        .user-list-item {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 15px 10px;
            border-bottom: 1px solid #f0f0f0;
            gap: 15px; /* Añadimos un espacio mínimo entre elementos */
        }

        .user-list-item:last-child { border-bottom: none; }
        
        /* --- AJUSTE 2 --- */
        .user-info {
            flex-grow: 1;
            min-width: 0; /* Permite que este contenedor se encoja correctamente */
        }

        .user-name { font-size: 16px; font-weight: 600; color: #333; }
        
        /* --- AJUSTE 3 --- */
        .user-email {
            font-size: 14px;
            color: #777;
            overflow-wrap: break-word; /* Permite que el texto largo se divida */
        }
        
        /* --- AJUSTE 4 --- */
        .user-actions {
            flex-shrink: 0; /* Evita que el contenedor de los botones se encoja */
        }

        .user-actions a {
            margin-left: 10px;
            text-decoration: none;
            padding: 5px 12px;
            border-radius: 5px;
            font-size: 14px;
            transition: all 0.2s;
            color: white;
        }

        .btn {
            padding: 10px 18px;
            border: none;
            border-radius: 6px;
            font-size: 15px;
            font-weight: 600;
            cursor: pointer;
            transition: transform 0.2s, box-shadow 0.2s;
            text-decoration: none;
            color: white !important;
        }
        .btn:hover { transform: translateY(-2px); }
        .btn-primary { background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); box-shadow: 0 5px 20px rgba(102, 126, 234, 0.4); }
        .user-actions .btn-edit { background-color: #007bff; }
        .user-actions .btn-edit:hover { background-color: #0056b3; }
        .user-actions .btn-delete { background-color: #dc3545; }
        .user-actions .btn-delete:hover { background-color: #c82333; }
        
        .footer-link { text-align: center; margin-top: 25px; }
        .footer-link a { color: #667eea; text-decoration: none; font-weight: 500; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="admin-card">
            <div class="admin-header">
                <h2>Administrar Usuarios</h2>
            </div>
            <div class="add-button-container">
                <asp:Button ID="btnAddUser" runat="server" Text="Añadir Nuevo Usuario" CssClass="btn btn-primary" OnClick="btnAddUser_Click" />
            </div>

            <asp:Repeater ID="rptUsers" runat="server" OnItemCommand="rptUsers_ItemCommand">
                <HeaderTemplate>
                    <ul class="user-list">
                </HeaderTemplate>
                <ItemTemplate>
                    <li class="user-list-item">
                        <div class="user-info">
                            <div class="user-name"><%# Eval("Name") %> <%# Eval("LastName") %></div>
                            <div class="user-email"><%# Eval("Email") %></div>
                        </div>
                        <div class="user-actions">
                            <asp:LinkButton ID="btnEdit" runat="server" CssClass="btn-edit" CommandName="Edit" CommandArgument='<%# Eval("Id") %>'>Editar</asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn-delete" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'>Borrar</asp:LinkButton>
                        </div>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
            
            <div class="footer-link">
                <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="~/MainView.aspx">Volver a la página principal</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>