using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class Dato_Eficiencia
    {
        public Dato_Eficiencia()
        {

        }

		public DataTable GetHorasTrabajadas(DateTime fechaini, DateTime fechafin, int param, int idEmpresa)
		{
			DataTable dt = new DataTable();
			ConnectionRepository conect = new ConnectionRepository();
			SqlConnection sqlConnection = conect.getConnection(idEmpresa);
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "HorasTrabajadas";
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			SqlParameter p3 = new SqlParameter("@param", SqlDbType.Int);
			p3.Value = param;
			cmd.Parameters.Add(p3);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			try
			{
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				string ms = ex.Message;
				if (cmd.Connection.State != 0)
				{
					cmd.Connection.Close();
				}
			}
			return dt;
		}

		public DataTable GetCostoManodeObra(int periodo, int semana, int idEmpresa)
		{
			DataTable dt = new DataTable();
			ConnectionRepository conect = new ConnectionRepository();
			SqlConnection sqlConnection = conect.getConnection(idEmpresa);
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "GetCostoManodeObra";
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@semana", SqlDbType.Int);
			p2.Value = semana;
			cmd.Parameters.Add(p2);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			try
			{
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				string ms = ex.Message;
				if (cmd.Connection.State != 0)
				{
					cmd.Connection.Close();
				}
			}
			return dt;
		}

		public DataTable plnPeriodoCostoSel(DateTime fechaini, int idEmpresa)
		{
			DataTable dt = new DataTable();
			ConnectionRepository conect = new ConnectionRepository();
			SqlConnection sqlConnection = new CnBD().GetConecctionC();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "plnPeriodoCostoSel";
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			try
			{
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				string ms = ex.Message;
				if (cmd.Connection.State != 0)
				{
					cmd.Connection.Close();
				}
			}
			return dt;
		}

		public DataTable GetPeriodo(int periodo, int idempresa)
		{
			DataTable dt = new DataTable();
			ConnectionRepository conect = new ConnectionRepository();
			SqlConnection sqlConnection = conect.getConnection(idempresa);
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "PeriodoSel";
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			try
			{
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				string ms = ex.Message;
				if (cmd.Connection.State != 0)
				{
					cmd.Connection.Close();
				}
			}
			return dt;
		}

		public DataTable GetCumplimientoxModulo(DateTime fechaini, DateTime fechafin, int idEmpresa)
		{
			DataTable dt = new DataTable();
			ConnectionRepository conect = new ConnectionRepository();
			SqlConnection sqlConnection = conect.getConnection(idEmpresa);
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "CosCumplimientoModulos";
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			try
			{
				da.Fill(dt);
			}
			catch (Exception ex)
			{
				string ms = ex.Message;
				if (cmd.Connection.State != 0)
				{
					cmd.Connection.Close();
				}
			}
			return dt;
		}

		public DataTable GetCosMinutosProducidos(DateTime fechaini, DateTime fechafin)
		{
			DataTable dt = new DataTable();
			SqlConnection sqlConnection = new CnBD().GetConecctionC();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "CosMinutosProducidos";
			SqlParameter p1 = new SqlParameter("@fechaini", SqlDbType.Date);
			p1.Value = fechaini;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@fechafin", SqlDbType.Date);
			p2.Value = fechafin;
			cmd.Parameters.Add(p2);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
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
			}
			return dt;
		}

		public DataTable GetLayoutPorModulo(DateTime fecha)
		{
			DataTable dt = new DataTable();
			SqlConnection sqlConnection = new CnBD().GetConecctionC();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "CosModulosLayout";
			SqlParameter p1 = new SqlParameter("@fecha", SqlDbType.Date);
			p1.Value = fecha;
			cmd.Parameters.Add(p1);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
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
			}
			return dt;
		}

		public DataTable GetEmpleadosModulos(DateTime fecha, int idempresa)
		{
			DataTable dt = new DataTable();
			SqlConnection sqlConnection = new ConnectionRepository().getConnection(idempresa);
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "pln_EmpleadosModulos";
			SqlParameter p1 = new SqlParameter("@fecha", SqlDbType.Date);
			p1.Value = fecha;
			cmd.Parameters.Add(p1);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			try
			{
				da.Fill(dt);
			}
			catch (Exception)
			{
				if (cmd.Connection.State != 0)
				{
					cmd.Connection.Close();
				}
			}
			return dt;
		}

		public DataTable GetCosCostoProduccion(int periodo, int semana)
		{
			DataTable dt = new DataTable();
			SqlConnection sqlConnection = new CnBD().GetConecctionC();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "CosCostoProduccion";
			SqlParameter p1 = new SqlParameter("@periodo", SqlDbType.Int);
			p1.Value = periodo;
			cmd.Parameters.Add(p1);
			SqlParameter p2 = new SqlParameter("@semana", SqlDbType.Int);
			p2.Value = semana;
			cmd.Parameters.Add(p2);
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
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
			}
			return dt;
		}

		public DataTable GetModulos()
		{
			DataTable dt = new DataTable();
			SqlConnection sqlConnection = new CnBD().GetConecctionC();
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "CosModulosAct";
			cmd.Connection = sqlConnection;
			SqlDataAdapter da = new SqlDataAdapter(cmd);
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
			}
			return dt;
		}
	}
}
