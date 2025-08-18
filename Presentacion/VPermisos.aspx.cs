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

    public partial class VPermisos : System.Web.UI.Page
    {
         #region Referencias
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTipoPermisos();
            }
        }
        private void CargarTipoPermisos()
        {
            this.ddltipo.DataSource = Neg_Informes.PermisosSel();
            //this.ddlConceptos.DataMember = "tipo";
            this.ddltipo.DataValueField = "id_Descripcion";
            this.ddltipo.DataTextField = "Descripcion";
            this.ddltipo.DataBind();
        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            if (txtFechaIni.Text.Trim().Length != 0 && txtFechaFin.Text.Trim().Length != 0)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Permisos.rdlc");
                DataSet ds = Neg_Informes.CargarPermisos(Convert.ToDateTime(txtFechaIni.Text.Trim()), Convert.ToDateTime(txtFechaFin.Text.Trim()),Convert.ToInt32(ddltipo.SelectedValue.Trim()));

                ReportParameter[] parameters = new ReportParameter[3];
                parameters[0] = new ReportParameter("fecha1", txtFechaIni.Text.Trim());
                parameters[1] = new ReportParameter("fecha2", txtFechaFin.Text.Trim());
                parameters[2] = new ReportParameter("tipo", ddltipo.SelectedValue.Trim());

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