using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Turnos
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public bool insertarTurno(int lunes, string horaEntLu, string horaSalLu, string horaComLu,
            int martes, string horaEntMar, string horaSalMar, string horaComMar, int miercoles, string horaEntMie,
            string horaSalMie, string horaComMie, int jueves, string horaEntJue, string horaSalJue, string horaComJue,
            int viernes, string horaEntVie, string horaSalVie, string horaComVie, int sabado, string horaEntSab, string horaSalSab,
            string horaComSab, int domingo, string horaEntDom, string horaSalDom, string horaComDom, string nombTurno,
            string minComodin, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TurnosIns";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@lunes", System.Data.SqlDbType.Int);
            p1.Value = lunes;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@horaEntLu", System.Data.SqlDbType.VarChar);
            if (horaEntLu ==  "")
            {
                horaEntLu = "0:00";
            }
            p2.Value = horaEntLu;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@horaSalLu", System.Data.SqlDbType.VarChar);
            if (horaSalLu == "")
            {
                horaSalLu = "0:00";
            }
            p3.Value = horaSalLu;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horaComLu", System.Data.SqlDbType.Decimal);
            if(horaComLu == "")
            {
                horaComLu = Convert.ToString(0.00m);
            }
            p4.Value = horaComLu;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@martes", System.Data.SqlDbType.Int);
            p5.Value = martes;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@horaEntMar", System.Data.SqlDbType.VarChar);
            if (horaEntMar == "")
            {
                horaEntMar = "0:00";
            }
            p6.Value = horaEntMar;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@horaSalMar", System.Data.SqlDbType.VarChar);
            if (horaSalMar == "")
            {
                horaSalMar = "0:00";
            }
            p7.Value = horaSalMar;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@horaComMar", System.Data.SqlDbType.Decimal);
            if (horaComMar == "")
            {
                horaComMar = Convert.ToString(0.00m);
            }
            p8.Value = horaComMar;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@miercoles", System.Data.SqlDbType.Int);
            p9.Value = miercoles;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@horaEntMie", System.Data.SqlDbType.VarChar);
            if (horaEntMie == "")
            {
                horaEntMie = "0:00";
            }
            p10.Value = horaEntMie;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@horaSalMie", System.Data.SqlDbType.VarChar);
            if (horaSalMie == "")
            {
                horaSalMie = "0:00";
            }
            p11.Value = horaSalMie;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@horaComMie", System.Data.SqlDbType.Decimal);
            if (horaComMie == "")
            {
                horaComMie = Convert.ToString(0.00m);
            }
            p12.Value = horaComMie;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@jueves", System.Data.SqlDbType.Int);
            p13.Value = jueves;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@horaEntJue", System.Data.SqlDbType.VarChar);
            if (horaEntJue == "")
            {
                horaEntJue = "0:00";
            }
            p14.Value = horaEntJue;
            cmd.Parameters.Add(p14);
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@horaSalJue", System.Data.SqlDbType.VarChar);
            if (horaSalJue == "")
            {
                horaSalJue = "0:00";
            }
            p15.Value = horaSalJue;
            cmd.Parameters.Add(p15);
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@horaComJue", System.Data.SqlDbType.Decimal);
            if (horaComJue == "")
            {
                horaComJue = Convert.ToString(0.00m);
            }
            p16.Value = horaComJue;
            cmd.Parameters.Add(p16);
            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@viernes", System.Data.SqlDbType.Int);
            p17.Value = viernes;
            cmd.Parameters.Add(p17);
            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@horaEntVie", System.Data.SqlDbType.VarChar);
            if (horaEntVie == "")
            {
                horaEntVie = "0:00";
            }
            p18.Value = horaEntVie;
            cmd.Parameters.Add(p18);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@horaSalVie", System.Data.SqlDbType.VarChar);
            if (horaSalVie == "")
            {
                horaSalVie = "0:00";
            }
            p19.Value = horaSalVie;
            cmd.Parameters.Add(p19);
            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@horaComVie", System.Data.SqlDbType.Decimal);
            if (horaComVie == "")
            {
                horaComVie = Convert.ToString(0.00m);
            }
            p20.Value = horaComVie;
            cmd.Parameters.Add(p20);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@sabado", System.Data.SqlDbType.Int);
            p21.Value = sabado;
            cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@horaEntSab", System.Data.SqlDbType.VarChar);
            if (horaEntSab == "")
            {
                horaEntSab = "0:00";
            }
            p22.Value = horaEntSab;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@horaSalSab", System.Data.SqlDbType.VarChar);
            if (horaSalSab == "")
            {
                horaSalSab = "0:00";
            }
            p23.Value = horaSalSab;
            cmd.Parameters.Add(p23);
            System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@horaComSab", System.Data.SqlDbType.Decimal);
            if (horaComSab == "")
            {
                horaComSab = Convert.ToString(0.00m);
            }
            p24.Value = horaComSab;
            cmd.Parameters.Add(p24);
            System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@domingo", System.Data.SqlDbType.Int);
            p25.Value = domingo;
            cmd.Parameters.Add(p25);
            System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@horaEntDom", System.Data.SqlDbType.VarChar);
            if (horaEntDom == "")
            {
                horaEntDom = "0:00";
            }
            p26.Value = horaEntDom;
            cmd.Parameters.Add(p26);
            System.Data.SqlClient.SqlParameter p27 = new SqlParameter("@horaSalDom", System.Data.SqlDbType.VarChar);
            if (horaSalDom == "")
            {
                horaSalDom = "0:00";
            }
            p27.Value = horaSalDom;
            cmd.Parameters.Add(p27);
            System.Data.SqlClient.SqlParameter p28 = new SqlParameter("@horaComDom", System.Data.SqlDbType.Decimal);
            if (horaComDom == "")
            {
                horaComDom = Convert.ToString(0.00m);
            }
            p28.Value = horaComDom;
            cmd.Parameters.Add(p28);
            System.Data.SqlClient.SqlParameter p29 = new SqlParameter("@nombTurno", System.Data.SqlDbType.VarChar);
            p29.Value = nombTurno;
            cmd.Parameters.Add(p29);
            System.Data.SqlClient.SqlParameter p30 = new SqlParameter("@minComodin", System.Data.SqlDbType.Int);
            p30.Value = minComodin;
            cmd.Parameters.Add(p30);

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

        public bool EditarTurno(int lunes, string horaEntLu, string horaSalLu, string horaComLu,
            int martes, string horaEntMar, string horaSalMar, string horaComMar, int miercoles, string horaEntMie,
            string horaSalMie, string horaComMie, int jueves, string horaEntJue, string horaSalJue, string horaComJue,
            int viernes, string horaEntVie, string horaSalVie, string horaComVie, int sabado, string horaEntSab, string horaSalSab,
            string horaComSab, int domingo, string horaEntDom, string horaSalDom, string horaComDom, string nombTurno,
            string minComodin, string tempNombTurno, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TurnoEdit";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@lunes", System.Data.SqlDbType.Int);
            p1.Value = lunes;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@horaEntLu", System.Data.SqlDbType.VarChar);
            if (horaEntLu == "")
            {
                horaEntLu = "0:00";
            }
            p2.Value = horaEntLu;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@horaSalLu", System.Data.SqlDbType.VarChar);
            if (horaSalLu == "")
            {
                horaSalLu = "0:00";
            }
            p3.Value = horaSalLu;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horaComLu", System.Data.SqlDbType.Decimal);
            if (horaComLu == "")
            {
                horaComLu = Convert.ToString(0.00m);
            }
            p4.Value = horaComLu;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@martes", System.Data.SqlDbType.Int);
            p5.Value = martes;
            cmd.Parameters.Add(p5);
            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@horaEntMar", System.Data.SqlDbType.VarChar);
            if (horaEntMar == "")
            {
                horaEntMar = "0:00";
            }
            p6.Value = horaEntMar;
            cmd.Parameters.Add(p6);
            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@horaSalMar", System.Data.SqlDbType.VarChar);
            if (horaSalMar == "")
            {
                horaSalMar = "0:00";
            }
            p7.Value = horaSalMar;
            cmd.Parameters.Add(p7);
            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@horaComMar", System.Data.SqlDbType.Decimal);
            if (horaComMar == "")
            {
                horaComMar = Convert.ToString(0.00m);
            }
            p8.Value = horaComMar;
            cmd.Parameters.Add(p8);
            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@miercoles", System.Data.SqlDbType.Int);
            p9.Value = miercoles;
            cmd.Parameters.Add(p9);
            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@horaEntMie", System.Data.SqlDbType.VarChar);
            if (horaEntMie == "")
            {
                horaEntMie = "0:00";
            }
            p10.Value = horaEntMie;
            cmd.Parameters.Add(p10);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@horaSalMie", System.Data.SqlDbType.VarChar);
            if (horaSalMie == "")
            {
                horaSalMie = "0:00";
            }
            p11.Value = horaSalMie;
            cmd.Parameters.Add(p11);
            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@horaComMie", System.Data.SqlDbType.Decimal);
            if (horaComMie == "")
            {
                horaComMie = Convert.ToString(0.00m);
            }
            p12.Value = horaComMie;
            cmd.Parameters.Add(p12);
            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@jueves", System.Data.SqlDbType.Int);
            p13.Value = jueves;
            cmd.Parameters.Add(p13);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@horaEntJue", System.Data.SqlDbType.VarChar);
            if (horaEntJue == "")
            {
                horaEntJue = "0:00";
            }
            p14.Value = horaEntJue;
            cmd.Parameters.Add(p14);
            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@horaSalJue", System.Data.SqlDbType.VarChar);
            if (horaSalJue == "")
            {
                horaSalJue = "0:00";
            }
            p15.Value = horaSalJue;
            cmd.Parameters.Add(p15);
            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@horaComJue", System.Data.SqlDbType.Decimal);
            if (horaComJue == "")
            {
                horaComJue = Convert.ToString(0.00m);
            }
            p16.Value = horaComJue;
            cmd.Parameters.Add(p16);
            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@viernes", System.Data.SqlDbType.Int);
            p17.Value = viernes;
            cmd.Parameters.Add(p17);
            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@horaEntVie", System.Data.SqlDbType.VarChar);
            if (horaEntVie == "")
            {
                horaEntVie = "0:00";
            }
            p18.Value = horaEntVie;
            cmd.Parameters.Add(p18);
            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@horaSalVie", System.Data.SqlDbType.VarChar);
            if (horaSalVie == "")
            {
                horaSalVie = "0:00";
            }
            p19.Value = horaSalVie;
            cmd.Parameters.Add(p19);
            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@horaComVie", System.Data.SqlDbType.Decimal);
            if (horaComVie == "")
            {
                horaComVie = Convert.ToString(0.00m);
            }
            p20.Value = horaComVie;
            cmd.Parameters.Add(p20);
            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@sabado", System.Data.SqlDbType.Int);
            p21.Value = sabado;
            cmd.Parameters.Add(p21);
            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@horaEntSab", System.Data.SqlDbType.VarChar);
            if (horaEntSab == "")
            {
                horaEntSab = "0:00";
            }
            p22.Value = horaEntSab;
            cmd.Parameters.Add(p22);
            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@horaSalSab", System.Data.SqlDbType.VarChar);
            if (horaSalSab == "")
            {
                horaSalSab = "0:00";
            }
            p23.Value = horaSalSab;
            cmd.Parameters.Add(p23);
            System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@horaComSab", System.Data.SqlDbType.Decimal);
            if (horaComSab == "")
            {
                horaComSab = Convert.ToString(0.00m);
            }
            p24.Value = horaComSab;
            cmd.Parameters.Add(p24);
            System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@domingo", System.Data.SqlDbType.Int);
            p25.Value = domingo;
            cmd.Parameters.Add(p25);
            System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@horaEntDom", System.Data.SqlDbType.VarChar);
            if (horaEntDom == "")
            {
                horaEntDom = "0:00";
            }
            p26.Value = horaEntDom;
            cmd.Parameters.Add(p26);
            System.Data.SqlClient.SqlParameter p27 = new SqlParameter("@horaSalDom", System.Data.SqlDbType.VarChar);
            if (horaSalDom == "")
            {
                horaSalDom = "0:00";
            }
            p27.Value = horaSalDom;
            cmd.Parameters.Add(p27);
            System.Data.SqlClient.SqlParameter p28 = new SqlParameter("@horaComDom", System.Data.SqlDbType.Decimal);
            if (horaComDom == "")
            {
                horaComDom = Convert.ToString(0.00m);
            }
            p28.Value = horaComDom;
            cmd.Parameters.Add(p28);
            System.Data.SqlClient.SqlParameter p29 = new SqlParameter("@nombTurno", System.Data.SqlDbType.VarChar);
            p29.Value = nombTurno;
            cmd.Parameters.Add(p29);
            System.Data.SqlClient.SqlParameter p30 = new SqlParameter("@minComodin", System.Data.SqlDbType.Int);
            p30.Value = minComodin;
            cmd.Parameters.Add(p30);
            System.Data.SqlClient.SqlParameter p31 = new SqlParameter("@tempNombTurno", System.Data.SqlDbType.VarChar);
            p31.Value = tempNombTurno;
            cmd.Parameters.Add(p31);
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

        public bool EliminarTurno(string tempNombTurno, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TurnoElim";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@tempNombTurno", System.Data.SqlDbType.VarChar);
            p1.Value = tempNombTurno;
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

        public DataSet obtenerTurnos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SeleccionarTurnos";
            cmd.Connection = sqlConnection;
           
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "turnos");

            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        //AGREGADO POR WBRAVO
        public dsPlanilla.dtTurnoDiaDataTable Todos(int idEmpresa)
        {
            dsPlanilla.dtTurnoDiaDataTable ds = new dsPlanilla.dtTurnoDiaDataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TurnoTodos";
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
        //AGREGADO POR WMEMBRENO
        public DataTable obtenerTodosTurnos(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosSelect";
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

        public bool TurnosInsert(int idEmpresa, string nombre,decimal hrsturno, int bonus, int tipo, int horasseptimo,bool consolidar)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosInsert";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p1.Value = nombre;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@hrsturno", System.Data.SqlDbType.Decimal);
            p11.Value = hrsturno;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@bonus", System.Data.SqlDbType.Int);
            p2.Value = bonus;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p3.Value = tipo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horaseptimo", System.Data.SqlDbType.Int);
            p4.Value = horasseptimo;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@consolidar", System.Data.SqlDbType.Bit);
            p5.Value = consolidar;
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

        public bool PlnTurnosUpdate(int idEmpresa, int id, string nombre,decimal hrsturno, int bonus, int tipo, int horasseptimo,bool consolidar)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosUpdate";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombre", System.Data.SqlDbType.VarChar);
            p1.Value = nombre;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@hrsturno", System.Data.SqlDbType.Decimal);
            p11.Value = hrsturno;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@bonus", System.Data.SqlDbType.Int);
            p2.Value = bonus;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p3.Value = tipo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@horaseptimo", System.Data.SqlDbType.Int);
            p4.Value = horasseptimo;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@consolidar", System.Data.SqlDbType.Bit);
            p5.Value = consolidar;
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

        public bool PlnTurnosDelete(int idEmpresa, int id)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosDelete";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

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

        public bool PlnTurnosDiasinsert(int idEmpresa, int id, int diasemana, DateTime horaIni, DateTime HoraFin, int almuerzo)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosDiasinsert";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@diasemana", System.Data.SqlDbType.Int);
            p1.Value = diasemana;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@horaini", System.Data.SqlDbType.DateTime);
            p2.Value = horaIni;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@horaFin", System.Data.SqlDbType.DateTime);
            p3.Value = HoraFin;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@almuerzo", System.Data.SqlDbType.Int);
            p4.Value = almuerzo;
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

        public bool PlnTurnosDiasUpdate(int idEmpresa, int id, int diasemana, DateTime horaIni, DateTime HoraFin, int almuerzo)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosDiasUpdate";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@diasemana", System.Data.SqlDbType.Int);
            p1.Value = diasemana;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@horaini", System.Data.SqlDbType.DateTime);
            p2.Value = horaIni;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@horaFin", System.Data.SqlDbType.DateTime);
            p3.Value = HoraFin;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@almuerzo", System.Data.SqlDbType.Int);
            p4.Value = almuerzo;
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

        public bool PlnTurnosDiasDelete(int idEmpresa, int id, int diasemana)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosDiasDelete";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@diasemana", System.Data.SqlDbType.Int);
            p1.Value = diasemana;
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


        public bool PlnTurnosDiasDeleteAll(int idEmpresa, int id)
        {

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosDiasDeleteAll";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

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

        public DataTable PlnTurnosDiasSelect(int idEmpresa, int id)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTurnosDiasSelect";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p0 = new SqlParameter("@id", System.Data.SqlDbType.Int);
            p0.Value = id;
            cmd.Parameters.Add(p0);

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
    }
}
