<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginEmpleado.aspx.cs" Inherits="LoginEmpleado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            font-size: xx-large;
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; height: 400px;">
                <strong><em><span class="style1" style="color: #336666">Empleados</span></em></strong><br />
        <br />
        <br />
    <div style="text-align: center">
        <table style="width: 271px" align="center" >
            <tr>
                <td style="width: 347px">
                    Usuario: &nbsp;&nbsp;
                    <asp:TextBox ID="TxtNomUsu" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 347px">
                    Password:&nbsp;
                    <asp:TextBox ID="TxtPassUsu" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 347px; height: 21px; text-align: right">
                    <span style="color: #ff0066">
                    <asp:Button ID="BtnLogueo" runat="server" OnClick="BtnLogueo_Click" Text="Login" /></span></td>
            </tr>
            <tr>
                <td style="width: 347px; height: 21px; text-align: center">
                    <asp:Label ID="LblError" runat="server" Width="320px" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
    
    </div>
                <br />
                <asp:LinkButton ID="idVolver" runat="server" PostBackUrl="~/Principal.aspx" 
                    Font-Underline="False" ForeColor="#336666">Volver</asp:LinkButton>
        <br />
    
       
    
    </div>
    </form>
</body>
</html>
