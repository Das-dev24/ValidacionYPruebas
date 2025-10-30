<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="WWW.AddUser" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Añadir Nuevo Usuario</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        /* (Se pueden usar los mismos estilos del formulario de 'AddActivity') */
        * { margin: 0; padding: 0; box-sizing: border-box; }
        html, body, form { height: 100%; }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: #333;
            display: flex;
            justify-content: center;
            align-items: flex-start;
            padding: 40px;
            overflow-y: auto;
        }
        .form-card {
            background: white; padding: 40px; border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1);
            width: 100%; max-width: 600px;
        }
        .form-card h1 {
            font-size: 28px; margin-bottom: 30px; border-bottom: 1px solid #eee;
            padding-bottom: 20px; text-align: center;
        }
        .form-group { margin-bottom: 20px; }
        .form-label { display: block; font-weight: 600; margin-bottom: 8px; color: #555; }
        .form-control {
            width: 100%; padding: 12px; border: 1px solid #ddd;
            border-radius: 6px; font-size: 16px; transition: border-color 0.2s;
        }
        .form-control:focus { outline: none; border-color: #667eea; }
        .form-row { display: flex; gap: 20px; }
        .form-row .form-group { flex: 1; }
        .btn-container {
            display: flex; justify-content: flex-end; gap: 15px;
            margin-top: 30px; border-top: 1px solid #eee; padding-top: 20px;
        }
        .btn {
            padding: 12px 25px; border: none; border-radius: 6px; font-size: 16px;
            font-weight: 600; cursor: pointer; transition: all 0.2s;
            text-decoration: none; color: white !important;
        }
        .btn:hover { transform: translateY(-2px); box-shadow: 0 4px 10px rgba(0,0,0,0.1); }
        .btn-primary { background-color: #667eea; }
        .btn-primary:hover { background-color: #5a67d8; }
        .btn-secondary { background-color: #888; }
        .btn-secondary:hover { background-color: #777; }
        .validation-error { color: #dc3545; font-size: 14px; margin-top: 5px; }
        .checkbox-group { display: flex; align-items: center; gap: 10px; margin-top: 10px; }
    </style>
</head>
<body>
    <form id="formAddUser" runat="server">
        <div class="form-card">
            <h1>Crear Nuevo Usuario</h1>

            <div class="form-row">
                <div class="form-group">
                    <asp:Label ID="lblName" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                        ErrorMessage="El nombre es obligatorio." CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblLastName" runat="server" Text="Apellidos" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                        ErrorMessage="El apellido es obligatorio." CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El email es obligatorio." CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="El formato del email no es válido." CssClass="validation-error" Display="Dynamic"></asp:RegularExpressionValidator>
            </div>

            <div class="form-group">
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es obligatoria." CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
            
            <div class="form-group">
                <asp:Label ID="lblIsSuperUser" runat="server" Text="Permisos de Administrador" CssClass="form-label"></asp:Label>
                <div class="checkbox-group">
                    <asp:CheckBox ID="chkIsSuperUser" runat="server" />
                    <asp:Label ID="lblCheck" runat="server" Text="Conceder permisos de administrador a este usuario." AssociatedControlID="chkIsSuperUser"></asp:Label>
                </div>
            </div>

            <div class="btn-container">
                <asp:Button ID="btnSave" runat="server" Text="Guardar Usuario" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" CssClass="btn btn-secondary" CausesValidation="false" />
            </div>

            <asp:ValidationSummary ID="vsErrors" runat="server" CssClass="validation-error" HeaderText="Por favor, corrige los siguientes errores:" />
            <asp:Label ID="lblError" runat="server" CssClass="validation-error" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>