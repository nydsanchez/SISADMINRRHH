using System;
using System.Data;
using Negocios;

namespace NominaRRHH.Presentacion
{
    public partial class EmpleadosRevertirBaja : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        //Neg_Empleados NegEmp = new Neg_Empleados();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtRepeticiones.Text = "1";              
            }
        }

       void MessageError(string msg)
        {
            alertSucces.Visible = false;
            alertValida.Visible = true;
            lblAlert.Visible = true;
            lblAlert.Text = msg;
        }
       
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerDatosEmpleado();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void ObtenerDatosEmpleado()
        {
            if (validar())
            {
                try
                {
                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(this.txtCodigo.Text.Trim()), 1);

                    if (dtEmp.Rows.Count > 0 && (dtEmp.Rows[0]["idestado"].ToString().Trim() != "0" && dtEmp.Rows[0]["idestado"].ToString().Trim() != "1"))
                    {
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();
                    }
                    else//No aplica a reversion.
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;

                        if (dtEmp.Rows.Count > 0)
                        {
                            if(dtEmp.Rows[0]["idestado"].ToString().Trim() == "0")
                                lblAlert.Text = "El empleado se encuentra en estado inactivo";

                            if(dtEmp.Rows[0]["idestado"].ToString().Trim() == "1")
                                lblAlert.Text = "El empleado se encuentra en estado activo";
                        }
                        else
                            lblAlert.Text = "El empleado no se encuentra";
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
        public bool validar()
        {
            if (txtCodigo.Text.Trim() == "")
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtCodigo.Focus();
                return false;
            }

            return true;
        }
        
        protected void btnRevertir_Click(object sender, EventArgs e)
        {
            
            if (validar() == true)
            {
                int codigo = int.Parse(txtCodigo.Text.Trim());
                string msg = new Neg_Empleados().RevertirBaja(codigo);
                alertSucces.Visible = true;
                alertValida.Visible = false;
                lblAlert.Visible = false;
                LblSuccess.Visible = true;
                LblSuccess.Text = msg;
            }
        }
    }

}
