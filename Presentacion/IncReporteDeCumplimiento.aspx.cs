using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Datos;
using System.Data;

namespace NominaRRHH.Presentacion
{
    public partial class IncReporteDeCumplimiento : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Marca Neg_Marca = new Neg_Marca();
        #endregion
        #region VARIABLES GLOBALES
        int horaL, minutoL, segundoL, horaP, minutoP, segundoP;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                System.Data.DataTable dt = new Dato_PlnEficienciaModulo().Sel(0);

                if (dt.Rows.Count > 0)
                {
                    gvRpt.DataSource = dt;
                    txtPeriodo.Text = dt.Rows[0]["Periodo"].ToString();
                    gvRpt.DataBind();
                }
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            int periodo = 0;
            if(int.TryParse(txtPeriodo.Text,out periodo)==false)
            {
                lblAlert.Text = "Periodo invalido";
                lblAlert.Visible = true;
                return;
            }

            System.Data.DataTable dt = new Dato_PlnEficienciaModulo().Sel(periodo);

            if (dt.Rows.Count > 0)
            {
                gvRpt.DataSource = dt;
                gvRpt.DataBind();
                txtPeriodo.Text = dt.Rows[0]["Periodo"].ToString();
                lblAlert.Text = "";
                lblAlert.Visible = false;

            }
            else
            {
                lblAlert.Text = "No se han encontrado registros. La planilla de incentivos ya se genero?";
                lblAlert.Visible = true;
            }

        }        
       
    }
}