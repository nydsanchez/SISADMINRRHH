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


namespace NominaRRHH
{
    public partial class VVacacionesDetalleActivos : System.Web.UI.Page
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
                CargarDptos();
                obtenerUbicaciones();
                txtFecCorte.Text = DateTime.Now.ToShortDateString();
                DataTable dt = new DataTable();
                dt.Columns.Add("ubicacion", typeof(string));
                dt.Columns.Add("depto", typeof(string));
                dt.Columns.Add("codigo", typeof(int));
                dt.Columns.Add("nombre", typeof(string));
                dt.Columns.Add("fechaingreso", typeof(DateTime));
                dt.Columns.Add("vacacum", typeof(decimal));
                dt.Columns.Add("vacdesc", typeof(decimal));
                dt.Columns.Add("vacpag", typeof(decimal));
                dt.Columns.Add("saldovacaciones", typeof(decimal));               
                Session["dt"] = dt;            
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

        private void CargarDptos()
        {
            DataTable ft = Neg_Catalogos.CargarProcesos().Tables[0];
            DataView dv = ft.DefaultView;
            dv.Sort = "codigo_depto asc";
            DataTable sortedDT = dv.ToTable();
         
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
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (ddlAsigPerm.SelectedValue != "3")//todos y ubicaccion
                {
                    ds = Neg_Informes.CargarEmpActivos(Convert.ToInt32(ddlAsigPerm.SelectedValue),Convert.ToInt32(ddlubicacion.SelectedValue));
                }
                else
                {
                    ds = Neg_Informes.spEmpleadosActivosxDepto(int.Parse(ddldepto1.SelectedValue.Trim().ToString()), int.Parse(ddldepto2.SelectedValue.Trim().ToString()));
                }

                DataTable dt = (DataTable)Session["dt"];
                dt.Clear();
                Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());
                DateTime fecha_ingreso;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string ubicacion = ds.Tables[0].Rows[i]["nombre_ubicacion"].ToString();
                    string depto = ds.Tables[0].Rows[i]["nombre_depto"].ToString();
                    int codigo = Convert.ToInt32(ds.Tables[0].Rows[i]["codigo_empleado"].ToString());
                    string nombre = ds.Tables[0].Rows[i]["nombrecompleto"].ToString();
                    fecha_ingreso = Convert.ToDateTime(ds.Tables[0].Rows[i]["fecha_ingreso"]);
                    //columnas de prestaciones a pagar
                    DataTable Datos = Neg_Liquidacion.CalcularDiasVacaciones(codigo, fecha_ingreso,1,userDetail.getIDEmpresa());
                    decimal saldovacaciones = 0, vacacum = 0, vacdesc = 0, vacpag = 0;
                    if (Datos != null)
                    {                      
                        vacacum =  Convert.ToDecimal(Datos.Rows[0]["vacacumuladas"]);
                        vacdesc = Convert.ToDecimal(Datos.Rows[0]["vacdescansadas"]);
                        vacpag = Convert.ToDecimal(Datos.Rows[0]["vacpagadas"]);
                        saldovacaciones = Convert.ToDecimal(Datos.Rows[0]["saldovacaciones"]);
                    }
                    dt.Rows.Add(ubicacion,depto, codigo, nombre,fecha_ingreso.ToShortDateString(),vacacum.ToString("n2"), vacdesc.ToString("n2"), vacpag.ToString("n2"), saldovacaciones.ToString("n2"));
                }
                MostrarReporte(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de pasivo laboral");
            }
        }

        private void MostrarReporte(DataTable dtReporte)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/VacacionesDetalleActivos.rdlc");
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
            if (ddlAsigPerm.SelectedValue == "1")//todos
            {
                depto.Visible = false;
                divubic.Visible = false;
            }
            else if (ddlAsigPerm.SelectedValue == "2")
            {
                depto.Visible = false;
                divubic.Visible = true;
            }else
            {
                depto.Visible = true;
                divubic.Visible = false;
            }
        }
    }
}