using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Datos;
using System.Data;
using System.Data.SqlClient;

namespace Negocios
{
    public class Neg_IR
    {
        DataTable dtTablaIR = new DataTable();
        public static class Globales
        {
            public static dsPlanilla.dtIRHistoricoDataTable dtIRHistorico;
        }
        // Dato_IR ir = new Dato_IR();
        public Neg_IR()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_IR datIR = new Datos.Dato_IR();
            dtTablaIR = datIR.TablaIR(userDetail.getIDEmpresa());
        }
        // descontinuada ver version 2025 del IR ObtenerIR2025()
        public decimal ObtenerIR(dsPlanilla.dtIRHistoricoRow hist, decimal IngresoDelPeriodo, bool ocasional, int tipoperiodo)
        {
            decimal IR = default(decimal);
            decimal hingresos = default(decimal);
            decimal hdsegurosocial = default(decimal);
            decimal hdias = default(decimal);
            decimal hingresosvac = default(decimal);
            decimal hdsegurosocialvac = default(decimal);
            decimal hdimpuestos = default(decimal);
            decimal hdimpuestosvac = default(decimal);
            int diasanio = 364;
            if (hist != null)
            {
                hingresos = hist.ingresos;
                hdsegurosocial = hist.dsegurosocial;
                hdias = hist.dias;
                hingresosvac = hist.ingresosvac;
                hdsegurosocialvac = hist.dsegurosocialvac;
                diasanio = 364 - hist.diasmenos;
                hdimpuestos = hist.dimpuestos;
                hdimpuestosvac = hist.dimpuestosvac;
            }
            decimal proyeccion = (hingresos - hdsegurosocial + IngresoDelPeriodo) / (hdias + 7m) * (decimal)diasanio;
            proyeccion += hingresosvac - hdsegurosocialvac;
            for (int i = 0; i < dtTablaIR.Rows.Count; i++)
            {
                decimal desde = (decimal)dtTablaIR.Rows[i]["rentadesde"];
                decimal hasta = (decimal)dtTablaIR.Rows[i]["rentahasta"];
                if (proyeccion >= desde && proyeccion <= hasta)
                {
                    decimal impuestobase = (decimal)dtTablaIR.Rows[i]["ImpuestoBase"];
                    decimal porcentaje = (decimal)dtTablaIR.Rows[i]["PorcentajeAplicable"];
                    decimal sobreexceso = (decimal)dtTablaIR.Rows[i]["SobreExceso"];
                    if (ocasional && tipoperiodo != 1)
                    {
                        IR = IngresoDelPeriodo * (porcentaje / 100m);
                    }
                    else if (tipoperiodo == 1)
                    {
                        decimal impuestodiario = impuestobase + (proyeccion - sobreexceso) * (porcentaje / 100m);
                        impuestodiario /= (decimal)diasanio;
                        IR = impuestodiario * (hdias + 7m) - hdimpuestos - hdimpuestosvac;
                    }
                    break;
                }
            }
            if (IR < 0m)
            {
                IR = default(decimal);
            }
            return IR;
        }


