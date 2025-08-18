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
    public partial class IngresoxVacaciones : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_PlanillaVacaciones Neg_PlanillaVacaciones = new Neg_PlanillaVacaciones();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_Permisos Neg_Permisos = new Neg_Permisos();
        #endregion
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerUbicaciones();
                obtenerTiposPlanilla();
               
              
                Session["SaldoVac"] = 0;
            }
        }
        private void obtenerUbicaciones()
        {            
            this.ddlUbicacionVac.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacionVac.DataMember = "ubicaciones";
            this.ddlUbicacionVac.DataValueField = "codigo_ubicacion";
            this.ddlUbicacionVac.DataTextField = "nombre_ubicacion";
            this.ddlUbicacionVac.DataBind();
        }
        private void obtenerPeriodo()
        {
            dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()), Convert.ToInt32(ddlUbicacionVac.SelectedValue.Trim()));

            if (dtPeriodo.Rows.Count > 0)
            {
                txtPeriodo.Text = dtPeriodo[0].nperiodo.ToString();
                Session["fechaini"]=dtPeriodo[0].fechaini.ToString();
                Session["fechafin"]= dtPeriodo[0].fechafin2.ToString();
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
        protected void txtcodigoEmpleadoVacaciones_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable demp;
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                    throw new Exception("Periodo Invalido");

                Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(Session["fechafin"]);

                if (!string.IsNullOrEmpty(txtcodigoEmpleadoVacaciones.Text))
                {
                    demp = Neg_PlanillaVacaciones.obtenerEmpleadoPagoVacaciones(Convert.ToInt32(ddlUbicacionVac.SelectedValue.Trim()), Convert.ToInt32(txtcodigoEmpleadoVacaciones.Text.Trim()), Neg_Liquidacion.Globales.fechaR,false);
                   
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
        protected void btnProcVacaciones_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);              

            try
            {
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                    throw new Exception("Periodo Invalido");

                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                if (dtPeriodo[0].cerrado == 1)
                    throw new Exception("El periodo esta cerrado");

                if (!string.IsNullOrEmpty(txtcodigoEmpleadoVacaciones.Text))
                {
                    int ubicacion = Convert.ToInt32(ddlUbicacionVac.SelectedValue.Trim());
                    int codemp = Convert.ToInt32(txtcodigoEmpleadoVacaciones.Text.Trim());
                  
                    double saldovac = Convert.ToDouble(TxtSaldoVac.Text.Trim());

                    if (saldovac <= Convert.ToDouble(Session["SaldoVac"]))
                    {
                        if (Neg_PlanillaVacaciones.calcularVacaciones(ubicacion, codemp,  saldovac, 1, Convert.ToInt32(txtPeriodo.Text), Session["fechaini"].ToString(), Session["fechafin"].ToString(), user, 0))
                        {
                            Neg_Permisos.ActualizarSaldoVacaciones(Convert.ToInt32(txtPeriodo.Text.Trim()));
                            limpiarCampos();
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Se registro exitosamente el ingreso por vacaciones";
                        }
                    }
                    else
                    {
                        throw new Exception("Las vacaciones a pagar deben ser menor o igual al Saldo Actual");
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
        void limpiarCampos() {
            txtcodigoEmpleadoVacaciones.Text = "";
            TxtNombreEmpVac.Text = "";
            TxtDeptoVac.Text = "";
            TxtSaldoVac.Text = "0.00";
            Session["SaldoVac"] = 0;
        }

        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }       

        protected void ddlUbicacionVac_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }
    }
}