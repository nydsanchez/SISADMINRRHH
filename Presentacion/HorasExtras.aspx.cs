using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class HorasExtras : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //obtenerPeriodo();
                obtenerTiposPlanilla();
                obtenerUbicaciones();
            }
        }
        private void obtenerUbicaciones()
        {
            this.ddlUbicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacion.DataMember = "ubicaciones";
            this.ddlUbicacion.DataValueField = "codigo_ubicacion";
            this.ddlUbicacion.DataTextField = "nombre_ubicacion";
            this.ddlUbicacion.DataBind();
        }

        private void obtenerPeriodo()
        {           
            dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.cargarUltPeriodoAbieCat(1, Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()), Convert.ToInt32(ddlUbicacion.SelectedValue.Trim()));
            
            if (dtPeriodo.Rows.Count > 0)
            {
                txtPeriodo.Text = dtPeriodo[0].nperiodo.ToString();

                //if (!dtPeriodo[0].consolidar && Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()) == 1)
                //{
                //    divSemana.Visible = true;
                //}else
                //{
                //    divSemana.Visible = false;
                //}

            }
            else
            {
                txtPeriodo.Text = "0";
            }
           
        }
        private void obtenerTiposPlanilla()
        {
            this.ddlTiposPlanilla.DataSource = Neg_Planilla.cargarTiposPlanilla();
            this.ddlTiposPlanilla.DataMember = "planillas";
            this.ddlTiposPlanilla.DataValueField = "idNomina";
            this.ddlTiposPlanilla.DataTextField = "Descripcion";
            this.ddlTiposPlanilla.DataBind();
        }
      
        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }
      

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
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

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                if (dt.Rows.Count > 0)
                {
                    //Bind Data to GridView
                    GVDevDeduc.Caption = Path.GetFileName(FilePath);
                    GVDevDeduc.DataSource = dt;
                    GVDevDeduc.DataBind();
                    btnProcesar.Visible = true;
                    dt2 = dt;
                    Session["horas"] = dt2;

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

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            DataTable dt1 = Session["horas"] as DataTable;
            Neg_Marca Neg_Marca = new Neg_Marca();
            int semana = 0;
            try {
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                    throw new Exception("Periodo Invalido");

                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                if (dtPeriodo[0].cerrado == 1)
                    throw new Exception("El periodo esta cerrado");

                if (ChkFecha.Checked)
                {
                    if (dt1.Columns.Count == 5)
                    {
                        if (dt1.Columns[0].ToString().ToLower() == "codigo" && dt1.Columns[1].ToString().ToLower() == "periodo" && dt1.Columns[2].ToString().ToLower() == "tipo" && dt1.Columns[3].ToString().ToLower() == "fecha" && dt1.Columns[4].ToString().ToLower() == "horas")
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                if (dt1.Rows[i][0].ToString().Trim() != "" && dt1.Rows[i][2].ToString().Trim() != "" && dt1.Rows[i][3].ToString().Trim() != "")
                                {
                                    if (Convert.ToInt32(dt1.Rows[i][0].ToString())==870572)
                                    {

                                    }
                                    int tipop = Convert.ToInt32(dt1.Rows[i][2].ToString());
                                    if (tipop != 26)//bono cumplimiento sin calculo
                                    {
                                        //if (!Neg_Planilla.insertarHorasExtrasxDia(Convert.ToInt32(dt1.Rows[i][0].ToString()), Convert.ToInt32(dt1.Rows[i][1].ToString()), Convert.ToInt32(dt1.Rows[i][2].ToString()), Convert.ToDateTime(dt1.Rows[i][3].ToString()), Convert.ToDecimal(dt1.Rows[i][4].ToString()), 0, 0, user))
                                        //{
                                        //    throw new Exception("Error registrando horas extra por fecha.");
                                        //}
                                        Neg_Marca.VerificarTipoIngresoExcedente(Convert.ToDateTime(dt1.Rows[i][3].ToString()), Convert.ToInt32(dt1.Rows[i][1].ToString()), Convert.ToInt32(dt1.Rows[i][0].ToString()), tipop, Convert.ToDecimal(dt1.Rows[i][4].ToString()), 0, 0, user);

                                    }

                                }
                                else
                                {
                                    alertSucces.Visible = false;
                                    alertValida.Visible = true;
                                    lblAlert.Visible = true;
                                    lblAlert.Text = "El archivo que esta intentando procesar posee celdas vacias";
                                }
                            }
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "PROCESADO SATISFACTORIAMENTE";
                        }
                    }
                }else {
                    if (dt1.Columns.Count == 6)
                    {
                        if (dt1.Columns[0].ToString().ToLower() == "codigo" && dt1.Columns[1].ToString().ToLower() == "periodo" && dt1.Columns[2].ToString().ToLower() == "semana" && dt1.Columns[3].ToString().ToLower() == "tipo" && dt1.Columns[4].ToString().ToLower() == "horas" && dt1.Columns[5].ToString().ToLower() == "tplanilla")
                        {
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                if (dt1.Rows[i][0].ToString().Trim() != "" && dt1.Rows[i][1].ToString().Trim() != "")
                                {
                                    if (!dtPeriodo[0].consolidar && dtPeriodo[0].tplanilla == 1)
                                    {
                                        semana = Convert.ToInt32(dt1.Rows[i][2].ToString());//para catorcenas se asignada semana correspondiente
                                    }
                                    else
                                    {
                                        semana = 1;//para periodo consolidados unica semana.
                                    }
                                    if (!Neg_Planilla.insertarHorasExtras(Convert.ToInt32(dt1.Rows[i][0].ToString()), Convert.ToInt32(dt1.Rows[i][1].ToString()), semana, Convert.ToInt32(dt1.Rows[i][3].ToString()), Convert.ToDecimal(dt1.Rows[i][4].ToString()), Convert.ToInt32(dt1.Rows[i][5].ToString())))
                                    {
                                        throw new Exception("Error registrando horas extra.");
                                    }
                                }
                                else
                                {
                                    alertSucces.Visible = false;
                                    alertValida.Visible = true;
                                    lblAlert.Visible = true;
                                    lblAlert.Text = "El archivo que esta intentando procesar posee celdas vacias";
                                }
                            }
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "PROCESADO SATISFACTORIAMENTE";
                            //recalculo de planilla
                            //string user = Convert.ToString(this.Page.Session["usuario"]);
                            //Neg_Planilla.GenerarProcesoPlanilla(Convert.ToInt32(txtPeriodo.Text.Trim()),user);

                        }

                        else
                        {
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "El archivo que esta intentando procesar no es el correcto";
                        }
                    }

                    else
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "El archivo que esta intentando procesar no es el correcto";
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
        
        //Carga de excel para ingresos y/o deducciones
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string connectionString = "";
                string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FileName);
                string fileLocation = HttpContext.Current.Server.MapPath(".").ToString() + @"\Trash\" + FileName;
                //this.divCargArch.Attributes.Remove("class");
                FileUpload1.SaveAs(FilePath);

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
                    //Bind Data to GridView
                    GridView1.Caption = Path.GetFileName(FilePath);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    Button2.Visible = true;
                    dt2 = dt;
                    Session["datos"] = dt2;
                }
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                FileUpload1.Focus();
            }
        }

        //Insercion del excel cargado de ingresos y/o deducciones
        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt1 = Session["datos"] as DataTable;
            //this.divCargArch.Attributes.Remove("class");

            string user = Convert.ToString(this.Page.Session["usuario"]);
            int periodo;

            try
            {
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                    throw new Exception("Periodo Invalido");

                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                //if (dtPeriodo[0].cerrado == 1)
                //    throw new Exception("El periodo esta cerrado");

                int semana = 0;

                if (dt1.Columns.Count == 6)//CATORCENAL Y COLUMNAS COMPLETAS
                {//VALIDANDO NOMBRE DE COLUMNAS
                    if (dt1.Columns[0].ToString().ToLower() == "tipo" && dt1.Columns[1].ToString().ToLower() == "codigoempleado" && dt1.Columns[2].ToString().ToLower() == "semana" && dt1.Columns[3].ToString().ToLower() == "idtipo" && dt1.Columns[4].ToString().ToLower() == "periodo" && dt1.Columns[5].ToString().ToLower() == "valor")
                    {
                        Neg_DevYDed NDevyDed = new Neg_DevYDed();
                        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
                        periodo = Convert.ToInt32(txtPeriodo.Text.Trim());
                        //DataTable dting = NDevyDed.ObtenerIngresocnDeduccionIBruto();
                        bool viatico = false;
                        foreach (DataRow item in dt1.Rows)//se inicializan valores
                        {
                            if (item[0].ToString().Trim() != "" && item[1].ToString().Trim() != "" && item[2].ToString().Trim() != "" && item[3].ToString().Trim() != "" && item[4].ToString().Trim() != "" && item[5].ToString().Trim() != "")
                            {
                          
                                if (!dtPeriodo[0].consolidar && dtPeriodo[0].tplanilla == 1)
                                {
                                    semana = Convert.ToInt32(item[2]);//para catorcenas se asignada semana correspondiente
                                }
                                else
                                {
                                    semana = 1;//para periodo consolidados unica semana.
                                }
                                //validar si aplica a viatico, de lo contrario su rubro debe ser 30 incentivo indirecto                               
                                int tipoingdeduc = Convert.ToInt32(item[3]);
                                viatico = false;
                                if (Convert.ToInt32(item[0]) == 1)//ingreso
                                {
                                    //int stids = NDevyDed.PlnValidarPersonalAplicaProteccion(Convert.ToInt32(item[1]));
                                    //if (stids == 0)//si no aplica a viatico se paga en planilla con impuestos
                                    //{
                                    //    //crear tabla equivalencias
                                    //    if (tipoingdeduc == 4)
                                    //    {
                                    //        viatico = true;
                                    //        tipoingdeduc = 29;
                                    //    }
                                    //    else if (tipoingdeduc == 14)
                                    //    {
                                    //        viatico = true;
                                    //        tipoingdeduc = 30;
                                    //    }

                                    //}
                                    //else if (tipoingdeduc == 4 || tipoingdeduc == 14)
                                    //{
                                    //    viatico = true;
                                    //}
                                    tipoingdeduc = Neg_Incentivos.PlnObtenerIDRubroIncentivo(Convert.ToInt32(item[1]), tipoingdeduc, 1);

                                    if (tipoingdeduc == 4 || tipoingdeduc == 14)
                                    {
                                        viatico = true;
                                    }

                                    if (viatico)
                                    {
                                        //if (!Neg_Incentivos.IncentivosHistoricoInsert(Convert.ToInt32(item[1]), 0, "0", "NA", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, Math.Ceiling(Convert.ToDecimal(item[5])), 0, 0, Math.Ceiling(Convert.ToDecimal(item[5])), periodo, semana, user, tipoingdeduc, "Carga de Ingreso manual", false))
                                        //{
                                        //    throw new Exception("Error al insertar ingreso");
                                        //}
                                        if (!Neg_Incentivos.PlnIncentivosxEmpleadoIns(periodo, semana, "0", Convert.ToInt32(item[1]), "", "NA", 0, 0, 0, Math.Ceiling(Convert.ToDecimal(item[5])), 0, 0, Math.Ceiling(Convert.ToDecimal(item[5])), user, tipoingdeduc, "Carga de Ingreso manual", false))
                                        {
                                            throw new Exception("Error al insertar ingreso");
                                        }
                                    }
                                    //if (Convert.ToInt32(item[3]) == 4 || Convert.ToInt32(item[3]) == 14)
                                    //{
                                    if (!NDevyDed.IngresosAplicaIBrutoBakIns(Convert.ToInt32(item[0]), Convert.ToInt32(item[1]), semana, tipoingdeduc, periodo, Math.Ceiling(Convert.ToDecimal(item[5]))))
                                        {
                                            throw new Exception("Error al insertar ingreso");
                                        }
                                        
                                   // }
                                }
                               
                                if (!NDevyDed.InsertarIngrDeduc(Convert.ToInt32(item[0]), Convert.ToInt32(item[1]), semana, tipoingdeduc, periodo, Math.Ceiling(Convert.ToDecimal(item[5])), user))
                                {
                                    throw new Exception("Error al insertar ingreso");
                                }
                                //SE INSERTA INGRESO RESPALDO SOLO PARA INGRESOS QUE APLICAN A DEDUCCION BASE
                                //DataTable dv = NDevyDed.verificarIngresocnDeduccionIBruto(Convert.ToInt32(item[3])).Tables[0];

                               /// if (dv.Rows.Count > 0)//viatico o incentivo, ingresos que deben protegerse
                               // {

                                   
                                //}
                            }
                        }
                        //recalculo de planilla
                        //Neg_Planilla.GenerarProcesoPlanilla(Convert.ToInt32(txtPeriodo.Text.Trim()), user);
                       
                    }
                    else
                    {
                        throw new Exception("El archivo que esta intentando procesar no es el correcto");
                    }
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
    }
}