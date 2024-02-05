using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Logica;
using System.Drawing;
using EntidadesCompartidas;

public partial class ABMCiudades : System.Web.UI.Page
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

    // ===== LIMPIAR ===== //
    private void LimpioFormulario()
    {
        btnModificar.Enabled = false;
        btnEliminar.Enabled = false;
        btnAgregar.Enabled = false;
        btnBuscar.Enabled = true;

        txtCodigo.Text = "";
        txtCodigo.Enabled = true;
        txtNombre.Text = "";
        txtNombre.Enabled = false;
        txtPais.Text = "";
        txtPais.Enabled = false;
    }

    // ===== ACTIVAR BOTONES ===== //
    private void ActivoBotones(bool esAlta)
    {
        btnModificar.Enabled = esAlta;
        btnEliminar.Enabled = esAlta;
        btnAgregar.Enabled = !esAlta;

        txtCodigo.Enabled = false;
        txtNombre.Enabled = true;
        txtPais.Enabled = true;
    }

    // ===== BUSCAR CIUDAD ===== //
    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text.Trim()))
            {
                throw new Exception("El código no puede estar vacío");
            }
            if (txtCodigo.Text.Trim().Length != 6)
            {
                throw new Exception("El código debe tener exactamente 6 letras");
            }
            if (txtCodigo.Text.Length > 30)
            {
                throw new Exception("El nombre de la ciudad no puede superar los 30 caracteres");
            }

            string ciudad = txtCodigo.Text.Trim();

            Ciudad ciu = LogicaCiudad.Buscar(ciudad);

            if (ciu != null)
            {
                txtCodigo.Text = ciu.Codigo;
                txtNombre.Text = ciu.NombreCoudad;
                txtPais.Text = ciu.Pais;

                ActivoBotones(true);

                Session["UnaCiudad"] = ciu;
            }
            else
            {
                ActivoBotones(false);

                lblError.ForeColor = Color.Blue;
                lblError.Text = "No existe una ciudad registrada con el código ingresado";

                Session["UnaCiudad"] = null;
            }
        }
        catch (Exception ex)
        {
            lblError.ForeColor = Color.Red;
            lblError.Text = ex.Message;
        }
    }

    // ===== AGREGAR CIUDAD ===== //
    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        try
        {
            string codigo = txtCodigo.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string pais = txtPais.Text.Trim();

            Ciudad ciu = new Ciudad(codigo, nombre, pais);

            LogicaCiudad.Agregar(ciu);

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

    // ===== MODIFICAR CIUDAD ===== //
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        try
        {
            Ciudad ciudad = (Ciudad)Session["UnaCiudad"];

            ciudad.NombreCoudad = txtNombre.Text.Trim();
            ciudad.Pais = txtPais.Text.Trim();

            LogicaCiudad.Modificar(ciudad);

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

    // ===== ELIMINAR CIUDAD ===== //
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            Ciudad ciudad = (Ciudad)Session["UnaCiudad"];

            LogicaCiudad.Eliminar(ciudad);

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
    
    // ===== BOTÓN LIMPIAR ===== //
    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpioFormulario();
    }
}