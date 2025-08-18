using System;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class IncTablaIncentivos : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                alertSucces.Visible = false;
                alertValida.Visible = false;
                CargarConstrucciones();
                CargarRangoOql();
                ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());                             
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
       
        public void ObtenerTablaIncentivos(DataTable dtd)
        {
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
            Mensaje("", 3, false);

            gvTabla.PageIndex = e.NewPageIndex;
            gvTabla.DataBind();
            ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!Validadar())
                return;

            int idarea = int.Parse(txtIdArea.Text);
            int construccion = int.Parse(ddlConstruccion.SelectedValue);//int.Parse(txtConstruccion.Text);
            decimal bonoasistencia = decimal.Parse(txtBonoasistencia.Text);
            decimal meta5dias = decimal.Parse(txtMeta5dias.Text);
            int rangooql = int.Parse(ddlRangooql.SelectedValue);
            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);
            decimal bonocalidad = decimal.Parse(txtBonocalidad.Text);
            decimal incentivo = decimal.Parse(txtIncsemanal.Text);
            new Neg_TablaIncentivos().Insert(idarea, construccion, bonoasistencia, meta5dias, rangooql, incentivo, eficienciadesde, eficienciahasta, bonocalidad);
            ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());
            Mensaje("OK", 1, true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validadar())
                return;

            int id = int.Parse(gvTabla.SelectedDataKey["ID"].ToString());
            int idarea = int.Parse(txtIdArea.Text);
            int construccion = int.Parse(ddlConstruccion.SelectedValue);
            decimal bonoasistencia = decimal.Parse(txtBonoasistencia.Text);
            decimal meta5dias = decimal.Parse(txtMeta5dias.Text);
            int rangooql = int.Parse(ddlRangooql.SelectedValue);
            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);
            decimal bonocalidad = decimal.Parse(txtBonocalidad.Text);
            decimal incentivo = decimal.Parse(txtIncsemanal.Text);

            new Neg_TablaIncentivos().Update(id, idarea, construccion, bonoasistencia, meta5dias, rangooql, incentivo, eficienciadesde, eficienciahasta, bonocalidad);
            ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());
            Mensaje("OK", 1, true);

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id;
            if (int.TryParse(gvTabla.SelectedDataKey["ID"].ToString(),out id))
            {
                if(new Neg_TablaIncentivos().Delete(id)) 
                {
                    ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());
                    Mensaje("OK", 1, true);

                }
            }
        }

        protected void gvTabla_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mensaje("", 3, false);

            GridViewRow selectedRow = gvTabla.Rows[gvTabla.SelectedIndex];
            txtIdArea.Text = selectedRow.Cells[2].Text.Trim();
            ddlConstruccion.SelectedValue = gvTabla.SelectedDataKey["IdConstruccion"].ToString();
            txtBonoasistencia.Text = selectedRow.Cells[4].Text.Trim();

            ddlRangooql.SelectedValue = selectedRow.Cells[7].Text.Trim();
            txtEficdesde.Text = selectedRow.Cells[9].Text.Trim();
            txtEfichasta.Text = selectedRow.Cells[10].Text.Trim();
            txtBonocalidad.Text = selectedRow.Cells[13].Text.Trim();
            txtMeta5dias.Text = selectedRow.Cells[12].Text.Trim();
            txtIncsemanal.Text = selectedRow.Cells[14].Text.Trim();
        }

        bool Validadar()
        {
            string v = "OK";

            if (txtIdArea.Text == "") v = "Campo ID Area es obligatorio";
            if (txtBonoasistencia.Text == "") v = "Bono asistencia es un campo obligatorio";
            if (txtMeta5dias.Text == "") v = "Campo Dz Desde es obligatorio";
            if (txtEficdesde.Text == "") v = "Campo Eficiencia Desde es obligatorio";
            if (txtEfichasta.Text == "") v = "Campo Eficiencia Hasta es obligatorio";
            if (txtIncsemanal.Text == "") v = "Campo incentivo semanal es obligatorio";
            if (txtBonocalidad.Text == "") v = "Campo incentivo semanal es obligatorio";

            if (v != "OK")
            {
                Mensaje(v, 2, true);
                return false;
            }
            else
            {
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

        protected void btnGuardarEficiencia_Click(object sender, EventArgs e)
        {
            string v = "OK";

            if (txtBonoasistencia.Text == "") v = "Bono asistencia es un campo obligatorio";
            if (txtEficdesde.Text == "") v = "Campo Eficiencia Desde es obligatorio";
            if (txtEfichasta.Text == "") v = "Campo Eficiencia Hasta es obligatorio";
            if (txtIncsemanal.Text == "") v = "Campo incentivo semanal es obligatorio";
            if (txtBonocalidad.Text == "") v = "Campo incentivo semanal es obligatorio";

            if (v != "OK")
            {
                Mensaje(v, 2, true);
            }            

            decimal bonoasistencia = decimal.Parse(txtBonoasistencia.Text);
            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);
            decimal bonocalidad = decimal.Parse(txtBonocalidad.Text);
            decimal incentivo = decimal.Parse(txtIncsemanal.Text);

            new Neg_TablaIncentivos().UpdateByEffic(bonoasistencia, incentivo, eficienciadesde, eficienciahasta, bonocalidad);
            ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());
            Mensaje("OK", 1, true);

        }

        protected void btnAgregarEfic_Click(object sender, EventArgs e)
        {
            int idarea = int.Parse(txtIdArea.Text);
            decimal bonoasistencia = decimal.Parse(txtBonoasistencia.Text);
            decimal meta5dias = decimal.Parse(txtMeta5dias.Text);
            int rangooql = int.Parse(ddlRangooql.SelectedValue);
            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);
            decimal bonocalidad = decimal.Parse(txtBonocalidad.Text);
            decimal incentivo = decimal.Parse(txtIncsemanal.Text);

            new Neg_TablaIncentivos().InsertByEffic(idarea, bonoasistencia, meta5dias, rangooql, incentivo, eficienciadesde, eficienciahasta, bonocalidad);
            ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());
            Mensaje("OK", 1, true);

        }

        protected void btnEliminarEfic_Click(object sender, EventArgs e)
        {
            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);

            if (new Neg_TablaIncentivos().DeleteByEffic(eficienciadesde,eficienciahasta))
            {
                ObtenerTablaIncentivos(new Neg_TablaIncentivos().SelectAll());
                Mensaje("OK", 1, true);
            }
        }

        protected void btnBuscarEficiencia_Click(object sender, EventArgs e)
        {
            Mensaje("",3,false);

            decimal eficienciadesde = decimal.Parse(txtEficdesde.Text);
            decimal eficienciahasta = decimal.Parse(txtEfichasta.Text);

            DataTable dt = new Neg_TablaIncentivos().SelectByEffic(eficienciadesde, eficienciahasta);
            ObtenerTablaIncentivos(dt);
        }

        protected void btnBuscarConstruccion_Click(object sender, EventArgs e)
        {
            Mensaje("", 3, false);

            int idconstruccion = int.Parse(ddlConstruccion.SelectedValue);
            DataTable dt = new Neg_TablaIncentivos().SelectByConstruccion(idconstruccion);
            ObtenerTablaIncentivos(dt);
        }
    }
}