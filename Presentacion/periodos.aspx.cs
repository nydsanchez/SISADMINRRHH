using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Negocios;
using Datos;
namespace NominaRRHH.Presentacion
{

    public partial class periodos : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();


        #endregion
        string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Convert.ToString(this.Page.Session["usuario"]);
            if (!this.Page.IsPostBack)
            {

                obtenerUbicaciones();
                obtenerMeses();
                obtenerProximoPeriodoCatorcenal();
                obtenerTiposPlanilla();
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
        private void obtenerProximoPeriodoCatorcenal()
        {
            string periodo = Neg_Periodo.cargarProxPeriodoCatorc();
            txtNo.Text = periodo;
            txtPeriodoAguinaldo.Text = periodo;
            txtNumeroPeriodoVacaciones.Text = periodo;
        }

        private void obtenerMeses()
        {
            DataSet ds = Neg_Catalogos.CargarMeses();
            this.ddlMesSem1.DataSource = ds;
            this.ddlMesSem1.DataMember = "meses";
            this.ddlMesSem1.DataValueField = "idMes";
            this.ddlMesSem1.DataTextField = "mes";
            this.ddlMesSem1.DataBind();


            this.ddlMesSem2.DataSource = ds;
            this.ddlMesSem2.DataMember = "meses";
            this.ddlMesSem2.DataValueField = "idMes";
            this.ddlMesSem2.DataTextField = "mes";
            this.ddlMesSem2.DataBind();


            this.ddlMesVac.DataSource = ds;
            this.ddlMesVac.DataMember = "meses";
            this.ddlMesVac.DataValueField = "idMes";
            this.ddlMesVac.DataTextField = "mes";
            this.ddlMesVac.DataBind();
        }

        private void obtenerUbicaciones()
        {
            DataSet ds = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacion.DataSource = ds;
            this.ddlUbicacion.DataMember = "ubicaciones";
            this.ddlUbicacion.DataValueField = "codigo_ubicacion";
            this.ddlUbicacion.DataTextField = "nombre_ubicacion";
            this.ddlUbicacion.DataBind();


            this.ddlUbicacionVacaciones.DataSource = ds;
            this.ddlUbicacionVacaciones.DataMember = "ubicaciones";
            this.ddlUbicacionVacaciones.DataValueField = "codigo_ubicacion";
            this.ddlUbicacionVacaciones.DataTextField = "nombre_ubicacion";
            this.ddlUbicacionVacaciones.DataBind();

            this.ddlUbicacionAguinaldo.DataSource = ds;
            this.ddlUbicacionAguinaldo.DataMember = "ubicaciones";
            this.ddlUbicacionAguinaldo.DataValueField = "codigo_ubicacion";
            this.ddlUbicacionAguinaldo.DataTextField = "nombre_ubicacion";
            this.ddlUbicacionAguinaldo.DataBind();
        }



        public bool validar()
        {
            if (txtNo.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtNo.Focus();
                return false;
            }

            if (txtDesde1er.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtDesde1er.Focus();
                return false;
            }

            if (txtHasta1er.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHasta1er.Focus();
                return false;
            }
            if (TxtFactor.Text.Trim() == "" || TxtFactor.Text.Trim() == "0")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                TxtFactor.Focus();
                return false;
            }
            if (!ChkConsolidar.Checked && ddlTiposPlanilla.SelectedValue == "1")
            {
                if (txtDesde2da.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un valor valido";
                    txtDesde2da.Focus();
                    return false;
                }

                if (txtHasta2da.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un valor valido";
                    txtHasta2da.Focus();
                    return false;
                }
            }
            return true;
        }

        protected void btnAagregar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                DateTime hasta1era = new DateTime(1900, 1, 1, 0, 0, 0), desde2da = new DateTime(1900, 1, 1, 0, 0, 0);
                bool consolidar = true;
                DateTime hasta2da = new DateTime(1900, 1, 1, 0, 0, 0);
                int mes2da = 0;
                //catorcenas semanas divididas
                if (!ChkConsolidar.Checked && ddlTiposPlanilla.SelectedValue == "1")
                {
                    hasta1era = Convert.ToDateTime(txtHasta1er.Text.Trim());
                    desde2da = Convert.ToDateTime(txtDesde2da.Text.Trim());
                    mes2da = Convert.ToInt32(ddlMesSem2.SelectedValue);
                    hasta2da = Convert.ToDateTime(txtHasta2da.Text.Trim());
                    consolidar = false;
                }
                else//periodos consolidados incluye catorcena
                {
                    hasta2da = Convert.ToDateTime(txtHasta1er.Text.Trim());
                }

                if (Neg_Periodo.AgregarPeriodo(Convert.ToInt32(txtNo.Text.Trim()), Convert.ToInt32(ddlUbicacion.SelectedValue),
                    Convert.ToInt32(ddlMesSem1.SelectedValue), Convert.ToDateTime(txtDesde1er.Text.Trim()), hasta1era,
                    mes2da, desde2da, hasta2da,
                    Convert.ToInt32(RbTipoPeriodos.SelectedValue), user, Convert.ToInt32(ddlTiposPlanilla.SelectedValue), consolidar, Convert.ToDecimal(TxtFactor.Text.Trim())))

