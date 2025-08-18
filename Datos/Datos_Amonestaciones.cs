using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class Datos_Amonestaciones
    {

        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion

        public DataTable ObtenerCatalogoAmonestaciones(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();



            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerCatalogoAmonestaciones";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Amonestaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable ObtenergrupoCatalogo(int idEmpresa, int idgrupo)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@grupo", System.Data.SqlDbType.Int);
            p1.Value = idgrupo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenergrupoCatalogo";
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

        public bool InsertarAmonestaciones(int idEmpresa, int codigoEmpleado, int idAmonestacion, string sancion, DateTime fechaAMonestacion, string usuario, string Observacion)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AmonestacionesInsertar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p1.Value = idEmpresa;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_Empleado", System.Data.SqlDbType.Int);
            p2.Value = codigoEmpleado;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@id_Amonestacion", System.Data.SqlDbType.Int);
            p3.Value = idAmonestacion;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@sancion", System.Data.SqlDbType.VarChar);
            p4.Value = sancion;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaSancion", System.Data.SqlDbType.Date);
            p5.Value = fechaAMonestacion;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@fechaRegistro", System.Data.SqlDbType.Date);
            p6.Value = DateTime.Now.Date;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@horaRegistro", System.Data.SqlDbType.Time);
            p7.Value = DateTime.Now.TimeOfDay;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@usuarioRegistro", System.Data.SqlDbType.VarChar);
            p8.Value = usuario;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p9.Value = Observacion;
            cmd.Parameters.Add(p9);

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

        public bool UpdateAmonestaciones(int idEmpresa, int codigoEmpleado, int idAmonestacion, int idAmonestacionnew, string sancion, string sancionnew, DateTime fechaAMonestacion, DateTime fechaAMonestacionnew, DateTime fecharegistro,TimeSpan horaregistro, string usuario, string Observacion)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AmonestacionesUpdate";
            cmd.Connection = sqlConnection;

          

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_Empleado", System.Data.SqlDbType.Int);
            p2.Value = codigoEmpleado;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@id_Amonestacion", System.Data.SqlDbType.Int);
            p3.Value = idAmonestacion;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@id_AmonestacionNew", System.Data.SqlDbType.Int);
            p4.Value = idAmonestacionnew;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@sancion", System.Data.SqlDbType.VarChar);
            p5.Value = sancion;
            cmd.Parameters.Add(p5);


            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@sancionNew", System.Data.SqlDbType.VarChar);
            p6.Value = sancionnew;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@fechaSancion", System.Data.SqlDbType.Date);
            p7.Value = fechaAMonestacion;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@fechaSancionNew", System.Data.SqlDbType.Date);
            p8.Value = fechaAMonestacionnew;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@fechaRegistro", System.Data.SqlDbType.Date);
            p9.Value = fecharegistro;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@fechaRegistroNew", System.Data.SqlDbType.Date);
            p10.Value = DateTime.Now.Date;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@horaRegistro", System.Data.SqlDbType.Time);
            p11.Value = horaregistro;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@horaRegistroNew", System.Data.SqlDbType.Time);
            p12.Value = DateTime.Now.TimeOfDay;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@usuarioRegistro", System.Data.SqlDbType.VarChar);
            p13.Value = usuario;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p14.Value = Observacion;
            cmd.Parameters.Add(p14);

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

        public bool AmonestacionesDelete(int idEmpresa, int codigoEmpleado, int idAmonestacion, string sancion, DateTime fechaAMonestacion, DateTime fechaReg, TimeSpan horaR, string usuario)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AmonestacionesDelete";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_Empleado", System.Data.SqlDbType.Int);
            p2.Value = codigoEmpleado;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@id_Amonestacion", System.Data.SqlDbType.Int);
            p3.Value = idAmonestacion;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@sancion", System.Data.SqlDbType.VarChar);
            p4.Value = sancion;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaSancion", System.Data.SqlDbType.Date);
            p5.Value = fechaAMonestacion;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@fechaRegistro", System.Data.SqlDbType.Date);
            p6.Value = fechaReg;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@horaRegistro", System.Data.SqlDbType.Time);
            p7.Value = horaR;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@usuarioRegistro", System.Data.SqlDbType.VarChar);
            p8.Value = usuario;
            cmd.Parameters.Add(p8);


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

        public bool AmonestacionesLOGInsertar(int idEmpresa,  int codigoEmpleado, int idAmonestacion, int idAmonestacionnew, string sancion, string sancionnew, DateTime fechaAMonestacion, DateTime fechaAMonestacionnew,  string usuario, string Observacion, string accion)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AmonestacionesLOGInsertar";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p1.Value = idEmpresa;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_Empleado", System.Data.SqlDbType.Int);
            p2.Value = codigoEmpleado;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@id_Amonestacion", System.Data.SqlDbType.Int);
            p3.Value = idAmonestacion;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@id_AmonestacionNew", System.Data.SqlDbType.Int);
            p4.Value = idAmonestacionnew;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@sancion", System.Data.SqlDbType.VarChar);
            p5.Value = sancion;
            cmd.Parameters.Add(p5);


            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@sancionNew", System.Data.SqlDbType.VarChar);
            p6.Value = sancionnew;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@fechaSancion", System.Data.SqlDbType.Date);
            p7.Value = fechaAMonestacion;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@fechaSancionNew", System.Data.SqlDbType.Date);
            p8.Value = fechaAMonestacionnew;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@fechaRegistro", System.Data.SqlDbType.Date);
            p9.Value = DateTime.Now.Date; ;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@horaRegistro", System.Data.SqlDbType.Time);
            p11.Value = DateTime.Now.TimeOfDay;
            cmd.Parameters.Add(p11);


            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@usuarioRegistro", System.Data.SqlDbType.VarChar);
            p13.Value = usuario;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p14.Value = Observacion;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@accion", System.Data.SqlDbType.VarChar);
            p15.Value = accion;
            cmd.Parameters.Add(p15);

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

        public DataTable getamonestaciones(int idEmpresa, DateTime fechaini, DateTime fechafin)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getamonestaciones";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Amonestaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable getAmonestacionesAplicarInc(int idEmpresa, DateTime fechaini, DateTime fechafin)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getAmonestacionesAplicarInc";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Amonestaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public DataTable getamonestacionesByRango(int idEmpresa, DateTime fechaini, DateTime fechafin, int codigoE, bool bycodigo, bool byrango)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            SqlParameter p3 = new SqlParameter("@codigoE", System.Data.SqlDbType.VarChar);
            p3.Value = codigoE;
            cmd.Parameters.Add(p3);

           
            SqlParameter p4 = new SqlParameter("@bycodigo", System.Data.SqlDbType.Bit);
            p4.Value = bycodigo;
            cmd.Parameters.Add(p4);

            SqlParameter p5 = new SqlParameter("@byRango", System.Data.SqlDbType.Bit);
            p5.Value = byrango;
            cmd.Parameters.Add(p5);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getamonestacionesByRango";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Amonestaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public bool CatalogoAmonestacionesInsertar(int idEmpresa, string DetalleF, string codigF, string nivelF, string sancion)
        {
          

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CatalogoAmonestacionesInsertar";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@detalleFalta", System.Data.SqlDbType.VarChar);
            p1.Value = DetalleF;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigofalta", System.Data.SqlDbType.NChar);
            p2.Value = codigF;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@nivelFalta", System.Data.SqlDbType.VarChar);
            p3.Value = nivelF;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@sancion", System.Data.SqlDbType.VarChar);
            p4.Value = sancion;
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
        public bool CatalogoAmonestacionesUpdate(int idEmpresa, int idA, string DetalleF, string codigF, string nivelF, string sancion)
        {


            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CatalogoAmonestacionesUpdate";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = idA;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@detalleFalta", System.Data.SqlDbType.VarChar);
            p1.Value = DetalleF;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigofalta", System.Data.SqlDbType.NChar);
            p2.Value = codigF;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@nivelFalta", System.Data.SqlDbType.VarChar);
            p3.Value = nivelF;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@sancion", System.Data.SqlDbType.VarChar);
            p4.Value = sancion;
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
        public bool CatalogoInsert(int idEmpresa, int idG, string Descripcion)
        {


            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CatalogoInsert";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@idGrupo", System.Data.SqlDbType.Int);
            p0.Value = idG;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p1.Value = Descripcion;
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

        public bool updategrupoCatalogo(int idEmpresa, int idG, string Descripcion, int iddes)
        {


            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "updategrupoCatalogo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@idGrupo", System.Data.SqlDbType.Int);
            p0.Value = idG;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idDescripcion", System.Data.SqlDbType.Int);
            p1.Value = iddes;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p2.Value = Descripcion;
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
    }
}
