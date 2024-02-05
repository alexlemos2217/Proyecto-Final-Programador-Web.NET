<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado.master" AutoEventWireup="true" CodeFile="ABMCiudades.aspx.cs" Inherits="ABMCiudades" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">

        .style10
        {
            height: 21px;
            width: 94px;
        }
        .style8
        {
            height: 21px;
            width: 153px;
        }
        .style11
        {
            width: 94px;
        }
        .style9
        {
            width: 153px;
        }
        .style4
        {
            width: 62px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div align="center">
        
        Mantenimiento Ciudades<br />
        <br />
        <table align="center" style="width: 25%; height: 153px;">
            <tr>
                <td align="justify" class="style10">
                    Código:</td>
                <td class="style8">
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
                <td class="style9">
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td align="justify" class="style11">
                    País:</td>
                <td class="style9">
                    <asp:TextBox ID="txtPais" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style11">
                </td>
                <td class="style9">
                </td>
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

