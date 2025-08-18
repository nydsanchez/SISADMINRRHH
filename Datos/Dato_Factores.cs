using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Factores
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public DataTable obtenerFactor(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerFactor";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "factor");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }


            return ds.Tables[0];
        }

        public DataTable obtenerTurnos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerTurnos";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "turnos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds.Tables[0];
        }

        public DataTable SeleccionarRetencionesIR(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerRetencionesIR";
            cmd.Connection = sqlConnection;
          
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "retenciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds.Tables[0];
        }
    }
}
