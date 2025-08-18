using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using System.Data;
using Datos;
namespace NominaRRHH.Presentacion
{
    public partial class ConfigParametros : System.Web.UI.Page
    {
           Dictionary<string, int> dictionary = new Dictionary<string, int>();
         
        #region REFERENCIAS
        Neg_Parametros Neg_Parametros = new Neg_Parametros();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                alertSucces.Visible = false;
                alertValida.Visible = false;
                plnDetalle.Visible = false;
                plnDetalleEditar.Visible = false;
                plnEditarT.Visible = false;
                Session["Tipo"] = "";
                Session["TipoDetalle"] = "";
                Llenargrid("Grupo");
                Session["idGrupo"] = "";
                Session["idParametro"] = "";
              
            }
        }
        #region METODOS

        public void Llenargrid(string tipo)
        {
            DataTable dtd = new DataTable();
            if (tipo == "Grupo")
            {
                dtd = Neg_Parametros.ParametrosGroupSelect();
                if (dtd != null && dtd.Rows.Count > 0)
                {

                    gvGrupo.DataSource = dtd;
                    gvGrupo.DataBind();
                }
                else
                {
                    gvGrupo.DataSource = TablaDetalleEnBlanco();
                    gvGrupo.DataBind();
                }
            }
            else if (tipo == "Parametro")
            {
                int idgrupo = 0;
                if (Session["idGrupo"].ToString() != "")
                {
                    idgrupo = Convert.ToInt32(Session["idGrupo"].ToString());
                }

                dtd = Neg_Parametros.ParametrosSelect(idgrupo);
                plnDetalle.Visible = true;
                if (dtd != null && dtd.Rows.Count > 0)
                {
                    gvdetalle.DataSource = dtd;
                    gvdetalle.DataBind();
                }
                else
                {
                    gvdetalle.DataSource = TablaGrupoEnBlanco();
                    gvdetalle.DataBind();
                }
            }
        }
        public void LimpiarCampos(string tipo)
        {
            if (tipo == "Grupo")
            {
                txtNombreGrupo.Text = string.Empty;
                txtDescripcionGrupo.Text = string.Empty;
            }
            else if (tipo == "Parametro")
            {
                txtNombreParametro.Text = string.Empty;
                txtDescripcionP.Text = string.Empty;
                txtvalorP.Text = string.Empty;
            }
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

        public void guardar(string tipo, int idgrupo, int idParametro)
        {
            string MENSAJE = "";
            int tipoM = 0;
            if (Validacion(tipo))
            {
                
                if (tipo == "Grupo")
                {

                    if (Session["ACCION"].ToString() == "NUEVO")
                    {
                        if (Neg_Parametros.ParametrosGrupoInsert(txtNombreGrupo.Text, txtDescripcionGrupo.Text))
                        {
                            MENSAJE = "NUEVO GRUPO CREADO SATISFACTORIAMENTE";
                            tipoM = 1;
                        }
                        else
                        {
                            MENSAJE = "OCURRIO UN PROBLEMA AL CREAR NUEVO GRUPO, INTENTE NUEVAMENTE";
                            tipoM = 2;
                        }

                    }
                    else if (Session["ACCION"].ToString() == "EDITAR")
                    {
                        if (Neg_Parametros.ParametrosGrupoUpdate(idgrupo, txtNombreGrupo.Text,txtDescripcionGrupo.Text))
                        {
                            MENSAJE = "DATOS DEL GRUPO ACTUALIZADOS SATISFACTORIAMENTE";
                            tipoM = 1;
                        }
                        else
                        {
                            MENSAJE = "OCURRIO UN PROBLEMA AL ACTUALIZAR DATOS DEL GRUPO, INTENTE NUEVAMENTE";
                            tipoM = 2;
                        }
                    }

                }
                else if (tipo == "Parametro")
                {
                    decimal valorp = decimal.Parse(txtvalorP.Text);
                    if (Session["ACCION"].ToString() == "NUEVO")
                    {
                        if (Neg_Parametros.ParametrosInsert(idgrupo, txtNombreParametro.Text,valorp, txtDescripcionP.Text))
                        {
                            MENSAJE = "NUEVO PARAMETRO CREADO SATISFACTORIAMENTE";
                            tipoM = 1;

                        }
                        else
                        {
                            MENSAJE = "OCURRIO UN PROBLEMA AL CREAR PARAMETRO, INTENTE NUEVAMENTE";
                            tipoM = 2;
                        }

                    }
                    else if (Session["ACCION"].ToString() == "EDITAR")
                    {
                        if (Neg_Parametros.ParametrosUpdate(idParametro, idgrupo, txtNombreParametro.Text,valorp, txtDescripcionP.Text))
                        {
                            MENSAJE = "DATOS DEL PARAMETRO ACTUALIZADO SATISFACTORIAMENTE";
                            tipoM = 1;
                        }
                        else
                        {
                            MENSAJE = "OCURRIO UN PROBLEMA AL ACTUALIZAR DATOS DEL PARAMETRO, INTENTE NUEVAMENTE";
                            tipoM = 2;
                        }
                    }

                }
            }
            else
            {
                MENSAJE = "DEBE LLENAR TODOS LOS CAMPOS";
                tipoM = 2;
            }

            Mensaje(MENSAJE, tipoM, true);
            Llenargrid(tipo);
            plnDetalleEditar.Visible = false;
            plnEditarT.Visible = false;
        }
        public bool Validacion(string tipo)
        {
            int contador = 0;
            if (tipo == "Grupo")
            {
                if (txtNombreGrupo.Text == "")
                {
                    contador = contador + 1;
                }
                if (txtDescripcionGrupo.Text == "")
                {
                    contador = contador + 1;
                }
            }
            else if (tipo == "Parametro")
            {
                if (txtNombreParametro.Text == "")
                { contador = contador + 1; }
                else if (txtDescripcionP.Text == "")
                { contador = contador + 1; }
                else if (txtvalorP.Text == "")
                { contador = contador + 1; }
            }

            if (contador > 0)
                return false;

            else return true;
        }
        public DataTable TablaGrupoEnBlanco()
        {
            DataTable dt = new DataTable();//se crea el dt con los campos semejantes a los del gv
            dt.Columns.Add("nombre");
            dt.Columns.Add("Descripcion");

            return dt;
        }
        public DataTable TablaDetalleEnBlanco()
        {
            DataTable dt = new DataTable();//se crea el dt con los campos semejantes a los del gv
            dt.Columns.Add("nombre");
            dt.Columns.Add("valor");
            dt.Columns.Add("descripcion");

            return dt;
        }
        #endregion

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            int idgrupo = 0;
            if (Session["idGrupo"].ToString() != "")
            {
                idgrupo = Convert.ToInt32(Session["idGrupo"].ToString());
            }

            guardar("Grupo", idgrupo, 0);
            plnEditarT.Visible = false;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            LimpiarCampos("Grupo");
            plnEditarT.Visible = false;
            Mensaje("", 1, false);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            plnEditarT.Visible = true;
            LimpiarCampos("Grupo");
            Session["ACCION"] = "NUEVO";
            plnDetalle.Visible = false;
            plnDetalleEditar.Visible = false;
            Mensaje("", 1, false);
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarCampos("Parametro");
            Session["ACCION"] = "NUEVO";
            plnDetalleEditar.Visible = true;
            Mensaje("", 2, false);
        }

        protected void btnGuardarDetalle_Click(object sender, EventArgs e)
        {
            int idgrupo = 0, idpa = 0;
            if (Session["idGrupo"].ToString() != "")
            {
                idgrupo = Convert.ToInt32(Session["idGrupo"].ToString());
            }
            if (Session["idParametro"].ToString() != "")
            {
                idpa = Convert.ToInt32(Session["idParametro"].ToString());
            }

            guardar("Parametro", idgrupo, idpa);
        }

        protected void btnEliminarDetalle_Click(object sender, EventArgs e)
        {
            LimpiarCampos("Parametro");
            plnDetalleEditar.Visible = false;
            Mensaje("", 1, false);
        }

        protected void lblDetalleG_Click(object sender, EventArgs e)
        {
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());
            int id = Convert.ToInt32(gvGrupo.DataKeys[index].Values[0].ToString());
            string grupo = gvGrupo.Rows[index].Cells[0].Text;
            Session["idGrupo"] = id;
            Llenargrid("Parametro");
            LimpiarCampos("Grupo");
            plnEditarT.Visible = false;
            Mensaje("", 1, false);
            lblDetalle.Text = "DETALLE DEL GRUPO " + grupo.ToUpper();
        }

        protected void lblEditarGrupo_Click(object sender, EventArgs e)
        {
            Session["ACCION"] = "EDITAR";
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());
            int id = Convert.ToInt32(gvGrupo.DataKeys[index].Values[0].ToString());
            Session["idGrupo"] = id;
            plnEditarT.Visible = true;
            plnDetalle.Visible = false;
            LimpiarCampos("Grupo");
            txtNombreGrupo.Text = gvGrupo.Rows[index].Cells[0].Text;
            txtDescripcionGrupo.Text = gvGrupo.Rows[index].Cells[1].Text;
        }
        protected void lblEditarPa_Click(object sender, EventArgs e)
        {
            Session["ACCION"] = "EDITAR";
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());
            int id = Convert.ToInt32(gvdetalle.DataKeys[index].Values[1].ToString());
            Session["idParametro"] = id;
            int idgrupo = Convert.ToInt32(gvdetalle.DataKeys[index].Values[0].ToString());
            plnEditarT.Visible = false;
            plnDetalle.Visible = true;
            plnDetalleEditar.Visible = true;
            txtNombreParametro.Text = gvdetalle.Rows[index].Cells[0].Text;
            txtDescripcionP.Text = gvdetalle.Rows[index].Cells[2].Text;
            txtvalorP.Text = gvdetalle.Rows[index].Cells[1].Text;
        }
        protected void lblEliminarG_Click(object sender, EventArgs e)
        {
            LimpiarCampos("Grupo");
            plnDetalleEditar.Visible = false;
            plnDetalle.Visible = false;
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());
            int id = Convert.ToInt32(gvGrupo.DataKeys[index].Values[0].ToString());
            Session["idGrupo"] = id;
            if (Neg_Parametros.ParametrosGrupoDelete(id))
            {
                Mensaje("GRUPO Y SUS PARAMETROS ELIMINADOS", 1, true);

            }
            else
            {
                Mensaje("GRUPO Y SUS PARAMETROS ELIMINADOS", 1, false);
            }
            Llenargrid("Grupo");

        }



        protected void lblEliminarP_Click(object sender, EventArgs e)
        {
            LimpiarCampos("Parametro");
            plnDetalleEditar.Visible = false;
            LinkButton btneditar = (LinkButton)sender;
            int index = Convert.ToInt32(btneditar.CommandArgument.ToString());
            int id = Convert.ToInt32(gvdetalle.DataKeys[index].Values[1].ToString());
            Session["idParametro"] = id;
            int idgrupo = Convert.ToInt32(gvdetalle.DataKeys[index].Values[0].ToString());
            plnEditarT.Visible = false;

            plnDetalle.Visible = true;
            plnDetalleEditar.Visible = false;

            if (Neg_Parametros.ParametrosDelete(id, idgrupo))
            {
                Mensaje("PARAMETRO ELIMINADO", 1, true);

            }
            else
            {
                Mensaje("OCURRIO UN PROBLEMA INTENTANDO ELIMINAR EL PARAMETRO, INTENTE DE NUEVO", 1, false);
            }
            Llenargrid("Parametro");


        }

        protected void btbOcultar_Click(object sender, EventArgs e)
        {
            plnDetalle.Visible = false;
            plnDetalleEditar.Visible = false;
            
        }

    }
}