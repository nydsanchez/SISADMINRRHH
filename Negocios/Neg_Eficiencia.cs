using System;
using Datos;
using System.Data;
using System.Collections.Generic;
using System.Globalization;

namespace Negocios
{
    public class Neg_Eficiencia
    {       
        public System.Data.DataTable MinutosProducidos(DateTime fechaini, DateTime fechafin, int param, int idempresa)
        {
            //Tabla de respuesta.
            System.Data.DataTable dtMinProducidos = new System.Data.DataTable();            

            if (fechaini > fechafin)
                return dtMinProducidos;

            System.Data.DataTable dtMinutosProducidos = new Dato_Eficiencia().GetCosMinutosProducidos(fechaini, fechafin);
            System.Data.DataTable dtModulo = new Dato_Eficiencia().GetModulos();

            //Se agrega la primer columna.
            dtMinProducidos.Columns.Add("Modulo", typeof(int));
            dtMinProducidos.Columns.Add("Supervisor", typeof(string));

            //if (dtModulo.Rows.Count > 7)
             //   return dtMinProducidos;

            //Se crean todas las columnas de fecha.
            while (fechaini <= fechafin)
            {
                string columname = "D" + fechaini.Day + "-" + fechaini.Month + "-" + fechaini.Year;
                System.Data.DataColumn col = new DataColumn(columname, typeof(double));
                col.DefaultValue = 0;
                dtMinProducidos.Columns.Add(col);
                fechaini = fechaini.AddDays(1);
            }

            //Se agregan todos los modulos que tuvieron produccion
            for (int m = 0; m < dtModulo.Rows.Count; m++)
            {
                DataRow dr = dtMinProducidos.NewRow();
                dr["Modulo"] = int.Parse(dtModulo.Rows[m]["modulo"].ToString());
                dr["Supervisor"] = dtModulo.Rows[m]["supervisor"].ToString();
                dtMinProducidos.Rows.Add(dr);
            }

            //Se dejan en firme todos los cambios sobre el datatable.
            dtMinProducidos.AcceptChanges();

            Neg_Marca neg_Marca = new Neg_Marca();            
            int i = 0;

            for (int k = 0; k < dtMinProducidos.Rows.Count;)
            {
                int modulo = (int)dtMinProducidos.Rows[k]["Modulo"];

                while (i < dtMinutosProducidos.Rows.Count)
                {                    
                    int modulop = int.Parse(dtMinutosProducidos.Rows[i]["modulo"].ToString());
                    DateTime fechap = (DateTime)dtMinutosProducidos.Rows[i]["fecha"];

                    if (modulo > modulop)
                    {
                        i++;
                        continue;
                    }

                    //No hay mas produccion para ese modulo en ese rango de fecha
                    if (modulo < modulop)
                    {
                        k++;
                        break;
                    }

                    string columname = "D" + fechap.Day + "-" + fechap.Month + "-" + fechap.Year;
                    double min = double.Parse(dtMinutosProducidos.Rows[i]["minprod"].ToString());
                    dtMinProducidos.Rows[k][columname] = min;
                    
                    i++;
                    if (i == dtMinutosProducidos.Rows.Count)
                    {
                        k = dtMinProducidos.Rows.Count;
                    }
                }
            }

            return dtMinProducidos;
        }

