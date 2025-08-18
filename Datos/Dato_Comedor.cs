using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Datos
{
    public class Dato_Comedor
    {
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        public decimal CreditoPeriodoSel(string codigo, string fecini, string fecfin)
        {
            CnBD con = new CnBD();
            SqlConnection sqlConnection = con.GetConecctionComedor();
         
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spCreditoPeriodoSel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.NVarChar);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fecini", System.Data.SqlDbType.Date);
            p2.Value = fecini;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fecfin", System.Data.SqlDbType.Date);
            p3.Value = fecfin;
            cmd.Parameters.Add(p3);

            decimal rsp = 0;
            object obj;
            try
            {
                cmd.Connection.Open();
                obj = cmd.ExecuteScalar();
                cmd.Connection.Close();

                rsp= string.IsNullOrEmpty(obj.ToString()) ? 0 : Convert.ToDecimal(obj);
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return 0;
            }

            return rsp;
        }
        public DataTable CreditoEmpresaSel(string fecini, string fecfin)
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spCreditoConsolidadoKaizen";
            cmd.Connection = con.GetConecctionComedor();


            SqlParameter p1 = new SqlParameter("@inicio", System.Data.SqlDbType.Date);
            p1.Value = fecini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fin", System.Data.SqlDbType.Date);
            p2.Value = fecfin;
            cmd.Parameters.Add(p2);
           

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataTable spCreditoSemanaSel(string codigo)
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spCreditoSemanaSel";
            cmd.Connection = con.GetConecctionComedor();
            SqlParameter p1 = new SqlParameter("@codigo", SqlDbType.NVarChar);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }

    }
}
