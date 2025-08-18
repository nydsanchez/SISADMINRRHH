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


namespace NominaRRHH.Presentacion
{
    public partial class ComprobanteIncentivo : System.Web.UI.Page
    {

        #region REFERENCIAS
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_HojasPDF Neg_HojasPDF = new Neg_HojasPDF();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlModulo.Visible = false;
                pnlCodigo.Visible = false;
                pnlExcel.Visible = false;
                btnAceptar.Visible = false;

            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtperiodo.Text != "")
            {
                int periodo = int.Parse(txtperiodo.Text);
                int param = 0, tipo = 0;


                DataTable dtInfon = new DataTable();

                #region DATATASET CON DATOS EMPLEADOS

                dtInfon.Columns.Add("periodo", typeof(int));
                dtInfon.Columns.Add("semana", typeof(int));
                dtInfon.Columns.Add("codigo_empleado", typeof(int));
                dtInfon.Columns.Add("nombrecompleto", typeof(string));
                dtInfon.Columns.Add("Modulo", typeof(int));
                dtInfon.Columns.Add("Estilo", typeof(int));
                dtInfon.Columns.Add("Construccion", typeof(string));
                dtInfon.Columns.Add("Produccion", typeof(decimal));
                dtInfon.Columns.Add("metaAlcanzada", typeof(decimal));
                dtInfon.Columns.Add("EficienciaAlcanzada", typeof(decimal));
                dtInfon.Columns.Add("IncentivoMeta", typeof(decimal));
                dtInfon.Columns.Add("TotalIngreso", typeof(decimal));
                dtInfon.Columns.Add("TotalDeducciones", typeof(decimal));
                dtInfon.Columns.Add("TotalIncentivo", typeof(decimal));
                dtInfon.Columns.Add("UsuarioGraba", typeof(string));
                dtInfon.Columns.Add("Fechagraba", typeof(DateTime));
                dtInfon.Columns.Add("HoraGraba", typeof(TimeSpan));
                #endregion

                DataTable dt = new DataTable();

                // GENERACION DE COMPROBANTE POR MODULO
                if (rbllistImpresion.SelectedValue == "1")
                {
                    tipo = 1;
                    param = int.Parse(ddlProceso.SelectedValue);
                    dt = Neg_Incentivos.IncentivoHistoricoSelectModulo(periodo, 3, param);


                }
                // GENERACION DE COMPROBANTE POR CODIGO
                else if (rbllistImpresion.SelectedValue == "2")
                {
                    tipo = 2;
                    param = int.Parse(TxtCodigoE.Text);
                    dt = Neg_Incentivos.IncentivoHistoricoSelectCodigo(periodo, 3, param);


                }

                     // GENERACION DE COMPROBANTE POR EXCEL
                else if (rbllistImpresion.SelectedValue == "3")
                {
                    tipo = 3;
                    DataTable dtCodigo = CargarExcel();

                    foreach (DataRow row in dtCodigo.Rows)
                    {
                        param = int.Parse(row["Codigo"].ToString());
                        dt = Neg_Incentivos.IncentivoHistoricoSelectCodigo(periodo, 3, param);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                dtInfon.ImportRow(dt.Rows[0]);
                            }
                        }


                    }
                    param = 0;
                    dt = dtInfon;
                }
                else
                {
                    tipo = 4;
                    dt = Neg_Incentivos.IncentivoHistoricoSelect(periodo, 3);

                }

                List<Neg_Incentivos.DetalleComprobante> listado = new List<Neg_Incentivos.DetalleComprobante>();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        listado = Neg_Incentivos.IncentivoHistoricoDT(periodo, 3, dt);
                        string html = Neg_HojasPDF.armarEstructuraHojaReconciliacion(listado, tipo, param, dtInfon, periodo);
                        CrearPDF(html);
                    }
                }

            }
        }

        protected void rbllistImpresion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtperiodo.Text != "")
            {
                btnAceptar.Visible = true;
                if (rbllistImpresion.SelectedValue == "1")
                {
                    pnlModulo.Visible = true;
                    pnlCodigo.Visible = false;
                    pnlExcel.Visible = false;
                    ddlProceso.DataSource = CargarDll();
                    ddlProceso.DataBind();

                }
                else if (rbllistImpresion.SelectedValue == "2")
                {
                    pnlModulo.Visible = false;
                    pnlCodigo.Visible = true;
                    pnlExcel.Visible = false;

                }
                else if (rbllistImpresion.SelectedValue == "3")
                {
                    pnlModulo.Visible = false;
                    pnlCodigo.Visible = false;
                    pnlExcel.Visible = true;
                }

                else
                {
                    pnlModulo.Visible = false;
                    pnlCodigo.Visible = false;
                    pnlExcel.Visible = false;
                }
            }
            else
            {
                btnAceptar.Visible = false;

            }
        }

        public DataTable CargarExcel()
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            FileUpload fileUpload = (FileUpload)cph.FindControl("file");
            DataTable dtExcelRecords = new DataTable();

            if (fileUpload.HasFile)
            {
                string connectionString = "";
                string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                string fileExtension = Path.GetExtension(fileUpload.PostedFile.FileName);
                string fileLocation = HttpContext.Current.Server.MapPath("..").ToString() + @"\Trash\" + fileName;
                fileUpload.SaveAs(fileLocation);
                //  int totalcajas = 0;
                //Check whether file extension is xls or xslx
                if (fileExtension == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (fileExtension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }

                //Create OleDB Connection and OleDb Command
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);

                con.Open();
                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtExcelRecords);
                con.Close();

            }

            return dtExcelRecords;
        }

        public DataTable CargarDll()
        {
            DataTable Modulos = new DataTable();
            if (txtperiodo.Text != "")
            {
                int periodo = int.Parse(txtperiodo.Text);
                Modulos = Neg_Incentivos.IncentivoModulosConMeta(periodo);
            }
            return Modulos;
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
    }
}