        public System.Data.DataTable MinutosTrabajados(DateTime fechaini, DateTime fechafin, int param, int idempresa)
        {
            //Tabla de respuesta.
            System.Data.DataTable dtMinTrabajados = new System.Data.DataTable();           

            if (fechaini > fechafin)
                return dtMinTrabajados;

            System.Data.DataTable dtMinutosTrabajados = new Dato_Eficiencia().GetHorasTrabajadas(fechaini, fechafin, 1, idempresa);            
            System.Data.DataTable dtModulo = new Dato_Eficiencia().GetModulos();

            //Se agrega la primer columna.
            dtMinTrabajados.Columns.Add("Modulo", typeof(int));
            dtMinTrabajados.Columns.Add("Supervisor", typeof(string));

            //if (dtModulo.Rows.Count > 7)
              //  return dtMinTrabajados;

            //Se crean todas las columnas de fecha.
            while (fechaini <= fechafin)
            {
                string columname = "D" + fechaini.Day + "-" + fechaini.Month + "-" + fechaini.Year;
                System.Data.DataColumn col = new DataColumn(columname, typeof(double));
                col.DefaultValue = 0;
                dtMinTrabajados.Columns.Add(col);
                fechaini = fechaini.AddDays(1);
            }

            //Se agregan todos los modulos que tuvieron produccion
            for (int m = 0; m < dtModulo.Rows.Count; m++)
            {
                DataRow dr = dtMinTrabajados.NewRow();
                dr["Modulo"] = int.Parse(dtModulo.Rows[m]["modulo"].ToString());
                dr["Supervisor"] = dtModulo.Rows[m]["supervisor"].ToString();
                dtMinTrabajados.Rows.Add(dr);
            }

            //Se dejan en firme todos los cambios sobre el datatable.
            dtMinTrabajados.AcceptChanges();

            Neg_Marca neg_Marca = new Neg_Marca();
            int j = 0;
            double porcentaje_opera = 0.0;
            for (int k = 0; k < dtMinTrabajados.Rows.Count;)
            {
                int modulo = (int)dtMinTrabajados.Rows[k]["Modulo"];

                double horast = 0;
                while (j < dtMinutosTrabajados.Rows.Count)
                {
                    int modulot = int.Parse(dtMinutosTrabajados.Rows[j]["modulo"].ToString());
                    DateTime fechat = (DateTime)dtMinutosTrabajados.Rows[j]["fecha"];
                    porcentaje_opera = double.Parse(dtMinutosTrabajados.Rows[j]["porcentaje_opera"].ToString());

                    if (modulo == modulot)
                    {
                        TimeSpan horaE = TimeSpan.Parse(dtMinutosTrabajados.Rows[j]["horaE"].ToString() + ":00");
                        TimeSpan horaS = TimeSpan.Parse(dtMinutosTrabajados.Rows[j]["horaS"].ToString() + ":00");
                        TimeSpan horaturnoini = (TimeSpan)dtMinutosTrabajados.Rows[j]["horaini"];
                        TimeSpan horaturnofin = (TimeSpan)dtMinutosTrabajados.Rows[j]["horafin"];
                        TimeSpan horaecomida = (TimeSpan)dtMinutosTrabajados.Rows[j]["horaecomida"];
                        TimeSpan horascomida = (TimeSpan)dtMinutosTrabajados.Rows[j]["horascomida"];

                        //Corto la hora de entrada.
                        if (horaE < horaturnoini && horaE != TimeSpan.Zero && (horaturnoini != horaturnofin))
                        {
                            horaE = horaturnoini;
                        }

                        horast += neg_Marca.RestarHoraDeAlmuerzo(horaE, horaS, horaecomida, horascomida) * porcentaje_opera;
                        j++;

                        if (j < dtMinutosTrabajados.Rows.Count)
                        {
                            int modulotmp = int.Parse(dtMinutosTrabajados.Rows[j]["modulo"].ToString());
                            DateTime fechatmp = (DateTime)dtMinutosTrabajados.Rows[j]["fecha"];

                            if ((modulot == modulotmp && fechat == fechatmp))
                            {
                                continue;
                            }
                        }                       

                        string columname = "D" + fechat.Day + "-" + fechat.Month + "-" + fechat.Year;                         
                        dtMinTrabajados.Rows[k][columname] = horast * 60;
                        horast = 0;

                        if (j == dtMinutosTrabajados.Rows.Count)
                        {
                            k = dtMinTrabajados.Rows.Count;
                        }
                        break;
                    }
                    else
                    {
                        if (modulo > modulot)
                        {
                            j++;
                            continue;
                        }

                        //No hay mas produccion para ese modulo en ese rango de fecha
                        if (modulo < modulot)
                        {
                            k++;
                            break;
                        }

                        j++;
                        if (j == dtMinutosTrabajados.Rows.Count)
                        {
                            k = dtMinTrabajados.Rows.Count;
                        }
                    }
                }
            }

            return dtMinTrabajados;
        }


