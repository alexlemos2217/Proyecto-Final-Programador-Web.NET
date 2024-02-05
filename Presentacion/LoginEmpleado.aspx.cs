using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;

public partial class LoginEmpleado : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Empleado"] = null;
    }

    // ===== LOGUEO ===== //
    protected void BtnLogueo_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Empleado unEmp = LogicaEmpleado.Logueo(TxtNomUsu.Text.Trim(), TxtPassUsu.Text.Trim());

            if (unEmp != null)
            {
                Session["Empleado"] = unEmp;
                Response.Redirect("BienvenidaEmpleado.aspx");

            }
            else
            {
                LblError.Text = "Datos incorrectos";
            }
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}