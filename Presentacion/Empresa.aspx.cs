using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;

namespace NominaRRHH.Presentacion
{
    public partial class Empresa : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Empresas Neg_Empresas = new Neg_Empresas();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                ObtenerPaises();
                obtenerMoneda();
                obtenerTipoNomina();
                cargarDatosEmpresa();
            }
        }

        private void ObtenerPaises()
        {
            this.ddlPais.DataSource = Neg_Catalogos.CargarPaises();
            this.ddlPais.DataMember = "paises";
            this.ddlPais.DataValueField = "id_descripcion";
            ddlPais.DataTextField = "descripcion";
            this.ddlPais.DataBind();
        }

        private void obtenerTipoNomina()
        {
            this.ddlTipNomina.DataSource = Neg_Empresas.CargarTipoNomina();
            this.ddlTipNomina.DataMember = "nomina";
            this.ddlTipNomina.DataValueField = "idnomina";
            ddlTipNomina.DataTextField = "descripcion";
            this.ddlTipNomina.DataBind();
        }
        private void obtenerMoneda()
        {
            this.ddlMoneda.DataSource = Neg_Catalogos.CargarMonedas();
            this.ddlMoneda.DataMember = "monedas";
            this.ddlMoneda.DataValueField = "monedaId";
            this.ddlMoneda.DataTextField = "nombre_Moneda";
            this.ddlMoneda.DataBind();
        }
        void cargarDatosEmpresa() {
            try
            {
                dsPlanilla.dtEmpresaDataTable DetEmpresas = Neg_Empresas.ObtenerInfoDetEmpresas();
                if (DetEmpresas.Rows.Count > 0)
                {
                    hfempresa.Value = DetEmpresas[0].id_empresa.ToString();
                    txtEmpresa.Text = DetEmpresas[0].empresa;
                    txtGerRrhh.Text = DetEmpresas[0].Gterrhh;
                    ddlPais.SelectedValue = DetEmpresas[0].idpais.ToString();
                    ddlMoneda.SelectedValue = DetEmpresas[0].moneda.ToString();
                    // txtIdioma.Text = DetEmpresas.Rows[0]["idiomaprincipal"].ToString();
                    txtSalarMin.Text = DetEmpresas[0].salariominimo.ToString();
                    ddlTipNomina.SelectedValue = DetEmpresas[0].tiponomina.ToString();
                    //txtPorcSEmple.Text = DetEmpresas.Rows[0]["porseguroempleado"].ToString();
                    //txtPorcSEmpresa.Text = DetEmpresas.Rows[0]["porseguroempresa"].ToString();
                    //txtPorcEdcEmp.Text = DetEmpresas.Rows[0]["poreducacion"].ToString();
                    //txtSalarMaxSS.Text = DetEmpresas.Rows[0]["salariomaximoseguro"].ToString();
                    //txtMaxSSEmp.Text = DetEmpresas.Rows[0]["dmaxsegurodeduc"].ToString();
                    //txtMaxSSEmpr.Text = DetEmpresas.Rows[0]["dmaxsegurodeducE"].ToString();
                    //txtMinS4Sempl.Text = DetEmpresas.Rows[0]["dminsegurosem4"].ToString();
                    //txtMinS5Sem.Text = DetEmpresas.Rows[0]["dminsegurosem5"].ToString();
                    //txtValorS4Sem.Text = DetEmpresas.Rows[0]["dvalorSem4"].ToString();
                    //txtValorS5Sem.Text = DetEmpresas.Rows[0]["dvalorSem5"].ToString();
                    //txtMinS4Sempresa.Text = DetEmpresas.Rows[0]["dminseguroesem4"].ToString();
                    //txtMinS5Sempresa.Text = DetEmpresas.Rows[0]["dminseguroesem5"].ToString();
                    //txtValorSS4Semprs.Text = DetEmpresas.Rows[0]["dvalorseme4"].ToString();
                    //txtValorSS5Semprs.Text = DetEmpresas.Rows[0]["dvalorseme5"].ToString();
                    //txtFactCamb.Text = DetEmpresas.Rows[0]["factordecambio"].ToString();
                    chkVacPromedio.Checked = Convert.ToBoolean(DetEmpresas[0].promvac);
                    //btnGuardar.Visible = true;
                    //btnEliminar.Visible = true;
                    //btnAgregar.Visible = false;

                }
                
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            //if(txtNombEmprs.Text != "")
            //{
                
            //}

            //else
            //{
            //    this.alertValida.Visible = true;
            //    this.lblAlert.Visible = true;
            //    this.lblAlert.Text = "Favor Ingrese su criterio de busqueda";
            //}
        }

        //protected void btnGuardar_Click(object sender, EventArgs e)
        //{
        //    string user = Convert.ToString(this.Page.Session["usuario"]);

        //    if(validar())
        //    {
        //        if(Neg_Empresas.EditarEmpresa(txtNombEmprs.Text.Trim(), txtEmpresa.Text.Trim(), Convert.ToInt32(ddlPais.SelectedValue), "",
        //            Convert.ToInt32(ddlTipNomina.SelectedValue), Convert.ToDecimal(txtSalarMin.Text.Trim()), 
        //            0, 0, 0,
        //            0, 0, 0,0, 0,0,0, 0, 0,0, 0, 0,
        //            txtGerRrhh.Text.Trim(), user, false, false, chkVacPromedio.Checked, Convert.ToInt32(ddlMoneda.SelectedValue)))
        //            //if (Neg_Empresas.EditarEmpresa(txtNombEmprs.Text.Trim(), txtEmpresa.Text.Trim(), Convert.ToInt32(ddlPais.SelectedValue), txtIdioma.Text.Trim(),
        //            //Convert.ToInt32(ddlTipNomina.SelectedValue), Convert.ToDecimal(txtSalarMin.Text.Trim()),
        //            //Convert.ToDecimal(txtPorcSEmple.Text.Trim()), Convert.ToDecimal(txtPorcSEmpresa.Text.Trim()), Convert.ToDecimal(txtPorcEdcEmp.Text.Trim()),
        //            //Convert.ToDecimal(txtSalarMaxSS.Text.Trim()), Convert.ToDecimal(txtMaxSSEmp.Text.Trim()), Convert.ToDecimal(txtMaxSSEmpr.Text.Trim()), Convert.ToDecimal(txtMinS4Sempl.Text.Trim()), Convert.ToDecimal(txtMinS5Sem.Text.Trim()), Convert.ToDecimal(txtValorS4Sem.Text.Trim()),
        //            //Convert.ToDecimal(txtValorS5Sem.Text.Trim()), Convert.ToDecimal(txtMinS4Sempresa.Text.Trim()), Convert.ToDecimal(txtMinS5Sempresa.Text.Trim()), Convert.ToDecimal(txtValorSS4Semprs.Text.Trim()), Convert.ToDecimal(txtValorSS5Semprs.Text.Trim()), Convert.ToDecimal(txtFactCamb.Text.Trim()),
        //            //txtGerRrhh.Text.Trim(), user, ChkAntiguedad.Checked, chkRedondeo.Checked, chkVacPromedio.Checked))
        //            {
        //            alertValida.Visible = false;
        //            alertSucces.Visible = true;
        //            LblSuccess.Visible = true;
        //            LblSuccess.Text = "Actualizacion Satisfactoria";
        //            limpiar();
        //        }
        //        else
        //        {
        //            alertValida.Visible = true;
        //            lblAlert.Visible = true;
        //            lblAlert.Text = "EROR EN LA ACTUALIZACION";
        //        }
        //    }
        //}

        public bool validar()
        {
            if(txtEmpresa.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtEmpresa.Focus();
                return false;
            }
            if (txtGerRrhh.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtGerRrhh.Focus();
                return false;
            }
            //if (txtIdioma.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtIdioma.Focus();
            //    return false;
            //}
            if (txtSalarMin.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtSalarMin.Focus();
                return false;
            }
            //if (txtPorcSEmple.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtPorcSEmple.Focus();
            //    return false;
            //}
            //if (txtPorcSEmpresa.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtPorcSEmpresa.Focus();
            //    return false;
            //}
            //if (txtPorcEdcEmp.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtPorcEdcEmp.Focus();
            //    return false;
            //}
            //if (txtSalarMaxSS.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtSalarMaxSS.Focus();
            //    return false;
            //}
            //if (txtMaxSSEmp.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtMaxSSEmp.Focus();
            //    return false;
            //}
            //if (txtMaxSSEmpr.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtMaxSSEmpr.Focus();
            //    return false;
            //}
            //if (txtMinS4Sempl.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtMinS4Sempl.Focus();
            //    return false;
            //}
            //if (txtMinS5Sem.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtMinS5Sem.Focus();
            //    return false;
            //}
            //if (txtValorS4Sem.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtValorS4Sem.Focus();
            //    return false;
            //}
            //if (txtValorS5Sem.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtValorS5Sem.Focus();
            //    return false;
            //}
            //if (txtMinS4Sempresa.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtMinS4Sempresa.Focus();
            //    return false;
            //}
            //if (txtMinS5Sempresa.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtMinS5Sempresa.Focus();
            //    return false;
            //}
            //if (txtValorSS4Semprs.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtValorSS4Semprs.Focus();
            //    return false;
            //}
            //if (txtValorSS5Semprs.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtValorSS5Semprs.Focus();
            //    return false;
            //}
            //if (txtFactCamb.Text.Trim() == "")
            //{
            //    alertValida.Visible = true;
            //    lblAlert.Visible = true;
            //    lblAlert.Text = "Favor Ingrese un valor valido";
            //    txtFactCamb.Focus();
            //    return false;
            //}


            return true;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);

            if (validar())
            {
                if (Neg_Empresas.AgregarEmpresa(Convert.ToInt32(hfempresa.Value), txtEmpresa.Text.Trim(), Convert.ToInt32(ddlPais.SelectedValue), 
                   Convert.ToInt32(ddlTipNomina.SelectedValue), Convert.ToDecimal(txtSalarMin.Text.Trim()), 
                   txtGerRrhh.Text.Trim(), user, chkVacPromedio.Checked, Convert.ToInt32(ddlMoneda.SelectedValue)))
                    //if (Neg_Empresas.AgregarEmpresa(txtNombEmprs.Text.Trim(), txtEmpresa.Text.Trim(), Convert.ToInt32(ddlPais.SelectedValue), txtIdioma.Text.Trim(),
                    //Convert.ToInt32(ddlTipNomina.SelectedValue), Convert.ToDecimal(txtSalarMin.Text.Trim()), Convert.ToDecimal(txtPorcSEmple.Text.Trim()), Convert.ToDecimal(txtPorcSEmpresa.Text.Trim()), Convert.ToDecimal(txtPorcEdcEmp.Text.Trim()),
                    //Convert.ToDecimal(txtSalarMaxSS.Text.Trim()), Convert.ToDecimal(txtMaxSSEmp.Text.Trim()), Convert.ToDecimal(txtMaxSSEmpr.Text.Trim()), Convert.ToDecimal(txtMinS4Sempl.Text.Trim()), Convert.ToDecimal(txtMinS5Sem.Text.Trim()), Convert.ToDecimal(txtValorS4Sem.Text.Trim()),
                    //Convert.ToDecimal(txtValorS5Sem.Text.Trim()), Convert.ToDecimal(txtMinS4Sempresa.Text.Trim()), Convert.ToDecimal(txtMinS5Sempresa.Text.Trim()), Convert.ToDecimal(txtValorSS4Semprs.Text.Trim()), Convert.ToDecimal(txtValorSS5Semprs.Text.Trim()), Convert.ToDecimal(txtFactCamb.Text.Trim()),
                    //txtGerRrhh.Text.Trim(), user, ChkAntiguedad.Checked, chkRedondeo.Checked, chkVacPromedio.Checked))
                    {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Se Agrego Satisfactoriamente";
                    cargarDatosEmpresa();
                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "ERROR EN LA INSERCION";
                }
            }
        }

        //protected void btnEliminar_Click(object sender, EventArgs e)
        //{
        //    if(Neg_Empresas.EliminarEmpresa(txtEmpresa.Text.Trim()))
        //    {
        //        alertValida.Visible = false;
        //        alertSucces.Visible = true;
        //        LblSuccess.Visible = true;
        //        LblSuccess.Text = "Eliminado Satisfactoriamente";
        //        limpiar();
        //    }
        //    else
        //    {
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;
        //        lblAlert.Text = "ERROR EN LA ELIMINACION";
        //    }
        //}

        private void limpiar()
        {
            //txtNombEmprs.Text = "";
            txtEmpresa.Text = "";
            txtGerRrhh.Text = "";
            //txtIdioma.Text = "";
            txtSalarMin.Text = "";
            //txtPorcSEmple.Text = "";
            //txtPorcSEmpresa.Text = "";
            //txtPorcEdcEmp.Text = "";
            //txtSalarMaxSS.Text = "";
            //txtValorS4Sem.Text = "";
            //txtValorS5Sem.Text = "";
            //txtValorSS4Semprs.Text = "";
            //txtValorSS5Semprs.Text = "";
            //txtFactCamb.Text = "";
            //txtMaxSSEmp.Text = "";
            //txtMaxSSEmpr.Text = "";
            //txtMinS4Sempl.Text = "";
            //txtMinS4Sempresa.Text = "";
            //txtMinS5Sem.Text = "";
            //txtMinS5Sempresa.Text = "";
            //ChkAntiguedad.Checked = false;
            //chkRedondeo.Checked = false;
            chkVacPromedio.Checked = false;
        }
    }
}