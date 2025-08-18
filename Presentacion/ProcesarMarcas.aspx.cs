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
using System.Data.OleDb;
using System.IO;

namespace NominaRRHH.Presentacion
{
    public partial class ProcesarMarcas : System.Web.UI.Page
    {
        #region
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        private void ObtenerMarcas()
        {
            Negocios.Neg_Marca Marcas = new Neg_Marca();

            DateTime Fecha = Convert.ToDateTime(txtFechaIni.Text.Trim());
            TxtMsg.Text = Marcas.spObtenerMarcas(Fecha,1);
            TxtMsg.Visible = true;
        }
        private void ObtenerReporte(DateTime Inicio, DateTime Fin)
        {

            DataSet ds = Neg_Informes.spListarMarcas(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaIni.Text));

            if (ds.Tables[0].Rows.Count == 0)
                return;

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Marcas.rdlc");


            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("FechaInicio", txtFechaIni.Text);
            parameters[1] = new ReportParameter("FechaFin", txtFechaIni.Text);

            this.ReportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);

            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            if (txtFechaIni.Text.Trim().Length != 0)
            {
                ObtenerMarcas();
                ObtenerReporte(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaIni.Text));
            }
        }
    }
}