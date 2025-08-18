using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Negocios;
using System;

namespace NominaRRHH
{
    public partial class VLiquidacionesEmp : System.Web.UI.Page
    {
        #region Referencias
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtcodigo.Text = "0";
                txtFechaIni.Text = DateTime.Now.ToShortDateString();
                txtFechaFin.Text = DateTime.Now.ToShortDateString();
            }
            
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                cargarGrid();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        
          
        }
        void cargarGrid()
        {
            if (rbl.SelectedValue == "1" && txtFechaIni.Text == "" && txtFechaFin.Text == "")
            {
                throw new Exception("Debe agregar fechas validas");
            }
            if (rbl.SelectedValue == "2" && txtcodigo.Text == "")
            {
                throw new Exception("Debe agregar codigo valido");
            }

            gvIngresosEmp.DataSource = Neg_Liquidacion.plnObtenerLiquidacionesxfiltro(Convert.ToInt32(rbl.SelectedValue), Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text), Convert.ToInt32(txtcodigo.Text));
            gvIngresosEmp.DataBind();

        }
        protected void gvIngresosEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                int index = Convert.ToInt32(e.CommandArgument.ToString());

                int ID = (int)(gvIngresosEmp.DataKeys[index][0]);
                int codigo = (int)(gvIngresosEmp.DataKeys[index][1]);
                DateTime fechaliq = Convert.ToDateTime(gvIngresosEmp.Rows[index].Cells[3].Text);
              
                // Eliminar Egresos catorcenales
                if (e.CommandName.CompareTo("cerrar") == 0)
                {
                    if (!Neg_Liquidacion.plnLiquidacionesCerrar(ID,codigo))
                    {
                        throw new Exception("Error al cerrar liquidacion");
                    }
                    cargarGrid();
                }

                //Editar Egresos catorcenales
                if (e.CommandName.CompareTo("imprimir") == 0)
                {
                    Session["dtParametrosLiq"] = codigo;
                    Session["dtfechaliq"] = fechaliq;
                    Response.Redirect("VLiquidacion.aspx");
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
         
        }
        
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbl.SelectedValue.ToString() == "2")//codiggo
            {
                divcodigo.Visible = true;
                divfec.Visible = false;              
            }            
            else
            {
                divcodigo.Visible = false;               
                divfec.Visible = true;
            }
        }

        protected void gvIngresosEmp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Button btncerrar = ((Button)e.Row.FindControl("btncerrar"));
                bool cerrada = Convert.ToBoolean(gvIngresosEmp.DataKeys[e.Row.RowIndex].Values[2]);
                if (cerrada)
                {
                    btncerrar.Visible = false;
                }
                else
                {
                    btncerrar.Visible = true;
                }
            }
        }
    }
}