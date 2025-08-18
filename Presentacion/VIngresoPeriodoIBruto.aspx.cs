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
    public partial class VIngresoPeriodoIBruto : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarIngresos();
            }
          
        }
        private void CargarIngresos()
        {
            this.ddlConceptos.DataSource = Neg_DevYDed.cargarIngresos(0);
            //this.ddlConceptos.DataMember = "tipo";
            this.ddlConceptos.DataValueField = "idDevengado";
            this.ddlConceptos.DataTextField = "devengadoNombre";
            this.ddlConceptos.DataBind();
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            int Periodo = 0;
            Periodo = Convert.ToInt32(txtperiodo.Text.Trim());
          
            DataSet ds = null;

            int concepto = chktodos.Checked ? 0 : Convert.ToInt32(ddlConceptos.SelectedValue.Trim());

            ds = Neg_Informes.CargarIngresoPeriodoIBruto(Periodo,Periodo, concepto);

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IngresoPeriodoIBruto.rdlc");
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("periodo", Periodo.ToString().Trim());           

            this.ReportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
    }
}