using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Datos;
using System.Text;
using NLog;

namespace NominaRRHH
{
    public partial class SiteMaster : MasterPage
    {
        // Propiedad pública para acceder al Label desde otras páginas
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        Neg_Menu menup = new Neg_Menu();
        public int pruebamaster;
        protected void Page_Load(object sender, EventArgs e)
        {
            int idperfil = 0, idusuario;
            object ob = this.Page.Session["Idusuario"];
            string obName = this.Page.Session["Nombre"] +" " + this.Page.Session["Apellido"];

            string pagename = Request.Url.Segments[Request.Url.Segments.Length - 1];
            //Page.Header.DataBind();

          
            Page.Header.DataBind();
            if (ob != null)
            {
                string idusuarioName = (string)this.Page.Session["usuario"]; // Recupera el 
                //usuario en session
                logger.Info("Session Iniciada por Usuario: " + idusuarioName);
                if (ltMenu.Text == "")
                {
                    if (pagename != "Login.aspx" && pagename != "About.aspx")
                    {
                        idusuario = Convert.ToInt32(ob);
                        idperfil = menup.ObtenerPerfilxUsuario(idusuario);
                        List<MenuPerfil> menu = menup.MenuxPerfil(idperfil);

                        var padre = from m in menu
                                    where m.Padre.Equals(0)
                                    select m;

                        ltMenu.Text = "<div class='navbar navbar-inverse navbar-fixed-top' role='navigation'>"
                                            + "<div class='container'>";
                        
                        int bandera = 0;
                        if (padre.Count() > 0)
                        {
                            ltMenu.Text += "<div id='navbar' class='navbar-collapse collapse'>"
                                                  + "<ul class='nav navbar-nav'>";

                            

                            foreach (var itempadre in padre)
                            {
                                string pagini = itempadre.Formulario;

                                var hijo = from m in menu
                                           where m.Padre.Equals(Convert.ToInt32(itempadre.IdMenu.ToString()))
                                           select m;

                                if (hijo.Count() > 0)
                                {
                                    ltMenu.Text += "<li><a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-expanded='false'>" + itempadre.Nombre + "<span class='caret'></span></a>";
                                    ltMenu.Text += "<ul class='dropdown-menu' role='menu'>";
                                    foreach (var itemhijo in hijo)
                                    {
                                        pagini = itemhijo.Formulario;                                                                             

                                        if (pagini == "~/../" + pagename)
                                        {
                                            bandera = 1;
                                        }
                                        ltMenu.Text += "<li><a href='" + itemhijo.Formulario + "'>" + itemhijo.Nombre + "</a></li>";
                                    }
                                    ltMenu.Text += "</ul>";

                                }

                                ltMenu.Text += "</li>";

                            }

                           

                            ltMenu.Text += "</ul>"
                                    + " <div class='navbar-right'><a class='btn btn-primary glyphicon glyphicon-off' style='margin-top: 5px; background-color: #506477;' data-toggle='tooltip' href='../Account/Login.aspx'></a></div>"
                                        + "</div>"
                                      + "</div>"
                                   + "</div>";
                        }
                        else
                        {
                            ltMenu.Text += "<div id='navbar' class='navbar-collapse collapse'>"
                                                  + "<ul class='nav navbar-nav'>";
                                                       

                            ltMenu.Text += " <li><a href='../Account/Login.aspx' class='navbar-right btn-primary'>Salir</a></li> </ul>";
                            ltMenu.Text += "</div>"
                                 + "</div>";

                        }


               //         ltMenu.Text += "<div class='navbar-left'>"
               //+ "<asp:LinkButton ID='btnSalir' runat='server' OnClick='btnSalir_Click' CssClass='btn btn-link' style='margin-top: 5px;'>"
               //+ " Usuario: " + obName // Aquí se agrega el nombre de usuario al label
               //+ "</asp:LinkButton>"
               //+ "</div>";
                        ltMenu.Text += "<div class='navbar-left'>"
               + "<label class='btn btn-link' style='margin-top: 5px;' onclick='btnSalir()'>"
               + " Usuario: " + obName
               + "</label>"
               + "</div>";

                        if (bandera == 0 && pagename != "Default.aspx")
                        {
                            //if (ltMenu.Text == "" && pagename != "Login.aspx")
                            //{
                            Page.Response.Redirect("~/Account/Login.aspx");
                            //  }
                        }

                    }
                    else
                    {
                        ltMenu.Text = "";
                    }
                }
            }
            else {
                Page.Response.Redirect("~/Account/Login.aspx");
            }
            
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Presentacion/Default.aspx");
        }
    }
}