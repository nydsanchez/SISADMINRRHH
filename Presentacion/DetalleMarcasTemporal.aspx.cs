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
    public partial class DetalleMarcasTemporal : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Marca Neg_Marca = new Neg_Marca();
        #endregion
        #region VARIABLES GLOBALES
        int horaL, minutoL, segundoL, horaP, minutoP, segundoP;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
               
                lblRM.Visible = false;                
                lblrh.Visible = false;
                Session["horast"] = "";
            }
        }

        #region
        public bool validar()
        {
            int c = 0;
            if (txtFechaIni.Text.Trim() == "" || txtFechaFin.Text.Trim() == "")
            {
                c = c + 1;
                txtFechaFin.Focus();
                lblAlert.Text = "Favor Ingrese una Fecha de Inicio y una fecha Fin";
            }

            else
            {
                if (Convert.ToDateTime(txtFechaIni.Text.Trim()) > Convert.ToDateTime(txtFechaFin.Text.Trim()))
                {
                    lblAlert.Text = "Rango de Fechas invalidos";
                    txtFechaIni.Focus();
                    c = c + 1;
                }

            }

            if (txtCodEmp.Text.Trim() == "")
            {
                c = c + 1;
                txtCodEmp.Focus();
                lblAlert.Text = "Favor Ingrese un Codigo de Empleados";
            }

            if (c > 0)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;

                return false;
            }
            else { return true; }

        }
        #endregion
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                cargargrid();
            }
        }
        public TimeSpan ValidarHora(TimeSpan horaM, TimeSpan horaT, int tipo)
        {
            //ENTRADA
            if (tipo == 1)
            {
                //SI LA HORA DE ENTRADA ES MENOR A LA HORA DE ENTRADA DEL TURNO, SE SETEA A LA HORA INICIAL DEL TURNO
                if (horaM < horaT)
                    return horaT;

                 //SI LA HORA DE ENTRADA ES MAYOR A LA HORA FINAL DEL PERIODO, SE LE ASIGNA LA HORA DE SALIDA
                else return horaM;
            }
            //SALIDA
            else
            {
                if (horaM > horaT)
                    return horaT;

                else return horaM;

            }
        }
        public void cargargrid()
        {
            try
            {
                List<Neg_Empleados> lt = new List<Neg_Empleados>();


                horaL = 0; minutoL = 0; segundoL = 0; horaP = 0; minutoP = 0; segundoP = 0;
                DateTime fechaini = Convert.ToDateTime(txtFechaIni.Text);
                DateTime fechafin = Convert.ToDateTime(txtFechaFin.Text);
                int codigo = Convert.ToInt32(txtCodEmp.Text.Trim());

                DataTable dtR = new DataTable();
                DataTable dtD = new DataTable();
                DataTable dtM = new DataTable();
                DataTable dtRH = new DataTable();
                DataTable dt = Neg_Planilla.obtenerDetalleHorasLab(fechaini, fechafin, codigo);
                lt = Neg_Marca.ObtenerHTxE(codigo, fechaini, fechafin, 3, 3);

                if (lt.Count > 0)
                {
                   
                    lblRM.Visible = true;                   
                    lblrh.Visible = true;
                    dtR = Neg_Planilla.ObtenerPermisosxEmpleadoR(fechaini, fechafin, codigo);

                    dtRH.Columns.Add("horast", typeof(decimal));
                    dtRH.Columns.Add("horasv", typeof(decimal));
                    dtRH.Columns.Add("horascg", typeof(decimal));
                    dtRH.Columns.Add("horassg", typeof(decimal));
                    dtRH.Columns.Add("totalp", typeof(decimal));

                    for (int i = 0; i < lt.Count; i++)
                    {
                        dtM = lt[i].dtHorasT;
                        Session["horast"] = lt[i].horast;
                        dtRH.Rows.Add(lt[i].horast, lt[i].horasv, lt[i].horascg, lt[i].horassg, lt[i].horasapagar);

                    }
                }

                txtCodigo.Text = codigo.ToString();

                GVDetNomEmpl.DataSource = dtM;
                GVDetNomEmpl.DataBind();

                gvpermisos.DataSource = dtR;
                gvpermisos.DataBind();

                gvDatosEmp.DataSource = dt;
                gvDatosEmp.DataBind();

                gvResumen.DataSource = dtRH;
                gvResumen.DataBind();

                this.editMarcas.Visible = true;
                btnCrear.Visible = true;
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        private void limpiar()
        {
            //this.txtNombre.Text = "";
            //this.txtCodigo.Text = "";
            this.txtFecha.Text = "";
            this.txtHoraE.Text = "";
            this.txtHoraS.Text = "";

        }

        public bool validarEdicion()
        {

            if (txtCodigo.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtCodigo.Focus();
                return false;
            }
            if (txtFecha.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtFecha.Focus();
                return false;
            }
            if (txtHoraE.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHoraE.Focus();
                return false;
            }
            if (txtHoraS.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtHoraS.Focus();
                return false;
            }

            return true;
        }
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            if (validarEdicion())
            {
                TimeSpan intervalo = Convert.ToDateTime(TxtFecha2.Text.Trim()) - Convert.ToDateTime(txtFecha.Text.Trim());
                DateTime Fecha = Convert.ToDateTime(txtFecha.Text.Trim());
                DateTime FechaFin = Convert.ToDateTime(TxtFecha2.Text.Trim());

                if (gvDatosEmp.Rows.Count > 0 && txtCodigo.Text.Trim() != "")
                {
                    DateTime FechaIngreso = Convert.ToDateTime(gvDatosEmp.Rows[0].Cells[4].Text.Trim());
                    if (Fecha < FechaIngreso)
                    {
                        alertSucces.Visible = false;
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "La fecha inicio debe ser mayor o igual a la fecha de ingreso del empleado";
                        return;
                    }
                }
                if (FechaFin < Fecha)
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "La fecha fin debe ser mayor o igual a la fecha inicio del empleado";
                    return;
                }
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                for (int i = 0; i < intervalo.Days + 1; i++)
                {

                    //if (Neg_Planilla.InsertarMarcaPorEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()), Fecha, txtHoraE.Text.Trim(), txtHoraS.Text.Trim()))
                    if (Neg_Planilla.InsertarMarcaPorEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()), Fecha, txtHoraE.Text.Trim(), txtHoraS.Text.Trim(), userDetail.getUser()))
                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Marca creada Satisfactoriamente";
                        Fecha = Fecha.AddDays(1);

                        //string resultado = Fecha.ToString("ddMMyyyy");
                    }
                    else
                    {
                        alertSucces.Visible = false;
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Error al crear Marca";
                    }


                }
                cargargrid();
            }
        }

        protected void GVDetNomEmpl_SelectedIndexChanged(object sender, EventArgs e)
        {           
            //this.txtNombre.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[1].Text.Trim();
            this.txtCodigo.Text = gvDatosEmp.Rows[0].Cells[1].Text.Trim();
            this.txtFecha.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[1].Text.Trim();
            this.txtHoraE.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[4].Text.Trim();
            this.txtHoraS.Text = GVDetNomEmpl.Rows[GVDetNomEmpl.SelectedIndex].Cells[5].Text.Trim();
            this.calendarFin.Visible = false;
            btnEditar.Visible = true;
            btnCrear.Visible = false;

        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            TxtFecha2.Text = "";
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

            if (validarEdicion())
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (Neg_DevYDed.EditarMarcasxEmpleado(Convert.ToInt32(txtCodigo.Text.Trim()), Convert.ToDateTime(txtFecha.Text.Trim()), txtHoraE.Text.Trim(), txtHoraS.Text.Trim(), userDetail.getUser()))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Edicion De Marca Satisfactoria";
                    cargargrid();
                    limpiar();
                    this.calendarFin.Visible = true;
                    btnEditar.Visible = false;
                    btnCrear.Visible = true;
                    TxtFecha2.Text = "";
                }
                else
                {
                    alertSucces.Visible = false;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Editar la Marca";
                }
            }
        }

        public void CalculoTotalHoras(int horas, int minutos, int segundos, int tipo)
        {
            //si son horas laboradas
            if (tipo == 1)
            {

                horaL = horaL + horas;
                minutoL = minutoL + minutos;
                segundoL = segundoL + segundos;

                while (segundoL >= 60)
                {
                    minutoL += 1;
                    segundoL = segundoL - 60;
                }

                while (minutoL >= 60)
                {
                    horaL += 1;
                    minutoL = minutoL - 60;
                }


            }
            else
            {
                horaP = horaP + horas;
                minutoP = minutoP + minutos;
                segundoP = segundoP + segundos;

                while (segundoP >= 60)
                {
                    minutoP += 1;
                    segundoP = segundoP - 60;
                }
                while (minutoP >= 60)
                {
                    horaP += 1;
                    minutoP = minutoP - 60;
                }


            }


        }
        protected void GVDetNomEmpl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[5].Text = "Total";
                e.Row.Cells[6].Text = String.Format("{0:0.00}", Convert.ToDecimal(Session["horast"].ToString()));

            }
        }
    }
}