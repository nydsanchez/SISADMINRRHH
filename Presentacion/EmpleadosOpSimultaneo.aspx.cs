using Microsoft.Reporting.WebForms;
using Negocios;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NominaRRHH.Presentacion
{
    public partial class EmpleadosOpSimultaneo : System.Web.UI.Page
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
            ProcesarExcel(fileProtectedDz, gvINGDD, Button1);
        }

        #endregion
        void ProcesarExcel(FileUpload file, GridView gv, Button btn)
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

                ///
                Session["dt"] = dt;
                btn.Visible = true;
                gv.DataSource = dt;
                gv.DataBind();
            }

            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                file.Focus();
            }

        }

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


                    if (dt.Columns[0].ToString().ToLower() == "codigo" && dt.Columns[1].ToString().ToLower() == "modulo" && dt.Columns[2].ToString().ToLower() == "porcentaje")
                    {
                        DataSet dtp = Neg_Catalogos.CargarProcesos();
                        DataTable dp = dtp.Tables[0].AsEnumerable().Where(c => c.Field<string>("nombre_depto").ToLower().IndexOf("modulo") > -1).CopyToDataTable();

                        int codigo = 0, codigo_depto = 0, codigo_deptoc = 0;
                        string modulo = "", moduloc = "";
                        decimal porcentaje = 0;
                        int desactivar = 0;

                        if (!NegEmp.PlnEmpleadoOperaSimultaneoDel())
                        {
                            throw new Exception("Error al eliminar distribucion");
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString() != "")
                            {//crear nueva tabla con traslados, al momento del job obtener el depto que fue actualizado para la fecha

                                codigo = Convert.ToInt32(dt.Rows[i][0]);
                                if (dt.Rows[i][1].ToString() != "")
                                {
                                    modulo = dt.Rows[i][1].ToString().Trim().ToLower().IndexOf("modulo") < 0 ? "MODULO " + dt.Rows[i][1].ToString().Trim() : dt.Rows[i][1].ToString().Trim();
                                    codigo_depto = Convert.ToInt32(dp.AsEnumerable().Where(c => c.Field<string>("nombre_depto").Trim().IndexOf(modulo) > -1).FirstOrDefault()["codigo_depto"]);
                                }

                                porcentaje = dt.Rows[i][2].ToString() != "" ? Convert.ToDecimal(dt.Rows[i][2]) : 0;

                                if (dt.Columns.Count == 4)//4ta columna opcional
                                {
                                    if (dt.Rows[i][3].ToString() != "")
                                    {

                                        if (dt.Columns[3].ToString().ToLower() == "moduloc")
                                        {
                                            moduloc = dt.Rows[i][3].ToString().Trim().ToLower().IndexOf("modulo") < 0 ? "MODULO " + dt.Rows[i][3].ToString().Trim() : dt.Rows[i][3].ToString().Trim();
                                            codigo_deptoc = Convert.ToInt32(dp.AsEnumerable().Where(c => c.Field<string>("nombre_depto").Trim().IndexOf(moduloc) > -1).FirstOrDefault()["codigo_depto"]);

                                        }
                                        else if (dt.Columns[3].ToString().ToLower() == "desactivar")
                                        {
                                            desactivar = 1;
                                        }
                                    }

                                }

                                if (!NegEmp.PlnEmpleadoOperaSimultaneoIns(codigo, codigo_depto, porcentaje, codigo_deptoc, desactivar, user))
                                {
                                    throw new Exception("Error al registrar distribucion");
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
                        LblSuccess.Text = "Distribucion guardada exitosamente";
                        gvINGDD.DataSource = null;
                        gvINGDD.DataBind();
                        Button1.Visible = false;
                    }
                    else
                    {
                        throw new Exception("El Archivo Excel no contiene Nombres de Columnas Requeridos");
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
                DataTable prot = NegEmp.plnObtenerEmpleadosOpSimultaneo(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text), 0);
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
            window.LocalReport.ReportPath = Server.MapPath("../Reportes/OperadoresSimultaneos.rdlc");
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
                int codigo_depto = Convert.ToInt32(gvtraslados.DataKeys[index][2]);
                if (NegEmp.PlnTrasladoEmpleadosDel(fecha, codigo, 0))
                {
                    obtenerRegistros();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Distribucion Eliminada";
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al eliminar distribucion";

                }
            }

        }
        void obtenerRegistros()
        {
            DataTable prot = NegEmp.plnObtenerEmpleadosOpSimultaneo(Convert.ToDateTime(txtfechainiB.Text), Convert.ToDateTime(txtfechafinB.Text), Convert.ToInt32(txtCodigoB.Text));

            gvtraslados.DataSource = prot;
            gvtraslados.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            obtenerRegistros();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ProcesarExcel(FileUpload1, GridView1, Button5);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["dt"] as DataTable;
                string user = Convert.ToString(this.Page.Session["usuario"]);
                div2.Visible = false;

                if (dt.Rows.Count > 0)
                {

                    if (dt.Columns[0].ToString().ToLower() == "codigo" && dt.Columns[1].ToString().ToLower() == "modulo" && dt.Columns[2].ToString().ToLower() == "porcentaje"
                         && dt.Columns[3].ToString().ToLower() == "fecha"
                        )
                    {
                        DataSet dtp = Neg_Catalogos.CargarProcesos();
                        DataTable dp = dtp.Tables[0].AsEnumerable().Where(c => c.Field<string>("nombre_depto").ToLower().IndexOf("modulo") > -1).CopyToDataTable();

                        int codigo = 0, codigo_depto = 0, codigo_deptoc = 0;
                        string modulo = "", moduloc = "",operacion="";
                        decimal porcentaje = 0;

                        EliminarRegistroHistorico(dt);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() != "" && dt.Rows[i][2].ToString() != "" && dt.Rows[i][3].ToString() != ""
                               )
                            {//crear nueva tabla con traslados, al momento del job obtener el depto que fue actualizado para la fecha

                                codigo = Convert.ToInt32(dt.Rows[i][0]);
                                modulo = dt.Rows[i][1].ToString().Trim().ToLower().IndexOf("modulo") < 0 ? "MODULO " + dt.Rows[i][1].ToString().Trim() : dt.Rows[i][1].ToString().Trim();
                                codigo_depto = Convert.ToInt32(dp.AsEnumerable().Where(c => c.Field<string>("nombre_depto").Trim().IndexOf(modulo) > -1).FirstOrDefault()["codigo_depto"]);
                                porcentaje = Convert.ToDecimal(dt.Rows[i][2]);
                                DateTime fecha = Convert.ToDateTime(dt.Rows[i][3].ToString().Trim());
                                codigo_deptoc = codigo_depto;

                                if (dt.Columns.Count == 5)//5ta columna opcional
                                {
                                    if (dt.Rows[i][4].ToString() != "")
                                    {
                                        //if (dt.Columns[4].ToString().ToLower() == "moduloc")
                                        //{
                                        //    moduloc = dt.Rows[i][4].ToString().Trim().ToLower().IndexOf("modulo") < 0 ? "MODULO " + dt.Rows[i][4].ToString().Trim() : dt.Rows[i][4].ToString().Trim();
                                        //    codigo_deptoc = Convert.ToInt32(dp.AsEnumerable().Where(c => c.Field<string>("nombre_depto").Trim().IndexOf(moduloc) > -1).FirstOrDefault()["codigo_depto"]);
                                        //}
                                        if (dt.Columns[4].ToString().ToLower() == "operacion")
                                        {
                                            operacion = dt.Rows[i][4].ToString().Trim() ;
                                        }
                                    }
                                    
                                }

                                if (!NegEmp.PlnEmpleadosHistUpd(codigo, fecha, codigo_depto, codigo_deptoc, porcentaje,operacion))
                                {
                                    throw new Exception("Error al realizar distribucion");
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
                        LblSuccess.Text = "Distribucion Historica actualizada exitosamente";
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        Button5.Visible = false;
                    }
                    else
                    {
                        throw new Exception("El Archivo Excel no contiene Nombres de Columnas Requeridos");
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
        void EliminarRegistroHistorico(DataTable dt)
        {
            try
            {
                //primera eliminar codigo y fecha del pln historico

                DataRow[] toDelete = dt.AsEnumerable().GroupBy(c => new { c1 = c["codigo"], c2 = c["fecha"] }).Select(c => c.First()).ToArray();
                foreach (DataRow dr in toDelete)
                {
                    if (!NegEmp.PlnTrasladoEmpleadosDel(Convert.ToDateTime(dr["fecha"]), Convert.ToInt32(dr["codigo"]),0))
                    {
                        throw new Exception("Error al eliminar registro de historico");
                    }
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
