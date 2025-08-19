using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using Negocios;
using Datos;
using System.Linq;

namespace NominaRRHH.Presentacion
{
    public partial class IncAsignarProteccion : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        //Neg_Empleados NegEmp = new Neg_Empleados();
        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtRepeticiones.Text = "1";              
            }
        }   
       
        #endregion

        public void cargarReporte(DataTable dt, string rpt, ReportViewer window)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            window.ProcessingMode = ProcessingMode.Local;
            window.LocalReport.DataSources.Clear();
            window.LocalReport.ReportPath = Server.MapPath("../Reportes/"+rpt+".rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
          
                if (string.IsNullOrEmpty(txtPeriodo.Text.Trim()))
                {
                    throw new Exception("Debe ingresar periodo valido.");
                }
                div1.Visible = true;
                obtenerProtecciones();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        private void obtenerProtecciones()
        {
            DataTable dt = Neg_Incentivos.PlnObtenerProteccionIncentivoFijo(int.Parse(txtPeriodo.Text));
            cargarReporte(dt, "ProteccionesIndividuales", ReportViewer2);
        }
        protected void btnAgregarDed_Click(object sender, EventArgs e)
        {
            try
            {
                string user = Convert.ToString(this.Page.Session["usuario"]);                

                if (txtCodigo.Text.Trim() == "")
                {
                    MessageError("El codigo empleado es un campo obligatorio");
                    return;
                }

                if (txtPeriodo.Text.Trim() == "")
                {
                    MessageError("El numero del periodo es un campo obligatorio");
                    return;
                }

                if (txtViatico.Text.Trim() != "" && txtAsistencia.Text.Trim() == "")
                {
                    MessageError("El bono de asistencia es obligatorio cuando se define una proteccion de viatico.");
                    return;
                }

                int codigo;
                int.TryParse(txtCodigo.Text,out codigo);
                if(codigo==0)
                {
                    MessageError("El codigo empleado debe ser numerico");
                    return;
                }

                decimal asistencia;
                decimal viatico;
                
                decimal porcentaje;
                decimal.TryParse(TxtPorcentaje.Text, out porcentaje);
                porcentaje = porcentaje/ 100;

                int repeticiones;
                int.TryParse(txtRepeticiones.Text,out repeticiones);

                int periodo;
                int.TryParse(txtPeriodo.Text, out periodo);
                if (periodo == 0)
                {
                    MessageError("El codigo del periodo debe ser numerico");
                    return;
                }

                decimal BonoCalidad;

                if (txtCodigo.Text.Trim() != "" && txtPeriodo.Text.Trim() != "")
                {
                    if (decimal.TryParse(txtBonoCalidad.Text, out BonoCalidad))
                    {
                        new Dato_Incentivos().IncentivoIngDedccLOGInsert(1, codigo, periodo, 1, 1, "OpCritica", 1, BonoCalidad, BonoCalidad, "", 38, false);
                    }

                    if (decimal.TryParse(txtAsistencia.Text, out asistencia))
                    {
                        new Dato_Incentivos().IncentivoIngDedccLOGInsert(1, codigo, periodo, 1, 1, "BonoAsistencia", 1, asistencia, asistencia, "", 4, false);
                    }

                    if (decimal.TryParse(txtViatico.Text, out viatico))
                    {
                        Neg_Incentivos.PlnProteccionIncentivoFijoIns(codigo, asistencia, viatico, porcentaje, repeticiones, ChkRecurrente.Checked, ChkActivo.Checked, user);
                    }

                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Ingreso Satisfactorio";
                    LimpiarCamposProtIndividual();
                }
                else
                {
                    throw new Exception("Inserte un valor valido");
                }
            }
            catch (Exception ex)
            {
                MessageError(ex.Message);
            }
        }

       void MessageError(string msg)
        {
            alertSucces.Visible = false;
            alertValida.Visible = true;
            lblAlert.Visible = true;
            lblAlert.Text = msg;
        }
       void LimpiarCamposProtIndividual()
        {
            txtCodigo.Text="";
            TxtNombreE.Text = "";
            ChkRecurrente.Checked = false;
            ChkActivo.Checked = true;
        }
        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ObtenerDatosEmpleado();
                operacionCt();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void ObtenerDatosEmpleado()
        {
            if (validar())
            {
                obtenerPeriodo();
                try
                {
                    DataTable dtEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(this.txtCodigo.Text.Trim()), 1);

                    if (dtEmp.Rows.Count > 0 && dtEmp.Rows[0]["idestado"].ToString().Trim() != "0")
                    {
                        this.TxtNombreE.Text = dtEmp.Rows[0]["nombrecompleto"].ToString();
                        obtenerProtecciones();

                    }
                    else//No aplica a liquidación.
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "El empleado no se encuentra";
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
        public bool validar()
        {
            if (txtCodigo.Text.Trim() == "")
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un valor valido";
                txtCodigo.Focus();
                return false;
            }

            return true;
        }
        private void obtenerPeriodo()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtCodigo.Text.Trim()))
                {
                    DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtCodigo.Text);
                    Session["DetEmpleados"] = DetEmpleados;

                    DataTable ubicacion = Neg_Catalogos.seleccionarUbicacionesxCod(Convert.ToInt32(DetEmpleados.Rows[0]["codigo_ubicacion"]));
                    dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, Convert.ToInt32(ubicacion.Rows[0]["tplanilla"]), 0);
                    if (dtPeriodo.Rows.Count > 0)
                    {
                        txtPeriodo.Text = dtPeriodo[0].nperiodo.ToString();
                    }
                    else
                    {
                        txtPeriodo.Text = "0";
                    }
                    Session["periodo"] = txtPeriodo.Text.Trim();                   
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "No hay periodo abierto que este vigente";
            }
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            
            if (TxtPorcentaje.Text.Trim().Length > 0 && TxtEstilo.Text.Trim().Length > 0)
            {
                int proteccion = int.Parse(TxtPorcentaje.Text.Trim());
                int estilo = int.Parse(TxtEstilo.Text);
                DataTable dt = new Dato_TablaIncentivo().Select(estilo, proteccion);

                if (dt.Rows.Count > 0)
                {
                  
                    txtAsistencia.Text = decimal.ToInt32((decimal)dt.Rows[0]["BonoAsistencia"]).ToString();
                    txtViatico.Text = decimal.ToInt32((decimal)dt.Rows[0]["Incentivo"]).ToString();
                }

               string bandera = operacionCt();                  
                
                if (bandera != "NoBono")
                {
                    DataTable dtOpC = Neg_Incentivos.PlnTablaOperacionCriticaSel();
                   var row = dtOpC.AsEnumerable()
                       .FirstOrDefault(r => proteccion >= r.Field<decimal>("eficienciadesde")
                                   && proteccion <= r.Field<decimal>("eficienciahasta"));

                    decimal monto = row != null ? row.Field<decimal>("montoOpCritica") : 0;

                    txtBonoCalidad.Text = decimal.ToInt32(monto).ToString();
                }

            }
        }

        string operacionCt()
        {
            DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(txtCodigo.Text);
            Session["DetEmpleados"] = DetEmpleados;

            if (Session["DetEmpleados"] != null)
            {
                DataTable dt = Session["DetEmpleados"] as DataTable;

                if (dt.Rows.Count > 0)
                {
                    string idOperacion = dt.Rows[0]["idOperacion"].ToString().Trim();

                    // Si idOperacion NO está en la lista permitida → deshabilita
                    if (idOperacion != "PM" && idOperacion != "PC" &&
                        idOperacion != "PT" && idOperacion != "RF" &&
                        idOperacion != "UH")
                    {
                        txtBonoCalidad.Enabled = false;
                        return "NoBono";
                    }
                    else
                    {
                        txtBonoCalidad.Enabled = true;
                    }
                }
            }
            return "Ok";
        }
    
    }

}
