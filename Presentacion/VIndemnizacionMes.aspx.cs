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
    public partial class VIndemnizacionMes : System.Web.UI.Page
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
                DataTable dt = new DataTable();             
                dt.Columns.Add("depto", typeof(string));
                dt.Columns.Add("codigo", typeof(int));
                dt.Columns.Add("nombre", typeof(string));
                dt.Columns.Add("salpromedio", typeof(decimal));
                dt.Columns.Add("salmayor", typeof(decimal));
                dt.Columns.Add("aguinaldodia", typeof(decimal));
                dt.Columns.Add("pagoaguinaldo", typeof(decimal));
                dt.Columns.Add("indemnizaciondia", typeof(decimal));
                dt.Columns.Add("pagoindemnizacion", typeof(decimal));
                dt.Columns.Add("vacacionesdias", typeof(decimal));
                dt.Columns.Add("pagovacaciones", typeof(decimal));
                dt.Columns.Add("fechaingreso", typeof(DateTime));
                dt.Columns.Add("salmensual", typeof(decimal));
                dt.Columns.Add("neto", typeof(decimal));
                dt.Columns.Add("adindemnizacion", typeof(decimal));
                dt.Columns.Add("totalprestaciones", typeof(decimal));
                dt.Columns.Add("MesNumero", typeof(int));
                dt.Columns.Add("MesNombre", typeof(string));
                dt.Columns.Add("Ingreso", typeof(decimal));
                dt.Columns.Add("TipoSalario", typeof(int));
                Session["dt"] = dt;

                txtFecCorte.Text = DateTime.Now.ToShortDateString();
                txtAnio.Text = DateTime.Now.Year.ToString();
                ckproyecta.Checked = false;
            }
        }
        void CargarMeses() {
            var dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            var months = ((System.Globalization.DateTimeFormatInfo) dtinfo).MonthNames;
            this.ddlMes.DataSource = months;
            ddlMes.DataBind();
        }
       
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }

       
        private void ObtenerDatos()
        {
            DataSet ds = new DataSet();
            try {
                int anio = string.IsNullOrEmpty(txtAnio.Text) ? DateTime.Now.Year : Convert.ToInt32(txtAnio.Text);
                ds = Neg_Informes.ObtenerEmpIndemnizacionMes(ddlMes.SelectedIndex + 1,anio);

                DataTable dt = (DataTable)Session["dt"];
                dt.Clear();
                Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());
                int proyecta = ckproyecta.Checked ? 1 : 0;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string depto = ds.Tables[0].Rows[i]["nombre_depto"].ToString();
                    int codigo = Convert.ToInt32(ds.Tables[0].Rows[i]["codigo_empleado"].ToString());
                    string nombre = ds.Tables[0].Rows[i]["nombrecompleto"].ToString();
                  
                    //columnas de prestaciones a pagar
                    DataSet Datos = Neg_Liquidacion.ObtenerDatosLiquidacion(codigo, 1, 0,0,proyecta,0,false);
                    string fechaingreso = "";
                    decimal SalPromedio = 0, SalMayor = 0, aguinaldodia = 0, pagoaguinaldo = 0, indemnizaciondia = 0, pagoindemnizacion = 0, vacacionesdia = 0, pagovacaciones = 0, SalMensual = 0;

                    //columna ad indemnizacion
                    DataSet dsdeduc = Neg_DevYDed.DeduccionesOrdinariasObtenerxTipo(codigo, 15);//Obtengo el detalle de la deduccion 
                    decimal AdIndemnizacion = 0, neto = 0, totalprestaciones = 0;
                    string mesnombre = "", tiposalario = "";
                    int Ultimos6M;
                    decimal ingresomes;

                    if (dsdeduc.Tables.Count>0 && dsdeduc.Tables[0].Rows.Count>0)                    
                        AdIndemnizacion = Convert.ToDecimal(dsdeduc.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row["Debe"])));                    
                    else                    
                        AdIndemnizacion = 0;

                    //if (Datos != null)
                    //{
                    //    SalPromedio = float.Parse(Datos.Tables[1].Rows[0]["salPromedioDia"].ToString());
                    //    SalMayor = float.Parse(Datos.Tables[1].Rows[0]["SalMayorDia"].ToString());
                    //    aguinaldodia = float.Parse(Datos.Tables[1].Rows[0]["AguinaldoDia"].ToString());
                    //    pagoaguinaldo = float.Parse(Datos.Tables[1].Rows[0]["Aguinaldo"].ToString());
                    //    indemnizaciondia = float.Parse(Datos.Tables[1].Rows[0]["IndemnizacionDia"].ToString());
                    //    pagoindemnizacion = float.Parse(Datos.Tables[1].Rows[0]["Indemnizacion"].ToString());
                    //    vacacionesdia = float.Parse(Datos.Tables[1].Rows[0]["vacacionesDia"].ToString());
                    //    pagovacaciones = float.Parse(Datos.Tables[1].Rows[0]["Vacaciones"].ToString());
                    //    fechaingreso = Convert.ToDateTime(Datos.Tables[1].Rows[0]["fechaingreso"]).ToShortDateString();
                    //    SalMensual = float.Parse(Datos.Tables[1].Rows[0]["salMensual"].ToString());                        
                    //    totalprestaciones = Convert.ToDecimal(Datos.Tables[1].Rows[0]["totalPagar"].ToString());

                    //    neto = totalprestaciones - AdIndemnizacion;
                    //}
                    //dt.Rows.Add(depto, codigo, nombre, Math.Round(SalPromedio, 2), Math.Round(SalMayor, 2), Math.Round(aguinaldodia,2), Math.Round(pagoaguinaldo,2), Math.Round(indemnizaciondia,2), Math.Round(pagoindemnizacion,2), Math.Round(vacacionesdia,2), Math.Round(pagovacaciones,2),fechaingreso, Math.Round(SalMensual, 2), Math.Round(neto, 2), Math.Round(AdIndemnizacion, 2), Math.Round(totalprestaciones, 2));

                    if (Datos != null)
                    {
                        Ultimos6M = 6;
                        for (int j = 0; j < Datos.Tables[0].Rows.Count; j++)
                        {
                            SalPromedio = Convert.ToDecimal(Datos.Tables[1].Rows[0]["salPromedioDia"].ToString());
                            SalMayor = Convert.ToDecimal(Datos.Tables[1].Rows[0]["salMayorDia"].ToString());
                            aguinaldodia = Convert.ToDecimal(Datos.Tables[1].Rows[0]["AguinaldoDia"].ToString());
                            pagoaguinaldo = Convert.ToDecimal(Datos.Tables[1].Rows[0]["Aguinaldo"].ToString());
                            indemnizaciondia = Convert.ToDecimal(Datos.Tables[1].Rows[0]["IndemnizacionDia"].ToString());
                            pagoindemnizacion = Convert.ToDecimal(Datos.Tables[1].Rows[0]["Indemnizacion"].ToString());
                            vacacionesdia = Convert.ToDecimal(Datos.Tables[1].Rows[0]["vacacionesDia"].ToString());
                            pagovacaciones = Convert.ToDecimal(Datos.Tables[1].Rows[0]["Vacaciones"].ToString());
                            fechaingreso = Convert.ToDateTime(Datos.Tables[1].Rows[0]["fechaingreso"]).ToShortDateString();
                            SalMensual = Convert.ToDecimal(Datos.Tables[1].Rows[0]["salMensual"].ToString());
                            totalprestaciones = Convert.ToDecimal(Datos.Tables[1].Rows[0]["totalPagar"].ToString());
                            tiposalario = Datos.Tables[1].Rows[0]["TipoSalario"].ToString();
                            //totalprestaciones = pagoaguinaldo + pagoindemnizacion + pagovacaciones;
                            neto = totalprestaciones - AdIndemnizacion;

                            mesnombre = Datos.Tables[0].Rows[j]["MesNombre"].ToString();
                            ingresomes = Convert.ToDecimal(Datos.Tables[0].Rows[j]["Ingreso"]);

                            dt.Rows.Add(depto, codigo, nombre, Math.Round(SalPromedio, 2), Math.Round(SalMayor, 2), Math.Round(aguinaldodia, 2), Math.Round(pagoaguinaldo, 2), Math.Round(indemnizaciondia, 2), Math.Round(pagoindemnizacion, 2), Math.Round(vacacionesdia, 2), Math.Round(pagovacaciones, 2), fechaingreso, Math.Round(SalMensual, 2), Math.Round(neto, 2), Math.Round(AdIndemnizacion, 2), Math.Round(totalprestaciones, 2), Ultimos6M, mesnombre, ingresomes, tiposalario);
                            Ultimos6M--;
                        }
                    }
                }
                MostrarReporte(dt);
            }
            catch (Exception ex) {
                throw new Exception("Error al obtener datos de pasivo laboral");
            }
        }

        private void MostrarReporte(DataTable dtReporte)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PasivoLaboral.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtReporte);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            //Globales.fechaR = null;
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

    }
}