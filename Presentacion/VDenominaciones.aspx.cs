using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Negocios;


namespace NominaRRHH.Presentacion
{
    public partial class VDenominaciones : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (txtPeriodo.Text.Trim().Length != 0)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Denominaciones.rdlc");
                DataSet ds = Neg_Informes.CargarDenominaciones(Convert.ToInt32(txtPeriodo.Text.Trim()));
                DataTable dt = Neg_Planilla.desgloceMoneda(ds.Tables[0]);
                ReportParameter[] parameters = new ReportParameter[1];
                parameters[0] = new ReportParameter("periodo", txtPeriodo.Text.Trim());

                this.ReportViewer1.LocalReport.SetParameters(parameters);
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                ReportDataSource datasource2 = new ReportDataSource("DataSet2", dt);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasource2);
                this.ReportViewer1.LocalReport.SubreportProcessing +=
                new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                txtPeriodo.Focus();
                return;
            }
        }
    }
}