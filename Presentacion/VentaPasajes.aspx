<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado.master" AutoEventWireup="true" CodeFile="VentaPasajes.aspx.cs" Inherits="VentaPasajes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style4
        {
            width: 62px;
        }
        .style12
        {
            width: 116px;
        }
        .style13
        {
            width: 150px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div>
        
    <div align="center" style="height: 431px; width: 1453px">
        
        Venta de pasajes<br />
        <br />
        <table align="center" style="width: 24%; height: 221px;">
            <tr>
                <td align="left" class="style12">
                    &nbsp;Vuelo:</td>
                <td class="style13">
                    <asp:DropDownList ID="ddlVuelo" runat="server" AutoPostBack="True" 
                        Height="25px" Width="160px">
                        <asp:ListItem Value="-------------------------">-------------------------------------</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style12" align="left">
                    &nbsp;Cliente:</td>
                <td class="style13">
                    <asp:DropDownList ID="ddlCliente" runat="server" AutoPostBack="True" 
                        Height="25px" Width="160px">
                        <asp:ListItem Value="-------------------------">-------------------------------------</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style12">
                    &nbsp;</td>
                <td class="style13" align="center">
                    <asp:Button ID="btnAgregar" runat="server" Font-Bold="True" 
                        OnClick="btnAgregar_Click" Text="Agregar" />
                    <asp:Button ID="btnLimpiar" runat="server" Font-Bold="True" 
                        OnClick="btnLimpiar_Click" Text="Limpiar" />
                </td>
                <td class="style4">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblError" runat="server" Font-Bold="False"></asp:Label>
        &nbsp;<br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="False" 
            Font-Underline="False" PostBackUrl="~/BienvenidaEmpleado.aspx" 
            ForeColor="#336666">Volver</asp:LinkButton>
        
    </div>
        
    </div>
    <br />
</asp:Content>

