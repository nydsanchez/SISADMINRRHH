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
    public class Dato_UsuarioPerfil
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public DataTable UsuarioPerfil_Select(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SEG].[UsuarioPerfil_Select]";
            cmd.Connection = sqlConnection;
           

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            IEnumerable<DataRow> sequence = ds.AsEnumerable();
            List<DataRow> list = ds.AsEnumerable().ToList();

            return ds;
        }
        public bool UsuarioPerfilInsert(int idUsuario, int IdPerfil, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UsuarioPerfilInsert";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pusuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            pusuario.Value = idUsuario;
            cmd.Parameters.Add(pusuario);

            System.Data.SqlClient.SqlParameter pPerfil = new SqlParameter("@idPErfil", System.Data.SqlDbType.Int);
            pPerfil.Value = IdPerfil;
            cmd.Parameters.Add(pPerfil);

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
        public bool UsuarioPerfilDelete(int idUsuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UsuarioPerfilDelete";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pusuario = new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int);
            pusuario.Value = idUsuario;
            cmd.Parameters.Add(pusuario);

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
    }
}