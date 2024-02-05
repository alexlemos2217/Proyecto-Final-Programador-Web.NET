using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Persistencia;
using EntidadesCompartidas;
using Logica;
using System.Drawing;

public partial class HistoricoCompras : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                EntidadesCompartidas.Cliente clienteLogueado =  Session["Cliente"] as EntidadesCompartidas.Cliente;

                if (clienteLogueado != null)
                {
                    // Historial de pasajes para el cliente logueado
                    List<Pasaje> historicoPasajes = LogicaPasaje.ListarPasajesUsuario(clienteLogueado.NumPasaporte);

                    Session["ListaPasajes"] = historicoPasajes;

                    if (historicoPasajes.Count > 0)
                    {
                        lblPasaje.Text = "Histórico de pasajes del cliente: " + clienteLogueado.Nombre;
                    }
                    else 
                    {
                        lblPasaje.Text = "El cliente no tiene pasajes comprados";
                    }

                    gvPasajes.DataSource = historicoPasajes;
                    gvPasajes.DataBind();

                    gvPasajes.SelectedIndex = -1;
                }
                else
                {
                    // Si el cliente no está logueado, redirigir al login
                    Response.Redirect("LoginCliente.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            lblPasaje.ForeColor = Color.Red;
            lblPasaje.Text = ex.Message;
        }
    }

    // ===== GRILLA DATOS COMPLETOS ===== //
    protected void ddlOpciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int selectedIndex = gvPasajes.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < gvPasajes.Rows.Count)
            {
                Pasaje unP = ((List<Pasaje>)Session["ListaPasajes"])[selectedIndex];

                if (unP != null)
                {
                    lblCompleto.Text = "Datos completos";

                    gvCompleto.DataSource = new List<Pasaje> { unP };
                    gvCompleto.DataBind();
                }
                else
                {
                    gvCompleto.DataSource = null;
                    gvCompleto.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblCompleto.ForeColor = Color.Red;
            lblCompleto.Text = ex.Message;
        }
    }

    // ===== PAGINADO GRILLA HISTÓRICO ===== //
    protected void gvPasajes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPasajes.PageIndex = e.NewPageIndex;
            gvPasajes.DataSource = (List<Pasaje>)Session["ListaPasajes"];
            gvPasajes.DataBind();

            gvPasajes.SelectedIndex = -1;

            lblCompleto.Text = "";
            gvCompleto.DataSource = null;
            gvCompleto.DataBind();
        }
        catch (Exception ex)
        {
            lblPasaje.ForeColor = Color.Red;
            lblPasaje.Text = ex.Message;
        }
    }
}