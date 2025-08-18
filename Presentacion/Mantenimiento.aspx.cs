using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class Mantenimiento : System.Web.UI.Page
    {
        Negocios.Neg_Menu menup = new Negocios.Neg_Menu();
        Negocios.Neg_Perfil np = new Negocios.Neg_Perfil();
        Negocios.Neg_UsuarioPerfil nu = new Negocios.Neg_UsuarioPerfil();
        Negocios.Neg_Usuario ngu = new Negocios.Neg_Usuario();
        Negocios.Neg_MenuPerfil nmp = new Negocios.Neg_MenuPerfil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Header.DataBind();
                //SE LE AGREGA AL CONTROL TREVIEW EL EVENTO EN JAVASCRIPT QUE SIRVE
                //PARA EL CHECKEO DE LOS ITEMS
                tvOpcionesR.Attributes.Add("onclick", "OnTreeClick(event)");
                //CARGA DE DATOS EN GRIDVIEWS
                CargargvUser();
                CargargvPerfil();
                //SE ACTIVA LA PRIMERA VISTA
                mvPermisos.SetActiveView(wvUsuario);

            }
        }

        #region Métodos
        //---------------------METODOS PARA OPERACIONES SOBRE USUARIOS
        /// <summary>
        /// Método para la paginación del gridview que contiene los datos de los usuarios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoPageu(object sender, System.EventArgs e)
        {
            DropDownList otraPag = (DropDownList)sender;

            int iNumPag = 0;
            if (int.TryParse(otraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= gvUser.PageCount)
            {
                if (int.TryParse(otraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= gvUser.PageCount)
                {
                    gvUser.PageIndex = iNumPag - 1;
                }
                else
                {
                    gvUser.PageIndex = 0;
                }
            }
            CargargvUser();
        }
        /// <summary>
        /// Método para limpiar los campos del panel de ingreso, visualización y actualización de datos de Usuarios
        /// </summary>
        public void LimpiarCampos()
        {
            tbNombre.Text = "";
            tbApellido.Text = "";
            tbUsuario.Text = "";
            tbPass.Text = "";
            cbactivoU.Checked = true;

            filldllPerfil();

        }
        /// <summary>
        /// Método para llenar el gridview que contiene los datos de Usuarios
        /// </summary>
        public void CargargvUser()
        {
            gvUser.DataSource = nu.UsuarioPerfil_Select();
            gvUser.DataBind();

        }

        //-------------------------METODOS PARA OPERACIONES SOBRE ROLES
        /// <summary>
        /// Método que sirve para la paginación del gridview que contiene los datos de los roles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GoPageR(object sender, System.EventArgs e)
        {
            DropDownList otraPag = (DropDownList)sender;

            int iNumPag = 0;
            if (int.TryParse(otraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= gvPerfil.PageCount)
            {
                if (int.TryParse(otraPag.Text.Trim(), out iNumPag) && iNumPag > 0 && iNumPag <= gvPerfil.PageCount)
                {
                    gvPerfil.PageIndex = iNumPag - 1;
                }
                else
                {
                    gvPerfil.PageIndex = 0;
                }
            }
            CargargvPerfil();
        }
        /// <summary>
        /// Método para llenar el dropdownList de datos de Roles
        /// </summary>
        public void filldllPerfil()
        {
            dllPerfil.DataSource = np.Perfil_Select();
            dllPerfil.DataBind();
        }
        /// <summary>
        /// Método para limpiar los campos del panel de ingreso, visualización y actualización de datos de Roles
        /// </summary>
        public void LimpiarCamposR()
        {
            tbNombreRol.Text = "";
            cbActivoR.Checked = true;
        }
        /// <summary>
        /// Método para habilitar o deshabilitar campos del formulario de edición, visualización o inserción del panel de usuario
        /// </summary>
        /// <param name="estado">Estado de enabled del controles</param>
        public void HabilitarInabilitarPlnUser(bool estado)
        {
            tbNombre.Enabled = estado;
            tbApellido.Enabled = estado;
            tbUsuario.Enabled = estado;
            tbPass.Enabled = estado;
            cbactivoU.Enabled = estado;
            dllPerfil.Enabled = estado;
        }
        /// <summary>
        /// Método para llenar el gridview que contiene los datos de Roles
        /// </summary>
        public void CargargvPerfil()
        {
            gvPerfil.DataSource = np.Perfil_Select();
            gvPerfil.DataBind();
        }
        /// <summary>
        /// Método para llenar el los campos que contiene los datos de usuarios
        /// </summary>
        public void cargarDatosUser(int indice)
        {
            //SE OBTIENE EL ID DE USUARIO PARA CARGAR LOS DATOS
            int idusuario = int.Parse(gvUser.DataKeys[indice].Values[0].ToString());
            //SE GUARDA EL ID EN UNA VARIBLE DE SESSION
            Session["IdU"] = idusuario;

            //SE OBTIENEN TODOS LOS DATOS PARA MOSTRARLOS EN LOS CAMPOS DEL FORMULARIO
            string nombres = gvUser.DataKeys[indice].Values[1].ToString();
            string Apellidos = gvUser.DataKeys[indice].Values[2].ToString();
            string Contraseña = gvUser.DataKeys[indice].Values[6].ToString();
            string usuario = gvUser.Rows[indice].Cells[1].Text.ToString();

            //SE LLENA EL DROPDOWNLIST DE PERFILES
            filldllPerfil();

            int IdPerfil;
            if (gvUser.DataKeys[indice].Values[5].ToString() != "")
            {
                IdPerfil = int.Parse(gvUser.DataKeys[indice].Values[5].ToString());
                // SE SETEA EL VALOR SELECCIONADO CON EL ID DEL ROL ASIGNADO AL USUARIO 
                dllPerfil.SelectedValue = IdPerfil.ToString();
                //SE GUARDA EL ID DE PERFIL EN UNA VARIABLE DE SESSION
                Session["IdPerfil"] = IdPerfil;
            }

            bool Activo = bool.Parse(gvUser.DataKeys[indice].Values[3].ToString());

            cbactivoU.Checked = Activo;

            //ESTOS IF SIRVEN PARA ACTIVAR LOS ATRIBUTOS CSS EN LOS TEXTBOX DEL FORMUAIO
            if (nombres != "")
            {
                tbNombre.Text = nombres;
                lblnombre.Attributes.Add("class", "active");

            }
            if (Apellidos != "")
            {
                tbApellido.Text = Apellidos;
                lblApellido.Attributes.Add("class", "active");
            }
            if (usuario != "")
            {
                tbUsuario.Text = usuario;
                lblUsuario.Attributes.Add("class", "active");
            }
            if (Contraseña != "")
            {
                tbPass.Text = Contraseña;
                lblPass.Attributes.Add("class", "active");
            }

            ////LEVANTAMOS EL POP-UP
            ////btnCrearNuevaU_MPExtender.Enabled = true;
            ////btnCrearNuevaU_MPExtender.Show();

            ////ACTIVAMOS EL PANEL DEL FORMULARIO
            ////pnlUser.Style["display"] = "";
            ////BtnActualizar.Style["display"] = "";
            ////BtnCancelar.Style["display"] = "";
            ////btnIngresar.Style["display"] = "";
            ////btnCerrar.Style["display"] = "";

            mvPermisos.SetActiveView(vwPanelUsuario);
            //LLAMAMOS ELMETODO PARA HABILITAR Y DESHABILITA CAMPOS
            HabilitarInabilitarPlnUser(true);

        }
        /// <summary>
        /// Método para llenar el los campos que contiene los datos de roles
        /// </summary>
        public void cargaDatosRol(int indice)
        {
            //se obtiene el Id del usuario a Visualizar
            int idPerfil = int.Parse(gvPerfil.DataKeys[indice].Values[0].ToString());
            //se guarda el id en una variable de session
            Session["IdR"] = idPerfil;

            // se obtienen todos los datos para mostrarlos en los campos del formulario
            string NombreRol = gvPerfil.Rows[indice].Cells[1].Text.ToString();

            bool Activo = bool.Parse(gvPerfil.DataKeys[indice].Values[1].ToString());

            cbActivoR.Checked = Activo;

            //estos if son para activar atributos css en los texbox del formulario
            if (NombreRol != "")
            {
                if (NombreRol.Trim() != "&nbsp;")
                {
                    tbNombreRol.Text = NombreRol;
                    lblNombreRol.Attributes.Add("class", "active");
                }
                else
                {
                    tbNombreRol.Text = "";
                    lblNombreRol.Attributes.Add("class", "active");
                }

            }

            ////levantamos el pop-up
            //btnCrearRol_MPExtender.Enabled = true;
            //btnCrearRol_MPExtender.Show();

            ////activamos el panel del formulario
            //pnlRol.Style["display"] = "";
            btnGuardarRol.Style["display"] = "";
            btnCancelarRol.Style["display"] = "";
            btnActualizarRol.Style["display"] = "";
            btnCerrarRol.Style["display"] = "";

            mvPermisos.SetActiveView(vwpnlRoles);

            // llamamos a la funcion para habilitar y deshabilitar campos
            HabilitarInabilitarPlnRol(true);
            filltreeview();
            tvOpcionesR.ExpandAll();
            tvOpcionesR.Attributes.Add("onclick", "OnTreeClick(event)");
            ChecarNodos(idPerfil);

        }
        /// <summary>
        /// Método que sirve para cargar el árbol jeráquico con los items del menú existente
        /// </summary>
        public void filltreeview()
        {

            List<MenuPerfil> menu = menup.MenuObtenerItems();
            TreeNode root = new TreeNode();
            root.Text = "Items del Menú";

            var padre = from m in menu
                        where m.Padre.Equals(0)
                        select m;

            foreach (var p in padre)
            {
                TreeNode Npadre = new TreeNode();
                Npadre.Text = p.Nombre;
                Npadre.Value = p.IdMenu.ToString();

                agregarhijos(Npadre, p.IdMenu, menu);

                root.ChildNodes.Add(Npadre);


            }

            tvOpcionesR.Nodes.Clear();
            tvOpcionesR.Nodes.Add(root);
        }

        /// <summary>
        /// Método que sirve para agregar los hijos a cada item del menú
        /// </summary>
        /// <param name="padre">Id menu del item padre</param>
        /// <param name="id">Id menu del item</param>
        /// <param name="menu">lista con los item existentes</param>
        public void agregarhijos(TreeNode padre, int id, List<MenuPerfil> menu)
        {
            var hijos = from m in menu
                        where m.Padre.Equals(id)
                        select m;

            foreach (var c in hijos)
            {
                TreeNode Nhijo = new TreeNode();
                Nhijo.Text = c.Nombre;
                Nhijo.Value = c.IdMenu.ToString();

                agregarhijos(Nhijo, c.IdMenu, menu);
                padre.ChildNodes.Add(Nhijo);

            }

        }
        /// <summary>
        /// Método que sirve para poner checked true los item del menú asignados en dependencia del rol a visualizar o actualizar
        /// </summary>
        /// <param name="idPerfil">Id del perfil a visualizar o actualziar</param>
        public void ChecarNodos(int idPerfil)
        {

            List<int> i = menup.ObtenerItemsxPerfil(idPerfil);
            bool chek = false;
            TreeNodeCollection nodes = tvOpcionesR.Nodes;
            foreach (TreeNode item in nodes)
            {
                string value = item.Value;
                string nombre = item.Text;
                bool c = item.Checked;

                if (RecorrerNodos(item, i, chek))
                {
                    item.Checked = true;
                }

            }
        }
        /// <summary>
        /// Método que sirve para recorrer  checar los items hijos asignados en dependencia del rol a visualizar o a actualizar
        /// </summary>
        /// <param name="treeNode">arbol treview con datos</param>
        /// <param name="i">lista de item asignados por rol</param>
        /// <param name="chek">bandera bool para saber si el rol tiene o asignados item</param>
        /// <returns></returns>
        private bool RecorrerNodos(TreeNode treeNode, List<int> i, bool chek)
        {
            try
            {
                //Si el nodo que recibimos tiene hijos se recorrerá
                //para luego verificar si esta o no checado
                foreach (TreeNode tn in treeNode.ChildNodes)
                {
                    string value = tn.Value;
                    string nombre = tn.Text;
                    if (i.Contains(int.Parse(value)))
                    {
                        tn.Checked = true;
                        chek = true;

                    }

                    RecorrerNodos(tn, i, chek);
                }
                return chek;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Método para insertar items por rol asignados en una inserción o actualización de datos de roles
        /// </summary>
        /// <param name="idPerfil">id perfil del rol a a asignar nuevos items</param>
        public void InsertarItems(int idPerfil)
        {
            TreeNodeCollection nodes = tvOpcionesR.Nodes;
            foreach (TreeNode item in nodes)
            {
                string value = item.Value;
                RecorrerNodosChecked(item, idPerfil);
            }
        }
        /// <summary>
        /// Método para insertar items hijos por rol asignados en una inserción o actualización de datos de roles
        /// </summary>
        /// <param name="idPerfil">Id del rol a asignar los nuevos items</param>
        private void RecorrerNodosChecked(TreeNode treeNode, int idPerfil)
        {
            try
            {
                //Si el nodo que recibimos tiene hijos se recorrerá
                //para luego verificar si esta o no checado
                foreach (TreeNode tn in treeNode.ChildNodes)
                {
                    int IdMenu = int.Parse(tn.Value);
                    if (tn.Checked)
                    {
                        bool insert = false;

                        while (!insert)
                        {
                            if (nmp.MenuPerfilInsert(IdMenu, idPerfil))
                            {
                                insert = true;
                            }
                        }

                    }

                    RecorrerNodosChecked(tn, idPerfil);
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Método para habilitar o deshabilitar campos del formulario de edición, visualización o inserción del panel de roles
        /// </summary>
        /// <param name="estado">Estado de enabled del controles</param>
        public void HabilitarInabilitarPlnRol(bool estado)
        {
            tbNombreRol.Enabled = estado;
            cbActivoR.Enabled = estado;
            tvOpcionesR.Enabled = estado;
        }
        //--------------------METODOS PARA AMBAS VISTAS----------------------------------//
        /// <summary>
        /// Método para la creación de mensajes de error o éxito después de realizar una acción sobre cada registro de ambos gridview de usuario o roles
        /// </summary>
        /// <param name="tipo">Tipo de mensaje error o éxito</param>
        /// <param name="mensaje">Mensaje a presentar</param>
        /// <param name="control">Nombre del control donde se mostrará el mensaje, panel o form</param>
        /// <param name="form">Nombre de la vista donde se presentará usuario o roles</param>
        //public void Mensaje(string tipo, string mensaje, string control, string form)
        //{
        //    string divclass = "";


        //    if (tipo == "success")
        //        divclass = "alert alert-success";
        //    else if (tipo == "danger")
        //        divclass = "alert alert-danger";
        //    else if (tipo == "warning")
        //        divclass = "alert alert-danger";
        //    if (form == "Usuario")
        //    {
        //        if (control != "panel")
        //        {
        //            ltMessage.Text = "<div class='" + divclass + "' role='alert' id='alerta'>" +
        //                                   "<span class='sr-only'>Error:</span>" + mensaje +
        //                             "</div>";
        //        }
        //        else
        //        {
        //            ltMsgPanel.Text = "<div class='" + divclass + "' role='alert' id='alerta'>" +
        //                                   "<span class='sr-only'>Error:</span>" + mensaje +
        //                             "</div>";
        //        }
        //    }
        //    else
        //    {
        //        if (control != "panel")
        //        {
        //            LtMsgRo.Text = "<div class='" + divclass + "' role='alert' id='alerta'>" +
        //                                   "<span class='sr-only'>Error:</span>" + mensaje +
        //                             "</div>";
        //        }
        //        else
        //        {
        //            ltPanelR.Text = "<div class='" + divclass + "' role='alert' id='alerta'>" +
        //                                   "<span class='sr-only'>Error:</span>" + mensaje +
        //                             "</div>";
        //        }


        //    }
        //}
        #endregion
        #region EVENTOS
        //-----------------------EVENTOS VISTA USUARIOS--------------------------------------------------------//
        protected void gvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int page = gvUser.PageIndex;
            int y = e.NewPageIndex;
            if (y >= 0)
            {
                gvUser.PageIndex = e.NewPageIndex;
                CargargvUser();
            }

        }
        protected void imgEdit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnEditar = (ImageButton)sender;
            //SE OBITIENE EL ID DEL REGISTRO
            int indice = Convert.ToInt32(btnEditar.CommandArgument.ToString());
            cargarDatosUser(indice);

            btnIngresar.Style["display"] = "none";
            //btnCerrar.Style["display"] = "none";

            string script = @"<script type='text/javascript'>
                        $(document).ready(function () {
                        $('#User').removeClass('m12');
                        $('#User').addClass('m6');
                        $('#pass').removeClass('divhide');
                        $('#myTabs a[href='#Usuario']').tab('show')
                   });
                  </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
            cbactivoU.Enabled = true;
            dllPerfil.Enabled = true;
        }
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            if ((tbNombre.Text != "") && (tbApellido.Text != "") && (tbUsuario.Text != ""))
            {
                if (tbPass.Text != "")
                {
                    string pass = "CL4v3" + tbPass.Text + "S3gur1dad";
                    pass = ngu.EncriptarContraseña(pass);
                    if (ngu.UsuarioUpdatePass(tbUsuario.Text, pass, cbactivoU.Checked, tbNombre.Text, tbApellido.Text, int.Parse(Session["IdU"].ToString())))
                    {
                        if (nu.UsuarioPerfilDelete(int.Parse(Session["IdU"].ToString())))
                        {
                            if (nu.UsuarioPerfilInsert(int.Parse(Session["IdU"].ToString()), int.Parse(dllPerfil.SelectedValue)))
                            {
                                //Mensaje("success", "Usuario Actualizado Correctamente", "", "Usuario");
                                alertValida.Visible = false;
                                alertSucces.Visible = true;
                                LblSuccess.Visible = true;
                                LblSuccess.Text = "Usuario Actualizado Correctamente";
                                CargargvUser();
                                mvPermisos.SetActiveView(wvUsuario);
                            }
                        }

                    }

                }
                else
                {
                    if (ngu.UsuarioUpdate(tbUsuario.Text, cbactivoU.Checked, tbNombre.Text, tbApellido.Text, int.Parse(Session["IdU"].ToString())))
                    {
                        if (nu.UsuarioPerfilDelete(int.Parse(Session["IdU"].ToString())))
                        {
                            if (nu.UsuarioPerfilInsert(int.Parse(Session["IdU"].ToString()), int.Parse(dllPerfil.SelectedValue)))
                            {
                                //Mensaje("success", "Usuario Actualizado Correctamente", "", "Usuario");
                                alertValida.Visible = false;
                                alertSucces.Visible = true;
                                LblSuccess.Visible = true;
                                LblSuccess.Text = "Usuario Actualizado Correctamente";
                                CargargvUser();
                                mvPermisos.SetActiveView(wvUsuario);
                            }
                        }

                    }

                }
            }
            else
            {
                //Mensaje("danger", "Por Favor llene los todos los Campos", "panel", "Usuario");
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = "Por Favor llene los todos los Campos";
                mvPermisos.SetActiveView(vwPanelUsuario);

                //btnCrearNuevaU_MPExtender.Enabled = true;
                //btnCrearNuevaU_MPExtender.Show();

            }


        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (tbNombre.Text != "" && tbApellido.Text != "" && tbUsuario.Text != "" && tbPass.Text != "")
            {
                string pass = "CL4v3" + tbPass.Text + "S3gur1dad";
                pass = ngu.EncriptarContraseña(pass);
                if (ngu.usuarioInsert(tbUsuario.Text, pass, cbactivoU.Checked, tbNombre.Text, tbApellido.Text))
                {
                    int idUsuario = ngu.Usuario_SelectByMaxId();

                    if (idUsuario > 0)
                    {
                        if (nu.UsuarioPerfilInsert(idUsuario, int.Parse(dllPerfil.SelectedValue)))
                        {
                            //Mensaje("success", "Usuario Creado Correctamente", "", "Usuario");
                            alertValida.Visible = false;
                            alertSucces.Visible = true;
                            LblSuccess.Visible = true;
                            LblSuccess.Text = "Usuario Creado Correctamente";
                            CargargvUser();
                            //btnCrearNuevaU_MPExtender.Enabled = true;
                            //btnCrearNuevaU_MPExtender.Show();
                            mvPermisos.SetActiveView(wvUsuario);

                        }
                    }

                }

            }
            else
            {
                //Mensaje("danger", "Por Favor llene todos los Campos", "panel", "Usuario");
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = "Por Favor llene todos los Campos";
                mvPermisos.SetActiveView(vwPanelUsuario);
                //btnCrearNuevaU_MPExtender.Enabled = true;
                //btnCrearNuevaU_MPExtender.Show();

            }

        }
        protected void BtnCancelar_Click1(object sender, EventArgs e)
        {
            mvPermisos.SetActiveView(wvUsuario);
            //btnCrearNuevaU_MPExtender.Enabled = false;
            //btnCrearNuevaU_MPExtender.Hide();
            //pnlUser.Style["display"] = "none";
        }

        protected void imgDetails_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnEditar = (ImageButton)sender;
            int indice = Convert.ToInt32(btnEditar.CommandArgument.ToString());

            cargarDatosUser(indice);
            BtnActualizar.Style["display"] = "none";
            BtnCancelar.Style["display"] = "none";
            btnIngresar.Style["display"] = "none";

            string script = @"<script type='text/javascript'>
                        $(document).ready(function () {
                        $('#User').removeClass('m6');
                        $('#User').addClass('m12');
                        $('#pass').addClass('divhide');
                        $('#myTabs a[href='#Usuario']').tab('show')
                   });
                  </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
            HabilitarInabilitarPlnUser(false);
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            //btnCrearNuevaU_MPExtender.Enabled = false;
            //btnCrearNuevaU_MPExtender.Hide();
            mvPermisos.SetActiveView(wvUsuario);
            //pnlUser.Style["display"] = "none";
        }
        protected void btnCrearNuevaU_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarCampos();

            //pnlUser.Style["display"] = "";
            BtnActualizar.Style["display"] = "none";
            BtnCancelar.Style["display"] = "";
            btnIngresar.Style["display"] = "";
            //btnCerrar.Style["display"] = "none";

            //btnCrearNuevaU_MPExtender.Enabled = true;
            //btnCrearNuevaU_MPExtender.Show();
            mvPermisos.SetActiveView(vwPanelUsuario);

            string script = @"<script type='text/javascript'>
                        $(document).ready(function () {
                        $('#User').removeClass('m12');
                        $('#User').addClass('m6');
                        $('#pass').removeClass('divhide');
                   });
                  </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);

            cbactivoU.Enabled = true;
            dllPerfil.Enabled = true;

            lblNombreRol.Attributes.Remove("active");
            lblNombreRol.Attributes.Remove("active");
            lblNombreRol.Attributes.Remove("active");
            lblNombreRol.Attributes.Remove("active");
        }
        protected void btnUsuariosvr_Click(object sender, EventArgs e)
        {
            mvPermisos.SetActiveView(wvUsuario);
            CargargvUser();
        }
        //----------------------EVENTOS VISTA ROLES--------------------------------------------------------------//
        protected void gvPerfil_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int page = gvPerfil.PageIndex;
            int y = e.NewPageIndex;
            if (y >= 0)
            {
                gvPerfil.PageIndex = e.NewPageIndex;
                CargargvPerfil();
            }

        }
        protected void imgEditR_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnEditar = (ImageButton)sender;
            int indice = Convert.ToInt32(btnEditar.CommandArgument.ToString());

            cargaDatosRol(indice);

            //pnlRol.Style["display"] = "";
            btnActualizarRol.Style["display"] = "";
            btnCancelarRol.Style["display"] = "";
            btnGuardarRol.Style["display"] = "none";
            btnCerrarRol.Style["display"] = "none";

            //btnCrearRol_MPExtender.Enabled = true;
            //btnCrearRol_MPExtender.Show();


            string script = @"<script type='text/javascript'>
                        $(document).ready(function () {
                        $('#rol').removeClass('m12');
                        $('#rol').addClass('m6');
                        $('#myTabs a[href='#perfil']').tab('show')
                   });
                  </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);

        }
        protected void imgDetailsR_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnEditar = (ImageButton)sender;
            int indice = Convert.ToInt32(btnEditar.CommandArgument.ToString());

            cargaDatosRol(indice);
            btnActualizarRol.Style["display"] = "none";
            btnCancelarRol.Style["display"] = "none";
            btnGuardarRol.Style["display"] = "none";


            string script = @"<script type='text/javascript'>
                        $(document).ready(function () {
                        $('#rol').removeClass('m12');
                        $('#rol').addClass('m6');
                        $('#myTabs a[href='#perfil']').tab('show')
                   });
                  </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);
            HabilitarInabilitarPlnRol(false);
            tvOpcionesR.ExpandAll();
        }
        protected void btnp_Click(object sender, ImageClickEventArgs e)
        {
            LimpiarCamposR();
            HabilitarInabilitarPlnRol(true);
            //pnlRol.Style["display"] = "";
            btnActualizarRol.Style["display"] = "none";
            btnCancelarRol.Style["display"] = "";
            btnGuardarRol.Style["display"] = "";
            btnCerrarRol.Style["display"] = "none";

            //btnCrearRol_MPExtender.Enabled = true;
            //btnCrearRol_MPExtender.Show();

            mvPermisos.SetActiveView(vwpnlRoles);

            string script = @"<script type='text/javascript'>
                        $(document).ready(function () {
                        $('#rol').removeClass('m12');
                        $('#rol').addClass('m6');
                   });
                  </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "invocarfuncion", script, false);

            cbactivoU.Enabled = true;
            dllPerfil.Enabled = true;

            lblnombre.Attributes.Remove("active");

            filltreeview();
            tvOpcionesR.ExpandAll();

        }
        protected void btnGuardarRol_Click(object sender, EventArgs e)
        {
            if (tbNombreRol.Text != "")
            {
                if (np.PerfilInsert(cbActivoR.Checked, tbNombreRol.Text))
                {
                    int idPerfil = np.Perfil_SelectByMaxId();
                    if (idPerfil > 0)
                    {
                        InsertarItems(idPerfil);
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Rol Creado Correctamente";
                        //Mensaje("success", "Rol Creado Correctamente", "", "Rol");
                        CargargvPerfil();
                        //btnCrearRol_MPExtender.Enabled = false;
                        //btnCrearRol_MPExtender.Hide();
                        mvPermisos.SetActiveView(wvRol);

                        //pnlRol.Style["display"] = "none";
                        mvPermisos.SetActiveView(wvRol);
                    }
                }


            }
            else
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = "Por Favor llene los todos los Campos";
                //Mensaje("danger", "Por Favor llene los todos los Campos", "panel", "Rol");
                mvPermisos.SetActiveView(vwpnlRoles);
            }

        }
        protected void btnActualizarRol_Click(object sender, EventArgs e)
        {
            if (tbNombreRol.Text != "")
            {
                if (np.PerfilUpdate(int.Parse(Session["IdR"].ToString()), cbActivoR.Checked, tbNombreRol.Text))
                {

                    if (nmp.MenuPerfilDelete(int.Parse(Session["IdR"].ToString())))
                    {
                        InsertarItems(int.Parse(Session["IdR"].ToString()));
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Rol Actualizado Correctamente";
                        //Mensaje("success", "Rol Actualizado Correctamente", "", "Rol");
                        CargargvPerfil();
                        mvPermisos.SetActiveView(wvRol);

                    }
                }
            }
            else
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = "Por Favor llene los todos los Campos";
                //Mensaje("danger", "Por Favor llene los todos los Campos", "panel", "Rol");
                //btnCrearRol_MPExtender.Enabled = true;
                //btnCrearRol_MPExtender.Show();
                mvPermisos.SetActiveView(vwpnlRoles);
            }


        }
        protected void btnCancelarRol_Click(object sender, EventArgs e)
        {
            mvPermisos.SetActiveView(wvRol);
            //btnCrearRol_MPExtender.Enabled = false;
            //btnCrearRol_MPExtender.Hide();
            //pnlRol.Style["display"] = "none";
        }
        protected void btnCerrarRol_Click(object sender, EventArgs e)
        {
            mvPermisos.SetActiveView(wvRol);

            //btnCrearRol_MPExtender.Enabled = false;
            //btnCrearRol_MPExtender.Hide();
            //pnlRol.Style["display"] = "none";

        }
        protected void BubtnRolesvu_Click(object sender, EventArgs e)
        {
            mvPermisos.SetActiveView(wvRol);
            BubtnRolesvu.Visible = true;
            CargargvPerfil();
        }
        #endregion

        protected void tvOpcionesR_SelectedNodeChanged(object sender, EventArgs e)
        {
            mvPermisos.SetActiveView(vwpnlRoles);

            //pnlRol.Style["display"] = "";

        }

        protected void btnCrearNuevaU2_Click(object sender, ImageClickEventArgs e)
        {
            mvPermisos.SetActiveView(vwPanelUsuario);
            LimpiarCampos();
        }

        protected void gvPerfil_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            int page = gvUser.PageIndex;
            int y = e.NewPageIndex;
            if (y >= 0)
            {
                gvPerfil.PageIndex = e.NewPageIndex;
                CargargvPerfil();
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtUser = nu.UsuarioPerfil_Select();
            DataView dtvUser = dtUser.Copy().DefaultView;


            if (tbUsuario.Text != "")
            {
                dtvUser.RowFilter = "usuario like '%" + tbUsuario.Text.Trim() + "%' or NombreCompleto like '%" + tbUsuario.Text.Trim() + "%'";
                gvUser.DataSource = dtvUser.ToTable();
                gvUser.DataBind();

            }
            else
            {
                gvUser.DataSource = dtUser;
                gvUser.DataBind();
            }
        }

        protected void btnIBuscarPerfil_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtPerfil= np.Perfil_Select();
            DataView dtvPerfil = dtPerfil.Copy().DefaultView;


            if (tbNombreRol.Text != "")
            {
                dtvPerfil.RowFilter = "Descripcion like '%" + tbNombreRol.Text.Trim() + "%'";
                gvPerfil.DataSource = dtvPerfil.ToTable() ;
                gvPerfil.DataBind();

            }
            else
            {
                gvPerfil.DataSource = dtPerfil;
                gvPerfil.DataBind();
            }
        }
    }
}