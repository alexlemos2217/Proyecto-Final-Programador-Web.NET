using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Logica;
using EntidadesCompartidas;
using System.Drawing;

public partial class VentaPasajes : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!IsPostBack)
        {
            CargarDropDownList();
        }
    }

    // ===== AGREGAR PASAJE ===== //
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            string vueloDdl = ddlVuelo.SelectedValue;
            Vuelo vuelo = LogicaVuelo.Buscar(vueloDdl);

            string numeroPasaporte = ddlCliente.SelectedValue;
            EntidadesCompartidas.Cliente cliente = LogicaCliente.Buscar(numeroPasaporte);

            Pasaje pasaje = new Pasaje(vuelo, cliente, 0, DateTime.MinValue, 0.0);

            LogicaPasaje.Agregar(pasaje);

            int numeroPasaje = pasaje.NumVenta;
            DateTime fecha = pasaje.Fecha;
            double precio = pasaje.Precio;

            lblError.ForeColor = Color.Green;
            lblError.Text = "Alta de pasaje Codigo: " + numeroPasaje + ", Fecha: " + fecha + ", Precio: " + precio + " , Vuelo: " + pasaje.oVuelo.Codigo + ", Cliente " + pasaje.oCliente.Nombre;
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = "Error al agregar pasaje: " + ex.Message;
        }
    }

    // ===== CARGAR DROPDOWNLIST ===== //
    private void CargarDropDownList()
    {
        try
        {
            List<Vuelo> colVuelos = LogicaVuelo.ListarVuelos();
            List<EntidadesCompartidas.Cliente> colClientes = LogicaCliente.ListarClientes();

            // Limpiar el ddl
            ddlCliente.Items.Clear();
            ddlVuelo.Items.Clear();

            ddlCliente.Items.Add(new ListItem("--------------------------------", ""));

            ddlVuelo.Items.Add(new ListItem("--------------------------------"));

            foreach (Vuelo vuelo in colVuelos)
            {
                ddlVuelo.Items.Add(new ListItem(vuelo.Codigo));
            }

            foreach (EntidadesCompartidas.Cliente cliente in colClientes)
            {
                ddlCliente.Items.Add(new ListItem(cliente.Nombre, cliente.NumPasaporte));
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblError.ForeColor = Color.Red;
        }
    }

    // ===== LIMPIAR ===== //
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        ddlCliente.SelectedIndex = 0;
        ddlVuelo.SelectedIndex = 0;
    }
}