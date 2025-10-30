<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainView.aspx.cs" Inherits="WWW.MainView" %>
<%@ Import Namespace="Practica.Model" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mis Actividades</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <style>
        * { margin: 0; padding: 0; box-sizing: border-box; }
        html, body, form { height: 100%; }
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: #333;
        }
        
        .main-header {
            background-color: rgba(0, 0, 0, 0.2); padding: 15px 40px;
            display: flex; justify-content: space-between; align-items: center;
            color: white; box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        .user-info .user-name { font-size: 18px; font-weight: 600; }
        .user-nav { display: flex; align-items: center; gap: 15px; }

        .main-container {
            display: flex; justify-content: center; align-items: flex-start;
            padding: 40px; overflow-y: auto;
            height: calc(100% - 70px);
        }
        .content-card {
            background: white; padding: 40px; border-radius: 10px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.1); width: 100%; max-width: 900px;
            text-align: left;
        }

        .card-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
            border-bottom: 1px solid #eee;
            padding-bottom: 15px;
        }
        .card-header h1 {
            font-size: 32px;
        }
        
        .activity-list { list-style: none; }
        .activity-item {
            padding: 15px 0;
            border-bottom: 1px solid #f0f0f0;
        }
        .activity-item:last-child { border-bottom: none; }
        .activity-header {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 8px;
        }
        .activity-title { font-size: 18px; font-weight: 600; }
        .activity-date { font-size: 14px; color: #888; }
        .activity-description { font-size: 15px; color: #555; margin-bottom: 8px; }
        .activity-notes {
            font-size: 14px;
            color: #777;
            background-color: #f9f9f9;
            border-left: 3px solid #667eea;
            padding: 8px 12px;
            border-radius: 4px;
        }
        .no-activities-panel p { font-size: 18px; color: #666; text-align: center; padding: 40px 0; }
        
        .btn { padding: 10px 20px; border: none; border-radius: 6px; font-size: 14px; font-weight: 600; cursor: pointer; transition: all 0.2s; text-decoration: none; color: white !important; }
        .btn:hover { transform: translateY(-2px); box-shadow: 0 4px 10px rgba(0,0,0,0.1); }
        .btn-profile { background-color: rgba(255, 255, 255, 0.2); border: 1px solid rgba(255, 255, 255, 0.5); }
        .btn-profile:hover { background-color: rgba(255, 255, 255, 0.3); }
        .btn-logout { background-color: #dc3545; }
        .btn-logout:hover { background-color: #c82333; }
        .btn-primary { background-color: #667eea; }
        .btn-primary:hover { background-color: #5a67d8; }

        .activity-actions {
            margin-top: 15px;
            text-align: right; 
        }
        .btn-edit { background-color: #17a2b8; }
        .btn-edit:hover { background-color: #138496; }
        
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
                <div class="card-header">
                    <h1>Mis Actividades</h1>
                    <asp:Button ID="btnAddActivity" runat="server" Text="Añadir Actividad" OnClick="btnAddActivity_Click" CssClass="btn btn-primary" />
                </div>
                
                <asp:Repeater ID="rptActivities" runat="server" Visible="false">
                    <HeaderTemplate>
                        <ul class="activity-list">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li class="activity-item">
                            <div class="activity-header">
                                <span class="activity-title"><strong><%# Eval("TypeActivity") %></strong>: <%# Eval("Name") %></span>
                                <span class="activity-date"> <%# Eval("StartTime", "{0:dd/MM/yyyy HH:mm}") %></span>
                            </div>
                            <p class="activity-description"><%# ((Activity)Container.DataItem).ObtainActivity() %></p>
                            <p class="activity-notes" runat="server" visible='<%# !string.IsNullOrEmpty(Eval("Notes") as string) %>'>
                                <strong>Notas:</strong> <%# Eval("Notes") %>
                            </p>
                            
                            <div class="activity-actions">
                                <asp:Button ID="btnEdit" runat="server" 
                                    Text="Editar" 
                                    CssClass="btn btn-edit" 
                                    OnClick="btnEdit_Click"
                                    CommandArgument='<%# Eval("Id") %>' />
                            </div>
                            </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul>
                    </FooterTemplate>
                </asp:Repeater>
                
                <asp:Panel ID="pnlNoActivities" runat="server" Visible="false" CssClass="no-activities-panel">
                    <p>Aún no has registrado ninguna actividad. ¡Anímate a empezar!</p>
                </asp:Panel>
            </div>
        </main>
    </form>
</body>
</html>