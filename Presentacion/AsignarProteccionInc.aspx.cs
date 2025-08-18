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
    public partial class AsignarProteccionInc : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        //Neg_Empleados NegEmp = new Neg_Empleados();
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
                    if (dt.Columns.Count == 5)
                    {

                        if (dt.Columns[0].ToString().ToLower() == "fecha" && dt.Columns[1].ToString().ToLower() == "modulo"
                            && dt.Columns[2].ToString().ToLower() == "problema" && dt.Columns[3].ToString().ToLower() == "dz"
                            && dt.Columns[4].ToString().ToLower() == "observacion"
                            )
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() != "" && dt.Rows[i][2].ToString() != "" && dt.Rows[i][3].ToString() != ""
                                    )
                                {
                                    DateTime fecha = Convert.ToDateTime(dt.Rows[i][0].ToString().Trim());
                                    string modulo = dt.Rows[i][1].ToString().Trim();
                                    string problema = dt.Rows[i][2].ToString().Trim();
                                    decimal dz = Convert.ToDecimal(dt.Rows[i][3].ToString().Trim());
                                    string observacion = dt.Rows[i][4].ToString().Trim();

                                    if (!Neg_Incentivos.PlnProteccionModuloIns(fecha, modulo, problema, dz, observacion, user))
                                    {
                                        throw new Exception("Error al actualizar protecciones");
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
                            LblSuccess.Text = "Protecciones guardadas exitosamente";
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
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFechaIni.Text) || string.IsNullOrEmpty(txtFechaFin.Text))
                {
                    throw new Exception("Debe ingresar Fechas validas");
                }
                div2.Visible = true;
                DataTable prot = Neg_Incentivos.PlnObtenerProteccionModulo(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text));
                var sortdt = prot.AsEnumerable().OrderBy(c => c.Field<string>("modulo")).ThenBy(c => c.Field<DateTime>("fecha")).CopyToDataTable();

                cargarReporte(sortdt, "ProteccionxModulo", ReportViewer1);

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }

        }
        public void cargarReporte(DataTable dt, string rpt, ReportViewer window)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            window.ProcessingMode = ProcessingMode.Local;
            window.LocalReport.DataSources.Clear();
            window.LocalReport.ReportPath = Server.MapPath("../Reportes/" + rpt + ".rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtPeriodo.Text.Trim()))
                {
                    throw new Exception("Debe ingresar periodo valido.");
                }
                div1.Visible = true;
                obtenerProtecciones();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        private void obtenerProtecciones()
        {
            DataTable dt = Neg_Incentivos.PlnObtenerProteccionIncentivoFijo(int.Parse(txtPeriodo.Text));
            cargarReporte(dt, "ProteccionesIndividuales", ReportViewer2);
        }
        protected void btnAgregarDed_Click(object sender, EventArgs e)
        {
            try
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);
                if (txtCodigo.Text.Trim() != "" && txtAsistencia.Text.Trim() != "" && txtViatico.Text.Trim() != "" && txtRepeticiones.Text.Trim() != "")
                {
                    int codigo = int.Parse(txtCodigo.Text);
                    decimal asistencia = decimal.Parse(txtAsistencia.Text);
                    decimal viatico = decimal.Parse(txtViatico.Text);
                    decimal porcentaje = decimal.Parse(txtPorcentaje.Text) / 100;
                    int repeticiones = int.Parse(txtRepeticiones.Text);

                    if (Neg_Incentivos.PlnProteccionIncentivoFijoIns(codigo, asistencia, viatico, porcentaje, repeticiones, ChkRecurrente.Checked, ChkActivo.Checked, user))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Ingreso Satisfactorio";

                        LimpiarCamposProtIndividual();
                    }

                }
                else
                {
                    throw new Exception("Inserte un valor valido");

                }
            }
            catch (Exception ex)
            {
                alertSucces.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void LimpiarCamposProtIndividual()
        {
            txtCodigo.Text = "";
            TxtNombreE.Text = "";
            txtAsistencia.Text = "";
            txtViatico.Text = "";
            txtPorcentaje.Text = "";
            txtRepeticiones.Text = "";
            ChkRecurrente.Checked = false;
            ChkActivo.Checked = true;
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerDatosEmpleado();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void ObtenerDatosEmpleado()
        {
            if (validar())
            {
                obtenerPeriodo();
                try
                {
                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(this.txtCodigo.Text.Trim()), 1);

                    if (dtEmp.Rows.Count > 0 && dtEmp.Rows[0]["idestado"].ToString().Trim() != "0")
                    {
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();
                        obtenerProtecciones();

                    }
                    else//No aplica a liquidación.
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "El empleado no se encuentra";
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
        public bool validar()
        {


            if (txtCodigo.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtCodigo.Focus();
                return false;
            }

            return true;
        }
        private void obtenerPeriodo()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtCodigo.Text.Trim()))
                {
                    DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtCodigo.Text);
                    DataTable ubicacion = Neg_Catalogos.seleccionarUbicacionesxCod(Convert.ToInt32(DetEmpleados.Rows[0]["codigo_ubicacion"]));
                    dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, Convert.ToInt32(ubicacion.Rows[0]["tplanilla"]), 0);
                    if (dtPeriodo.Rows.Count > 0)
                    {
                        txtPeriodo.Text = dtPeriodo[0].nperiodo.ToString();
                    }
                    else
                    {
                        txtPeriodo.Text = "0";
                    }
                    Session["periodo"] = txtPeriodo.Text.Trim();
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "No hay periodo abierto que este vigente";
            }
        }

    }

}
