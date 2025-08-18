using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Periodo
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public bool AgregarPeriodo(int nperiodo, int ubicacion, int mesSem1, DateTime desdeSem1,
            DateTime hastaSem1, int mesSem2, DateTime desdeSem2, DateTime hastaSem2, int tPeriodo, string user, int tipoPlanilla, bool consolidar,decimal factor, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PeriodosAgregar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p2.Value = nperiodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p3.Value = ubicacion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@mesSem1", System.Data.SqlDbType.Int);
            p4.Value = mesSem1;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@desdeSem1", System.Data.SqlDbType.Date);
            p5.Value = desdeSem1;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@hastaSem1", System.Data.SqlDbType.Date);
            p6.Value = hastaSem1;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@mesSem2", System.Data.SqlDbType.Int);
            p7.Value = mesSem2;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@desdeSem2", System.Data.SqlDbType.Date);
            p8.Value = desdeSem2;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@hastaSem2", System.Data.SqlDbType.Date);
            p9.Value = hastaSem2;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@tPeriodo", System.Data.SqlDbType.Int);
            p10.Value = tPeriodo;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p11.Value = user;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p12.Value = tipoPlanilla;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@consolidar", System.Data.SqlDbType.Bit);
            p13.Value = consolidar;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@factor", System.Data.SqlDbType.Decimal);
            p14.Value = factor;
            cmd.Parameters.Add(p14);

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


        public bool CerrarPeriodo(int nperiodo, string user, int idEmpresa, int tipoPlanilla)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PeriodoCerrar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p2.Value = nperiodo;
            cmd.Parameters.Add(p2);
          
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p11.Value = user;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p1.Value = tipoPlanilla;
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

        public string cargarProxPeriodoCatorc(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            //DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "seleccionarProximoPeriodoCatorcenal";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

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
                return "";
            }
            if (obj != null)
            {
                return obj.ToString();
            }

            return "";
        }
        
        public bool AgregarPeriodoXFecha(int nperiodo, int ubicacion, int mes, DateTime fechaIni, DateTime fechaF, int tPeriodo, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AgregarPeriodoXFecha";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p2.Value = nperiodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p3.Value = ubicacion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@mes", System.Data.SqlDbType.Int);
            p4.Value = mes;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaIni", System.Data.SqlDbType.Date);
            p5.Value = fechaIni;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@fechaF", System.Data.SqlDbType.Date);
            p6.Value = fechaF;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@tPeriodo", System.Data.SqlDbType.Int);
            p8.Value = tPeriodo;
            cmd.Parameters.Add(p8);
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

        //PERIODO QUINCENAL
        public bool AgregarPeriodoQuincenal(int nperiodo, int ubicacion, int mesSem1, DateTime desdeSem1,
            string hastaSem1, int mesSem2, string desdeSem2, DateTime hastaSem2, int tPeriodo, string user, int tipoPlanilla, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PeriodosAgregarQuincenal";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p2.Value = nperiodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p3.Value = ubicacion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@mesSem1", System.Data.SqlDbType.Int);
            p4.Value = mesSem1;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@desdeSem1", System.Data.SqlDbType.Date);
            p5.Value = desdeSem1;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@hastaSem1", System.Data.SqlDbType.VarChar);
            p6.Value = hastaSem1;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@mesSem2", System.Data.SqlDbType.VarChar);
            p7.Value = mesSem2;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@desdeSem2", System.Data.SqlDbType.VarChar);
            p8.Value = desdeSem2;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@hastaSem2", System.Data.SqlDbType.Date);
            p9.Value = hastaSem2;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@tPeriodo", System.Data.SqlDbType.Int);
            p10.Value = tPeriodo;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p11.Value = user;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p12.Value = tipoPlanilla;
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

        public bool AgregarPeriodoVacaciones(int nperiodo, int ubicacion, int mesSemana, DateTime desde, DateTime hasta, int tperiodo, string user, int tipoPlanilla,decimal factor, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AgregarPeriodoVacaciones";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p2.Value = nperiodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p3.Value = ubicacion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@mesSemana", System.Data.SqlDbType.Int);
            p4.Value = mesSemana;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@desde", System.Data.SqlDbType.Date);
            p5.Value = desde;
            cmd.Parameters.Add(p5);            
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@hasta", System.Data.SqlDbType.Date);
            p9.Value = hasta;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
            p10.Value = tperiodo;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p11.Value = user;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p12.Value = tipoPlanilla;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@factor", System.Data.SqlDbType.Decimal);
            p13.Value = factor;
            cmd.Parameters.Add(p13);

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

        public bool AgregarPeriodoAguinaldo(int nperiodo, int ubicacion, int mesSemana, DateTime desde, DateTime hasta, int tperiodo, string user, int tipoPlanilla,decimal factor, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "AgregarPeriodoAguinaldo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p3.Value = ubicacion;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p2.Value = nperiodo;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
            p10.Value = tperiodo;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@mesSemana", System.Data.SqlDbType.Int);
            p4.Value = mesSemana;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@desde", System.Data.SqlDbType.Date);
            p5.Value = desde;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@hasta", System.Data.SqlDbType.Date);
            p9.Value = hasta;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p11.Value = user;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipoPlanilla", System.Data.SqlDbType.Int);
            p12.Value = tipoPlanilla;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@factor", System.Data.SqlDbType.Decimal);
            p13.Value = factor;
            cmd.Parameters.Add(p13);

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
        //AGREGADOS POR WBRAVO
        //Dato_Periodo
        public dsPlanilla.dtPeriodoDataTable Sel(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p = new SqlParameter("@nperiodo", System.Data.SqlDbType.Int);
            p.Value = periodo;
            cmd.Parameters.Add(p);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PeriodoSel";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            dsPlanilla.dtPeriodoDataTable dt = new dsPlanilla.dtPeriodoDataTable();

            try
            {
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return dt;
            }

            return dt;
        }
        public dsPlanilla.dtPeriodoDataTable SeleccionarPeriodoCat(int tperiodo, int tplanilla,int ubicacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
            p.Value = tperiodo;
            cmd.Parameters.Add(p);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tplanilla", System.Data.SqlDbType.Int);
            p2.Value = tplanilla;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p21.Value = ubicacion;
            cmd.Parameters.Add(p21);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "seleccionarUltPeriodoAbiertoCatorcenal";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
            dsPlanilla.dtPeriodoDataTable dt = new dsPlanilla.dtPeriodoDataTable();

            try
            {
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return dt;
            }

            return dt;
        }
        public dsPlanilla.dtPeriodoDataTable SeleccionarPeriodoCerrado(int tperiodo, int tplanilla, int ubicacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p = new SqlParameter("@tperiodo", System.Data.SqlDbType.Int);
            p.Value = tperiodo;
            cmd.Parameters.Add(p);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@tplanilla", System.Data.SqlDbType.Int);
            p2.Value = tplanilla;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p21.Value = ubicacion;
            cmd.Parameters.Add(p21);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "seleccionarUltPeriodoCerrado";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            dsPlanilla.dtPeriodoDataTable dt = new dsPlanilla.dtPeriodoDataTable();

            try
            {
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return dt;
            }

            return dt;
        }
        public DataTable PlnPeriodoFiscalSel(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnPeriodoFiscalSel";
            cmd.Connection = sqlConnection;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                return dt;
            }
            return dt;
        }
    }
}
