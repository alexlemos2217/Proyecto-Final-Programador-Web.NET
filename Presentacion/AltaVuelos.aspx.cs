using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Logica;
using EntidadesCompartidas;
using System.Drawing;

public partial class AltaVuelos : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!IsPostBack)
        {
            txtSalida.Attributes.Add("type", "datetime-local");
            txtLlegada.Attributes.Add("type", "datetime-local");

            CargarDropDownList();
        }
    }

    // ===== AGREGAR VUELO ===== //
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime fechaSalida = Convert.ToDateTime(txtSalida.Text);
            DateTime fechaLlegada = Convert.ToDateTime(txtLlegada.Text);

            double precio = Convert.ToDouble(txtPrecio.Text);
            int asientos = Convert.ToInt32(txtAsientos.Text);

            string aerSalida = ddlAeroSalida.SelectedValue.ToUpper();
            string aerLlegada = ddlAeroLlegada.SelectedValue.ToUpper();

            Aeropuerto salida = LogicaAeropuerto.Buscar(aerSalida);
            Aeropuerto llegada = LogicaAeropuerto.Buscar(aerLlegada);

            Vuelo vuelo = new Vuelo("", fechaSalida, fechaLlegada, precio, asientos, salida, llegada);

            LogicaVuelo.Agregar(vuelo);

            string codigoVuelo = vuelo.Codigo;

            if (!string.IsNullOrEmpty(codigoVuelo))
            {
                lblError.ForeColor = Color.Green;
                lblError.Text = "Alta con éxito. Código de vuelo: " + codigoVuelo;
            }
            else
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "Ocurrió un error inesperado durante la alta del vuelo.";
            }

        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== LIMPIAR ===== //
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        try
        {
            txtSalida.Text = "";
            txtLlegada.Text = "";
            txtPrecio.Text = "";
            txtAsientos.Text = "";

            ddlAeroSalida.SelectedIndex = 0;
            ddlAeroLlegada.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== CARGAR DROPDOWNLIST ===== //
    private void CargarDropDownList()
    {
        try
        {
            List<Aeropuerto> colAeropuertos = LogicaAeropuerto.ListarAeropuertos();

            ddlAeroLlegada.Items.Clear();
            ddlAeroSalida.Items.Clear();

            ddlAeroLlegada.Items.Add(new ListItem("--------------------------------", ""));
            ddlAeroSalida.Items.Add(new ListItem("--------------------------------", ""));

            foreach (Aeropuerto aeropuerto in colAeropuertos)
            {
                ddlAeroLlegada.Items.Add(new ListItem(aeropuerto.Nombre, aeropuerto.Codigo));
                ddlAeroSalida.Items.Add(new ListItem(aeropuerto.Nombre, aeropuerto.Codigo));
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error al cargar los aeropuertos: " + ex.Message;
            lblError.ForeColor = Color.Red;
        }
    }
}