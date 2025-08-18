using System;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class Dato_Cooperativa
    {
        public string LimpiarSaldo(int codigo_empleado, int idEmpresa)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            ConnectionRepository ConnectionRepository = new ConnectionRepository();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CooLimpiarSaldo";
            cmd.Connection = sqlConnection;

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
