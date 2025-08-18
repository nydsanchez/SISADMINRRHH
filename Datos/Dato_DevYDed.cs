using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Datos
{
    public class Dato_DevYDed
    {
     
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
         ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public DataSet SeleccionarDeducciones(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarDeducciones";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataSet SeleccionarDeduccionesEspeciales(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarDeduccionesEspeciales";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataSet SeleccionarDeduccionesRecurrentes(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarDeduccionesRecurrentes";
            cmd.Connection = sqlConnection;
           
            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataSet cargarIngresos(int filtro,int idEmpresa)
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
                da.Fill(ds, "ingresos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        public DataSet cargarIngresosAplicaDia( int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarIngresosAplicaDia";
            cmd.Connection = sqlConnection;

            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ingresos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        public DataSet SeleccionarDevengados(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarDevengados";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "devengados");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        public bool DeduccionesEditar(string deduccion, bool activo, int idDeduc, bool aplicaagui, bool aplicavac, string prioridad, bool mostrarc, bool deducibruto, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesEdit";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@deduccion", System.Data.SqlDbType.VarChar);
            p1.Value = deduccion;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            p2.Value = activo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idDeduc", System.Data.SqlDbType.Int);
            p3.Value = idDeduc;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p33 = new SqlParameter("@aplicaagui", System.Data.SqlDbType.Bit);
            p33.Value = aplicaagui;
            cmd.Parameters.Add(p33);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@aplicavac", System.Data.SqlDbType.Bit);
            p4.Value = aplicavac;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@prioridad", System.Data.SqlDbType.Int);
            p5.Value = prioridad;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@mostrarc", System.Data.SqlDbType.Bit);
            p6.Value = mostrarc;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@deducibruto", System.Data.SqlDbType.Bit);
            p7.Value = deducibruto;
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

        public bool DeduccionesEliminar(int idDeduc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idDeduc", System.Data.SqlDbType.Int);
            p3.Value = idDeduc;
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

        public bool DeduccionesEmpleadoEliminar(int idDeduc, int codEmpleado, int periodo, int semana, int id, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesEmpleadoElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idDeduc", System.Data.SqlDbType.Int);
            p3.Value = idDeduc;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p5.Value = id;
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

        public bool IngresosEmpleadoEliminar(int idDeduc, int codEmpleado, int periodo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosEmpleadoElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idDeduc", System.Data.SqlDbType.Int);
            p3.Value = idDeduc;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
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

        public bool DeduccionesEmpleadoEditar(int IdDeduccion, decimal total, int codEmpleado, int periodo, int semana, int id, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionesEmpleadoEditar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@IdDeduc", System.Data.SqlDbType.Int);
            p1.Value = IdDeduccion;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p2.Value = total;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p4.Value = periodo;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p5.Value = semana;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p6.Value = id;
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
        //retornar cuota incompleta
        public bool plnRetornarCuotaIncompleta( int periodo, int id,int codigo,int semana,decimal egreso, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnRetornarCuotaIncompleta";
            cmd.Connection = sqlConnection;
        
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p4.Value = periodo;
            cmd.Parameters.Add(p4);          

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p6.Value = id;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@egreso", System.Data.SqlDbType.Decimal);
            p3.Value = egreso;
            cmd.Parameters.Add(p3);


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

        public bool plnAplicarEgresoCuotaParc(int periodo, int id, int codigo, int semana, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnAplicarEgresoCuotaParc";
            cmd.Connection = sqlConnection;
            SqlParameter p3 = new SqlParameter("@periodo", SqlDbType.Int);
            p3.Value = periodo;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@id", SqlDbType.Int);
            p4.Value = id;
            cmd.Parameters.Add(p4);
            SqlParameter p1 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@semana", SqlDbType.Int);
            p2.Value = semana;
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

        public bool IngresosEmpleadoEditar(int ID, decimal total, int codEmpleado, int periodo, int semana,int ingresoAsociado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosEmpleadoEditar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            p1.Value = ID;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p2.Value = total;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p4.Value = periodo;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p5.Value = semana;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@ingresoAsociado", System.Data.SqlDbType.Int);
            p6.Value = ingresoAsociado;
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

        public bool devengadosEditar(int Id, string deveng, bool inss, bool ir, bool Liq, bool aplicadeduc, bool mostrarc, bool deducibruto, int ingasoc, int deducasoc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "devengadosEdit";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@Id", System.Data.SqlDbType.Int);
            p1.Value = Id;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@deveng", System.Data.SqlDbType.VarChar);
            p2.Value = deveng;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@inss", System.Data.SqlDbType.Bit);
            p3.Value = inss;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@ir", System.Data.SqlDbType.Bit);
            p4.Value = ir;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@Liq", System.Data.SqlDbType.Bit);
            p5.Value = Liq;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p33 = new SqlParameter("@aplicadeduc", System.Data.SqlDbType.Bit);
            p33.Value = aplicadeduc;
            cmd.Parameters.Add(p33);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@mostrar", System.Data.SqlDbType.Bit);
            p6.Value = mostrarc;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@aplicadeducibruto", System.Data.SqlDbType.Bit);
            p7.Value = deducibruto;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p44 = new SqlParameter("@ingasoc", System.Data.SqlDbType.Int);
            p44.Value = ingasoc;
            cmd.Parameters.Add(p44);
            System.Data.SqlClient.SqlParameter p55 = new SqlParameter("@deducasoc", System.Data.SqlDbType.Int);
            p55.Value = deducasoc;
            cmd.Parameters.Add(p55);
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
        public bool DevengadosEliminar(int idDeveng, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "devengadosElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@idDeveng", System.Data.SqlDbType.Int);
            p3.Value = idDeveng;
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
        public bool DeduccionesAgregar(string deduccion, bool activo, bool aplicaagui, bool aplicavac, string prioridad, bool mostrarc, bool deducibruto, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesAdd";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@deduccion", System.Data.SqlDbType.VarChar);
            p1.Value = deduccion;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@activo", System.Data.SqlDbType.Bit);
            p2.Value = activo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@aplicaagui", System.Data.SqlDbType.Bit);
            p3.Value = aplicaagui;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@aplicavac", System.Data.SqlDbType.Bit);
            p4.Value = aplicavac;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@prioridad", System.Data.SqlDbType.Int);
            p5.Value = prioridad;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@mostrarc", System.Data.SqlDbType.Bit);
            p6.Value = mostrarc;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@deducibruto", System.Data.SqlDbType.Bit);
            p7.Value = deducibruto;
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

        public bool DevengadosAgregar(string devengado, bool inss, bool ir, bool liquidacion, bool aplicadeduc, bool mostrarc, bool deducibruto, int ingasoc, int deducasoc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "devengadosAdd";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@devengado", System.Data.SqlDbType.VarChar);
            p1.Value = devengado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@inss", System.Data.SqlDbType.Bit);
            p2.Value = inss;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ir", System.Data.SqlDbType.Bit);
            p3.Value = ir;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@liquidacion", System.Data.SqlDbType.Bit);
            p4.Value = liquidacion;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p33 = new SqlParameter("@aplicadeduc", System.Data.SqlDbType.Bit);
            p33.Value = aplicadeduc;
            cmd.Parameters.Add(p33);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@mostrar", System.Data.SqlDbType.Bit);
            p6.Value = mostrarc;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@aplicadeducibruto", System.Data.SqlDbType.Bit);
            p7.Value = deducibruto;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p44 = new SqlParameter("@ingasoc", System.Data.SqlDbType.Int);
            p44.Value = ingasoc;
            cmd.Parameters.Add(p44);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@deducasoc", System.Data.SqlDbType.Int);
            p5.Value = deducasoc;
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
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }

        public bool PlnRegistrarRubrosIncentivoIns(int tipo, int codEmpleado, int nSemana, int idTipo, int periodo, decimal valor, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnRegistrarRubrosIncentivoIns";
            cmd.Connection = sqlConnection;
            SqlParameter p1 = new SqlParameter("@tipo", SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@codEmpleado", SqlDbType.Int);
            p2.Value = codEmpleado;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@nSemana", SqlDbType.Int);
            p3.Value = nSemana;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@idTipoIngDed", SqlDbType.Int);
            p4.Value = idTipo;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@periodo", SqlDbType.Int);
            p5.Value = periodo;
            cmd.Parameters.Add(p5);
            SqlParameter p6 = new SqlParameter("@valor", SqlDbType.Decimal);
            p6.Value = valor;
            cmd.Parameters.Add(p6);
            SqlParameter p7 = new SqlParameter("@user", SqlDbType.VarChar);
            p7.Value = user;
            cmd.Parameters.Add(p7);
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
        public bool plnAplicarPlanillaIncentivo(int tipo,  int nSemana, int idTipo, int periodo,string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnAplicarPlanillaIncentivo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p1.Value = tipo;
            cmd.Parameters.Add(p1);
          
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@nSemana", System.Data.SqlDbType.Int);
            p3.Value = nSemana;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@idTipoIngDed", System.Data.SqlDbType.Int);
            p4.Value = idTipo;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p5.Value = periodo;
            cmd.Parameters.Add(p5);          
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar);
            p7.Value = user;
            cmd.Parameters.Add(p7);

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
        public bool ActualizarSalarioHorasExtras(int periodo, int nSemana,bool consolidar, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarSalarioHorasExtras";
            cmd.Connection = sqlConnection;

        
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p3.Value = nSemana;
            cmd.Parameters.Add(p3);
          
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p5.Value = periodo;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@consolidar", System.Data.SqlDbType.Bit);
            p6.Value = consolidar;
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
        public bool IngresosAplicaIBrutoBakIns(int tipo, int codEmpleado, int nSemana, int idTipo, int periodo, decimal valor, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnIngresosAplicaIBrutoBakIns";
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
        public int verificarExiste(int codEmpleado, int periodo, int tipoDeduccion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "verificarExisteDeduccion";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduccion", System.Data.SqlDbType.Int);
            p3.Value = tipoDeduccion;
            cmd.Parameters.Add(p3);

            int rsp = 0;

            try
            {
                cmd.Connection.Open();
                rsp = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return -1;
            }

            return rsp;
        }
        public bool AsignarDeduccionesOrdinariasEmpleado(int codEmpleado, int periodo, int tipoDeduccion, decimal total, decimal cuotas, string fecsol, string fecaut, string user, int porc, int recurrente,int periodovalidez,string factiva,string fexpira,string coment, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesOrdinariasEmpleadoIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduccion", System.Data.SqlDbType.Int);
            p3.Value = tipoDeduccion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p4.Value = total;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@cantCuota", System.Data.SqlDbType.Decimal);
            p5.Value = cuotas;
            cmd.Parameters.Add(p5);
            //AGREGADO POR GRETHEL TERCERO
            System.Data.SqlClient.SqlParameter p55 = new SqlParameter("@fecsol", System.Data.SqlDbType.Date);
            p55.Value = fecsol;
            cmd.Parameters.Add(p55);
            System.Data.SqlClient.SqlParameter p54 = new SqlParameter("@fecaut", System.Data.SqlDbType.Date);
            p54.Value = fecaut;
            cmd.Parameters.Add(p54);
            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@userreg", System.Data.SqlDbType.NChar);
            p56.Value = user;
            cmd.Parameters.Add(p56);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@porcentual", System.Data.SqlDbType.Int);
            p22.Value = porc;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@recurrente", System.Data.SqlDbType.Int);
            p23.Value = recurrente;
            cmd.Parameters.Add(p23);
            //
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@factiva", System.Data.SqlDbType.Date);
            p6.Value = factiva;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@fexpira", System.Data.SqlDbType.Date);
            p7.Value = fexpira;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@periodovalidez", System.Data.SqlDbType.Int);
            p8.Value = periodovalidez;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@coment", System.Data.SqlDbType.NVarChar);
            p9.Value = coment;
            cmd.Parameters.Add(p9);
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
        //  ingresoS ESPECIALES RECURRENTE
        public bool IngresosEspecialesEmpleadoIns(int codEmpleado, int periodo, int tipoIng, decimal total,  string user, int recurrente, string coment, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosEspecialesEmpleadoIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoIng", System.Data.SqlDbType.Int);
            p3.Value = tipoIng;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p4.Value = total;
            cmd.Parameters.Add(p4);
          
            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@userreg", System.Data.SqlDbType.NChar);
            p56.Value = user;
            cmd.Parameters.Add(p56);
           
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@recurrente", System.Data.SqlDbType.Int);
            p23.Value = recurrente;
            cmd.Parameters.Add(p23);
            //
           
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@coment", System.Data.SqlDbType.NVarChar);
            p9.Value = coment;
            cmd.Parameters.Add(p9);
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
        //  ingresoS ESPECIALES RECURRENTE
        public bool SalarioEspecialEmpleadoIns(int codEmpleado, decimal salariop, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SalarioEspecialEmpleadoIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@salariop", System.Data.SqlDbType.Decimal);
            p4.Value = salariop;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@userreg", System.Data.SqlDbType.NChar);
            p56.Value = user;
            cmd.Parameters.Add(p56);

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
        //DEDUCCIONES ESPECIALES FUERA DE PLANILLA
        public bool AsignardeduccionesEspecialesIns( int periodo, int tipoDeduccion, decimal total, decimal cuotas,string user, int porc, int recurrente,int tipo,int filtro1,int filtro2,int exceemb,int excefec, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesEspecialesIns";
            cmd.Connection = sqlConnection;
          
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduc", System.Data.SqlDbType.Int);
            p3.Value = tipoDeduccion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@valorCuota", System.Data.SqlDbType.Decimal);
            p5.Value = cuotas;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p4.Value = total;
            cmd.Parameters.Add(p4);           
            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@usuario", System.Data.SqlDbType.NChar);
            p56.Value = user;
            cmd.Parameters.Add(p56);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@porcentual", System.Data.SqlDbType.Int);
            p22.Value = porc;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@recurrente", System.Data.SqlDbType.Int);
            p23.Value = recurrente;
            cmd.Parameters.Add(p23);
            //
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p6.Value = tipo;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@filtro1", System.Data.SqlDbType.Int);
            p7.Value = filtro1;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@filtro2", System.Data.SqlDbType.Int);
            p8.Value = filtro2;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@excembargo", System.Data.SqlDbType.Bit);
            p9.Value = exceemb;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@excefectivo", System.Data.SqlDbType.Bit);
            p10.Value = excefec;
            cmd.Parameters.Add(p10);
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
        public bool AsignardeduccionesEspecialesUpd(int periodo, int tipoDeduccion, decimal total, decimal cuotas, string user, int porc, int recurrente, int tipo, int filtro1, int filtro2, int exceemb, int excefec, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesEspecialesUpd";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduc", System.Data.SqlDbType.Int);
            p3.Value = tipoDeduccion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@valorCuota", System.Data.SqlDbType.Decimal);
            p5.Value = cuotas;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p4.Value = total;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@usuario", System.Data.SqlDbType.NChar);
            p56.Value = user;
            cmd.Parameters.Add(p56);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@porcentual", System.Data.SqlDbType.Int);
            p22.Value = porc;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@recurrente", System.Data.SqlDbType.Int);
            p23.Value = recurrente;
            cmd.Parameters.Add(p23);
            //
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p6.Value = tipo;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@filtro1", System.Data.SqlDbType.Int);
            p7.Value = filtro1;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@filtro2", System.Data.SqlDbType.Int);
            p8.Value = filtro2;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@excembargo", System.Data.SqlDbType.Bit);
            p9.Value = exceemb;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@excefectivo", System.Data.SqlDbType.Bit);
            p10.Value = excefec;
            cmd.Parameters.Add(p10);
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
        public bool AsignarIngresosEspecialesIns(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AsignarIngresosEspecialesIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
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
        public bool DeduccionesExternasLogIns(int codEmpleado, decimal sobregiro,string tiposobregiro,int tipodeduc,string usuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionesExternasLogIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@sobregiro", System.Data.SqlDbType.Decimal);
            p2.Value = sobregiro;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tiposobregiro", System.Data.SqlDbType.NChar);
            p3.Value = tiposobregiro;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipodeduc", System.Data.SqlDbType.Int);
            p4.Value = tipodeduc;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@usuario", System.Data.SqlDbType.NChar);
            p5.Value = usuario;
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
        public bool deduccionesSaldoPendEmpleadoIns(int codEmpleado, int periodo, int tipoDeduccion, decimal total, decimal cuotas, string fecsol, string fecaut, string user, int porc, int recurrente, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesSaldoPendEmpleadoIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduccion", System.Data.SqlDbType.Int);
            p3.Value = tipoDeduccion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p4.Value = total;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@cantCuota", System.Data.SqlDbType.Decimal);
            p5.Value = cuotas;
            cmd.Parameters.Add(p5);
            //AGREGADO POR GRETHEL TERCERO
            System.Data.SqlClient.SqlParameter p55 = new SqlParameter("@fecsol", System.Data.SqlDbType.Date);
            p55.Value = fecsol;
            cmd.Parameters.Add(p55);
            System.Data.SqlClient.SqlParameter p54 = new SqlParameter("@fecaut", System.Data.SqlDbType.Date);
            p54.Value = fecaut;
            cmd.Parameters.Add(p54);
            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@userreg", System.Data.SqlDbType.NChar);
            p56.Value = user;
            cmd.Parameters.Add(p56);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@porcentual", System.Data.SqlDbType.Int);
            p22.Value = porc;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@recurrente", System.Data.SqlDbType.Int);
            p23.Value = recurrente;
            cmd.Parameters.Add(p23);
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
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool AsignarDeduccionesRecurrentes(int codEmpleado, int tipoDeduccion, bool especial, decimal total, bool porcentual, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionesRecurrentesEmpleadoIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipoDeduccion", System.Data.SqlDbType.Int);
            p2.Value = tipoDeduccion;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@deducEspecial", System.Data.SqlDbType.Bit);
            p4.Value = especial;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p3.Value = total;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@porcentual", System.Data.SqlDbType.Bit);
            p5.Value = porcentual;
            cmd.Parameters.Add(p5);
            
            System.Data.SqlClient.SqlParameter p55 = new SqlParameter("@user", System.Data.SqlDbType.NChar);
            p55.Value = user;
            cmd.Parameters.Add(p55);

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
        //MODIFICADO POR GRETHEL TERCERO 25/10/2016
        public DataSet DeduccionesOrdinariasObtener(int codEmpleado,int mostrar,int saldo,  int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@mostrar", System.Data.SqlDbType.Int);
            p11.Value = mostrar;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@saldo", System.Data.SqlDbType.Int);
            p2.Value = saldo;
            cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tipoDeduccion", System.Data.SqlDbType.Int);
            //p2.Value = tipoDeduccion;
            //cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionesOrdinariasSel";
            cmd.Connection = sqlConnection;
            

            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet DeduccionesOrdinariasInactivasSel(int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
          
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionesOrdinariasInactivasSel";
            cmd.Connection = sqlConnection;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet IngresosEspecialesSel(int codEmpleado,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosEspecialesSel";
            cmd.Connection = sqlConnection;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ingresos");

            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet DeduccionesOrdinariasObtenerxTipo(int codEmpleado,int tipodeduc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@tipodeduc", System.Data.SqlDbType.Int);
            p11.Value = tipodeduc;
            cmd.Parameters.Add(p11);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionesOrdinariasxTipoSel";
            cmd.Connection = sqlConnection;


            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public DataSet IngresosPeriodoxTipoSel(int codEmpleado, int periodo,int semana,int iddeduc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p11.Value = periodo;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p13.Value = semana;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@iddeduc", System.Data.SqlDbType.Int);
            p12.Value = iddeduc;
            cmd.Parameters.Add(p12);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosPeriodoxTipoSel";
            cmd.Connection = sqlConnection;


            try
            {

                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        public bool IngresosPeriodoAplicaIBrutoSel(int codEmpleado, int periodo,int iddeduc, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosPeriodoAplicaIBrutoSel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p11.Value = periodo;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@iddeduc", System.Data.SqlDbType.Int);
            p12.Value = iddeduc;
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
        public bool DeduccionOrdinariaEliminar(int ID, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "deduccionOrdinariaElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p1.Value = ID;
            cmd.Parameters.Add(p1);

            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@deduccion", System.Data.SqlDbType.VarChar);
            //p2.Value = deduccion;
            //cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@periodoI", System.Data.SqlDbType.Int);
            //p3.Value = periodoI;
            //cmd.Parameters.Add(p3);

            //System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@periodoF", System.Data.SqlDbType.Int);
            //p4.Value = periodoF;
            //cmd.Parameters.Add(p4);


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

        public bool EditarMarcasxEmpleado(int codigo, DateTime fecha, string horaE, string horaS, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarMarcasxEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
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

        //public bool EditarDeduccionesxEmpleado(int codEmpleado, decimal cuotas, string tipoDeduc, int idEmpresa)
        //{
        //    SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
        //    CnBD con = new CnBD();
        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "EditarDeduccionesxEmpleado";
        //    cmd.Connection = sqlConnection;

        //    System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
        //    p1.Value = codEmpleado;
        //    cmd.Parameters.Add(p1);

        //    System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@cuotas", System.Data.SqlDbType.Decimal);
        //    p2.Value = cuotas;
        //    cmd.Parameters.Add(p2);

        //    System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduc", System.Data.SqlDbType.VarChar);
        //    p3.Value = tipoDeduc;
        //    cmd.Parameters.Add(p3);

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
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool EditarDeduccionesxEmpleado(int ID, decimal total, decimal cuotas, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarDeduccionesxEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            p1.Value = ID;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p2.Value = total;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@cuotas", System.Data.SqlDbType.Decimal);
            p3.Value = cuotas;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@usermodif", System.Data.SqlDbType.NChar);
            p4.Value = user;
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
        public bool EditarIngresosEspxEmpleado(int ID, decimal total,  string user,int estado ,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EditarIngresosEspxEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            p1.Value = ID;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@total", System.Data.SqlDbType.Decimal);
            p2.Value = total;
            cmd.Parameters.Add(p2);          
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@usermodif", System.Data.SqlDbType.NChar);
            p4.Value = user;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@estado", System.Data.SqlDbType.Int);
            p11.Value = estado;
            cmd.Parameters.Add(p11);

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
        public bool IngresosEmpAplicaDeducIBrutoDel(int codigo,int periodo,int iddeduc,  int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosEmpAplicaDeducIBrutoDel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p11.Value = periodo;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@iddeduc", System.Data.SqlDbType.Int);
            p12.Value = iddeduc;
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
        //public bool DeshabilitarDeuda(int codEmpleado, string idDeduccion, int idEmpresa)
        //{
        //    SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
        //    CnBD con = new CnBD();
        //    System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = "DeshabilitarDeuda";
        //    cmd.Connection = sqlConnection;

        //    System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
        //    p1.Value = codEmpleado;
        //    cmd.Parameters.Add(p1);
        //    System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@Deduccion", System.Data.SqlDbType.VarChar);
        //    p2.Value = idDeduccion;
        //    cmd.Parameters.Add(p2);

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
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool DeshabilitarDeuda(int ID, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeshabilitarDeuda";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@ID", System.Data.SqlDbType.Int);
            p1.Value = ID;
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
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool AsignarIngresoOdeduccionPorEmpleado(int codEmpleado, int periodo, int semana, decimal valor, int tipoAsignacion, int IngrsDeduc, int idEmpresa, int tipoPlanilla, string user)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AsignarIngresoOdeduccionPorEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p6.Value = semana;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p3.Value = valor;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipoAsignacion", System.Data.SqlDbType.Int);
            p4.Value = tipoAsignacion;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@IngrsDeduc", System.Data.SqlDbType.Int);
            p5.Value = IngrsDeduc;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p7.Value = tipoPlanilla;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p72 = new SqlParameter("@user", System.Data.SqlDbType.NChar);
            p72.Value = user;
            cmd.Parameters.Add(p72);
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

        public DataSet cargarIngresosLiq(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "cargarIngresosLiq";
            cmd.Connection = sqlConnection;
           
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "ingresos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }
        //AGREGADO POR GRETHEL TERCERO 24/10/2016
        public DataSet ObtenerDeduccionesVigentes(int codigo,int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDeduccionesVigentes";
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
        public DataSet DeduccionEspecialesActivas(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeduccionEspecialesActivas";
            cmd.Connection = sqlConnection;

            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        public DataSet AdelantosEspecialesActivos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AdelantosEspecialesActivos";
            cmd.Connection = sqlConnection;

            try
            {
               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "deducciones");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        //AGREGADO POR WBRAVO
        public dsPlanilla.dtIngDedDataTable IngresosxPeriodoSel(int periodo, int semana, int idEmpresa)
        {

            dsPlanilla.dtIngDedDataTable dt = new dsPlanilla.dtIngDedDataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosxPeriodoSel";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public dsPlanilla.dtIngDedDataTable IngresosxPeriodoSelAll(int periodo, int idEmpresa)
        {

            dsPlanilla.dtIngDedDataTable dt = new dsPlanilla.dtIngDedDataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            //p4.Value = semana;
            //cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosxPeriodoSelAll";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public dsPlanilla.dtIngDedDataTable BeneficiosxPeriodoSel(int periodo,int idEmpresa)
        {

            dsPlanilla.dtIngDedDataTable dt = new dsPlanilla.dtIngDedDataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            //p4.Value = semana;
            //cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BeneficiosxPeriodoSel";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public int verificaDeduccionIBruto(int codEmpleado ,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "verificaDeduccionIBruto";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            //System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            //p2.Value = periodo;
            //cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduccion", System.Data.SqlDbType.Int);
            //p3.Value = tipoDeduccion;
            //cmd.Parameters.Add(p3);

            int rsp = 0;

            try
            {
                cmd.Connection.Open();
                rsp = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return -1;
            }

            return rsp;
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPeriodoSel(int periodo, int semana, int idEmpresa)
        {

            dsPlanilla.dtEgresosDataTable dt = new dsPlanilla.dtEgresosDataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EgresosxPeriodoSel";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPeriodoSelAll(int periodo, int idEmpresa)
        {

            dsPlanilla.dtEgresosDataTable dt = new dsPlanilla.dtEgresosDataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            //System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            //p4.Value = semana;
            //cmd.Parameters.Add(p4);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EgresosxPeriodoSelAll";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public dsPlanilla.dtEgresosDataTable EgresosEspecialesxPeriodoSel(int periodo,int semana,  int idEmpresa)
        {

            dsPlanilla.dtEgresosDataTable dt = new dsPlanilla.dtEgresosDataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);
            //System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            //p41.Value = codigo;
            //cmd.Parameters.Add(p41);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EgresosEspecialesxPeriodoSel";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPrestacionesSel(int periodo, int tprestacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            dsPlanilla.dtEgresosDataTable dt = new dsPlanilla.dtEgresosDataTable();
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

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPrestacionesSelxEmpleado(int periodo, int tprestacion,int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            dsPlanilla.dtEgresosDataTable dt = new dsPlanilla.dtEgresosDataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@tprestacion", System.Data.SqlDbType.Int);
            p11.Value = tprestacion;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p12.Value = codigo;
            cmd.Parameters.Add(p12);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDeduccionesARestarPrestacionesxEmpleado";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public DataSet verificarIngresocnDeduccionIBruto(int idDevengado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idDevengado", System.Data.SqlDbType.Int);
            p1.Value = idDevengado;
            cmd.Parameters.Add(p1);           

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "verificarIngresocnDeduccionIBruto";
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
        public int verificarAplicaDeduccionIBruto(int idDeduccion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "verificarAplicaDeduccionIBruto";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@idDeduccion", System.Data.SqlDbType.Int);
            p1.Value = idDeduccion;
            cmd.Parameters.Add(p1);
            int rsp = 0;

            try
            {
                cmd.Connection.Open();
                rsp = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return -1;
            }

            return rsp;
        }

        public int verificaDeduccionPrioridad(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "verificaDeduccionPrioridad";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            int rsp = 0;

            try
            {
                cmd.Connection.Open();
                rsp = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return -1;
            }

            return rsp;
        }

        public int PlnValidarPersonalAplicaProteccion(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnValidarPersonalAplicaProteccion";
            cmd.Connection = sqlConnection;
            SqlParameter p1 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            int rsp = 0;
            try
            {
                cmd.Connection.Open();
                rsp = (int)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                return -1;
            }
            return rsp;
        }
        public bool IngresosIncentivoIBrutoEliminar(int periodo, int semana,int tipoingr, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosIncentivoIBrutoEliminar";
            cmd.Connection = sqlConnection;        
          
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@tipoingr", System.Data.SqlDbType.Int);
            p41.Value = tipoingr;
            cmd.Parameters.Add(p41);

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
        public bool IngresosIncentivoIBrutoEliminarxEmp(int periodo, int semana, int tipoingr, int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosIncentivoIBrutoEliminarxEmp";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p4.Value = semana;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@tipoingr", System.Data.SqlDbType.Int);
            p41.Value = tipoingr;
            cmd.Parameters.Add(p41);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p22.Value = codigo;
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
        public DataTable ObtenerIngresocnDeduccionIBruto( int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();         

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerIngresocnDeduccionIBruto";
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
        public dsPlanilla.dtEgresosDataTable EgresosxPrestacionesFechaSelxEmpleado(string fecaut, int tprestacion, int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            dsPlanilla.dtEgresosDataTable dt = new dsPlanilla.dtEgresosDataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaut", System.Data.SqlDbType.Date);
            p1.Value = fecaut;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@tprestacion", System.Data.SqlDbType.Int);
            p11.Value = tprestacion;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p12.Value = codigo;
            cmd.Parameters.Add(p12);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DeducPrestacionesxFechaEmpSel";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dt;
        }
        public DataTable plnObtenerRubros(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnObtenerRubros";
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
        public DataTable ObtenerDetalleHorasExtrasxFecha(int filtro,int periodo,DateTime fechaini,DateTime fechafin,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnObtenerDetalleHorasExtrasxFecha";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@filtro", System.Data.SqlDbType.Int);
            p12.Value = filtro;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p13.Value = periodo;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p11.Value = fechafin;
            cmd.Parameters.Add(p11);

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
    }
}

