using System;
using System.Data;
using Microsoft.Reporting.WebForms;
using Negocios;

namespace NominaRRHH.Presentacion
{
    public partial class VDevolventes : System.Web.UI.Page
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
                obtenerDeducciones();
            }
        }


        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }

        private void obtenerDeducciones()
        {
            this.ddlTipDeduc.DataSource = Neg_DevYDed.AdelantosEspecialesActivos();
            this.ddlTipDeduc.DataMember = "deducciones";
            this.ddlTipDeduc.DataValueField = "idDeduccion";
            this.ddlTipDeduc.DataTextField = "deduccionNombre";
            this.ddlTipDeduc.DataBind();

            ddlTipDeduc.Items.Insert(0, "<-- TODAS -->");
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {

            int deduccion = 0;
            int Codigo_empleado = 0;
            if (ddlTipDeduc.SelectedItem.ToString().Trim() != "<-- TODAS -->")
            {
                deduccion = Convert.ToInt32(ddlTipDeduc.SelectedValue);
            }
            DataSet ds = null;
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();

            //SE SELECCIONO VER POR EMPLEADO
            if (rbl.SelectedValue.ToString() == "2")
            {
                //SE VALIDA QUE HALLA INGRESADO CODIGO DE EMPLEADO
                if (!(txtcodigoEmp.Text.Trim() == ""))
                {
                    Codigo_empleado = Convert.ToInt32(txtcodigoEmp.Text.Trim());
                    lblAlert.Text = "";
                    alertValida.Visible = false;
                }
                else
                {
                    lblAlert.Text = "INGRESE EL CODIGO DE EMPLEADO";
                    lblAlert.ForeColor = System.Drawing.Color.White;
                    alertValida.Visible = true;
                    return;
                }
            }
            ds = Neg_Informes.AdelantosEspecialesDetalle(Codigo_empleado, deduccion);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/AdelantosE.rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", ds.Tables[0]);
            ReportViewer1.LocalReport.DataSources.Add(source);
            ReportViewer1.LocalReport.Refresh();
           
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