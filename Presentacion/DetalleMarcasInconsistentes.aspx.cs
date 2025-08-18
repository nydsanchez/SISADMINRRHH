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
    public partial class DetalleMarcasInconsistentes : System.Web.UI.Page
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
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/DetalleMarca.rdlc");
            }
        }      

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            Neg_Marca x = new Neg_Marca();
            try {
                if (validar())
                {
                    DateTime fechaini = Convert.ToDateTime(txtFechaIni.Text);
                    DateTime fechafin = Convert.ToDateTime(txtFechaFin.Text);

                    DataSet ds = x.Inconsistencias(fechaini, fechafin, 3, 0);
                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ReporteMarcasxRangoFecha.rdlc");
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.Refresh();
                }
               
            }
            catch (Exception ex) {

            }
        }
        public bool validar()
        {
            int c = 0;
            if (txtFechaIni.Text.Trim() == "" || txtFechaFin.Text.Trim() == "")
            {
                c = c + 1;
                txtFechaFin.Focus();
                lblAlert.Text = "Favor Ingrese una Fecha de Inicio y una fecha Fin";
            }

            else
            {
                if (Convert.ToDateTime(txtFechaIni.Text.Trim()) > Convert.ToDateTime(txtFechaFin.Text.Trim()))
                {
                    lblAlert.Text = "Rango de Fechas invalidos";
                    txtFechaIni.Focus();
                    c = c + 1;
                }

            }

           

            if (c > 0)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;

                return false;
            }
            else { return true; }

        }
    }
}