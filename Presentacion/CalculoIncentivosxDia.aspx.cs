using Datos;
using Microsoft.Reporting.WebForms;
using Negocios;
using System;
using System.Data;
using System.Linq;

namespace NominaRRHH.Presentacion
{
    public partial class CalculoIncentivosxDia : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Dato_Incentivos Dato_Incentivos = new Dato_Incentivos();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        //Neg_Empleados NegEmp = new Neg_Empleados();
        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Session["INCENTIVOCUT"] = null; //VARIABLE ALMACENARA INCENTIVOS PARA MODULOS QUE ALCANZARON META

                Session["INCENTIVOMODULO"] = null;
                Session["INCENTIVOSDIARIO"] = null;
                Session["INCENTIVOSDIARIOCUT"] = null;
                Session["INCENTIVOTOTAL"] = null;
                Session["INCENTIVOPENDIENTEMOD"] = null;
                Session["INCENTIVOPENDIENTECUT"] = null;
                Session["INCENTIVOPENDIENTE"] = null;
                Session["eficienciaMod"] = null;
                Session["eficienciaModHist"] = null;
                Session["tablaConfig"] = null;
                Session["tablaOQL"] = null;
                Session["LayoutxEstilo"] = null;
                Session["tablaIncentivos"] = null;
                Session["IngDed"] = null;
                Session["fechaini"] = null;
                Session["fechafin"] = null;
            }
        }
        protected void BtnDetalleEmpleados_Click(object sender, EventArgs e)
        {
            try
            {
                ImprimirDetalleEmpleados();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void BtnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtCorteAprobacion.Text))
                {
                    throw new Exception("Debe ingresar Fechas validas");
                }
                if (string.IsNullOrEmpty(txtPeriodo.Text) || txtPeriodo.Text.Trim() == "0")
                {
                    throw new Exception("Periodo Invalido");
                }

                GenerarPlanillaIncentivo(1);
                int periodo = Convert.ToInt32(txtPeriodo.Text);

                int semana = Convert.ToInt32(Session["semana"]);
                GuardarRegistrosGenerados(periodo, semana);


            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }

        }
        protected void BtnIncentivoTotal_Click(object sender, EventArgs e)
        {

            try
            {
                ImprimirIncentivoTotal();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void BtnConsultar2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TxtCodigo.Text))
                {
                    throw new Exception("Debe ingresar codigo valido");
                }
                if (Session["INCENTIVOCUT"] != null)
                {
                    if (Session["INCENTIVOSDIARIOCUT"] != null)
                    {
                        div1.Visible = false;
                        div7.Visible = false;
                        div2.Visible = false;
                        div3.Visible = false;
                        div4.Visible = true;
                        div6.Visible = false;

                        DataTable cutEmp = new DataTable();
                        cutEmp = Session["INCENTIVOSDIARIOCUT"] as DataTable;

                        var sortdt = cutEmp.AsEnumerable().Where(c => c.Field<int>("codigo_empleado") == Convert.ToInt32(TxtCodigo.Text))
                            .OrderBy(c => c.Field<string>("modulo"))
                            .ThenBy(d => d.Field<DateTime>("fecha_producido")).CopyToDataTable();

                        cargarReporte(sortdt, 4, ReportViewer4);
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
        protected void BtnDetalleModulos_Click(object sender, EventArgs e)
        {
            try
            {

                ImprimirDetalleModulo();

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void ChkCargarProtecciones_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCargarProtecciones.Checked)
            {
                divFile.Visible = true;
                divGrid.Visible = true;
            }
            else
            {
                divFile.Visible = false;
                divGrid.Visible = false;
            }
        }
        protected void BtnPendPago_Click(object sender, EventArgs e)
        {
            try
            {
                ImprimirIncentivoPend();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void btAlicarPlanilla_Click(object sender, EventArgs e)
        {
            //AQUI SE DEBE GUARDAR DETALLE INCENTIVO
            //INGRESOS PARA PLANILLA
            try
            {
                if (string.IsNullOrEmpty(txtPeriodo.Text) || txtPeriodo.Text.Trim() == "0")
                {
                    throw new Exception("Periodo Invalido");
                }
                if (Convert.ToBoolean(Session["periodoCerrado"]))
                {
                    throw new Exception("El periodo esta cerrado");
                }

                int semana = 1;

                semana = Convert.ToInt32(Session["semana"]);
                DataTable dt = Session["INCENTIVOTOTAL"] as DataTable;

                #region proceso aplicar
                //se inserta en plningresosydeducciones cuando ya se aplica pago
                if (!Neg_Incentivos.PlnIngresoPlanillaIns(dt, int.Parse(txtPeriodo.Text), semana, false))
                {
                    throw new Exception("Error al ingresar planilla incentivo");
                }
                string user = Convert.ToString(this.Page.Session["usuario"]);
                if (!Neg_DevYDed.plnAplicarPlanillaIncentivo(1, semana, 4, int.Parse(txtPeriodo.Text), user))
                {
                    throw new Exception("Error al insertar ingreso");
                }
                #endregion

                divApl.Visible = false;
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "La planilla de incentivos se ha registrado satisfactoriamente";

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void BtnConsultar1_Click(object sender, EventArgs e)
        {
            GenerarPlanillaIncentivo(0);
        }
        protected void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlReporteria.SelectedValue == "1")//mod
                {
                    ImprimirDetalleModulo();
                }
                else if (ddlReporteria.SelectedValue == "2")//emp
                {
                    ImprimirDetalleEmpleados();
                }
                else if (ddlReporteria.SelectedValue == "3")//tt
                {
                    ImprimirIncentivoTotal();
                }
                else if (ddlReporteria.SelectedValue == "5")//cump
                {
                    ImprimirCumplimientoModulo(int.Parse(txtPeriodo.Text));
                }
                else
                {
                    ImprimirIncentivoPend();
                    BtnAutorizarPagosPendientes.Visible = true;
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void BtnProcesar_Click(object sender, EventArgs e)
        {

        }
        protected void txtPeriodo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                {
                    throw new Exception("Periodo Invalido");
                }

                ObtenerFechasPeriodo(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlTipo.SelectedValue));
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        protected void BtnAutorizarPagosPendientes_Click(object sender, EventArgs e)
        {
            DataTable dtcut = Session["INCENTIVOPENDIENTEMOD"] as DataTable;

            string comentario = "Autorizado Calidad Interna";
            string user = Convert.ToString(this.Page.Session["usuario"]);

            // Verificar si el DataTable es nulo o vacío antes de llamar al método
            if (dtcut != null && dtcut.Rows.Count > 0)
            {
               
                if (Neg_Incentivos.PlnPagoIncentivoCortes(dtcut, comentario, user))
                {
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Los cortes han sido cargados correctamente";
                    dtcut.Clear();
                    Session["INCENTIVOPENDIENTEMOD"] = dtcut;
                }
                else
                {
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Error al insertar los registros";
                }
            }
        }
        #endregion

        #region METODOS DE DATOS
        void GenerarPlanillaIncentivo(int calculo)
        {
            try
            {
                DateTime ini;
                DateTime fin;
                DateTime fechaaprobacion;
                int periodo = 0;

                periodo = Convert.ToInt32(txtPeriodo.Text);
                fechaaprobacion = Convert.ToDateTime(TxtCorteAprobacion.Text);
                int semana = Convert.ToInt32(Session["semana"]);

                //Verifica que sea un periodo abierto.
                ObtenerFechasPeriodo(periodo, semana);
                ini = (DateTime)Session["fechaini"];
                fin = (DateTime)Session["fechafin"];
                //3112
                ObtenerParametrosIncentivos(ini, fin);
                ObtenerTablaEficiencia(ini, fin, fechaaprobacion, periodo, semana);

                ObtenerOQLxModulo(periodo, ini, fin); //esto va pero sin los rangos ya no iria

                procesoModulos(ini, fin, fechaaprobacion, periodo);
                procesoEmpleado(periodo);
                ObtenerIncentivoPendPago(ini, fin, fechaaprobacion, periodo, semana);

                procesoConsolidadoInc(calculo, ini, fin, periodo, semana);

                divApl.Visible = true;
                BtnDetalleModulos.Visible = true;
                BtnDetalleEmpleados.Visible = true;
                BtnIncentivoTotal.Visible = true;
                BtnPendPago.Visible = true;
                div5.Visible = true;
                div1.Visible = false;
                div7.Visible = false;
                div2.Visible = false;
                div3.Visible = true;
                div4.Visible = false;
                div6.Visible = false;
                divrpt.Visible = true;

                DataTable dt = Session["INCENTIVOTOTAL"] as DataTable;
                var sortdt = dt.AsEnumerable().OrderBy(c => c.Field<string>("modulo")).ThenBy(c => c.Field<int>("codigo_empleado")).CopyToDataTable();

                cargarReporte(sortdt, 2, ReportViewer2);
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void GuardarRegistrosGenerados(int periodo, int semana)
        {
            try
            {

                DataTable dt = Session["INCENTIVOTOTAL"] as DataTable;

                #region proceso generar
                if (!Neg_Incentivos.PlnPagoIncentivoDel(periodo, semana))
                {
                    throw new Exception("Error al eliminar datos de incentivo");
                }
                //BDRRHH
                //inserto en plnpagoincentivoempleado
                if (!Neg_Incentivos.PlnIncentivosxDPlanillaIns(dt, periodo, semana))
                {
                    throw new Exception("Error al ingresar planilla incentivo");
                }
                //plnpagoincentivoempleadobycut
                if (Session["INCENTIVOSDIARIOCUT"] != null)

                {
                    DataTable empcut = Session["INCENTIVOSDIARIOCUT"] as DataTable;
                    if (empcut.Rows.Count > 0)
                    {
                        DataRow[] resc = empcut.AsEnumerable().Where(c => c.Field<decimal>("horas") > 0 && !string.IsNullOrEmpty(c.Field<string>("corte"))).ToArray();

                        if (resc.Length > 0)
                        {
                            empcut = resc.CopyToDataTable();

                            if (!Neg_Incentivos.PlnPagoIncentivoEmpleadoByCutIns(empcut, periodo, semana))
                            {
                                throw new Exception("Error al ingresar incentivo empleado por corte");
                            }
                        }

                    }

                }
                //plnincentivopendpagoempleado
                if (Session["INCENTIVOPENDIENTECUT"] != null)
                {
                    DataTable emp = Session["INCENTIVOPENDIENTECUT"] as DataTable;
                    if (emp.Rows.Count > 0)
                    {
                        DataRow[] res = emp.AsEnumerable().Where(c => c.Field<decimal>("horas") > 0 && !string.IsNullOrEmpty(c.Field<string>("corte"))).ToArray();

                        if (res.Length > 0)
                        {
                            emp = res.CopyToDataTable();

                            if (!Neg_Incentivos.PlnIncentivoPendPagarxPeriodoIns(emp, periodo, semana))
                            {
                                throw new Exception("Error al ingresar incentivo pend. empleado");
                            }
                        }

                    }

                }
                if (Session["IngDed"] != null)
                {
                    DataTable IngDed = Session["IngDed"] as DataTable;
                    if (IngDed.Rows.Count > 0)
                    {
                        if (!Neg_Incentivos.IncentivoIngDedccLOGInsert(IngDed, periodo, semana))
                        {
                            throw new Exception("Error al ingresar detalle de ingreso y deducciones por empleado.");
                        }
                    }
                }

                //eficiencia
                if (!Neg_Incentivos.PlnEficienciaModDel(periodo))
                {
                    throw new Exception("Error al eliminar registros de eficiencia");
                }
                if (Session["eficienciaMod"] != null)
                {
                    DataTable ef = Session["eficienciaMod"] as DataTable;

                    Dato_Incentivos.PlnEficienciaModuloIns(ef);
                }
                //oql modulos
                if (!Neg_Incentivos.PlnOqlPeriodoPagoDel(periodo))
                {
                    throw new Exception("Error al eliminar registros de oql");
                }
                if (Session["CumplimientoOQL"] != null)
                {
                    DataTable oqlM = Session["CumplimientoOQL"] as DataTable;

                    // Insert masivo a la tabla PlnOqlPeriodoPago
                    Dato_Incentivos.PlnOqlPeriodoPagoIns(oqlM);
                }
                //bloque de insercion de historico de cortes pagados
                if (!Neg_Incentivos.PlnPagoIncentivobyCutDel(periodo, Convert.ToInt32(Session["semana"])))
                {
                    throw new Exception("Error al eliminar registros de incentivo");
                }
                if (Session["INCENTIVOCUT"] != null)
                {//inserto en krp
                    DataTable cut = Session["INCENTIVOCUT"] as DataTable;

                    if (!Neg_Incentivos.PlnPagoIncentivoByCutIns(cut, periodo, semana))
                    {
                        throw new Exception("Error al ingresar incentivo por corte");
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void ObtenerParametrosIncentivos(DateTime ini, DateTime fin)
        {
            try
            {
                DataTable li = Neg_Incentivos.PlnObtenerTablaIncentivo();
                Session["tablaIncentivos"] = li;
                DataTable Areas = Neg_Incentivos.PlnModuloIncentivoAreaSel();
                Session["ModuloArea"] = Areas;
                //layout por estilo
                DataTable lay = Neg_Incentivos.PlnObtenerLayoutxEstilo();
                Session["LayoutxEstilo"] = lay;
                //TABLA RANGO OQL
                DataTable lo = Neg_Incentivos.PlnObtenerRangoOql();
                Session["tablaOQL"] = lo;
                //TABLA RANGO adicionales
                DataTable lad = Neg_Incentivos.PlnObtenerRangoAdicionales();
                Session["tablaAdicional"] = lad;

                SetConfig();

                DataTable OpCrit = Neg_Incentivos.PlnObtenerOperacionCriticaSel();
                Session["OpCrit"] = OpCrit;
                DataTable tablaOpC = Neg_Incentivos.PlnTablaOperacionCriticaSel();
                Session["tablaOpC"] = tablaOpC;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        void ObtenerTablaEficiencia(DateTime ini, DateTime fin, DateTime corteaprobacion, int periodo, int semana)
        {
            try
            {

                DataSet DsMod = Neg_Incentivos.PlnObtenerEficienciaMod(ini, fin, corteaprobacion, periodo, semana);

                Session["eficienciaMod"] = DsMod.Tables[0];
                Session["CalidadMod"] = DsMod.Tables[1];

                DataTable eficienciaModHist = Neg_Incentivos.PlnObtenerEficienciaModuloHist(ini, fin);//Historico de cortes atrasados
                Session["eficienciaModHist"] = eficienciaModHist;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        void ObtenerOQLxModulo(int periodo, DateTime inicio, DateTime final)
        {
            try
            {
                DateTime ini = inicio.AddDays(-3); //revisar
                DateTime fin = final.AddDays(-1);

                DataTable CumplimientoOQL = Neg_Incentivos.PlnObtenerOQLMod(periodo, ini, fin);

                Session["CumplimientoOQL"] = CumplimientoOQL;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        void procesoModulos(DateTime ini, DateTime fin, DateTime corteaprobacion, int periodo)
        {
            try
            {
                DataTable dtcut = new DataTable();
                DataSet ds = Neg_Incentivos.plnObtenerProdAPagarxMod(ini, fin, corteaprobacion, 1, periodo);
                DataTable dtmod = ds.Tables[0];
                Session["INCENTIVOMODULO"] = dtmod;
                dtcut = ds.Tables[1];
                Session["INCENTIVOCUT"] = dtcut;
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void procesoEmpleado(int periodo)
        {//tengo que ver si el nuevo proceso de proteccion esta vacio, 
            //de lo contrario este es el que le voy a pasar al proceso de empleados
            try
            {
                if (Session["INCENTIVOCUT"] != null)
                {
                    DataTable dtmod = Session["INCENTIVOCUT"] as DataTable;

                    //rango de fechas produccion por modulo
                    DataRow[] fechas = dtmod.AsEnumerable().GroupBy(c => c.Field<DateTime>("fecha_producido")).Select(g => g.First()).ToArray();
                    DateTime fechaini = Convert.ToDateTime(fechas.OrderBy(f => f.Field<DateTime>("fecha_producido")).Take(1).First()["fecha_producido"]);
                    DateTime fechafin = Convert.ToDateTime(fechas.OrderByDescending(f => f.Field<DateTime>("fecha_producido")).Take(1).First()["fecha_producido"]);
                    //habilitar marcas del fin de semana para adicionales
                    DateTime finweekend;
                    if ((int)fechafin.DayOfWeek == 5)
                    {
                        finweekend = fechafin.AddDays(2);
                    }
                    else if ((int)fechafin.DayOfWeek == 6)
                    {
                        finweekend = fechafin.AddDays(1);
                    }
                    else
                    {
                        finweekend = fechafin;
                    }

                    DataTable lt = Neg_Incentivos.ObtenerHorasTrabajadasInc(fechaini, finweekend);
                    Session["HORASSEMANA"] = lt;

                    int semana = Convert.ToInt32(Session["semana"]);
                    DataSet ds = Neg_Incentivos.ProcesarIncentivosxDia(dtmod, periodo, semana, 1);
                    DataTable dt = ds.Tables[0];

                    Session["INCENTIVOSDIARIO"] = dt;
                    Session["INCENTIVOSDIARIOCUT"] = ds.Tables[1];

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        void procesoConsolidadoInc(int calculo, DateTime ini, DateTime fin, int periodo, int semana)
        {
            try
            {
                if (Session["INCENTIVOSDIARIO"] != null)
                {
                    DataTable dt = Neg_Incentivos.ObtenerIncentivoTotal(ini, fin, periodo, semana, calculo);
                    Session["INCENTIVOTOTAL"] = dt;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        void ObtenerIncentivoPendPago(DateTime ini, DateTime fin, DateTime fechaaprobacion, int periodo, int semana)
        {
            try
            {

                //if (Session["INCENTIVOTOTAL"] != null)
                //{
                //obtener produccion directa de rango de fecha,solo para bono de asistencia
                DataTable dtmod = Neg_Incentivos.plnObtenerProdAPagarxMod(ini, fin, fechaaprobacion, 3, periodo).Tables[1];
                Session["INCENTIVOPENDIENTEMOD"] = dtmod;
                DataTable inc = new DataTable();
                DataTable incEmp = new DataTable();
                if (dtmod.Rows.Count > 0)
                {
                    DataSet detpend = Neg_Incentivos.ProcesarIncentivosxDia(dtmod, periodo, semana, 3);
                    incEmp = detpend.Tables[0];
                    Session["INCENTIVOPENDIENTE"] = incEmp;
                    inc = detpend.Tables[1];
                    Session["INCENTIVOPENDIENTECUT"] = inc;
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void ObtenerFechasPeriodo(int periodo, int semana)
        {
            if (txtPeriodo.Text != "")
            {

                //MODIFICAR SP PARA ELIMINAR SOLO LOS TIPOS INGRESOS QUE SE ESTEN PROCESANDO EN ESTA PANTALLA
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                {
                    throw new Exception("Periodo Invalido");
                }

                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(periodo);
                Session["periodoCerrado"] = true;
                if (dtPeriodo.Rows.Count > 0)
                {
                    Session["periodoCerrado"] = dtPeriodo[0].cerrado;

                    divGen.Visible = true;
                    if (dtPeriodo[0].consolidar && dtPeriodo[0].tplanilla != 1)
                    {
                        semana = 1;
                    }
                    Session["semana"] = semana;
                    Session["fechaini"] = dtPeriodo[0].fechaini;
                    Session["fechafin"] = dtPeriodo[0].fechafin2.AddDays(-2);


                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "El periodo no existe o esta cerrado, para aplicar incentivos necesitara un periodo abierto.";
                }


            }
            else
            {
                throw new Exception("Debe ingresar un periodo valido");
            }

        }

        #endregion

        #region METODOS DE IMPRESION
        void ImprimirIncentivoPend()
        {
            DataTable dtcut = new DataTable();
            dtcut = Session["INCENTIVOPENDIENTEMOD"] as DataTable;

            div1.Visible = false;
            div7.Visible = false;
            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
            div6.Visible = true;
            var sortdt = dtcut.AsEnumerable().OrderBy(c => c.Field<string>("modulo"))
            .CopyToDataTable();
            cargarReporte(sortdt, 6, ReportViewer5);
        }

        void ImprimirDetalleModulo()
        {
            DataTable dtcut = new DataTable();
            dtcut = Session["INCENTIVOCUT"] as DataTable;
            div1.Visible = true;
            div7.Visible = false;
            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
            div6.Visible = false;
            var sortdt = dtcut.AsEnumerable().OrderBy(c => c.Field<string>("modulo"))
                        .ThenBy(d => d.Field<DateTime>("fecha_producido")).CopyToDataTable();

            cargarReporte(sortdt, 3, ReportViewer3);
        }
        void ImprimirCumplimientoModulo(int periodo)
        {
            div7.Visible = true;
            div1.Visible = false;
            div2.Visible = false;
            div3.Visible = false;
            div4.Visible = false;
            div6.Visible = false;

            DataTable dtcump = new DataTable();
            dtcump = Session["eficienciaMod"] as DataTable;
            DataRow[] eficMod = null;
            eficMod = dtcump.AsEnumerable().GroupBy(c => c.Field<string>("modulo").Trim()).Select(c => c.FirstOrDefault()).OrderBy(c => c.Field<string>("modulo")).ToArray();

            DataTable sortdt = new DataTable();

            DataTable dtprod = new DataTable();
            dtprod.Columns.Add("modulo", typeof(string));
            dtprod.Columns.Add("periodo", typeof(int));
            dtprod.Columns.Add("dzpagarSem", typeof(decimal));
            dtprod.Columns.Add("dzpendSem", typeof(decimal));
            dtprod.Columns.Add("dzprotSem", typeof(decimal));
            dtprod.Columns.Add("dztotalSem", typeof(decimal));
            dtprod.Columns.Add("eficienciaSem", typeof(decimal));

            decimal dzpagar = 0, dzpend = 0, dzprot = 0, dztotal = 0, efictotal = 0;
            foreach (DataRow dr1 in eficMod)
            {
                dzpagar = dtcump.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == dr1["modulo"].ToString().Trim()).Sum(r => r.Field<decimal>("dzpagarDia"));
                dzpend = dtcump.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == dr1["modulo"].ToString().Trim()).Sum(r => r.Field<decimal>("dzpendDia"));
                dzprot = dtcump.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == dr1["modulo"].ToString().Trim()).Sum(r => r.Field<decimal>("dzprotDia"));
                dztotal = dtcump.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == dr1["modulo"].ToString().Trim()).Sum(r => r.Field<decimal>("dztotalDia"));
                efictotal = Convert.ToDecimal(dr1["eficienciaSem"]);

                dtprod.Rows.Add(dr1["modulo"].ToString().Trim(), periodo, dzpagar, dzpend, dzprot, dztotal, efictotal);
            }

            cargarReporte(dtprod, 5, ReportViewer6);
        }
        void ImprimirDetalleEmpleados()
        {
            if (Session["INCENTIVOCUT"] != null)
            {
                if (Session["INCENTIVOSDIARIO"] != null)
                {
                    div1.Visible = false;
                    div7.Visible = false;
                    div2.Visible = true;
                    div3.Visible = false;
                    div4.Visible = false;
                    div6.Visible = false;

                    DataTable pagofiltrado = new DataTable();
                    pagofiltrado = Session["INCENTIVOSDIARIO"] as DataTable;

                    var sortdt = pagofiltrado.AsEnumerable().OrderBy(c => c.Field<string>("modulo"))
                        .ThenBy(d => d.Field<DateTime>("fecha"))
                        .ThenBy(f => f.Field<int>("codigo_empleado")).CopyToDataTable();

                    cargarReporte(sortdt, 1, ReportViewer1);
                }

            }
        }
        void ImprimirIncentivoTotal()
        {
            if (Session["INCENTIVOTOTAL"] != null)
            {
                div1.Visible = false;
                div2.Visible = false;
                div3.Visible = true;
                div4.Visible = false;
                div6.Visible = false;
                DataTable dt = Session["INCENTIVOTOTAL"] as DataTable;
                var sortdt = dt.AsEnumerable().OrderBy(c => c.Field<string>("modulo")).ThenBy(c => c.Field<int>("codigo_empleado"))
                .CopyToDataTable();

                cargarReporte(sortdt, 2, ReportViewer2);
            }
        }
        #endregion

        #region METODOS AUXILIARES
        private void SetConfig()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("fechaini", typeof(DateTime));
            dt.Columns.Add("fechafin", typeof(DateTime));
            dt.Columns.Add("incluyeFinSem", typeof(int));
            dt.Columns.Add("excluyeDias", typeof(int));
            dt.Columns.Add("diasPagar", typeof(decimal));

            DateTime fechaini = (DateTime)Session["fechaini"];
            DateTime fechafin = (DateTime)Session["fechafin"];

            decimal excluyedias = 0;
            decimal diasapagar = 5m;

            if (txtDiasaPagar.Text == "")
            {
                txtDiasaPagar.Text = "5";
                diasapagar = 5m;
            }

            diasapagar = decimal.Parse(txtDiasaPagar.Text);
            if (diasapagar < 5m)
                excluyedias = 5m - diasapagar;


            dt.Rows.Add(fechaini, fechafin, 1, excluyedias, diasapagar);

            Session["tablaConfig"] = dt;

        }
        public void cargarReporte(DataTable dt, int rpt, ReportViewer window)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            window.ProcessingMode = ProcessingMode.Local;
            window.LocalReport.DataSources.Clear();
            if (rpt == 1)//incentivos por dia
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoDiario.rdlc");
            }
            else if (rpt == 2)//calculo total a pagar incluyendo bono
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoTotal.rdlc");
            }
            else if (rpt == 3)//calculo pr modulo
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoModulo.rdlc");
            }//IncentivoDiarioEmpDet
            else if (rpt == 4)
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoDiarioEmpDet.rdlc");
            }
            else if (rpt == 5)
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoCumplimiento.rdlc");
            }
            else
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoPendientePago.rdlc");
            }
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }

        #endregion
    }

}
