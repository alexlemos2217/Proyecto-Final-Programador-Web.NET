using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

using Logica;
using System.Drawing;
using EntidadesCompartidas;

public partial class ABMAeropuertos : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!IsPostBack)
        {
            LimpioFormulario();
            CargarDropDownList();
        }
    }

    // ===== LIMPIAR ===== //
    private void LimpioFormulario()
    {
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;

        txtCodigo.Text = "";
        txtCodigo.Enabled = true;
        txtDireccion.Text = "";
        txtDireccion.Enabled = false;
        txtLlegada.Text = "";
        txtLlegada.Enabled = false;
        txtPartida.Text = "";
        txtPartida.Enabled = false;
        txtNombre.Text = "";
        txtNombre.Enabled = false;
        ddlCiudad.Enabled = false;

        ddlCiudad.SelectedIndex = 0;
    }

    // ===== ACTIVAR BOTONES ===== //
    private void ActivoBotones(bool esAlta = true)
    {
        btnModificar.Enabled = esAlta;
        btnEliminar.Enabled = esAlta;
        btnAgregar.Enabled = !esAlta;
        btnBuscar.Enabled = false;

        ddlCiudad.Enabled = true;

        txtCodigo.Enabled = false;
        txtDireccion.Enabled = true;
        txtLlegada.Enabled = true;
        txtPartida.Enabled = true;
        txtNombre.Enabled = true;
    }

    // ===== BUSCAR AEROPUERTOS ===== //
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text.Trim()))
            {
                throw new Exception("El código no puede estar vacío");
            }
            if (!Regex.IsMatch(txtCodigo.Text, "^[a-zA-Z]{3}$"))
            {
                throw new Exception("El código debe tener tres letras");
            }

            string codigo = txtCodigo.Text.Trim();

            Aeropuerto aero = LogicaAeropuerto.Buscar(codigo);

            if (aero != null)
            {
                txtNombre.Text = aero.Nombre;
                txtDireccion.Text = aero.Direccion;
                txtPartida.Text = aero.ImpuestoPartida.ToString();
                txtLlegada.Text = aero.ImpuestoLlegada.ToString();
                ddlCiudad.SelectedValue = aero.Ciudad.NombreCoudad;

                ActivoBotones(true);

                Session["UnAeropuerto"] = aero;
            }
            else
            {
                ActivoBotones(false);

                lblError.ForeColor = Color.Blue;
                lblError.Text = "No hay Aeropuertos registrados con ese código";

                Session["UnAeropuerto"] = null;
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== AGREGAR AEROPUERTOS ===== //
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string direccion = txtDireccion.Text;

            double impPartida = Convert.ToDouble(txtPartida.Text);
            double impLlegada = Convert.ToDouble(txtLlegada.Text);

            string ciudadNombre = ddlCiudad.SelectedValue;

            string ciudadCodigo = ObtenerCodigoCiudadPorNombre(ciudadNombre);

            if (!string.IsNullOrEmpty(ciudadCodigo))
            {
                Ciudad ciu = LogicaCiudad.Buscar(ciudadCodigo);

                Aeropuerto aeropuerto = new Aeropuerto(codigo, nombre, direccion, ciu, impPartida, impLlegada);

                LogicaAeropuerto.Agregar(aeropuerto);

                lblError.ForeColor = Color.Green;
                lblError.Text = "Alta con éxito";
                CargarDropDownList();
                LimpioFormulario();
            }
            else
            {
                lblError.ForeColor = Color.Red;
                lblError.Text = "No se pudo encontrar el código de la ciudad asociada al aeropuerto.";
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== MODIFICAR AEROPUERTO ===== //
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Aeropuerto aeropuerto = (Aeropuerto)Session["UnAeropuerto"];

            aeropuerto.Nombre = txtNombre.Text.Trim();
            aeropuerto.Direccion = txtDireccion.Text.Trim();

            string ciudadNombre = ddlCiudad.SelectedValue;
            string ciudadCodigo = ObtenerCodigoCiudadPorNombre(ciudadNombre);
            // Buscar ciudad
            Ciudad ciudad = LogicaCiudad.Buscar(ciudadCodigo);
            aeropuerto.Ciudad = ciudad;
            aeropuerto.ImpuestoPartida = Convert.ToDouble(txtPartida.Text.Trim());
            aeropuerto.ImpuestoLlegada = Convert.ToDouble(txtLlegada.Text.Trim());

            LogicaAeropuerto.Modificar(aeropuerto);

            lblError.ForeColor = Color.Green;
            lblError.Text = "Modificación con éxito";

            LimpioFormulario();

        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== ELIMINAR AEROPUERTO ===== //
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            Aeropuerto aeropuerto = (Aeropuerto)Session["UnAeropuerto"];

            LogicaAeropuerto.Eliminar(aeropuerto);

            lblError.ForeColor = Color.Green;
            lblError.Text = "Eliminación exitosa";

            LimpioFormulario();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== BOTÓN LIMPIAR ===== //
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpioFormulario();
    }

    // ===== CARGAR DROPDOWNLIST ===== //
    private void CargarDropDownList()
    {
        try
        {
            List<Ciudad> colCiudades = LogicaCiudad.ListarCiudades();

            ddlCiudad.Items.Clear();

            ddlCiudad.Items.Add(new ListItem("--------------------------------", ""));

            foreach (Ciudad ciudad in colCiudades)
            {
                ddlCiudad.Items.Add(new ListItem(ciudad.NombreCoudad));
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== OBTENER CÓDIGO DE CIUDAD ===== //
    private string ObtenerCodigoCiudadPorNombre(string ciudadNombre)
    {
        List<Ciudad> colCiudades = LogicaCiudad.ListarCiudades();

        foreach (Ciudad ciudad in colCiudades)
        {
            if (ciudad.NombreCoudad == ciudadNombre)
            {
                return ciudad.Codigo;
            }
        }

        return null;
    }
}