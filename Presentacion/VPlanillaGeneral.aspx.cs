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
    public partial class VPlanillaGeneral : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ObtenerTipoContratosActivo();
            }
        }
        private void ObtenerTipoContratosActivo()
        {
            DataTable dt= Neg_Catalogos.ObtenerTipoContratosActivo();
            dt.Rows.Add(0,"Todos");
            this.ddlContrato.DataSource = dt;           
            this.ddlContrato.DataValueField = "idcontrato";
            this.ddlContrato.DataTextField = "Descripcion";
            this.ddlContrato.DataBind();
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
                DataSet ds2 = null;

               
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaConsolidada.rdlc");

                if(ChkConsolida.Checked)//reporte consolidad dos periodos
                {
                    //PlanillaResumenPeriodoCSel
                    if (!string.IsNullOrEmpty(txtPeriodo2.Text.Trim()))
                    {
                        ds = Neg_Informes.PlanillaResumenPeriodoCSel(Convert.ToInt32(txtPeriodo.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim()), 0,0);
                        ds2 = Neg_Informes.PlanillaTotalSel(Convert.ToInt32(txtPeriodo.Text.Trim()), 10,Convert.ToInt32(txtPeriodo2.Text.Trim()));
                    }
                }
                else//reporte normal
                {
                    ds = Neg_Informes.CargarPlanillaConsolidada(Convert.ToInt32(txtPeriodo.Text.Trim()), 10, 0,0);
                    ds2 = Neg_Informes.PlanillaTotalSel(Convert.ToInt32(txtPeriodo.Text.Trim()), 10,0);
                }

                

                DataTable dsf1 =new DataTable();
                DataTable ds2f1 = new DataTable();
                DataRow[] dr1 = null, dr2 = null;
                string condicion = "";
                if (ChkAll.Checked)//tarjeta
                {
                    condicion = "cuentabancaria<>'0' and cuentabancaria<>'' and cuentabancaria is not null and neto>0";
                    if (ddlContrato.SelectedValue != "0")//contrato especifico
                    {
                        condicion += " and idcontrato=" + ddlContrato.SelectedValue.Trim();

                        dr1 = ds.Tables[0].Select(condicion).ToArray();
                        dr2 = ds2.Tables[0].Select(condicion).ToArray();

                        if (dr1.Length > 0 && dr2.Length > 0)
                        {
                            dsf1 = dr1.CopyToDataTable();//ds.Tables[0].Select(condicion).CopyToDataTable();                    
                            ds2f1 = dr2.CopyToDataTable();//ds2.Tables[0].Select(condicion).CopyToDataTable();
                        }
                    }
                    else//todos los contratos
                    {
                        dsf1 = ds.Tables[0].Select(condicion).CopyToDataTable();                    
                        ds2f1 = ds2.Tables[0].Select(condicion).CopyToDataTable();
                    }
                    
                }
                else//todos
                {
                    if (ddlContrato.SelectedValue != "0")//contrato especifico
                    {
                        condicion += "idcontrato=" + ddlContrato.SelectedValue.Trim();

                        dr1 = ds.Tables[0].Select(condicion).ToArray();
                        dr2 = ds2.Tables[0].Select(condicion).ToArray();

                        if (dr1.Length > 0 && dr2.Length > 0)
                        {
                            dsf1 = dr1.CopyToDataTable();//ds.Tables[0].Select(condicion).CopyToDataTable();                    
                            ds2f1 = dr2.CopyToDataTable();//ds2.Tables[0].Select(condicion).CopyToDataTable();
                        }
                    }
                    else//todos los contratos
                    {
                        dsf1 = ds.Tables[0];
                        ds2f1 = ds2.Tables[0];
                    }
                    
                }

                //TODO: VHPO
                // se crea tabla temporal para emprarentar movimientos con cada empleado 
                // para que el reporte presente informacion de forma correcta

                //// Crear el DataTable
                //DataTable conceptosTable = new DataTable("conceptos");

                //// Definir las columnas
                //conceptosTable.Columns.Add("codigo", typeof(int));
                //conceptosTable.Columns.Add("semana", typeof(int));
                //conceptosTable.Columns.Add("tipo", typeof(int));
                //conceptosTable.Columns.Add("tipe", typeof(string));
                //conceptosTable.Columns.Add("concepto", typeof(string));
                //conceptosTable.Columns.Add("valor", typeof(decimal));
                //conceptosTable.Columns.Add("horasextras", typeof(decimal));
                //conceptosTable.Columns.Add("nombre", typeof(string));
                //conceptosTable.Columns.Add("empresa", typeof(string));
                //conceptosTable.Columns.Add("periodo", typeof(int));
                //conceptosTable.Columns.Add("cuentabancaria", typeof(string));
                //conceptosTable.Columns.Add("depto", typeof(string));
                //conceptosTable.Columns.Add("numero_seguro", typeof(string));
                //conceptosTable.Columns.Add("horast", typeof(decimal));
                //conceptosTable.Columns.Add("salario", typeof(decimal));
                //conceptosTable.Columns.Add("dimpuestos", typeof(decimal));
                //conceptosTable.Columns.Add("dsegurosocial", typeof(decimal));
                //conceptosTable.Columns.Add("ingresos", typeof(decimal));
                //conceptosTable.Columns.Add("adelantorenovacion", typeof(int));
                //conceptosTable.Columns.Add("egresos", typeof(decimal));
                //conceptosTable.Columns.Add("neto", typeof(decimal));
                //conceptosTable.Columns.Add("vacaciones", typeof(decimal));
                //conceptosTable.Columns.Add("totalneto", typeof(decimal));
                //conceptosTable.Columns.Add("totaldimpuesto", typeof(decimal));
                //conceptosTable.Columns.Add("totalseguro", typeof(decimal));
                //conceptosTable.Columns.Add("mostrar", typeof(int));
                //conceptosTable.Columns.Add("idContrato", typeof(int));


                //// Obtener referencias de los DataTables
                //DataTable dtDs2f1 = ds2.Tables[0]; // DataTable ds2f1
                //DataTable dtDsf1 = ds.Tables[0];     // DataTable dsf1

                //// Asegurar que conceptosTable tenga las mismas columnas que dsf1
                //conceptosTable = dtDsf1.Clone();

                //// Recorrer cada empleado en ds2f1
                //foreach (DataRow rowDs2 in dtDs2f1.Rows)
                //{
                //    string codigoEmpleado = rowDs2["codigo_empleado"].ToString();

                //    // Filtrar registros en dsf1 que coincidan con el código de empleado
                //    DataRow[] filasRelacionadas = dtDsf1.Select($"codigo = '{codigoEmpleado}'");

                //    if (filasRelacionadas.Length > 0)
                //    {
                //        // Agregar todas las filas encontradas en dsf1 a conceptosTable
                //        foreach (DataRow fila in filasRelacionadas)
                //        {
                //            conceptosTable.ImportRow(fila);
                //        }
                //    }
                //    else
                //    {
                //        // No hay datos en dsf1 para este empleado, agregar un registro por defecto
                //        DataRow nuevaFila = conceptosTable.NewRow();
                //        nuevaFila["codigo"] = codigoEmpleado;

                //        // Asignar valores en cero o vacíos según el tipo de datos
                //        foreach (DataColumn col in conceptosTable.Columns)
                //        {
                //            if (col.ColumnName != "codigo") // Evitar sobrescribir el código
                //            {
                //                if (col.DataType == typeof(string))
                //                    nuevaFila[col.ColumnName] = "";
                //                else if (col.DataType == typeof(int) || col.DataType == typeof(decimal) || col.DataType == typeof(double))
                //                    nuevaFila[col.ColumnName] = 0;
                //            }
                //        }

                //        // Agregar la fila de valores por defecto
                //        conceptosTable.Rows.Add(nuevaFila);
                //    }
                //}

                //// Confirmar cambios en conceptosTable
                //conceptosTable.AcceptChanges();

                //dsf1 = conceptosTable;

                //} 

                //ReportParameter[] parameters = new ReportParameter[2];
                //parameters[0] = new ReportParameter("periodo", txtPeriodo.Text.Trim());
                //parameters[1] = new ReportParameter("semana",  "10");
                //this.ReportViewer1.LocalReport.SetParameters(parameters);

                ReportDataSource datasource = new ReportDataSource("DataSet1", dsf1);               
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

       
        protected void ChkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            
            if (ChkEfectivo.Checked)
            {             
                this.ChkAll.Checked = false;
            }
           
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
           
            if (ChkAll.Checked)
            {              
                ChkEfectivo.Checked = false;
            }
         
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