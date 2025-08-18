using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
//////
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Negocios;
using Datos;


namespace NominaRRHH.Presentacion
{
    public partial class ComprobanteNew : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Periodo NPeriodo = new Neg_Periodo();
        #endregion       
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                obtenerProcesos();
                ddlProceso.SelectedIndex = -1;
            }
        }
        
        private void obtenerProcesos()
        {
            this.ddlProceso.DataSource = Neg_Catalogos.CargarProcesos();
            this.ddlProceso.DataMember = "procesos";
            this.ddlProceso.DataValueField = "codigo_depto";
            this.ddlProceso.DataTextField = "nombre_depto";
            this.ddlProceso.DataBind();
        }
      
        private void CrearPDF(string html)
        {
            try {
                string CodigoDocumento = Convert.ToString(System.Guid.NewGuid().ToString());
                string fileName = HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\Bolante" + CodigoDocumento.Trim() + ".pdf";
                //Ultimo
                Document doc = new Document(PageSize.LETTER, 15, 15, 0, 20);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                doc.Open();
                iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
                hw.Parse(new StringReader(html));
                doc.Close();
                writer.Close();
                ShowPdf(HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\Bolante" + CodigoDocumento.Trim() + ".pdf");
            }
            catch (Exception ex) {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }

        private void ShowPdf(string s)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + s);
            Response.ContentType = "application/pdf";
            Response.WriteFile(s);
            Response.Flush();
            Response.Clear();
        }
      
        private string Crear(int filtroemail)
        {
            //empleados pagados en planilla
            string html = "";
            try {
                if (TxtBuscar.Text.Trim()=="")
                {
                    throw new Exception("Debe especificar periodo");
                }
                int tplanilla = 0, semana2 = 2, periodo2= 0;//en caso d consolidar esta variable puede tener semana 1 de otro periodo
                DateTime fechaini = new DateTime();
                DateTime fechafin = new DateTime();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(TxtBuscar.Text.Trim()));
                tplanilla = dtPeriodo[0].tplanilla;
                periodo2 = Convert.ToInt32(TxtBuscar.Text.Trim());
                fechaini = dtPeriodo[0].fechaini;
                fechafin = dtPeriodo[0].fechafin2;
                //aqui hacer conversion periodo semanal a catorcenal
                string encabezado = TxtBuscar.Text.Trim();

                if (ChkConsolida.Checked)//reporte consolidad dos periodos
                {
                    if (!string.IsNullOrEmpty(txtPeriodo2.Text.Trim()))
                    {
                        encabezado += " - " + txtPeriodo2.Text.Trim();
                        if (!Neg_Informes.PlnPeriodosConsolidadosEqvIns(Convert.ToInt32(TxtBuscar.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim())))
                        {
                            throw new Exception("Error al consolidar periodo");
                        }
                        //if (!Neg_Informes.PlnConversionPeriodoConsolidado(Convert.ToInt32(TxtBuscar.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim()), 1))
                        //{
                        //    throw new Exception("Error al convertir periodo");
                        //}
                    }
                    if (dtPeriodo[0].tperiodo == 1 && dtPeriodo[0].tplanilla == 4)
                    {
                        //cuando se consolidan dos periodos semanales se debe dar tratamiento de catorcena, cambiar a tplanilla 4 consolidar 0
                        //select periodoconsolidado nueva tabla
                        //asignar a variables locales valores de la tabla
                        DataTable periodoConsolidado = Neg_Informes.PlnPeriodosConsolidadosEqvSel(int.Parse(TxtBuscar.Text.Trim()));
                        tplanilla = 1;
                        semana2 = 1;
                        periodo2 = Convert.ToInt32(txtPeriodo2.Text.Trim());
                        fechaini = Convert.ToDateTime(periodoConsolidado.Rows[0]["fechaini"]);
                        fechafin = Convert.ToDateTime(periodoConsolidado.Rows[0]["fechafin"]);
                    }

                }
                                               
                DataSet dsEmpleadospln = Neg_Informes.ObtenerEmpleadosPlanilla(TxtBuscar.Text.Trim(),txtPeriodo2.Text,fechaini,fechafin, ddlProceso.SelectedValue.Trim(), TxtCodigoE.Text.Trim(), ChkAll.Checked, ChkEfectivo.Checked,filtroemail,ChkConsolida.Checked);
               
                if (dsEmpleadospln.Tables.Count > 0)
                {
                    DataTable dtEmpleadospln = dsEmpleadospln.Tables[0];
                    if (dtPeriodo[0].tperiodo == 3 || dtPeriodo[0].tperiodo == 4 || dtPeriodo[0].tperiodo == 5)//planilla de aguinaldo o vacaciones
                    {
                        html = Neg_Informes.GenerarComprobantePrestacionPdf(dtEmpleadospln, TxtBuscar.Text.Trim(), periodo2,1, tplanilla, dtPeriodo[0].tperiodo,filtroemail);
                    }
                    else
                    {
                        html = Neg_Informes.GenerarComprobantePeriodoPdf(dtEmpleadospln, TxtBuscar.Text.Trim(),periodo2,semana2, tplanilla, dtPeriodo[0].tperiodo, filtroemail, encabezado, ChkConsolida.Checked);
                    }
                   
                }
                //if (ChkConsolida.Checked)//reporte consolidad dos periodos
                //{
                //    if (!string.IsNullOrEmpty(txtPeriodo2.Text.Trim()))
                //    {
                //        if (!Neg_Informes.PlnConversionPeriodoConsolidado(Convert.ToInt32(TxtBuscar.Text.Trim()), Convert.ToInt32(txtPeriodo2.Text.Trim()), 2))
                //        {
                //            throw new Exception("Error al convertir periodo");
                //        }
                //    }

                //}
                return html;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
       
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try {
                if (TxtBuscar.Text.Trim() != "")
                {
                    int filtroeamil = ChkEmail.Checked ? 2 : 1;
                    string html = Crear(filtroeamil);
                    CrearPDF(html);
                }
                else
                {
                    throw new Exception("Debe digitar un periodo");
                }
            }
            catch (Exception ex) {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
            }
        }

        protected void ddlUbicacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ChkEfectivo_CheckedChanged(object sender, EventArgs e)
        {
            //if (!ChkEmail.Checked)
            //{
            //    ChkEfectivo.Checked = false;
            //}
            if (ChkEfectivo.Checked)
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                this.ChkAll.Checked = false;
            }
            else
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                ChkAll.Checked = false;
            }
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (!ChkEmail.Checked)
            //{
            //    ChkAll.Checked = false;
            //}
            if (ChkAll.Checked)
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                ChkEfectivo.Checked = false;
            }
            else
            {
                ddlProceso.SelectedIndex = -1;
                TxtCodigoE.Text = "";
                ChkEfectivo.Checked = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (TxtBuscar.Text.Trim() != "")
                {
                    string html = Crear(3);
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Se han enviado las colillas de pago a todo el personal con correo";
                }
                else
                {
                    throw new Exception("Debe digitar un periodo");
                }
            }
            catch (Exception ex)
            {
                alertSucces.Visible = false;
                LblSuccess.Visible = false;
                alertValida.Visible = true;
                alertValida.InnerText = ex.Message;
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