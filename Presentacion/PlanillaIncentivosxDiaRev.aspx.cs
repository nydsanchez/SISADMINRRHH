using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Negocios;
using Datos;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace NominaRRHH.Presentacion
{
    public partial class PlanillaIncentivosxDiaRev : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Dato_Incentivos Dato_Incentivos = new Dato_Incentivos();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        //Neg_Empleados NegEmp = new Neg_Empleados();
        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
               
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlReporte.SelectedValue == "1")//modulo
                {
                    procesoModulos();
                }else if (ddlReporte.SelectedValue == "2")//empleado
                {
                    procesoEmpleado();
                }else if (ddlReporte.SelectedValue == "3")//total
                {
                    procesoConsolidadoInc();
                }else if (ddlReporte.SelectedValue == "4")//pendiente
                {
                    ObtenerIncentivoPendPago();
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }

        }
   
     
        #endregion
        #region METODOS
        void procesoModulos()
        {
            try
            {
                div1.Visible = true;
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                div6.Visible = false;
                DataTable dtcut=new DataTable();

                var sortdt = dtcut.AsEnumerable().OrderBy(c => c.Field<string>("modulo"))
                            .ThenBy(d => d.Field<DateTime>("fecha_producido")).CopyToDataTable();

                cargarReporte(sortdt, 3, ReportViewer3);
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void procesoEmpleado()
        {//tengo que ver si el nuevo proceso de proteccion esta vacio, 
            //de lo contrario este es el que le voy a pasar al proceso de empleados
            try
            {
                div1.Visible = false;
                div2.Visible = true;
                div3.Visible = false;
                div4.Visible = false;
                div6.Visible = false;

                DataTable pagofiltrado = new DataTable();
              
                var sortdt = pagofiltrado.AsEnumerable().OrderBy(c => c.Field<string>("modulo"))
                    .ThenBy(d => d.Field<DateTime>("fecha"))
                    .ThenBy(f => f.Field<int>("codigo_empleado")).CopyToDataTable();

                cargarReporte(sortdt, 1, ReportViewer1);
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
            
        }
        void procesoConsolidadoInc()
        {
            DateTime ini;
            DateTime fin;
            try
            {
                if (Session["INCENTIVOTOTAL"] != null)
                {
                    div1.Visible = false;
                    div2.Visible = false;
                    div3.Visible = true;
                    div4.Visible = false;
                    div6.Visible = false;
                    DataTable dt = Session["INCENTIVOTOTAL"] as DataTable;
                    var sortdt = dt.AsEnumerable().OrderBy(c => c.Field<string>("modulo")).ThenBy(c => c.Field<int>("codigo_empleado"))
                    .CopyToDataTable();

                    cargarReporte(sortdt, 2, ReportViewer2);
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
     
        void ObtenerIncentivoxCodigo()
        {
           
            try
            {
                div1.Visible = false;
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = true;
                div6.Visible = false;

                DataTable cutEmp = new DataTable();
                cutEmp = Session["INCENTIVOSDIARIOCUT"] as DataTable;

                var sortdt = cutEmp.AsEnumerable().Where(c => c.Field<int>("codigo_empleado") == Convert.ToInt32(TxtCodigo.Text))
                    .OrderBy(c => c.Field<string>("modulo"))
                    .ThenBy(d => d.Field<DateTime>("fecha_producido")).CopyToDataTable();

                cargarReporte(sortdt, 4, ReportViewer4);

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void ObtenerIncentivoPendPago()
        {
           
            try
            {
                DataTable dtcut = new DataTable();
                dtcut = Session["INCENTIVOPENDIENTECUT"] as DataTable;


                div1.Visible = false;
                div2.Visible = false;
                div3.Visible = false;
                div4.Visible = false;
                div6.Visible = true;
                var sortdt = dtcut.AsEnumerable().OrderBy(c => c.Field<string>("modulo"))
                .CopyToDataTable();
                cargarReporte(sortdt, 5, ReportViewer5);
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        public void cargarReporte(DataTable dt, int rpt, ReportViewer window)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            window.ProcessingMode = ProcessingMode.Local;
            window.LocalReport.DataSources.Clear();
            if (rpt == 1)//incentivos por dia
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoDiario.rdlc");
            }
            else if (rpt == 2)//calculo total a pagar incluyendo bono
            {               
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoTotal.rdlc");               
            }
            else if (rpt == 3)//calculo pr modulo
            {              
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoModulo.rdlc");               
            }//IncentivoDiarioEmpDet
            else if (rpt == 4)
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoDiarioEmpDet.rdlc");
            }
            else
            {
                window.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoPendientePago.rdlc");
            }
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }

        #endregion                
    }

}