        // IR version 2025
        // TODO: Victor Porras
        public decimal ObtenerIR2025(dsPlanilla.dtIRHistoricoRow hist, decimal IngresoDelPeriodo, bool ocasional, int tipoperiodo, DateTime FecIniPerfiscal, int codigoEmpleado, DateTime FechaCalculo, decimal ingresoporvacaciones)
        {
            decimal IR = default(decimal);
            decimal hingresos = default(decimal);
            decimal hdsegurosocial = default(decimal);
            decimal hdias = default(decimal);
            decimal hingresosvac = default(decimal);
            decimal hdsegurosocialvac = default(decimal);
            decimal hdimpuestos = default(decimal);
            decimal hdimpuestosvac = default(decimal);
            decimal IRacumuladoPagado = default(decimal);

            // Obtener el último día del año
            DateTime FecFinPerfiscal = new DateTime(FecIniPerfiscal.Year, 12, 31);
            int diasanio = 364;

            // Acumulados del Periodo Fiscal Actual
            if (hist != null)
            {
                hingresos = hist.ingresos;
                hdsegurosocial = hist.dsegurosocial;
                hdias = hist.dias;
                hingresosvac = hist.ingresosvac;
                hdsegurosocialvac = hist.dsegurosocialvac;
                diasanio = 364 - hist.diasmenos;
                hdimpuestos = hist.dimpuestos;
                hdimpuestosvac = hist.dimpuestosvac;
            }

     
            IRacumuladoPagado = hdimpuestos;

            DateTime? FechaIngresoLaboral;
            // obtener fecha de ingreso del empleado
            FechaIngresoLaboral = ObtenerFechaIngreso(codigoEmpleado);

            // comprobar si es periodo completo
            bool EsPeriodoCompleto = DeterminarPeriodoCompleto(FechaIngresoLaboral, FecIniPerfiscal);

            // Declarar las variables necesarias
            int semanasRestantes;
            decimal ingresoAnual;
            decimal SalarioAcumulado;
            SalarioAcumulado = (hingresos - hdsegurosocial);
		
            // Calcular semanas restantes redondeando hacia arriba
			semanasRestantes = (int)Math.Ceiling((FecFinPerfiscal - FechaCalculo).TotalDays / 7) + 1;

			// Calcular el ingreso anual
			if (EsPeriodoCompleto) // Suponiendo que EsPeriodoCompleto es un bool+ OtrosIngresos 52_sem
			{
				ingresoAnual = (IngresoDelPeriodo * 52);

				//ingresoAnual = (IngresoDelPeriodo * semanasRestantes) + SalarioAcumulado;
			}
			else
			{
				ingresoAnual = (IngresoDelPeriodo  * semanasRestantes)  + SalarioAcumulado;
			}
			decimal IRVacaciones = 0;
			decimal IRAnual = 0;
			for (int i = 0; i < dtTablaIR.Rows.Count; i++)
			{
				decimal desde = (decimal)dtTablaIR.Rows[i]["rentadesde"];
				decimal hasta = (decimal)dtTablaIR.Rows[i]["rentahasta"];
				if (ingresoAnual >= desde && ingresoAnual <= hasta)
				{
					decimal impuestobase = (decimal)dtTablaIR.Rows[i]["ImpuestoBase"];
					decimal porcentaje = (decimal)dtTablaIR.Rows[i]["PorcentajeAplicable"];
					decimal sobreexceso = (decimal)dtTablaIR.Rows[i]["SobreExceso"];

					// solo planilla semanal y catorcenal -- no se incluye quincenal y mensual
					if (tipoperiodo == 1 || tipoperiodo == 4)
					{
						if  (EsPeriodoCompleto)
                        {
                            decimal Montodiferencial = (ingresoAnual - SalarioAcumulado);
                            IRAnual = impuestobase + ((Montodiferencial + SalarioAcumulado) - sobreexceso) * (porcentaje / 100m);

                            //IRAnual = impuestobase + (ingresoAnual - sobreexceso) * (porcentaje / 100m);
                        }
						else
                        {
							IRAnual = impuestobase + (ingresoAnual - sobreexceso) * (porcentaje / 100m);
						}
								
					}
									
					if (ocasional && tipoperiodo != 1 && ingresoporvacaciones > 0)
					{
						IRVacaciones = ingresoporvacaciones * (porcentaje / 100m);
					}
					if (ingresoporvacaciones > 0)
					{
						IRVacaciones = ingresoporvacaciones * (porcentaje / 100m);
					}
					break;
				}
			}
			if (ocasional && tipoperiodo != 1 && ingresoporvacaciones > 0)
			{
				IR = 0;
			}
			else
            {
				// Calcular IR semanal
				if (semanasRestantes > 0)
				{
					IR = (IRAnual - IRacumuladoPagado) / semanasRestantes;
				}
				else
				{
					IR = IRAnual - IRacumuladoPagado; // Si no hay semanas restantes, no dividir
				}
			}			

			IR += IRVacaciones;

			if (IR < 0m)
			{
				IR = default(decimal);
			}
			return IR;			
		}
		// End IR 2025

		/// <summary>
		/// Obtiene la fecha de ingreso de un empleado por su código.
		/// </summary>
		/// <param name="codigoEmpleado">Código del empleado</param>
		/// <returns>Fecha de ingreso del empleado, o null si no se encuentra</returns>
		public DateTime? ObtenerFechaIngreso(int codigoEmpleado)
		{
			ConnectionRepository conect = new ConnectionRepository();
			SqlConnection sqlConnection = conect.getConnection(1);

			try
			{
				using (SqlConnection connection = new SqlConnection(sqlConnection.ConnectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand("[dbo].[PlnEmpleadoSelFechaIngreso]", connection))
					{
						command.CommandType = CommandType.StoredProcedure;
						command.Parameters.Add(new SqlParameter("@codEmpleado", SqlDbType.Int) { Value = codigoEmpleado });

						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								// Verifica si el valor no es nulo antes de retornarlo
								if (!reader.IsDBNull(0))
								{
									return reader.GetDateTime(0);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Manejo de excepciones (puedes usar logs aquí)
				Console.WriteLine($"Ocurrió un error: {ex.Message}");
			}

			// Retorna null si no se encuentra el registro o si hay un error
			return null;
		}
        
        public bool DeterminarPeriodoCompleto(DateTime? fechaIngreso, DateTime? fechaInicioFiscal)
		{
			// Maneja el caso de valores nulos como desees (aquí se usa DateTime.MinValue)
			DateTime ingreso = fechaIngreso ?? DateTime.MinValue;
			DateTime inicioFiscal = fechaInicioFiscal ?? DateTime.MinValue;

			return ingreso <= inicioFiscal;
		}

		public dsPlanilla.dtIRHistoricoDataTable ObtenerHistoricoIR(DateTime fecha)
		{
			IUserDetail userDetail = UserDetailResolver.getUserDetail();
			Dato_IR datIR = new Dato_IR();
			return datIR.ObtenerHistoricoIR(userDetail.getIDEmpresa(), fecha);
		}

		public dsPlanilla.dtIRHistoricoDataTable ObtenerHistoricoIRxE(DateTime fecha, int codigo)
		{
			dsPlanilla.dtIRHistoricoDataTable lt = ObtenerHistoricoIR(fecha);
			return (dsPlanilla.dtIRHistoricoDataTable)lt.Where((dsPlanilla.dtIRHistoricoRow x) => x.codigo_empleado.Equals(codigo)).CopyToDataTable();
		}
	}
}
