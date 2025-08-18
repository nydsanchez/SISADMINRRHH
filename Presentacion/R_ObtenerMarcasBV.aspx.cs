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
    public partial class R_ObtenerMarcasBV : System.Web.UI.Page
    {
        #region
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Marca Neg_Marca = new Neg_Marca();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
              
                obtenerProcesos();
                obtenerUbicaciones();
            }
        }        

        
        private void obtenerUbicaciones()
        {
            this.ddlubicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlubicacion.DataMember = "ubicaciones";
            this.ddlubicacion.DataValueField = "codigo_ubicacion";
            this.ddlubicacion.DataTextField = "nombre_ubicacion";
            this.ddlubicacion.DataBind();
        }
        private void obtenerProcesos()
        {
            this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            if (txtFechaIni.Text.Trim().Length != 0 && txtFechaFin.Text.Trim().Length != 0)
            {
                //ObtenerMarcas();
                DataTable dtInD= Neg_Marca.ObtenerMarcasHorasOficial(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text), Convert.ToInt32(ddlAsigPerm.SelectedValue), Convert.ToInt32(ddlubicacion.SelectedValue), Convert.ToInt32(ddlProceso.SelectedValue));
                MostrarReporte(dtInD);
            }

        }
     
        public void MostrarReporte(DataTable ds)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource source = new ReportDataSource("DataSet1", ds);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ReporteMarcasxRangoFecha.rdlc");
            ReportViewer1.LocalReport.DataSources.Add(source);
            ReportViewer1.LocalReport.Refresh();

        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }

        protected void ddlAsigPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAsigPerm.SelectedValue == "1")//todos
            {
                divproceso.Visible = false;
                divubic.Visible = false;
            }
            else if (ddlAsigPerm.SelectedValue == "2")//ubic
            {
                divproceso.Visible = false;
                divubic.Visible = true;
            }
            else if (ddlAsigPerm.SelectedValue == "3")//dptos x ubic
            {
                divproceso.Visible = true;
                divubic.Visible = true;
            }           
            else//depts
            {
                divproceso.Visible = true;
                divubic.Visible = false;
            }
        }
    }
}