<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="WWW.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 182px;
        }
        .auto-style2 {
            width: 37px;
        }
        .auto-style3 {
            width: 460px;
        }
        .auto-style4 {            text-align: center;
        }
        .auto-style5 {
            width: 182px;
            height: 23px;
        }
        .auto-style6 {
            height: 23px;
            text-align: right;
        }
        .auto-style7 {
            width: 37px;
            height: 23px;
        }
        .auto-style8 {
            width: 460px;
            height: 23px;
        }
        .auto-style10 {
            text-align: right;
            width: 444px;
        }
        .newStyle1 {
            text-align: center;
        }
        .auto-style11 {
            height: 23px;
            text-align: right;
            width: 444px;
        }
        .auto-style12 {
            text-align: center;
            width: 444px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    <table style="width:100%;">
        <tr>
            <td class="auto-style5"></td>
            <td class="auto-style11"></td>
            <td class="auto-style7"></td>
            <td class="auto-style8"></td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style4" colspan="3">
                <asp:Label ID="lblTitle" runat="server" style="text-align: center" Text="Inicio de Sesión"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style12">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style10">
                <asp:Label ID="lblUserId" runat="server" Text="Identificador Usuario"></asp:Label>
            </td>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">
                <asp:TextBox ID="tbxUser" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style10">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5"></td>
            <td class="auto-style11">
                <asp:Label ID="lblPassword" runat="server" Text="Contrasena"></asp:Label>
            </td>
            <td class="auto-style7"></td>
            <td class="auto-style8">
                <asp:TextBox ID="tbxPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td class="auto-style12">&nbsp;</td>
            <td class="auto-style2">&nbsp;</td>
            <td class="auto-style3">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5"></td>
            <td class="auto-style11">
                <asp:Button ID="btnPasswdReset" runat="server" style="text-align: right" Text="Recuperar la Contrasena" />
            </td>
            <td class="auto-style7"></td>
            <td class="auto-style8">
                <asp:Button ID="btnLogin" runat="server" Text="Aceptar" Width="140px" OnClick="btnLogin_Click" />
            </td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style5"></td>
            <td class="auto-style11"></td>
            <td class="auto-style7"></td>
            <td class="auto-style8"></td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style5"></td>
            <td class="auto-style11"></td>
            <td class="auto-style7"></td>
            <td class="auto-style8"></td>
            <td class="auto-style6"></td>
        </tr>
        <tr>
            <td class="auto-style5">&nbsp;</td>
            <td class="newStyle1" colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblFailedLogIn" runat="server" ForeColor="Red" style="text-align: center" Text="Credenciales no Validos" Visible="False"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td class="auto-style6">&nbsp;</td>
        </tr>
    </table>
    </form>
    </body>
</html>
