using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class rptMarcas : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerPeriodo();

            }
        }

        private void obtenerPeriodo()
        {
            dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, 1);
            if (dtPeriodo.Rows.Count > 0)
            {
                txtPeriodo.Text = dtPeriodo.Rows[0]["nperiodo"].ToString();
            }
            else
            {
                txtPeriodo.Text = "0";
            }
        }

        protected void btnProcesarMarcas_Click(object sender, EventArgs e)
        {
            Neg_Informes.CargarMarcasHorasT(Convert.ToInt32(txtPeriodo.Text.Trim()),Convert.ToInt32(ddlSemana.SelectedValue));
            obtenerMarcas(Convert.ToInt32(txtPeriodo.Text.Trim()),Convert.ToInt32(ddlSemana.SelectedValue));
        }

        private void obtenerMarcas(int periodo, int semana)
        {
            DataTable ds = new DataTable();
            ds = Neg_Informes.obtenerMarcas(periodo, semana);
            GvMarcasMenoresTurno.DataSource = ds;
            GvMarcasMenoresTurno.DataMember = "marcas";
            GvMarcasMenoresTurno.DataBind();
        }

        protected void GvMarcasMenoresTurno_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvMarcasMenoresTurno.PageIndex = e.NewPageIndex;
            GvMarcasMenoresTurno.DataBind();
            obtenerMarcas(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(ddlSemana.SelectedValue));

        }  
    }
}