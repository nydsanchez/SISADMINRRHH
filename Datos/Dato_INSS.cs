using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_INSS
    {
        ConnectionRepository ConnectionRepository;

        public Dato_INSS()
        {
            ConnectionRepository = new ConnectionRepository();
        }

        public dsInss.dtIngresoDataTable ObtenerIngresosxRangodeFecha(DateTime fechainicio, DateTime fechafin,int idempresa)
        {
            dsInss.dtIngresoDataTable dt = new dsInss.dtIngresoDataTable();

            ConnectionRepository conect = new ConnectionRepository();
            SqlConnection sqlConnection = conect.getConnection(idempresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechainicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerIngresosxRangodeFecha";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }

        public dsPlanilla.dtEmpleadoDataTable ObtEmpleadosxRangodeTiempoLiquidaciones(DateTime fechaini, DateTime fechafin, int ubicacion, int idEmpresa)
        {
            dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtEmpleadosxRangodeTiempoLiquidados";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.DateTime);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@fechafin", System.Data.SqlDbType.DateTime);
            p41.Value = fechafin;
            cmd.Parameters.Add(p41);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p1.Value = ubicacion;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dtEmpleado);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dtEmpleado;
        }
    }
}