		public string EficienciaPorModuloHTML(DateTime fechaini, DateTime fechafin, int idempresa)
		{
			DataSet dsEficiencia = Eficiencia(fechaini, fechafin, idempresa);
			DataTable dtHeadCount = HeadCountModulo(fechaini, fechafin, idempresa);
			string tabla = "";
			for (int j = 0; j < dsEficiencia.Tables["EM"].Rows.Count; j++)
			{
				if (int.Parse(dsEficiencia.Tables["EMSemana"].Rows[j][0].ToString()) == 0)
				{
					continue;
				}
				tabla += "<tr>";
				tabla = tabla + "<td style='background: SteelBlue; color: White;font-weight: bold;'>" + dsEficiencia.Tables["EM"].Rows[j][0].ToString() + "</td>";
				tabla = tabla + "<td>" + dsEficiencia.Tables["EM"].Rows[j][1].ToString() + "</td>";
				int k = 2;
				int v = 0;
				while (k < dsEficiencia.Tables["EM"].Columns.Count)
				{
					if (v <= 15)
					{
						tabla = tabla + "<td bgcolor='#CFD5D7'>" + dtHeadCount.Rows[j][v + 1].ToString() + "</td>";
						double headcount = double.Parse(dtHeadCount.Rows[j][v + 3].ToString());
						double layout = double.Parse(dtHeadCount.Rows[j][v + 2].ToString());
						tabla = ((!(headcount > layout)) ? ((!(headcount < layout)) ? (tabla + "<td bgcolor='#64FE2E'>" + dtHeadCount.Rows[j][v + 3].ToString() + "</td>") : (tabla + "<td bgcolor='#F4FA58'>" + dtHeadCount.Rows[j][v + 3].ToString() + "</td>")) : (tabla + "<td bgcolor='#ff0d06'>" + dtHeadCount.Rows[j][v + 3].ToString() + "</td>"));
					}
					tabla = tabla + "<td>" + dsEficiencia.Tables["EM"].Rows[j][k].ToString() + "</td>";
					k++;
					v += 3;
				}
				int semana = int.Parse(dsEficiencia.Tables["EMSemana"].Rows[j][0].ToString());
				tabla = ((semana >= 90) ? (tabla + "<td bgcolor='#64FE2E'>" + dsEficiencia.Tables["EMSemana"].Rows[j][0].ToString() + "</td>") : ((semana >= 85) ? (tabla + "<td bgcolor='#2b80f2'>" + dsEficiencia.Tables["EMSemana"].Rows[j][0].ToString() + "</td>") : ((semana < 80) ? (tabla + "<td bgcolor='#FF0000'>" + dsEficiencia.Tables["EMSemana"].Rows[j][0].ToString() + "</td>") : (tabla + "<td bgcolor='#FF8000'>" + dsEficiencia.Tables["EMSemana"].Rows[j][0].ToString() + "</td>"))));
				tabla += "</tr>";
			}
			tabla += "<tr><td>Total</td><td></td>";
			for (int i = 2; i < dsEficiencia.Tables["EMDias"].Rows.Count; i++)
			{
				tabla = tabla + "<td></td><td></td><td>" + dsEficiencia.Tables["EMDias"].Rows[i][0].ToString() + "</td>";
			}
			string encabezado = "<CENTER><table border='1' style='border - collapse: collapse;'><tr align='center' style='background: SteelBlue; color: White;font-weight: bold;'>";
			encabezado = encabezado + "<td colspan='22'>Reporte de Eficiencia Del  " + fechaini.ToString("dd-MM-yyyy") + "  Al  " + fechafin.AddDays(1.0).ToString("dd-MM-yyyy") + "</td></tr>";
			encabezado += "<tr style='background: SteelBlue; color: White;font-weight: bold;'><td>Modulo</td><td>Supervisor</td><td colspan='3'>Lunes</td><td colspan='3'>Martes</td><td colspan='3'>Miercoles</td><td colspan='3'>Jueves</td><td colspan='3'>Viernes</td><td colspan='3'>Sabado</td><td>Total</td></tr>";
			encabezado += "<tr style='background: SteelBlue; color: White;font-weight: bold;'><td></td><td></td><td>Estilo</td><td>HC</td><td>Efic.</td><td>Estilo</td><td>HC</td><td>Efic.</td><td>Estilo</td><td>HC</td><td>Efic.</td><td>Estilo</td><td>HC</td><td>Efic.</td><td>Estilo</td><td>HC</td><td>Efic.</td><td>Estilo</td><td>HC</td><td>Efic.</td><td>Total</td></tr>";
			tabla = encabezado + tabla;
			tabla = tabla + "<td>" + dsEficiencia.Tables["EPlanta"].Rows[0][0].ToString() + "</td>";
			tabla += "</tr>";
			return tabla + "</table>";
		}

