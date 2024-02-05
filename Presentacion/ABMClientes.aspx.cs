using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Logica;
using EntidadesCompartidas;
using System.Drawing;

public partial class ABMClientes : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";

        if (!IsPostBack)
        {
            LimpioFormulario();
        }
    }

    // ===== LIMPIO FORMULARIO ===== //
    private void LimpioFormulario()
    {
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;

        txtNombre.Text = "";
        txtNombre.Enabled = false;
        txtPasaporte.Text = "";
        txtPasaporte.Enabled = true;
        txtTarjeta.Text = "";
        txtTarjeta.Enabled = false;
        txtContrasenia.Text = "";
        txtContrasenia.Enabled = false;
    }

    // ===== ACTIVO BOTONES ===== //
    private void ActivoBotones(bool esAlta)
    {
        btnModificar.Enabled = !esAlta;
        btnEliminar.Enabled = !esAlta;
        btnAgregar.Enabled = esAlta;

        txtPasaporte.Enabled = false;
        txtNombre.Enabled = true;
        txtTarjeta.Enabled = true;
        txtContrasenia.Enabled = true;
    }

    // ===== BUSCAR CLIENTE ===== //
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtPasaporte.Text.Trim()))
            {
                throw new Exception("El pasaporte no puede estar vacío");
            }
            if (!txtPasaporte.Text.All(char.IsDigit))
            {
                throw new Exception("Los dígitos deben ser números");
            }
            if ((txtPasaporte.Text.Length > 15))
            {
                throw new Exception("El número de pasaporte debe contener hasta 15 números");
            }

            string pasaporte = txtPasaporte.Text.Trim();

            EntidadesCompartidas.Cliente clie = LogicaCliente.Buscar(pasaporte);

            if (clie != null)
            {
                txtNombre.Text = clie.Nombre;
                txtTarjeta.Text = clie.NumTarCre.ToString();
                txtContrasenia.Text = clie.Contrasenia;

                ActivoBotones(false);

                Session["Cliente"] = clie;
            }
            else
            {
                ActivoBotones(true);

                lblError.ForeColor = Color.Blue;
                lblError.Text = "No hay clientes con ese número de pasaporte";

                Session["Cliente"] = null;
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== AGREGAR CLIENTE ===== //
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            string pasaporte = txtPasaporte.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string tarjeta = txtTarjeta.Text.Trim();
            string contrasenia = txtContrasenia.Text.Trim();

            EntidadesCompartidas.Cliente clie = new EntidadesCompartidas.Cliente(pasaporte, nombre, tarjeta, contrasenia);

            LogicaCliente.Agregar(clie);

            lblError.ForeColor = Color.Green;
            lblError.Text = "Alta con éxito";

            LimpioFormulario();
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== MODIFICAR CLIENTE ===== //
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Cliente cliente = Session["Cliente"] as EntidadesCompartidas.Cliente;

            cliente.Nombre = txtNombre.Text.Trim();
            cliente.NumTarCre = txtTarjeta.Text.Trim();
            cliente.Contrasenia = txtContrasenia.Text.Trim();

            LogicaCliente.Modificar(cliente);

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

    // ===== ELIMINAR CLIENTE ===== //
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Cliente cliente = Session["Cliente"] as EntidadesCompartidas.Cliente;

            LogicaCliente.Eliminar(cliente);

            lblError.ForeColor = Color.Green;
            lblError.Text = "Eliminación con éxito";

            LimpioFormulario();
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
        LimpioFormulario();
    }
}