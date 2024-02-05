using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesCompartidas;
using Logica;

public partial class LoginCliente : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Cliente"] = null;
    }

    // ===== LOGUEO ===== //
    protected void BtnLogueo_Click(object sender, EventArgs e)
    {
        try
        {
            EntidadesCompartidas.Cliente unClie = LogicaCliente.Logueo(TxtPasaporte.Text.Trim(), TxtPassUsu.Text.Trim());

            if (unClie != null)
            {
                Session["Cliente"] = unClie;
                Response.Redirect("HistoricoCompras.aspx");
            }
            else
            {
                LblError.Text = "Datos incorrectos";
            }
        }
        catch(Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}