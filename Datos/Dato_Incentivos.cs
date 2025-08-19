using System;
using System.Data;
using System.Data.SqlClient;

namespace Datos
{
    public class Dato_Incentivos
    {
        ConnectionRepository ConnectionRepository = new ConnectionRepository();
        public DataTable MontoTotalIncentivos(DateTime fechaini, DateTime fechafin)
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CalculIncentivosxModulo";
            cmd.Connection = con.GetConecctionC();


            SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

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

        public DataTable PRODUCCIONXMODULO(DateTime fechaini, DateTime fechafin)
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PRODUCCIONXMODULO";
            cmd.Connection = con.GetConecctionC();


            SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

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

        public DataTable PRODUCCIONXMODULOXDIA(DateTime fechaini, DateTime fechafin, int modulo)
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CosProduccionxDiaxModulo";
            cmd.Connection = con.GetConecctionC();


            SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

            SqlParameter p3 = new SqlParameter("@modulo", System.Data.SqlDbType.Int);
            p3.Value = modulo;
            cmd.Parameters.Add(p3);

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
        public DataTable CosProduccionporDiaRangoFecha(DateTime fechaini, DateTime fechafin)
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 240;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CosProduccionporDiaRangoFecha";
            cmd.Connection = con.GetConecctionC();


            SqlParameter p1 = new SqlParameter("@fechaini", System.Data.SqlDbType.Date);
            p1.Value = fechaini;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@fechafin", System.Data.SqlDbType.Date);
            p2.Value = fechafin;
            cmd.Parameters.Add(p2);

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

        public DataTable TrasladoDZEfectivos(int periodo, int semana) //
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 240;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TrasladoDZEfectivos";
            cmd.Connection = con.GetConecction();


            SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);

            SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

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
        public DataTable incentivos()
        {

            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerIncentivos";
            cmd.Connection = con.GetConecctionC();

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
        public DataTable obtenerModulos()
        {
			
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "obtenerModulos";
            cmd.Connection = con.GetConecctionC();

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

        public DataTable EmpleadoOperacion()
        {

            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmpleadoOperacion";
            cmd.Connection = con.GetConecction();

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

        public DataTable ParametosIngresosDeduccionesIncentivos(int tipoDeduccion)
        {

            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametosIngresosDeduccionesIncentivos";
            cmd.Connection = con.GetConecctionC();

            SqlParameter p1 = new SqlParameter("@tipoDeduccion", System.Data.SqlDbType.Int);
            p1.Value = tipoDeduccion;
            cmd.Parameters.Add(p1);

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

        public DataTable ParametosIngresosDeduccionesDepencias(int id)
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ParametosIngresosDeduccionesDepencias";
            cmd.Connection = con.GetConecctionC();


            SqlParameter p1 = new SqlParameter("@IdIngresDeducP", System.Data.SqlDbType.Int);
            p1.Value = id;
            cmd.Parameters.Add(p1);

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

        public bool IncentivosHistoricoInsert(int idEmpresa, int codigo, int modulo, string estilo, string operacion, string construccion, decimal produccion, decimal metaAlcanzada, int amonestacion, decimal diasLaborados, decimal diasAusencia, decimal AusenciaJustificada, decimal AusenciaInJustificada, decimal TotalAusencia, decimal Eficiencia, decimal IncentivoMeta, decimal totalIngreso, decimal totalEgreso, decimal totalIncentivo, int periodo, int semana, string usuario, int tipoing, string comentario,bool GeneradoSistema)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivosHistoricoInsert";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@modulo", System.Data.SqlDbType.Int);
            p3.Value = modulo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@estilo", System.Data.SqlDbType.NChar);
            p1.Value = estilo;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@operacion", System.Data.SqlDbType.NChar);
            p4.Value = operacion;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@Construccion", System.Data.SqlDbType.VarChar);
            p5.Value = construccion;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@produccion", System.Data.SqlDbType.Decimal);
            p6.Value = produccion;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@metaAlcanzada", System.Data.SqlDbType.Decimal);
            p7.Value = metaAlcanzada;
            cmd.Parameters.Add(p7);

            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@Amonestacion", System.Data.SqlDbType.Int);
            p8.Value = amonestacion;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p9 = new SqlParameter("@DiasLaborados", System.Data.SqlDbType.Int);
            p9.Value = diasLaborados;
            cmd.Parameters.Add(p9);

            System.Data.SqlClient.SqlParameter p10 = new SqlParameter("@DiasAusencia", System.Data.SqlDbType.Int);
            p10.Value = diasAusencia;
            cmd.Parameters.Add(p10);

            System.Data.SqlClient.SqlParameter p11 = new SqlParameter("@AusenciaJustificadas", System.Data.SqlDbType.Int);
            p11.Value = AusenciaJustificada;
            cmd.Parameters.Add(p11);

            System.Data.SqlClient.SqlParameter p12 = new SqlParameter("@AusenciaInJustificadas", System.Data.SqlDbType.Int);
            p12.Value = AusenciaInJustificada;
            cmd.Parameters.Add(p12);

            System.Data.SqlClient.SqlParameter p13 = new SqlParameter("@TotalAusencia", System.Data.SqlDbType.Decimal);
            p13.Value = TotalAusencia;
            cmd.Parameters.Add(p13);

            System.Data.SqlClient.SqlParameter p14 = new SqlParameter("@EficienciaAlacanzada", System.Data.SqlDbType.Decimal);
            p14.Value = Eficiencia;
            cmd.Parameters.Add(p14);

            System.Data.SqlClient.SqlParameter p15 = new SqlParameter("@IncentivoMeta", System.Data.SqlDbType.Decimal);
            p15.Value = IncentivoMeta;
            cmd.Parameters.Add(p15);

            System.Data.SqlClient.SqlParameter p16 = new SqlParameter("@TotalIngreso", System.Data.SqlDbType.Decimal);
            p16.Value = totalIngreso;
            cmd.Parameters.Add(p16);

            System.Data.SqlClient.SqlParameter p17 = new SqlParameter("@TotalDeduccion", System.Data.SqlDbType.Decimal);
            p17.Value = totalEgreso;
            cmd.Parameters.Add(p17);

            System.Data.SqlClient.SqlParameter p18 = new SqlParameter("@TotalIncentivo", System.Data.SqlDbType.Decimal);
            p18.Value = totalIncentivo;
            cmd.Parameters.Add(p18);

            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@usuario", System.Data.SqlDbType.VarChar);
            p21.Value = usuario;
            cmd.Parameters.Add(p21);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@fechaG", System.Data.SqlDbType.Date);
            p22.Value = DateTime.Now;
            cmd.Parameters.Add(p22);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@horaG", System.Data.SqlDbType.Time);
            p23.Value = DateTime.Now.TimeOfDay;
            cmd.Parameters.Add(p23);

            System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@idtipoing", System.Data.SqlDbType.Int);
            p24.Value = tipoing;
            cmd.Parameters.Add(p24);

            System.Data.SqlClient.SqlParameter p25 = new SqlParameter("@Comentario", System.Data.SqlDbType.VarChar);
            p25.Value = comentario;
            cmd.Parameters.Add(p25);

              System.Data.SqlClient.SqlParameter p26 = new SqlParameter("@GeneradoSistema", System.Data.SqlDbType.Bit);
            p26.Value = GeneradoSistema;
            cmd.Parameters.Add(p26);


            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }

        public DataTable IncentivoHistoricoSelect(int idEmpresa, int periodo, int semana)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoHistoricoSelectAll";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

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
        public DataTable IncentivoHistoricoSelectDia(int idEmpresa, int periodo, int semana)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoHistoricoSelectAllDia";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

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


		public DataTable PlnConsultarIncentivoDetallexEmp(int periodo, int semana, int codigo, int idEmpresa)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnConsultarIncentivoDetallexEmp";
			cmd.Connection = sqlConnection;
			SqlParameter p19 = new SqlParameter("@periodo", SqlDbType.Int);
			p19.Value = periodo;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@semana", SqlDbType.Int);
			p20.Value = semana;
			cmd.Parameters.Add(p20);
			SqlParameter p21 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
			p21.Value = codigo;
			cmd.Parameters.Add(p21);
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

		public DataTable PlnConsultarSubCategoriaIncxEmp(int periodo, int semana, int codigo, int idtipoing, int idEmpresa)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnConsultarSubCategoriaIncxEmp";
			cmd.Connection = sqlConnection;
			SqlParameter p19 = new SqlParameter("@periodo", SqlDbType.Int);
			p19.Value = periodo;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@semana", SqlDbType.Int);
			p20.Value = semana;
			cmd.Parameters.Add(p20);
			SqlParameter p21 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
			p21.Value = codigo;
			cmd.Parameters.Add(p21);
			SqlParameter p22 = new SqlParameter("@idtipoing", SqlDbType.Int);
			p22.Value = idtipoing;
			cmd.Parameters.Add(p22);
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
		public DataTable IncentivoConsolidadoSelect(int idEmpresa, int periodo, int semana)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "IncentivoConsolidadoSelect";
			cmd.Connection = sqlConnection;
			SqlParameter p19 = new SqlParameter("@periodo", SqlDbType.Int);
			p19.Value = periodo;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@semana", SqlDbType.Int);
			p20.Value = semana;
			cmd.Parameters.Add(p20);
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

		public DataTable IncentivoConsolidadoSelectxPeriodo(int idEmpresa, int periodo, int periodo2, int semana)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "IncentivoConsolidadoSelectxPeriodo";
			cmd.Connection = sqlConnection;
			SqlParameter p12 = new SqlParameter("@periodo", SqlDbType.Int);
			p12.Value = periodo;
			cmd.Parameters.Add(p12);
			SqlParameter p11 = new SqlParameter("@periodo2", SqlDbType.Int);
			p11.Value = periodo2;
			cmd.Parameters.Add(p11);
			SqlParameter p13 = new SqlParameter("@semana", SqlDbType.Int);
			p13.Value = semana;
			cmd.Parameters.Add(p13);
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

		public DataTable ObtenerIncentivoPlantaSel(int idEmpresa, int periodo, int semana)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "ObtenerIncentivoPlantaSel";
			cmd.Connection = sqlConnection;
			SqlParameter p19 = new SqlParameter("@periodo", SqlDbType.Int);
			p19.Value = periodo;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@semana", SqlDbType.Int);
			p20.Value = semana;
			cmd.Parameters.Add(p20);
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

		public DataTable ObtenerIncentivoPlantaVATSel(int idEmpresa, int periodo, int semana)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "ObtenerIncentivoPlantaVATSel";
			cmd.Connection = sqlConnection;
			SqlParameter p19 = new SqlParameter("@periodo", SqlDbType.Int);
			p19.Value = periodo;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@semana", SqlDbType.Int);
			p20.Value = semana;
			cmd.Parameters.Add(p20);
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
		public DataTable IncentivoIngDedExcepcionesSel(int idEmpresa, int periodo, int semana)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "IncentivoIngDedExcepcionesSel";
			cmd.Connection = sqlConnection;
			SqlParameter p19 = new SqlParameter("@periodo", SqlDbType.Int);
			p19.Value = periodo;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@semana", SqlDbType.Int);
			p20.Value = semana;
			cmd.Parameters.Add(p20);
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
		public DataTable PlnObtenerLayoutxEstilo()
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerLayoutxEstilo";
			cmd.Connection = con.GetConecctionC();
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnPagoIncentivobyCutSel(int periodo)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnPagoIncentivobyCutSel";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
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
		public DataTable IncentivoIngDedccLOGxEmpleado(int idEmpresa, int periodo, int semana)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoIngDedccLOGxEmpleado";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

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

        public DataTable IncentivoHistoricoSelectCodigo(int idEmpresa, int periodo, int semana, int codigo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoHistoricoSelectCodigo";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p21.Value = codigo;
            cmd.Parameters.Add(p21);

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

        public DataTable IncentivoHistoricoSelectModulo(int idEmpresa, int periodo, int semana, int modulo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoHistoricoSelectModulo";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@modulo", System.Data.SqlDbType.Int);
            p21.Value = modulo;
            cmd.Parameters.Add(p21);

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

        public DataTable IncentivoModulosConMeta(int idEmpresa, int periodo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoModulosConMeta";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

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

        public DataTable IncentivoHistoricoSelectCodigoEmpleados(int idEmpresa, int periodo, int TIPO, int PARAMETRO)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoHistoricoSelectCodigoEmpleados";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p20.Value = TIPO;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@param", System.Data.SqlDbType.Int);
            p21.Value = PARAMETRO;
            cmd.Parameters.Add(p21);

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

        public DataTable IncentivoIngDedccLOGSelect(int idEmpresa, int periodo, int semana, int codigo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoIngDedccLOGSelect";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p21.Value = codigo;
            cmd.Parameters.Add(p21);

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

        public bool IncentivoHistoricoDZTrasInsert(int idEmpresa, int periodo, int semana, string codigoOrigen, string codigoDestino, decimal Dz, string observacion, string comentario)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoHistoricoDZTrasInsert";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@codigoDestino", System.Data.SqlDbType.VarChar);
            p3.Value = codigoDestino;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@codigoOrigen", System.Data.SqlDbType.VarChar);
            p4.Value = codigoOrigen;
            cmd.Parameters.Add(p4);


            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@Dz", System.Data.SqlDbType.Decimal);
            p5.Value = Dz;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p6.Value = observacion;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@comentario", System.Data.SqlDbType.VarChar);
            p7.Value = comentario;
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

        public bool IncentivoHistoricoDZTrasDelete(int idEmpresa, int periodo, int semana)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoHistoricoDZTrasDelete";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
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

        public DataTable CosModulosSel()
        {
            DataTable ds = new DataTable();
            CnBD con = new CnBD();

            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CosModulosSel";
            cmd.Connection = con.GetConecctionC();
            System.Data.SqlClient.SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            return ds;
        }
        public DataTable PlnDEpartamentosObtenerModulo(int idEmpresa, int codigoDepto)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PlnDEpartamentosObtenerModulo";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@codigo_depto", System.Data.SqlDbType.Int);
            p19.Value = codigoDepto;
            cmd.Parameters.Add(p19);



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

        public bool IncentivosTrasladosEspecialesAutorizadosInsert(int idEmpresa, int periodo, int semana, string codigo, string OP, string UsuarioAutoriza)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivosTrasladosEspecialesAutorizadosInsert";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@Periodo", System.Data.SqlDbType.Int);
            p1.Value = periodo;
            cmd.Parameters.Add(p1);


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p2.Value = semana;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@Codigo", System.Data.SqlDbType.NChar);
            p3.Value = codigo;
            cmd.Parameters.Add(p3);


            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@Operacion", System.Data.SqlDbType.NChar);
            p6.Value = OP;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@UsuarioAutoriza", System.Data.SqlDbType.NChar);
            p7.Value = UsuarioAutoriza;
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

        public DataTable EmpleadosPagosXOperacionSelect(int idEmpresa)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmpleadosPagosXOperacionSelect";
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
        public DataTable plnDevengadoSelect(int idEmpresa)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "plnDevengadoSelect";
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

        public bool IncentivoAQLxModuloInsert(int idEmpresa, int semana, int periodo, string Modulo, decimal AQL, decimal AQL_Meta, decimal Porcentaje)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoAQLxModuloInsert";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p1.Value = semana;
            cmd.Parameters.Add(p1);


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@Modulo", System.Data.SqlDbType.Int);
            p3.Value = int.Parse(Modulo);
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@AQL", System.Data.SqlDbType.Decimal);
            p4.Value = AQL;
            cmd.Parameters.Add(p4);


            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@AQL_Meta", System.Data.SqlDbType.Decimal);
            p5.Value = AQL_Meta;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@Porcentaje", System.Data.SqlDbType.Decimal);
            p6.Value = Porcentaje;
            cmd.Parameters.Add(p6);


            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (SystemException e)
            {
                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
                return false;
            }
            return true;

        }