		public DataSet Eficiencia(DateTime fechaini, DateTime fechafin, int idempresa)
		{
			DataTable dtMinProducidos = MinutosProducidos(fechaini, fechafin, 1, idempresa);
			DataTable dtMinTrabajados = MinutosTrabajados(fechaini, fechafin, 1, idempresa);
			DataTable dtEM = new DataTable();
			dtEM.TableName = "EM";
			DataTable dtEMSemana = new DataTable();
			dtEMSemana.TableName = "EMSemana";
			DataTable dtEMDias = new DataTable();
			dtEMDias.TableName = "EMDias";
			DataTable dtEPlanta = new DataTable();
			dtEPlanta.TableName = "EPlanta";
			dtEMDias.Columns.Add("TotalDias");
			dtEMSemana.Columns.Add("TotalSemana");
			dtEMSemana.Columns.Add("MinutosProducidos", typeof(decimal));
			dtEMSemana.Columns.Add("MinutosTrabajados", typeof(decimal));
			dtEPlanta.Columns.Add("TotalPlanta");
			dtEPlanta.Columns.Add("MinutosProducidos");
			dtEPlanta.Columns.Add("MinutosTrabajados");
			List<decimal> TotaldiasMp = new List<decimal>();
			List<decimal> TotaldiasMt = new List<decimal>();
			for (int i = 0; i < dtMinProducidos.Columns.Count; i++)
			{
				dtEM.Columns.Add(dtMinProducidos.Columns[i].ColumnName);
				TotaldiasMp.Add(0m);
				TotaldiasMt.Add(0m);
			}
			decimal minprodtotal = 0;
			decimal mintrabtotal = 0;
			for (int j = 0; j < dtMinProducidos.Rows.Count; j++)
			{
				decimal minprodsem = 0;
				decimal mintrabsem = 0;
				DataRow dr = dtEM.NewRow();
				DataRow dataRow = dr;
				dataRow[0] = dataRow[0]?.ToString() + dtMinProducidos.Rows[j][0].ToString();
				dataRow = dr;
				dataRow[1] = dataRow[1]?.ToString() + dtMinProducidos.Rows[j][1].ToString();
				for (int l = 2; l < dtMinProducidos.Columns.Count; l++)
				{
					decimal minprod = decimal.Parse(dtMinProducidos.Rows[j][l].ToString());
					decimal mintrab = decimal.Parse(dtMinTrabajados.Rows[j][l].ToString());
					if (minprod <= 0m)
					{
						minprod = 0;
					}
					if (mintrab <= 0m)
					{
						mintrab = 0;
					}
					minprodsem += minprod;
					mintrabsem += mintrab;
					TotaldiasMp[l] += minprod;
					TotaldiasMt[l] += mintrab;
					if (mintrab > 0m)
					{
						dr[l] = Math.Round(minprod * 100m / mintrab);
					}
					else
					{
						dr[l] = 0;
					}
				}
				dtEM.Rows.Add(dr);
				DataRow drSemana = dtEMSemana.NewRow();
				drSemana["MinutosProducidos"] = minprodsem;
				drSemana["MinutosTrabajados"] = mintrabsem;
				if (mintrabsem > 0m)
				{
					drSemana["TotalSemana"] = Math.Round(minprodsem * 100m / mintrabsem);
				}
				else
				{
					drSemana["TotalSemana"] = 0;
				}
				dtEMSemana.Rows.Add(drSemana);
			}
			for (int k = 0; k < dtMinProducidos.Columns.Count; k++)
			{
				DataRow drDias = dtEMDias.NewRow();
				minprodtotal += TotaldiasMp[k];
				mintrabtotal += TotaldiasMt[k];
				if (TotaldiasMt[k] > 0m)
				{
					drDias[0] = Math.Round(TotaldiasMp[k] * 100m / TotaldiasMt[k]);
				}
				else
				{
					drDias[0] = 0;
				}
				dtEMDias.Rows.Add(drDias);
			}
			DataRow drPlanta = dtEPlanta.NewRow();
			if (mintrabtotal > 0m)
			{
				drPlanta[0] = Math.Round(minprodtotal * 100m / mintrabtotal);
			}
			else
			{
				drPlanta[0] = 0;
			}
			dtEPlanta.Rows.Add(drPlanta);
			DataSet ds = new DataSet();
			ds.Tables.Add(dtEM);
			ds.Tables.Add(dtEMSemana);
			ds.Tables.Add(dtEMDias);
			ds.Tables.Add(dtEPlanta);
			return ds;
		}

