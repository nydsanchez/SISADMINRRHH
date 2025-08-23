using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using OfficeOpenXml;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Negocios;
using Datos;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace NominaRRHH
{
    public partial class IngresosyDeduccionesIncentivo : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Marca Neg_Marca = new Neg_Marca();
        Neg_Amonestaciones Neg_Amonestaciones = new Neg_Amonestaciones();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Empleados NegEmp = new Neg_Empleados();
        Neg_DevYDed NDevyDed = new Neg_DevYDed();
        #endregion

        DataTable dtDD = new DataTable();
        DataTable dtID = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["dtcargado"] = "";
                panelinc.Visible = false;
                Button1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                    throw new Exception("Periodo Invalido");

                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text));

                string user = Convert.ToString(this.Page.Session["usuario"]);

                if (gvING.Rows.Count > 0)
                {
                    IUserDetail userDetail = UserDetailResolver.getUserDetail();

                    int periodo = int.Parse(txtPeriodo.Text);
                    int semana = int.Parse(ddlTipo.SelectedValue);

                    int codigo = 0;
                    int rubroP = 4;

                    dtID = (DataTable)Session["dtcargado"];
                    foreach (DataRow dr in dtID.Rows)
                    {
                        codigo = int.Parse(dr["codigo"].ToString());
                        rubroP = Neg_Incentivos.PlnObtenerIDRubroIncentivo(codigo, 4, 1);// 4;
                        Neg_Incentivos.IncentivoIngDedccLOGInsert(codigo, periodo, semana, int.Parse(dr["tipo"].ToString()), dr["detalle"].ToString(), 1, decimal.Parse(dr["Cantidad"].ToString()), decimal.Parse(dr["Valor"].ToString()), "", rubroP, false);

                    }
                    Session["dtcargado"] = null;
                    gvING.DataSource = null;
                    gvING.DataBind();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Los registros se han insertado exitosamente.";
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        } //aqui fin     
        protected void btnCargarID_Click(object sender, EventArgs e)
        {
            if (txtPeriodo.Text != "")
            {
                if (fuIngr.HasFile)
                {
                    Button1.Visible = true;
                    CargarArchivo("fuIngr", "NG", "gvING");
                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Seleccione un archivo";
                    fuIngr.Focus();
                }
            }

            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un Periodo";
                fuIngr.Focus();
            }
        }

        #region METODOS

        public void CargarArchivo(string nameFU, string NombreVS, string nombreGV)
        {
            // Configurar licencia de EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            FileUpload FU = (FileUpload)cph.FindControl(nameFU);
            GridView gv = (GridView)cph.FindControl(nombreGV);

            if (FU.HasFile)
            {
                string fileName = Path.GetFileName(FU.PostedFile.FileName);
                string fileLocation = HttpContext.Current.Server.MapPath("..") + @"\Trash\" + fileName;
                FU.SaveAs(fileLocation);

                // Creamos DataTable
                dtID.Columns.Clear();
                dtID.Columns.Add("codigo");
                dtID.Columns.Add("tipo");
                dtID.Columns.Add("detalle");
                dtID.Columns.Add("cantidad");
                dtID.Columns.Add("valor");

                using (var package = new ExcelPackage(new FileInfo(fileLocation)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Cast<ExcelWorksheet>().FirstOrDefault();

                    if (worksheet == null)
                    {
                        lblAlert.Text = "El archivo no tiene hojas válidas.";
                        alertValida.Visible = true;
                        return;
                    }

                    // Validamos columnas
                    bool Correcto = worksheet.Cells[1, 1].Text.Trim().ToLower() == "codigo" &&
                                     worksheet.Cells[1, 2].Text.Trim().ToLower() == "tipo" &&
                                     worksheet.Cells[1, 3].Text.Trim().ToLower() == "detalle" &&
                                     worksheet.Cells[1, 4].Text.Trim().ToLower() == "cantidad" &&
                                     worksheet.Cells[1, 5].Text.Trim().ToLower() == "valor";

                    if (!Correcto)
                    {
                        lblAlert.Text = "El formato del archivo no es válido.";
                        alertValida.Visible = true;
                        return;
                    }

                    int rowCount = worksheet.Dimension.End.Row;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        if (!string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                        {
                            dtID.Rows.Add(
                                worksheet.Cells[row, 1].Text,
                                worksheet.Cells[row, 2].Text,
                                worksheet.Cells[row, 3].Text,
                                worksheet.Cells[row, 4].Text,
                                worksheet.Cells[row, 5].Text
                            );
                        }
                    }
                }

                if (dtID.Rows.Count > 0)
                {
                    panelinc.Visible = true;
                    Button1.Visible = true;
                }

                gv.DataSource = dtID;
                gv.DataBind();

                Session["dtcargado"] = dtID;
            }
        }

        #endregion

        protected void gvING_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvING.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)Session["dtcargado"];

            gvING.DataSource = dt;
            gvING.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPeriodo.Text != "")
                {
                    Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
                    int periodo = int.Parse(txtPeriodo.Text);
                    int semana = int.Parse(ddlTipo.SelectedValue);
                    DataTable dt = Neg_Incentivos.IncentivoIngDedccLOGxEmpleado(periodo, semana);
                    panelinc.Visible = true;
                    cargarReporte(dt, ReportViewer1);
                }

            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }
        }
        public void cargarReporte(DataTable dt, ReportViewer window)
        {

            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            window.ProcessingMode = ProcessingMode.Local;
            window.LocalReport.DataSources.Clear();
            window.LocalReport.ReportPath = Server.MapPath("../Reportes/IngresoDeduccionIncDet.rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", dt);
            window.LocalReport.DataSources.Add(source);
            window.LocalReport.Refresh();

        }
    }
}