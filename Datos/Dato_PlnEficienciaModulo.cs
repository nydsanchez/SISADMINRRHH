using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class Dato_PlnEficienciaModulo
    {
        ConnectionRepository ConnectionRepository = new ConnectionRepository();

        public DataTable Sel(int periodo)
        {
            CnBD con = new CnBD();
            DataTable dt = new System.Data.DataTable();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnEficienciaModuloSel";

            SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.Connection = con.GetConecction();

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
