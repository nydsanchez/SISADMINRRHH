using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Feriados
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
       public dsPlanilla.dtFeriadosDataTable SeleccionarDiasFeriados(DateTime fecini,DateTime fecfin,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            dsPlanilla.dtFeriadosDataTable feriados = new dsPlanilla.dtFeriadosDataTable();

            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecini", System.Data.SqlDbType.Date);
            p1.Value = fecini;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fecfin", System.Data.SqlDbType.Date);
            p11.Value = fecfin;
            cmd.Parameters.Add(p11);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDiasFeriados";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(feriados);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }


            return feriados;
        }

       public bool InsertardiasFeriados(DateTime fecha, string descripcion, decimal cantidadDias, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DiasFeriadosIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p1.Value = fecha;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p2.Value = descripcion;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@cantidadDias", System.Data.SqlDbType.Decimal);
            p3.Value = cantidadDias;
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

       public bool EditardiasFeriados(DateTime fecha, string descripcion, decimal cantidadDias, DateTime nfecha, string ndesc, decimal ncant, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DiasFeriadosEdit";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p1.Value = fecha;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@descripcion", System.Data.SqlDbType.VarChar);
            p2.Value = descripcion;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@cantidadDias", System.Data.SqlDbType.Decimal);
            p3.Value = cantidadDias;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaTemp", System.Data.SqlDbType.Date);
            p4.Value = nfecha;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@descripcionTemp", System.Data.SqlDbType.VarChar);
            p5.Value = ndesc;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@cantidadDiasTemp", System.Data.SqlDbType.Decimal);
            p6.Value = ncant;
            cmd.Parameters.Add(p6);

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

       public bool EliminardiasFeriados(DateTime nfecha, string ndesc, decimal ncant, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DiasFeriadosElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaTemp", System.Data.SqlDbType.Date);
            p4.Value = nfecha;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@descripcionTemp", System.Data.SqlDbType.VarChar);
            p5.Value = ndesc;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@cantidadDiasTemp", System.Data.SqlDbType.Decimal);
            p6.Value = ncant;
            cmd.Parameters.Add(p6);

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
