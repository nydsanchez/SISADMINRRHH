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
    public partial class ComprobanteConsolida : System.Web.UI.Page
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
            alertValida.Visible = false;
            lblAlert.Visible = false;
            lblAlert.Text = "";
            try
            {
                if (txtperiodo.Text.Trim() == "")
                {
                    throw new Exception("Debe especificar periodo");
                }
                int filtroeamil = ChkEmail.Checked ? 2 : 1;
                string html = Crear(filtroeamil);
                CrearPDF(html);
            }
            catch (Exception ex)
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }
        public string Crear(int filtroemail)
        {
            //empleados pagados en planilla
            Neg_Informes Neg_Informes = new Neg_Informes();
            Neg_Periodo NPeriodo = new Neg_Periodo();
            string html = "",htmlv="";
            try
            {                
                string encabezado = txtperiodo.Text.Trim();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(txtperiodo.Text.Trim()));
                DateTime ini = dtPeriodo[0].fechaini;
                DateTime fin = dtPeriodo[0].fechafin2;
                int filtro = 0;
                int valorFiltro = 0;
                DataTable codigo=new DataTable();
                if (rbllistImpresion.SelectedValue == "1")//modulo=ubic depto filtro 3
                {
                    filtro = 3;
                    valorFiltro = int.Parse(ddlProceso.SelectedValue);
                }               
                else if(rbllistImpresion.SelectedValue == "2" || rbllistImpresion.SelectedValue == "4")//todos y por codigo filtro 2
                {
                    filtro = 2;
                    if (rbllistImpresion.SelectedValue == "2")
                    {
                        valorFiltro = int.Parse(TxtCodigoE.Text);
                    }
                    else
                    {
                        valorFiltro = 0;
                    }
                }
                else
                {
                    filtro = 1;
                    valorFiltro = 0;
                    codigo = Session["datos"] as DataTable;
                }
                DataSet dsEmpleadospln = Neg_Informes.ObtenerEmpleadosPlanilla(txtperiodo.Text.Trim(), txtperiodo.Text, ini, fin, ddlProceso.SelectedValue.Trim(), TxtCodigoE.Text.Trim(), ChkAll.Checked, ChkEfectivo.Checked, filtroemail, false, true);
                if (dsEmpleadospln.Tables.Count > 0)
                {
                    DataTable dtEmpleadospln = dsEmpleadospln.Tables[0];
                    if (codigo.Rows.Count > 0)
                    {
                        int[] codemp = codigo.AsEnumerable().Select(u => u.Field<int>("codigo_empleado")).ToArray();
                        dtEmpleadospln = dtEmpleadospln.AsEnumerable().Where(c => codemp.Contains(c.Field<int>("codigo_empleado"))).CopyToDataTable();
                    }
                    html = Neg_Informes.GenerarColilla_ViaticoPeriodoPdf(dtEmpleadospln, txtperiodo.Text.Trim(),ini,fin, dtPeriodo[0].tperiodo, 1, 4, dtPeriodo[0].tperiodo, filtroemail, encabezado, false);
                }

                return html;

            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
            return "";
        }
        
        protected void rbllistImpresion_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtCodigoE.Text = "";
            if (rbllistImpresion.SelectedValue == "1")
            {
                pnlModulo.Visible = true;
                pnlCodigo.Visible = false;
                divfile.Visible = false;
                divEmp.Visible = false;

            }
            else if (rbllistImpresion.SelectedValue == "2")
            {
                pnlModulo.Visible = false;
                pnlCodigo.Visible = true;
                divEmp.Visible = true;

                divfile.Visible = false;

            }
            else if (rbllistImpresion.SelectedValue == "3")
            {
                pnlModulo.Visible = false;
                pnlCodigo.Visible = false;
                divfile.Visible = true;
                divEmp.Visible = false;
            }

            else if (rbllistImpresion.SelectedValue == "5")
            {
                pnlModulo.Visible = false;
                pnlCodigo.Visible = false;
                divfile.Visible = false;
                divEmp.Visible = false;
            }
            else
            {
                pnlModulo.Visible = false;
                pnlCodigo.Visible = false;
                divEmp.Visible = false;
                divfile.Visible = false;
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

        public void CargarDll()
        {
            DataSet dt = Neg_Catalogos.CargarProcesos();
            DataTable dp = dt.Tables[0];//.AsEnumerable().Where(c=>c.Field<string>("nombre_depto").ToLower().IndexOf("modulo")>-1).CopyToDataTable();
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
        protected void ChkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            //if (!ChkEmail.Checked)
            //{
            //    ChkEfectivo.Checked = false;
            //}
            if (ChkEfectivo.Checked)
            {
                
                TxtCodigoE.Text = "";
                this.ChkAll.Checked = false;
            }
            else
            {
               
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
               
                TxtCodigoE.Text = "";
                ChkEfectivo.Checked = false;
            }
            else
            {
               
                TxtCodigoE.Text = "";
                ChkEfectivo.Checked = false;
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtperiodo.Text.Trim() != "")
                {
                    string html = Crear(3);
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
    }
}