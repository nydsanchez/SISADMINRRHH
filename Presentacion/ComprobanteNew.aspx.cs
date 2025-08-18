using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Datos;
//////
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Negocios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Schema;

namespace NominaRRHH.Presentacion
{

    public partial class ComprobanteNew : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Liquidacion liquidacion = new Neg_Liquidacion();
        //private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        #endregion
        // Crear un objeto PdfRenderer
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                obtenerProcesos();
                ddlProceso.SelectedIndex = -1;
            }
        }
        
        private void obtenerProcesos()
        {
            this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }

        private string CrearColilla(int filtroemail)
        {

            // Llamar desde aqui a PrintColilla

            //empleados pagados en planilla
            string html = "";
            try
            {
                if (TxtBuscar.Text.Trim() == "")
                {
                    throw new Exception("Debe especificar periodo");
                }
                int tplanilla = 0, semana2 = 2, periodo2 = 0;//en caso d consolidar esta variable puede tener semana 1 de otro periodo
                DateTime fechaini = new DateTime();
                DateTime fechafin = new DateTime();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(TxtBuscar.Text.Trim()));
                tplanilla = dtPeriodo[0].tplanilla;
                periodo2 = Convert.ToInt32(TxtBuscar.Text.Trim());
                fechaini = dtPeriodo[0].fechaini;
                fechafin = dtPeriodo[0].fechafin2;
                //aqui hacer conversion periodo semanal a catorcenal
                string encabezado = TxtBuscar.Text.Trim();
                string Depto = ddlProceso.SelectedValue.Trim();
                string CodEmpleado = TxtCodigoE.Text.Trim();

                // solo para emials
                DataTable emailTable = new DataTable();
                emailTable.Columns.Add("correo");
                emailTable.Columns.Add("adjunto");
                string asunto = "";


                if (ChkAllEmpleados.Checked)
                {
                    Depto = "";
                    CodEmpleado = "";
                }
                string txtperiodo2 = TxtBuscar.Text.Trim();
                if (ChkConsolida.Checked)//reporte consolidad dos periodos
                {
                    if (!string.IsNullOrEmpty(txtPeriodo2.Text.Trim()))
                    {
                        encabezado += " - " + txtPeriodo2.Text.Trim();
                        if (!Neg_Informes.PlnPeriodosConsolidadosEqvIns(Convert.ToInt32(TxtBuscar.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim())))
                        {
                            throw new Exception("Error al consolidar periodo");
                        }
                       
                    }

                   

                    if (dtPeriodo[0].tperiodo == 1 && dtPeriodo[0].tplanilla == 4)
                    {
                        //cuando se consolidan dos periodos semanales se debe dar tratamiento de catorcena, cambiar a tplanilla 4 consolidar 0
                        //select periodoconsolidado nueva tabla
                        //asignar a variables locales valores de la tabla
                        DataTable periodoConsolidado = Neg_Informes.PlnPeriodosConsolidadosEqvSel(int.Parse(TxtBuscar.Text.Trim()));
                        tplanilla = 1;
                        semana2 = 1;
                        periodo2 = Convert.ToInt32(txtPeriodo2.Text.Trim());
                        txtperiodo2 = txtPeriodo2.Text.Trim();
                        fechaini = Convert.ToDateTime(periodoConsolidado.Rows[0]["fechaini"]);
                        fechafin = Convert.ToDateTime(periodoConsolidado.Rows[0]["fechafin"]);
                    }

                }

                if (string.IsNullOrWhiteSpace(TxtCodigoE.Text.Trim()))
                {
                    // El campo está vacío o solo contiene espacios en blanco
                    // Aquí puedes mostrar un mensaje de error o realizar alguna acción
                   // MessageBox.Show("El código no puede estar vacío. Por favor, ingresa un valor válido.");
                }
                else
                {
                    // El campo tiene un valor válido
                    // Aquí puedes continuar con la lógica de tu aplicación
                    Depto = "";
                }


                DataSet dsEmpleadospln = Neg_Informes.ObtenerEmpleadosPlanilla14nal(TxtBuscar.Text.Trim(), txtperiodo2, fechaini, fechafin, Depto, TxtCodigoE.Text.Trim(), ChkAll.Checked, ChkEfectivo.Checked, filtroemail, ChkConsolida.Checked, ChkAllEmpleados.Checked);

                DataTable codigoExcel = new DataTable();

                if (dsEmpleadospln.Tables.Count > 0)
                {
                    DataTable dtEmpleadospln = dsEmpleadospln.Tables[0];
                    if (dtPeriodo[0].tperiodo == 3 || dtPeriodo[0].tperiodo == 4 || dtPeriodo[0].tperiodo == 5)//planilla de aguinaldo o vacaciones
                    {
                       // html = Neg_Informes.GenerarComprobantePrestacionPdf(dtEmpleadospln, TxtBuscar.Text.Trim(), periodo2, 1, tplanilla, dtPeriodo[0].tperiodo, filtroemail);
                    }
                    else
                    {

                        string codigo = "";
                        string email = "";
                        // email colillas
                        if (filtroemail == 3)
                        {
                            // Usando foreach para iterar sobre las filas
                            foreach (DataRow row in dtEmpleadospln.Rows)
                            {
                                // Leer el campo "codigo"
                               codigo = row["codigo"].ToString();
                               email = row["email"].ToString();
                                
                                ChkAllEmpleados.Checked = false;

                                GenerarComprobantePeriodoCR(TxtBuscar.Text.Trim(), periodo2, semana2, tplanilla, dtPeriodo[0].tperiodo, filtroemail, encabezado, ChkConsolida.Checked, ChkAllEmpleados.Checked, codigo, email, false, null);

                                // Hacer algo con el código (por ejemplo, imprimirlo)
                                Console.WriteLine(codigo);
                            }
                            

                        }
                        else
                        {
                            if (CheckExcel.Checked)
                            {
                                
                                codigoExcel = Session["datos"] as DataTable;



                                GenerarComprobantePeriodoCR(TxtBuscar.Text.Trim(), periodo2, semana2, tplanilla, dtPeriodo[0].tperiodo, filtroemail, encabezado, ChkConsolida.Checked, ChkAllEmpleados.Checked, codigo, email, CheckExcel.Checked, codigoExcel);
                            }
                            else
                            {
                                // Original
                                GenerarComprobantePeriodoCR(TxtBuscar.Text.Trim(), periodo2, semana2, tplanilla, dtPeriodo[0].tperiodo, filtroemail, encabezado, ChkConsolida.Checked, ChkAllEmpleados.Checked, codigo, email, CheckExcel.Checked, codigoExcel);
                                //ChkAllEmpleados.Checked = false;
                                // GenerarComprobantePeriodoCRV2(TxtBuscar.Text.Trim(), periodo2, semana2, tplanilla, dtPeriodo[0].tperiodo, filtroemail, encabezado, ChkConsolida.Checked, ChkAllEmpleados.Checked, codigo, email, dtEmpleadospln);
                            }


                        }



                        /// Solo empleados desde Excel
                        //else
                        //{
                        //    filtro = 1;
                        //    valorFiltro = 0;
                        //    codigo = Session["datos"] as DataTable;
                        //}
                        //DataSet dsEmpleadospln = Neg_Informes.ObtenerEmpleadosPlanilla(txtperiodo.Text.Trim(), txtperiodo.Text, ini, fin, ddlProceso.SelectedValue.Trim(), TxtCodigoE.Text.Trim(), ChkAll.Checked, ChkEfectivo.Checked, filtroemail, false, true);
                        //if (dsEmpleadospln.Tables.Count > 0)
                        //{
                        //    DataTable dtEmpleadospln = dsEmpleadospln.Tables[0];
                        //    if (codigo.Rows.Count > 0)
                        //    {
                        //        int[] codemp = codigo.AsEnumerable().Select(u => u.Field<int>("codigo_empleado")).ToArray();
                        //        dtEmpleadospln = dtEmpleadospln.AsEnumerable().Where(c => codemp.Contains(c.Field<int>("codigo_empleado"))).CopyToDataTable();
                        //    }
                        //    html = Neg_Informes.GenerarColilla_ViaticoPeriodoPdf(dtEmpleadospln, txtperiodo.Text.Trim(), ini, fin, dtPeriodo[0].tperiodo, 1, 4, dtPeriodo[0].tperiodo, filtroemail, encabezado, false);
                        //}

                    }



                }
               
                return html;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        //metodos de generacion de comprobantes, Crystal Reports
        //public void GenerarComprobantePeriodoCR( string nperiodo, int periodo2, int semana2, int tipoplanilla, int tperiodo, int filtroemail, string encabezado, bool periodoConsolida, bool AllEmpleados, string codigo, string email)
        //{
        //    try
        //    {
        //        Neg_Periodo NPeriodo = new Neg_Periodo();
        //        dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(nperiodo));
        //        DateTime ini = dtPeriodo[0].fechaini;
        //        DateTime fin = dtPeriodo[0].fechafin;

        //        DateTime ini2 = dtPeriodo[0].fechaini2;
        //        DateTime fin2 = dtPeriodo[0].fechafin2;
        //        int Semana1 = 0;
        //        string vmSubtituloS2 = "";
        //        var vmSubtitulo = "Colilla de Pago del Periodo " + nperiodo + ", Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin2.Date.ToString("d/MM/yyyy");
        //        vmSubtituloS2 = vmSubtitulo;
        //        if (periodoConsolida == true)
        //        {
        //            if (Convert.ToInt32(nperiodo) != periodo2)
        //            {
        //                 ini = dtPeriodo[0].fechaini;
        //                 fin = dtPeriodo[0].fechafin2;

        //                dsPlanilla.dtPeriodoDataTable dtPeriodo2 = NPeriodo.PeriodoSel(periodo2);
        //                //ini = dtPeriodo[0].fechaini;
        //                //fin = dtPeriodo[0].fechafin;

        //                ini2 = dtPeriodo[0].fechaini;
        //                fin2 = dtPeriodo[0].fechafin2;

        //                vmSubtitulo   = "Colilla de Pago del Periodo " + nperiodo + ", Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin.Date.ToString("d/MM/yyyy");
        //                vmSubtituloS2 = "Colilla de Pago del Periodo " + periodo2 + ", Del " + ini2.Date.ToString("d/MM/yyyy") + " Al " + fin2.Date.ToString("d/MM/yyyy");
        //            }
        //            Semana1 = 1;
        //            semana2 = 1;
        //            //DateTime ini2 = dtPeriodo[0].fechaini;
        //            //DateTime fin2 = dtPeriodo[0].fechafin2;
        //        }
        //        else
        //        {
        //            periodo2 = Convert.ToInt32( nperiodo);
        //            //semana2 = 1;
        //        }

        //        string CodEmpleado = TxtCodigoE.Text.Trim();
        //        if (filtroemail == 3)
        //        {
        //            CodEmpleado = codigo;

        //        }

        //        // Generar el DataSet con los datos
        //        DataSet Reporte = GenerarPDF(nperiodo, periodo2.ToString(), ini, fin2, ddlProceso.SelectedValue.Trim(), CodEmpleado, ChkAll.Checked, ChkEfectivo.Checked, filtroemail, ChkConsolida.Checked, Semana1, semana2, 1, vmSubtitulo, vmSubtituloS2, AllEmpleados);

        //        // Configuración del reporte
        //        ReportDocument rd = new ReportDocument();
        //        rd.Load(Server.MapPath("~/Reportes/ComprobantePago.rpt"));
        //        rd.SetDataSource(Reporte);

        //        // Acceso al subreporte
        //        ReportDocument subReport = rd.OpenSubreport("ColillaConceptos1.rpt");
        //        subReport.SetDataSource(Reporte);

        //        ReportDocument subReport2 = rd.OpenSubreport("ColumnViaticos01.rpt");
        //        subReport2.SetDataSource(Reporte);

        //        ReportDocument subReport3 = rd.OpenSubreport("ColumnViaticos02.rpt");
        //        subReport3.SetDataSource(Reporte);

        //        // Asignar parámetros
        //        rd.SetParameterValue("titEmpresa", "KAIZEN");
        //        rd.SetParameterValue("FechaVacaciones", fin2.ToShortDateString());
        //        rd.SetParameterValue("titPeriodo", vmSubtitulo);
        //        //rd.SetParameterValue("titPeriodoS2", vmSubtituloS2);


        //        Response.Buffer = false;
        //        Response.ClearContent();
        //        Response.ClearHeaders();

        //        //Exportar el reporte a un Stream
        //        // 
        //        string ruta = Server.MapPath("~/Trash/ColillaPago_Periodo_"+ nperiodo +"_Empleado_"+ CodEmpleado + ".pdf");


        //        //string directorio = Path.GetDirectoryName(ruta);
        //        //if (!Directory.Exists(directorio))
        //        //    Directory.CreateDirectory(directorio); // Crea la carpeta si no existe

        //        //rd.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);



        //        string asunto = "";
        //        // envia correo
        //        if (filtroemail == 3)
        //        {
        //            rd.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);
        //            rd.Close();
        //            rd.Dispose();

        //            asunto = vmSubtitulo;
        //            bool resultadoemail = Neg_Informes.EnviarCorreoColillaPDF(asunto, email, ruta);
        //        }
        //        else
        //        {
        //            // Exportar directamente a Stream
        //            using (Stream pdfStream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
        //            {
        //                Response.Clear();
        //                Response.ContentType = "application/pdf";
        //                //Alternativa: Descargar el PDF Directamente
        //                //Response.AddHeader("content-disposition", "attachment; filename=ComprobantePago.pdf");
        //                //abrir en la misma pestaña del navegados
        //                //Response.AddHeader("content-disposition", "inline; filename=ComprobantePago.pdf");
        //                // Abrir en una Nueva Pestaña
        //                Response.AddHeader("content-disposition", "inline; filename=ComprobantePagoPeriodo" + nperiodo + "_AL_" + periodo2.ToString() + ".pdf");
        //                Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                pdfStream.CopyTo(Response.OutputStream);
        //                Response.Flush();
        //                Response.End();
        //                rd.Close();
        //                rd.Dispose();
        //            }
        //            //try
        //            //{
        //            //    using (Stream pdfStream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
        //            //    {
        //            //        Response.Clear();
        //            //        Response.ContentType = "application/pdf";
        //            //        Response.AddHeader("content-disposition", "inline; filename=ComprobantePagoPeriodo" + nperiodo + "_AL_" + periodo2.ToString() + ".pdf");
        //            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            //        pdfStream.CopyTo(Response.OutputStream);
        //            //        Response.Flush();
        //            //        Response.End();
        //            //    }
        //            //}
        //            //catch (Exception ex)
        //            //{
        //            //    // Maneja la excepción según sea necesario
        //            //    Console.WriteLine("Error al exportar el reporte a PDF: " + ex.Message);
        //            //}
        //            //finally
        //            //{
        //            //    if (rd != null)
        //            //    {
        //            //        rd.Close();
        //            //        rd.Dispose();
        //            //    }
        //            //}


        //            //using (Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
        //            //{
        //            //    using (FileStream fileStream = new FileStream(ruta, FileMode.Create, FileAccess.Write))
        //            //    {
        //            //        stream.CopyTo(fileStream);
        //            //    }
        //            //}

        //            //End

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public void GenerarComprobantePeriodoCR(string nperiodo, int periodo2, int semana2, int tipoplanilla, int tperiodo, int filtroemail, string encabezado, bool periodoConsolida, bool AllEmpleados, string codigo, string email, bool Excel, DataTable dtEmpleados)
        {
           
        
            try
            { 



                //// Ruta original del archivo XSD
                string rutaOriginal = @"c:\K2025\NominaRRHH\NominaRRHH\Reportes\DataSets\Colillas.xsd";

                //// Nueva ruta del archivo XSD
                string nuevaRuta = @"c:\inetpub\wwwroot\SISADMINRRHH\Reportes\DataSets\Colillas.xsd";
             
                               
                //// Cargar el esquema desde la ruta original
                XmlSchema esquema = CargarEsquema(rutaOriginal);

               
                if (esquema == null)
                {
                    // Si no se pudo cargar desde la ruta original, intenta con la nueva ruta
                    esquema = CargarEsquema(nuevaRuta);
                }

                //// Verificar si el esquema se cargó correctamente
                //if (esquema != null)
                //{
                //    Console.WriteLine("Esquema cargado correctamente.");
                //    // Aquí puedes trabajar con el esquema
                //}
                //else
                //{
                //    Console.WriteLine("No se pudo cargar el esquema desde ninguna de las rutas.");
                //}

                Neg_Periodo NPeriodo = new Neg_Periodo();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(nperiodo));
                DateTime ini = dtPeriodo[0].fechaini;
                DateTime fin = dtPeriodo[0].fechafin;

                DateTime ini2 = dtPeriodo[0].fechaini2;
                DateTime fin2 = dtPeriodo[0].fechafin2;
                int Semana1 = 0;
                string vmSubtituloS1 = "";
                string vmSubtituloS2 = "";
                var vmSubtitulo = "Colilla de Pago del Periodo " + nperiodo + ", Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin2.Date.ToString("d/MM/yyyy");
                vmSubtituloS2 = vmSubtitulo;
                if (dtPeriodo.Rows.Count == 0)
                {
                    //logger.Error("No se encontraron datos para el periodo especificado.");
                    throw new Exception("No se encontraron datos para el periodo especificado.");
                }

                if (periodoConsolida == true)
                {
                    if (Convert.ToInt32(nperiodo) != periodo2)
                    {
                        ini = dtPeriodo[0].fechaini;
                        fin = dtPeriodo[0].fechafin2;

                        dsPlanilla.dtPeriodoDataTable dtPeriodo2 = NPeriodo.PeriodoSel(periodo2);
                        ini2 = dtPeriodo2[0].fechaini;
                        fin2 = dtPeriodo2[0].fechafin2;

                        vmSubtitulo = "Colilla de Pago del Periodo " + nperiodo + " Al " + periodo2 + " Fechas : Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin2.Date.ToString("d/MM/yyyy");
                        vmSubtituloS1 = "Colilla de Pago del Periodo " + nperiodo + ", Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin.Date.ToString("d/MM/yyyy");
                        //vmSubtitulo = "Colilla de Pago del Periodo " + nperiodo + ", Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin.Date.ToString("d/MM/yyyy");
                        vmSubtituloS2 = "Colilla de Pago del Periodo " + periodo2 + ", Del " + ini2.Date.ToString("d/MM/yyyy") + " Al " + fin2.Date.ToString("d/MM/yyyy");
                    }
                    Semana1 = 1;
                    semana2 = 1;
                }
                else
                {
                    periodo2 = Convert.ToInt32(nperiodo);
                }

                string CodEmpleado = TxtCodigoE.Text.Trim();
                if (filtroemail == 3)
                {
                    CodEmpleado = codigo;
                }

                // Generar el DataSet con los datos
                DataSet Reporte = GenerarPDF(nperiodo, periodo2.ToString(), ini, fin2, ddlProceso.SelectedValue.Trim(), CodEmpleado, ChkAll.Checked, ChkEfectivo.Checked, filtroemail, ChkConsolida.Checked, Semana1, semana2, 1, vmSubtituloS1, vmSubtituloS2, AllEmpleados, Excel, dtEmpleados);


               
                // Configuración del reporte
                //ReportDocument rd = new ReportDocument();

                using (var rd = new ReportDocument())
                {
                    rd.Load(Server.MapPath("~/Reportes/ComprobantePago.rpt"));

                    DataSet temporalDataSetR1 = new DataSet();
                    //subReport2.SetDataSource(Reporte);


                    // Nombres de las tablas que necesitas para el subreporte
                    string[] tablasRequeridas = new string[] { "ObtenerEmpleadosPlanillaSP",  "periodos" };

                    // Generar el DataSet para el subreporte
                    temporalDataSetR1 = GenerarDataSetSubReporte(Reporte, tablasRequeridas);

                    // Agregar solo las tablas necesarias
                    //temporalDataSetR1.Tables.Add(Reporte.Tables["ObtenerEmpleadosPlanillaSP"].Copy());
                    //temporalDataSetR1.Tables.Add(Reporte.Tables["ObtenerEncComprobantePago"].Copy());
                    //temporalDataSetR1.Tables.Add(Reporte.Tables["periodos"].Copy());


                    rd.SetDataSource(temporalDataSetR1);

                    // Acceso al subreporte ingresos
                    ReportDocument subReport = rd.OpenSubreport("ColillaConceptos1.rpt");

                    DataSet temporalDataSetC1 = new DataSet();
                    //subReport2.SetDataSource(Reporte);

                    tablasRequeridas = new string[] { "ObtenerEstructuraComprobantePago",  "periodos" };

                    // Agregar solo las tablas necesarias
                    //temporalDataSetC1.Tables.Add(Reporte.Tables["ObtenerEstructuraComprobantePago"].Copy());
                    //temporalDataSetC1.Tables.Add(Reporte.Tables["periodos"].Copy());
                    temporalDataSetC1 = GenerarDataSetSubReporte(Reporte, tablasRequeridas);

                    subReport.SetDataSource(temporalDataSetC1);

                    // Acceso al subreporte Egresos
                    ReportDocument subReportE = rd.OpenSubreport("ColillaConceptos2.rpt");

                    DataSet temporalDataSetC2 = new DataSet();
                    //subReport2.SetDataSource(Reporte);
                    tablasRequeridas = new string[] { "ObtenerEstructuraComprobantePagoNewV2", "periodos" };

                    // Agregar solo las tablas necesarias
                    //temporalDataSetC2.Tables.Add(Reporte.Tables["ObtenerEstructuraComprobantePagoNewV2"].Copy());
                    //temporalDataSetC2.Tables.Add(Reporte.Tables["periodos"].Copy());
                    temporalDataSetC2 = GenerarDataSetSubReporte(Reporte, tablasRequeridas);

                    subReportE.SetDataSource(temporalDataSetC2);


                    ReportDocument subReport2 = rd.OpenSubreport("ColumnViaticos01.rpt");
                    // Crear un nuevo DataSet temporal si es necesario
                    DataSet temporalDataSet1 = new DataSet();
                    //subReport2.SetDataSource(Reporte);

                    tablasRequeridas = new string[] { "ViaticosS1", "periodos" };

                    // Agregar solo las tablas necesarias
                    //temporalDataSet1.Tables.Add(Reporte.Tables["ViaticosS1"].Copy());
                    //temporalDataSet1.Tables.Add(Reporte.Tables["periodos"].Copy());
                    temporalDataSet1 = GenerarDataSetSubReporte(Reporte, tablasRequeridas);

                    // Pasar el nuevo DataSet al subreporte
                    subReport2.SetDataSource(temporalDataSet1);



                    ReportDocument subReport3 = rd.OpenSubreport("ColumnViaticos02.rpt");
                    //subReport3.SetDataSource(Reporte);
                    // Crear un nuevo DataSet temporal si es necesario
                    DataSet temporalDataSet2 = new DataSet();
                    tablasRequeridas = new string[] { "ViaticosS2", "periodos" };

                    // Agregar solo las tablas necesarias
                    //temporalDataSet2.Tables.Add(Reporte.Tables["ViaticosS2"].Copy());
                    //temporalDataSet2.Tables.Add(Reporte.Tables["periodos"].Copy());
                    temporalDataSet2 = GenerarDataSetSubReporte(Reporte, tablasRequeridas);

                    // Pasar el nuevo DataSet al subreporte
                    subReport3.SetDataSource(temporalDataSet2);

                    // segunda impresion solo colilla pago
                    // Acceso al subreporte ingresos
                    ReportDocument subReportb1 = rd.OpenSubreport("ColillaConceptos1b.rpt");

                    DataSet temporalDataSetC1b = new DataSet();
                    //subReport2.SetDataSource(Reporte);
                    tablasRequeridas = new string[] { "ObtenerEstructuraComprobantePago", "periodos" };

                    // Agregar solo las tablas necesarias
                    //temporalDataSetC1b.Tables.Add(Reporte.Tables["ObtenerEstructuraComprobantePago"].Copy());
                    //temporalDataSetC1b.Tables.Add(Reporte.Tables["periodos"].Copy());
                    temporalDataSetC1b = GenerarDataSetSubReporte(Reporte, tablasRequeridas);

                    subReportb1.SetDataSource(temporalDataSetC1b);

                    // Acceso al subreporte Egresos
                    ReportDocument subReportb2 = rd.OpenSubreport("ColillaConceptos2b.rpt");

                    DataSet temporalDataSetC2b = new DataSet();
                    //subReport2.SetDataSource(Reporte);
                    tablasRequeridas = new string[] { "ObtenerEstructuraComprobantePagoNewV2", "periodos" };

                    // Agregar solo las tablas necesarias
                    //temporalDataSetC2b.Tables.Add(Reporte.Tables["ObtenerEstructuraComprobantePagoNewV2"].Copy());
                    //temporalDataSetC2b.Tables.Add(Reporte.Tables["periodos"].Copy());
                    temporalDataSetC2b = GenerarDataSetSubReporte(Reporte, tablasRequeridas);
                    subReportb2.SetDataSource(temporalDataSetC2b);


                    // Asignar parámetros
                    rd.SetParameterValue("titEmpresa", "KAIZEN");
                    rd.SetParameterValue("FechaVacaciones", fin2.ToShortDateString());
                    rd.SetParameterValue("titPeriodo", vmSubtitulo);

                    Response.Buffer = false;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    string ruta = Server.MapPath("~/Trash/ColillaPago_Periodo_" + nperiodo + "_Empleado_" + CodEmpleado + ".pdf");



                    if (filtroemail == 3)
                    {
                        rd.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);
                        //rd.Close();
                        //rd.Dispose();
                        // Cerrar y liberar recursos del ReportDocument
                        rd.Close();
                        rd.Dispose();
                        string asunto = vmSubtitulo;
                        bool resultadoemail = Neg_Informes.EnviarCorreoColillaPDF(asunto, email, ruta);
                    }
                    else
                    {
                        

                        // Esta es la ultima version 2025
                        try
                        {
                            using (Stream pdfStream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
                            {
                                pdfStream.Seek(0, SeekOrigin.Begin);
                                Response.Clear();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("content-disposition", $"inline; filename=ComprobantePagoPeriodo{nperiodo}.pdf");
                                Response.Buffer = false;


                                // Copiar el contenido directamente a la respuesta
                                pdfStream.CopyTo(Response.OutputStream);

                                // Limpia recursos y cierra conexión
                                Response.Flush();
                                Response.SuppressContent = true;
                                HttpContext.Current.ApplicationInstance.CompleteRequest();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Manejo de errores: registra el error o muestra un mensaje adecuado
                            // Por ejemplo: LogError(ex);
                        }
                        finally
                        {
                            // Cerrar y liberar recursos del objeto rd
                            if (rd != null)
                            {
                                rd.Close();
                                rd.Dispose();
                            }
                        }
                       
                    }

                    
                }

            }
            catch (SqlException ex)
            {
                //logger.Error(ex, "Ocurrió un error en GenerarComprobantePeriodoCR");
                //string errorMessage = "Error al conectar a la base de datos: " + HttpUtility.JavaScriptStringEncode(ex.Message) +
                //                      " \\n Stack Trace: " + HttpUtility.JavaScriptStringEncode(ex.StackTrace) +
                //                      " \\n Línea: " + ex.LineNumber; // Asegúrate de que ex.LineNumber esté disponible
                //ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", $"mostrarMensajeEnConsola('{errorMessage}');", true);
                throw;
            }
            catch (Exception ex)
            {
                //logger.Error(ex, "Ocurrió un error en GenerarComprobantePeriodoCR");
                //// Aquí puedes agregar información adicional si es necesario
                //string errorMessage = "Error al ejecutar el comando: " + HttpUtility.JavaScriptStringEncode(ex.Message) +
                //                      " \\n Stack Trace: " + HttpUtility.JavaScriptStringEncode(ex.StackTrace);
                //ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", $"mostrarMensajeEnConsola('{errorMessage}');", true);
                throw;
            }

            //{
            //    // Enviar mensaje de error a la consola de la página web
            //    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al conectar a la base de datos: " + ex.Message + "');", true);
            //}
            //catch (Exception ex)
            //{
            //    // Enviar mensaje de error a la consola de la página web
            //    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al ejecutar el comando: " + ex.Message + "');", true);
            //}
        }

        private DataSet GenerarDataSetSubReporte(DataSet reporte, string[] tablas)
        {
            var dataSet = new DataSet();
            foreach (var tabla in tablas)
            {
                dataSet.Tables.Add(reporte.Tables[tabla].Copy());
            }
            return dataSet;
        }

        public static DataSet FiltrarYConstruirDataSet(DataSet datasetOriginal, string codigo, DataTable periodos)
        {
            // Crear un nuevo DataSet para almacenar los DataTable filtrados
            DataSet nuevoDataSet = new DataSet();

            // Listar los nombres de los DataTable a procesar
            string[] nombresTablas = {
        "ObtenerEmpleadosPlanillaSP",
        "ObtenerEncComprobantePago",
        "ObtenerEstructuraComprobantePago",
        "ObtenerEstructuraComprobantePagoNewV2",
        "ViaticosS1",
        "ViaticosS2"
    };

            // Filtrar cada DataTable por el código y agregarlo al nuevo DataSet
            foreach (string nombreTabla in nombresTablas)
            {
                if (datasetOriginal.Tables.Contains(nombreTabla))
                {
                    // Obtener el DataTable original
                    DataTable originalTable = datasetOriginal.Tables[nombreTabla];

                    // Determinar el nombre de la columna a usar para el filtro
                    string nombreColumna = (nombreTabla == "ViaticosS1" || nombreTabla == "ViaticosS2")
                        ? "codigo_empleado"
                        : "codigo";

                    // Determinar si la columna es de tipo string o int
                    Type tipoCodigo = originalTable.Columns[nombreColumna].DataType;

                    // Filtrar usando LINQ
                    var filasFiltradas = originalTable.AsEnumerable()
                        .Where(row => tipoCodigo == typeof(string)
                            ? row.Field<string>(nombreColumna) == codigo
                            : row.Field<int>(nombreColumna).ToString() == codigo);

                    // Crear un DataTable con las filas filtradas
                    DataTable tablaFiltrada = filasFiltradas.Any() ? filasFiltradas.CopyToDataTable() : originalTable.Clone();

                    // Agregar el DataTable filtrado al nuevo DataSet
                    tablaFiltrada.TableName = nombreTabla; // Mantener el nombre original
                    nuevoDataSet.Tables.Add(tablaFiltrada);
                }
            }


            // Agregar el DataTable adicional "periodos"
            if (periodos != null)
            {
                periodos.TableName = "periodos";
                nuevoDataSet.Tables.Add(periodos.Copy());
            }

            return nuevoDataSet;
        }


        // version no 2

        //public void GenerarComprobantePeriodoCRV2(string nperiodo, int periodo2, int semana2, int tipoplanilla, int tperiodo, int filtroemail, string encabezado, bool periodoConsolida, bool AllEmpleados, string codigo, string email, DataTable empleados)
        //{


        //    try
        //    {

                
        //        Neg_Periodo NPeriodo = new Neg_Periodo();
        //        dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(nperiodo));
        //        DateTime ini = dtPeriodo[0].fechaini;
        //        DateTime fin = dtPeriodo[0].fechafin;

        //        DateTime ini2 = dtPeriodo[0].fechaini2;
        //        DateTime fin2 = dtPeriodo[0].fechafin2;
        //        int Semana1 = 0;
        //        string vmSubtituloS2 = "";
        //        var vmSubtitulo = "Colilla de Pago del Periodo " + nperiodo + ", Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin2.Date.ToString("d/MM/yyyy");
        //        vmSubtituloS2 = vmSubtitulo;
        //        if (dtPeriodo.Rows.Count == 0)
        //        {
        //            //logger.Error("No se encontraron datos para el periodo especificado.");
        //            throw new Exception("No se encontraron datos para el periodo especificado.");
        //        }

        //        if (periodoConsolida == true)
        //        {
        //            if (Convert.ToInt32(nperiodo) != periodo2)
        //            {
        //                ini = dtPeriodo[0].fechaini;
        //                fin = dtPeriodo[0].fechafin2;

        //                dsPlanilla.dtPeriodoDataTable dtPeriodo2 = NPeriodo.PeriodoSel(periodo2);
        //                ini2 = dtPeriodo[0].fechaini;
        //                fin2 = dtPeriodo[0].fechafin2;

        //                vmSubtitulo = "Colilla de Pago del Periodo " + nperiodo + " Al " + periodo2 + " Fechas : Del " + ini.Date.ToString("d/MM/yyyy") +  " Al " + fin2.Date.ToString("d/MM/yyyy");

                          
        //                //vmSubtitulo = "Colilla de Pago del Periodo " + nperiodo + ", Del " + ini.Date.ToString("d/MM/yyyy") + " Al " + fin.Date.ToString("d/MM/yyyy");
        //                //vmSubtituloS2 = "Colilla de Pago del Periodo " + periodo2 + ", Del " + ini2.Date.ToString("d/MM/yyyy") + " Al " + fin2.Date.ToString("d/MM/yyyy");
        //            }
        //            Semana1 = 1;
        //            semana2 = 1;
        //        }
        //        else
        //        {
        //            periodo2 = Convert.ToInt32(nperiodo);
        //        }

        //        string CodEmpleado = TxtCodigoE.Text.Trim();
        //        if (filtroemail == 3)
        //        {
        //            CodEmpleado = codigo;
        //        }

        //        // Ruta temporal para archivos individuales
        //        string carpetaTemporal = Server.MapPath("~/Trash/Temp/");
        //        if (!Directory.Exists(carpetaTemporal))
        //            Directory.CreateDirectory(carpetaTemporal);

        //        List<string> archivosGenerados = new List<string>();


        //        // Generar el DataSet con los datos
        //        //DataSet nuevoDataSet  = GenerarPDF(nperiodo, periodo2.ToString(), ini, fin2, ddlProceso.SelectedValue.Trim(), CodEmpleado, ChkAll.Checked, ChkEfectivo.Checked, filtroemail, ChkConsolida.Checked, Semana1, semana2, 1, vmSubtitulo, vmSubtituloS2, AllEmpleados);

        //        // Recorrer cada empleado en el DataTable
        //        foreach (DataRow empleado in empleados.Rows)
        //        {
        //            CodEmpleado = empleado["Codigo"].ToString();

        //            //DataTable tablaPeriodos = nuevoDataSet.Tables["periodos"];

        //            // filtramos datos por empleado
        //            //DataSet Reporte  = FiltrarYConstruirDataSet(nuevoDataSet, CodEmpleado, tablaPeriodos);

        //            DataSet Reporte = GenerarPDF(nperiodo, periodo2.ToString(), ini, fin2, ddlProceso.SelectedValue.Trim(), CodEmpleado, ChkAll.Checked, ChkEfectivo.Checked, filtroemail, ChkConsolida.Checked, Semana1, semana2, 1, vmSubtitulo, vmSubtituloS2, AllEmpleados);

        //            // Configuración del reporte
        //            ReportDocument rd = new ReportDocument();
        //            rd.Load(Server.MapPath("~/Reportes/ComprobantePago.rpt"));
        //            rd.SetDataSource(Reporte);

        //            // Acceso al subreporte ingresos
        //            ReportDocument subReport = rd.OpenSubreport("ColillaConceptos1.rpt");
        //            subReport.SetDataSource(Reporte);

        //            // Acceso al subreporte Egresos
        //            ReportDocument subReportE = rd.OpenSubreport("ColillaConceptos2.rpt");
        //            subReportE.SetDataSource(Reporte);


        //            ReportDocument subReport2 = rd.OpenSubreport("ColumnViaticos01.rpt");
        //            subReport2.SetDataSource(Reporte);

        //            ReportDocument subReport3 = rd.OpenSubreport("ColumnViaticos02.rpt");
        //            subReport3.SetDataSource(Reporte);

        //            // Asignar parámetros
        //            rd.SetParameterValue("titEmpresa", "KAIZEN");
        //            rd.SetParameterValue("FechaVacaciones", fin2.ToShortDateString());
        //            rd.SetParameterValue("titPeriodo", vmSubtitulo);

        //            Response.Buffer = false;
        //            Response.ClearContent();
        //            Response.ClearHeaders();

        //            string ruta = Server.MapPath("~/Trash/ColillaPago_Periodo_" + nperiodo + "_Empleado_" + CodEmpleado + ".pdf");

        //            // Exportar el archivo individual
        //            string rutaArchivoEmpleado = Path.Combine(carpetaTemporal, $"Colilla_{CodEmpleado}.pdf");
        //            rd.ExportToDisk(ExportFormatType.PortableDocFormat, rutaArchivoEmpleado);
        //            archivosGenerados.Add(rutaArchivoEmpleado);

        //            // Liberar recursos
        //            rd.Close();
        //            rd.Dispose();

        //            //if (CodEmpleado == "871992")
        //            //{
        //            //    break;
        //            //}
        //        }

        //        string rutaSalida = Server.MapPath("~/Trash/ColillaPago_Periodo_" + nperiodo + ".pdf");
        //        // Combinar todos los PDFs en uno solo
        //        CombinarPDFs(archivosGenerados, rutaSalida);

        //        // Limpiar archivos temporales
        //        foreach (string archivo in archivosGenerados)
        //        {
        //            if (File.Exists(archivo)) File.Delete(archivo);
        //        }

        //        // Opcional: mostrar el archivo combinado al usuario
        //        Response.Clear();
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", $"inline; filename=ColillasPeriodo_{nperiodo}.pdf");
        //        Response.WriteFile(rutaSalida);
        //        Response.Flush();
        //        Response.End();


        //        //if (filtroemail == 3)
        //        //{
        //        //    rd.ExportToDisk(ExportFormatType.PortableDocFormat, ruta);

        //        //    // Cerrar y liberar recursos del ReportDocument
        //        //    rd.Close();
        //        //    rd.Dispose();
        //        //    string asunto = vmSubtitulo;
        //        //    bool resultadoemail = Neg_Informes.EnviarCorreoColillaPDF(asunto, email, ruta);
        //        //}
        //        //else
        //        //{


        //        //    using (Stream pdfStream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
        //        //    {
        //        //        Response.Clear();
        //        //        Response.ContentType = "application/pdf";
        //        //        Response.AddHeader("content-disposition", $"inline; filename=ComprobantePagoPeriodo{nperiodo}.pdf");
        //        //        Response.Buffer = true; // Asegura que todo el contenido se procese antes de enviarlo.

        //        //        // Copiar el contenido directamente a la respuesta
        //        //        pdfStream.CopyTo(Response.OutputStream);

        //        //        // Limpia recursos y cierra conexión
        //        //        Response.Flush();
        //        //        Response.SuppressContent = true; // Impide procesar más contenido en la respuesta.
        //        //        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Finaliza la solicitud correctamente.
        //        //    }
        //        //    rd.Close();
        //        //    rd.Dispose();



        //        //}


        //    }
        //    catch (SqlException ex)
        //    {
        //        //logger.Error(ex, "Ocurrió un error en GenerarComprobantePeriodoCR");
        //        //string errorMessage = "Error al conectar a la base de datos: " + HttpUtility.JavaScriptStringEncode(ex.Message) +
        //        //                      " \\n Stack Trace: " + HttpUtility.JavaScriptStringEncode(ex.StackTrace) +
        //        //                      " \\n Línea: " + ex.LineNumber; // Asegúrate de que ex.LineNumber esté disponible
        //        //ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", $"mostrarMensajeEnConsola('{errorMessage}');", true);
        //        throw;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logger.Error(ex, "Ocurrió un error en GenerarComprobantePeriodoCR");
        //        //// Aquí puedes agregar información adicional si es necesario
        //        //string errorMessage = "Error al ejecutar el comando: " + HttpUtility.JavaScriptStringEncode(ex.Message) +
        //        //                      " \\n Stack Trace: " + HttpUtility.JavaScriptStringEncode(ex.StackTrace);
        //        //ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", $"mostrarMensajeEnConsola('{errorMessage}');", true);
        //        throw;
        //    }

        //    //{
        //    //    // Enviar mensaje de error a la consola de la página web
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al conectar a la base de datos: " + ex.Message + "');", true);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    // Enviar mensaje de error a la consola de la página web
        //    //    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al ejecutar el comando: " + ex.Message + "');", true);
        //    //}
        //}

        // version mejorada
        public void GenerarColillasCombinadas(string nperiodo, DataTable empleados, string rutaSalida)
        {
            try
            {
                // Ruta temporal para archivos individuales
                string carpetaTemporal = Server.MapPath("~/Trash/Temp/");
                if (!Directory.Exists(carpetaTemporal))
                    Directory.CreateDirectory(carpetaTemporal);

                List<string> archivosGenerados = new List<string>();

                // Recorrer cada empleado en el DataTable
                foreach (DataRow empleado in empleados.Rows)
                {
                    string codEmpleado = empleado["CodEmpleado"].ToString();

                    // Generar dataset específico para el empleado
                    DataSet reporteEmpleado = GenerarDatasetPorEmpleado(codEmpleado, nperiodo);

                    // Configurar Crystal Report
                    ReportDocument rd = new ReportDocument();
                    rd.Load(Server.MapPath("~/Reportes/ComprobantePago.rpt"));
                    rd.SetDataSource(reporteEmpleado);

                    // Asignar parámetros específicos
                    rd.SetParameterValue("titEmpresa", "KAIZEN");
                    rd.SetParameterValue("titPeriodo", "Colilla de Pago del Periodo " + nperiodo);

                    // Exportar el archivo individual
                    string rutaArchivoEmpleado = Path.Combine(carpetaTemporal, $"Colilla_{codEmpleado}.pdf");
                    rd.ExportToDisk(ExportFormatType.PortableDocFormat, rutaArchivoEmpleado);
                    archivosGenerados.Add(rutaArchivoEmpleado);

                    // Liberar recursos
                    rd.Close();
                    rd.Dispose();
                }

                // Combinar todos los PDFs en uno solo
                CombinarPDFs(archivosGenerados, rutaSalida);

                // Limpiar archivos temporales
                foreach (string archivo in archivosGenerados)
                {
                    if (File.Exists(archivo)) File.Delete(archivo);
                }

                // Opcional: mostrar el archivo combinado al usuario
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", $"inline; filename=ColillasPeriodo_{nperiodo}.pdf");
                Response.WriteFile(rutaSalida);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                // Manejar errores
                throw new Exception("Error al generar las colillas combinadas: " + ex.Message, ex);
            }
        }

        private void CombinarPDFs(List<string> archivosPDF, string rutaSalida)
        {
            using (FileStream streamSalida = new FileStream(rutaSalida, FileMode.Create))
            using (Document documento = new Document())
            using (PdfCopy copista = new PdfCopy(documento, streamSalida))
            {
                documento.Open();
                foreach (string archivo in archivosPDF)
                {
                    using (PdfReader lector = new PdfReader(archivo))
                    {
                        for (int i = 1; i <= lector.NumberOfPages; i++)
                        {
                            PdfImportedPage pagina = copista.GetImportedPage(lector, i);
                            copista.AddPage(pagina);
                        }
                    }
                }
            }
        }

        private DataSet GenerarDatasetPorEmpleado(string codEmpleado, string nperiodo)
        {
            // Implementa aquí la lógica para generar el DataSet específico de cada empleado.
            // Por ejemplo, filtrar datos del DataTable original según el código del empleado.
            DataSet datasetEmpleado = new DataSet();
            // ...
            return datasetEmpleado;
        }

        // end version mejorada

        static XmlSchema CargarEsquema(string ruta)
        {
            try
            {
                // Verificar si el archivo existe
                if (File.Exists(ruta))
                {
                    // Cargar el esquema XSD desde el archivo
                    using (var reader = new StreamReader(ruta))
                    {
                        return XmlSchema.Read(reader, null);
                    }
                }
                else
                {
                    Console.WriteLine($"El archivo no se encontró en la ruta: {ruta}");
                    return null; // Retornar null si no se encontró el archivo
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar el esquema: {ex.Message}");
                return null; // Retornar null en caso de error
            }
        }
        private DataSet GenerarPDF(string periodo1, string periodo2, DateTime fechaini, DateTime fechafin,string depto, string codigo,  bool tarjeta, bool efectivo, int filtroemail, bool consolida, int semana1 , int semana2, int idEmpresa, string vmSubtitulo, string vmSubtituloS2, bool AllEmpleados, bool Excel, DataTable dtEmpleados)
        {
            Neg_Periodo NPeriodo = new Neg_Periodo();
            Neg_Informes dviaticosS2 = new Neg_Informes();
            dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(periodo1));
            int vmTipoPlanilla = dtPeriodo[0].tplanilla;
             
            if (!String.IsNullOrEmpty(codigo))
            {
                depto = "";
                Console.WriteLine("codigo tiene contenido.");
            }
            if (AllEmpleados)
            {
                depto = "";
                codigo = "";
            }

            if (!consolida)
            {
                semana1 = 1;
            }
            string strSqlCommand;
            DataSet resultado = new DataSet();
            DataSet resultadoFiltrado = new DataSet();
            string connectionString = ConfigurationManager.ConnectionStrings["BDRRHHConnectionString"].ConnectionString;
            DataTable tablaEmpleados;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Estado de Cuenta
                strSqlCommand = @"[dbo].[ObtenerEmpleadosPlanillaSP]";
                using (SqlCommand command = new SqlCommand(strSqlCommand, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros Maestro de Empleados
                    command.Parameters.AddWithValue("@periodo", periodo1);
                    command.Parameters.AddWithValue("@periodo2", periodo2);
                    command.Parameters.AddWithValue("@fechaini", fechaini);
                    command.Parameters.AddWithValue("@fechafin", fechafin);
                    command.Parameters.AddWithValue("@depto", depto ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@codigo", codigo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@all", tarjeta);
                    command.Parameters.AddWithValue("@efectivo", efectivo);
                    command.Parameters.AddWithValue("@filtroemail", filtroemail);
                    command.Parameters.AddWithValue("@periodoConsolida", consolida);
                    command.Parameters.AddWithValue("@AllEmpleados", AllEmpleados);
                    

                    try
                    {
                        
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                          


                            adapter.Fill(resultado, "ObtenerEmpleadosPlanillaSP");


                            if (resultado.Tables.Contains("ObtenerEmpleadosPlanillaSP1"))
                            {
                                resultado.Tables.Remove("ObtenerEmpleadosPlanillaSP1");
                            }
                 
                            // Periodo base
                            //dtEmpleadospln.Rows[i]["periodo"].ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al conectar a la base de datos: " + ex.Message + "');", true);
                        Console.WriteLine("Error al conectar a la base de datos: " + ex.Message); // Manejar el error de conexión
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al ejecutar el comando: " + ex.Message + "');", true);
                        Console.WriteLine("Error al ejecutar el comando: " + ex.Message);
                        // Manejar otros errores 
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open) connection.Close();
                    }
                }

                // Encabezado Comprobante de Pago
                //strSqlCommand = @"[dbo].[ObtenerEncComprobantePagoNewV2]";
                //using (SqlCommand command = new SqlCommand(strSqlCommand, connection))
                //{
                //    command.CommandType = CommandType.StoredProcedure;
                //    SqlParameter p1 = new SqlParameter("@periodoini", SqlDbType.NChar);
                //    p1.Value = periodo1;
                //    command.Parameters.Add(p1);
                //    SqlParameter p2 = new SqlParameter("@periodofin", SqlDbType.NChar);
                //    p2.Value = periodo2;
                //    command.Parameters.Add(p2);

                //    SqlParameter p3 = new SqlParameter("@codigo", SqlDbType.Int);                    
                //    // Asignar a p3.Value el valor convertido o cero si 'codigo' está vacío
                //    p3.Value = string.IsNullOrEmpty(codigo) ? 0 : Convert.ToInt32(codigo);
                //    command.Parameters.Add(p3);

                //    SqlParameter p4 = new SqlParameter("@semana", SqlDbType.Int);
                //    p4.Value = semana1;
                //    command.Parameters.Add(p4);
                //    //SqlParameter p5 = new SqlParameter("@semana2", SqlDbType.Int);
                //    //p5.Value = semana1;
                //    //command.Parameters.Add(p5);


                //    try
                //    {
                //        connection.Open();
                //        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                //        {
                //            adapter.Fill(resultado, "ObtenerEncComprobantePago");


                //        }
                //    }
                //    catch (SqlException ex)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al conectar a la base de datos: " + ex.Message + "');", true);
                //        Console.WriteLine("Error al conectar a la base de datos: " + ex.Message); // Manejar el error de conexión
                //    }
                //    catch (Exception ex)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al ejecutar el comando: " + ex.Message + "');", true);
                //        Console.WriteLine("Error al ejecutar el comando: " + ex.Message);
                //        // Manejar otros errores 
                //    }
                //    finally
                //    {
                //        if (connection.State == ConnectionState.Open) connection.Close();
                //    }
                //}



                // solo si es catorcenal
                //if (vmTipoPlanilla == 4)
                //{
                //    periodo2 = periodo1;
                //    semana2 = 1;
                //    vmTipoPlanilla = 4;
                //}


                // Encabezado Comprobante de Pago Solo Ingresos
                strSqlCommand = @"[dbo].[ObtenerEstructuraComprobantePagoNewV2]";
                using (SqlCommand command = new SqlCommand(strSqlCommand, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter p1 = new SqlParameter("@periodo1", SqlDbType.NChar);
                    p1.Value = periodo1;
                    command.Parameters.Add(p1);
                    SqlParameter p2 = new SqlParameter("@periodo2", SqlDbType.NChar);
                    p2.Value = periodo2;
                    command.Parameters.Add(p2);
                    SqlParameter p3 = new SqlParameter("@tipotrs", SqlDbType.Int);
                    p3.Value = 1; // tipo de concepto 1 = ingresos
                    command.Parameters.Add(p3);

                    //SqlParameter p4 = new SqlParameter("@codigo", SqlDbType.Int);
                    //// Asignar a p3.Value el valor convertido o cero si 'codigo' está vacío
                    //p4.Value = string.IsNullOrEmpty(codigo) ? 0 : Convert.ToInt32(codigo);
                    //command.Parameters.Add(p4);

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(resultado, "ObtenerEstructuraComprobantePago");


                        }
                    }
                    catch (SqlException ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al conectar a la base de datos: " + ex.Message + "');", true);
                        Console.WriteLine("Error al conectar a la base de datos: " + ex.Message); // Manejar el error de conexión
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al ejecutar el comando: " + ex.Message + "');", true);
                        Console.WriteLine("Error al ejecutar el comando: " + ex.Message);
                        // Manejar otros errores 
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open) connection.Close();
                    }
                }


                // Encabezado Comprobante de Pago Solo Egresos
                strSqlCommand = @"[dbo].[ObtenerEstructuraComprobantePagoNewV2]";
                using (SqlCommand command = new SqlCommand(strSqlCommand, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    SqlParameter p1 = new SqlParameter("@periodo1", SqlDbType.NChar);
                    p1.Value = periodo1;
                    command.Parameters.Add(p1);
                    SqlParameter p2 = new SqlParameter("@periodo2", SqlDbType.NChar);
                    p2.Value = periodo2;
                    command.Parameters.Add(p2);
                    SqlParameter p3 = new SqlParameter("@tipotrs", SqlDbType.Int);
                    p3.Value = 2; // tipo de concepto 2 = egresos
                    command.Parameters.Add(p3);

                    //SqlParameter p4 = new SqlParameter("@codigo", SqlDbType.Int);
                    //// Asignar a p3.Value el valor convertido o cero si 'codigo' está vacío
                    //p3.Value = string.IsNullOrEmpty(codigo) ? 0 : Convert.ToInt32(codigo);
                    //command.Parameters.Add(p4);

                    try
                    {
                        connection.Open();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(resultado, "ObtenerEstructuraComprobantePagoNewV2");


                        }
                    }
                    catch (SqlException ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al conectar a la base de datos: " + ex.Message + "');", true);
                        Console.WriteLine("Error al conectar a la base de datos: " + ex.Message); // Manejar el error de conexión
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "mostrarMensaje", "mostrarMensajeEnConsola('Error al ejecutar el comando: " + ex.Message + "');", true);
                        Console.WriteLine("Error al ejecutar el comando: " + ex.Message);
                        // Manejar otros errores 
                    }
                    finally
                    {
                        if (connection.State == ConnectionState.Open) connection.Close();
                    }
                }

            }

            // Crear la tabla "resultado" si no existe
            if (!resultado.Tables.Contains("Periodos"))
            {
                DataTable dtResultado = new DataTable("Periodos");
                dtResultado.Columns.Add("TitPeriodoS1", typeof(string));
                dtResultado.Columns.Add("TitPeriodoS2", typeof(string));
                dtResultado.Columns.Add("periodoConsolida", typeof(string));
                dtResultado.Columns.Add("TipoPlanilla", typeof(int));
                resultado.Tables.Add(dtResultado);
            }

            // Agregar una fila con los valores deseados
            DataRow nuevaFila = resultado.Tables["Periodos"].NewRow();
            nuevaFila["TitPeriodoS1"] = vmSubtitulo; // Asignar el valor de vmSubtitulo
            nuevaFila["TitPeriodoS2"] = vmSubtituloS2; // Asignar el valor de vmSubtituloS2
            string CadConsolidad = "0";
           if (consolida)
            {
                CadConsolidad = "1";
            }
            nuevaFila["periodoConsolida"] = CadConsolidad; // Asignar el valor de CadConsolidad
            nuevaFila["TipoPlanilla"] = vmTipoPlanilla; // Asignar el valor de vmTipoPlanilla


            resultado.Tables["Periodos"].Rows.Add(nuevaFila);




            DataSet ds = null;
            ds = Neg_Informes.CargarIngresoPeriodoIBruto(Convert.ToInt32(periodo1), Convert.ToInt32(periodo2), 0);

            //DataRow[] existeviaticopS1 = null;

            DataTable viaticopersonaS1 = new DataTable();
            //bool existeVS1 = false;

            // Agrega datos de Vacaciones a Maestro de empleados
            // Asegúrate de que resultado.Tables["ObtenerEmpleadosPlanillaSP"] y Datos.Rows[0]["saldovacaciones"] existan
            if (resultado.Tables.Contains("ObtenerEmpleadosPlanillaSP") && resultado.Tables["ObtenerEmpleadosPlanillaSP"].Rows.Count > 0)
            {

                //Neg_Liquidacion liquidacion = new Neg_Liquidacion();

               tablaEmpleados = resultado.Tables["ObtenerEmpleadosPlanillaSP"];

                foreach (DataRow fila in tablaEmpleados.Rows)
                {
                    // Validación de código
                    int codigoE = 0;
                    if (fila["codigo"] != DBNull.Value && int.TryParse(fila["codigo"].ToString(), out int parsedCodigo))
                    {
                        codigoE = parsedCodigo;
                    }
                    else
                    {
                        Console.WriteLine("Error: Código inválido en la fila.");
                        continue; // Saltar esta fila
                    }

                    // Validación de fecha_ingreso
                    DateTime fechaIngreso;
                    if (fila["fecha_ingreso"] != DBNull.Value && DateTime.TryParse(fila["fecha_ingreso"].ToString(), out DateTime parsedFecha))
                    {
                        fechaIngreso = parsedFecha;
                    }
                    else
                    {
                        Console.WriteLine("Error: Fecha de ingreso inválida en la fila.");
                        continue; // Saltar esta fila
                    }

                    dsPlanilla.dtPeriodoDataTable dtPeriodo2Vac = NPeriodo.PeriodoSel(Convert.ToInt32(periodo2));
                    DateTime ini1vac;
                    DateTime fin2vac ;
                    fin2vac = fechafin;
                    int ruptura = 0;
                    if (consolida)
                    {
                        
                        if (codigoE == 871098)
                        {
                            ruptura = 2;
                        }

                        ini1vac = dtPeriodo2Vac[0].fechaini;
                        fin2vac = dtPeriodo2Vac[0].fechafin2;
                    }

                    // Llamada al método
                    DataTable Datos = liquidacion.CalcularDiasVacacionesV14nal(codigoE, fechaIngreso, fin2vac, 1, 1);

                    string saldoVacaciones = string.Empty;

                    // Verificamos que Datos tenga al menos una fila y que "saldovacaciones" esté disponible
                    if (Datos != null && Datos.Rows.Count > 0 && Datos.Columns.Contains("saldovacaciones"))
                    {
                        saldoVacaciones = Datos.Rows[0]["saldovacaciones"].ToString();
                    }
                    else
                    {
                        Console.WriteLine("No se encontró información de saldo de vacaciones en Datos.");
                    }

                    // Validamos que la columna "Vacaciones" exista en la tabla antes de asignar el valor
                    if (tablaEmpleados.Columns.Contains("SldoVacaciones"))
                    {
                        fila["SldoVacaciones"] = saldoVacaciones;
                    }
                    else
                    {
                        Console.WriteLine("La columna 'Vacaciones' no existe en la tabla. Por favor, agrégala antes de usarla.");
                    }

                    // use SP Encabezados para conceptos resumen al pie de la colilla

                    if (resultado.Tables.Contains("ObtenerEncComprobantePago") && resultado.Tables["ObtenerEncComprobantePago"].Rows.Count > 0)
                    {
                        DataTable tablaEmpleadosEnc = resultado.Tables["ObtenerEncComprobantePago"];

                        // filtramos datos del empleado
                        var viaticopersonaS11 = (from c in tablaEmpleadosEnc.AsEnumerable().AsQueryable()
                                            where c.Field<string>("codigo") == codigoE.ToString()
                                            select c).ToList();

                        decimal vmMontoPrestamo = 0;
                        decimal vmSaldoPrestamo = 0;
                        decimal vmAhorro = 0;
                        decimal vmProteccion = 0;
                        decimal vmBono = 0;
                        decimal vmHrsBV = 0;
                        decimal vmIngresoEspecial = 0;
                        decimal vmOtros = 0;

                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            vmProteccion = ds.Tables[0].Select($"[codigo]='{codigoE.ToString().Trim()}' and [tipoIngrDeduc]<>'{25}'").Sum((DataRow c) => Convert.ToDecimal(c["valor"]));
                            vmBono = ds.Tables[0].Select($"[codigo]='{codigoE.ToString().Trim()}' and [tipoIngrDeduc]='{25}'").Sum((DataRow c) => Convert.ToDecimal(c["valor"]));
                            vmIngresoEspecial = ((vmProteccion == 0m) ? vmOtros : vmProteccion);
                        }
                        decimal valorRedondeado = 0 ;
                        if (viaticopersonaS11.Count() > 0)
                        {
                            // Acceder al primer (y único) registro y obtener el valor de 'totalapagar'
                            vmMontoPrestamo = viaticopersonaS11.First().Field<decimal?>("TotalPagar") ?? 0; // Asigna 0 si es nulo
                            vmSaldoPrestamo = viaticopersonaS11.First().Field<decimal?>("Saldo") ?? 0; // Asigna 0 si es nulo
                            vmAhorro = viaticopersonaS11.First().Field<decimal?>("ahorro") ?? 0; // Asigna 0 si es nulo
                            vmHrsBV = viaticopersonaS11.First().Field<decimal?>("horasbv") ?? 0; // Asigna 0 si es nulo
                            vmBono  = viaticopersonaS11.First().Field<decimal?>("bonovariable") ?? 0; // Asigna 0 si es nulo;
                            //valorRedondeado = (int)Math.Ceiling(vmBono); // Redondear 
                            valorRedondeado = Math.Round(vmBono, 2); // Redondea a un decimal

                            //if (codigoE == 873590)
                            //{
                            //    ruptura = 2;
                            //}
                        }

                        // actualiza maestro de empleados para resumen al pie de colilla
                        fila["TotalPagar"] = vmMontoPrestamo;
                        fila["Saldo"] = vmSaldoPrestamo;
                        fila["Ahorro"] = 0.00; // vmAhorro;
                        fila["Proteccion"] = vmIngresoEspecial;
                        fila["Bono"] = valorRedondeado;
                        fila["horasbv"] = vmHrsBV;
                       

                    }
                }
            }
            else
            {
                Console.WriteLine("La tabla 'ObtenerEmpleadosPlanillaSP' no contiene filas.");
            }




            // se llenan columnas de viaticos semana 1 y 2                               
            
            DateTime ini = dtPeriodo[0].fechaini;
            DateTime fin = dtPeriodo[0].fechafin2;

            tablaEmpleados = resultado.Tables["ObtenerEmpleadosPlanillaSP"];

            DataTable ViaticosS1 = Neg_Informes.ObtenerPersonalPagoViatico(int.Parse(periodo1), 1, ini, fin, 1, 0, "", tablaEmpleados);
            DataTable ViaticosS2;

            dsPlanilla.dtPeriodoDataTable dtPeriodo2 = NPeriodo.PeriodoSel(Convert.ToInt32(periodo2));

            DateTime ini2 = dtPeriodo2[0].fechaini;
            DateTime fin2 = dtPeriodo2[0].fechafin2;

            if (ViaticosS1 != null && ViaticosS1.Rows.Count > 0)
            {
                // Agregar los DataTables al DataSet
                ViaticosS1.TableName = "ViaticosS1"; // Asigna el nombre deseado
                resultado.Tables.Add(ViaticosS1.Copy()); // Copia para evitar problemas de referencia
            }
            else
            {
                // Crear el DataTable
                DataTable viaticosS1Table = new DataTable("ViaticosS1");

                // Definir las columnas
                viaticosS1Table.Columns.Add("Codigo_Empleado", typeof(int));
                viaticosS1Table.Columns.Add("NombreCompleto", typeof(string));
                viaticosS1Table.Columns.Add("Nombre_Depto", typeof(string));
                viaticosS1Table.Columns.Add("Fecha", typeof(DateTime));
                viaticosS1Table.Columns.Add("Viatico_Total", typeof(decimal));
                viaticosS1Table.Columns.Add("Total", typeof(decimal));
                viaticosS1Table.Columns.Add("Saldo", typeof(decimal));
                viaticosS1Table.Columns.Add("Desayuno", typeof(decimal));
                viaticosS1Table.Columns.Add("Almuerzo", typeof(decimal));
                viaticosS1Table.Columns.Add("Refrigerio", typeof(decimal));
                viaticosS1Table.Columns.Add("Cena", typeof(decimal));
                viaticosS1Table.Columns.Add("Transporte", typeof(decimal));
                viaticosS1Table.Columns.Add("Periodo", typeof(int));
                ViaticosS1.TableName = "ViaticosS1"; // Asigna el nombre deseado
                                                     // Agregar el DataTable vacío al DataSet
                                                     // Agregar filas al DataTable usando el método AddViaticosS1Row
                AddViaticosS1Row(viaticosS1Table, 12345, "Juan Pérez", "Ventas", DateTime.Now, 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 2024);

                //// simulador de viaticos 873463 emerson
                //AddViaticosS1Row(viaticosS1Table, 873463, "Emerson Mercado", "Modulo 07", DateTime.Now, 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 533);
                //AddViaticosS1Row(viaticosS1Table, 873463, "Emerson Mercado", "Modulo 07", DateTime.Now.AddDays(1), 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 533);
                //AddViaticosS1Row(viaticosS1Table, 873463, "Emerson Mercado", "Modulo 07", DateTime.Now.AddDays(2), 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 533);
                //AddViaticosS1Row(viaticosS1Table, 873463, "Emerson Mercado", "Modulo 07", DateTime.Now.AddDays(3), 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 533);
                //AddViaticosS1Row(viaticosS1Table, 873463, "Emerson Mercado", "Modulo 07", DateTime.Now.AddDays(4), 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 533);
                //AddViaticosS1Row(viaticosS1Table, 873463, "Emerson Mercado", "Modulo 07", DateTime.Now.AddDays(5), 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 533);
                //AddViaticosS1Row(viaticosS1Table, 873463, "Emerson Mercado", "Modulo 07", DateTime.Now.AddDays(6), 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 533);


                resultado.Tables.Add(viaticosS1Table); // Agrega el DataTable vacío

            }

            if (consolida)
            {
                //periodo2 = periodo1;
                dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(periodo2));
                ini2 = dtPeriodo[0].fechaini;
                fin2 = dtPeriodo[0].fechafin2;

                ViaticosS2 = dviaticosS2.ObtenerPersonalPagoViatico(int.Parse(periodo2), 1, ini2, fin2, 1, 0, "", tablaEmpleados);
                if (ViaticosS2 != null && ViaticosS2.Rows.Count > 0)
                {
                    ViaticosS2.TableName = "ViaticosS2"; // Asigna el nombre deseado
                    resultado.Tables.Add(ViaticosS2.Copy()); // Copia para evitar problemas de referencia

                    //var clonedViaticosS2 = ViaticosS2.Clone(); // Clona solo el esquema
                    //clonedViaticosS2.TableName = "ViaticosS2"; // Asigna el nombre deseado
                    //resultado.Tables.Add(clonedViaticosS2);    // Agrega el DataTable vacío al DataSet
                }
                
                else
                {
                    // Crear el DataTable
                    DataTable viaticosS2Table = new DataTable("ViaticosS2");

                    // Definir las columnas
                    viaticosS2Table.Columns.Add("Codigo_Empleado", typeof(int));
                    viaticosS2Table.Columns.Add("NombreCompleto", typeof(string));
                    viaticosS2Table.Columns.Add("Nombre_Depto", typeof(string));
                    viaticosS2Table.Columns.Add("Fecha", typeof(DateTime));
                    viaticosS2Table.Columns.Add("Viatico_Total", typeof(decimal));
                    viaticosS2Table.Columns.Add("Total", typeof(decimal));
                    viaticosS2Table.Columns.Add("Saldo", typeof(decimal));
                    viaticosS2Table.Columns.Add("Desayuno", typeof(decimal));
                    viaticosS2Table.Columns.Add("Almuerzo", typeof(decimal));
                    viaticosS2Table.Columns.Add("Refrigerio", typeof(decimal));
                    viaticosS2Table.Columns.Add("Cena", typeof(decimal));
                    viaticosS2Table.Columns.Add("Transporte", typeof(decimal));
                    viaticosS2Table.Columns.Add("Periodo", typeof(int));
                    ViaticosS2.TableName = "ViaticosS2"; // Asigna el nombre deseado
                                                         // Agregar el DataTable vacío al DataSet
                                                         // Agregar filas al DataTable usando el método AddViaticosS1Row
                    AddViaticosS1Row(viaticosS2Table, 12345, "Juan Pérez", "Ventas", DateTime.Now, 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 2024);
                    resultado.Tables.Add(viaticosS2Table); // Agrega el DataTable vacío
                }
            }
            
            // solo si es catorcenal
            if (vmTipoPlanilla==1)
            {
                ViaticosS2 = dviaticosS2.ObtenerPersonalPagoViatico(int.Parse(periodo1), 2, ini2, fin2, 1, 0, "", tablaEmpleados);
                if (ViaticosS2 != null && ViaticosS2.Rows.Count > 0)
                {
                    // Agregar los DataTables al DataSet
                    ViaticosS2.TableName = "ViaticosS2"; // Asigna el nombre deseado

                    // Crear el DataTable
                    DataTable viaticosS1Table = new DataTable("ViaticosS2");

                   
                    resultado.Tables.Add(ViaticosS2.Copy()); // Copia para evitar problemas de referencia
                }
                else
                {
                    // Crear el DataTable
                    DataTable viaticosS2Table = new DataTable("ViaticosS2");

                    // Definir las columnas
                    viaticosS2Table.Columns.Add("Codigo_Empleado", typeof(int));
                    viaticosS2Table.Columns.Add("NombreCompleto", typeof(string));
                    viaticosS2Table.Columns.Add("Nombre_Depto", typeof(string));
                    viaticosS2Table.Columns.Add("Fecha", typeof(DateTime));
                    viaticosS2Table.Columns.Add("Viatico_Total", typeof(decimal));
                    viaticosS2Table.Columns.Add("Total", typeof(decimal));
                    viaticosS2Table.Columns.Add("Saldo", typeof(decimal));
                    viaticosS2Table.Columns.Add("Desayuno", typeof(decimal));
                    viaticosS2Table.Columns.Add("Almuerzo", typeof(decimal));
                    viaticosS2Table.Columns.Add("Refrigerio", typeof(decimal));
                    viaticosS2Table.Columns.Add("Cena", typeof(decimal));
                    viaticosS2Table.Columns.Add("Transporte", typeof(decimal));
                    viaticosS2Table.Columns.Add("Periodo", typeof(int));

                    ViaticosS2.TableName = "ViaticosS2"; // Asigna el nombre deseado

                    AddViaticosS1Row(viaticosS2Table, 12345, "Juan Pérez", "Ventas", DateTime.Now, 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 2024);

                    // Agregar el DataTable vacío al DataSet
                    resultado.Tables.Add(viaticosS2Table); // Agrega el DataTable vacío
                }
            }




            if (Excel)
            {
                // Verificar si resultadoFiltrado está inicializado
                if (resultadoFiltrado == null)
                    resultadoFiltrado = new DataSet();

                // Obtener los códigos de empleados desde dtEmpleados
                var codigosFiltrados = dtEmpleados.AsEnumerable()
                                                  .Select(row => row["codigo_empleado"].ToString())
                                                  .ToList();

                // Filtrar y agregar tablas al DataSet, asegurando que no haya errores si la tabla está vacía
                void AgregarTablaFiltrada(string tablaOrigen, string tablaDestino, string columna)
                {
                    if (resultado.Tables.Contains(tablaOrigen))
                    {
                        var filasFiltradas = resultado.Tables[tablaOrigen].AsEnumerable()
                                                .Where(row => codigosFiltrados.Contains(row[columna].ToString()));

                        if (filasFiltradas.Any()) // Evitar error si está vacío
                        {
                            var tablaFiltrada = filasFiltradas.CopyToDataTable();
                            tablaFiltrada.TableName = tablaDestino;
                            resultadoFiltrado.Tables.Add(tablaFiltrada);
                        }
                    }
                }

                // Llamar a la función para cada tabla
                AgregarTablaFiltrada("ObtenerEmpleadosPlanillaSP", "ObtenerEmpleadosPlanillaSP", "Codigo");
                AgregarTablaFiltrada("ObtenerEstructuraComprobantePago", "ObtenerEstructuraComprobantePago", "Codigo");
                AgregarTablaFiltrada("ObtenerEstructuraComprobantePagoNewV2", "ObtenerEstructuraComprobantePagoNewV2", "Codigo");
                AgregarTablaFiltrada("ViaticosS1", "ViaticosS1", "Codigo_Empleado");
                AgregarTablaFiltrada("ViaticosS2", "ViaticosS2", "Codigo_Empleado");


                // Agregar la tabla filtrada al DataSet
                if (resultado.Tables.Contains("Periodos")) // Verifica si la tabla existe
                {
                    // Clonar la tabla para evitar conflictos de referencia
                    DataTable periodosClonado = resultado.Tables["Periodos"].Copy();
                    periodosClonado.TableName = "Periodos"; // Asegurar que conserve el nombre

                    // Verificar si ya existe en resultadoFiltrado antes de agregarla
                    if (!resultadoFiltrado.Tables.Contains("Periodos"))
                    {
                        resultadoFiltrado.Tables.Add(periodosClonado);
                    }
                }
                if (!resultadoFiltrado.Tables.Contains("ViaticosS1") || resultadoFiltrado.Tables["ViaticosS1"].AsEnumerable().Count()==0) // Evitar error si está vacío
                {
                    // Crear el DataTable
                    DataTable viaticosS1Table = new DataTable("ViaticosS1");

                    // Definir las columnas
                    viaticosS1Table.Columns.Add("Codigo_Empleado", typeof(int));
                    viaticosS1Table.Columns.Add("NombreCompleto", typeof(string));
                    viaticosS1Table.Columns.Add("Nombre_Depto", typeof(string));
                    viaticosS1Table.Columns.Add("Fecha", typeof(DateTime));
                    viaticosS1Table.Columns.Add("Viatico_Total", typeof(decimal));
                    viaticosS1Table.Columns.Add("Total", typeof(decimal));
                    viaticosS1Table.Columns.Add("Saldo", typeof(decimal));
                    viaticosS1Table.Columns.Add("Desayuno", typeof(decimal));
                    viaticosS1Table.Columns.Add("Almuerzo", typeof(decimal));
                    viaticosS1Table.Columns.Add("Refrigerio", typeof(decimal));
                    viaticosS1Table.Columns.Add("Cena", typeof(decimal));
                    viaticosS1Table.Columns.Add("Transporte", typeof(decimal));
                    viaticosS1Table.Columns.Add("Periodo", typeof(int));
                    //ViaticosS1.TableName = "ViaticosS1"; // Asigna el nombre deseado
                                                         // Agregar el DataTable vacío al DataSet
                                                         // Agregar filas al DataTable usando el método AddViaticosS1Row
                    AddViaticosS1Row(viaticosS1Table, 12345, "Juan Pérez", "Ventas", DateTime.Now, 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 2024);




                    resultadoFiltrado.Tables.Add(viaticosS1Table); // Agrega el DataTable vacío
                }
                if (!resultadoFiltrado.Tables.Contains("ViaticosS2") || resultadoFiltrado.Tables["ViaticosS2"].AsEnumerable().Count() == 0) // Evitar error si está vacío
                {
                    // Crear el DataTable
                    DataTable viaticosS2Table = new DataTable("ViaticosS2");

                    // Definir las columnas
                    viaticosS2Table.Columns.Add("Codigo_Empleado", typeof(int));
                    viaticosS2Table.Columns.Add("NombreCompleto", typeof(string));
                    viaticosS2Table.Columns.Add("Nombre_Depto", typeof(string));
                    viaticosS2Table.Columns.Add("Fecha", typeof(DateTime));
                    viaticosS2Table.Columns.Add("Viatico_Total", typeof(decimal));
                    viaticosS2Table.Columns.Add("Total", typeof(decimal));
                    viaticosS2Table.Columns.Add("Saldo", typeof(decimal));
                    viaticosS2Table.Columns.Add("Desayuno", typeof(decimal));
                    viaticosS2Table.Columns.Add("Almuerzo", typeof(decimal));
                    viaticosS2Table.Columns.Add("Refrigerio", typeof(decimal));
                    viaticosS2Table.Columns.Add("Cena", typeof(decimal));
                    viaticosS2Table.Columns.Add("Transporte", typeof(decimal));
                    viaticosS2Table.Columns.Add("Periodo", typeof(int));
                    //ViaticosS2.TableName = "ViaticosS2"; // Asigna el nombre deseado
                                                         // Agregar el DataTable vacío al DataSet
                                                         // Agregar filas al DataTable usando el método AddViaticosS1Row
                    AddViaticosS1Row(viaticosS2Table, 12345, "Juan Pérez", "Ventas", DateTime.Now, 100.50m, 200.00m, 50.00m, 10.00m, 20.00m, 5.00m, 15.00m, 30.00m, 2024);
                    
                    resultadoFiltrado.Tables.Add(viaticosS2Table); // Agrega el DataTable vacío
                }
                return resultadoFiltrado;
            }
            // Generar el PDF a partir del DataTable resultado
            return resultado;

        }


        public static void AddViaticosS1Row(DataTable table, int codigo_empleado, string nombrecompleto, string nombre_depto, DateTime fecha,
                                           decimal viatico_total, decimal total, decimal saldo,
                                           decimal Desayuno, decimal Almuerzo,
                                           decimal Refrigerio, decimal Cena,
                                           decimal Transporte, int periodo)
        {
            // Crear una nueva fila
            DataRow row = table.NewRow();

            // Asignar valores a la fila
            row["Codigo_Empleado"] = codigo_empleado;
            row["NombreCompleto"] = nombrecompleto;
            row["Nombre_Depto"] = nombre_depto;
            row["Fecha"] = fecha;
            row["Viatico_Total"] = viatico_total;
            row["Total"] = total;
            row["Saldo"] = saldo;
            row["Desayuno"] = Desayuno;
            row["Almuerzo"] = Almuerzo;
            row["Refrigerio"] = Refrigerio;
            row["Cena"] = Cena;
            row["Transporte"] = Transporte;
            row["Periodo"] = periodo;

            // Agregar la fila al DataTable
            table.Rows.Add(row);
        }

        private void CrearPDF(string html)
        {
            try {
                string CodigoDocumento = Convert.ToString(System.Guid.NewGuid().ToString());
                string fileName = HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\Bolante" + CodigoDocumento.Trim() + ".pdf";
                //Ultimo
                Document doc = new Document(PageSize.LETTER, 15, 15, 0, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                doc.Open();
                iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                hw.Parse(new StringReader(html));
                doc.Close();
                writer.Close();
                ShowPdf(HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\Bolante" + CodigoDocumento.Trim() + ".pdf");
            }
            catch (Exception ex) {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }

        private void ShowPdf(string s)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + s);
            Response.ContentType = "application/pdf";
            Response.WriteFile(s);
            Response.Flush();
            Response.Clear();
        }
      
        private string Crear(int filtroemail)
        {
            //empleados pagados en planilla
            string html = "";
            try {
                if (TxtBuscar.Text.Trim()=="")
                {
                    throw new Exception("Debe especificar periodo");
                }
                int tplanilla = 0, semana2 = 2, periodo2= 0;//en caso d consolidar esta variable puede tener semana 1 de otro periodo
                DateTime fechaini = new DateTime();
                DateTime fechafin = new DateTime();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(TxtBuscar.Text.Trim()));
                tplanilla = dtPeriodo[0].tplanilla;
                periodo2 = Convert.ToInt32(TxtBuscar.Text.Trim());
                fechaini = dtPeriodo[0].fechaini;
                fechafin = dtPeriodo[0].fechafin2;
                //aqui hacer conversion periodo semanal a catorcenal
                string encabezado = TxtBuscar.Text.Trim();

                if (ChkConsolida.Checked)//reporte consolidad dos periodos
                {
                    if (!string.IsNullOrEmpty(txtPeriodo2.Text.Trim()))
                    {
                        encabezado += " - " + txtPeriodo2.Text.Trim();
                        if (!Neg_Informes.PlnPeriodosConsolidadosEqvIns(Convert.ToInt32(TxtBuscar.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim())))
                        {
                            throw new Exception("Error al consolidar periodo");
                        }
                        //if (!Neg_Informes.PlnConversionPeriodoConsolidado(Convert.ToInt32(TxtBuscar.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim()), 1))
                        //{
                        //    throw new Exception("Error al convertir periodo");
                        //}
                    }

                    if (dtPeriodo[0].tperiodo == 1 && dtPeriodo[0].tplanilla == 4)
                    {
                        //cuando se consolidan dos periodos semanales se debe dar tratamiento de catorcena, cambiar a tplanilla 4 consolidar 0
                        //select periodoconsolidado nueva tabla
                        //asignar a variables locales valores de la tabla
                        DataTable periodoConsolidado = Neg_Informes.PlnPeriodosConsolidadosEqvSel(int.Parse(TxtBuscar.Text.Trim()));
                        tplanilla = 1;
                        semana2 = 1;
                        periodo2 = Convert.ToInt32(txtPeriodo2.Text.Trim());
                        fechaini = Convert.ToDateTime(periodoConsolidado.Rows[0]["fechaini"]);
                        fechafin = Convert.ToDateTime(periodoConsolidado.Rows[0]["fechafin"]);
                    }

                }
                                               
                DataSet dsEmpleadospln = Neg_Informes.ObtenerEmpleadosPlanilla(TxtBuscar.Text.Trim(),txtPeriodo2.Text,fechaini,fechafin, ddlProceso.SelectedValue.Trim(), TxtCodigoE.Text.Trim(), ChkAll.Checked, ChkEfectivo.Checked,filtroemail,ChkConsolida.Checked, ChkAllEmpleados.Checked);
               
                if (dsEmpleadospln.Tables.Count > 0)
                {
                    DataTable dtEmpleadospln = dsEmpleadospln.Tables[0];
                    if (dtPeriodo[0].tperiodo == 3 || dtPeriodo[0].tperiodo == 4 || dtPeriodo[0].tperiodo == 5)//planilla de aguinaldo o vacaciones
                    {
                        html = Neg_Informes.GenerarComprobantePrestacionPdf(dtEmpleadospln, TxtBuscar.Text.Trim(), periodo2,1, tplanilla, dtPeriodo[0].tperiodo,filtroemail);
                    }
                    else
                    {
                        // Original
                        html = Neg_Informes.GenerarComprobantePeriodoPdf(dtEmpleadospln, TxtBuscar.Text.Trim(), periodo2, semana2, tplanilla, dtPeriodo[0].tperiodo, filtroemail, encabezado, ChkConsolida.Checked);
                    }

                  
                   
                }
                //if (ChkConsolida.Checked)//reporte consolidad dos periodos
                //{
                //    if (!string.IsNullOrEmpty(txtPeriodo2.Text.Trim()))
                //    {
                //        if (!Neg_Informes.PlnConversionPeriodoConsolidado(Convert.ToInt32(TxtBuscar.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim()), 2))
                //        {
                //            throw new Exception("Error al convertir periodo");
                //        }
                //    }

                //}
                return html;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
       
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try {
                if (TxtBuscar.Text.Trim() != "")
                {
                    int filtroeamil = ChkEmail.Checked ? 2 : 1;
                    // colillas de Pago - version crystal report
                    if (ChkColilla.Checked)
                    {
                        // Crystal Report
                        // Parametros Consolidar, Tarjeta, Efectivo, Excluir Correos
                        CrearColilla(filtroeamil);
                    }
                    else
                    {         
                        // Colilla HTML TO PDF
                        string html = Crear(filtroeamil);
                        CrearPDF(html);
                    }


                }
                else
                {
                    throw new Exception("Debe digitar un periodo");
                }
            }
            catch (Exception ex) {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ChkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            //if (!ChkEmail.Checked)
            //{
            //    ChkEfectivo.Checked = false;
            //}
            if (ChkEfectivo.Checked)
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                this.ChkAll.Checked = false;
            }
            else
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                ChkAll.Checked = false;
            }
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (!ChkEmail.Checked)
            //{
            //    ChkAll.Checked = false;
            //}
            if (ChkAll.Checked)
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                ChkEfectivo.Checked = false;
            }
            else
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                ChkEfectivo.Checked = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtBuscar.Text.Trim() != "")
                {
                    // TODO:VHPO 17/02/2024
                    if (ChkColilla.Checked)
                    {
                        // Crystal Report
                        // Parametros Consolidar, Tarjeta, Efectivo, Excluir Correos
                        CrearColilla(3); // correo
                                         // email colillas
                        //if (filtroemail == 3)
                        //{

                        //}
                    }
                    else
                    {
                      string html = Crear(3); //correo
                       
                    }
                    // end
                    //string html = Crear(3);
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Se han enviado las colillas de pago a todo el personal con correo";

                    
                }
                else
                {
                    throw new Exception("Debe digitar un periodo");
                }
            }
            catch (Exception ex)
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }
        protected void ChkConsolida_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConsolida.Checked)
            {
                divp.Visible = true;
            }
            else
            {
                divp.Visible = false;
            }

        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("codigo_empleado", typeof(int));
            if (file.HasFile)
            {
                string connectionString = "";
                string FileName = Path.GetFileName(file.PostedFile.FileName);
                string Extension = Path.GetExtension(file.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FileName);
                string fileLocation = HttpContext.Current.Server.MapPath(".").ToString() + @"\Trash\" + FileName;
                file.SaveAs(FilePath);

                if (Extension == ".xls")
                {

                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                }
                else if (Extension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                connectionString = String.Format(connectionString, FilePath);
                OleDbConnection connExcel = new OleDbConnection(connectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                cmdExcel.CommandType = System.Data.CommandType.Text;
                cmdExcel.Connection = connExcel;
                OleDbDataAdapter oda = new OleDbDataAdapter();
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 1)
                    {

                        if (dt.Columns[0].ToString().ToLower() == "codigo_empleado")
                        {
                            //foreach (DataRow item in dt.Rows)
                            //{
                            //    dt
                            //}
                            Session["datos"] = dt;
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "El Archivo esta preparado para procesar Empleados, de click en Imprimir";
                        }
                        else
                        {
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "El Archivo Excel no contiene Nombres de Columnas Requeridos";
                        }

                    }
                    else
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "El Archivo Excel no contiene las Columnas Requeridas";
                    }

                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "El Archivo Excel no contiene registros";
                }
            }

            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                file.Focus();
            }
        }

        protected void CheckExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckExcel.Checked)
            {
                //pnlModulo.Visible = false;
                //pnlCodigo.Visible = false;
                divfile.Visible = true;
                pnlExcel.Visible = true;
                //divEmp.Visible = false;
            }
            else
            {
                //pnlModulo.Visible = false;
                //pnlCodigo.Visible = false;
                divfile.Visible = false;
                pnlExcel.Visible = false;
                //divEmp.Visible = false;
            }
        }
    }
}