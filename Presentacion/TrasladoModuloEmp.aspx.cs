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
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace NominaRRHH.Presentacion
{
    public partial class TrasladoModuloEmp : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Empleados NegEmp = new Neg_Empleados();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
               
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (fileProtectedDz.HasFile)
            {
                div2.Visible = false;
                string connectionString = "";
                string FileName = Path.GetFileName(fileProtectedDz.PostedFile.FileName);
                string Extension = Path.GetExtension(fileProtectedDz.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FileName);
                string fileLocation = HttpContext.Current.Server.MapPath(".").ToString() + @"\Trash\" + FileName;
                fileProtectedDz.SaveAs(FilePath);

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
                Button1.Visible = true;
                gvINGDD.DataSource = dt;
                gvINGDD.DataBind();
            }

            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                fileProtectedDz.Focus();
            }
        }
       
        #endregion

        #region METODOS


        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["dt"] as DataTable;
                string user = Convert.ToString(this.Page.Session["usuario"]);
                div2.Visible = false;

                if (dt.Rows.Count > 0)
                {
                    //if (dt.Columns.Count == 4)
                    //{

                        if (dt.Columns[0].ToString().ToLower() == "fecha" && dt.Columns[1].ToString().ToLower() == "codigo"
                            && dt.Columns[2].ToString().ToLower() == "modulo" && dt.Columns[3].ToString().ToLower() == "justificacion"
                            
                            )
                        {
                            DataSet dtp = Neg_Catalogos.CargarProcesos();
                            DataTable dp = dtp.Tables[0].AsEnumerable().Where(c => c.Field<string>("nombre_depto").ToLower().IndexOf("modulo") > -1).CopyToDataTable();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() != "" && dt.Rows[i][2].ToString() != "" && dt.Rows[i][3].ToString() != ""
                                   )
                                {//crear nueva tabla con traslados, al momento del job obtener el depto que fue actualizado para la fecha
                                    DateTime fecha = Convert.ToDateTime(dt.Rows[i][0].ToString().Trim());
                                    int codigo = Convert.ToInt32(dt.Rows[i][1]);
                                    string modulo = dt.Rows[i][2].ToString().Trim().ToLower().IndexOf("modulo") < 0 ? "MODULO " + dt.Rows[i][2].ToString().Trim() : dt.Rows[i][2].ToString().Trim();
                                    int codigo_depto = Convert.ToInt32(dp.AsEnumerable().Where(c => c.Field<string>("nombre_depto").Trim().IndexOf(modulo) > -1).FirstOrDefault()["codigo_depto"]);                                  
                                    string observacion = dt.Rows[i][3].ToString().Trim();
                                string operacion = dt.Columns.Count > 4 ? dt.Rows[i][4].ToString().Trim() : "";
                                    if (!NegEmp.PlnTrasladoEmpleadosIns(fecha, codigo, codigo_depto,observacion,operacion, user))
                                    {
                                        throw new Exception("Error al realizar traslado de modulo");
                                    }
                                }
                                else
                                {
                                    throw new Exception("Todos los campos son obligatorios");
                                }
                            }
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Traslados guardados exitosamente";
                            gvINGDD.DataSource = null;
                            gvINGDD.DataBind();
                            Button1.Visible = false;
                        }
                        else
                        {
                            throw new Exception("El Archivo Excel no contiene Nombres de Columnas Requeridos");
                        }

                    //}
                    //else
                    //{
                    //    throw new Exception("El Archivo Excel no contiene las Columnas Requeridas");
                    //}

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
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFechaIni.Text) || string.IsNullOrEmpty(txtFechaFin.Text))
                {
                    throw new Exception("Debe ingresar Fechas validas");
                }
                div2.Visible = true;
                //AQUI SOLO DEBE OBTENER LOS EMPLEADOS EN HISTORICO DE TRASLADOS QUE SU DEPTO ACTUAL ES DIFERENTE AL DE LAS FECHAS CONSULTADAS
                DataTable prot = NegEmp.PlnTrasladoEmpleadosSel(Convert.ToDateTime(txtFechaIni.Text),Convert.ToDateTime(txtFechaFin.Text));
                //var sortdt = prot.AsEnumerable().OrderBy(c => c.Field<string>("nombre_depto")).ThenBy(c => c.Field<DateTime>("fecha")).CopyToDataTable();

                cargarReporte(prot, 2, ReportViewer1);

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }

        }
        public void cargarReporte(DataTable dt, int rpt, ReportViewer window)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            window.ProcessingMode = ProcessingMode.Local;
            window.LocalReport.DataSources.Clear();
            window.LocalReport.ReportPath = Server.MapPath("../Reportes/TrasladoEmpleados.rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }
        protected void gvtraslados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());


            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                GridViewRow selectedRow = gvtraslados.Rows[index];
                int codigo = Convert.ToInt32(((TextBox)selectedRow.FindControl("txtCodigo")).Text.Trim());
                DateTime fecha = Convert.ToDateTime(((TextBox)selectedRow.FindControl("txtFecha")).Text.Trim());
                int codigo_depto= Convert.ToInt32(gvtraslados.DataKeys[index][2]);
                if (NegEmp.PlnTrasladoEmpleadosDel(fecha,codigo,codigo_depto))
                {
                    obtenerRegistros();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Traslado Eliminado";
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error Al Eliminar Traslado";

                }
            }

        }
        void obtenerRegistros()
        {
            DataTable prot = NegEmp.PlnDeptoEmpHistoricoSel(int.Parse(txtCodigoB.Text) ,Convert.ToDateTime(txtfechainiB.Text), Convert.ToDateTime(txtfechafinB.Text));

            gvtraslados.DataSource = prot;
            gvtraslados.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            obtenerRegistros();
        }
    }

}
