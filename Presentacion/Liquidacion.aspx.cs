using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class Liquidacion : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        //ULTIMA MODIFICACION GRETHEL TERCERO 31-10-2016

        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_DevYDed IngryDeduc = new Neg_DevYDed();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                motivosRenuncia();
                LimpiarSession();
                SetValueCampos();
                txtFechaing.Text = DateTime.Now.ToShortDateString();
                txtFechRenuncia.Text = DateTime.Now.ToShortDateString();
                
            }
        }
        void CrearEstructuraGrid()
        {
            DataTable dtIngresos = new DataTable();
            dtIngresos.Columns.Add("Id");
            dtIngresos.Columns.Add("Ingreso");
            dtIngresos.Columns.Add("Total");
            dtIngresos.Columns.Add("AplicaInss");

            DataTable dtEgresos = new DataTable();
            dtEgresos.Columns.Add("ID");
            dtEgresos.Columns.Add("TipoDeduc");
            dtEgresos.Columns.Add("deduccionNombre");
            dtEgresos.Columns.Add("Total");

            DataTable dtEgresosDig = new DataTable();
            dtEgresosDig.Columns.Add("ID");
            dtEgresosDig.Columns.Add("TipoDeduc");
            dtEgresosDig.Columns.Add("deduccionNombre");
            dtEgresosDig.Columns.Add("Total");

            Session["Ingresos"] = dtIngresos;//persistir datos en la tabla
            Session["Egresos"] = dtEgresos;
            Session["EgresosDig"] = dtEgresosDig;
        }
        private void LimpiarSession()
        {
            //Limpiar variables de Session.
            Session["dtParametrosLiq"] = null;
            Session["dtfechaliq"] = null;
            Session["Meses"] = null;
            Session["Ingresos"] = null;
            Session["Egresos"] = null;
            Session["EgresosDig"] = null;

            CrearEstructuraGrid();
        }
        public void SetValueCampos()
        {
            if (this.txtDiasLab.Text == "") txtDiasLab.Text = "0";
            if (txtTotalVac.Text == "") txtTotalVac.Text = "0";
            if (txtSalMayor.Text == "") txtSalMayor.Text = "0";
            if (txtIndemnizacion.Text == "") txtIndemnizacion.Text = "0";
            if (txtDiasAguinaldo.Text == "") txtDiasAguinaldo.Text = "0";
            if (txtAguinaldo.Text == "") txtAguinaldo.Text = "0";
            if (txtSalMens.Text == "") txtSalMens.Text = "0";
            if (txtSalProm.Text == "") txtSalProm.Text = "0";
            if (txtDiasVac.Text == "") txtDiasVac.Text = "0";
            if (txtTotalVac.Text == "") txtTotalVac.Text = "0";
            if (txtTiempoPend.Text == "") txtTiempoPend.Text = "0";
            if (txtSalPend.Text == "") txtSalPend.Text = "0";
            if (this.txtSubTotal.Text == "") txtSubTotal.Text = "0";
            if (txtNetoRecib.Text == "") txtNetoRecib.Text = "0";
            if (this.TxtINSS.Text == "") TxtINSS.Text = "0";
            if (this.TxtIR.Text == "") TxtIR.Text = "0";
            if (hfIngresoMayorMes.Value == "") hfIngresoMayorMes.Value = "0";
            if (hfIngresoProMes.Value == "") hfIngresoProMes.Value = "0";
        }
        private void LimpiarCampos()
        {
            txtSalMayor.Text = "0.00";
            txtSalMens.Text = "0.00";
            txtSalProm.Text = "0.00";
            TxtIR.Text = "0.00";
            txtIndemnizacion.Text = "0.00";
            this.TxtINSS.Text = "0.00";
            txtDiasAguinaldo.Text = "0.00";
            txtAguinaldo.Text = "0.00";
            txtDiasVac.Text = "0.00";
            txtTotalVac.Text = "0.00";
            txtNetoRecib.Text = "0.00";

            txtTiempoPend.Text = "0.00";
            txtSalPend.Text = "0.00";
            txtSubTotal.Text = "0.00";
            txtObservEmpl.Text = "";

            txtDiasLab.Text = "0.00";
            //txtFechaing.Text = "";
            GVLiquidacion.DataSource = null;
            GVLiquidacion.DataBind();
                        
            DataTable Ingresos = Session["Ingresos"] as DataTable;
            Ingresos.Rows.Clear();
            
            DataTable Egresos = Session["Egresos"] as DataTable;
            Egresos.Rows.Clear();

            DataTable EgresosDig = Session["EgresosDig"] as DataTable;
            EgresosDig.Rows.Clear();

            GVIngresos.DataSource = null;
            GVIngresos.DataBind();
            GvEgresos.DataSource = null;
            GvEgresos.DataBind();

            lblDetallePrestamo.Text = "";
            lbldebe.Text = "";
            this.LblDetalleIngreso.Text = "";
            this.LblIngresosExtra.Text = " ";

        }
        private void motivosRenuncia()
        {
            this.ddlMotivo.DataSource = Neg_Liquidacion.cargarMotivos();
            this.ddlMotivo.DataMember = "motivo";
            this.ddlMotivo.DataValueField = "cod_motivo";
            this.ddlMotivo.DataTextField = "descripcion";
            this.ddlMotivo.DataBind();
        }
        private void obtenerIngresosLiq()
        {
            this.ddlTipo.DataSource = Neg_DevYDed.cargarIngresosLiq();
            this.ddlTipo.DataMember = "ingresos";
            this.ddlTipo.DataValueField = "idDevengado";
            this.ddlTipo.DataTextField = "devengadoNombre";
            this.ddlTipo.DataBind();
        }
        private void obtenerDeducciones()
        {
            this.ddlTipo.DataSource = Neg_DevYDed.cargarDeducciones();
            this.ddlTipo.DataMember = "deducciones";
            this.ddlTipo.DataValueField = "idDeduccion";
            this.ddlTipo.DataTextField = "deduccionNombre";
            this.ddlTipo.DataBind();

        }
        protected void txtCodEmp_TextChanged(object sender, EventArgs e)
        {
            LimpiarCampos();

            if (txtCodEmp.Text.Trim().Length != 0)
            {
                try
                {
                    DataTable dtDatosEmp = new DataTable();//Datos principales utilizados para la liquidacion,fechas,diasLaborados etc.
                    dtDatosEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtCodEmp.Text.Trim()), bandera);

                    btnProcLiq.Visible = true;
                    if (dtDatosEmp.Rows.Count > 0)//&& dtDatosEmp.Rows[0]["idestado"].ToString().Trim() == "1")//Si es un codigo válido.
                    {
                        txtNombre.Text = dtDatosEmp.Rows[0]["nombrecompleto"].ToString();
                        txtFechaing.Text = Convert.ToDateTime(dtDatosEmp.Rows[0]["fecha_ingreso"].ToString()).ToShortDateString();
                        txtFechRenuncia.Text = Convert.ToDateTime(dtDatosEmp.Rows[0]["fecha_egreso"].ToString()).ToShortDateString();
                        Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(txtFechRenuncia.Text);

                        int TipoSalario = Convert.ToInt32(dtDatosEmp.Rows[0]["idTipoSalario"].ToString());
                        CrearTipoPagoPend(TipoSalario);//Crear origen de datos para dtTipoPago,los tipos son Catorcenal y Quincenal.                        

                        if (dtDatosEmp.Rows[0]["idestado"].ToString().Trim() == "0")
                        {
                            btnProcLiq.Text = "ReProcesar";
                            //ReCargarLiquidacion();
                        }
                        else
                        {
                            btnProcLiq.Text = "Procesar";
                        }
                        //CargarDatos();//Obtengo la tabla meses y el detalle de la liquidacion para X persona. 
                        //aqui validar si esta cerrada, si-llamar poblar ingresos y deducciones guardadas
                        bool estatusliq = Convert.ToBoolean(dtDatosEmp.Rows[0]["cerrada"]);
                        if (!estatusliq)//abierta
                        {
                            CamposLiqEstatus(0);
                            CargarDatos(estatusliq);
                            LlenarGridDeducciones(CalcularIngresosAplicarDeduc());//Obtengo las deduccionesEspeciales pendientes.    
                        }
                        else
                        {
                            CamposLiqEstatus(1);
                            ReCargarLiquidacion(estatusliq);
                            LlenarGridIngrDeducLiq(Convert.ToInt32(txtCodEmp.Text.Trim()), Convert.ToDateTime(txtFechRenuncia.Text));
                        }
                        ActualizarLabelTotales();
                        ActualizarCampoInss();
                        CalcularNetoARecibir();
                    }
                    else
                    {
                        alertValida.Visible = true;
                        alertValida.InnerText = "No se han encontrado registros";
                    }
                }
                catch (Exception ex)
                {
                    LimpiarCampos();
                    alertValida.Visible = true;
                    alertValida.InnerText = ex.Message;
                }
            }
        }
        int bandera = 1;//Esta variable lo utilizara el procedimiento spLiquidacionDatosEmp,
                        //si es->1 Pantalla liquidacion individual,tomo la fecha de egreso real.
                        //si es->2 Pantalla VPasivoLaboral.aspx ,Solamennte para obtener salario mayor y promedio,tomo siempre el dia actual,(getdate()).    
        protected void btnProcLiq_Click(object sender, EventArgs e)
        {
            if (txtNetoRecib.Text.Trim().Length != 0)
            {
                try
                {
                    SetValueCampos();
                    decimal IrVacaciones = 0;
                    //Validacion para liquidaciones en negativo
                    decimal Neto = Convert.ToDecimal(txtNetoRecib.Text.Trim()) < 0 ? 0 : Convert.ToDecimal(txtNetoRecib.Text.Trim());
                    string user = Convert.ToString(this.Page.Session["usuario"]);
                    decimal ingresos = CalcularIngresosAplicarDeduc();
                    decimal egresos = CalcularEgresos();

                    if (Neg_Liquidacion.spInsertarLiquidaciones(Convert.ToInt32(txtCodEmp.Text.Trim()),
                        Convert.ToDateTime(txtFechRenuncia.Text.Trim()), Convert.ToInt32(ddlMotivo.SelectedValue.Trim()),
                        Convert.ToDecimal(hfIngresoMayorMes.Value), Convert.ToDecimal(txtSalMayor.Text.Trim()),
                        Convert.ToDecimal(hfIngresoProMes.Value), Convert.ToDecimal(txtSalProm.Text),
                        Convert.ToDecimal(txtAguinaldo.Text.Trim()), Convert.ToDecimal(txtDiasAguinaldo.Text.Trim()),
                        Convert.ToDecimal(txtTotalVac.Text.Trim()), Convert.ToDecimal(txtDiasVac.Text.Trim()),
                        Convert.ToDecimal(txtIndemnizacion.Text.Trim()), txtTiempoPend.Text.Trim(), Convert.ToDecimal(txtSalPend.Text.Trim()),
                        Neto, Convert.ToDateTime(txtFechaing.Text.Trim()), Convert.ToDecimal(this.TxtINSS.Text.Trim()), IrVacaciones, 0, txtObservEmpl.Text.Trim(), Convert.ToDecimal(txtDiasLab.Text.Trim()), user,ingresos,egresos))
                    {
                        btnProcLiq.Text = "Procesar";
                        InsertarMesesLiq();
                        InsertarIngresosDeducLiq();                       
                        //LimpiarSession();
                        Session["dtParametrosLiq"] = txtCodEmp.Text.Trim();
                        Session["dtfechaliq"] = txtFechRenuncia.Text.Trim();
                        Response.Redirect("VLiquidacion.aspx");
                    }
                    else
                    {
                        alertValida.Visible = true;
                        alertValida.InnerText = "Este Asociado ya fue Liquidado";
                    }
                }
                catch (Exception ex)
                {
                    alertValida.Visible = true;
                    alertValida.InnerText = ex.Message;
                }
            }
        }
       
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text.Trim().Length != 0 && ddlTipoConcepto.SelectedValue.Trim() != "0")
            {
                Agregar();
            }
        }
        protected void txtHorasp_TextChanged1(object sender, EventArgs e)
        {
            if (txtCodEmp.Text.Trim().Length != 0)
            {
                decimal FactorHora = 0, PagoPend = 0, SalarioMensual = 0,  HorasTurno = 0, Septimo = 0, ValorHora = 0, TiempoPend = 0;
                string TipoSalario = "";

                DataTable df = new DataTable();
                df = Session["factores"] as DataTable;

                //DataTable dtFactores = Neg_Catalogos.obtenerFactor();
                DataTable dtDatosEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtCodEmp.Text.Trim()), bandera);
                if (dtDatosEmp.Rows.Count != 0)
                {
                    SalarioMensual = Convert.ToDecimal(dtDatosEmp.Rows[0]["salariomensual"].ToString());
                    TipoSalario = dtDatosEmp.Rows[0]["idTipoSalario"].ToString();
                }
                if (TipoSalario == "1" || TipoSalario == "2")
                {
                    FactorHora = Convert.ToDecimal(df.Rows[0]["factor"].ToString());//Factor Hora Catorcenal
                }
                if (TipoSalario == "3")
                {
                    FactorHora = Convert.ToDecimal(df.Rows[4]["factor"].ToString()); //Factor Hora Quincenal
                }
                if (txtTiempoPend.Text.Trim() != "") {
                    TiempoPend = Convert.ToDecimal(txtTiempoPend.Text.Trim());
                }
                HorasTurno = 9.6M;
                Septimo = 8;
                ValorHora = (FactorHora * SalarioMensual);

                if (ddlTipoPago.SelectedValue.Trim() == "1")//Pago por Hora
                {
                    if (TipoSalario == "1" || TipoSalario == "2")//Catorcenal Fijo y Variable
                    {
                        PagoPend = (SalarioMensual * FactorHora) * TiempoPend;
                    }
                }
                if (ddlTipoPago.SelectedValue.Trim() == "2")//Pago por Dia
                {
                    if (TipoSalario == "1" || TipoSalario == "2")//Catorcenal Fijo y Variable
                    {
                        if (TiempoPend > 5 && TiempoPend < 10) { Septimo = 8; }
                        if (TiempoPend >= 10) { TiempoPend = 10; Septimo = 16; }
                        PagoPend = (TiempoPend * HorasTurno) * ValorHora;
                        PagoPend += Septimo * ValorHora;
                    }
                    if (TipoSalario == "3")//Quincenal.
                    {
                        PagoPend = (SalarioMensual / 30) * TiempoPend;
                    }
                }
                
                //if (PagoPend != 0)
                //{
                    txtSalPend.Text = Convert.ToString(PagoPend.ToString("n2"));
                    ActualizarCampoInss();                  
                    LlenarGridDeducciones(CalcularIngresosAplicarDeduc());
                    ActualizarLabelTotales();
                    CalcularNetoARecibir();
                //}
            }
        }       
        private void CrearTipoPagoPend(int TipoSalario)
        {
            DataTable dtTipoPago = new DataTable();
            dtTipoPago.Columns.Add("Id");
            dtTipoPago.Columns.Add("TipoPago");
            DataRow row = dtTipoPago.NewRow();
            if (TipoSalario == 1 || TipoSalario == 2)//Catorcenal
            {
                row["Id"] = 1;
                row["TipoPago"] = "Pago Hora";
                dtTipoPago.Rows.Add(row);

                row = dtTipoPago.NewRow();
                row["Id"] = 2;
                row["TipoPago"] = "Pago Dia";
            }

            if (TipoSalario == 3)//Quincenal
            {
                row = dtTipoPago.NewRow();
                row["Id"] = 2;
                row["TipoPago"] = "Pago Dia";
            }
            dtTipoPago.Rows.Add(row);
            ddlTipoPago.DataSource = dtTipoPago;
            this.ddlTipoPago.DataValueField = "Id";
            this.ddlTipoPago.DataTextField = "TipoPago";
            ddlTipoPago.DataBind();
        }      
        protected void ddlTipoConcepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoConcepto.SelectedValue.Trim() != "0")
            {
                if (ddlTipoConcepto.SelectedValue.Trim() == "1")//Ingreso
                {
                    obtenerIngresosLiq();
                }
                else//
                {
                    obtenerDeducciones();
                }
            }
        }
        protected void Btnrprint_Click(object sender, EventArgs e)
        {
            LimpiarSession();
            Session["dtParametrosLiq"] = txtCodEmp.Text.Trim();
            Session["dtfechaliq"] = txtFechRenuncia.Text.Trim();
            Response.Redirect("VLiquidacion.aspx");
        }
        protected void ddlMotivo_SelectedIndexChanged(object sender, EventArgs e) {
            try
            {
                ValidarIndemxMotivoLiq(ddlMotivo.SelectedValue.Trim());
            }
            catch (Exception ex)
            {
                LimpiarCampos();
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }
        void ValidarIndemxMotivoLiq(string codmotivo) {
            try
            {
                DataTable dt = Neg_Liquidacion.cargarMotivos().Tables[0];
                DataRow infomotivo = dt.Select(string.Format("[cod_motivo]= '{0}'", codmotivo)).FirstOrDefault();
                bool aplicaindem = Convert.ToBoolean(infomotivo["aplicaindem"]);

                double diasIndemnizacion = 0, indemnizacion = 0;

                if (!aplicaindem)
                {
                    txtIndemnizacion.Text = "0.00";
                }
                else
                {
                    double DiasTrabajados = (Neg_Liquidacion.Globales.fechaR - Convert.ToDateTime(txtFechaing.Text)).Days + 1;

                    if (DiasTrabajados >= 365)
                    {
                        diasIndemnizacion = Neg_Liquidacion.CalcularDiasPrestacion(Convert.ToDateTime(txtFechaing.Text), Neg_Liquidacion.Globales.fechaR, 1,0);
                        indemnizacion = diasIndemnizacion * Convert.ToDouble(txtSalProm.Text);
                        txtIndemnizacion.Text = indemnizacion.ToString("n2");
                    }
                }
                ActualizarSubTotal();
                LlenarGridDeducciones(CalcularIngresosAplicarDeduc());
                ActualizarLabelTotales();
                CalcularNetoARecibir();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void txtIndemnizacion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ActualizarSubTotal();
                LlenarGridDeducciones(CalcularIngresosAplicarDeduc());
                ActualizarLabelTotales();
                CalcularNetoARecibir();
            }
            catch (Exception ex)
            {
                LimpiarCampos();
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }

        }
        void ActualizarSubTotal()
        {
            try
            {
                decimal aguinaldo = 0, vacaciones = 0, indemnizacion = 0, subtotal = 0;

                indemnizacion = Convert.ToDecimal(txtIndemnizacion.Text);

                aguinaldo = Convert.ToDecimal(txtAguinaldo.Text);

                vacaciones = Convert.ToDecimal(txtTotalVac.Text);

                subtotal = indemnizacion + aguinaldo + vacaciones;

                this.txtSubTotal.Text = subtotal.ToString("n2");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private void AsignarCampos(DataTable dt,int codigo,bool estatusliq)
        {
            try
            {
                if (dt.Rows.Count != 0)
                {
                    decimal Indemnizacion = 0, Aguinaldo = 0, diasAguinaldo = 0, SalMensual = 0, SalPromedio = 0, SalMayor = 0, TotalVac = 0, InssVacaciones = 0, IRVacaciones = 0, Neto = 0, diasvac = 0, horaspend = 0, salariopend = 0;
                    string IndemnizacionDia = "";

                    IndemnizacionDia = string.IsNullOrEmpty(dt.Rows[0]["IndemnizacionDia"].ToString()) || Convert.ToDouble(dt.Rows[0]["IndemnizacionDia"]) == 0 ? Neg_Liquidacion.CalcularDiasPrestacion(Convert.ToDateTime(txtFechaing.Text), Neg_Liquidacion.Globales.fechaR, 1,0).ToString() : dt.Rows[0]["IndemnizacionDia"].ToString();
                    Neg_IR IR = new Neg_IR();
                    Neg_Periodo neg_Periodo = new Neg_Periodo();
                    DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
                    DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);//new DateTime(2021, 1, 4);
                    Neg_IR.Globales.dtIRHistorico = IR.ObtenerHistoricoIR(inicioano);

                    Aguinaldo = Convert.ToDecimal(dt.Rows[0]["Aguinaldo"].ToString());
                    diasAguinaldo = Convert.ToDecimal(dt.Rows[0]["AguinaldoDia"]);

                    TotalVac = Convert.ToDecimal(dt.Rows[0]["Vacaciones"].ToString());
                    diasvac = Convert.ToDecimal(dt.Rows[0]["vacacionesDia"]);
                    InssVacaciones = Convert.ToDecimal(dt.Rows[0]["INSS"].ToString());
                    Session["INSS"] = InssVacaciones;
                    IRVacaciones = Neg_Liquidacion.CalcularIRVacaciones(codigo, TotalVac, InssVacaciones); //Convert.ToDecimal(dt.Rows[0]["IR"].ToString());
                    Session["Ir"] = IRVacaciones;

                    SalMensual = Convert.ToDecimal(dt.Rows[0]["salMensual"].ToString());
                    SalPromedio = Convert.ToDecimal(dt.Rows[0]["salPromedioDia"].ToString());
                    SalMayor = Convert.ToDecimal(dt.Rows[0]["salMayorDia"].ToString());

                    horaspend = Convert.ToDecimal(dt.Rows[0]["horaspend"].ToString());
                    salariopend = Convert.ToDecimal(dt.Rows[0]["salariopend"].ToString());

                    txtIndemnizacion.Text = Convert.ToDecimal(dt.Rows[0]["Indemnizacion"]).ToString("n2");
                    txtDiasLab.Text = String.Format("{0:#,0.##}", IndemnizacionDia);
                    txtDiasAguinaldo.Text = diasAguinaldo.ToString("n2");
                    txtAguinaldo.Text = Aguinaldo.ToString("n2");
                    txtDiasVac.Text = diasvac.ToString("n2");
                    txtTotalVac.Text = TotalVac.ToString("n2");
                    this.TxtINSS.Text = InssVacaciones.ToString("n2");

                    txtSalMens.Text = SalMensual.ToString("n2");
                    txtSalProm.Text = SalPromedio.ToString("n2");
                    txtSalMayor.Text = SalMayor.ToString("n2");

                    txtTiempoPend.Text = horaspend.ToString("n2");
                    txtSalPend.Text = salariopend.ToString("n2");
                    txtObservEmpl.Text = dt.Rows[0]["Observacion"].ToString();
                    
                    if (dt.Rows[0]["CodMotivo"].ToString() == "0")
                    {
                        ddlMotivo.SelectedIndex = 0;
                    }
                    else {
                        ddlMotivo.SelectedValue = dt.Rows[0]["CodMotivo"].ToString();
                    }
                    if (!estatusliq)//abierta
                    {
                        ValidarIndemxMotivoLiq(ddlMotivo.SelectedValue.Trim());
                    }
                    Indemnizacion = Convert.ToDecimal(txtIndemnizacion.Text);

                    Neto = Indemnizacion + Aguinaldo + TotalVac;
                    txtSubTotal.Text = Convert.ToString(Neto.ToString("n2"));
                    Neto -= InssVacaciones;
                    if (estatusliq)//cerrada
                    {
                        txtNetoRecib.Text = dt.Rows[0]["Neto"].ToString(); 
                    } else {
                        txtNetoRecib.Text = Convert.ToString(Neto.ToString("n2"));
                    }
                    hfIngresoMayorMes.Value = String.Format("{0:#,0.##}", dt.Rows[0]["SalMayor"].ToString());
                    this.hfIngresoProMes.Value = String.Format("{0:#,0.##}", dt.Rows[0]["SalPromedio"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        void InsertarMesesLiq()
        {
            int anio = 0, mes = 0, diames = 0;
            decimal salario = 0, incentivo = 0, ingreso = 0, promediodias = 0, IR = 0, beneficio = 0;
            DateTime fecliquidacion;

            try
            {
                if (Session["Meses"] != null)
                {
                    int codigo = Convert.ToInt32(txtCodEmp.Text.Trim());
                    DataTable ds = Session["Meses"] as DataTable;

                    for (int i = 0; i < ds.Rows.Count; i++)
                    {
                        mes = Convert.ToInt32(ds.Rows[i]["MesNumero"].ToString());
                        anio = Convert.ToInt32(ds.Rows[i]["Anio"].ToString());
                        diames = Convert.ToInt32(ds.Rows[i]["diasMes"].ToString());
                        salario = Convert.ToDecimal(ds.Rows[i]["Salario"].ToString());
                        incentivo = Convert.ToDecimal(ds.Rows[i]["Incentivo"].ToString());
                        beneficio = string.IsNullOrEmpty(ds.Rows[i]["Beneficio"].ToString()) ? 0 : Convert.ToDecimal(ds.Rows[i]["Beneficio"].ToString());
                        ingreso = Convert.ToDecimal(ds.Rows[i]["Ingreso"].ToString());
                        promediodias = Convert.ToDecimal(ds.Rows[i]["PromedioDias"].ToString());
                        fecliquidacion = Convert.ToDateTime(txtFechRenuncia.Text.Trim());

                        if (Neg_Liquidacion.spInsertarLiquidacionMeses(codigo, anio, mes, diames, salario, incentivo, beneficio, ingreso, promediodias, fecliquidacion))
                        {
                            alertSucces.Visible = true;
                            alertSucces.InnerText = "Guardado Satisfactoriamente";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        void InsertarIngresosDeducLiq()
        {
            try
            {
                //insertar ingresos reportados en liquidacion
                DataTable dtIng = new DataTable();
                dtIng = Session["Ingresos"] as DataTable;
                int codigo = Convert.ToInt32(txtCodEmp.Text.Trim());

                for (int i = 0; i < dtIng.Rows.Count; i++)
                {
                    int id_tipo = 1;//Tipo Ingreso
                    int Tipo = Convert.ToInt32(dtIng.Rows[i]["Id"].ToString());
                    decimal Valor = Convert.ToDecimal(dtIng.Rows[i]["Total"].ToString());
                    Neg_Liquidacion.spInsertarPendientesLiq(codigo, id_tipo, Tipo, Valor, 0);
                }
                //insertar egresos reportados en liquidacion
                DataTable dtEg = new DataTable();
                dtEg = Session["Egresos"] as DataTable;

                //obtener el total de ingresos - impuestos
                decimal IngresoAplicaDeduc = CalcularIngresosAplicarDeduc() - Convert.ToDecimal(TxtINSS.Text.Trim());
                int banderad = 0;
                //validar segun ingresos que deudas se saldan                
                for (int c = 0; c < dtEg.Rows.Count; c++)
                {
                    int id_tipo = 2;//Tipo Egreso
                    int Tipo = Convert.ToInt32(dtEg.Rows[c]["tipoDeduc"].ToString());
                    int id = Convert.ToInt32(dtEg.Rows[c]["ID"]);
                    decimal Valor = Convert.ToDecimal(dtEg.Rows[c]["Total"].ToString());

                    if (IngresoAplicaDeduc > 0)
                    {                       
                        if (IngresoAplicaDeduc >= Valor)//cuota completa
                        { //plnIngresosYDeduccionesLiquidacion
                            Neg_Liquidacion.spInsertarPendientesLiq(codigo, id_tipo, Tipo, Valor, id);

                            //Actualiza saldo
                            if (dtEg.Rows[c]["ID"].ToString() != "0")
                            {//deducespeciales
                                Neg_Liquidacion.AplicarDeduccionPrestamos(Valor, codigo, id);
                            }
                            IngresoAplicaDeduc -= Valor;
                        }
                        else
                        {
                            //plnIngresosYDeduccionesLiquidacion
                            Neg_Liquidacion.spInsertarPendientesLiq(codigo, id_tipo, Tipo, IngresoAplicaDeduc, id);//cuota parcial 

                            if (dtEg.Rows[c]["ID"].ToString() != "0")//deducciones en sistema
                            {//deducespeciales
                                Neg_Liquidacion.AplicarDeduccionPrestamos(IngresoAplicaDeduc, codigo, id);
                            }
                            //plndeduccionespendliq
                            if (!Neg_Liquidacion.plnDeduccionesPendLiqIns(codigo, Convert.ToDateTime(txtFechRenuncia.Text), Tipo, (Valor - IngresoAplicaDeduc), id))
                            {
                                throw new Exception("Hubo problemas al registrar saldo deduccion pendiente");
                            }
                            IngresoAplicaDeduc -= IngresoAplicaDeduc;
                        }
                    }
                    else
                    {
                        //plndeduccionespendliq
                        if (!Neg_Liquidacion.plnDeduccionesPendLiqIns(codigo, Convert.ToDateTime(txtFechRenuncia.Text), Tipo, Valor, id))
                        {
                            throw new Exception("Hubo problemas al registrar saldo deduccion pendiente");
                        }
                    }
                }//termina for
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void ReCargarLiquidacion(bool estatusliq)
        {
            try
            {
                int codigo = Convert.ToInt32(txtCodEmp.Text.Trim());
                DateTime fechaliq = Convert.ToDateTime(txtFechRenuncia.Text);

                DataSet ds2 = Neg_Informes.spLiquidacionDetallado(codigo,fechaliq);
                if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                {
                    //prestaciones a pagadas
                    DataTable campos = ds2.Tables[0];
                    AsignarCampos(campos,codigo, estatusliq);

                    //tabla de meses
                    DataSet ds1 = Neg_Informes.spMesesLiquidacion(codigo,fechaliq);
                    CargarMeses(ds1.Tables[0]);
                }
            }
            catch (Exception ex)
            {
                LimpiarCampos();
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }

        }
        private void CargarDatos(bool estatusliq)
        {
            try
            {
                int codigo = Convert.ToInt32(txtCodEmp.Text.Trim());
                DataSet ds = Neg_Liquidacion.ObtenerDatosLiquidacion(codigo, 1, 0, 0, 0,0,true);//Obtengo la tabla de meses,y el detalle de liquidacion de X Persona.
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//Si no tiene los suficientes dias,no se podra calcular el detalle de la liquidacion.X Persona no aplica a liquidacion.
                {
                    CargarMeses(ds.Tables[0]);
                    DataTable campos = ds.Tables[1];
                    AsignarCampos(campos,codigo, estatusliq);
                }
                else//No aplica a liquidación.
                {
                    txtDiasLab.Text = String.Format("{0:#,0.##}", Neg_Liquidacion.CalcularDiasPrestacion(Convert.ToDateTime(txtFechaing.Text), Neg_Liquidacion.Globales.fechaR, 1,0).ToString());
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }
        private void CargarMeses(DataTable meses)
        {
            GVLiquidacion.DataSource = meses;
            GVLiquidacion.DataBind();
            Session["Meses"] = meses;
        }        
        void LlenarGridIngrDeducLiq(int codigo, DateTime fechaliq)
        {
            DataTable dtIngresos = new System.Data.DataTable();
            dtIngresos = Session["Ingresos"] as DataTable;
            dtIngresos.Rows.Clear();

            DataTable dv = Neg_Informes.spLiquidacionPendiente(codigo,fechaliq).Tables[0];
            for (int i = 0; i < dv.Rows.Count; i++)
            {
                dtIngresos.Rows.Add(0, dv.Rows[i]["devengadoNombre"], dv.Rows[i]["valor"]);
            }
            Session["Ingresos"] = dtIngresos;
            GVIngresos.DataSource = dtIngresos;
            GVIngresos.DataBind();

            DataTable dtEgresos = new System.Data.DataTable();
            dtEgresos = Session["Egresos"] as DataTable;
            dtEgresos.Rows.Clear();

            DataTable dd = Neg_Informes.spLiquidacionPendientesDeduc(codigo,fechaliq).Tables[0];
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                dtEgresos.Rows.Add(dd.Rows[i]["id"], dd.Rows[i]["tipoDeduc"], dd.Rows[i]["deduccionNombre"], dd.Rows[i]["valor"]);
            }

            Session["Egresos"] = dtEgresos;
            GvEgresos.DataSource = dtEgresos;
            GvEgresos.DataBind();
        }
        void LlenarGridDeducciones(decimal IngresoAplicaDeduc)
        {
            DataTable dtEgresos = new System.Data.DataTable();
            dtEgresos = Session["Egresos"] as DataTable;
            dtEgresos.Rows.Clear();

            Neg_Liquidacion.IngresosyDeduccionesLiqEliminar(Convert.ToInt32(txtCodEmp.Text.Trim()),Convert.ToDateTime(txtFechRenuncia.Text));
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //deducciones por cuotas registradas
            //DataSet dd = Neg_DevYDed.DeduccionesOrdinariasObtener(Convert.ToInt32(txtCodEmp.Text.Trim()),0,0);//Obtengo el detalle de la deduccion  
            DataTable dd = Neg_DevYDed.CalcularDetalleDeduccionesxEmp(Convert.ToInt32(this.txtCodEmp.Text.Trim()), 0, 0, IngresoAplicaDeduc,Convert.ToDateTime(txtFechRenuncia.Text), userDetail.getIDEmpresa());

            decimal monto = 0;
            if (dd.Rows.Count > 0)
            {
                for (int i = 0; i < dd.Rows.Count; i++)
                {

                 
                    //if (Convert.ToBoolean(dd.Rows[i]["aplicavalidez"]))
                    //{
                    //    //if (Convert.ToDateTime(txtFechRenuncia.Text) >= Convert.ToDateTime(dd.Rows[i]["fechaexpira"]))
                    //    //if (Convert.ToDateTime(txtFechRenuncia.Text) <= Convert.ToDateTime(dd.Rows[i]["fechaactiva"]) &&
                    //    //    Convert.ToDateTime(txtFechRenuncia.Text) <= Convert.ToDateTime(dd.Rows[i]["fechaexpira"]))
                    //    //{
                    //    //    continue;
                    //    //}
                    //    if (Convert.ToDateTime(txtFechRenuncia.Text)>= Convert.ToDateTime(dd.Rows[i]["fechaactiva"]) && Convert.ToDateTime(txtFechRenuncia.Text) < Convert.ToDateTime(dd.Rows[i]["fechaexpira"]))
                    //    {
                    //        dtEgresos.Rows.Add(dd.Rows[i]["id"], dd.Rows[i]["tipoDeduc"], dd.Rows[i]["deduccionNombre"], dd.Rows[i]["debe"]);
                    //    }
                    //    else
                    //    {
                    //        continue;
                    //    }
                    //}
                    //else
                    //{
                    //    dtEgresos.Rows.Add(dd.Rows[i]["id"], dd.Rows[i]["tipoDeduc"], dd.Rows[i]["deduccionNombre"], dd.Rows[i]["debe"]);
                    //}
                    dtEgresos.Rows.Add(dd.Rows[i]["id"], dd.Rows[i]["tipoDeduc"], dd.Rows[i]["deduccionNombre"], dd.Rows[i]["debe"]);
                }
            }
            //egresos digitados por el usuario
            DataTable dtEgresosDig = new System.Data.DataTable();
            dtEgresosDig = Session["EgresosDig"] as DataTable;

            for (int i = 0; i < dtEgresosDig.Rows.Count; i++)
            {
                dtEgresos.Rows.Add(dtEgresosDig.Rows[i]["ID"], dtEgresosDig.Rows[i]["tipoDeduc"], dtEgresosDig.Rows[i]["deduccionNombre"], dtEgresosDig.Rows[i]["Total"]);
            }

            Session["Egresos"] = dtEgresos;
            GvEgresos.DataSource = dtEgresos;
            GvEgresos.DataBind();

        }
        private void Agregar()
        {
            DataTable dtIngresos = new System.Data.DataTable();
            dtIngresos = Session["Ingresos"] as DataTable;
            DataTable dtEgresos = new System.Data.DataTable();
            dtEgresos = Session["Egresos"] as DataTable;

            DataTable dtEgresosDig = new System.Data.DataTable();
            dtEgresosDig = Session["EgresosDig"] as DataTable;
            string Id = "", Concepto = "";
            decimal Valor = 0;

            DataTable df = new DataTable();
            df = Session["factores"] as DataTable;

            if (txtNetoRecib.Text.Trim().Length == 0) txtNetoRecib.Text = "0";
         
            Id = ddlTipo.SelectedValue.Trim();
            Concepto = ddlTipo.SelectedItem.Text.Trim();
            Valor = Convert.ToDecimal(txtTotal.Text.Trim());
            this.ingDed.Visible = true;

            if (txtTotal.Text.Trim().Length != 0)
            {
                if (ddlTipoConcepto.SelectedValue.Trim() == "1")
                {
                    DataTable dev = Neg_DevYDed.verificarIngresocnDeduccionIBruto(Convert.ToInt32(Id)).Tables[0];

                    dtIngresos.Rows.Add(Id, Concepto, Valor,dev.Rows[0]["aplicaINSS"]);
                    Session["Ingresos"] = dtIngresos;
                    GVIngresos.DataSource = dtIngresos;
                    GVIngresos.DataBind();
                    GVIngresos.Columns[0].Visible = false;

                    ActualizarCampoInss();
                }
                else
                {
                    dtEgresos.Rows.Add(0, Id, Concepto, Valor);
                    dtEgresosDig.Rows.Add(0, Id, Concepto, Valor);
                    Session["Egresos"] = dtEgresos;
                    Session["EgresosDig"] = dtEgresosDig;
                    GvEgresos.DataSource = dtEgresos;
                    GvEgresos.DataBind();
                }
                txtTotal.Text = String.Empty;
            }
            LlenarGridDeducciones(CalcularIngresosAplicarDeduc());
            ActualizarLabelTotales();
            CalcularNetoARecibir();
        }
        void ActualizarLabelTotales()
        {
            decimal IngresoValue = Convert.ToDecimal(((DataTable)Session["Ingresos"]).AsEnumerable().Sum(row => Convert.ToDecimal(row["Total"])));
            this.LblDetalleIngreso.Text = "Ingresos Extra: ";
            this.LblIngresosExtra.Text = " " + IngresoValue.ToString("n2");

            decimal DebeValue = Convert.ToDecimal(((DataTable)Session["Egresos"]).AsEnumerable().Sum(row => Convert.ToDecimal(row["Total"])));
            lblDetallePrestamo.Text = "Deuda Total: ";
            lbldebe.Text = " " + DebeValue.ToString("n2");
        }
        decimal CalcularIngresosAplicarDeduc()
        {
            decimal IngresoAplicaDeduc = 0;
            try
            {
                //subtotal+salariopend
                IngresoAplicaDeduc = Convert.ToDecimal(this.txtSubTotal.Text.Trim()) + Convert.ToDecimal(this.txtSalPend.Text.Trim());
                //+ingresos en tabla
                IngresoAplicaDeduc += Convert.ToDecimal(((DataTable)Session["Ingresos"]).AsEnumerable().Sum(row => Convert.ToDecimal(row["Total"])));
            }
            catch (Exception ex)
            {
                return 0;
            }
            return IngresoAplicaDeduc;
        }
        decimal CalcularIngresosAplicarInss()
        {
            decimal IngresoAplicaInss = 0;
            try
            {
                //subtotal+salariopend, vacaciones con saldo positivo
                if (Convert.ToDecimal(this.txtTotalVac.Text.Trim()) > 0)
                {
                    IngresoAplicaInss = Convert.ToDecimal(this.txtTotalVac.Text.Trim()) + Convert.ToDecimal(this.txtSalPend.Text.Trim());

                }
                else
                {
                    IngresoAplicaInss = Convert.ToDecimal(this.txtSalPend.Text.Trim());
                }
                //+ingresos en tabla
                IngresoAplicaInss += Convert.ToDecimal(((DataTable)Session["Ingresos"]).AsEnumerable().Where(a => Convert.ToBoolean(a["aplicaINSS"]) == true).Sum(row => Convert.ToDecimal(row["Total"])));
            }
            catch (Exception ex)
            {
                return 0;
            }
            return IngresoAplicaInss;
        }
        void CalcularNetoARecibir()
        {
            decimal Neto = 0;
            try
            {
                Neto = CalcularIngresosAplicarDeduc();
                //-impuestos
                Neto -= Convert.ToDecimal(TxtINSS.Text.Trim());
                //-egresos en tabla
                Neto -= Convert.ToDecimal(((DataTable)Session["Egresos"]).AsEnumerable().Sum(row => Convert.ToDecimal(row["Total"])));

                //Resto el valor de la deduccion al Neto general.                
                txtNetoRecib.Text = Convert.ToString(Neto.ToString("n2"));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        decimal CalcularEgresos()
        {
            decimal egresos = 0;
            try
            {
                //-impuestos
                egresos += Convert.ToDecimal(TxtINSS.Text.Trim())+ Convert.ToDecimal(TxtIR.Text.Trim());
                //-egresos en tabla
                egresos += Convert.ToDecimal(((DataTable)Session["Egresos"]).AsEnumerable().Sum(row => Convert.ToDecimal(row["Total"])));
            }
            catch (Exception ex)
            {
               return 0;
            }
            return egresos;
        }
        void ActualizarCampoInss()
        {
            decimal Inss=0;

            //factores
            DataTable df = new DataTable();
            df = Neg_Catalogos.obtenerFactor();
            Session["factores"] = df;

            if (this.TxtINSS.Text.Trim().Length == 0)
            {
                TxtINSS.Text = "0";
            }
            else
            {
                Inss = CalcularIngresosAplicarInss() * (Convert.ToDecimal(df.Rows[1]["factor"]) / 100);
            }

            TxtINSS.Text = Convert.ToString(Inss.ToString("n2"));
        }
        void CamposLiqEstatus(int cerrada)
        {
            if (cerrada==1)
            {
                btnProcLiq.Visible = false;
                txtIndemnizacion.ReadOnly = true;
                txtObservEmpl.ReadOnly = true;
                ddlMotivo.Visible = false;            
                txtTiempoPend.ReadOnly = true;
                txtTotal.ReadOnly = true;
                btnAgregar.Visible = false;
                lblmotivo.Text = ddlMotivo.SelectedItem.Text;
                lblmotivo.Visible = true;
            }else
            {
                btnProcLiq.Visible = true;
                txtIndemnizacion.ReadOnly = false;
                txtObservEmpl.ReadOnly = false;
                ddlMotivo.Visible = true;               
                txtTiempoPend.ReadOnly = false;
                txtTotal.ReadOnly = false;
                btnAgregar.Visible = true;
                lblmotivo.Visible = false;
            }            
        }

       
    }
}