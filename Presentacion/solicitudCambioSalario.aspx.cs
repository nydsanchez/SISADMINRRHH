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
    public partial class solicitudCambioSalario : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        //ULTIMA MODIFICACION 17112016 GRETHEL TERCERO
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerProcesos();
                obtenerUbicaciones();
                LimpiarCampos();
            }
        }
        private void obtenerProcesos()
        {
            this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }
        private void obtenerUbicaciones()
        {
            this.ddlUbicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacion.DataMember = "ubicaciones";
            this.ddlUbicacion.DataValueField = "codigo_ubicacion";
            this.ddlUbicacion.DataTextField = "nombre_ubicacion";
            this.ddlUbicacion.DataBind();
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
          
            //Busqueda de empleado por Codigo
            if (txtCodigo.Text != "")
            {
                DataTable DetEmpleados;
                DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtCodigo.Text);
                if (DetEmpleados.Rows.Count > 0)
                {
                    txtNombreEmpleado.Text = DetEmpleados.Rows[0]["nombrecompleto"].ToString();                    
                    ddlProceso.SelectedValue = DetEmpleados.Rows[0]["codigo_depto"].ToString();
                    txtSalarioActual.Text = DetEmpleados.Rows[0]["salariomensual"].ToString();
                    this.alertValida.Visible = false;
                    this.lblAlert.Visible = false;                   
                }
                else
                {

                    this.alertValida.Visible = true;
                    this.lblAlert.Visible = true;
                    this.lblAlert.Text = "No se encontraron resultados";
                }
            }
        }

        protected void btnEnviarS_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                try
                {
                    string user = Convert.ToString(this.Page.Session["usuario"]);
                    if (Neg_Empleados.CambioSalarioGuardar(Convert.ToInt32(this.txtCodigo.Text.Trim()), Convert.ToInt32(ddlUbicacion.SelectedValue.Trim()), Convert.ToInt32(this.ddlProceso.SelectedValue.Trim()), Convert.ToDecimal(this.txtSalarioActual.Text.Trim()), Convert.ToDecimal(this.TxtSalarioNew.Text.Trim()),TxtObservacion.Text.Trim(), user))
                    {
                        LimpiarCampos();
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Cambio de Salario actualizado Satisfactoriamente";
                        
                    }
                    else
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Hubo un error al actualizar el salario del empleado";
                    }
                }
                catch (Exception ex)
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Los campos contienen caracteres invalidos";
                }
            }
        }
        void LimpiarCampos() {
            this.txtCodigo.Text = "";
            txtSalarioActual.Text = "0.00";
            txtNombreEmpleado.Text = "";
            this.TxtSalarioNew.Text = "0.00";
            TxtObservacion.Text = "";
        }
        public bool validar()
        {
            try
            {
                if (txtCodigo.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un Codigo valido";
                    //txtCodigo.Focus();
                    return false;
                }

                if (TxtSalarioNew.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un Salario valido";
                    TxtSalarioNew.Focus();
                    return false;
                }
                if (Convert.ToDecimal(this.txtSalarioActual.Text.Trim()) == Convert.ToDecimal(this.TxtSalarioNew.Text.Trim()))
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "El cambio de salario no es valido";
                    TxtSalarioNew.Focus();
                    return false;
                }
                if (TxtObservacion.Text.Trim() == "")
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese una Observacion";
                    TxtObservacion.Focus();
                    return false;
                }
                             
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Error validando datos ingresados";
                return false;
            }
            return true;
        }
    }
}