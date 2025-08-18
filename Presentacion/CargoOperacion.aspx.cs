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
    public partial class CargoOperacion : System.Web.UI.Page
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
                DataTable dt = new Neg_Operacion().PlnOperacionSel();
                ddlOperacion.DataSource = dt;
                ddlOperacion.DataTextField = "descripcion";
                ddlOperacion.DataValueField = "codigo_operacion";
                ddlOperacion.DataBind();

                ddlCargo.DataSource = new Neg_Cargo().PlnCargoSel();
                ddlCargo.DataTextField = "nombre_cargo";
                ddlCargo.DataValueField = "codigo_cargo";
                ddlCargo.DataBind();
            }
        }

        private void SeleccionarCargos()
        {
            if (ddlCargo.Items.Count > 0)
            {
                int x;
                int.TryParse(ddlCargo.SelectedValue,out x);
                gvOperaciones.DataSource = new Neg_Operacion().PlnCargosOperacionSel(x);
                gvOperaciones.DataBind();
            }
        }

        protected void gvOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            Neg_Operacion nope = new Neg_Operacion();
            TextBox operacion = ((TextBox)gvOperaciones.Rows[gvOperaciones.SelectedIndex].FindControl("txtcodigo_operacion"));            

            if (ddlCargo.Items.Count > 0)
            {
                int x;
                int.TryParse(ddlCargo.SelectedValue, out x);
                if (nope.PlnCargosOperacioDel(operacion.Text, x.ToString()))
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
                string codigo_operacion = ddlOperacion.SelectedValue;

                if (new Neg_Operacion().PlnCargosOperacionIns(codigo_operacion,codigo_cargo))
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
                
        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            SeleccionarCargos();
        }
    }
}