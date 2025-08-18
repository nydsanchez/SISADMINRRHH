using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class DevenYDeduc : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerDeducciones();
                obtenerDevengados();
            }
        }

        private void obtenerDevengados()
        {
            GVdevengados.DataSource = Neg_DevYDed.Devengados();
            GVdevengados.DataMember = "devengados";
            GVdevengados.DataBind();
        }

        private void obtenerDeducciones()
        {
            GVDeducciones.DataSource = Neg_DevYDed.Deducciones();
            GVDeducciones.DataMember = "deducciones";
            GVDeducciones.DataBind();
        }

        protected void btnAgregarDed_Click(object sender, EventArgs e)
        {
            if (txtDeduccion.Text.Trim() != "")
            {
                if (Neg_DevYDed.DeduccionesAgregar(txtDeduccion.Text.Trim(), ChkActivo.Checked, ChkAplicaAg.Checked, ChkAplicaVac.Checked,ddlprioridad.SelectedValue.Trim(), ChkMostrarComp.Checked, ChkAplicaDeducIBruto.Checked))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Satisfactorio";
                    this.txtDeduccion.Text = "";
                    obtenerDeducciones();
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error en la insercion";
                }
            }
            else
            {
                alertSucces.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Inserte un valor valido";
                txtDeduccion.Focus();
            }
        }

        protected void btnAgregarDev_Click(object sender, EventArgs e)
        { 
            if(txtDevengado.Text.Trim() != "")
            {
                int idingresoasoc = TxtIngAsociado.Text.Trim() == "" ? 0 : Convert.ToInt32(TxtIngAsociado.Text.Trim());
                int iddeducasoc= TxtDeducAsociada.Text.Trim() == "" ? 0 : Convert.ToInt32(TxtDeducAsociada.Text.Trim());
                if (Neg_DevYDed.DevengadosAgregar(txtDevengado.Text.Trim(), chkInss.Checked, chkIr.Checked, ChKLiq.Checked, ChkAplicaDeduc.Checked, ChkBoleta.Checked, ChkDobleP.Checked, idingresoasoc,iddeducasoc))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Satisfactorio";
                    this.txtDevengado.Text = "";
                    obtenerDevengados();
                }

                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error en la insercion";
                }
            }
            else
            {
                alertSucces.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Inserte un valor valido";
                txtDevengado.Focus();
            }
        }

        protected void GVDeducciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            
            if (e.CommandName.CompareTo("eliminar") == 0)
           {
                GridViewRow selectedRow = GVDeducciones.Rows[index];
                int ID = Convert.ToInt32(((TextBox)selectedRow.FindControl("txtId")).Text.Trim());

                if (Neg_DevYDed.DeduccionesEliminar(ID))
                {
                    obtenerDeducciones();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Deduccion Eliminada Satisfactoriamente";
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error Al Eliminar La Deduccion";
                   
                }
           }
            if(e.CommandName.CompareTo("editar") == 0)
            {
                GridViewRow selectedRow = GVDeducciones.Rows[index];
                int ID = Convert.ToInt32(((TextBox)selectedRow.FindControl("txtId")).Text.Trim());

                string deduc = ((TextBox)selectedRow.FindControl("txtDesc")).Text.Trim();
                    bool estado = ((CheckBox)selectedRow.FindControl("chkAct")).Checked;
                bool chkapag = ((CheckBox)selectedRow.FindControl("chkapag")).Checked;
                bool chkapva = ((CheckBox)selectedRow.FindControl("chkapva")).Checked;
                string ddldeducp = ((DropDownList)selectedRow.FindControl("ddldeducp")).SelectedValue.Trim();
                bool chkmostrar = ((CheckBox)selectedRow.FindControl("chkmostrar")).Checked;
                bool chkdeducib = ((CheckBox)selectedRow.FindControl("chkdeducib")).Checked;
                if (((TextBox)selectedRow.FindControl("txtDesc")).Text.Trim() != "")
                    {
                        if (Neg_DevYDed.DeduccionesEditar(deduc, estado, ID, chkapag,chkapva,ddldeducp,chkmostrar,chkdeducib))
                        {
                            
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Actualizacion Satisfactoria";
                        }
                        else
                        {
                            alertSucces.Visible = false;
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "Error en la actualizacion";
                            return;
                        }
                    }
                    else
                    {
                        alertSucces.Visible = false;
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Inserte un valor valido";
                        return;
                    }
               // }
                obtenerDeducciones();
               
            }
        }

        protected void GVdevengados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            

            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                GridViewRow selectedRow = GVdevengados.Rows[index];
                int ID = Convert.ToInt32(((TextBox)selectedRow.FindControl("txtId")).Text.Trim());

                if (Neg_DevYDed.DevengadosEliminar(ID))
                {
                    obtenerDevengados();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Devengado Eliminada Satisfactoriamente";
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error Al Eliminar Devengado";

                }
            }

            if (e.CommandName.CompareTo("editar") == 0)
            {
                GridViewRow selectedRow = GVdevengados.Rows[index];
                int ID = Convert.ToInt32(((TextBox)selectedRow.FindControl("txtId")).Text.Trim());

                string deveng = ((TextBox)selectedRow.FindControl("txtDesc")).Text.Trim();
                    bool inss = ((CheckBox)selectedRow.FindControl("chkInss")).Checked;
                    bool ir = ((CheckBox)selectedRow.FindControl("chkIR")).Checked;
                    bool Liq = ((CheckBox)selectedRow.FindControl("ChKLiq")).Checked;
                bool aplicadeduc = ((CheckBox)selectedRow.FindControl("chkaplicadeduc")).Checked;
                bool mostrar = ((CheckBox)selectedRow.FindControl("chkmostrar")).Checked;
                bool aplicadeducibruto = ((CheckBox)selectedRow.FindControl("chkdeducib")).Checked;
                int ingasoc = ((TextBox)selectedRow.FindControl("TxtIngAsociado")).Text.Trim() == "" ?  0 : Convert.ToInt32(((TextBox)selectedRow.FindControl("TxtIngAsociado")).Text.Trim());
                int deducasoc = ((TextBox)selectedRow.FindControl("TxtDeducAsociada")).Text.Trim() == "" ? 0 : Convert.ToInt32(((TextBox)selectedRow.FindControl("TxtDeducAsociada")).Text.Trim());

                if (((TextBox)selectedRow.FindControl("txtDesc")).Text.Trim() != "")
                    {
                        if (Neg_DevYDed.devengadosEditar(ID, deveng, inss, ir, Liq, aplicadeduc, mostrar, aplicadeducibruto, ingasoc, deducasoc))
                        {
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Actualizacion Satisfactoria";
                        }
                        else
                        {
                            alertSucces.Visible = false;
                            alertValida.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = "Error en la actualizacion";
                            return;
                        }
                    }
                //}
                obtenerDevengados();
            }
        }

        protected void GVDeducciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDeducciones.PageIndex = e.NewPageIndex;
            GVDeducciones.DataBind();
            obtenerDeducciones();
        }
        protected void GVdevengados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVdevengados.PageIndex = e.NewPageIndex;
            GVdevengados.DataBind();
            obtenerDevengados();
        }

        protected void GVDeducciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddldeducp = (e.Row.FindControl("ddldeducp") as DropDownList);
                
                //Select the Country of Customer in DropDownList
                string prioridad = (e.Row.FindControl("lblprioridad") as Label).Text;
                ddldeducp.Items.FindByValue(prioridad).Selected = true;
            }
        }

        protected void ChkDobleP_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkDobleP.Checked)
            {
                divasociado.Visible = true;
                
            }
            else
            {
                divasociado.Visible = false;
               
            }
            TxtIngAsociado.Text = "0";
            TxtDeducAsociada.Text = "0";
        }
    }
}