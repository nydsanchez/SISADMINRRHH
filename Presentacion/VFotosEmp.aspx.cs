using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using Negocios;

namespace NominaRRHH.Presentacion
{
    public partial class VFotosEmp : System.Web.UI.Page
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDepartamentos();
            }
        }
        private void ObtenerDepartamentos()
        {
            this.ddlDepto1.DataSource = Neg_Informes.CargarDeptos();
            this.ddlDepto1.DataValueField = "codigo_depto";
            this.ddlDepto1.DataTextField = "nombre_depto";
            this.ddlDepto1.DataBind();
            this.ddlDepto2.DataSource = Neg_Informes.CargarDeptos();
            this.ddlDepto2.DataValueField = "codigo_depto";
            this.ddlDepto2.DataTextField = "nombre_depto";
            this.ddlDepto2.DataBind();
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
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
                DataTable dt = new DataTable();//se guarda lo cargado en excel
                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 2)
                    {

                        if (dt.Columns[0].ToString().ToLower() == "codigo1" && dt.Columns[1].ToString().ToLower() == "nombre1")
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
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            int depto1 = 0, depto2 = 0;

            depto1 = Convert.ToInt32(ddlDepto1.SelectedValue.Trim());
            depto2 = Convert.ToInt32(ddlDepto2.SelectedValue.Trim());
            if (ChkExcel.Checked)
            {
                DataTable dt = Session["datos"] as DataTable;
                ds.Tables.Add(dt.Copy());
            }
            else {
                ds = Neg_Informes.CargarFotoDepto(depto1, depto2);
            }
                        
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/FotosDepartamento.rdlc");

            DataTable dtPrincipal = new DataTable();
            dtPrincipal.Columns.Add("codigo1");
            dtPrincipal.Columns.Add("foto1", typeof(Byte[]));
            dtPrincipal.Columns.Add("nombre1");
            dtPrincipal.Columns.Add("codigo2");
            dtPrincipal.Columns.Add("foto2", typeof(Byte[]));
            dtPrincipal.Columns.Add("nombre2");
            dtPrincipal.Columns.Add("codigo3");
            dtPrincipal.Columns.Add("foto3", typeof(Byte[]));
            dtPrincipal.Columns.Add("nombre3");

            int indice = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                #region Menos de tres carnet
                if (ds.Tables[0].Rows.Count < 3)
                {


                }
                #endregion
                string codigo1 = "", codigo2 = "", codigo3 = "";
                string nombre1 = "", nombre2 = "", nombre3 = "";

                indice += 1;

                if (indice == 3)//Creo la primera fila.
                {
                    byte[] byteArray1 = new byte[0];
                    byte[] byteArray2 = new byte[0];
                    byte[] byteArray3 = new byte[0];

                    if (ds.Tables[0].Rows[i - 2]["codigo1"].ToString() != "")
                    {
                        codigo1 = ds.Tables[0].Rows[i - 2]["codigo1"].ToString();
                        if (codigo1 != "")
                            byteArray1 = Neg_Catalogos.cargarFoto(Convert.ToInt32(codigo1));
                        nombre1 = ds.Tables[0].Rows[i - 2]["nombre1"].ToString();
                    }
                    if (ds.Tables[0].Rows[i - 1]["codigo1"].ToString() != "")
                    {
                        codigo2 = ds.Tables[0].Rows[i - 1]["codigo1"].ToString();
                        if (codigo2 != "")
                            byteArray2 = Neg_Catalogos.cargarFoto(Convert.ToInt32(codigo2));
                        nombre2 = ds.Tables[0].Rows[i - 1]["nombre1"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["codigo1"].ToString() != "")
                    {
                        codigo3 = ds.Tables[0].Rows[i]["codigo1"].ToString();
                        if (codigo3 != "")
                            byteArray3 = Neg_Catalogos.cargarFoto(Convert.ToInt32(codigo3));
                        nombre3 = ds.Tables[0].Rows[i]["nombre1"].ToString();
                        dtPrincipal.Rows.Add(codigo1, byteArray1, nombre1, codigo2, byteArray2, nombre2, codigo3, byteArray3, nombre3);
                        indice = 0;//Lo regreso al indice 0,
                    }
                }
                else
                {
                    if (i == ds.Tables[0].Rows.Count - 1)//Si es la ultima fila.
                    {
                        byte[] ArregloVacio = new byte[0];
                        byte[] byteArray1I = new byte[0];
                        byte[] byteArray2I = new byte[0];
                        string UltimoCodigo = "", PenultimoCodigo = "";
                        string UltimoNombre = "", PenultimoNombre = "";

                        UltimoCodigo = ds.Tables[0].Rows[i]["codigo1"].ToString();
                        UltimoNombre = ds.Tables[0].Rows[i]["nombre1"].ToString();

                        if (ds.Tables[0].Rows.Count > 1)
                        {
                            PenultimoCodigo = ds.Tables[0].Rows[i - 1]["codigo1"].ToString();
                            PenultimoNombre = ds.Tables[0].Rows[i - 1]["nombre1"].ToString();
                        }

                        if (UltimoCodigo != "")
                            byteArray1I = Neg_Catalogos.cargarFoto(Convert.ToInt32(UltimoCodigo));

                        var resultado = from r in dtPrincipal.AsEnumerable()
                                        where r.Field<string>("codigo1") == PenultimoCodigo ||
                                              r.Field<string>("codigo2") == PenultimoCodigo ||
                                              r.Field<string>("codigo3") == PenultimoCodigo
                                        select r;
                        int Nfila = resultado.Count();
                        if (Nfila != 0)//Si no esta 
                        {
                            PenultimoCodigo = "";
                            byteArray2I = ArregloVacio;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count > 1)
                            {
                                PenultimoCodigo = ds.Tables[0].Rows[i - 1]["codigo1"].ToString();
                                byteArray2I = Neg_Catalogos.cargarFoto(Convert.ToInt32(PenultimoCodigo));
                                PenultimoNombre = ds.Tables[0].Rows[i - 1]["nombre1"].ToString();
                            }
                            //dtPrincipal.Rows.Add(UltimoCodigo, byteArray1I, UltimoNombre, UltimoDepto, PenultimoCodigo, byteArray2I, PenultimoNombre, PenultimoDepto, "", ArregloVacio, "", "");
                        }
                    }
                }
            }

            if (ds.Tables[0].Rows.Count % 3 != 0)//Si es multiplo de 3
            {
                byte[] ArregloVacio = new byte[0];//El tercero de cada fila,en este caso no existe.
                int i1 = ds.Tables[0].Rows.Count;
                string UltimoCodigo = ds.Tables[0].Rows[i1 - 1]["codigo1"].ToString();
                string UltimoNombre = ds.Tables[0].Rows[i1 - 1]["nombre1"].ToString();

                byte[] byteArray1I = new byte[0];
                if (UltimoCodigo != "")
                    byteArray1I = Neg_Catalogos.cargarFoto(Convert.ToInt32(UltimoCodigo));

                string PenultimoCodigo = "";
                string PenultimoNombre = "";
                if (ds.Tables[0].Rows.Count > 1)
                {
                    PenultimoCodigo = ds.Tables[0].Rows[i1 - 2]["codigo1"].ToString();
                    PenultimoNombre = ds.Tables[0].Rows[i1 - 2]["nombre1"].ToString();
                }

                byte[] byteArray2I = new byte[0];
                if (PenultimoCodigo != "")
                    byteArray2I = Neg_Catalogos.cargarFoto(Convert.ToInt32(PenultimoCodigo));

                if ((ds.Tables[0].Rows.Count - 1) % 3 == 0)//Sobra 1
                {
                    //Response.Write("Sobra 1");
                    dtPrincipal.Rows.Add(UltimoCodigo, byteArray1I, UltimoNombre, "", ArregloVacio, "", "", ArregloVacio, "");
                }
                if ((ds.Tables[0].Rows.Count - 2) % 3 == 0)//Sobran 2
                {
                    dtPrincipal.Rows.Add(UltimoCodigo, byteArray1I, UltimoNombre, PenultimoCodigo, byteArray2I, PenultimoNombre, "", ArregloVacio, "");
                    // Response.Write("Sobran 2");
                }
            }
            dtPrincipal.DefaultView.Sort = "[codigo1] ASC";
            dtPrincipal = dtPrincipal.DefaultView.ToTable();
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtPrincipal);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing +=
            new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportDataSource datasource2 = new ReportDataSource("DataSet1", ds.Tables[0]);
        }

        protected void ChkExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkExcel.Checked)
            {
                divexcel.Visible = true;
                divdpto.Visible = false;
            }
            else {
                divexcel.Visible = false;
                divdpto.Visible = true;
            }
        }
    }
}