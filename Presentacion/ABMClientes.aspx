<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado.master" AutoEventWireup="true" CodeFile="ABMClientes.aspx.cs" Inherits="ABMClientes" %>

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
        .style12
    {
        width: 94px;
        height: 29px;
    }
    .style13
    {
        width: 153px;
        height: 29px;
    }
    .style14
    {
        width: 62px;
        height: 29px;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div>

    <div align="center">
        
        Mantenimiento Clientes<br />
        <br />
        <table align="center" style="width: 25%; height: 153px;">
            <tr>
                <td align="justify" class="style10">
                    Pasaporte:</td>
                <td class="style8">
                    <asp:TextBox ID="txtPasaporte" runat="server"></asp:TextBox>
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
                    Tarjeta:</td>
                <td class="style9">
                    <asp:TextBox ID="txtTarjeta" runat="server"></asp:TextBox>
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td align="justify" class="style12">
                    Contraseña:</td>
                <td class="style13">
                    <asp:TextBox ID="txtContrasenia" runat="server"></asp:TextBox>
                </td>
                <td class="style14">
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

    </div>
    <br /> 
</asp:Content>

