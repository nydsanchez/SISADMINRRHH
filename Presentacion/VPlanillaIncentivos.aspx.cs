using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Negocios;
using Microsoft.Reporting.WebForms;

namespace NominaRRHH
{
    public partial class VPlanillaIncentivos : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            if (txtPeriodo.Text != "")
            {
                int periodo = int.Parse(txtPeriodo.Text);
                int semana = 0;
                if (ddlTipo.SelectedValue.ToString() == "Consolidado")
                {
                    semana = 3;
                }
                else
                {
                    semana = int.Parse(ddlTipo.SelectedValue.ToString());
                   
                }
                dt = Neg_Incentivos.IncentivoHistoricoSelect(periodo, semana);
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaIncentivo.rdlc");
                ReportDataSource source = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Add(source);
                ReportViewer1.LocalReport.Refresh();
            }


        }
    }
}