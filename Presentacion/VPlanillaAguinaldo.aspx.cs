using Microsoft.Reporting.WebForms;
using Negocios;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace NominaRRHH.Presentacion
{
    public partial class VPlanillaAguinaldo : System.Web.UI.Page
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
        Neg_Periodo NPeriodo = new Neg_Periodo();
        //Globales Globales = new Globales();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("gerencia", typeof(string));
                dt.Columns.Add("depto", typeof(string));
                dt.Columns.Add("codigo", typeof(int));
                dt.Columns.Add("nombre", typeof(string));
                dt.Columns.Add("salmayor", typeof(decimal));
                dt.Columns.Add("salmayordia", typeof(decimal));
                dt.Columns.Add("aguinaldodia", typeof(decimal));
                dt.Columns.Add("pagoaguinaldo", typeof(decimal));
                dt.Columns.Add("fechaingreso", typeof(DateTime));
                dt.Columns.Add("salmensual", typeof(decimal));
                dt.Columns.Add("MesNumero", typeof(int));
                dt.Columns.Add("MesNombre", typeof(string));
                dt.Columns.Add("Ingreso", typeof(decimal));
                dt.Columns.Add("TipoSalario", typeof(int));
                Session["dt"] = dt;


            }
        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }


        private void ObtenerDatos()
        {

            try
            {


                DataTable dt = (DataTable)Session["dt"];
                dt.Clear();
                DataTable devengado = Neg_Informes.obtenerPlanillaAguinaldo(Convert.ToInt32(txtPeriodo.Text));
                DataTable meses = Neg_Informes.ObtenerDetalleIngresoMesAguinaldo(Convert.ToInt32(txtPeriodo.Text));

                for (int i = 0; i < devengado.Rows.Count; i++)
                {
                    string fechaingreso = "";
                    decimal SalPromedio = 0, SalMayorDia = 0, SalMayor = 0, aguinaldodia = 0, pagoaguinaldo = 0, indemnizaciondia = 0, pagoindemnizacion = 0, vacacionesdia = 0, pagovacaciones = 0, SalMensual = 0;
                    string mesnombre = "", tiposalario = "";

                    string gerencia = devengado.Rows[i]["gerencia"].ToString();
                    string depto = devengado.Rows[i]["nombre_depto"].ToString();
                    int codigo = Convert.ToInt32(devengado.Rows[i]["codigo_empleado"].ToString());
                    string nombre = devengado.Rows[i]["nombrecompleto"].ToString();
                    SalMayor = Convert.ToDecimal(devengado.Rows[i]["salMayor"].ToString());
                    SalMayorDia = Convert.ToDecimal(devengado.Rows[i]["salMayorDia"].ToString());
                    
                    aguinaldodia = Convert.ToDecimal(devengado.Rows[i]["AguinaldoDia"].ToString());
                    pagoaguinaldo = Convert.ToDecimal(devengado.Rows[i]["Aguinaldo"].ToString());

                    fechaingreso = Convert.ToDateTime(devengado.Rows[i]["fecha_ingreso"]).ToShortDateString();
                    SalMensual = Convert.ToDecimal(devengado.Rows[i]["salariomensual"].ToString());

                    tiposalario = devengado.Rows[i]["idtiposalario"].ToString();
                    //columnas de prestaciones a pagar
                    if (codigo == 871986)
                    {

                    }

                    int Ultimos6M;
                    decimal ingresomes;
                    DataRow[] mescod = null;
                    mescod = meses.AsEnumerable().Where(c => c.Field<int>("codigo") == codigo).ToArray();    
                    Ultimos6M = 6;
                    foreach (DataRow dr in mescod)
                    {
                        //totalprestaciones = pagoaguinaldo + pagoindemnizacion + pagovacaciones;                          
                        mesnombre = dr["MesNombre"].ToString();
                        ingresomes = Convert.ToDecimal(dr["Ingreso"]);

                        dt.Rows.Add(gerencia, depto, codigo, nombre,  Math.Round(SalMayor, 2), Math.Round(SalMayorDia, 2), Math.Round(aguinaldodia, 2), Math.Round(pagoaguinaldo, 2),  fechaingreso, Math.Round(SalMensual, 2), Ultimos6M, mesnombre, ingresomes, tiposalario);
                        Ultimos6M--;
                    }

                }
                MostrarReporte(dt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void MostrarReporte(DataTable dtReporte)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaAguinaldo.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtReporte);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }


        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPeriodo.Text))
                {
                    throw new Exception("Digite un periodo");
                }
                ObtenerDatos();


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