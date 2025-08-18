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
    public partial class ProcesoPlanilla : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_PllanillaQuincenal Neg_PllanillaQuincenal = new Neg_PllanillaQuincenal();
        Neg_PlanillaVacaciones Neg_PlanillaVacaciones = new Neg_PlanillaVacaciones();
        Neg_PlanillaAguinaldo Neg_PlanillaAguinaldo = new Neg_PlanillaAguinaldo();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Permisos Neg_Permisos = new Neg_Permisos();
        Neg_DevYDed NDevyDed = new Neg_DevYDed();
        Globales Globales = new Globales();
        #endregion

        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                mvPlanillas.SetActiveView(wvUsuario);
                //obtenerPeriodoCatorcenal();
                //obtenerProcesos();
                obtenerTiposPlanilla();
                obtenerUbicaciones();
                Session["SaldoVac"] = 0;
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
        private void obtenerPeriodo()
        {
            int i = Convert.ToInt32(RbTipoPeriodos.SelectedValue);
            int ubicacion = 0;
            if (i == 1)
            {
                ubicacion = Convert.ToInt32(ddlUbicacion.SelectedValue.Trim());
            }
            else if (i == 3 || i == 4)
            {
                ubicacion = Convert.ToInt32(ddlUbicacionVac.SelectedValue.Trim());
            }
            else if (i == 5)
            {
                ubicacion = Convert.ToInt32(ddlUbicacionAguinaldo.SelectedValue.Trim());
            } else {
                ubicacion = 0;
            }
                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.cargarUltPeriodoAbieCat(i, Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()),ubicacion);

            int periodo = 0;
            if (dtPeriodo.Rows.Count > 0)
            {
                periodo = dtPeriodo[0].nperiodo;
                Session["fechaini"] = dtPeriodo[0]["fechaini"].ToString();
                Session["fechafin"] = dtPeriodo[0]["fechafin2"].ToString();
                Session["ubicacion"] = dtPeriodo[0].ubicacion;
            }
            procPlan.Visible = true;
            //divsemana.Visible = false;
            if (i == 1)
            {
                txtPeriodo.Text = periodo.ToString();
                if (dtPeriodo.Rows.Count > 0)
                {
                    if (!dtPeriodo[0].consolidar && Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()) == 1)
                    {
                       // divsemana.Visible = true;
                    }
                }
            }
            else if (i == 3 || i == 4)
            {
                txtPeriodoVacaciones.Text = periodo.ToString();

            }
            else if (i == 5)
            {
                txtPeriodoAguinaldo.Text = periodo.ToString();
            }
            else
            {
                procPlan.Visible = false;
            }
        }
        //private void obtenerProcesos()
        //{
        //    this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
        //    this.ddlProceso.DataMember = "procesos";
        //    this.ddlProceso.DataValueField = "codigo_depto";
        //    this.ddlProceso.DataTextField = "nombre_depto";
        //    this.ddlProceso.DataBind();           
        //}
        private void obtenerUbicaciones()
        {
            this.ddlUbicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacion.DataMember = "ubicaciones";
            this.ddlUbicacion.DataValueField = "codigo_ubicacion";
            this.ddlUbicacion.DataTextField = "nombre_ubicacion";
            this.ddlUbicacion.DataBind();

            this.ddlUbicacionAguinaldo.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacionAguinaldo.DataMember = "ubicaciones";
            this.ddlUbicacionAguinaldo.DataValueField = "codigo_ubicacion";
            this.ddlUbicacionAguinaldo.DataTextField = "nombre_ubicacion";
            this.ddlUbicacionAguinaldo.DataBind();

            this.ddlUbicacionVac.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacionVac.DataMember = "ubicaciones";
            this.ddlUbicacionVac.DataValueField = "codigo_ubicacion";
            this.ddlUbicacionVac.DataTextField = "nombre_ubicacion";
            this.ddlUbicacionVac.DataBind();
        }
        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(RbTipoPeriodos.SelectedValue);
            procPlan.Visible = false;
			

            if (i == 1)
            {
                mvPlanillas.SetActiveView(vwPlanillaOrdinaria);
				
                //this.divCargArch.Visible = true;
            }
            else if (i == 3 || i == 4)
            {
                mvPlanillas.SetActiveView(vwPlanillaVacaciones);
                empleado.Visible = false;
            }
            else if (i == 5)
            {
                mvPlanillas.SetActiveView(vwPlanillaAguinaldo);
            }
        }
        
        //Proceso de planilla catorcenal
        protected void btnProcesarPlanilla_Click(object sender, EventArgs e)
        {
            if (txtPeriodo.Text.Trim() != "" && txtPeriodo.Text.Trim() != "0")
            {
                try
                {
                    string user = Convert.ToString(this.Page.Session["usuario"]);


                    //cambiar a Neg_PlanillaTest para obtener horas trabajads separadas de PCGS
                    //fijar numero de periodo como parametro
                    Neg_Planilla.GenerarProcesoPlanilla(Convert.ToInt32(txtPeriodo.Text.Trim()),user);
                    
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Nomina Procesada satisfactoriamente";
                }
                catch (Exception ex)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = ex.Message;
                }
            }

            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Debe digitar un periodo para procesar la nomina";
            }
        }
        protected void btnCerrarPeriodo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPeriodo.Text.Trim()))
            {
                CerrarPeriodo(txtPeriodo.Text.Trim(), 1);
            }
            else
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Debe ingresar el periodo";

            }
        }
        //protected void btnElimPlanilla_Click(object sender, EventArgs e)
        //{
        //    if (txtPeriodo.Text.Trim() != "")
        //    {
        //        if (Neg_Planilla.EliminarPlanilla(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue)))
        //        {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "El Proceso de planilla para dicho periodo y dicha semana ha sido eliminado satisfactoriamente";
        //        }
        //        else
        //        {
        //            alertValida.Visible = true;
        //            lblAlert.Visible = true;
        //            lblAlert.Text = "Ocurrio un error al eliminar la planilla";
        //        }
        //    }
        //    else
        //    {
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = "Favor digite el periodo de la planilla a Eliminar";
        //    }
        //}             
        //Calculo de planilla quincenal
        //protected void btnprocesarPlnQuinc_Click(object sender, EventArgs e)
        //{
        //    string user = Convert.ToString(this.Page.Session["usuario"]);
        //    if (txtPeriodoQuinc.Text.Trim() != "" && ddlTiposPlanilla.SelectedValue == "2")
        //    {
        //        DataTable ds = new DataTable();
        //        int a = Neg_PllanillaQuincenal.procesarPlanillaQuincenal(Convert.ToInt32(txtPeriodoQuinc.Text.Trim()), Convert.ToInt32(ddlTiposPlanilla.SelectedValue), Convert.ToInt32(RbTipoPeriodos.SelectedValue),
        //            user, Convert.ToInt32(ddlUbicacion.SelectedValue));
        //        if (a == 0)
        //        {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Nomina Recalculada satisfactoriamente";
        //        }

        //        else
        //        {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Ocurrio Un Error Al Recalcular la Nomina";
        //        }
        //    }
        //}
        ////Recalculo planilla quincenal
        //protected void btnRecalcPlnQuinc_Click(object sender, EventArgs e)
        //{
        //    string user = Convert.ToString(this.Page.Session["usuario"]);
        //    if (txtPeriodoQuinc.Text.Trim() != "" && ddlTiposPlanilla.SelectedValue == "2")
        //    {
        //        DataTable ds = new DataTable();
        //        int a = Neg_PllanillaQuincenal.ReprocesarPlanillaQuincenal(Convert.ToInt32(txtPeriodoQuinc.Text.Trim()), Convert.ToInt32(ddlTiposPlanilla.SelectedValue), Convert.ToInt32(RbTipoPeriodos.SelectedValue),
        //            user, Convert.ToInt32(ddlUbicacion.SelectedValue));
        //        if (a == 0)
        //        {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Nomina Recalculada satisfactoriamente";
        //        }

        //        else
        //        {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Ocurrio Un Error Al Recalcular la Nomina";
        //        }
        //    }
        //}
        ////cierre periodo quincenal
        //protected void btnCerrarperiodoQuinc_Click(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(this.txtPeriodoQuinc.Text.Trim()))
        //    {
        //        CerrarPeriodo(txtPeriodoQuinc.Text.Trim(), 2);
        //    }
        //    else
        //    {
        //        alertSucces.Visible = false;
        //        LblSuccess.Visible = false;
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = "Debe ingresar el periodo";

        //    }
        //}        
        protected void btnProcAguinaldo_Click(object sender, EventArgs e)
        {
            procesarAguinaldo();
        }
        protected void btnRAgui_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPeriodoAguinaldo.Text.Trim()))
            {
                Neg_Planilla.EliminarPlanilla(Convert.ToInt32(txtPeriodoAguinaldo.Text.Trim()),1);
                Neg_PlanillaAguinaldo.EliminarHistoricoAguinaldo(Convert.ToInt32(txtPeriodoAguinaldo.Text.Trim()));
                Neg_PlanillaAguinaldo.EliminarAguinaldoMeses(Convert.ToInt32(txtPeriodoAguinaldo.Text.Trim()));
                procesarAguinaldo();
            }
            else
            {

                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Debe ingresar periodo";
            }
            
        }
        protected void btnCAgui_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPeriodoAguinaldo.Text.Trim()))
            {
                CerrarPeriodo(txtPeriodoAguinaldo.Text.Trim(),1);
            }
            else {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Debe ingresar el periodo";

            }
            }
        public void procesarAguinaldo() {
            try
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);
                DataTable dt;
                                
                //Proceso calculo de aguinaldo
                if (Neg_PlanillaAguinaldo.calcularAguinaldo(Convert.ToInt32(Session["ubicacion"]), Convert.ToInt32(RbTipoPeriodos.SelectedValue),
                    Convert.ToInt32(txtPeriodoAguinaldo.Text.Trim()), Session["fechaini"].ToString(), Session["fechafin"].ToString(), user))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Realizado satisfactoriamente";
                }
                else
                {
                    alertSucces.Visible = false;
                    LblSuccess.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Ocurrio un error";
                }
            }
            catch (Exception ex)
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }

        }        
        public void CerrarPeriodo(string periodo,int tplanilla) {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            
                if (Neg_Periodo.CerrarPeriodo(Convert.ToInt32(periodo), user, tplanilla))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "El Periodo ha sido cerrado satisfactoriamente";
                }
                else
                {
                    alertSucces.Visible = false;
                    LblSuccess.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Ocurrio un error al cerrar el periodo";
                }
            }
        protected void btnProcVacaciones_Click(object sender, EventArgs e)
        {
            procesarVacaciones();
        }
        public void procesarVacaciones()
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(Session["fechafin"].ToString());
            bool resp = false;

            try
            {                

                if (!string.IsNullOrEmpty(txtPeriodoVacaciones.Text.Trim()) && txtPeriodoVacaciones.Text.Trim()!="0")
                {
                    int ubicacion = Convert.ToInt32(Session["ubicacion"]);

                    if (!ChkDptoVac.Checked)//cuando es planilla masiva
                    {
                        bool todos = ChkExcel.Checked ? false : true;//
                        DataTable empleadoVac = Session["empleadoVac"] as DataTable;
                        Neg_PlanillaVacaciones.FiltrarLoteEmpleados(todos,empleadoVac ,ubicacion);
                        if (Neg_PlanillaVacaciones.calcularVacaciones(ubicacion, Convert.ToInt32(RbTipoPeriodos.SelectedValue), Convert.ToInt32(txtPeriodoVacaciones.Text), Session["fechaini"].ToString(), Session["fechafin"].ToString(), user,todos))
                        {
                            resp = true;
                        }
                    }
                    else
                    {//pago por empleado
                        //if (!string.IsNullOrEmpty(this.txtPeriodoVacaciones.Text.Trim()) && txtPeriodoVacaciones.Text.Trim() != "0")
                        //{
                            int codemp = Convert.ToInt32(txtcodigoEmpleadoVacaciones.Text.Trim());                         
                            double saldovac = Convert.ToDouble(TxtSaldoVac.Text.Trim());

                            if (saldovac <= Convert.ToDouble(Session["SaldoVac"]))
                            {
                                if (Neg_PlanillaVacaciones.calcularVacaciones(ubicacion, codemp,  saldovac, Convert.ToInt32(RbTipoPeriodos.SelectedValue),
                            Convert.ToInt32(txtPeriodoVacaciones.Text), Session["fechaini"].ToString(), Session["fechafin"].ToString(), user, 1))
                                {
                                    resp = true;
                                }
                            }
                            else {
                                throw new Exception("Las vacaciones a pagar deben ser menor o igual al Saldo Actual");
                            }                         
                       // }
                    }

                    if (resp)
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Planilla de Vacaciones Procesada satisfactoriamente";
                    }
                }
                else
                {
                    throw new Exception("Debe Ingresar periodo valido");
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void btnRVac_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPeriodoVacaciones.Text.Trim()) && txtPeriodoVacaciones.Text.Trim() != "0")
            {
                Neg_Planilla.EliminarPlanilla(Convert.ToInt32(txtPeriodoVacaciones.Text.Trim()), 1);
                Neg_PlanillaVacaciones.EliminarHistoricoVacaciones(Convert.ToInt32(txtPeriodoVacaciones.Text.Trim()), 0);
                procesarVacaciones();
            }
            else
            {

                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Debe ingresar periodo";
            }
        }    
        protected void btnCVac_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtPeriodoVacaciones.Text.Trim()) && txtPeriodoVacaciones.Text.Trim() != "0")
            {
                Neg_Permisos.ActualizarSaldoVacaciones(Convert.ToInt32(txtPeriodoVacaciones.Text.Trim()));
                CerrarPeriodo(txtPeriodoVacaciones.Text.Trim(), 1);
            }
            else
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Debe ingresar el periodo";

            }
        }
        protected void txtcodigoEmpleadoVacaciones_TextChanged(object sender, EventArgs e)
        {
            try
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                DataTable demp;
                Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(Session["fechafin"]);

                if (!string.IsNullOrEmpty(txtcodigoEmpleadoVacaciones.Text))
                {
                    demp = Neg_PlanillaVacaciones.obtenerEmpleadoPagoVacaciones(Convert.ToInt32(Session["ubicacion"]), Convert.ToInt32(txtcodigoEmpleadoVacaciones.Text.Trim()), Neg_Liquidacion.Globales.fechaR,false);

                    if (demp.Rows.Count > 0)
                    {
                        TxtNombreEmpVac.Text = demp.Rows[0]["nombre"].ToString();
                        this.TxtDeptoVac.Text = demp.Rows[0]["depto"].ToString();

                        DataTable vacaciones = Neg_Liquidacion.CalcularDiasVacaciones(Convert.ToInt32(txtcodigoEmpleadoVacaciones.Text.Trim()), Convert.ToDateTime(demp.Rows[0]["fecha_ingreso"]),1,userDetail.getIDEmpresa());
                        double diasVacaciones = 0;

                        if (vacaciones.Rows.Count > 0)
                        {
                            diasVacaciones = Convert.ToDouble(vacaciones.Rows[0]["saldovacaciones"]);
                            TxtSaldoVac.Text = diasVacaciones.ToString("n2");
                            Session["SaldoVac"] = TxtSaldoVac.Text;
                            TxtSaldoVac.ReadOnly = false;
                        }
                        else
                        {
                            TxtSaldoVac.Text = "0";
                            Session["SaldoVac"] = "0";
                            TxtSaldoVac.ReadOnly = true;
                        }

                    }
                    else
                    {
                        TxtSaldoVac.ReadOnly = true;
                        throw new Exception("No se encontro empleado o no aplica a pago de vacaciones");
                    }
                }
            }
            catch (Exception ex)
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        //protected void chkGeneral_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkGeneral.Checked)
        //    {
        //        chkXDepto.Checked = false;
        //        this.divDeptos.Visible = false;
        //    }

        //}
        //protected void chkXDepto_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkXDepto.Checked)
        //    {
        //        this.chkGeneral.Checked = false;
        //        this.divDeptos.Visible = true;
        //    }
        //    else
        //    {
        //        this.divDeptos.Visible = false;
        //        this.chkGeneral.Checked = true;
        //    }

        //}
        protected void ChkExcel_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkExcel.Checked)//solo codigos en excel
            {
                empleado.Visible = false;
                divexcel.Visible = true;
                btnRVac.Visible = true;
                btnCVac.Visible = true;
                ChkDptoVac.Checked = false;
            }
            else
            {
                empleado.Visible = false;
                divexcel.Visible = false;
                btnRVac.Visible = true;
                btnCVac.Visible = true;
            }
            Session["empleadoVac"] = null;
        }
        protected void ChkDptoVac_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkDptoVac.Checked)//por empleado
            {
                empleado.Visible = true;
                divexcel.Visible = false;
                btnRVac.Visible = false;
                btnCVac.Visible = false;
                ChkExcel.Checked = false;
            }
            else//todos
            {
                empleado.Visible = false;
                divexcel.Visible = false;
                btnRVac.Visible = true;
                btnCVac.Visible = true;
            }
            txtcodigoEmpleadoVacaciones.Text = "";
            TxtNombreEmpVac.Text = "";
            TxtDeptoVac.Text = "";
            TxtSaldoVac.Text = "0.00";
            Session["SaldoVac"] = 0;
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

                if (dt.Rows.Count > 0)
                {
                    if (dt.Columns.Count == 2)
                    {

                        if (dt.Columns[0].ToString().ToLower() == "codigo" && dt.Columns[1].ToString().ToLower() == "diasvac")
                        {
                            Session["empleadoVac"] = dt;
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
                FileVac.Focus();
            }
        }
        //protected void btnTipoPlanilla_Click(object sender, EventArgs e)
        //{
        //    if (ddlTiposPlanilla.SelectedValue == "1")
        //    {
        //        this.divCargArch.Attributes.Remove("class");
        //        this.procPlanSemana.Visible = true;
        //        this.procPlanQuincenal.Visible = false;

        //    }

        //    if (ddlTiposPlanilla.SelectedValue == "2")
        //    {
        //        this.procPlanQuincenal.Visible = true;
        //        this.procPlanSemana.Visible = false;

        //    }
        //}
        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

        protected void RbTipoPeriodos_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

        protected void ddlUbicacionVac_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

        protected void ddlUbicacionAguinaldo_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }
    }
}