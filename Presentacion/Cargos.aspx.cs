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
    public partial class cargos : System.Web.UI.Page
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
                selecionarCargos();
            }
        }

        private void selecionarCargos()
        {
            gvCargos.DataSource = new Neg_Cargo().PlnCargoSel();
            gvCargos.DataMember = "cargos";
            gvCargos.DataBind();
        }

        protected void gvCargos_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox cargo = ((TextBox)gvCargos.Rows[gvCargos.SelectedIndex].FindControl("txtnombre_cargo"));
            txtnombre_cargoup.Text = cargo.Text;

            TextBox codigo_cargo = ((TextBox)gvCargos.Rows[gvCargos.SelectedIndex].FindControl("txtcodigo_cargo"));
            Session["codigo_cargo"] = int.Parse(codigo_cargo.Text.Trim());

            CheckBox chkActivo = ((CheckBox)gvCargos.Rows[gvCargos.SelectedIndex].FindControl("chkActivo"));
            chkActivoUp.Checked = chkActivo.Checked;

            CheckBox chkDir = ((CheckBox)gvCargos.Rows[gvCargos.SelectedIndex].FindControl("chkIndirecto"));
            chkIndirecto.Checked = chkDir.Checked;
            
            btnEditar.Visible = true;
            BtnAgregar.Visible = false;
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                string cargo = txtnombre_cargoup.Text.Trim();
                bool indirecto = chkIndirecto.Checked;

                if (new Neg_Cargo().PlnCargoIns(cargo,indirecto))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Cargo Agregado Satisfactoriamente";
                    selecionarCargos();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Agregar Cargo";
                    
                }
            }
        }

        private bool validar()
        {
            if (txtnombre_cargoup.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un nombre de cargo";
                return false;
            }           

            return true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                string nombre_cargo = txtnombre_cargoup.Text.Trim();
                int codigo_cargo = (int)Session["codigo_cargo"];
                bool activo = chkActivoUp.Checked;
                bool indirecto = chkIndirecto.Checked;

                if (new Neg_Cargo().PlnCargoUpd(codigo_cargo,nombre_cargo,indirecto,activo))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Cargo Editado Satisfactoriamente";
                    selecionarCargos();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Editar Cargo";
                }
            }
        }

        protected void gvCargos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCargos.PageIndex = e.NewPageIndex;
            gvCargos.DataBind();
            selecionarCargos();
        }
    }
}