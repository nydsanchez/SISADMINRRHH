using System;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class Dato_PlnProteccionDzxFecha
    {
        public DataTable Select(DateTime fechaini, DateTime fechafin,int idEmpresa)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            ConnectionRepository ConnectionRepository = new ConnectionRepository();
            
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnProteccionDzxFechaSel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt;
        }

        public string Insert(DateTime fechaini, DateTime fechafin,int codigo_empleado, int idEmpresa)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            ConnectionRepository ConnectionRepository = new ConnectionRepository();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnProteccionDzxFechaIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p3.Value = codigo_empleado;
            cmd.Parameters.Add(p3);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (System.Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return ex.Message;
            }

            return "OK";
        }
    }
}
