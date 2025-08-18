using System;
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
using System;

namespace NominaRRHH.Presentacion
{
    public partial class VLiqxAntiguedad : System.Web.UI.Page
    {
        #region REFERENCIAS
        //CREADO POR WENDY MEMBREÑO
        // 30 DE NOV DE 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }

        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        public void PresentarReporte(DataTable dt)
        {

            //ReportViewer1.ProcessingMode = ProcessingMode.Local;
            //ReportDataSource source = new ReportDataSource(nombredt, dt);
            //ReportViewer1.LocalReport.ReportPath = Server.MapPath(serverpath);
            //ReportViewer1.LocalReport.DataSources.Clear();
            //ReportViewer1.LocalReport.DataSources.Add(source);
            //ReportViewer1.LocalReport.Refresh();

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/LiqAntiguedad.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            int Codigo_empleado = 0;
            int anio = string.IsNullOrEmpty(txtAnio.Text) ? DateTime.Now.Year : Convert.ToInt32(txtAnio.Text);
            DataSet ds1 = Neg_Informes.ObtenerEmpLiqAntiguedad( anio);
            DataTable dt = Neg_Informes.DetalleSolvenciaxE(Codigo_empleado);
           
            DataSet ds = new DataSet();

            DataTable dtdatos = new DataTable();

            dtdatos.Columns.Add("nombre_depto", typeof(string));
            dtdatos.Columns.Add("Codigo_empleado", typeof(int));
            dtdatos.Columns.Add("nombrecompleto", typeof(string));
            dtdatos.Columns.Add("fechaingreso", typeof(string));
            dtdatos.Columns.Add("salMensual", typeof(decimal));
            dtdatos.Columns.Add("TipoSalario", typeof(string));
            dtdatos.Columns.Add("salMayorDia", typeof(decimal));
            dtdatos.Columns.Add("salPromedioDia", typeof(decimal));
            dtdatos.Columns.Add("IndemnizacionDia", typeof(decimal));
            dtdatos.Columns.Add("Indemnizacion", typeof(decimal));
            dtdatos.Columns.Add("AguinaldoDia", typeof(decimal));
            dtdatos.Columns.Add("Aguinaldo", typeof(decimal));
            dtdatos.Columns.Add("vacacionesDia", typeof(decimal));
            dtdatos.Columns.Add("Vacaciones", typeof(decimal));
            dtdatos.Columns.Add("TLiquidacion", typeof(decimal));
            dtdatos.Columns.Add("Deduccion", typeof(decimal));
            dtdatos.Columns.Add("Embargo", typeof(decimal));
            dtdatos.Columns.Add("Adelanto", typeof(decimal));
            

            decimal Indemnizacion = 0, Aguinaldo = 0, Vacaciones = 0, total = 0, salariomensual = 0, salmaydia = 0, salpromdia = 0, diasindem = 0, diasagui = 0, diasvac = 0;
           
            DataTable Empleados=ds1.Tables[0];//= dt.Rows.Cast<DataRow>().GroupBy(c => new { c2 = c["codigo_empleado"] }).Select(grp => grp.First()).CopyToDataTable();

            DataTable tipoxEmp = dt.Rows.Cast<DataRow>().GroupBy(c => new { c2 = c["tipo"] }).Select(grp => grp.First()).CopyToDataTable();
        
            decimal sumtotal = 0, sumporc = 0;
            Dato_Planilla Dato_Planilla = new Dato_Planilla();
            Neg_IR IR = new Neg_IR();
            Neg_Periodo neg_Periodo = new Neg_Periodo();
            DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
            DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);//new DateTime(2021, 1, 4);
            Neg_IR.Globales.dtIRHistorico = IR.ObtenerHistoricoIR(inicioano);
            Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());
            int proyecta = ckproyecta.Checked ? 1 : 0;

