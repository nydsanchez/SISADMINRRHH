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
    public class Neg_Permisos
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Permisos Dato_Permisos = new Dato_Permisos();
		#endregion
		public bool AgregarPermiso(int tipo, int diaHora, int tipoPermiso, int codEmp, int codigodepto, DateTime fechaIni, DateTime fechaFin, TimeSpan horaIn, TimeSpan horaF, decimal cantidadD, string Observ, int ubicacion)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			decimal cantdias = Convert.ToDecimal(cantidadD);
			decimal canthoras = default(decimal);
			switch (diaHora)
			{
				case 1:
					cantdias = (fechaFin - fechaIni).Days + 1;
					break;
				case 2:
					fechaFin = fechaIni;
					canthoras = decimal.Parse((horaF - horaIn).TotalHours.ToString());
					break;
			}
			if (Dato_Permisos.InsertarPermiso(tipo, diaHora, tipoPermiso, codEmp, codigodepto, fechaIni, fechaFin, horaIn, horaF, cantdias, canthoras, DateTime.Now, Observ, userDetail.getUser(), ubicacion, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public DataSet BuscarEmpleado(int codigoEmp, DateTime fechaIni, DateTime fechaFin, string tipo, string idtipo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			DataSet ds = new DataSet();
			return Dato_Permisos.SeleccionarDatosEmpleado(codigoEmp, fechaIni, fechaFin, tipo, idtipo, userDetail.getIDEmpresa());
		}

		public bool editarPermiso(decimal cantidadEditAnt, decimal cantidadAct, int codEmpleado, DateTime fechaIni, DateTime fechaFin, TimeSpan horaini, TimeSpan horafin)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Permisos.editarPermiso(cantidadEditAnt, cantidadAct, codEmpleado, fechaIni, fechaFin, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public bool ActualizarSaldoVacaciones(int periodo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Permisos.ActualizarSaldoVacaciones(periodo, userDetail.getIDEmpresa()))
			{
				return true;
			}
			return false;
		}

		public int validarEditElimPermiso(int codigo)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			return Dato_Permisos.validarEditElimPermiso(codigo, userDetail.getIDEmpresa());
		}

		public bool eliminarPermisos(decimal dias, decimal horas, int codEmpleado, DateTime fechaIni, DateTime fechaFin, string tipoPermiso)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			if (Dato_Permisos.eliminarPermisos(dias, horas, codEmpleado, fechaIni, fechaFin, userDetail.getIDEmpresa(), tipoPermiso))
			{
				return true;
			}
			return false;
		}

		public string obtenerNombreEmpleado(string codEmpleado)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			return Dato_Permisos.obtenerNombreEmpleado(codEmpleado, userDetail.getIDEmpresa());
		}

		public dsPlanilla.dtPermisosDataTable PermisosSel(DateTime fechaIni, DateTime fechaFin, int userDetail)
		{
			Dato_Permisos dp = new Dato_Permisos();
			return dp.PermisosSel(fechaIni, fechaFin, userDetail);
		}

		public decimal PermisosVacEmpleadosxRangoSel(int codigo, DateTime fechacorte, int aplicacorte)
		{
			Dato_Permisos dp = new Dato_Permisos();
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			return dp.PermisosVacEmpleadosxRangoSel(codigo, fechacorte, aplicacorte, userDetail.getIDEmpresa());
		}

		public decimal obtenerVacacionesPagadasxEmp(int codigo, DateTime fechacorte, int aplicacorte)
		{
			Dato_Permisos dp = new Dato_Permisos();
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			return dp.obtenerVacacionesPagadasxEmp(codigo, fechacorte, aplicacorte, userDetail.getIDEmpresa());
		}

		public decimal SubsidiosEmpleadosxRangoSel(int codigo, DateTime fechacorte, int aplicacorte)
		{
			Dato_Permisos dp = new Dato_Permisos();
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			return dp.SubsidiosEmpleadosxRangoSel(codigo, fechacorte, aplicacorte, userDetail.getIDEmpresa());
		}

		public DataSet PermisosVacEmpleadoDetalleSel(int codigo, DateTime fechacorte, int aplicacorte, int tipoPermiso)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			Dato_Permisos dp = new Dato_Permisos();
			return dp.PermisosVacEmpleadoDetalleSel(codigo, fechacorte, aplicacorte, tipoPermiso, userDetail.getIDEmpresa());
		}

		public DataSet VacacionesPagadasxEmpDetalleSel(int codigo, DateTime fechacorte, int aplicacorte)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			Dato_Permisos dp = new Dato_Permisos();
			return dp.VacacionesPagadasxEmpDetalleSel(codigo, fechacorte, aplicacorte, userDetail.getIDEmpresa());
		}

		public DataTable SubsidiosEmpleadoDetalleSel(int codigo, int aplicaCorte)
		{
			DataTable sub = new DataTable();
			sub.Columns.Add("fechaini");
			sub.Columns.Add("fechafin");
			sub.Columns.Add("diascalendario");
			sub.Columns.Add("diasprestaciones");
			Neg_Liquidacion NLiquidacion = new Neg_Liquidacion();
			DataSet sdet = PermisosVacEmpleadoDetalleSel(codigo, Neg_Liquidacion.Globales.fechaR, aplicaCorte, 5);
			double subprestaciones = 0.0;
			foreach (DataRow item in sdet.Tables[0].Rows)
			{
				subprestaciones = NLiquidacion.CalcularDiasPrestacion(Convert.ToDateTime(item["fechaini"]), Convert.ToDateTime(item["fechafin"]), 0, 0);
				sub.Rows.Add(Convert.ToDateTime(item["fechaini"]).ToShortDateString(), Convert.ToDateTime(item["fechafin"]).ToShortDateString(), item["cantVacaciones"], subprestaciones);
			}
			return sub;
		}
	}
}
