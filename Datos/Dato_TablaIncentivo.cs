using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_TablaIncentivo
    {
        public DataTable Select(int estilo, int proteccion)
        {
            DataTable dt = new DataTable();            
            SqlConnection sqlConnection = new CnBD().GetConecctionC();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoSel";
            
            SqlParameter p1 = new SqlParameter("@estilo", SqlDbType.Int);
            p1.Value = estilo;
            cmd.Parameters.Add(p1);
            
            SqlParameter p2 = new SqlParameter("@proteccion", SqlDbType.Int);
            p2.Value = proteccion;
            cmd.Parameters.Add(p2);
            
            cmd.Connection = sqlConnection;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }

            return dt;
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();
            ConnectionRepository conect = new ConnectionRepository();

            CnBD cnBD = new CnBD();
            SqlConnection sqlConnection = cnBD.GetConecctionC();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoAll";
            cmd.Connection = sqlConnection;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                    throw new Exception(ms);
                }
            }
            return dt;
        }
        public DataTable SelectByEffic(decimal eficdesde,decimal efichasta)
        {
            DataTable dt = new DataTable();
            ConnectionRepository conect = new ConnectionRepository();

            CnBD cnBD = new CnBD();
            SqlConnection sqlConnection = cnBD.GetConecctionC();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoByRango";
            cmd.Connection = sqlConnection;

            SqlParameter p1 = new SqlParameter("@eficienciaDesde", SqlDbType.Decimal);
            p1.Value = eficdesde;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@eficienciaHasta", SqlDbType.Decimal);
            p2.Value = efichasta;
            cmd.Parameters.Add(p2);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                    throw new Exception(ms);
                }
            }
            return dt;
        }
        public DataTable SelectByConstruccion(int idconstruccion)
        {
            DataTable dt = new DataTable();
            ConnectionRepository conect = new ConnectionRepository();

            CnBD cnBD = new CnBD();
            SqlConnection sqlConnection = cnBD.GetConecctionC();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoByConstr";
            cmd.Connection = sqlConnection;

            SqlParameter p1 = new SqlParameter("@idconstruccion", SqlDbType.Decimal);
            p1.Value = idconstruccion;
            cmd.Parameters.Add(p1);            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                    throw new Exception(ms);
                }
            }
            return dt;
        }
        public bool Insert(int idArea, int idConstIncent,decimal bonoAsist,decimal dzdesde, decimal dzhasta, int idRangoOql,decimal costoDz, decimal eficienciaDesde, decimal eficienciaHasta, decimal meta5dias, decimal meta4dias, decimal bonoCalidad, decimal costoSem, int idtablaprecio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new CnBD().GetConecctionC();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivonIns";

            SqlParameter p1 = new SqlParameter("@idArea", SqlDbType.Int);
            p1.Value = idArea;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@idConstIncent", SqlDbType.Int);
            p2.Value = idConstIncent;
            cmd.Parameters.Add(p2);

            SqlParameter p3 = new SqlParameter("@bonoAsist", SqlDbType.Decimal);
            p3.Value = bonoAsist;
            cmd.Parameters.Add(p3);

            SqlParameter p4 = new SqlParameter("@dzdesde", SqlDbType.Decimal);
            p4.Value = dzdesde;
            cmd.Parameters.Add(p4);

            SqlParameter p5 = new SqlParameter("@dzhasta", SqlDbType.Decimal);
            p5.Value = dzhasta;
            cmd.Parameters.Add(p5);

            SqlParameter p6 = new SqlParameter("@idRangoOql", SqlDbType.Int);
            p6.Value = idRangoOql;
            cmd.Parameters.Add(p6);

            SqlParameter p7 = new SqlParameter("@costoDz", SqlDbType.Decimal);
            p7.Value = costoDz;
            cmd.Parameters.Add(p7);

            SqlParameter p8 = new SqlParameter("@eficienciaDesde", SqlDbType.Decimal);
            p8.Value = eficienciaDesde;
            cmd.Parameters.Add(p8);

            SqlParameter p9 = new SqlParameter("@eficienciaHasta", SqlDbType.Decimal);
            p9.Value = eficienciaHasta;
            cmd.Parameters.Add(p9);

            SqlParameter p10 = new SqlParameter("@meta5dias", SqlDbType.Decimal);
            p10.Value = meta5dias;
            cmd.Parameters.Add(p10);

            SqlParameter p11 = new SqlParameter("@meta4dias", SqlDbType.Decimal);
            p11.Value = meta4dias;
            cmd.Parameters.Add(p11);

            SqlParameter p12 = new SqlParameter("@bonoCalidad", SqlDbType.Decimal);
            p12.Value = bonoCalidad;
            cmd.Parameters.Add(p12);            

            SqlParameter p13 = new SqlParameter("@costoSem", SqlDbType.Decimal);
            p13.Value = costoSem;
            cmd.Parameters.Add(p13);

            SqlParameter p14 = new SqlParameter("@idtablaprecio", SqlDbType.Decimal);
            p14.Value = idtablaprecio;
            cmd.Parameters.Add(p14);

            cmd.Connection = sqlConnection;

            int rsp = 0;
            try
            {
                cmd.Connection.Open();
                rsp = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                throw new Exception(ms);
            }

            if(rsp==1)
            return true;
            return false;
        }
        public bool InsertByEffic(int idArea, decimal bonoAsist, decimal dzdesde, decimal dzhasta, int idRangoOql, decimal costoDz, decimal eficienciaDesde, decimal eficienciaHasta, decimal meta5dias, decimal meta4dias, decimal bonoCalidad, decimal costoSem, int idtablaprecio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new CnBD().GetConecctionC();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoInsByEffic";

            SqlParameter p1 = new SqlParameter("@idArea", SqlDbType.Int);
            p1.Value = idArea;
            cmd.Parameters.Add(p1);            

            SqlParameter p3 = new SqlParameter("@bonoAsist", SqlDbType.Decimal);
            p3.Value = bonoAsist;
            cmd.Parameters.Add(p3);

            SqlParameter p4 = new SqlParameter("@dzdesde", SqlDbType.Decimal);
            p4.Value = dzdesde;
            cmd.Parameters.Add(p4);

            SqlParameter p5 = new SqlParameter("@dzhasta", SqlDbType.Decimal);
            p5.Value = dzhasta;
            cmd.Parameters.Add(p5);

            SqlParameter p6 = new SqlParameter("@idRangoOql", SqlDbType.Int);
            p6.Value = idRangoOql;
            cmd.Parameters.Add(p6);

            SqlParameter p7 = new SqlParameter("@costoDz", SqlDbType.Decimal);
            p7.Value = costoDz;
            cmd.Parameters.Add(p7);

            SqlParameter p8 = new SqlParameter("@eficienciaDesde", SqlDbType.Decimal);
            p8.Value = eficienciaDesde;
            cmd.Parameters.Add(p8);

            SqlParameter p9 = new SqlParameter("@eficienciaHasta", SqlDbType.Decimal);
            p9.Value = eficienciaHasta;
            cmd.Parameters.Add(p9);

            SqlParameter p10 = new SqlParameter("@meta5dias", SqlDbType.Decimal);
            p10.Value = meta5dias;
            cmd.Parameters.Add(p10);

            SqlParameter p11 = new SqlParameter("@meta4dias", SqlDbType.Decimal);
            p11.Value = meta4dias;
            cmd.Parameters.Add(p11);

            SqlParameter p12 = new SqlParameter("@bonoCalidad", SqlDbType.Decimal);
            p12.Value = bonoCalidad;
            cmd.Parameters.Add(p12);

            SqlParameter p13 = new SqlParameter("@costoSem", SqlDbType.Decimal);
            p13.Value = costoSem;
            cmd.Parameters.Add(p13);

            SqlParameter p14 = new SqlParameter("@idtablaprecio", SqlDbType.Decimal);
            p14.Value = idtablaprecio;
            cmd.Parameters.Add(p14);

            cmd.Connection = sqlConnection;

            int rsp = 0;
            try
            {
                cmd.Connection.Open();
                rsp = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                throw new Exception(ms);
            }

            if (rsp == 1)
                return true;
            return false;
        }
        public bool Update(int id,int idArea, int idConstIncent, decimal bonoAsist, decimal dzdesde, decimal dzhasta, int idRangoOql, decimal costoDz, decimal eficienciaDesde, decimal eficienciaHasta, decimal meta5dias, decimal meta4dias, decimal bonoCalidad, decimal costoSem, int idtablaprecio)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new CnBD().GetConecctionC();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoUpd";

            SqlParameter p0 = new SqlParameter("@id", SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

            SqlParameter p1 = new SqlParameter("@idArea", SqlDbType.Int);
            p1.Value = idArea;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@idConstIncent", SqlDbType.Int);
            p2.Value = idConstIncent;
            cmd.Parameters.Add(p2);

            SqlParameter p3 = new SqlParameter("@bonoAsist", SqlDbType.Decimal);
            p3.Value = bonoAsist;
            cmd.Parameters.Add(p3);

            SqlParameter p4 = new SqlParameter("@dzdesde", SqlDbType.Decimal);
            p4.Value = dzdesde;
            cmd.Parameters.Add(p4);

            SqlParameter p5 = new SqlParameter("@dzhasta", SqlDbType.Decimal);
            p5.Value = dzhasta;
            cmd.Parameters.Add(p5);

            SqlParameter p6 = new SqlParameter("@idRangoOql", SqlDbType.Int);
            p6.Value = idRangoOql;
            cmd.Parameters.Add(p6);

            SqlParameter p7 = new SqlParameter("@costoDz", SqlDbType.Decimal);
            p7.Value = costoDz;
            cmd.Parameters.Add(p7);

            SqlParameter p8 = new SqlParameter("@eficienciaDesde", SqlDbType.Decimal);
            p8.Value = eficienciaDesde;
            cmd.Parameters.Add(p8);

            SqlParameter p9 = new SqlParameter("@eficienciaHasta", SqlDbType.Decimal);
            p9.Value = eficienciaHasta;
            cmd.Parameters.Add(p9);

            SqlParameter p10 = new SqlParameter("@meta5dias", SqlDbType.Int);
            p10.Value = meta5dias;
            cmd.Parameters.Add(p10);

            SqlParameter p11 = new SqlParameter("@meta4dias", SqlDbType.Int);
            p11.Value = meta4dias;
            cmd.Parameters.Add(p11);

            SqlParameter p12 = new SqlParameter("@bonoCalidad", SqlDbType.Decimal);
            p12.Value = bonoCalidad;
            cmd.Parameters.Add(p12);

            SqlParameter p13 = new SqlParameter("@costoSem", SqlDbType.Decimal);
            p13.Value = costoSem;
            cmd.Parameters.Add(p13);

            SqlParameter p14 = new SqlParameter("@idtablaprecio", SqlDbType.Int);
            p14.Value = idtablaprecio;
            cmd.Parameters.Add(p14);

            cmd.Connection = sqlConnection;

            int rsp;
            try
            {
                cmd.Connection.Open();
                rsp = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                throw new Exception(ms);
            }

            if (rsp == 1)
                return true;
            return false;
        }
        public bool UpdateByEffic(decimal bonoAsist, decimal eficienciaDesde, decimal eficienciaHasta, decimal bonoCalidad, decimal costoSem)
        {
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new CnBD().GetConecctionC();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoUpdByEffic";

            SqlParameter p3 = new SqlParameter("@bonoAsist", SqlDbType.Decimal);
            p3.Value = bonoAsist;
            cmd.Parameters.Add(p3);

            SqlParameter p8 = new SqlParameter("@eficienciaDesde", SqlDbType.Decimal);
            p8.Value = eficienciaDesde;
            cmd.Parameters.Add(p8);

            SqlParameter p9 = new SqlParameter("@eficienciaHasta", SqlDbType.Decimal);
            p9.Value = eficienciaHasta;
            cmd.Parameters.Add(p9);
            
            SqlParameter p12 = new SqlParameter("@bonoCalidad", SqlDbType.Decimal);
            p12.Value = bonoCalidad;
            cmd.Parameters.Add(p12);

            SqlParameter p13 = new SqlParameter("@costoSem", SqlDbType.Decimal);
            p13.Value = costoSem;
            cmd.Parameters.Add(p13);            

            cmd.Connection = sqlConnection;

            int rsp;
            try
            {
                cmd.Connection.Open();
                rsp = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                throw new Exception(ms);
            }

            if (rsp == 1)
                return true;
            return false;
        }

        public bool Delete(int id)
        {
            SqlConnection sqlConnection = new CnBD().GetConecctionC();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoDel";

            SqlParameter p0 = new SqlParameter("@id", SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);            

            cmd.Connection = sqlConnection;

            int rsp;
            try
            {
                cmd.Connection.Open();
                rsp = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                throw new Exception(ms);
            }

            if (rsp == 1)
                return true;
            return false;
        }
        public bool DeleteByEffic(decimal eficienciadesde, decimal eficienciahasta)
        {
            SqlConnection sqlConnection = new CnBD().GetConecctionC();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncTablaIncentivoDelEffic";

            SqlParameter p1 = new SqlParameter("@eficienciadesde", SqlDbType.Decimal);
            p1.Value = eficienciadesde;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@eficienciahasta", SqlDbType.Decimal);
            p2.Value = eficienciahasta;
            cmd.Parameters.Add(p2);

            cmd.Connection = sqlConnection;

            int rsp;
            try
            {
                cmd.Connection.Open();
                rsp = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                throw new Exception(ms);
            }

            if (rsp == 1)
                return true;
            return false;
        }
    }
}
