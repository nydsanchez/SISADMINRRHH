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
    public partial class turnos : System.Web.UI.Page
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Turnos Neg_Turnos = new Neg_Turnos();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                iniciarGrid();
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
           

            if(validar())
            {
                int lunes, martes, miercoles, jueves, viernes, sabado, domingo;

                if (txtHoraEntradaLunes.Text.Trim().Length > 0)
                {
                    lunes = 1;
                }
                else
                {
                    lunes = 0;
                }
                if (txtHoraEntradaMartes.Text.Trim().Length > 0)
                {
                    martes = 1;

                }
                else
                {
                    martes = 0;
                }

                if (txtHoraEntradaMiercoles.Text.Trim().Length > 0)
                {
                    miercoles = 1;
                }
                else
                {
                    miercoles = 0;
                }

                if (txtHoraEntradaJueves.Text.Trim().Length > 0)
                {
                    jueves = 1;
                }
                else
                {
                    jueves = 0;
                }

                if (txtHoraEntradaViernes.Text.Trim().Length > 0)
                {
                    viernes = 1;
                }
                else
                {
                    viernes = 0;
                }

                if (txtHoraEntradaSabado.Text.Trim().Length > 0)
                {
                    sabado = 1;
                }
                else
                {
                    sabado = 0;
                }

                if (txtHoraEntradaDomingo.Text.Trim().Length > 0)
                {
                    domingo = 1;
                }
                else
                {
                    domingo = 0;
                }

                AgregarTurno(lunes, martes, miercoles, jueves, viernes, sabado, domingo);
                
            }
        }

        public bool AgregarTurno(int lunes, int martes, int miercoles, int jueves, int viernes, int sabado, int domingo)
        {


            if (Neg_Turnos.AgregarTurno(txtNombTurno.Text.Trim(), txtMinComodin.Text.Trim(), lunes, txtHoraEntradaLunes.Text.Trim(), txtHoraSalidaLunes.Text.Trim(), txtHorComidaLunes.Text.Trim(),
            martes, txtHoraEntradaMartes.Text.Trim(), txtHoraSalidaMartes.Text.Trim(), txtHoraComidaMartes.Text.Trim(), miercoles,
            txtHoraEntradaMiercoles.Text.Trim(), txtHoraSalidaMiercoles.Text.Trim(), txtHoraComidaMiercoles.Text.Trim(),
            jueves, txtHoraEntradaJueves.Text.Trim(), txtHoraSalidaJueves.Text.Trim(), txtHoraComidaJueves.Text.Trim(), viernes, txtHoraEntradaViernes.Text.Trim(),
            txtHoraSalidaViernes.Text.Trim(), txtHoraComidaViernes.Text.Trim(), sabado, txtHoraEntradaSabado.Text.Trim(), txtHoraSalidaSabado.Text.Trim(),
            txtHoraComidaSabado.Text.Trim(), domingo, txtHoraEntradaDomingo.Text.Trim(), txtHoraSalidaDomingo.Text.Trim(), txtHoraComidaDomingo.Text.Trim()))
            {
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "Ingreso Satisfactorio";
                limpiar();
                iniciarGrid();
            }
      
            return true;
        }

        private void limpiar()
        {
            this.txtNombTurno.Text = "";
            this.txtMinComodin.Text = "";
            this.txtHoraEntradaLunes.Text = "";
            this.txtHoraSalidaLunes.Text = "";
            this.txtHorComidaLunes.Text = "";
            this.txtHoraEntradaMartes.Text = "";
            this.txtHoraSalidaMartes.Text = "";
            this.txtHoraComidaMartes.Text = "";
            this.txtHoraEntradaMiercoles.Text = "";
            this.txtHoraSalidaMiercoles.Text = "";
            this.txtHoraComidaMiercoles.Text = "";
            this.txtHoraEntradaJueves.Text = "";
            this.txtHoraSalidaJueves.Text = "";
            this.txtHoraComidaJueves.Text = "";
            this.txtHoraEntradaViernes.Text = "";
            this.txtHoraSalidaViernes.Text = "";
            this.txtHoraComidaViernes.Text = "";
            this.txtHoraEntradaSabado.Text = "";
            this.txtHoraSalidaSabado.Text = "";
            this.txtHoraComidaSabado.Text = "";
            this.txtHoraEntradaDomingo.Text = "";
            this.txtHoraSalidaDomingo.Text = "";
            this.txtHoraComidaDomingo.Text = "";

        }


        public bool validar()
        {
            if (txtNombTurno.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese el nombre de turno";
                return false;
            }

            if(txtMinComodin.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese la cantidad de minutos comodin";
                return false;

            }
            if(txtTotalHoras.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese El Total De Horas Por Semana";
                return false;
            }

            return true;
        }

        private void iniciarGrid()
        {
            GVturnos.DataSource = Neg_Turnos.turnos();
            GVturnos.DataMember = "turnos";
            GVturnos.DataBind();
        }

        protected void GVturnos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtNombTurno.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[1].Text.Trim();
            this.txtHoraEntradaLunes.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[2].Text.Trim();   
            this.txtHoraSalidaLunes.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[3].Text.Trim();
            this.txtHoraEntradaMartes.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[4].Text.Trim();
            this.txtHoraSalidaMartes.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[5].Text.Trim();
            this.txtHoraEntradaMiercoles.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[6].Text.Trim();
            this.txtHoraSalidaMiercoles.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[7].Text.Trim();
            this.txtHoraEntradaJueves.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[8].Text.Trim();
            this.txtHoraSalidaJueves.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[9].Text.Trim();
            this.txtHoraEntradaViernes.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[10].Text.Trim();
            this.txtHoraSalidaViernes.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[11].Text.Trim();
            if (GVturnos.Rows[GVturnos.SelectedIndex].Cells[12].Text.Trim() != "00:00")
            { 
                 this.txtHoraEntradaSabado.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[12].Text.Trim();
            }
            else
            {
                this.txtHoraEntradaSabado.Text = "";
            }
            if (GVturnos.Rows[GVturnos.SelectedIndex].Cells[13].Text.Trim() != "00:00")
            { 
                this.txtHoraSalidaSabado.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[13].Text.Trim();
            }
            else
            {
                this.txtHoraSalidaSabado.Text = "";
            }
            if (GVturnos.Rows[GVturnos.SelectedIndex].Cells[14].Text.Trim() != "00:00")
            { 
                this.txtHoraEntradaDomingo.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[14].Text.Trim();
            }
            else 
            {
                this.txtHoraEntradaDomingo.Text = "";
            }
            if (GVturnos.Rows[GVturnos.SelectedIndex].Cells[15].Text.Trim() != "00:00")
            { 
                this.txtHoraSalidaDomingo.Text = GVturnos.Rows[GVturnos.SelectedIndex].Cells[15].Text.Trim();
            }
            else
            {
                this.txtHoraSalidaDomingo.Text = "";
            }
            Session["tempNomTurno"] = txtNombTurno.Text;
            btnAgregar.Visible = false;
            BtnEditar.Visible = true;
            btnEliminar.Visible = true;
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                int lunes, martes, miercoles, jueves, viernes, sabado, domingo;

                if (txtHoraEntradaLunes.Text.Trim().Length > 0)
                {
                    lunes = 1;
                }
                else
                {
                    lunes = 0;
                }
                if (txtHoraEntradaMartes.Text.Trim().Length > 0)
                {
                    martes = 1;

                }
                else
                {
                    martes = 0;
                }

                if (txtHoraEntradaMiercoles.Text.Trim().Length > 0)
                {
                    miercoles = 1;
                }
                else
                {
                    miercoles = 0;
                }

                if (txtHoraEntradaJueves.Text.Trim().Length > 0)
                {
                    jueves = 1;
                }
                else
                {
                    jueves = 0;
                }

                if (txtHoraEntradaViernes.Text.Trim().Length > 0)
                {
                    viernes = 1;
                }
                else
                {
                    viernes = 0;
                }

                if (txtHoraEntradaSabado.Text.Trim().Length > 0)
                {
                    sabado = 1;
                }
                else
                {
                    sabado = 0;
                }

                if (txtHoraEntradaDomingo.Text.Trim().Length > 0)
                {
                    domingo = 1;
                }
                else
                {
                    domingo = 0;
                }

                EditarTurno(lunes, martes, miercoles, jueves, viernes, sabado, domingo);
            }
        }

        public bool EditarTurno(int lunes, int martes, int miercoles, int jueves, int viernes, int sabado, int domingo)
        {
            if (Neg_Turnos.EditarTurno(txtNombTurno.Text.Trim(), txtMinComodin.Text.Trim(), lunes, txtHoraEntradaLunes.Text.Trim(), txtHoraSalidaLunes.Text.Trim(), txtHorComidaLunes.Text.Trim(),
            martes, txtHoraEntradaMartes.Text.Trim(), txtHoraSalidaMartes.Text.Trim(), txtHoraComidaMartes.Text.Trim(), miercoles,
            txtHoraEntradaMiercoles.Text.Trim(), txtHoraSalidaMiercoles.Text.Trim(), txtHoraComidaMiercoles.Text.Trim(),
            jueves, txtHoraEntradaJueves.Text.Trim(), txtHoraSalidaJueves.Text.Trim(), txtHoraComidaJueves.Text.Trim(), viernes, txtHoraEntradaViernes.Text.Trim(),
            txtHoraSalidaViernes.Text.Trim(), txtHoraComidaViernes.Text.Trim(), sabado, txtHoraEntradaSabado.Text.Trim(), txtHoraSalidaSabado.Text.Trim(),
            txtHoraComidaSabado.Text.Trim(), domingo, txtHoraEntradaDomingo.Text.Trim(), txtHoraSalidaDomingo.Text.Trim(), txtHoraComidaDomingo.Text.Trim(), Session["tempNomTurno"].ToString()))
            {
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "Actualizacion Satisfactoria";
                limpiar();
                iniciarGrid();
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Error al Editar Turno";
            }

            return true;

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if(Neg_Turnos.EliminarTurno(Session["tempNomTurno"].ToString()))
            {
                limpiar();
                iniciarGrid();
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "Turno Emiminado Satisfactoriamente";
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Error al Eliminar Turno";
            }
        }

    }
}