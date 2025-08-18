using System;
using System.Web.UI;
using System.Drawing;
using System.Web.Services.Description;

namespace CRP
{
    public partial class CosProduccionMoverFechaRango : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //dsSeguridad.SegUsuarioRow usr = (dsSeguridad.SegUsuarioRow)this.Page.Session["usuario"];
            //if (usr == null)
            //{
            //    Page.Response.Redirect("~/Account/Login.aspx");
            //}

            if (!this.Page.IsPostBack)
            {
                ddlAnoI.SelectedValue = DateTime.Now.Year.ToString();
                ddlMesI.SelectedValue = DateTime.Now.Month.ToString();
                ActualizardllDiasIni();
                ddlDiaI.SelectedValue = DateTime.Now.Day.ToString();

                ddlAnoF.SelectedValue = DateTime.Now.Year.ToString();
                ddlMesF.SelectedValue = DateTime.Now.Month.ToString();
                ActualizardllDiasFin();
                ddlDiaF.SelectedValue = DateTime.Now.Day.ToString();
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

        //string sts = new Dato_CosProduccion().CosCuadrajeMoverFecha(TxtLeerCaja.Text.Trim(), FechaOrigenSeleccionada());
                    
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
        private TimeSpan GetHora(string hora,string minutos)
        {
            if(hora == null || hora == "" || minutos == null || minutos == "")
                return TimeSpan.Zero;

            int ihora = 0;
            int iminutos = 0;

            try
            {
                ihora = int.Parse(hora);
                iminutos = int.Parse(minutos);
            }
            catch(Exception)
            {
                return TimeSpan.Zero;
            }

            return new TimeSpan(ihora,iminutos,0);
        }        

        protected void ddlMesF_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizardllDiasFin();
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            LblMsg.Text = "";
            DateTime fechaorigen = GetFecha(ddlAnoI.SelectedValue, ddlMesI.SelectedValue, ddlDiaI.SelectedValue);
            if (fechaorigen == DateTime.MinValue)
            {
                Mensaje("Error formato fecha origen", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            DateTime fechadestino = GetFecha(ddlAnoF.SelectedValue, ddlMesF.SelectedValue, ddlDiaF.SelectedValue);
            if (fechadestino == DateTime.MinValue)
            {
                Mensaje("Error formato fecha destino", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            TimeSpan HoraInicio = GetHora(ddlHoraI.SelectedValue, ddlMinutoI.SelectedValue);
            if (HoraInicio == TimeSpan.Zero)
            {
                Mensaje("Error formato hora inicio", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            TimeSpan HoraFin = GetHora(ddlHoraF.SelectedValue, ddlMinutoF.SelectedValue);
            if (HoraFin == TimeSpan.Zero)
            {
                Mensaje("Error formato hora fin", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            if (HoraInicio >= HoraFin)
            {
                Mensaje("Hora inicio no puede ser mayor o igual que la hora fin", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            string modulo = ddlModulo.SelectedValue;
            if (modulo.Length < 2)
            {
                Mensaje("Error en numeracion de modulo", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            GVResult.DataSource = new Dato_CosProduccion().CosCuadrajeMoverFechaRangoQ(HoraInicio, HoraFin, fechaorigen, modulo);
            GVResult.DataBind();
        }

        protected void BtnAplicar_Click(object sender, EventArgs e)
        {
            DateTime fechaorigen = GetFecha(ddlAnoI.SelectedValue, ddlMesI.SelectedValue, ddlDiaI.SelectedValue);
            if (fechaorigen == DateTime.MinValue)
            {
                Mensaje("Error formato fecha origen", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            DateTime fechadestino = GetFecha(ddlAnoF.SelectedValue, ddlMesF.SelectedValue, ddlDiaF.SelectedValue);
            if (fechadestino == DateTime.MinValue)
            {
                Mensaje("Error formato fecha destino", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            TimeSpan HoraInicio = GetHora(ddlHoraI.SelectedValue, ddlMinutoI.SelectedValue);
            if (HoraInicio == TimeSpan.Zero)
            {
                Mensaje("Error formato hora inicio", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            TimeSpan HoraFin = GetHora(ddlHoraF.SelectedValue, ddlMinutoF.SelectedValue);
            if (HoraFin == TimeSpan.Zero)
            {
                Mensaje("Error formato hora fin", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            if (HoraInicio >= HoraFin)
            {
                Mensaje("Hora inicio no puede ser mayor que hora fin", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            string modulo = ddlModulo.SelectedValue;
            if (modulo.Length < 2)
            {
                Mensaje("Error en numeracion de modulo", true);
                GVResult.DataSource = null;
                GVResult.DataBind();
                return;
            }

            if (new Dato_CosProduccion().CosCuadrajeMoverFechaRango(HoraInicio, HoraFin, fechaorigen,fechadestino,modulo))
            {
                GVResult.DataSource = new Dato_CosProduccion().CosCuadrajeMoverFechaRangoQ(HoraInicio, HoraFin, fechaorigen, modulo);
                GVResult.DataBind();
            }
        }
    }
}
