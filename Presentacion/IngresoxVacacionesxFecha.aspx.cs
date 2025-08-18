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
    public partial class IngresoxVacacionesxFecha : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_PlanillaVacaciones Neg_PlanillaVacaciones = new Neg_PlanillaVacaciones();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
               
                obtenerUbicaciones();
                Session["SaldoVac"] = 0;
                txtFecAut.Text = DateTime.Now.ToShortDateString();

                //txtFechaIni2.Text = DateTime.Now.ToShortDateString();
                //txtFechaFin2.Text = DateTime.Now.ToShortDateString();
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
       
        protected void txtcodigoEmpleadoVacaciones_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable demp;
                Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(txtFecAut.Text);
                IUserDetail userDetail = UserDetailResolver.getUserDetail();

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
                if (!string.IsNullOrEmpty(txtcodigoEmpleadoVacaciones.Text))
                {
                    int ubicacion = Convert.ToInt32(ddlUbicacionVac.SelectedValue.Trim());
                    int codemp = Convert.ToInt32(txtcodigoEmpleadoVacaciones.Text.Trim());                   
                    double saldovac = Convert.ToDouble(TxtSaldoVac.Text.Trim());
                    string fecaut = Convert.ToDateTime(txtFecAut.Text).ToShortDateString();

                    if (saldovac <= Convert.ToDouble(Session["SaldoVac"]))
                    {
                        if (Neg_PlanillaVacaciones.calcularVacaciones(ubicacion, codemp, saldovac, 1, fecaut, user))
                        {
                            //Neg_Permisos.ActualizarSaldoVacaciones(Convert.ToInt32(txtPeriodo.Text.Trim()));
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
        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    if (txtBuscar.Text.Trim() != "")
        //    {

        //        string pfini, pffin;
        //        bool todos = rbRango.SelectedValue == "1" ? false : true;
        //        pfini = string.IsNullOrEmpty(txtFechaIni2.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtFechaIni2.Text.Trim();
        //        pffin = string.IsNullOrEmpty(txtFechaFin2.Text.Trim()) ? DateTime.Now.ToShortDateString() : txtFechaFin2.Text.Trim();

        //        if (!todos)
        //        {
        //            if (string.IsNullOrEmpty(txtBuscar.Text.Trim()))
        //            {
        //                alertSucces.Visible = false;
        //                alertValida.Visible = true;
        //                lblAlert.Visible = true;
        //                lblAlert.Text = "Debe escribir el codigo";
        //                return;
        //            }
        //        }
        //        if (string.IsNullOrEmpty(txtFechaIni2.Text.Trim()))
        //        {
        //            alertSucces.Visible = false;
        //            alertValida.Visible = true;
        //            lblAlert.Visible = true;
        //            lblAlert.Text = "Debe ingresar un rango de fechas";
        //            return;
        //        }
        //        if (string.IsNullOrEmpty(txtFechaFin2.Text.Trim()))
        //        {
        //            alertSucces.Visible = false;
        //            alertValida.Visible = true;
        //            lblAlert.Visible = true;
        //            lblAlert.Text = "Debe ingresar un rango de fechas";
        //            return;
        //        }

        //        obtenerDetalleVacaciones(Convert.ToInt32(txtBuscar.Text), pfini, pffin);
        //    }
        //}

        //private void obtenerDetalleVacaciones(int codigo, string fechaIni, string fechaFin)
        //{
        //    DataSet ds = new DataSet();
        //    ds = Neg_Informes.obtenerPlanillaVacaciones(0,codigo, fechaIni, fechaFin);
        //    if (ds.Tables.Count > 0)
        //    {
        //        GVDetalleVac.DataSource = ds;
        //        GVDetalleVac.DataMember = "Vacaciones";
        //        GVDetalleVac.DataBind();
        //    }
        //}
        //protected void rbRango_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (rbRango.SelectedValue == "1")//x empleado
        //    {
             
        //        txtBuscar.Text = "";
                
        //    }
        //    else
        //    {
        //        txtFechaIni2.Text = DateTime.Now.ToShortDateString();
        //        txtFechaFin2.Text = DateTime.Now.ToShortDateString();
        //    }
            
        //    GVDetalleVac.DataSource = null;
        //    GVDetalleVac.DataBind();
        //    btnBuscar.Focus();
        //}
        //protected void GVDetalleVac_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int index = Convert.ToInt32(e.CommandArgument.ToString());

        //    if (e.CommandName.CompareTo("eliminar") == 0)
        //    {

        //        GridViewRow selectedRow = GVDetalleVac.Rows[index];

        //        string codigo = selectedRow.Cells[1].Text;
        //        string fechaut = selectedRow.Cells[3].Text;

        //        Neg_PlanillaVacaciones Neg_PlanillaVacaciones = new Neg_PlanillaVacaciones();
        //        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        //        dsPlanilla.dtEgresosDataTable dtEgresos = Neg_DevYDed.EgresosxPrestacionesFechaSelxEmpleado(fechaut, 2, Convert.ToInt32(codigo));
        //        Neg_PlanillaVacaciones.EliminarHistoricoVacacionesFecha(fechaut, Convert.ToInt32(codigo));
        //    }
        //}

    }
}