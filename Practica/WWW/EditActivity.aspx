<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditActivity.aspx.cs" Inherits="WWW.EditActivity" UnobtrusiveValidationMode="None" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Editar Actividad</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
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
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1); width: 100%; max-width: 700px;
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
            font-weight: 600; cursor: pointer; transition: all 0.2s; text-decoration: none; color: white !important;
        }
        .btn:hover { transform: translateY(-2px); box-shadow: 0 4px 10px rgba(0,0,0,0.1); }
        .btn-primary { background-color: #667eea; }
        .btn-primary:hover { background-color: #5a67d8; }
        .btn-secondary { background-color: #888; }
        .btn-secondary:hover { background-color: #777; }
        .validation-error { color: #dc3545; font-size: 14px; margin-top: 5px; }
        .specific-panel { padding: 20px; margin-top: 20px; border: 1px dashed #ccc; border-radius: 8px; }
    </style>
</head>
<body>
    <form id="formEditActivity" runat="server">
        <div class="form-card">
            <h1>Editar Actividad</h1>
            
            <asp:HiddenField ID="hdnActivityId" runat="server" />

            <div class="form-group">
                <asp:Label ID="lblActivityType" runat="server" Text="Tipo de Actividad" CssClass="form-label"></asp:Label>
                <asp:DropDownList ID="ddlActivityType" runat="server" CssClass="form-control" Enabled="false">
                    <asp:ListItem Text="-- Selecciona un tipo --" Value=""></asp:ListItem>
                    <asp:ListItem Text="Carrera" Value="Carrera"></asp:ListItem>
                    <asp:ListItem Text="Ciclismo" Value="Ciclismo"></asp:ListItem>
                    <asp:ListItem Text="Natación" Value="Natacion"></asp:ListItem>
                    <asp:ListItem Text="Gimnasio" Value="Gimnasio"></asp:ListItem>
                    <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="lblName" runat="server" Text="Nombre de la Actividad" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="El nombre es obligatorio." CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <asp:Label ID="lblStartTime" runat="server" Text="Fecha y Hora de Inicio" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStartTime" runat="server" ControlToValidate="txtStartTime" ErrorMessage="La fecha de inicio es obligatoria." CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblDuration" runat="server" Text="Duración (en minutos)" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDuration" runat="server" ControlToValidate="txtDuration" ErrorMessage="La duración es obligatoria." CssClass="validation-error" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="rvDuration" runat="server" ControlToValidate="txtDuration" Type="Integer" MinimumValue="1" MaximumValue="99999" ErrorMessage="La duración debe ser mayor que cero." CssClass="validation-error" Display="Dynamic"></asp:RangeValidator>
                </div>
            </div>

            <asp:Panel ID="pnlRunningCycling" runat="server" Visible="false" CssClass="specific-panel">
                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lblDistance" runat="server" Text="Distancia (km)" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtDistance" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSlope" runat="server" Text="Desnivel (metros)" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtSlope" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                 <div class="form-group">
                    <asp:Label ID="lblPlaceRunBike" runat="server" Text="Lugar o Ruta" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="txtPlaceRunBike" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlSwimming" runat="server" Visible="false" CssClass="specific-panel">
                 <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lblSwimDistance" runat="server" Text="Distancia (metros)" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtSwimDistance" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSwimPlace" runat="server" Text="Lugar (Piscina, Mar, etc.)" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtSwimPlace" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlGym" runat="server" Visible="false" CssClass="specific-panel">
                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lblCalories" runat="server" Text="Calorías Quemadas (aprox.)" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtCalories" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblBodyPart" runat="server" Text="Grupo Muscular Trabajado" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtBodyPart" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlOther" runat="server" Visible="false" CssClass="specific-panel">
                <div class="form-row">
                    <div class="form-group">
                        <asp:Label ID="lblOtherActivity" runat="server" Text="Nombre del Deporte o Actividad" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtOtherActivity" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                     <div class="form-group">
                        <asp:Label ID="lblOtherPlace" runat="server" Text="Lugar" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="txtOtherPlace" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </asp:Panel>

            <div class="form-group">
                <asp:Label ID="lblNotes" runat="server" Text="Notas Adicionales (opcional)" CssClass="form-label"></asp:Label>
                <asp:TextBox ID="txtNotes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
            </div>

            <div class="btn-container">
                <asp:Button ID="btnSave" runat="server" Text="Guardar Cambios" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" CssClass="btn btn-secondary" CausesValidation="false" />
            </div>

            <asp:ValidationSummary ID="vsErrors" runat="server" CssClass="validation-error" HeaderText="Por favor, corrige los siguientes errores:" />

        </div>
    </form>
</body>
</html>