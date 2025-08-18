using System;
using System.Drawing;
using Negocios;
using System.Data;

namespace NominaRRHH.Presentacion
{
    public partial class IncProtegerFechas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.Page.IsPostBack)
            {
                DataTable dt = new Neg_Periodo().GetPeriodoActual();

                DateTime Lunes = Convert.ToDateTime(dt.Rows[0]["fechaini"]);
                Lunes = Lunes.AddDays(-7);
                DateTime Viernes = Lunes.AddDays(4);

                ddlAnoI.SelectedValue = Lunes.Year.ToString();
                ddlMesI.SelectedValue = Lunes.Month.ToString();
                ActualizardllDiasIni();
                ddlDiaI.SelectedValue = Lunes.Day.ToString();

                ddlAnoF.SelectedValue = Viernes.Year.ToString();
                ddlMesF.SelectedValue = Viernes.Month.ToString();
                ActualizardllDiasFin();
                ddlDiaF.SelectedValue = Viernes.Day.ToString();

                Buscar(Lunes, Viernes, 1);

            }
        }

        private void Mensaje(string msg, bool error)
        {
            LblMsg.Text = msg;
            if (error)
            {
                LblMsg.ForeColor = Color.Red;
            }
            else
            {
                LblMsg.ForeColor = Color.BlueViolet;
            }
        }
                   
        protected void ddlMesI_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizardllDiasIni();
        }

        private void ActualizardllDiasIni()
        {
            int ano = int.Parse(ddlAnoI.SelectedValue);
            int mes = int.Parse(ddlMesI.SelectedValue);
            int dias = DateTime.DaysInMonth(ano, mes);

            ddlDiaI.Items.Clear();
            for (int i = 1; i <= dias; i++)
            {
                ddlDiaI.Items.Add(i.ToString());
            }
        }
        private void ActualizardllDiasFin()
        {
            int ano = int.Parse(ddlAnoF.SelectedValue);
            int mes = int.Parse(ddlMesF.SelectedValue);
            int dias = DateTime.DaysInMonth(ano, mes);

            ddlDiaF.Items.Clear();
            for (int i = 1; i <= dias; i++)
            {
                ddlDiaF.Items.Add(i.ToString());
            }
        }
        private DateTime GetFecha(string ano,string mes,string dia)
        {
            if(ano == null || ano == "" || mes == null || mes == "" || dia == null || dia == "")
                return DateTime.MinValue;

            int iano;
            int imes;
            int idia;

            DateTime r;
            try
            {
                iano = int.Parse(ano);
                imes = int.Parse(mes);
                idia = int.Parse(dia);
                r = new DateTime(iano, imes, idia);
            }
            catch(Exception)
            {
                return DateTime.MinValue;
            }
            return r;
        }
        protected void ddlMesF_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizardllDiasFin();
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            LblMsg.Text = "";
            DateTime fechaini = GetFecha(ddlAnoI.SelectedValue, ddlMesI.SelectedValue, ddlDiaI.SelectedValue);
            if (fechaini == DateTime.MinValue)
            {
                Mensaje("Error formato fecha origen", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            DateTime fechafin = GetFecha(ddlAnoF.SelectedValue, ddlMesF.SelectedValue, ddlDiaF.SelectedValue);
            if (fechafin == DateTime.MinValue)
            {
                Mensaje("Error formato fecha destino", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }            

            Buscar(fechaini, fechafin, 1);
        }

        protected void Buscar(DateTime fechaini, DateTime fechafin, int idempresa)
        {
            GVResult.DataSource = new Neg_PlnProteccionDzxFecha().Select(fechaini, fechafin, idempresa);
            GVResult.DataBind();
        }

        protected void BtnAplicar_Click(object sender, EventArgs e)
        {
            DateTime fechaini = GetFecha(ddlAnoI.SelectedValue, ddlMesI.SelectedValue, ddlDiaI.SelectedValue);
            if (fechaini == DateTime.MinValue)
            {
                Mensaje("Error formato fecha origen", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            DateTime fechafin = GetFecha(ddlAnoF.SelectedValue, ddlMesF.SelectedValue, ddlDiaF.SelectedValue);
            if (fechafin == DateTime.MinValue)
            {
                Mensaje("Error formato fecha destino", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }            

            if (fechaini > fechafin)
            {
                Mensaje("Fecha inicio no puede ser mayor que fecha fin", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            int codigo_empleado = 0;

            if (int.TryParse(TxtCodigo.Text.Trim(), out codigo_empleado) == false)
            {
                Mensaje("Codigo de empleado invalido",true);
                return;
            }

            if (fechaini > fechafin)
            {
                Mensaje("Fecha inicio no puede ser mayor que fecha fin", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            string rsp = new Neg_PlnProteccionDzxFecha().Insert(fechaini, fechafin, codigo_empleado, 1);

            if (rsp == "OK")
            {
                Mensaje(rsp, false);
                Buscar(fechaini, fechafin, 1);
                TxtCodigo.Text = "";
            }
            else
            {
                Mensaje(rsp, true);
            }
        }

        protected void BtnBuscarNombre_Click(object sender, EventArgs e)
        {
            string r;
            r = new Neg_Empleados().SelNombreCompleto(TxtCodigo.Text.Trim());

            TxtNombre.Text = r;
        }
    }
}
