    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Principal.aspx.cs" Inherits="Principal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="height: 877px">
    
        <br />
        <br />
        <br />
        <div style="height: 611px">
            
            <asp:Label ID="lblTitulo" runat="server" Font-Bold="True" Font-Size="XX-Large" 
                ForeColor="#336666" Text="Aeropuertos Americanos"></asp:Label>
            <br />
            <br />
        <asp:LinkButton ID="idLogueoClientes" runat="server" 
            PostBackUrl="~/LoginCliente.aspx" Font-Underline="False" ForeColor="#336666">Login Clientes</asp:LinkButton>
            <br />
        <asp:LinkButton ID="idLogueoEmpleados" runat="server" 
            PostBackUrl="~/LoginEmpleado.aspx" Font-Underline="False" ForeColor="#336666">Login Empleados</asp:LinkButton>
            <br />
            <br />
            
            <asp:DropDownList ID="ddlOpciones" runat="server" AutoPostBack="True" 
                onselectedindexchanged="ddlOpciones_SelectedIndexChanged">
                <asp:ListItem Value="-------------------------">-----------------------------</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="lblError" runat="server"></asp:Label>
            <br />
            <br />
            <div align="center">

                <asp:Label ID="lblPartidas" runat="server"></asp:Label>

            <br />
                <br />
                <asp:GridView ID="gvPartidas" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                    BorderStyle="Double" BorderWidth="3px" CellPadding="5" GridLines="Horizontal" 
                    onselectedindexchanged="ddlOpciones_SelectedIndexChanged" PageSize="5" 
                    Width="592px" onpageindexchanging="gvPartidas_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText=" FECHA DE PARTIDA" DataField="fecHorSalida" />
                        <asp:BoundField HeaderText="DESTINO" DataField="aeropuertoLlegada.nombre" />
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#333333" />
                    <HeaderStyle BackColor="#336666" BorderStyle="Solid" Font-Bold="True" 
                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                        Width="150px" />
                    <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Center" 
                        VerticalAlign="Middle" />
                    <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                    <SortedAscendingHeaderStyle BackColor="#487575" />
                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                    <SortedDescendingHeaderStyle BackColor="#275353" />
                </asp:GridView>
            </div>
            <div>

                <br />
                <div align="center">
                <br />
                    <asp:Label ID="lblArribos" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="gvArribos" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                        BorderStyle="Double" BorderWidth="3px" CellPadding="5" GridLines="Horizontal" 
                        onselectedindexchanged="ddlOpciones_SelectedIndexChanged" PageSize="5" 
                        Width="592px" onpageindexchanging="gvArribos_PageIndexChanging">
                        <Columns>
                            <asp:BoundField HeaderText="FECHA DE PARTIDA" DataField="FecHorSalida" />
                            <asp:BoundField HeaderText="DESTINO" DataField="AeropuertoLlegada.Nombre" />
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#333333" />
                        <HeaderStyle BackColor="#336666" BorderStyle="Solid" Font-Bold="True" 
                            ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                            Width="150px" />
                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Center" 
                            VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                        <SortedAscendingHeaderStyle BackColor="#487575" />
                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                        <SortedDescendingHeaderStyle BackColor="#275353" />
                    </asp:GridView>
                </div>

            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
