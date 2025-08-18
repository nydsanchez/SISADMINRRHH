using System;
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

namespace NominaRRHH.Presentacion
{
    public partial class VMasterC : System.Web.UI.Page
    {
        #region REFERENCIAS
        //CREADO POR WENDY MEMBREÑO
        // 11 NOV 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            DataTable ds = null;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();

            ds = Neg_Informes.MasterPComedor();
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/MasterPComedor.rdlc");

            ReportDataSource source = new ReportDataSource("DataSet1", ds);
            ReportViewer1.LocalReport.DataSources.Add(source);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}