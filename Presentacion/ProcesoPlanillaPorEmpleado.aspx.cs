using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class ProcesoPlanillaPorEmpleado : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerPeriodo();
               
            }

        }
        private void obtenerPeriodo()
        {
            DataTable dt;
            dt = Neg_Planilla.cargarUltPeriodoAbieCat(1);

            if (dt.Rows.Count > 0)
            {
                txtPeriodo.Text = dt.Rows[0]["nperiodo"].ToString();
            }
            else
            {
                txtPeriodo.Text = "0";
            }
        }

        protected void btnRecalc_Click(object sender, EventArgs e)
        {
            if(txtNombEmpl.Text.Trim() != "")
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);
                //string p = Neg_Planilla.ProcesarPlanillaPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), txtNombEmpl.Text.Trim(), user);
                //if (p == "T")
                //{
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Nomina Procesada satisfactoriamente";
               // }
            }
        }
    }
}