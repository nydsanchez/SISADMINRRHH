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
    public class Dato_Liquidacion
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        //MODIFICADO POR GRETHEL TERCERO 31-10-2016
        public bool AplicarDeduccionPrestamos(decimal cuota, int codEmpl, int id, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AplicarDeduccionPrestamos";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@UltimaCuota", System.Data.SqlDbType.Decimal);
            p2.Value = cuota;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p3.Value = id;
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

        public DataTable procesarIndemnizacion(int codEmpl, DateTime fechaRenuncia, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p2.Value = fechaRenuncia;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerIndemnizacion";
            cmd.Connection = sqlConnection;
            
            try
            {                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Indemnizacion");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        
        public bool  spInsertarLiquidacion(DateTime fechaIngreso, decimal salIndem, decimal salMayor, decimal salAguinaldo, decimal salAguinaldod, decimal salPromedio,
        decimal salVacaciones, decimal salVacacionesD, decimal horasT,decimal salHorast,decimal Neto,decimal m1,decimal m2,decimal m3,decimal m4,decimal m5,decimal m6,
        decimal m7, decimal m8, decimal m9, decimal m10, decimal m11, decimal m12, DateTime FecLiquida, int motivo, int codigo, decimal salMayorD, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarLiquidacion";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecingreso", System.Data.SqlDbType.Date);
            p1.Value = fechaIngreso;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@salidem", System.Data.SqlDbType.Decimal);
            p2.Value = salIndem;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@salmayor", System.Data.SqlDbType.Decimal);
            p3.Value = salMayor;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@salaguinaldo", System.Data.SqlDbType.Decimal);
            p4.Value = salAguinaldo;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@salaguinaldod", System.Data.SqlDbType.Decimal);
            p5.Value = salAguinaldod;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@salpromedio", System.Data.SqlDbType.Decimal);
            p6.Value = salPromedio;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@salvacaciones", System.Data.SqlDbType.Decimal);
            p7.Value = salVacaciones;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@salvacacionesd", System.Data.SqlDbType.Decimal);
            p8.Value = salVacacionesD;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@horast", System.Data.SqlDbType.Decimal);
            p9.Value = horasT;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@salhorast", System.Data.SqlDbType.Decimal);
            p10.Value = salHorast;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@neto", System.Data.SqlDbType.Decimal);
            p11.Value = Neto;
            cmd.Parameters.Add(p11);
            //System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@m1", System.Data.SqlDbType.Decimal);
            //p12.Value = m1;
            //cmd.Parameters.Add(p12);
            //System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@m2", System.Data.SqlDbType.Decimal);
            //p13.Value = m2;
            //cmd.Parameters.Add(p13);
            //System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@m3", System.Data.SqlDbType.Decimal);
            //p14.Value = m3;
            //cmd.Parameters.Add(p14);
            //System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@m4", System.Data.SqlDbType.Decimal);
            //p15.Value = m4;
            //cmd.Parameters.Add(p15);
            //System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@m5", System.Data.SqlDbType.Decimal);
            //p16.Value = m5;
            //cmd.Parameters.Add(p16);
            //System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@m6", System.Data.SqlDbType.Decimal);
            //p17.Value = m6;
            //cmd.Parameters.Add(p17);
            //System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@m7", System.Data.SqlDbType.Decimal);
            //p18.Value = m7;
            //cmd.Parameters.Add(p18);
            //System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@m8", System.Data.SqlDbType.Decimal);
            //p19.Value = m7;
            //cmd.Parameters.Add(p19);
            //System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@m9", System.Data.SqlDbType.Decimal);
            //p20.Value = m9;
            //cmd.Parameters.Add(p20);
            //System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@m10", System.Data.SqlDbType.Decimal);
            //p21.Value = m10;
            //cmd.Parameters.Add(p21);
            //System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@m11", System.Data.SqlDbType.Decimal);
            //p22.Value = m11;
            //cmd.Parameters.Add(p22);
            //System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@m12", System.Data.SqlDbType.Decimal);
            //p23.Value = m12;
            //cmd.Parameters.Add(p23);
            System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@fechaliquida", System.Data.SqlDbType.DateTime);
            p24.Value = FecLiquida;
            cmd.Parameters.Add(p24);
            System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@motivo", System.Data.SqlDbType.Int);
            p25.Value = motivo;
            cmd.Parameters.Add(p25);
            System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p26.Value = codigo;
            cmd.Parameters.Add(p26);
            System.Data.SqlClient.SqlParameter p27 = new SqlParameter("@salmayord", System.Data.SqlDbType.Decimal);
            p27.Value = salMayorD;
            cmd.Parameters.Add(p27);
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
       public bool spInsertarLiquidaciones(int codigo, DateTime FecLiquida, int motivo, decimal salMayor, decimal salMayorD, decimal salPromedio, decimal salPromedioD, decimal salAguinaldo, decimal salAguinaldod,
       decimal salVacaciones, decimal salVacacionesD, decimal salIndem, string HorasP, decimal SalPend, decimal Neto, DateTime FechaIngreso, decimal Inss, decimal IrVacaciones, decimal DeducPendiente,string observ,decimal indemnizaciondia,string usuario,
       decimal ingresos,decimal egresos,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarLiquidaciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@Codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@Fecha ", System.Data.SqlDbType.DateTime);
            p2.Value = FecLiquida;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@Motivo", System.Data.SqlDbType.Int);
            p3.Value = motivo;
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
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@Vacaciones", System.Data.SqlDbType.Decimal);
            p10.Value = salVacaciones;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@VacacionesDia", System.Data.SqlDbType.Decimal);
            p11.Value = salVacacionesD;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@Indemnizacion", System.Data.SqlDbType.Decimal);
            p12.Value = salIndem;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@HorasPend", System.Data.SqlDbType.NVarChar);
            p13.Value = HorasP;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@SalPend", System.Data.SqlDbType.Decimal);
            p14.Value = SalPend;
            cmd.Parameters.Add(p14);
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@Neto", System.Data.SqlDbType.NVarChar);
            p15.Value = Neto;
            cmd.Parameters.Add(p15);
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@FecIngreso", System.Data.SqlDbType.DateTime);
            p16.Value = FechaIngreso;
            cmd.Parameters.Add(p16);
            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@Inss", System.Data.SqlDbType.Decimal);
            p17.Value = Inss;
            cmd.Parameters.Add(p17);
            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@IRVacaciones", System.Data.SqlDbType.Decimal);
            p18.Value = IrVacaciones;
            cmd.Parameters.Add(p18);
            System.Data.SqlClient.SqlParameter p19= new SqlParameter("@DeducPendiente", System.Data.SqlDbType.Decimal);
            p19.Value = DeducPendiente;
            cmd.Parameters.Add(p19);
            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p20.Value = observ;
            cmd.Parameters.Add(p20);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@indemnizaciondia", System.Data.SqlDbType.Decimal);
            p21.Value = indemnizaciondia;
            cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@usuario", System.Data.SqlDbType.NChar);
            p22.Value = usuario;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@ingresos", System.Data.SqlDbType.Decimal);
            p23.Value = ingresos;
            cmd.Parameters.Add(p23);
            System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@egresos", System.Data.SqlDbType.Decimal);
            p24.Value = egresos;
            cmd.Parameters.Add(p24);

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
        public bool IngresosyDeduccionesLiqEliminar(int codigo,DateTime fechaliquidacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IngresosyDeduccionesLiqEliminar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@Codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaliquidacion", System.Data.SqlDbType.Date);
            p11.Value = fechaliquidacion;
            cmd.Parameters.Add(p11);

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
        public DataTable obtenerDiasLaborados(int codEmpl, DateTime fechaRenuncia, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpl", System.Data.SqlDbType.VarChar);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p2.Value = fechaRenuncia;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerDiasLaboradosLiquidacion";
            cmd.Connection = sqlConnection;
            
            try
            {               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Indemnizacion");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public bool procesarLiquidacion(int codEmpl, DateTime fechaRenuncia, decimal diasVac, decimal TVacaciones, decimal indemnizacion, decimal inssLaboral,
            decimal inssPatronal, decimal ir, int motivo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procesarLiquidacion";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpl", System.Data.SqlDbType.VarChar);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p2.Value = fechaRenuncia;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@diasVac", System.Data.SqlDbType.Decimal);
            p3.Value = diasVac;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@TVacaciones", System.Data.SqlDbType.Decimal);
            p4.Value = TVacaciones;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@indemnizacion", System.Data.SqlDbType.Decimal);
            p5.Value = indemnizacion;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@inssLaboral", System.Data.SqlDbType.Decimal);
            p6.Value = inssLaboral;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@inssPatronal", System.Data.SqlDbType.Decimal);
            p7.Value = inssLaboral;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@ir", System.Data.SqlDbType.Decimal);
            p8.Value = ir;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@motivo", System.Data.SqlDbType.Decimal);
            p9.Value = motivo;
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

        public DataSet cargarMotivos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "motivosRenunciaObtener";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
            
            try
            {                
                da.Fill(ds, "motivo");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataTable obtenerIngresosPendientes(int codEmpl, DateTime fechaRenuncia, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpl", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p2.Value = fechaRenuncia;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerIngresosPendientes";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
           
            try
            {                
                da.Fill(ds, "ingresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable obtenerTotalIngresos(int codEmpl, DateTime fechaRenuncia, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpl", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p2.Value = fechaRenuncia;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerTotalIngresos";
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
            return ds.Tables[0];
        }
        public DataTable obtenerTotalDeducciones(int codEmpl, DateTime fechaRenuncia, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpl", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p2.Value = fechaRenuncia;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerTotalDeducciones";
            cmd.Connection = sqlConnection;
           
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "egresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public DataTable obtenerDeduccionesPendientes(int codEmpleado, DateTime fechaRenuncia, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p2.Value = fechaRenuncia;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerDeduccionesPendientes";
            cmd.Connection = sqlConnection;
           
            try
            {               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "egresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public bool spInsertarLiquidacionMeses(int codigo,int anio,int mes,int diasMes,decimal salario,decimal incentivo,decimal beneficio,decimal ingreso,decimal promediodias,DateTime fechaliq, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarLiquidacionMeses";
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
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@FechaLiq", System.Data.SqlDbType.DateTime);
            p9.Value = fechaliq;
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

        public DataSet ObtenerMesesLiq(int codEmpl, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "spLiquidacionSelForm";
            cmd.CommandText = "spLiquidacionSel";
            cmd.Connection = sqlConnection;
            

            try
            {              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "liquidacion");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds;
        }

        public DataTable ObtenerPlanillasLiquidacion(int codEmpl, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPlanillasLiquidacion";
            cmd.Connection = sqlConnection;
            
            try
            {              
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionPlanillas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        //nuevo
        public DataTable plnobtenerdetalleplanillasxemp(DateTime fini,DateTime ffin,int depto1,int depto2,int todos, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fini;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p11.Value = ffin;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@depto1", System.Data.SqlDbType.Int);
            p2.Value = depto1;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@depto2", System.Data.SqlDbType.Int);
            p3.Value = depto2;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@todos", System.Data.SqlDbType.Int);
            p4.Value = todos;
            cmd.Parameters.Add(p4);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnobtenerdetalleplanillasxemp";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionPlanillas");
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable ObtenerPlanillasxMes(int codEmpl,DateTime fecini,DateTime fecfin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p2.Value = fecini;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p3.Value = fecfin;
            cmd.Parameters.Add(p3);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPlanillasxMes";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionPlanillas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable ObtenerPlanillasMesAguinaldo(int codEmpl,int mes, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@mes", System.Data.SqlDbType.Int);
            p2.Value = mes;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerPlanillasMesAguinaldo";
            cmd.Connection = sqlConnection;

            try
            {                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionPlanillas");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataTable MesesLaboradosCompletos(int codEmpl, int aplicacorte, string fechacorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@aplicacorte", System.Data.SqlDbType.Int);
            p11.Value = aplicacorte;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@fechacorte", System.Data.SqlDbType.VarChar);
            p21.Value = fechacorte;
            cmd.Parameters.Add(p21);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "MesesLaboradosCompletos";
            //cmd.CommandText = "MesesLaboradosCompletosTest"; 
            cmd.Connection = sqlConnection;
           
            try
            {               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionMeses");
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public DataSet ObtenerNetoxPlanillas(int codEmpl, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerNetoxPlanillas";
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
        public DataTable ObtenerIngresosxPlanillas(int codEmpl, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerIngresosxPlanillas";
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
        public DataTable spLiquidacionDatosEmp(int codEmpl,int bandera,  int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@bandera", System.Data.SqlDbType.Int);
            p2.Value = bandera;
            cmd.Parameters.Add(p2);

         

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spLiquidacionDatosEmp";
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
            return ds.Tables[0];
        }
        public DataTable plnObtenerLiquidacionesxfiltro(int filtro,DateTime fechaini,DateTime fechafin,int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@filtro", System.Data.SqlDbType.Int);
            p1.Value = filtro;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p2.Value = fechaini;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p21.Value = fechafin;
            cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p22.Value = codigo;
            cmd.Parameters.Add(p22);



            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnObtenerLiquidacionesxfiltro";
            cmd.Connection = sqlConnection;

            try
            {
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Liquidacion");
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }
        public bool plnLiquidacionesCerrar(int id, int codigo,  int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnLiquidacionesCerrar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p2.Value = id;
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
        //obtener dias pagados en el periodo pasado de aguinaldo
        public int ObtenerdiasAguinaldoPayEmp(int codEmpl, DateTime fecini,DateTime fecfin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fecini", System.Data.SqlDbType.Date);
            p2.Value = fecini;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@fecfin", System.Data.SqlDbType.Date);
            p21.Value = fecfin;
            cmd.Parameters.Add(p21);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerdiasAguinaldoPayEmp";
            cmd.Connection = sqlConnection;

            object obj;

            try
            {
                cmd.Connection.Open();
                obj = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return 0;
            }
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }

            return 0;
        }
        public bool spInsertarPendientesLiq(int codigo, int id_tipo, int tipo, decimal valor,int id, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spInsertarPendientesLiq";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@id_tipo", System.Data.SqlDbType.Int);
            p2.Value = id_tipo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoIngrDeduc", System.Data.SqlDbType.Int);
            p3.Value = tipo;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p4.Value = valor;
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
        public bool plnDeduccionesPendLiqIns(int codigo, DateTime fechaliquidacion, int tipo, decimal valor, int id, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnDeduccionesPendLiqIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaliquidacion", System.Data.SqlDbType.Date);
            p2.Value = fechaliquidacion;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipoDeduc", System.Data.SqlDbType.Int);
            p3.Value = tipo;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p4.Value = valor;
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

        public DataTable obtenerParametrosLiquidacion(DateTime fechaRenuncia, int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaRenuncia", System.Data.SqlDbType.Date);
            p1.Value = fechaRenuncia;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p2.Value = codEmpleado;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerParametrosLiquidacion";
            cmd.Connection = sqlConnection;
            
            try
            {               
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "LiquidacionMeses");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }
            return ds.Tables[0];
        }

        public DataTable spmatrizliqQuincenal(int codEmpl, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codempleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpl;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spmatrizliqQuincenal";
            cmd.Connection = sqlConnection;
            
            try
            {
                
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "MatrizLiq");
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
