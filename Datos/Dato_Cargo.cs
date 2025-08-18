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
    public class Dato_Cargo
    {
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
                
        public DataTable PlnCargosSel(int idEmpresa, int codigo_departamento)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p1.Value = codigo_departamento;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosSelDepto";
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

        public bool PlnCargoIns(string nombre_cargo, bool indirecto, int idEmpresa, string user)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombre_cargo", System.Data.SqlDbType.VarChar);
            p1.Value = nombre_cargo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@indirecto", System.Data.SqlDbType.Bit);
            p2.Value = indirecto;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p3.Value = user;
            cmd.Parameters.Add(p3);
            
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

        public bool PlnDepartamentoCargoIns(string codigo_cargo, int codigo_depto, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosDepartamentoIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_cargo", System.Data.SqlDbType.Int);
            p1.Value = codigo_cargo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p2.Value = codigo_depto;
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

        public bool PlnDepartamentoCargoDel(string codigo_cargo, int codigo_depto, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosDepartamentoDel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_cargo", System.Data.SqlDbType.Int);
            p1.Value = codigo_cargo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p2.Value = codigo_depto;
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

        public DataTable PlnCargosSel(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosSel";
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

        public bool PlnCargoUpd(int codigo_cargo,string nombre_cargo, bool indirecto,bool activo, int idEmpresa, string user)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnCargosUpd";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombre_cargo", System.Data.SqlDbType.VarChar);
            p1.Value = nombre_cargo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@indirecto", System.Data.SqlDbType.Bit);
            p2.Value = indirecto;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            p3.Value = activo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@codigo_cargo", System.Data.SqlDbType.Int);
            p4.Value = codigo_cargo;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar);
            p5.Value = user;
            cmd.Parameters.Add(p5);

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
                return false;
            }
            return true;
        }

    }
}
