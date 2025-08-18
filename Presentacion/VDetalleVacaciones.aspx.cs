using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Datos;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Globalization;

namespace NominaRRHH.Presentacion
{
    public partial class VDetalleVacaciones : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Permisos NPermisos = new Neg_Permisos();
        //Globales Globales = new Globales();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                txtFecCorte.Text = DateTime.Now.ToShortDateString();                
            }
        }
      
       
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }              

        private void MostrarReporte(DataTable ds)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/DetalleVacaciones.rdlc");
            //DataTable ds = Neg_Informes.plnObtenerHistoricoIRrpt();
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("codigo_empleado", TxtCodigo.InnerText.Trim());
            parameters[1] = new ReportParameter("nombrecompleto", txtNombre.InnerText.Trim());
            this.ReportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
                             
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (txtCodEmp.Text.Trim().Length != 0)
            {
                try
                {
                    limpiarCampos();
                    DataTable dtDatosEmp = new DataTable();//Datos principales utilizados para la liquidacion,fechas,diasLaborados etc.
                    dtDatosEmp = Neg_Liquidacion.spLiquidacionDatosEmp(Convert.ToInt32(txtCodEmp.Text.Trim()), 1);
                    if (dtDatosEmp.Rows.Count > 0 )//&& dtDatosEmp.Rows[0]["idestado"].ToString().Trim() == "1")//Si es un codigo válido.
                    {
                        if (dtDatosEmp.Rows[0]["idestado"].ToString().Trim() != "1")
                        {
                            txtFecCorte.Text = Convert.ToDateTime(dtDatosEmp.Rows[0]["fecha_egreso"]).ToShortDateString();
                        }
                        Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());
                        TxtCodigo.InnerText = dtDatosEmp.Rows[0]["codigo_empleado"].ToString();
                        txtFechaing.InnerText = Convert.ToDateTime(dtDatosEmp.Rows[0]["fecha_ingreso"].ToString()).ToShortDateString();
                        txtNombre.InnerText = dtDatosEmp.Rows[0]["nombrecompleto"].ToString();
                        TxtTipoSalario.InnerText = dtDatosEmp.Rows[0]["idTipoSalario"].ToString() == "1" ? "Fijo" : "Variable";
                        TxtEstado.InnerText = dtDatosEmp.Rows[0]["idestado"].ToString() == "1" ? "Activo" : (dtDatosEmp.Rows[0]["idestado"].ToString() == "3") ? "Liquidado" : "Inactivo";

                        DataTable dt= Neg_Liquidacion.CrearDetalleVacaciones(Convert.ToInt32(txtCodEmp.Text), Convert.ToDateTime(dtDatosEmp.Rows[0]["fecha_ingreso"].ToString()), Convert.ToDateTime(txtFecCorte.Text.Trim()));
                        MostrarReporte(dt);
                    }
                    else
                    {
                        alertValida.Visible = true;
                        alertValida.InnerText = "No se han encontrado registros";
                        limpiarCampos();
                    }
                }
                catch (Exception ex)
                {

                    alertValida.Visible = true;
                    alertValida.InnerText = ex.Message;
                }
            }
        }
        void limpiarCampos()
        {
            TxtCodigo.InnerText = "";
            txtFechaing.InnerText = "";
            txtNombre.InnerText = "";
            TxtTipoSalario.InnerText = "";
            
        }
    }
}