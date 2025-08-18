using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Datos;

namespace NominaRRHH
{
    
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class JefeInmediato : System.Web.Services.WebService
    {
        Datos.CnBD conect = new Datos.CnBD();
        

        [WebMethod]
        public List<string> getNombreJefe(string nombreJefe)
        {
            
            //string CS = ConfigurationManager.ConnectionStrings["NominaConnectionString"].ConnectionString;
            
            List<string> nombresJefe = new List<string>();
            //using (conect.GetConecction())
            //{
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "obtenerNombreJefeInmediato";
                cmd.Connection = conect.GetConecction();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter parameter = new SqlParameter("@nombreJefe", nombreJefe);
                cmd.Parameters.Add(parameter);
                cmd.Connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    nombresJefe.Add(rdr["nombrecompleto"].ToString());
                }
                cmd.Connection.Close();
            //}
            return nombresJefe;
        }
    }
}
