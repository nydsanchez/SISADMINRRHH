using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class Dato_Marca
    {

        //public dsPlanilla.dtMarcasDataTable ImportarMarcasdePlanilla(DateTime fecha, int idEmpresa)
        //{

        //    ConnectionRepository conect = new ConnectionRepository();
        //    dsPlanilla.dtMarcasDataTable dtMarcas = new dsPlanilla.dtMarcasDataTable();

        //    SqlConnection sqlConnection = conect.getConnection(idEmpresa);
        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

        //    System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
        //    p1.Value = fecha;
        //    cmd.Parameters.Add(p1);

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "spMarcadasSel";
        //    cmd.Connection = sqlConnection;
        //    System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    try
        //    {
        //        da.Fill(dtMarcas);
        //    }
        //    catch (SystemException)
        //    {
        //        if (cmd.Connection.State != ConnectionState.Closed)
        //            cmd.Connection.Close();
        //    }

        //    return dtMarcas;
        //}
        public dsPlanilla.dtMarcasDataTable ImportarMarcasdePlanilla(DateTime fechaini, DateTime fechafin, int idEmpresa)
        {

            ConnectionRepository conect = new ConnectionRepository();
            dsPlanilla.dtMarcasDataTable dtMarcas = new dsPlanilla.dtMarcasDataTable();

            SqlConnection sqlConnection = conect.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMarcadasSel";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dtMarcas);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dtMarcas;
        }
        public dsPlanilla.dtMarcasDataTable ImportarMarcas(DateTime fecha, int idEmpresa)
        {
            ConnectionRepository conect = new ConnectionRepository();
            dsPlanilla.dtMarcasDataTable dtMarcas = new dsPlanilla.dtMarcasDataTable();

            SqlConnection sqlConnection = conect.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p1.Value = fecha;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMarcasBioadminSel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                 da.Fill(dtMarcas);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dtMarcas;
        }

        public int UpdMarcasdePlanilla(dsPlanilla.dtMarcasDataTable dtMarcas, int idEmpresa)
        {

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter pfecha = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            cmd.Parameters.Add(pfecha);

            System.Data.SqlClient.SqlParameter phorae = new SqlParameter("@horae", System.Data.SqlDbType.Time);
            cmd.Parameters.Add(phorae);

            System.Data.SqlClient.SqlParameter phoras = new SqlParameter("@horas", System.Data.SqlDbType.Time);
            cmd.Parameters.Add(phoras);

            System.Data.SqlClient.SqlParameter pcodigo_ubicacion = new SqlParameter("@codigo_ubicacion", System.Data.SqlDbType.Int);
            cmd.Parameters.Add(pcodigo_ubicacion);

            System.Data.SqlClient.SqlParameter pcodigo_empleado = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            cmd.Parameters.Add(pcodigo_empleado);

            System.Data.SqlClient.SqlParameter pcodigo_depto = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            cmd.Parameters.Add(pcodigo_depto);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMarcadasUpd";
            cmd.Connection = sqlConnection;

            int i = 0;
            try
            {
                cmd.Connection.Open();

                for (i = 0; i < dtMarcas.Rows.Count; i++)
                {
                    cmd.Parameters["@fecha"].Value = dtMarcas[i].fecha;
                    cmd.Parameters["@horae"].Value = dtMarcas[i].horae;
                    cmd.Parameters["@horas"].Value = dtMarcas[i].horas;
                    cmd.Parameters["@codigo_ubicacion"].Value = dtMarcas[i].codigo_ubicacion;
                    cmd.Parameters["@codigo_empleado"].Value = dtMarcas[i].codigo_empleado;
                    cmd.Parameters["@codigo_depto"].Value = dtMarcas[i].codigo_depto;
                    cmd.ExecuteNonQuery();
                }
                cmd.Connection.Close();

            }
            catch (System.Data.DataException ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                string exc = ex.Message;
            }

            return i;
            
        }


        public int AddMarcasdePlanilla(dsPlanilla.dtMarcasDataTable dtMarcas, int idEmpresa)
        {

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter pfecha = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            cmd.Parameters.Add(pfecha);

            System.Data.SqlClient.SqlParameter phorae = new SqlParameter("@horae", System.Data.SqlDbType.Time);
            cmd.Parameters.Add(phorae);

            System.Data.SqlClient.SqlParameter phoras = new SqlParameter("@horas", System.Data.SqlDbType.Time);
            cmd.Parameters.Add(phoras);

            System.Data.SqlClient.SqlParameter pcodigo_ubicacion = new SqlParameter("@codigo_ubicacion", System.Data.SqlDbType.Int);
            cmd.Parameters.Add(pcodigo_ubicacion);

            System.Data.SqlClient.SqlParameter pcodigo_empleado = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            cmd.Parameters.Add(pcodigo_empleado);

            System.Data.SqlClient.SqlParameter pcodigo_depto = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            cmd.Parameters.Add(pcodigo_depto);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMarcadasAdd";
            cmd.Connection = sqlConnection;

            int i = 0;
            try
            {
                cmd.Connection.Open();

                for (i = 0; i < dtMarcas.Rows.Count; i++)
                {
                    cmd.Parameters["@fecha"].Value = dtMarcas[i].fecha;
                    cmd.Parameters["@horae"].Value = dtMarcas[i].horae;
                    cmd.Parameters["@horas"].Value = dtMarcas[i].horas;
                    cmd.Parameters["@codigo_ubicacion"].Value = dtMarcas[i].codigo_ubicacion;
                    cmd.Parameters["@codigo_empleado"].Value = dtMarcas[i].codigo_empleado;
                    cmd.Parameters["@codigo_depto"].Value = dtMarcas[i].codigo_depto;

                    cmd.ExecuteNonQuery();
                }
                cmd.Connection.Close();

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return i;

        }


    }
}
