using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Negocios;
using System;

namespace NominaRRHH
{
    public partial class VLiquidaciones : System.Web.UI.Page
    {
        #region Referencias
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                txtFechaIni.Text = DateTime.Now.ToShortDateString();
                txtFechaFin.Text = DateTime.Now.ToShortDateString();
            }
            
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                generarReporte();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        
          
        }
       
        void generarReporte()
        {
            if (txtFechaIni.Text.Trim().Length != 0 && txtFechaFin.Text.Trim().Length != 0)
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Liquidaciones.rdlc");
                DataSet ds = null;
                ds = Neg_Informes.spLiquidacionDetalladoSel(Convert.ToDateTime(txtFechaIni.Text.Trim()), Convert.ToDateTime(txtFechaFin.Text.Trim()));
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("inicio", txtFechaIni.Text.Trim());
                parameters[1] = new ReportParameter("fin", txtFechaFin.Text.Trim());

                this.ReportViewer1.LocalReport.SetParameters(parameters);
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                txtFechaIni.Focus();
                return;
            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
       
    }
}