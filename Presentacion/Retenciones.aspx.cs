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
    public partial class Retenciones : System.Web.UI.Page
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Retenciones Neg_Retenciones = new Neg_Retenciones();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerRetenciones();
            }
        }

        private void obtenerRetenciones()
        {
            GVRetenciones.DataSource = Neg_Retenciones.retenciones();
            GVRetenciones.DataMember = "retenciones";
            GVRetenciones.DataBind();
        }

        protected void GVRetenciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtDesde.Text = GVRetenciones.Rows[GVRetenciones.SelectedIndex].Cells[2].Text.Trim();
            this.txtHasta.Text = GVRetenciones.Rows[GVRetenciones.SelectedIndex].Cells[3].Text.Trim();
            this.txtImpBas.Text = GVRetenciones.Rows[GVRetenciones.SelectedIndex].Cells[4].Text.Trim();
            this.txtPorcentaje.Text = GVRetenciones.Rows[GVRetenciones.SelectedIndex].Cells[5].Text.Trim();
            this.txtSobExc.Text = GVRetenciones.Rows[GVRetenciones.SelectedIndex].Cells[6].Text.Trim();
            Session["IdRenta"] = GVRetenciones.Rows[GVRetenciones.SelectedIndex].Cells[1].Text.Trim();

            btnAgregar.Visible = false;
            btnEditar.Visible = true;
            btnEliminar.Visible = true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if(validar())
            { 
                string user = Convert.ToString(this.Page.Session["usuario"]);
                int idRenta = Convert.ToInt32(Session["IdRenta"].ToString());

                if(Neg_Retenciones.EditarRetenciones(idRenta, Convert.ToDecimal(txtDesde.Text.Trim()), Convert.ToDecimal(txtHasta.Text.Trim()), Convert.ToDecimal(txtImpBas.Text.Trim()), Convert.ToDecimal(txtPorcentaje.Text.Trim()), Convert.ToDecimal(txtSobExc.Text.Trim())))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Actualizacion Satisfactoria";
                    limpiar();
                    obtenerRetenciones();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Actualizar Datos";
                }
            }
        }

        private void limpiar()
        {
            this.txtDesde.Text = "";
            this.txtHasta.Text = "";
            this.txtImpBas.Text = "";
            this.txtPorcentaje.Text = "";
            this.txtSobExc.Text = "";
        }

        public bool validar()
        {
            if (txtDesde.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtDesde.Focus();
                return false;
            }
            if (txtHasta.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHasta.Focus();
                return false;
            }
            if (txtImpBas.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtImpBas.Focus();
                return false;
            }
            if (txtPorcentaje.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtPorcentaje.Focus();
                return false;
            }
            if (txtSobExc.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtSobExc.Focus();
                return false;
            }

            return true;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                if (Neg_Retenciones.AgregarRetenciones(Convert.ToDecimal(txtDesde.Text.Trim()), Convert.ToDecimal(txtHasta.Text.Trim()), Convert.ToDecimal(txtImpBas.Text.Trim()), Convert.ToDecimal(txtPorcentaje.Text.Trim()), Convert.ToDecimal(txtSobExc.Text.Trim())))
                {
                    limpiar();
                    obtenerRetenciones();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Satisfactorio";
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Ingresar Datos";
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);
                int idRenta = Convert.ToInt32(Session["IdRenta"].ToString());

                if (Neg_Retenciones.EliminarRetenciones(idRenta))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Datos Eliminados Satisfactoriamente";
                    limpiar();
                    obtenerRetenciones();
                }

                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Eliminar Datos";
                }
            }
        }
    }
}