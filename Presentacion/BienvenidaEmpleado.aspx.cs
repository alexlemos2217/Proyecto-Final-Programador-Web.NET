using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Drawing;
using EntidadesCompartidas;
using Logica;

public partial class BienvenidaEmpleado : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Empleado"] != null)
        {
            EntidadesCompartidas.Empleado empleado = Session["Empleado"] as EntidadesCompartidas.Empleado;

            lblBienvenida.Text = "Bienvenido " + empleado.NombreCompleto;

            lblTitulo.Text = "Datos del empleado:";
            lblDatos.Text = empleado.ToString();
        }
    }
}