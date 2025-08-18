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
    public partial class VConceptosEmp : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            int Periodo = 0, Semana = 0, IdTipo = 0, TipoIngDed = 0;
            Periodo = Convert.ToInt32(txtperiodo.Text.Trim());
            Semana = Convert.ToInt32(ddlSemana.SelectedValue.ToString().Trim());
            IdTipo = Convert.ToInt32(ddlTipo.SelectedValue.ToString().Trim());
            TipoIngDed = Convert.ToInt32(ddlConceptos.SelectedValue.ToString().Trim());
            DataSet ds = null;
            ds=Neg_Informes.CargarTipoConcepto(Periodo,Semana,IdTipo,TipoIngDed);

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ConceptosTipo.rdlc");
            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("periodo", Periodo.ToString().Trim());
            parameters[1] = new ReportParameter("semana",Semana.ToString().Trim());
            parameters[2] = new ReportParameter("Id_Tipo", IdTipo.ToString().Trim());
            parameters[3] = new ReportParameter("TipoIngDed", TipoIngDed.ToString().Trim());

            this.ReportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        private void CargarIngresos()
        {
            this.ddlConceptos.DataSource = Neg_DevYDed.cargarIngresos(1);
            //this.ddlConceptos.DataMember = "tipo";
            this.ddlConceptos.DataValueField = "idDevengado";
            this.ddlConceptos.DataTextField = "devengadoNombre";
            this.ddlConceptos.DataBind();
        }
        private void CargarEgresos()
        {
            this.ddlConceptos.DataSource = Neg_DevYDed.cargarDeducciones();
            //this.ddlConceptos.DataMember = "tipo";
            this.ddlConceptos.DataValueField = "iddeduccion";
            this.ddlConceptos.DataTextField = "deduccionnombre";
            this.ddlConceptos.DataBind();
        }
        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.DataBind();
            if (ddlTipo.SelectedIndex == 1)
            {
                CargarIngresos();
            }
            if (ddlTipo.SelectedIndex == 2)
            {
                CargarEgresos();
            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
    }
}