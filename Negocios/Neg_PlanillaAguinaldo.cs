using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Datos;
using System.Web;

namespace Negocios
{
    public class Neg_PlanillaAguinaldo
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Factores Dato_Factores = new Dato_Factores();
        Dato_Planilla Dato_Planilla = new Dato_Planilla();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        dsPlanilla.dtDeduccionCuotaDataTable dtDeduccionCuota;// = new dsPlanilla.dtDeduccionCuotaDataTable();
		#endregion
		public bool calcularAguinaldo(int ubicacion, int tipoPeriodo, int periodoAguinaldo, string fechaini, string fechafin, string user)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			DataTable demp = new DataTable();
			dtDeduccionCuota = new dsPlanilla.dtDeduccionCuotaDataTable();
			try
			{
				Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(fechafin);
				demp = Dato_Planilla.obtenerEmpleadosPagoAguinaldo(ubicacion, Neg_Liquidacion.Globales.fechaR, userDetail.getIDEmpresa());
				Neg_DevYDed NDevyDed = new Neg_DevYDed();
				dsPlanilla.dtEgresosDataTable dtEgresos = NDevyDed.EgresosxPrestacionesSel(periodoAguinaldo, 1);
				foreach (DataRow dr in demp.Rows)
				{
					DataSet Aguinaldo = new DataSet();
					decimal totalPagarAguinaldo = default(decimal);
					decimal egresosAguinaldo = default(decimal);
					decimal salMensual = default(decimal);
					Aguinaldo = Neg_Liquidacion.ObtenerDatosLiquidacion(Convert.ToInt32(dr["codigo"].ToString()), 0, 0, 0.0, 1, 0, pago: true);
					if (Aguinaldo == null)
					{
						totalPagarAguinaldo = default(decimal);
						salMensual = default(decimal);
					}
					else
					{
						totalPagarAguinaldo = Convert.ToDecimal(Aguinaldo.Tables[1].Rows[0]["Aguinaldo"].ToString());
						salMensual = Convert.ToDecimal(Aguinaldo.Tables[1].Rows[0]["salMensual"].ToString());
					}
					if (Convert.ToInt32(dr["codigo"].ToString()) == 869425)
					{
						int aa = 0;
					}
					if (!(totalPagarAguinaldo > 0m))
					{
						continue;
					}
					for (int e = 0; e < dtEgresos.Rows.Count; e++)
					{
						if (Convert.ToInt32(dr["codigo"].ToString()) == dtEgresos[e].codigo_empleado && dtEgresos[e].id_tipo != 1)
						{
							Neg_Planilla deduccion = new Neg_Planilla();
							deduccion.id = dtEgresos[e].id;
							deduccion.id_tipo = dtEgresos[e].id_tipo;
							deduccion.codigo_empleado = dtEgresos[e].codigo_empleado;
							deduccion.tipoingrdeduc = dtEgresos[e].tipoingrdeduc;
							deduccion.valor = dtEgresos[e].valor;
							deduccion.porcentual = dtEgresos[e].porcentual;
							deduccion.modalidad = dtEgresos[e].modalidad;
							deduccion.recurrente = dtEgresos[e].recurrente;
							deduccion.idprioridad = dtEgresos[e].idprioridad;
							deduccion.debe = dtEgresos[e].debe;
							deduccion.pagopendiente = dtEgresos[e].pagopendiente;
							deduccion.ultimacuota = dtEgresos[e].ultimacuota;
							egresosAguinaldo += deduccion.procesarDeducciones(deduccion, periodoAguinaldo, 1, totalPagarAguinaldo, totalPagarAguinaldo, "a", 0, 0, dtDeduccionCuota);
						}
						else if (Convert.ToInt32(dr["codigo"].ToString()) < dtEgresos[e].codigo_empleado)
						{
							break;
						}
					}
					if (!Dato_Planilla.insertarCalculoAguinaldo(Convert.ToInt32(dr["codigo"]), dr["depto"].ToString(), fechaini, fechafin, 1, DateTime.Now.Month, dr["nombre"].ToString(), salMensual, totalPagarAguinaldo, egresosAguinaldo, periodoAguinaldo, tipoPeriodo, DateTime.Now.Year, user, userDetail.getIDEmpresa()))
					{
						throw new Exception("Error al registrar Aguinaldo");
					}
					InsertarHistoricoAguinaldo(Aguinaldo.Tables[1], Convert.ToInt32(dr["codigo"]), fechaini, fechafin, periodoAguinaldo);
					if (Aguinaldo.Tables.Count > 0 && Aguinaldo.Tables[0].Rows.Count > 0)
					{
						InsertarMesesAguinaldo(Aguinaldo.Tables[0], Convert.ToInt32(dr["codigo"]), periodoAguinaldo);
					}
				}
				Neg_Planilla.RegistrarDeduccionCuotaEC(dtDeduccionCuota, periodoAguinaldo, 1, fin: true);
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void InsertarHistoricoAguinaldo(DataTable dt, int codigo, string fechaini, string fechafin, int periodo)
		{
			try
			{
				IUserDetail userDetail = UserDetailResolver.getUserDetail();
				if (!Dato_Planilla.spInsertarHistoricoAguinaldo(codigo, Convert.ToDateTime(fechaini), Convert.ToDateTime(fechafin), Convert.ToDecimal(dt.Rows[0]["SalMayor"]), Convert.ToDecimal(dt.Rows[0]["SalMayorDia"]), Convert.ToDecimal(dt.Rows[0]["SalPromedio"]), Convert.ToDecimal(dt.Rows[0]["salPromedioDia"]), Convert.ToDecimal(dt.Rows[0]["Aguinaldo"]), Convert.ToDecimal(dt.Rows[0]["AguinaldoDia"]), periodo, userDetail.getIDEmpresa()))
				{
					throw new Exception("Error al insertar historico de aguinaldo");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void InsertarMesesAguinaldo(DataTable ds, int codigo, int periodo)
		{
			int anio = 0;
			int mes = 0;
			int diames = 0;
			decimal salario = default(decimal);
			decimal incentivo = default(decimal);
			decimal ingreso = default(decimal);
			decimal promediodias = default(decimal);
			decimal beneficio = default(decimal);
			string mesnombre = "";
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			try
			{
				if (ds.Rows.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < ds.Rows.Count; i++)
				{
					mes = Convert.ToInt32(ds.Rows[i]["MesNumero"].ToString());
					mesnombre = ds.Rows[i]["MesNombre"].ToString();
					anio = Convert.ToInt32(ds.Rows[i]["Anio"].ToString());
					diames = Convert.ToInt32(ds.Rows[i]["diasMes"].ToString());
					salario = Convert.ToDecimal(ds.Rows[i]["Salario"].ToString());
					incentivo = Convert.ToDecimal(ds.Rows[i]["Incentivo"].ToString());
					beneficio = Convert.ToDecimal(ds.Rows[i]["Beneficio"].ToString());
					ingreso = Convert.ToDecimal(ds.Rows[i]["Ingreso"].ToString());
					promediodias = Convert.ToDecimal(ds.Rows[i]["PromedioDias"].ToString());
					if (!Dato_Planilla.spInsertarAguinaldoMeses(codigo, anio, mes, diames, salario, incentivo, beneficio, ingreso, promediodias, Neg_Liquidacion.Globales.fechaR, periodo, mesnombre, userDetail.getIDEmpresa()))
					{
						throw new Exception("Error al insertar meses");
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool EliminarHistoricoAguinaldo(int periodo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Planilla.EliminarHistoricoAguinaldo(periodo, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public bool EliminarAguinaldoMeses(int periodo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Planilla.EliminarAguinaldoMeses(periodo, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}
	}
}
