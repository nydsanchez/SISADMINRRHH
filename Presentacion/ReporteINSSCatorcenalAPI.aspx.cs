using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Negocios;
using System.Text;
using System.IO;

namespace NominaRRHH
{
   
    public partial class ReporteINSSCatorcenalAPI : System.Web.UI.Page
    {

        #region REFERENCIAS      
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Informes Neg_Informes = new Neg_Informes();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            ObtenerInns();
         
            
        }

        private void ObtenerInns()
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

                string url = "api/inss?year1=" + fechaini.Year.ToString() + "&month1=" + fechaini.Month.ToString() + "&days1=" + fechaini.Day.ToString() + "&year2=" + fechafin.Year.ToString() + "&month2=" + fechafin.Month.ToString() + "&days2=" + fechafin.Day.ToString();
                HttpResponseMessage rsp2 = client.GetAsync(url).Result;

                if (rsp2.IsSuccessStatusCode)
                {
                    string eco2 = rsp2.Content.ReadAsStringAsync().Result.ToString();
                    eco2 = eco2.Remove(0, 1);
                    eco2 = eco2.Remove(eco2.Length - 1);

                    string[] rsp = eco2.Split('*');

                    System.IO.File.Delete(Server.MapPath("~/Trash/Inss.txt"));
                    System.IO.File.Delete(Server.MapPath("~/Trash/Inss.xml"));

                    for (int i = 0; i < 3; i++)
                    {
                        if (rsp[i].Substring(0,4) == "html")
                        {
                            ltDatosHtml.InnerHtml = rsp[i].Substring(4, rsp[i].Length - 4);
                        }
                        else
                        {
                            if (rsp[i].Substring(0, 4) == "text")
                            {
                                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/Trash/Inss.txt"), true))
                                {
                                    string temp = rsp[i].Substring(4, rsp[i].Length - 4);                                   
                                    string[] archtxt = temp.Split('#');

                                    for (int m = 0; m < archtxt.Length; m++)
                                    {
                                        _testData.WriteLine(archtxt[m].ToString());                                        
                                    }
                                }
                            }
                            else
                            if (rsp[i].Substring(0, 3) == "xml")
                            {
                                using (StreamWriter _testData = new StreamWriter(Server.MapPath("~/Trash/Inss.xml"), true))
                                {
                                    string temp = rsp[i].Substring(3, rsp[i].Length - 3);
                                    string[] archtxt = temp.Split('#');

                                    for (int m = 0; m < archtxt.Length; m++)
                                    {
                                        _testData.WriteLine(archtxt[m].ToString());
                                    }
                                   
                                }
                            }
                        }
                    }

                    lblExportar.Visible = true;
                    btnExpTexto.Visible = true;
                    btnExpXml.Visible = true;                   

                }
            }
        }

        protected void btnExpTexto_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath("~/Trash/Inss.txt"));
            Response.AddHeader("Content-Disposition", "attachment;filename=Inss.txt");
            Response.AddHeader("Content-Length", file.Length.ToString());            
            Response.ContentType = "text/plain";
            Response.WriteFile(file.FullName);
            Response.End();          
        }

        protected void btnExpXml_Click(object sender, EventArgs e)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath("~/Trash/Inss.xml"));            
            Response.AddHeader("Content-Disposition", "attachment;filename=Inss.xml");
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/xml";
            Response.WriteFile(file.FullName);
            Response.End();
        }
    }
}