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
    public partial class DetallePorEmpleado : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerTiposPlanilla();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            
            if (validar())
            {
                if(ddlTiposPlanilla.SelectedValue == "1")
                { 
                    DataTable ds = new DataTable();
                    GVDetNomEmpl.DataSource = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
                    Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
                    GVDetNomEmpl.DataMember = "detalle";
                    GVDetNomEmpl.DataBind();

                    GVDetalleIngrs.DataSource = Neg_Planilla.obtenerDetalleIngresosPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
                    Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
                    GVDetalleIngrs.DataMember = "detalle";
                    GVDetalleIngrs.DataBind();

                    ds = Neg_Planilla.obtenerPlanillaPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()));

                    if(ds.Rows.Count > 0)
                    {
                        txtHorast.Text = ds.Rows[0]["horast"].ToString();
                        txtSalario.Text = ds.Rows[0]["salario"].ToString();
                        txtHe.Text = ds.Rows[0]["tiempoHE"].ToString();
                        txtValorHe.Text = ds.Rows[0]["HE"].ToString();
                        txtInss.Text = ds.Rows[0]["dsegurosocial"].ToString();
                        txtIr.Text = ds.Rows[0]["dimpuestos"].ToString();
                        txtTotIngresos.Text = ds.Rows[0]["ingresos"].ToString();
                        txtTotEgresos.Text = ds.Rows[0]["egresos"].ToString();
                        txtNeto.Text = ds.Rows[0]["neto"].ToString();
                    }
                    gridIngresos.Visible = true;
                    gridEgresos.Visible = true;
                }
            }
            if (ddlTiposPlanilla.SelectedValue == "2")
            {
                DataTable ds = new DataTable();
                GVDetNomEmpl.DataSource = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), 0, Convert.ToInt32(txtCodigo.Text.Trim()),
                Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
                GVDetNomEmpl.DataMember = "detalle";
                GVDetNomEmpl.DataBind();

                GVDetalleIngrs.DataSource = Neg_Planilla.obtenerDetalleIngresosPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), 0, Convert.ToInt32(txtCodigo.Text.Trim()),
                Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
                GVDetalleIngrs.DataMember = "detalle";
                GVDetalleIngrs.DataBind();

                ds = Neg_Planilla.obtenerPlanillaPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), 0, Convert.ToInt32(txtCodigo.Text.Trim()));

                if (ds.Rows.Count > 0)
                {
                    txtHorast.Text = ds.Rows[0]["horast"].ToString();
                    txtSalario.Text = ds.Rows[0]["salario"].ToString();
                    txtHe.Text = ds.Rows[0]["tiempoHE"].ToString();
                    txtValorHe.Text = ds.Rows[0]["HE"].ToString();
                    txtInss.Text = ds.Rows[0]["dsegurosocial"].ToString();
                    txtIr.Text = ds.Rows[0]["dimpuestos"].ToString();
                    txtTotIngresos.Text = ds.Rows[0]["ingresos"].ToString();
                    txtTotEgresos.Text = ds.Rows[0]["egresos"].ToString();
                    txtNeto.Text = ds.Rows[0]["neto"].ToString();
                }
                gridIngresos.Visible = true;
                gridEgresos.Visible = true;

            }
        }

        public bool validar()
        {
            if (txtPeriodo.Text.Trim() == "")
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

        private void obtenerTiposPlanilla()
        {
            this.ddlTiposPlanilla.DataSource = Neg_Planilla.cargarTiposPlanilla();
            this.ddlTiposPlanilla.DataMember = "planillas";
            this.ddlTiposPlanilla.DataValueField = "idNomina";
            this.ddlTiposPlanilla.DataTextField = "Descripcion";
            this.ddlTiposPlanilla.DataBind();
        }

        protected void GVDetNomEmpl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox Id = ((TextBox)GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].FindControl("txtId"));
            TextBox total = ((TextBox)GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].FindControl("txtTotal"));
            GridViewRow row = GVDetNomEmpl.SelectedRow;
            int fila = Convert.ToInt32(row.DataItemIndex);
            Session["fila"] = fila;
            total.ReadOnly = false;
            if (total.ReadOnly == true)
            {
                Session["total"] = 0;
            }
            else
            {
                Session["total"] = total.ReadOnly;
            }
            Session["Id"] = Id.Text;
            Session["total"] = total.Text;

        }

        //EGRESOS
        //protected void GVDetNomEmpl_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName.CompareTo("eliminar") == 0)
        //    {
        //        int ID = (int)GVDetNomEmpl.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
        //        string p = Convert.ToString(Session["total"]);
        //        if (Neg_DevYDed.DeduccionesEmpleadoEliminar(ID, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
        //            Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToDecimal(p)))
        //        {
        //            GVDetNomEmpl.DataSource = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
        //            Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
        //            GVDetNomEmpl.DataMember = "detalle";
        //            GVDetNomEmpl.DataBind();

        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Deduccion Eliminada Satisfactoriamente";
        //        }
        //    }

        //    if (e.CommandName.CompareTo("editar") == 0)
        //    {
        //        int ID = (int)GVDetNomEmpl.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
        //        string p = Convert.ToString(Session["total"]);

        //            GridViewRow selectedRow = GVDetNomEmpl.Rows[Convert.ToInt32(Session["fila"])];
        //            TextBox Total = ((TextBox)selectedRow.FindControl("txtTotal"));
        //            decimal total = Convert.ToDecimal(Total.Text);


        //            if (Neg_DevYDed.DeduccioesEmpleadoEditar(ID, total, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
        //                Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToDecimal(p)))
        //            {
        //                GVDetNomEmpl.DataSource = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
        //                Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
        //                GVDetNomEmpl.DataMember = "detalle";
        //                GVDetNomEmpl.DataBind();

        //                alertValida.Visible = false;
        //                alertSucces.Visible = true;
        //                LblSuccess.Visible = true;
        //                LblSuccess.Text = "Deduccion Editada Satisfactoriamente";
        //            }
        //    }
        //}
         //INGRESOS
        protected void GVDetalleIngrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox Id = ((TextBox)GVDetalleIngrs.Rows[GVDetalleIngrs.SelectedIndex].FindControl("txtId"));
            TextBox total = ((TextBox)GVDetalleIngrs.Rows[GVDetalleIngrs.SelectedIndex].FindControl("txtTotal"));
            GridViewRow row = GVDetalleIngrs.SelectedRow;
            int fila = Convert.ToInt32(row.DataItemIndex);
            Session["fila"] = fila;
            total.ReadOnly = false;
            if (total.ReadOnly == true)
            {
                Session["total"] = 0;
            }
            else
            {
                Session["total"] = total.ReadOnly;
            }
            Session["Id"] = Id.Text;
        }

        protected void GVDetalleIngrs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                int ID = (int)GVDetalleIngrs.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                if (Neg_DevYDed.IngresosEmpleadoEliminar(ID, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue)))
                {
                    GVDetalleIngrs.DataSource = Neg_Planilla.obtenerDetalleIngresosPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
                    Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
                    GVDetalleIngrs.DataMember = "detalle";
                    GVDetalleIngrs.DataBind();

                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Eliminado Satisfactoriamente";
                }
            }

            if (e.CommandName.CompareTo("editar") == 0)
            {
                int ID = (int)GVDetalleIngrs.DataKeys[Convert.ToInt32(e.CommandArgument)].Value;
                string p = Convert.ToString(Session["total"]);

                if (p == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Seleccione Una Fila Para Editar";
                }
                if (p == "False")
                {
                    GridViewRow selectedRow = GVDetalleIngrs.Rows[Convert.ToInt32(Session["fila"])];
                    TextBox Total = ((TextBox)selectedRow.FindControl("txtTotal"));
                    decimal total = Convert.ToDecimal(Total.Text);
                    
                    if (Neg_DevYDed.IngresosEmpleadoEditar(ID, total, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue),0))
                    {
                        GVDetalleIngrs.DataSource = Neg_Planilla.obtenerDetalleIngresosPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
                        Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
                        GVDetalleIngrs.DataMember = "detalle";
                        GVDetalleIngrs.DataBind();

                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Ingreso Editado Satisfactoriamente";
                    }
                }
            }
        }

        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    string user = Convert.ToString(this.Page.Session["usuario"]);
        //    if(validarEdicion() && ddlTiposPlanilla.SelectedValue == "1")
        //    {
        //        if(Neg_Planilla.EditarPlanillaPorEmpleado(Convert.ToDecimal(txtHorast.Text.Trim()), Convert.ToDecimal(txtSalario.Text.Trim()), Convert.ToDecimal(txtHe.Text.Trim()),
        //            Convert.ToDecimal(txtValorHe.Text.Trim()), Convert.ToDecimal(txtInss.Text.Trim()), Convert.ToDecimal(txtIr.Text.Trim()), Convert.ToDecimal(txtTotIngresos.Text.Trim()),
        //            Convert.ToDecimal(txtTotEgresos.Text.Trim()), Convert.ToDecimal(txtNeto.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim())))
        //        {
        //            Neg_Planilla.ProcesarPlanillaPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()), user,
        //                Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Planilla Editada Satisfactoriamente";
        //            limpiar();
        //        }
        //        else
        //        { 
        //            alertValida.Visible = true;
        //            lblAlert.Visible = true;
        //            lblAlert.Text = "Error al Editar Planilla";
        //        }
        //    }

        //    if (validarEdicion() && ddlTiposPlanilla.SelectedValue == "2")
        //    { 
               
        //    }
        //}

        private void limpiar()
        {
            txtHorast.Text = "";
            txtSalario.Text = "";
            txtHe.Text = "";
            txtValorHe.Text = "";
            txtInss.Text = "";
            txtIr.Text = "";
            txtTotIngresos.Text = "";
            txtTotEgresos.Text = "";
            txtNeto.Text = "";
            gridEgresos.Visible = false;
            gridIngresos.Visible = false;
        }

        private bool validarEdicion()
        {
            if(txtHorast.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHorast.Focus();
                return false;
            }
            if(txtSalario.Text.Trim () == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtSalario.Focus();
                return false;
            }
            if(txtHe.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHe.Focus();
                return false;
            }

            if (txtValorHe.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtValorHe.Focus();
                return false;
            }

            if(txtInss.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtInss.Focus();
                return false;
            }

            if(txtIr.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtIr.Focus();
                return false;
            }

            if (txtTotIngresos.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtTotIngresos.Focus();
                return false;
            }

            if(txtTotEgresos.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtTotEgresos.Focus();
                return false;
            }

            if(txtNeto.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtNeto.Focus();
                return false;
            }
            return true;
        }
        protected void ddlTiposPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTiposPlanilla.SelectedValue == "1")
                this.divSemana.Visible = true;
            else
                this.divSemana.Visible = false;
        }
    }
}

