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
    public partial class Catalogos : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {

                obtenerGrupos(ddlGrupo);
                obtenerGrupos(ddlfiltro);
                CargarGridGrupo(false);
            }
        }

        private void obtenerGrupos(DropDownList dll)
        {
            dll.DataSource = Neg_Catalogos.cargarGrupos();
            dll.DataMember = "grupos";
            dll.DataValueField = "id_grupo";
            dll.DataTextField = "nomb_grupo";
            dll.DataBind();
        }


        private void CargarGridGrupo(bool todos)
        {
            DataSet ds = new DataSet();
            if (todos)
            {
                ds = Neg_Catalogos.SeleccionarGrupos();
            }
            else
            {
                ds = Neg_Catalogos.SeleccionarCatalogosxGrupos(Convert.ToInt32(ddlfiltro.SelectedValue.Trim()));
            }
            Gvgrupos.DataSource = ds;
            Gvgrupos.DataMember = "grupos";
            Gvgrupos.DataBind();
        }


        //protected void Gvgrupos_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.txtDescrp.Text = Gvgrupos.Rows[Gvgrupos.SelectedIndex].Cells[3].Text.Trim();
        //    ddlGrupo.SelectedValue = Gvgrupos.Rows[Gvgrupos.SelectedIndex].Cells[1].Text.Trim();
        //    Session["antDescrp"] = Gvgrupos.Rows[Gvgrupos.SelectedIndex].Cells[3].Text.Trim();
        //    Session["antGrupo"] = Gvgrupos.Rows[Gvgrupos.SelectedIndex].Cells[1].Text.Trim();

        //}

        protected void Gvgrupos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvgrupos.PageIndex = e.NewPageIndex;
            Gvgrupos.DataBind();
            CargarGridGrupo(false);
        }

        //protected void btnEditar_Click(object sender, EventArgs e)
        //{
        //    if(txtDescrp.Text.Trim() != "" && ddlGrupo.SelectedValue != "0")
        //    {
        //        if (Neg_Catalogos.EditarGrupos(Convert.ToInt32(ddlGrupo.SelectedValue), Convert.ToInt32(Session["antGrupo"].ToString()), txtDescrp.Text.Trim(), Session["antDescrp"].ToString()))
        //        {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Actualizacion Satisfactoria";
        //            limpiar();
        //            CargarGridGrupo();
        //        }

        //        else
        //        {
        //            alertSucces.Visible = false;
        //            alertValida.Visible = true;
        //            lblAlert.Visible = true;
        //            lblAlert.Text = "Error en la Actualizacion";
        //        }
        //    }

        //    else
        //    {
        //        alertSucces.Visible = false;
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = "Favor Ingrese o seleccione un valor valido";
        //    }
        //}

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtDescrp.Text.Trim() != "" && ddlGrupo.SelectedValue != "0")
            {
                if (Neg_Catalogos.AgregarAGrupo(Convert.ToInt32(ddlGrupo.SelectedValue), txtDescrp.Text.Trim()))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Satisfactorio";
                    CargarGridGrupo(false);
                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Ingresar Datos";
                }
            }
            else
            {
                alertSucces.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese o seleccione un valor valido";
            }
        }

        private void limpiar()
        {
            this.txtDescrp.Text = "";
            this.ddlGrupo.SelectedValue = "0";
        }

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    if (txtDescrp.Text.Trim() != "" && ddlGrupo.SelectedValue != "0")
        //    {
        //        if (Neg_Catalogos.EliminarGrupos(Convert.ToInt32(Session["antGrupo"].ToString()), Session["antDescrp"].ToString()))
        //        {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Datos Eliminados Satisfactoriamente";
        //            limpiar();
        //            CargarGridGrupo();
        //        }
        //        else
        //        {
        //            alertSucces.Visible = false;
        //            alertValida.Visible = true;
        //            lblAlert.Visible = true;
        //            lblAlert.Text = "Error al Eliminar";
        //        }
        //    }
        //}
        protected void Gvgrupos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.CompareTo("eliminar") == 0)
            {

                GridViewRow selectedRow = Gvgrupos.Rows[index];

                string descant = Gvgrupos.DataKeys[index].Values[1].ToString();
                int idgrupoant = Convert.ToInt32(Gvgrupos.DataKeys[index].Values[0].ToString());

                if (Neg_Catalogos.EliminarGrupos(idgrupoant, descant))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Datos Eliminados Satisfactoriamente";
                    limpiar();
                    CargarGridGrupo(false);

                }
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Eliminar";
                }

            }
            if (e.CommandName.CompareTo("editar") == 0)
            {
                GridViewRow selectedRow = Gvgrupos.Rows[index];

                string descant = Gvgrupos.DataKeys[index].Values[1].ToString();
                int idgrupoant = Convert.ToInt32(Gvgrupos.DataKeys[index].Values[0].ToString());

                string descnew = ((TextBox)selectedRow.FindControl("txtDesc")).Text.Trim();
                string idgruponew = ((DropDownList)selectedRow.FindControl("ddlGrupoGv")).SelectedValue.Trim();

                if (descnew != "" && idgruponew != "0")
                {

                    if (Neg_Catalogos.EditarGrupos(Convert.ToInt32(idgruponew), idgrupoant, descnew, descant))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Actualizacion Satisfactoria";
                        CargarGridGrupo(false);
                        limpiar();

                    }

                    else
                    {
                        alertSucces.Visible = false;
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Error en la Actualizacion";
                    }
                }

                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese o seleccione un valor valido";
                }

            }
        }

        protected void Gvgrupos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddlGrupoGv = (e.Row.FindControl("ddlGrupoGv") as DropDownList);
                obtenerGrupos(ddlGrupoGv);
                //Select the Country of Customer in DropDownList
                string grupo = (e.Row.FindControl("lblgrupo") as Label).Text;
                ddlGrupoGv.Items.FindByValue(grupo).Selected = true;
            }
        }

        protected void ddlfiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGridGrupo(false);
        }
    }
}