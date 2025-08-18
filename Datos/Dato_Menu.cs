using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Web;

namespace Datos
{
    public class Dato_Menu
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public List<MenuPerfil> MenuxPerfil(int Idperfil, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            List<MenuPerfil> e = new List<MenuPerfil>();

            CnBD con = new CnBD();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerMenuxPerfil";
            cmd.Connection = sqlConnection;

            SqlParameter perfil = new SqlParameter("@IdPerfil", SqlDbType.Int);
            perfil.Value = Idperfil;
            cmd.Parameters.Add(perfil);

            cmd.Connection.Open();
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    e.Add(
                    new MenuPerfil((int)dr["idMenu"],
                                         (string)dr["Nombre"],
                                         (string)dr["Descripcion"],
                                         (int)dr["Padre"],
                                         (string)dr["formulario"],
                                         (int)dr["orden"]
                                         ));
                }
            }
            cmd.Connection.Close();
            return e;
        }

        public int ObtenerPerfilxUsuario(int idusuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPerfilxUsuario";
            cmd.Connection = sqlConnection;


            SqlParameter ptipoV = new SqlParameter("@idUsuario", SqlDbType.Int);
            ptipoV.Value = idusuario;
            cmd.Parameters.Add(ptipoV);

           
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
            return int.Parse(ds.Rows[0][0].ToString());
        }

        public List<MenuPerfil> MenuObtenerItems(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            List<MenuPerfil> e = new List<MenuPerfil>();

            CnBD con = new CnBD();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MenuObtenerItems";
            cmd.Connection = sqlConnection;

            cmd.Connection.Open();
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    e.Add(
                    new MenuPerfil((int)dr["idMenu"],
                                         (string)dr["Nombre"],
                                         (string)dr["Descripcion"],
                                         (int)dr["Padre"],
                                         (string)dr["formulario"],
                                         (int)dr["orden"]
                                         ));
                }
            }
            cmd.Connection.Close();
            return e;
        }

        public List<int> ObtenerItemsxPerfil(int IdPerfil, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD cn = new CnBD();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MenuPerfilObtenerIdxPErfil";
            cmd.Connection = sqlConnection;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            SqlParameter pidPErfil = new SqlParameter("@IdPerfil", SqlDbType.Int);
            pidPErfil.Value = IdPerfil;
            cmd.Parameters.Add(pidPErfil);

            
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            List<int> i = dt.AsEnumerable().Select(r => r.Field<int>("IdMenu")).ToList();


            return i;


        }

        public DataSet cargarEmpresas()
        {
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarEmpresas";
            cmd.Connection = con.Init();
            
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empresa");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public string DataSource
        {
            get;
            set;
        }

        public string InitialCatalog
        {
            get;
            set;
        }

        public string UserID
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
        public void ObtenerCadena(int idEmpresa)
        {

            DataSet ds = new DataSet();
            DataRow dt;
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarEmpresas";
            cmd.Connection = con.Init();
            
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empresa");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            dt = ds.Tables[0].Select("Id_Bd=" + idEmpresa).FirstOrDefault();

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            DataSource = dt[2].ToString();
            InitialCatalog = dt[1].ToString();
            UserID = dt[3].ToString();
            Password = dt[4].ToString();
            builder.DataSource = DataSource;
            builder.InitialCatalog = InitialCatalog;
            builder.UserID = UserID;
            builder.Password = Password;

            string connectionString = ConfigurationManager.ConnectionStrings["BDRRHHConnectionString"].ConnectionString;
            //con = new CnBD(builder.ConnectionString);
            con = new CnBD(connectionString);
        }
    }

        #region
        //public static DataTable ObtenerCadena(int idEmpresa)
        //{
        //    DataSet ds = new DataSet();
        //    DataRow[] dt;

        //    CnBD con = new CnBD();
        //    DataTable nodos = new DataTable();
        //    nodos.Columns.Add("DataSource");
        //    nodos.Columns.Add("InitialCatalog");
        //    nodos.Columns.Add("UserID");
        //    nodos.Columns.Add("Password");

        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "cargarEmpresas";
        //    cmd.Connection = con.Init();
        //    System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    da.Fill(ds, "empresa");

        //    //dt = ds.Tables[0].Select("Id_Bd=" + idEmpresa);

        //    //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

        //    //DataSource = dt[2].ToString();
        //    //InitialCatalog = dt[1].ToString();
        //    //UserID = dt[3].ToString();
        //    //Password = dt[4].ToString();
        //    //builder.DataSource = DataSource;
        //    //builder.InitialCatalog = InitialCatalog;
        //    //builder.UserID = UserID;
        //    //builder.Password = Password;
        //    //con = new CnBD(builder.ConnectionString);
        //    //foreach (DataRow drow in dt)
        //    //{

        //    //    nodos.Rows.Add(drow[2].ToString(), drow[1].ToString(), drow[3].ToString(), drow[4].ToString());
        //    //}
        //    return ds.Tables[0];

        //}
        #endregion
    }

