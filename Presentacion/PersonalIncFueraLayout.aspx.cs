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
    public partial class PersonalIncFueraLayout : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();      
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

        
       
        #endregion

        #region METODOS


        #endregion

       
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPeriodo.Text) )
                {
                    throw new Exception("Debe ingresar periodo valido");
                }
                div2.Visible = true;
                int filtro = ckfiltro.Checked ? 1 : 0;
                DataTable prot = Neg_Incentivos.PlnObtenerPersonalIncFueraLayout(Convert.ToInt32(txtPeriodo.Text),filtro);
                

                cargarReporte(prot, 2, ReportViewer1);

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
            window.LocalReport.ReportPath = Server.MapPath("../Reportes/PersonalInFueraLayout.rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }
    }

}
