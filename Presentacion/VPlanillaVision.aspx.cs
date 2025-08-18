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
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class VPlanillaVision : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                obtenerUbicaciones();
                obtenerTiposPlanilla();
            }
        }
        private void obtenerUbicaciones()
        {
            this.ddlUbicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacion.DataMember = "ubicaciones";
            this.ddlUbicacion.DataValueField = "codigo_ubicacion";
            this.ddlUbicacion.DataTextField = "nombre_ubicacion";
            this.ddlUbicacion.DataBind();
           
        }
        private void obtenerTiposPlanilla()
        {
            this.ddlTiposPlanilla.DataSource = Neg_Planilla.cargarTiposPlanilla();
            this.ddlTiposPlanilla.DataMember = "planillas";
            this.ddlTiposPlanilla.DataValueField = "idNomina";
            this.ddlTiposPlanilla.DataTextField = "Descripcion";
            this.ddlTiposPlanilla.DataBind();
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            DataSet ds=null;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
          
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaVision.rdlc");
                ds = Neg_Informes.PlanillaVisionSel(Convert.ToInt32(txtPeriodo.Text));
            
     
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing +=
            new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        private void obtenerPeriodo()
        {
            dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.SeleccionarPeriodoCerrado(1, Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()), Convert.ToInt32(ddlUbicacion.SelectedValue.Trim()));

            if (dtPeriodo.Rows.Count > 0)
            {
                txtPeriodo.Text = dtPeriodo[0].nperiodo.ToString();               

            }
            else
            {
                txtPeriodo.Text = "0";
            }
        }
        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

        protected void RbTipoPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

    }
}