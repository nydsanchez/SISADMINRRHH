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





namespace NominaRRHH.Presentacion
{
    public partial class ReporteAusentismo : System.Web.UI.Page
    {
        Negocios.Notificacion noti = new Negocios.Notificacion();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text != "" && TxtFecha2.Text != "")
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                int id = Convert.ToInt32(userDetail.getIDEmpresa());

                System.Data.DataTable x = new System.Data.DataTable();

                DateTime fechaini = Convert.ToDateTime(txtFecha.Text);
                DateTime fechafin = Convert.ToDateTime(TxtFecha2.Text);

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://192.168.2.8:54656/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
                
               // HttpResponseMessage rsp = client.GetAsync("api/EmpleadosHorasLaborales?year1=" + fechaini.Year.ToString() + "&month1=" + fechaini.Month.ToString() + "&days1=" + fechaini.Day.ToString() + "&year2=" + fechafin.Year.ToString() + "&month2=" + fechafin.Month.ToString() + "&days2=" + fechafin.Day.ToString()).Result;
                HttpResponseMessage rsp2 =client.GetAsync("api/EmpleadosHorasLaborales?year1=" + fechaini.Year.ToString() + "&month1=" + fechaini.Month.ToString() + "&days1=" + fechaini.Day.ToString() + "&year2=" + fechafin.Year.ToString() + "&month2=" + fechafin.Month.ToString() + "&days2=" + fechafin.Day.ToString() + "&tipo="+id).Result;


                if (rsp2.IsSuccessStatusCode)
                {
                    //string eco = rsp.Content.ReadAsStringAsync().Result.ToString();
                    string eco2 = rsp2.Content.ReadAsStringAsync().Result.ToString();
                    x = JsonStringToDataTable(eco2);

                    ReportViewer1.ProcessingMode = ProcessingMode.Local;
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ReporteAusentismos.rdlc");
                    ReportDataSource datasource = new ReportDataSource("DataSet1", x);
                    ReportViewer1.LocalReport.DataSources.Add(datasource);
                    ReportViewer1.LocalReport.Refresh();
                    // x.ReadXml(new XmlTextReader(new StringReader(eco2)));
                    // html2 = HttpContext.Current.Server.HtmlDecode(eco);

                    //List<string> CORREOS = new List<string>();
                    //CORREOS.Add("wmembreno");
                    // noti.EnviarNotificacionVariosDestinatarios(eco, "prueba", CORREOS, "");
                }
            }
            else
            {
                //MENSAJE ERROR
            }

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

        protected void cbcodigo_CheckedChanged(object sender, EventArgs e)
        {
            //if (cbcodigo.Checked)
            //{

            //}
            //else
            //{

            //}
            dvparamcodigo.Visible = true;
        }

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
    }
}