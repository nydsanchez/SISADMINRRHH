using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class Dato_IR
    {
        ConnectionRepository ConnectionRepository = new ConnectionRepository();

        public System.Data.DataTable TablaIR(int idempresa)
        {
            System.Data.DataTable dt = new DataTable();

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idempresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnIRSel";
            cmd.Connection = sqlConnection;
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

        public dsPlanilla.dtIRHistoricoDataTable ObtenerHistoricoIR(int idempresa, DateTime fecha)
        {
            dsPlanilla.dtIRHistoricoDataTable dt = new dsPlanilla.dtIRHistoricoDataTable();

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idempresa);

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            System.Data.SqlClient.SqlParameter p = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p.Value = fecha;
            cmd.Parameters.Add(p);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnplanillasIRhist";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                string x = ex.Message;

                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
    }
}
