using System;
using Datos;
using Microsoft.Office.Interop;
using System.Data;

namespace Negocios
{
    public class Neg_INSS
    {
        public DateTime fecha_fin;

        public Neg_INSS()
        {
            fecha_fin = DateTime.Now;
        }

		public dsInss.dtInssDataTable InssCatorcenal_DataTable(DateTime fechainicio, DateTime fechafin)
		{
			Dato_Empleados DEmpleado = new Dato_Empleados();
			Dato_INSS dato_INSS = new Dato_INSS();
			Neg_Permisos NPermisos = new Neg_Permisos();
			string semanas = "";
			dsInss.dtIngresoDataTable dtIngresos = new dsInss.dtIngresoDataTable();
			dsInss.dtIngresoDataTable dtPeriodoAnterior = new dsInss.dtIngresoDataTable();
			dsInss.dtInssDataTable dtInss = new dsInss.dtInssDataTable();
			dsPlanilla.dtEmpleadoDataTable dtEmpleado = DEmpleado.ObtEmpleadosParaInss(fechainicio, fechafin, 3, 1);
			DataTable dtLiquidados = dato_INSS.ObtEmpleadosxRangodeTiempoLiquidaciones(fechainicio, fechafin, 3, 1);
			dsPlanilla.dtPermisosDataTable dtPermisos = NPermisos.PermisosSel(fechainicio, fechafin, 1);
			DateTime fechainiciosemana = fechainicio;
			DateTime fechafinsemana = fechainiciosemana.AddDays(6.0);
			for (int i = 0; i < dtEmpleado.Rows.Count; i++)
			{
				dsInss.dtInssRow dtInssRow = dtInss.NewdtInssRow();
				dtInssRow.codigoempleado = dtEmpleado[i].codigo_empleado;
				dtInssRow.codigonomina = "1";
				dtInssRow.nomina = "KAIZEN S.A.";
				dtInssRow.regimen = "INTEGRAL";
				dtInssRow.novedad = "03";
				dtInssRow.fecha = new DateTime(fechafin.Year, fechafin.Month, 1);
				dtInssRow.fechaegreso = dtEmpleado[i].fechaegreso;
				dtInssRow.fechaingreso = dtEmpleado[i].fechaingreso;
				dtInssRow.inss = dtEmpleado[i].numero_seguro;
				dtInssRow.nombre_depto = dtEmpleado[i].nombre_depto.TrimEnd('\t');
				dtInssRow.salario = dtEmpleado[i].salariomensual;
				dtInssRow.estatus = dtEmpleado[i].idestado.ToString();
				dtInssRow.primernombre = dtEmpleado[i].primer_nombre;
				dtInssRow.segundonombre = dtEmpleado[i].segundo_nombre;
				dtInssRow.primerapellido = dtEmpleado[i].primer_apellido;
				dtInssRow.segundoapellido = dtEmpleado[i].segundo_apellido;
				dtInssRow.ingreso = 0m;
				dtInssRow.semana = 0;
				dtInssRow.sembin = "";
				dtInss.AdddtInssRow(dtInssRow);
			}
			while (fechainiciosemana < fechafin)
			{
				bool semanaproyectada = false;
				bool semanaaproyectar = false;
				int z = 0;
				int t = 0;
				semanas += "1";
				dtIngresos = dato_INSS.ObtenerIngresosxRangodeFecha(fechainiciosemana, fechafinsemana, 1);
				if (semanas == "1" && dtIngresos.Count > 0 && dtIngresos[0].fechaini < fechainiciosemana)
				{
					DateTime fechai2 = fechainiciosemana.AddDays(-21.0);
					DateTime fechaf2 = fechainiciosemana.AddDays(-8.0);
					dtPeriodoAnterior = dato_INSS.ObtenerIngresosxRangodeFecha(fechai2, fechaf2, 1);
					semanaproyectada = true;
				}
				else if (semanas.Length >= 4 && dtIngresos.Count == 0)
				{
					DateTime fechai = fechainiciosemana.AddDays(-7.0);
					DateTime fechaf = fechainiciosemana.AddDays(-1.0);
					dtPeriodoAnterior = dato_INSS.ObtenerIngresosxRangodeFecha(fechai, fechaf, 1);
					semanaaproyectar = true;
				}
				int l = 0;
				int m = 0;
				for (; l < dtInss.Rows.Count; l++)
				{
					if (m < dtIngresos.Rows.Count && dtInss[l].codigoempleado == dtIngresos[m].codigo_empleado)
					{
						if (dtIngresos[m].liquidado && dtInss[l].fechaegreso > dtInss[l].fechaingreso && dtInss[l].fechaegreso <= fechafin)
						{
							dtInss[l].novedad = "02";
							dtInss[l].fecha = dtEmpleado[l].fechaegreso;
						}
						if (semanaproyectada)
						{
							for (; z < dtPeriodoAnterior.Count && dtPeriodoAnterior[z].codigo_empleado < dtInss[l].codigoempleado; z++)
							{
							}
							if (z < dtPeriodoAnterior.Count && dtPeriodoAnterior[z].codigo_empleado == dtInss[l].codigoempleado)
							{
								dtInss[l].ingreso += dtIngresos[m].ingreso - dtPeriodoAnterior[z].ingreso / 2m;
							}
						}
						else if (semanaaproyectar)
						{
							for (; z < dtPeriodoAnterior.Count && dtPeriodoAnterior[z].codigo_empleado < dtInss[l].codigoempleado; z++)
							{
							}
							if (z < dtPeriodoAnterior.Count && dtPeriodoAnterior[z].codigo_empleado == dtInss[l].codigoempleado)
							{
								dtInss[l].ingreso += dtPeriodoAnterior[z].ingreso / 2m;
							}
						}
						else
						{
							dtInss[l].ingreso += dtIngresos[m].ingreso / 2m;
						}
						dtInss[l].semana++;
						dtInss[l].sembin += "1";
						m++;
						continue;
					}
					dtInss[l].sembin += "0";
					if (m < dtIngresos.Rows.Count && dtInss[l].codigoempleado < dtIngresos[m].codigo_empleado)
					{
						m = m;
					}
					else if (m < dtIngresos.Rows.Count)
					{
						l--;
						m++;
					}
					for (; t < dtPermisos.Rows.Count && dtPermisos[t].codigo_empleado <= dtInss[l].codigoempleado; t++)
					{
						if (!(dtPermisos[t].fechaini <= fechafinsemana) || !(dtPermisos[t].fechafin >= fechainiciosemana) || !(dtPermisos[t].cantvacaciones >= 1m))
						{
							continue;
						}
						if (dtInss[l].codigoempleado == dtPermisos[t].codigo_empleado)
						{
							if (dtPermisos[t].tipo == 5)
							{
								t++;
								dtInss[l].novedad = "09";
								break;
							}
						}
						else if (dtPermisos[t].codigo_empleado > dtEmpleado[l].codigo_empleado)
						{
							break;
						}
					}
				}
				fechainiciosemana = fechainiciosemana.AddDays(7.0);
				fechafinsemana = fechainiciosemana.AddDays(6.0);
			}
			for (int j = 0; j < dtLiquidados.Rows.Count; j++)
			{
				dsInss.dtInssRow dtInssRow2 = dtInss.NewdtInssRow();
				dtInssRow2.codigoempleado = (int)dtLiquidados.Rows[j]["codigo_empleado"];
				dtInssRow2.codigonomina = "1";
				dtInssRow2.nomina = "KAIZEN S.A.";
				dtInssRow2.regimen = "INTEGRAL";
				dtInssRow2.novedad = "08";
				dtInssRow2.fecha = new DateTime(fechafin.Year, fechafin.Month, 1);
				dtInssRow2.fechaegreso = (DateTime)dtLiquidados.Rows[j]["fechaegreso"];
				dtInssRow2.fechaingreso = (DateTime)dtLiquidados.Rows[j]["fechaingreso"];
				dtInssRow2.inss = dtLiquidados.Rows[j]["numero_seguro"].ToString();
				dtInssRow2.nombre_depto = dtLiquidados.Rows[j]["nombre_depto"].ToString();
				dtInssRow2.salario = (decimal)dtLiquidados.Rows[j]["salariomensual"];
				dtInssRow2.estatus = dtLiquidados.Rows[j]["idestado"].ToString();
				dtInssRow2.primernombre = dtLiquidados.Rows[j]["primer_nombre"].ToString();
				dtInssRow2.segundonombre = dtLiquidados.Rows[j]["segundo_nombre"].ToString();
				dtInssRow2.primerapellido = dtLiquidados.Rows[j]["primer_apellido"].ToString();
				dtInssRow2.segundoapellido = dtLiquidados.Rows[j]["segundo_apellido"].ToString();
				dtInssRow2.ingreso = (decimal)dtLiquidados.Rows[j]["ingreso"];
				dtInssRow2.semana = 1;
				dtInssRow2.sembin = "10000";
				dtInss.AdddtInssRow(dtInssRow2);
			}
			for (int k = 0; k < dtInss.Rows.Count; k++)
			{
				if (dtInss[k].sembin.Length == 4)
				{
					dtInss[k].sembin += "0";
				}
			}
			return dtInss;
		}

