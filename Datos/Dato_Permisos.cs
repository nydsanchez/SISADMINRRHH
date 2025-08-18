using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Permisos
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
       public bool InsertarPermiso(int tipo, int diaHora, int tipoPermiso, int codigo_empleado, int codigo_depto,
            DateTime fechaInicial, DateTime fechaFinal, TimeSpan hi, TimeSpan hf, decimal cantdias,decimal canthoras, DateTime fechaPerm,
            string Observacion, string user,int ubicacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "PermisoVacacionesIns";
            cmd.CommandText = "PermisoIns";
            cmd.Connection = sqlConnection;
           
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p2.Value = codigo_empleado;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter coddepto = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            coddepto.Value = codigo_depto;
            cmd.Parameters.Add(coddepto);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fechaIni", System.Data.SqlDbType.Date);
            p3.Value = fechaInicial;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaFin", System.Data.SqlDbType.Date);
            p4.Value = fechaFinal;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@horaInicio", System.Data.SqlDbType.Time);
            p5.Value = hi;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@horaFin", System.Data.SqlDbType.Time);
            p6.Value = hf;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@cantidadDias", System.Data.SqlDbType.Decimal);
            p7.Value = cantdias;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter canthor = new SqlParameter("@canthoras", System.Data.SqlDbType.Decimal);
            canthor.Value = canthoras;
            cmd.Parameters.Add(canthor);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@fechaPerm", System.Data.SqlDbType.DateTime);
            p8.Value = fechaPerm;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@Observ", System.Data.SqlDbType.NChar);
            p9.Value = Observacion;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@Usuario", System.Data.SqlDbType.NChar);
            p10.Value = user;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@tipoPermiso", System.Data.SqlDbType.Int);
            p11.Value = tipoPermiso;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p12.Value = tipo;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@diaHora", System.Data.SqlDbType.Int);
            p13.Value = diaHora;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p14.Value = ubicacion;
            cmd.Parameters.Add(p14);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception EX)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;
        }


       public DataSet SeleccionarDatosEmpleado(int codEmple, DateTime fechaIni, DateTime fechaFin, string tipo, string idtipo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmple;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechaIni", System.Data.SqlDbType.Date);
            p2.Value = fechaIni;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fechaFin", System.Data.SqlDbType.Date);
            p3.Value = fechaFin;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p33 = new SqlParameter("@tipo", System.Data.SqlDbType.NChar);
            p33.Value = tipo;
            cmd.Parameters.Add(p33);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@idtipo", System.Data.SqlDbType.NChar);
            p4.Value = idtipo;
            cmd.Parameters.Add(p4);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "seleccionarDetalleVacaciones";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                               
                da.Fill(ds, "Vacaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return ds;
        }

       public bool editarPermiso(decimal cantidadEditAnt, decimal cantidadAct, int codEmpleado,
            DateTime fechaIni, DateTime fechaFin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PermisoVacacionesEditar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@cantidadEditAnt", System.Data.SqlDbType.Decimal);
            p1.Value = cantidadEditAnt;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@cantidadAct", System.Data.SqlDbType.Decimal);
            p2.Value = cantidadAct;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaIni", System.Data.SqlDbType.Date);
            p4.Value = fechaIni;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaFin", System.Data.SqlDbType.Date);
            p5.Value = fechaFin;
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
        public bool ActualizarSaldoVacaciones(int periodo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ActualizarSaldoVacaciones";
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

        public int validarEditElimPermiso(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "validarEditElimPermiso";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
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

       public bool eliminarPermisos(decimal dias, decimal horas, int codEmpleado,
            DateTime fechaIni, DateTime fechaFin, int idEmpresa, string tipoPermiso)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PermisoVacacionesEliminar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@dias", System.Data.SqlDbType.Decimal);
            p1.Value = dias;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@horas", System.Data.SqlDbType.Decimal);
            p2.Value = horas;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p3.Value = codEmpleado;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaIni", System.Data.SqlDbType.Date);
            p4.Value = fechaIni;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@fechaFin", System.Data.SqlDbType.Date);
            p5.Value = fechaFin;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@tipoPermiso", System.Data.SqlDbType.VarChar);
            p6.Value = tipoPermiso;
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

       public string obtenerNombreEmpleado(string codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerNombreEmpleado";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.VarChar);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            string rsp = "";

            try
            {
                cmd.Connection.Open();
                rsp = (string)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return "";
            }

            return rsp;
        }

        //AGREGADOS POR WBRAVO
        public dsPlanilla.dtPermisosDataTable PermisosSel(DateTime fechaini, DateTime fechafin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            dsPlanilla.dtPermisosDataTable dtPermisos = new dsPlanilla.dtPermisosDataTable();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PermisosSel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {
                da.Fill(dtPermisos);
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return dtPermisos;
        }
        public decimal PermisosVacEmpleadosxRangoSel(int codigo,DateTime fechacorte,int aplicaCorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();            

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p13.Value = codigo;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechacorte", System.Data.SqlDbType.DateTime);
            p1.Value = fechacorte;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@aplicaCorte", System.Data.SqlDbType.Int);
            p2.Value = aplicaCorte;
            cmd.Parameters.Add(p2);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "PermisosVacEmpleadosxRangoSel";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            decimal rsp = 0;

            try
            {
                cmd.Connection.Open();
                rsp = (decimal)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return -1;
            }

            return rsp;
        }
        public decimal obtenerVacacionesPagadasxEmp(int codigo, DateTime fechacorte, int aplicaCorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p13.Value = codigo;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechacorte", System.Data.SqlDbType.DateTime);
            p1.Value = fechacorte;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@aplicaCorte", System.Data.SqlDbType.Int);
            p2.Value = aplicaCorte;
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlanillaVacacionesEmpleadosSel";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            decimal rsp = 0;

            try
            {
                cmd.Connection.Open();
                rsp = (decimal)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return -1;
            }

            return rsp;
        }
        public decimal SubsidiosEmpleadosxRangoSel(int codigo, DateTime fechacorte, int aplicaCorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p13.Value = codigo;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechacorte", System.Data.SqlDbType.DateTime);
            p1.Value = fechacorte;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@aplicaCorte", System.Data.SqlDbType.Int);
            p2.Value = aplicaCorte;
            cmd.Parameters.Add(p2);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "SubsidiosEmpleadosxRangoSel";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            decimal rsp = 0;

            try
            {
                cmd.Connection.Open();
                rsp = (decimal)cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return -1;
            }

            return rsp;
        }
        public DataSet PermisosVacEmpleadoDetalleSel(int codigo, DateTime fechacorte, int aplicaCorte,int tipoPermiso, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p13.Value = codigo;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechacorte", System.Data.SqlDbType.DateTime);
            p1.Value = fechacorte;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@aplicaCorte", System.Data.SqlDbType.Int);
            p2.Value = aplicaCorte;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@tipoPermiso", System.Data.SqlDbType.Int);
            p22.Value = tipoPermiso;
            cmd.Parameters.Add(p22);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PermisosVacEmpleadoDetalleSel";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {

                da.Fill(ds, "Vacaciones");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }

            return ds;
        }

        public DataSet VacacionesPagadasxEmpDetalleSel(int codigo, DateTime fechacorte, int aplicaCorte, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p13.Value = codigo;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fechacorte", System.Data.SqlDbType.DateTime);
            p1.Value = fechacorte;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@aplicaCorte", System.Data.SqlDbType.Int);
            p2.Value = aplicaCorte;
            cmd.Parameters.Add(p2);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "VacacionesPagadasxEmpDetalleSel";
            cmd.Connection = sqlConnection;
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);

            try
            {

                da.Fill(ds, "Vacaciones");
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
