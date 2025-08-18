using System;
using System.Collections.Generic;
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
    public partial class VPlanillaGeneralViatico : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            if (txtPeriodo.Text.Trim().Length != 0)
            {
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                //ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaTotal.rdlc");
                DataSet ds=null;
              
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaConsolidadaC.rdlc");

                if (ChkConsolida.Checked)//reporte consolidad dos periodos
                {
                    //PlanillaResumenPeriodoCSel
                    if (!string.IsNullOrEmpty(txtPeriodo2.Text.Trim()))
                    {
                        ds = Neg_Informes.PlanillaResumenPeriodoCSel(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim()), 1,1);
                    }
                }
                else
                {
                    ds = Neg_Informes.CargarPlanillaConsolidada(Convert.ToInt32(txtPeriodo.Text.Trim()), 10, 1,1);
                }

               
                //ds2 = Neg_Informes.PlanillaTotalSel(Convert.ToInt32(txtPeriodo.Text.Trim()), 10);

                DataTable dsf1 =new DataTable();                

                if (ChkAll.Checked)
                {
                    string condicion = "cuentabancaria<>'0' and cuentabancaria<>'' and cuentabancaria is not null";
                    dsf1 = ds.Tables[0].Select(condicion).CopyToDataTable();
                    //ds2f1= ds2.Tables[0].Select(condicion).CopyToDataTable();
                }
                else
                {
                    dsf1 = ds.Tables[0];
                    //ds2f1 = ds2.Tables[0];
                }
                //dsf1.Columns.Add("ingresost", typeof(decimal));
                //dsf1.Columns.Add("egresost", typeof(decimal));
                DataTable rpt = new DataTable();
                rpt.Columns.Add("depto", typeof(string));
                rpt.Columns.Add("codigo", typeof(decimal));
                rpt.Columns.Add("nombre", typeof(string));
                rpt.Columns.Add("cuentabancaria", typeof(string));
                //rpt.Columns.Add("neto", typeof(decimal));
                rpt.Columns.Add("tipo", typeof(int));
                rpt.Columns.Add("tipe", typeof(string));
                rpt.Columns.Add("concepto", typeof(string));
                rpt.Columns.Add("valor", typeof(decimal));
                rpt.Columns.Add("netot", typeof(decimal));
                rpt.Columns.Add("contrato", typeof(string));

                DataTable ds2f1 = new DataTable();
                ds2f1.Columns.Add("codigo_empleado", typeof(int));
                ds2f1.Columns.Add("Inss", typeof(decimal));
                ds2f1.Columns.Add("Impuestos", typeof(decimal));
                ds2f1.Columns.Add("NetoTotal", typeof(decimal));

                //decimal ingresos = 0, egresos = 0, neto = 0;
                //foreach (DataRow dr in dsf1.Rows)
                //{
                //    ingresos = 0; egresos = 0; neto = 0;
                //    neto += Convert.ToDecimal(dr["neto"]);
                //    //ingresos += Convert.ToDecimal(dr["salario"]);
                //    ingresos += dsf1.Select("codigo=" + dr["codigo"].ToString() + " and tipo=1 and mostrar=0").Sum(r => Convert.ToDecimal(r["valor"]));

                //    //egresos += Convert.ToDecimal(dr["dsegurosocial"]) + Convert.ToDecimal(dr["dimpuestos"]);
                //    egresos += dsf1.Select("codigo=" + dr["codigo"].ToString() + " and tipo=2").Sum(r => Convert.ToDecimal(r["valor"]));
                //    //neto = ingresos - egresos;
                //    neto += ingresos - egresos;
                //    //dr["ingresost"] = ingresos;
                //    //dr["egresost"] = egresos;
                //    dr["netot"] = neto;                    
                //}
                Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
                DataTable dtcont = Neg_Catalogos.ObtenerTipoContratosActivo();
                DataTable Empleados = dsf1.Rows.Cast<DataRow>().GroupBy(c => new { c2 = c["codigo"] }).Select(grp => grp.First()).CopyToDataTable();
                for (int i = 0; i < Empleados.Rows.Count; i++)
                {            
                    string depto = Empleados.Rows[i]["depto"].ToString();
                    int codigo = Convert.ToInt32(Empleados.Rows[i]["codigo"]);
                    string nombre = Empleados.Rows[i]["nombre"].ToString();
                    string cuentabancaria = Empleados.Rows[i]["cuentabancaria"].ToString();
                    decimal neto = 0;//Convert.ToDecimal(Empleados.Rows[i]["neto"]);
                    decimal inss = Convert.ToDecimal(Empleados.Rows[i]["dsegurosocial"]);
                    decimal ir = Convert.ToDecimal(Empleados.Rows[i]["dimpuestos"]);
                    decimal netot = neto;
                    string contrato = dtcont.AsEnumerable().Where(c => c.Field<int>("idcontrato") == Convert.ToInt32(Empleados.Rows[i]["idcontrato"])).Select(c => c.Field<string>("Descripcion")).FirstOrDefault();
                    if (codigo== 866328)
                    {

                    }
                    
                    DataRow[] ingresosEspeciales = dsf1.Select("codigo=" + Empleados.Rows[i]["codigo"].ToString() + " and tipo=1 and mostrar=0 and valor>0").OrderBy(c=>c.Field<string>("concepto")).ToArray();

                    if (ingresosEspeciales.Length>0)
                    {                  
                        netot += ingresosEspeciales.Sum(r => Convert.ToDecimal(r["valor"]));
                    }
                    if (netot>0)
                    {
                        //rpt.Rows.Add(depto, codigo, nombre, cuentabancaria, 1, "Ingreso", "Neto Planilla", neto, netot,contrato);
                    }
                    //totales por empleado
                    ds2f1.Rows.Add(codigo, inss, ir, netot);

                    foreach (DataRow dr in ingresosEspeciales)
                    {
                        if (netot > 0)
                        {
                            rpt.Rows.Add(depto, codigo, nombre, cuentabancaria, Convert.ToInt32(dr["tipo"]), dr["tipe"], dr["concepto"].ToString(), Convert.ToDecimal(dr["valor"]), netot,contrato);
                        }
                    }
                    //rpt.Columns.Add("depto", typeof(int));
                    //rpt.Columns.Add("codigo", typeof(decimal));
                    //rpt.Columns.Add("nombre", typeof(decimal));
                    //rpt.Columns.Add("cuentabancaria", typeof(decimal));
                    //rpt.Columns.Add("neto", typeof(decimal));
                    //rpt.Columns.Add("tipo", typeof(int));
                    //rpt.Columns.Add("tipe", typeof(string));
                    //rpt.Columns.Add("concepto", typeof(string));
                    //rpt.Columns.Add("valor", typeof(decimal));
                    //rpt.Columns.Add("netot", typeof(decimal));



                    
                }
                //} 

                //ReportParameter[] parameters = new ReportParameter[2];
                //parameters[0] = new ReportParameter("periodo", txtPeriodo.Text.Trim());
                //parameters[1] = new ReportParameter("semana",  "10");
                //this.ReportViewer1.LocalReport.SetParameters(parameters);

                ReportDataSource datasource = new ReportDataSource("DataSet1", rpt);               
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                //if (ddlTipoReporte.SelectedValue.Trim() == "2")
                //{
                    ReportDataSource datasource2 = new ReportDataSource("DataSet2", ds2f1);
                    ReportViewer1.LocalReport.DataSources.Add(datasource2);
                //} 
                this.ReportViewer1.LocalReport.SubreportProcessing +=
                new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
                ReportViewer1.ShowPrintButton=true;

            }
            else
            {
                txtPeriodo.Focus();
                return;
            }
        }

        //protected void ddlTipoPlanilla_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlTipoPlanilla.SelectedValue.Trim() == "1" || ddlTipoPlanilla.SelectedValue.Trim() == "5")//Catorcenal
        //    {
        //        lblSemana.Visible = true;
        //        ddlTipo.Visible = true;
        //    }
        //    else
        //    {
        //        lblSemana.Visible = false;
        //        ddlTipo.SelectedValue="10";
        //        ddlTipo.Visible = false;
        //    }
        //}
        protected void ChkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            //if (!ChkEmail.Checked)
            //{
            //    ChkEfectivo.Checked = false;
            //}
            if (ChkEfectivo.Checked)
            {             
                this.ChkAll.Checked = false;
            }
            //else
            //{               
            //    ChkAll.Checked = false;
            //}
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (!ChkEmail.Checked)
            //{
            //    ChkAll.Checked = false;
            //}
            if (ChkAll.Checked)
            {              
                ChkEfectivo.Checked = false;
            }
            //else
            //{              
            //    ChkEfectivo.Checked = false;
            //}
        }

        protected void ChkConsolida_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkConsolida.Checked)
            {
                divp.Visible = true;
            }
            else
            {
                divp.Visible = false;
            }

        }
    }
}