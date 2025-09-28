/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class PeriodoFiscal2 : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Periodo Neg_Periodo = new Neg_Periodo();

        #endregion
        string user;
        protected void Page_Load(object sender, EventArgs e)
        {
           user = Convert.ToString(this.Page.Session["usuario"]);
            if (!this.Page.IsPostBack)
            {               
                ObtenerPeriodosFiscal();
                EnabledDisabledControls();
            }
        }

        private void ObtenerPeriodosFiscal()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Periodo datos = new Dato_Periodo();
            System.Data.DataTable periodos = datos.GetAllPeriodosFiscal(userDetail.getIDEmpresa());

            object estadoPeriodo = periodos.Rows[0]["estado"];
            Session["EstadoPeriodo"] = estadoPeriodo;

            gvPeriodosFiscales.DataSource = datos.GetAllPeriodosFiscal(userDetail.getIDEmpresa());
            gvPeriodosFiscales.DataBind();

        }
        protected void gvPeriodosFiscales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CerrarPeriodo")
            {
                if (e.CommandArgument != null && int.TryParse(e.CommandArgument.ToString(), out int anioFiscal))
                {
                    string mensajeDB = string.Empty;
                    try
                    {
                        IUserDetail userDetail = UserDetailResolver.getUserDetail();
                        Dato_Periodo datos = new Dato_Periodo();
                        string user = Convert.ToString(this.Page.Session["usuario"]);

                        bool exito = datos.CerrarPeriodoFiscal(anioFiscal, user, userDetail.getIDEmpresa(), out mensajeDB);

                        if (exito)
                        {
                            ObtenerPeriodosFiscal();
                            alertError.Visible = false;
                            alertSuccess.Visible = true;
                            lblSuccess.Visible = true;
                            lblSuccess.Text = "El periodo fiscal ha sido cerrado exitosamente.";
                        }
                        else
                        {
                            alertSuccess.Visible = false;
                            alertError.Visible = true;
                            lblAlert.Visible = true;
                            lblAlert.Text = string.IsNullOrEmpty(mensajeDB) ?
                                "Ocurrió un error al intentar cerrar el periodo en la base de datos (Error no específico)." :
                                mensajeDB; 
                        }
                    }
                    catch (Exception ex)
                    {
                      
                        alertSuccess.Visible = false;
                        alertError.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Ocurrió un error inesperado: " + ex.Message;
                    }
                }
                else
                {
                    alertSuccess.Visible = false;
                    alertError.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error: No se pudo obtener el año fiscal para cerrar el periodo.";
                }             
            }
        }

        public bool validar()
        {
            return true;
        }

        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            DateTime fechaIni;
            int anioFiscal = 0;
            int nPlanilla = 0;
            string mensajeDB2 = string.Empty;

            anioFiscal = Convert.ToInt32(txtAnioFiscal.Text.Trim());
            nPlanilla = Convert.ToInt32(txtNoPlanilla.Text.Trim());
            fechaIni = Convert.ToDateTime(txtFechaInicio.Text.Trim());
        

                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                Dato_Periodo datos = new Dato_Periodo();
                bool creado = datos.AgregarPeriodoFiscal(nPlanilla, fechaIni, anioFiscal, user, userDetail.getIDEmpresa(), out mensajeDB2);

                if (creado)
                {
                    alertError.Visible = false;
                    alertSuccess.Visible = true;
                    lblSuccess.Visible = true;
                    lblSuccess.Text = string.IsNullOrEmpty(mensajeDB2) ? "Ingreso satisfactorio" :
                                mensajeDB2; ;

            }
                else
                {
                    alertSuccess.Visible = false;
                    alertError.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error ingresando datos";
                }
           
        }

        private void EnabledDisabledControls()
        {
            if (Session["EstadoPeriodo"] != null)
            {
                string estado = Session["EstadoPeriodo"].ToString().ToUpper();
                if (estado == "CERRADO")
                {
               
                    this.txtAnioFiscal.Enabled = true;
                    this.txtFechaInicio.Enabled = true;
                    this.txtNoPlanilla.Enabled = true;
                    this.btnCrearPeriodo.Enabled = true;
                }
                else 
                {
                    this.txtAnioFiscal.Enabled = false;
                    this.txtFechaInicio.Enabled = false;
                    this.txtNoPlanilla.Enabled = false;
                }
            }
            else
            {
                this.txtAnioFiscal.Enabled = false;
                this.txtFechaInicio.Enabled = false;
                this.txtNoPlanilla.Enabled = false;
            }
        }
        protected void gvPeriodosFiscales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Periodo datos = new Dato_Periodo();
            gvPeriodosFiscales.PageIndex = e.NewPageIndex;
            gvPeriodosFiscales.DataBind();
            gvPeriodosFiscales.DataSource = datos.GetAllPeriodosFiscal(userDetail.getIDEmpresa());
        }
       
    }
}

*/

using System;
using System.Data;
using System.Web.UI.WebControls;
using Datos;
using Negocios;