                //if (Neg_Periodo.AgregarPeriodoQuincenal(Convert.ToInt32(txtNo.Text.Trim()), Convert.ToInt32(ddlUbicacion.SelectedValue),
                //    Convert.ToInt32(ddlMesPerid.SelectedValue), Convert.ToDateTime(txtFechaDesdPeriQ.Text.Trim()), "", 
                //    0, "", Convert.ToDateTime(txtFechaHastPeriQ.Text.Trim()),
                //    Convert.ToInt32(RbTipoPeriodos.SelectedValue), user, Convert.ToInt32(ddlTiposPlanilla.SelectedValue)))

                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Satisfactorio";
                }
                else
                {
                    alertValida.Visible = true;
                    alertSucces.Visible = false;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al ingresar Datos";
                }
            }
        }

        protected void btnIniciar_Click(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(RbTipoPeriodos.SelectedValue);
            divoptions.Visible = false;
            if (i == 1)
            {
                mvPeriodos.SetActiveView(VwPeriodoOrdinario);
            }
            if (i == 3 || i == 4)
            {
                mvPeriodos.SetActiveView(vwPeriodoVacaciones);
            }
            if (i == 5)
            {
                mvPeriodos.SetActiveView(vwPeriodoAguinaldo);
            }
        }

        //protected void btnTipoPlanilla_Click(object sender, EventArgs e)
        //{
        //    if (ddlTiposPlanilla.SelectedValue == "1")
        //    {
        //        periodSemana2.Visible = true;
        //        divconsolidar.Visible = true;
        //    }
        //    else if (ddlTiposPlanilla.SelectedValue == "2" || ddlTiposPlanilla.SelectedValue == "3")
        //    {
        //        periodSemana2.Visible = false;
        //        divconsolidar.Visible = false;
        //    }
        //}

        //protected void btnGuardarPQ_Click(object sender, EventArgs e)
        //{
        //    //if(Neg_Periodo.AgregarPeriodoQuincenal(Convert.ToInt32(txtNo.Text.Trim()), Convert.ToInt32(ddlUbicacion.SelectedValue),
        //    //    Convert.ToInt32(ddlMesPerid.SelectedValue), Convert.ToDateTime(txtFechaDesdPeriQ.Text.Trim()), "", 0, "", Convert.ToDateTime(txtFechaHastPeriQ.Text.Trim()), 
        //    //    Convert.ToInt32(RbTipoPeriodos.SelectedValue), user, Convert.ToInt32(ddlTiposPlanilla.SelectedValue)))
        //    //{
        //    //      alertValida.Visible = false;
        //    //        alertSucces.Visible = true;
        //    //        LblSuccess.Visible = true;
        //    //        LblSuccess.Text = "Ingreso Satisfactorio";
        //    //}
        //    //else
        //    //{
        //    //    alertValida.Visible = true;
        //    //    alertSucces.Visible = false;
        //    //    lblAlert.Visible = true;
        //    //    lblAlert.Text = "Error al ingresar Datos";
        //    //}
        //}

        protected void btnVacaciones_Click(object sender, EventArgs e)
        {
            if (Neg_Periodo.AgregarPeriodoVacaciones(Convert.ToInt32(txtNumeroPeriodoVacaciones.Text.Trim()), Convert.ToInt32(ddlUbicacionVacaciones.SelectedValue),
                Convert.ToInt32(ddlMesVac.SelectedValue.Trim()), Convert.ToDateTime(txtDesdeVac.Text.Trim()), Convert.ToDateTime(txtHastaVac.Text.Trim()),
                Convert.ToInt32(RbTipoPeriodos.SelectedValue), user, Convert.ToInt32(ddlTiposPlanilla.SelectedValue), Convert.ToDecimal(TxtFactorVac.Text.Trim())))
            {
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "Ingreso Satisfactorio";
            }
            else
            {
                alertValida.Visible = true;
                alertSucces.Visible = false;
                lblAlert.Visible = true;
                lblAlert.Text = "Error al ingresar Datos";
            }
        }

        protected void btnAguinaldo_Click(object sender, EventArgs e)
        {
            if (Neg_Periodo.AgregarPeriodoAguinaldo(Convert.ToInt32(txtPeriodoAguinaldo.Text.Trim()), Convert.ToInt32(this.ddlUbicacionAguinaldo.SelectedValue),
                12, Convert.ToDateTime(TxtDesdeAgui.Text.Trim()), Convert.ToDateTime(TxtHastaAgui.Text.Trim()),
                Convert.ToInt32(RbTipoPeriodos.SelectedValue), user, Convert.ToInt32(ddlTiposPlanilla.SelectedValue), Convert.ToDecimal(TxtFactorAg.Text.Trim())))
            {
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "Ingreso Satisfactorio";
            }
            else
            {
                alertValida.Visible = true;
                alertSucces.Visible = false;
                lblAlert.Visible = true;
                lblAlert.Text = "Error al ingresar Datos";
            }
        }

        protected void ChkConsolidar_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConsolidar.Checked)
            {
                periodSemana2.Visible = false;
            }
            else
            {
                periodSemana2.Visible = true;
            }
        }

        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            periodSemana1.Visible = true;
            if (ddlTiposPlanilla.SelectedValue == "1")
            {
                periodSemana2.Visible = true;
                //divconsolidar.Visible = true;
                ChkConsolidar.Checked = false;
            }
            else
            {
                periodSemana2.Visible = false;
                divconsolidar.Visible = false;

            }
        }
    }
}