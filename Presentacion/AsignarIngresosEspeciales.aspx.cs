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
    public partial class AsignarIngresosEspeciales : System.Web.UI.Page
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
                obtenerIngresosEspeciales();

            }
        }
        private void obtenerIngresosEspeciales()
        {
            this.ddlTipIng.DataSource = Neg_DevYDed.cargarIngresos(0);
            this.ddlTipIng.DataValueField = "idDevengado";
            this.ddlTipIng.DataTextField = "devengadoNombre";
            this.ddlTipIng.DataBind();

        }
        private void obtenerPeriodo()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtcodigoAsig.Text.Trim()))
                {
                    DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtcodigoAsig.Text);
                    DataTable ubicacion = Neg_Catalogos.seleccionarUbicacionesxCod(Convert.ToInt32(DetEmpleados.Rows[0]["codigo_ubicacion"]));
                    dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, Convert.ToInt32(ubicacion.Rows[0]["tplanilla"]), 0);
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
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "No hay periodo abierto que este vigente";
            }
        }
        void limpiarCampos()
        {
            txtTotal.Text = "0.00";

        }
        public bool validar()
        {
            try
            {
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

            }
            catch (Exception ex)
            {
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
                try
                {
                    string user = Convert.ToString(this.Page.Session["usuario"]);

                    int recurrente = chkRecurrente.Checked ? 1 : 0;

                    if (Neg_DevYDed.IngresosEspecialesEmpleadoIns(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlTipIng.SelectedValue), Convert.ToDecimal(txtTotal.Text.Trim()), user, recurrente, txtObservEmpl.Text))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Actualizacion realizada Satisfactoriamente";
                        //Actualizar los campos

                        limpiarCampos();
                        obtenerIngresos();
                    }
                    else
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Ya Existe Un ingreso De Este Tipo Para Este Periodo y Empleado";
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
        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            try
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);
                if (validar())
                {
                    if (!Neg_DevYDed.SalarioEspecialEmpleadoIns(Convert.ToInt32(txtcodigoAsig.Text.Trim()), Convert.ToDecimal(TxtSalarioEsp.Text.Trim()), user))
                    {
                        throw new Exception("Error al actualizar Salario Especial");
                    }
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Actualizacion realizada Satisfactoriamente";
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        protected void GVIngresos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow selectedRow = this.GVIngresos.Rows[Convert.ToInt32(e.RowIndex)];
            int indice = selectedRow.DataItemIndex;
            int id = Convert.ToInt32(GVIngresos.Rows[GVIngresos.TabIndex].Cells[1].Text.Trim());
            if (Neg_DevYDed.DeduccionOrdinariaEliminar(id))
            {

            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Error Al Eliminar El Registro";
            }
        }
        protected void GVIngresos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["estado"].ToString().Trim() == "1")
            {
                btnGuardar.Visible = false;
                btnEditar.Visible = true;
                btnDeshabilitar.Visible = true;

                this.txtTotal.Text = GVIngresos.Rows[GVIngresos.SelectedIndex].Cells[3].Text.Trim();
                editarhide.Visible = false;
                editarhide2.Visible = false;

                editarhide6.Visible = false;
                editarhide5.Visible = true;

                divsalario.Visible = false;
                Session["ID"] = GVIngresos.Rows[GVIngresos.SelectedIndex].Cells[1].Text.Trim();

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
            if (Neg_DevYDed.EditarIngresosEspxEmpleado(Convert.ToInt32(Session["ID"].ToString()), Convert.ToDecimal(this.txtTotal.Text.Trim()), 1, user))
            {

                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;

                LblSuccess.Text = "Actualizacion realizada Satisfactoriamente";
                HabilitarCampos();
                obtenerIngresos();

            }
        }
        private void HabilitarCampos()
        {
            txtTotal.Text = "";

            editarhide.Visible = true;
            editarhide2.Visible = true;

            editarhide6.Visible = true;
            editarhide5.Visible = true;
            btnGuardar.Visible = true;
            btnEditar.Visible = false;
            btnDeshabilitar.Visible = false;
        }
        protected void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            try
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);
                if (Neg_DevYDed.EditarIngresosEspxEmpleado(Convert.ToInt32(Session["ID"].ToString()), Convert.ToDecimal(this.txtTotal.Text.Trim()), 0, user))
                {

                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;

                    LblSuccess.Text = "Actualizacion realizada Satisfactoriamente";
                    HabilitarCampos();
                    obtenerIngresos();
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        private void ObtenerDatos()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtcodigoAsig.Text.Trim()))
                {
                    obtenerPeriodo();
                    Neg_Liquidacion.Globales.fechaR = DateTime.Now;

                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtcodigoAsig.Text.Trim()), 2);

                    if (dtEmp.Rows.Count > 0)
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
                        //Session["parametro"] = dtEmp.Rows[0]["parametro"].ToString();
                        //if (Convert.ToBoolean(dtEmp.Rows[0]["parametro"]))
                        //{
                            divsalario.Visible = true;
                            if (Convert.ToDecimal(dtEmp.Rows[0]["salariop"]) == 0)
                            {
                                TxtSalarioEsp.Text = dtEmp.Rows[0]["salariomensual"].ToString();
                            }
                            else
                            {
                                TxtSalarioEsp.Text = dtEmp.Rows[0]["salariop"].ToString();
                            }
                        //}
                        //else
                        //{
                        //    divsalario.Visible = false;
                        //}
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
            catch (Exception)
            {
                //LimpiarTxt();
            }
        }

        protected void txtcodigoAsig_TextChanged(object sender, EventArgs e)
        {
            limpiarCampos();
            ObtenerDatos();
            obtenerIngresos();
        }
        void obtenerIngresos()
        {
            GVIngresos.DataSource = Neg_DevYDed.IngresosEspecialesSel(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()));
            GVIngresos.DataBind();
        }
        decimal total = 0;
        protected void GVIngresos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total";
                e.Row.Cells[3].Text = total.ToString("n2");
                e.Row.Font.Bold = true;

            }
        }
        protected void GVIngresos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVIngresos.PageIndex = e.NewPageIndex;
            GVIngresos.DataBind();

        }
    }
}