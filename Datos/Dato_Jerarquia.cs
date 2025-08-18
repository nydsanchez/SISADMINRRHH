using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Jerarquia
    {
        ConnectionRepository ConnectionRepository = new ConnectionRepository();

        public System.Data.DataTable ObtenerDeptoSubordinados(string codigo_empleado, int idempresa)
        {
            System.Data.DataTable dt = new DataTable();

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idempresa);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "JerarquiaObtenerDeptoSubordinados";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p = new SqlParameter("@idempleado", System.Data.SqlDbType.VarChar);
            p.Value = codigo_empleado;
            cmd.Parameters.Add(p);

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (System.Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                string msg = ex.Message;
            }

            return dt;
        }

        public System.Data.DataTable ObtenerSubordinados(string codigo_empleado, int idempresa)
        {
            System.Data.DataTable dt = new DataTable();

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idempresa);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "JerarquiaObtenerSubordinados";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p = new SqlParameter("@idempleado", System.Data.SqlDbType.VarChar);
            p.Value = codigo_empleado;
            cmd.Parameters.Add(p);

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (System.Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

                string x = ex.Message;
            }

            return dt;
        }


        public System.Data.DataTable ObtenerSubordinadosPorDepto(int iddepartamento, int idempresa)
        {
            System.Data.DataTable dt = new DataTable();

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idempresa);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "JerarquiaObtenerSubordinadosPorDepto";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p.Value = iddepartamento;
            cmd.Parameters.Add(p);

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
    }
}
