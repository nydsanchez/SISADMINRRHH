using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Retenciones
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public bool EditarRetenciones(int idRenta, decimal desde, decimal hasta, decimal impBase, decimal porcentj,
            decimal sobExc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RetencionesIREdit";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idRenta", System.Data.SqlDbType.Int);
            p1.Value = idRenta;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@desde", System.Data.SqlDbType.Decimal);
            p2.Value = desde;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@hasta", System.Data.SqlDbType.Decimal);
            p3.Value = hasta;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@impBase", System.Data.SqlDbType.Decimal);
            p4.Value = impBase;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@porcentj", System.Data.SqlDbType.Decimal);
            p5.Value = porcentj;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@sobExc", System.Data.SqlDbType.Decimal);
            p6.Value = sobExc;
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

        public bool InsertarRetenciones (decimal desde, decimal hasta, decimal impBase, decimal porcentj,
            decimal sobExc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RetencionesIRIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@desde", System.Data.SqlDbType.Decimal);
            p2.Value = desde;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@hasta", System.Data.SqlDbType.Decimal);
            p3.Value = hasta;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@impBase", System.Data.SqlDbType.Decimal);
            p4.Value = impBase;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@porcentj", System.Data.SqlDbType.Decimal);
            p5.Value = porcentj;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@sobExc", System.Data.SqlDbType.Decimal);
            p6.Value = sobExc;
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

        public bool EliminarRetenciones(int idRenta, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "RetencionesIRElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idRenta", System.Data.SqlDbType.Int);
            p1.Value = idRenta;
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


    }
}
