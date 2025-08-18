using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class DeduccionesRecurrentes : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerDeducciones();
            }

        }
        private void obtenerDeducciones()
        {
            this.ddlTipDeduc.DataSource = Neg_DevYDed.cargarDeduccionesRecurrentes();
            this.ddlTipDeduc.DataMember = "deducciones";
            this.ddlTipDeduc.DataValueField = "idDeduccion";
            this.ddlTipDeduc.DataTextField = "deduccionNombre";
            this.ddlTipDeduc.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Trim() != "" && txtTotal.Text.Trim() != "")
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);
                if (Neg_DevYDed.AsignarDeduccionesRecurrentes(Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(ddlTipDeduc.SelectedValue), ChkEspecial.Checked, Convert.ToDecimal(txtTotal.Text.Trim()), chkPorc.Checked, user))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Deduccion Asignada Satisfactoriamente";
                }
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese Todos Los Datos Correspondientes";
            }
        }
    }
}