		public DataTable EficienciaxAño(int idempresa)
		{
			DateTime fechaInicioAno = DateTime.Now.AddDays(1 - DateTime.Now.DayOfYear);
			if (fechaInicioAno.DayOfWeek != DayOfWeek.Monday)
			{
				fechaInicioAno = fechaInicioAno.AddDays((double)(1 - fechaInicioAno.DayOfWeek + 7));
			}
			DateTime inicioSemana = fechaInicioAno;
			DateTime finSemana = fechaInicioAno.AddDays(5.0);
			DataTable dtEficienciaHistorica = new Dato_Eficiencia().GetModulos();
			dtEficienciaHistorica.Rows.Add(dtEficienciaHistorica.NewRow());
			dtEficienciaHistorica.Columns.Add("MinutosTrabajados", typeof(decimal));
			dtEficienciaHistorica.Columns.Add("MinutosProducidos", typeof(decimal));
			int i = 1;
			while (finSemana < DateTime.Now)
			{
				DataSet ds = Eficiencia(inicioSemana, finSemana, idempresa);
				string columnName = "Sem" + i;
				dtEficienciaHistorica.Columns.Add(columnName, typeof(decimal));
				decimal totsemana = 0;
				for (int j = 0; j < ds.Tables["EMSemana"].Rows.Count; j++)
				{
					if (i == 1)
					{
						dtEficienciaHistorica.Rows[j]["MinutosTrabajados"] = 0;
						dtEficienciaHistorica.Rows[j]["MinutosProducidos"] = 0;
					}
					totsemana = decimal.Parse(ds.Tables["EMSemana"].Rows[j]["TotalSemana"].ToString());
					dtEficienciaHistorica.Rows[j]["MinutosTrabajados"] = (decimal)dtEficienciaHistorica.Rows[j]["MinutosTrabajados"] + (decimal)ds.Tables["EMSemana"].Rows[j]["MinutosTrabajados"];
					dtEficienciaHistorica.Rows[j]["MinutosProducidos"] = (decimal)dtEficienciaHistorica.Rows[j]["MinutosProducidos"] + (decimal)ds.Tables["EMSemana"].Rows[j]["MinutosProducidos"];
					dtEficienciaHistorica.Rows[j][columnName] = ds.Tables["EMSemana"].Rows[j]["TotalSemana"];
				}
				if (ds.Tables["EPlanta"].Rows.Count > 0)
				{
					dtEficienciaHistorica.Rows[dtEficienciaHistorica.Rows.Count - 1][columnName] = ds.Tables["EPlanta"].Rows[0][0];
				}
				inicioSemana = inicioSemana.AddDays(7.0);
				finSemana = finSemana.AddDays(7.0);
				i++;
			}
			return dtEficienciaHistorica;
		}

		public string EficienciaxAñoHTML(int idempresa)
		{
			DataTable dtEficiencia = EficienciaxAño(idempresa);
			string tabla = "";
			string encabezado = "<CENTER><table border='1' style='border - collapse: collapse;'><tr align='center' style='background: SteelBlue; color: White;font-weight: bold;'>";
			encabezado = encabezado + "<td colspan='" + dtEficiencia.Columns.Count + "'> Eficiencia Anual" + DateTime.Now.Year + "</td>";
			encabezado += "<tr style='background: SteelBlue; color: White;font-weight: bold;'><td>Modulo</td><td>Supervisor</td>";
			for (int j = 1; j < dtEficiencia.Columns.Count - 3; j++)
			{
				encabezado = encabezado + "<td>" + j + "</td>";
			}
			encabezado += "<td>Eficiencia Anual</td></tr>";
			decimal totalMinutosTrabajados = 0;
			decimal totalMinutosProducidos = 0;
			for (int i = 0; i < dtEficiencia.Rows.Count; i++)
			{
				decimal t = 0;
				decimal minutosTrabajados = 0;
				decimal minutosProducidos = 0;
				decimal.TryParse(dtEficiencia.Rows[i]["MinutosTrabajados"].ToString(), out minutosTrabajados);
				decimal.TryParse(dtEficiencia.Rows[i]["MinutosProducidos"].ToString(), out minutosProducidos);
				totalMinutosTrabajados += minutosTrabajados;
				totalMinutosProducidos += minutosProducidos;
				if (minutosTrabajados > 0m)
				{
					t = Math.Round(minutosProducidos / minutosTrabajados * 100m, 0);
				}
				if (i >= dtEficiencia.Rows.Count - 1 || !(t < 0.0001m))
				{
					tabla += "<tr>";
					tabla = tabla + "<td style='background: SteelBlue; color: White;font-weight: bold;'>" + dtEficiencia.Rows[i][0].ToString() + "</td>";
					tabla = tabla + "<td>" + dtEficiencia.Rows[i][1].ToString() + "</td>";
					for (int k = 4; k < dtEficiencia.Columns.Count; k++)
					{
						int semana = int.Parse(dtEficiencia.Rows[i][k].ToString());
						tabla += DarColorACelda(semana);
					}
					tabla = ((i >= dtEficiencia.Rows.Count - 1) ? (tabla + DarColorACelda((int)Math.Round(totalMinutosProducidos / totalMinutosTrabajados * 100m, 0))) : (tabla + DarColorACelda(Convert.ToInt32(t))));
					tabla += "</tr>";
				}
			}
			tabla = encabezado + tabla;
			return tabla + "</table>";
		}

		public string DarColorACelda(int valor)
		{
			if (valor >= 90)
			{
				return "<td bgcolor='#64FE2E'>" + valor + "</td>";
			}
			if (valor >= 85)
			{
				return "<td bgcolor='#2b80f2'>" + valor + "</td>";
			}
			return "<td bgcolor='#FF0000'>" + valor + "</td>";
		}

