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
    public partial class AsignacionIngrYDeducRevision : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        //Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        //Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
               // obtenerPeriodo();
               
                // txtFechRenuncia.Text = DateTime.Now.ToShortDateString();
            }
        }        
        private void obtenerPeriodo()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtCodigo.Text.Trim()))
                {
                    DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtCodigo.Text);
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
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try {
                ObtenerIngresosyDeduccines();
            } catch (Exception ex) {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void ObtenerIngresosyDeduccines() {
            //if (validar())
            //{
                //TxtNombreE.Text = dt.Rows[0]["nombrecompleto"].ToString();
                try
                {
                if (!string.IsNullOrEmpty(this.txtCodigo.Text.Trim()))
                {
                    obtenerPeriodo();                   
                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtCodigo.Text.Trim()), 2);
                   
                    if (dtEmp.Rows.Count > 0)
                    {
                        Session["estado"] = dtEmp.Rows[0]["idestado"].ToString().Trim();
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();

                        if (dtEmp.Rows[0]["idestado"].ToString().Trim() != "0")
                        {                            
                            ObtenerDevengados();
                            ObtenerDeducciones();
                        }
                        else//No aplica a liquidación.
                        {
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "El empleado no esta Activo";
                        }
                    }
                    else//No aplica a liquidación.
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "El empleado no se encuentra";
                    }
                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Debe Ingresar Codigo de Empleado";
                }

                //////////////////************               
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
           // }

        }
        void ObtenerDevengados()
        {         
            DataSet ds;          
            ds = Neg_Planilla.obtenerDetalleIngresosPorEmpleadoAll(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(txtCodigo.Text.Trim()),
                     1);

            gvIngresosEmp.DataSource = ds;
           
            gvIngresosEmp.DataMember = "detalle";
            gvIngresosEmp.DataBind();
        }
        void ObtenerDeducciones()
        {           
            DataSet ds;          
            ds = Neg_Planilla.obtenerDetalleDeduccionesPorEmpleadoAll(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(txtCodigo.Text.Trim()),
            1);

            GVDetNomEmpl.DataSource = ds;
            GVDetNomEmpl.DataMember = "detalle";
            GVDetNomEmpl.DataBind();

        }

        protected void txtPeriodo_TextChanged(object sender, EventArgs e)
        {
            ObtenerDevengados();
            ObtenerDeducciones();
        }
        
    }
}