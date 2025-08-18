using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
//////
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.OleDb;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class ComprobanteReimprimir : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        #endregion
        DataTable dt = new DataTable();//se guarda lo cargado en excel
        DataTable dt2 = new DataTable();//se guardan los empleados con correcciones de planilla con los datos necesarios para comprobante

        string html = "", htmle = "", htmltmp = "";

        int d500 = 0, d200 = 0, d100 = 0, d50 = 0, d20 = 0, d10 = 0, d5 = 0, d1 = 0, d05 = 0, d025 = 0, d010 = 0, d005 = 0, d001 = 0;

        protected void ChkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkEfectivo.Checked)
            {
                
                this.ChkAll.Checked = false;
            }
            else
            {
               
                ChkAll.Checked = true;
            }
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAll.Checked)
            {
                
                ChkEfectivo.Checked = false;
            }
            else
            {
                
                ChkEfectivo.Checked = true;
            }
        }

        decimal ingresote = 0, egresote = 0, ingresosem = 0, egresosem = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              
            }
        }
       
        #region Pdf
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
        #endregion
        protected void btnCargar_Click(object sender, EventArgs e)
        {
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
                //DataTable dt = new DataTable();
                //cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
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

                        if (dt.Columns[0].ToString().ToLower() == "codigo")
                        {
                            Session["datos"] = dt;
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
        private string Crear(int tperiodo,int periodo2,int semana2,int tplanilla,string encabezado,int filtroemail)
        {
            //empleados pagados en planilla
            string html = "";
            try
            {
                DataTable dtEmpleadospln = new DataTable();
                if (dt2.Rows.Count>0)
                {
                    dtEmpleadospln = dt2.AsEnumerable().OrderBy(c => c.Field<string>("depto")).CopyToDataTable();
                }
                else
                {
                    dtEmpleadospln = dt2;
                }
               
                if (dtEmpleadospln.Rows.Count > 0)
                {
                    if (tperiodo == 3 || tperiodo == 4 || tperiodo == 5)//planilla de aguinaldo o vacaciones
                    {
                        html = Neg_Informes.GenerarComprobantePrestacionPdf(dtEmpleadospln, TxtBuscar.Text.Trim(), periodo2, 1, tplanilla, tperiodo, filtroemail);
                    }
                    else
                    {
                        html = Neg_Informes.GenerarComprobantePeriodoPdf(dtEmpleadospln, TxtBuscar.Text.Trim(), periodo2, semana2, tplanilla, tperiodo, filtroemail, encabezado,ChkConsolida.Checked);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void FiltrarLoteEmpleados(int filtroemail) {
            //tabla que se usara para construir comprobantes
            dt2.Columns.Add("empresa");
            dt2.Columns.Add("periodo");
            dt2.Columns.Add("fechaini");
            dt2.Columns.Add("fechafin");
            dt2.Columns.Add("codigo");
            dt2.Columns.Add("nombre");
            dt2.Columns.Add("cuenta");
            dt2.Columns.Add("depto");
            dt2.Columns.Add("numero_seguro");
            dt2.Columns.Add("cedula_identidad");
            dt2.Columns.Add("nombre_cargo");
            dt2.Columns.Add("fecha_ingreso");
            dt2.Columns.Add("email");

            try {
                if (TxtBuscar.Text.Trim() == "")
                {
                    throw new Exception("Debe especificar periodo");
                }
                int tplanilla = 0, semana2 = 2, periodo2 = 0, tperiodo=0;//en caso d consolidar esta variable puede tener semana 1 de otro periodo
                DateTime fechaini = new DateTime();
                DateTime fechafin = new DateTime();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(TxtBuscar.Text.Trim()));
                tplanilla = dtPeriodo[0].tplanilla;
                tperiodo = dtPeriodo[0].tperiodo;
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
                //TODO:VHPO revisar parametro allEmpleados
                //empleados pagados en planilla
                DataSet dsEmpleadospln = Neg_Informes.ObtenerEmpleadosPlanilla(TxtBuscar.Text.Trim(), txtPeriodo2.Text, fechaini, fechafin, "", "", ChkAll.Checked, ChkEfectivo.Checked,filtroemail, ChkConsolida.Checked, true);
                //empleados con correcciones cargadas en excel
                DataTable excel = (DataTable)Session["datos"];
                //coincidencia de empleado en ambas tablas anteriores
                DataRow[] temporal;

                if (dsEmpleadospln.Tables.Count > 0)
                {
                    if (dsEmpleadospln.Tables[0].Rows.Count > 0 && excel.Rows.Count > 0)
                    {
                        //recorrer empleados pagados en planilla y encontrar filas que coincidan con los cargados en excel
                        for (int i = 0; i < excel.Rows.Count; i++)
                        {
                            temporal = dsEmpleadospln.Tables[0].Select(string.Format("[codigo]='{0}'", excel.Rows[i]["codigo"].ToString()));

                            foreach (DataRow item in temporal)
                            {
                                dt2.Rows.Add(
                                    item["empresa"].ToString(),
                                    item["periodo"].ToString(),
                                    item["fechaini"].ToString(),
                                    item["fechafin"].ToString(),
                                    item["codigo"].ToString(),
                                    item["nombre"].ToString(),
                                    item["cuenta"].ToString(),
                                    item["depto"].ToString(),
                                    item["numero_seguro"].ToString(),
                                    item["cedula_identidad"].ToString(),
                                    item["nombre_cargo"].ToString(),
                                    item["fecha_ingreso"].ToString(),
                                    item["email"].ToString()
                                    );
                            }
                        }
                        string html = Crear(tperiodo,periodo2,semana2,tplanilla,encabezado,filtroemail);

                        if (filtroemail!=3)
                        {
                            CrearPDF(html);
                        }                                                  
                       
                    }
                    else
                    {
                        throw new Exception("El excel no contiene registros");
                      
                    }
                }
                else {
                
                    throw new Exception("No hay registros de planilla");
                  
                }
            }
            catch (Exception ex) {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
           
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtBuscar.Text.Trim() != "")
                {
                    int filtroemail = ChkEmail.Checked ? 2 : 1;
                    FiltrarLoteEmpleados(filtroemail);

                }
                else
                {
                    alertValida.Visible = true;
                    alertValida.InnerText = "Debe digitar un periodo";
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
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtBuscar.Text.Trim() != "")
                {
                    FiltrarLoteEmpleados(3);
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Se han enviado las colillas de pago a todo el personal con correo";
                }
                else
                {
                    alertValida.Visible = true;
                    alertValida.InnerText = "Debe digitar un periodo";
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

    }
}