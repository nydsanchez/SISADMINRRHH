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


namespace NominaRRHH.VistaReportes
{
    public partial class VHojaInss : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }  
        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            if (ddlTipo.SelectedValue.Trim().ToString() == "0")
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/HojaInss.rdlc");
            }
            if (ddlTipo.SelectedValue.Trim().ToString() == "1")
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/SolicitudEmpleo.rdlc");
            }

            // DataSet ds = Neg_Informes.CargarEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()));
            DataSet ds = Neg_Informes.CargarEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()));
            

            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing +=
            new SubreportProcessingEventHandler(SubreportProcessingEventHandler);

            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
    }
}