using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Negocios
{
    public class Neg_Periodo
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Periodo Dato_Periodo = new Dato_Periodo();
		#endregion
		public bool AgregarPeriodo(int nperiodo, int ubicacion, int mesSem1, DateTime desdeSem1, DateTime hastaSem1, int mesSem2, DateTime desdeSem2, DateTime hastaSem2, int tPeriodo, string user, int tipoPlanilla, bool consolidar, decimal factor)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Periodo.AgregarPeriodo(nperiodo, ubicacion, mesSem1, desdeSem1, hastaSem1, mesSem2, desdeSem2, hastaSem2, tPeriodo, user, tipoPlanilla, consolidar, factor, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public bool CerrarPeriodo(int nperiodo, string user, int tipoPlanilla)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Periodo.CerrarPeriodo(nperiodo, user, userDetail.getIDEmpresa(), tipoPlanilla))
			{
				return true;
			}
			return false;
		}

		public string cargarProxPeriodoCatorc()
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			return Dato_Periodo.cargarProxPeriodoCatorc(userDetail.getIDEmpresa());
		}

		public bool AgregarPeriodoXFecha(int nperiodo, int ubicacion, int mes, DateTime fechaIni, DateTime fechaF, int tPeriodo, string user)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Periodo.AgregarPeriodoXFecha(nperiodo, ubicacion, mes, fechaIni, fechaF, tPeriodo, user, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public bool AgregarPeriodoQuincenal(int nperiodo, int ubicacion, int mesSem1, DateTime desdeSem1, string hastaSem1, int mesSem2, string desdeSem2, DateTime hastaSem2, int tPeriodo, string user, int tipoPlanilla)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Periodo.AgregarPeriodoQuincenal(nperiodo, ubicacion, mesSem1, desdeSem1, hastaSem1, mesSem2, desdeSem2, hastaSem2, tPeriodo, user, tipoPlanilla, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public bool AgregarPeriodoVacaciones(int nperiodo, int ubicacion, int mesSemana, DateTime desde, DateTime hasta, int tperiodo, string user, int tipoPlanilla, decimal factor)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Periodo.AgregarPeriodoVacaciones(nperiodo, ubicacion, mesSemana, desde, hasta, tperiodo, user, tipoPlanilla, factor, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public bool AgregarPeriodoAguinaldo(int nperiodo, int ubicacion, int mesSemana, DateTime desde, DateTime hasta, int tperiodo, string user, int tipoPlanilla, decimal factor)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Periodo.AgregarPeriodoAguinaldo(nperiodo, ubicacion, mesSemana, desde, hasta, tperiodo, user, tipoPlanilla, factor, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public dsPlanilla.dtPeriodoDataTable PeriodoSel(int periodo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			Dato_Periodo x = new Dato_Periodo();
			return x.Sel(periodo, userDetail.getIDEmpresa());
		}

		public dsPlanilla.dtPeriodoDataTable cargarUltPeriodoAbieCat(int tperiodo, int tplanilla, int ubicacion)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			Dato_Periodo x = new Dato_Periodo();
			return x.SeleccionarPeriodoCat(tperiodo, tplanilla, ubicacion, userDetail.getIDEmpresa());
		}

		public dsPlanilla.dtPeriodoDataTable SeleccionarPeriodoCerrado(int tperiodo, int tplanilla, int ubicacion)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			Dato_Periodo x = new Dato_Periodo();
			return x.SeleccionarPeriodoCerrado(tperiodo, tplanilla, ubicacion, userDetail.getIDEmpresa());
		}

		public DataTable PlnPeriodoFiscalSel()
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			Dato_Periodo x = new Dato_Periodo();
			return x.PlnPeriodoFiscalSel(userDetail.getIDEmpresa());
		}

		public DataTable GetPeriodoActual()
		{
			Dato_Periodo Dato_Periodo = new Dato_Periodo();
			DataTable periodo = Dato_Periodo.SeleccionarPeriodoCat(1, 4, 3, 1);
			DataTable periodocerrado = Dato_Periodo.SeleccionarPeriodoCerrado(1, 4, 3, 1);
			int nperiodo = 0;
			DateTime pini = default(DateTime);
			DateTime pfin = default(DateTime);
			DateTime fini = default(DateTime);
			DateTime ffin = default(DateTime);
			DataTable result = new DataTable();
			result.Columns.Add("fechaini",typeof(DateTime));
			result.Columns.Add("fechafin",typeof(DateTime));
			result.Columns.Add("periodo");
			DateTime fechaactual = DateTime.Now;
			if (periodo.Rows.Count == 0 && periodocerrado.Rows.Count > 0)
			{
				pini = Convert.ToDateTime(periodocerrado.Rows[0]["fechafin2"]).AddDays(1.0);
				pfin = Convert.ToDateTime(periodocerrado.Rows[0]["fechafin2"]).AddDays(14.0);
				nperiodo = Convert.ToInt32(periodocerrado.Rows[0]["nperiodo"]);
			}
			else if (periodo.Rows.Count > 0)
			{
				pini = Convert.ToDateTime(periodo.Rows[0]["fechaini"]);
				pfin = Convert.ToDateTime(periodo.Rows[0]["fechafin2"]);
				nperiodo = Convert.ToInt32(periodo.Rows[0]["nperiodo"]);
			}
			if (fechaactual < pini)
			{
				fini = pini.AddDays(-14.0);
				ffin = pini.AddDays(-1.0);
			}
			else if (fechaactual >= pini && fechaactual <= pfin)
			{
				fini = pini;
				ffin = pfin;
			}
			else if (fechaactual > pfin)
			{
				fini = pfin.AddDays(1.0);
				ffin = pfin.AddDays(14.0);
				nperiodo++;
			}
			result.Rows.Add(fini, ffin, nperiodo);
			return result;
		}
	}
}
