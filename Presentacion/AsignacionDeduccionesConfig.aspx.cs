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
    public partial class AsignacionDeduccionesConfig : System.Web.UI.Page
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
                obtenerDeducciones();
                //txtFecSol.Text = DateTime.Now.ToShortDateString();
                //this.txtFecAut.Text = DateTime.Now.ToShortDateString();
                //txtFecha.Text = DateTime.Now.ToShortDateString();
                //TxtFecha2.Text = DateTime.Now.ToShortDateString();
                chkPorc.Checked = false;
                chkRecurrente.Checked = false;
                obtenerProcesos();
                obtenerUbicaciones();
                obtenerPeriodo();
            }

        }
        private void obtenerUbicaciones()
        {
            this.ddlubicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlubicacion.DataMember = "ubicaciones";
            this.ddlubicacion.DataValueField = "codigo_ubicacion";
            this.ddlubicacion.DataTextField = "nombre_ubicacion";
            this.ddlubicacion.DataBind();
        }
        private void obtenerProcesos()
        {
            this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }
        private void obtenerDeducciones()
        {
            this.ddlTipDeduc.DataSource = Neg_DevYDed.cargarDeduccionesEspeciales();
            this.ddlTipDeduc.DataMember = "deducciones";
            this.ddlTipDeduc.DataValueField = "idDeduccion";
            this.ddlTipDeduc.DataTextField = "deduccionNombre";
            this.ddlTipDeduc.DataBind();

        }
        private void obtenerPeriodo()
        {
            try
            {
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, 1, 0);
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
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "No hay periodo abierto que este vigente";
            }
        }
        void limpiarCampos() {
            txtTotal.Text = "";
            txtCuotas.Text = "";
            //TxtNetoProm.Text = "0.00";
            //txtIndemnizacion.Text = "0.00";
            //txtAguinaldo.Text = "0.00";
            //txtTotalVac.Text = "0.00";
            //txtSubTotal.Text = "0.00";
            //TxtSaldoDisp.Text = "0.00";
            chkPorc.Checked = false;
            //chkvalidez.Checked = false;
            this.chkRecurrente.Checked = false;
        }
        public bool validar()
        {
            try {
                //if (txtcodigoAsig.Text.Trim() == "")
                //{
                //    alertValida.Visible = true;
                //    lblAlert.Visible = true;
                //    lblAlert.Text = "Favor Ingrese un valor valido";
                //    //txtCodigo.Focus();
                //    return false;
                //}

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
                    //int exemb = ckembargos.Checked ? 1 : 0;
                    //int exefe = ckefectivo.Checked ? 1 : 0;
                    int exemb = 1;
                    int exefe = 1;
                    int periodo = Convert.ToInt32(txtPeriodo.Text);
                    int tipodeduc = Convert.ToInt32(ddlTipDeduc.SelectedValue.Trim());
                    int tipo = Convert.ToInt32(ddlAsigPerm.SelectedValue.Trim());
                    decimal total = Convert.ToDecimal(txtTotal.Text);
                    decimal cuota = Convert.ToDecimal(txtCuotas.Text);
                    int filtro1 = Convert.ToInt32(ddlubicacion.SelectedValue);
                    int filtro2 = Convert.ToInt32(ddlProceso.SelectedValue);
                    //int periodovalidez = chkvalidez.Checked ? 1 : 0;
                    if (!Neg_DevYDed.AsignardeduccionesEspecialesUpd(periodo, tipodeduc, total, cuota, user, porcentual, recurrente, tipo, filtro1, filtro2, exemb, exefe))
                    {
                        throw new Exception("Error al registrar deduccion");

                    }
                    //se registran nuevas
                    if (Neg_DevYDed.AsignardeduccionesEspecialesIns(periodo, tipodeduc, total, cuota, user, porcentual, recurrente, tipo, filtro1, filtro2, exemb, exefe))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Deduccion Asignada Satisfactoriamente";

                    }
                    else
                    {
                        throw new Exception("Error al registrar deduccion");
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
     
        private void HabilitarCampos()
        {
            txtTotal.Text = "";
            txtCuotas.Text = "";
            editarhide.Visible = true;
            editarhide2.Visible = true;
            editarhide3.Visible = true;
            //editarhide4.Visible = true;
            editarhide5.Visible = true;
            btnGuardar.Visible = true;
            //btnEditar.Visible = false;
           // btnDeshabilitar.Visible = false;
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

                //txtIndemnizacion.Text = Convert.ToString(Indemnizacion.ToString("n2"));
                //txtAguinaldo.Text = Convert.ToString(Aguinaldo.ToString("n2"));
                //txtTotalVac.Text = Convert.ToString(TotalVac.ToString("n2"));
                ////Neto = Indemnizacion + Aguinaldo + TotalVac;
                //txtSubTotal.Text = Convert.ToString(Subtotal.ToString("n2"));

            }
        }
    
        protected void ddlAsigPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
           if (ddlAsigPerm.SelectedValue == "1")//dpto
            {
               // divempleado.Visible = false;
                divproceso.Visible = true;
                divubic.Visible = false;
            }
            else if (ddlAsigPerm.SelectedValue == "2")//todos
            {
                //divempleado.Visible = false;
                divproceso.Visible = false;
                divubic.Visible = false;
            }
            else if (ddlAsigPerm.SelectedValue == "3")//ubicacion
            {
               // divempleado.Visible = false;
                divproceso.Visible = false;
                divubic.Visible = true;
            }
            else
            {
               // divempleado.Visible = false;
                divproceso.Visible = true;
                divubic.Visible = true;
            }
            ddlAsigPerm.Focus();
        }

       
    }
}