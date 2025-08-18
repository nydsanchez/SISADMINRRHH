using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using System.Data;
using Microsoft.Reporting.WebForms;
namespace NominaRRHH.Presentacion
{
    public partial class Amonestaciones : System.Web.UI.Page
    {
        #region
        Neg_Amonestaciones Neg_Amonestaciones = new Neg_Amonestaciones();
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.SetActiveView(vgeneral);
                btnMostrar.Visible = true;
                btnActualizar.Visible = false;
                btnCancelar.Visible = false;
                btnEliminar.Visible = false;
                btnEditar.Visible = false;

                alertSucces.Visible = false;
                alertValida.Visible = false;
                divconsultaAlert.Visible = false;
                divconsultaSuccess.Visible = false;

                ddltipo.DataSource = obtenerAmonestaciones();
                ddltipo.DataBind();



                ddltipoSancion.DataSource = ObtenerSancion();
                ddltipoSancion.DataBind();

                ddlamonestacion();

                Session["IdAmonestacion"] = "";
                Session["sancion"] = "";
                Session["fechaAMonestacion"] = "";
                Session["fecharegistro"] = "";
                Session["horaregistro"] = "";

                //txtBuscar.Visible = false;
                //txtFechaIni2.Visible = false;
                //txtFechaFin2.Visible = false;
            }

        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                ingresar(1);

            }
            else
            {
                lblAlert.Text = "LLENE TODOS LOS CAMPOS";
                alertValida.Visible = true;
                alertSucces.Visible = false;

            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            bool bycodigo = false, byrbRango = false;
            int codigo = 0;
            DateTime fechaini = DateTime.Now, fechafin = DateTime.Now;
            if (rbCodgio.SelectedValue == "1")
            {
                if (txtBuscar.Text != "")
                {
                    bycodigo = true;
                    codigo = int.Parse(txtBuscar.Text);
                }

                else
                {
                    lblconsultaAlert.Text = "LLENE TODOS LOS CAMPOS";
                    lblconsultaAlert.Visible = true;
                    lblconsultaSuccess.Visible = false;
                    divconsultaAlert.Visible = true;
                    divconsultaSuccess.Visible = false;
                    btnBuscar.Focus();
                    return;
                }

            }

            if (rbRango.SelectedValue == "1")
            {
                if (txtFechaIni2.Text != "" && txtFechaFin2.Text != "")
                {

                    fechaini = Convert.ToDateTime(txtFechaIni2.Text);
                    fechafin = Convert.ToDateTime(txtFechaFin2.Text);
                    if (fechaini > fechafin)
                    {
                        lblconsultaAlert.Text = "RANGO DE FECHAS INCORRECTO FECHA INICIAL ES MAYOR A LA FECHA FINAL";

                        lblconsultaAlert.Visible = true;
                        lblconsultaSuccess.Visible = false;
                        divconsultaAlert.Visible = true;
                        divconsultaSuccess.Visible = false;
                        btnBuscar.Focus();
                        return;
                    }
                    byrbRango = true;
                    lblAlert.Text = "";
                    divconsultaAlert.Visible = false;
                    divconsultaSuccess.Visible = false;

                }
                else
                {
                    lblconsultaAlert.Text = "LLENE LOS CAMPOS RANGO DE FECHA";
                    lblconsultaAlert.Visible = true;
                    lblconsultaSuccess.Visible = false;
                    divconsultaAlert.Visible = true;
                    divconsultaSuccess.Visible = false;
                    btnBuscar.Focus();
                    return;
                }
            }
            divconsultaAlert.Visible = false;
            divconsultaSuccess.Visible = false;
            obteneramonestaciones(fechaini, fechafin, codigo, bycodigo, byrbRango);
            btnBuscar.Focus();

        }

        protected void GVDetallePermisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt1 = (DataTable)Session["DTAmonestaciones"];
            ddltipo.DataSource = dt1;
            ddltipo.DataBind();


            DataTable dt = (DataTable)Session["DTipo"];
            Session["DTipo"] = dt;

            ddltipoSancion.DataSource = dt;
            ddltipoSancion.DataBind();


            this.txtCodigo.Text = gvAmonestaciones.Rows[0].Cells[2].Text.Trim();
            this.txtFechaIni.Text = gvAmonestaciones.Rows[gvAmonestaciones.SelectedIndex].Cells[5].Text.Trim();




            ddltipo.SelectedValue = gvAmonestaciones.DataKeys[gvAmonestaciones.SelectedIndex].Values[0].ToString();
            ddlamonestacion();
            ddltipoSancion.SelectedValue = gvAmonestaciones.DataKeys[gvAmonestaciones.SelectedIndex].Values[1].ToString();
            string observacion = "";
            if (gvAmonestaciones.Rows[gvAmonestaciones.SelectedIndex].Cells[7].Text != "&nbsp;")
            {
                observacion = gvAmonestaciones.Rows[gvAmonestaciones.SelectedIndex].Cells[7].Text;
            }
            this.txtob.Text = observacion;

            Session["IdAmonestacion"] = int.Parse(ddltipo.SelectedValue.ToString()); ;
            Session["sancion"] = ddltipoSancion.SelectedItem.ToString();
            Session["fechaAMonestacion"] = Convert.ToDateTime(gvAmonestaciones.Rows[gvAmonestaciones.SelectedIndex].Cells[5].Text);
            Session["fecharegistro"] = Convert.ToDateTime(gvAmonestaciones.Rows[gvAmonestaciones.SelectedIndex].Cells[8].Text);
            Session["horaregistro"] = TimeSpan.Parse(gvAmonestaciones.Rows[gvAmonestaciones.SelectedIndex].Cells[9].Text);

            btnMostrar.Visible = false;
            btnActualizar.Visible = false;
            btnCancelar.Visible = false;
            btnEliminar.Visible = true;
            btnEditar.Visible = true;

            alertValida.Visible = false;
            alertSucces.Visible = false;

            HabilitarInabilitar(false);

        }

        protected void ddltipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlamonestacion();
        }

        #region METODO
        public void ddlamonestacion()
        {

            string id = ddltipo.SelectedValue.ToString();
            DataTable dt = (DataTable)Session["DTAmonestaciones"];
            DataTable copyDataTable;
            copyDataTable = dt.Copy();
            DataView dtv = copyDataTable.DefaultView;
            dtv.RowFilter = "idAmonestacion='" + id + "'";
            asignarCodigoFalta(dtv);
            copyDataTable.Clear();

        }
        public void asignarCodigoFalta(DataView dtv)
        {
            string codigo = "", falta = "";
            if (dtv.Count > 0)
            {
                if (dtv[0][2].ToString() != "&nbsp;")
                {
                    codigo = dtv[0][2].ToString();
                }
                if (dtv[0][3].ToString() != "&nbsp;")
                {
                    falta = dtv[0][3].ToString();
                }
            }

            lblcodigo.Text = codigo;
            nivelf.Text = falta;
        }
        public void obteneramonestaciones(DateTime fechaini, DateTime fechafin, int codigoE, bool bycodigo, bool byrango)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            List<Neg_Amonestaciones.DetalleAmonestacion> la = Neg_Amonestaciones.getamonestacionesByRango(fechaini, fechafin, codigoE, bycodigo, byrango, userDetail.getIDEmpresa());
            gvAmonestaciones.DataSource = la;
            gvAmonestaciones.DataBind();
            gvAmonestaciones.Focus();
        }
        public bool validarCampos()
        {
            int c = 0;

            if (txtCodigo.Text == "")
            {
                c = c + 1;
            }
            if (txtFechaIni.Text == "")
            {
                c = c + 1;
            }

            if (c > 0)
            {
                return false;
            }
            else
                return true;

        }
        public void limpiarCampos()
        {
            txtCodigo.Text = "";
            txtFechaIni.Text = "";
            txtob.Text = "";
            txtBuscarNombre.Text = "";
        }
        #endregion
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (validarCampos())
            {
                ingresar(2);
            }
            else
            {
                lblAlert.Text = "LLENE TODOS LOS CAMPOS";
                alertValida.Visible = false;
                alertSucces.Visible = true;
            }
        }
        public void ingresar(int operacion)
        {

            int codigo = int.Parse(txtCodigo.Text);
            int tipoamon = int.Parse(ddltipo.SelectedValue.ToString());
            string tiposancion = ddltipoSancion.SelectedItem.ToString();
            DateTime fecha = Convert.ToDateTime(txtFechaIni.Text);
            string observacion = "";
            if (txtob.Text != "")
            {
                observacion = txtob.Text;
            }
            if (operacion == 1)
            {
                if (Neg_Amonestaciones.InsertarAmonestaciones(codigo, tipoamon, tiposancion, fecha, observacion))
                {
                    if (Neg_Amonestaciones.AmonestacionesLOGInsertar(codigo, tipoamon, tipoamon, tiposancion, tiposancion, fecha, fecha, observacion, "INSERCION"))
                    {
                        obteneramonestaciones(fecha, fecha, codigo, true, true);
                        limpiarCampos();
                        HabilitarInabilitar(true);
                        txtFechaIni2.Text = fecha.ToString();
                        txtFechaFin2.Text = fecha.ToString();

                        btnMostrar.Visible = true;
                        btnActualizar.Visible = false;
                        btnCancelar.Visible = true;
                        btnEliminar.Visible = false;
                        btnEditar.Visible = false;


                        LblSuccess.Text = "AMONESTACION REGISTRADA";
                        alertValida.Visible = false;
                        alertSucces.Visible = true;

                    }
                    else
                    {
                        lblAlert.Text = "PROBLEMA AL INTENTAR REGISTRAR AMONESTACION";
                        alertValida.Visible = true;
                        alertSucces.Visible = false;

                    }
                }

                else
                {
                    lblAlert.Text = "PROBLEMA AL INTENTAR REGISTRAR AMONESTACION";
                    alertValida.Visible = true;
                    alertSucces.Visible = false;

                }
            }

            if (operacion == 2)
            {
                if ((int.Parse(Session["IdAmonestacion"].ToString()) != tipoamon) || (Session["sancion"].ToString() != tiposancion) || (Convert.ToDateTime(Session["fechaAMonestacion"]) != fecha))
                {
                    if (Neg_Amonestaciones.UpdateAmonestaciones(codigo, int.Parse(Session["IdAmonestacion"].ToString()), tipoamon, Session["sancion"].ToString(), tiposancion, Convert.ToDateTime(Session["fechaAMonestacion"]), fecha, Convert.ToDateTime(Session["fecharegistro"]), TimeSpan.Parse(Session["horaregistro"].ToString()), observacion))
                    {
                        if (Neg_Amonestaciones.AmonestacionesLOGInsertar(codigo, int.Parse(Session["IdAmonestacion"].ToString()), tipoamon, Session["sancion"].ToString(), tiposancion, Convert.ToDateTime(Session["fechaAMonestacion"]), fecha, observacion, "EDICION"))
                        {
                            ActualizacionSuccess();

                        }
                        else
                        {
                            lblAlert.Text = "PROBLEMA AL INTENTAR ACTUALIZAR AMONESTACION";
                            alertValida.Visible = true;
                            alertSucces.Visible = false;

                        }
                    }
                    else
                    {
                        lblAlert.Text = "PROBLEMA AL INTENTAR ACTUALIZAR AMONESTACION";
                        alertValida.Visible = true;
                        alertSucces.Visible = false;

                    }
                }
                else
                {
                    ActualizacionSuccess();

                }

            }
            btnMostrar.Visible = true;
            btnActualizar.Visible = false;
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            HabilitarInabilitar(true);

            btnMostrar.Visible = true;
            btnActualizar.Visible = false;
            btnCancelar.Visible = true;
            btnEliminar.Visible = false;
            btnEditar.Visible = false;
        }
        protected void btnEliminar_Click1(object sender, EventArgs e)
        {

            int codigo = int.Parse(txtCodigo.Text);
            int tipoamon = int.Parse(ddltipo.SelectedValue.ToString());
            string tiposancion = ddltipoSancion.SelectedItem.ToString();
            DateTime fecha = Convert.ToDateTime(txtFechaIni.Text);
            string observacion = "";
            if (txtob.Text != "")
            {
                observacion = txtob.Text;
            }
            if (Neg_Amonestaciones.AmonestacionesDelete(codigo, tipoamon, tiposancion, fecha, Convert.ToDateTime(Session["fecharegistro"]), TimeSpan.Parse(Session["horaregistro"].ToString())))
            {
                if (Neg_Amonestaciones.AmonestacionesLOGInsertar(codigo, tipoamon, tipoamon, tiposancion, tiposancion, fecha, fecha, observacion, "ELIMINACION"))
                {

                    bool bycodigo = false, byrbRango = false;
                    int cod = 0;
                    DateTime fechaini = DateTime.Now, fechafin = DateTime.Now;

                    if (rbCodgio.SelectedValue == "1")
                    {
                        if (txtBuscar.Text != "")
                        {
                            bycodigo = true;
                            cod = int.Parse(txtBuscar.Text);
                        }

                    }

                    if (rbRango.SelectedValue == "1")
                    {
                        if (txtFechaIni2.Text != "" && txtFechaFin2.Text != "")
                        {
                            fechaini = Convert.ToDateTime(txtFechaIni2.Text);
                            fechafin = Convert.ToDateTime(txtFechaFin2.Text);
                            byrbRango = true;
                            if (fechaini > fechafin)
                            {
                                fechaini = DateTime.Now; fechafin = DateTime.Now;
                                byrbRango = false;
                            }


                        }

                    }


                    obteneramonestaciones(fechaini, fechafin, cod, bycodigo, byrbRango);
                    limpiarCampos();
                    HabilitarInabilitar(true);
                    btnMostrar.Visible = true;
                    btnActualizar.Visible = false;
                    btnCancelar.Visible = false;
                    btnEliminar.Visible = false;
                    btnEditar.Visible = false;
                    LblSuccess.Text = "AMONESTACION ELIMINADA";
                    alertValida.Visible = false;
                    alertSucces.Visible = true;

                }
                else
                {
                    lblAlert.Text = "PROBLEMA AL INTENTAR ELIMNIAR AMONESTACION";
                    alertValida.Visible = false;
                    alertSucces.Visible = true;

                }

            }
            else
            {
                lblAlert.Text = "PROBLEMA AL INTENTAR ELIMNIAR AMONESTACION";
                alertValida.Visible = false;
                alertSucces.Visible = true;

            }
        }
        protected void btnEditar_Click1(object sender, EventArgs e)
        {
            HabilitarInabilitar(true);
            btnActualizar.Visible = true;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            btnCancelar.Visible = true;
        }
        public void HabilitarInabilitar(bool estado)
        {
            txtCodigo.Enabled = estado;
            ddltipo.Enabled = estado;
            ddltipoSancion.Enabled = estado;
            txtFechaIni.Enabled = estado;
            txtob.Enabled = estado;

        }
        public void HabilitarInabilitarB(bool estado)
        {
            btnMostrar.Visible = estado;
            btnActualizar.Visible = estado;
            btnCancelar.Visible = estado;
            btnEliminar.Visible = !estado;
            btnEditar.Visible = !estado;

        }
        public void ActualizacionSuccess()
        {
            int cod = 0;
            if (txtBuscar.Text != "")
            {
                cod = int.Parse(txtBuscar.Text);
            }
            obteneramonestaciones(Convert.ToDateTime(txtFechaIni2.Text), Convert.ToDateTime(txtFechaFin2.Text), cod, true, true);
            limpiarCampos();
            HabilitarInabilitar(true);
            btnMostrar.Visible = true;
            btnActualizar.Visible = false;
            btnCancelar.Visible = true;
            btnEliminar.Visible = false;
            btnEditar.Visible = false;
            LblSuccess.Text = "AMONESTACION ACTUALIZADA";
            alertValida.Visible = false;
            alertSucces.Visible = true;
        }
        protected void rbRango_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbRango.SelectedValue == "1")
            {

                divfechaini.Visible = true;
                divfechafin.Visible = true;
                txtFechaIni2.Text = "";
                txtFechaFin2.Text = "";
                divconsultaAlert.Visible = false;

            }
            else
            {

                divfechaini.Visible = false;
                divfechafin.Visible = false;
            }
            btnBuscar.Focus();
        }

        protected void rbCodgio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbCodgio.SelectedValue == "1")
            {
                txtBuscar.Visible = true;
                txtBuscar.Text = "";
                lblcodigob.Visible = true;
                divconsultaAlert.Visible = false;


            }
            else
            {
                txtBuscar.Visible = false;
                lblcodigob.Visible = false;
            }

            btnBuscar.Focus();
            txtBuscar.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(vwadmin);
            DataTable dt = obtenerAmonestaciones();
            gvTiposAmonestacion.DataSource = dt;
            gvTiposAmonestacion.DataBind();
            Session["DtTiposAm"] = dt;

            btnACeptarAmon.Visible = true;
            btnCancelarAmon.Visible = true;
            btnActualizarAmon.Visible = false;

            CargarddllNivelFalta();

            divAmon1.Visible = false;
            divAmon2.Visible = false;

            limpiatVwTiposAmonest();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(vgeneral);
        }
        protected void Button9_Click(object sender, EventArgs e)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            bool bycodigo = false, byrbRango = false;
            int codigo = 0;
            DateTime fechaini = DateTime.Now, fechafin = DateTime.Now;

            if (rbCodgio.SelectedValue == "1")
            {
                if (txtBuscar.Text != "")
                {
                    bycodigo = true;
                    codigo = int.Parse(txtBuscar.Text);
                }

            }

            if (rbRango.SelectedValue == "1")
            {
                if (txtFechaIni2.Text != "" && txtFechaFin2.Text != "")
                {
                    fechaini = Convert.ToDateTime(txtFechaIni2.Text);
                    fechafin = Convert.ToDateTime(txtFechaFin2.Text);
                    byrbRango = true;
                    if (fechaini > fechafin)
                    {
                        fechaini = DateTime.Now; fechafin = DateTime.Now;
                        byrbRango = false;
                    }


                }

            }

            List<Neg_Amonestaciones.DetalleAmonestacion> la = Neg_Amonestaciones.getamonestacionesByRango(fechaini, fechafin, codigo, bycodigo, byrbRango,userDetail.getIDEmpresa());
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/AmonestacionesReport.rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", la);
            ReportViewer1.LocalReport.DataSources.Add(source);

            ReportViewer1.LocalReport.Refresh();
            MultiView1.SetActiveView(vwreport);

        }
        protected void Button10_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(vgeneral);
        }
        public void CargarddllNivelFalta()
        {

            DataTable dt = Neg_Amonestaciones.ObtenergrupoCatalogo(14);
            Session["DTNivelA"] = dt;
            ddlnivelF.DataSource = dt;
            ddlnivelF.DataBind();
        }

        protected void Button12_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(vgeneral);
        }
        protected void Button20_Click(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(vwadmin);
        }
        public DataTable obtenerAmonestaciones()
        {
            DataTable dt1 = Neg_Amonestaciones.ObtenerCatalogoAmonestaciones();
            Session["DTAmonestaciones"] = dt1;
            return dt1;

        }
        public DataTable ObtenerSancion()
        {
            DataTable dt = Neg_Amonestaciones.ObtenergrupoCatalogo(13);
            Session["DTipo"] = dt;
            return dt;
        }

        protected void btnACeptarAmon_Click(object sender, EventArgs e)
        {
            if (txtdetalleF.Text != "" && txtcodigoF.Text != "")
            {
                if (Neg_Amonestaciones.CatalogoAmonestacionesInsertar(txtdetalleF.Text, txtcodigoF.Text, ddlnivelF.SelectedItem.ToString(), ""))
                {
                    btnACeptarAmon.Visible = true;
                    btnCancelar.Visible = true;
                    btnActualizarAmon.Visible = false;
                    divAmon2.Visible = true;
                    divAmon1.Visible = false;
                    lblsuccesstAmon.Text = "TIPO AMONESTACION CREADA";
                    DataTable dt = obtenerAmonestaciones();
                    Session["DtTiposAm"] = dt;
                    filtrargvtiposA(txtcodigoF.Text);
                    limpiatVwTiposAmonest();
                }
                else
                {
                    divAmon1.Visible = true;
                    divAmon2.Visible = false;
                    lblalertAmon.Text = "PROBLEMA AL INTENTAR CREAR TIPO DE AMONESTACION";
                }

            }
            else
            {
                divAmon1.Visible = true;
                divAmon2.Visible = false;
                lblalertAmon.Text = "LLENE TODOS LOS CAMPOS";
            }
        }

        protected void gvTiposAmonestacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["DTNivelA"];
            Session["DTNivelA"] = dt;

            ddlnivelF.DataSource = dt;
            ddlnivelF.DataBind();

            ddlnivelF.SelectedValue = gvTiposAmonestacion.DataKeys[gvTiposAmonestacion.SelectedIndex].Values[1].ToString();
            this.txtdetalleF.Text = gvTiposAmonestacion.Rows[gvTiposAmonestacion.SelectedIndex].Cells[1].Text.Trim();
            this.txtcodigoF.Text = gvTiposAmonestacion.Rows[gvTiposAmonestacion.SelectedIndex].Cells[2].Text.Trim();

            btnACeptarAmon.Visible = false;
            btnActualizarAmon.Visible = true;
            btnCancelarAmon.Visible = true;
            HabilitarInhabilitarTiposSancion(true);
            divAmon1.Visible = false;
            divAmon2.Visible = false;
            Session["DetalleF"] = this.txtdetalleF.Text;
            Session["idA"] = gvTiposAmonestacion.DataKeys[gvTiposAmonestacion.SelectedIndex].Values[0].ToString();
        }

        public void HabilitarInhabilitarTiposSancion(bool estado)
        {
            txtdetalleF.Enabled = estado;
            txtcodigoF.Enabled = estado;
            ddlnivelF.Enabled = estado;
        }

        protected void btnEditarAmon_Click(object sender, EventArgs e)
        {
            HabilitarInhabilitarTiposSancion(true);
        }

        protected void btnCancelarAmon_Click(object sender, EventArgs e)
        {
            limpiatVwTiposAmonest();
            filtrargvtiposA("");
            divAmon1.Visible = false;
            divAmon2.Visible = false;
        }



        protected void Button17_Click(object sender, EventArgs e)
        {
            limpiatVwTiposAmonest();
            btnACeptarAmon.Visible = true;
            btnCancelarAmon.Visible = true;
            btnActualizar.Visible = false;
        }

        public void limpiatVwTiposAmonest()
        {
            txtcodigoF.Text = "";
            txtdetalleF.Text = "";
        }

        protected void btnActualizarAmon_Click(object sender, EventArgs e)
        {
            if (txtdetalleF.Text != "" && txtcodigoF.Text != "")
            {
                if (Neg_Amonestaciones.CatalogoAmonestacionesUpdate(txtdetalleF.Text, int.Parse(Session["idA"].ToString()), txtcodigoF.Text, ddlnivelF.SelectedItem.ToString(), ""))
                {
                    btnACeptarAmon.Visible = true;
                    btnCancelar.Visible = true;
                    btnActualizarAmon.Visible = false;
                    divAmon2.Visible = true;
                    divAmon1.Visible = false;
                    lblsuccesstAmon.Text = "TIPO AMONESTACION ACTUALIZADO";
                    DataTable dt = obtenerAmonestaciones();
                    gvTiposAmonestacion.DataSource = dt;
                    Session["DtTiposAm"] = dt;
                    filtrargvtiposA(txtcodigoF.Text);
                    limpiatVwTiposAmonest();
                }

                else
                {
                    divAmon1.Visible = true;
                    divAmon2.Visible = false;
                    lblalertAmon.Text = "PROBLEMA AL INTENTAR ACTUALIZAR TIPO DE AMONESTACION";
                }
            }
            else
            {
                divAmon1.Visible = true;
                divAmon2.Visible = false;
                lblalertAmon.Text = "LLENE TODOS LOS CAMPOS";
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            filtrargvtiposA(txtbuscarcodigo.Text);
            txtbuscarcodigo.Text = "";
        }

        public void filtrargvtiposA(string filtro)
        {
            DataTable dt = (DataTable)Session["DtTiposAm"];
            DataTable dt2 = dt.Copy();
            DataView dtv = dt2.DefaultView;
            if (filtro != "")
            {

                dtv.RowFilter = "Codigo='" + filtro + "'";
            }

            gvTiposAmonestacion.DataSource = dtv;
            gvTiposAmonestacion.DataBind();
            dt2.Clear();
        }

        protected void ddlnivelF_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            inicializarvistaSancion();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            inicializarvistaFalta();
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            inicializarvistaFalta();
        }



        #region METODOS FALTAS
        public void inicializarvistaFalta()
        {
            MultiView1.SetActiveView(vwFalta);
            btnAceptarFalta.Visible = true;
            btnActualizarFalta.Visible = false;
            btnCancelarFalta.Visible = true;
            divfaltaAlert.Visible = false;
            divfaltaSuccess.Visible = false;
            txtDetalleFalta.Text = "";


            gvFalta.DataSource = cargarGVGrupos(14, "");
            gvFalta.DataBind();
        }
        public void inicializarvistaSancion()
        {
            MultiView1.SetActiveView(vwSancion);
            btnAceptarSancion.Visible = true;
            btnActualizarSancion.Visible = false;
            btnCancelarSancion.Visible = true;
            divsancionAlert.Visible = false;
            divsancionSuccess.Visible = false;

            gvtiposSancion.DataSource = cargarGVGrupos(13, "");
            gvtiposSancion.DataBind();

            txtDetallesancion.Text = "";


        }

        public bool InsertarActualizar(int accion, int idgrupo, string txt, int iddes)
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            TextBox txtbox = (TextBox)cph.FindControl(txt);
            //INSERTAR
            if (accion == 1)
            {
                if (Neg_Amonestaciones.CatalogoInsert(idgrupo, txtbox.Text))
                {
                    return true;
                }
                else
                    return false;
            }
            //ACTUALIZAR
            else
            {
                //  int iddescripcion = int.Parse(ddlist.SelectedValue);
                if (Neg_Amonestaciones.updategrupoCatalogo(idgrupo, txtbox.Text, iddes))
                {
                    return true;
                }
                else
                    return false;
            }



        }
        #endregion
        #region EVENTOS FALTAS
        protected void btnAceptarFalta_Click(object sender, EventArgs e)
        {
            if (txtDetalleFalta.Text != "")
            {
                int id = Convert.ToInt32(Session["IdDescripcion"]);
                if (InsertarActualizar(1, 14, "txtDetalleFalta", id))
                {

                    mensaje("lblFaltasuccess", "TIPO DE FALTA CREADA", 1, "divfaltaSuccess");
                    btnAceptarFalta.Visible = true;
                    btnActualizarFalta.Visible = false;
                    btnCancelarFalta.Visible = true;
                    gvFalta.DataSource = cargarGVGrupos(14, "");
                    gvFalta.DataBind();
                    txtDetalleFalta.Text = "";
                }

                else
                {
                    mensaje("lblFaltaalert", "ERROR AL INTENTAR CREAR TIPO DE FALTA ", 1, "divfaltaAlert");
                }

            }
            else
            {
                mensaje("lblFaltaalert", "LLENE TODOS LOS CAMPOS", 1, "divfaltaAlert");
            }
        }
        #endregion


        public void mensaje(string txt, string mensaje, int tipo, string div)
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            Control micontrol = cph.FindControl(div);
            Label txtbox = (Label)cph.FindControl(txt);

            txtbox.Font.Bold = true;
            txtbox.Text = mensaje;

            micontrol.Visible = true;

        }
        public DataTable cargarGVGrupos(int grupo, string filtro)
        {
            DataTable dt = Neg_Amonestaciones.ObtenergrupoCatalogo(grupo);

            DataTable dt2 = dt.Copy();
            DataView dtv = dt2.DefaultView;
            if (filtro != "")
            {
                dtv.RowFilter = "Descripcion='" + filtro + "'";
                dt2 = dtv.ToTable();
            }

            return dt2;
        }


        protected void btnAceptarSancion_Click(object sender, EventArgs e)
        {
            if (txtDetallesancion.Text != "")
            {
                if (InsertarActualizar(1, 13, "txtDetallesancion", 0))
                {
                    mensaje("LabelSuccessSancion", "TIPO DE SANCION CREADA", 1, "divsancionSuccess");
                    btnAceptarSancion.Visible = true;
                    btnActualizarSancion.Visible = false;
                    btnCancelarSancion.Visible = true;
                    gvtiposSancion.DataSource = cargarGVGrupos(13, "");
                    gvtiposSancion.DataBind();
                    txtDetallesancion.Text = "";

                }
                else
                {
                    mensaje("LabelALertSancion", "ERROR AL INTENTAR CREAR TIPO DE SANCION ", 2, "divsancionAlert");
                }

            }
            else
            {
                mensaje("LabelALertSancion", "LLENE TODOS LOS CAMPOS", 2, "divsancionAlert");
            }
        }

        protected void btnActualizarSancion_Click(object sender, EventArgs e)
        {
            if (txtDetallesancion.Text != "")
            {
                int id = Convert.ToInt32(Session["IdDescripcion"]);

                if (InsertarActualizar(2, 13, "txtDetallesancion", id))
                {
                    mensaje("LabelSuccessSancion", "TIPO DE SANCION ACTUALIZADO", 1, "divsancionSuccess");
                    btnAceptarSancion.Visible = true;
                    btnActualizarSancion.Visible = false;
                    btnCancelarSancion.Visible = true;
                    gvtiposSancion.DataSource = cargarGVGrupos(13, "");
                    gvtiposSancion.DataBind();
                    txtDetallesancion.Text = "";
                }
                else
                {
                    mensaje("LabelALertSancion", "ERROR AL INTENTAR ACTUALIZAR TIPO DE SANCION ", 1, "divsancionAlert");
                }
            }
            else
            {
                mensaje("LabelALertSancion", "LLENE TODOS LOS CAMPOS ", 1, "divsancionAlert");
            }
        }

        protected void btnCancelarSancion_Click(object sender, EventArgs e)
        {
            cargarGVGrupos(13, "");
            btnActualizarSancion.Visible = false;
            btnAceptarSancion.Visible = true;

            divsancionAlert.Visible = false;
            divsancionSuccess.Visible = false;
        }

        protected void Button20_Click1(object sender, EventArgs e)
        {
            MultiView1.SetActiveView(vgeneral);
        }

        protected void btnActualizarFalta_Click(object sender, EventArgs e)
        {
            if (txtDetalleFalta.Text != "")
            {
                int id = Convert.ToInt32(Session["IdDescripcion"]);
                if (InsertarActualizar(2, 14, "txtDetalleFalta", id))
                {

                    mensaje("lblFaltasuccess", "TIPO DE SANCION ACTUALIZADA", 1, "divfaltaSuccess");
                    btnAceptarFalta.Visible = true;
                    btnActualizarFalta.Visible = false;
                    btnCancelarFalta.Visible = true;
                    gvFalta.DataSource = cargarGVGrupos(14, "");
                    gvFalta.DataBind();
                    txtDetalleFalta.Text = "";
                }

                else
                {
                    mensaje("lblFaltaalert", "ERROR AL INTENTAR ACTUALIZAR TIPO DE SANCION", 1, "divfaltaAlert");
                }

            }
            else
            {
                mensaje("lblFaltaalert", "LLENES TODOS LOS CAMPOS", 1, "divfaltaAlert");
            }
        }

        protected void btnCancelarFalta_Click(object sender, EventArgs e)
        {
            cargarGVGrupos(14, "");
            txtDetalleFalta.Text = "";

            divfaltaAlert.Visible = false;
            divfaltaSuccess.Visible = false;
        }

        protected void Button8_Click(object sender, EventArgs e)
        {
            inicializarvistaSancion();
        }

        protected void gvtiposSancion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(gvtiposSancion.DataKeys[gvtiposSancion.SelectedIndex].Values[0].ToString());
            Session["IdDescripcion"] = id;
            this.txtDetallesancion.Text = gvtiposSancion.Rows[gvtiposSancion.SelectedIndex].Cells[1].Text.Trim();
            btnAceptarSancion.Visible = false;
            btnActualizarSancion.Visible = true;
            btnCancelarSancion.Visible = true;

            divsancionAlert.Visible = false;
            divsancionSuccess.Visible = false;
        }

        protected void gvFalta_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDetalleFalta.Text = gvFalta.Rows[gvFalta.SelectedIndex].Cells[1].Text.Trim();
            int id = Convert.ToInt32(gvFalta.DataKeys[gvFalta.SelectedIndex].Values[0].ToString());
            Session["IdDescripcion"] = id;
            btnAceptarFalta.Visible = false;
            btnActualizarFalta.Visible = true;
            btnCancelarFalta.Visible = true;
            divfaltaAlert.Visible = false;
            divfaltaSuccess.Visible = false;
        }

        protected void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            DataTable DetEmpleados = new DataTable();
            DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleadoxNombre(txtBuscarNombre.Text.Trim());

            if (DetEmpleados != null)
            {
                if (DetEmpleados.Rows.Count > 0)
                {
                    txtCodigo.Text = DetEmpleados.Rows[0]["codigo_empleado"].ToString();
                }
            }
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            DataTable DetEmpleados = new DataTable();
            DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtCodigo.Text);
            if (DetEmpleados != null)
            {
                if (DetEmpleados.Rows.Count > 0)
                {
                    txtBuscarNombre.Text = DetEmpleados.Rows[0]["nombrecompleto"].ToString();
                }
            }
        }

    }
}