using System.Data.SqlClient;
using System.Configuration;


namespace Datos
{
    //probando
    public class CnBD
    {
        public static System.Data.SqlClient.SqlConnection sqlcon;

        void cnBD()
        {

        }

        //private string _nombre;
        public static string cadena
        {
            get;
            set;
        }

       
         public CnBD (string cad)
        {
            cadena = cad;
           
        }
        public CnBD ()
        {

        }


        void cnBD(string cadena)
        {
            sqlcon = new System.Data.SqlClient.SqlConnection(cadena);
        }
      

        public System.Data.SqlClient.SqlConnection GetConecction()
        {
            sqlcon = new SqlConnection();
            sqlcon.ConnectionString = cadena;
            return sqlcon;

        }

        public System.Data.SqlClient.SqlConnection Init()
        {
            sqlcon = new SqlConnection();
            sqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["NominaConnectionString"].ConnectionString;
            return sqlcon;

        }
        public System.Data.SqlClient.SqlConnection GetConecctionC()
        {
            sqlcon = new SqlConnection();
            sqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["CoutureConnectionString"].ConnectionString;
            return sqlcon;

        }
        public System.Data.SqlClient.SqlConnection GetConecctionComedor()
        {
            sqlcon = new SqlConnection();
            sqlcon.ConnectionString = ConfigurationManager.ConnectionStrings["ComedorConnectionString"].ConnectionString;
            return sqlcon;

        }

    }
}