<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="WWW.EditUser" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <title>Editar Usuario</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        /* (Tus estilos se mantienen igual, no hay cambios aquí) */
        * { margin: 0; padding: 0; box-sizing: border-box; }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex; justify-content: center; align-items: center; padding: 20px;
        }
        .edit-card {
            background: white; padding: 30px; border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1); width: 100%; max-width: 800px;
        }
        .edit-header { text-align: center; margin-bottom: 20px; padding-bottom: 15px; border-bottom: 1px solid #eee; }
        .edit-header h2 { color: #333; font-size: 28px; }
        
        .section-title {
            color: #444; font-size: 18px; font-weight: 600; margin-top: 25px;
            margin-bottom: 15px; padding-bottom: 10px; border-bottom: 1px solid #eee;
        }

        .info-grid, .form-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
        }
        .form-group { margin-bottom: 0; }
        .form-group label, .form-group strong {
            display: block; margin-bottom: 6px; color: #555; font-weight: 500; font-size: 14px;
        }
        .form-group input[type="text"], .form-group input[type="password"], .form-group select {
            width: 100%; padding: 10px 12px; border: 2px solid #e0e0e0;
            border-radius: 6px; font-size: 14px; transition: border-color 0.3s;
        }
        .form-group input[type="text"]:focus, .form-group input[type="password"]:focus, .form-group select:focus { outline: none; border-color: #667eea; }
        .form-group-full { grid-column: 1 / -1; }

        .checkbox-group { display: flex; gap: 30px; align-items: center; padding-top: 10px; }
        .form-check { display: flex; align-items: center; }
        .form-check-label { margin-left: 8px; margin-bottom: 0; font-size: 14px; color: #333; }

        .button-group { display: flex; gap: 15px; margin-top: 25px; }
        .btn { width: auto; flex-grow: 1; padding: 12px; border: none; border-radius: 6px; font-size: 16px; font-weight: 600; cursor: pointer; }
        .btn-primary { background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; }
        .btn-secondary { background: #6c757d; color: white; }

        .footer-link { text-align: center; margin-top: 25px; }
        .footer-link a { color: #667eea; text-decoration: none; font-weight: 500; }
        
        .message-container { position: fixed; top: 20px; right: 20px; z-index: 1050; }
        .alert { padding: 15px; border-radius: 6px; color: white; box-shadow: 0 4px 15px rgba(0,0,0,0.1); }
        .alert-success { background-color: #28a745; }
        .alert-danger { background-color: #dc3545; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="message-container">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" Visible="false"></asp:Label>
        </div>

        <div class="edit-card">
            <div class="edit-header">
                <h2>Editar Usuario</h2>
            </div>
            <asp:HiddenField ID="hdnUserId" runat="server" />

            <h5 class="section-title">Datos Informativos</h5>
            <div class="info-grid">
                <div class="form-group"><strong>ID de Usuario:</strong> <asp:Label ID="lblUserId" runat="server"></asp:Label></div>
                <div class="form-group"><strong>Último Login:</strong> <asp:Label ID="lblLastLogin" runat="server"></asp:Label></div>
            </div>

            <h5 class="section-title">Datos Personales y de Estado</h5>
            <div class="form-grid">
                <div class="form-group"><label>Nombre:</label><asp:TextBox ID="txtName" runat="server"></asp:TextBox></div>
                <div class="form-group"><label>Apellidos:</label><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></div>
                
                <div class="form-group"><label>Email:</label><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></div>
                
                <div class="form-group"><label>Estado de la cuenta:</label><asp:DropDownList ID="ddlState" runat="server"></asp:DropDownList></div>
            </div>

            <h5 class="section-title">Permisos</h5>
            <div class="form-group checkbox-group">
                <div class="form-check"><asp:CheckBox ID="chkIsSubscribed" runat="server" /><label class="form-check-label">Suscripción Activa</label></div>
                <div class="form-check"><asp:CheckBox ID="chkIsSuperuser" runat="server" /><label class="form-check-label">Es Superusuario</label></div>
            </div>
            
            <div class="button-group">
                <asp:Button ID="btnSaveChanges" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" />
            </div>

            <h5 class="section-title">Restablecer Contraseña</h5>
            <div class="form-grid">
                <div class="form-group"><label>Nueva Contraseña:</label><asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" placeholder="Dejar en blanco para no cambiar"></asp:TextBox></div>
                <div class="form-grup"><asp:Button ID="btnResetPassword" runat="server" Text="Forzar Cambio de Contraseña" CssClass="btn btn-secondary" OnClick="btnResetPassword_Click" style="margin-top: 22px;"/></div>
            </div>

            <div class="footer-link">
                <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="~/AdminUsers.aspx">Cancelar y volver a la lista</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>