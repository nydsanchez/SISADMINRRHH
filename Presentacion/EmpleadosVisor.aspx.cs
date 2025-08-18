using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Negocios;
using Datos;
using System.Text.RegularExpressions;
//using System.Data.SqlClient;

namespace NominaRRHH.Presentacion
{
    public partial class EmpleadosVisor : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.IsPostBack)
            {
                ObtenerPaises();
                ObtenerDepartamentos( Convert.ToInt32(ddlPais.SelectedValue.Trim()));               
                obtenerMunicipios(ddlMunicipio, Convert.ToInt32(ddlDepartamento.SelectedValue.Trim()));
                obtenerNivelAcademico();
                obtenerUbicaciones();
                obtenerProcesos();
              
                obtenerEstadoEmpl();
       
                obtenerCargo(Convert.ToInt32(ddlProceso.SelectedValue));
                obtenerOperacion(Convert.ToInt32(ddlCargo.SelectedValue.Trim()));
                Session["procesoA"] = "";
                Session["procesoNameA"] = "";
                Session["cargoA"] = "";
                Session["operacionA"] = "";
            }
        }

  

       

        private void obtenerEstadoEmpl()
        {
            this.ddlEstadoEmpl.DataSource = Neg_Catalogos.CargarEstadoEmpleado();
            this.ddlEstadoEmpl.DataMember = "estado";
            this.ddlEstadoEmpl.DataValueField = "id_descripcion";
            this.ddlEstadoEmpl.DataTextField = "descripcion";
            this.ddlEstadoEmpl.DataBind();
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

          
        }
        private void ObtenerDepartamentos(int idpadre)
        {
            DataSet dt= Neg_Catalogos.CargarDepartamentos(idpadre);
            ddlDepartamento.DataSource = dt;
            ddlDepartamento.DataMember = "departamentos";
            ddlDepartamento.DataValueField = "id_descripcion";
            ddlDepartamento.DataTextField = "descripcion";
            ddlDepartamento.DataBind();

        
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
                  
                    txtJefeInmed.Text = DetEmpleados.Rows[0]["Jefe"].ToString();
                    txtObservEmpl.Text = DetEmpleados.Rows[0]["observacion"].ToString();

                    ObtenerFoto(Convert.ToInt32(txtCodEmp.Text));
                 
                    this.alertValida.Visible = false;
                    this.lblAlert.Visible = false;

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
          
            txtJefeInmed.Text = "";
            txtObservEmpl.Text = "";
          
        }
        //Agregar empleado
       
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



            if (txtNumInss.Text.Trim() == "")
            {
                txtNumInss.Text = "0";
            }

          
            
            //validacion para liquidados
            if (ddlEstadoEmpl.SelectedValue.Trim() == "3" || ddlEstadoEmpl.SelectedValue.Trim() == "1")
            {

                if (Convert.ToDateTime(txtFechaIngreso.Text) != Convert.ToDateTime(Session["fecha_ingreso"]))
                {
                    
                    alertValida.Visible = true;
                    alertSucces.Visible = false;
                    alertValida.InnerText = "La fecha Ingreso solo puede cambiar en casos de reingreso";
                    txtFechaIngreso.Text = Session["fecha_ingreso"].ToString();
                    return false;
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

      
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerMunicipios(ddlMunicipio, Convert.ToInt32(ddlDepartamento.SelectedValue.Trim()));
        }

        protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerDepartamentos(Convert.ToInt32(ddlPais.SelectedValue.Trim()));
            obtenerMunicipios(ddlMunicipio, Convert.ToInt32(ddlDepartamento.SelectedValue.Trim()));
          
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

      
      
       
    }
}