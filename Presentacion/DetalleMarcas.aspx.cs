using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class DetalleMarcas : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                obtenerTiposPlanilla();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                GVDetNomEmpl.DataSource = Neg_Planilla.obtenerDetalleMarcas(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodEmp.Text.Trim()));
                GVDetNomEmpl.DataMember = "detalle";
                GVDetNomEmpl.DataBind();
                this.editMarcas.Visible = true;
                btnCrear.Visible = true;
            }
        }
        private void obtenerTiposPlanilla()
        {
            this.ddlTiposPlanilla.DataSource = Neg_Planilla.cargarTiposPlanilla();
            this.ddlTiposPlanilla.DataMember = "planillas";
            this.ddlTiposPlanilla.DataValueField = "idNomina";
            this.ddlTiposPlanilla.DataTextField = "Descripcion";
            this.ddlTiposPlanilla.DataBind();
        }
        public bool validar()
        {
            if (txtPeriodo.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtPeriodo.Focus();
                return false;
            }

            if (txtCodEmp.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtCodEmp.Focus();
                return false;
            }

            return true;
        }

        protected void GVDetNomEmpl_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.calendarFin.Visible = false;
            //this.txtNombre.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[1].Text.Trim();
            this.txtCodigo.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[2].Text.Trim();
            this.txtFecha.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[3].Text.Trim();
            this.txtHoraE.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[4].Text.Trim();
            this.txtHoraS.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[5].Text.Trim();
            btnEditar.Visible = true;
            btnCrear.Visible = false;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

            if(validarEdicion())
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (Neg_DevYDed.EditarMarcasxEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToDateTime(txtFecha.Text.Trim()), txtHoraE.Text.Trim(), txtHoraS.Text.Trim(), userDetail.getUser()))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Edicion De Marca Satisfactoria";
                    GVDetNomEmpl.DataSource = Neg_Planilla.obtenerDetalleMarcas(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodEmp.Text.Trim()));
                    GVDetNomEmpl.DataMember = "detalle";
                    GVDetNomEmpl.DataBind();
                    limpiar();
                    
                    TxtFecha2.Text = "";
                }
                else
                {
                    alertSucces.Visible = false;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Editar la Marca";
                }
            }
        }

        private void limpiar()
        {
            //this.txtNombre.Text = "";
            this.txtCodigo.Text = "";
            this.txtFecha.Text = "";
            this.txtHoraE.Text = "";
            this.txtHoraS.Text = "";
        }

        public bool validarEdicion()
        {
            //if (txtNombre.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtNombre.Focus();
            //    return false;
            //}

            if (txtCodigo.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtCodigo.Focus();
                return false;
            }
            if (txtFecha.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtFecha.Focus();
                return false;
            }
            if (txtHoraE.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHoraE.Focus();
                return false;
            }
            if (txtHoraS.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHoraS.Focus();
                return false;
            }

            return true;
        }
      
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            TimeSpan intervalo = Convert.ToDateTime(TxtFecha2.Text.Trim()) - Convert.ToDateTime(txtFecha.Text.Trim());
            DateTime Fecha = Convert.ToDateTime(txtFecha.Text.Trim());
          
            for (int i = 0; i < intervalo.Days+1; i++)
            {
                if (validarEdicion())
                {
                    IUserDetail userDetail = UserDetailResolver.getUserDetail();

                    

                    if (Neg_Planilla.InsertarMarcaPorEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()), Fecha, txtHoraE.Text.Trim(), txtHoraS.Text.Trim(), userDetail.getUser()))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Marca creada Satisfactoriamente";
                        Fecha= Fecha.AddDays(1);
                        
                        //string resultado = Fecha.ToString("ddMMyyyy");
                    }
                    else
                    {
                        alertSucces.Visible = false;
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Error al crear Marca";
                    }
                }
                GVDetNomEmpl.DataSource = Neg_Planilla.obtenerDetalleMarcas(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodEmp.Text.Trim()));
                GVDetNomEmpl.DataMember = "detalle";
                GVDetNomEmpl.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            TxtFecha2.Text = "";
        }

        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTiposPlanilla.SelectedValue == "1")
                this.divSemana.Visible = true;
            else
                this.divSemana.Visible = false;
        }
    }
}