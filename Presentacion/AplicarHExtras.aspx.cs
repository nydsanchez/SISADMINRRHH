using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Negocios;
using Datos;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace NominaRRHH.Presentacion
{
    public partial class AplicarHExtras : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Marca Neg_Marca = new Neg_Marca();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                txtFechaIni.Text = DateTime.Now.ToShortDateString();
                txtFechaFin.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void ObtenerHorasExtras()
        {
            //obtengo horas extras
            //agrupo departamentos y lleno el ddl 
            //con el primer elementos seleccionado filtro las horas extras
            //horas extras se filtran por deptamento
            try
            {                
                DataTable he = new DataTable();
                int periodo = ChkEstatus.Checked ? Convert.ToInt32(txtPeriodo.Text) : 0;                
                he = Neg_Marca.ObtenerHorasExtrasAprobacion(DateTime.Now, DateTime.Now, 2,periodo,false);
                Session["HorasExtrasAprob"] = he;
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }

        }
        private void ObtenerIngresos()
        {
            DataTable he = new DataTable();
            DataRow[] ingresos = null;
            he = Session["HorasExtrasAprob"] as DataTable;

            ingresos = he.AsEnumerable().GroupBy(c => c.Field<int>("tipoingrdeduc")).Select(c => c.First()).ToArray();

            if (ingresos.Length > 0)
            {
                divdp.Visible = true;
                this.ddlIngreso.DataSource = ingresos.CopyToDataTable();
                this.ddlIngreso.DataValueField = "tipoingrdeduc";
                this.ddlIngreso.DataTextField = "nombrerubro";
                this.ddlIngreso.DataBind();
            }
            else
            {
                divdp.Visible = false;
            }

        }
        private void FiltrarDatosxIngresos()
        {
            DataTable he = new DataTable();
            DataRow[] personal = null;
            he = Session["HorasExtrasAprob"] as DataTable;

            personal = he.AsEnumerable().Where(c => c.Field<int>("tipoingrdeduc") == Convert.ToInt32(ddlIngreso.SelectedValue)).ToArray();

            if (personal.Length>0)
            {
                cargarReporte(personal.CopyToDataTable(),ReportViewer3,1);
            }
            else
            {
                cargarReporte(new DataTable(), ReportViewer3, 1);
            }
            

        }
     
        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarDatosxIngresos();

        }
    
        public void cargarReporte(DataTable dt, ReportViewer rpt, int opcion)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            rpt.ProcessingMode = ProcessingMode.Local;
            rpt.LocalReport.DataSources.Clear();
            if (opcion == 1)
            {
                rpt.LocalReport.ReportPath = Server.MapPath("../Reportes/IngresosDetallexDia.rdlc");

            }
            else if (opcion == 2)
            {
                rpt.LocalReport.ReportPath = Server.MapPath("../Reportes/RevisionHoraExtrasPend.rdlc");

            }
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            rpt.LocalReport.DataSources.Add(source);
            rpt.LocalReport.Refresh();

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            ActualizarGrid();
        }
        void ActualizarGrid()
        {
            try
            {
                
                if (ChkEstatus.Checked)
                {
                    if (string.IsNullOrEmpty(txtPeriodo.Text) || txtPeriodo.Text.Trim() == "0")
                    {
                        throw new Exception("Periodo Invalido");
                    }
                }
               

                ObtenerHorasExtras();
                ObtenerIngresos();
                FiltrarDatosxIngresos();
               
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
       
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Neg_Planilla Neg_Planilla = new Neg_Planilla();
                string user = Convert.ToString(this.Page.Session["usuario"]);
                //aqui recorrer la grid
                //bono checked tipoingrdeduc=18, insertarIngDeducxDia
                //HE checked tipoingrdeduc=1, insertarHorasExtrasxDia
                //metodo update periodo, revison=0 usuarioaplica,fechaaplica
                if (string.IsNullOrEmpty(txtPeriodo.Text) || txtPeriodo.Text.Trim() == "0")
                {
                    throw new Exception("Periodo Invalido");
                }
                Neg_Periodo Neg_Periodo = new Neg_Periodo();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
               
                if (dtPeriodo.Rows.Count > 0)
                {

                    if (dtPeriodo[0].cerrado == 1)
                        throw new Exception("El periodo esta cerrado");
                }
                
                if (!Neg_Planilla.AplicarIngDeducxDia(Convert.ToInt32(txtPeriodo.Text.Trim())))
                {
                    throw new Exception("Error aplicando ingresos en periodo.");
                }
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "Los registros se han aplicado correctamente.";
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }

        protected void ddlIngreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarDatosxIngresos();
        }

       
        protected void Button6_Click(object sender, EventArgs e)
        {
            if (fileProtectedDz.HasFile)
            {
              
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
                Button2.Visible = true;
                gvING.DataSource = dt;
                gvING.DataBind();
            }

            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                fileProtectedDz.Focus();
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Session["dt"] as DataTable;
                string user = Convert.ToString(this.Page.Session["usuario"]);
                Neg_Planilla Neg_Planilla = new Neg_Planilla();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 5)
                    {

                        if (dt.Columns[0].ToString().ToLower() == "fecha" && dt.Columns[1].ToString().ToLower() == "codigo"
                            && dt.Columns[2].ToString().ToLower() == "id_tipo" && dt.Columns[3].ToString().ToLower() == "tipoingrdeduc"
                            && dt.Columns[4].ToString().ToLower() == "comentario"
                            )
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][0].ToString() != "" && dt.Rows[i][1].ToString() != "" && dt.Rows[i][2].ToString() != "" && dt.Rows[i][3].ToString() != ""
                                   )
                                {//
                                    DateTime fecha = Convert.ToDateTime(dt.Rows[i][0].ToString().Trim());
                                    int codigo = Convert.ToInt32(dt.Rows[i][1]);
                                    int idtipo = Convert.ToInt32(dt.Rows[i][2]);
                                    int tipop = Convert.ToInt32(dt.Rows[i][3]);
                                    string comentario = dt.Rows[i][4].ToString().Trim();

                                    if (!Neg_Planilla.ActualizarIngDeducxDia(codigo, 0, idtipo, tipop, fecha,comentario))
                                    {
                                        throw new Exception("Error eliminando ingreso por dia.");
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
                            LblSuccess.Text = "Registros enviados a revision";
                            gvING.DataSource = null;
                            gvING.DataBind();
                            Button2.Visible = false;
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

        protected void ChkEstatus_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkEstatus.Checked)
            {
                divbtn.Visible = false;
            }
            else
            {
                divbtn.Visible = true;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFechaIni.Text) || string.IsNullOrEmpty(txtFechaFin.Text))
                {
                    throw new Exception("Debe ingresar fechas validas.");
                }
                DataTable he = new DataTable();               
                he = Neg_Marca.ObtenerHorasExtrasAprobacion(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text), 3, 0,false);
                if (he.Rows.Count > 0)
                {
                    he = he.AsEnumerable().OrderBy(c => c.Field<string>("nombre_depto")).ThenBy(c => c.Field<DateTime>("fecha")).ThenBy(c=>c.Field<int>("codigo_empleado")).CopyToDataTable();
                    cargarReporte(he, ReportViewer1, 2);
                }
                
                Button3.Focus();
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