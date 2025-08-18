using System;
using System.Data.SqlClient;
using System.Data;


namespace Datos
{
    public class Dato_Empleados
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        #endregion
        public DataTable ObtenerDetalleEmpleados(int codEmple, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmple;
            cmd.Parameters.Add(p1);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDetalleEmpleado";
            cmd.Connection = sqlConnection;
           
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
            return ds.Tables[0];
        }
        public string SelNombreCompleto(int codEmple, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmple;
            cmd.Parameters.Add(p1);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnEmpleadoSelNombreCompleto";
            cmd.Connection = sqlConnection;

            string str="";
            object obj = null;

            try
            {
                cmd.Connection.Open();
                obj = cmd.ExecuteScalar();
                cmd.Connection.Close();
            }
            catch (System.Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return ex.Message;
            }

            if(obj == null)
            {
                return "Codigo no encontrado.";
            }

            str = (string)obj;

            return str.Trim();
        }

        public DataTable ObtenerDetalleEmpleadoxNombre(string nombreEmp, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@nombreEmp", System.Data.SqlDbType.VarChar);
            p1.Value = nombreEmp;
            cmd.Parameters.Add(p1);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerDetalleEmpleadoxNombre";
            cmd.Connection = sqlConnection;

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

            return ds.Tables[0];
        }

