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
    public partial class AsignacionDeduccionesComedor : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATICm
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
               
            }
        }
       
        //private void obtenerPeriodo()
        //{
        //    try {
        //        if (!string.IsNullOrEmpty(this.txtcodigoAsig.Text.Trim()))
        //        {
        //            DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtcodigoAsig.Text);
        //            DataTable ubicacion = Neg_Catalogos.seleccionarUbicacionesxCod(Convert.ToInt32(DetEmpleados.Rows[0]["codigo_ubicacion"]));
        //            dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1,Convert.ToInt32(ubicacion.Rows[0]["tplanilla"]), 0);
        //            if (dtPeriodo.Rows.Count > 0)
        //            {
        //                txtPeriodo.Text = dtPeriodo[0].nperiodo.ToString();
        //            }
        //            else
        //            {
        //                txtPeriodo.Text = "0";
        //            }
        //            Session["periodo"] = txtPeriodo.Text.Trim();
        //        }

        //    }
        //    catch (Exception ex) {
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = "No hay periodo abierto que este vigente";
        //    }
        //}
        void limpiarCampos() {
            txtTotal.Text = "";
            txtCuotas.Text = "";
            TxtNetoProm.Text = "0.00";
            
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

                //if (Convert.ToInt32(txtPeriodo.Text.Trim()) < Convert.ToInt32(Session["periodo"].ToString()))
                //{
                //    alertValida.Visible = true;
                //    lblAlert.Visible = true;
                //    lblAlert.Text = "No puede asignar deducciones a periodos cerrados";
                //    txtPeriodo.Focus();
                //    return false;
                //}
                
                if ((txtTotal.Text.Trim() == "" || txtTotal.Text.Trim() == "0") )
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
                decimal disponible = string.IsNullOrEmpty(Txtingresodisp.Value) ? 0 : Convert.ToDecimal(Txtingresodisp.Value);
                decimal cuota = Convert.ToDecimal(txtCuotas.Text.Trim());
                decimal total = Convert.ToDecimal(txtTotal.Text.Trim());

                if (cuota > total)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "La cuota no puede ser mayor que el total de la deuda";
                    txtCuotas.Focus();
                    return false;
                }
                string user = Convert.ToString(this.Page.Session["usuario"]);
                decimal acumliq = string.IsNullOrEmpty(Txtliquidaciondisp.Text.Trim()) ? 0 : (Convert.ToDecimal(Txtliquidaciondisp.Text.Trim()) - Convert.ToDecimal(txtTotal.Text.Trim()));
                //no tiene indemnizacion y la cuota es mayor a los disponible en catorcena
                if (acumliq < 0)
                {
                    if (cuota == total)
                    {
                        if (total <= disponible)
                        {
                            return true;
                        }
                        else
                        {
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "Hay un sobregiro de C$" + (total - disponible).ToString("n2") + " en Cuota Unica.";
                            txtTotal.Focus();
                            return false;
                        }

                    }
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Hay un sobregiro de C$" + acumliq.ToString("n2") + " en el Total. Consulte con su Administrador.";
                    txtTotal.Focus();

                    if (!Neg_DevYDed.DeduccionesExternasLogIns(Convert.ToInt32(txtcodigoAsig.Text.Trim()), acumliq, "Total", 27, user))
                    {
                        lblAlert.Text = "Error al insertar log sobregiro total";
                    }
                    return false;
                }
                decimal cuotadisp = string.IsNullOrEmpty(Txtingresodisp.Value) ? 0 : (Convert.ToDecimal(Txtingresodisp.Value) - Convert.ToDecimal(txtCuotas.Text.Trim()));
                if (cuotadisp < 0)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Hay un sobregiro de C$" + cuotadisp.ToString("n2") + " en la Cuota. Consulte con su Administrador.";
                    txtTotal.Focus();
                    if (!Neg_DevYDed.DeduccionesExternasLogIns(Convert.ToInt32(txtcodigoAsig.Text.Trim()), cuotadisp, "Cuota", 27, user))
                    {
                        lblAlert.Text = "Error al insertar log sobregiro cuota";
                    }
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
                    int porcentual =  0;
                    int recurrente =  0;
                    string fecha = DateTime.Now.ToShortDateString();
                    if (Neg_DevYDed.AsignarDeduccionesEmpleado(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), 27, Convert.ToDecimal(txtTotal.Text.Trim()), Convert.ToDecimal(txtCuotas.Text.Trim()), fecha,fecha, user,porcentual,recurrente,0, fecha,fecha, txtObservEmpl.Text))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Deduccion Asignada Satisfactoriamente";
                        //Actualizar los campos
                        AsignarCampos();                        
                        limpiarCampos();
                        
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
        //protected void GVDeducciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    GridViewRow selectedRow = this.GVDeducciones.Rows[Convert.ToInt32(e.RowIndex)];
        //    int indice = selectedRow.DataItemIndex;
        //    int id = Convert.ToInt32(GVDeducciones.Rows[GVDeducciones.TabIndex].Cells[1].Text.Trim());
        //    if (Neg_DevYDed.DeduccionOrdinariaEliminar(id))
        //    {
        //        cargarDeducciones();
        //    }
        //    else
        //    {
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = "Error Al Eliminar El Registro";
        //    }
        //}
        //protected void GVDeducciones_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (Session["estado"].ToString().Trim()=="1")
        //    {
        //        btnGuardar.Visible = false;
        //        btnEditar.Visible = true;
        //        btnDeshabilitar.Visible = true;
        //        txtCuotas.Text = GVDeducciones.Rows[GVDeducciones.SelectedIndex].Cells[3].Text.Trim();
        //        this.txtTotal.Text = GVDeducciones.Rows[GVDeducciones.SelectedIndex].Cells[4].Text.Trim();
        //        editarhide.Visible = false;
        //        editarhide2.Visible = false;
        //        editarhide3.Visible = false;
        //        Session["ID"] = GVDeducciones.Rows[GVDeducciones.SelectedIndex].Cells[1].Text.Trim();
        //        Session["IdDeduc"] = GVDeducciones.DataKeys[GVDeducciones.SelectedIndex][0].ToString();
        //    }
        //    else
        //    {
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = "El empleado no esta activo";
        //    }
            
        //}
        //protected void btnEditar_Click(object sender, EventArgs e)
        //{
        //    string user = Convert.ToString(this.Page.Session["usuario"]);
        //    if (Neg_DevYDed.EditarDeduccionesxEmpleado(Convert.ToInt32(Session["ID"].ToString()), Convert.ToDecimal(this.txtTotal.Text.Trim()), Convert.ToDecimal(txtCuotas.Text.Trim()), user))
        //    {                
        //        //aqui ingreso i bruto
        //        //se verifica si la deduccion aplica a ingreso bruto
        //        int existe = Neg_DevYDed.verificarAplicaDeduccionIBruto(Convert.ToInt32(Session["IdDeduc"].ToString()));
        //        if (existe > 0)
        //        {
        //            DataTable dt1 = new DataTable();
        //            DataSet ds = new DataSet();

        //            //se eliminan cuotas aplicadas y se inserta ingreso bruto original
        //            if (!Neg_DevYDed.ReestablecerIngresosIBruto(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString())))
        //            {
        //                throw new Exception("Error al reestablecer ingreso aplicables a deduccion");

        //            }
        //            IUserDetail userDetail = UserDetailResolver.getUserDetail();
        //            //Se actualiza ultima cuota
        //            if (!Dato_Planilla.ActualizarEstadoCuentaDeudaEmp(Convert.ToInt32(Session["ID"].ToString()), 2, Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString()), Convert.ToInt32(txtPeriodo.Text.Trim()), 0, 1, userDetail.getIDEmpresa()))
        //            {
        //                throw new Exception("Hubo problemas al deducir cuota");
        //            }

        //            ds = Neg_DevYDed.IngresosPeriodoxTipoSel(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),0, Convert.ToInt32(Session["IdDeduc"].ToString()));

        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                dt1 = ds.Tables[0];
        //                Neg_DevYDed.RegistrarIngresoEgresoAsociado(Convert.ToInt32(txtPeriodo.Text.Trim()),0, dt1, user);
        //            }
        //        }
                
        //        alertValida.Visible = false;
        //        alertSucces.Visible = true;
        //        LblSuccess.Visible = true;
        //        cargarDeducciones();
        //        LblSuccess.Text = "Deduccion Actualizada Satisfactoriamente";
        //        HabilitarCampos();

        //    }
        //}
        //private void HabilitarCampos()
        //{
        //    txtTotal.Text = "";
        //    txtCuotas.Text = "";
        //    editarhide.Visible = true;
        //    editarhide2.Visible = true;
        //    editarhide3.Visible = true;
        //    btnGuardar.Visible = true;
        //    //btnEditar.Visible = false;
        //    //btnDeshabilitar.Visible = false;
        //}
        //protected void btnDeshabilitar_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Neg_DevYDed.DeshabilitarDeuda(Convert.ToInt32(Session["ID"].ToString())))
        //        {
        //            int existe = Neg_DevYDed.verificarAplicaDeduccionIBruto(Convert.ToInt32(Session["IdDeduc"].ToString()));
        //            if (existe > 0)
        //            {
        //                //aqui ingreso i bruto
        //                //se verifica si tiene deduccion aplicada a ingreso bruto                   
        //                if (!Neg_DevYDed.ReestablecerIngresosIBruto(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString())))
        //                {
        //                    throw new Exception("Error al reestablecer ingreso aplicables a deduccion");

        //                }
        //                IUserDetail userDetail = UserDetailResolver.getUserDetail();
        //                //Se actualiza DEDUCCION DE INGRESO ASOCIADO
        //                if (!Dato_Planilla.ActualizarEstadoCuentaDeudaEmp(Convert.ToInt32(Session["ID"].ToString()), 2, Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(Session["IdDeduc"].ToString()), Convert.ToInt32(txtPeriodo.Text.Trim()), 0, 1, userDetail.getIDEmpresa()))
        //                {
        //                    throw new Exception("Hubo problemas al deducir cuota");
        //                }
        //            }

        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            cargarDeducciones();
        //            LblSuccess.Text = "Deduccion Deshabilitada Satisfactoriamente";
        //            HabilitarCampos();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = ex.Message;
        //    }
        //}
        private void AsignarCampos()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtcodigoAsig.Text.Trim()))
                {
                    //obtenerPeriodo();
                    DataTable fechap = NPeriodo.GetPeriodoActual();
                    string periodo = fechap.Rows[0][2].ToString();
                    txtPeriodo.Text = periodo;

                    Neg_Liquidacion.Globales.fechaR = DateTime.Now;

                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtcodigoAsig.Text.Trim()), 2);

                    if (dtEmp.Rows.Count > 0)
                    {
                        Session["estado"] = dtEmp.Rows[0]["idestado"].ToString().Trim();
                        if (Session["estado"].ToString().Trim() == "0")
                        {
                            throw new Exception("El codigo de empleado esta inactivo");
                        }

                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();

                        //CalcularIngPromedio(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()));

                        DataSet ds = Neg_Liquidacion.ObtenerDatosLiquidacion(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), 0, 0, 0, 0,0,true);//Obtengo la tabla de meses,y el detalle de liquidacion de X Persona.
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//Si no tiene los suficientes dias,no se podra calcular el detalle de la liquidacion.X Persona no aplica a liquidacion.
                        {
                            DataTable campos = ds.Tables[1];
                            decimal Indemnizacion = Convert.ToDecimal(campos.Rows[0]["Indemnizacion"].ToString());

                            hfaculiq.Value = Convert.ToString(Indemnizacion.ToString("n2"));
                            cargarDeducciones(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), Convert.ToDateTime(fechap.Rows[0][0]).ToShortDateString(), Convert.ToDateTime(fechap.Rows[0][1]).ToShortDateString());
                        }
                        else//No aplica a liquidación.
                        {
                            throw new Exception("No se han encontrado datos de Liquidacion Acumulada");
                        }


                    }
                    else//No aplica a liquidación.
                    {
                        throw new Exception("El empleado no se encuentra activo");
                    }
                }
                else
                {
                    throw new Exception("Debe Ingresar Codigo de Empleado");
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
      
        void cargarDeducciones(int codigo,string fini,string ffin)
        {
            DataSet ds;
            DataTable dtEgresos = new DataTable();

            decimal TotalDeduc = 0, deduccarga=0;

            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //ds = Neg_DevYDed.DeduccionesOrdinariasObtener(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()),1,0);//Obtengo el detalle de la deduccion  
            
            dtEgresos = Neg_DevYDed.CalcularDetalleDeduccionesxEmp(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), 1, 0, Convert.ToDecimal(hfaculiq.Value),DateTime.Now, userDetail.getIDEmpresa());
            TotalDeduc += Convert.ToDecimal(dtEgresos.AsEnumerable().Sum(row => Convert.ToDecimal(row["Debe"])));
                                    
            //ds = Neg_DevYDed.ObtenerDeduccionesVigentes(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()));//Obtengo el detalle de la deduccion              
            //deduccarga += Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row["total"])));
            //TotalDeduc += deduccarga;

            Neg_Comedor neg_comedor = new Neg_Comedor();
            //if (deduccarga==0)
            //{
                //DataTable fechap = NPeriodo.GetPeriodoActual();
                deduccarga += neg_comedor.CreditoPeriodoSel(this.txtcodigoAsig.Text.Trim(),fini,ffin);
                TotalDeduc += deduccarga;
            //}

            //GVDeducciones.DataSource = dtEgresos.Select("tipoDeduc=4 or tipoDeduc=27").CopyToDataTable();
            //GVDeducciones.DataBind();
            //GvEgresos.DataSource = ds.Tables[0].Select("tipoIngrDeduc=4 or tipoIngrDeduc=27").CopyToDataTable();
            //GvEgresos.DataBind();

            //se calcula saldo disponible               
            decimal disponible = Neg_Liquidacion.GetMontoDisponible(codigo.ToString(), userDetail.getIDEmpresa());
            Txtingresodisp.Value = disponible.ToString();
            Txtliquidaciondisp.Text = (Convert.ToDecimal(hfaculiq.Value) - TotalDeduc).ToString("n2");
            Txtingresodisp.Value = (Convert.ToDecimal(Txtingresodisp.Value) - deduccarga).ToString();
        }        
        protected void txtcodigoAsig_TextChanged(object sender, EventArgs e)
        {
            limpiarCampos();
            AsignarCampos();
        }
        decimal total = 0;
      

        //protected void txtPeriodo_TextChanged(object sender, EventArgs e)
        //{
        //    cargarDeducciones(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()));
        //}
       
    }
}