		public string InnsCatorcenal_All(dsInss.dtInssDataTable dt)
		{
			string cabhtml = "";
			string cuerpohtml = "";
			string cabxml = "";
			string cpoxml = "";
			string cabtxt = "";
			string cpotxt = "";
			cabhtml = "<html><head><title></title></head><body>";
			cabhtml += "<table align='center' border='1' cellpadding='1' cellspacing='0' text - align:center;'> ";
			cabhtml += "<tr style='background: SteelBlue; color: White;font-weight: bold;'><td>Codigo Nomina</td><td>Nomina</td><td>Regimen</td><td>NSS</td><td>P.Nombre</td><td>S.Nombre</td>";
			cabhtml += "<td>P.Apellido</td><td>S.Apellido</td><td>Salario Mensual</td><td>Codigo</td><td>Ingresos</td><td>Departamento</td><td>Novedad</td>";
			cabhtml += "<td>Estatus</td><td>Fecha</td><td>Semanas</td><td>Semanas</td><td>Fecha Ingreso</td><td>Fecha Egreso</td></tr>";
			cabxml = "<?xml version='1.0' encoding='iso-8859-1'?>#<inss:documento xmlns:inss = 'http://inss.gob.ni/novedades'>#";
			cabxml = cabxml + "<bloque>#<registro>988766</registro>#<periodo>" + fecha_fin.Year + "-" + fecha_fin.Month.ToString().PadLeft(2, '0') + "</periodo>#</bloque>#<detalle>#";
			for (int i = 0; i < dt.Count; i++)
			{
				cuerpohtml = "";
				cuerpohtml = cuerpohtml + "<tr><td>" + dt[i].codigonomina + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].nomina + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].regimen + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].inss + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].primernombre + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].segundonombre + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].primerapellido + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].segundoapellido + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].salario + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].codigoempleado + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].ingreso + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].nombre_depto + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].novedad + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].estatus + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].fecha.Year + "-" + dt[i].fecha.Month.ToString().PadLeft(2, '0') + "-" + dt[i].fecha.Day.ToString().PadLeft(2, '0') + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].semana + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].sembin + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].fechaingreso.Date.ToString("dd/MM/yyyy") + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].fechaegreso.Date.ToString("dd/MM/yyyy") + "</td></tr>";
				cabhtml += cuerpohtml;
				cpoxml = "";
				cpoxml += "<novedad>#";
				cpoxml = cpoxml + "<nss>" + dt[i].inss + "</nss>#";
				cpoxml = cpoxml + "<p-nombre>'" + dt[i].primernombre + "</p-nombre>#";
				cpoxml = cpoxml + "<p-apellido>'" + dt[i].primerapellido + "</p-apellido>#";
				cpoxml = cpoxml + "<nomina>" + dt[i].codigonomina + "</nomina>#";
				cpoxml = cpoxml + "<tipo-novedad>" + dt[i].novedad + "</tipo-novedad>#";
				cpoxml = cpoxml + "<fecha>" + dt[i].fecha.Year + "-" + dt[i].fecha.Month.ToString().PadLeft(2, '0') + "-" + dt[i].fecha.Day.ToString().PadLeft(2, '0') + "</fecha>#";
				cpoxml = cpoxml + "<sal-devengado>" + dt[i].ingreso + "</sal-devengado>#";
				cpoxml = cpoxml + "<sal-mensual>" + dt[i].salario + "</sal-mensual>#";
				cpoxml += "<aporte>0</aporte>#";
				cpoxml = cpoxml + "<semanas>" + dt[i].sembin + "</semanas>#";
				cpoxml = cpoxml + "<centro-costo>" + dt[i].nombre_depto + "</centro-costo>#";
				cpoxml += "<tipo-empleo/>#";
				cpoxml += "</novedad>#";
				cabxml += cpoxml;
				cpotxt = "";
				cpotxt = cpotxt + dt[i].inss + ";'";
				cpotxt = cpotxt + dt[i].primernombre + ";'";
				cpotxt = cpotxt + dt[i].primerapellido + ";";
				cpotxt = cpotxt + dt[i].codigonomina + ";";
				cpotxt = cpotxt + dt[i].novedad + ";";
				cpotxt = cpotxt + dt[i].fecha.Year + "-" + dt[i].fecha.Month.ToString().PadLeft(2, '0') + "-" + dt[i].fecha.Day.ToString().PadLeft(2, '0') + ";";
				cpotxt = cpotxt + dt[i].ingreso + ";";
				cpotxt = cpotxt + dt[i].salario + ";";
				cpotxt += "0;";
				cpotxt = cpotxt + dt[i].sembin + ";";
				cpotxt = cpotxt + dt[i].nombre_depto + ";";
				cpotxt += ";";
				cpotxt += "#";
				cabtxt += cpotxt;
			}
			return "html" + cabhtml + "</table></body></html>*text" + cabtxt + "*xml" + cabxml;
		}

		public string InnsCatorcenal_XML(dsInss.dtInssDataTable dt)
		{
			string cabxml = "<?xml version='1.0' encoding='iso-8859-1'?>#<inss:documento xmlns:inss = 'http://inss.gob.ni/novedades'>#";
			cabxml = cabxml + "<bloque>#<registro>988766</registro>#<periodo>" + fecha_fin.Year + "-" + fecha_fin.Month.ToString().PadLeft(2, '0') + "</periodo>#</bloque>#<detalle>#";
			string cpoxml = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				cpoxml = "";
				cpoxml += "<novedad>#";
				cpoxml = cpoxml + "<nss>" + dt[i].inss + "</nss>#";
				cpoxml = cpoxml + "<p-nombre>'" + dt[i].primernombre + "</p-nombre>#";
				cpoxml = cpoxml + "<p-apellido>'" + dt[i].primerapellido + "</p-apellido>#";
				cpoxml = cpoxml + "<nomina>" + dt[i].codigonomina + "</nomina>#";
				cpoxml = cpoxml + "<tipo-novedad>" + dt[i].novedad + "</tipo-novedad>#";
				cpoxml = cpoxml + "<fecha>" + dt[i].fecha.Year + "-" + dt[i].fecha.Month.ToString().PadLeft(2, '0') + "-" + dt[i].fecha.Day.ToString().PadLeft(2, '0') + "</fecha>#";
				cpoxml = cpoxml + "<sal-devengado>" + dt[i].ingreso + "</sal-devengado>#";
				cpoxml = cpoxml + "<sal-mensual>" + dt[i].salario + "</sal-mensual>#";
				cpoxml += "<aporte>0</aporte>#";
				cpoxml = cpoxml + "<semanas>" + dt[i].sembin + "</semanas>#";
				cpoxml = cpoxml + "<centro-costo>" + dt[i].nombre_depto + "</centro-costo>#";
				cpoxml += "<tipo-empleo/>#";
				cpoxml += "</novedad>#";
				cabxml += cpoxml;
			}
			return cabxml + "</detalle>#</inss:documento>";
		}

		public string InnsCatorcenal_Text(dsInss.dtInssDataTable dt)
		{
			string rsp = "";
			string reg = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				reg = "";
				reg = reg + dt[i].inss + ";'";
				reg = reg + dt[i].primernombre + ";'";
				reg = reg + dt[i].primerapellido + ";";
				reg = reg + dt[i].codigonomina + ";";
				reg = reg + dt[i].novedad + ";";
				reg = reg + dt[i].fecha.Year + "-" + dt[i].fecha.Month.ToString().PadLeft(2, '0') + "-" + dt[i].fecha.Day.ToString().PadLeft(2, '0') + ";";
				reg = reg + dt[i].ingreso + ";";
				reg = reg + dt[i].salario + ";";
				reg += "0;";
				reg = reg + dt[i].sembin + ";";
				reg = reg + dt[i].nombre_depto + ";";
				reg += ";";
				reg += "#";
				rsp += reg;
			}
			return rsp;
		}

		public string InnsCatorcenal_HTML(dsInss.dtInssDataTable dt)
		{
			string cabhtml = "";
			string cuerpohtml = "";
			cabhtml = "<html><head><title></title></head><body>";
			cabhtml += "<table align='center' border='1' cellpadding='1' cellspacing='0' text - align:center;'> ";
			cabhtml += "<tr style='background: SteelBlue; color: White;font-weight: bold;'><td>Codigo Nomina</td><td>Nomina</td><td>Regimen</td><td>NSS</td><td>P.Nombre</td><td>S.Nombre</td>";
			cabhtml += "<td>P.Apellido</td><td>S.Apellido</td><td>Salario Mensual</td><td>Codigo</td><td>Ingresos</td><td>Departamento</td><td>Novedad</td>";
			cabhtml += "<td>Estatus</td><td>Fecha</td><td>Semanas</td><td>Semanas</td><td>Fecha Ingreso</td><td>Fecha Egreso</td></tr>";
			for (int i = 0; i < dt.Count; i++)
			{
				cuerpohtml = "";
				cuerpohtml = cuerpohtml + "<tr><td>" + dt[i].codigonomina + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].nomina + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].regimen + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].inss + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].primernombre + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].segundonombre + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].primerapellido + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].segundoapellido + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].salario + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].codigoempleado + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].ingreso + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].nombre_depto + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].novedad + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].estatus + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].fecha.Year + "-" + dt[i].fecha.Month.ToString().PadLeft(2, '0') + "-" + dt[i].fecha.Day.ToString().PadLeft(2, '0') + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].semana + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].sembin + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].fechaingreso.Date.ToString("dd/MM/yyyy") + "</td>";
				cuerpohtml = cuerpohtml + "<td>" + dt[i].fechaegreso.Date.ToString("dd/MM/yyyy") + "</td></tr>";
				cabhtml += cuerpohtml;
			}
			return cabhtml + "</table></body></html>";
		}
	}
}