		public DataTable ContribucionxAño(int idempresa)
		{
			DateTime fechaInicioAno = DateTime.Now.AddDays(1 - DateTime.Now.DayOfYear);
			if (fechaInicioAno.DayOfWeek != DayOfWeek.Monday)
			{
				fechaInicioAno = fechaInicioAno.AddDays((double)(1 - fechaInicioAno.DayOfWeek + 7));
			}
			DateTime inicioSemana = fechaInicioAno;
			DateTime finSemana = fechaInicioAno.AddDays(5.0);
			DataTable dtContribucionHistorica = new Dato_Eficiencia().GetModulos();
			int i = 1;
			while (finSemana < DateTime.Now)
			{
				DataTable dtparametros = new Dato_Eficiencia().plnPeriodoCostoSel(inicioSemana, idempresa);
				string columnName = "Sem" + i;
				dtContribucionHistorica.Columns.Add(columnName, typeof(decimal));
				if (dtparametros.Rows.Count > 0)
				{
					int periodo = (int)dtparametros.Rows[0]["periodo"];
					int semana = (int)dtparametros.Rows[0]["semana"];
					DataTable dt = RptContribucionXModulo(periodo, semana, idempresa);
					for (int j = 0; j < dtContribucionHistorica.Rows.Count; j++)
					{
						dtContribucionHistorica.Rows[j][columnName] = dt.Rows[j]["Contribucion"];
					}
				}
				inicioSemana = inicioSemana.AddDays(7.0);
				finSemana = finSemana.AddDays(7.0);
				i++;
			}
			return dtContribucionHistorica;
		}

		public DataTable RptContribucionXModulo(int periodo, int semana, int idempresa)
		{
			dsPlanilla.dtPeriodoDataTable dtPeriodo = new Neg_Periodo().PeriodoSel(periodo);
			DateTime fechaini;
			if (semana == 1)
			{
				fechaini = dtPeriodo[0].fechaini;
				DateTime fechafin = dtPeriodo[0].fechafin;
			}
			else
			{
				fechaini = dtPeriodo[0].fechaini2;
				DateTime fechafin = dtPeriodo[0].fechafin2;
			}
			DataSet dsEficiencia = Eficiencia(fechaini, fechaini.AddDays(5.0), idempresa);
			DataTable dtCostoProduccion = new Dato_Eficiencia().GetCosCostoProduccion(periodo, semana);
			DataTable dtCostoManodeObra = new Dato_Eficiencia().GetCostoManodeObra(periodo, semana, idempresa);
			DataTable dt = CrearDtContribucion();
			int j = 0;
			int k = 0;
			int eficienciadeplanta = 0;
			int cumplimiento = 0;
			decimal facturado = 0;
			decimal trims = 0;
			decimal salario = 0;
			decimal incentivo = 0;
			decimal prestaciones = 0;
			decimal costototal = 0;
			decimal contribucion = 0;
			decimal tmp = 0;
			eficienciadeplanta = int.Parse(dsEficiencia.Tables["EPlanta"].Rows[0][0].ToString());
			for (int i = 0; i < dsEficiencia.Tables["EM"].Rows.Count; i++)
			{
				DataRow dr = dt.NewRow();
				int modulo = int.Parse(dsEficiencia.Tables["EM"].Rows[i]["modulo"].ToString());
				dr["Mod"] = dsEficiencia.Tables["EM"].Rows[i]["modulo"];
				dr["Supervisor"] = dsEficiencia.Tables["EM"].Rows[i]["supervisor"];
				dr["Eficiencia"] = dsEficiencia.Tables["EMSemana"].Rows[i]["TotalSemana"];
				int modulocp = int.Parse(dtCostoProduccion.Rows[j]["modulo"].ToString());
				if (modulo == modulocp)
				{
					dr["Cum"] = dtCostoProduccion.Rows[j]["cumplimiento"];
					decimal.TryParse(dtCostoProduccion.Rows[j]["facturado"].ToString(), out tmp);
					facturado += tmp;
					dr["Facturado"] = tmp.ToString("0,0", CultureInfo.InvariantCulture);
					decimal.TryParse(dtCostoProduccion.Rows[j]["trims"].ToString(), out tmp);
					trims += tmp;
					dr["Trims"] = tmp.ToString("0,0", CultureInfo.InvariantCulture);
					j++;
				}
				else
				{
					dr["Cum"] = 0;
					dr["Facturado"] = 0;
					dr["Trims"] = 0;
				}
				modulocp = int.Parse(dtCostoManodeObra.Rows[k]["modulo"].ToString());
				if (modulo == modulocp)
				{
					decimal.TryParse(dtCostoManodeObra.Rows[k]["salario"].ToString(), out tmp);
					salario += tmp;
					dr["Salario"] = tmp.ToString("0,0", CultureInfo.InvariantCulture);
					decimal.TryParse(dtCostoManodeObra.Rows[k]["incentivo"].ToString(), out tmp);
					incentivo += tmp;
					dr["Incentivo"] = tmp.ToString("0,0", CultureInfo.InvariantCulture);
					decimal.TryParse(dtCostoManodeObra.Rows[k]["prestaciones"].ToString(), out tmp);
					prestaciones += tmp;
					dr["Prestaciones"] = tmp.ToString("0,0", CultureInfo.InvariantCulture);
					k++;
				}
				else
				{
					dr["Salario"] = 0;
					dr["Incentivo"] = 0;
					dr["Prestaciones"] = 0;
				}
				tmp = (decimal)dr["Salario"] + (decimal)dr["Incentivo"] + (decimal)dr["Prestaciones"] + (decimal)dr["Trims"];
				dr["Costototal"] = tmp.ToString("0,0", CultureInfo.InvariantCulture);
				costototal += tmp;
				tmp = (decimal)dr["Facturado"] - (decimal)dr["Costototal"];
				dr["Contribucion"] = tmp.ToString("0,0", CultureInfo.InvariantCulture);
				contribucion += tmp;
				dt.Rows.Add(dr);
			}
			DataRow drTotales = dt.NewRow();
			drTotales["Mod"] = "0";
			drTotales["Supervisor"] = "";
			drTotales["Eficiencia"] = eficienciadeplanta;
			drTotales["Cum"] = cumplimiento;
			drTotales["Facturado"] = facturado.ToString("0,0", CultureInfo.InvariantCulture);
			drTotales["Trims"] = trims.ToString("0,0", CultureInfo.InvariantCulture);
			drTotales["Salario"] = salario.ToString("0,0", CultureInfo.InvariantCulture);
			drTotales["Incentivo"] = incentivo.ToString("0,0", CultureInfo.InvariantCulture);
			drTotales["Prestaciones"] = prestaciones.ToString("0,0", CultureInfo.InvariantCulture);
			drTotales["Costototal"] = costototal.ToString("0,0", CultureInfo.InvariantCulture);
			drTotales["Contribucion"] = contribucion.ToString("0,0", CultureInfo.InvariantCulture);
			dt.Rows.Add(drTotales);
			return dt;
		}

