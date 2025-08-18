using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace Datos
{
    public class Dato_Perfil
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public DataTable Perfil_Select(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SEG].[Perfil_Select]";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                da.Fill(ds);

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public bool PerfilInsert(bool activo, string nombreRol, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PerfilInsert";
            cmd.Connection = sqlConnection;

           

            System.Data.SqlClient.SqlParameter pactivo = new SqlParameter("@Activo", System.Data.SqlDbType.Bit);
            pactivo.Value = activo;
            cmd.Parameters.Add(pactivo);

            System.Data.SqlClient.SqlParameter pnombre = new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar);
            pnombre.Value = nombreRol;
            cmd.Parameters.Add(pnombre);

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
        public bool PerfilUpdate(int IdPerfil, bool Activo, string Descripcion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PerfilUpdate";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pactivo = new SqlParameter("@Activo", System.Data.SqlDbType.Bit);
            pactivo.Value = Activo;
            cmd.Parameters.Add(pactivo);

            System.Data.SqlClient.SqlParameter pdescripcion = new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar);
            pdescripcion.Value = Descripcion;
            cmd.Parameters.Add(pdescripcion);

            System.Data.SqlClient.SqlParameter pId = new SqlParameter("@IdPerfil", System.Data.SqlDbType.Int);
            pId.Value = IdPerfil;
            cmd.Parameters.Add(pId);

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

        public int Perfil_SelectByMaxId(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Perfil_SelectByMaxId";
            cmd.Connection = sqlConnection;
           
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            if (ds.Rows.Count > 0)
            {
                return int.Parse(ds.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }

        }
        
    }
}