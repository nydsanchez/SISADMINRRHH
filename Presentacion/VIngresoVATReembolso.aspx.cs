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

using System;

namespace NominaRRHH.Presentacion
{
    public partial class VIngresoVATReembolso : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Marca Neg_Marca = new Neg_Marca();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

              
            }
          
        }
    
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            int Periodo = 0;
            Periodo = Convert.ToInt32(txtperiodo.Text.Trim());

            DataTable dt= ObtenerDatos(Periodo,int.Parse(ddlTipo.SelectedValue));

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IngresoReembolsoVAT.rdlc");
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("periodo", Periodo.ToString().Trim());           

            this.ReportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource datasource = new ReportDataSource("DataSet1", dt);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);

        }
        public DataTable ObtenerDatos(int periodo, int semana)
        {
            try
            {
                int[] codemp = null;
                DataRow[] datosMarca = null;
                DataRow[] reembolsoemp = null;
                DataTable dtInD = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable detalleemp = new DataTable();


                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(periodo);
                DateTime ini = dtPeriodo[0].fechaini;
                DateTime fin = dtPeriodo[0].fechafin2;

                DataTable reporte = new DataTable();
                if (periodo >= 399)
                {
                    dt2 = Neg_Incentivos.ObtenerIncentivoPlantaVATSel(periodo, semana);

                    if (dt2.Rows.Count > 0)
                    {
                        ////personal vat
                        codemp = dt2.AsEnumerable().Select(u => u.Field<int>("codigo_empleado")).ToArray();

                        dtInD = Neg_Marca.ObtenerMarcasHorasOficial(ini, fin, 2, 3, 0);//todas las marccas
                                                                                       //solo las marcas del personal VAT
                        datosMarca = dtInD.AsEnumerable().Where(c => codemp.Contains(c.Field<int>("codigo_empleado"))).ToArray();

                        detalleemp = Neg_Incentivos.ObtenerDesgloceViatico(dt2.AsEnumerable().ToArray(), ini, datosMarca, periodo);//desgloce concepto mas reembolso.

                        reembolsoemp = detalleemp.AsEnumerable().Where(c => c.Field<decimal>("saldo") > 0).GroupBy(c => c.Field<int>("codigo_empleado")).Select(c => c.FirstOrDefault()).OrderBy(c => c.Field<string>("nombre_depto")).ToArray();

                        if (reembolsoemp != null)
                        {
                            if (reembolsoemp.Length > 0)
                            {
                                reporte = reembolsoemp.CopyToDataTable();
                            }
                        }
                    }

                }
                return reporte;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
    }
}