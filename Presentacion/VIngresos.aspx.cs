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
    public partial class VIngresos : System.Web.UI.Page
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
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Ingresos.rdlc");
            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            if (txtFechaIni.Text.Trim().Length != 0 && txtFechaFin.Text.Trim().Length!=0)
            {
                DataSet ds=null;
                if (ddlTipo.SelectedValue.Trim() == "1")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Ingresos.rdlc");
                    ds = Neg_Informes.CargarIngresos(Convert.ToDateTime(txtFechaIni.Text.Trim()), Convert.ToDateTime(txtFechaFin.Text.Trim()));
                }
                if (ddlTipo.SelectedValue.Trim() == "2")
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Egresos.rdlc");
                    ds = Neg_Informes.CargarEgresos(Convert.ToDateTime(txtFechaIni.Text.Trim()), Convert.ToDateTime(txtFechaFin.Text.Trim()));
                }        

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
    }
}