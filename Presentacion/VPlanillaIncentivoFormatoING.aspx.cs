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
using System.Data.OleDb;
using Negocios;


namespace NominaRRHH.Presentacion
{
    public partial class VPlanillaIncentivoFormatoING : System.Web.UI.Page
    {

        #region REFERENCIAS
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_HojasPDF Neg_HojasPDF = new Neg_HojasPDF();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlModulo.Visible = false;
                pnlCodigo.Visible = false;
                Session["Filtro"] = "TODOS";

            }

        }

        protected void rbllistImpresion_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rbllistImpresion.SelectedValue == "1")
            {
                Session["Filtro"] = "MODULO";
                ddlProceso.DataSource = CargarDll();
                ddlProceso.DataBind();
                pnlModulo.Visible = true;
                pnlCodigo.Visible = false;

            }
            else if (rbllistImpresion.SelectedValue == "2")
            {
                Session["Filtro"] = "RANGO";
                pnlCodigo.Visible = true;
                pnlModulo.Visible = false;
            }
            else
            {
                Session["Filtro"] = "TODOS";
                pnlCodigo.Visible = false;
                pnlModulo.Visible = false;
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtperiodo.Text != "")
            {

                int periodo = int.Parse(txtperiodo.Text);
                int semana = int.Parse(ddlTipo.SelectedValue.ToString());

                DataTable dtInfon = Neg_Incentivos.IncentivoHistoricoSelect(periodo, semana);
                DataTable dtIncDeducEmpleados = Neg_Incentivos.IncentivoIngDedccLOGxEmpleado(periodo, semana);
                DataTable dtInfon2 = new DataTable();
                int filtro1 = 0, filtro2 = 0;
                if (dtInfon != null)
                {
                    if (dtInfon.Rows.Count > 0)
                    {
                        DataView dtv = dtInfon.DefaultView;

                        if (Session["Filtro"].ToString() == "MODULO")
                        {
                            filtro1 = int.Parse(ddlProceso.SelectedValue);
                            dtv.RowFilter = "Modulo=" + filtro1;
                            dtInfon2 = dtv.ToTable();

                        }
                        else if (Session["Filtro"].ToString() == "RANGO")
                        {
                            if (txtmoduloini.Text != "" && txtmodulohasta.Text != "")
                            {
                                filtro1 = int.Parse(txtmoduloini.Text);
                                filtro2 = int.Parse(txtmodulohasta.Text);
                                dtv.RowFilter = "Modulo >= " + filtro1 + " and Modulo <= " + filtro2;
                                dtInfon2 = dtv.ToTable();
                            }


                        }
                        else
                        {
                            dtInfon2 = dtInfon;

                        }

                        if (dtInfon2.Rows.Count > 0)
                        {
                            string path = HttpContext.Current.Server.MapPath("../Trash");          //se obtiene la ruta de la carpeta donde se almacenara el documento
                            string[] ficheros = Directory.GetFiles(path);

                            foreach (var item in ficheros)
                            {

                                if (File.Exists(item))
                                {
                                    try
                                    {
                                        File.Delete(item);
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }

                            }
                            Neg_HojasPDF.armarEstructuraHojaInventivosPDF(path + "/Incentivos_periodo" + periodo + "semana" + semana + ".pdf", dtInfon2, dtIncDeducEmpleados);
                            Neg_HojasPDF.ShowPdf(path + "/Incentivos_periodo" + periodo + "semana" + semana + ".pdf");
                        }
                    }



                }

            }
        }
        public DataTable CargarDll()
        {
            DataTable Modulos = new DataTable();
            if (txtperiodo.Text != "")
            {
                int periodo = int.Parse(txtperiodo.Text);
                Modulos = Neg_Incentivos.IncentivoModulosConMeta(periodo);
            }
            return Modulos;
        }
    }
}