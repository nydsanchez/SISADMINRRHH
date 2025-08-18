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
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using Negocios;
using Datos;
using Microsoft.Reporting.WebForms;

namespace NominaRRHH.Presentacion
{
    public partial class ComprobanteIncentivoDia : System.Web.UI.Page
    {

        #region REFERENCIAS
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_HojasPDF Neg_HojasPDF = new Neg_HojasPDF();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                CargarDll();
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //empleados pagados en planilla
            Neg_Informes Neg_Informes = new Neg_Informes();
            Neg_Periodo NPeriodo = new Neg_Periodo();
            string html = "";
            try
            {
                if (txtperiodo.Text.Trim() == "")
                {
                    throw new Exception("Debe especificar periodo");
                }
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(txtperiodo.Text.Trim()));
                if (rbllistImpresion.SelectedValue == "1")
                {
                    html = Neg_Informes.CrearHojaIncentivoHTML(int.Parse(txtperiodo.Text.Trim()),1, ddlProceso.SelectedItem.Text.Trim(),dtPeriodo[0].fechaini);
                    CrearPDF(html);
                }
                else
                {
                    bool all = rbllistImpresion.SelectedValue.Trim() == "4" ? true : false;
                    DataSet dsEmpleadospln = Neg_Informes.ObtenerEmpleadosPlanillaIncentivo(txtperiodo.Text.Trim(), ddlProceso.SelectedValue.Trim(), TxtCodigoE.Text.Trim(), all);

                    if (dsEmpleadospln.Tables.Count > 0)
                    {
                        DataTable dtEmpleadospln = dsEmpleadospln.Tables[0];

                        html = Neg_Informes.GenerarComprobantIncentivoPdf(dtEmpleadospln, Convert.ToInt32(txtperiodo.Text.Trim()), dtPeriodo[0].tplanilla, dtPeriodo[0].tperiodo);
                        CrearPDF(html);
                    }
                }

            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
            
        }
        
        protected void rbllistImpresion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtperiodo.Text != "")
            {
                btnAceptar.Visible = true;
                div2.Visible = false;
                if (rbllistImpresion.SelectedValue == "1")
                {
                    pnlModulo.Visible = true;
                    pnlCodigo.Visible = false;
                    //pnlExcel.Visible = false;
                    divEmp.Visible = false;

                }
                else if (rbllistImpresion.SelectedValue == "2")
                {
                    pnlModulo.Visible = false;
                    pnlCodigo.Visible = true;
                    divEmp.Visible = true;
                    div2.Visible = true;
                    //pnlExcel.Visible = false;

                }
                else if (rbllistImpresion.SelectedValue == "3")
                {
                    pnlModulo.Visible = false;
                    pnlCodigo.Visible = false;
                    //pnlExcel.Visible = true;
                    divEmp.Visible = false;
                }

                else if (rbllistImpresion.SelectedValue == "5")
                {
                    pnlModulo.Visible = false;
                    pnlCodigo.Visible = false;
                    //pnlExcel.Visible = false;
                    divEmp.Visible = false;
                }
                else
                {
                    pnlModulo.Visible = false;
                    pnlCodigo.Visible = false;
                    divEmp.Visible = false;
                }
            }
            else
            {
                btnAceptar.Visible = false;

            }
        }

        //public DataTable CargarExcel()
        //{
        //    ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
        //    FileUpload fileUpload = (FileUpload)cph.FindControl("file");
        //    DataTable dtExcelRecords = new DataTable();

        //    if (fileUpload.HasFile)
        //    {
        //        string connectionString = "";
        //        string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
        //        string fileExtension = Path.GetExtension(fileUpload.PostedFile.FileName);
        //        string fileLocation = HttpContext.Current.Server.MapPath("..").ToString() + @"\Trash\" + fileName;
        //        fileUpload.SaveAs(fileLocation);
        //        //  int totalcajas = 0;
        //        //Check whether file extension is xls or xslx
        //        if (fileExtension == ".xls")
        //        {
        //            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
        //        }
        //        else if (fileExtension == ".xlsx")
        //        {
        //            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //        }

        //        //Create OleDB Connection and OleDb Command
        //        OleDbConnection con = new OleDbConnection(connectionString);
        //        OleDbCommand cmd = new OleDbCommand();
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        cmd.Connection = con;

        //        OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);

        //        con.Open();
        //        DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

        //        string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
        //        cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
        //        dAdapter.SelectCommand = cmd;
        //        dAdapter.Fill(dtExcelRecords);
        //        con.Close();

        //    }

        //    return dtExcelRecords;
        //}

        public void CargarDll()
        {
            DataSet dt = Neg_Catalogos.CargarProcesos();
            DataTable dp = dt.Tables[0].AsEnumerable().Where(c=>c.Field<string>("nombre_depto").ToLower().IndexOf("modulo")>-1).CopyToDataTable();
            this.ddlProceso.DataSource = dp;//Neg_Incentivos.CosModulosSel();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }
        private void CrearPDF(string html)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("../Trash");           //se obtiene la ruta de la carpeta donde se almacenara el documento
                string[] ficheros = Directory.GetFiles(path);
                foreach (var item in ficheros)
                {

                    if (File.Exists(item))
                    {
                        try
                        {
                            File.Delete(item);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }

                string CodigoDocumento = Convert.ToString(System.Guid.NewGuid().ToString());
                string fileName = HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\ComprobanteIncentivo" + CodigoDocumento.Trim() + ".pdf";
                //Ultimo

                Document doc = new Document(PageSize.LETTER, 15, 15, 0, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                doc.Open();
                iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                hw.Parse(new StringReader(html));
                doc.Close();
                writer.Close();
                ShowPdf(HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\ComprobanteIncentivo" + CodigoDocumento.Trim() + ".pdf");
            }
            catch (Exception ex)
            {
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

        protected void btnDetEmp_Click(object sender, EventArgs e)
        {
            try
            {               
                //detalle de deducciones e ingresos de empleados por semana
                DataSet dsComprobante = Neg_Informes.ObtenerEstructuraComprobanteIncentivo(int.Parse(txtperiodo.Text.Trim()));
                DataTable cutEmp = dsComprobante.Tables[0];

                var sortdt = cutEmp.AsEnumerable().Where(c => c.Field<int>("codigo_empleado") == Convert.ToInt32(TxtCodigoE.Text))
                    .OrderBy(c => c.Field<string>("modulo"))
                    .ThenBy(d => d.Field<DateTime>("fecha_producido")).CopyToDataTable();

                cargarReporte(sortdt, 4, ReportViewer1);
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
        public void cargarReporte(DataTable dt, int rpt, ReportViewer window)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            window.ProcessingMode = ProcessingMode.Local;
            window.LocalReport.DataSources.Clear();
            window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoDiarioEmpDet.rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }
    }
}