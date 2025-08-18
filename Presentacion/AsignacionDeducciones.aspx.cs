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
    public partial class AsignacionDeducciones : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        //ULTIMA MODIFICACION GRETHEL TERCERO 25/10/2016
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Dato_Planilla Dato_Planilla = new Dato_Planilla();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //obtenerPeriodo();
                obtenerDeducciones();
                txtFecSol.Text = DateTime.Now.ToShortDateString();
                this.txtFecAut.Text = DateTime.Now.ToShortDateString();
                txtFecha.Text = DateTime.Now.ToShortDateString();
                TxtFecha2.Text = DateTime.Now.ToShortDateString();
                chkPorc.Checked = false;
                chkRecurrente.Checked = false;
            }
        }
        private void obtenerDeducciones()
        {
            this.ddlTipDeduc.DataSource = Neg_DevYDed.cargarDeducciones();
            this.ddlTipDeduc.DataMember = "deducciones";
            this.ddlTipDeduc.DataValueField = "idDeduccion";
            this.ddlTipDeduc.DataTextField = "deduccionNombre";
            this.ddlTipDeduc.DataBind();

            //this.ddlTipDeduc2.DataSource = Neg_DevYDed.cargarDeducciones();
            //this.ddlTipDeduc2.DataMember = "deducciones";
            //this.ddlTipDeduc2.DataValueField = "idDeduccion";
            //this.ddlTipDeduc2.DataTextField = "deduccionNombre";
            //this.ddlTipDeduc2.DataBind();
        }
        private void obtenerPeriodo()
        {
            try {
                if (!string.IsNullOrEmpty(this.txtcodigoAsig.Text.Trim()))
                {
                    DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtcodigoAsig.Text);
                    DataTable ubicacion = Neg_Catalogos.seleccionarUbicacionesxCod(Convert.ToInt32(DetEmpleados.Rows[0]["codigo_ubicacion"]));
                    dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1,Convert.ToInt32(ubicacion.Rows[0]["tplanilla"]), 0);
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
            catch (Exception ex) {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "No hay periodo abierto que este vigente";
            }
        }
        void limpiarCampos() {
            txtTotal.Text = "";
            txtCuotas.Text = "";
            TxtNetoProm.Text = "0.00";
            txtIndemnizacion.Text = "0.00";
            txtAguinaldo.Text = "0.00";
            txtTotalVac.Text = "0.00";
            txtSubTotal.Text = "0.00";
            TxtSaldoDisp.Text = "0.00";
            chkPorc.Checked = false;
            chkvalidez.Checked = false;
            this.chkRecurrente.Checked = false;
        }
        public bool validar()
        {
            try {
                if (txtcodigoAsig.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un valor valido";
                    //txtCodigo.Focus();
                    return false;
                }

                if (txtPeriodo.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un valor valido";
                    txtPeriodo.Focus();
                    return false;
                }

                if (Convert.ToInt32(txtPeriodo.Text.Trim()) < Convert.ToInt32(Session["periodo"].ToString()))
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "No puede asignar deducciones a periodos cerrados";
                    txtPeriodo.Focus();
                    return false;
                }

                if ((txtTotal.Text.Trim() == "" || txtTotal.Text.Trim() == "0") && chkRecurrente.Checked== false)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un valor valido";
                    txtTotal.Focus();
                    return false;
                }

                if (txtCuotas.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un valor valido";
                    txtCuotas.Focus();
                    return false;
                }

                if (ddlTipDeduc.SelectedValue == "15" && Convert.ToDecimal(txtCuotas.Text.Trim())!=0)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "El prestamo de adelanto indemnizacion se asigna con cuota cero";
                    txtCuotas.Text = "0.00";
                    txtCuotas.Focus();
                    return false;
                }
                //if (ddlTipDeduc.SelectedValue == "15" && Convert.ToDecimal(txtIndemnizacion.Text.Trim()) < Convert.ToDecimal(this.txtTotal.Text.Trim()))
                //{
                //    alertValida.Visible = true;
                //    lblAlert.Visible = true;
                //    lblAlert.Text = "El prestamo de adelanto indemnizacion no puede ser mayor a la indemnizacion acumulada";
                //    txtCuotas.Focus();
                //    return false;
                //}
                if (Convert.ToDecimal(txtTotal.Text.Trim()) < Convert.ToDecimal(this.txtCuotas.Text.Trim()) && chkRecurrente.Checked== false)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "La cuota no puede ser mayor que el total de la deuda";
                    txtCuotas.Focus();
                    return false;
                }
            }
            catch (Exception ex) {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Formato incorrecto en datos ingresados";
                return false;           
            }
            return true;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                try {
                    string user = Convert.ToString(this.Page.Session["usuario"]);
                    int porcentual = chkPorc.Checked ? 1 : 0;
                    int recurrente = chkRecurrente.Checked ? 1 : 0;
                    int periodovalidez = chkvalidez.Checked ? 1 : 0;
                    if (Neg_DevYDed.AsignarDeduccionesEmpleado(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlTipDeduc.SelectedValue), Convert.ToDecimal(txtTotal.Text.Trim()), Convert.ToDecimal(txtCuotas.Text.Trim()), txtFecSol.Text.Trim(), txtFecAut.Text.Trim(), user,porcentual,recurrente, periodovalidez, txtFecha.Text.Trim(), TxtFecha2.Text.Trim(), txtObservEmpl.Text))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Deduccion Asignada Satisfactoriamente";
                        //Actualizar los campos
                        ObtenerIngresosEgresosVigentes();                        
                        limpiarCampos();

                        //aqui ingreso i bruto
                        //se verifica si la deduccion aplica a ingreso bruto
                        int existe = Neg_DevYDed.verificarAplicaDeduccionIBruto(Convert.ToInt32(ddlTipDeduc.SelectedValue));
                        if (existe>0)
                        {
                            DataTable dt1 = new DataTable();                            
                            DataSet ds=new DataSet();

                            ds = Neg_DevYDed.IngresosPeriodoxTipoSel(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),0, Convert.ToInt32(ddlTipDeduc.SelectedValue));

                            if (ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                            {
                                dt1 = ds.Tables[0];
                                Neg_DevYDed.RegistrarIngresoEgresoAsociado(Convert.ToInt32(txtPeriodo.Text.Trim()),0,dt1,user);                                                                
                            }
                        }
                    }
                    else
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Ya Existe Una Deduccion De Este Tipo Para Este Periodo y Empleado";
                    }
                }
                catch (Exception ex) {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Los campos contienen caracteres invalidos";
                }
            }
        } 
        protected void GVDeducciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow selectedRow = this.GVDeducciones.Rows[Convert.ToInt32(e.RowIndex)];
            int indice = selectedRow.DataItemIndex;
            int id = Convert.ToInt32(GVDeducciones.Rows[GVDeducciones.TabIndex].Cells[1].Text.Trim());
            if (Neg_DevYDed.DeduccionOrdinariaEliminar(id))
            {
                cargarDeducciones();
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Error Al Eliminar El Registro";
            }
        }
        protected void GVDeducciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["estado"].ToString().Trim()!="2")
            {
                btnGuardar.Visible = false;
                btnEditar.Visible = true;
                btnDeshabilitar.Visible = true;
                btnSkip.Visible = true;
                txtCuotas.Text = GVDeducciones.Rows[GVDeducciones.SelectedIndex].Cells[3].Text.Trim();
                this.txtTotal.Text = GVDeducciones.Rows[GVDeducciones.SelectedIndex].Cells[4].Text.Trim();
                editarhide.Visible = false;
                editarhide2.Visible = false;
                editarhide3.Visible = false;
                editarhide4.Visible = false;
                editarhide6.Visible = false;
                editarhide5.Visible = true;
                Session["ID"] = GVDeducciones.Rows[GVDeducciones.SelectedIndex].Cells[1].Text.Trim();
                Session["IdDeduc"] = GVDeducciones.DataKeys[GVDeducciones.SelectedIndex][0].ToString();
            }
            else
            {
                editarhide5.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "El empleado no esta activo";
            }
            
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            if (Neg_DevYDed.EditarDeduccionesxEmpleado(Convert.ToInt32(Session["ID"].ToString()), Convert.ToDecimal(this.txtTotal.Text.Trim()), Convert.ToDecimal(txtCuotas.Text.Trim()), user))
            {                
                //aqui ingreso i bruto
                //se verifica si la deduccion aplica a ingreso bruto
                int existe = Neg_DevYDed.verificarAplicaDeduccionIBruto(Convert.ToInt32(Session["IdDeduc"].ToString()));
                if (existe > 0)
                {
                    DataTable dt1 = new DataTable();
                    DataSet ds = new DataSet();

                    //se eliminan cuotas aplicadas y se inserta ingreso bruto original
                    if (!Neg_DevYDed.ReestablecerIngresosIBruto(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString())))
                    {
                        throw new Exception("Error al reestablecer ingreso aplicables a deduccion");

                    }
                    IUserDetail userDetail = UserDetailResolver.getUserDetail();
                    //Se actualiza ultima cuota
                    if (!Dato_Planilla.ActualizarEstadoCuentaDeudaEmp(Convert.ToInt32(Session["ID"].ToString()), 2, Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString()), Convert.ToInt32(txtPeriodo.Text.Trim()), 0, 1, userDetail.getIDEmpresa()))
                    {
                        throw new Exception("Hubo problemas al deducir cuota");
                    }

                    ds = Neg_DevYDed.IngresosPeriodoxTipoSel(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),0, Convert.ToInt32(Session["IdDeduc"].ToString()));

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        dt1 = ds.Tables[0];
                        Neg_DevYDed.RegistrarIngresoEgresoAsociado(Convert.ToInt32(txtPeriodo.Text.Trim()),0, dt1, user);
                    }
                }
                
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                cargarDeducciones();
                LblSuccess.Text = "Deduccion Actualizada Satisfactoriamente";
                HabilitarCampos();

            }
        }
        private void HabilitarCampos()
        {
            txtTotal.Text = "";
            txtCuotas.Text = "";
            editarhide.Visible = true;
            editarhide2.Visible = true;
            editarhide3.Visible = true;
            editarhide4.Visible = true;
            editarhide6.Visible = true;
            editarhide5.Visible = true;
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            btnDeshabilitar.Visible = false;
            btnSkip.Visible = false;
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Neg_DevYDed.DeshabilitarDeuda(Convert.ToInt32(Session["ID"].ToString())))
                {
                    int existe = Neg_DevYDed.verificarAplicaDeduccionIBruto(Convert.ToInt32(Session["IdDeduc"].ToString()));
                    if (existe > 0)
                    {
                        //aqui ingreso i bruto
                        //se verifica si tiene deduccion aplicada a ingreso bruto                   
                        if (!Neg_DevYDed.ReestablecerIngresosIBruto(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString())))
                        {
                            throw new Exception("Error al reestablecer ingreso aplicables a deduccion");

                        }
                        IUserDetail userDetail = UserDetailResolver.getUserDetail();
                        //Se actualiza DEDUCCION DE INGRESO ASOCIADO
                        if (!Dato_Planilla.ActualizarEstadoCuentaDeudaEmp(Convert.ToInt32(Session["ID"].ToString()), 2, Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString()), Convert.ToInt32(txtPeriodo.Text.Trim()), 0, 1, userDetail.getIDEmpresa()))
                        {
                            throw new Exception("Hubo problemas al deducir cuota");
                        }
                    }

                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    cargarDeducciones();
                    LblSuccess.Text = "Deduccion Deshabilitada Satisfactoriamente";
                    HabilitarCampos();
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        private void ObtenerIngresosEgresosVigentes()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtcodigoAsig.Text.Trim()))
                {
                    obtenerPeriodo();
                    Neg_Liquidacion.Globales.fechaR = DateTime.Now;                    

                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtcodigoAsig.Text.Trim()), 2);
                   
                    if (dtEmp.Rows.Count > 0 )
                    {
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();
                        Session["estado"] = dtEmp.Rows[0]["idestado"].ToString().Trim();
                        if (Session["estado"].ToString().Trim() == "0")
                        {
                            btnGuardar.Visible = false;
                        }
                        else
                        {
                            btnGuardar.Visible = true;
                        }

                        DataSet ds = Neg_Liquidacion.ObtenerDatosLiquidacion(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()),0,0,0,0,0,true);//Obtengo la tabla de meses,y el detalle de liquidacion de X Persona.
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//Si no tiene los suficientes dias,no se podra calcular el detalle de la liquidacion.X Persona no aplica a liquidacion.
                        {

                            DataTable campos = ds.Tables[1];
                            AsignarCampos(campos);

                        }
                        else//No aplica a liquidación.
                        {
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "No se han encontrado datos de Liquidacion Acumulada";
                        }
                        CalcularNetoPromedio();
                        cargarDeducciones();
                    }
                    else//No aplica a liquidación.
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "El empleado no se encuentra activo";
                    }
                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Debe Ingresar Codigo de Empleado";
                }

            }
            catch (Exception ex)
            {
                //LimpiarTxt();
            }
        }
        private void AsignarCampos(DataTable dt)
        {
            if (dt.Rows.Count != 0)
            {
                decimal Indemnizacion = 0, Aguinaldo = 0,  TotalVac = 0,  Neto = 0, Subtotal = 0;
                Indemnizacion = Convert.ToDecimal(dt.Rows[0]["Indemnizacion"].ToString());
                Aguinaldo = Convert.ToDecimal(dt.Rows[0]["Aguinaldo"].ToString());
                TotalVac = Convert.ToDecimal(dt.Rows[0]["Vacaciones"].ToString());
                Subtotal = Convert.ToDecimal(dt.Rows[0]["totalPagar"].ToString());

                txtIndemnizacion.Text = Convert.ToString(Indemnizacion.ToString("n2"));
                txtAguinaldo.Text = Convert.ToString(Aguinaldo.ToString("n2"));
                txtTotalVac.Text = Convert.ToString(TotalVac.ToString("n2"));
                //Neto = Indemnizacion + Aguinaldo + TotalVac;
                txtSubTotal.Text = Convert.ToString(Subtotal.ToString("n2"));

            }
        }
        void CalcularNetoPromedio()
        {
            DataSet ds;
            decimal NetoPromedio=0, deduccarga=0,montodisp=0;
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable fechap = NPeriodo.GetPeriodoActual();

            Neg_Comedor neg_comedor = new Neg_Comedor();
            deduccarga = neg_comedor.CreditoPeriodoSel(this.txtcodigoAsig.Text.Trim(), Convert.ToDateTime(fechap.Rows[0][0]).ToShortDateString(), Convert.ToDateTime(fechap.Rows[0][1]).ToShortDateString());
            DataTable consumo = new DataTable();
            consumo = neg_comedor.spCreditoSemanaSel(this.txtcodigoAsig.Text.Trim());
            deduccarga += consumo.Rows.Count > 0 ? Convert.ToDecimal(consumo.Rows[0]["Total"]) : 0;
            
            //ds = Neg_Liquidacion.ObtenerNetoxPlanillas(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()));//Obtengo el detalle de la deduccion  
            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    NetoPromedio = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row["neto"]))) / ds.Tables[0].Rows.Count;
            //}
            //else {
            //    NetoPromedio = 0;
            //}
            montodisp = Neg_Liquidacion.GetMontoDisponible(this.txtcodigoAsig.Text.Trim(), userDetail.getIDEmpresa());
            NetoPromedio = montodisp - deduccarga;
            TxtNetoProm.Text = NetoPromedio.ToString("n2");//NetoPromedio.ToString("n2");
        }
        void cargarDeducciones()
        {
            DataSet ds;
            decimal TotalDeduc = 0;
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //ds = Neg_DevYDed.DeduccionesOrdinariasObtener(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()),1,0);//Obtengo el detalle de la deduccion  
            DataTable dtEgresos = new DataTable();
            dtEgresos = Neg_DevYDed.CalcularDetalleDeduccionesxEmp(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), 1, 0, Convert.ToDecimal(txtSubTotal.Text),DateTime.Now, userDetail.getIDEmpresa());
           
            GVDeducciones.DataSource = dtEgresos;
            GVDeducciones.DataBind();
            TotalDeduc += Convert.ToDecimal(dtEgresos.AsEnumerable().Sum(row => Convert.ToDecimal(row["Debe"])));
            

            ds = Neg_DevYDed.ObtenerDeduccionesVigentes(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()));//Obtengo el detalle de la deduccion              
            GvEgresos.DataSource = ds;
            GvEgresos.DataBind();

            TotalDeduc += Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row["total"])));

            //se calcula saldo disponible
            TotalDeduc = Convert.ToDecimal(txtSubTotal.Text) - TotalDeduc;
            TxtSaldoDisp.Text = TotalDeduc.ToString("n2");

            //inactivas
            Dato_DevYDed Dato_DevYDed = new Dato_DevYDed();
            DataSet dd = Dato_DevYDed.DeduccionesOrdinariasInactivasSel(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), userDetail.getIDEmpresa());//Obtengo el detalle de la deduccion  
            Gvinactivas.DataSource = dd;
            Gvinactivas.DataBind();
        }
        protected void txtcodigoAsig_TextChanged(object sender, EventArgs e)
        {
            limpiarCampos();
            ObtenerIngresosEgresosVigentes();
        }
        decimal total = 0;
        protected void GVDeducciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debe"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Deuda Total";
                e.Row.Cells[5].Text = total.ToString("n2");
                e.Row.Font.Bold = true;

            }
        }
        protected void GVDeducciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDeducciones.PageIndex = e.NewPageIndex;
            GVDeducciones.DataBind();
            cargarDeducciones();
        }
        protected void Gvinactivas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvinactivas.PageIndex = e.NewPageIndex;
            Gvinactivas.DataBind();
            cargarDeducciones();
        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            try
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (!Dato_Planilla.ActualizarPeriodoDeduccion(Convert.ToInt32(Session["ID"].ToString()), Convert.ToInt32(txtPeriodo.Text.Trim()) + 1, 1, userDetail.getIDEmpresa()))
                {
                    throw new Exception("Hubo problemas mover de periodo");
                }
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                cargarDeducciones();
                LblSuccess.Text = "Deduccion no se aplicara en el periodo " + txtPeriodo.Text.Trim();
                HabilitarCampos();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void Gvinactivas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());

                if (e.CommandName.CompareTo("reactivar") == 0)
                {
                    GridViewRow selectedRow = Gvinactivas.Rows[index];
                    int id = Convert.ToInt32(selectedRow.Cells[0].Text.Trim());
                    IUserDetail userDetail = UserDetailResolver.getUserDetail();

                    if (!Dato_Planilla.ActualizarPeriodoDeduccion(Convert.ToInt32(id), Convert.ToInt32(txtPeriodo.Text.Trim()), 1, userDetail.getIDEmpresa()))
                    {
                        throw new Exception("Hubo problemas al reactivar la deduccion");
                    }
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    cargarDeducciones();
                    LblSuccess.Text = "Deduccion reactivada Satisfactoriamente";
                    HabilitarCampos();

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