            //SE RECORRE CADA CODIGO DE EMPLEADO
            foreach (DataRow emp in Empleados.Rows) //se recorre cada Empleado
            {            
                //SE SETEA LOS VALORES PARA AGREGARLOS AL DATATBLE
                sumtotal = 0; sumporc = 0; Indemnizacion = 0; Aguinaldo = 0; Vacaciones = 0; total = 0; salariomensual = 0; salmaydia = 0; salpromdia = 0; diasindem = 0; diasagui = 0; diasvac=0;
                string fechaingreso = "", tiposalario = "";
                DataSet dtCalculos = Neg_Liquidacion.ObtenerDatosLiquidacion(Convert.ToInt32(emp["codigo_empleado"]), 1, 0, 0, proyecta,0,false);              
                //SI SE OBTIENEN DATOS DE LIQUIDACION
                if (dtCalculos != null)
                {
                    if (dtCalculos.Tables[1] != null)
                    {
                        if (dtCalculos.Tables[1].Rows.Count > 0)
                        {
                            decimal irvac = 0;
                            //decimal irvac = Neg_Liquidacion.CalcularIRVacaciones(Convert.ToInt32(emp["codigo_empleado"]), Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Vacaciones"]), Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["INSS"]));
                            Indemnizacion = Convert.ToDecimal(dtCalculos.Tables["Table2"].Rows[0]["Indemnizacion"]);
                            Aguinaldo = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Aguinaldo"]);
                            Vacaciones = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Vacaciones"]) - (Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["INSS"]) + irvac);
                            total = Indemnizacion + Aguinaldo + Vacaciones;
                            fechaingreso = Convert.ToDateTime(dtCalculos.Tables[1].Rows[0]["fechaingreso"]).ToShortDateString();
                            salariomensual = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["salMensual"]);
                            salmaydia = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["salMayorDia"]);
                            salpromdia = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["salPromedioDia"]);
                            diasindem = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["IndemnizacionDia"]);
                            diasagui = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["AguinaldoDia"]);
                            diasvac = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["vacacionesDia"]);
                            tiposalario = dtCalculos.Tables[1].Rows[0]["TipoSalario"].ToString();
                        }
                    }
                }
                //SE AGREGAN FILAS AL DATATABLE
                dtdatos.Rows.Add(emp["nombre_depto"].ToString(), Convert.ToInt32(emp["codigo_empleado"]), emp["nombrecompleto"].ToString(),fechaingreso,salariomensual,tiposalario,salmaydia,salpromdia,diasindem,Indemnizacion,diasagui,Aguinaldo,diasvac,Vacaciones,total , 0, 0, 0);
                //SE RECORRE CADA TIPO DE DEUDA CON LA EMPRESA
                foreach (DataRow tipo in tipoxEmp.Rows) //se recorre cada Empleado
                {
                    //SE CONSULTA Y SE SUMAN EL TIPO DE ENDEUDAMIENTO EN EL FOREACH QUE NO SEAN DEDUCCIONES PORCENTUALES
                    sumtotal = dt.AsEnumerable().Where(r => r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && r.Field<bool>("porcentual") == false && r.Field<bool>("recurrente") == false).Sum(r => r.Field<decimal>("debe"));

                    sumtotal += dt.AsEnumerable().Where(r => r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && r.Field<bool>("porcentual") == false && r.Field<bool>("recurrente") == true).Sum(r => r.Field<decimal>("valor"));

                    if (sumtotal < 0)
                        sumtotal = 0;
                    //SE OBTIENEN TODOS LOS ID DE TIPO ACTUAL EN EL FOREACH PARA HACER UN SUM POR EMPLEADO
                    var Tipoporc = (from r in dt.AsEnumerable()
                                    where r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && r.Field<bool>("porcentual") == true
                                    select new
                                    {
                                        tipo = r.Field<string>("tipo"),
                                        debe = r.Field<decimal>("debe"),
                                        valor = r.Field<decimal>("valor")
                                    }
                                ).ToList();
                    //SE RECORREN LOS TIPOS DE DEDUCCIONES PORCENTUALES EXISTENTES
                    foreach (var Porcentaje in Tipoporc)
                    {
                        if (Porcentaje.debe == 0)
                        {
                            sumporc += (Porcentaje.valor * (Indemnizacion + Aguinaldo + Vacaciones)) / 100;
                        }
                        else
                        {
                            sumporc += Porcentaje.debe;
                        }
                        
                    }

                    sumtotal = sumtotal + sumporc;

                    if (sumtotal > 0)
                    {
                        dtdatos.AsEnumerable().ToList<DataRow>().ForEach
                           (r =>
                           {
                               if (r["Codigo_empleado"].ToString().Trim() == Convert.ToInt32(emp["codigo_empleado"]).ToString().Trim())
                                   r[tipo["tipo"].ToString().Trim()] = sumtotal;
                           }
                           );
                    }
                }
            }

            PresentarReporte(dtdatos);
        }

       
    }
}