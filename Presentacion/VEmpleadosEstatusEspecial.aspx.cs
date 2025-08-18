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
    public partial class VEmpleadosEstatusEspecial : System.Web.UI.Page
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
        //Globales Globales = new Globales();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {             
                DataTable dt = new DataTable();
                dt.Columns.Add("depto");
                dt.Columns.Add("codigo");
                dt.Columns.Add("nombre");               
                dt.Columns.Add("fechaingreso");
                dt.Columns.Add("sexo");
                dt.Columns.Add("edad");
                dt.Columns.Add("condicion");              
                Session["dt"] = dt;
                ObtenerDatos();
            }
        }
       
       
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }

       
        private void ObtenerDatos()
        {
            DataSet ds = new DataSet();
            try {
                ds = Neg_Informes.ObtenerEmpleadosEstatusEspecial();

                DataTable dt = (DataTable)Session["dt"];
                dt.Clear();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string depto = ds.Tables[0].Rows[i]["nombre_depto"].ToString();
                    int codigo = Convert.ToInt32(ds.Tables[0].Rows[i]["codigo_empleado"].ToString());
                    string nombre = ds.Tables[0].Rows[i]["nombrecompleto"].ToString();
                    string fechaingreso = ds.Tables[0].Rows[i]["fecha_ingreso"].ToString();
                    string sexo = ds.Tables[0].Rows[i]["sexo"].ToString();
                    string edad = ds.Tables[0].Rows[i]["edad"].ToString();
                    string condicion = ds.Tables[0].Rows[i]["condicion"].ToString();

                    dt.Rows.Add(depto, codigo, nombre, fechaingreso,sexo,edad,condicion);
                }
                MostrarReporte(dt);
            }
            catch (Exception ex) {
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        private void MostrarReporte(DataTable dtReporte)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/EmpleadosEstatusEspecial.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtReporte);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }
       

    }
}