        public DataTable IncentivoAQLxModuloSelect(int idEmpresa, int semana, int periodo, int Modulo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoAQLxModuloSelect";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p1.Value = semana;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@Modulo", System.Data.SqlDbType.Int);
            p3.Value = Modulo;
            cmd.Parameters.Add(p3);

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

        public DataTable IncentivoAQLxModuloSelectSemanaPEriodo(int idEmpresa, int semana, int periodo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoAQLxModuloSelectSemanaPEriodo";
            cmd.Connection = sqlConnection;

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p1.Value = semana;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);


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
        public DataTable IncentivosHistoricoGetXEmpleado(int idEmpresa, int semana, int periodo, string codigo, int idtipo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivosHistoricoGetXEmpleado";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p1.Value = semana;
            cmd.Parameters.Add(p1);


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@empleado", System.Data.SqlDbType.VarChar);
            p3.Value = codigo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@idtipo", System.Data.SqlDbType.Int);
            p4.Value = idtipo;
            cmd.Parameters.Add(p4);

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
        public bool IncentivosHistoricoDelete(int idEmpresa, int periodo, int semana, int tipoIng, bool GeneradoSistema)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivosHistoricoDelete";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@tipoIng", System.Data.SqlDbType.Int);
            p21.Value = tipoIng;
            cmd.Parameters.Add(p21);


            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@tipoGeneracion", System.Data.SqlDbType.Bit);
            p22.Value = GeneradoSistema;
            cmd.Parameters.Add(p22);




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



