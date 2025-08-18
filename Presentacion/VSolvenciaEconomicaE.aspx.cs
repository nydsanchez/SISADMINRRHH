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


namespace NominaRRHH.Presentacion
{
    public partial class VSolvenciaEconomicaE : System.Web.UI.Page
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
                plnemp.Visible = false;
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

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/SolvenciaE.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            int Codigo_empleado = 0;
            try
            {
                if (rbl.SelectedValue.ToString() == "2")
                {

                    //SE VALIDA QUE HALLA INGRESADO CODIGO DE EMPLEADO
                    if (!(txtcodigoEmp.Text.Trim() == ""))
                    {
                        Codigo_empleado = Convert.ToInt32(txtcodigoEmp.Text.Trim());
                        lblAlert.Text = "";
                        alertValida.Visible = false;
                    }
                    else
                    {
                        lblAlert.Text = "INGRESE EL CODIGO DE EMPLEADO";
                        lblAlert.ForeColor = System.Drawing.Color.White;
                        alertValida.Visible = true;
                        return;
                    }
                }
                //SE SELECCIONO VER GENERAL
                else
                {
                    lblAlert.Text = "";
                }

                //DataTable dt = Neg_Informes.DetalleSolvenciaxE(Codigo_empleado);
                //DataSet dtCalculos = new DataSet();
                //DataSet ds = new DataSet();

                //DataTable dtdatos = new DataTable();

                //dtdatos.Columns.Add("Estado", typeof(string));
                //dtdatos.Columns.Add("Codigo_empleado", typeof(int));
                //dtdatos.Columns.Add("nombrecompleto", typeof(string));
                //dtdatos.Columns.Add("nombre_depto", typeof(string));
                //dtdatos.Columns.Add("Deduccion", typeof(decimal));
                //dtdatos.Columns.Add("Adelanto", typeof(decimal));
                //dtdatos.Columns.Add("Embargo", typeof(decimal));
                //dtdatos.Columns.Add("Indemnizacion", typeof(decimal));
                //dtdatos.Columns.Add("Aguinaldo", typeof(decimal));
                //dtdatos.Columns.Add("Vacaciones", typeof(decimal));
                //dtdatos.Columns.Add("TLiquidacion", typeof(decimal));

                //decimal Indemnizacion = 0, Aguinaldo = 0, Vacaciones = 0, total = 0;

                ////SE OBTIENEN TODOS LOS CODIGOS DE EMPLEADOS, HACIENDO UN DISTINCT
                ////var Empleados = (from r in dt.AsEnumerable()

                ////                 select new
                ////                     {
                ////                         codigoe = r.Field<int>("codigo_empleado"),
                ////                         estado = r.Field<string>("Estado"),
                ////                         nombre = r.Field<string>("nombrecompleto"),
                ////                         depto = r.Field<string>("nombre_depto")
                ////                     }).ToList().Distinct();
                //DataTable Empleados = dt.Rows.Cast<DataRow>().GroupBy(c => new { c2 = c["codigo_empleado"] }).Select(grp => grp.First()).CopyToDataTable();


                ////SE OBTIENEN LOS TIPOS EXISTENTES,               
                ////var tipoxEmp = (from r in dt.AsEnumerable()
                ////                select r["tipo"]).Distinct().ToList();
                //DataTable tipoxEmp = dt.Rows.Cast<DataRow>().GroupBy(c => new { c2 = c["tipo"] }).Select(grp => grp.First()).CopyToDataTable();



                //decimal sumtotal = 0, sumporc = 0;
                //Dato_Planilla Dato_Planilla = new Dato_Planilla();
                //Neg_IR IR = new Neg_IR();
                //Neg_Periodo neg_Periodo = new Neg_Periodo();
                //DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
                //DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);//new DateTime(2021, 1, 4);
                //Neg_IR.Globales.dtIRHistorico = IR.ObtenerHistoricoIR(inicioano);

                ////SE RECORRE CADA CODIGO DE EMPLEADO
                //foreach (DataRow emp in Empleados.Rows) //se recorre cada Empleado
                //{

                //    //SE SETEA LOS VALORES PARA AGREGARLOS AL DATATBLE
                //    sumtotal = 0; sumporc = 0; Indemnizacion = 0; Aguinaldo = 0; Vacaciones = 0; total = 0;
                //    dtCalculos = Neg_Liquidacion.IngresosUltimos6M(Convert.ToInt32(emp["codigo_empleado"]));
                //    //SI SE OBTIENEN DATOS DE LIQUIDACION
                //    if (dtCalculos != null)
                //    {
                //        if (dtCalculos.Tables[1] != null)
                //        {
                //            if (dtCalculos.Tables[1].Rows.Count > 0)
                //            {
                //                decimal irvac = Neg_Liquidacion.CalcularIRVacaciones(Convert.ToInt32(emp["codigo_empleado"]), Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Vacaciones"]), Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["INSS"]));
                //                Indemnizacion = Convert.ToDecimal(dtCalculos.Tables["Table2"].Rows[0]["Indemnizacion"]);
                //                Aguinaldo = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Aguinaldo"]);
                //                Vacaciones = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Vacaciones"]) - (Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["INSS"]) + irvac);
                //                total = Indemnizacion + Aguinaldo + Vacaciones;
                //            }
                //        }
                //    }
                //    //SE AGREGAN FILAS AL DATATABLE
                //    dtdatos.Rows.Add(emp["estado"].ToString(), Convert.ToInt32(emp["codigo_empleado"]), emp["nombrecompleto"].ToString(), emp["nombre_depto"].ToString(), 0, 0, 0, Indemnizacion, Aguinaldo, Vacaciones, total);
                //    //SE RECORRE CADA TIPO DE DEUDA CON LA EMPRESA
                //    foreach (DataRow tipo in tipoxEmp.Rows) //se recorre cada Empleado
                //    {
                //        //SE CONSULTA Y SE SUMAN EL TIPO DE ENDEUDAMIENTO EN EL FOREACH QUE NO SEAN DEDUCCIONES PORCENTUALES
                //        sumtotal = dt.AsEnumerable().Where(r => r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && r.Field<bool>("porcentual") == false && r.Field<bool>("recurrente") == false).Sum(r => r.Field<decimal>("debe"));

                //        sumtotal += dt.AsEnumerable().Where(r => r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && r.Field<bool>("porcentual") == false && r.Field<bool>("recurrente") == true).Sum(r => r.Field<decimal>("valor"));

                //        if (sumtotal < 0)
                //            sumtotal = 0;
                //        //SE OBTIENEN TODOS LOS ID DE TIPO ACTUAL EN EL FOREACH PARA HACER UN SUM POR EMPLEADO
                //        var Tipoporc = (from r in dt.AsEnumerable()
                //                        where r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && r.Field<bool>("porcentual") == true
                //                        select new
                //                        {
                //                            tipo = r.Field<string>("tipo"),
                //                            debe = r.Field<decimal>("debe"),
                //                            valor = r.Field<decimal>("valor")
                //                        }
                //                    ).ToList();
                //        //SE RECORREN LOS TIPOS DE DEDUCCIONES PORCENTUALES EXISTENTES
                //        foreach (var Porcentaje in Tipoporc)
                //        {
                //            if (Porcentaje.debe == 0)
                //            {
                //                sumporc += (Porcentaje.valor * (Indemnizacion + Aguinaldo + Vacaciones)) / 100;
                //            }
                //            else
                //            {
                //                sumporc += Porcentaje.debe;
                //            }

                //        }

                //        sumtotal = sumtotal + sumporc;

                //        if (sumtotal > 0)
                //        {
                //            dtdatos.AsEnumerable().ToList<DataRow>().ForEach
                //               (r =>
                //               {
                //                   if (r["Codigo_empleado"].ToString().Trim() == Convert.ToInt32(emp["codigo_empleado"]).ToString().Trim())
                //                       r[tipo["tipo"].ToString().Trim()] = sumtotal;
                //               }
                //               );
                //        }
                //    }
                //}
                Neg_Liquidacion.Globales.fechaR = DateTime.Now;
                DataTable dt = Neg_Liquidacion.ObtenerSolvenciaEconomica(Codigo_empleado);
                PresentarReporte(dt);
            } catch (Exception ex)
            {
                lblAlert.Text = ex.Message;
                lblAlert.ForeColor = System.Drawing.Color.White;
                alertValida.Visible = true;
            }
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (rbl.SelectedValue.ToString() == "2")
            {
                plnemp.Visible = true;
            }
            else
            {
                plnemp.Visible = false;

            }
        }
    }
}