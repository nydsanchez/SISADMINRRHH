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
    public partial class Permisos : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Permisos Neg_Permisos = new Neg_Permisos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //obtenerUbicaciones();
                obtenerTipoPermisos();
                obtenerProcesos();
                obtenerUbicaciones();
            }

        }
        private void obtenerUbicaciones()
        {
            this.ddlubicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlubicacion.DataMember = "ubicaciones";
            this.ddlubicacion.DataValueField = "codigo_ubicacion";
            this.ddlubicacion.DataTextField = "nombre_ubicacion";
            this.ddlubicacion.DataBind();
        }
        private void obtenerProcesos()
        {
            this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }

        private void obtenerTipoPermisos()
        {
            DataSet ds = Neg_Catalogos.cargarTipoPermisos();
            this.ddlTipoPermiso.DataSource = ds;
            this.ddlTipoPermiso.DataMember = "permisos";
            this.ddlTipoPermiso.DataValueField = "Id_Descripcion";
            this.ddlTipoPermiso.DataTextField = "Descripcion";
            this.ddlTipoPermiso.DataBind();

            this.ddltipoB.DataSource = ds;
            this.ddltipoB.DataMember = "permisos";
            this.ddltipoB.DataValueField = "Id_Descripcion";
            this.ddltipoB.DataTextField = "Descripcion";
            this.ddltipoB.DataBind();
        }

        //private void obtenerUbicaciones()
        //{
        //    this.ddlUbicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
        //    this.ddlUbicacion.DataMember = "ubicaciones";
        //    this.ddlUbicacion.DataValueField = "codigo_ubicacion";
        //    this.ddlUbicacion.DataTextField = "nombre_ubicacion";
        //    this.ddlUbicacion.DataBind();
        //}

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
                        
            int codigo_empleado=0;
            int codigo_depto = 0;
            DateTime fechini = new DateTime();
            DateTime fechfin = new DateTime();
            TimeSpan hrini = TimeSpan.Zero;
            TimeSpan hrfin = TimeSpan.Zero;

            if (ddlAsigPerm.SelectedValue == "1")
            {
                if (!int.TryParse(txtCodigo.Text.Trim(),out codigo_empleado))
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese un Codigo de Empleado Valido";
                    return;
                }
            }

            if (ddlAsigPerm.SelectedValue == "2" || ddlAsigPerm.SelectedValue == "5")
            {
                if (!int.TryParse(ddlProceso.SelectedValue, out codigo_depto))
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Seleccione un Departamento Valido";
                    return;
                }
            }

            if (txtObserv.Text.Length < 8)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese una Observacion";
                return;
            }

            if (!DateTime.TryParse(txtFechaIni.Text.Trim(),out fechini))
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese una Fecha de Inicio Valida";
                return;
            }
            
            if (ddlDiasHrs.SelectedValue == "1")//dias
            {
                if (!DateTime.TryParse(txtFechaFin.Text.Trim(), out fechfin))
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Ingrese una Fecha de Fin Valida";
                    return;
                }                
            }

            //Cambio para q puedan agregar las horas q fuesen necesarias, no limitarlas a q fueran menos q medio dia
            if (ddlDiasHrs.SelectedValue == "2")
            {
                if (!TimeSpan.TryParse(txtHoraIni.Text, out hrini) || !TimeSpan.TryParse(txtHoraFin.Text, out hrfin))
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Formato de hora invalido";
                    return;
                }               
                                
            }

            if (Neg_Permisos.AgregarPermiso(int.Parse(ddlAsigPerm.SelectedValue), int.Parse(ddlDiasHrs.SelectedValue), int.Parse(ddlTipoPermiso.SelectedValue), codigo_empleado, codigo_depto,
                            fechini, fechfin, hrini, hrfin, 0, txtObserv.Text,Convert.ToInt32(ddlubicacion.SelectedValue)))
            {
                limpiar();
                alertValida.Visible = false;
                alertSucces.Visible = true;
                LblSuccess.Visible = true;
                LblSuccess.Text = "Permiso Agregado Satisfactoriamente";
            }          
        }

        void limpiar()
        {
            this.txtFechaIni.Text = "";
            this.txtFechaFin.Text = "";
            this.txtHoraIni.Text = "";
            this.txtHoraFin.Text = "";
            this.txtCantidadDias.Text = "";
            txtHoras.Text = "";
            this.txtObserv.Text = "";
            txtCodigo.Text = "";
            txtNombreEmpleado.Text = "";
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() != "")
            {
                
                DateTime pfini, pffin;
                string tipo = rbRango.SelectedValue.Trim();
                string idtipo = ddltipoB.SelectedValue.Trim();
                //bool todos = rbRango.SelectedValue == "1" ? false : true;
                pfini = string.IsNullOrEmpty(txtFechaIni2.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(txtFechaIni2.Text.Trim());
                pffin = string.IsNullOrEmpty(txtFechaFin2.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(txtFechaFin2.Text.Trim());
                
                obtenerDetalleVacaciones(Convert.ToInt32(txtBuscar.Text), pfini, pffin, tipo, idtipo);
            }
        }

        private void obtenerDetalleVacaciones(int codigo, DateTime fechaIni, DateTime fechaFin, string tipo, string idtipo)
        {
            DataSet ds = new DataSet();
            ds = Neg_Permisos.BuscarEmpleado(codigo, fechaIni, fechaFin, tipo, idtipo);
            if (ds.Tables.Count > 0)
            {
                GVDetallePermisos.DataSource = ds;
                GVDetallePermisos.DataMember = "Vacaciones";
                GVDetallePermisos.DataBind();
            }
        }

        protected void GVDetallePermisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTitle.Visible = true;
            divEdit.Visible = true;
            divElim.Visible = true;
            divrango.Visible = true;
            
            this.txtFechaIni2.Text = GVDetallePermisos.Rows[GVDetallePermisos.SelectedIndex].Cells[3].Text.Trim();
            this.txtFechaFin2.Text = GVDetallePermisos.Rows[GVDetallePermisos.SelectedIndex].Cells[4].Text.Trim();
            this.TxtHoraini2.Text = GVDetallePermisos.Rows[GVDetallePermisos.SelectedIndex].Cells[5].Text.Trim();
            this.TxtHorafin2.Text = GVDetallePermisos.Rows[GVDetallePermisos.SelectedIndex].Cells[6].Text.Trim();
            this.txtDiasEdit.Text = GVDetallePermisos.Rows[GVDetallePermisos.SelectedIndex].Cells[7].Text.Trim();
            this.txtHorasEdit.Text = GVDetallePermisos.Rows[GVDetallePermisos.SelectedIndex].Cells[8].Text.Trim();

            Session["dias"] = txtDiasEdit.Text;
            Session["horas"] = txtHorasEdit.Text;
            Session["fechaini"] = txtFechaIni2.Text;
            Session["fechafin"] = txtFechaFin2.Text;
            Session["horaini"] = TxtHoraini2.Text;
            Session["horafin"] = TxtHorafin2.Text;
            Session["tipoPermiso"] = GVDetallePermisos.Rows[GVDetallePermisos.SelectedIndex].Cells[10].Text.Trim();
            
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //if (txtBuscar.Text.Trim() != "" && txtFechaIni2.Text.Trim() != "" && txtFechaFin2.Text.Trim() != "" && txtDiasEdit.Text.Trim() != "" && txtHorasEdit.Text.Trim() != "")
            //{
            //    decimal cantidadEditAnt = Convert.ToDecimal(Session["dias"]);
            //    DateTime fechaIni = Convert.ToDateTime(Session["fechaini"]);
            //    DateTime fechaFin = Convert.ToDateTime(Session["fechafin"]);
            //    TimeSpan horaini = TimeSpan.Parse(Session["HoraIni"].ToString());
            //    TimeSpan horafin = TimeSpan.Parse(Session["HoraFin"].ToString());

            //    decimal cantidadAct = Convert.ToDecimal(this.txtDiasEdit.Text);
            //    decimal horasAct = Convert.ToDecimal(this.txtHorasEdit.Text);

            //    int codEmpleado = Convert.ToInt32(txtBuscar.Text);
            //    int p = Neg_Permisos.validarEditElimPermiso(Convert.ToInt32(txtBuscar.Text));
            //    if (p == 0)
            //    {
            //        Neg_Permisos.editarPermiso(cantidadEditAnt, cantidadAct, codEmpleado, fechaIni, fechaFin,horaini,horafin);
            //        obtenerDetalleVacaciones(Convert.ToInt32(txtBuscar.Text), Convert.ToDateTime(txtFechaIni2.Text.Trim()), Convert.ToDateTime(txtFechaFin2.Text.Trim()));
            //        alertValida.Visible = false;
            //        alertSucces.Visible = true;
            //        LblSuccess.Visible = true;
            //        LblSuccess.Text = "Permiso Editado Satisfactoriamente";
            //        txtCantidadEdit.Text = "";
            //    }
            //    if (p == 1)
            //    {
            //        alertSucces.Visible = false;
            //        alertValida.Visible = true;
            //        lblAlert.Visible = true;
            //        lblAlert.Text = "El Permiso que desea Editar se encuentra en un perido que ya fue cerrado";
            //    }
            //}
            //else
            //{
            //    alertSucces.Visible = false;
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Ingrese todos los datos correspondientes";
            //}
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Trim() != "" && txtFechaIni2.Text.Trim() != "" && txtFechaFin2.Text.Trim() != "" && txtDiasEdit.Text.Trim() != "")
            {
                decimal cantidadEditAnt = Convert.ToDecimal(Session["dias"]);
                DateTime fechaIni = Convert.ToDateTime(Session["fechaini"]);
                DateTime fechaFin = Convert.ToDateTime(Session["fechafin"]);
                TimeSpan horaini = TimeSpan.Parse(Session["HoraIni"].ToString());
                TimeSpan horafin = TimeSpan.Parse(Session["HoraFin"].ToString());

                decimal dias = Convert.ToDecimal(this.txtDiasEdit.Text);
                decimal horas = Convert.ToDecimal(this.txtHorasEdit.Text);

                int codEmpleado = Convert.ToInt32(txtBuscar.Text);
                string tipoPermiso = Session["tipoPermiso"].ToString();

                DateTime pfini, pffin;
                string tipo = rbRango.SelectedValue.Trim();
                string idtipo = ddltipoB.SelectedValue.Trim();
                //bool todos = rbRango.SelectedValue == "1" ? false : true;
                pfini = string.IsNullOrEmpty(txtFechaIni2.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(txtFechaIni2.Text.Trim());
                pffin = string.IsNullOrEmpty(txtFechaFin2.Text.Trim()) ? DateTime.Now : Convert.ToDateTime(txtFechaFin2.Text.Trim());

                //int p = Neg_Permisos.validarEditElimPermiso(Convert.ToInt32(txtBuscar.Text));
                //if (p == 0)
                //{
                if (Neg_Permisos.eliminarPermisos(dias, horas, codEmpleado, fechaIni, fechaFin, tipoPermiso))
                    {
                        obtenerDetalleVacaciones(Convert.ToInt32(txtBuscar.Text),pfini , pffin,tipo,idtipo);
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Permiso Eliminado Satisfactoriamente";
                        //txtCantidadEdit.Text = "";
                    }
                //}
                else
                {
                    alertSucces.Visible = false;
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Ocurrio un error al eliminar el permiso";
                }
            }
            else
            {
                alertSucces.Visible = false;
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Ingrese todos los datos correspondientes";
            }
        }

        protected void ddlAsigPerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAsigPerm.SelectedValue == "1")//ind
            {
                divempleado.Visible = true;
                divproceso.Visible = false;
                divubic.Visible = false;
            }
            else if (ddlAsigPerm.SelectedValue == "2")//dpto
            {
                divempleado.Visible = false;
                divproceso.Visible = true;
                divubic.Visible = false;
            }
            else if (ddlAsigPerm.SelectedValue == "3")//todos
            {
                divempleado.Visible = false;
                divproceso.Visible = false;
                divubic.Visible = false;
            }
            else if(ddlAsigPerm.SelectedValue == "4")//ubicacion
            {
                divempleado.Visible = false;
                divproceso.Visible = false;
                divubic.Visible = true;
            }
            else{
                divempleado.Visible = false;
                divproceso.Visible = true;
                divubic.Visible = true;
            }
            ddlAsigPerm.Focus();
        }

        protected void ddlDiasHrs_SelectedIndexChanged(object sender, EventArgs e)
        {
            EstablecerTiempo();
            ddlDiasHrs.Focus();          
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            txtNombreEmpleado.Text = Neg_Permisos.obtenerNombreEmpleado(txtCodigo.Text);
            txtCodigo.Focus();
        }

        protected void txtFechaIni_TextChanged(object sender, EventArgs e)
        {
            EstablecerTiempo();
            txtFechaIni.Focus();
        }

        protected void txtFechaFin_TextChanged(object sender, EventArgs e)
        {
            EstablecerTiempo();
            txtFechaFin.Focus();
        }

        protected void txtHoraIni_TextChanged(object sender, EventArgs e)
        {
            EstablecerTiempo();
            txtHoraIni.Focus();
        }

        protected void txtHoraFin_TextChanged(object sender, EventArgs e)
        {
            EstablecerTiempo();
            txtHoraFin.Focus();
        }

        protected double EstablecerTiempo()
        {
            
            DateTime fchini = new DateTime();
            DateTime fchfin = new DateTime();
            TimeSpan hrini = new TimeSpan();
            TimeSpan hrfin = new TimeSpan();

            if (ddlDiasHrs.SelectedValue == "1")//Dias
            {
                divdias.Visible = true;
                divhoras.Visible = false;

                if (!DateTime.TryParse(txtFechaIni.Text.Trim(), out fchini))
                {
                    return 0;
                }

                if (!DateTime.TryParse(txtFechaFin.Text.Trim(), out fchfin))
                {
                    return 0;
                }

                int n = (fchfin - fchini).Days + 1;
                txtCantidadDias.Text = n.ToString();
                return (double)n;
            }
            else//Horas
            {
                divdias.Visible = false;
                divhoras.Visible = true;

                if (!DateTime.TryParse(txtFechaIni.Text.Trim(), out fchini))
                {
                    return 0;
                }

                if (!TimeSpan.TryParse(txtHoraIni.Text, out hrini))
                {
                    return 0;
                }

                if (!TimeSpan.TryParse(txtHoraFin.Text, out hrfin))
                {
                    return 0;
                }

                double m = (hrfin - hrini).TotalHours;
                txtHoras.Text = m.ToString("00.00");
                return m;
            }
        }

        protected void rbRango_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbRango.SelectedValue == "1")
            {
                divrango.Visible = true;
                divtipo.Visible = false;
                txtFechaIni2.Text = DateTime.Now.ToShortDateString();
                txtFechaFin2.Text = DateTime.Now.ToShortDateString();
            }
            else if (rbRango.SelectedValue == "2")//tipo
            {
                divtipo.Visible = true;
                divrango.Visible = false;
            }
            else//todos
            {
                divtipo.Visible = false;
                divrango.Visible = false;

            }
            lblTitle.Visible = false;
            divEdit.Visible = false;
            divElim.Visible = false;
            txtDiasEdit.Text = "";
            GVDetallePermisos.DataSource = null;           
            GVDetallePermisos.DataBind();
            btnBuscar.Focus();
        }
    }
}