        public DataTable HistorialContratacionXEmpleado(string doc_identidad, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@cedula_identidad", System.Data.SqlDbType.NVarChar);
            p1.Value = doc_identidad;
            cmd.Parameters.Add(p1);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "HistorialContratacionXEmpleado";
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

        public bool EditarEmpleado(int codEmpleado, string primerNomb, string segundoNomb, string primerApellido, string segundApellido, string nombreCompleto, string sexo, int pais, int departamento, int municipio,
            DateTime fechaNac, string numCedula, DateTime emiteCed, DateTime venceCed, string operacion, int nivelAcademico, string numInss, int ubicacion, int proceso, int cargo, DateTime primerIngreso,
            int estadoEmpl, DateTime fechaIngr, DateTime fechaEgrs, int moneda, decimal salario, string cuentaContable, decimal subsidio, string cuentaBanc, decimal incentivo, int turno, int tipoContrato,
            int tipoSalario, int liquidado, bool marca, bool ganaExtras, string telefono, string celular, string correo, int estadoCivil, int tipoCasa, string viveCon, int domiciMunc, string direccion,
            string nombreEmerg, string telfEmerg, string parentesco, string direcEmerg, string nombPadre, string numCedPadre, string vivePadre, string nombMadre, string numCedMadre,
            string viveMadre, string nombEsps, string numCedEsps, string nombreHijo1, string lugarNacH1, DateTime fechaNacH1, string sexoH1, string nombreHijo2, string lugarNacH2, DateTime fechaNacH2, string sexoH2,
            string nombreHijo3, string lugarNacH3, DateTime fechaNacH3, string sexoH3, string nombreHijo4, string lugarNacH4, DateTime fechaNacH4, string sexoH4, string empAnterior1, string verfEmpAnt1, string observEmpAnt1,
            decimal ultimoSalarioEmp1, string verfUltSalaEm1, string observSalAntEm1, string cargoEmp1, string verfCargoEmp1, string observUltCargoEmp1, string motivoSalidaEmp1, string verfMotvSal1, string observMotvSal1,
            DateTime fechaIngEmp1, string verfFechaIngEm1, string observFechaIngEm1, DateTime fechaEgrEmp1, string verfFechEgrEm1, string obsercFechEgrEm1, string empAnterior2, decimal UltSalarioEmp2, string ultCargoEmp2,
            string motSalidEmp2, DateTime fechaIngEmp2, DateTime fechaEgrsEmp2, string referenciaPers, string verfRefPers, string observRefPers, string parentsRefPers, string verfParentRef, string observRefParnRef, string numTelfRefPers,
            string verfNumTelfRef, string observNumTelRef, string tiempoConcRef, string verfTiempCncRef, string observTiempCncRef, string padeceEnfermedad, string tipoEnfermedad, string enfermedadDesde, string descEstudios, string centroEstudios,
            //string fechIngHistoEgr, string fechaEgrsHistoEg, string ubicHistoEgrs, string deptoHistoEgrs, string cargoHistoEgrs, string motvHistoEgrs, 
            string nombreJefe, string observacion, int idEmpresa, string user, bool pagoVacacion, bool discapacidad,string cuentaMall,bool flexitime,bool credito,int domicdepto,bool multitask
            )
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DetalleEmpleadoEditar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@primerNomb", System.Data.SqlDbType.VarChar);
            p3.Value = primerNomb;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@segundoNomb", System.Data.SqlDbType.VarChar);
            p4.Value = segundoNomb;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@primerApellido", System.Data.SqlDbType.VarChar);
            p5.Value = primerApellido;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@segundApellido", System.Data.SqlDbType.VarChar);
            p6.Value = segundApellido;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p124 = new SqlParameter("@nombreCompleto", System.Data.SqlDbType.VarChar);
            p124.Value = nombreCompleto;
            cmd.Parameters.Add(p124);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@sexo", System.Data.SqlDbType.Char);
            p7.Value = sexo;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@pais", System.Data.SqlDbType.Int);
            p8.Value = pais;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@departamento", System.Data.SqlDbType.Int);
            p9.Value = departamento;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@municipio", System.Data.SqlDbType.Int);
            p10.Value = municipio;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaNac", System.Data.SqlDbType.Date);
            p11.Value = fechaNac;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@numCedula", System.Data.SqlDbType.VarChar);
            p12.Value = numCedula;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@emiteCed", System.Data.SqlDbType.Date);
            p13.Value = emiteCed;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@venceCed", System.Data.SqlDbType.Date);
            p14.Value = venceCed;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@operacion", System.Data.SqlDbType.VarChar);
            p15.Value = operacion;
            cmd.Parameters.Add(p15);

            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@nivelAcademico", System.Data.SqlDbType.Int);
            p16.Value = nivelAcademico;
            cmd.Parameters.Add(p16);

            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@numInss", System.Data.SqlDbType.VarChar);
            p17.Value = numInss;
            cmd.Parameters.Add(p17);

            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p18.Value = ubicacion;
            cmd.Parameters.Add(p18);

            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@proceso", System.Data.SqlDbType.Int);
            p19.Value = proceso;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@Idcargo", System.Data.SqlDbType.Int);
            p20.Value = cargo;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@primerIngreso", System.Data.SqlDbType.Date);
            p21.Value = primerIngreso;
            cmd.Parameters.Add(p21);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@estadoEmpl", System.Data.SqlDbType.Int);
            p22.Value = estadoEmpl;
            cmd.Parameters.Add(p22);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@fechaIngr", System.Data.SqlDbType.Date);
            p23.Value = fechaIngr;
            cmd.Parameters.Add(p23);

            System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@fechaEgrs", System.Data.SqlDbType.Date);
            p25.Value = fechaEgrs;
            cmd.Parameters.Add(p25);

            System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@moneda", System.Data.SqlDbType.Int);
            p26.Value = moneda;
            cmd.Parameters.Add(p26);

            System.Data.SqlClient.SqlParameter p27 = new SqlParameter("@salario", System.Data.SqlDbType.Decimal);
            p27.Value = salario;
            cmd.Parameters.Add(p27);

            System.Data.SqlClient.SqlParameter p28 = new SqlParameter("@cuentaContable", System.Data.SqlDbType.VarChar);
            p28.Value = cuentaContable;
            cmd.Parameters.Add(p28);

            System.Data.SqlClient.SqlParameter p29 = new SqlParameter("@subsidio", System.Data.SqlDbType.Decimal);
            p29.Value = subsidio;
            cmd.Parameters.Add(p29);

            System.Data.SqlClient.SqlParameter p30 = new SqlParameter("@cuentaBanc", System.Data.SqlDbType.VarChar);
            p30.Value = cuentaBanc;
            cmd.Parameters.Add(p30);

            System.Data.SqlClient.SqlParameter p138 = new SqlParameter("@cuentaMall", System.Data.SqlDbType.VarChar);
            p138.Value = cuentaMall;
            cmd.Parameters.Add(p138);

            System.Data.SqlClient.SqlParameter p31 = new SqlParameter("@incentivo", System.Data.SqlDbType.Decimal);
            p31.Value = incentivo;
            cmd.Parameters.Add(p31);

            System.Data.SqlClient.SqlParameter p32 = new SqlParameter("@turno", System.Data.SqlDbType.Int);
            p32.Value = turno;
            cmd.Parameters.Add(p32);

            System.Data.SqlClient.SqlParameter p33 = new SqlParameter("@tipoContrato", System.Data.SqlDbType.Int);
            p33.Value = tipoContrato;
            cmd.Parameters.Add(p33);

            System.Data.SqlClient.SqlParameter p34 = new SqlParameter("@tipoSalario", System.Data.SqlDbType.Int);
            p34.Value = tipoSalario;
            cmd.Parameters.Add(p34);

            System.Data.SqlClient.SqlParameter p35 = new SqlParameter("@liquidado", System.Data.SqlDbType.Int);
            p35.Value = liquidado;
            cmd.Parameters.Add(p35);

            System.Data.SqlClient.SqlParameter p36 = new SqlParameter("@marca", System.Data.SqlDbType.Bit);
            p36.Value = marca;
            cmd.Parameters.Add(p36);

            System.Data.SqlClient.SqlParameter p37 = new SqlParameter("@ganaExtras", System.Data.SqlDbType.Bit);
            p37.Value = ganaExtras;
            cmd.Parameters.Add(p37);

            System.Data.SqlClient.SqlParameter p38 = new SqlParameter("@telefono", System.Data.SqlDbType.VarChar);
            p38.Value = telefono;
            cmd.Parameters.Add(p38);

            System.Data.SqlClient.SqlParameter p39 = new SqlParameter("@celular", System.Data.SqlDbType.VarChar);
            p39.Value = celular;
            cmd.Parameters.Add(p39);

            System.Data.SqlClient.SqlParameter p40 = new SqlParameter("@correo", System.Data.SqlDbType.VarChar);
            p40.Value = correo;
            cmd.Parameters.Add(p40);

            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@estadoCivil", System.Data.SqlDbType.Int);
            p41.Value = estadoCivil;
            cmd.Parameters.Add(p41);

            System.Data.SqlClient.SqlParameter p42 = new SqlParameter("@tipoCasa", System.Data.SqlDbType.Int);
            p42.Value = tipoCasa;
            cmd.Parameters.Add(p42);

            System.Data.SqlClient.SqlParameter p43 = new SqlParameter("@viveCon", System.Data.SqlDbType.VarChar);
            p43.Value = viveCon;
            cmd.Parameters.Add(p43);

            System.Data.SqlClient.SqlParameter p44 = new SqlParameter("@domiciMunc", System.Data.SqlDbType.Int);
            p44.Value = domiciMunc;
            cmd.Parameters.Add(p44);

            System.Data.SqlClient.SqlParameter p45 = new SqlParameter("@direccion", System.Data.SqlDbType.VarChar);
            p45.Value = direccion;
            cmd.Parameters.Add(p45);

            System.Data.SqlClient.SqlParameter p48 = new SqlParameter("@nombreEmerg", System.Data.SqlDbType.VarChar);
            p48.Value = nombreEmerg;
            cmd.Parameters.Add(p48);

            System.Data.SqlClient.SqlParameter p49 = new SqlParameter("@telfEmerg", System.Data.SqlDbType.VarChar);
            p49.Value = telfEmerg;
            cmd.Parameters.Add(p49);

            System.Data.SqlClient.SqlParameter p50 = new SqlParameter("@parentesco", System.Data.SqlDbType.VarChar);
            p50.Value = parentesco;
            cmd.Parameters.Add(p50);

            System.Data.SqlClient.SqlParameter p51 = new SqlParameter("@direcEmerg", System.Data.SqlDbType.VarChar);
            p51.Value = direcEmerg;
            cmd.Parameters.Add(p51);

            System.Data.SqlClient.SqlParameter p52 = new SqlParameter("@nombPadre", System.Data.SqlDbType.VarChar);
            p52.Value = nombPadre;
            cmd.Parameters.Add(p52);

            System.Data.SqlClient.SqlParameter p53 = new SqlParameter("@numCedPadre", System.Data.SqlDbType.VarChar);
            p53.Value = numCedPadre;
            cmd.Parameters.Add(p53);

            System.Data.SqlClient.SqlParameter p54 = new SqlParameter("@vivePadre", System.Data.SqlDbType.VarChar);
            p54.Value = vivePadre;
            cmd.Parameters.Add(p54);

            System.Data.SqlClient.SqlParameter p55 = new SqlParameter("@nombMadre", System.Data.SqlDbType.VarChar);
            p55.Value = nombMadre;
            cmd.Parameters.Add(p55);

            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@numCedMadre", System.Data.SqlDbType.VarChar);
            p56.Value = numCedMadre;
            cmd.Parameters.Add(p56);

            System.Data.SqlClient.SqlParameter p57 = new SqlParameter("@viveMadre", System.Data.SqlDbType.VarChar);
            p57.Value = viveMadre;
            cmd.Parameters.Add(p57);

            System.Data.SqlClient.SqlParameter p58 = new SqlParameter("@nombEsps", System.Data.SqlDbType.VarChar);
            p58.Value = nombEsps;
            cmd.Parameters.Add(p58);

            System.Data.SqlClient.SqlParameter p59 = new SqlParameter("@numCedEsps", System.Data.SqlDbType.VarChar);
            p59.Value = numCedEsps;
            cmd.Parameters.Add(p59);

            System.Data.SqlClient.SqlParameter p60 = new SqlParameter("@nombreHijo1", System.Data.SqlDbType.VarChar);
            p60.Value = nombreHijo1;
            cmd.Parameters.Add(p60);

            System.Data.SqlClient.SqlParameter p61 = new SqlParameter("@lugarNacH1", System.Data.SqlDbType.VarChar);
            p61.Value = lugarNacH1;
            cmd.Parameters.Add(p61);

            System.Data.SqlClient.SqlParameter p62 = new SqlParameter("@fechaNacH1", System.Data.SqlDbType.Date);
            p62.Value = fechaNacH1;
            cmd.Parameters.Add(p62);

            System.Data.SqlClient.SqlParameter p63 = new SqlParameter("@sexoH1", System.Data.SqlDbType.VarChar);
            p63.Value = sexoH1;
            cmd.Parameters.Add(p63);

            System.Data.SqlClient.SqlParameter p64 = new SqlParameter("@nombreHijo2", System.Data.SqlDbType.VarChar);
            p64.Value = nombreHijo2;
            cmd.Parameters.Add(p64);

            System.Data.SqlClient.SqlParameter p65 = new SqlParameter("@lugarNacH2", System.Data.SqlDbType.VarChar);
            p65.Value = lugarNacH2;
            cmd.Parameters.Add(p65);

            System.Data.SqlClient.SqlParameter p66 = new SqlParameter("@fechaNacH2", System.Data.SqlDbType.Date);
            p66.Value = fechaNacH2;
            cmd.Parameters.Add(p66);

            System.Data.SqlClient.SqlParameter p67 = new SqlParameter("@sexoH2", System.Data.SqlDbType.VarChar);
            p67.Value = sexoH2;
            cmd.Parameters.Add(p67);

            System.Data.SqlClient.SqlParameter p68 = new SqlParameter("@nombreHijo3", System.Data.SqlDbType.VarChar);
            p68.Value = nombreHijo3;
            cmd.Parameters.Add(p68);

            System.Data.SqlClient.SqlParameter p69 = new SqlParameter("@lugarNacH3", System.Data.SqlDbType.VarChar);
            p69.Value = lugarNacH3;
            cmd.Parameters.Add(p69);

            System.Data.SqlClient.SqlParameter p70 = new SqlParameter("@fechaNacH3", System.Data.SqlDbType.Date);
            p70.Value = fechaNacH3;
            cmd.Parameters.Add(p70);

            System.Data.SqlClient.SqlParameter p71 = new SqlParameter("@sexoH3", System.Data.SqlDbType.VarChar);
            p71.Value = sexoH3;
            cmd.Parameters.Add(p71);

            System.Data.SqlClient.SqlParameter p72 = new SqlParameter("@nombreHijo4", System.Data.SqlDbType.VarChar);
            p72.Value = nombreHijo4;
            cmd.Parameters.Add(p72);

            System.Data.SqlClient.SqlParameter p73 = new SqlParameter("@lugarNacH4", System.Data.SqlDbType.VarChar);
            p73.Value = lugarNacH4;
            cmd.Parameters.Add(p73);

            System.Data.SqlClient.SqlParameter p74 = new SqlParameter("@fechaNacH4", System.Data.SqlDbType.Date);
            p74.Value = fechaNacH4;
            cmd.Parameters.Add(p74);

            System.Data.SqlClient.SqlParameter p75 = new SqlParameter("@sexoH4", System.Data.SqlDbType.VarChar);
            p75.Value = sexoH4;
            cmd.Parameters.Add(p75);

            System.Data.SqlClient.SqlParameter p76 = new SqlParameter("@empAnterior1", System.Data.SqlDbType.VarChar);
            p76.Value = empAnterior1;
            cmd.Parameters.Add(p76);

            System.Data.SqlClient.SqlParameter p77 = new SqlParameter("@verfEmpAnt1", System.Data.SqlDbType.VarChar);
            p77.Value = verfEmpAnt1;
            cmd.Parameters.Add(p77);

            System.Data.SqlClient.SqlParameter p78 = new SqlParameter("@observEmpAnt1", System.Data.SqlDbType.VarChar);
            p78.Value = observEmpAnt1;
            cmd.Parameters.Add(p78);

            System.Data.SqlClient.SqlParameter p79 = new SqlParameter("@ultimoSalarioEmp1", System.Data.SqlDbType.Decimal);
            p79.Value = ultimoSalarioEmp1;
            cmd.Parameters.Add(p79);

            System.Data.SqlClient.SqlParameter p80 = new SqlParameter("@verfUltSalaEm1", System.Data.SqlDbType.VarChar);
            p80.Value = verfUltSalaEm1;
            cmd.Parameters.Add(p80);

            System.Data.SqlClient.SqlParameter p81 = new SqlParameter("@observSalAntEm1", System.Data.SqlDbType.VarChar);
            p81.Value = observSalAntEm1;
            cmd.Parameters.Add(p81);

            System.Data.SqlClient.SqlParameter p82 = new SqlParameter("@cargoEmp1", System.Data.SqlDbType.VarChar);
            p82.Value = cargoEmp1;
            cmd.Parameters.Add(p82);

            System.Data.SqlClient.SqlParameter p83 = new SqlParameter("@verfCargoEmp1", System.Data.SqlDbType.VarChar);
            p83.Value = verfCargoEmp1;
            cmd.Parameters.Add(p83);

            System.Data.SqlClient.SqlParameter p84 = new SqlParameter("@observUltCargoEmp1", System.Data.SqlDbType.VarChar);
            p84.Value = observUltCargoEmp1;
            cmd.Parameters.Add(p84);

            System.Data.SqlClient.SqlParameter p85 = new SqlParameter("@motivoSalidaEmp1", System.Data.SqlDbType.VarChar);
            p85.Value = motivoSalidaEmp1;
            cmd.Parameters.Add(p85);

            System.Data.SqlClient.SqlParameter p86 = new SqlParameter("@verfMotvSal1", System.Data.SqlDbType.VarChar);
            p86.Value = verfMotvSal1;
            cmd.Parameters.Add(p86);

            System.Data.SqlClient.SqlParameter p87 = new SqlParameter("@observMotvSal1", System.Data.SqlDbType.VarChar);
            p87.Value = observMotvSal1;
            cmd.Parameters.Add(p87);

            System.Data.SqlClient.SqlParameter p88 = new SqlParameter("@fechaIngEmp1", System.Data.SqlDbType.Date);
            p88.Value = fechaIngEmp1;
            cmd.Parameters.Add(p88);

            System.Data.SqlClient.SqlParameter p89 = new SqlParameter("@verfFechaIngEm1", System.Data.SqlDbType.VarChar);
            p89.Value = verfFechaIngEm1;
            cmd.Parameters.Add(p89);

            System.Data.SqlClient.SqlParameter p90 = new SqlParameter("@observFechaIngEm1", System.Data.SqlDbType.VarChar);
            p90.Value = observFechaIngEm1;
            cmd.Parameters.Add(p90);

            System.Data.SqlClient.SqlParameter p91 = new SqlParameter("@fechaEgrEmp1", System.Data.SqlDbType.Date);
            p91.Value = fechaEgrEmp1;
            cmd.Parameters.Add(p91);

            System.Data.SqlClient.SqlParameter p92 = new SqlParameter("@verfFechEgrEm1", System.Data.SqlDbType.VarChar);
            p92.Value = verfFechEgrEm1;
            cmd.Parameters.Add(p92);

            System.Data.SqlClient.SqlParameter p93 = new SqlParameter("@obsercFechEgrEm1", System.Data.SqlDbType.VarChar);
            p93.Value = obsercFechEgrEm1;
            cmd.Parameters.Add(p93);

            System.Data.SqlClient.SqlParameter p94 = new SqlParameter("@empAnterior2", System.Data.SqlDbType.VarChar);
            p94.Value = empAnterior2;
            cmd.Parameters.Add(p94);

            System.Data.SqlClient.SqlParameter p95 = new SqlParameter("@UltSalarioEmp2", System.Data.SqlDbType.Decimal);
            p95.Value = UltSalarioEmp2;
            cmd.Parameters.Add(p95);

            System.Data.SqlClient.SqlParameter p96 = new SqlParameter("@ultCargoEmp2", System.Data.SqlDbType.VarChar);
            p96.Value = ultCargoEmp2;
            cmd.Parameters.Add(p96);

            System.Data.SqlClient.SqlParameter p97 = new SqlParameter("@motSalidEmp2", System.Data.SqlDbType.VarChar);
            p97.Value = motSalidEmp2;
            cmd.Parameters.Add(p97);

            System.Data.SqlClient.SqlParameter p98 = new SqlParameter("@fechaIngEmp2", System.Data.SqlDbType.Date);
            p98.Value = fechaIngEmp2;
            cmd.Parameters.Add(p98);

            System.Data.SqlClient.SqlParameter p99 = new SqlParameter("@fechaEgrsEmp2", System.Data.SqlDbType.Date);
            p99.Value = fechaEgrsEmp2;
            cmd.Parameters.Add(p99);

            System.Data.SqlClient.SqlParameter p100 = new SqlParameter("@referenciaPers", System.Data.SqlDbType.VarChar);
            p100.Value = referenciaPers;
            cmd.Parameters.Add(p100);

            System.Data.SqlClient.SqlParameter p101 = new SqlParameter("@verfRefPers", System.Data.SqlDbType.VarChar);
            p101.Value = verfRefPers;
            cmd.Parameters.Add(p101);

            System.Data.SqlClient.SqlParameter p102 = new SqlParameter("@observRefPers", System.Data.SqlDbType.VarChar);
            p102.Value = observRefPers;
            cmd.Parameters.Add(p102);

            System.Data.SqlClient.SqlParameter p103 = new SqlParameter("@parentsRefPers", System.Data.SqlDbType.VarChar);
            p103.Value = parentsRefPers;
            cmd.Parameters.Add(p103);

            System.Data.SqlClient.SqlParameter p104 = new SqlParameter("@verfParentRef", System.Data.SqlDbType.VarChar);
            p104.Value = verfParentRef;
            cmd.Parameters.Add(p104);

            System.Data.SqlClient.SqlParameter p105 = new SqlParameter("@observRefParnRef", System.Data.SqlDbType.VarChar);
            p105.Value = observRefParnRef;
            cmd.Parameters.Add(p105);

            System.Data.SqlClient.SqlParameter p106 = new SqlParameter("@numTelfRefPers", System.Data.SqlDbType.VarChar);
            p106.Value = numTelfRefPers;
            cmd.Parameters.Add(p106);

            System.Data.SqlClient.SqlParameter p107 = new SqlParameter("@verfNumTelfRef", System.Data.SqlDbType.VarChar);
            p107.Value = verfNumTelfRef;
            cmd.Parameters.Add(p107);

            System.Data.SqlClient.SqlParameter p108 = new SqlParameter("@observNumTelRef", System.Data.SqlDbType.VarChar);
            p108.Value = observNumTelRef;
            cmd.Parameters.Add(p108);

            System.Data.SqlClient.SqlParameter p109 = new SqlParameter("@tiempoConcRef", System.Data.SqlDbType.VarChar);
            p109.Value = tiempoConcRef;
            cmd.Parameters.Add(p109);

            System.Data.SqlClient.SqlParameter p110 = new SqlParameter("@verfTiempCncRef", System.Data.SqlDbType.VarChar);
            p110.Value = verfTiempCncRef;
            cmd.Parameters.Add(p110);

            System.Data.SqlClient.SqlParameter p111 = new SqlParameter("@observTiempCncRef", System.Data.SqlDbType.VarChar);
            p111.Value = observTiempCncRef;
            cmd.Parameters.Add(p111);

            System.Data.SqlClient.SqlParameter p112 = new SqlParameter("@padeceEnfermedad", System.Data.SqlDbType.VarChar);
            p112.Value = padeceEnfermedad;
            cmd.Parameters.Add(p112);

            System.Data.SqlClient.SqlParameter p113 = new SqlParameter("@tipoEnfermedad", System.Data.SqlDbType.VarChar);
            p113.Value = tipoEnfermedad;
            cmd.Parameters.Add(p113);

            System.Data.SqlClient.SqlParameter p114 = new SqlParameter("@enfermedadDesde", System.Data.SqlDbType.VarChar);
            p114.Value = enfermedadDesde;
            cmd.Parameters.Add(p114);

            System.Data.SqlClient.SqlParameter p115 = new SqlParameter("@descEstudios", System.Data.SqlDbType.VarChar);
            p115.Value = descEstudios;
            cmd.Parameters.Add(p115);

            System.Data.SqlClient.SqlParameter p116 = new SqlParameter("@centroEstudios", System.Data.SqlDbType.VarChar);
            p116.Value = centroEstudios;
            cmd.Parameters.Add(p116);

            //System.Data.SqlClient.SqlParameter p117 = new SqlParameter("@fechIngHistoEgr", System.Data.SqlDbType.Date);
            //p117.Value = Convert.ToDateTime(fechIngHistoEgr);
            //cmd.Parameters.Add(p117);

            //System.Data.SqlClient.SqlParameter p118 = new SqlParameter("@fechaEgrsHistoEg", System.Data.SqlDbType.Date);
            //p118.Value =Convert.ToDateTime(fechaEgrsHistoEg);
            //cmd.Parameters.Add(p118);

            //System.Data.SqlClient.SqlParameter p119 = new SqlParameter("@ubicHistoEgrs", System.Data.SqlDbType.VarChar);
            //p119.Value = ubicHistoEgrs;
            //cmd.Parameters.Add(p119);

            //System.Data.SqlClient.SqlParameter p120 = new SqlParameter("@deptoHistoEgrs", System.Data.SqlDbType.VarChar);
            //p120.Value = deptoHistoEgrs;
            //cmd.Parameters.Add(p120);

            //System.Data.SqlClient.SqlParameter p121 = new SqlParameter("@cargoHistoEgrs", System.Data.SqlDbType.VarChar);
            //p121.Value = cargoHistoEgrs;
            //cmd.Parameters.Add(p121);

            //System.Data.SqlClient.SqlParameter p122 = new SqlParameter("@motvHistoEgrs", System.Data.SqlDbType.VarChar);
            //p122.Value = motvHistoEgrs;
            //cmd.Parameters.Add(p122);

            System.Data.SqlClient.SqlParameter p123 = new SqlParameter("@nombreJefe", System.Data.SqlDbType.VarChar);
            p123.Value = nombreJefe;
            cmd.Parameters.Add(p123);

            System.Data.SqlClient.SqlParameter p125 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p125.Value = observacion;
            cmd.Parameters.Add(p125);

            System.Data.SqlClient.SqlParameter p126 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p126.Value = user;
            cmd.Parameters.Add(p126);
            System.Data.SqlClient.SqlParameter p136 = new SqlParameter("@pagoVacacion", System.Data.SqlDbType.Bit);
            p136.Value = pagoVacacion;
            cmd.Parameters.Add(p136);
            System.Data.SqlClient.SqlParameter p137 = new SqlParameter("@discapacidad", System.Data.SqlDbType.Bit);
            p137.Value = discapacidad;
            cmd.Parameters.Add(p137);
            System.Data.SqlClient.SqlParameter p139 = new SqlParameter("@flexitime", System.Data.SqlDbType.Bit);
            p139.Value = flexitime;
            cmd.Parameters.Add(p139);
            System.Data.SqlClient.SqlParameter p140 = new SqlParameter("@credito", System.Data.SqlDbType.Bit);
            p140.Value = credito;
            cmd.Parameters.Add(p140);

            System.Data.SqlClient.SqlParameter p144 = new SqlParameter("@domicdepto", System.Data.SqlDbType.Int);
            p144.Value = domicdepto;
            cmd.Parameters.Add(p144);
            System.Data.SqlClient.SqlParameter p141 = new SqlParameter("@Multitarea", System.Data.SqlDbType.Bit);
            p141.Value = multitask;
            cmd.Parameters.Add(p141);

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


        public bool AgregarEmpleado(string primerNomb, string segundoNomb, string primerApellido, string segundApellido, string nombreCompleto, string sexo, int pais, int departamento, int municipio,
            DateTime fechaNac, string numCedula, DateTime emiteCed, DateTime venceCed, string operacion, int nivelAcademico, string numInss, int ubicacion, int proceso, int cargo, DateTime primerIngreso,
            int estadoEmpl, DateTime fechaIngr, DateTime fechaEgrs, int moneda, decimal salario, string cuentaContable, decimal subsidio, string cuentaBanc, decimal incentivo, int turno, int tipoContrato,
            int tipoSalario, int liquidado, bool marca, bool ganaExtras, string telefono, string celular, string correo, int estadoCivil, int tipoCasa, string viveCon, int domiciMunc, string direccion,
            string nombreEmerg, string telfEmerg, string parentesco, string direcEmerg, string nombPadre, string numCedPadre, string vivePadre, string nombMadre, string numCedMadre,
            string viveMadre, string nombEsps, string numCedEsps, string nombreHijo1, string lugarNacH1, DateTime fechaNacH1, string sexoH1, string nombreHijo2, string lugarNacH2, DateTime fechaNacH2, string sexoH2,
            string nombreHijo3, string lugarNacH3, DateTime fechaNacH3, string sexoH3, string nombreHijo4, string lugarNacH4, DateTime fechaNacH4, string sexoH4, string empAnterior1, string verfEmpAnt1, string observEmpAnt1,
            decimal ultimoSalarioEmp1, string verfUltSalaEm1, string observSalAntEm1, string cargoEmp1, string verfCargoEmp1, string observUltCargoEmp1, string motivoSalidaEmp1, string verfMotvSal1, string observMotvSal1,
            DateTime fechaIngEmp1, string verfFechaIngEm1, string observFechaIngEm1, DateTime fechaEgrEmp1, string verfFechEgrEm1, string obsercFechEgrEm1, string empAnterior2, decimal UltSalarioEmp2, string ultCargoEmp2,
            string motSalidEmp2, DateTime fechaIngEmp2, DateTime fechaEgrsEmp2, string referenciaPers, string verfRefPers, string observRefPers, string parentsRefPers, string verfParentRef, string observRefParnRef, string numTelfRefPers,
            string verfNumTelfRef, string observNumTelRef, string tiempoConcRef, string verfTiempCncRef, string observTiempCncRef, string padeceEnfermedad, string tipoEnfermedad, string enfermedadDesde, string descEstudios, string centroEstudios,
            //string fechIngHistoEgr, string fechaEgrsHistoEg, string ubicHistoEgrs, string deptoHistoEgrs, string cargoHistoEgrs, string motvHistoEgrs, 
            string nombreJefe, string observacion, int idEmpresa, string user, bool pagoVacacion,bool discapacidad,string cuentaMall,bool flexitime,bool credito,int domicdepto,bool multitask)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DetalleEmpleadoAgregar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@primerNomb", System.Data.SqlDbType.VarChar);
            p3.Value = primerNomb;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@segundoNomb", System.Data.SqlDbType.VarChar);
            p4.Value = segundoNomb;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@primerApellido", System.Data.SqlDbType.VarChar);
            p5.Value = primerApellido;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@segundApellido", System.Data.SqlDbType.VarChar);
            p6.Value = segundApellido;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p124 = new SqlParameter("@nombreCompleto", System.Data.SqlDbType.VarChar);
            p124.Value = nombreCompleto;
            cmd.Parameters.Add(p124);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@sexo", System.Data.SqlDbType.Char);
            p7.Value = sexo;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@pais", System.Data.SqlDbType.Int);
            p8.Value = pais;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@departamento", System.Data.SqlDbType.Int);
            p9.Value = departamento;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@municipio", System.Data.SqlDbType.Int);
            p10.Value = municipio;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@fechaNac", System.Data.SqlDbType.Date);
            p11.Value = fechaNac;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@numCedula", System.Data.SqlDbType.VarChar);
            p12.Value = numCedula;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@emiteCed", System.Data.SqlDbType.Date);
            p13.Value = emiteCed;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@venceCed", System.Data.SqlDbType.Date);
            p14.Value = venceCed;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@operacion", System.Data.SqlDbType.VarChar);
            p15.Value = operacion;
            cmd.Parameters.Add(p15);

            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@nivelAcademico", System.Data.SqlDbType.Int);
            p16.Value = nivelAcademico;
            cmd.Parameters.Add(p16);

            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@numInss", System.Data.SqlDbType.VarChar);
            p17.Value = numInss;
            cmd.Parameters.Add(p17);

            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p18.Value = ubicacion;
            cmd.Parameters.Add(p18);

            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@proceso", System.Data.SqlDbType.Int);
            p19.Value = proceso;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@cargo", System.Data.SqlDbType.Int);
            p20.Value = cargo;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@primerIngreso", System.Data.SqlDbType.Date);
            p21.Value = primerIngreso;
            cmd.Parameters.Add(p21);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@estadoEmpl", System.Data.SqlDbType.Int);
            p22.Value = estadoEmpl;
            cmd.Parameters.Add(p22);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@fechaIngr", System.Data.SqlDbType.Date);
            p23.Value = fechaIngr;
            cmd.Parameters.Add(p23);

            System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@fechaEgrs", System.Data.SqlDbType.Date);
            p25.Value = fechaEgrs;
            cmd.Parameters.Add(p25);

            System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@moneda", System.Data.SqlDbType.Int);
            p26.Value = moneda;
            cmd.Parameters.Add(p26);

            System.Data.SqlClient.SqlParameter p27 = new SqlParameter("@salario", System.Data.SqlDbType.Decimal);
            p27.Value = salario;
            cmd.Parameters.Add(p27);

            System.Data.SqlClient.SqlParameter p28 = new SqlParameter("@cuentaContable", System.Data.SqlDbType.VarChar);
            p28.Value = cuentaContable;
            cmd.Parameters.Add(p28);

            System.Data.SqlClient.SqlParameter p29 = new SqlParameter("@subsidio", System.Data.SqlDbType.Decimal);
            p29.Value = subsidio;
            cmd.Parameters.Add(p29);

            System.Data.SqlClient.SqlParameter p30 = new SqlParameter("@cuentaBanc", System.Data.SqlDbType.VarChar);
            p30.Value = cuentaBanc;
            cmd.Parameters.Add(p30);

            System.Data.SqlClient.SqlParameter p138 = new SqlParameter("@cuentaMall", System.Data.SqlDbType.VarChar);
            p138.Value = cuentaMall;
            cmd.Parameters.Add(p138);

            System.Data.SqlClient.SqlParameter p31 = new SqlParameter("@incentivo", System.Data.SqlDbType.Decimal);
            p31.Value = incentivo;
            cmd.Parameters.Add(p31);

            System.Data.SqlClient.SqlParameter p32 = new SqlParameter("@turno", System.Data.SqlDbType.Int);
            p32.Value = turno;
            cmd.Parameters.Add(p32);

            System.Data.SqlClient.SqlParameter p33 = new SqlParameter("@tipoContrato", System.Data.SqlDbType.Int);
            p33.Value = tipoContrato;
            cmd.Parameters.Add(p33);

            System.Data.SqlClient.SqlParameter p34 = new SqlParameter("@tipoSalario", System.Data.SqlDbType.Int);
            p34.Value = tipoSalario;
            cmd.Parameters.Add(p34);

            System.Data.SqlClient.SqlParameter p35 = new SqlParameter("@liquidado", System.Data.SqlDbType.Int);
            p35.Value = liquidado;
            cmd.Parameters.Add(p35);

            System.Data.SqlClient.SqlParameter p36 = new SqlParameter("@marca", System.Data.SqlDbType.Bit);
            p36.Value = marca;
            cmd.Parameters.Add(p36);

            System.Data.SqlClient.SqlParameter p37 = new SqlParameter("@ganaExtras", System.Data.SqlDbType.Bit);
            p37.Value = ganaExtras;
            cmd.Parameters.Add(p37);

            System.Data.SqlClient.SqlParameter p38 = new SqlParameter("@telefono", System.Data.SqlDbType.VarChar);
            p38.Value = telefono;
            cmd.Parameters.Add(p38);

            System.Data.SqlClient.SqlParameter p39 = new SqlParameter("@celular", System.Data.SqlDbType.VarChar);
            p39.Value = celular;
            cmd.Parameters.Add(p39);

            System.Data.SqlClient.SqlParameter p40 = new SqlParameter("@correo", System.Data.SqlDbType.VarChar);
            p40.Value = correo;
            cmd.Parameters.Add(p40);

            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@estadoCivil", System.Data.SqlDbType.Int);
            p41.Value = estadoCivil;
            cmd.Parameters.Add(p41);

            System.Data.SqlClient.SqlParameter p42 = new SqlParameter("@tipoCasa", System.Data.SqlDbType.Int);
            p42.Value = tipoCasa;
            cmd.Parameters.Add(p42);

            System.Data.SqlClient.SqlParameter p43 = new SqlParameter("@viveCon", System.Data.SqlDbType.VarChar);
            p43.Value = viveCon;
            cmd.Parameters.Add(p43);

            System.Data.SqlClient.SqlParameter p44 = new SqlParameter("@domiciMunc", System.Data.SqlDbType.Int);
            p44.Value = domiciMunc;
            cmd.Parameters.Add(p44);

            System.Data.SqlClient.SqlParameter p45 = new SqlParameter("@direccion", System.Data.SqlDbType.VarChar);
            p45.Value = direccion;
            cmd.Parameters.Add(p45);

            System.Data.SqlClient.SqlParameter p48 = new SqlParameter("@nombreEmerg", System.Data.SqlDbType.VarChar);
            p48.Value = nombreEmerg;
            cmd.Parameters.Add(p48);

            System.Data.SqlClient.SqlParameter p49 = new SqlParameter("@telfEmerg", System.Data.SqlDbType.VarChar);
            p49.Value = telfEmerg;
            cmd.Parameters.Add(p49);

            System.Data.SqlClient.SqlParameter p50 = new SqlParameter("@parentesco", System.Data.SqlDbType.VarChar);
            p50.Value = parentesco;
            cmd.Parameters.Add(p50);

            System.Data.SqlClient.SqlParameter p51 = new SqlParameter("@direcEmerg", System.Data.SqlDbType.VarChar);
            p51.Value = direcEmerg;
            cmd.Parameters.Add(p51);

            System.Data.SqlClient.SqlParameter p52 = new SqlParameter("@nombPadre", System.Data.SqlDbType.VarChar);
            p52.Value = nombPadre;
            cmd.Parameters.Add(p52);

            System.Data.SqlClient.SqlParameter p53 = new SqlParameter("@numCedPadre", System.Data.SqlDbType.VarChar);
            p53.Value = numCedPadre;
            cmd.Parameters.Add(p53);

            System.Data.SqlClient.SqlParameter p54 = new SqlParameter("@vivePadre", System.Data.SqlDbType.VarChar);
            p54.Value = vivePadre;
            cmd.Parameters.Add(p54);

            System.Data.SqlClient.SqlParameter p55 = new SqlParameter("@nombMadre", System.Data.SqlDbType.VarChar);
            p55.Value = nombMadre;
            cmd.Parameters.Add(p55);

            System.Data.SqlClient.SqlParameter p56 = new SqlParameter("@numCedMadre", System.Data.SqlDbType.VarChar);
            p56.Value = numCedMadre;
            cmd.Parameters.Add(p56);

            System.Data.SqlClient.SqlParameter p57 = new SqlParameter("@viveMadre", System.Data.SqlDbType.VarChar);
            p57.Value = viveMadre;
            cmd.Parameters.Add(p57);

            System.Data.SqlClient.SqlParameter p58 = new SqlParameter("@nombEsps", System.Data.SqlDbType.VarChar);
            p58.Value = nombEsps;
            cmd.Parameters.Add(p58);

            System.Data.SqlClient.SqlParameter p59 = new SqlParameter("@numCedEsps", System.Data.SqlDbType.VarChar);
            p59.Value = numCedEsps;
            cmd.Parameters.Add(p59);

            System.Data.SqlClient.SqlParameter p60 = new SqlParameter("@nombreHijo1", System.Data.SqlDbType.VarChar);
            p60.Value = nombreHijo1;
            cmd.Parameters.Add(p60);

            System.Data.SqlClient.SqlParameter p61 = new SqlParameter("@lugarNacH1", System.Data.SqlDbType.VarChar);
            p61.Value = lugarNacH1;
            cmd.Parameters.Add(p61);

            System.Data.SqlClient.SqlParameter p62 = new SqlParameter("@fechaNacH1", System.Data.SqlDbType.Date);
            p62.Value = fechaNacH1;
            cmd.Parameters.Add(p62);

            System.Data.SqlClient.SqlParameter p63 = new SqlParameter("@sexoH1", System.Data.SqlDbType.VarChar);
            p63.Value = sexoH1;
            cmd.Parameters.Add(p63);

            System.Data.SqlClient.SqlParameter p64 = new SqlParameter("@nombreHijo2", System.Data.SqlDbType.VarChar);
            p64.Value = nombreHijo2;
            cmd.Parameters.Add(p64);

            System.Data.SqlClient.SqlParameter p65 = new SqlParameter("@lugarNacH2", System.Data.SqlDbType.VarChar);
            p65.Value = lugarNacH2;
            cmd.Parameters.Add(p65);

            System.Data.SqlClient.SqlParameter p66 = new SqlParameter("@fechaNacH2", System.Data.SqlDbType.Date);
            p66.Value = fechaNacH2;
            cmd.Parameters.Add(p66);

            System.Data.SqlClient.SqlParameter p67 = new SqlParameter("@sexoH2", System.Data.SqlDbType.VarChar);
            p67.Value = sexoH2;
            cmd.Parameters.Add(p67);

            System.Data.SqlClient.SqlParameter p68 = new SqlParameter("@nombreHijo3", System.Data.SqlDbType.VarChar);
            p68.Value = nombreHijo3;
            cmd.Parameters.Add(p68);

            System.Data.SqlClient.SqlParameter p69 = new SqlParameter("@lugarNacH3", System.Data.SqlDbType.VarChar);
            p69.Value = lugarNacH3;
            cmd.Parameters.Add(p69);

            System.Data.SqlClient.SqlParameter p70 = new SqlParameter("@fechaNacH3", System.Data.SqlDbType.Date);
            p70.Value = fechaNacH3;
            cmd.Parameters.Add(p70);

            System.Data.SqlClient.SqlParameter p71 = new SqlParameter("@sexoH3", System.Data.SqlDbType.VarChar);
            p71.Value = sexoH3;
            cmd.Parameters.Add(p71);

            System.Data.SqlClient.SqlParameter p72 = new SqlParameter("@nombreHijo4", System.Data.SqlDbType.VarChar);
            p72.Value = nombreHijo4;
            cmd.Parameters.Add(p72);

            System.Data.SqlClient.SqlParameter p73 = new SqlParameter("@lugarNacH4", System.Data.SqlDbType.VarChar);
            p73.Value = lugarNacH4;
            cmd.Parameters.Add(p73);

            System.Data.SqlClient.SqlParameter p74 = new SqlParameter("@fechaNacH4", System.Data.SqlDbType.Date);
            p74.Value = fechaNacH4;
            cmd.Parameters.Add(p74);

            System.Data.SqlClient.SqlParameter p75 = new SqlParameter("@sexoH4", System.Data.SqlDbType.VarChar);
            p75.Value = sexoH4;
            cmd.Parameters.Add(p75);

            System.Data.SqlClient.SqlParameter p76 = new SqlParameter("@empAnterior1", System.Data.SqlDbType.VarChar);
            p76.Value = empAnterior1;
            cmd.Parameters.Add(p76);

            System.Data.SqlClient.SqlParameter p77 = new SqlParameter("@verfEmpAnt1", System.Data.SqlDbType.VarChar);
            p77.Value = verfEmpAnt1;
            cmd.Parameters.Add(p77);

            System.Data.SqlClient.SqlParameter p78 = new SqlParameter("@observEmpAnt1", System.Data.SqlDbType.VarChar);
            p78.Value = observEmpAnt1;
            cmd.Parameters.Add(p78);

            System.Data.SqlClient.SqlParameter p79 = new SqlParameter("@ultimoSalarioEmp1", System.Data.SqlDbType.Decimal);
            p79.Value = ultimoSalarioEmp1;
            cmd.Parameters.Add(p79);

            System.Data.SqlClient.SqlParameter p80 = new SqlParameter("@verfUltSalaEm1", System.Data.SqlDbType.VarChar);
            p80.Value = verfUltSalaEm1;
            cmd.Parameters.Add(p80);

            System.Data.SqlClient.SqlParameter p81 = new SqlParameter("@observSalAntEm1", System.Data.SqlDbType.VarChar);
            p81.Value = observSalAntEm1;
            cmd.Parameters.Add(p81);

            System.Data.SqlClient.SqlParameter p82 = new SqlParameter("@cargoEmp1", System.Data.SqlDbType.VarChar);
            p82.Value = cargoEmp1;
            cmd.Parameters.Add(p82);

            System.Data.SqlClient.SqlParameter p83 = new SqlParameter("@verfCargoEmp1", System.Data.SqlDbType.VarChar);
            p83.Value = verfCargoEmp1;
            cmd.Parameters.Add(p83);

            System.Data.SqlClient.SqlParameter p84 = new SqlParameter("@observUltCargoEmp1", System.Data.SqlDbType.VarChar);
            p84.Value = observUltCargoEmp1;
            cmd.Parameters.Add(p84);

            System.Data.SqlClient.SqlParameter p85 = new SqlParameter("@motivoSalidaEmp1", System.Data.SqlDbType.VarChar);
            p85.Value = motivoSalidaEmp1;
            cmd.Parameters.Add(p85);

            System.Data.SqlClient.SqlParameter p86 = new SqlParameter("@verfMotvSal1", System.Data.SqlDbType.VarChar);
            p86.Value = verfMotvSal1;
            cmd.Parameters.Add(p86);

            System.Data.SqlClient.SqlParameter p87 = new SqlParameter("@observMotvSal1", System.Data.SqlDbType.VarChar);
            p87.Value = observMotvSal1;
            cmd.Parameters.Add(p87);

            System.Data.SqlClient.SqlParameter p88 = new SqlParameter("@fechaIngEmp1", System.Data.SqlDbType.Date);
            p88.Value = fechaIngEmp1;
            cmd.Parameters.Add(p88);

            System.Data.SqlClient.SqlParameter p89 = new SqlParameter("@verfFechaIngEm1", System.Data.SqlDbType.VarChar);
            p89.Value = verfFechaIngEm1;
            cmd.Parameters.Add(p89);

            System.Data.SqlClient.SqlParameter p90 = new SqlParameter("@observFechaIngEm1", System.Data.SqlDbType.VarChar);
            p90.Value = observFechaIngEm1;
            cmd.Parameters.Add(p90);

            System.Data.SqlClient.SqlParameter p91 = new SqlParameter("@fechaEgrEmp1", System.Data.SqlDbType.Date);
            p91.Value = fechaEgrEmp1;
            cmd.Parameters.Add(p91);

            System.Data.SqlClient.SqlParameter p92 = new SqlParameter("@verfFechEgrEm1", System.Data.SqlDbType.VarChar);
            p92.Value = verfFechEgrEm1;
            cmd.Parameters.Add(p92);

            System.Data.SqlClient.SqlParameter p93 = new SqlParameter("@obsercFechEgrEm1", System.Data.SqlDbType.VarChar);
            p93.Value = obsercFechEgrEm1;
            cmd.Parameters.Add(p93);

            System.Data.SqlClient.SqlParameter p94 = new SqlParameter("@empAnterior2", System.Data.SqlDbType.VarChar);
            p94.Value = empAnterior2;
            cmd.Parameters.Add(p94);

            System.Data.SqlClient.SqlParameter p95 = new SqlParameter("@UltSalarioEmp2", System.Data.SqlDbType.Decimal);
            p95.Value = UltSalarioEmp2;
            cmd.Parameters.Add(p95);

            System.Data.SqlClient.SqlParameter p96 = new SqlParameter("@ultCargoEmp2", System.Data.SqlDbType.VarChar);
            p96.Value = ultCargoEmp2;
            cmd.Parameters.Add(p96);

            System.Data.SqlClient.SqlParameter p97 = new SqlParameter("@motSalidEmp2", System.Data.SqlDbType.VarChar);
            p97.Value = motSalidEmp2;
            cmd.Parameters.Add(p97);

            System.Data.SqlClient.SqlParameter p98 = new SqlParameter("@fechaIngEmp2", System.Data.SqlDbType.Date);
            p98.Value = fechaIngEmp2;
            cmd.Parameters.Add(p98);

            System.Data.SqlClient.SqlParameter p99 = new SqlParameter("@fechaEgrsEmp2", System.Data.SqlDbType.Date);
            p99.Value = fechaEgrsEmp2;
            cmd.Parameters.Add(p99);

            System.Data.SqlClient.SqlParameter p100 = new SqlParameter("@referenciaPers", System.Data.SqlDbType.VarChar);
            p100.Value = referenciaPers;
            cmd.Parameters.Add(p100);

            System.Data.SqlClient.SqlParameter p101 = new SqlParameter("@verfRefPers", System.Data.SqlDbType.VarChar);
            p101.Value = verfRefPers;
            cmd.Parameters.Add(p101);

            System.Data.SqlClient.SqlParameter p102 = new SqlParameter("@observRefPers", System.Data.SqlDbType.VarChar);
            p102.Value = observRefPers;
            cmd.Parameters.Add(p102);

            System.Data.SqlClient.SqlParameter p103 = new SqlParameter("@parentsRefPers", System.Data.SqlDbType.VarChar);
            p103.Value = parentsRefPers;
            cmd.Parameters.Add(p103);

            System.Data.SqlClient.SqlParameter p104 = new SqlParameter("@verfParentRef", System.Data.SqlDbType.VarChar);
            p104.Value = verfParentRef;
            cmd.Parameters.Add(p104);

            System.Data.SqlClient.SqlParameter p105 = new SqlParameter("@observRefParnRef", System.Data.SqlDbType.VarChar);
            p105.Value = observRefParnRef;
            cmd.Parameters.Add(p105);

            System.Data.SqlClient.SqlParameter p106 = new SqlParameter("@numTelfRefPers", System.Data.SqlDbType.VarChar);
            p106.Value = numTelfRefPers;
            cmd.Parameters.Add(p106);

            System.Data.SqlClient.SqlParameter p107 = new SqlParameter("@verfNumTelfRef", System.Data.SqlDbType.VarChar);
            p107.Value = verfNumTelfRef;
            cmd.Parameters.Add(p107);

            System.Data.SqlClient.SqlParameter p108 = new SqlParameter("@observNumTelRef", System.Data.SqlDbType.VarChar);
            p108.Value = observNumTelRef;
            cmd.Parameters.Add(p108);

            System.Data.SqlClient.SqlParameter p109 = new SqlParameter("@tiempoConcRef", System.Data.SqlDbType.VarChar);
            p109.Value = tiempoConcRef;
            cmd.Parameters.Add(p109);

            System.Data.SqlClient.SqlParameter p110 = new SqlParameter("@verfTiempCncRef", System.Data.SqlDbType.VarChar);
            p110.Value = verfTiempCncRef;
            cmd.Parameters.Add(p110);

            System.Data.SqlClient.SqlParameter p111 = new SqlParameter("@observTiempCncRef", System.Data.SqlDbType.VarChar);
            p111.Value = observTiempCncRef;
            cmd.Parameters.Add(p111);

            System.Data.SqlClient.SqlParameter p112 = new SqlParameter("@padeceEnfermedad", System.Data.SqlDbType.VarChar);
            p112.Value = padeceEnfermedad;
            cmd.Parameters.Add(p112);

            System.Data.SqlClient.SqlParameter p113 = new SqlParameter("@tipoEnfermedad", System.Data.SqlDbType.VarChar);
            p113.Value = tipoEnfermedad;
            cmd.Parameters.Add(p113);

            System.Data.SqlClient.SqlParameter p114 = new SqlParameter("@enfermedadDesde", System.Data.SqlDbType.VarChar);
            p114.Value = enfermedadDesde;
            cmd.Parameters.Add(p114);

            System.Data.SqlClient.SqlParameter p115 = new SqlParameter("@descEstudios", System.Data.SqlDbType.VarChar);
            p115.Value = descEstudios;
            cmd.Parameters.Add(p115);

            System.Data.SqlClient.SqlParameter p116 = new SqlParameter("@centroEstudios", System.Data.SqlDbType.VarChar);
            p116.Value = centroEstudios;
            cmd.Parameters.Add(p116);

            //System.Data.SqlClient.SqlParameter p117 = new SqlParameter("@fechIngHistoEgr", System.Data.SqlDbType.Date);
            //p117.Value = Convert.ToDateTime(fechIngHistoEgr);
            //cmd.Parameters.Add(p117);

            //System.Data.SqlClient.SqlParameter p118 = new SqlParameter("@fechaEgrsHistoEg", System.Data.SqlDbType.Date);
            //p118.Value = Convert.ToDateTime(fechaEgrsHistoEg); 
            //cmd.Parameters.Add(p118);

            //System.Data.SqlClient.SqlParameter p119 = new SqlParameter("@ubicHistoEgrs", System.Data.SqlDbType.VarChar);
            //p119.Value = ubicHistoEgrs;
            //cmd.Parameters.Add(p119);

            //System.Data.SqlClient.SqlParameter p120 = new SqlParameter("@deptoHistoEgrs", System.Data.SqlDbType.VarChar);
            //p120.Value = deptoHistoEgrs;
            //cmd.Parameters.Add(p120);

            //System.Data.SqlClient.SqlParameter p121 = new SqlParameter("@cargoHistoEgrs", System.Data.SqlDbType.VarChar);
            //p121.Value = cargoHistoEgrs;
            //cmd.Parameters.Add(p121);

            //System.Data.SqlClient.SqlParameter p122 = new SqlParameter("@motvHistoEgrs", System.Data.SqlDbType.VarChar);
            //p122.Value = motvHistoEgrs;
            //cmd.Parameters.Add(p122);

            System.Data.SqlClient.SqlParameter p123 = new SqlParameter("@nombreJefe", System.Data.SqlDbType.VarChar);
            p123.Value = nombreJefe;
            cmd.Parameters.Add(p123);

            System.Data.SqlClient.SqlParameter p125 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p125.Value = observacion;
            cmd.Parameters.Add(p125);

            System.Data.SqlClient.SqlParameter p126 = new SqlParameter("@user", System.Data.SqlDbType.VarChar);
            p126.Value = user;
            cmd.Parameters.Add(p126);
            System.Data.SqlClient.SqlParameter p136 = new SqlParameter("@pagoVacacion", System.Data.SqlDbType.Bit);
            p136.Value = pagoVacacion;
            cmd.Parameters.Add(p136);
            System.Data.SqlClient.SqlParameter p137 = new SqlParameter("@discapacidad", System.Data.SqlDbType.Bit);
            p137.Value = discapacidad;
            cmd.Parameters.Add(p137);
            System.Data.SqlClient.SqlParameter p139 = new SqlParameter("@flexitime", System.Data.SqlDbType.Bit);
            p139.Value = flexitime;
            cmd.Parameters.Add(p139);
            System.Data.SqlClient.SqlParameter p140 = new SqlParameter("@credito", System.Data.SqlDbType.Bit);
            p140.Value = credito;
            cmd.Parameters.Add(p140);

            System.Data.SqlClient.SqlParameter p144 = new SqlParameter("@domicdepto", System.Data.SqlDbType.Int);
            p144.Value = domicdepto;
            cmd.Parameters.Add(p144);
            System.Data.SqlClient.SqlParameter p141 = new SqlParameter("@Multitarea", System.Data.SqlDbType.Bit);
            p141.Value = multitask;
            cmd.Parameters.Add(p141);

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

        public void SaveFile(string name, byte[] data, int numemp, int tipo, int idEmpresa)
        {


            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertarFoto";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@name", System.Data.SqlDbType.VarChar);
            p1.Value = name;
            cmd.Parameters.Add(p1);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@data", System.Data.SqlDbType.Binary);
            p2.Value = data;
            cmd.Parameters.Add(p2);
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@numemp", System.Data.SqlDbType.Int);
            p3.Value = numemp;
            cmd.Parameters.Add(p3);
            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p4.Value = tipo;
            cmd.Parameters.Add(p4);


            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }

