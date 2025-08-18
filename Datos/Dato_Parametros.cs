using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Configuration;
using System.Data.Common;
using System.Xml.Linq;

namespace Datos
{
    public class Dato_Parametros 
    {

        #region CLASE DE OBJETOS

        public class GrupoParam
        {
            private int _idgrupo;

            public int Idgrupo
            {
                get { return _idgrupo; }
                set { _idgrupo = value; }
            }

            private string _nombre;

            public string Nombre
            {
                get { return _nombre; }
                set { _nombre = value; }
            }

            private int _idparametro;

            public int Idparametro
            {
                get { return _idparametro; }
                set { _idparametro = value; }
            }

            private string _nombreP;

            public string NombreP
            {
                get { return _nombreP; }
                set { _nombreP = value; }
            }
            private decimal _valor;

            public decimal Valor
            {
                get { return _valor; }
                set { _valor = value; }
            }

            public GrupoParam(int idgrupo, string nombre, int idparametro, string nombreP, decimal valor)
            {
                Idgrupo = idgrupo;
                Nombre = nombre;
                Idparametro = idparametro;
                NombreP = nombreP;
                Valor = valor;
            }
            
        }

        public Dictionary<string, int> diccionario = new Dictionary<string, int>();
        #endregion

        Dictionary<string, int> dictionary = new Dictionary<string, int>();
	
        #region
        //CREADO POR WENDY MEMBREÑO
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public bool ParametrosInsert(int idEmpresa, int idgrupo, string nombre,decimal valor, string descripcion)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosInsert";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@idgrupo", System.Data.SqlDbType.Int);
            p0.Value = idgrupo;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p1.Value = nombre;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p3.Value = valor;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p2.Value = descripcion;
            cmd.Parameters.Add(p2);

            

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

        public DataTable ParametrosSelect(int idEmpresa, int idgrupo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosSelect";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@idgroup", System.Data.SqlDbType.Int);
            p0.Value = idgrupo;
            cmd.Parameters.Add(p0);

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

        public bool ParametrosUpdate(int idEmpresa, int id, int idgrupo, string nombre, decimal valor, string descripcion)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosUpdate";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idgrupo", System.Data.SqlDbType.Int);
            p1.Value = idgrupo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p2.Value = nombre;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p3.Value = descripcion;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p4.Value = valor;
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
        public bool ParametrosDelete(int idEmpresa, int id, int idgrupo)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosDelete";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idgrupo", System.Data.SqlDbType.Int);
            p1.Value = idgrupo;
            cmd.Parameters.Add(p1);

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
        public DataTable ParametrosGroupSelect(int idEmpresa)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosGroupSelect";
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
        public bool ParametrosGrupoInsert(int idEmpresa, string nombre, string descripcion)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosGrupoInsert";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p1.Value = nombre;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p2.Value = descripcion;
            cmd.Parameters.Add(p2);

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
        public bool ParametrosGrupoUpdate(int idEmpresa, int id, string nombre, string descripcion)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosGrupoUpdate";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p2.Value = nombre;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p3.Value = descripcion;
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
        public bool ParametrosGrupoDelete(int idEmpresa, int id)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosGrupoDelete";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

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

        public void ParametrosGrupoObtenerGruposyParametros(int idEmpresa)
        {
            HttpContext.Current.Session["GrupoParametros"] = "";
            List<GrupoParam> e = new List<GrupoParam>();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametrosGrupoObtenerxGrupo";
            cmd.Connection = sqlConnection;
            cmd.Connection.Open();
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        e.Add(
                            new GrupoParam((int)dr["idgrupo"],
                                             (string)dr["nombre"],
                                             (int)dr["idparametro"],
                                             (string)dr["nombreP"],
                                             (decimal)dr["valor"]));

                    }
                }
                cmd.Connection.Close();
                 HttpContext.Current.Session["GrupoParametros"] = e;
                
        }
        public  Dictionary<string, int> ParametrosGrupoObtenerGrupos()
        {
            Dictionary<string, int> dicionario = new Dictionary<string, int>();
            List<GrupoParam> e = (List<GrupoParam>)HttpContext.Current.Session["GrupoParametros"];
            e = e.GroupBy(p => new { p.Idgrupo, p.Nombre }).Select(p => p.First()).ToList();
            foreach (var item in e)
            {
                dicionario.Add(item.Nombre, item.Idgrupo);
            }

            return dicionario;
        }

        public Dictionary<string, int> ObtenerParametrosxGrupo(int idgrupo)
        {
            Dictionary<string, int> dicionario = new Dictionary<string, int>();
            List<GrupoParam> e = (List<GrupoParam>)HttpContext.Current.Session["GrupoParametros"];
            var query = (from i in e
                         where i.Idgrupo.Equals(idgrupo)
                         select new
                         {
                             i.NombreP,
                             i.Idparametro
                         });
            foreach (var item in query)
            {
                dicionario.Add(item.NombreP, item.Idparametro);
            }

            return dicionario;
        }
        public decimal obtenerValorParametro(int parametro)
        {
            List<GrupoParam> e =  ( List<GrupoParam>) HttpContext.Current.Session["GrupoParametros"];
            decimal valor = (from i in e
                           where i.Idparametro.Equals(parametro)
                           select i.Valor).SingleOrDefault();
            return valor;
            
        }
     
    }
}
