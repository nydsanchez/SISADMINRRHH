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
                //if (dtPeriodo[0].cerrado == 1)
                //    throw new Exception("El periodo esta cerrado");

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

            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            FileUpload FU = (FileUpload)cph.FindControl(nameFU);
            GridView gv = (GridView)cph.FindControl(nombreGV);

            if (FU.HasFile)
            {

                string connectionString = "";
                string fileName = Path.GetFileName(FU.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FU.PostedFile.FileName);
                string fileLocation = HttpContext.Current.Server.MapPath("..").ToString() + @"\Trash\" + fileName;
                FU.SaveAs(fileLocation);


                if (fileExtension == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (fileExtension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }



                //Create OleDB Connection and OleDb Command
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);

                con.Open();

                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtDD);
                con.Close();



                dtID.Columns.Add("codigo");
                dtID.Columns.Add("tipo");
                dtID.Columns.Add("detalle");
                dtID.Columns.Add("cantidad");
                dtID.Columns.Add("valor");
              
                bool Correcto = false;

                if ((dtDD.Columns[0].ToString().ToLower().Trim() == "codigo".ToLower().Trim())
                    && (dtDD.Columns[1].ToString().ToLower().Trim() == "tipo".ToLower().Trim())
                    && (dtDD.Columns[2].ToString().Trim().ToLower() == "detalle".ToLower().Trim())
                    && (dtDD.Columns[3].ToString().Trim().ToLower() == "cantidad".ToLower().Trim())
                    && (dtDD.Columns[4].ToString().Trim().ToLower() == "valor".ToLower().Trim()))
                   
                {
                    Correcto = true;
                }

                if (Correcto)
                {
                    DataView dtwExcelRecords = new DataView();
                    dtwExcelRecords = dtDD.DefaultView;  //se obtiene el total de filas
                    dtwExcelRecords.RowFilter = "[" + dtDD.Columns[0] + "] is not null"; //se obtiene las filas con datos y evitar recorrer campos nulos

                    DataTable dt2 = dtwExcelRecords.ToTable();
                    if (dt2.Rows.Count > 0)
                    {
                        panelinc.Visible = true;
                        Button1.Visible = true;
                    }
                    foreach (DataRow item in dt2.Rows)
                    {
                        dtID.Rows.Add(item["codigo"].ToString(), item["tipo"].ToString(), item["detalle"].ToString(), item["cantidad"].ToString(), item["valor"].ToString());


                    }

                    gvING.DataSource = dtID;
                    gvING.DataBind();

                    Session["dtcargado"] = dtID;
                   
                }

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
        public void cargarReporte(DataTable dt,  ReportViewer window)
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