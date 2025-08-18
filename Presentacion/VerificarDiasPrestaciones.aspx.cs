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
    public partial class VerificarDiasPrestaciones : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
       //CREACION GRETHEL TERCERO 18-08-2017

        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Permisos NPermisos = new Neg_Permisos();
        #endregion       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limpiarCampos();
                txtFecCorte.Text = DateTime.Now.ToShortDateString();
            }
        }        
        protected void Button1_Click(object sender, EventArgs e)
        {
          
            if (txtCodEmp.Text.Trim().Length != 0)
            {
                try
                {
                    limpiarCampos();
                    DataTable dtDatosEmp = new DataTable();//Datos principales utilizados para la liquidacion,fechas,diasLaborados etc.
                    dtDatosEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtCodEmp.Text.Trim()), 1);                
                    if (dtDatosEmp.Rows.Count > 0 )//&& dtDatosEmp.Rows[0]["idestado"].ToString().Trim() == "1")//Si es un codigo válido.
                    {
                        if (dtDatosEmp.Rows[0]["idestado"].ToString().Trim()!="1")
                        {
                            txtFecCorte.Text = Convert.ToDateTime( dtDatosEmp.Rows[0]["fecha_egreso"]).ToShortDateString();
                        }
                        
                        Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());
                        TxtCodigo.InnerText = dtDatosEmp.Rows[0]["codigo_empleado"].ToString();
                        txtFechaing.InnerText = Convert.ToDateTime(dtDatosEmp.Rows[0]["fecha_ingreso"].ToString()).ToShortDateString();
                        txtNombre.InnerText = dtDatosEmp.Rows[0]["nombrecompleto"].ToString();
                        TxtTipoSalario.InnerText = dtDatosEmp.Rows[0]["idTipoSalario"].ToString() == "1" ? "Fijo" : "Variable";
                        TxtEstado.InnerText = dtDatosEmp.Rows[0]["idestado"].ToString() == "1" ? "Activo" : (dtDatosEmp.Rows[0]["idestado"].ToString() == "3") ? "Liquidado" : "Inactivo";
                        CargarGrid();//Obtengo la tabla meses y el detalle de la liquidacion para X persona.                       
                    }
                    else
                    {
                        alertValida.Visible = true;
                        alertValida.InnerText = "No se han encontrado registros";
                        limpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                  
                    alertValida.Visible = true;
                    alertValida.InnerText = ex.Message;
                }
            }
        }
        private void CargarGrid()
        {
            try
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                DataTable vacaciones = Neg_Liquidacion.CalcularDiasVacaciones(Convert.ToInt32(txtCodEmp.Text.Trim()), Convert.ToDateTime(txtFechaing.InnerText.Trim()),1,userDetail.getIDEmpresa());
                if (vacaciones.Rows.Count > 0)//Si no tiene los suficientes dias,no se podra calcular el detalle de la liquidacion.X Persona no aplica a liquidacion.
                {
                    GVLiquidacion.DataSource = vacaciones;
                    GVLiquidacion.DataBind();
                   
                    DataTable desc=NPermisos.PermisosVacEmpleadoDetalleSel(Convert.ToInt32(txtCodEmp.Text.Trim()), Convert.ToDateTime(txtFecCorte.Text.Trim()), 1,1).Tables[0];
                    GVDEtalleDesc.DataSource = desc;
                    GVDEtalleDesc.DataBind();

                    //DataTable subs = NPermisos.SubsidiosEmpleadoDetalleSel(Convert.ToInt32(txtCodEmp.Text.Trim()),1);//NPermisos.PermisosVacEmpleadoDetalleSel(Convert.ToInt32(txtCodEmp.Text.Trim()), Convert.ToDateTime(txtFecCorte.Text.Trim()), 1, 5).Tables[0];
                    //GVDetalleSubsidios.DataSource = subs;
                    //GVDetalleSubsidios.DataBind();

                    DataTable pag = NPermisos.VacacionesPagadasxEmpDetalleSel(Convert.ToInt32(txtCodEmp.Text.Trim()), Convert.ToDateTime(txtFecCorte.Text.Trim()), 1).Tables[0];
                    GVDEtallePag.DataSource = pag;
                    GVDEtallePag.DataBind();

                }
                else//No aplica a liquidación.
                {
                    alertValida.Visible = true;
                    alertValida.InnerText = "No hay registros";
                    limpiarCampos();
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }        
                
       
        //si es->1 Pantalla liquidacion individual,tomo la fecha de egreso real.
        //si es->2 Pantalla VPasivoLaboral.aspx ,Solamennte para obtener salario mayor y promedio,tomo siempre el dia actual,(getdate()).    
        void limpiarCampos()
        {
            TxtCodigo.InnerText = "";
            txtFechaing.InnerText = "";
            txtNombre.InnerText = "";
            TxtTipoSalario.InnerText = "";
          
            GVLiquidacion.DataSource = null;
            GVLiquidacion.DataBind();
            GVDEtalleDesc.DataSource = null;
            GVDEtalleDesc.DataBind();
            //GVDetalleSubsidios.DataSource = null;
            //GVDetalleSubsidios.DataBind();
            GVDEtallePag.DataSource = null;
            GVDEtallePag.DataBind();
            lblmes.InnerText = "";
        }
        double total = 0;
        protected void GVDEtalleDesc_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Total"));               

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Totales";
                e.Row.Cells[6].Text = total.ToString("n2");
               
                e.Row.Font.Bold = true;
            }
        }
        double dias = 0;
        protected void GVDEtallePag_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dias += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "diasVacacionesPagar"));                

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Totales";
                e.Row.Cells[2].Text = dias.ToString("n2");
               
                e.Row.Font.Bold = true;
            }
        }
        double subsidios = 0;
        protected void GVDetalleSubsidios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                subsidios += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "diasprestaciones"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "Total";
                e.Row.Cells[3].Text = subsidios.ToString("n2");

                e.Row.Font.Bold = true;
            }
        }
    }
}