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
    public partial class Operaciones : System.Web.UI.Page
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
                selecionarOperaciones();
            }
        }

        private void selecionarOperaciones()
        {
            gvOperaciones.DataSource = new Neg_Operacion().PlnOperacionSel();
            gvOperaciones.DataMember = "Operaciones";
            gvOperaciones.DataBind();
        }

        protected void gvOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox operacion = ((TextBox)gvOperaciones.Rows[gvOperaciones.SelectedIndex].FindControl("txtdescripcion"));
            Txtdescripcion.Text = operacion.Text;

            TextBox codigooperacion = ((TextBox)gvOperaciones.Rows[gvOperaciones.SelectedIndex].FindControl("txtcodigo_operacion"));
            txtcodigo_operacion.Text = codigooperacion.Text;
            txtcodigo_operacion.ReadOnly = true;

            CheckBox chkActivo = ((CheckBox)gvOperaciones.Rows[gvOperaciones.SelectedIndex].FindControl("chkActivo"));
            chkActivoUp.Checked = chkActivo.Checked;
            
            btnEditar.Visible = true;
            BtnAgregar.Visible = false;
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                string operacion = Txtdescripcion.Text.Trim();
                bool activo = chkActivoUp.Checked;
                string codigo_operacion = txtcodigo_operacion.Text.Trim();

                if (new Neg_Operacion().PlnOperacionIns(operacion,false,operacion))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Operacion Agregada Satisfactoriamente";
                    selecionarOperaciones();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Agregar Operacion";                    
                }
            }
        }

        private bool validar()
        {
            if (Txtdescripcion.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un nombre de operacion";
                return false;
            }

            if (txtcodigo_operacion.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un codigo de operacion";
                return false;
            }

            return true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                string descripcion = Txtdescripcion.Text.Trim();
                bool activo = chkActivoUp.Checked;
                string codigo_operacion = txtcodigo_operacion.Text.Trim();                

                if (new Neg_Operacion().PlnOperacionUpd(codigo_operacion,false,descripcion,activo))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Operacion Editada Satisfactoriamente";
                    selecionarOperaciones();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Editar Operacion";
                }
            }
        }

        protected void gvOperaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOperaciones.PageIndex = e.NewPageIndex;
            gvOperaciones.DataBind();
            selecionarOperaciones();
        }
    }
}