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
using System.Globalization;

namespace NominaRRHH
{
    public partial class VDetalleHorasExtrasxFecha : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        //Globales Globales = new Globales();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtFecha.Text = DateTime.Now.ToShortDateString();
                TxtFecha2.Text = DateTime.Now.ToShortDateString();
            }
        }
       
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }

       
        

        private void MostrarReporte(DataTable dtReporte)
        {
            if (ddlAsigPerm.SelectedValue == "1" || ddlAsigPerm.SelectedValue == "3")//por depto y gerencia
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/DetalleHorasExtrasxFecha.rdlc");
            }else
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/DetalleHorasExtrasxFechaEmp.rdlc");
            }      
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtReporte);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                MostrarReporte(Neg_Informes.GenerarDetalleHorasExtrasRpt(ddlAsigPerm.SelectedValue,txtFecha.Text,TxtFecha2.Text));   
                                 
            }
            catch (Exception ex)
            {
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }      
       
    }
}