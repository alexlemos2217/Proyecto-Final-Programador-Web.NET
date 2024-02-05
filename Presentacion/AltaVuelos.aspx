<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado.master" AutoEventWireup="true" CodeFile="AltaVuelos.aspx.cs" Inherits="AltaVuelos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style8
        {
            height: 21px;
            width: 153px;
        }
        .style9
        {
            width: 153px;
        }
        .style4
        {
            width: 62px;
        }
        .style10
        {
            height: 21px;
            width: 161px;
        }
        .style11
        {
            width: 161px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div align="center">
        
        Agregar Vuelos<br />
        <br />
        <table align="center" style="width: 26%; height: 289px;">
            <tr>
                <td align="justify" class="style11">
                    Salida:</td>
                <td class="style9">
                    <asp:TextBox ID="txtSalida" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style11" align="left">
                    Llegada</td>
                <td class="style9">
                    <asp:TextBox ID="txtLlegada" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td align="justify" class="style11">
                    Precio:</td>
                <td class="style9">
                    <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style11" align="left">
                    Asientos:</td>
                <td class="style9">
                    <asp:TextBox ID="txtAsientos" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style11" align="left">
                    Aeropuerto Salida</td>
                <td class="style9">
                    <asp:DropDownList ID="ddlAeroSalida" runat="server" AutoPostBack="True" 
                        Height="25px" Width="160px">
                        <asp:ListItem Value="-------------------------">-------------------------------------</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style11" align="left">
                    Aeropuerto Llegada</td>
                <td class="style9">
                    <asp:DropDownList ID="ddlAeroLlegada" runat="server" AutoPostBack="True" 
                        Height="25px" Width="160px">
                        <asp:ListItem Value="-------------------------">-------------------------------------</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style11">
                </td>
                <td class="style9">
                    <asp:Button ID="btnAgregar" runat="server" Font-Bold="True" 
                        OnClick="btnAgregar_Click" Text="Agregar" />
                </td>
                <td class="style4">
                    <asp:Button ID="btnLimpiar" runat="server" Font-Bold="True" 
                        OnClick="btnLimpiar_Click" Text="Limpiar" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblError" runat="server" Font-Bold="False"></asp:Label>
        &nbsp;<br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="False" 
            Font-Underline="False" PostBackUrl="~/BienvenidaEmpleado.aspx" 
            ForeColor="#336666">Volver</asp:LinkButton>
        
    </div>
    <br />
</asp:Content>

