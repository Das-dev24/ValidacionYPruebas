<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainView.aspx.cs" Inherits="WWW.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página Principal</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        
        html, body, form {
            height: 100%;
        }

        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: #333;
        }
        
        /* --- Barra de Navegación Superior --- */
        .main-header {
            background-color: rgba(0, 0, 0, 0.2);
            padding: 15px 40px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            color: white;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        .user-info .user-name {
            font-size: 18px;
            font-weight: 600;
        }
        .user-nav {
            display: flex;
            align-items: center;
            gap: 15px; /* Espacio entre botones */
        }

        /* --- Contenedor Principal --- */
        .main-container {
            display: flex;
            justify-content: center;
            align-items: center;
            padding: 40px;
            height: calc(100% - 70px); /* Ocupa el resto de la altura */
        }
        .content-card {
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
            width: 100%;
            max-width: 900px;
            text-align: center;
        }
        .content-card h1 {
            font-size: 32px;
            margin-bottom: 15px;
        }
        .content-card p {
            font-size: 18px;
            color: #666;
        }
        
        /* --- Estilos de Botones para la Navegación --- */
        .btn {
            padding: 8px 16px;
            border: none;
            border-radius: 6px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.2s;
            text-decoration: none;
            color: white !important;
        }
        .btn:hover { transform: translateY(-2px); }
        .btn-profile {
            background-color: rgba(255, 255, 255, 0.2);
            border: 1px solid rgba(255, 255, 255, 0.5);
        }
        .btn-profile:hover { background-color: rgba(255, 255, 255, 0.3); }
        .btn-logout { background-color: #dc3545; }
        .btn-logout:hover { background-color: #c82333; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header class="main-header">
            <div class="user-info">
                <asp:Label ID="lblUser" runat="server" CssClass="user-name" Text="Usuario"></asp:Label>
            </div>
            <nav class="user-nav">
                <asp:Button ID="btnProfile" runat="server" Text="Perfil" OnClick="btnProfile_Click" CssClass="btn btn-profile" />
                <asp:Button ID="btnLogOut" runat="server" Text="Salir" OnClick="btnLogOut_Click" CssClass="btn btn-logout" />
            </nav>
        </header>

        <main class="main-container">
            <div class="content-card">
                <h1>Bienvenido a la Aplicación</h1>
                <p>Desde aquí podrás acceder a las diferentes secciones.</p>
                <br />
                <p>Este es el contenido principal de tu página de inicio.</p>
            </div>
        </main>
    </form>
</body>
</html>