<%@ Page Title="" Language="C#" MasterPageFile="~/Empleado.master" AutoEventWireup="true" CodeFile="BienvenidaEmpleado.aspx.cs" Inherits="BienvenidaEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
<br />
    <div align="center">
        <asp:Label ID="lblBienvenida" runat="server" Font-Bold="True" 
            Font-Size="XX-Large" ForeColor="#336666">Bienvenido</asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblDatos" runat="server" Font-Size="Medium"></asp:Label>
    </div>
    <br />
<br />
</asp:Content>