namespace NominaRRHH.Presentacion
{
    public partial class PeriodoFiscal2 : System.Web.UI.Page
    {
        
        private string user;

        private Dato_Periodo DatosPeriodo => new Dato_Periodo();
        private IUserDetail userDetail = UserDetailResolver.getUserDetail();
  
        protected void Page_Load(object sender, EventArgs e)
        {
            user = Convert.ToString(this.Page.Session["usuario"]);
            if (!this.Page.IsPostBack)
            {
                ObtenerPeriodosFiscal();
            }
        }

        private void ObtenerPeriodosFiscal()
        {
            DataTable periodos = DatosPeriodo.GetAllPeriodosFiscal(userDetail.getIDEmpresa());

            if (periodos != null && periodos.Rows.Count > 0)
            {

                object estadoPeriodo = periodos.Rows[0]["estado"];
                Session["EstadoPeriodo"] = estadoPeriodo;
                EnabledDisabledControls(); // Se llama aquí después de establecer la sesión
            }
            else
            {
                Session["EstadoPeriodo"] = "CERRADO";
                EnabledDisabledControls();
            }

            gvPeriodosFiscales.DataSource = periodos;
            gvPeriodosFiscales.DataBind();
        }

        private void ShowSuccessAlert(string message)
        {
            alertError.Visible = false;
            alertSuccess.Visible = true;
            lblSuccess.Visible = true;
            lblSuccess.Text = string.IsNullOrEmpty(message) ? "Operación satisfactoria" : message;
        }

        private void ShowErrorAlert(string message)
        {
            alertSuccess.Visible = false;
            alertError.Visible = true;
            lblAlert.Visible = true;
            lblAlert.Text = string.IsNullOrEmpty(message) ? "Error inesperado." : message;
        }

        protected void gvPeriodosFiscales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CerrarPeriodo")
            {
                if (e.CommandArgument != null && int.TryParse(e.CommandArgument.ToString(), out int anioFiscal))
                {
                    string mensajeDB = string.Empty;

                    bool exito = DatosPeriodo.CerrarPeriodoFiscal(anioFiscal, user, userDetail.getIDEmpresa(), out mensajeDB);

                    if (exito)
                    {
                        Session["EstadoPeriodo"] = "CERRADO";
                        ObtenerPeriodosFiscal();
                        ShowSuccessAlert("El periodo fiscal ha sido cerrado exitosamente.");

                    }
                    else
                    {
                        ShowErrorAlert(mensajeDB);
                    }
                }
                else
                {
                    ShowErrorAlert("Error: No se pudo obtener el año fiscal para cerrar el periodo.");
                }
            }
        }

        public bool validar()
        {
            return true;
        }

        protected void btnCrearPeriodo_Click(object sender, EventArgs e)
        {
            if (!validar())
            {
                ShowErrorAlert("La validación de datos falló.");
                return;
            }

            DateTime fechaIni;
            int anioFiscal = 0;
            int nPlanilla = 0;
            string mensajeDB2 = string.Empty;

            try
            {
                anioFiscal = Convert.ToInt32(txtAnioFiscal.Text.Trim());
                nPlanilla = Convert.ToInt32(txtNoPlanilla.Text.Trim());
                fechaIni = Convert.ToDateTime(txtFechaInicio.Text.Trim());
            }
            catch (FormatException)
            {
                ShowErrorAlert("Error de formato: Asegúrese de que el año, número de planilla y fecha sean correctos.");
                return;
            }

            bool creado = DatosPeriodo.AgregarPeriodoFiscal(nPlanilla, fechaIni, anioFiscal, user, userDetail.getIDEmpresa(), out mensajeDB2);

            if (creado)
            {
                Session["EstadoPeriodo"] = "ABIERTO";
                ObtenerPeriodosFiscal(); // Recargar la grilla después del ingreso
                ShowSuccessAlert(mensajeDB2);
            }
            else
            {
                ShowErrorAlert(mensajeDB2);
            }
        }

        private void EnabledDisabledControls()
        {
            if (Session["EstadoPeriodo"] != null)
            {
                string estado = Session["EstadoPeriodo"].ToString().ToUpper();
                bool estaCerrado = (estado == "CERRADO");

                // Se simplifica la asignación de propiedades.
                this.txtAnioFiscal.Enabled = estaCerrado;
                this.txtFechaInicio.Enabled = estaCerrado;
                this.txtNoPlanilla.Enabled = estaCerrado;
                this.btnCrearPeriodo.Enabled = estaCerrado; 
                this.imgPopup.Enabled = estaCerrado;
            }
            else
            {
   
                this.txtAnioFiscal.Enabled = false;
                this.txtFechaInicio.Enabled = false;
                this.txtNoPlanilla.Enabled = false;
                this.btnCrearPeriodo.Enabled = false;
                this.imgPopup.Enabled = false;
            }
        }

        protected void gvPeriodosFiscales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPeriodosFiscales.PageIndex = e.NewPageIndex;
            ObtenerPeriodosFiscal();
        }
    }
}