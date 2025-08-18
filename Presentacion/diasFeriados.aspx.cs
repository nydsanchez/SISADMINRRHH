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
    public partial class diasFeriados : System.Web.UI.Page
    {
         #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Feriados Neg_Feriados = new Neg_Feriados();
#endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                txtFechaIni.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                txtFechaFin.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToShortDateString();
                obtenerFeriados();
            }

        }

        private void obtenerFeriados()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (string.IsNullOrEmpty( txtFechaIni.Text))
            {
                return;
            }
            if (string.IsNullOrEmpty(txtFechaFin.Text))
            {
                return;
            }
            GVdiasFeriados.DataSource = Neg_Feriados.diasFeriados(Convert.ToDateTime( txtFechaIni.Text.Trim()),Convert.ToDateTime( txtFechaFin.Text.Trim()),userDetail.getIDEmpresa());
            //GVdiasFeriados.DataMember = "Feriados";
            GVdiasFeriados.DataBind();
        }

        protected void GVdiasFeriados_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtFecha.Text = GVdiasFeriados.Rows[GVdiasFeriados.SelectedIndex].Cells[1].Text.Trim();
            this.txtDescripcion.Text = GVdiasFeriados.Rows[GVdiasFeriados.SelectedIndex].Cells[2].Text.Trim();
            this.txtCantidadDias.Text = GVdiasFeriados.Rows[GVdiasFeriados.SelectedIndex].Cells[3].Text.Trim();
            BtnAgregar.Visible = false;
            BtnEditar.Visible = true;
            BtnEliminar.Visible = true;
            Session["tempFecha"] = txtFecha.Text;
            Session["tempDesc"] = txtDescripcion.Text;
            Session["tempCantD"] = txtCantidadDias.Text;
        }

        protected void GVdiasFeriados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVdiasFeriados.PageIndex = e.NewPageIndex;
            GVdiasFeriados.DataBind();
            obtenerFeriados();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            if (validar())
            {
                if (Neg_Feriados.AgregardiasFeriados(Convert.ToDateTime(txtFecha.Text.Trim()), txtDescripcion.Text.Trim(), Convert.ToDecimal(txtCantidadDias.Text.Trim())))
                {
                    limpiar();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    Label1.Visible = true;
                    Label1.Text = "Ingreso Satisfactorio";
                }
            }
        }

        private void limpiar()
        {
            this.txtFecha.Text = "";
            this.txtCantidadDias.Text = "";
            txtDescripcion.Text = "";
        }

        public bool validar()
        {
            if (txtFecha.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese una Fecha Valida";
                return false;
            }

            if (txtCantidadDias.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese una cantidad de dias";
                return false;
            }

            if (txtDescripcion.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese una descripcion";
                return false;
            }

            return true;
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
             string user = Convert.ToString(this.Page.Session["usuario"]);
             if (validar())
             {
                 if (Neg_Feriados.EditarFeriados(Convert.ToDateTime(txtFecha.Text.Trim()), txtDescripcion.Text.Trim(), Convert.ToDecimal(txtCantidadDias.Text.Trim()), Convert.ToDateTime(Session["tempFecha"].ToString()), Session["tempDesc"].ToString(), Convert.ToDecimal(Session["tempCantD"].ToString())))
                 {
                     limpiar();
                     alertValida.Visible = false;
                     alertSucces.Visible = true;
                     Label1.Visible = true;
                     Label1.Text = "Actualizacion Satisfactoria";
                     obtenerFeriados();
                     BtnEditar.Visible = false;
                     BtnAgregar.Visible = true;
                 }
             
             }
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            if (validar())
            {
                if (Neg_Feriados.EliminarFeriados(Convert.ToDateTime(Session["tempFecha"].ToString()), Session["tempDesc"].ToString(), Convert.ToDecimal(Session["tempCantD"].ToString())))
                {
                    limpiar();
                    obtenerFeriados();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    Label1.Visible = true;
                    Label1.Text = "Dia Feriado Eliminado Satisfactoriamente";
                }
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            obtenerFeriados();
        }
    }
}