		public string RptContribucionXModuloHTML(DateTime fechaini, int idempresa)
		{
			EficienciaxAño(idempresa);
			int periodo = 0;
			int semana = 0;
			DataTable dtparametros = new Dato_Eficiencia().plnPeriodoCostoSel(fechaini, idempresa);
			if (dtparametros.Rows.Count == 0)
			{
				return "<tr><td>Error de parametros</td></tr>";
			}
			periodo = (int)dtparametros.Rows[0]["periodo"];
			semana = (int)dtparametros.Rows[0]["semana"];
			DataTable dt = RptContribucionXModulo(periodo, semana, idempresa);
			string tabla = "<CENTER><table border='1' cellspacing=0 cellpadding=0 bordercolor='#000000'><tr align='center' style='background: SteelBlue; color: White;font-weight: bold;'>";
			for (int i = 0; i < dt.Columns.Count; i++)
			{
				tabla = tabla + "<td>" + dt.Columns[i].ColumnName + "</td>";
			}
			tabla += "</tr>";
			for (int j = 0; j < dt.Rows.Count; j++)
			{
				if (!((decimal)dt.Rows[j]["Salario"] == 0m))
				{
					tabla += "<tr>";
					if (j == dt.Rows.Count - 1)
					{
						tabla += "<strong><td></td>";
						tabla = tabla + "<td font color = '#000000'>" + dt.Rows[j]["Supervisor"].ToString() + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000'>" + Math.Round((decimal)dt.Rows[j]["Cum"], 0) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000'>" + Math.Round((decimal)dt.Rows[j]["Eficiencia"], 0) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Salario"] }) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Incentivo"] }) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Prestaciones"] }) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Trims"] }) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000' bgcolor='#F8CBAD'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Costototal"] }) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000' bgcolor='#E2EFDA'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Facturado"] }) + "</td>";
						tabla = tabla + "<td align='right' font color = '#000000' bgcolor='#D9E1F2'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Contribucion"] }) + "</td></strong>";
						tabla += "</tr>";
					}
					else
					{
						tabla = tabla + "<td>" + dt.Rows[j]["Mod"].ToString() + "</td>";
						tabla = tabla + "<td>" + dt.Rows[j]["Supervisor"].ToString() + "</td>";
						tabla = tabla + "<td>" + Math.Round((decimal)dt.Rows[j]["Cum"], 0) + "</td>";
						tabla = tabla + "<td>" + Math.Round((decimal)dt.Rows[j]["Eficiencia"], 0) + "</td>";
						tabla = tabla + "<td align='right'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Salario"] }) + "</td>";
						tabla = tabla + "<td align='right'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Incentivo"] }) + "</td>";
						tabla = tabla + "<td align='right'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Prestaciones"] }) + "</td>";
						tabla = tabla + "<td align='right'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Trims"] }) + "</td>";
						tabla = tabla + "<td align='right' bgcolor='#F8CBAD'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Costototal"] }) + "</td>";
						tabla = tabla + "<td align='right' bgcolor='#E2EFDA'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Facturado"] }) + "</td>";
						tabla = tabla + "<td align='right' bgcolor='#D9E1F2'>" + string.Format(CultureInfo.InvariantCulture, "{0:0,0}", new object[1] { (decimal)dt.Rows[j]["Contribucion"] }) + "</td>";
						tabla += "</tr>";
					}
				}
			}
			return tabla + "</table>";
		}

		public DataTable CrearDtContribucion()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Mod", typeof(int));
			dt.Columns.Add("Supervisor", typeof(string));
			dt.Columns.Add("Cum", typeof(decimal));
			dt.Columns.Add("Eficiencia", typeof(decimal));
			dt.Columns.Add("Salario", typeof(decimal));
			dt.Columns.Add("Incentivo", typeof(decimal));
			dt.Columns.Add("Prestaciones", typeof(decimal));
			dt.Columns.Add("Trims", typeof(decimal));
			dt.Columns.Add("Costototal", typeof(decimal));
			dt.Columns.Add("Facturado", typeof(decimal));
			dt.Columns.Add("Contribucion", typeof(decimal));
			return dt;
		}

		public DataTable CrearDtHeadCount()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Modulo", typeof(int));
			dt.Columns.Add("EstiloLunes", typeof(string));
			dt.Columns.Add("LayoutLunes", typeof(decimal));
			dt.Columns.Add("HeadCountLunes", typeof(decimal));
			dt.Columns.Add("EstiloMartes", typeof(string));
			dt.Columns.Add("LayoutMartes", typeof(decimal));
			dt.Columns.Add("HeadCountMartes", typeof(decimal));
			dt.Columns.Add("EstiloMiercoles", typeof(string));
			dt.Columns.Add("LayoutMiercoles", typeof(decimal));
			dt.Columns.Add("HeadCountMiercoles", typeof(decimal));
			dt.Columns.Add("EstiloJueves", typeof(string));
			dt.Columns.Add("LayoutJueves", typeof(decimal));
			dt.Columns.Add("HeadCountJueves", typeof(decimal));
			dt.Columns.Add("EstiloViernes", typeof(string));
			dt.Columns.Add("LayoutViernes", typeof(decimal));
			dt.Columns.Add("HeadCountViernes", typeof(decimal));
			dt.Columns.Add("EstiloSabado", typeof(string));
			dt.Columns.Add("LayoutSabado", typeof(decimal));
			dt.Columns.Add("HeadCountSabado", typeof(decimal));
			return dt;
		}

		public DataTable HeadCountModulo(DateTime fechaini, DateTime fechafin, int idempresa)
		{
			DataTable dt = CrearDtHeadCount();
			int v = 0;
			while (fechaini.DayOfWeek <= fechafin.DayOfWeek && v <= 15)
			{
				DataTable dtLayout = new Dato_Eficiencia().GetLayoutPorModulo(fechaini);
				DataTable dtHeadCount = new Dato_Eficiencia().GetEmpleadosModulos(fechaini, idempresa);
				int i = 0;
				int j = 0;
				for (; i < dtLayout.Rows.Count; i++)
				{
					if (v == 0)
					{
						dt.Rows.Add(dt.NewRow());
					}
					DataRow dr = dt.Rows[i];
					dr["Modulo"] = dtLayout.Rows[i]["modulo"];
					dr[v + 1] = dtLayout.Rows[i]["estilo"];
					dr[v + 2] = dtLayout.Rows[i]["layout"];
					dr[v + 3] = 0;
					for (; j < dtHeadCount.Rows.Count; j++)
					{
						int ml = int.Parse(dtLayout.Rows[i]["codigo_depto"].ToString());
						int mh = (int)dtHeadCount.Rows[j]["codigo_depto"];
						if (ml == mh)
						{
							dr[v + 3] = dtHeadCount.Rows[j]["headcount"];
							j++;
							break;
						}
						if (ml < mh)
						{
							break;
						}
					}
				}
				v += 3;
				fechaini = fechaini.AddDays(1.0);
			}
			return dt;
		}
	}
}
