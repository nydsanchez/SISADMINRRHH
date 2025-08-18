using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using System.Data;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class DetalleDDEmp : System.Web.UI.Page
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
                Session["total"] = true;
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validar())
            {              
                try
                {
                    ObtenerDeducciones();
                }
                catch (Exception ex)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = ex.Message;
                }
            }
        }
        void ObtenerDeducciones (){
            DataSet ds = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleado(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),
                    Convert.ToInt32(ddlTiposPlanilla.SelectedValue));
            try
            {
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    txtcodigoAsig.Text = dt.Rows[0]["codigo_empleado"].ToString();
                    TxtNombreE.Text = dt.Rows[0]["nombrecompleto"].ToString();

                    GVDetNomEmpl.DataSource = ds;
                    GVDetNomEmpl.DataMember = "detalle";
                    GVDetNomEmpl.DataBind();
                }
                else
                {
                    throw new Exception("No se encontraron registros");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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

        protected void GVDetNomEmpl_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextBox Id = ((TextBox)GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].FindControl("txtId"));
            TextBox IdDeduccion = ((TextBox)GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].FindControl("txtIdDeduccion"));
            TextBox total = ((TextBox)GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].FindControl("txtTotal"));
            GridViewRow row = GVDetNomEmpl.SelectedRow;
            int fila = Convert.ToInt32(row.DataItemIndex);
            total.ReadOnly = false;
            Session["fila"] = fila;

            Session["total"] = false;

        }

        //EGRESOS
        protected void GVDetNomEmpl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try {
                int ID = (int)(GVDetNomEmpl.DataKeys[Convert.ToInt32(e.CommandArgument)][0]);
                int IdDeduccion = (int)(GVDetNomEmpl.DataKeys[Convert.ToInt32(e.CommandArgument)][1]);
                bool activototal = Convert.ToBoolean(Session["total"]);
                // Eliminar Egresos catorcenales
                if (e.CommandName.CompareTo("eliminar") == 0 && ddlTiposPlanilla.SelectedValue == "1")
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

                // Eliminar Egresos quincenales
                if (e.CommandName.CompareTo("eliminar") == 0 && ddlTiposPlanilla.SelectedValue == "2")
                {
                    if (Neg_DevYDed.DeduccionesEmpleadoEliminar(IdDeduccion, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
                        0, ID))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Deduccion Eliminada Satisfactoriamente";
                    }
                }

                //Editar Egresos catorcenales
                if (e.CommandName.CompareTo("editar") == 0 && ddlTiposPlanilla.SelectedValue == "1")
                {

                    if (activototal)
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Favor Seleccione Una Fila Para Editar";
                        return;
                    }
                    else
                    {
                        GridViewRow selectedRow = GVDetNomEmpl.Rows[Convert.ToInt32(Session["fila"])];
                        TextBox Total = ((TextBox)selectedRow.FindControl("txtTotal"));
                        decimal total = Convert.ToDecimal(Total.Text);

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
                //Editar Egresos quincenales
                if (e.CommandName.CompareTo("editar") == 0 && ddlTiposPlanilla.SelectedValue == "2")
                {

                    if (activototal)
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Favor Seleccione Una Fila Para Editar";
                        return;
                    }
                    else
                    {
                        GridViewRow selectedRow = GVDetNomEmpl.Rows[Convert.ToInt32(Session["fila"])];
                        TextBox Total = ((TextBox)selectedRow.FindControl("txtTotal"));
                        decimal total = Convert.ToDecimal(Total.Text);
                    
                        if (Neg_DevYDed.DeduccioesEmpleadoEditar(IdDeduccion, total, Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()),
                            0, ID))
                        {
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Deduccion Editada Satisfactoriamente";
                        }
                    }
                }
            }
            catch (Exception ex) {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
            ObtenerDeducciones();
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