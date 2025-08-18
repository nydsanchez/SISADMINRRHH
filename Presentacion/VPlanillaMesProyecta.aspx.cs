using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;

namespace NominaRRHH.Presentacion
{
    public partial class VPlanillaMesProyecta : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        //ULTIMA MODIFICACION GRETHEL TERCERO 31-10-2016

        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Planilla NPlanilla = new Neg_Planilla();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Periodo NPeriodo = new Neg_Periodo();

        #endregion       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMeses();
                DataTable dt = new DataTable();
                dt.Columns.Add("depto", typeof(string));
                dt.Columns.Add("codigo", typeof(int));
                dt.Columns.Add("nombre", typeof(string));
                dt.Columns.Add("destado", typeof(string));
                dt.Columns.Add("periodo", typeof(int));
                dt.Columns.Add("semana", typeof(int));
                dt.Columns.Add("fechaini", typeof(DateTime));
                dt.Columns.Add("fechafin", typeof(DateTime));
                dt.Columns.Add("dias", typeof(int));
                dt.Columns.Add("salario", typeof(decimal));
                dt.Columns.Add("incentivo", typeof(decimal));
                dt.Columns.Add("beneficio", typeof(decimal));
                dt.Columns.Add("totalingresos", typeof(decimal));
               
                Session["dt"] = dt;

                txtFecCorte.Text = DateTime.Now.ToShortDateString();
                txtAnio.Text = DateTime.Now.Year.ToString();
            }
        }
        void CargarMeses()
        {
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            var months = ((System.Globalization.DateTimeFormatInfo)dtinfo).MonthNames;
            this.ddlMes.DataSource = months;
            ddlMes.DataBind();
        }
                                      
      
        private void ObtenerDatos(DateTime fechaperiodo)
        {
            DataSet ds = new DataSet();
            int i = 0;
            try
            {
                DataTable dt = (DataTable)Session["dt"];
                dt.Clear();

                int anio = string.IsNullOrEmpty(txtAnio.Text) ? DateTime.Now.Year : Convert.ToInt32(txtAnio.Text);
                int mes = ddlMes.SelectedIndex + 1;
                Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());

                ds = Neg_Informes.PlnObtenerEmpleadosPlanillaMes( Neg_Liquidacion.Globales.fechaR);
                //se recorre cada empleado                                        

                for ( i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string depto = ds.Tables[0].Rows[i]["nombre_depto"].ToString();
                    int codigo = Convert.ToInt32(ds.Tables[0].Rows[i]["codigo_empleado"].ToString());
                    string nombre = ds.Tables[0].Rows[i]["nombre"].ToString();
                    DateTime fechaegreso = Convert.ToDateTime(ds.Tables[0].Rows[i]["fechaegreso"]);
                    int estado = Convert.ToInt32(ds.Tables[0].Rows[i]["idestado"].ToString());
                    string destado = estado == 1 ? "Activo" : "Liquidado";
                    if (estado!=1 && fechaegreso<fechaperiodo)
                    {
                        continue;
                    }
                   
                    //planillas pagadas
                    DataTable Datos = Neg_Liquidacion.ObtenerPlanillaMesProyecta(codigo,estado,fechaperiodo);
                                        
                    for (int j = 0; j < Datos.Rows.Count; j++)
                    {
                                          
                        string periodo = Datos.Rows[j]["periodo"].ToString();
                        string semana = Datos.Rows[j]["semana"].ToString();
                        string fechaini = Datos.Rows[j]["fechaini"].ToString();
                        string fechafin = Datos.Rows[j]["fechafin"].ToString();
                        string dias = Datos.Rows[j]["dias"].ToString();
                        double salariomes= Convert.ToDouble(Datos.Rows[j]["salario"].ToString());
                        double incentivomes = Convert.ToDouble(Datos.Rows[j]["incentivo"].ToString());
                        double beneficiomes = Convert.ToDouble(Datos.Rows[j]["beneficio"].ToString());
                        double totalingresos = salariomes+incentivomes+beneficiomes;

                        dt.Rows.Add(depto, codigo, nombre,destado, periodo, semana, fechaini, fechafin,dias, salariomes, incentivomes,beneficiomes, totalingresos);
                    }
                                       
                }
             
               
                MostrarReporte(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(i.ToString());
            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        private void MostrarReporte(DataTable dtReporte)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaMesProyecta.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtReporte);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            //Globales.fechaR = null;
            //Globales.fechaR = null;
            alertValida.Visible = false;
            lblAlert.Visible = false;
            try
            {
                //fechaini periodo vigente
               
                DateTime fechaperiodo = DateTime.Now;
                DateTime fechacorte = Convert.ToDateTime(txtFecCorte.Text.Trim());

                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, 1,0);
                if (dtPeriodo.Rows.Count > 0)
                {
                    fechaperiodo = Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini"]);
                }


                if (ddlMes.SelectedIndex + 1 != fechacorte.Month || Convert.ToInt32(txtAnio.Text) != fechacorte.Year)
                {
                    throw new Exception("Año y Mes no coinciden con la Fecha Corte");
                }
                int result;
                result = DateTime.Compare(fechacorte, fechaperiodo.AddDays(-1));//1ero diciembre anio anterior

                if (result >= 0)
                {
                    ObtenerDatos(fechaperiodo);
                }
                else
                {
                    throw new Exception("El parametro Fecha Corte debe ser mayor al cierre del ultimo periodo");
                }
                

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
    }
}