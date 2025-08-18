using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Negocios;
using Datos;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Reporting.WebForms;

namespace NominaRRHH.Presentacion
{
    public partial class DepartamentoCargo : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                DataTable dt = Neg_Catalogos.selecionarDepartamentos();
                ddlDepartamento.DataSource = dt;
                ddlDepartamento.DataTextField = "nombre_depto";
                ddlDepartamento.DataValueField = "codigo_depto";
                ddlDepartamento.DataBind();

                ddlCargo.DataSource = new Neg_Cargo().PlnCargoSel();
                ddlCargo.DataTextField = "nombre_cargo";
                ddlCargo.DataValueField = "codigo_cargo";
                ddlCargo.DataBind();
            }
        }

        private void SeleccionarCargos()
        {
            if (ddlDepartamento.Items.Count > 0)
            {
                int x;
                int.TryParse(ddlDepartamento.SelectedValue,out x);
                gvOperaciones.DataSource = new Neg_Cargo().PlnCargoSel(x);
                gvOperaciones.DataBind();
            }
        }

        protected void gvOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            Neg_Cargo nc = new Neg_Cargo();
            TextBox cargo = ((TextBox)gvOperaciones.Rows[gvOperaciones.SelectedIndex].FindControl("codigo_cargo"));            

            if (ddlDepartamento.Items.Count > 0)
            {
                int x;
                int.TryParse(ddlDepartamento.SelectedValue, out x);
                if (nc.PlnDepartamentoCargoDel(cargo.Text, x))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Vinculacion eliminada Satisfactoriamente";
                    SeleccionarCargos();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al eliminar vinculacion";
                }                
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
                string codigo_cargo = ddlCargo.SelectedValue;
                int codigo_depto = int.Parse(ddlDepartamento.SelectedValue);

                if (new Neg_Cargo().PlnDepartamentoCargoIns(codigo_cargo,codigo_depto))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Vinculacion agregada Satisfactoriamente";
                    SeleccionarCargos();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Agregar vinculacion";                    
                }
        }

        protected void gvOperaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOperaciones.PageIndex = e.NewPageIndex;
            gvOperaciones.DataBind();
            SeleccionarCargos();
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDepartamento.Items.Count > 0)
            {
                int x;
                int.TryParse(ddlDepartamento.SelectedValue, out x);
                gvOperaciones.DataSource = new Neg_Cargo().PlnCargoSel(x);
                gvOperaciones.DataBind();
            }
        }
    }
}