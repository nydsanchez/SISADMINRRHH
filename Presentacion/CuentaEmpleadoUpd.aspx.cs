using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class CuentaEmpleadoUpd : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Empleados Neg_Empleados = new Neg_Empleados();   
        Globales Globales = new Globales();
        #endregion

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
            }
        }
        
        protected void BtnExcelEmp_Click(object sender, EventArgs e)
        {
            if (FileVac.HasFile)
            {
                string connectionString = "";
                string FileName = Path.GetFileName(FileVac.PostedFile.FileName);
                string Extension = Path.GetExtension(FileVac.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FileName);
                string fileLocation = HttpContext.Current.Server.MapPath(".").ToString() + @"\Trash\" + FileName;
                FileVac.SaveAs(FilePath);

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

                ///
                Session["dt"] = dt;
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                FileVac.Focus();
            }
        }

        protected void btnProc_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["dt"] as DataTable;

                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 2)
                    {

                        if (dt.Columns[0].ToString().ToLower() == "codigo" && dt.Columns[1].ToString().ToLower() == "numero")
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (!Neg_Empleados.CuentaEmpleadoUpd(Convert.ToInt32(dt.Rows[i][0].ToString().Trim()), dt.Rows[i][1].ToString().Trim(), Convert.ToInt32(ddlTipoCuenta.SelectedValue)))
                                {
                                    throw new Exception( "Error al actualizar numeros de cuenta");
                                }
                            }
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;                           
                            LblSuccess.Text = "Cuentas guardadas exitosamente";
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                        else
                        {
                            throw new Exception("El Archivo Excel no contiene Nombres de Columnas Requeridos");
                        }

                    }
                    else
                    {
                        throw new Exception("El Archivo Excel no contiene las Columnas Requeridas");
                    }

                }
                else
                {
                    throw new Exception("El Archivo Excel no contiene registros");
                }
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