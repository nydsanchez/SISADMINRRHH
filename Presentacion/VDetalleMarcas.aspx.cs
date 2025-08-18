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
    public partial class VDetalleMarcas : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDptos();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/DetalleMarca.rdlc");
            }
        }

        private void CargarDptos()
        {
            ddldepto1.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddldepto1.DataMember = "procesos";
            this.ddldepto1.DataValueField = "codigo_depto";
            ddldepto1.DataTextField = "nombre_depto";
            this.ddldepto1.DataBind();

            ddldepto2.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddldepto2.DataMember = "procesos";
            this.ddldepto2.DataValueField = "codigo_depto";
            ddldepto2.DataTextField = "nombre_depto";
            this.ddldepto2.DataBind();
        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            if (txtperiodo.Text.Trim().Length!=0)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/DetalleMarca.rdlc");
                DataSet ds = Neg_Informes.CargarMarcasPlanilla(Convert.ToInt32(txtperiodo.Text),Convert.ToInt32(ddlsemana.SelectedItem.Text),Convert.ToInt32(ddldepto1.SelectedValue.Trim()),Convert.ToInt32(ddldepto2.SelectedValue.Trim()));

                ReportParameter[] parameters = new ReportParameter[4];
                parameters[0] = new ReportParameter("periodo", txtperiodo.Text.Trim());
                parameters[1] = new ReportParameter("semana", ddlsemana.SelectedItem.Text);
                parameters[2] = new ReportParameter("depto", ddldepto1.SelectedItem.Text);
                parameters[3] = new ReportParameter("depto2", ddldepto2.SelectedItem.Text);

                this.ReportViewer1.LocalReport.SetParameters(parameters);
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                this.ReportViewer1.LocalReport.SubreportProcessing +=new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            else
            {
                txtperiodo.Focus();
                return;
            }
        }
    }
}