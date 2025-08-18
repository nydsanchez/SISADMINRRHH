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
    public partial class VConceptos : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Informes Neg_Informes = new Neg_Informes();
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
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Conceptos.rdlc");
                // DataSet ds = Neg_Informes.CargarPreplanilla(Convert.ToInt32(txtPeriodo.Text.Trim()));
                DataSet ds = Neg_Informes.CargarConceptos(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlTipo.SelectedValue.Trim()));

                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("periodo", txtPeriodo.Text.Trim());
                parameters[1] = new ReportParameter("semana", ddlTipo.SelectedValue.Trim());

                this.ReportViewer1.LocalReport.SetParameters(parameters);
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
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

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}