using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Configuration;

namespace Datos
{
    public class ConnectionRepository
    {
         
        private  IDictionary<int, SqlConnection> Connections = new Dictionary<int, SqlConnection>();
        //MODIFICADO POR GRETHEL TERCERO 24/10/2106
        public SqlConnection getConnection(int id)
        {
            try
            {
                if (!Connections.ContainsKey(id))
                {
                    SqlConnection conn = createConnection(id);
                    Connections.Add(new KeyValuePair<int, SqlConnection>(id, conn));
                }
                return Connections[id];
            }
            catch (Exception ex)
            {
                string ruta = HttpContext.Current.Server.MapPath("~/Account/Login.aspx");
                HttpContext.Current.Response.Redirect(ruta);

                throw new Exception(ex.Message);
            }
        }
        
        private SqlConnection createConnection(int id)
        {
            System.Data.DataSet ds = new DataSet();
            try
            {
                //string connectionString = "Data Source=192.168.2.8;Initial Catalog=Empresas;User id=CRP;Password=Snorlax3112; MultipleActiveResultSets=true;";
                // --- VHPO string connectionString = "Data Source=192.168.2.7;Initial Catalog=Empresas;User id=CRP;Password=Snorlax3112; MultipleActiveResultSets=true;";
                //string connectionString = "Data Source=(local);Initial Catalog=Empresas;User id=sa;Password=123; MultipleActiveResultSets=true;";

                //TODO: VHPO 15/11/2024
                //
                
                string connectionString  = ConfigurationManager.ConnectionStrings["NominaConnectionString"].ConnectionString;

                string queryString =
                "SELECT Id_Bd, DB, Servidor, Usuario, Clave, Empresa"
            + " from tbLogEmpresa"
                    + " WHERE Id_Bd = @id ";

                string a = queryString;
                SqlCommand command;
                using (SqlConnection connection =
                    new SqlConnection(connectionString))
                {
                    command = new SqlCommand(queryString, connection);
                    command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            Console.WriteLine("\t{0}\t{1}\t{2}",
                                reader[0], reader[1], reader[2]);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds, "empln");
                }

                //string ipEmpresa = ds.Tables[0].Rows[0]["Servidor"].ToString();
                //string dbEmpresa = ds.Tables[0].Rows[0]["DB"].ToString();
                //string userEmpresa = ds.Tables[0].Rows[0]["Usuario"].ToString();
                //string passwordEmpresa = ds.Tables[0].Rows[0]["Clave"].ToString();

                //string conectionEmpresa = "Data Source=" + ipEmpresa + ";Initial Catalog=" + dbEmpresa + ";User id=" + userEmpresa + ";Password=" + passwordEmpresa + ";";
                string conectionEmpresa = ConfigurationManager.ConnectionStrings["BDRRHHConnectionString"].ConnectionString;
                return new SqlConnection(conectionEmpresa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
