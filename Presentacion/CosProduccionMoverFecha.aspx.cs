using System;
using System.Web.UI;
using System.Drawing;

namespace CRP
{
    public partial class CosProduccionMoverFecha : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            dsSeguridad.SegUsuarioRow usr = (dsSeguridad.SegUsuarioRow)this.Page.Session["usuario"];
            if (usr == null)
            {
                Page.Response.Redirect("~/Account/Login.aspx");
            }

            if (!this.Page.IsPostBack)
            {
                this.TxtLeerCaja.Attributes.Add("autocomplete","off");
                this.TxtLeerCaja.Focus();
                this.ddlAno.SelectedValue = DateTime.Now.Year.ToString();
                this.ddlMes.SelectedValue = DateTime.Now.Month.ToString();
                ActualizardllDias();
                this.ddlDia.SelectedValue = DateTime.Now.Day.ToString();
            }
            else
            {
                if (TxtLeerCaja.Text.Trim().Length > 0)
                {
                    LeerCaja(usr.Idusuario);
                }
            }
        }

        void CargarGrid()
        {
            //GVEntradas.DataSource = (System.Data.DataSet)Page.Session["dsCosCajas"];
            //GVEntradas.DataMember = "Cajas";
            //GVEntradas.DataBind();
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

        void LeerCaja(string idusuario)
        {
            if (TxtLeerCaja.Text.Trim().Length == 0)
                return;


            string sts = new Dato_CosProduccion().CosCuadrajeMoverFecha(TxtLeerCaja.Text.Trim(), FechaSeleccionada());

            if (sts == "OK")
            {
                Mensaje("Lectura Correcta " + TxtLeerCaja.Text.Trim(), false);
            }
            
            Mensaje(sts + ": " + TxtLeerCaja.Text.Trim(), true);            

            LblMsg.Visible = true;
            TxtLeerCaja.Text = "";
            TxtLeerCaja.Focus();
        }
        protected void ddlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizardllDias();
        }

        private void ActualizardllDias()
        {
            int ano = int.Parse(ddlAno.SelectedValue);
            int mes = int.Parse(ddlMes.SelectedValue);
            int dias = DateTime.DaysInMonth(ano, mes);

            ddlDia.Items.Clear();
            for (int i = 1; i <= dias; i++)
            {
                ddlDia.Items.Add(i.ToString());
            }
        }

        private DateTime FechaSeleccionada()
        {
            int ano = int.Parse(ddlAno.SelectedValue);
            int mes = int.Parse(ddlMes.SelectedValue);
            int dia = int.Parse(ddlDia.SelectedValue);

            return new DateTime(ano, mes, dia);
        }

    }
}
