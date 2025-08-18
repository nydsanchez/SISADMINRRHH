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
    public partial class departamentos : System.Web.UI.Page
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
                selecionarDepartamentos();
            }
        }

        private void selecionarDepartamentos()
        {
            gvDepartamentos.DataSource = Neg_Catalogos.selecionarDepartamentos();
            gvDepartamentos.DataMember = "departamentos";
            gvDepartamentos.DataBind();
        }

        protected void gvDepartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox departamento = ((TextBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("txtNombre"));
            txtdepartamento.Text = departamento.Text;

            TextBox centroCosto = ((TextBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("txtCentro"));
            txtCentroCosto.Text = centroCosto.Text;
            
            TextBox codigoDepto = ((TextBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("txtCodigo"));
            Session["codigoDepto"] = codigoDepto.Text;

            TextBox MailJefe = ((TextBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("txtMailJefe"));
            txtMailJefeUp.Text = MailJefe.Text;

            TextBox IdJefe = ((TextBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("txtIdJefe"));
            txtIdJefeUp.Text = IdJefe.Text;

            TextBox idpadre = ((TextBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("txtidpadre"));
            txtCodigoPadreUp.Text = idpadre.Text;

            CheckBox chkActivo = ((CheckBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("chkActivo"));
            chkActivoUp.Checked = chkActivo.Checked;
            CheckBox chkDir = ((CheckBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("chkDir"));
            chkCostoDirUp.Checked = chkDir.Checked;
            CheckBox chkOmitirP = ((CheckBox)gvDepartamentos.Rows[gvDepartamentos.SelectedIndex].FindControl("chkOmitirPlanilla"));
            chkOmitirPlUp.Checked = chkOmitirP.Checked;
            btnEditar.Visible = true;
            BtnAgregar.Visible = false;
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                string departamento = txtdepartamento.Text.Trim();
                int centrodecosto = int.Parse(txtCentroCosto.Text.Trim());
                bool costodirecto = chkCostoDirUp.Checked;
                bool activo = chkActivoUp.Checked;
                int omitirpl = Convert.ToInt32(chkOmitirPlUp.Checked);
                int codigopadre = int.Parse(txtCodigoPadreUp.Text.Trim());
                int idjefe = int.Parse(txtIdJefeUp.Text.Trim());
                int idpadre = int.Parse(txtCodigoPadreUp.Text.Trim());
                string jefe = txtMailJefeUp.Text.Trim();

                if (Neg_Catalogos.PlnDepartamentosIns(departamento, centrodecosto,costodirecto,idpadre,idjefe,jefe,omitirpl))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Departamento Agregado Satisfactoriamente";
                    selecionarDepartamentos();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Agregar Departamento";
                    
                }
            }
        }

        private bool validar()
        {
            if (txtdepartamento.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un nombre de departamento";
                return false;
            }

            if (txtIdJefeUp.Text.Trim() == "")
            {
                txtIdJefeUp.Text = "0";
            }

            if (txtCodigoPadreUp.Text.Trim() == "")
            {
                txtCodigoPadreUp.Text = "0";
            }
            
            if (txtCentroCosto.Text.Trim() == "")
            {
                txtCentroCosto.Text = "0";
            }

            return true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                string departamento = txtdepartamento.Text.Trim();
                int centrodecosto = int.Parse(txtCentroCosto.Text.Trim());
                int codigodepto = Convert.ToInt32(Session["codigoDepto"]);
                bool costodirecto = chkCostoDirUp.Checked;
                bool activo = chkActivoUp.Checked;
                int omitirpl = Convert.ToInt32(chkOmitirPlUp.Checked);
                int codigopadre = int.Parse(txtCodigoPadreUp.Text.Trim());
                int idjefe = int.Parse(txtIdJefeUp.Text.Trim());
                int idpadre = int.Parse(txtCodigoPadreUp.Text.Trim());
                string jefe = txtMailJefeUp.Text.Trim();                

                if (Neg_Catalogos.PlnDepartamentosUpd(departamento,centrodecosto,costodirecto,codigodepto,idpadre,idjefe,jefe,omitirpl,activo))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Departamento Editado Satisfactoriamente";
                    selecionarDepartamentos();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Editar Departamento";
                }
            }
        }

        protected void gvDepartamentos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDepartamentos.PageIndex = e.NewPageIndex;
            gvDepartamentos.DataBind();
            selecionarDepartamentos();
        }
    }
}