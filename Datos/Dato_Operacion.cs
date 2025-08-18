using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Datos
{
    public class Dato_Operacion
    {
        ConnectionRepository ConnectionRepository = new ConnectionRepository();

        public DataTable PlnOperacionSel(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnOperacionSel";
            cmd.Connection = sqlConnection;

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
        public bool PlnOperacionIns(string codigo_operacion, bool critica, int idEmpresa, string user, string descripcion)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnOperacionIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_operacion", System.Data.SqlDbType.Char);
            p1.Value = codigo_operacion;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.Char);
            p2.Value = descripcion;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@critica", System.Data.SqlDbType.Bit);
            p3.Value = critica;
            cmd.Parameters.Add(p3);
            
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

        public bool PlnOperacionUpd(string codigo_operacion, bool critica, int idEmpresa,bool activo, string user, string descripcion)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnOperacionIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_operacion", System.Data.SqlDbType.Char);
            p1.Value = codigo_operacion;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.Char);
            p2.Value = descripcion;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@critica", System.Data.SqlDbType.Bit);
            p3.Value = critica;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            p4.Value = activo;
            cmd.Parameters.Add(p4);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

        public bool PlnCargosOperacionIns(string codigo_cargo, string codigo_operacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosOperacionIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_cargo", System.Data.SqlDbType.Int);
            p1.Value = codigo_cargo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_operacion", System.Data.SqlDbType.NChar);
            p2.Value = codigo_operacion;
            cmd.Parameters.Add(p2);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (System.Exception ex)
            {
                string x = ex.Message;
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

        public bool PlnCargosOperacionDel(string codigo_cargo, string codigo_operacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosOperacionDel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_cargo", System.Data.SqlDbType.Int);
            p1.Value = codigo_cargo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_operacion", System.Data.SqlDbType.NChar);
            p2.Value = codigo_operacion;
            cmd.Parameters.Add(p2);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (System.Exception ex)
            {
                string x = ex.Message;
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }

        public DataTable PlnCargosOperacionSel(int idEmpresa, int codigo_cargo)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_cargo", System.Data.SqlDbType.Int);
            p1.Value = codigo_cargo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosOperacionSel";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (System.Exception ex)
            {

                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            return dt;
        }

    }
}