        public bool EliminarEmpleado(int codEmpleado, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "DetalleEmpleadoEliminar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codEmpleado;
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

        public string obtenerUltimoEmpleadoAgregado(int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerUltimoEmpleadoAgregado";
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
        public bool CambioSalarioGuardar(int codigo,int codubicacion,int coddepto,decimal salarioant,decimal salarionew, string observacion, int idEmpresa, string user
            )
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CambioSalarioGuardar";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codubicacion", System.Data.SqlDbType.Int);
            p3.Value = codubicacion;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@coddepto", System.Data.SqlDbType.Int);
            p4.Value = coddepto;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@salarioant", System.Data.SqlDbType.Decimal);
            p5.Value = salarioant;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@salarionew", System.Data.SqlDbType.Decimal);
            p6.Value = salarionew;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p124 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p124.Value = observacion;
            cmd.Parameters.Add(p124);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@user", System.Data.SqlDbType.Char);
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
        //AGREGADOS POR WBRAVO
        public dsPlanilla.dtEmpleadoDataTable ActivosParaPlanilla(int idEmpresa, int activo,int ubicacion)
        {
            dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmpleadosActivos";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@filtro", System.Data.SqlDbType.Int);
            p4.Value = activo;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@ubicacion", System.Data.SqlDbType.Int);
            p14.Value = ubicacion;
            cmd.Parameters.Add(p14);

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

        
        public dsPlanilla.dtEmpleadoDataTable ObtEmpleadosxRangodeTiempo(DateTime fechaini,DateTime fechafin,int ubicacion, int idEmpresa)
        {
            dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtEmpleadosxRangodeTiempo";
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
        

        public dsPlanilla.dtEmpleadoDataTable ObtEmpleadosParaInss(DateTime fechaini, DateTime fechafin, int ubicacion, int idEmpresa)
        {
            dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtEmpleadosParaInss";
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

        public dsPlanilla.dtEmpleadoDataTable pln_empleadosHistoricoSelect(int idEmpresa, DateTime fechaini)
        {
            dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pln_empleadosHistoricoSelect";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);

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
        public DataTable pln_empleadosHistoricoSelectxDia(int idEmpresa, DateTime fechaini,DateTime fechafin)
        {
            DataTable dtEmpleado = new DataTable();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pln_empleadosHistoricoSelectxDia";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p41.Value = fechafin;
            cmd.Parameters.Add(p41);

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
        //[pln_empleadosHistoricoSelectxDiaDistinct]
        public dsPlanilla.dtEmpleadoDataTable pln_empleadosHistoricoSelectxDiaDistinct(int idEmpresa, DateTime fechaini, DateTime fechafin)
        {
            dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pln_empleadosHistoricoSelectxDiaDistinct";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p41.Value = fechafin;
            cmd.Parameters.Add(p41);

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
        public DataTable pln_empleadosHistoricoALL(int idEmpresa, DateTime fechaini, int filtro)
        {
            //dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();
            DataTable dtEmpleado = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "pln_empleadosHistoricoALL";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter pf = new SqlParameter("@filtro", System.Data.SqlDbType.Int);
            pf.Value = filtro;
            cmd.Parameters.Add(pf);

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

        public DataSet HistorialSalariosXEmpleado(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerHistSalariosEmp";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "salarios");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }
        public DataSet HistorialEgresosXEmpleado(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            DataSet ds = new DataSet();
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);


            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnObtenerHistorialEgresosEmp";
            cmd.Connection = sqlConnection;

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "egresos");
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();

            }

            return ds;
        }

        public bool plnHistorialEgresosIns(int codigo, DateTime fechaingreso, DateTime fechaegreso, bool renovacion, string user, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnHistorialEgresosIns";
            cmd.Connection = sqlConnection;
            SqlParameter p1 = new SqlParameter("@codEmpleado", SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            SqlParameter p3 = new SqlParameter("@fechIngHistoEgr", SqlDbType.Date);
            p3.Value = fechaingreso;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@fechaEgrsHistoEg", SqlDbType.Date);
            p4.Value = fechaegreso;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@renovacion", SqlDbType.Bit);
            p5.Value = renovacion;
            cmd.Parameters.Add(p5);
            SqlParameter p2 = new SqlParameter("@user", SqlDbType.NChar);
            p2.Value = user;
            cmd.Parameters.Add(p2);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                return false;
            }
            return true;
        }
        public bool plnEstadoEmpleadoUpd(int codigo, DateTime fechaingreso, DateTime fechaegreso,int estado, int idEmpresa
            )
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnEstadoEmpleadoUpd";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codEmpleado", System.Data.SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@fechIngHistoEgr", System.Data.SqlDbType.Date);
            p3.Value = fechaingreso;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaEgrsHistoEg", System.Data.SqlDbType.Date);
            p4.Value = fechaegreso;
            cmd.Parameters.Add(p4);
            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@estado", System.Data.SqlDbType.Int);
            p2.Value = estado;
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

        public bool plnSalarioMinEmpleadoUpd(int codigo, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnSalarioMinEmpleadoUpd";
            cmd.Connection = sqlConnection;
            SqlParameter p1 = new SqlParameter("@codEmpleado", SqlDbType.Int);
            p1.Value = codigo;
            cmd.Parameters.Add(p1);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException)
            {
                if (cmd.Connection.State != 0)
                {
                    cmd.Connection.Close();
                }
                return false;
            }
            return true;
        }
        public bool CuentaEmpleadoUpd(int codigo,string numero,int tipo, int idEmpresa            )
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CuentaEmpleadoUpd";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p11.Value = codigo;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@numero", System.Data.SqlDbType.NChar);
            p1.Value = numero;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p3.Value = tipo;
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

        public bool PlnTrasladoEmpleadosIns(DateTime fecha, int codigo_empleado, int codigo_depto, string justificacion, string operacion, string usuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTrasladoEmpleadosIns";
            cmd.Connection = sqlConnection;
            SqlParameter p1 = new SqlParameter("@fecha", SqlDbType.Date);
            p1.Value = fecha;
            cmd.Parameters.Add(p1);
            SqlParameter p2 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
            p2.Value = codigo_empleado;
            cmd.Parameters.Add(p2);
            SqlParameter p3 = new SqlParameter("@codigo_depto", SqlDbType.Int);
            p3.Value = codigo_depto;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@justificacion", SqlDbType.VarChar);
            p4.Value = justificacion;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@operacion", SqlDbType.VarChar);
            p5.Value = operacion;
            cmd.Parameters.Add(p5);
            SqlParameter p6 = new SqlParameter("@usuariograba", SqlDbType.VarChar);
            p6.Value = usuario;
            cmd.Parameters.Add(p6);
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
        public bool PlnEmpleadoOperaSimultaneoIns(int codigo_empleado, int codigo_depto,decimal porcentaje,int codigo_deptoc,int desactivar, string usuario, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnEmpleadoOperaSimultaneoIns";
            cmd.Connection = sqlConnection;
            
            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p3.Value = codigo_empleado;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p4.Value = codigo_depto;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p42 = new SqlParameter("@porcentaje_opera", System.Data.SqlDbType.Decimal);
            p42.Value = porcentaje;
            cmd.Parameters.Add(p42);

            System.Data.SqlClient.SqlParameter p41 = new SqlParameter("@codigo_deptoc", System.Data.SqlDbType.Int);
            p41.Value = codigo_deptoc;
            cmd.Parameters.Add(p41);
            System.Data.SqlClient.SqlParameter p43 = new SqlParameter("@desactivar", System.Data.SqlDbType.Int);
            p43.Value = desactivar;
            cmd.Parameters.Add(p43);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@usuariograba", System.Data.SqlDbType.VarChar);
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
        public bool PlnEmpleadoOperaSimultaneoDel( int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnEmpleadoOperaSimultaneoDel";
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

        public bool PlnEmpleadosHistUpd(int codigo_empleado, DateTime fecha, int codigo_depto, int codigo_deptoc, decimal porcentaje, string operacion, int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnEmpleadosHistUpd";
            cmd.Connection = sqlConnection;
            SqlParameter p2 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
            p2.Value = codigo_empleado;
            cmd.Parameters.Add(p2);
            SqlParameter p1 = new SqlParameter("@fecha", SqlDbType.Date);
            p1.Value = fecha;
            cmd.Parameters.Add(p1);
            SqlParameter p3 = new SqlParameter("@codigo_depto", SqlDbType.Int);
            p3.Value = codigo_depto;
            cmd.Parameters.Add(p3);
            SqlParameter p4 = new SqlParameter("@codigo_deptoc", SqlDbType.Int);
            p4.Value = codigo_deptoc;
            cmd.Parameters.Add(p4);
            SqlParameter p5 = new SqlParameter("@porcentaje_opera", SqlDbType.Decimal);
            p5.Value = porcentaje;
            cmd.Parameters.Add(p5);
            SqlParameter p6 = new SqlParameter("@operacion", SqlDbType.NChar);
            p6.Value = operacion;
            cmd.Parameters.Add(p6);
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
        public bool PlnTrasladoEmpleadosDel(DateTime fecha, int codigo_empleado,int codigo_depto,int idEmpresa)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTrasladoEmpleadosDel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@fecha", System.Data.SqlDbType.Date);
            p1.Value = fecha;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p3.Value = codigo_empleado;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p2.Value = codigo_depto;
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
        public DataTable PlnTrasladoEmpleadosSel(DateTime fechaini, DateTime fechafin, int idEmpresa)
        {
            //dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();
            DataTable dtEmpleado = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnTrasladoEmpleadosSel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter pf = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            pf.Value = fechafin;
            cmd.Parameters.Add(pf);

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
        public DataTable plnObtenerEmpleadosOpSimultaneo(DateTime fechaini, DateTime fechafin,int codigo, int idEmpresa)
        {
            //dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();
            DataTable dtEmpleado = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnObtenerEmpleadosOpSimultaneo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter pf = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            pf.Value = fechafin;
            cmd.Parameters.Add(pf);
            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p1.Value = codigo;
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
        public DataTable PlnDeptoEmpHistoricoSel(int codigo,DateTime fechaini, DateTime fechafin, int idEmpresa)
        {
            //dsPlanilla.dtEmpleadoDataTable dtEmpleado = new dsPlanilla.dtEmpleadoDataTable();
            DataTable dtEmpleado = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnDeptoEmpHistoricoSel";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p4.Value = fechaini;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter pf = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            pf.Value = fechafin;
            cmd.Parameters.Add(pf);
            System.Data.SqlClient.SqlParameter pc = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            pc.Value = codigo;
            cmd.Parameters.Add(pc);

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

        public string RevertirBaja(int codigo_empleado, int idEmpresa)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            ConnectionRepository ConnectionRepository = new ConnectionRepository();

            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnEmpleadosRevertirBaja";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigo_empleado", System.Data.SqlDbType.Int);
            p3.Value = codigo_empleado;
            cmd.Parameters.Add(p3);

            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (System.Exception ex)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return ex.Message;
            }

            return "OK";
        }
    }
}