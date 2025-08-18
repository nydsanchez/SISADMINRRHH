using System.Web;
using System.Data;
using Microsoft.Reporting.WebForms;
using Negocios;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace NominaRRHH.Presentacion
{
    public partial class VLiquidacion : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // int codigo = Convert.ToInt32(((DataTable)Session["dtParametrosLiq"]).Rows[0]["codigo"]);
                int codigo = Convert.ToInt32(Session["dtParametrosLiq"]);
                DateTime fechaliquidacion = Convert.ToDateTime(Session["dtfechaliq"]);
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Liquidacion2.rdlc");
                //DataSet ds1 = Neg_Informes.CargarLiquidacion(Convert.ToInt32(txtCodigo.Text.Trim()));
                DataSet ds1 = Neg_Informes.spMesesLiquidacion(codigo, fechaliquidacion);
                DataSet ds2 = Neg_Informes.spLiquidacionDetallado(codigo, fechaliquidacion);
                DataSet ds3 = Neg_Informes.spLiquidacionPendiente(codigo, fechaliquidacion);
                DataSet ds4 = Neg_Informes.spLiquidacionPendientesDeduc(codigo, fechaliquidacion);
                DataSet ds5 = Neg_Informes.plnDeduccionesPendPagoLiqSel(codigo, fechaliquidacion);
                //si no se guardaron deducciones manuales como pendientes en la liquidacion, se consulta directamente de deducespciales
                if (ds5.Tables.Count==0 && ds5.Tables[0].Rows.Count==0)
                {
                    ds5 = Neg_DevYDed.DeduccionesOrdinariasObtener(codigo, 0, 1);
                }
                // DataSet ds3 = Neg_Informes.CargarLiquidacionEncabezado(codigo);               
                // ReportDataSource datasource = new ReportDataSource("DataSet1", (DataTable)Session["Meses"]);
                // ReportDataSource datasource2 = new ReportDataSource("DataSet2", (DataTable)Session["dtParametrosLiq"]);
                ReportDataSource datasource = new ReportDataSource("DataSet1", ds1.Tables[0]);
                ReportDataSource datasource2 = new ReportDataSource("DataSet2",ds2.Tables[0]);
                ReportDataSource datasource3 = new ReportDataSource("DataSet3",ds4.Tables[0]);
                ReportDataSource datasource4 = new ReportDataSource("DataSet4", ds3.Tables[0]);
                ReportDataSource datasource5 = new ReportDataSource("DataSet5", ds5.Tables[0]);

                ReportViewer1.LocalReport.DataSources.Clear();
                //Recibe dos Multiple origenes de Datos.
                ReportViewer1.LocalReport.DataSources.Add(datasource);
                ReportViewer1.LocalReport.DataSources.Add(datasource2);
                ReportViewer1.LocalReport.DataSources.Add(datasource3);
                ReportViewer1.LocalReport.DataSources.Add(datasource4);
                ReportViewer1.LocalReport.DataSources.Add(datasource5);

                this.ReportViewer1.LocalReport.SubreportProcessing +=
                new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            }
        }
        private void LimpiarSession()
        {
            //Limpiar variables de Session.
            Session["INSS_Vacaciones"] = null;
            Session["PendienteInss"] = null;
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
        
        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            try
            {
                byte[] bytes = ReportViewer1.LocalReport.Render("PDF", null, out mimeType,
                               out encoding, out extension, out streamids, out warnings);

                //string Codigo = Convert.ToString(((DataTable)Session["dtParametrosLiq"]).Rows[0]["codigo"]);
                string Codigo = Session["dtParametrosLiq"].ToString();

                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("" + Codigo.Trim() + ".pdf"), FileMode.Create);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                //Open exsisting pdf
                Document document = new Document(PageSize.LETTER);
                PdfReader reader = new PdfReader(HttpContext.Current.Server.MapPath("" + Codigo.Trim() + ".pdf"));
                //Getting a instance of new pdf wrtiter
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(
                HttpContext.Current.Server.MapPath("Print.pdf"), FileMode.Create));
                document.Open();
                PdfContentByte cb = writer.DirectContent;

                int i = 0;
                int p = 0;
                int n = reader.NumberOfPages;
                Rectangle psize = reader.GetPageSize(1);

                float width = psize.Width;
                float height = psize.Height;

                //Add Page to new document
                while (i < n)
                {
                    document.NewPage();
                    p++;
                    i++;

                    PdfImportedPage page1 = writer.GetImportedPage(reader, i);
                    cb.AddTemplate(page1, 0, 0);
                }
                //Attach javascript to the document
                PdfAction jAction = PdfAction.JavaScript("this.print(true);\r", writer);
                writer.AddJavaScript(jAction);
                document.Close();
                //Attach pdf to the iframe
                frmPrint.Attributes["src"] = "Print.pdf";
                frmPrint.Attributes["onLoad"] = "PrintReport()";
                LimpiarSession();
            }
            catch (Exception ex)
            {
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        
    }
}