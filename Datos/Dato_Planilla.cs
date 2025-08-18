using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Planilla
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        //public DataTable SeleccionarPeriodoCat(int tperiodo, int idEmpresa)
        //{
        //    SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
        //    DataSet ds = new DataSet();
        //    CnBD con = new CnBD();
        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

        //    System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
        //    p1.Value = tperiodo;
        //    cmd.Parameters.Add(p1);

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "seleccionarUltPeriodoAbiertoCatorcenal";
        //    cmd.Connection = sqlConnection;


        //    object obj;

        //    try
        //    {
               
        //        System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds, "planilla");
        //    }
        //    catch (SystemException)
        //    {
        //        if (cmd.Connection.State != ConnectionState.Closed)
        //            cmd.Connection.Close();
        //    }

        //    return ds.Tables[0];
        //}

        public bool InsertarIngrDeduc(int tipo, int codEmpleado, int nSemana, int idTipo, int periodo, decimal valor, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosYDeduccionesIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p2.Value = codEmpleado;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@nSemana", System.Data.SqlDbType.Int);
            p3.Value = nSemana;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@idTipoIngDed", System.Data.SqlDbType.Int);
            p4.Value = idTipo;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p5.Value = periodo;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p6.Value = valor;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p7.Value = user;
            cmd.Parameters.Add(p7);

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

        public bool insertarHorasExtras(int codEmpleado, int periodo, int semana, int tipo,decimal horas, int idEmpresa, int tplanilla)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarHorasExtras";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p3.Value = semana;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p13.Value = tipo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horas", System.Data.SqlDbType.Decimal);
            p4.Value = horas;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@tplanilla", System.Data.SqlDbType.Int);
            p5.Value = tplanilla;
            cmd.Parameters.Add(p5);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }
        public bool insertarHorasExtrasxDia(int codEmpleado, int periodo,int tipoIngrDeduc, DateTime fecha, decimal horas,decimal tiempolibre, int depto_afecta, string usuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarHorasExtrasxDia";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@tipoIngrDeduc", System.Data.SqlDbType.Int);
            p21.Value = tipoIngrDeduc;
            cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horas", System.Data.SqlDbType.Decimal);
            p4.Value = horas;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p5.Value = fecha;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@tiempo_libre", System.Data.SqlDbType.Decimal);
            p10.Value = tiempolibre;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@depto_afecta", System.Data.SqlDbType.Int);
            p11.Value = depto_afecta;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@usuario", System.Data.SqlDbType.NChar);
            p6.Value = usuario;
            cmd.Parameters.Add(p6);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }
        public bool insertarIngDeducxDia(int codEmpleado, int periodo,int idtipo,int tipoingdeduc, DateTime fecha, decimal horas,decimal valor,decimal tiempolibre,int depto_afecta, string usuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarIngDeducxDia";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idtipo", System.Data.SqlDbType.Int);
            p3.Value = idtipo;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@tipoingdeduc", System.Data.SqlDbType.Int);
            p7.Value = tipoingdeduc;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@horas", System.Data.SqlDbType.Decimal);
            p8.Value = horas;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p9.Value = valor;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@tiempo_libre", System.Data.SqlDbType.Decimal);
            p10.Value = tiempolibre;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@depto_afecta", System.Data.SqlDbType.Int);
            p11.Value = depto_afecta;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p5.Value = fecha;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@usuario", System.Data.SqlDbType.NChar);
            p6.Value = usuario;
            cmd.Parameters.Add(p6);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }
        public bool EliminarIngDeducxDia(int codEmpleado, int periodo, int idtipo, int tipoingdeduc, DateTime fecha, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarIngDeducxDia";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idtipo", System.Data.SqlDbType.Int);
            p3.Value = idtipo;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@tipoingdeduc", System.Data.SqlDbType.Int);
            p7.Value = tipoingdeduc;
            cmd.Parameters.Add(p7);          

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p5.Value = fecha;
            cmd.Parameters.Add(p5);           

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }
        public bool ActualizarIngDeducxDia(int codEmpleado, int periodo, int idtipo, int tipoingdeduc, DateTime fecha,string comentario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarIngDeducxDia";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p22.Value = periodo;
            cmd.Parameters.Add(p22);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idtipo", System.Data.SqlDbType.Int);
            p3.Value = idtipo;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@tipoingdeduc", System.Data.SqlDbType.Int);
            p7.Value = tipoingdeduc;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p5.Value = fecha;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@comentario", System.Data.SqlDbType.NVarChar);
            p2.Value = comentario;
            cmd.Parameters.Add(p2);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }
        public bool AplicarIngDeducxDia(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AplicarIngDeducxDia";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p22.Value = periodo;
            cmd.Parameters.Add(p22);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }
        public bool insertarHorasExtrasIndividuales(string nombre, int periodo, int semana, decimal horas, int idEmpresa, int tPlanilla)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarHorasExtrasIndividuales";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p1.Value = nombre;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p3.Value = semana;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horas", System.Data.SqlDbType.Decimal);
            p4.Value = horas;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p5.Value = tPlanilla;
            cmd.Parameters.Add(p5);

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
        public DataTable ObtenerHTEmpleadosActivosXPeriodo(int periodo, int semana, int ubicacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.VarChar);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p3.Value = ubicacion;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SeleccionarHTEmpleadosActivosXPeriodo";
            cmd.Connection = sqlConnection;
            
            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "planilla");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable ObtenerDevengadosASumar(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet di = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.VarChar);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDevengadosASumar";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            try
            {
               
                da.Fill(di, "devengados");
             
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return di.Tables[0];
        }

        public DataTable ObtenerDeduccionesARestar(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.VarChar);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDeduccionesARestar";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
           
            try
            {
               
                da.Fill(dd, "deducciones");
                return dd.Tables[0];
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return dd.Tables[0];
            }
        }
        public DataTable ObtenerDeduccionesARestarPrestaciones(int periodo,int tprestacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@tprestacion", System.Data.SqlDbType.Int);
            p11.Value = tprestacion;
            cmd.Parameters.Add(p11);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDeduccionesARestarPrestaciones";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {               
                da.Fill(dd, "deducciones");
                return dd.Tables[0];
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return dd.Tables[0];
            }
        }
       
        public DataTable ObtenerPermisosPeriodo(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPermisosPeriodo";
            cmd.Connection = sqlConnection;
            
            try
            {              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "deducciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }

        public DataTable ObtenerDeduccionesOrdinarias(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerDeduccionesOrdinarias";
            cmd.Connection = sqlConnection;
           
            try
            {              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "deducciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }

        public bool IngTotal(int tipo, int codigo, DateTime fechaI, DateTime fechaF,
            int messemana, int nSemana, string nombre, decimal salMensual, decimal hT, decimal tIngrs,
            decimal inss, decimal salNeto, decimal valorIncentivo, int periodo, int semana,
            decimal inssPatronal, decimal inatec, decimal vacAcum, string depto, int tPlanilla,
            string user, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosTotalesIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fechaI", System.Data.SqlDbType.Date);
            p3.Value = fechaI;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaF", System.Data.SqlDbType.Date);
            p4.Value = fechaF;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@mesSemana", System.Data.SqlDbType.Int);
            p19.Value = messemana;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@nSemana", System.Data.SqlDbType.Int);
            p5.Value = nSemana;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p6.Value = nombre;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@salMensual", System.Data.SqlDbType.Decimal);
            p7.Value = salMensual;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@hT", System.Data.SqlDbType.Decimal);
            p8.Value = hT;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@totalCatorcena", System.Data.SqlDbType.Decimal);
            p9.Value = tIngrs;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@inss", System.Data.SqlDbType.Decimal);
            p10.Value = inss;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@salNeto", System.Data.SqlDbType.Decimal);
            p11.Value = salNeto;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@valorIncentivo", System.Data.SqlDbType.Decimal);
            p12.Value = valorIncentivo;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p14.Value = semana;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@inssPatronal", System.Data.SqlDbType.Decimal);
            p16.Value = inssPatronal;
            cmd.Parameters.Add(p16);

            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@inatec", System.Data.SqlDbType.Decimal);
            p17.Value = inatec;
            cmd.Parameters.Add(p17);

            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@vacAcumulada", System.Data.SqlDbType.Decimal);
            p18.Value = vacAcum;
            cmd.Parameters.Add(p18);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@depto", System.Data.SqlDbType.VarChar);
            p20.Value = depto;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p21.Value = tPlanilla;
            cmd.Parameters.Add(p21);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p22.Value = user;
            cmd.Parameters.Add(p22);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p23.Value = tipoPlanilla;
            cmd.Parameters.Add(p23);
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

        public bool IngParcial(int tipo, int codigo, DateTime fechaI, DateTime fechaF,
         int nSemana, string nombre, decimal salMensual, decimal hT, decimal tIngrs,
           decimal inss, decimal salNeto, decimal valorIncentivo, int periodo, int semana,
           decimal inssPatronal, decimal inatec, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosParcialesIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fechaI", System.Data.SqlDbType.Date);
            p3.Value = fechaI;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaF", System.Data.SqlDbType.Date);
            p4.Value = fechaF;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@nSemana", System.Data.SqlDbType.Int);
            p5.Value = nSemana;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p6.Value = nombre;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@salMensual", System.Data.SqlDbType.Decimal);
            p7.Value = salMensual;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@hT", System.Data.SqlDbType.Decimal);
            p8.Value = hT;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@totalCatorcena", System.Data.SqlDbType.Decimal);
            p9.Value = tIngrs;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@inss", System.Data.SqlDbType.Decimal);
            p10.Value = inss;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@salNeto", System.Data.SqlDbType.Decimal);
            p11.Value = salNeto;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@valorIncentivo", System.Data.SqlDbType.Decimal);
            p12.Value = valorIncentivo;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p14.Value = semana;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@inssPatronal", System.Data.SqlDbType.Decimal);
            p16.Value = inssPatronal;
            cmd.Parameters.Add(p16);

            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@inatec", System.Data.SqlDbType.Decimal);
            p17.Value = inatec;
            cmd.Parameters.Add(p17);


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

        public DataSet obtenerDetalleDeduccionesPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa, int tipoPlanilla)
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
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p4.Value = tipoPlanilla;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "obtenerDetalleSemanaEmpleado";
            cmd.CommandText = "obtenerDetalleDeducciones";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "detalle");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        public DataSet obtenerDetalleIngresosPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa, int tipoPlanilla)
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
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p4.Value = tipoPlanilla;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerDetalleIngresos";
            cmd.Connection = sqlConnection;
            

            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "detalle");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet obtenerDetalleMarcas(int periodo, int semana, int CodEmp, int idEmpresa)
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
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@CodEmp", System.Data.SqlDbType.VarChar);
            p3.Value = CodEmp;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerDetalleSemanaEmpleado";
            //cmd.CommandText = "obtenerDetalleDeducciones";
            cmd.Connection = sqlConnection;
           
            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "detalle");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public bool AgregarVacaciones(int codigo, int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "calculoProcesoVacaciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
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

        public DataTable validarExistePlanillaPeriodo(int periodo, int semana, int idEmpresa)
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
            cmd.CommandText = "validarProcesoPlanillaExiste";
            cmd.Connection = sqlConnection;
           
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "proceso");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public bool EliminarPlanilla(int nperiodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarPlanillaPeriodo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p1.Value = nperiodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
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
        public bool EliminarPlanilla(int nperiodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarPlanilla";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p1.Value = nperiodo;
            cmd.Parameters.Add(p1);
            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            //p2.Value = semana;
            //cmd.Parameters.Add(p2);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }
        public bool EliminarHistoricoAguinaldo(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEliminarHistoricoAguinaldo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
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
        public bool EliminarHistoricoVacaciones(int periodo,int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEliminarHistoricoVacaciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
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
        public bool EliminarAguinaldoMeses(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEliminarAguinaldoMeses";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
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
        public bool EliminarDeduccionesAguinaldo(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEliminarDeduccionesAguinaldo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
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
        public bool EliminarPlanillaPorEmpleado(int nperiodo, int semana, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarPlanillaPeriodoPorEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p1.Value = nperiodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
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

        public DataTable ObtenerPlanillaActual(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPlanillaActual";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "planilla");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }
        public DataTable ObtenerDenominaciones( int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();          

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDenominaciones";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "deno");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }

        public bool aplicarDeducciones(int nperiodo, int semana, int codEmpleado, decimal deduccion, int idDeduc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "aplicarDeducciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p1.Value = nperiodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@deduccion", System.Data.SqlDbType.Decimal);
            p5.Value = deduccion;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@idDeduc", System.Data.SqlDbType.Int);
            p6.Value = idDeduc;
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

        public bool distribuirDenominacionesMoneda(int d500, int d200, int d100, int d50, int d20, int d10,
            int d5, int d1, int d05, int d025, int d010, int d005, int d001, int codEmpleado, int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "distribuirDenominacionesMoneda";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@d500", System.Data.SqlDbType.Int);
            p1.Value = d500;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@d200", System.Data.SqlDbType.Int);
            p2.Value = d200;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@d100", System.Data.SqlDbType.Int);
            p3.Value = d100;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@d50", System.Data.SqlDbType.Int);
            p4.Value = d50;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@d20", System.Data.SqlDbType.Int);
            p5.Value = d20;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@d10", System.Data.SqlDbType.Int);
            p6.Value = d10;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@d5", System.Data.SqlDbType.Int);
            p7.Value = d5;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@d1", System.Data.SqlDbType.Int);
            p8.Value = d1;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@d05", System.Data.SqlDbType.Int);
            p9.Value = d05;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@d025", System.Data.SqlDbType.Int);
            p10.Value = d025;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@d010", System.Data.SqlDbType.Int);
            p11.Value = d010;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@d005", System.Data.SqlDbType.Int);
            p12.Value = d005;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@d001", System.Data.SqlDbType.Int);
            p13.Value = d001;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p14.Value = codEmpleado;
            cmd.Parameters.Add(p14);
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p15.Value = periodo;
            cmd.Parameters.Add(p15);
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p16.Value = semana;
            cmd.Parameters.Add(p16);

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

        public DataTable procesarDeduccionLlegadasTarde(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procesarDeduccionLlegadasTarde";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "llegadasT");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }

        public bool insertarDeduccionesLLegT(int tipoDeduc, int codEmpleado, int semana, int tipoIngDed,
            int periodo, decimal totalDeducir, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarDeduccionesLLegT";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipoDeduc", System.Data.SqlDbType.Int);
            p1.Value = tipoDeduc;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p2.Value = codEmpleado;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p3.Value = semana;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipoIngDed", System.Data.SqlDbType.Int);
            p4.Value = tipoIngDed;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p5.Value = periodo;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@totalDeducir", System.Data.SqlDbType.Decimal);
            p6.Value = totalDeducir;
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

        public DataTable EmpleadosSaldoNegativo(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmpleadosSaldoNegativo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.Connection = sqlConnection;
           
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "negativo");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }

        public DataTable obtenerDatosEmpleadosNeg(int periodo, int semana, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SeleccionarEmpleadosSaldNegtvo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = @codEmpleado;
            cmd.Parameters.Add(p3);

            cmd.Connection = sqlConnection;
           
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "negativo");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }

        public bool ActualizarCalculoEmplNeg(int tipo, int codEmpleado, int periodo, int semana, decimal HT, decimal inss, decimal inssPatr, decimal inatec,
             decimal tIngrs, decimal salNeto, decimal incentivo, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarCalculoEmplNeg";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p2.Value = codEmpleado;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p3.Value = periodo;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@HT", System.Data.SqlDbType.Decimal);
            p5.Value = HT;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@inss", System.Data.SqlDbType.Decimal);
            p6.Value = inss;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@inssPatr", System.Data.SqlDbType.Decimal);
            p7.Value = inssPatr;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@inatec", System.Data.SqlDbType.Decimal);
            p8.Value = inatec;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@tIngrs", System.Data.SqlDbType.Decimal);
            p9.Value = tIngrs;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@salNeto", System.Data.SqlDbType.Decimal);
            p10.Value = salNeto;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@incentivo", System.Data.SqlDbType.Decimal);
            p11.Value = incentivo;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p12.Value = user;
            cmd.Parameters.Add(p12);

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

        public DataTable ProcesarPlanillaPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa)
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

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDetalleEmpleadoPorPeriodo";
            cmd.Connection = sqlConnection;
            
            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "planilla");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public DataTable ObtenerDevengadosASumarPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet di = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDevengadosASumarPorEmpleado";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(di, "devengados");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return di.Tables[0];
        }

        public DataTable ObtenerPermisosPeriodoPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPermisosPeriodoPorEmpleado";
            cmd.Connection = sqlConnection;
           
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }

        public bool ProcesarCalculoPlanillaPorEmpleado(int tipo, int codEmpleado, int periodo, int semana, 
            decimal HT, decimal inss, decimal inssPatr, decimal inatec,decimal tIngrs, decimal salNeto, decimal incentivo,
            DateTime fechaI, DateTime fechaF, int messemana, string nombre, decimal salMensual, string depto, string user,
            int idEmpresa, int tipoPlanilla)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ProcesarCalculoPlanillaPorEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p2.Value = codEmpleado;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p3.Value = periodo;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@HT", System.Data.SqlDbType.Decimal);
            p5.Value = HT;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@inss", System.Data.SqlDbType.Decimal);
            p6.Value = inss;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@inssPatr", System.Data.SqlDbType.Decimal);
            p7.Value = inssPatr;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@inatec", System.Data.SqlDbType.Decimal);
            p8.Value = inatec;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@tIngrs", System.Data.SqlDbType.Decimal);
            p9.Value = tIngrs;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@salNeto", System.Data.SqlDbType.Decimal);
            p10.Value = salNeto;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@incentivo", System.Data.SqlDbType.Decimal);
            p11.Value = incentivo;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@fechaI", System.Data.SqlDbType.Date);
            p12.Value = fechaI;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@fechaF", System.Data.SqlDbType.Date);
            p13.Value = fechaF;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@mesSemana", System.Data.SqlDbType.Int);
            p14.Value = messemana;
            cmd.Parameters.Add(p14);
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p15.Value = nombre;
            cmd.Parameters.Add(p15);
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@salMensual", System.Data.SqlDbType.Decimal);
            p16.Value = salMensual;
            cmd.Parameters.Add(p16);
            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@depto", System.Data.SqlDbType.VarChar);
            p17.Value = depto;
            cmd.Parameters.Add(p17);
            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p18.Value = user;
            cmd.Parameters.Add(p18);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p19.Value = tipoPlanilla;
            cmd.Parameters.Add(p19);
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

        public DataTable ObtenerPlanillaActualPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPlanillaActualPorEmpleado";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "planilla");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }

        public DataTable ObtenerDeduccionesARestarPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDeduccionesARestarPorEmpleado";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }
        public bool InsertarMarcaPorEmpleado(int codEmpl, DateTime fecha, string horaE, string horaS, int idEmpresa, string user)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertarMarcaPorEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpl", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p2.Value = fecha;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@horaE", System.Data.SqlDbType.VarChar);
            p3.Value = horaE;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horaS", System.Data.SqlDbType.VarChar);
            p4.Value = horaS;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p5.Value = user;
            cmd.Parameters.Add(p5);

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


        //public bool InsertarMarcaPorEmpleado(int codEmpl, DateTime fecha, string horaE, string horaS, int idEmpresa)
        //{
        //    SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
        //    CnBD con = new CnBD();
        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "InsertarMarcaPorEmpleado";
        //    cmd.Connection = sqlConnection;

        //    System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpl", System.Data.SqlDbType.Int);
        //    p1.Value = codEmpl;
        //    cmd.Parameters.Add(p1);
        //    System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
        //    p2.Value = fecha;
        //    cmd.Parameters.Add(p2);
        //    System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@horaE", System.Data.SqlDbType.VarChar);
        //    p3.Value = horaE;
        //    cmd.Parameters.Add(p3);
        //    System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horaS", System.Data.SqlDbType.VarChar);
        //    p4.Value = horaS;
        //    cmd.Parameters.Add(p4);

        //    try
        //    {
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (cmd.Connection.State != ConnectionState.Closed)
        //            cmd.Connection.Close();
        //        return false;
        //    }
        //    return true;
        //}


        public DataTable obtenerPlanillaPorEmpleado(int periodo, int semana, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "detallePllanillaEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            cmd.Connection = sqlConnection;
          
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "negativo");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }

        public bool EditarPlanillaPorEmpleado(decimal horasT, decimal salario, decimal hE, decimal valorHe,
            decimal inss, decimal iR, decimal totIngresos, decimal totEgresos, decimal Neto, int periodo, int semana, int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarPlanillaPorEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@horasT", System.Data.SqlDbType.Decimal);
            p1.Value = horasT;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@salario", System.Data.SqlDbType.Decimal);
            p2.Value = salario;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@hE", System.Data.SqlDbType.Decimal);
            p3.Value = hE;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@valorHe", System.Data.SqlDbType.Decimal);
            p4.Value = valorHe;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@inss", System.Data.SqlDbType.Decimal);
            p5.Value = inss;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@iR", System.Data.SqlDbType.Decimal);
            p6.Value = iR;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@totIngresos", System.Data.SqlDbType.Decimal);
            p7.Value = totIngresos;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@totEgresos", System.Data.SqlDbType.Decimal);
            p8.Value = totEgresos;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@Neto", System.Data.SqlDbType.Decimal);
            p9.Value = Neto;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p10.Value = periodo;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p11.Value = semana;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p12.Value = codigo;
            cmd.Parameters.Add(p12);

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

        public DataSet cargarTiposPlanilla(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarTiposPlanilla";
            cmd.Connection = sqlConnection;
            
            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "planillas");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        // obtener empleados que deben IR
        public DataTable obtenerEmpleadosDebenImpuesto(int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.VarChar);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerEmpleadosDebenImpuesto";
            cmd.Connection = sqlConnection;
           
            try
            {
             
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }

        public bool calcularIRaDeber(int periodo, int semana, int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "calcularIRaDeber";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p3.Value = codigo;
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
        //PLANILLA QUINCENAL
        public DataTable obtenerEmpleadosPlanillaQuincenal(int periodo, int tipoPlanilla, int ubicacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p2.Value = tipoPlanilla;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p3.Value = ubicacion;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerEmpleadosPlanillaQuincenal";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "planilla");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public DataTable obtenerIngresosQuincenalesASumar(int periodo, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet di = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p2.Value = tipoPlanilla;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerIngresosQuincenales";
            cmd.Connection = sqlConnection;
            
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(di, "devengados");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return di.Tables[0];
        }

        public bool insertarCalculosPlanillaQuincenal(int tipo, int codigo, DateTime fechaI, DateTime fechaF,
           int mesplanilla, string nombre, decimal salMensual, decimal hT, decimal quincenaPago,
           decimal inss, decimal netoQuincena, decimal valorIncentivo, int periodo, int semana,
           decimal inssPatronal, decimal inatec, string depto, int tPlanilla, string user, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarCalculosPlanillaQuincenal";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fechaI", System.Data.SqlDbType.Date);
            p3.Value = fechaI;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaF", System.Data.SqlDbType.Date);
            p4.Value = fechaF;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@mesplanilla", System.Data.SqlDbType.Int);
            p19.Value = mesplanilla;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p6.Value = nombre;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@salMensual", System.Data.SqlDbType.Decimal);
            p7.Value = salMensual;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@hT", System.Data.SqlDbType.Decimal);
            p8.Value = hT;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@quincenaPago", System.Data.SqlDbType.Decimal);
            p9.Value = quincenaPago;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@inss", System.Data.SqlDbType.Decimal);
            p10.Value = inss;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@netoQuincena", System.Data.SqlDbType.Decimal);
            p11.Value = netoQuincena;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@valorIncentivo", System.Data.SqlDbType.Decimal);
            p12.Value = valorIncentivo;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p14.Value = semana;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@inssPatronal", System.Data.SqlDbType.Decimal);
            p16.Value = inssPatronal;
            cmd.Parameters.Add(p16);

            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@inatec", System.Data.SqlDbType.Decimal);
            p17.Value = inatec;
            cmd.Parameters.Add(p17);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@depto", System.Data.SqlDbType.VarChar);
            p20.Value = depto;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p21.Value = tPlanilla;
            cmd.Parameters.Add(p21);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p22.Value = user;
            cmd.Parameters.Add(p22);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p23.Value = tipoPlanilla;
            cmd.Parameters.Add(p23);
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

        public DataTable ObtenerPermisosPlanillaQuinc(int periodo, int tPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p2.Value = tPlanilla;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPermisosPlanillaQuinc";
            cmd.Connection = sqlConnection;
            
            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "permisos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }

        public DataTable ObtenerPlanillaQuincenal(int periodo, int tPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p2.Value = tPlanilla;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPlanillaQuincenal";
            cmd.Connection = sqlConnection;
            
            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "planillaQ");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }

        public bool insertarCalculoIRPlanillaQuincenal(int codigo, decimal iR, int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarCalculoIRPlanillaQuincenal";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@iR", System.Data.SqlDbType.Decimal);
            p2.Value = iR;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p3.Value = periodo;
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

        public bool aplicarDeduccionesQuincenales(int nperiodo, int codEmpleado, decimal deduccion, int idDeduc,
            int tPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "aplicarDeduccionesQuincenales";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p1.Value = nperiodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@deduccion", System.Data.SqlDbType.Decimal);
            p5.Value = deduccion;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@idDeduc", System.Data.SqlDbType.Int);
            p6.Value = idDeduc;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p2.Value = tPlanilla;
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

        public bool distribuirDenominacionesMonedaQuincenal(int d500, int d200, int d100, int d50, int d20, int d10,
            int d5, int d1, int d05, int d025, int d010, int d005, int d001, int codEmpleado, int periodo, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "distribuirDenominacionesPlanillaQuincenal";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@d500", System.Data.SqlDbType.Int);
            p1.Value = d500;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@d200", System.Data.SqlDbType.Int);
            p2.Value = d200;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@d100", System.Data.SqlDbType.Int);
            p3.Value = d100;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@d50", System.Data.SqlDbType.Int);
            p4.Value = d50;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@d20", System.Data.SqlDbType.Int);
            p5.Value = d20;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@d10", System.Data.SqlDbType.Int);
            p6.Value = d10;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@d5", System.Data.SqlDbType.Int);
            p7.Value = d5;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@d1", System.Data.SqlDbType.Int);
            p8.Value = d1;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@d05", System.Data.SqlDbType.Int);
            p9.Value = d05;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@d025", System.Data.SqlDbType.Int);
            p10.Value = d025;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@d010", System.Data.SqlDbType.Int);
            p11.Value = d010;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@d005", System.Data.SqlDbType.Int);
            p12.Value = d005;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@d001", System.Data.SqlDbType.Int);
            p13.Value = d001;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p14.Value = codEmpleado;
            cmd.Parameters.Add(p14);
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p15.Value = periodo;
            cmd.Parameters.Add(p15);
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p16.Value = tipoPlanilla;
            cmd.Parameters.Add(p16);

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

        public DataTable ObtenerDeduccionesQuincenales(int periodo, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p2.Value = tipoPlanilla;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDeduccionesQuincenales";
            cmd.Connection = sqlConnection;
            
            try
            {
              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "planillaQ");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
        }

        public bool InsertarIngrDeducQuincenales(int tipo, int codEmpleado, int idTipo, int periodo, decimal valor,
            string user, int tPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertarIngrDeducQuincenales";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p2.Value = codEmpleado;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@idTipoIngDed", System.Data.SqlDbType.Int);
            p4.Value = idTipo;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p5.Value = periodo;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p6.Value = valor;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p7.Value = user;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@tPlanilla", System.Data.SqlDbType.Int);
            p8.Value = tPlanilla;
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

        public bool EliminarPlanillaQuincenal(int nperiodo, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EliminarPlanillaQuincenalPorPeriodo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p1.Value = nperiodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p2.Value = tipoPlanilla;
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

        public DataTable obtenerEmpleadoPagoVacaciones(int ubicacion,int codigoemp,DateTime fechacorte,bool todos, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable dd = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p1.Value = ubicacion;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@codigoemp", System.Data.SqlDbType.Int);
            p11.Value = codigoemp;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@fechacorte", System.Data.SqlDbType.Date);
            p12.Value = fechacorte;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@todos", System.Data.SqlDbType.Bit);
            p2.Value = todos;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerEmpleadoPagoVacaciones";
            cmd.Connection = sqlConnection;
            
            try
            {               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd);

            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd;
        }

        //public bool insertarCalculoVacaciones(int codigoEmpleado, decimal promedio, decimal mayor, decimal salMensual, int idEmpresa)
        //{

        //    SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
        //    CnBD con = new CnBD();
        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "insertarCalculoPasivoLaboral";
        //    cmd.Connection = sqlConnection;

        //    System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigoEmpleado", System.Data.SqlDbType.Int);
        //    p1.Value = codigoEmpleado;
        //    cmd.Parameters.Add(p1);
        //    System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@promedio", System.Data.SqlDbType.Decimal);
        //    p2.Value = promedio;
        //    cmd.Parameters.Add(p2);
        //    System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@mayor", System.Data.SqlDbType.Decimal);
        //    p3.Value = mayor;
        //    cmd.Parameters.Add(p3);
        //    System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@salMensual", System.Data.SqlDbType.Decimal);
        //    p4.Value = salMensual;
        //    cmd.Parameters.Add(p4);

        //    try
        //    {
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //    }
        //    catch (SystemException)
        //    {
        //        if (cmd.Connection.State != ConnectionState.Closed)
        //            cmd.Connection.Close();
        //        return false;
        //    }
        //    return true;
        //}

        public bool insertarCalculoVacaciones(int codempleado, string nom_depto, string fechaini, string fechafin,
           int semana, int messemana, string nombreemp, decimal salMensual, decimal totalPagar, int periodo, int tperiodo, int anio, string usuario,
           decimal egresos,decimal neto,decimal inss,decimal ir, decimal patronal,decimal inatec,
           int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarCalculoVacacionesPorEmpleados";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codempleado", System.Data.SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nom_depto", System.Data.SqlDbType.VarChar);
            p2.Value = nom_depto;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p5.Value = fechaini;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p6.Value = fechafin;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p7.Value = semana;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@messemana", System.Data.SqlDbType.Int);
            p8.Value = messemana;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@nombreemp", System.Data.SqlDbType.VarChar);
            p9.Value = nombreemp;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@salMensual", System.Data.SqlDbType.Decimal);
            p10.Value = salMensual;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@totalPagarVacaciones", System.Data.SqlDbType.Decimal);
            p11.Value = totalPagar;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@periodoVacaciones", System.Data.SqlDbType.Int);
            p12.Value = periodo;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
            p13.Value = tperiodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@anio", System.Data.SqlDbType.Int);
            p14.Value = anio;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar);
            p15.Value = usuario;
            cmd.Parameters.Add(p15);
            //parametros agregados
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@egresosVacaciones", System.Data.SqlDbType.Decimal);
            p16.Value = egresos;
            cmd.Parameters.Add(p16);
            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@netoVacaciones", System.Data.SqlDbType.Decimal);
            p17.Value = neto;
            cmd.Parameters.Add(p17);
            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@inssVacaciones", System.Data.SqlDbType.Decimal);
            p18.Value =inss;
            cmd.Parameters.Add(p18);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@iRVacaciones", System.Data.SqlDbType.Decimal);
            p19.Value = ir;
            cmd.Parameters.Add(p19);
            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@patronal", System.Data.SqlDbType.Decimal);
            p20.Value = patronal;
            cmd.Parameters.Add(p20);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@inatec", System.Data.SqlDbType.Decimal);
            p21.Value = inatec;
            cmd.Parameters.Add(p21);

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

        public DataTable obtenerEmpleadosPagoAguinaldo(int ubicacion,DateTime fechacorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dd = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p1.Value = ubicacion;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechacorte", System.Data.SqlDbType.Date);
            p2.Value = fechacorte;
            cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@periodoAguinaldo", System.Data.SqlDbType.Int);
            //p3.Value = periodoAguinaldo;
            //cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerEmpleadosPagoAguinaldo";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dd, "planillaAG");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dd.Tables[0];
            
        }

        public bool insertarCalculoAguinaldo(int codempleado, string nom_depto, string fechaini, string fechafin,
           int semana, int messemana, string nombreemp, decimal salMensual, decimal totalPagarAguinaldo,decimal egresosAguinaldo, int periodoAguinaldo, int tperiodo, int anio, string usuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarCalculoAguinaldo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codempleado", System.Data.SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nom_depto", System.Data.SqlDbType.VarChar);
            p2.Value = nom_depto;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p5.Value = fechaini;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p6.Value = fechafin;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p7.Value = semana;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@messemana", System.Data.SqlDbType.Int);
            p8.Value = messemana;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@nombreemp", System.Data.SqlDbType.VarChar);
            p9.Value = nombreemp;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@salMensual", System.Data.SqlDbType.Decimal);
            p10.Value = salMensual;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@totalPagarAguinaldo", System.Data.SqlDbType.Decimal);
            p11.Value = totalPagarAguinaldo;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@egresosAguinaldo", System.Data.SqlDbType.Decimal);
            p16.Value = egresosAguinaldo;
            cmd.Parameters.Add(p16);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@periodoAguinaldo", System.Data.SqlDbType.Int);
            p12.Value = periodoAguinaldo;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
            p13.Value = tperiodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@anio", System.Data.SqlDbType.Int);
            p14.Value = anio;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar);
            p15.Value = usuario;
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
        public bool spInsertarHistoricoAguinaldo(int codigo, DateTime FecIni, DateTime FecFin, decimal salMayor, decimal salMayorD, decimal salPromedio, decimal salPromedioD, decimal salAguinaldo, decimal salAguinaldod,int periodo,
        int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarHistoricoAguinaldo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@Codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@FechaIni", System.Data.SqlDbType.DateTime);
            p2.Value = FecIni;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime);
            p3.Value = FecFin;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@SalMayor", System.Data.SqlDbType.Decimal);
            p4.Value = salMayor;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@SalMayorDia", System.Data.SqlDbType.Decimal);
            p5.Value = salMayorD;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@SalPromedio", System.Data.SqlDbType.Decimal);
            p6.Value = salPromedio;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@SalPromedioDia", System.Data.SqlDbType.Decimal);
            p7.Value = salPromedioD;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@Aguinaldo", System.Data.SqlDbType.Decimal);
            p8.Value = salAguinaldo;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@AguinaldoDia", System.Data.SqlDbType.Decimal);
            p9.Value = salAguinaldod;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }
        public bool insertarPromedioVacacionesTemp(int codigoEmpleado, decimal promedio, decimal mayor, int idEmpresa)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarPromedioVacacionesTemp";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigoEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codigoEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@promedio", System.Data.SqlDbType.Decimal);
            p2.Value = promedio;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@mayor", System.Data.SqlDbType.Decimal);
            p5.Value = mayor;
            cmd.Parameters.Add(p5);

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
        public bool spInsertarHistoricoVacaciones(int codigo, string Feccrea, decimal salPromedio, decimal salPromedioD, decimal vacacionesdia, decimal pagovacaciones, 
            decimal inssvac,decimal irvac, decimal otrasDeduc, int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarHistoricoVacaciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@Codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@FechaCrea", System.Data.SqlDbType.NChar);
            p2.Value = Feccrea;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@SalPromedio", System.Data.SqlDbType.Decimal);
            p6.Value = salPromedio;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@SalPromedioDia", System.Data.SqlDbType.Decimal);
            p7.Value = salPromedioD;
            cmd.Parameters.Add(p7);            
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@VacacionesDia", System.Data.SqlDbType.Decimal);
            p9.Value = vacacionesdia;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@pagoVacaciones", System.Data.SqlDbType.Decimal);
            p8.Value = pagovacaciones;
            cmd.Parameters.Add(p8);
            //parametros agregados
            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@inssVacaciones", System.Data.SqlDbType.Decimal);
            p20.Value = inssvac;
            cmd.Parameters.Add(p20);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@irVacaciones", System.Data.SqlDbType.Decimal);
            p21.Value = irvac;
            cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@otrasDeduc", System.Data.SqlDbType.Decimal);
            p22.Value = otrasDeduc;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);
            

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }
        public bool EliminarHistoricoVacacionesFecha(string fecaut, int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spEliminarHistoricoVacacionesFecha";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaut", System.Data.SqlDbType.Date);
            p1.Value = fecaut;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
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
        public bool spInsertarAguinaldoMeses(int codigo, int anio, int mes, int diasMes, decimal salario, decimal incentivo,decimal beneficio, decimal ingreso, decimal promediodias, DateTime fechaliq,int periodo,string mesnombre, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarAguinaldoMeses";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@anio", System.Data.SqlDbType.Int);
            p2.Value = anio;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@Mes", System.Data.SqlDbType.Int);
            p3.Value = mes;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@salario", System.Data.SqlDbType.Decimal);
            p4.Value = salario;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@incentivo", System.Data.SqlDbType.Decimal);
            p5.Value = incentivo;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p55 = new SqlParameter("@beneficio", System.Data.SqlDbType.Decimal);
            p55.Value = beneficio;
            cmd.Parameters.Add(p55);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@ingreso", System.Data.SqlDbType.Decimal);
            p6.Value = ingreso;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@PromedioDias", System.Data.SqlDbType.Decimal);
            p7.Value = promediodias;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@diaMes", System.Data.SqlDbType.Int);
            p8.Value = diasMes;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@fechaagui", System.Data.SqlDbType.DateTime);
            p9.Value = fechaliq;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);
            System.Data.SqlClient.SqlParameter p29 = new SqlParameter("@mesnombre", System.Data.SqlDbType.NChar);
            p29.Value = mesnombre;
            cmd.Parameters.Add(p29);
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
        public bool aplicarCalculoIR(int codigo, int periodo, int semana, DateTime @fechaI, DateTime fechaF, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "calculoIRCatorcenal";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p3.Value = semana;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaI", System.Data.SqlDbType.Date);
            p4.Value = fechaI;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaF", System.Data.SqlDbType.Date);
            p5.Value = fechaF;
            cmd.Parameters.Add(p5);

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
        //AGREGADO POR GRETHEL TERCERO 24/10/2016
        //OBTIENE O ACTUALIZA EL ESTADO DE CUENTA SEGUN PARAMETRO DE PROCESO
        public bool ActualizarEstadoCuentaDeudaEmp(int id, int id_Tipo, int codempleado, int tipodeduc, int periodo,  decimal ultimacuota,int ddespecial, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p14.Value = id;
            cmd.Parameters.Add(p14);

            //System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@id_Tipo", System.Data.SqlDbType.Int);
            //p11.Value = id_Tipo;
            //cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipodeduc", System.Data.SqlDbType.Int);
            p12.Value = tipodeduc;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);
           
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@UltimaCuota", System.Data.SqlDbType.Decimal);
            p15.Value = ultimacuota;
            cmd.Parameters.Add(p15);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@ddespecial", System.Data.SqlDbType.Int);
            p23.Value = ddespecial;
            cmd.Parameters.Add(p23);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarEstadoCuentaDeudaEmp";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }
        public bool ActualizarPeriodoDeduccion(int id, int periodo, int estado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p14.Value = id;
            cmd.Parameters.Add(p14);      

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@estado", System.Data.SqlDbType.Int);
            p1.Value = estado;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarPeriodoDeduccion";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }
        public bool DeduccionCuotaPlanillaEmpIns(int id, int id_Tipo, int codempleado, int tipodeduc, int periodo, int semana, decimal ultimacuota, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p14.Value = id;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@id_Tipo", System.Data.SqlDbType.Int);
            p11.Value = id_Tipo;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipodeduc", System.Data.SqlDbType.Int);
            p12.Value = tipodeduc;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p23.Value = semana;
            cmd.Parameters.Add(p23);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@UltimaCuota", System.Data.SqlDbType.Decimal);
            p15.Value = ultimacuota;
            cmd.Parameters.Add(p15);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionCuotaPlanillaEmpIns";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }
        public DataTable ObtenerEstadoCuentaDeudaEmp(string id, int codempleado, int tipodeduc, int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p11.Value = id;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipodeduc", System.Data.SqlDbType.Int);
            p12.Value = tipodeduc;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p14.Value = semana;
            cmd.Parameters.Add(p14);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerEstadoCuentaDeudaEmp";
            cmd.Connection = sqlConnection;

            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt, "planilla");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return dt.Tables[0];
        }
        public bool ActualizarTotalesPlanillaEmp(int codempleado, int periodo, int semana, decimal egresodeduc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p12.Value = semana;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@egreso", System.Data.SqlDbType.Decimal);
            p14.Value = egresodeduc;
            cmd.Parameters.Add(p14);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarTotalesPlanillaEmp";
            cmd.Connection = sqlConnection;

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
        //AGREGADO POR WBRAVO
        public void PlnplanillasInsBulk(int idEmpresa, dsPlanilla.dtPlanillaDataTable dtpln)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection))
            {
                bulkCopy.DestinationTableName = "dbo.plnplanillas";

                try
                {
                    sqlConnection.Open();
                    bulkCopy.WriteToServer(dtpln);
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public DataTable obtenerDetalleHorasLab(DateTime fechaini, DateTime fechafin, int CodEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p1.Value = fechafin;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p2.Value = fechaini;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@CodEmp", System.Data.SqlDbType.Int);
            p3.Value = CodEmp;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerDetalleHorasLab2";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        //public DataTable obtenerDetalleHorasLab(int periodo, int semana, int CodEmp, int idEmpresa)
        //{
        //    SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
        //    DataTable ds = new DataTable();
        //    CnBD con = new CnBD();
        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

        //    System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
        //    p1.Value = periodo;
        //    cmd.Parameters.Add(p1);
        //    System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
        //    p2.Value = semana;
        //    cmd.Parameters.Add(p2);
        //    System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@CodEmp", System.Data.SqlDbType.VarChar);
        //    p3.Value = CodEmp;
        //    cmd.Parameters.Add(p3);

        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "obtenerDetalleHorasLab";
        //    //cmd.CommandText = "obtenerDetalleDeducciones";
        //    cmd.Connection = sqlConnection;

        //    try
        //    {
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        cmd.Connection.Close();
        //        System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(ds);
        //    }
        //    catch (SystemException)
        //    {
        //        if (cmd.Connection.State != ConnectionState.Closed)
        //            cmd.Connection.Close();

        //    }

        //    return ds;
        //}
        public DataTable ObtenerPermisosxEmpleado(DateTime fechaini, DateTime fechafin, int CodEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@CodEmp", System.Data.SqlDbType.VarChar);
            p3.Value = CodEmp;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPermisosxEmpleado";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(ds);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataTable ObtenerPermisosxEmpleadoR(DateTime fechaini, DateTime fechafin, int CodEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@CodEmp", System.Data.SqlDbType.VarChar);
            p3.Value = CodEmp;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPermisosxEmpleadoR";
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
        public DataTable ObtenerDatosTurnoEmpleadoMarcas(int CodEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p3.Value = CodEmp;
            cmd.Parameters.Add(p3);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDatosTurnoEmpleadoMarcas";
            //cmd.CommandText = "obtenerDetalleDeducciones";
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
        public bool DeduccionCuotaPrestacionesEmpIns(int id, int id_Tipo, int codempleado, int tipodeduc, string fecaut, decimal ultimacuota, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p14.Value = id;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@id_Tipo", System.Data.SqlDbType.Int);
            p11.Value = id_Tipo;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipodeduc", System.Data.SqlDbType.Int);
            p12.Value = tipodeduc;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@fecaut", System.Data.SqlDbType.Date);
            p13.Value = fecaut;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@user", System.Data.SqlDbType.NChar);
            p23.Value = user;
            cmd.Parameters.Add(p23);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@UltimaCuota", System.Data.SqlDbType.Decimal);
            p15.Value = ultimacuota;
            cmd.Parameters.Add(p15);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionCuotaPrestacionesEmpIns";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }
        public DataSet obtenerDetalleDeduccionesPorEmpleadoAll(int periodo, int codEmpleado, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            //p2.Value = semana;
            //cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p4.Value = tipoPlanilla;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "obtenerDetalleSemanaEmpleado";
            cmd.CommandText = "obtenerDetalleDeduccionesAll";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "detalle");
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }


        public DataSet obtenerDetalleIngresosPorEmpleadoAll(int periodo, int codEmpleado, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            //p2.Value = semana;
            //cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p4.Value = tipoPlanilla;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerDetalleIngresosAll";
            cmd.Connection = sqlConnection;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "detalle");
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public bool PlnIngresoVacDescIns(int id, int id_Tipo, int codempleado, int nsemana, int tipodeduc, int periodo, decimal valor, decimal tiempo, string usuario, int tplanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet dt = new DataSet();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            SqlParameter p5 = new SqlParameter("@id", SqlDbType.Int);
            p5.Value = id;
            cmd.Parameters.Add(p5);
            SqlParameter p2 = new SqlParameter("@Id_tipo", SqlDbType.Int);
            p2.Value = id_Tipo;
            cmd.Parameters.Add(p2);
            SqlParameter p1 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
            p1.Value = codempleado;
            cmd.Parameters.Add(p1);
            SqlParameter p9 = new SqlParameter("@nsemana", SqlDbType.Int);
            p9.Value = nsemana;
            cmd.Parameters.Add(p9);
            SqlParameter p3 = new SqlParameter("@tipoIngrDeduc", SqlDbType.Int);
            p3.Value = tipodeduc;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@periodo", SqlDbType.Int);
            p4.Value = periodo;
            cmd.Parameters.Add(p4);
            SqlParameter p6 = new SqlParameter("@valor", SqlDbType.Decimal);
            p6.Value = valor;
            cmd.Parameters.Add(p6);
            SqlParameter p8 = new SqlParameter("@tiempo", SqlDbType.Decimal);
            p8.Value = tiempo;
            cmd.Parameters.Add(p8);
            SqlParameter p7 = new SqlParameter("@usuariograba", SqlDbType.NChar);
            p7.Value = usuario;
            cmd.Parameters.Add(p7);
            SqlParameter p10 = new SqlParameter("@tplanilla", SqlDbType.Int);
            p10.Value = tplanilla;
            cmd.Parameters.Add(p10);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnIngresoVacDescIns";
            cmd.Connection = sqlConnection;
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
    }
}
