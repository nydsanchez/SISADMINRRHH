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
    public partial class configuracionFactor : System.Web.UI.Page
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
                obtenerFactor();
            }
        }

        private void obtenerFactor()
        {
            GVfactorHora.DataSource = Neg_Catalogos.obtenerFactor();
            GVfactorHora.DataMember = "factor";
            GVfactorHora.DataBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if(txtFactor.Text.Trim() =="")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese el factor";
            }
            else
            {
                if (Neg_Catalogos.EditarFactor(Convert.ToDecimal(txtFactor.Text.Trim()), ChkActivo.Checked, Convert.ToInt32(Session["Id"].ToString())))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Edicion Satisfactoria";
                    this.txtFactor.Text = "";
                    obtenerFactor();
                }
            }
        }

        protected void GVfactorHora_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtFactor.Text = GVfactorHora.Rows[GVfactorHora.SelectedIndex].Cells[3].Text.Trim();
            //bool a = Convert.ToBoolean((CheckBox["GVfactorHora"].Controls[3]);
            GridViewRow row = GVfactorHora.SelectedRow;
            CheckBox chk = ((CheckBox)GVfactorHora.Rows[GVfactorHora.SelectedIndex].FindControl("chkAct"));
            ChkActivo.Checked = chk.Checked;
            Session["Id"] = GVfactorHora.Rows[GVfactorHora.SelectedIndex].Cells[1].Text.Trim();
        }
    }
}