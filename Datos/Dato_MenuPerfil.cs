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
    public class Dato_MenuPerfil
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public bool MenuPerfilDelete(int IdPerfil, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MenuPerfilDelete";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pusuario = new SqlParameter("@IdPerfil", System.Data.SqlDbType.Int);
            pusuario.Value = IdPerfil;
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

        public bool MenuPerfilInsert(int IdMenu, int IdPerfil, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MenuPerfilInsert";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pusuario = new SqlParameter("@IdMenu", System.Data.SqlDbType.Int);
            pusuario.Value = IdMenu;
            cmd.Parameters.Add(pusuario);

            System.Data.SqlClient.SqlParameter pperfil = new SqlParameter("@IdPerfil", System.Data.SqlDbType.Int);
            pperfil.Value = IdPerfil;
            cmd.Parameters.Add(pperfil);

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