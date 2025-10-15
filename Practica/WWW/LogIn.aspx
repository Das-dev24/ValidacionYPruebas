<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="WWW.LogIn" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Iniciar Sesión</title>
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

        .login-container {
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 400px;
        }

        .login-header {
            text-align: center;
            margin-bottom: 30px;
        }

        .login-header h1 {
            color: #333;
            font-size: 28px;
            margin-bottom: 10px;
        }

        .login-header p {
            color: #666;
            font-size: 14px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            margin-bottom: 8px;
            color: #333;
            font-weight: 500;
            font-size: 14px;
        }

        .form-group input {
            width: 100%;
            padding: 12px 15px;
            border: 2px solid #e0e0e0;
            border-radius: 6px;
            font-size: 14px;
            transition: border-color 0.3s;
        }

        .form-group input:focus {
            outline: none;
            border-color: #667eea;
        }
        
        /* --- ESTILOS AÑADIDOS/MODIFICADOS --- */
        /* Mensajes flotantes (esquina superior derecha) */
        .message-container {
            position: fixed;
            top: 20px;
            right: 20px;
            z-index: 1050;
        }

        .alert {
            padding: 15px;
            border-radius: 6px;
            color: white;
            box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            opacity: 0.95;
        }

        .alert-danger { background-color: #dc3545; }
        /* Se eliminó el antiguo estilo .error-message */
        
        .btn-login {
            width: 100%;
            padding: 14px;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            border: none;
            border-radius: 6px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: transform 0.2s, box-shadow 0.2s;
        }

        .btn-login:hover {
            transform: translateY(-2px);
            box-shadow: 0 5px 20px rgba(102, 126, 234, 0.4);
        }

        .forgot-password {
            text-align: center;
            margin-top: 20px;
            font-size: 14px;
        }
        .forgot-password a {
            color: #667eea;
            text-decoration: none;
            transition: color 0.3s;
        }
        .forgot-password a:hover {
            color: #764ba2;
        }
    </style>
</head>
<body>
    <form id="loginForm" runat="server">
        <!-- Contenedor para el mensaje de error flotante -->
        <div class="message-container">
            <asp:Label ID="lblErrorMessage" runat="server" EnableViewState="false" Visible="false"></asp:Label>
        </div>

        <div class="login-container">
            <div class="login-header">
                <h1>Bienvenido</h1>
                <p>Ingresa tus credenciales para continuar</p>
            </div>
            <div class="form-group">
                <label for="txtEmail">Correo Electrónico</label>
                <asp:TextBox 
                    ID="txtEmail" 
                    runat="server" 
                    placeholder="tu@email.com"
                    TextMode="Email">
                </asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtPassword">Contraseña</label>
                <asp:TextBox 
                    ID="txtPassword" 
                    runat="server" 
                    placeholder="••••••••" 
                    TextMode="Password">
                </asp:TextBox>
            </div>

            <asp:Button 
                ID="btnLogin" 
                runat="server" 
                Text="Iniciar Sesión" 
                OnClick="btnLogin_Click" 
                CssClass="btn-login" />

            <div class="forgot-password">
                <a href="/forgot-password">¿Olvidaste tu contraseña?</a>
            </div>
        </div>
    </form>
</body>
</html>