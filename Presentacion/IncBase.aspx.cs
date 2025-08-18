using System;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class IncBase : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                alertSucces.Visible = false;
                alertValida.Visible = false;
                CargarConstrucciones();
                CargarRangoOql();
                obtenerIncentivos();                             
            }
        }
        #region METODOS
        public DataTable TablaEnBlanco()
        {
            DataTable dt = new DataTable();//se crea el dt con los campos semejantes a los del gv
            dt.Columns.Add("ID");
            dt.Columns.Add("idArea");
            dt.Columns.Add("Construccion");
            dt.Columns.Add("BonoAsist");
            dt.Columns.Add("DzDesde");
            dt.Columns.Add("DzHasta");
            dt.Columns.Add("idRangoOql");
            dt.Columns.Add("CostoDz");
            dt.Columns.Add("EficienciaDesde");
            dt.Columns.Add("EficienciaHasta");
            dt.Columns.Add("Meta5dias");
            dt.Columns.Add("Meta4dias");
            dt.Columns.Add("BonoCalidad");
            dt.Columns.Add("CostoSem");

            return dt;
        }
       
        public void obtenerIncentivos()
        {
            DataTable dtd = new Neg_TablaIncentivos().SelectAll();
            if (dtd != null && dtd.Rows.Count > 0)
            {
                gvTabla.DataSource = dtd;
                gvTabla.DataBind();
            }
            else
            {
                gvTabla.DataSource = TablaEnBlanco();
                gvTabla.DataBind();
            }

        }

        public void LimpiarCampos()
        {
            //txtNombTurno.Text = "";
            //txtMinComodin.Text = "";
            //txtTotalHoras.Text = "";
            //TxtHrsTurno.Text = "";
            //ChkConsolidar.Checked = false;
        }

        public void MostrarDatosTurno(int index)
        {
            LimpiarCampos();
            //txtNombTurno.Text = gvTabla.Rows[index].Cells[0].Text;
            //txtMinComodin.Text = gvTabla.Rows[index].Cells[1].Text;
            //txtTotalHoras.Text = gvTabla.Rows[index].Cells[3].Text;
            //TxtHrsTurno.Text = gvTabla.Rows[index].Cells[4].Text;

            GridViewRow selectedRow = gvTabla.Rows[index];
        }

        #endregion

        public void Mensaje(string mensaje, int tipo, bool mostrar)
        {
            LblSuccess.Text = "";
            lblAlert.Text = "";
            alertSucces.Visible = false;
            alertValida.Visible = false;

            if (tipo == 1)
            {
                if (mostrar)
                {
                    alertSucces.Visible = true;
                    LblSuccess.Text = mensaje;
                }
            }
            else
            {
                if (mostrar)
                {
                    alertValida.Visible = true;
                    lblAlert.Text = mensaje;
                }
            }
        }
       
        protected void gvTabla_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTabla.PageIndex = e.NewPageIndex;
            gvTabla.DataBind();
            obtenerIncentivos();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!Validadar())
                return;

            GridViewRow selectedRow = gvTabla.Rows[gvTabla.SelectedIndex];
            int idarea = int.Parse(txtIdArea.Text);
            int construccion = int.Parse(ddlConstruccion.SelectedValue);//int.Parse(txtConstruccion.Text);
            decimal bonoasistencia = decimal.Parse(txtBonoasistencia.Text);
            decimal dzdesde = decimal.Parse(txtDzDesde.Text);
            decimal dzhasta = decimal.Parse(txtDzHasta.Text);
            int rangooql = int.Parse(ddlRangooql.SelectedValue);
            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);
            decimal bonocalidad = decimal.Parse(txtBonocalidad.Text);
            decimal incentivo = decimal.Parse(txtIncsemanal.Text);
            //new Neg_TablaIncentivos().Insert(idarea, construccion, bonoasistencia, dzdesde, dzhasta, rangooql, incentivo, eficienciadesde, eficienciahasta, bonocalidad);
            obtenerIncentivos();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validadar())
                return;

            int id = int.Parse(gvTabla.SelectedDataKey["ID"].ToString());
            GridViewRow selectedRow = gvTabla.Rows[gvTabla.SelectedIndex];
            int idarea = int.Parse(txtIdArea.Text);
            int construccion = int.Parse(ddlConstruccion.SelectedValue);
            decimal bonoasistencia = decimal.Parse(txtBonoasistencia.Text);
            decimal dzdesde = decimal.Parse(txtDzDesde.Text);
            decimal dzhasta = decimal.Parse(txtDzHasta.Text);
            int rangooql = int.Parse(ddlRangooql.SelectedValue);
            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);
            decimal bonocalidad = decimal.Parse(txtBonocalidad.Text);
            decimal incentivo = decimal.Parse(txtIncsemanal.Text);

            //new Neg_TablaIncentivos().Update(id, idarea, construccion, bonoasistencia, dzdesde, dzhasta, rangooql, incentivo, eficienciadesde, eficienciahasta, bonocalidad);

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(gvTabla.SelectedDataKey["ID"].ToString(),out id))
            {
                new Neg_TablaIncentivos().Delete(id);
            }
        }

        protected void gvTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = gvTabla.Rows[gvTabla.SelectedIndex];
            txtIdArea.Text = selectedRow.Cells[2].Text.Trim();
            ddlConstruccion.SelectedValue = gvTabla.SelectedDataKey["IdConstruccion"].ToString();
            txtBonoasistencia.Text = selectedRow.Cells[4].Text.Trim();
            txtDzDesde.Text = selectedRow.Cells[5].Text.Trim();
            txtDzHasta.Text = selectedRow.Cells[6].Text.Trim();

            ddlRangooql.SelectedValue = selectedRow.Cells[7].Text.Trim();
            txtEficdesde.Text = selectedRow.Cells[9].Text.Trim();
            txtEfichasta.Text = selectedRow.Cells[10].Text.Trim();
            txtBonocalidad.Text = selectedRow.Cells[13].Text.Trim();
            txtIncsemanal.Text = selectedRow.Cells[14].Text.Trim();
        }

        bool Validadar()
        {
            string v = "OK";

            if (txtIdArea.Text == "") v = "Campo ID Area es obligatorio";
            if (txtBonoasistencia.Text == "") v = "Bono asistencia es un campo obligatorio";
            if (txtDzDesde.Text == "") v = "Campo Dz Desde es obligatorio";
            if (txtDzHasta.Text == "") v = "Campo Dz Hasta es obligatorio";
            if (txtEficdesde.Text == "") v = "Campo Eficiencia Desde es obligatorio";
            if (txtEfichasta.Text == "") v = "Campo Eficiencia Hasta es obligatorio";
            if (txtIncsemanal.Text == "") v = "Campo incentivo semanal es obligatorio";
            
            if (v != "OK")
            {
                lblAlert.Visible = true;
                LblSuccess.Visible = false;
                lblAlert.Text = v;
                return false;
            }
            else
            {
                lblAlert.Visible = false;   
                return true;
            }

        }

        private void CargarConstrucciones()
        {
            ddlConstruccion.DataSource = new Dato_PlnConstruccionIncentivo().SelectAll();
            ddlConstruccion.DataValueField = "id";
            ddlConstruccion.DataTextField = "construccionincentivo";
            ddlConstruccion.DataBind();
        }

        private void CargarRangoOql()
        {
            ddlRangooql.DataSource = new Dato_PlnConstruccionIncentivo().SelectAll();
            ddlRangooql.DataValueField = "id";
            ddlRangooql.DataTextField = "id";
            ddlRangooql.DataBind();
        }
    }
}