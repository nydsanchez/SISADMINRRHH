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
    public partial class VEmpActivos : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {             
                obtenerUbicaciones();
                activarAcciones();
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
      
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            DataSet ds=new DataSet();
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            if (ddlTipo.SelectedValue.Trim() == "1")
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/EmpleadosActivos.rdlc");
                ds = Neg_Informes.CargarEmpActivos(Convert.ToInt32(ddlAsigPerm.SelectedValue),Convert.ToInt32(ddlubicacion.SelectedValue));
            }else if(ddlTipo.SelectedValue.Trim() == "3") {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/AltasBanco.rdlc");
                DataTable dt = Neg_Informes.CargarEmpActivos(Convert.ToInt32(ddlAsigPerm.SelectedValue), Convert.ToInt32(ddlubicacion.SelectedValue)).Tables[0].Select("cuentabancaria='' or cuentabancaria='0'").CopyToDataTable();              
                ds.Tables.Add(dt.Copy());
            }
            if (ddlTipo.SelectedValue.Trim() == "2")
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/EmpleadosTodos.rdlc");
                ds = Neg_Informes.spMasterTodos();
            }
     
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing +=
            new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            activarAcciones();
        }
        protected void ddlAsigPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAsigPerm.SelectedValue == "1")//todos
            {
              
                divubic.Visible = false;
            }
            else 
            {
              
                divubic.Visible = true;
            }           
        }
        void activarAcciones()
        {
            if (ddlTipo.SelectedValue.Trim() == "1")
            {
                filtro.Visible = true;
            }
            else
            {
                filtro.Visible = false;
            }

        }
    }
}