<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WWW.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mi Perfil</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 20px;
        }

        /* --- Estilos para la tarjeta principal del perfil --- */
        .profile-card {
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
            width: 40vw; 
            max-width: 700px;
        }

        .profile-header {
            text-align: center;
            margin-bottom: 20px; /* Reducido de 30px */
            border-bottom: 1px solid #eee;
            padding-bottom: 15px; /* Reducido de 20px */
        }

        .profile-header h2 {
            color: #333;
            font-size: 28px;
        }

        .section-title {
            color: #444;
            font-size: 20px;
            font-weight: 600;
            margin-top: 25px; /* Reducido de 30px */
            margin-bottom: 15px;
            padding-bottom: 10px;
            border-bottom: 1px solid #eee;
        }

        /* --- Estilos para la sección de información --- */
        .info-row {
            display: flex;
            justify-content: space-between;
            padding: 8px 0; /* Reducido de 12px */
            font-size: 15px;
            border-bottom: 1px solid #f0f0f0;
        }

        .info-row strong { color: #555; }
        .info-row span { color: #333; }

        /* --- Estilos para los formularios --- */
        .form-group {
            margin-bottom: 15px; /* Reducido de 20px */
        }

        .form-group label {
            display: block;
            margin-bottom: 6px; /* Reducido de 8px */
            color: #333;
            font-weight: 500;
            font-size: 14px;
        }

        .form-group input[type="text"],
        .form-group input[type="password"] {
            width: 100%;
            padding: 10px 12px; /* Reducido de 12px 15px */
            border: 2px solid #e0e0e0;
            border-radius: 6px;
            font-size: 14px;
            transition: border-color 0.3s;
        }

        .form-group input[type="text"]:focus,
        .form-group input[type="password"]:focus {
            outline: none;
            border-color: #667eea;
        }

        /* --- Estilos para los botones --- */
        .btn {
            width: 100%;
            padding: 12px; /* Reducido de 14px */
            border: none;
            border-radius: 6px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: transform 0.2s, box-shadow 0.2s;
            margin-top: 5px; /* Reducido de 10px */
        }

        .btn:hover { transform: translateY(-2px); }

        .btn-primary {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            box-shadow: 0 5px 20px rgba(102, 126, 234, 0.4);
        }

        .btn-secondary {
            background: #6c757d;
            color: white;
            box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
        }

        /* --- Mensajes flotantes (esquina superior derecha) --- */
        .message-container {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1050;
        }

        .alert {
            padding: 15px;
            margin-bottom: 10px;
            border-radius: 6px;
            color: white;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            opacity: 0.95;
        }

        .alert-success { background-color: #28a745; }
        .alert-danger { background-color: #dc3545; }

        /* --- Enlace del footer --- */
        .footer-link {
            text-align: center;
            margin-top: 25px; /* Reducido de 30px */
        }

        .footer-link a {
            color: #667eea;
            text-decoration: none;
            font-weight: 500;
            transition: color 0.3s;
        }

        .footer-link a:hover { color: #764ba2; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="message-container">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" Visible="false"></asp:Label>
            <asp:Label ID="lblPasswordMessage" runat="server" EnableViewState="false" Visible="false"></asp:Label>
        </div>

        <div class="profile-card">
            <div class="profile-header">
                <h2>Mi Perfil</h2>
            </div>

            <h5 class="section-title">Información de la Cuenta</h5>
            <div class="info-row">
                <strong>Nombre Completo:</strong>
                <span><asp:Label ID="lblFullName" runat="server"></asp:Label></span>
            </div>
            <div class="info-row">
                <strong>Email Registrado:</strong>
                <span><asp:Label ID="lblEmailInfo" runat="server"></asp:Label></span>
            </div>
            <div class="info-row">
                <strong>Estado de la Suscripción:</strong>
                <span><asp:Label ID="lblSubscriptionStatus" runat="server"></asp:Label></span>
            </div>
            <div class="info-row">
                <strong>Estado de la Cuenta:</strong>
                <span><asp:Label ID="lblAccountStatus" runat="server"></asp:Label></span>
            </div>
            <div class="info-row">
                <strong>Último Inicio de Sesión:</strong>
                <span><asp:Label ID="lblLastLogin" runat="server"></asp:Label></span>
            </div>

            <h5 class="section-title">Editar mis Datos</h5>
            <div class="form-group">
                <label for="txtName">Nombre:</label>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtLastName">Apellidos:</label>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtEmail">Email:</label>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </div>
            <asp:Button ID="btnSaveChanges" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnSaveChanges_Click" />

            <h5 class="section-title">Cambiar Contraseña</h5>
            <div class="form-group">
                <label>Contraseña Actual:</label>
                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Nueva Contraseña:</label>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div class="form-group">
                <label>Confirmar Nueva Contraseña:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button ID="btnChangePassword" runat="server" Text="Cambiar Contraseña" CssClass="btn btn-secondary" OnClick="btnChangePassword_Click" />

            <div class="footer-link">
                <asp:HyperLink ID="hlBack" runat="server" NavigateUrl="~/MainView.aspx">Volver a la página principal</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>