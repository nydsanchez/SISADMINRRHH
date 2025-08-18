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
namespace NominaRRHH.Presentacion
{
    public partial class VDetalleDeduccionesExt : System.Web.UI.Page
    {
        #region REFERENCIAS
        //CREADO POR WENDY MEMBREÑO

        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                plnemp.Visible = false;
               // obtenerDeducciones();                
            }

        }

        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

            int deduccion = 0, mostrarcuotas = 0;
            int Codigo_empleado = 0;
            if (ddlTipDeduc.SelectedItem.ToString().Trim() != "<-- TODAS -->")
            {
                deduccion = Convert.ToInt32(ddlTipDeduc.SelectedValue);
            }
            int pgpend = ckpagopend.Checked ? 1 : 0;


            DataSet ds = null;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();

            //SE SELECCIONO VER POR EMPLEADO
            if (rbl.SelectedValue.ToString() == "2")
            {
                //SE VALIDA QUE HAYA INGRESADO CODIGO DE EMPLEADO
                if (!(txtcodigoEmp.Text.Trim() == ""))
                {
                    Codigo_empleado = Convert.ToInt32(txtcodigoEmp.Text.Trim());
                    lblAlert.Text = "";
                    alertValida.Visible = false;
                    // SELECCIONO VISUALIZAR DETALLE DE CUOTAS POR EMPLEADOS
                    if (cbD.Checked)
                    {
                        mostrarcuotas = 1;
                        lblAlert.Text = "";
                        alertValida.Visible = false;
                       
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PrestamosDetalle.rdlc");
                       // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ReporteGeneralPrestamos.rdlc");
                    }
                    else
                    {
                        lblAlert.Text = "";
                        alertValida.Visible = false;
                       
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ReporteGeneralPrestamos.rdlc");

                    }
                }
                else
                {
                    lblAlert.Text = "INGRESE EL CODIGO DE EMPLEADO";
                    lblAlert.ForeColor = System.Drawing.Color.White;
                    alertValida.Visible = true;
                    return;
                }

            }
            //SE SELECCIONO VER GENERAL
            else
            {
                lblAlert.Text = "";
             
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ReporteGeneralPrestamos.rdlc");
            }


            ds = Neg_Informes.PrestamosConsultarDetallexEmp(Codigo_empleado, deduccion, mostrarcuotas,pgpend);
            ReportDataSource source = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Add(source);
            ReportViewer1.LocalReport.Refresh();

        }
        private void obtenerDeducciones()
        {
            DataSet dtd = Neg_DevYDed.DeduccionEspecialesActivas();
            this.ddlTipDeduc.DataSource = dtd.Tables[0];
            this.ddlTipDeduc.DataMember = "deducciones";
            this.ddlTipDeduc.DataValueField = "idDeduccion";
            this.ddlTipDeduc.DataTextField = "deduccionNombre";
            this.ddlTipDeduc.DataBind();

            ddlTipDeduc.Items.Insert(0, "<-- TODAS -->");


        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (rbl.SelectedValue.ToString() == "2")
            {
                plnemp.Visible = true;
            }
            else
            {
                plnemp.Visible = false;

            }
        }

        





    }
}