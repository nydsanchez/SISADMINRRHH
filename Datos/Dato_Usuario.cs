using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Security.Cryptography;

namespace Datos
{
    public class Dato_Usuario
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public List<UsuarioSession> ValidarUsuario(UsuarioSessionD usuario)
        {

            List<UsuarioSession> u = new List<UsuarioSession>();

            CnBD con = new CnBD();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();

            SqlParameter pusuario = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar);
            pusuario.Value = usuario.Usuarios;
            cmd.Parameters.Add(pusuario);

            SqlParameter p_pass = new SqlParameter("@pass", System.Data.SqlDbType.VarChar);
            p_pass.Value = usuario.Pass;
            cmd.Parameters.Add(p_pass);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SEG].[Usuario_ValidarUsuario]";
            cmd.Connection = con.GetConecction();
            cmd.Connection.Open();
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    u.Add(
                        new UsuarioSession((int)dr["idUsuario"],
                                         (string)dr["usuario"],
                                             (string)dr["pass"],
                                             (Boolean)dr["activo"],
                                             (string)dr["nombre"],
                                             (string)dr["apellido"],
                                             (int)dr["codigo_empleado"]
                                             ));
                }
            }
            cmd.Connection.Close();
            return u;
        }

        public List<UsuarioSession> ValidarCredenciales(UsuarioSessionD usuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            List<UsuarioSession> u = new List<UsuarioSession>();

            CnBD con = new CnBD();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();

            SqlParameter pusuario = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar);
            pusuario.Value = usuario.Usuarios;
            cmd.Parameters.Add(pusuario);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SEG].[Usuario_ValidarCredenciales]";
            cmd.Connection = sqlConnection;
            cmd.Connection.Open();
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    u.Add(
                        new UsuarioSession((int)dr["idUsuario"],
                                         (string)dr["usuario"],
                                             (string)dr["pass"],
                                             (Boolean)dr["activo"],
                                             (string)dr["nombre"],
                                             (string)dr["apellido"],
                                             (int)dr["codigo_empleado"]
                                             ));
                }
            }
            cmd.Connection.Close();
            return u;
            //if (u.Count > 0) return true;
            //else return false;
        }

        public bool ValidarOpcion(int rol, string menu, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dsMenusHijos = this.ExtraerNiveles(rol, idEmpresa);

            for (int j = 0; j < dsMenusHijos.Tables["Niveles"].Rows.Count; j++)
            {
                string opcion = dsMenusHijos.Tables["Niveles"].Rows[j]["programa"].ToString().Trim();
                int y = opcion.Length - 1;
                if (menu == opcion.Substring(2))
                {
                    return true;
                }
            }

            return false;
        }

        private DataSet ExtraerNiveles(int rol, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            DataSet ds = new System.Data.DataSet();
            string query = "SELECT SegMenu.idgrupo, SegMenu.idnivel,programa,leyenda FROM SegMenuxRoles INNER JOIN SegMenu on SegMenuxRoles.idmenu = SegMenu.idmenu INNER JOIN SegMenuGrupo on SegMenu.idgrupo = SegMenuGrupo.idgrupo where SegMenuxRoles.idrol = @idrol";
            SqlCommand cmd = new SqlCommand(query, con.GetConecction());

            SqlParameter p1 = new SqlParameter("@idrol", System.Data.SqlDbType.Int);
            p1.Value = rol;
            cmd.Parameters.Add(p1);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.Connection = sqlConnection;

            
            try
            {
                da.Fill(ds, "Niveles");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        private DataSet ExtraerMenus(int rol, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.DataSet ds = new System.Data.DataSet();
            string query = "SELECT descripcion,SegMenu.idgrupo FROM SegMenuxRoles INNER JOIN SegMenu on SegMenuxRoles.idmenu = SegMenu.idmenu INNER JOIN SegMenuGrupo on SegMenu.idgrupo = SegMenuGrupo.idgrupo where SegMenuxRoles.idrol = @idrol group by descripcion, SegMenu.idgrupo order by SegMenu.idgrupo";
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(query, con.GetConecction());

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idrol", System.Data.SqlDbType.Int);
            p1.Value = rol;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand.Connection = sqlConnection;

           
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                da.Fill(ds, "MenuRaiz");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataTable Usuario_Select(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "[SEG].[Usuario_Select]";
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
            return ds;
        }
        public bool UsuarioInsert(string usuario, string pass, bool activo, string nombre, string apellido, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SEG.UsuarioInsertar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pusuario = new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar);
            pusuario.Value = usuario;
            cmd.Parameters.Add(pusuario);

            System.Data.SqlClient.SqlParameter ppass = new SqlParameter("@pass", System.Data.SqlDbType.VarChar);
            ppass.Value = pass;
            cmd.Parameters.Add(ppass);

            System.Data.SqlClient.SqlParameter pactivo = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            pactivo.Value = activo;
            cmd.Parameters.Add(pactivo);

            System.Data.SqlClient.SqlParameter pnombre = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            pnombre.Value = nombre;
            cmd.Parameters.Add(pnombre);

            System.Data.SqlClient.SqlParameter papellido = new SqlParameter("@apellido", System.Data.SqlDbType.VarChar);
            papellido.Value = apellido;
            cmd.Parameters.Add(papellido);

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
        public bool UsuarioUpdate(string usuario, bool activo, string nombre, string apellido, int idUsuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UsuarioUpdate";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pusuario = new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar);
            pusuario.Value = usuario;
            cmd.Parameters.Add(pusuario);

            System.Data.SqlClient.SqlParameter pactivo = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            pactivo.Value = activo;
            cmd.Parameters.Add(pactivo);

            System.Data.SqlClient.SqlParameter pnombre = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            pnombre.Value = nombre;
            cmd.Parameters.Add(pnombre);

            System.Data.SqlClient.SqlParameter papellido = new SqlParameter("@apellido", System.Data.SqlDbType.VarChar);
            papellido.Value = apellido;
            cmd.Parameters.Add(papellido);

            System.Data.SqlClient.SqlParameter pIdUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            pIdUsuario.Value = idUsuario;
            cmd.Parameters.Add(pIdUsuario);


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
        public bool UsuarioUpdatePass(string usuario, string pass, bool activo, string nombre, string apellido, int idUsuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UsuarioUpdatePass";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pusuario = new SqlParameter("@Usuario", System.Data.SqlDbType.VarChar);
            pusuario.Value = usuario;
            cmd.Parameters.Add(pusuario);

            System.Data.SqlClient.SqlParameter ppass = new SqlParameter("@pass", System.Data.SqlDbType.VarChar);
            ppass.Value = pass;
            cmd.Parameters.Add(ppass);

            System.Data.SqlClient.SqlParameter pactivo = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            pactivo.Value = activo;
            cmd.Parameters.Add(pactivo);

            System.Data.SqlClient.SqlParameter pnombre = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            pnombre.Value = nombre;
            cmd.Parameters.Add(pnombre);

            System.Data.SqlClient.SqlParameter papellido = new SqlParameter("@apellido", System.Data.SqlDbType.VarChar);
            papellido.Value = apellido;
            cmd.Parameters.Add(papellido);

            System.Data.SqlClient.SqlParameter pIdUsuario = new SqlParameter("@idUsuario", System.Data.SqlDbType.Int);
            pIdUsuario.Value = idUsuario;
            cmd.Parameters.Add(pIdUsuario);

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

        public int Usuario_SelectByMaxId(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Usuario_SelectByMaxId";
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

        public string EncriptarContraseña(string contraseña)
        {
            UnicodeEncoding codificador = new
                UnicodeEncoding();

            string encriptar = contraseña;
            byte[] datos = codificador.GetBytes(encriptar);
            byte[] resultado;
            SHA1 encriptarSHA = new
            SHA1CryptoServiceProvider();

            resultado = encriptarSHA.ComputeHash(datos);


            StringBuilder sBuilder = new
            StringBuilder();


            // Repite a travez de cada byte de el hash y formatea cada uno como un string hexadecimal.

            for (int i = 0; i < resultado.Length; i++)
            {

                sBuilder.Append(resultado[i].ToString("x2"));

            }


            return sBuilder.ToString();
        }


    }
}
