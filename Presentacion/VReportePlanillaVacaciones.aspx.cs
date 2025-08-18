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
    public partial class VReportePlanillaVacaciones : System.Web.UI.Page
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
                divcodigo.Visible = false;
                divrango.Visible = false;
            }  
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            try {
                int periodo = 0,codigo=0;

                string fecini = "", fecfin = "";

                if (rbl.SelectedValue.ToString().Trim() == "3")//por empleado
                {
                    if (txtcodigo.Text.Trim().Length == 0)
                        return;

                    codigo = Convert.ToInt32(txtcodigo.Text);
                }
                else if (rbl.SelectedValue.ToString().Trim() == "1")
                {//por periodo
                    if (txtPeriodo.Text.Trim().Length == 0)
                        return;

                    periodo = Convert.ToInt32(txtPeriodo.Text);
                }
                else
                {
                    if (txtFechaIni2.Text.Trim().Length == 0 || txtFechaFin2.Text.Trim().Length == 0)
                        return;

                    fecini = txtFechaIni2.Text.Trim();
                    fecfin = txtFechaFin2.Text.Trim();
                }
                //if (txtPeriodo.Text.Trim().Length != 0)
                //{
                //    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/planillaVacaciones.rdlc");
                //    DataSet ds = Neg_Informes.obtenerPlanillaVacaciones(Convert.ToInt32(txtPeriodo.Text.Trim()));
                //    ReportParameter[] parameters = new ReportParameter[1];
                //    parameters[0] = new ReportParameter("periodo", txtPeriodo.Text.Trim());

                //    this.ReportViewer1.LocalReport.SetParameters(parameters);
                //    ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                //    ReportViewer1.LocalReport.DataSources.Clear();
                //    ReportViewer1.LocalReport.DataSources.Add(datasource);
                //    this.ReportViewer1.LocalReport.SubreportProcessing +=
                //    new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
                //    ReportViewer1.LocalReport.DataSources.Add(datasource);

                //}
                //else
                //{
                //    txtPeriodo.Focus();
                //    return;
                //}

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/planillaVacaciones.rdlc");
                DataSet ds = Neg_Informes.obtenerPlanillaVacaciones(periodo,codigo, fecini, fecfin);
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.Refresh();
            }
            catch (Exception ex) {
                alertSucces.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl.SelectedValue.ToString() == "3")//codiggo
            {
                divcodigo.Visible = true;
                divperiodo.Visible = false;
                divrango.Visible = false;
            }
            else if (rbl.SelectedValue.ToString() == "1")//periodo
            {
                divperiodo.Visible = true;
                divcodigo.Visible = false;
                divrango.Visible = false;
            }
            else
            {
                divperiodo.Visible = false;
                divcodigo.Visible = false;
                divrango.Visible = true;
            }
        }
    }
}