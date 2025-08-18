using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using System.Data;
using Negocios;

using Microsoft.Reporting.WebForms;
using NominaRRHH.Presentacion;

namespace NominaRRHH
{
   
    public partial class ReporteMarcasAPI : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Informes Neg_Informes = new Neg_Informes();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                //this.Page
                CargarDptos();
                
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text != "" && TxtFecha2.Text != "")
            {
                //IUserDetail userDetail = UserDetailResolver.getUserDetail();
                //int id = Convert.ToInt32(userDetail.getIDEmpresa());

                //System.Data.DataTable x = new System.Data.DataTable();

                DateTime fechaini = Convert.ToDateTime(txtFecha.Text);
                DateTime fechafin = Convert.ToDateTime(TxtFecha2.Text);
                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("http://192.168.2.8:54656/");

                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));

                //string url = "api/Marcas?ano1=" + fechaini.Year.ToString() + "&mes1=" + fechaini.Month.ToString() + "&dia1=" + fechaini.Day.ToString() + "&ano2=" + fechafin.Year.ToString() + "&mes2=" + fechafin.Month.ToString() + "&dia2=" + fechafin.Day.ToString() + "&tipo=5&ubicacion=3&iddepartamentoini=" + int.Parse(ddldepto1.SelectedValue.ToString()) + "&iddepartamentofin=" + int.Parse(ddldepto2.SelectedValue.ToString());
                //HttpResponseMessage rsp2 = client.GetAsync(url).Result;

                //if (rsp2.IsSuccessStatusCode)
                //{

                //    string eco2 = rsp2.Content.ReadAsStringAsync().Result.ToString();
                //    x = JsonStringToDataTable(eco2);

                //    eco2 = eco2.Remove(0, 1);
                //    eco2 = eco2.Remove(eco2.Length - 1);

                //    ltDatosHtml.InnerHtml = eco2;                    
                //}

                //DateTime fechainicio = new DateTime(ano1, mes1, dia1);
                //DateTime fechafin = new DateTime(ano2, mes2, dia2);

                int iddepartamentoini = int.Parse(ddldepto1.SelectedValue.ToString());
                int iddepartamentofin = int.Parse(ddldepto2.SelectedValue.ToString());
                ltDatosHtml.InnerHtml = new Neg_Marca().ObtenerReportedeMarcas(fechaini, fechafin, 5, 3, iddepartamentoini, iddepartamentofin);

            }

        }


        #region metodos

        public DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");

                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");

                        if (RowColumns == "fecha")
                        {
                            nr[RowColumns] = Convert.ToDateTime(RowDataString).Date;
                        }
                        else
                        {
                            nr[RowColumns] = RowDataString;
                        }

                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }
        public static Object ObjectToXML(string xml, Type objectType)
        {
            StringReader strReader = null;
            XmlSerializer serializer = null;
            XmlTextReader xmlReader = null;
            Object obj = null;
            try
            {
                strReader = new StringReader(xml);
                serializer = new XmlSerializer(objectType);
                xmlReader = new XmlTextReader(strReader);
                obj = serializer.Deserialize(xmlReader);
            }
            catch (Exception exp)
            {
                //Handle Exception Code
            }
            finally
            {
                if (xmlReader != null)
                {
                    xmlReader.Close();
                }
                if (strReader != null)
                {
                    strReader.Close();
                }
            }
            return obj;
        }
        private void CargarDptos()
        {
            DataTable ft = Neg_Catalogos.CargarProcesos().Tables[0];
            DataView dv = ft.DefaultView;
            dv.Sort = "codigo_depto asc";
            DataTable sortedDT = dv.ToTable();

            ddldepto1.DataSource = sortedDT;
            this.ddldepto1.DataMember = "procesos";
            this.ddldepto1.DataValueField = "codigo_depto";
            ddldepto1.DataTextField = "nombre_depto";
            this.ddldepto1.DataBind();

            ddldepto2.DataSource = sortedDT;
            this.ddldepto2.DataMember = "procesos";
            this.ddldepto2.DataValueField = "codigo_depto";
            ddldepto2.DataTextField = "nombre_depto";
            this.ddldepto2.DataBind();
        }
        #endregion
    }
}