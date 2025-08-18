using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;


using Negocios;

namespace NominaRRHH.Presentacion
{
    public partial class ConfigTurnos : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Turnos ngt = new Neg_Turnos();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                alertSucces.Visible = false;
                alertValida.Visible = false;
                obtenerTurnos();
                plnDetalle.Visible = false;
                plnDetalleEditar.Visible = false;
                plnEditarT.Visible = false;
                Session["Tipo"] = "";
                Session["TipoDetalle"] = "";
                ddldias.DataSource = Tabladias();
                ddldias.DataBind();
            }
        }
        #region METODOS
        public bool validarDia(int dia, string nombredia, int id)
        {
            if (Session["TipoDetalle"].ToString() == "NUEVO")
            {
                return ngt.ExisteDia(id, dia);
            }
            //ESTO SI ES UNA ACTUALIZACION
            else
            {
                //SI NO SE HA CAMBIADO EL DIA

                string textoOriginal = HttpUtility.HtmlDecode(Session["NOMBRE_DIA"].ToString().Trim().ToUpper());

                //transformación UNICODE
                string textoNormalizado = textoOriginal.Normalize(NormalizationForm.FormD);




                //coincide todo lo que no sean letras y números ascii o espacio
                //y lo reemplazamos por una cadena vacía.
                Regex reg = new Regex("[^a-zA-Z0-9 ]");
                string textoSinAcentos = reg.Replace(textoNormalizado, "");

                if (textoSinAcentos.Trim().ToUpper() != HttpUtility.HtmlDecode(nombredia.Trim().ToUpper()))
                {
                    return ngt.ExisteDia(id, dia);
                }
                //NO SE HIZO UN CAMBIO EN EL DIA
                else
                {
                    return true;

                }

            }

        }

        public void guardarDia()
        {
            string diasemana = "";
            int diasemanaN = 0;
            int horaAlmuerzo = 0;
            int id = Convert.ToInt32(Session["id"].ToString());
            if (validarNuevoTurnoDetalle())
            {
                diasemana = ddldias.SelectedItem.ToString();
                diasemanaN = int.Parse(ddldias.SelectedValue.ToString());
                //string currentDateString = DateTime.Now.ToString("dd-MMM-yyyy h:mm tt");
                //DateTime currentDate = Convert.ToDateTime(currentDateString);
                if (ngt.validarhora(txthoraI.Text, txtHoraF.Text))
                {
                    DateTime horaIni = Convert.ToDateTime(txthoraI.Text);
                    DateTime horaFin = Convert.ToDateTime(txtHoraF.Text);

                    if (validarDia(diasemanaN, diasemana, id))
                    {
                        horaAlmuerzo = int.Parse(txtAlmuerzo.Text);

                        if (Session["TipoDetalle"].ToString() == "NUEVO")
                        {
                            if (ngt.PlnTurnosDiasinsert(id, diasemanaN, horaIni, horaFin, horaAlmuerzo))
                            {
                                Mensaje("TURNO CREADO", 1, true);
                            }
                            else
                            {
                                Mensaje("ERROR AL INGRESAR TURNO", 2, true);
                            }
                        }
                        else
                        {
                            if (ngt.PlnTurnosDiasUpdate(id, diasemanaN, horaIni, horaFin, horaAlmuerzo))
                            {
                                Mensaje("TURNO ACTUALIZADO", 1, true);
                            }
                            else
                            {
                                Mensaje("ERROR AL ACTUALIZAR TURNO", 2, true);
                            }

                        }
                        plnDetalleEditar.Visible = false;
                        LimpiarCamposDetalle();
                    }
                    else
                    {
                        Mensaje("DIA YA EXISTE PARA EL TURNO", 2, true);
                        plnDetalleEditar.Visible = true;
                    }
                    //}
                    //else
                    //{
                    //    Mensaje("HORA DE SALIDA NO PUEDE SER MENOR", 2, true);
                    //    plnDetalleEditar.Visible = true;
                    //}
                }
                else
                {
                    Mensaje("FORMATO DE HORA INCORRECTO", 2, true);
                    plnDetalleEditar.Visible = true;

                }

            }
            else
            {
                Mensaje("DEBE LLENAR TODOS LOS CAMPOS", 2, true);
                plnDetalleEditar.Visible = true;
            }
            ObtenerDetalle(id);

        }
        public void guardarTurno()
        {
            string nombreturno = "";
            int minComodin = 0;
            int totalH = 0;
            decimal hrsTurno = 0;

            if (Session["Tipo"].ToString() == "NUEVO")
            {
                if (validarNuevoTurno())
                {
                    nombreturno = txtNombTurno.Text;
                    minComodin = int.Parse(txtMinComodin.Text);
                    totalH = int.Parse(txtTotalHoras.Text);
                    hrsTurno = decimal.Parse(TxtHrsTurno.Text);
                    if (ngt.TurnosInsert(nombreturno, hrsTurno,minComodin, 1, totalH,ChkConsolidar.Checked))
                    {
                        Mensaje("TURNO CREADO", 1, true);
                    }
                    else
                    {
                        Mensaje("ERROR AL CREAR TURNO, INTENTE NUEVAMENTE", 2, true);
                    }
                }
                else
                {
                    Mensaje("DEBE LLENAR TODOS LOS CAMPOS", 2, true);
                }
            }

            else
            {
                int id = Convert.ToInt32(Session["id"].ToString());
                if (validarNuevoTurno())
                {
                    nombreturno = txtNombTurno.Text;
                    minComodin = int.Parse(txtMinComodin.Text);
                    totalH = int.Parse(txtTotalHoras.Text);
                    hrsTurno = decimal.Parse(TxtHrsTurno.Text);
                    if (ngt.PlnTurnosUpdate(nombreturno,hrsTurno, id, minComodin, 1, totalH,ChkConsolidar.Checked))
                    {
                        Mensaje("TURNO ACTUALIZADO", 1, true);
                    }
                    else
                    {
                        Mensaje("ERROR AL ACTUALIZAR TURNO, INTENTE DE NUEVO", 2, true);
                    }
                }
                else
                {
                    Mensaje("DEBE LLENAR TODOS LOS CAMPOS", 2, true);
                }

            }
            obtenerTurnos();
            plnEditarT.Visible = false;
        }
        public DataTable Tabladias()
        {
            DataTable dt = new DataTable();//se crea el dt con los campos semejantes a los del gv
            dt.Columns.Add("numero");
            dt.Columns.Add("Nombre");

            dt.Rows.Add(1, "Lunes");
            dt.Rows.Add(2, "Martes");
            dt.Rows.Add(3, "Miercoles");
            dt.Rows.Add(4, "Jueves");
            dt.Rows.Add(5, "Viernes");
            dt.Rows.Add(6, "Sabado");
            dt.Rows.Add(7, "Domingo");
            return dt;
        }
        public DataTable TablaTurnosEnBlanco()
        {
            DataTable dt = new DataTable();//se crea el dt con los campos semejantes a los del gv
            dt.Columns.Add("nombreturno");
            dt.Columns.Add("bonus");
            dt.Columns.Add("tipo");
            dt.Columns.Add("horaseptimo");
            dt.Columns.Add("horasturno");
            dt.Columns.Add("consolidar");

            return dt;
        }
        public DataTable TablaDetalleEnBlanco()
        {
            DataTable dt = new DataTable();//se crea el dt con los campos semejantes a los del gv
            dt.Columns.Add("idturno");
            dt.Columns.Add("nombreturno");
            dt.Columns.Add("diasemana");
            dt.Columns.Add("horaini");
            dt.Columns.Add("horafin");
            dt.Columns.Add("almuerzo");

            return dt;
        }
        public void obtenerTurnos()
        {
            DataTable dtd = ngt.obtenerTodosTurnos();
            if (dtd != null && dtd.Rows.Count > 0)
            {
                GVTURNOS.DataSource = dtd;
                GVTURNOS.DataBind();
            }
            else
            {
                GVTURNOS.DataSource = TablaTurnosEnBlanco();
                GVTURNOS.DataBind();
            }

        }
        public void ObtenerDetalle(int id)
        {
            CultureInfo ci = new CultureInfo("Es-Es");
            //CultureInfo ci = new CultureInfo("en-US");
            DataTable dtd = ngt.PlnTurnosDiasSelect(id);
            plnDetalle.Visible = true;
            if (dtd != null && dtd.Rows.Count > 0)
            {
                dtd.Columns.Add("NombreDia");
                foreach (DataRow item in dtd.Rows)
                {
                    if (item[2].ToString()=="7")//domingo
                    {
                        item[6] = "Domingo";
                    }
                    else
                    {
                        item[6] = ci.DateTimeFormat.GetDayName((DayOfWeek)Enum.Parse(typeof(DayOfWeek), item[2].ToString()));
                    }
                    
                }
                gvdetalle.DataSource = dtd;
                gvdetalle.DataBind();
            }
            else
            {
                gvdetalle.DataSource = TablaDetalleEnBlanco();
                gvdetalle.DataBind();
            }
        }
        public void LimpiarCampos()
        {
            txtNombTurno.Text = "";
            txtMinComodin.Text = "";
            txtTotalHoras.Text = "";
            TxtHrsTurno.Text = "";
            ChkConsolidar.Checked = false;
        }
        public void LimpiarCamposDetalle()
        {
            txthoraI.Text = "";
            txtHoraF.Text = "";
            txtAlmuerzo.Text = "";
        }
        public bool validarNuevoTurno()
        {
            int c = 0;
            if (txtNombTurno.Text == "")
                c = c + 1;
            if (txtMinComodin.Text == "")
                c = c + 1;
            if (txtTotalHoras.Text == "")
                c = c + 1;
            if (TxtHrsTurno.Text == "")
                c = c + 1;

            if (c > 0)
            { return false; }
            else return true;

        }
        public bool validarNuevoTurnoDetalle()
        {
            int c = 0;
            if (txthoraI.Text == "")
                c = c + 1;
            if (txtHoraF.Text == "")
                c = c + 1;
            if (txtAlmuerzo.Text == "")
                c = c + 1;

            if (c > 0)
            { return false; }
            else return true;

        }
        public void MostrarDatosTurno(int index)
        {
            LimpiarCampos();
            txtNombTurno.Text = GVTURNOS.Rows[index].Cells[0].Text;
            txtMinComodin.Text = GVTURNOS.Rows[index].Cells[1].Text;
            txtTotalHoras.Text = GVTURNOS.Rows[index].Cells[3].Text;
            TxtHrsTurno.Text = GVTURNOS.Rows[index].Cells[4].Text;

            GridViewRow selectedRow = GVTURNOS.Rows[index];           
            ChkConsolidar.Checked = ((CheckBox)selectedRow.FindControl("chkConsolidar")).Checked;
        }
        public void MostrarDatosTurnoDetalle(int index)
        {
            LimpiarCamposDetalle();

            ddldias.SelectedValue = gvdetalle.DataKeys[index].Values[1].ToString();
            txthoraI.Text = gvdetalle.Rows[index].Cells[2].Text;
            txtHoraF.Text = gvdetalle.Rows[index].Cells[3].Text;
            txtAlmuerzo.Text = gvdetalle.Rows[index].Cells[4].Text;
            plnDetalleEditar.Visible = true;
        }
        public void Eliminarturno(int id)
        {
            if (ngt.PlnTurnosDelete(id))
            {
                if (ngt.PlnTurnosDiasDeleteAll(id))
                {
                    obtenerTurnos();
                    Mensaje("TURNO ELIMINADO", 1, true);
                }

            }
            else
            {
                Mensaje("ERROR AL ELIMINAR TURNO", 2, true);

            }

        }

        #endregion

        #region EVENTOS
        protected void LbD1_Click(object sender, EventArgs e)
        {
            Session["TipoDetalle"] = "EDITAR";
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());

            int id = Convert.ToInt32(gvdetalle.DataKeys[index].Values[0].ToString());
            MostrarDatosTurnoDetalle(index);
            Mensaje("TURNO CREADO", 1, false);
            Session["NOMBRE_DIA"] = gvdetalle.Rows[index].Cells[1].Text;

        }
        protected void LbD2_Click(object sender, EventArgs e)
        {
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());

            int diasemana = Convert.ToInt32(gvdetalle.DataKeys[index].Values[1].ToString());
            int idT = int.Parse(Session["id"].ToString());

            if (ngt.PlnTurnosDiasDelete(idT, diasemana))
            {

                obtenerTurnos();
                Mensaje("DIA ELIMINADO", 1, true);
                ObtenerDetalle(idT);
            }
            else
            {
                Mensaje("ERROR AL ELIMINAR DIA", 2, true);

            }
            plnDetalleEditar.Visible = false;
            Mensaje("", 2, false);

        }
        protected void btnGuardarDetalle_Click(object sender, EventArgs e)
        {
            guardarDia();
            gvdetalle.Focus();
        }
        protected void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            LimpiarCamposDetalle();
            plnDetalleEditar.Visible = false;
            Mensaje("", 1, false);
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            plnEditarT.Visible = true;
            LimpiarCampos();
            Session["Tipo"] = "NUEVO";
            plnDetalle.Visible = false;
            plnDetalleEditar.Visible = false;
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarCamposDetalle();
            Session["TipoDetalle"] = "NUEVO";
            plnDetalleEditar.Visible = true;
            Mensaje("", 1, false);

        }
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
        protected void LbT1_Click(object sender, EventArgs e)
        {
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());
            int id = Convert.ToInt32(GVTURNOS.DataKeys[index].Values[0].ToString());
            string turno = GVTURNOS.Rows[index].Cells[0].Text;
            Session["id"] = id;
            LimpiarCampos();
            ObtenerDetalle(id);            
            plnEditarT.Visible = false;
            Mensaje("", 1, false);
            lblDetalle.Text = "DETALLE DE TURNO " + turno.ToUpper();
        }
        protected void LbT2_Click(object sender, EventArgs e)
        {
            Session["Tipo"] = "EDITAR";
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());

            int id = Convert.ToInt32(GVTURNOS.DataKeys[index].Values[0].ToString());
            Session["id"] = id;
            LimpiarCampos();
            LimpiarCamposDetalle();
            MostrarDatosTurno(index);
            plnEditarT.Visible = true;
            plnDetalle.Visible = false;
            
        }
        protected void lbT3_Click(object sender, EventArgs e)
        {
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());
            int id = Convert.ToInt32(GVTURNOS.DataKeys[index].Values[0].ToString());
            Eliminarturno(id);
            plnEditarT.Visible = false;
            plnDetalle.Visible = false;
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            guardarTurno();
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            plnEditarT.Visible = false;
            plnDetalle.Visible = false;
            plnDetalleEditar.Visible = false;
        }






        #endregion

        protected void GVTURNOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVTURNOS.PageIndex = e.NewPageIndex;
            GVTURNOS.DataBind();
            obtenerTurnos();
        }
    }
}