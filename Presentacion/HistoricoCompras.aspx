<%@ Page Title="" Language="C#" MasterPageFile="~/Cliente.master" AutoEventWireup="true" CodeFile="HistoricoCompras.aspx.cs" Inherits="HistoricoCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <div>
        
            <div align="center" style="height: 751px; width: 1518px">

                <asp:Label ID="lblPasaje" runat="server" Font-Size="Large" Font-Bold="True" 
                    ForeColor="#336666"></asp:Label>

            <br />
                <br />
                <asp:GridView ID="gvPasajes" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                    BorderStyle="Double" BorderWidth="3px" CellPadding="5" GridLines="Horizontal" 
                    onselectedindexchanged="ddlOpciones_SelectedIndexChanged" PageSize="5" 
                    Width="592px" onpageindexchanging="gvPasajes_PageIndexChanging">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                        <asp:BoundField DataField="fecha" HeaderText="FECHA" />
                        <asp:BoundField DataField="oVuelo.codigo" HeaderText="VUELO" />
                        <asp:BoundField DataField="precio" HeaderText="PRECIO" />
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
                <br />
                    <asp:Label ID="lblCompleto" runat="server"></asp:Label>
                <br />
                    <br />
                    <asp:GridView ID="gvCompleto" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#336666" 
                        BorderStyle="Double" BorderWidth="3px" CellPadding="5" GridLines="Horizontal" 
                        onselectedindexchanged="ddlOpciones_SelectedIndexChanged" PageSize="5" 
                        Width="592px">
                        <Columns>
                            <asp:BoundField DataField="oVuelo.AeropuertoSalida.Ciudad.NombreCoudad" 
                                HeaderText="SALIDA CIUDAD" />
                            <asp:BoundField DataField="oVuelo.AeropuertoSalida.Ciudad.Pais" 
                                HeaderText="SALIDA PAÍS" />
                            <asp:BoundField DataField="oVuelo.AeropuertoLlegada.Ciudad.NombreCoudad" 
                                HeaderText="LLEGADA CIUDAD" />
                            <asp:BoundField DataField="oVuelo.AeropuertoLlegada.Ciudad.Pais" 
                                HeaderText="LLEGADA PAÍS" />
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
                <br />
                <br />
                <br />
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Principal.aspx" 
                    Font-Bold="False" Font-Underline="False" ForeColor="#336666">Volver</asp:LinkButton>
            </div>
        
    </div>
    <br />
</asp:Content>

