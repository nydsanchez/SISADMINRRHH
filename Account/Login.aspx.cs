using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using NominaRRHH.Presentacion;

namespace NominaRRHH.Account
{
    public partial class Login : System.Web.UI.Page
    {
        Neg_Usuario nu = new Neg_Usuario();
        UsuarioSessionD usd = new UsuarioSessionD();
        Neg_Menu Neg_Menu = new Neg_Menu();
        public static class Globales
        {
            public static string id = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                obtenerEmpresas();
            }
            
        }
        private void obtenerEmpresas()
        {

            this.ddlEmpresa.DataSource = Neg_Menu.cargarEmpresas();
            this.ddlEmpresa.DataMember = "empresa";
            this.ddlEmpresa.DataValueField = "Id_Bd";
            this.ddlEmpresa.DataTextField = "Empresa";
            this.ddlEmpresa.DataBind();
        }

        //protected void btnIngresar_Click(object sender, EventArgs e)
        //{
        //    DataTable ds = new DataTable();

        //    if (Convert.ToInt32(ddlEmpresa.SelectedValue) > 0)
        //    {

        //        ds = Neg_Menu.ObtenerCadena(Convert.ToInt32(ddlEmpresa.SelectedValue));

        //        Session["DataSource"] = ds.Rows[0][0].ToString();
        //        Session["InitialCatalog"] = ds.Rows[0][1].ToString();
        //        Session["UserID"] = ds.Rows[0][2].ToString();
        //        Session["Password"] = ds.Rows[0][3].ToString();
        //        armarCadena(Session["DataSource"].ToString(), Session["InitialCatalog"].ToString(), Session["UserID"].ToString(),
        //        Session["Password"].ToString());
        //        usd.Usuarios = TxtUsuario.Text.Trim();
        //        usd.Pass = nu.EncriptarContraseña("CL4v3" + TxtPass.Text.Trim() + "S3gur1dad");

        //        List<UsuarioSession> us = nu.VerificarUsuario(usd);

        //        if (us.Count > 0)
        //        {
        //            this.Page.Session["usuario"] = us[0].Usuarios.ToString();
        //            this.Page.Session["Idusuario"] = us[0].IdUsuario.ToString();
        //            Response.Redirect("~/Presentacion/Default.aspx");
        //        }

        //        else
        //        {
        //            alertLog.Visible = true;
        //            LblMsg.Text = "Usuario o Contraseña Incorrectos";
        //        }
        //    }
        //    else
        //    {
        //        alertLog.Visible = true;
        //        LblMsg.Text = "Favor Elija Una Empresa";
        //    }
        //}

        //private void armarCadena(string DataSource, string InitialCatalog, string UserID, string Password)
        //{
        //    if (Session["cadena"] == null)
        //    {

        //        Datos.CnBD con;
        //        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        //        builder.DataSource = DataSource;
        //        builder.InitialCatalog = InitialCatalog;
        //        builder.UserID = UserID;
        //        builder.Password = Password;

        //        Session["cadena"] = builder.ConnectionString;
        //        con = new Datos.CnBD(Session["cadena"].ToString());
        //    }
        //}

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlEmpresa.SelectedValue) > 0)
                {
                    Neg_Menu.ObtenerCadena(Convert.ToInt32(ddlEmpresa.SelectedValue));
                    usd.Usuarios = TxtUsuario.Text.Trim();
                    usd.Pass = nu.EncriptarContraseña("CL4v3" + TxtPass.Text.Trim() + "S3gur1dad");
                    List<UsuarioSession> us = nu.VerificarUsuario(usd);
                    if (us != null && us.Count > 0)
                    {
                        UserDetailResolver.setUserDetail(new WebFormUserDetail());

                        this.Page.Session["usuario"] = us[0].Usuarios.ToString();
                        this.Page.Session["Idusuario"] = us[0].IdUsuario.ToString();
                        this.Page.Session["user_codempleado"] = us[0].Codigo_Empleado.ToString();
                        this.Page.Session["Nombre"] = us[0].Nombre.ToString();
                        this.Page.Session["Apellido"] = us[0].Apellido.ToString();
                        this.Page.Session["UsuariosName"] = us[0].Usuarios.ToString();
                        string a = us[0].Usuarios.ToString();
                        WebFormUserDetail.storeUserDetail(us[0].Usuarios.ToString(), Convert.ToInt32(ddlEmpresa.SelectedValue), Convert.ToInt32(us[0].Codigo_Empleado));

                        // Establecer la cookie con el código del empleado
                        HttpCookie cookie = new HttpCookie("Idusuario", us[0].IdUsuario.ToString());
                        cookie.Expires = DateTime.Now.AddDays(30); // La cookie expirará en 30 días
                        Response.Cookies.Add(cookie);

                        Response.Redirect("~/Presentacion/Default.aspx");

                    }
                    else
                    {
                        alertLog.Visible = true;
                        LblMsg.Text = "Usuario o Contraseña Incorrectos";
                        //this.Page.Session["Idusuario"] = 0;

                    }


                }
                else
                {
                    alertLog.Visible = true;
                    LblMsg.Text = "Favor Elija Una Empresa";
                }
            }
            catch (NullReferenceException ex)
            {
              
                LblMsg.Text = $"Ocurrió un error: {ex.Message}"; // Muestra el mensaje de error
            }
            catch (Exception ex)
            {

                LblMsg.Text = $"Ocurrió un error:: {ex.Message}"; // Muestra el mensaje de error
            }

        }

    }
}