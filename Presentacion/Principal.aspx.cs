using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI.WebControls;
using Logica;
using EntidadesCompartidas;

public partial class Principal : System.Web.UI.Page
{
    // ===== PAGE LOAD ===== //
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                CargarDropDownList();
                CargarGrillaPartidas();
                CargarGrillaArribos();
            }
            else
            {
                gvPartidas.DataSource = null;
                gvPartidas.DataBind();
                gvArribos.DataSource = null;
                gvArribos.DataBind();

                lblArribos.Text = "";
                lblPartidas.Text = "";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error en la carga de la página: " + ex.Message;
            lblError.ForeColor = Color.Red;
        }
    }

    // ===== GRILLAS ===== //
    protected void ddlOpciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarGrillaPartidas();
        CargarGrillaArribos();
    }

    // ===== CARGAR DROPDOWNLIST ===== //
    private void CargarDropDownList()
    {
        try
        {
            List<Aeropuerto> colAeropuertos = LogicaAeropuerto.ListarAeropuertos();

            ddlOpciones.Items.Clear();

            ddlOpciones.Items.Add(new ListItem("Seleccione un aeropuerto", ""));

            foreach (Aeropuerto aeropuerto in colAeropuertos)
            {
                ddlOpciones.Items.Add(new ListItem(aeropuerto.Nombre, aeropuerto.Codigo));
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error al cargar los aeropuertos: " + ex.Message;
            lblError.ForeColor = Color.Red;
        }
    }

    // ===== CARGAR GRILLA DE PARTIDAS ===== //
    private void CargarGrillaPartidas()
    {
        try
        {
            string codigoAeropuerto = ddlOpciones.SelectedValue;

            List<Vuelo> partidas = LogicaVuelo.ListarPartidas(codigoAeropuerto);
            Session["Partidas"] = partidas;

            if (partidas.Count > 0)
            {
                lblPartidas.ForeColor = Color.Black;
                lblPartidas.Text = "PARTIDAS";
                
                gvPartidas.DataSource = partidas;
                gvPartidas.DataBind();
            }
            else if (string.IsNullOrEmpty(codigoAeropuerto))
            {
                lblPartidas.Text = "";
            }
            else
            {
                lblPartidas.ForeColor = Color.Red;
                lblPartidas.Text = "El aeropuerto seleccionado no tiene partidas";
            }
            
        }
        catch (Exception ex)
        {
            lblError.Text = "Error al cargar las partidas: " + ex.Message;
            lblError.ForeColor = Color.Red;
        }
    }

    // ===== CARGAR GRILLA DE ARRIBOS ===== //
    private void CargarGrillaArribos()
    {
        try
        {
            string codigoAeropuerto = ddlOpciones.SelectedValue;


            List<Vuelo> arribos = LogicaVuelo.ListarArribos(codigoAeropuerto);
            Session["Arribos"] = arribos;

            if (arribos.Count > 0)
            {
                lblArribos.ForeColor = Color.Black;
                lblArribos.Text = "ARRIBOS";
               
                gvArribos.DataSource = arribos;
                gvArribos.DataBind();
            }
            else if (string.IsNullOrEmpty(codigoAeropuerto))
            {
                lblArribos.Text = "";
            }
            else
            {
                lblArribos.ForeColor = Color.Red;
                lblArribos.Text = "El aeropuerto seleccionado no tiene arribos";
            }
        }
        catch (Exception ex)
        {
            lblError.Text = "Error al cargar los arribos: " + ex.Message;
            lblError.ForeColor = Color.Red;
        }
    }

    // ===== PAGINADO GRILLA DE PARTIDAS ===== //
    protected void gvPartidas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvPartidas.PageIndex = e.NewPageIndex;
            gvPartidas.DataSource = (List<Vuelo>)Session["Partidas"];
            gvPartidas.DataBind();

            gvPartidas.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            lblPartidas.ForeColor = Color.Red;
            lblPartidas.Text = ex.Message;
        }
    }

    // ===== PAGINADO GRILLA DE ARRIBOS ===== //
    protected void gvArribos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvArribos.PageIndex = e.NewPageIndex;
            gvArribos.DataSource = (List<Vuelo>)Session["Arribos"];
            gvArribos.DataBind();

            gvArribos.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            lblPartidas.ForeColor = Color.Red;
            lblPartidas.Text = ex.Message;
        }
    }
}
