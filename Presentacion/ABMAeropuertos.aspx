<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado.master" AutoEventWireup="true" CodeFile="ABMAeropuertos.aspx.cs" Inherits="ABMAeropuertos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style4
        {
            width: 62px;
        }
        .style10
    {
        height: 21px;
        width: 114px;
    }
    .style11
    {
            width: 114px;
        }
        .style12
        {
            height: 21px;
            width: 91px;
        }
        .style13
        {
            width: 91px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p</p>
    <br />
    <br />
    <div align="center">
        
        &nbsp;Mantenimiento Aeropuertos<br />
        <br />
        <table align="center" style="width: 29%; height: 201px;">
            <tr>
                <td align="justify" class="style10">
                    Código:</td>
                <td class="style12">
                    <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                </td>
                <td class="style3">
                    <asp:Button ID="btnBuscar" runat="server" Font-Bold="True" 
                        OnClick="btnBuscar_Click" style="margin-left: 0px" Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td align="justify" class="style11">
                    Nombre:</td>
                <td class="style13">
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td align="justify" class="style11">
                    Dirección:</td>
                <td class="style13">
                    <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style11" align="left">
                    Ciudad</td>
                <td class="style13">
                    <asp:DropDownList ID="ddlCiudad" runat="server" AutoPostBack="True" 
                        Height="25px" Width="160px">
                        <asp:ListItem>-------------------------------------</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style11" align="left">
                    Impuesto Partida</td>
                <td class="style13">
                    <asp:TextBox ID="txtPartida" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style11" align="left">
                    Impuesto Llegada</td>
                <td class="style13">
                    <asp:TextBox ID="txtLlegada" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style11">
                </td>
                <td class="style13">
                    &nbsp;</td>
                <td class="style4">
                    <asp:Button ID="btnLimpiar" runat="server" Font-Bold="True" 
                        OnClick="btnLimpiar_Click" style="margin-left: 0px" Text="Limpiar" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnAgregar" runat="server" Font-Bold="True" 
                        OnClick="btnAgregar_Click" Text="Agregar" />
                    <asp:Button ID="btnModificar" runat="server" Font-Bold="True" 
                        OnClick="btnModificar_Click" Text="Modificar" />
                    <asp:Button ID="btnEliminar" runat="server" Font-Bold="True" 
                        OnClick="btnEliminar_Click" Text="Eliminar" />
                </td>
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

