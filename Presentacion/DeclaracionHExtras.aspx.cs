using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Negocios;
using Datos;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration;

namespace NominaRRHH.Presentacion
{
    public partial class DeclaracionHExtras : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Marca Neg_Marca = new Neg_Marca();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                txtFechaIni.Text = DateTime.Now.ToShortDateString();
                txtFechaFin.Text = DateTime.Now.ToShortDateString();
                ObtenerIngresos();
            }
        }
                 
        public void cargarReporte(DataTable dt, ReportViewer rpt, int opcion)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            rpt.ProcessingMode = ProcessingMode.Local;
            rpt.LocalReport.DataSources.Clear();
            if (!ckagrupar.Checked)
            {
                rpt.LocalReport.ReportPath = Server.MapPath("../Reportes/DeclaracionHoraExtras.rdlc");

            }
            else 
            {
                if (ddlGrupo.SelectedValue.ToLower()=="na")
                {
                    rpt.LocalReport.ReportPath = Server.MapPath("../Reportes/DeclaracionHoraNA.rdlc");
                }
                else
                {
                    rpt.LocalReport.ReportPath = Server.MapPath("../Reportes/DeclaracionHoraExtras.rdlc");

                }
                

            }
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Neg_Empleados Neg_Empleados = new Neg_Empleados();
            DataTable DetEmpleados = Neg_Empleados.ObtenerInfoDetEmpleado(userDetail.getUserCodEmpleado().ToString());
            ReportParameter[] parameters = new ReportParameter[2];
            string nombrerequiere = DetEmpleados.Rows[0]["primer_nombre"].ToString() + " " + DetEmpleados.Rows[0]["primer_apellido"].ToString();
            parameters[0] = new ReportParameter("nombrerequiere", nombrerequiere);
            if (ckagrupar.Checked)
            {
                parameters[1] = new ReportParameter("nombrerubro", ddlGrupo.SelectedItem.Text);
            }
            else
            {
                parameters[1] = new ReportParameter("nombrerubro", ddlIngreso.SelectedItem.Text);
            }
            

            this.ReportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            rpt.LocalReport.DataSources.Add(source);
            rpt.LocalReport.Refresh();

        }

        private void ObtenerIngresos()
        {
            Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
            DataTable catalog =  Neg_DevYDed.cargarIngresosAplicaDia().Tables[0];

            ddlIngreso.DataSource = catalog;
            ddlIngreso.DataValueField = "idDevengado";
            ddlIngreso.DataTextField = "devengadoNombre";
            ddlIngreso.DataBind();

        }
        private void FiltrarDatosxIngresos()
        {
            DataTable he = new DataTable();
            DataRow[] personal = null;
            he = Session["HorasExtrasAprob"] as DataTable;
            if (ckagrupar.Checked)//HE, 1,28//HFERIADO, 31,32//ERRORES, 33
            {
                if (ddlGrupo.SelectedValue.ToLower() == "he")
                {
                    personal = he.AsEnumerable().Where(c => c.Field<int>("periodo") > 0 && (c.Field<int>("tipoingrdeduc") == 1 || c.Field<int>("tipoingrdeduc") == 28)).ToArray();
                }
                else if (ddlGrupo.SelectedValue.ToLower() == "feriado")
                {
                    personal = he.AsEnumerable().Where(c => c.Field<int>("periodo") > 0 && (c.Field<int>("tipoingrdeduc") == 31 || c.Field<int>("tipoingrdeduc") == 32)).ToArray();
                }
                else
                {
                    personal = he.AsEnumerable().Where(c => c.Field<int>("periodo") > 0 && (c.Field<int>("tipoingrdeduc") == 33)).ToArray();
                }
            }
            else
            {
                personal = he.AsEnumerable().Where(c => c.Field<int>("periodo") > 0 && c.Field<int>("tipoingrdeduc") == Convert.ToInt32(ddlIngreso.SelectedValue)).ToArray();
            }
            
            
            if (personal.Length > 0)
            {
                personal = personal.OrderBy(c => c.Field<string>("nombre_depto")).ThenBy(c => c.Field<int>("codigo_empleado")).ToArray();
                cargarReporte(personal.CopyToDataTable(), ReportViewer1, 1);
            }
            else
            {
                cargarReporte(new DataTable(), ReportViewer1, 1);
            }

        }
        private void ObtenerHorasExtras()
        {
            //obtengo horas extras
            //agrupo departamentos y lleno el ddl 
            //con el primer elementos seleccionado filtro las horas extras
            //horas extras se filtran por deptamento
            try
            {
                DataTable he = new DataTable();
                if (ckagrupar.Checked && ddlGrupo.SelectedValue.ToLower() == "na")
                {
                    he = Neg_Marca.ObtenerHorasExtrasAprobacion(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text), 1, 0, true);
                }
                else
                {
                    he = Neg_Marca.ObtenerHorasExtrasAprobacion(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaIni.Text), 1, 0, true);
                }
                
                Session["HorasExtrasAprob"] = he;
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }

        }
      
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFechaIni.Text))
                {
                    throw new Exception("Debe ingresar una fecha valida.");
                }
                ObtenerHorasExtras();               
                FiltrarDatosxIngresos();

                Button3.Focus();
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        protected void ckagrupar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckagrupar.Checked)
                {
                    divtipos.Visible = false;
                    divgrupos.Visible = true;
                }
                else
                {
                    divtipos.Visible = true;
                    divgrupos.Visible = false;
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckagrupar.Checked && ddlGrupo.SelectedValue.ToLower()=="na")
                {
                    divffin.Visible = true;                   
                }
                else
                {
                    divffin.Visible = false;
                  
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
    }
}