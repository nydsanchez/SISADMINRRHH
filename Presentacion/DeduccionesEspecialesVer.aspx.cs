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
    public partial class DeduccionesEspecialesVer : System.Web.UI.Page
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
                
            }
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
                        Session["periodo"] = dtPeriodo[0].nperiodo.ToString();
                    }
                    else
                    {
                        Session["periodo"] = "0";
                    }
                    this.txtPeriodo.Text = Session["periodo"].ToString();
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "No hay periodo abierto que este vigente";
            }
        }
        void limpiarCampos() {
          
            TxtNetoProm.Text = "0.00";
            txtIndemnizacion.Text = "0.00";
            txtAguinaldo.Text = "0.00";
            txtTotalVac.Text = "0.00";
            txtSubTotal.Text = "0.00";
            TxtSaldoDisp.Text = "0.00";
           
        }
       
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            
                try {
                   
                }
                catch (Exception ex) {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Los campos contienen caracteres invalidos";
                }
           
        } 
        
        private void ObtenerIngresosEgresosVigentes()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtcodigoAsig.Text.Trim()))
                {
                    obtenerPeriodo();
                    Neg_Liquidacion.Globales.fechaR = DateTime.Now;                    

                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtcodigoAsig.Text.Trim()), 2);
                    
                    if (dtEmp.Rows.Count > 0 )
                    {
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();
                        if (dtEmp.Rows[0]["idestado"].ToString().Trim()=="1")
                        {
                            this.txtEstado.Text = "Activo";
                        }else if (dtEmp.Rows[0]["idestado"].ToString().Trim() == "3")
                        {
                            this.txtEstado.Text = "Liquidado";
                        }
                        else
                        {
                            this.txtEstado.Text = "Inactivo";
                        }                            

                        DataSet ds = Neg_Liquidacion.ObtenerDatosLiquidacion(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()),0,0,0,0,0,true);//Obtengo la tabla de meses,y el detalle de liquidacion de X Persona.
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//Si no tiene los suficientes dias,no se podra calcular el detalle de la liquidacion.X Persona no aplica a liquidacion.
                        {

                            DataTable campos = ds.Tables[1];
                            AsignarCampos(campos);

                        }
                        else//No aplica a liquidación.
                        {
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "No se han encontrado datos de Liquidacion Acumulada";
                        }
                        CalcularNetoPromedio();
                        cargarDeducciones();
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
        private void AsignarCampos(DataTable dt)
        {
            if (dt.Rows.Count != 0)
            {
                decimal Indemnizacion = 0, Aguinaldo = 0,  TotalVac = 0,  Neto = 0, Subtotal = 0;
                Indemnizacion = Convert.ToDecimal(dt.Rows[0]["Indemnizacion"].ToString());
                Aguinaldo = Convert.ToDecimal(dt.Rows[0]["Aguinaldo"].ToString());
                TotalVac = Convert.ToDecimal(dt.Rows[0]["Vacaciones"].ToString());
                Subtotal = Convert.ToDecimal(dt.Rows[0]["totalPagar"].ToString());

                txtIndemnizacion.Text = Convert.ToString(Indemnizacion.ToString("n2"));
                txtAguinaldo.Text = Convert.ToString(Aguinaldo.ToString("n2"));
                txtTotalVac.Text = Convert.ToString(TotalVac.ToString("n2"));
                //Neto = Indemnizacion + Aguinaldo + TotalVac;
                txtSubTotal.Text = Convert.ToString(Subtotal.ToString("n2"));

            }
        }
        void CalcularNetoPromedio()
        {
            DataSet ds;
            decimal NetoPromedio=0;

            ds = Neg_Liquidacion.ObtenerNetoxPlanillas(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()));//Obtengo el detalle de la deduccion  
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                NetoPromedio = Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row["neto"]))) / ds.Tables[0].Rows.Count;
            }
            else {
                NetoPromedio = 0;
            }
            TxtNetoProm.Text = NetoPromedio.ToString("n2");
        }
        void cargarDeducciones()
        {
            DataSet ds;
            decimal TotalDeduc = 0;
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //ds = Neg_DevYDed.DeduccionesOrdinariasObtener(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()),1,0);//Obtengo el detalle de la deduccion  
            DataTable dtEgresos = new DataTable();
            dtEgresos = Neg_DevYDed.CalcularDetalleDeduccionesxEmp(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), 1, 0, Convert.ToDecimal(txtSubTotal.Text),DateTime.Now, userDetail.getIDEmpresa());
           
            GVDeducciones.DataSource = dtEgresos;
            GVDeducciones.DataBind();
            TotalDeduc += Convert.ToDecimal(dtEgresos.AsEnumerable().Sum(row => Convert.ToDecimal(row["Debe"])));
            

            ds = Neg_DevYDed.ObtenerDeduccionesVigentes(Convert.ToInt32(this.txtcodigoAsig.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()));//Obtengo el detalle de la deduccion              
            GvEgresos.DataSource = ds;
            GvEgresos.DataBind();

            TotalDeduc += Convert.ToDecimal(ds.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row["total"])));

            //se calcula saldo disponible
            TotalDeduc = Convert.ToDecimal(txtSubTotal.Text) - TotalDeduc;
            TxtSaldoDisp.Text = TotalDeduc.ToString("n2");
        }        
        protected void txtcodigoAsig_TextChanged(object sender, EventArgs e)
        {
            limpiarCampos();
            ObtenerIngresosEgresosVigentes();
        }
        decimal total = 0;
        protected void GVDeducciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Debe"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "Deuda Total";
                e.Row.Cells[5].Text = total.ToString("n2");
                e.Row.Font.Bold = true;

            }
        }
        protected void GVDeducciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDeducciones.PageIndex = e.NewPageIndex;
            GVDeducciones.DataBind();
            cargarDeducciones();
        }

        protected void txtPeriodo_TextChanged(object sender, EventArgs e)
        {
            cargarDeducciones();
        }
    }
}