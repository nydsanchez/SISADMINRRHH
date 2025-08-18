using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Datos;
using System.Data;

namespace NominaRRHH.Presentacion
{
    public partial class AsignacionIngrYDeduc : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                // obtenerPeriodo();
                obtenerIngresos();
                obtenerDeducciones();
                obtenerTiposPlanilla();
                obtenerUbicaciones();
                // txtFechRenuncia.Text = DateTime.Now.ToShortDateString();
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
        private void obtenerIngresos()
        {
            this.ddlTipoIngrs.DataSource = Neg_DevYDed.cargarIngresos(1);
            this.ddlTipoIngrs.DataMember = "ingresos";
            this.ddlTipoIngrs.DataValueField = "idDevengado";
            this.ddlTipoIngrs.DataTextField = "devengadoNombre";
            this.ddlTipoIngrs.DataBind();
        }
        private void obtenerTiposPlanilla()
        {
            this.ddlTiposPlanilla.DataSource = Neg_Planilla.cargarTiposPlanilla();
            this.ddlTiposPlanilla.DataMember = "planillas";
            this.ddlTiposPlanilla.DataValueField = "idNomina";
            this.ddlTiposPlanilla.DataTextField = "Descripcion";
            this.ddlTiposPlanilla.DataBind();
        }
        private void obtenerDeducciones()
        {
            this.ddlTipDeduc.DataSource = Neg_DevYDed.cargarDeducciones();
            this.ddlTipDeduc.DataMember = "deducciones";
            this.ddlTipDeduc.DataValueField = "idDeduccion";
            this.ddlTipDeduc.DataTextField = "deduccionNombre";
            this.ddlTipDeduc.DataBind();
        }
        protected void ddlAsig_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarCampos();

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text.Trim() != "" && txtPeriodo.Text.Trim() != "" && txtPeriodo.Text.Trim() != "0" && (txtTotal.Text.Trim() != "" || txtHoras.Text.Trim() != "") && ddlAsig.SelectedValue != "0")
                {
                    dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                    if (dtPeriodo[0].cerrado == 1)
                        throw new Exception("El periodo esta cerrado");

                    int ingrdeduc = 0;
                    string user = Convert.ToString(this.Page.Session["usuario"]);

                    if (ddlAsig.SelectedValue == "1")
                        ingrdeduc = Convert.ToInt32(ddlTipoIngrs.SelectedValue);
                    else
                        ingrdeduc = Convert.ToInt32(this.ddlTipDeduc.SelectedValue);

                    int tipoplanilla = ddlTiposPlanilla.SelectedValue.Trim() == "0" ? 1 : Convert.ToInt32(ddlTiposPlanilla.SelectedValue);

                    if (ddlAsig.SelectedValue == "1" && ddlTipoIngrs.SelectedValue == "1")//si es hrs extras
                    {
                        if (!Neg_Planilla.insertarHorasExtras(Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), ingrdeduc,
                        Convert.ToDecimal(txtHoras.Text.Trim()), Convert.ToInt32(ddlTiposPlanilla.SelectedValue)))
                        {
                            throw new Exception("Error al insertar el registro");
                        }
                    }
                    else//cualquier otro ingreso y egreso
                    {
                        if (!Neg_DevYDed.AsignarIngresoOdeduccionPorEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue),
                           Convert.ToDecimal(txtTotal.Text.Trim()), Convert.ToInt32(ddlAsig.SelectedValue), ingrdeduc, tipoplanilla, user))
                        {
                            throw new Exception("Error al insertar el registro");
                        }
                    }

                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Asignado Satisfactoriamente";

                    //aqui ingreso i bruto
                    //se verifica si la deduccion aplica a ingreso bruto
                    if (Convert.ToInt32(ddlAsig.SelectedValue) == 1)
                    {
                        DataTable dv = Neg_DevYDed.verificarIngresocnDeduccionIBruto(ingrdeduc).Tables[0];

                        if (Convert.ToBoolean(dv.Rows[0]["aplicarDeduccionIBruto"]))
                        {
                            if (!Neg_DevYDed.IngresosAplicaIBrutoBakIns(Convert.ToInt32(ddlAsig.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), ingrdeduc, Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToDecimal(txtTotal.Text.Trim())))
                            {
                                throw new Exception("Error al insertar ingreso");
                            }
                            //DataTable dt1 = new DataTable();
                            //DataSet ds = new DataSet();

                            //ds = Neg_DevYDed.IngresosPeriodoxTipoSel(Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), idDeduccionIBruto);

                            //if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            //{
                            //    dt1 = ds.Tables[0];
                            //    Neg_DevYDed.RegistrarIngresoEgresoAsociado(Convert.ToInt32(txtPeriodo.Text.Trim()), dt1, user);
                            //}
                        }
                    }
                    if (ddlAsig.SelectedValue == "1")
                        ObtenerDevengados();
                    else
                        ObtenerDeducciones();
                }
                //else
                //{
                //    alertValida.Visible = true;
                //    lblAlert.Visible = true;
                //    lblAlert.Text = "Debe ingresar datos validos";
                //}
                txtTotal.Text = "";
                txtHoras.Text = "";
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlTiposPlanilla.SelectedValue == "1")
            //    this.divSemana.Visible = true;
            //else
            //    this.divSemana.Visible = false;
            obtenerPeriodo();
        }
        private void obtenerPeriodo()
        {
            try
            {
                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.cargarUltPeriodoAbieCat(1, Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()), Convert.ToInt32(ddlUbicacion.SelectedValue.Trim()));

                if (dtPeriodo.Rows.Count > 0)
                {
                    txtPeriodo.Text = dtPeriodo[0].nperiodo.ToString();

                    if (!dtPeriodo[0].consolidar && Convert.ToInt32(ddlTiposPlanilla.SelectedValue.Trim()) == 1)
                    {
                        divSemana.Visible = true;
                    }
                    else
                    {
                        divSemana.Visible = false;
                    }

                }
                else
                {
                    txtPeriodo.Text = "0";
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "No hay periodo abierto que este vigente";
            }
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerIngresosyDeduccines();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void ObtenerIngresosyDeduccines()
        {
            if (validar())
            {
                //TxtNombreE.Text = dt.Rows[0]["nombrecompleto"].ToString();
                try
                {
                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(this.txtCodigo.Text.Trim()), 1);

                    if (dtEmp.Rows.Count > 0 && dtEmp.Rows[0]["idestado"].ToString().Trim() != "0")
                    {
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();
                        ObtenerDevengados();
                        ObtenerDeducciones();
                    }
                    else//No aplica a liquidación.
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "El empleado no se encuentra";
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
        void ObtenerDevengados()
        {
            int tipoplanilla = ddlTiposPlanilla.SelectedValue.Trim() == "0" ? 1 : Convert.ToInt32(ddlTiposPlanilla.SelectedValue);
            DataSet ds;

            if (divSemana.Visible)
            {
                ds = Neg_Planilla.obtenerDetalleIngresosPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
                     tipoplanilla);
            }
            else
            {
                ds = Neg_Planilla.obtenerDetalleIngresosPorEmpleadoAll(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(txtCodigo.Text.Trim()),
                     tipoplanilla);
            }

            gvIngresosEmp.DataSource = ds;

            gvIngresosEmp.DataMember = "detalle";
            gvIngresosEmp.DataBind();
        }
        void ObtenerDeducciones()
        {
            int tipoplanilla = ddlTiposPlanilla.SelectedValue.Trim() == "0" ? 1 : Convert.ToInt32(ddlTiposPlanilla.SelectedValue);
            DataSet ds;
            if (divSemana.Visible)
            {
                ds = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
            tipoplanilla);
            }
            else
            {
                ds = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleadoAll(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(txtCodigo.Text.Trim()),
            tipoplanilla);
            }

            GVDetNomEmpl.DataSource = ds;
            GVDetNomEmpl.DataMember = "detalle";
            GVDetNomEmpl.DataBind();

        }
        public bool validar()
        {
            if (txtPeriodo.Text.Trim() == "" || txtPeriodo.Text.Trim() == "0")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtPeriodo.Focus();
                return false;
            }

            if (txtCodigo.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtCodigo.Focus();
                return false;
            }

            return true;
        }
        //EGRESOS

        protected void GVDetNomEmpl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                    throw new Exception("Periodo Invalido");

                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                if (dtPeriodo[0].cerrado == 1)
                    throw new Exception("El periodo esta cerrado");

                int index = Convert.ToInt32(e.CommandArgument.ToString());

                int ID = (int)(GVDetNomEmpl.DataKeys[index][0]);
                int IdDeduccion = (int)(GVDetNomEmpl.DataKeys[index][1]);

                // Eliminar Egresos catorcenales
                if (e.CommandName.CompareTo("eliminar") == 0)
                {
                    if (Neg_DevYDed.DeduccionesEmpleadoEliminar(IdDeduccion, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
                        Convert.ToInt32(ddlSemana.SelectedValue), ID))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Deduccion Eliminada Satisfactoriamente";
                    }
                }

                TextBox totalc = (TextBox)GVDetNomEmpl.Rows[index].FindControl("txtTotal");
                decimal total = Convert.ToDecimal(totalc.Text.Trim());
                //Editar Egresos catorcenales
                if (e.CommandName.CompareTo("editar") == 0)
                {

                    if (Neg_DevYDed.DeduccioesEmpleadoEditar(IdDeduccion, total, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
                        Convert.ToInt32(ddlSemana.SelectedValue), ID))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Deduccion Editada Satisfactoriamente";
                    }

                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
            ObtenerDeducciones();
        }
        /// INGRESOS

        protected void gvIngresosEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                if (dtPeriodo[0].cerrado == 1)
                    throw new Exception("El periodo esta cerrado");

                int index = Convert.ToInt32(e.CommandArgument.ToString());

                int ID = (int)(gvIngresosEmp.DataKeys[index][0]);
                int IdDevengado = (int)(gvIngresosEmp.DataKeys[index][1]);

                // Eliminar Egresos catorcenales
                if (e.CommandName.CompareTo("eliminar") == 0)
                {
                    if (Neg_DevYDed.IngresosEmpleadoEliminar(IdDevengado, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
                        Convert.ToInt32(ddlSemana.SelectedValue)))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Ingreso Eliminada Satisfactoriamente";
                    }
                }


                //Editar Egresos catorcenales
                if (e.CommandName.CompareTo("editar") == 0)
                {
                    TextBox totalc = (TextBox)gvIngresosEmp.Rows[index].FindControl("txtTotal");
                    decimal total = Convert.ToDecimal(totalc.Text.Trim());
                    if (Neg_DevYDed.IngresosEmpleadoEditar(IdDevengado, total, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
                            Convert.ToInt32(ddlSemana.SelectedValue), 0))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Ingreso Editada Satisfactoriamente";
                    }
                }


            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
            ObtenerDevengados();
        }

        protected void ddlSemana_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerIngresosyDeduccines();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerPeriodo();
        }

        protected void ddlTipoIngrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            HabilitarCampos();
        }
        void HabilitarCampos()
        {
            if (ddlAsig.SelectedValue == "1")
            {
                divEgrs.Visible = false;
                divIngrs.Visible = true;
                if (ddlTipoIngrs.SelectedValue.Trim() == "1")
                {
                    divhex.Visible = true;
                    divtotal.Visible = false;
                }
                else
                {
                    divhex.Visible = false;
                    divtotal.Visible = true;
                }

            }
            if (ddlAsig.SelectedValue == "2")
            {
                divIngrs.Visible = false;
                divEgrs.Visible = true;
                divhex.Visible = false;
                divtotal.Visible = true;
            }
        }
    }
}