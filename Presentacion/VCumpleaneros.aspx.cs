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
    public partial class VCumpleaneros : System.Web.UI.Page
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtenerDepartamentos();
            }
        }
        private void ObtenerDepartamentos()
        {
            this.ddlDepto1.DataSource = Neg_Informes.CargarDeptos();
            this.ddlDepto1.DataValueField = "codigo_depto";
            this.ddlDepto1.DataTextField = "nombre_depto";
            this.ddlDepto1.DataBind();
            this.ddlDepto2.DataSource = Neg_Informes.CargarDeptos();
            this.ddlDepto2.DataValueField = "codigo_depto";
            this.ddlDepto2.DataTextField = "nombre_depto";
            this.ddlDepto2.DataBind();
        }
        void SubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
        {

        }
        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            DataSet ds = null;
            int depto1=0, depto2=0, mes=0;
            string sexo = "";
            depto1 = Convert.ToInt32(ddlDepto1.SelectedValue.Trim());
            depto2 = Convert.ToInt32(ddlDepto2.SelectedValue.Trim());
            sexo = ddlSexo.SelectedValue.ToString().Trim();
            mes = Convert.ToInt32(ddlMes.SelectedValue.Trim());
            ds = Neg_Informes.CargarCumpleañeros(depto1,depto2,sexo,mes);

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/Cumple.rdlc"); 

            DataTable dtPrincipal = new DataTable();
            dtPrincipal.Columns.Add("codigo1");
            dtPrincipal.Columns.Add("foto1", typeof(Byte[]));
            dtPrincipal.Columns.Add("nombre1");
            dtPrincipal.Columns.Add("codigo2");
            dtPrincipal.Columns.Add("foto2", typeof(Byte[]));
            dtPrincipal.Columns.Add("nombre2");
            dtPrincipal.Columns.Add("codigo3");
            dtPrincipal.Columns.Add("foto3", typeof(Byte[]));
            dtPrincipal.Columns.Add("nombre3");

            int indice = 0;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                #region Menos de tres carnet
                if (ds.Tables[0].Rows.Count < 3)
                {
                    

                }
                #endregion
                string codigo1 = "", codigo2 = "", codigo3 = "";
                string nombre1 = "", nombre2 = "", nombre3 = "";

                indice += 1;

                if (indice == 3)//Creo la primera fila.
                {
                    byte[] byteArray1 = new byte[0];
                    byte[] byteArray2 = new byte[0];
                    byte[] byteArray3 = new byte[0];

                    if (ds.Tables[0].Rows[i - 2]["codigo1"].ToString() != "")
                    {
                        codigo1 = ds.Tables[0].Rows[i - 2]["codigo1"].ToString();
                        if (codigo1 != "")
                            byteArray1 = Neg_Catalogos.cargarFoto(Convert.ToInt32(codigo1));
                        nombre1 = ds.Tables[0].Rows[i - 2]["nombre1"].ToString();
                    }
                    if (ds.Tables[0].Rows[i - 1]["codigo1"].ToString() != "")
                    {
                        codigo2 = ds.Tables[0].Rows[i - 1]["codigo1"].ToString();
                        if (codigo2 != "")
                            byteArray2 = Neg_Catalogos.cargarFoto(Convert.ToInt32(codigo2));
                        nombre2 = ds.Tables[0].Rows[i - 1]["nombre1"].ToString();
                    }
                    if (ds.Tables[0].Rows[i]["codigo1"].ToString() != "")
                    {
                        codigo3 = ds.Tables[0].Rows[i]["codigo1"].ToString();
                        if (codigo3 != "")
                            byteArray3 = Neg_Catalogos.cargarFoto(Convert.ToInt32(codigo3));
                        nombre3 = ds.Tables[0].Rows[i]["nombre1"].ToString();
                        dtPrincipal.Rows.Add(codigo1, byteArray1, nombre1, codigo2, byteArray2, nombre2, codigo3, byteArray3, nombre3);
                        indice = 0;//Lo regreso al indice 0,
                    }
                }
                else
                {
                    if (i == ds.Tables[0].Rows.Count - 1)//Si es la ultima fila.
                    {
                        byte[] ArregloVacio = new byte[0];
                        byte[] byteArray1I = new byte[0];
                        byte[] byteArray2I = new byte[0];
                        string UltimoCodigo="", PenultimoCodigo="";
                        string UltimoNombre="", PenultimoNombre="";

                        UltimoCodigo = ds.Tables[0].Rows[i]["codigo1"].ToString();
                        UltimoNombre = ds.Tables[0].Rows[i]["nombre1"].ToString();
                        
                        if (ds.Tables[0].Rows.Count>1)
                        {
                            PenultimoCodigo = ds.Tables[0].Rows[i - 1]["codigo1"].ToString();
                            PenultimoNombre = ds.Tables[0].Rows[i - 1]["nombre1"].ToString();
                        }

                        if (UltimoCodigo != "")
                            byteArray1I = Neg_Catalogos.cargarFoto(Convert.ToInt32(UltimoCodigo));

                        var resultado = from r in dtPrincipal.AsEnumerable()
                                        where r.Field<string>("codigo1") == PenultimoCodigo ||
                                              r.Field<string>("codigo2") == PenultimoCodigo ||
                                              r.Field<string>("codigo3") == PenultimoCodigo
                                        select r;
                        int Nfila = resultado.Count();
                        if (Nfila != 0)//Si no esta 
                        {
                            PenultimoCodigo = "";
                            byteArray2I = ArregloVacio;
                        }
                        else
                        {
                            if (ds.Tables[0].Rows.Count>1)
                            {
                            PenultimoCodigo = ds.Tables[0].Rows[i - 1]["codigo1"].ToString();
                            byteArray2I = Neg_Catalogos.cargarFoto(Convert.ToInt32(PenultimoCodigo));
                            PenultimoNombre = ds.Tables[0].Rows[i - 1]["nombre1"].ToString();
                            }    
                            //dtPrincipal.Rows.Add(UltimoCodigo, byteArray1I, UltimoNombre, UltimoDepto, PenultimoCodigo, byteArray2I, PenultimoNombre, PenultimoDepto, "", ArregloVacio, "", "");
                        }                       
                    }  
                }
            }

            if (ds.Tables[0].Rows.Count % 3 != 0)//Si es multiplo de 3
            {
               byte[] ArregloVacio = new byte[0];//El tercero de cada fila,en este caso no existe.
               int i1 = ds.Tables[0].Rows.Count;
               string UltimoCodigo = ds.Tables[0].Rows[i1-1]["codigo1"].ToString();
               string UltimoNombre = ds.Tables[0].Rows[i1-1]["nombre1"].ToString();
               
               byte[] byteArray1I = new byte[0];
               if (UltimoCodigo != "")
                   byteArray1I = Neg_Catalogos.cargarFoto(Convert.ToInt32(UltimoCodigo));

               string PenultimoCodigo = "";
               string PenultimoNombre = "";
               if (ds.Tables[0].Rows.Count > 1)
               {
                    PenultimoCodigo = ds.Tables[0].Rows[i1 - 2]["codigo1"].ToString();
                    PenultimoNombre = ds.Tables[0].Rows[i1 - 2]["nombre1"].ToString();
               }
               
                byte[] byteArray2I = new byte[0];             
                if (PenultimoCodigo != "")
                   byteArray2I = Neg_Catalogos.cargarFoto(Convert.ToInt32(PenultimoCodigo));

                if ((ds.Tables[0].Rows.Count - 1) % 3 == 0)//Sobra 1
                {
                    //Response.Write("Sobra 1");
                    dtPrincipal.Rows.Add(UltimoCodigo, byteArray1I, UltimoNombre, "", ArregloVacio, "", "", ArregloVacio, "");
                }
                if ((ds.Tables[0].Rows.Count - 2) % 3 == 0)//Sobran 2
                {
                    dtPrincipal.Rows.Add(UltimoCodigo, byteArray1I, UltimoNombre, PenultimoCodigo, byteArray2I, PenultimoNombre, "", ArregloVacio, "");
                   // Response.Write("Sobran 2");
                }
            }
            dtPrincipal.DefaultView.Sort = "[codigo1] ASC";
            dtPrincipal = dtPrincipal.DefaultView.ToTable();
            ReportDataSource datasource = new ReportDataSource("DataSet1", dtPrincipal);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            this.ReportViewer1.LocalReport.SubreportProcessing +=
            new SubreportProcessingEventHandler(SubreportProcessingEventHandler);
            ReportDataSource datasource2 = new ReportDataSource("DataSet1", ds.Tables[0]);
        }
        }
    }
