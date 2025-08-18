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
    public partial class VPagosPlanillaMes : System.Web.UI.Page
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
                CargarMeses();
                CargarDptos();
                obtenerUbicaciones();                    
            }
        }
        void CargarMeses()
        {
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            var months = ((System.Globalization.DateTimeFormatInfo)dtinfo).MonthNames;
            this.ddlMes1.DataSource = months;
            ddlMes1.DataBind();
            this.ddlMes2.DataSource = months;
            ddlMes2.DataBind();
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

        private void CargarDptos()
        {
            DataTable ft = Neg_Catalogos.CargarProcesos().Tables[0];
            DataView dv = ft.DefaultView;
            dv.Sort = "codigo_depto asc";
            DataTable sortedDT = dv.ToTable();

            //DataSet de = Neg_Catalogos.CargarProcesos();
            ddldepto1.DataSource = sortedDT;
            this.ddldepto1.DataMember = "procesos";
            this.ddldepto1.DataValueField = "codigo_depto";
            ddldepto1.DataTextField = "nombre_depto";
            this.ddldepto1.DataBind();

            ddldepto2.DataSource = sortedDT;
            this.ddldepto2.DataMember = "procesos";
            this.ddldepto2.DataValueField = "codigo_depto";
            ddldepto2.DataTextField = "nombre_depto";
            this.ddldepto2.DataBind();
        }
        private void ObtenerDatos()
        {
            DataSet ds = new DataSet();
            try
            {
                int anio = Convert.ToInt32(txtAnio.Text.Trim());
                //siempre se inicia al 1er dia del mes
                DateTime fini = new DateTime(anio, ddlMes1.SelectedIndex + 1, 1);
                //se obtiene dias del mes
                int diasmes2 = DateTime.DaysInMonth(anio, ddlMes2.SelectedIndex + 1);
                DateTime ffin = new DateTime(anio, ddlMes2.SelectedIndex + 1, diasmes2);

                int todos = ddlAsigPerm.SelectedValue == "3" ? 0 : 1;
                int xemp = Convert.ToInt32(ddlAsigPerm.SelectedValue); //== "2" ? 1 : 0;
                int code1 = Convert.ToInt32(ddldepto1.SelectedValue.Trim());
                int code2 = Convert.ToInt32(ddldepto2.SelectedValue.Trim());

                DataTable rpt= Neg_Liquidacion.IngresosEgresosPlanillaxMes(fini,ffin,todos,code1,code2,xemp);
                MostrarReporte(rpt,xemp);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos");
            }
        }

        private void MostrarReporte(DataTable dtReporte,int xemp)
        {
            if (xemp==2)
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PagoPlanillasxMesEmp.rdlc");
               
            }else
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PagoPlanillasxMes.rdlc");
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
                    ObtenerDatos();                
            }
            catch (Exception ex)
            {
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }      
        protected void ddlAsigPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAsigPerm.SelectedValue == "1" || ddlAsigPerm.SelectedValue == "4")//todos por depto o gerencia
            {
                depto.Visible = false;
                divubic.Visible = false;
            }
            else if (ddlAsigPerm.SelectedValue == "2")//empleados
            {
                depto.Visible = false;
                divubic.Visible = false;
            }else//deptos
            {
                depto.Visible = true;
                divubic.Visible = false;
            }
        }
    }
}