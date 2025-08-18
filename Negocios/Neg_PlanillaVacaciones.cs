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
    public class Neg_PlanillaVacaciones
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        //MODIFICADA POR GRETHEL TERCERO 18012017
        Dato_Planilla Dato_Planilla = new Dato_Planilla();
        Neg_Planilla Neg_Planilla = new Neg_Planilla();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        decimal irVacaciones = 0, egresosVacaciones = 0;
        dsPlanilla.dtDeduccionCuotaDataTable dtDeduccionCuota;// = new dsPlanilla.dtDeduccionCuotaDataTable();
        DataTable demp;//= new DataTable();
		#endregion
		public bool calcularVacaciones(int ubicacion, int tipoPeriodo, int periodoVacaciones, string fechaini, string fechafin, string user, bool todos)
		{
			bool resp = false;
			Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(fechafin);
			DataSet vac = new DataSet();
			dtDeduccionCuota = new dsPlanilla.dtDeduccionCuotaDataTable();
			dsPlanilla.dtEgresosDataTable dtEgresos = Neg_DevYDed.EgresosxPrestacionesSel(periodoVacaciones, 2);
			Neg_IR IR = new Neg_IR();
			Neg_Periodo neg_Periodo = new Neg_Periodo();
			DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
			DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);
			Neg_IR.Globales.dtIRHistorico = IR.ObtenerHistoricoIR(inicioano);
			try
			{
				foreach (DataRow dr in demp.Rows)
				{
					string nombrei = dr["nombre"].ToString();
					string deptoi = dr["depto"].ToString();
					int codigoi = Convert.ToInt32(dr["codigo"].ToString());
					if (todos)
					{
						vac = Neg_Liquidacion.ObtenerDatosLiquidacion(codigoi, 0, 1, 0.0, 0, 0, pago: true);
					}
					else
					{
						double saldovac = Convert.ToDouble(dr["diasvac"].ToString());
						vac = ((!(saldovac > 0.0)) ? Neg_Liquidacion.ObtenerDatosLiquidacion(codigoi, 0, 1, 0.0, 0, 0, pago: true) : Neg_Liquidacion.ObtenerDatosLiquidacion(codigoi, 0, 1, saldovac, 0, 0, pago: true));
					}
					if (vac.Tables.Count > 0 && vac.Tables[0].Rows.Count > 0 && Convert.ToDecimal(vac.Tables[1].Rows[0]["Vacaciones"]) > 0m)
					{
						resp = InsertarPlanillaVacacionesxEmpleado(codigoi, nombrei, deptoi, vac.Tables[1], fechaini, fechafin, periodoVacaciones, tipoPeriodo, user, dtEgresos);
						InsertarHistoricoVacaciones(vac.Tables[1], "", codigoi, periodoVacaciones);
					}
				}
				Neg_Planilla.RegistrarDeduccionCuotaEC(dtDeduccionCuota, periodoVacaciones, 1, fin: true);
				return resp;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool calcularVacaciones(int ubicacion, int codigo, double saldovac, int tipoPeriodo, int periodoVacaciones, string fechaini, string fechafin, string user, int planillavac)
		{
			bool resp = false;
			Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(fechafin);
			DataSet vac = new DataSet();
			dsPlanilla.dtEgresosDataTable dtEgresos = Neg_DevYDed.EgresosxPrestacionesSelxEmpleado(periodoVacaciones, 2, codigo);
			Neg_IR IR = new Neg_IR();
			Neg_Periodo neg_Periodo = new Neg_Periodo();
			DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
			DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);
			Neg_IR.Globales.dtIRHistorico = IR.ObtenerHistoricoIR(inicioano);
			dtDeduccionCuota = new dsPlanilla.dtDeduccionCuotaDataTable();
			try
			{
				demp = obtenerEmpleadoPagoVacaciones(ubicacion, codigo, Neg_Liquidacion.Globales.fechaR, todos: false);
				string nombre = "";
				string depto = "";
				if (demp.Rows.Count > 0)
				{
					nombre = demp.Rows[0]["nombre"].ToString();
					depto = demp.Rows[0]["depto"].ToString();
					vac = Neg_Liquidacion.ObtenerDatosLiquidacion(codigo, 0, 1, saldovac, 0, 0, pago: true);
					if (vac.Tables.Count > 0 && vac.Tables[0].Rows.Count > 0)
					{
						if (!(Convert.ToDecimal(vac.Tables[1].Rows[0]["Vacaciones"]) > 0m))
						{
							throw new Exception("El monto a pagar es menor a cero");
						}
						EliminarHistoricoVacaciones(periodoVacaciones, codigo);
						if (planillavac == 1)
						{
							Neg_Planilla.EliminarPlanillaPorEmpleado(periodoVacaciones, 1, codigo);
							resp = InsertarPlanillaVacacionesxEmpleado(codigo, nombre, depto, vac.Tables[1], fechaini, fechafin, periodoVacaciones, tipoPeriodo, user, dtEgresos);
							Neg_Planilla.RegistrarDeduccionCuotaEC(dtDeduccionCuota, periodoVacaciones, 1, fin: true);
						}
						else
						{
							resp = Neg_DevYDed.AsignarIngresoOdeduccionPorEmpleado(codigo, periodoVacaciones, 1, Convert.ToDecimal(vac.Tables[1].Rows[0]["Vacaciones"]), 1, 6, 1, user);
						}
						InsertarHistoricoVacaciones(vac.Tables[1], "", codigo, periodoVacaciones);
					}
					return resp;
				}
				throw new Exception("El empleado no existe o no tiene activo el pago de vacaciones");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool calcularVacaciones(int ubicacion, int codigo, double saldovac, int tipoPeriodo, string fechaut, string user)
		{
			bool resp = false;
			Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(fechaut);
			DataSet vac = new DataSet();
			dsPlanilla.dtEgresosDataTable dtEgresos = Neg_DevYDed.EgresosxPrestacionesFechaSelxEmpleado(fechaut, 2, codigo);
			Neg_IR IR = new Neg_IR();
			Neg_Periodo neg_Periodo = new Neg_Periodo();
			DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
			DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);
			Neg_IR.Globales.dtIRHistorico = IR.ObtenerHistoricoIR(inicioano);
			dtDeduccionCuota = new dsPlanilla.dtDeduccionCuotaDataTable();
			try
			{
				demp = obtenerEmpleadoPagoVacaciones(ubicacion, codigo, Neg_Liquidacion.Globales.fechaR, todos: false);
				string nombre = "";
				string depto = "";
				if (demp.Rows.Count > 0)
				{
					nombre = demp.Rows[0]["nombre"].ToString();
					depto = demp.Rows[0]["depto"].ToString();
					vac = Neg_Liquidacion.ObtenerDatosLiquidacion(codigo, 0, 1, saldovac, 0, 0, pago: true);
					if (vac.Tables.Count > 0 && vac.Tables[0].Rows.Count > 0)
					{
						if (!(Convert.ToDecimal(vac.Tables[1].Rows[0]["Vacaciones"]) > 0m))
						{
							throw new Exception("El monto a pagar es menor a cero");
						}
						EliminarHistoricoVacacionesFecha(fechaut, codigo);
						resp = InsertarPlanillaVacacionesxEmpleado(codigo, nombre, depto, vac.Tables[1], "", "", 0, tipoPeriodo, user, dtEgresos);
						Neg_Planilla.RegistrarDeduccionCuotaEC(dtDeduccionCuota, fechaut);
						InsertarHistoricoVacaciones(vac.Tables[1], fechaut, codigo, 0);
					}
					return resp;
				}
				throw new Exception("El empleado no existe o no tiene activo el pago de vacaciones");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public bool InsertarPlanillaVacacionesxEmpleado(int codemp, string nombre, string depto, DataTable vac, string fechaini, string fechafin, int periodo, int tperiodo, string user, dsPlanilla.dtEgresosDataTable dtEgresos)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			decimal totalPagarVacaciones = default(decimal);
			decimal inssVacaciones = default(decimal);
			decimal salMensual = default(decimal);
			decimal patronal = default(decimal);
			decimal inatec = default(decimal);
			decimal neto = default(decimal);
			decimal totalegresos = default(decimal);
			int e = 0;
			try
			{
				totalPagarVacaciones = Convert.ToDecimal(vac.Rows[0]["Vacaciones"].ToString());
				inssVacaciones = Convert.ToDecimal(vac.Rows[0]["INSS"].ToString());
				irVacaciones = Neg_Liquidacion.CalcularIRVacaciones(codemp, totalPagarVacaciones, inssVacaciones);
				salMensual = Convert.ToDecimal(vac.Rows[0]["salMensual"].ToString());
				patronal = Convert.ToDecimal(vac.Rows[0]["Patronal_Vacaciones"].ToString());
				inatec = Convert.ToDecimal(vac.Rows[0]["Inatec_Vacaciones"].ToString());
				egresosVacaciones = default(decimal);
				for (; e < dtEgresos.Rows.Count; e++)
				{
					if (codemp == dtEgresos[e].codigo_empleado && dtEgresos[e].id_tipo != 1)
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
						egresosVacaciones += deduccion.procesarDeducciones(deduccion, periodo, 1, totalPagarVacaciones - inssVacaciones - irVacaciones, totalPagarVacaciones, "v", 0, 0, dtDeduccionCuota);
					}
					else if (codemp < dtEgresos[e].codigo_empleado)
					{
						break;
					}
				}
				totalegresos = inssVacaciones + irVacaciones + egresosVacaciones;
				neto = totalPagarVacaciones - totalegresos;
				if (periodo > 0 && !Dato_Planilla.insertarCalculoVacaciones(codemp, depto, fechaini, fechafin, 1, DateTime.Now.Month, nombre, salMensual, totalPagarVacaciones, periodo, tperiodo, DateTime.Now.Year, user, totalegresos, neto, inssVacaciones, irVacaciones, patronal, inatec, userDetail.getIDEmpresa()))
				{
					throw new Exception("Error al insertar registro de pago de vacaciones");
				}
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		private void InsertarHistoricoVacaciones(DataTable dt, string fecaut, int codigo, int periodo)
		{
			try
			{
				IUserDetail userDetail = UserDetailResolver.getUserDetail();
				if (!Dato_Planilla.spInsertarHistoricoVacaciones(codigo, fecaut, Convert.ToDecimal(dt.Rows[0]["SalPromedio"]), Convert.ToDecimal(dt.Rows[0]["salPromedioDia"]), Convert.ToDecimal(dt.Rows[0]["vacacionesDia"]), Convert.ToDecimal(dt.Rows[0]["Vacaciones"]), Convert.ToDecimal(dt.Rows[0]["INSS"]), irVacaciones, egresosVacaciones, periodo, userDetail.getIDEmpresa()))
				{
					throw new Exception("Error al insertar Historico de Vacaciones");
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public DataTable obtenerEmpleadoPagoVacaciones(int ubicacion, int codigo, DateTime fechacorte, bool todos)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			DataTable di = new DataTable();
			return Dato_Planilla.obtenerEmpleadoPagoVacaciones(ubicacion, codigo, fechacorte, todos, userDetail.getIDEmpresa());
		}

		public bool EliminarHistoricoVacaciones(int periodo, int codigo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Planilla.EliminarHistoricoVacaciones(periodo, codigo, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public bool EliminarHistoricoVacacionesFecha(string fecaut, int codigo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Planilla.EliminarHistoricoVacacionesFecha(fecaut, codigo, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public void FiltrarLoteEmpleados(bool todos, DataTable excel, int ubicacion)
		{
			DataTable dt2 = new DataTable();
			dt2.Columns.Add("codigo");
			dt2.Columns.Add("depto");
			dt2.Columns.Add("nombre");
			dt2.Columns.Add("salariomensual");
			dt2.Columns.Add("fecha_ingreso");
			try
			{
				demp = obtenerEmpleadoPagoVacaciones(ubicacion, 0, Neg_Liquidacion.Globales.fechaR, !todos);
				if (demp.Rows.Count > 0)
				{
					if (todos || excel == null)
					{
						return;
					}
					dt2.Columns.Add("diasvac");
					for (int i = 0; i < excel.Rows.Count; i++)
					{
						DataRow[] temporal = demp.Select(string.Format("[codigo]='{0}'", excel.Rows[i]["codigo"].ToString()));
						if (dt2.Select(string.Format("[codigo]='{0}'", excel.Rows[i]["codigo"].ToString())).Count() == 0)
						{
							DataRow[] array = temporal;
							foreach (DataRow item in array)
							{
								dt2.Rows.Add(item["codigo"].ToString(), item["depto"].ToString(), item["nombre"].ToString(), item["salariomensual"].ToString(), item["fecha_ingreso"].ToString(), excel.Rows[i]["diasvac"]);
							}
						}
					}
					demp = dt2;
					return;
				}
				throw new Exception("No hay registros");
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

	}
}
