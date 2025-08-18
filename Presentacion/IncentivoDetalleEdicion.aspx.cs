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
    public partial class IncentivoDetalleEdicion : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
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
                obtenerPeriodo();
                try
                {
                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(this.txtCodigo.Text.Trim()), 1);

                    if (dtEmp.Rows.Count > 0 && dtEmp.Rows[0]["idestado"].ToString().Trim() != "0")
                    {
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();
                        ObtenerDevengados();
                     
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
           
            DataTable ds;
            ds = Neg_Incentivos.PlnConsultarIncentivoDetallexEmp(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()));
        

            gvIngresosEmp.DataSource = ds;
            gvIngresosEmp.DataBind();

            GVrubroapldeduc.DataSource = null;
            GVrubroapldeduc.DataBind();


        }
        void ObtenerDetalleIngreso(int idtipoing)
        {

            DataTable ds;
            ds = Neg_Incentivos.PlnConsultarSubCategoriaIncxEmp(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()),idtipoing);
      
            GVrubroapldeduc.DataSource = ds;
            GVrubroapldeduc.DataBind();


        }

        public bool validar()
        {
        

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
        decimal totalvat = 0;
        protected void gvIngresosEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            index = gvIngresosEmp.SelectedIndex;
            int idtipoing = int.Parse(((HiddenField)gvIngresosEmp.Rows[index].FindControl("hfidtipoing")).Value);
            ObtenerDetalleIngreso(idtipoing);
        }
        protected void gvIngresosEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (!string.IsNullOrEmpty(((TextBox)e.Row.FindControl("txtTotal")).Text.Trim()))
                {
                    totalvat += Convert.ToDecimal(((TextBox)e.Row.FindControl("txtTotal")).Text.Trim());
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                e.Row.Cells[6].Text = totalvat.ToString("n2");
                e.Row.Font.Bold = true;

            }
        }
        protected void gvIngresosEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                if (dtPeriodo[0].cerrado == 1)
                    throw new Exception("El periodo esta cerrado");
                if (e.CommandName.CompareTo("editar") == 0 || e.CommandName.CompareTo("eliminar") == 0)
                {
                    string user = Convert.ToString(this.Page.Session["usuario"]);

                    int index = Convert.ToInt32(e.CommandArgument.ToString());
                    int idtipoing = int.Parse(((HiddenField)gvIngresosEmp.Rows[index].FindControl("hfidtipoing")).Value);
                    HiddenField hfbonoasistencia = (HiddenField)gvIngresosEmp.Rows[index].FindControl("hfbonoasistencia");
                    TextBox txtBonoA = (TextBox)gvIngresosEmp.Rows[index].FindControl("txtbonoasistencia");

                    HiddenField hfincentivo = (HiddenField)gvIngresosEmp.Rows[index].FindControl("hfincentivo");
                    TextBox txtInc = (TextBox)gvIngresosEmp.Rows[index].FindControl("txtincentivo");

                    TextBox txtOtrosIng = (TextBox)gvIngresosEmp.Rows[index].FindControl("txtotrosIngresos");
                    TextBox txtDeduc = (TextBox)gvIngresosEmp.Rows[index].FindControl("txtdeducciones");
                    TextBox txtTotal = (TextBox)gvIngresosEmp.Rows[index].FindControl("txtTotal");

                    // delete se elimina el rubro y sus sub categorias de todas las tablas
                    if (e.CommandName.CompareTo("eliminar") == 0)
                    {
                        if (Neg_Incentivos.PlnPagoIncentivoxRubroEmpleadoDel(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), Convert.ToInt32(txtCodigo.Text.Trim()), idtipoing))
                        {
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Se realizo el proceso correctamente.";
                        }
                    }


                    //Editar 
                    if (e.CommandName.CompareTo("editar") == 0)
                    {
                        decimal difbonoa, difinc = 0;
                        difbonoa = ((Convert.ToDecimal(txtBonoA.Text) - Convert.ToDecimal(hfbonoasistencia.Value)));
                        difinc = Convert.ToDecimal(txtInc.Text) - Convert.ToDecimal(hfincentivo.Value);
                        decimal total = Convert.ToDecimal(txtTotal.Text) + (difbonoa + difinc);
                        total = total < 0 ? 0 : total;
                        txtTotal.Text = total.ToString();

                        if (Neg_Incentivos.PlnIncentivosxEmpleadoIns(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), "0", Convert.ToInt32(txtCodigo.Text.Trim()), "", "NA", 0,
                            Convert.ToDecimal(txtBonoA.Text), 0, Convert.ToDecimal(txtInc.Text), Convert.ToDecimal(txtOtrosIng.Text), Convert.ToDecimal(txtDeduc.Text), total,
                            user, idtipoing, "Modificacion desde pantalla", false))
                        {
                            DataTable dt = new DataTable();

                            dt.Columns.Add("codigo_empleado");
                            dt.Columns.Add("idtipoing");
                            dt.Columns.Add("incentivo");
                            dt.Columns.Add("bonoasistencia");
                            dt.Columns.Add("otrosingresos");
                            dt.Columns.Add("total");

                            dt.Rows.Add(Convert.ToInt32(txtCodigo.Text.Trim()), idtipoing, Convert.ToDecimal(txtInc.Text), Convert.ToDecimal(txtBonoA.Text), Convert.ToDecimal(txtOtrosIng.Text), total);
                            if (!Neg_Incentivos.PlnIngresoPlanillaIns(dt, Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue),true))
                            {
                                throw new Exception("Error al ingresar rubro incentivo");
                            }

                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Se realizo el proceso correctamente.";
                        }
                    }
                    ObtenerDevengados();
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
          
        }
        protected void GVrubroapldeduc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text.Trim()));
                if (dtPeriodo[0].cerrado == 1)
                    throw new Exception("El periodo esta cerrado");
                if (e.CommandName.CompareTo("editar") == 0 || e.CommandName.CompareTo("eliminar") == 0)
                {
                    int index = Convert.ToInt32(e.CommandArgument.ToString());

                    GridViewRow gr = GVrubroapldeduc.Rows[index];
                    string user = Convert.ToString(this.Page.Session["usuario"]);
                    int indexp = gvIngresosEmp.SelectedIndex;

                    bool hfGeneradoSistema = bool.Parse(((HiddenField)gr.FindControl("hfGeneradoSistema")).Value);
                    int hfIdTipoIng = int.Parse(((HiddenField)gr.FindControl("hfIdTipoIng")).Value);//4,14/29,30

                  
                    // Eliminar
                    if (!hfGeneradoSistema)
                    {
                        string txtRubro = ((TextBox)gr.FindControl("txtRubro")).Text;//detalle
                        decimal txtValor = Convert.ToDecimal(((TextBox)gr.FindControl("txtValor")).Text);//valor actual
                        decimal hfvalor = decimal.Parse(((HiddenField)gr.FindControl("hfvalor")).Value);//valor anterior

                        int hfditipo = int.Parse(((HiddenField)gr.FindControl("hfditipo")).Value);//1 ing 2 egr
                        

                        string hfcampoafecta = ((HiddenField)gr.FindControl("hfcampoafecta")).Value;
                        string hfoperacion = ((HiddenField)gr.FindControl("hfoperacion")).Value;

                        if (e.CommandName.CompareTo("eliminar") == 0)
                        {
                            txtValor = 0;
                        }
                        decimal campoAfecta = decimal.Parse(((TextBox)gvIngresosEmp.Rows[indexp].FindControl("txt" + hfcampoafecta.Trim())).Text);
                        decimal campoTotal = decimal.Parse(((TextBox)gvIngresosEmp.Rows[indexp].FindControl("txtTotal")).Text);
                        decimal diferencia = 0;
                        diferencia = txtValor - hfvalor;

                        ((TextBox)gvIngresosEmp.Rows[indexp].FindControl("txt" + hfcampoafecta.Trim())).Text = (campoAfecta + diferencia) < 0 ? "0" : (campoAfecta + diferencia).ToString();
                        ((TextBox)gvIngresosEmp.Rows[indexp].FindControl("txtTotal")).Text = (campoAfecta + diferencia) < 0 ? campoTotal.ToString() : (campoTotal + diferencia).ToString();

                        if (Neg_Incentivos.IncentivoIngDedccLOGInsert(Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue),
                                hfditipo, txtRubro, 1, txtValor, txtValor, "", hfIdTipoIng, hfGeneradoSistema))
                        {
                            DataTable dt = new DataTable();

                            dt.Columns.Add("codigo_empleado");
                            dt.Columns.Add("idtipoing");
                            dt.Columns.Add("incentivo");
                            dt.Columns.Add("bonoasistencia");
                            dt.Columns.Add("otrosingresos");
                            dt.Columns.Add("total");

                            TextBox txtBonoA = (TextBox)gvIngresosEmp.Rows[indexp].FindControl("txtbonoasistencia");
                            TextBox txtInc = (TextBox)gvIngresosEmp.Rows[indexp].FindControl("txtincentivo");
                            TextBox txtOtrosIng = (TextBox)gvIngresosEmp.Rows[indexp].FindControl("txtotrosIngresos");
                            TextBox txtDeduc = (TextBox)gvIngresosEmp.Rows[indexp].FindControl("txtdeducciones");
                            TextBox txtTotal = (TextBox)gvIngresosEmp.Rows[indexp].FindControl("txtTotal");

                            dt.Rows.Add(Convert.ToInt32(txtCodigo.Text.Trim()), hfIdTipoIng, Convert.ToDecimal(txtInc.Text), Convert.ToDecimal(txtBonoA.Text), Convert.ToDecimal(txtOtrosIng.Text), Convert.ToDecimal(txtTotal.Text));

                            if (!Neg_Incentivos.PlnIncentivosxEmpleadoIns(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue), "0", Convert.ToInt32(txtCodigo.Text.Trim()), "", "NA", 0,
                              Convert.ToDecimal(txtBonoA.Text), 0, Convert.ToDecimal(txtInc.Text), Convert.ToDecimal(txtOtrosIng.Text), Convert.ToDecimal(txtDeduc.Text), Convert.ToDecimal(txtTotal.Text),
                              user, hfIdTipoIng, "Modificacion desde pantalla", false))
                            {
                                throw new Exception("Error al ingresar incentivo");
                            }
                            if (!Neg_Incentivos.PlnIngresoPlanillaIns(dt, int.Parse(txtPeriodo.Text), Convert.ToInt32(ddlSemana.SelectedValue),true))
                            {
                                throw new Exception("Error al ingresar rubro incentivo");
                            }
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Se realizo el proceso correctamente.";
                        }
                    }
                   
                    ObtenerDevengados();
                    ObtenerDetalleIngreso(hfIdTipoIng);
                    

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
}