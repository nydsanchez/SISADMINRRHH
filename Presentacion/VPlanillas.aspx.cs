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


namespace NominaRRHH.Reportes
{
    public partial class VPlanillas : System.Web.UI.Page
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
               // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PreplanillaIngresos.rdlc"); 
            }  
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (txtPeriodo.Text.Trim().Length != 0)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                if (ddlTipo.SelectedValue.Trim() == "1")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PreplanillaIngresos.rdlc");
                }
                if (ddlTipo.SelectedValue.Trim() == "2")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PreplanillaEgresos.rdlc");
                }                
                // DataSet ds = Neg_Informes.CargarPreplanilla(Convert.ToInt32(txtPeriodo.Text.Trim()));
                DataSet ds = Neg_Informes.CargarPreplanillaIng(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlTipo.SelectedValue.Trim()));
                DataSet ds2 = Neg_Informes.CargarPeriodo(Convert.ToInt32(txtPeriodo.Text.Trim()));
                
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("periodo", txtPeriodo.Text.Trim());
                parameters[1] = new ReportParameter("id_Tipo", ddlTipo.SelectedValue.Trim());

                this.ReportViewer1.LocalReport.SetParameters(parameters);
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                ReportDataSource datasource2 = new ReportDataSource("DataSet2", ds2.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasource2);
                this.ReportViewer1.LocalReport.SubreportProcessing +=
                new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            }
            else
            {
                txtPeriodo.Focus();
                return;
            }
        }
    }
}