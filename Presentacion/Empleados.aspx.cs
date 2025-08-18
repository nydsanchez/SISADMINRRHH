using Datos;
using Negocios;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

//using System.Data.SqlClient;

namespace NominaRRHH.Presentacion
{
   

    public partial class Empleados : System.Web.UI.Page
    {
        //private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
     

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        public string idUsuario;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                try
                {
                    // Recuperar el ID del usuario desde la sesión
                    object ob = this.Page.Session["usuario"];
                    if (ob != null)
                    {
                        idUsuario = ob.ToString(); // Asegúrate de convertirlo al tipo correcto
                                                             // Ahora puedes usar idUsuario según sea necesario
                        ///Response.Write($"ID del Usuario: {idUsuario}");
                    }
                    else
                    {
                        Response.Write("No se encontró el ID del usuario en la sesión.");
                    }

                    ObtenerPaises();
                    ObtenerDepartamentos(Convert.ToInt32(ddlPais.SelectedValue.Trim()));
                    obtenerMunicipios(ddlMunicipio, Convert.ToInt32(ddlDepartamento.SelectedValue.Trim()));
                    obtenerMunicipios(ddlDomiciMunicip, Convert.ToInt32(ddlDomiciDepto.SelectedValue.Trim()));
                    obtenerNivelAcademico();
                    obtenerUbicaciones();
                    obtenerProcesos();
                    obtenerMoneda();
                    obtenerEstadoCivil();
                    obtenerTipoCasa();
                    obtenerEstadoEmpl();
                    obtenerTipoContrato();
                    obtenerTipoSalario();
                    obtenerTurno();
                    obtenerCargo(Convert.ToInt32(ddlProceso.SelectedValue));
                    obtenerOperacion(Convert.ToInt32(ddlCargo.SelectedValue.Trim()));
                    txtSalario.ReadOnly = false;
                    Session["procesoA"] = "";
                    Session["procesoNameA"] = "";
                    Session["cargoA"] = "";
                    Session["operacionA"] = "";


                    // TODO: Victor Porras 11/11/2024
                    // Recuperar la cookie
                    HttpCookie cookie = Request.Cookies["Idusuario"];

                    if (cookie != null)
                    {
                        string codigoEmpleado = cookie.Value;
                        // Lista de valores permitidos
                        List<string> valoresPermitidos = new List<string> { "1015", "1006", "1059", "1073" };

                        // Verificamos si idUsuario está en la lista de valores permitidos
                        CheckBoxMFI.Visible = valoresPermitidos.Contains(codigoEmpleado);

                        //lblBienvenida.Text = "Bienvenido, empleado: " + codigoEmpleado;
                        // Aquí puedes continuar con más lógica usando codigoEmpleado
                    }
                    //object ob = this.Page.Session["Idusuario"];
                    //if (ob != null)
                    //{

                    //    // Convertimos el objeto a string
                    //    string idUsuario = ob.ToString();

                    //    // Lista de valores permitidos
                    //    List<string> valoresPermitidos = new List<string> { "1015", "1006", "1059", "1073" };

                    //    // Verificamos si idUsuario está en la lista de valores permitidos
                    //    CheckBoxMFI.Visible = valoresPermitidos.Contains(idUsuario);
                    //}
                    else
                    {
                        CheckBoxMFI.Visible = false; // Ocultamos el CheckBox si la sesión es nula

                        throw new NullReferenceException("La sesión 'Idusuario' es nula.");
                        // Manejar el caso donde no hay un usuario autenticado
                        // Response.Redirect("Login.aspx"); // Redirigir al usuario a la página de inicio de sesión
                    }
                    // End TODO:
                
                }
                catch (NullReferenceException ex)
                {
                    alertValida.Visible = true;
                    alertSucces.Visible = false;
                    // Registra el error para análisis posterior
                    //LogError(ex);

                    alertValida.InnerText = $"Ocurrió un error=> {ex.Message}"+ " ID Empleado {codigoEmpleado}";
                }
                catch (Exception ex)
                {
                    // Enviar un mensaje al usuario o registrar el error
                    alertValida.Visible = true; // Suponiendo que tienes un control para mostrar mensajes
                    alertSucces.Visible = false;
                    alertValida.InnerText = $"Ocurrió un error:: {ex.Message}"; // Muestra el mensaje de error
                }
            }

               
        }

        private void LogError(Exception ex)
        {
            string filePath = Server.MapPath("~/App_Data/ErrorLog.txt"); // Ruta del archivo
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
              
                writer.WriteLine("Fecha: " + DateTime.Now.ToString());
                writer.WriteLine("Mensaje: " + ex.Message);
                writer.WriteLine("StackTrace: " + ex.StackTrace);
                writer.WriteLine("--------------------------------------------------");
            }
        }
        private void obtenerTurno()
        {
            this.ddlTurno.DataSource = Neg_Catalogos.CargarTurno();
            this.ddlTurno.DataMember = "turno";
            this.ddlTurno.DataValueField = "codigo_turno";
            this.ddlTurno.DataTextField = "nombre_turno";
            this.ddlTurno.DataBind();
        }

        private void obtenerTipoSalario()
        {
            this.ddlTipoSalario.DataSource = Neg_Catalogos.CargarTipoSalario();
            this.ddlTipoSalario.DataMember = "tipo";
            this.ddlTipoSalario.DataValueField = "id_descripcion";
            this.ddlTipoSalario.DataTextField = "descripcion";
            this.ddlTipoSalario.DataBind();
            ddlTipoSalario.SelectedValue = "2";
        }

        private void obtenerTipoContrato()
        {
            this.ddlTipoContrato.DataSource = Neg_Catalogos.CargarTipoContrato();
            this.ddlTipoContrato.DataMember = "tipo";
            this.ddlTipoContrato.DataValueField = "id_descripcion";
            this.ddlTipoContrato.DataTextField = "descripcion";
            this.ddlTipoContrato.DataBind();
            ddlTipoContrato.SelectedValue = "1";
        }

        private void obtenerEstadoEmpl()
        {
            this.ddlEstadoEmpl.DataSource = Neg_Catalogos.CargarEstadoEmpleado();
            this.ddlEstadoEmpl.DataMember = "estado";
            this.ddlEstadoEmpl.DataValueField = "id_descripcion";
            this.ddlEstadoEmpl.DataTextField = "descripcion";
            this.ddlEstadoEmpl.DataBind();
        }

        private void obtenerTipoCasa()
        {
            this.ddlTipoCasa.DataSource = Neg_Catalogos.CargarTipoCasa();
            this.ddlTipoCasa.DataMember = "casa";
            this.ddlTipoCasa.DataValueField = "id_descripcion";
            this.ddlTipoCasa.DataTextField = "descripcion";
            this.ddlTipoCasa.DataBind();
        }

        private void obtenerEstadoCivil()
        {
            this.ddlEstadoCivil.DataSource = Neg_Catalogos.CargarEstadoCivil();
            this.ddlEstadoCivil.DataMember = "estado";
            this.ddlEstadoCivil.DataValueField = "id_descripcion";
            this.ddlEstadoCivil.DataTextField = "descripcion";
            this.ddlEstadoCivil.DataBind();
        }

        private void obtenerMoneda()
        {
            this.ddlMoneda.DataSource = Neg_Catalogos.CargarMonedas();
            this.ddlMoneda.DataMember = "monedas";
            this.ddlMoneda.DataValueField = "monedaId";
            this.ddlMoneda.DataTextField = "nombre_Moneda";
            this.ddlMoneda.DataBind();
        }

        private void obtenerProcesos()
        {
            this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }
        private void obtenerCargo(int codigo_depto)
        {
            this.ddlCargo.DataSource = Neg_Catalogos.CargarCargo(codigo_depto);
            this.ddlCargo.DataMember = "cargo";
            this.ddlCargo.DataValueField = "codigo_cargo";
            this.ddlCargo.DataTextField = "nombre_cargo";
            this.ddlCargo.DataBind();
        }

        private void obtenerUbicaciones()
        {
            this.ddlUbicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacion.DataMember = "ubicaciones";
            this.ddlUbicacion.DataValueField = "codigo_ubicacion";
            this.ddlUbicacion.DataTextField = "nombre_ubicacion";
            this.ddlUbicacion.DataBind();
        }

        private void obtenerNivelAcademico()
        {
            this.ddlNivelAcademico.DataSource = Neg_Catalogos.CargarNivelAcademico();
            this.ddlNivelAcademico.DataMember = "nivelacademico";
            this.ddlNivelAcademico.DataValueField = "id_descripcion";
            this.ddlNivelAcademico.DataTextField = "descripcion";
            this.ddlNivelAcademico.DataBind();
        }

        private void obtenerOperacion(int codigo_cargo)
        {
            this.ddlOperacion.DataSource = Neg_Catalogos.CargarOperaciones(codigo_cargo);
            this.ddlOperacion.DataMember = "operaciones";
            this.ddlOperacion.DataValueField = "codigo_operacion";
            this.ddlOperacion.DataTextField = "descripcion";
            this.ddlOperacion.DataBind();
        }

        private void obtenerMunicipios(DropDownList ddl, int idpadre)
        {
            ddl.DataSource = Neg_Catalogos.CargarMunicipios(idpadre);
            ddl.DataMember = "municipios";
            ddl.DataValueField = "id_descripcion";
            ddl.DataTextField = "descripcion";
            ddl.DataBind();

            //this.ddlDomiciMunicip.DataSource = Neg_Catalogos.CargarMunicipios(0);
            //this.ddlDomiciMunicip.DataMember = "municipios";
            //this.ddlDomiciMunicip.DataValueField = "id_descripcion";
            //this.ddlDomiciMunicip.DataTextField = "descripcion";
            //this.ddlDomiciMunicip.DataBind();
        }
        private void ObtenerDepartamentos(int idpadre)
        {
            DataSet dt= Neg_Catalogos.CargarDepartamentos(idpadre);
            ddlDepartamento.DataSource = dt;
            ddlDepartamento.DataMember = "departamentos";
            ddlDepartamento.DataValueField = "id_descripcion";
            ddlDepartamento.DataTextField = "descripcion";
            ddlDepartamento.DataBind();

            Neg_Empresas NEmpresas = new Neg_Empresas();
            dsPlanilla.dtEmpresaDataTable DetEmpresas = NEmpresas.ObtenerInfoDetEmpresas();

            ddlDomiciDepto.DataSource = Neg_Catalogos.CargarDepartamentos(DetEmpresas[0].idpais);
            ddlDomiciDepto.DataMember = "departamentos";
            ddlDomiciDepto.DataValueField = "id_descripcion";
            ddlDomiciDepto.DataTextField = "descripcion";
            ddlDomiciDepto.DataBind();
        }

        private void ObtenerPaises()
        {
            this.ddlPais.DataSource = Neg_Catalogos.CargarPaises();
            this.ddlPais.DataMember = "paises";
            this.ddlPais.DataValueField = "id_descripcion";
            ddlPais.DataTextField = "descripcion";
            this.ddlPais.DataBind();


        }

        private void ObtenerFoto(int codEmple)
        {
            GetImage(Neg_Catalogos.cargarFoto(codEmple));
        }

        public void GetImage(object img)
        {

            this.Image1.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
        }


        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            DataTable DetEmpleados = new DataTable();
            string user = Convert.ToString(this.Page.Session["usuario"]);
            try
            {
                if (txtCodEmp.Text != "")
                {
                    DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtCodEmp.Text);
                }
                else if (txtBuscarNombre.Text.Trim() != "")
                {
                    DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleadoxNombre(txtBuscarNombre.Text.Trim());
                }
                else
                {
                    throw new Exception("Debe ingresar codigo o nombre para realizar la busqueda");
                }

                if (DetEmpleados.Rows.Count > 0)
                {
                    txtCodEmp.Text = DetEmpleados.Rows[0]["codigo_empleado"].ToString();
                    txtBuscarNombre.Text = DetEmpleados.Rows[0]["nombrecompleto"].ToString();
                    txt1erNombre.Text = DetEmpleados.Rows[0]["primer_nombre"].ToString();
                    txt2doNombre.Text = DetEmpleados.Rows[0]["segundo_nombre"].ToString();
                    txt1erApellido.Text = DetEmpleados.Rows[0]["primer_apellido"].ToString();
                    txt2doApellido.Text = DetEmpleados.Rows[0]["segundo_apellido"].ToString();
                    ddlSexo.SelectedValue = DetEmpleados.Rows[0]["sexo"].ToString();
                    ddlPais.SelectedValue = DetEmpleados.Rows[0]["id_descripcion"].ToString();
                    ObtenerDepartamentos(Convert.ToInt32(ddlPais.SelectedValue));
                    txtNumInss.Text = DetEmpleados.Rows[0]["numero_seguro"].ToString();
                    txtFechaNac.Text = DetEmpleados.Rows[0]["fecha_nacimiento"].ToString();
                    txtCedula.Text = DetEmpleados.Rows[0]["cedula_identidad"].ToString();
                    txtEmite.Text = DetEmpleados.Rows[0]["emitecedula"].ToString();
                    txtVence.Text = DetEmpleados.Rows[0]["vencedula"].ToString();
                    ddlNivelAcademico.SelectedValue = DetEmpleados.Rows[0]["idnivelacademico"].ToString();
                    ddlDepartamento.SelectedValue = DetEmpleados.Rows[0]["iddepto"].ToString();
                    obtenerMunicipios(ddlMunicipio, Convert.ToInt32(ddlDepartamento.SelectedValue));
                    ddlMunicipio.SelectedValue = DetEmpleados.Rows[0]["idmunicipio"].ToString();
                    ddlUbicacion.SelectedValue = DetEmpleados.Rows[0]["codigo_ubicacion"].ToString();
                    ddlProceso.SelectedValue = DetEmpleados.Rows[0]["codigo_depto"].ToString();
                    Session["procesoA"] = ddlProceso.SelectedValue;
                    Session["procesoNameA"] = ddlProceso.SelectedItem.Text.Trim();
                    obtenerCargo(Convert.ToInt32(ddlProceso.SelectedValue));
                    ddlCargo.SelectedValue = DetEmpleados.Rows[0]["idcargo"].ToString();
                    Session["cargoA"] = ddlCargo.SelectedValue;
                    obtenerOperacion(Convert.ToInt32(ddlCargo.SelectedValue));
                    ddlOperacion.SelectedValue = DetEmpleados.Rows[0]["idoperacion"].ToString();
                    Session["operacionA"] = ddlOperacion.SelectedValue;
                    txt1erIngreso.Text = DetEmpleados.Rows[0]["fechaprimeringreso"].ToString();
                    txtFechaIngreso.Text = DetEmpleados.Rows[0]["fecha_ingreso"].ToString();
                    txtFechaEgreso.Text = DetEmpleados.Rows[0]["fecha_egreso"].ToString();
                    Session["fecha_ingreso"] = txtFechaIngreso.Text;
                    Session["fecha_egreso"] = txtFechaEgreso.Text;
                    ddlEstadoEmpl.SelectedValue = DetEmpleados.Rows[0]["idestadoEmpleado"].ToString();
                    TxtEstado.Text = ddlEstadoEmpl.SelectedItem.Text;
                    ddlMoneda.SelectedValue = DetEmpleados.Rows[0]["monedaId"].ToString();
                    txtSalario.Text = DetEmpleados.Rows[0]["salariomensual"].ToString();
                    txtSalario.ReadOnly = true;
                    txtCuentaContable.Text = DetEmpleados.Rows[0]["cuentacontable"].ToString();
                    txtSubsidio.Text = DetEmpleados.Rows[0]["subsidiodiario"].ToString();
                    txtCuentaBanc.Text = DetEmpleados.Rows[0]["cuentabancaria"].ToString();
                    TxtCuentaMall.Text = DetEmpleados.Rows[0]["cuentamall"].ToString();
                    txtIncentivo.Text = DetEmpleados.Rows[0]["incentivofijo"].ToString();
                    txtTelf.Text = DetEmpleados.Rows[0]["telefono"].ToString();
                    txtCel.Text = DetEmpleados.Rows[0]["celular"].ToString();
                    txtCorreo.Text = DetEmpleados.Rows[0]["email"].ToString();
                    ddlEstadoCivil.SelectedValue = DetEmpleados.Rows[0]["idestadocivil"].ToString();
                    ddlTipoCasa.SelectedValue = DetEmpleados.Rows[0]["idtipocasa"].ToString();
                    txtVive.Text = DetEmpleados.Rows[0]["vivecon"].ToString();
                    ddlDomiciDepto.SelectedValue = DetEmpleados.Rows[0]["iddomicdepto"].ToString();
                    obtenerMunicipios(ddlDomiciMunicip, Convert.ToInt32(ddlDomiciDepto.SelectedValue));                   
                    ddlDomiciMunicip.SelectedValue = DetEmpleados.Rows[0]["iddomicmunic"].ToString();
                    txtDireccion.Text = DetEmpleados.Rows[0]["direccion"].ToString();
                    txtNombEmerg.Text = DetEmpleados.Rows[0]["nombreemergencia"].ToString();
                    txtTelEmerg.Text = DetEmpleados.Rows[0]["telefonoemergencia"].ToString();
                    txtParentescEmerg.Text = DetEmpleados.Rows[0]["parentesco"].ToString();
                    txtDireccEmerg.Text = DetEmpleados.Rows[0]["direccionemergencia"].ToString();
                    txtNombPad.Text = DetEmpleados.Rows[0]["nombre_papa"].ToString();
                    txtNumCedulPadre.Text = DetEmpleados.Rows[0]["cedula_papa"].ToString();
                    ddlVivPad.SelectedValue = DetEmpleados.Rows[0]["vivepapa"].ToString();
                    txtNombMad.Text = DetEmpleados.Rows[0]["nombre_mama"].ToString();
                    txtNumCedulMadre.Text = DetEmpleados.Rows[0]["cedula_mama"].ToString();
                    ddlVivMad.SelectedValue = DetEmpleados.Rows[0]["vivemama"].ToString();
                    txtNombEspos.Text = DetEmpleados.Rows[0]["nombre_espos"].ToString();
                    txtNumCedulEsps.Text = DetEmpleados.Rows[0]["cedula_espos"].ToString();
                    txtNombH1.Text = DetEmpleados.Rows[0]["hijo1"].ToString();
                    txtLugNacH1.Text = DetEmpleados.Rows[0]["lugarnacehijo1"].ToString();
                    txtFechaNacH1.Text = DetEmpleados.Rows[0]["fechanacehijo1"].ToString();
                    ddlSexoH1.SelectedValue = DetEmpleados.Rows[0]["sexohijo1"].ToString();
                    txtNombH2.Text = DetEmpleados.Rows[0]["hijo2"].ToString();
                    txtLugNacH2.Text = DetEmpleados.Rows[0]["lugarnacehijo2"].ToString();
                    txtFechaNacH2.Text = DetEmpleados.Rows[0]["fechanacehijo2"].ToString();
                    ddlSexoH2.SelectedValue = DetEmpleados.Rows[0]["sexohijo2"].ToString();
                    txtNombH3.Text = DetEmpleados.Rows[0]["hijo3"].ToString();
                    txtLugNacH3.Text = DetEmpleados.Rows[0]["lugarnacehijo3"].ToString();
                    txtFechaNacH3.Text = DetEmpleados.Rows[0]["fechanacehijo3"].ToString();
                    ddlSexoH3.SelectedValue = DetEmpleados.Rows[0]["sexohijo3"].ToString();
                    txtNombH4.Text = DetEmpleados.Rows[0]["hijo4"].ToString();
                    txtLugNacH4.Text = DetEmpleados.Rows[0]["lugarnacehijo4"].ToString();
                    txtFechaNacH4.Text = DetEmpleados.Rows[0]["fechanacehijo4"].ToString();
                    ddlSexoH4.SelectedValue = DetEmpleados.Rows[0]["sexohijo4"].ToString();
                    txtEmpAnterior.Text = DetEmpleados.Rows[0]["NombEmpAnt1"].ToString();
                    ddlVerfEmAnt1.SelectedValue = DetEmpleados.Rows[0]["verifEmpAnt1"].ToString();
                    txtObservEmpAant1.Text = DetEmpleados.Rows[0]["observAnt1"].ToString();
                    txtUltSalRef1.Text = DetEmpleados.Rows[0]["ultSalEmpAnt1"].ToString();
                    ddlVerfUltSalAnt1.SelectedValue = DetEmpleados.Rows[0]["verifUltEmp1"].ToString();
                    txtObservSalAnt1.Text = DetEmpleados.Rows[0]["observUltSal1"].ToString();
                    txtCargUltRef1.Text = DetEmpleados.Rows[0]["cargoAnt1"].ToString();
                    ddlVerfUltCargRef1.SelectedValue = DetEmpleados.Rows[0]["verifCargoAnt1"].ToString();
                    txtObservCargAnt1.Text = DetEmpleados.Rows[0]["observUltCarg1"].ToString();
                    txtMotvSalidRef1.Text = DetEmpleados.Rows[0]["motvSal1"].ToString();
                    txtObservMotSal1.Text = DetEmpleados.Rows[0]["obsevMotvSal1"].ToString();
                    txtFechIngresRef1.Text = DetEmpleados.Rows[0]["fechaIngrEmp1"].ToString();
                    ddlVerFechIng1.SelectedValue = DetEmpleados.Rows[0]["VerffechaIngrEmp1"].ToString();
                    txtObservFecIng1.Text = DetEmpleados.Rows[0]["ObservfechaIngrEmp1"].ToString();
                    txtFechEgres1.Text = DetEmpleados.Rows[0]["fechaEgresEmp1"].ToString();
                    ddlVerfFechEgr1.SelectedValue = DetEmpleados.Rows[0]["VerffechaEgresEmp1"].ToString();
                    txtObservEgres1.Text = DetEmpleados.Rows[0]["ObservfechaEgresEmp1"].ToString();
                    ddlVerMtvSal1.SelectedValue = DetEmpleados.Rows[0]["verfMotvSal1"].ToString();
                    txtEmpAnt2.Text = DetEmpleados.Rows[0]["EmpAnt2"].ToString();
                    txtUltSalEmp2.Text = DetEmpleados.Rows[0]["SalrEmpAnt2"].ToString();
                    txtCargoEmp2.Text = DetEmpleados.Rows[0]["CargoEmpAnt2"].ToString();
                    txtMotivoSaldEmp2.Text = DetEmpleados.Rows[0]["MSalidaEmpAnt2"].ToString();
                    txtFechIngEmp2.Text = DetEmpleados.Rows[0]["fechaIngrsEmp2"].ToString();
                    txtFechaEgrsEmp2.Text = DetEmpleados.Rows[0]["fechaEgresoEmp2"].ToString();
                    txtRefPers.Text = DetEmpleados.Rows[0]["referenciapersonal"].ToString();
                    ddlVerifRefPers.SelectedValue = DetEmpleados.Rows[0]["verirefper1"].ToString();
                    txtObservRefPers.Text = DetEmpleados.Rows[0]["obsrefpersonal"].ToString();
                    txtparentRefPers.Text = DetEmpleados.Rows[0]["parentescopersonal"].ToString();
                    ddlVerifParentRefPers.SelectedValue = DetEmpleados.Rows[0]["verirefper2"].ToString();
                    txtObservParentRefPers.Text = DetEmpleados.Rows[0]["obsparentescopersonal"].ToString();
                    txtNumTelfRefPers.Text = DetEmpleados.Rows[0]["numeroreferpersonal"].ToString();
                    ddlVerNumTelRefPers.SelectedValue = DetEmpleados.Rows[0]["verirefper3"].ToString();
                    txtObservNumTelRefPers.Text = DetEmpleados.Rows[0]["obsnumrefpersonal"].ToString();
                    txtTiempConRefPers.Text = DetEmpleados.Rows[0]["tiemporeferpersonal"].ToString();
                    ddlVerTiempCncRefPers.SelectedValue = DetEmpleados.Rows[0]["verirefper4"].ToString();
                    txtObservTiempCncRefPers.Text = DetEmpleados.Rows[0]["obstiemporefpersonal"].ToString();
                    txtTipoEnfermedad.Text = DetEmpleados.Rows[0]["Descripcion_enfermedad"].ToString();
                    txtDese.Text = DetEmpleados.Rows[0]["cuando_enfermedad"].ToString();
                    txtDescEstudios.Text = DetEmpleados.Rows[0]["descestudia"].ToString();
                    txtCentroEstudios.Text = DetEmpleados.Rows[0]["dondeestudia"].ToString();
                    //txtFechaHistIng.Text = DetEmpleados.Rows[0]["histoFechaIngreso"].ToString();
                    //txtFechaHistEgrs.Text = DetEmpleados.Rows[0]["histoFechaEgreso"].ToString();
                    //txtUbicEgrs.Text = DetEmpleados.Rows[0]["histoUbicacion"].ToString();
                    //txtHistoDptoEgresos.Text = DetEmpleados.Rows[0]["histodepto"].ToString();
                    //txtHistCargoEgresos.Text = DetEmpleados.Rows[0]["histocargo"].ToString();
                    //txtHistMotivoEgreos.Text = DetEmpleados.Rows[0]["histomotivo"].ToString();
                    ddlTipoContrato.SelectedValue = DetEmpleados.Rows[0]["idtipocontrato"].ToString();
                    ddlTipoSalario.SelectedValue = DetEmpleados.Rows[0]["idtiposalario"].ToString();
                    ddlLiquidado.SelectedValue = DetEmpleados.Rows[0]["liquidado"].ToString();
                    ChkMarca.Checked = Convert.ToBoolean(DetEmpleados.Rows[0]["marca"].ToString());
                    ChkExtras.Checked = Convert.ToBoolean(DetEmpleados.Rows[0]["ganaextras"].ToString());
                    this.chkpagovac.Checked = Convert.ToBoolean(DetEmpleados.Rows[0]["pagovacacion"].ToString());
                    this.chkCredito.Checked = Convert.ToBoolean(DetEmpleados.Rows[0]["aplicaCredito"].ToString());
                    ///multitarea
                    chkMultiTarea.Checked = Convert.ToBoolean(DetEmpleados.Rows[0]["Multitarea"].ToString());
                    chkflexitime.Checked = Convert.ToBoolean(DetEmpleados.Rows[0]["flexitime"].ToString());
                    this.ckdiscapacidad.Checked = Convert.ToBoolean(DetEmpleados.Rows[0]["discapacidad"].ToString());
                    ddlPadEnfermedad.SelectedValue = DetEmpleados.Rows[0]["padece_enfermedad"].ToString();
                    ddlTurno.SelectedValue = DetEmpleados.Rows[0]["idturno"].ToString();
                    txtJefeInmed.Text = DetEmpleados.Rows[0]["Jefe"].ToString();
                    txtObservEmpl.Text = DetEmpleados.Rows[0]["observacion"].ToString();

                    ObtenerFoto(Convert.ToInt32(txtCodEmp.Text));
                    ObtenerHistoricoSalario(Convert.ToInt32(txtCodEmp.Text));
                    ObtenerHistoricoEgresos(Convert.ToInt32(txtCodEmp.Text));

                    this.alertValida.Visible = false;
                    this.lblAlert.Visible = false;

                    btnAgregar.Visible = false;
                    HabilitarBotones(ddlEstadoEmpl.SelectedValue.Trim());
                }
                else
                {

                    this.alertValida.Visible = true;
                    this.lblAlert.Visible = true;
                    this.lblAlert.Text = "No hay Datos para su busqueda";
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
        void HabilitarBotones(string filtro)
        {
            if (filtro == "1")//activo
            {
                btnEstatusLiq.Visible = true;
                BtnRenovar.Visible = true;
                btnGuardar.Visible = true;
                btnReingresar.Visible = false;
            }
            else if (filtro == "3")
            {//liquidado
                btnEstatusLiq.Visible = false;
                BtnRenovar.Visible = false;
                btnGuardar.Visible = true;
                btnReingresar.Visible = false;
            }
            else if(filtro == "0")//inactivo
            {
                btnEstatusLiq.Visible = false;
                BtnRenovar.Visible = false;
                btnGuardar.Visible = false;
                btnReingresar.Visible = true;
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            try
            {
                if (validar())
                {
                    int codEmpleado = Convert.ToInt32(this.txtCodEmp.Text.Trim());
                    string primerNomb = this.txt1erNombre.Text.Trim();
                    string segundoNomb = this.txt2doNombre.Text.Trim();
                    string primerApellido = this.txt1erApellido.Text.Trim();
                    string segundApellido = this.txt2doApellido.Text.Trim();
                    string nombreCompleto = primerNomb + " " + segundoNomb + " " + primerApellido + " " + segundApellido;
                    string sexo = ddlSexo.SelectedValue;
                    int pais = Convert.ToInt32(ddlPais.SelectedValue);
                    int departamento = Convert.ToInt32(ddlDepartamento.SelectedValue);
                    int municipio = Convert.ToInt32(ddlMunicipio.SelectedValue);
                    DateTime fechaNac = Convert.ToDateTime(this.txtFechaNac.Text.Trim());
                    string numCedula = this.txtCedula.Text.Trim();
                    DateTime emiteCed = Convert.ToDateTime(this.txtEmite.Text.Trim());
                    DateTime venceCed = Convert.ToDateTime(this.txtVence.Text.Trim());
                    string operacion = ddlOperacion.SelectedValue;
                    int nivelAcademico = Convert.ToInt32(ddlNivelAcademico.SelectedValue);
                    string numInss = this.txtNumInss.Text.Trim();
                    int ubicacion = Convert.ToInt32(ddlUbicacion.SelectedValue);
                    int proceso = Convert.ToInt32(ddlProceso.SelectedValue);
                    int cargo = Convert.ToInt32(ddlCargo.SelectedValue);
                    DateTime primerIngreso = Convert.ToDateTime(this.txt1erIngreso.Text.Trim());
                    int estadoEmpl = Convert.ToInt32(ddlEstadoEmpl.SelectedValue);
                    DateTime fechaIngr = Convert.ToDateTime(this.txtFechaIngreso.Text.Trim());
                    DateTime fechaEgrs = Convert.ToDateTime(this.txtFechaEgreso.Text.Trim());
                    int moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
                    decimal salario = Convert.ToDecimal(this.txtSalario.Text.Trim());
                    string cuentaContable = this.txtCuentaContable.Text.Trim();
                    decimal subsidio = Convert.ToDecimal(this.txtSubsidio.Text.Trim());
                    string cuentaBanc = this.txtCuentaBanc.Text.Trim();
                    string cuentaMall = this.TxtCuentaMall.Text.Trim();
                    decimal incentivo = Convert.ToDecimal(this.txtIncentivo.Text.Trim());
                    int turno = Convert.ToInt32(ddlTurno.SelectedValue);
                    int tipoContrato = Convert.ToInt32(ddlTipoContrato.SelectedValue);
                    int tipoSalario = Convert.ToInt32(ddlTipoSalario.SelectedValue);
                    int liquidado = Convert.ToInt32(ddlLiquidado.SelectedValue);
                    bool marca = Convert.ToBoolean(ChkMarca.Checked);
                    bool ganaExtras = Convert.ToBoolean(ChkExtras.Checked);
                    bool pagovacacion = Convert.ToBoolean(this.chkpagovac.Checked);
                    bool credito = Convert.ToBoolean(this.chkCredito.Checked);
                    //multitarea
                    bool MultiTarea = Convert.ToBoolean(this.chkMultiTarea.Checked);
                    bool flexitime = Convert.ToBoolean(this.chkflexitime.Checked);
                    bool discapacidad = Convert.ToBoolean(this.ckdiscapacidad.Checked);
                    string telefono = txtTelf.Text.Trim();
                    string celular = txtCel.Text.Trim();
                    string correo = txtCorreo.Text.Trim();
                    int estadoCivil = Convert.ToInt32(ddlEstadoCivil.SelectedValue);
                    int tipoCasa = Convert.ToInt32(ddlTipoCasa.SelectedValue);
                    string viveCon = txtVive.Text.Trim();
                    int domicdepto = Convert.ToInt32(ddlDomiciDepto.SelectedValue);
                    int domiciMunc = Convert.ToInt32(ddlDomiciMunicip.SelectedValue);
                    string direccion = txtDireccion.Text.Trim();
                    string nombreEmerg = this.txtNombEmerg.Text.Trim();
                    string telfEmerg = this.txtTelEmerg.Text.Trim();
                    string parentesco = this.txtParentescEmerg.Text.Trim();
                    string direcEmerg = this.txtDireccEmerg.Text.Trim();
                    string nombPadre = this.txtNombPad.Text.Trim();
                    string numCedPadre = this.txtNumCedulPadre.Text.Trim();
                    string vivePadre = ddlVivPad.SelectedValue;
                    string nombMadre = this.txtNombMad.Text.Trim();
                    string numCedMadre = this.txtNumCedulMadre.Text.Trim();
                    string viveMadre = ddlVivMad.SelectedValue;
                    string nombEsps = this.txtNombEspos.Text.Trim();
                    string numCedEsps = this.txtNumCedulEsps.Text.Trim();
                    string nombreHijo1 = this.txtNombH1.Text.Trim();
                    string lugarNacH1 = this.txtLugNacH1.Text.Trim();
                    DateTime fechaNacH1 = Convert.ToDateTime(this.txtFechaNacH1.Text.Trim());
                    string sexoH1 = ddlSexoH1.SelectedValue;
                    string nombreHijo2 = this.txtNombH2.Text.Trim();
                    string lugarNacH2 = this.txtLugNacH2.Text.Trim();
                    DateTime fechaNacH2 = Convert.ToDateTime(this.txtFechaNacH2.Text.Trim());
                    string sexoH2 = ddlSexoH2.SelectedValue;
                    string nombreHijo3 = this.txtNombH3.Text.Trim();
                    string lugarNacH3 = this.txtLugNacH3.Text.Trim();
                    DateTime fechaNacH3 = Convert.ToDateTime(this.txtFechaNacH3.Text.Trim());
                    string sexoH3 = ddlSexoH3.SelectedValue;
                    string nombreHijo4 = this.txtNombH4.Text.Trim();
                    string lugarNacH4 = this.txtLugNacH4.Text.Trim();
                    DateTime fechaNacH4 = Convert.ToDateTime(this.txtFechaNacH4.Text.Trim());
                    string sexoH4 = ddlSexoH4.SelectedValue;
                    string empAnterior1 = this.txtEmpAnterior.Text.Trim();
                    string verfEmpAnt1 = ddlVerfEmAnt1.SelectedValue;
                    string observEmpAnt1 = txtObservEmpAant1.Text.Trim();
                    decimal ultimoSalarioEmp1 = Convert.ToDecimal(txtUltSalRef1.Text.Trim());
                    string verfUltSalaEm1 = ddlVerfUltSalAnt1.SelectedValue;
                    string observSalAntEm1 = this.txtObservSalAnt1.Text.Trim();
                    string cargoEmp1 = this.txtCargUltRef1.Text.Trim();
                    string verfCargoEmp1 = ddlVerfUltCargRef1.SelectedValue;
                    string observUltCargoEmp1 = this.txtObservCargAnt1.Text.Trim();
                    string motivoSalidaEmp1 = this.txtMotvSalidRef1.Text.Trim();
                    string verfMotvSal1 = ddlVerMtvSal1.SelectedValue;
                    string observMotvSal1 = txtObservMotSal1.Text.Trim();
                    DateTime fechaIngEmp1 = Convert.ToDateTime(txtFechIngresRef1.Text.Trim());
                    string verfFechaIngEm1 = ddlVerFechIng1.SelectedValue;
                    string observFechaIngEm1 = txtObservFecIng1.Text.Trim();
                    DateTime fechaEgrEmp1 = Convert.ToDateTime(txtFechEgres1.Text.Trim());
                    string verfFechEgrEm1 = ddlVerfFechEgr1.SelectedValue;
                    string obsercFechEgrEm1 = txtObservEgres1.Text.Trim();
                    string empAnterior2 = txtEmpAnt2.Text.Trim();
                    decimal UltSalarioEmp2 = Convert.ToDecimal(txtUltSalEmp2.Text.Trim());
                    string ultCargoEmp2 = txtCargoEmp2.Text.Trim();
                    string motSalidEmp2 = txtMotivoSaldEmp2.Text.Trim();
                    DateTime fechaIngEmp2 = Convert.ToDateTime(txtFechIngEmp2.Text.Trim());
                    DateTime fechaEgrsEmp2 = Convert.ToDateTime(txtFechaEgrsEmp2.Text.Trim());
                    string referenciaPers = txtRefPers.Text.Trim();
                    string verfRefPers = ddlVerifRefPers.SelectedValue;
                    string observRefPers = txtObservRefPers.Text.Trim();
                    string parentsRefPers = txtparentRefPers.Text.Trim();
                    string verfParentRef = ddlVerifParentRefPers.SelectedValue;
                    string observRefParnRef = txtObservParentRefPers.Text.Trim();
                    string numTelfRefPers = txtNumTelfRefPers.Text.Trim();
                    string verfNumTelfRef = ddlVerNumTelRefPers.SelectedValue;
                    string observNumTelRef = txtObservNumTelRefPers.Text.Trim();
                    string tiempoConcRef = txtTiempConRefPers.Text.Trim();
                    string verfTiempCncRef = ddlVerTiempCncRefPers.SelectedValue;
                    string observTiempCncRef = txtObservTiempCncRefPers.Text.Trim();
                    string padeceEnfermedad = ddlPadEnfermedad.SelectedValue;
                    string tipoEnfermedad = txtTipoEnfermedad.Text.Trim();
                    string enfermedadDesde = txtDese.Text.Trim();
                    string descEstudios = txtDescEstudios.Text.Trim();
                    string centroEstudios = txtCentroEstudios.Text.Trim();
                    //string fechIngHistoEgr = string.IsNullOrEmpty(txtFechaHistIng.Text.Trim()) ? "01/01/1900" : txtFechaHistIng.Text.Trim();
                    //string fechaEgrsHistoEg = string.IsNullOrEmpty(txtFechaHistEgrs.Text.Trim()) ? "01/01/1900" : txtFechaHistEgrs.Text.Trim();
                    //string ubicHistoEgrs = txtUbicEgrs.Text.Trim();
                    //string deptoHistoEgrs = txtHistoDptoEgresos.Text.Trim();
                    //string cargoHistoEgrs = txtHistCargoEgresos.Text.Trim();
                    //string motvHistoEgrs = txtHistMotivoEgreos.Text.Trim();
                    string nombreJefe = txtJefeInmed.Text.Trim();

                    // Recuperar el ID del usuario desde la sesión
                    object ob = this.Page.Session["usuario"];
                    if (ob != null)
                    {
                        idUsuario = ob.ToString(); // Asegúrate de convertirlo al tipo correcto
                                                   // Ahora puedes usar idUsuario según sea necesario
                        ///Response.Write($"ID del Usuario: {idUsuario}");
                    }
                    else
                    {
                        Response.Write("No se encontró el ID del usuario en la sesión.");
                    }
                    // Log
                    //Log.Information("Datos del empleado con código {CodEmpleado} actualizados exitosamente.", codEmpleado);
                    //logger.Info("Empleados Actualizado. No. "+ codEmpleado +", Usuario: "+ idUsuario);

                    // Metodo para editar informacion de empleado
                    Neg_Empleados.InfoEmpleadoEditar(codEmpleado, primerNomb, segundoNomb, primerApellido, segundApellido, nombreCompleto.ToUpper(), sexo, pais, departamento, municipio,
                        fechaNac, numCedula, emiteCed, venceCed, operacion, nivelAcademico, numInss, ubicacion, proceso, cargo, primerIngreso, estadoEmpl, fechaIngr, fechaEgrs,
                        moneda, salario, cuentaContable, subsidio, cuentaBanc, incentivo, turno, tipoContrato, tipoSalario, liquidado, marca, ganaExtras, telefono, celular, correo,
                        estadoCivil, tipoCasa, viveCon, domiciMunc, direccion, nombreEmerg, telfEmerg, parentesco, direcEmerg, nombPadre, numCedPadre, vivePadre,
                        nombMadre, numCedMadre, viveMadre, nombEsps, numCedEsps, nombreHijo1, lugarNacH1, fechaNacH1, sexoH1, nombreHijo2, lugarNacH2, fechaNacH2, sexoH2, nombreHijo3,
                        lugarNacH3, fechaNacH3, sexoH3, nombreHijo4, lugarNacH4, fechaNacH4, sexoH4, empAnterior1, verfEmpAnt1, observEmpAnt1, ultimoSalarioEmp1, verfUltSalaEm1, observSalAntEm1,
                        cargoEmp1, verfCargoEmp1, observUltCargoEmp1, motivoSalidaEmp1, verfMotvSal1, observMotvSal1, fechaIngEmp1, verfFechaIngEm1, observFechaIngEm1, fechaEgrEmp1, verfFechEgrEm1,
                        obsercFechEgrEm1, empAnterior2, UltSalarioEmp2, ultCargoEmp2, motSalidEmp2, fechaIngEmp2, fechaEgrsEmp2, referenciaPers, verfRefPers, observRefPers, parentsRefPers, verfParentRef,
                        observRefParnRef, numTelfRefPers, verfNumTelfRef, observNumTelRef, tiempoConcRef, verfTiempCncRef, observTiempCncRef, padeceEnfermedad, tipoEnfermedad, enfermedadDesde, descEstudios,
                        centroEstudios, //fechIngHistoEgr, fechaEgrsHistoEg, ubicHistoEgrs, deptoHistoEgrs, cargoHistoEgrs, motvHistoEgrs, 
                        nombreJefe, txtObservEmpl.Text.Trim(), user, pagovacacion, discapacidad, cuentaMall, flexitime, credito,domicdepto,MultiTarea);
                    {
                        //fechas ingreso egreso
                        Session["fecha_ingreso"] = txtFechaIngreso.Text;
                        Session["fecha_egreso"] = txtFechaEgreso.Text;
                        txtSalario.ReadOnly = true;
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        LblSuccess.Text = "Actualizacion Satisfactoria";

                        HttpFileCollection files = Request.Files;
                        foreach (string fileTagName in files)
                        {
                            HttpPostedFile file = Request.Files[fileTagName];
                            string fileExtention = file.ContentType;
                            if (file.ContentLength > 0)
                            {
                                if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                                {
                                    if (file.ContentLength > 9288)
                                    {
                                        int size = file.ContentLength;
                                        string name = file.FileName;
                                        int tipo = 2;

                                        int position = name.LastIndexOf("\\");
                                        name = name.Substring(position + 1);
                                        string contentType = file.ContentType;
                                        //byte[] fileData = new byte[size];
                                        byte[] fileData = new byte[size];
                                        //ResizeImageFile(fileData, 300);
                                        int numemp = Convert.ToInt32(txtCodEmp.Text.Trim());
                                        file.InputStream.Read(fileData, 0, size);
                                        IUserDetail userDetail = UserDetailResolver.getUserDetail();
                                        int idEmpresa = userDetail.getIDEmpresa();
                                        Neg_Empleados.SaveFile(name, fileData, numemp, tipo, idEmpresa);
                                    }
                                }
                            }
                            
                        }
                        limpiar();
                    }
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
        //Limpiar campos catalogo de empleados
        private void limpiar()
        {
            this.Image1.ImageUrl = "";
            this.txtCodEmp.Text = "";
            this.txt1erNombre.Text = "";
            this.txt2doNombre.Text = "";
            this.txt1erApellido.Text = "";
            this.txt2doApellido.Text = "";
            this.txtFechaNac.Text = "";
            this.txtCedula.Text = "";
            this.txtEmite.Text = "";
            this.txtVence.Text = "";
            this.txtNumInss.Text = "";
            this.txt1erIngreso.Text = "";
            this.txtFechaIngreso.Text = "";
            this.txtFechaEgreso.Text = "";
            this.txtSalario.Text = "";
            this.txtCuentaContable.Text = "";
            this.txtSubsidio.Text = "";
            this.txtCuentaBanc.Text = "";
            this.TxtCuentaMall.Text = "";
            this.txtIncentivo.Text = "";
            ChkMarca.Checked = true;
            chkCredito.Checked = true;
            chkpagovac.Checked = false;
            this.ckdiscapacidad.Checked = false;
            txtTelf.Text = "";
            txtCel.Text = "";
            txtCorreo.Text = "";
            txtVive.Text = "";
            txtDireccion.Text = "";
            this.txtNombEmerg.Text = "";
            this.txtTelEmerg.Text = "";
            this.txtParentescEmerg.Text = "";
            this.txtDireccEmerg.Text = "";
            this.txtNombPad.Text = "";
            this.txtNumCedulPadre.Text = "";
            this.txtNombMad.Text = "";
            this.txtNumCedulMadre.Text = "";
            this.txtNombEspos.Text = "";
            this.txtNumCedulEsps.Text = "";
            this.txtNombH1.Text = "";
            this.txtLugNacH1.Text = "";
            this.txtNombH2.Text = "";
            this.txtLugNacH2.Text = "";
            this.txtNombH3.Text = "";
            this.txtLugNacH3.Text = "";
            this.txtNombH4.Text = "";
            this.txtLugNacH4.Text = "";
            this.txtEmpAnterior.Text = "";
            txtObservEmpAant1.Text = "";
            txtUltSalRef1.Text = "";
            this.txtObservSalAnt1.Text = "";
            this.txtCargUltRef1.Text = "";
            this.txtObservCargAnt1.Text = "";
            this.txtMotvSalidRef1.Text = "";
            txtObservMotSal1.Text = "";
            txtFechIngresRef1.Text = "";
            txtObservFecIng1.Text = "";
            txtFechEgres1.Text = "";
            txtObservEgres1.Text = "";
            txtEmpAnt2.Text = "";
            txtUltSalEmp2.Text = "";
            txtCargoEmp2.Text = "";
            txtMotivoSaldEmp2.Text = "";
            txtFechIngEmp2.Text = "";
            txtFechaEgrsEmp2.Text = "";
            txtRefPers.Text = "";
            txtObservRefPers.Text = "";
            txtparentRefPers.Text = "";
            txtObservParentRefPers.Text = "";
            txtNumTelfRefPers.Text = "";
            txtObservNumTelRefPers.Text = "";
            txtTiempConRefPers.Text = "";
            txtObservTiempCncRefPers.Text = "";
            txtTipoEnfermedad.Text = "";
            txtDese.Text = "";
            txtDescEstudios.Text = "";
            txtCentroEstudios.Text = "";
            //txtFechaHistIng.Text = "";
            //txtFechaHistEgrs.Text = "";
            //txtUbicEgrs.Text = "";
            //txtHistoDptoEgresos.Text = "";
            //txtHistCargoEgresos.Text = "";
            //txtHistMotivoEgreos.Text = "";
            txtJefeInmed.Text = "";
            txtObservEmpl.Text = "";
            btnGuardar.Visible = false;
            btnAgregar.Visible = true;
            txtSalario.ReadOnly = false;
        }
        //Agregar empleado
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string user = Convert.ToString(this.Page.Session["usuario"]);
            try
            {
                if (validar())
                {
                    string primerNomb = this.txt1erNombre.Text.Trim();
                    string segundoNomb = this.txt2doNombre.Text.Trim();
                    string primerApellido = this.txt1erApellido.Text.Trim();
                    string segundApellido = this.txt2doApellido.Text.Trim();
                    string nombreCompleto = primerNomb + " " + segundoNomb + " " + primerApellido + " " + segundApellido;
                    string sexo = ddlSexo.SelectedValue;
                    int pais = Convert.ToInt32(ddlPais.SelectedValue);
                    int departamento = Convert.ToInt32(ddlDepartamento.SelectedValue);
                    int municipio = Convert.ToInt32(ddlMunicipio.SelectedValue);
                    DateTime fechaNac = Convert.ToDateTime(this.txtFechaNac.Text.Trim());
                    string numCedula = this.txtCedula.Text.Trim();
                    DateTime emiteCed = Convert.ToDateTime(this.txtEmite.Text.Trim());
                    DateTime venceCed = Convert.ToDateTime(this.txtVence.Text.Trim());
                    string operacion = ddlOperacion.SelectedValue;
                    int nivelAcademico = Convert.ToInt32(ddlNivelAcademico.SelectedValue);
                    string numInss = this.txtNumInss.Text.Trim();
                    int ubicacion = Convert.ToInt32(ddlUbicacion.SelectedValue);
                    int proceso = Convert.ToInt32(ddlProceso.SelectedValue);
                    int cargo = Convert.ToInt32(ddlCargo.SelectedValue);
                    DateTime primerIngreso = Convert.ToDateTime(this.txt1erIngreso.Text.Trim());
                    int estadoEmpl = 1;//Nuevo empleado siempre activo
                    ddlEstadoEmpl.SelectedValue = "1";
                    TxtEstado.Text = ddlEstadoEmpl.SelectedItem.Text;
                    DateTime fechaIngr = Convert.ToDateTime(this.txtFechaIngreso.Text.Trim());
                    DateTime fechaEgrs = Convert.ToDateTime(this.txtFechaEgreso.Text.Trim());
                    int moneda = Convert.ToInt32(ddlMoneda.SelectedValue);
                    decimal salario = Convert.ToDecimal(this.txtSalario.Text.Trim());
                    string cuentaContable = this.txtCuentaContable.Text.Trim();
                    decimal subsidio = Convert.ToDecimal(this.txtSubsidio.Text.Trim());
                    string cuentaBanc = this.txtCuentaBanc.Text.Trim();
                    string cuentaMall = this.TxtCuentaMall.Text.Trim();
                    decimal incentivo = Convert.ToDecimal(this.txtIncentivo.Text.Trim());
                    int turno = Convert.ToInt32(ddlTurno.SelectedValue);
                    int tipoContrato = Convert.ToInt32(ddlTipoContrato.SelectedValue);
                    int tipoSalario = Convert.ToInt32(ddlTipoSalario.SelectedValue);
                    int liquidado = Convert.ToInt32(ddlLiquidado.SelectedValue);
                    bool marca = Convert.ToBoolean(ChkMarca.Checked);
                    bool ganaExtras = Convert.ToBoolean(ChkExtras.Checked);
                    bool pagovacacion = Convert.ToBoolean(chkpagovac.Checked);
                    bool credito = Convert.ToBoolean(chkCredito.Checked);
                    //multitareas
                    bool MultiTarea = Convert.ToBoolean(chkMultiTarea.Checked);
                    bool flexitime = Convert.ToBoolean(chkflexitime.Checked);
                    bool discapacidad = Convert.ToBoolean(ckdiscapacidad.Checked);
                    string telefono = txtTelf.Text.Trim();
                    string celular = txtCel.Text.Trim();
                    string correo = txtCorreo.Text.Trim();
                    int estadoCivil = Convert.ToInt32(ddlEstadoCivil.SelectedValue);
                    int tipoCasa = Convert.ToInt32(ddlTipoCasa.SelectedValue);
                    string viveCon = txtVive.Text.Trim();
                    int domicdepto = Convert.ToInt32(ddlDomiciDepto.SelectedValue);
                    int domiciMunc = Convert.ToInt32(ddlDomiciMunicip.SelectedValue);
                    string direccion = txtDireccion.Text.Trim();
                    //decimal embBanc = Convert.ToDecimal(txtEmbBanc.Text.Trim());
                    //decimal embPension = Convert.ToDecimal(txtEmbPens.Text.Trim());
                    string nombreEmerg = this.txtNombEmerg.Text.Trim();
                    string telfEmerg = this.txtTelEmerg.Text.Trim();
                    string parentesco = this.txtParentescEmerg.Text.Trim();
                    string direcEmerg = this.txtDireccEmerg.Text.Trim();
                    string nombPadre = this.txtNombPad.Text.Trim();
                    string numCedPadre = this.txtNumCedulPadre.Text.Trim();
                    string vivePadre = ddlVivPad.SelectedValue;
                    string nombMadre = this.txtNombMad.Text.Trim();
                    string numCedMadre = this.txtNumCedulMadre.Text.Trim();
                    string viveMadre = ddlVivMad.SelectedValue;
                    string nombEsps = this.txtNombEspos.Text.Trim();
                    string numCedEsps = this.txtNumCedulEsps.Text.Trim();
                    string nombreHijo1 = this.txtNombH1.Text.Trim();
                    string lugarNacH1 = this.txtLugNacH1.Text.Trim();
                    DateTime fechaNacH1 = Convert.ToDateTime(this.txtFechaNacH1.Text.Trim());
                    string sexoH1 = ddlSexoH1.SelectedValue;
                    string nombreHijo2 = this.txtNombH2.Text.Trim();
                    string lugarNacH2 = this.txtLugNacH2.Text.Trim();
                    DateTime fechaNacH2 = Convert.ToDateTime(this.txtFechaNacH2.Text.Trim());
                    string sexoH2 = ddlSexoH2.SelectedValue;
                    string nombreHijo3 = this.txtNombH3.Text.Trim();
                    string lugarNacH3 = this.txtLugNacH3.Text.Trim();
                    DateTime fechaNacH3 = Convert.ToDateTime(this.txtFechaNacH3.Text.Trim());
                    string sexoH3 = ddlSexoH3.SelectedValue;
                    string nombreHijo4 = this.txtNombH4.Text.Trim();
                    string lugarNacH4 = this.txtLugNacH4.Text.Trim();
                    DateTime fechaNacH4 = Convert.ToDateTime(this.txtFechaNacH4.Text.Trim());
                    string sexoH4 = ddlSexoH4.SelectedValue;
                    string empAnterior1 = this.txtEmpAnterior.Text.Trim();
                    string verfEmpAnt1 = ddlVerfEmAnt1.SelectedValue;
                    string observEmpAnt1 = txtObservEmpAant1.Text.Trim();
                    decimal ultimoSalarioEmp1 = Convert.ToDecimal(txtUltSalRef1.Text.Trim());
                    string verfUltSalaEm1 = ddlVerfUltSalAnt1.SelectedValue;
                    string observSalAntEm1 = this.txtObservSalAnt1.Text.Trim();
                    string cargoEmp1 = this.txtCargUltRef1.Text.Trim();
                    string verfCargoEmp1 = ddlVerfUltCargRef1.SelectedValue;
                    string observUltCargoEmp1 = this.txtObservCargAnt1.Text.Trim();
                    string motivoSalidaEmp1 = this.txtMotvSalidRef1.Text.Trim();
                    string verfMotvSal1 = ddlVerMtvSal1.SelectedValue;
                    string observMotvSal1 = txtObservMotSal1.Text.Trim();
                    DateTime fechaIngEmp1 = Convert.ToDateTime(txtFechIngresRef1.Text.Trim());
                    string verfFechaIngEm1 = ddlVerFechIng1.SelectedValue;
                    string observFechaIngEm1 = txtObservFecIng1.Text.Trim();
                    DateTime fechaEgrEmp1 = Convert.ToDateTime(txtFechEgres1.Text.Trim());
                    string verfFechEgrEm1 = ddlVerfFechEgr1.SelectedValue;
                    string obsercFechEgrEm1 = txtObservEgres1.Text.Trim();
                    string empAnterior2 = txtEmpAnt2.Text.Trim();
                    decimal UltSalarioEmp2 = Convert.ToDecimal(txtUltSalEmp2.Text.Trim());
                    string ultCargoEmp2 = txtCargoEmp2.Text.Trim();
                    string motSalidEmp2 = txtMotivoSaldEmp2.Text.Trim();
                    DateTime fechaIngEmp2 = Convert.ToDateTime(txtFechIngEmp2.Text.Trim());
                    DateTime fechaEgrsEmp2 = Convert.ToDateTime(txtFechaEgrsEmp2.Text.Trim());
                    string referenciaPers = txtRefPers.Text.Trim();
                    string verfRefPers = ddlVerifRefPers.SelectedValue;
                    string observRefPers = txtObservRefPers.Text.Trim();
                    string parentsRefPers = txtparentRefPers.Text.Trim();
                    string verfParentRef = ddlVerifParentRefPers.SelectedValue;
                    string observRefParnRef = txtObservParentRefPers.Text.Trim();
                    string numTelfRefPers = txtNumTelfRefPers.Text.Trim();
                    string verfNumTelfRef = ddlVerNumTelRefPers.SelectedValue;
                    string observNumTelRef = txtObservNumTelRefPers.Text.Trim();
                    string tiempoConcRef = txtTiempConRefPers.Text.Trim();
                    string verfTiempCncRef = ddlVerTiempCncRefPers.SelectedValue;
                    string observTiempCncRef = txtObservTiempCncRefPers.Text.Trim();
                    string padeceEnfermedad = ddlPadEnfermedad.SelectedValue;
                    string tipoEnfermedad = txtTipoEnfermedad.Text.Trim();
                    string enfermedadDesde = txtDese.Text.Trim();
                    string descEstudios = txtDescEstudios.Text.Trim();
                    string centroEstudios = txtCentroEstudios.Text.Trim();
                    //string fechIngHistoEgr = string.IsNullOrEmpty(txtFechaHistIng.Text.Trim()) ? "01/01/1900" : txtFechaHistIng.Text.Trim();
                    //string fechaEgrsHistoEg = string.IsNullOrEmpty(txtFechaHistEgrs.Text.Trim()) ? "01/01/1900" : txtFechaHistEgrs.Text.Trim();
                    ////string fechIngHistoEgr = txtFechaHistIng.Text.Trim();
                    ////string fechaEgrsHistoEg = txtFechaHistEgrs.Text.Trim();
                    //string ubicHistoEgrs = txtUbicEgrs.Text.Trim();
                    //string deptoHistoEgrs = txtHistoDptoEgresos.Text.Trim();
                    //string cargoHistoEgrs = txtHistCargoEgresos.Text.Trim();
                    //string motvHistoEgrs = txtHistMotivoEgreos.Text.Trim();
                    string nombreJefe = txtJefeInmed.Text.Trim();                    

                    if (Neg_Empleados.InfoEmpleadoAgregar(primerNomb, segundoNomb, primerApellido, segundApellido, nombreCompleto.ToUpper(), sexo, pais, departamento, municipio,
                        fechaNac, numCedula, emiteCed, venceCed, operacion, nivelAcademico, numInss, ubicacion, proceso, cargo, primerIngreso, estadoEmpl, fechaIngr, fechaEgrs,
                        moneda, salario, cuentaContable, subsidio, cuentaBanc, incentivo, turno, tipoContrato, tipoSalario, liquidado, marca, ganaExtras, telefono, celular, correo,
                        estadoCivil, tipoCasa, viveCon, domiciMunc, direccion, nombreEmerg, telfEmerg, parentesco, direcEmerg, nombPadre, numCedPadre, vivePadre,
                        nombMadre, numCedMadre, viveMadre, nombEsps, numCedEsps, nombreHijo1, lugarNacH1, fechaNacH1, sexoH1, nombreHijo2, lugarNacH2, fechaNacH2, sexoH2, nombreHijo3,
                        lugarNacH3, fechaNacH3, sexoH3, nombreHijo4, lugarNacH4, fechaNacH4, sexoH4, empAnterior1, verfEmpAnt1, observEmpAnt1, ultimoSalarioEmp1, verfUltSalaEm1, observSalAntEm1,
                        cargoEmp1, verfCargoEmp1, observUltCargoEmp1, motivoSalidaEmp1, verfMotvSal1, observMotvSal1, fechaIngEmp1, verfFechaIngEm1, observFechaIngEm1, fechaEgrEmp1, verfFechEgrEm1,
                        obsercFechEgrEm1, empAnterior2, UltSalarioEmp2, ultCargoEmp2, motSalidEmp2, fechaIngEmp2, fechaEgrsEmp2, referenciaPers, verfRefPers, observRefPers, parentsRefPers, verfParentRef,
                        observRefParnRef, numTelfRefPers, verfNumTelfRef, observNumTelRef, tiempoConcRef, verfTiempCncRef, observTiempCncRef, padeceEnfermedad, tipoEnfermedad, enfermedadDesde, descEstudios,
                        centroEstudios, //fechIngHistoEgr, fechaEgrsHistoEg, ubicHistoEgrs, deptoHistoEgrs, cargoHistoEgrs, motvHistoEgrs, 
                        nombreJefe, txtObservEmpl.Text.Trim(), user, pagovacacion, discapacidad, cuentaMall, flexitime, credito,domicdepto,MultiTarea
                       ))

                    {
                        alertValida.Visible = false;
                        alertSucces.Visible = true;
                        LblSuccess.Visible = true;
                        string codigo = Neg_Empleados.obtenerUltimoEmpleadoAgregado();
                        //fechas ingreso egreso
                        Session["fecha_ingreso"] = txtFechaIngreso.Text;
                        Session["fecha_egreso"] = txtFechaEgreso.Text;
                        HabilitarBotones(ddlEstadoEmpl.SelectedValue.Trim());
                        LblSuccess.Text = "Ingreso Satisfactorio: " + "El Codigo del empleado es: " + codigo;
                        btnGuardar.Visible = true;
                        btnAgregar.Visible = false;

                        //Agregar foto del empleadp
                        HttpFileCollection files = Request.Files;
                        foreach (string fileTagName in files)
                        {

                            HttpPostedFile file = Request.Files[fileTagName];
                            string fileExtention = file.ContentType;
                            if (file.ContentLength > 0)
                            {
                                if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                                {
                                    if (file.ContentLength < 9288)
                                    {
                                        int size = file.ContentLength;
                                        string name = file.FileName;
                                        int tipo = 1;

                                        int position = name.LastIndexOf("\\");
                                        name = name.Substring(position + 1);
                                        string contentType = file.ContentType;
                                        //byte[] fileData = new byte[size];
                                        byte[] fileData = new byte[size];
                                        //ResizeImageFile(fileData, 300);
                                        //int numemp = Convert.ToInt32(txtCodEmp.Text.Trim());
                                        file.InputStream.Read(fileData, 0, size);
                                        IUserDetail userDetail = UserDetailResolver.getUserDetail();
                                        int idEmpresa = userDetail.getIDEmpresa();
                                        Neg_Empleados.SaveFile(name, fileData, 0, tipo, idEmpresa);
                                    }
                                    else
                                    {
                                        alertValida.Visible = true;
                                        lblAlert.Visible = true;
                                        lblAlert.Text = "El tamaño de la imagen es demasiado grande";
                                    }
                                }
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;

            }
        }
        //Validar que todos los campos requeridos sean llenados
        public bool validar()
        {
            if (txt1erNombre.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese el primer nombre del empleado";
                return false;

            }

            if (txt1erApellido.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese el primer apellido del empleado";
                return false;

            }
            DateTime defaultdate;//= new DateTime(1, 1, 1900);
            if (!DateTime.TryParse(txtFechaNac.Text.Trim(), out defaultdate))//txtFechaNac.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese fecha de nacimiento del empleado";
                return false;

            }

            if (txtCedula.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese el numero de cedula";
                return false;

            }
            if (!DateTime.TryParse(txt1erIngreso.Text.Trim(), out defaultdate))//txt1erIngreso.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese fecha de su primer ingreso";
                return false;

            }

            if (!DateTime.TryParse(txtFechaIngreso.Text.Trim(), out defaultdate))//txtFechaIngreso.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese fecha de ingreso";
                return false;

            }
            if (txtDireccion.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese la direccion del empleado";
                return false;

            }
            if (!DateTime.TryParse(txtEmite.Text.Trim(), out defaultdate))//txtEmite.Text.Trim() == "")
            {
                txtEmite.Text = "01 / 01 / 1900";

            }

            if (!DateTime.TryParse(txtVence.Text.Trim(), out defaultdate))//txtVence.Text.Trim() == "")
            {
                txtVence.Text = "01 / 01 / 1900";

            }

            if (!DateTime.TryParse(txtFechaEgreso.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechaEgreso.Text = "01 / 01 / 1900";
            }


            if (txtSalario.Text.Trim() == "")
            {
                decimal salario = 0.00m;
                txtSalario.Text = Convert.ToString(salario);
            }

            if (txtCuentaContable.Text.Trim() == "")
            {
                txtCuentaContable.Text = "0";
            }

            if (txtNumInss.Text.Trim() == "")
            {
                txtNumInss.Text = "0";
            }

            if (txtSubsidio.Text.Trim() == "")
            {
                txtSubsidio.Text = "0.00";
            }

            if (txtCuentaBanc.Text.Trim() == "")
            {
                txtCuentaBanc.Text = "0";
            }

            if (this.TxtCuentaMall.Text.Trim() == "")
            {
                TxtCuentaMall.Text = "0";
            }

            if (txtIncentivo.Text.Trim() == "")
            {
                txtIncentivo.Text = "0.00";
            }
            //hijos
            if (!DateTime.TryParse(txtFechaNacH1.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechaNacH1.Text = "01 / 01 / 1900";
            }
            if (!DateTime.TryParse(txtFechaNacH2.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechaNacH2.Text = "01 / 01 / 1900";
            }
            if (!DateTime.TryParse(txtFechaNacH3.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechaNacH3.Text = "01 / 01 / 1900";
            }
            if (!DateTime.TryParse(txtFechaNacH4.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechaNacH4.Text = "01 / 01 / 1900";
            }
            //referencias
            if (!DateTime.TryParse(txtFechIngresRef1.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechIngresRef1.Text = "01 / 01 / 1900";
            }
            if (!DateTime.TryParse(txtFechEgres1.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechEgres1.Text = "01 / 01 / 1900";
            }
            if (!DateTime.TryParse(txtFechIngEmp2.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechIngEmp2.Text = "01 / 01 / 1900";
            }
            if (!DateTime.TryParse(txtFechaEgrsEmp2.Text.Trim(), out defaultdate))//txtFechaEgreso.Text.Trim() == "")
            {
                txtFechaEgrsEmp2.Text = "01 / 01 / 1900";
            }
            if (this.txtUltSalRef1.Text.Trim() == "")
            {
                txtUltSalRef1.Text = "0";
            }
            if (this.txtUltSalEmp2.Text.Trim() == "")
            {
                txtUltSalEmp2.Text = "0";
            }
            
            //validacion para liquidados
            if (ddlEstadoEmpl.SelectedValue.Trim() == "3" || ddlEstadoEmpl.SelectedValue.Trim() == "1")
            {

                if (Convert.ToDateTime(txtFechaIngreso.Text) != Convert.ToDateTime(Session["fecha_ingreso"]))
                {
                    // TODO: Victor Porras 11/11/2024
                    if (CheckBoxMFI.Checked)
                    {
                        object ob = this.Page.Session["Idusuario"];

                        if (ob != null)
                        {
                            // Convertimos el objeto a string
                            string idUsuario = ob.ToString();

                            // Creamos una lista de cadenas con los valores que queremos verificar
                            List<string> valoresPermitidos = new List<string> { "1015", "1006", "1059", "1073" };

                            // Verificamos si alguno de los valores permitidos está contenido en idUsuario
                            if (valoresPermitidos.Any(valor => idUsuario.Contains(valor)))
                            {
                                // Intentamos convertir el texto ingresado a una fecha para fechaIngreso
                                DateTime fechaIngreso;
                                if (DateTime.TryParse(txtFechaIngreso.Text, out fechaIngreso))
                                {
                                    // Intentamos convertir el texto de txt1erIngreso a fechaActual
                                    DateTime fechaActual;
                                    if (DateTime.TryParse(txt1erIngreso.Text, out fechaActual))
                                    {
                                        // Calcular el próximo lunes a partir de fechaPrimerIngreso
                                        DateTime proximoLunes;


                                        

                                        // Si fechaPrimerIngreso es un lunes, sumar 7 días para obtener el próximo lunes
                                        if (fechaActual.DayOfWeek == DayOfWeek.Monday)
                                        {
                                            proximoLunes = fechaActual.AddDays(7);
                                        }
                                        else
                                        {
                                            // Calcular cuántos días faltan hasta el próximo lunes
                                            proximoLunes = fechaActual.AddDays((7 - (int)fechaActual.DayOfWeek + 1) % 7);
                                        }
                                        

                                        txtFechaIngreso.Text = proximoLunes.ToString();
                                        if ((fechaIngreso - fechaActual).TotalDays > 7)
                                        {
                                            // La fecha es válida
                                            alertValida.Visible = true;
                                            alertSucces.Visible = false;
                                            alertValida.InnerText = "Mas de 7 dias entre fechas.";
                                            txtFechaIngreso.Text = proximoLunes.ToString();
                                            return false;
                                        }


                                    }
                                    else
                                    {
                                        // El texto de txt1erIngreso no es una fecha válida
                                        alertValida.Visible = true;
                                        alertSucces.Visible = false;
                                        alertValida.InnerText = "Por favor, ingrese una fecha válida en el campo de referencia.";
                                        return false;
                                    }
                                }
                                else
                                {
                                    // El texto ingresado en txtFechaIngreso no es una fecha válida
                                    alertValida.Visible = true;
                                    alertSucces.Visible = false;
                                    alertValida.InnerText = "Por favor, ingrese una fecha válida en el campo de ingreso.";
                                    return false;
                                }
                            }
                            //else
                            //{
                            //    // El ID de usuario no está en la lista permitida
                            //    alertValida.Visible = true;
                            //    alertSucces.Visible = false;
                            //    alertValida.InnerText = "El ID de usuario no está permitido.";
                            //}
                        }
                    }
                    else
                    {
                        alertValida.Visible = true;
                        alertSucces.Visible = false;
                        alertValida.InnerText = "La fecha Ingreso solo puede cambiar en casos de reingreso";
                        txtFechaIngreso.Text = Session["fecha_ingreso"].ToString();
                        return false;
                    }
                  
                }
                else
                {
                    // TODO: Victor Porras 11/11/2024
                    if (CheckBoxMFI.Checked)
                    {
                        object ob = this.Page.Session["Idusuario"];

                        if (ob != null)
                        {
                            // Convertimos el objeto a string
                            string idUsuario = ob.ToString();

                            // Creamos una lista de cadenas con los valores que queremos verificar
                            List<string> valoresPermitidos = new List<string> { "1015", "1006", "1059", "1073" };

                            // Verificamos si alguno de los valores permitidos está contenido en idUsuario
                            if (valoresPermitidos.Any(valor => idUsuario.Contains(valor)))
                            {
                                // Intentamos convertir el texto ingresado a una fecha para fechaIngreso
                                DateTime fechaIngreso;
                                if (DateTime.TryParse(txtFechaIngreso.Text, out fechaIngreso))
                                {
                                    // Intentamos convertir el texto de txt1erIngreso a fechaActual
                                    DateTime fechaActual;
                                    if (DateTime.TryParse(txt1erIngreso.Text, out fechaActual))
                                    {
                                        // Calcular el próximo lunes a partir de fechaPrimerIngreso
                                        DateTime proximoLunes;


                                        

                                        // Si fechaPrimerIngreso es un lunes, sumar 7 días para obtener el próximo lunes
                                        if (fechaActual.DayOfWeek == DayOfWeek.Monday)
                                        {
                                            proximoLunes = fechaActual.AddDays(7);
                                        }
                                        else
                                        {
                                            // Calcular cuántos días faltan hasta el próximo lunes
                                            proximoLunes = fechaActual.AddDays((7 - (int)fechaActual.DayOfWeek + 1) % 7);
                                        }

                                        txtFechaIngreso.Text = proximoLunes.ToString();
                                        if ((fechaIngreso - fechaActual).TotalDays > 7)
                                        {
                                            // La fecha es válida
                                            alertValida.Visible = true;
                                            alertSucces.Visible = false;
                                            alertValida.InnerText = "Mas de 7 dias entre fechas.";
                                            txtFechaIngreso.Text = proximoLunes.ToString();
                                            return false;
                                        }


                                    }
                                    else
                                    {
                                        // El texto de txt1erIngreso no es una fecha válida
                                        alertValida.Visible = true;
                                        alertSucces.Visible = false;
                                        alertValida.InnerText = "Por favor, ingrese una fecha válida en el campo de referencia.";
                                        return false;
                                    }
                                }
                                else
                                {
                                    // El texto ingresado en txtFechaIngreso no es una fecha válida
                                    alertValida.Visible = true;
                                    alertSucces.Visible = false;
                                    alertValida.InnerText = "Por favor, ingrese una fecha válida en el campo de ingreso.";
                                    return false;
                                }
                            }
                            //else
                            //{
                            //    // El ID de usuario no está en la lista permitida
                            //    alertValida.Visible = true;
                            //    alertSucces.Visible = false;
                            //    alertValida.InnerText = "El ID de usuario no está permitido.";
                            //}
                        }
                    }
                }
                if (Convert.ToDateTime(txtFechaEgreso.Text) != Convert.ToDateTime(Session["fecha_egreso"]))
                {
                   
                    alertValida.Visible = true;
                    alertSucces.Visible = false;
                    alertValida.InnerText = ddlEstadoEmpl.SelectedValue.Trim() == "3" ? "La fecha Egreso no puede modificarse en estado liquidado" : " La Fecha Egreso solo debe ser modificada al Liquidar";
                    txtFechaEgreso.Text = Session["fecha_egreso"].ToString();
                    return false;
                }
            }            

            return true;
        }
        void ObtenerHistoricoSalario(int codigo)
        {
            DataSet ds = new DataSet();
            ds = Neg_Empleados.HistorialSalariosXEmpleado(codigo);
            if (ds.Tables.Count > 0)
            {
                GvHistoricoSal.DataSource = ds;
                GvHistoricoSal.DataMember = "salarios";
                GvHistoricoSal.DataBind();
            }
        }
        void ObtenerHistoricoEgresos(int codigo)
        {
            DataSet ds = new DataSet();
            ds = Neg_Empleados.HistorialEgresosXEmpleado(codigo);
            if (ds.Tables.Count > 0)
            {
                gvHistEgresos.DataSource = ds;
                gvHistEgresos.DataMember = "egresos";
                gvHistEgresos.DataBind();
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtCodEmp.Text.Trim() != "")
            {
                if (Neg_Empleados.InfoEmpleadoEliminar(Convert.ToInt32(txtCodEmp.Text.Trim())))
                {
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "El Empleado ha sido Eliminado Correctamente";
                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Error al Eliminar el Empleado";
                }
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingresar el codigo del empleado para eliminar";
            }
        }

        protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Neg_Empresas NEmpresas = new Neg_Empresas();
                dsPlanilla.dtEmpresaDataTable DetEmpresas = NEmpresas.ObtenerInfoDetEmpresas();

                //si la moneda de pago de la empresa es diferente a la moneda de pago del empleado, setear a salario variable
                if (DetEmpresas.Rows.Count > 0)
                {
                    if (ddlMoneda.SelectedValue.Trim() != DetEmpresas[0].moneda.ToString().Trim())
                    {
                        ddlTipoSalario.SelectedValue = "2";
                    }
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;

            }
        }

        protected void ChkMarca_CheckedChanged(object sender, EventArgs e)
        {
            if (!ChkMarca.Checked)///si no marca no aplica a flexitime
            {
                chkflexitime.Checked = false;
            }
        }

        protected void chkflexitime_CheckedChanged(object sender, EventArgs e)
        {
            if (chkflexitime.Checked)//si es flexitime debe marcar
            {
                ChkMarca.Checked = true;
            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerMunicipios(ddlMunicipio, Convert.ToInt32(ddlDepartamento.SelectedValue.Trim()));
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerDepartamentos(Convert.ToInt32(ddlPais.SelectedValue.Trim()));
            obtenerMunicipios(ddlMunicipio, Convert.ToInt32(ddlDepartamento.SelectedValue.Trim()));
            obtenerMunicipios(ddlDomiciMunicip, Convert.ToInt32(ddlDomiciDepto.SelectedValue.Trim()));
        }

        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerCargo(Convert.ToInt32(ddlProceso.SelectedValue));
            obtenerOperacion(Convert.ToInt32(ddlCargo.SelectedValue));

            if (Session["procesoNameA"].ToString().Trim()!="")
            {
                //si pasa a baja/subsidio/traslados modulos se mantiene el cargo y operacion 
                if (ddlProceso.SelectedValue.Trim() == "4" || ddlProceso.SelectedValue.Trim() == "5" || ddlProceso.SelectedValue.Trim() == "144" || ddlProceso.SelectedValue.Trim() == "145" ||
                    (Session["procesoNameA"].ToString().Trim().ToLower().IndexOf("modulo") > -1 && ddlProceso.SelectedItem.Text.Trim().ToLower().IndexOf("modulo") > -1)
                    )
                {
                    ddlCargo.SelectedValue = Session["cargoA"].ToString();
                    obtenerOperacion(Convert.ToInt32(ddlCargo.SelectedValue));
                    ddlOperacion.SelectedValue = Session["operacionA"].ToString();
                }
            }
                      
        }

        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerOperacion(Convert.ToInt32(ddlCargo.SelectedValue));
        }

        protected void btnEstatusLiq_Click(object sender, EventArgs e)
        {
            try
            {
                AplicarCambioEstadoEmp(false);
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertSucces.Visible = false;
                alertValida.InnerText = ex.Message;
            }
        }
        private void AplicarCambioEstadoEmp(bool renovacion)
        {
            try
            {//debe cambiar fechas egreso cuando liquida                
                if (Convert.ToDateTime(txtFechaIngreso.Text) != Convert.ToDateTime(Session["fecha_ingreso"]))
                {
                    txtFechaIngreso.Text = Session["fecha_ingreso"].ToString();
                    throw new Exception("La fecha Ingreso solo puede cambiar en casos de reingreso");
                }
                if (Convert.ToDateTime(txtFechaEgreso.Text) == Convert.ToDateTime(Session["fecha_egreso"]))
                {
                    throw new Exception("La Fecha Egreso debe ser diferente al Egreso Actual");
                }
                if (ddlEstadoEmpl.SelectedValue.Trim() == "1")//activo
                {
                    //ddlEstadoEmpl.SelectedValue = "3";
                    //TxtEstado.Text = ddlEstadoEmpl.SelectedItem.Text;
                    ActualizarEstadoEHistorial(Convert.ToDateTime(txtFechaIngreso.Text), Convert.ToDateTime(txtFechaEgreso.Text), 3,renovacion);


                    //fechas ingreso egreso
                    Session["fecha_ingreso"] = txtFechaIngreso.Text;
                    Session["fecha_egreso"] = txtFechaEgreso.Text;
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertSucces.Visible = false;
                alertValida.InnerText = ex.Message;
            }
        }
       
        protected void BtnRenovar_Click(object sender, EventArgs e)
        {
            try
            {
                AplicarCambioEstadoEmp(true);
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertSucces.Visible = false;
                alertValida.InnerText = ex.Message;
            }
        }
        void ActualizarEstadoEHistorial(DateTime fecini, DateTime fecfin, int estado, bool renovacion)
        {
            try
            {
                //historial
                string user = Convert.ToString(this.Page.Session["usuario"]);
                DateTime defaultdate;
                if (!DateTime.TryParse(txtFechaIngreso.Text.Trim(), out defaultdate))//txtFechaIngreso.Text.Trim() == "")
                {
                    throw new Exception("Favor Ingrese fecha de ingreso valida");

                }
                if (!DateTime.TryParse(txtFechaEgreso.Text.Trim(), out defaultdate))//txtFechaIngreso.Text.Trim() == "")
                {
                    throw new Exception("Favor Ingrese fecha de egreso valida");
                }

                if (Neg_Empleados.plnHistorialEgresosIns(Convert.ToInt32(txtCodEmp.Text.Trim()), fecini, fecfin,renovacion, user))
                {
                    if (!Neg_Empleados.plnEstadoEmpleadoUpd(Convert.ToInt32(txtCodEmp.Text.Trim()), Convert.ToDateTime(txtFechaIngreso.Text.Trim()),
                             Convert.ToDateTime(txtFechaEgreso.Text.Trim()), estado))
                    {
                        throw new Exception("Error al actualizar estado de empleado");
                    }
                    ddlEstadoEmpl.SelectedValue = estado.ToString();
                    TxtEstado.Text = ddlEstadoEmpl.SelectedItem.Text;
                    ObtenerHistoricoEgresos(Convert.ToInt32(txtCodEmp.Text.Trim()));
                    HabilitarBotones(ddlEstadoEmpl.SelectedValue.Trim());
                    txtSalario.ReadOnly = false;

                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Actualizacion Satisfactoria";
                }
                else
                {
                    throw new Exception("Error al insertar historial de egreso");
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertSucces.Visible = false;
                alertValida.InnerText = ex.Message;
            }
        }



        protected void btnReingresar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDateTime(txtFechaIngreso.Text) == Convert.ToDateTime(Session["fecha_ingreso"]))
                {
                    throw new Exception("La Fecha Nuevo Ingreso debe ser posterior al ingreso anterior");
                }
                if (Convert.ToDateTime(txtFechaEgreso.Text) == Convert.ToDateTime(Session["fecha_egreso"]) && txtFechaEgreso.Text.Trim() != "01/01/1900")
                {
                    txtFechaEgreso.Text = "01/01/1900";
                    throw new Exception("La Fecha Egreso para Nuevo Ingreso debe ser 01/01/1900");
                }
                //ddlEstadoEmpl.SelectedValue = "1";
                //TxtEstado.Text = ddlEstadoEmpl.SelectedItem.Text;

                ActualizarEstadoEHistorial(Convert.ToDateTime(Session["fecha_ingreso"]), Convert.ToDateTime(Session["fecha_egreso"]), 1, false);

                if (!Neg_Empleados.plnSalarioMinEmpleadoUpd(Convert.ToInt32(txtCodEmp.Text.Trim())))
                {

                }

                //fechas ingreso egreso
                Session["fecha_ingreso"] = txtFechaIngreso.Text;
                Session["fecha_egreso"] = txtFechaEgreso.Text;
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }
        protected void ddlDomiciDepto_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerMunicipios(ddlDomiciMunicip, Convert.ToInt32(ddlDomiciDepto.SelectedValue.Trim()));
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/Presentacion/Default.aspx");
        }
    }
}