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

namespace NominaRRHH.Presentacion
{
    public partial class VPasivoLaboral : System.Web.UI.Page
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
        Neg_Periodo NPeriodo = new Neg_Periodo();
        //Globales Globales = new Globales();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDptos();
                DataTable dt = new DataTable();
                dt.Columns.Add("gerencia", typeof(string));
                dt.Columns.Add("depto", typeof(string));
                dt.Columns.Add("codigo", typeof(int));
                dt.Columns.Add("nombre", typeof(string));
                dt.Columns.Add("salpromedio", typeof(decimal));
                dt.Columns.Add("salmayor", typeof(decimal));
                dt.Columns.Add("aguinaldodia", typeof(decimal));
                dt.Columns.Add("pagoaguinaldo", typeof(decimal));
                dt.Columns.Add("indemnizaciondia", typeof(decimal));
                dt.Columns.Add("pagoindemnizacion", typeof(decimal));
                dt.Columns.Add("vacacionesdias", typeof(decimal));
                dt.Columns.Add("pagovacaciones", typeof(decimal));
                dt.Columns.Add("fechaingreso", typeof(DateTime));
                dt.Columns.Add("salmensual", typeof(decimal));
                dt.Columns.Add("neto", typeof(decimal));
                dt.Columns.Add("adindemnizacion", typeof(decimal));
                dt.Columns.Add("totalprestaciones", typeof(decimal));
                dt.Columns.Add("MesNumero", typeof(int));
                dt.Columns.Add("MesNombre", typeof(string));
                dt.Columns.Add("Ingreso", typeof(decimal));
                dt.Columns.Add("TipoSalario", typeof(int));
                Session["dt"] = dt;
                obtenerUbicaciones();
                //txtFecCorte.Text = DateTime.Now.ToShortDateString();
                txtFecCorte.Text = DateTime.Now.ToString("dd/MM/yyyy");

                // estaba prefijado a mano
                ddlUbicacion.SelectedValue = "3";