        public bool IncentivoIngDedccLOGDelete(int idEmpresa, int periodo, int semana, int tipoIng, bool GeneradoSistema)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoIngDedccLOGDelete";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@tipoIng", System.Data.SqlDbType.Int);
            p21.Value = tipoIng;
            cmd.Parameters.Add(p21);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@GeneradoSistema", System.Data.SqlDbType.Bit);
            p22.Value = GeneradoSistema;
            cmd.Parameters.Add(p22);


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


        //////////////////////////////NUEVO/////////////////////////////////////////
        public DataTable IncentivosHistoricoGetXEmpleado(int idEmpresa, int semana, int periodo, string codigo)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivosHistoricoGetXEmpleado";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p1.Value = semana;
            cmd.Parameters.Add(p1);


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p2.Value = periodo;
            cmd.Parameters.Add(p2);

            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@empleado", System.Data.SqlDbType.VarChar);
            p3.Value = codigo;
            cmd.Parameters.Add(p3);

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
        ///////////////////////////////////////////////////////////////////////////////////////////

        public bool IncentivoIngDedccLOGInsert(int idEmpresa, int codigo, int periodo, int semana, int tipo, string detalle, int tipoCalc, decimal cantida, decimal valor, string observacion, int tipoing, bool GeneradoSistema)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoIngDedccLOGInsert";
            cmd.Connection = sqlConnection;


            System.Data.SqlClient.SqlParameter p2 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p2.Value = codigo;
            cmd.Parameters.Add(p2);


            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);


            System.Data.SqlClient.SqlParameter p3 = new SqlParameter("@tipo", System.Data.SqlDbType.Int);
            p3.Value = tipo;
            cmd.Parameters.Add(p3);

            System.Data.SqlClient.SqlParameter p1 = new SqlParameter("@detalle", System.Data.SqlDbType.VarChar);
            p1.Value = detalle;
            cmd.Parameters.Add(p1);

            System.Data.SqlClient.SqlParameter p4 = new SqlParameter("@tipoCalc", System.Data.SqlDbType.Int);
            p4.Value = tipoCalc;
            cmd.Parameters.Add(p4);

            System.Data.SqlClient.SqlParameter p5 = new SqlParameter("@cantidad", System.Data.SqlDbType.Decimal);
            p5.Value = cantida;
            cmd.Parameters.Add(p5);

            System.Data.SqlClient.SqlParameter p6 = new SqlParameter("@valor", System.Data.SqlDbType.Decimal);
            p6.Value = valor;
            cmd.Parameters.Add(p6);

            System.Data.SqlClient.SqlParameter p7 = new SqlParameter("@observacion", System.Data.SqlDbType.VarChar);
            p7.Value = observacion;
            cmd.Parameters.Add(p7);


            System.Data.SqlClient.SqlParameter p8 = new SqlParameter("@IdTipoIng", System.Data.SqlDbType.VarChar);
            p8.Value = tipoing;
            cmd.Parameters.Add(p8);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@GeneradoSistema", System.Data.SqlDbType.Bit);
            p22.Value = GeneradoSistema;
            cmd.Parameters.Add(p22);

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
        public DataTable EmpleadosPagosXOperacionGetPagossFijos(int idEmpresa)
        {
            DataTable ds = new DataTable();
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "EmpleadosPagosXOperacionGetPagossFijos";
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

        public bool IncentivoIngDedccLOGDeletexEmpleado(int idEmpresa, int periodo, int semana, int tipoIng, bool GeneradoSistema, int codigo, int idtipoProceso)
        {
            SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
            CnBD con = new CnBD();
            System.Data.SqlClient.SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "IncentivoIngDedccLOGDeletexEmpleado";
            cmd.Connection = sqlConnection;



            System.Data.SqlClient.SqlParameter p19 = new SqlParameter("@periodo", System.Data.SqlDbType.Int);
            p19.Value = periodo;
            cmd.Parameters.Add(p19);

            System.Data.SqlClient.SqlParameter p20 = new SqlParameter("@semana", System.Data.SqlDbType.Int);
            p20.Value = semana;
            cmd.Parameters.Add(p20);

            System.Data.SqlClient.SqlParameter p21 = new SqlParameter("@tipoIng", System.Data.SqlDbType.Int);
            p21.Value = tipoIng;
            cmd.Parameters.Add(p21);

            System.Data.SqlClient.SqlParameter p22 = new SqlParameter("@GeneradoSistema", System.Data.SqlDbType.Bit);
            p22.Value = GeneradoSistema;
            cmd.Parameters.Add(p22);

            System.Data.SqlClient.SqlParameter p23 = new SqlParameter("@codigo", System.Data.SqlDbType.Int);
            p23.Value = codigo;
            cmd.Parameters.Add(p23);

            System.Data.SqlClient.SqlParameter p24 = new SqlParameter("@idtipoProceso", System.Data.SqlDbType.Int);
            p24.Value = idtipoProceso;
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
		////CONSULTAS PARA CALCULO DIARIO DE INCENTIVOS
		public DataTable PlnObtenerTablaIncentivo()
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerTablaIncentivo";
			cmd.Connection = con.GetConecctionC();
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

		public DataTable PlnModuloIncentivoAreaSel()
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnModuloIncentivoAreaSel";
			cmd.Connection = con.GetConecctionC();
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

		public DataTable PlnParametroCalculoIncEmpSel(int periodo, int semana, int idempresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idempresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnParametroCalculoIncEmpSel";
			cmd.Connection = sqlConnection;
			DataTable ds = new DataTable();
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@semana", SqlDbType.Int);
			p2.Value = semana;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnConfigPeriodoIncentivoSel(DateTime fechaini, DateTime fechafin, int idEmpresa)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnConfigPeriodoIncentivoSel";
			cmd.Connection = sqlConnection;
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerOperacionCriticaSel(int idEmpresa)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerOperacionCritica";
			cmd.Connection = sqlConnection;
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerProteccionModulo(DateTime fechaini, DateTime fechafin)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerProteccionModulo";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
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

		public DataTable PlnObtenerPersonalIncFueraLayout(int periodo, int filtro, int idEmpresa)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerPersonalIncFueraLayout";
			cmd.Connection = sqlConnection;
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@filtro", SqlDbType.Int);
			p2.Value = filtro;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerEficienciaModuloHist(DateTime fechaini, DateTime fechafin)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerEficienciaMod";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
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

		public DataTable PlnObtenerEficienciaModuloByPeriodo(int periodo, int semana)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerEficienciaModuloByPeriodo";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@semana", SqlDbType.Int);
			p2.Value = semana;
			cmd.Parameters.Add(p2);
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

		public DataTable PlnObtenerProduccionAprobadaByPeriodo(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int vista, int periodo)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerProduccionAprobadaByPeriodo";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			SqlParameter p3 = new SqlParameter("@fechaaprobacion", SqlDbType.Date);
			p3.Value = corteaprobacion;
			cmd.Parameters.Add(p3);
			SqlParameter p4 = new SqlParameter("@vista", SqlDbType.Int);
			p4.Value = vista;
			cmd.Parameters.Add(p4);
			SqlParameter p5 = new SqlParameter("@periodo", SqlDbType.Int);
			p5.Value = periodo;
			cmd.Parameters.Add(p5);
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

		public DataTable PlnObtenerProdPendienteByPeriodo(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int periodo)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerProdPendienteByPeriodo";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			SqlParameter p3 = new SqlParameter("@fechaaprobacion", SqlDbType.Date);
			p3.Value = corteaprobacion;
			cmd.Parameters.Add(p3);
			SqlParameter p4 = new SqlParameter("@periodo", SqlDbType.Int);
			p4.Value = periodo;
			cmd.Parameters.Add(p4);
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

		public DataTable PlnObtenerProduccionByPeriodo(DateTime fechaini, DateTime fechafin)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerProduccionByPeriodo";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerProduccionDetalleByPeriodo(DateTime fechaini, DateTime fechafin)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerProduccionDetalleByPeriodo";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerIncidenciasCortesProd(DateTime fechaini, DateTime fechafin)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerIncidenciasCortesProd";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerOQLModulosProd(DateTime fechaini, DateTime fechafin, int tipo)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerOQLModulosProd";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			SqlParameter p3 = new SqlParameter("@tipo", SqlDbType.Int);
			p3.Value = tipo;
			cmd.Parameters.Add(p3);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnOqlPeriodoPagoSel(int periodo)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnOqlPeriodoPagoSel";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerProduccionBono(DateTime fechaini, DateTime fechafin)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerProduccionBono";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerIngresosPTByPeriodo(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int periodo)
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerIngresosPTByPeriodo";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			SqlParameter p3 = new SqlParameter("@fechaaprobacion", SqlDbType.Date);
			p3.Value = corteaprobacion;
			cmd.Parameters.Add(p3);
			SqlParameter p4 = new SqlParameter("@periodo", SqlDbType.Int);
			p4.Value = periodo;
			cmd.Parameters.Add(p4);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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

		public DataTable PlnObtenerRangoOql()
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerRangoOql";
			cmd.Connection = con.GetConecctionC();
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

		public DataTable PlnTablaOperacionCriticaSel()
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnTablaOperacionCriticaSel";
			cmd.Connection = con.GetConecctionC();
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

		public DataTable PlnObtenerRangoAdicionales()
		{
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerRangoAdicionales";
			cmd.Connection = con.GetConecctionC();
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

		public bool PlnPagoIncentivoByCutIns(DateTime fechap, DateTime fechaap, string modulo, string corte, int seccion, int estilo, int idconst, decimal oql, decimal dzdia, decimal dzpagar, decimal costo, int periodo, int semana, decimal montot, string user)
		{
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnPagoIncentivoByCutIns";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fecha_producido", SqlDbType.Date);
			p1.Value = fechap;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fecha_aprobado", SqlDbType.Date);
			p2.Value = fechaap;
			cmd.Parameters.Add(p2);
			SqlParameter p8 = new SqlParameter("@modulo", SqlDbType.NChar);
			p8.Value = modulo;
			cmd.Parameters.Add(p8);
			SqlParameter p9 = new SqlParameter("@corte", SqlDbType.NChar);
			p9.Value = corte;
			cmd.Parameters.Add(p9);
			SqlParameter p10 = new SqlParameter("@seccion", SqlDbType.Int);
			p10.Value = seccion;
			cmd.Parameters.Add(p10);
			SqlParameter p11 = new SqlParameter("@estilo", SqlDbType.Int);
			p11.Value = estilo;
			cmd.Parameters.Add(p11);
			SqlParameter p13 = new SqlParameter("@idConstrInc", SqlDbType.Int);
			p13.Value = idconst;
			cmd.Parameters.Add(p13);
			SqlParameter p15 = new SqlParameter("@oql", SqlDbType.Decimal);
			p15.Value = oql;
			cmd.Parameters.Add(p15);
			SqlParameter p3 = new SqlParameter("@dzDia", SqlDbType.Decimal);
			p3.Value = dzdia;
			cmd.Parameters.Add(p3);
			SqlParameter p4 = new SqlParameter("@dzPagar", SqlDbType.Decimal);
			p4.Value = dzpagar;
			cmd.Parameters.Add(p4);
			SqlParameter p5 = new SqlParameter("@costo", SqlDbType.Decimal);
			p5.Value = costo;
			cmd.Parameters.Add(p5);
			SqlParameter p6 = new SqlParameter("@montoPagar", SqlDbType.Decimal);
			p6.Value = montot;
			cmd.Parameters.Add(p6);
			SqlParameter p12 = new SqlParameter("@periodo", SqlDbType.Int);
			p12.Value = periodo;
			cmd.Parameters.Add(p12);
			SqlParameter p14 = new SqlParameter("@semana", SqlDbType.Int);
			p14.Value = semana;
			cmd.Parameters.Add(p14);
			SqlParameter p7 = new SqlParameter("@usuarioGraba", SqlDbType.NChar);
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
				if (cmd.Connection.State != 0)
				{
					cmd.Connection.Close();
				}
				return false;
			}
			return true;
		}

		public bool PlnProteccionModuloIns(DateTime fecha, string modulo, string problema, decimal dz, string observacion, string user)
		{
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnProteccionModuloIns";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p1 = new SqlParameter("@fecha", SqlDbType.Date);
			p1.Value = fecha;
			cmd.Parameters.Add(p1);
			SqlParameter p4 = new SqlParameter("@modulo", SqlDbType.NChar);
			p4.Value = modulo;
			cmd.Parameters.Add(p4);
			SqlParameter p5 = new SqlParameter("@problema", SqlDbType.NChar);
			p5.Value = problema;
			cmd.Parameters.Add(p5);
			SqlParameter p6 = new SqlParameter("@observacion", SqlDbType.NChar);
			p6.Value = observacion;
			cmd.Parameters.Add(p6);
			SqlParameter p2 = new SqlParameter("@dz", SqlDbType.Decimal);
			p2.Value = dz;
			cmd.Parameters.Add(p2);
			SqlParameter p3 = new SqlParameter("@usuariograba", SqlDbType.NChar);
			p3.Value = user;
			cmd.Parameters.Add(p3);
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

		public bool PlnPagoIncentivoEmpleadoByCutIns(int periodo, int semana, string modulo, DateTime fechap, string asistencia, decimal horas, int codigo, string nombre, string operacion, string corte, int seccion, int estilo, string color, string construccion, DateTime fechaapro, decimal oql, decimal dzdia, decimal dzpagar, decimal dzproteccion, decimal dzproteccionxsec, decimal dzprodprot, decimal dzpgprot, decimal costo, decimal montot, string user, int idempresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idempresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnPagoIncentivoEmpleadoByCutIns";
			cmd.Connection = sqlConnection;
			SqlParameter p2 = new SqlParameter("@periodo", SqlDbType.Int);
			p2.Value = periodo;
			cmd.Parameters.Add(p2);
			SqlParameter p5 = new SqlParameter("@semana", SqlDbType.Int);
			p5.Value = semana;
			cmd.Parameters.Add(p5);
			SqlParameter p13 = new SqlParameter("@modulo", SqlDbType.NChar);
			p13.Value = modulo;
			cmd.Parameters.Add(p13);
			SqlParameter p1 = new SqlParameter("@fecha_producido", SqlDbType.Date);
			p1.Value = fechap;
			cmd.Parameters.Add(p1);
			SqlParameter p15 = new SqlParameter("@asistencia", SqlDbType.NChar);
			p15.Value = asistencia;
			cmd.Parameters.Add(p15);
			SqlParameter p16 = new SqlParameter("@horas", SqlDbType.Decimal);
			p16.Value = horas;
			cmd.Parameters.Add(p16);
			SqlParameter p17 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
			p17.Value = codigo;
			cmd.Parameters.Add(p17);
			SqlParameter p18 = new SqlParameter("@nombrecompleto", SqlDbType.NChar);
			p18.Value = nombre;
			cmd.Parameters.Add(p18);
			SqlParameter p19 = new SqlParameter("@operacion", SqlDbType.NChar);
			p19.Value = operacion;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@corte", SqlDbType.NChar);
			p20.Value = corte;
			cmd.Parameters.Add(p20);
			SqlParameter p21 = new SqlParameter("@seccion", SqlDbType.Int);
			p21.Value = seccion;
			cmd.Parameters.Add(p21);
			SqlParameter p22 = new SqlParameter("@estilo", SqlDbType.Int);
			p22.Value = estilo;
			cmd.Parameters.Add(p22);
			SqlParameter p23 = new SqlParameter("@color", SqlDbType.NVarChar);
			p23.Value = color;
			cmd.Parameters.Add(p23);
			SqlParameter p24 = new SqlParameter("@construccion", SqlDbType.NChar);
			p24.Value = construccion;
			cmd.Parameters.Add(p24);
			SqlParameter p3 = new SqlParameter("@fecha_aprobado", SqlDbType.Date);
			p3.Value = fechaapro;
			cmd.Parameters.Add(p3);
			SqlParameter p25 = new SqlParameter("@oql", SqlDbType.Decimal);
			p25.Value = oql;
			cmd.Parameters.Add(p25);
			SqlParameter p4 = new SqlParameter("@dzDia", SqlDbType.Decimal);
			p4.Value = dzdia;
			cmd.Parameters.Add(p4);
			SqlParameter p9 = new SqlParameter("@dzPagar", SqlDbType.Decimal);
			p9.Value = dzpagar;
			cmd.Parameters.Add(p9);
			SqlParameter p10 = new SqlParameter("@dzProteccion", SqlDbType.Decimal);
			p10.Value = dzproteccion;
			cmd.Parameters.Add(p10);
			SqlParameter p11 = new SqlParameter("@dzProtxSec", SqlDbType.Decimal);
			p11.Value = dzproteccionxsec;
			cmd.Parameters.Add(p11);
			SqlParameter p12 = new SqlParameter("@dzProdProt", SqlDbType.Decimal);
			p12.Value = dzprodprot;
			cmd.Parameters.Add(p12);
			SqlParameter p14 = new SqlParameter("@dzPgProt", SqlDbType.Decimal);
			p14.Value = dzpgprot;
			cmd.Parameters.Add(p14);
			SqlParameter p6 = new SqlParameter("@costo", SqlDbType.Decimal);
			p6.Value = costo;
			cmd.Parameters.Add(p6);
			SqlParameter p7 = new SqlParameter("@montoPagar", SqlDbType.Decimal);
			p7.Value = montot;
			cmd.Parameters.Add(p7);
			SqlParameter p8 = new SqlParameter("@usuarioGraba", SqlDbType.NChar);
			p8.Value = user;
			cmd.Parameters.Add(p8);
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

		public bool PlnIncentivoPendPagarxPeriodoIns(int periodo, int semana, string modulo, DateTime fechap, decimal horas, int codigo, string corte, int seccion, int estilo, string color, string construccion, string subestatus, decimal dzdia, decimal dzpagar, decimal oql, decimal costo, decimal montodoc, string user, int idempresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idempresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnIncentivoPendPagarxPeriodoIns";
			cmd.Connection = sqlConnection;
			SqlParameter p2 = new SqlParameter("@periodo", SqlDbType.Int);
			p2.Value = periodo;
			cmd.Parameters.Add(p2);
			SqlParameter p5 = new SqlParameter("@semana", SqlDbType.Int);
			p5.Value = semana;
			cmd.Parameters.Add(p5);
			SqlParameter p12 = new SqlParameter("@modulo", SqlDbType.NChar);
			p12.Value = modulo;
			cmd.Parameters.Add(p12);
			SqlParameter p1 = new SqlParameter("@fecha_producido", SqlDbType.Date);
			p1.Value = fechap;
			cmd.Parameters.Add(p1);
			SqlParameter p4 = new SqlParameter("@horas", SqlDbType.Decimal);
			p4.Value = horas;
			cmd.Parameters.Add(p4);
			SqlParameter p13 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
			p13.Value = codigo;
			cmd.Parameters.Add(p13);
			SqlParameter p14 = new SqlParameter("@corte", SqlDbType.NChar);
			p14.Value = corte;
			cmd.Parameters.Add(p14);
			SqlParameter p15 = new SqlParameter("@seccion", SqlDbType.Int);
			p15.Value = seccion;
			cmd.Parameters.Add(p15);
			SqlParameter p16 = new SqlParameter("@estilo", SqlDbType.Int);
			p16.Value = estilo;
			cmd.Parameters.Add(p16);
			SqlParameter p17 = new SqlParameter("@color", SqlDbType.NVarChar);
			p17.Value = color;
			cmd.Parameters.Add(p17);
			SqlParameter p18 = new SqlParameter("@construccion", SqlDbType.NChar);
			p18.Value = construccion;
			cmd.Parameters.Add(p18);
			SqlParameter p7 = new SqlParameter("@subestatus", SqlDbType.NChar);
			p7.Value = subestatus;
			cmd.Parameters.Add(p7);
			SqlParameter p3 = new SqlParameter("@dzDia", SqlDbType.Decimal);
			p3.Value = dzdia;
			cmd.Parameters.Add(p3);
			SqlParameter p8 = new SqlParameter("@dzPagar", SqlDbType.Decimal);
			p8.Value = dzpagar;
			cmd.Parameters.Add(p8);
			SqlParameter p9 = new SqlParameter("@oql", SqlDbType.Decimal);
			p9.Value = oql;
			cmd.Parameters.Add(p9);
			SqlParameter p10 = new SqlParameter("@costo", SqlDbType.Decimal);
			p10.Value = costo;
			cmd.Parameters.Add(p10);
			SqlParameter p11 = new SqlParameter("@montodocenas", SqlDbType.Decimal);
			p11.Value = montodoc;
			cmd.Parameters.Add(p11);
			SqlParameter p6 = new SqlParameter("@usuariograba", SqlDbType.NChar);
			p6.Value = user;
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

		public bool PlnPagoIncentivoEmpleadoIns(int periodo, int semana, string modulo, int codigo, string nombre, string operacion, decimal dzpagar, decimal bonoasistencia, int amonestaciones, decimal incentivo, decimal otrosingresos, decimal deducciones, decimal total, string user, int idtipoing, string comentario, bool generadosistema, int idempresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idempresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnPagoIncentivoEmpleadoIns";
			cmd.Connection = sqlConnection;
			SqlParameter p2 = new SqlParameter("@periodo", SqlDbType.Int);
			p2.Value = periodo;
			cmd.Parameters.Add(p2);
			SqlParameter p4 = new SqlParameter("@semana", SqlDbType.Int);
			p4.Value = semana;
			cmd.Parameters.Add(p4);
			SqlParameter p1 = new SqlParameter("@modulo", SqlDbType.NChar);
			if(modulo.Length > 10)
				modulo = modulo.Substring(0, 10);
			p1.Value = modulo;
			cmd.Parameters.Add(p1);
			SqlParameter p13 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
			p13.Value = codigo;
			cmd.Parameters.Add(p13);
			SqlParameter p14 = new SqlParameter("@nombrecompleto", SqlDbType.NChar);
			p14.Value = nombre;
			cmd.Parameters.Add(p14);
			SqlParameter p3 = new SqlParameter("@operacion", SqlDbType.NChar);
			p3.Value = operacion;
			cmd.Parameters.Add(p3);
			SqlParameter p7 = new SqlParameter("@dzpagar", SqlDbType.Decimal);
			p7.Value = dzpagar;
			cmd.Parameters.Add(p7);
			SqlParameter p5 = new SqlParameter("@bonoasistencia", SqlDbType.Decimal);
			p5.Value = bonoasistencia;
			cmd.Parameters.Add(p5);
			SqlParameter p10 = new SqlParameter("@amonestaciones", SqlDbType.Int);
			p10.Value = amonestaciones;
			cmd.Parameters.Add(p10);
			SqlParameter p8 = new SqlParameter("@incentivo", SqlDbType.Decimal);
			p8.Value = incentivo;
			cmd.Parameters.Add(p8);
			SqlParameter p11 = new SqlParameter("@otrosingresos", SqlDbType.Decimal);
			p11.Value = otrosingresos;
			cmd.Parameters.Add(p11);
			SqlParameter p12 = new SqlParameter("@deducciones", SqlDbType.Decimal);
			p12.Value = deducciones;
			cmd.Parameters.Add(p12);
			SqlParameter p9 = new SqlParameter("@total", SqlDbType.Decimal);
			p9.Value = total;
			cmd.Parameters.Add(p9);
			SqlParameter p6 = new SqlParameter("@usuarioGraba", SqlDbType.NChar);
			p6.Value = user;
			cmd.Parameters.Add(p6);
			SqlParameter p15 = new SqlParameter("@idtipoing", SqlDbType.Int);
			p15.Value = idtipoing;
			cmd.Parameters.Add(p15);
			SqlParameter p16 = new SqlParameter("@comentario", SqlDbType.NChar);
			p16.Value = comentario;
			cmd.Parameters.Add(p16);
			SqlParameter p17 = new SqlParameter("@generadosistema", SqlDbType.Bit);
			p17.Value = generadosistema;
			cmd.Parameters.Add(p17);
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
				cmd.Connection.Close();
			}
			catch (System.Exception ex)
			{
				if (cmd.Connection.State != 0)
				{
					string e = ex.Message;
					cmd.Connection.Close();
				}
				return false;
			}
			return true;
		}

		public void PlnPagoIncentivoEmpleadoIns(int idEmpresa, DataTable dtpln)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
			bulkCopy.DestinationTableName = "dbo.PlnPagoIncentivoEmpleado";
			try
			{
				sqlConnection.Open();
				bulkCopy.WriteToServer(dtpln);
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public void PlnPagoIncentivoEmpleadoByCutIns(int idEmpresa, DataTable dtpln)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
			try
			{
				sqlConnection.Open();
				bulkCopy.DestinationTableName = "dbo.PlnPagoIncentivoEmpleadobyCut";
				bulkCopy.WriteToServer(dtpln);
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public void PlnIncentivoPendPagarxPeriodoIns(int idEmpresa, DataTable dtpln)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
			bulkCopy.DestinationTableName = "dbo.PlnIncentivoPendPagarxPeriodo";
			try
			{
				sqlConnection.Open();
				bulkCopy.WriteToServer(dtpln);
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool PlnPagoIncentivoDel(int periodo, int semana, int idempresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idempresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnPagoIncentivoDel";
			cmd.Connection = sqlConnection;
			SqlParameter p11 = new SqlParameter("@periodo", SqlDbType.Int);
			p11.Value = periodo;
			cmd.Parameters.Add(p11);
			SqlParameter p12 = new SqlParameter("@semana", SqlDbType.Int);
			p12.Value = semana;
			cmd.Parameters.Add(p12);
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

		public bool PlnPagoIncentivobyCutDel(int periodo, int semana)
		{
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnPagoIncentivobyCutDel";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p11 = new SqlParameter("@periodo", SqlDbType.Int);
			p11.Value = periodo;
			cmd.Parameters.Add(p11);
			SqlParameter p12 = new SqlParameter("@semana", SqlDbType.Int);
			p12.Value = semana;
			cmd.Parameters.Add(p12);
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

		public bool PlnOqlPeriodoPagoDel(int periodo)
		{
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnOqlPeriodoPagoDel";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p11 = new SqlParameter("@periodo", SqlDbType.Int);
			p11.Value = periodo;
			cmd.Parameters.Add(p11);
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

		public bool PlnEficienciaModDel(int periodo)
		{
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnEficienciaModDel";
			cmd.Connection = con.GetConecctionC();
			SqlParameter p11 = new SqlParameter("@periodo", SqlDbType.Int);
			p11.Value = periodo;
			cmd.Parameters.Add(p11);
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

		public void PlnEficienciaModuloIns(DataTable dtpln)
		{
			CnBD con = new CnBD();
			SqlConnection sqlConnection = con.GetConecctionC();
			SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
			bulkCopy.DestinationTableName = "dbo.PlnEficienciaModulo";
			try
			{
				sqlConnection.Open();
				bulkCopy.WriteToServer(dtpln);
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public void PlnOqlPeriodoPagoIns(DataTable dtpln)
		{
			CnBD con = new CnBD();
			SqlConnection sqlConnection = con.GetConecctionC();
			 SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
			bulkCopy.DestinationTableName = "dbo.PlnOqlPeriodoPago";
			try
			{
				sqlConnection.Open();
				bulkCopy.WriteToServer(dtpln);
				sqlConnection.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable PlnObtenerProteccionIncentivoFijo(int periodo, int idEmpresa)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnObtenerProteccionIncentivoFijo";
			cmd.Connection = sqlConnection;
			SqlParameter p11 = new SqlParameter("@periodo", SqlDbType.Int);
			p11.Value = periodo;
			cmd.Parameters.Add(p11);
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

		public bool plnExcepcionesCalculoInc(int periodo, int semana, int idempresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idempresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "plnExcepcionesCalculoInc";
			cmd.Connection = sqlConnection;
			SqlParameter p11 = new SqlParameter("@periodo", SqlDbType.Int);
			p11.Value = periodo;
			cmd.Parameters.Add(p11);
			SqlParameter p12 = new SqlParameter("@semana", SqlDbType.Int);
			p12.Value = semana;
			cmd.Parameters.Add(p12);
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

		public bool PlnPagoIncentivoxRubroEmpleadoDel(int periodo, int semana, int codigo, int idtipoing, int idEmpresa)
		{
			DataTable ds = new DataTable();
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnPagoIncentivoxRubroEmpleadoDel";
			cmd.Connection = sqlConnection;
			SqlParameter p19 = new SqlParameter("@periodo", SqlDbType.Int);
			p19.Value = periodo;
			cmd.Parameters.Add(p19);
			SqlParameter p20 = new SqlParameter("@semana", SqlDbType.Int);
			p20.Value = semana;
			cmd.Parameters.Add(p20);
			SqlParameter p21 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
			p21.Value = codigo;
			cmd.Parameters.Add(p21);
			SqlParameter p22 = new SqlParameter("@idtipoing", SqlDbType.Int);
			p22.Value = idtipoing;
			cmd.Parameters.Add(p22);
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

		public bool PlnProteccionIncentivoFijoIns(int codigo, decimal bonoasistencia, decimal incentivo, decimal porcentaje, int repeticiones, bool recurrente, bool estado, string user, int idempresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idempresa);
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnProteccionIncentivoFijoIns";
			cmd.Connection = sqlConnection;
			SqlParameter p30 = new SqlParameter("@codigo_empleado", SqlDbType.Int);
			p30.Value = codigo;
			cmd.Parameters.Add(p30);
			SqlParameter p24 = new SqlParameter("@bonoasistencia_fijo", SqlDbType.Decimal);
			p24.Value = bonoasistencia;
			cmd.Parameters.Add(p24);
			SqlParameter p26 = new SqlParameter("@incentivo_fijo", SqlDbType.Decimal);
			p26.Value = incentivo;
			cmd.Parameters.Add(p26);
			SqlParameter p28 = new SqlParameter("@porcentaje", SqlDbType.Decimal);
			p28.Value = porcentaje;
			cmd.Parameters.Add(p28);
			SqlParameter p29 = new SqlParameter("@repeticiones", SqlDbType.Int);
			p29.Value = repeticiones;
			cmd.Parameters.Add(p29);
			SqlParameter p27 = new SqlParameter("@recurrente", SqlDbType.Bit);
			p27.Value = recurrente;
			cmd.Parameters.Add(p27);
			SqlParameter p25 = new SqlParameter("@usuarioGraba", SqlDbType.NChar);
			p25.Value = user;
			cmd.Parameters.Add(p25);
			SqlParameter p31 = new SqlParameter("@estado", SqlDbType.Bit);
			p31.Value = estado;
			cmd.Parameters.Add(p31);
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

		public DataTable PlnProteccionDzxFechaSel(DateTime fechaini, DateTime fechafin, int idEmpresa)
		{
			SqlConnection sqlConnection = ConnectionRepository.getConnection(idEmpresa);
			DataTable ds = new DataTable();
			CnBD con = new CnBD();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PlnProteccionDzxFechaSel";
			cmd.Connection = sqlConnection;
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			try
			{
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(ds);
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
	}
}
