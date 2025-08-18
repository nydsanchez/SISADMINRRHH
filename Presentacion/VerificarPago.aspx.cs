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
    public partial class VerificarPago : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        //ULTIMA MODIFICACION GRETHEL TERCERO 31-10-2016

        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
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
                    dtDatosEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtCodEmp.Text.Trim()), 2);                
                    if (dtDatosEmp.Rows.Count > 0 && dtDatosEmp.Rows[0]["idestado"].ToString().Trim() == "1")//Si es un codigo válido.
                    {
                        Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());
                        TxtCodigo.InnerText = dtDatosEmp.Rows[0]["codigo_empleado"].ToString();
                        txtFechaing.InnerText = Convert.ToDateTime(dtDatosEmp.Rows[0]["fecha_ingreso"].ToString()).ToShortDateString();
                        txtNombre.InnerText = dtDatosEmp.Rows[0]["nombrecompleto"].ToString();
                        TxtTipoSalario.InnerText = dtDatosEmp.Rows[0]["idTipoSalario"].ToString() == "1" ? "Fijo" : "Variable";
                        CargarGrid();//Obtengo la tabla meses y el detalle de la liquidacion para X persona.                       
                    }
                    else
                    {
                        alertValida.Visible = true;
                        alertValida.InnerText = "No se han encontrado registros";
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
                DataSet ds = Neg_Liquidacion.ObtenerDatosLiquidacion(Convert.ToInt32(txtCodEmp.Text.Trim()),0,0,0,1,0,true);//Obtengo la tabla de meses,y el detalle de liquidacion de X Persona.
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)//Si no tiene los suficientes dias,no se podra calcular el detalle de la liquidacion.X Persona no aplica a liquidacion.
                {
                    GVLiquidacion.DataSource = ds.Tables[0];
                    GVLiquidacion.DataBind();
                    DataTable campos = ds.Tables[1];
                    Session["detallepago"] = ds.Tables[2];
                    AsignarCampos(campos);
                }
                else//No aplica a liquidación.
                {
                    alertValida.Visible = true;
                    alertValida.InnerText = "No hay registros de planillas";
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }        
        private void AsignarCampos(DataTable dt)
        {
            try
            {
                if (dt.Rows.Count != 0)
                {
                    decimal  SalMensual = 0, SalPromedio = 0, SalMayor = 0;                 
                    SalMensual = Convert.ToDecimal(dt.Rows[0]["salMensual"].ToString());
                    SalPromedio = Convert.ToDecimal(dt.Rows[0]["salPromedioDia"].ToString());
                    SalMayor = Convert.ToDecimal(dt.Rows[0]["SalMayorDia"].ToString());                                     

                    txtFechaing.InnerText = dt.Rows[0]["fechaingreso"].ToString();                                                           
                    txtSalMens.InnerText = SalMensual.ToString("n2");                    
                    txtSalProm.InnerText = SalPromedio.ToString("n2");
                    txtSalMayor.InnerText = SalMayor.ToString("n2");                                     
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }               
        protected void GVLiquidacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                int mes = Convert.ToInt32(GVLiquidacion.Rows[GVLiquidacion.SelectedIndex].Cells[1].Text.Trim());
                string nombremes = Server.HtmlDecode(GVLiquidacion.Rows[GVLiquidacion.SelectedIndex].Cells[2].Text.Trim());
                lblmes.InnerText = nombremes;

                DataTable dtt = new DataTable();                
                dtt = ((DataTable) Session["detallepago"]).Select("mes = '"+ mes +"'").CopyToDataTable();

                GVDEtallePago.DataSource = dtt;
                //GVDEtallePago.DataSource = Neg_Liquidacion.ObtenerPlanillasMesAguinaldo(codigo, mes);
                GVDEtallePago.DataBind();
            }
            catch (Exception ex) {
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
            txtSalMayor.InnerText = "0.00";
            txtSalMens.InnerText = "0.00";
            txtSalProm.InnerText = "0.00";
            GVLiquidacion.DataSource = null;
            GVLiquidacion.DataBind();
            GVDEtallePago.DataSource = null;
            GVDEtallePago.DataBind();
            lblmes.InnerText = "";
        }
        double dias = 0, incentivo = 0, salario = 0,beneficio=0;
        protected void GVDEtallePago_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                dias += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Dias"));
                salario += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "SalarioDias"));
                incentivo += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "IncentivoDias"));                
                beneficio += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "BeneficioDias"));

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[6].Text = "Totales";
                e.Row.Cells[7].Text = dias.ToString("n2");
                e.Row.Cells[8].Text = salario.ToString("n2");
                e.Row.Cells[9].Text = incentivo.ToString("n2");
                e.Row.Cells[10].Text = beneficio.ToString("n2");
                e.Row.Font.Bold = true;
            }
        }
    }
}