                FechaFinPeriodoAbiertoSel();
                ckproyecta.Checked = false;
            }
        }
        private void obtenerUbicaciones()
        {
            this.ddlUbicacion.DataSource = Neg_Catalogos.CargarUbicaciones();
            this.ddlUbicacion.DataMember = "ubicaciones";
            this.ddlUbicacion.DataValueField = "codigo_ubicacion";
            this.ddlUbicacion.DataTextField = "nombre_ubicacion";
            this.ddlUbicacion.DataBind();
        }
            private void FechaFinPeriodoAbiertoSel()
        {
            try
            {
                DataTable ubicacion = Neg_Catalogos.seleccionarUbicacionesxCod(Convert.ToInt32(ddlUbicacion.SelectedValue.Trim()));


                if (ubicacion.Rows.Count > 0)
                {
                    if (ubicacion.Columns.Contains("tplanilla"))
                    {
                        var tplanillaValue = ubicacion.Rows[0]["tplanilla"];
                        // Verifica si tplanillaValue es DBNull
                        if (tplanillaValue != DBNull.Value)
                        {
                            int tplanilla = Convert.ToInt32(tplanillaValue);
                            dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, tplanilla, 0);
                            if (dtPeriodo.Rows.Count > 0)
                            {
                                Session["fecini"] = Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini"]);
                            }
                            else
                            {
                                throw new Exception("No se tiene registro de periodos de planilla");
                            }
                        }
                        else
                        {
                            // Manejo si tplanilla es DBNull
                            //MessageBox.Show("El valor de 'tplanilla' es nulo.");
                            throw new Exception("El valor de 'tplanilla' es nulo. en tipo planilla");
                        }
                    }
                    else
                    {
                        // Manejo si la columna no existe
                        // MessageBox.Show("La columna 'tplanilla' no existe en el DataTable.");
                        throw new Exception("La columna 'tplanilla' no existe en el DataTable.");
                    }
                }
                else
                {
                    // Manejo si el DataTable está vacío
                    //MessageBox.Show("No se encontraron registros en el DataTable 'ubicacion'.");
                    throw new Exception("No se encontraron registros en el DataTable 'ubicacion'.");
                }

                //dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, Convert.ToInt32(ubicacion.Rows[0]["tplanilla"]), 0);

                //dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.cargarUltPeriodoAbieCat(1, 1,0);
                //if (dtPeriodo.Rows.Count > 0)
                //{
                //    Session["fecini"] = Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini"]);
                //}
                //else
                //{
                //    throw new Exception("No se tiene registro de periodos de planilla");
                //}


            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {
        }

        private void CargarDptos()
        {
            DataSet de= Neg_Catalogos.CargarProcesos();
            ddldepto1.DataSource = de;
            this.ddldepto1.DataMember = "procesos";
            this.ddldepto1.DataValueField = "codigo_depto";
            ddldepto1.DataTextField = "nombre_depto";
            this.ddldepto1.DataBind();

            ddldepto2.DataSource = de;
            this.ddldepto2.DataMember = "procesos";
            this.ddldepto2.DataValueField = "codigo_depto";
            ddldepto2.DataTextField = "nombre_depto";
            this.ddldepto2.DataBind();
        }
        private void ObtenerDatos()
        {
            DataSet ds = new DataSet();
            try {
                if (!chkHistorico.Checked)//empleados activos actuales a un corte anterior(previo) o a un corte posterior al ultimo cierre (sin casilla activa)
                {
                    if (cbgeneral.Checked)
                        ds = Neg_Informes.CargarEmpActivos(1,0);
                    else
                        ds = Neg_Informes.spEmpleadosActivosxDepto(int.Parse(ddldepto1.SelectedValue.Trim().ToString()), int.Parse(ddldepto2.SelectedValue.Trim().ToString()));

                }
                else//casilla historico retrato del personal activo hasta esa fecha corte
                {
                    ds = Neg_Informes.spEmpleadosActivosHistoricoSel(Convert.ToDateTime(txtFecCorte.Text.Trim()));
                }

                DataTable dt = (DataTable)Session["dt"];
                dt.Clear();
                Neg_Liquidacion.Globales.fechaR = string.IsNullOrEmpty(txtFecCorte.Text.Trim()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(txtFecCorte.Text.Trim());
                int proyecta = ckproyecta.Checked ? 1 : 0;
                int hist = chkHistorico.Checked || chkPrevio.Checked ? 1 : 0;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string gerencia = ds.Tables[0].Rows[i]["gerencia"].ToString();
                    string depto = ds.Tables[0].Rows[i]["nombre_depto"].ToString();
                    int codigo = Convert.ToInt32(ds.Tables[0].Rows[i]["codigo_empleado"].ToString());
                    string nombre = ds.Tables[0].Rows[i]["nombrecompleto"].ToString();

                    //columnas de prestaciones a pagar
                    if (codigo == 867153)
                    {

                    }
                    DataSet Datos = Neg_Liquidacion.ObtenerDatosLiquidacion(codigo, 0, 0,0,proyecta,hist,false);
                    string fechaingreso = "";
                    decimal SalPromedio = 0, SalMayor = 0, aguinaldodia = 0, pagoaguinaldo = 0, indemnizaciondia = 0, pagoindemnizacion = 0, vacacionesdia = 0, pagovacaciones = 0, SalMensual = 0;

                    //columna ad indemnizacion
                    DataSet dsdeduc = Neg_DevYDed.DeduccionesOrdinariasObtenerxTipo(codigo, 15);//Obtengo el detalle de la deduccion 
                    decimal AdIndemnizacion = 0, neto = 0, totalprestaciones = 0;
                    string mesnombre = "",tiposalario="";
                    int Ultimos6M;
                    decimal ingresomes;
                   

                    if (dsdeduc.Tables.Count>0 && dsdeduc.Tables[0].Rows.Count>0)                    
                        AdIndemnizacion = Convert.ToDecimal(dsdeduc.Tables[0].AsEnumerable().Sum(row => Convert.ToDecimal(row["Debe"])));                    
                    else                    
                        AdIndemnizacion = 0;                    
                                                     
                    if (Datos != null)
                    {
                        Ultimos6M = 6;
                        for (int j = 0; j < Datos.Tables[0].Rows.Count; j++)
                        {
                            SalPromedio = Convert.ToDecimal(Datos.Tables[1].Rows[0]["salPromedioDia"].ToString());
                            SalMayor = Convert.ToDecimal(Datos.Tables[1].Rows[0]["salMayorDia"].ToString());
                            aguinaldodia = Convert.ToDecimal(Datos.Tables[1].Rows[0]["AguinaldoDia"].ToString());
                            pagoaguinaldo = Convert.ToDecimal(Datos.Tables[1].Rows[0]["Aguinaldo"].ToString());
                            indemnizaciondia = Convert.ToDecimal(Datos.Tables[1].Rows[0]["IndemnizacionDia"].ToString());
                            pagoindemnizacion = Convert.ToDecimal(Datos.Tables[1].Rows[0]["Indemnizacion"].ToString());
                            vacacionesdia = Convert.ToDecimal(Datos.Tables[1].Rows[0]["vacacionesDia"].ToString());
                            pagovacaciones = Convert.ToDecimal(Datos.Tables[1].Rows[0]["Vacaciones"].ToString());
                            fechaingreso = Convert.ToDateTime(Datos.Tables[1].Rows[0]["fechaingreso"]).ToShortDateString();
                            SalMensual = Convert.ToDecimal(Datos.Tables[1].Rows[0]["salMensual"].ToString());
                            totalprestaciones = Convert.ToDecimal(Datos.Tables[1].Rows[0]["totalPagar"].ToString());
                            tiposalario = Datos.Tables[1].Rows[0]["TipoSalario"].ToString();
                            //totalprestaciones = pagoaguinaldo + pagoindemnizacion + pagovacaciones;
                            neto = totalprestaciones - AdIndemnizacion;

                            mesnombre = Datos.Tables[0].Rows[j]["MesNombre"].ToString();                           
                            ingresomes = Convert.ToDecimal(Datos.Tables[0].Rows[j]["Ingreso"]);
                           
                            dt.Rows.Add(gerencia,depto, codigo, nombre, Math.Round(SalPromedio, 2), Math.Round(SalMayor, 2), Math.Round(aguinaldodia, 2), Math.Round(pagoaguinaldo, 2), Math.Round(indemnizaciondia, 2), Math.Round(pagoindemnizacion, 2), Math.Round(vacacionesdia, 2), Math.Round(pagovacaciones, 2), fechaingreso, Math.Round(SalMensual, 2), Math.Round(neto, 2), Math.Round(AdIndemnizacion, 2), Math.Round(totalprestaciones, 2), Ultimos6M, mesnombre, ingresomes,tiposalario);
                            Ultimos6M--;
                        }
                    }
                }
                MostrarReporte(dt);
            }
            catch (Exception ex) {
                throw new Exception("Error al obtener datos de pasivo laboral");
            }
        }

        private void MostrarReporte(DataTable dtReporte)
        {
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PasivoLaboral.rdlc");
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtReporte);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportViewer1.LocalReport.DataSources.Add(datasource);
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            //Globales.fechaR = null;
            alertValida.Visible = false;
            lblAlert.Visible = false;
            int result;
            try
            {
                // tood vhpo

                FechaFinPeriodoAbiertoSel();

                // Convertir la fecha del TextBox a DateTime
                DateTime fechaCorte = Convert.ToDateTime(txtFecCorte.Text.Trim());

                // Obtener la fecha de inicio de la sesión y restarle un día
                DateTime fechaInicio = Convert.ToDateTime(Session["fecini"]).AddDays(-1);

                // Comparar las fechas
                result = DateTime.Compare(fechaCorte, fechaInicio);

                //result = DateTime.Compare(Convert.ToDateTime(txtFecCorte.Text.Trim()), Convert.ToDateTime(Session["fecini"]).AddDays(-1));//1ero diciembre anio anterior

                //historico, retrato del pasivo a un corte previo
                //previo, estatus de los empleados activos actuales a un corte previo
                if (chkHistorico.Checked || chkPrevio.Checked)
                {
                    if (result < 0)
                    {
                        ObtenerDatos();
                    }
                    else
                    {
                        throw new Exception("La opcion Historico/Previo esta activada, la fecha corte debe ser menor al cierre del ultimo periodo");
                    }
                }
                else
                {
                    if (result >= 0)
                    {
                        ObtenerDatos();
                    }
                    else
                    {
                        throw new Exception("El parametro fecha debe ser mayor al cierre del ultimo periodo");
                    }
                }
                
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }

        protected void cbgeneral_CheckedChanged(object sender, EventArgs e)
        {
            if (cbgeneral.Checked)
            {
                ddl1.Visible = false;
                ddl2.Visible = false;
            }
            else
            {
                ddl1.Visible = true;
                ddl2.Visible = true;
            }
        }

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            FechaFinPeriodoAbiertoSel();
        }

        protected void chkHistorico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHistorico.Checked)
            {
                chkPrevio.Checked = false;
            }
        }

        protected void chkPrevio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrevio.Checked)
            {
                chkHistorico.Checked = false;
            }
        }
    }
}