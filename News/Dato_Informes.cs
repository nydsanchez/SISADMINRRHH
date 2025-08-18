using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace Datos
{
    public class Dato_Informes
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        //Modificacion realizado por Wendy Membreño
        // La modificacion consiste en que se cerraron las conexiones en todos los metodos donde se abria conexion con la BD
        public DataSet CargarTipoConceptosSel(int periodo, int semana, int IdTipo, int TipoIngDed, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@Id_Tipo", System.Data.SqlDbType.Int);
            p3.Value = IdTipo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@TipoIngDed", System.Data.SqlDbType.Int);
            p4.Value = TipoIngDed;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spConceptosTipoSel";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TipoConceptos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataSet CargarDeptos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDepartamentosEmpSel";
            cmd.Connection = sqlConnection;


            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Departamentos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataSet CargarCumpleañeros(int depto1, int depto2, string sexo, int mes, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@depto1", System.Data.SqlDbType.Int);
            p1.Value = depto1;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@depto2", System.Data.SqlDbType.Int);
            p2.Value = depto2;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@sexo", System.Data.SqlDbType.NVarChar);
            p3.Value = sexo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@Mes", System.Data.SqlDbType.Int);
            p4.Value = mes;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spCumpleañerosFotoSel";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Cumpleañeros");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet CargarLiquidacionEncabezado(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacioEncabezado";
            cmd.Connection = sqlConnection;


            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Encabezado");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataSet CargarIngresos(int idEmpresa, int filtro)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@filtro", System.Data.SqlDbType.Int);
            p1.Value = filtro;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarIngresos";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Ingresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataSet CargarDeducciones(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarDeduccionesRpt";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Deducciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet CargarPrestamos(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPrestamosSel";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Prestamos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet PeriodoDepartamentos(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDepartamentos";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Departamentos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet PlanillaConsolidadaSel(int periodo, int semana, int todo, int pagoAdelantado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@semana", SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@todo", SqlDbType.Int);
            p3.Value = todo;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@pagoAdelantado", SqlDbType.Int);
            p4.Value = pagoAdelantado;
            cmd.Parameters.Add(p4);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPlanillaResumen";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "PlanillaResumen");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }


        public DataSet PlanillaResumenPeriodoCSel(int periodo, int periodo2, int todo, int pagoAdelantado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodo1", SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@periodo2", SqlDbType.Int);
            p2.Value = periodo2;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@todo", SqlDbType.Int);
            p3.Value = todo;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@pagoAdelantado", SqlDbType.Int);
            p4.Value = pagoAdelantado;
            cmd.Parameters.Add(p4);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPlanillaResumenPeriodoC";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "PlanillaResumen");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }

        public bool PlnConversionPeriodoConsolidado(int periodo,  int periodo2, int operacion,  int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnConversionPeriodoConsolidado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo1", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

        
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo2", System.Data.SqlDbType.Int);
            p2.Value = periodo2;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@operacion", System.Data.SqlDbType.Int);
            p3.Value = operacion;
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

        public DataSet PlnObtenerConsolidadoPlanillaxPeriodo(int periodo, int periodo2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodo1", SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@periodo2", SqlDbType.Int);
            p2.Value = periodo2;
            cmd.Parameters.Add(p2);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerConsolidadoPlanillaxPeriodo";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "PlanillaResumen");
            }
            catch (Exception)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }

        public bool PlnPeriodosConsolidadosEqvIns(int periodo, int periodo2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnPeriodosConsolidadosEqvIns";
            cmd.Connection = sqlConnection;
            SqlParameter p1 = new SqlParameter("@periodo1", SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@periodo2", SqlDbType.Int);
            p2.Value = periodo2;
            cmd.Parameters.Add(p2);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                return false;
            }
            return true;
        }

        public DataTable PlnPeriodosConsolidadosEqvSel(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnPeriodosConsolidadosEqvSel";
            cmd.Connection = sqlConnection;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }


        public DataSet PlanillaConsolidadaTotalSel(int periodo, int semana,int periodo2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@periodo2", System.Data.SqlDbType.Int);
            p3.Value = periodo2;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spTotalPlanilla";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TotalPlanilla");
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }


        public DataSet spTotalPlanillaxPeriodo(int periodo, int semana, int periodo2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@semana", SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@periodo2", SqlDbType.Int);
            p3.Value = periodo2;
            cmd.Parameters.Add(p3);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spTotalPlanillaxPeriodo";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TotalPlanilla");
            }
            catch (Exception)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }
        public DataSet PlanillaSelTotal(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPlanillaTotal";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "PlanillaTotal");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet spMasterTodos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMasterTodos";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "MasterTodos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet EmpleadoActivosSel(int filtro, int ubic, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@filtro", System.Data.SqlDbType.Int);
            p1.Value = filtro;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p11.Value = ubic;
            cmd.Parameters.Add(p11);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEmpleadosActivosSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Activos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataTable plnObtenerHistoricoIRrpt(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnObtenerHistoricoIRrpt";
            cmd.Connection = sqlConnection;

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
            return ds;
        }
        public DataSet PlanillaVisionSel(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerPlanillaVision";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet EmpleadoSel(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMostrarEmpleado";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet LiquidacionSel(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacionSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Liquidacion");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet LiquidacionDetalleSel(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacionDetalleSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionDetalle");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataTable MarcasHorasT(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMarcasHorasT";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionDetalle");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataSet EmpleadoFiltrar(int Codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = Codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spFiltrarEmpleado";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet EmpleadoInsCodigo(int Codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = Codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarEmTemporal";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet PreplanillaSel(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPreplanilla";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet Semana1Sel(int periodo, int tipo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p2.Value = tipo;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSemana1";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet Semana2Sel(int periodo, int tipo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p2.Value = tipo;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSemana2";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet SemanaCaracteres(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSemanaCaracteres";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet Semana1SelEmp(int pEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = pEmp;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSemana1Emp";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "SemanaEmp");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet Semana1SelEmpIng(int pEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = pEmp;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSemana1EmpIng";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "SemanaEmpIng");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet Semana2SelEmp(int pEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = pEmp;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSemana2Emp";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Semana2Emp");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet Semana2SelEmpIng(int pEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = pEmp;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSemana2EmpIng";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Semana2EmpIng");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet CargaAtributos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spAtributosEmpleado";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet CargaCarnet(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spCarnet";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet SeleccionarDeducciones(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeducciones";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet SeleccionaConcepto(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDetalleConcepto";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet CargarDeduccionesFijas(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            //System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            //p1.Value = pEmp;
            //cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPreplanillaDedSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Fijas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet BuscarDeduccionesFijas(int pEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = pEmp;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPreplanillaDedFil";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Fijas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet PreplanillaIngSel(int periodo, int tipo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@id_Tipo", System.Data.SqlDbType.Int);
            p2.Value = tipo;
            cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            //p3.Value = tipoPlanilla;
            //cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPreplanillaIng";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet MarcasSel(int periodo, int semana, int Depto1, int Depto2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@dept", System.Data.SqlDbType.Int);
            p3.Value = Depto1;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@dept2", System.Data.SqlDbType.Int);
            p4.Value = Depto2;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMatrizMarcasPlanilla";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet CargarDatosEmp(int periodo, int tipo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p2.Value = tipo;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDatosEmpleadoSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Empleado");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet EmpleadoContratoSel(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEmpleadoContratoSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Empleado");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet IngresosSel(DateTime Inicio, DateTime Fin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@inicio", System.Data.SqlDbType.Date);
            p1.Value = Inicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fin", System.Data.SqlDbType.Date);
            p2.Value = Fin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spIngresos";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Ingresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet EgresosSel(DateTime Inicio, DateTime Fin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@inicio", System.Data.SqlDbType.Date);
            p1.Value = Inicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fin", System.Data.SqlDbType.Date);
            p2.Value = Fin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEgresosSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Ingresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet PermisosSel(DateTime Inicio, DateTime Fin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha1", System.Data.SqlDbType.Date);
            p1.Value = Inicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fecha2", System.Data.SqlDbType.Date);
            p2.Value = Fin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPermisosSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Ingresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public bool InsertarMarcasMenores(int codEmpleado, string nombre, int periodo, int semana, decimal horasT, string departemento, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarMarcasMenoresRpt";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p5.Value = nombre;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p3.Value = semana;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horasT", System.Data.SqlDbType.Decimal);
            p4.Value = horasT;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@departemento", System.Data.SqlDbType.VarChar);
            p6.Value = departemento;
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

        public DataTable obtenerMarcasMenores(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerMarcasMenores";
            cmd.Connection = sqlConnection;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "marcas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public DataSet ConceptosSel(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spConceptosSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet CargarEmpDenomicaciones(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEmpleadosDenominaciones";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Empleado");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet PlanillaSelNegativos(int periodo, int semana, int idEmpresa, int tipoPlanilla)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p3.Value = tipoPlanilla;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPlanillaTotalNegativos";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "PlanillaNegativos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet PeriodoFechas(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPeriodoFechas";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet LimpiarTemporal(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LimpiarEmpTemporal";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Eliminar");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }


        public DataSet ObtenerEstructuraComprobantePago(string periodo, int periodo2, int semana2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.NChar);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@periodo2", SqlDbType.NChar);
            p2.Value = periodo2;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@semana2", SqlDbType.Int);
            p3.Value = semana2;
            cmd.Parameters.Add(p3);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEstructuraComprobantePago";
            cmd.Connection = sqlConnection;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "comprobantepago");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }

        public DataSet ObtenerEstructuraComprobanteIncentivo(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            //p2.Value = tipoPlanilla;
            //cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEstructuraComprobanteIncentivo";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "comprobantepago");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataTable PlnIncentivoPendPagarxPeriodoSel(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            //p2.Value = tipoPlanilla;
            //cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnIncentivoPendPagarxPeriodoSel";
            cmd.Connection = sqlConnection;

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
            return ds;
        }

        public DataSet ObtenerEncComprobantePago(string periodoBase, string periodo, int codigo, int semana, int semana2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodoBase", SqlDbType.NChar);
            p1.Value = periodoBase;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@periodo", SqlDbType.NChar);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@codigo", SqlDbType.Int);
            p3.Value = codigo;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@semana", SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@semana2", SqlDbType.Int);
            p5.Value = semana2;
            cmd.Parameters.Add(p5);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEncComprobantePago";
            cmd.Connection = sqlConnection;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Enccomprobantepago");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }

        public DataSet ObtenerEncComprobanteIncentivo(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEncComprobanteIncentivo";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Enccomprobantepago");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet ObtenerEncComprobantePrestacion(int periodo, int codigo, int tperiodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
            p12.Value = tperiodo;
            cmd.Parameters.Add(p12);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spObtenerEncabezadoPrestacion";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Enccomprobantepago");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        //filtro

        public DataSet ObtenerEmpleadosPlanilla(string periodo, string periodo2, DateTime fechaini, DateTime fechafin, string depto, string codigo, bool all, bool efectivo, int idEmpresa, int filtroemail, bool periodoConsolida)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            DataSet ds = new DataSet();
            string query = "select distinct b.nombre_ubicacion empresa,@periodo periodo,@fechaini fechaini,@fechafin fechafin,cast(p.codigo_empleado as nvarchar(40)) codigo,p.nombre nombre,e.cuentabancaria cuenta,substring(d1.nombre_depto,1,20) depto,e.numero_seguro,e.cedula_identidad,c.nombre_cargo,e.fecha_ingreso,e.email,p.codigo_empleado  from plnPlanillas p inner join plnempleados e on e.codigo_empleado=p.codigo_empleado inner join plndepartamentos d1 on d1.codigo_depto=e.codigo_depto inner join plnubicaciones b on e.codigo_ubicacion=b.codigo_ubicacion inner join plncargos c on c.codigo_cargo=e.codigo_cargo ";
            query = ((!periodoConsolida) ? (query + " where p.periodo=@periodo ") : (query + " where p.periodo in (@periodo,@periodo2) "));
            string condicion = " ";
            string estado = " and (e.idEstado=1 or e.idEstado=3)";
            int bandera = 0;
            if (filtroemail == 3)
            {
                condicion += " and (e.email!='' and e.email is not null)";
            }
            else
            {
                if (filtroemail == 2)
                {
                    condicion += " and (e.email='' or e.email is null)";
                }
                if (efectivo)
                {
                    condicion += " and (e.cuentabancaria='0' or e.cuentabancaria='' or e.cuentabancaria is null)";
                }
                else if (!all)
                {
                    if (!string.IsNullOrEmpty(codigo))
                    {
                        condicion = condicion + ((!string.IsNullOrEmpty(condicion)) ? " AND " : "") + "  p.codigo_empleado=@codigo";
                        bandera = 1;
                    }
                    else
                    {
                        condicion += " and d1.codigo_depto=@depto";
                    }
                }
                else
                {
                    condicion += " and (e.cuentabancaria!='0' and e.cuentabancaria!='' and e.cuentabancaria is not null)";
                }
            }
            if (bandera == 0)
            {
                condicion += estado;
            }
            condicion += " group by b.nombre_ubicacion,p.codigo_empleado,p.nombre,e.cuentabancaria,d1.nombre_depto,e.numero_seguro,e.cedula_identidad,c.nombre_cargo,e.fecha_ingreso,e.email order by depto,codigo";
            query += condicion;
            SqlCommand cmd = new SqlCommand(query, con.GetConecction());
            SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.VarChar);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@periodo2", SqlDbType.VarChar);
            p2.Value = periodo2;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@fechaini", SqlDbType.VarChar);
            p3.Value = fechaini;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@fechafin", SqlDbType.VarChar);
            p4.Value = fechafin;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@depto", SqlDbType.VarChar);
            p5.Value = depto;
            cmd.Parameters.Add(p5);
            SqlParameter p6 = new SqlParameter("@codigo", SqlDbType.VarChar);
            p6.Value = codigo;
            cmd.Parameters.Add(p6);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empln");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }
        public System.Data.DataSet ObtenerEmpleadosPlanillaIncentivo(string periodo, string depto, string codigo, bool all, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.DataSet ds = new DataSet();
            string query = "select distinct b.nombre_ubicacion empresa,p.periodo,min(pd.fechaini)fechaini,max(pd.fechafin2)fechafin,cast(p.codigo_empleado as nvarchar(40)) codigo_empleado,p.nombrecompleto nombre,"+
            "substring(d1.nombre_depto, 1, 20) depto" +
            " from PlnPagoIncentivoEmpleado p inner join plnempleados e on e.codigo_empleado=p.codigo_empleado inner join plndepartamentos d1 on d1.codigo_depto=e.codigo_depto inner join plnubicaciones b on e.codigo_ubicacion=b.codigo_ubicacion" +
            " inner join plnperiodos pd on p.periodo=pd.nperiodo"+
            " where p.periodo=@periodo ";//and p.tplanilla=" + tipoPlanilla + "";
            string condicion = " ";
            string estado = " and (e.idEstado=1 or e.idEstado=3)";
            int bandera = 0;
            //se construye la cadena con condiciones

            if (!all)
            {
                if (!string.IsNullOrEmpty(codigo))
                {
                    condicion += ((!string.IsNullOrEmpty(condicion)) ? " AND " : "") + "  p.codigo_empleado=@codigo";
                    bandera = 1;
                }
                else
                {
                    condicion += " and d1.codigo_depto=@depto";
                }
            }
            if (bandera == 0)
            {
                condicion += estado;
            }
            condicion += " group by b.nombre_ubicacion,p.periodo,p.codigo_empleado,p.nombrecompleto,d1.nombre_depto order by depto,codigo_empleado";
            query += condicion;

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand(query, con.GetConecction());

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.VarChar);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@depto", System.Data.SqlDbType.VarChar);
            p2.Value = depto;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo", System.Data.SqlDbType.VarChar);
            p3.Value = codigo;
            cmd.Parameters.Add(p3);


            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empln");
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet spMesesLiquidacion(int codigo,DateTime fechaliquidacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.NChar);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaegreso", System.Data.SqlDbType.Date);
            p11.Value = fechaliquidacion;
            cmd.Parameters.Add(p11);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMesesLiquidacion";
            cmd.Connection = sqlConnection;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Meses");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet spLiquidacionDetallado(int codigo,DateTime fechaliquidacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.NChar);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaegreso", System.Data.SqlDbType.Date);
            p11.Value = fechaliquidacion;
            cmd.Parameters.Add(p11);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacionDetallado";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "DetalleLiquidacion");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet spLiquidacionDetalladoSel(DateTime Inicio, DateTime Fin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@Inicio", System.Data.SqlDbType.DateTime);
            p1.Value = Inicio;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@Fin", System.Data.SqlDbType.DateTime);
            p2.Value = Fin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacionesSeleccionar";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "DetalleLiquidacion");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }


        public DataSet spTipoPermisoSel(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spTipoPermisoSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TipoPermiso");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet PermisosSel(DateTime Inicio, DateTime Fin, int tipo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha1", System.Data.SqlDbType.Date);
            p1.Value = Inicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fecha2", System.Data.SqlDbType.Date);
            p2.Value = Fin;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p3.Value = tipo;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPermisosSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Ingresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet generarInformePersonasSinMarcaEntrada(DateTime fecha, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p1.Value = fecha;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "generarInformePersonasSinMarcaEntrada";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Ingresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet MarcasAusentesSel(DateTime inicio, int Depto1, int Depto2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@inicio", System.Data.SqlDbType.Date);
            p1.Value = inicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@dept", System.Data.SqlDbType.Int);
            p3.Value = Depto1;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@dept2", System.Data.SqlDbType.Int);
            p4.Value = Depto2;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMarcasAusentesSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Reportes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet spLiquidacionPendiente(int codigo,DateTime fechaliquidacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.NChar);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaegreso", System.Data.SqlDbType.Date);
            p11.Value = fechaliquidacion;
            cmd.Parameters.Add(p11);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacionPendientes";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionPendientes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet spLiquidacionPendientesDeduc(int codigo,DateTime fechaliquidacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.NChar);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaegreso", System.Data.SqlDbType.Date);
            p11.Value = fechaliquidacion;
            cmd.Parameters.Add(p11);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacionPendientesDeduc";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionPendientesDeduc");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet plnDeduccionesPendPagoLiqSel(int codigo, DateTime fechaliquidacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.NChar);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaegreso", System.Data.SqlDbType.Date);
            p11.Value = fechaliquidacion;
            cmd.Parameters.Add(p11);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnDeduccionesPendPagoLiqSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionPendientesDeduc");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet CargarFotoDepto(int depto1, int depto2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@depto1", System.Data.SqlDbType.Int);
            p1.Value = depto1;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@depto2", System.Data.SqlDbType.Int);
            p2.Value = depto2;
            cmd.Parameters.Add(p2);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEmpFotoDeptoSel";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "FotoEmpleado");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet spSaldoVacacionesSel(int depto1, int depto2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@depto1", System.Data.SqlDbType.Int);
            p1.Value = depto1;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@depto2", System.Data.SqlDbType.Int);
            p2.Value = depto2;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spSaldoVacacionesSel";
            cmd.Connection = con.GetConecction();
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Vacaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public bool spObtenerMarcasBioAdmin_BDRRHH(DateTime Fecha, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spObtenerMarcasBioAdmin_BDRRHH";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter pFecha = new SqlParameter("@Fecha", System.Data.SqlDbType.Date);
            pFecha.Value = Fecha;
            cmd.Parameters.Add(pFecha);

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
        public DataSet spListarMarcas(DateTime Inicio, DateTime Fin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime);
            p1.Value = Inicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime);
            p2.Value = Fin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spListarMarcas";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ListarMarcas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet obtenerPlanillaVacaciones(int periodo, int codigo, string fecini, string fecfin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p11.Value = codigo;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@fecini", System.Data.SqlDbType.NChar);
            p12.Value = fecini;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@fecfin", System.Data.SqlDbType.NChar);
            p13.Value = fecfin;
            cmd.Parameters.Add(p13);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerPlanillaVacaciones";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "vacaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataTable obtenerPlanillaAguinaldo(int periodo,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
          

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerPlanillaAguinaldo";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


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
            return ds;
        }
        public DataTable ObtenerDetalleIngresoMesAguinaldo(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDetalleIngresoMesAguinaldo";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


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
            return ds;
        }
        public DataSet PrestamosConsultarDetallexEmp(int codigo_empleado, int idEmpresa, int iddeduc, int mostrarcuotas, int pgpend)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p2.Value = codigo_empleado;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idDeduccion", System.Data.SqlDbType.Int);
            p3.Value = iddeduc;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@MostrarCuotas", System.Data.SqlDbType.Int);
            p4.Value = mostrarcuotas;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@pgpend", System.Data.SqlDbType.Int);
            p41.Value = pgpend;
            cmd.Parameters.Add(p41);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PrestamosConsultarDetallexEmp ";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Detalle_Prestamos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet spEmpleadosActivosxDepto(int depto1, int depto2, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@depto1", System.Data.SqlDbType.Int);
            p1.Value = depto1;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@depto2", System.Data.SqlDbType.Int);
            p2.Value = depto2;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEmpleadosActivosxDepto";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ActivosxDepto");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet spEmpleadosActivosHistoricoSel(DateTime fechacorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechacorte", System.Data.SqlDbType.Date);
            p1.Value = fechacorte;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEmpleadosActivosHistoricoSel";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ActivosxDepto");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet ObtenerEmpIndemnizacionMes(int mes, int anio, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@mes", System.Data.SqlDbType.Int);
            p1.Value = mes;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@anio", System.Data.SqlDbType.Int);
            p11.Value = anio;
            cmd.Parameters.Add(p11);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEmpIndemnizacionMes";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empindem");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet ObtenerEmpLiqAntiguedad( int anio, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@anio", System.Data.SqlDbType.Int);
            p11.Value = anio;
            cmd.Parameters.Add(p11);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEmpLiqAntiguedad";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empindem");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet PlnObtenerEmpleadosPlanillaMes(DateTime fechacorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            //System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@mes", System.Data.SqlDbType.Int);
            //p1.Value = mes;
            //cmd.Parameters.Add(p1);
            //System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@anio", System.Data.SqlDbType.Int);
            //p11.Value = anio;
            //cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@fechacorte", System.Data.SqlDbType.Date);
            p12.Value = fechacorte;
            cmd.Parameters.Add(p12);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerEmpleadosPlanillaMes";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "empindem");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet ObtenerEmpleadosEstatusEspecial(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEmpleadosEstatusEspecial";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "emp");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet BDRRHHDevolventes(int periodo, int idEmpresa, int deduccion)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@deduccion", System.Data.SqlDbType.Int);
            p3.Value = deduccion;
            cmd.Parameters.Add(p3);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BDRRHHDevolventes ";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Devolventes");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet spDeducEspecialesSel(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spDeducEspecialesSel";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "DeducEspeciales");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet PrestamosConsultarDetalleCxEmp(int codigo_empleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p2.Value = codigo_empleado;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PrestamosConsultarDetalleCxEmp ";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Detalle_Prestamos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataTable spMarcasSelxFecha(DateTime Inicio, DateTime Fin, int filtro, int ubic, int dpto, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@inicio", System.Data.SqlDbType.Date);
            p1.Value = Inicio;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fin", System.Data.SqlDbType.Date);
            p2.Value = Fin;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@filtro", System.Data.SqlDbType.Int);
            p3.Value = filtro;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p4.Value = ubic;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@departamento", System.Data.SqlDbType.Int);
            p5.Value = dpto;
            cmd.Parameters.Add(p5);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spMarcasSelxFecha";
            cmd.Connection = sqlConnection;

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
            return ds;
        }
        public DataTable MasterPComedor(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MasterPComedor";
            cmd.Connection = sqlConnection;

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
            return ds;
        }

        public DataSet AdelantosEspecialesDetalle(int codigo_empleado, int idEmpresa, int iddeduc)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo_empleado;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@deduccion", System.Data.SqlDbType.Int);
            p3.Value = iddeduc;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AdelantosEspecialesDetalle ";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Detalle_Adl");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataTable DetalleSolvenciaxE(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DetalleSolvenciaxE";
            cmd.Connection = sqlConnection;

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
            return ds;
        }

        public DataSet CargarIngresoPeriodoIBruto(int periodo, int periodo2, int tipoing, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            SqlParameter p3 = new SqlParameter("@periodo2", SqlDbType.Int);
            p3.Value = periodo2;
            cmd.Parameters.Add(p3);
            SqlParameter p2 = new SqlParameter("@tipoing", SqlDbType.Int);
            p2.Value = tipoing;
            cmd.Parameters.Add(p2);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spResumenPeriodoIngresosIBruto";
            cmd.Connection = sqlConnection;
            cmd.CommandTimeout = 0;
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "TipoConceptos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
            }
            return ds;
        }

    }
}
