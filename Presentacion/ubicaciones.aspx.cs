using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class ubicaciones : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Empresas Neg_Empresas = new Neg_Empresas();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerEmpresas();
                obtenerTipoNomina();
                selecionarUbicaciones();
            }
        }
        private void obtenerTipoNomina()
        {
            this.ddlTipNomina.DataSource = Neg_Empresas.CargarTipoNomina();
            this.ddlTipNomina.DataMember = "nomina";
            this.ddlTipNomina.DataValueField = "idnomina";
            ddlTipNomina.DataTextField = "descripcion";
            this.ddlTipNomina.DataBind();
        }
        private void obtenerEmpresas()
        {
            this.ddlEmpresa.DataSource = Neg_Catalogos.CargarEmpresas();
            this.ddlEmpresa.DataMember = "empresas";
            this.ddlEmpresa.DataValueField = "id_empresa";
            this.ddlEmpresa.DataTextField = "descripcion";
            this.ddlEmpresa.DataBind();
        }

        private void selecionarUbicaciones()
        {
            //DataTable dt = Neg_Catalogos.selecionarUbicaciones();

            //DataTable ubic = new DataTable();
            //ubic.Columns.Add("codigo_ubicacion");
            //ubic.Columns.Add("nombre_ubicacion");
            //ubic.Columns.Add("tplanilla");
            //ubic.Columns.Add("Telefono");
            //ubic.Columns.Add("Direccion");
            //ubic.Columns.Add("Npatronal");
            //ubic.Columns.Add("Nruc");
            //ubic.Columns.Add("departamento");
            //ubic.Columns.Add("municipio");

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    DataRow dr = ubic.NewRow();
            //    dr[0]= ubic.Rows[i]["codigo_ubicacion"];
            //    dr[1] = ubic.Rows[i]["nombre_ubicacion"];
            //    dr[2] = ubic.Rows[i]["tplanilla"];
            //    dr[3] = ubic.Rows[i]["Telefono"];
            //    dr[4] = ubic.Rows[i]["Direccion"];
            //    dr[5] = ubic.Rows[i]["Npatronal"];
            //    dr[6] = ubic.Rows[i]["Nruc"];
            //    dr[7] = ubic.Rows[i]["departamento"];
            //    dr[8] = ubic.Rows[i]["municipio"];

            //    ubic.Rows.Add(dr);
            //}
            gvUbicaciones.DataSource = Neg_Catalogos.selecionarUbicaciones();
            gvUbicaciones.DataMember = "ubicaciones";
            gvUbicaciones.DataBind();




        }

        protected void gvUbicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex];
            Session["idUbicacion"] = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[1].Text.Trim();
            this.txtUbicacion.Text = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[2].Text.Trim();
            ddlTipNomina.SelectedValue= ((DropDownList)selectedRow.FindControl("ddltplanilla")).SelectedValue.Trim();
            this.txtTelefono.Text = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[4].Text.Trim();
            this.txtDireccion.Text = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[5].Text.Trim();
            this.txtNpatronal.Text = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[6].Text.Trim();
            this.txtNruc.Text = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[7].Text.Trim();
            this.txtDepartamento.Text = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[8].Text.Trim();
            this.txtMunicipio.Text = gvUbicaciones.Rows[gvUbicaciones.SelectedIndex].Cells[9].Text.Trim();
            btnEditar.Visible = true;
            BtnAgregar.Visible = false;
            
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                if(Neg_Catalogos.agregarUbicaciones(Convert.ToInt32(ddlEmpresa.SelectedValue), txtUbicacion.Text.Trim(),
                    txtTelefono.Text.Trim(), txtNpatronal.Text.Trim(), txtNruc.Text.Trim(), txtDepartamento.Text.Trim(),
                    txtMunicipio.Text.Trim(), txtDireccion.Text.Trim(),Convert.ToInt32(ddlTipNomina.SelectedValue)))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ubicacion Agregada Satisfactoriamente";
                    selecionarUbicaciones();
                }
            }
        }

        private bool validar()
        {
            if(txtUbicacion.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un nombre de ubicacion";
                return false;
            }
            if (txtTelefono.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un numero de telefono";
                return false;
            }
            if (txtDireccion.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese una direccion";
                return false;
            }
            if (txtNpatronal.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un numero patronal";
                return false;
            }
            if (txtNruc.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un numero Ruc";
                return false;
            }
            if (txtDepartamento.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un departamento";
                return false;
            }
            if (txtMunicipio.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un municipio";
                return false;
            }
            return true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if(validar())
            {
                if (Neg_Catalogos.editarUbicaciones(Convert.ToInt32(ddlEmpresa.SelectedValue), txtUbicacion.Text.Trim(),
                   txtTelefono.Text.Trim(), txtNpatronal.Text.Trim(), txtNruc.Text.Trim(), txtDepartamento.Text.Trim(),
                   txtMunicipio.Text.Trim(), txtDireccion.Text.Trim(), Convert.ToInt32(Session["idUbicacion"].ToString()),Convert.ToInt32(ddlTipNomina.SelectedValue)))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ubicacion Editada Satisfactoriamente";
                    selecionarUbicaciones();
                }
            }
        }

        protected void gvUbicaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                DropDownList ddltplanilla = (e.Row.FindControl("ddltplanilla") as DropDownList);
                ddltplanilla.DataSource = Neg_Empresas.CargarTipoNomina();
                ddltplanilla.DataMember = "nomina";
                ddltplanilla.DataValueField = "idnomina";
                ddltplanilla.DataTextField = "descripcion";
                ddltplanilla.DataBind();

                //Select the Country of Customer in DropDownList
                string tplanilla = (e.Row.FindControl("lbltplanilla") as Label).Text;
                ddltplanilla.Items.FindByValue(tplanilla).Selected = true;
            }
        }
    }
}