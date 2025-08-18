using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Datos;
using System.Globalization;

namespace Negocios
{

    public class Neg_Liquidacion
    {


        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Factores Dato_Factores = new Dato_Factores();
        Dato_Liquidacion Dato_Liquidacion = new Dato_Liquidacion();
        Neg_Planilla NPlanilla = new Neg_Planilla();
        Neg_Permisos NPermisos = new Neg_Permisos();
        Dato_Permisos dp = new Dato_Permisos();
        Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        #endregion
        public static class Globales
        {
            public static decimal inssLab;
            public static decimal inssPatron;
            public static decimal iR;
            public static decimal hPend;
            public static decimal horas;
            public static DateTime fechaR;
        }

        //MODIFICADO POR GRETHEL TERCERO 31-10-2016
        public bool AplicarDeduccionPrestamos(decimal cuota, int codigo, int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Liquidacion.AplicarDeduccionPrestamos(cuota, codigo, id, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable spmatrizliqQuincenal(int codEmpl)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.spmatrizliqQuincenal(codEmpl, userDetail.getIDEmpresa());
            return di;
        }
        public bool spInsertarLiquidacion(DateTime fechaIngreso, decimal salIndem, decimal salMayor, decimal salAguinaldo, decimal salAguinaldod, decimal salPromedio,
        decimal salVacaciones, decimal salVacacionesD, decimal horasT, decimal salHorast, decimal Neto, decimal m1, decimal m2, decimal m3, decimal m4, decimal m5, decimal m6,
        decimal m7, decimal m8, decimal m9, decimal m10, decimal m11, decimal m12, DateTime FecLiquida, int motivo, int codigo, decimal salMayorD)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Liquidacion.spInsertarLiquidacion(fechaIngreso, salIndem, salMayor, salAguinaldo, salAguinaldod, salPromedio, salVacaciones, salVacacionesD, horasT, salHorast, Neto, m1, m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, FecLiquida, motivo, codigo, salMayorD, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool spInsertarLiquidacionMeses(int codigo, int anio, int mes, int diasMes, decimal salario, decimal incentivo, decimal beneficio, decimal ingreso, decimal promediodias, DateTime fechaliq)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Liquidacion.spInsertarLiquidacionMeses(codigo, anio, mes, diasMes, salario, incentivo, beneficio, ingreso, promediodias, fechaliq, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool spInsertarLiquidaciones(int codigo, DateTime Fecha, int motivo, decimal Salmayor, decimal SalmayorD, decimal SalPromedio, decimal SalPromedioD,
            decimal Aguinaldo, decimal AguinaldoD, decimal vacaciones, decimal VacacionesD, decimal Indem, string Horas, decimal SalHoras, decimal Neto,
            DateTime FecIngreso, decimal Inss, decimal IrVacaciones, decimal DeducPendiente, string observ, decimal indemnizaciondia, string usuario, decimal ingresos, decimal egresos)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Liquidacion.spInsertarLiquidaciones(codigo, Fecha, motivo, Salmayor, SalmayorD, SalPromedio, SalPromedioD, Aguinaldo,
                AguinaldoD, vacaciones, VacacionesD, Indem, Horas, SalHoras, Neto, FecIngreso, Inss, IrVacaciones, DeducPendiente, observ, indemnizaciondia, usuario, ingresos, egresos, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool IngresosyDeduccionesLiqEliminar(int codigo,DateTime fechaliquidacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Liquidacion.IngresosyDeduccionesLiqEliminar(codigo,fechaliquidacion, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public DataTable obtenerIngresosPendientes(int codEmpl, DateTime fechaRenuncia)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.obtenerIngresosPendientes(codEmpl, fechaRenuncia, userDetail.getIDEmpresa());
            return di;
        }

        public DataTable obtenerTotalIngresos(int codEmpl, DateTime fechaRenuncia)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.obtenerTotalIngresos(codEmpl, fechaRenuncia, userDetail.getIDEmpresa());
            return di;
        }

        public DataTable obtenerDeduccionesPendientes(int codEmpleado, DateTime fechaRenuncia)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable de = new DataTable();
            de = Dato_Liquidacion.obtenerDeduccionesPendientes(codEmpleado, fechaRenuncia, userDetail.getIDEmpresa());
            return de;
        }
        public bool procesarLiquidacion(int codEmpl, DateTime fechaRenuncia, decimal diasVac, decimal TVacaciones, decimal indemnizacion, int motivo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            decimal inssLaboral = Globales.inssLab;
            decimal inssPatronal = Globales.inssPatron;
            decimal ir = Globales.iR;
            if (Dato_Liquidacion.procesarLiquidacion(codEmpl, fechaRenuncia, diasVac, TVacaciones, indemnizacion, inssLaboral, inssPatronal, ir, motivo, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet cargarMotivos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Liquidacion.cargarMotivos(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet ObtenerMesesLiq(int codEmpl)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Liquidacion.ObtenerMesesLiq(codEmpl, userDetail.getIDEmpresa());
            return ds;
        }

        public bool spInsertarPendientesLiq(int codigo, int id_tipo, int tipo, decimal valor, int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            decimal inssLaboral = Globales.inssLab;
            decimal inssPatronal = Globales.inssPatron;
            decimal ir = Globales.iR;

            if (Dato_Liquidacion.spInsertarPendientesLiq(codigo, id_tipo, tipo, valor, id, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool plnDeduccionesPendLiqIns(int codigo, DateTime fechaliquidacion, int tipo, decimal valor, int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            decimal inssLaboral = Globales.inssLab;
            decimal inssPatronal = Globales.inssPatron;
            decimal ir = Globales.iR;

            if (Dato_Liquidacion.plnDeduccionesPendLiqIns(codigo, fechaliquidacion, tipo, valor, id, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //planillas del mes seleccionado
        public DataTable MesesLaboradosCompletos(int codEmpl, int aplicacorte, string fechacorte)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.MesesLaboradosCompletos(codEmpl, aplicacorte, fechacorte, userDetail.getIDEmpresa());
            return di;
        }
        //MesesLaboradosCompletos
        public DataTable ObtenerPlanillasMesAguinaldo(int codEmpl, int mes)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.ObtenerPlanillasMesAguinaldo(codEmpl, mes, userDetail.getIDEmpresa());
            return di;
        }
        //obtiene neto de las planillas registradas del empleado
        public DataSet ObtenerNetoxPlanillas(int codEmpl)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet di = new DataSet();
            di = Dato_Liquidacion.ObtenerNetoxPlanillas(codEmpl, userDetail.getIDEmpresa());
            return di;
        }
        public DataTable ObtenerIngresosxPlanillas(int codEmpl)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.ObtenerIngresosxPlanillas(codEmpl, userDetail.getIDEmpresa());
            return di;
        }
        public DataTable spLiquidacionDatosEmp(int codEmpl, int bandera)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.spLiquidacionDatosEmp(codEmpl, bandera, userDetail.getIDEmpresa());
            return di;
        }
        public DataTable plnObtenerLiquidacionesxfiltro(int filtro,DateTime fechaini,DateTime fechafin, int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable di = new DataTable();
            di = Dato_Liquidacion.plnObtenerLiquidacionesxfiltro(filtro, fechaini,fechafin,codigo, userDetail.getIDEmpresa());
            return di;
        }
        public bool plnLiquidacionesCerrar( int id, int codEmpl)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Liquidacion.plnLiquidacionesCerrar(id, codEmpl,  userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //obtener dias pagados en el ultimo periodo de aguinaldo
        public int ObtenerdiasAguinaldoPayEmp(int codEmpl, DateTime fecini, DateTime fecfin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int di = 0;
            di = Dato_Liquidacion.ObtenerdiasAguinaldoPayEmp(codEmpl, fecini, fecfin, userDetail.getIDEmpresa());
            return di;
        }
        public DataSet ObtenerDatosLiquidacion(int codEmpl, int tperiodo, int aplicacortevac, double vac, int proyectamesi, int hist, bool pago)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return ObtenerPlanillasLiquidacion(codEmpl, tperiodo, aplicacortevac, vac, proyectamesi, hist, pago, userDetail.getIDEmpresa());
        }
        public DataSet ObtenerPlanillasLiquidacion(int codEmpl, int tperiodo, int aplicacortevac, double vac, int proyectamesi, int hist, bool pago, int idempresa)
        {
            try
            {

                //IUserDetail userDetail = UserDetailResolver.getUserDetail();                

                DataTable matrizliq = new DataTable();
                //Estructura de la tabla Meses.
                matrizliq.Columns.Add("MesNumero");
                matrizliq.Columns.Add("MesNombre");
                matrizliq.Columns.Add("diasMes");
                matrizliq.Columns.Add("Salario");
                matrizliq.Columns.Add("Incentivo");
                matrizliq.Columns.Add("Beneficio");
                matrizliq.Columns.Add("PromedioDias");
                matrizliq.Columns.Add("Ingreso");
                matrizliq.Columns.Add("Anio");
                //detalle de pago por smenas
                DataTable matrizdetallepago = new DataTable();
                matrizdetallepago.Columns.Add("mes");
                matrizdetallepago.Columns.Add("fechaini");
                matrizdetallepago.Columns.Add("fechafin");
                matrizdetallepago.Columns.Add("periodo");
                matrizdetallepago.Columns.Add("semana");
                matrizdetallepago.Columns.Add("salario");
                matrizdetallepago.Columns.Add("incentivo");
                matrizdetallepago.Columns.Add("beneficio");
                matrizdetallepago.Columns.Add("dias");
                matrizdetallepago.Columns.Add("salariodias");
                matrizdetallepago.Columns.Add("incentivodias");
                matrizdetallepago.Columns.Add("beneficiodias");

                DataSet Liquidacion = new DataSet();
                DataTable dp = new DataTable();
                int bandera = 0;

                if (Globales.fechaR == Convert.ToDateTime("01/01/0001"))
                {
                    Globales.fechaR = DateTime.Now;
                    bandera = 2;//corte a la fecha actual
                }
                else
                    bandera = 1;//corte a la fecha de egreso                             

                if (codEmpl == 870707)
                {

                }
                DataTable deDatos = Dato_Liquidacion.spLiquidacionDatosEmp(codEmpl, bandera, idempresa);

                if (deDatos.Rows.Count != 0)//si el empleado existe
                {
                    DateTime fechaingreso = Convert.ToDateTime(deDatos.Rows[0]["fecha_ingreso"]);
                    DateTime fechaegreso = Convert.ToDateTime(deDatos.Rows[0]["fecha_egreso"]);
                    int estado = Convert.ToInt32(deDatos.Rows[0]["idestado"]);
                    double salariomensual = Convert.ToDouble(deDatos.Rows[0]["salariomensual"]);//contrato
                    double salariomensualp = Convert.ToDouble(deDatos.Rows[0]["salariop"]);//salario fijo sumado con beneficios
                    //si hay salario que recibi incluido beneficios
                    salariomensual = salariomensualp > salariomensual ? salariomensualp : salariomensual;
                    double DiasTrabajados = (Globales.fechaR - fechaingreso).Days + 1;
                    double diasVacaciones = 0, saldovaccorte = 0;
                    int TipoSalario = 0;
                    //dias de vacaciones acumulados al corte
                    DataTable vacaciones = CalcularDiasVacaciones(codEmpl, fechaingreso, aplicacortevac, idempresa);
                    if (vacaciones.Rows.Count > 0)
                    {
                        saldovaccorte = Convert.ToDouble(vacaciones.Rows[0]["saldovacaciones"]);
                    }
                    diasVacaciones = saldovaccorte;

                    if (aplicacortevac == 1)//si no se pagan todos los dias
                    {
                        if (vac > 0 && vac <= saldovaccorte)//lo solicitado debe ser menor o igual al saldo
                        {
                            diasVacaciones = vac;
                        }
                    }

                    if (deDatos.Rows[0][6].ToString() == "1" || deDatos.Rows[0][6].ToString() == "2")//Planilla Catorcenal fija y Variable
                    {
                        double salario = 0, incentivo = 0, beneficio = 0;
                        double salariomes = 0;//historico, salario que devengo en el mes
                        TipoSalario = 0;
                        DataTable fechafindt = new DataTable();
                        DateTime fechafinmax = Globales.fechaR;
                        //Obtengo las planillas registradas para codEmpl.                    
                        dp = Dato_Liquidacion.ObtenerPlanillasLiquidacion(codEmpl, idempresa);

                        //obtengo la fecha cierre de la ultima planilla cerrada
                        if (dp.Rows.Count > 0)
                            fechafindt = dp.Rows.Cast<DataRow>().OrderByDescending(row => Convert.ToDateTime(row["fechafin"])).Take(1).CopyToDataTable();

                        if (fechafindt.Rows.Count > 0)
                        {
                            DateTime ftemp = Convert.ToDateTime(fechafindt.Rows[0]["fechafin"]);
                            if (proyectamesi == 1 || (fechafinmax.Month > ftemp.Month && fechafinmax.Year == ftemp.Year) || hist == 1)
                            {
                                if (proyectamesi == 1 && !pago && fechafinmax.Month == ftemp.Month && fechafinmax.Year == ftemp.Year && fechafinmax.Day > ftemp.Day)
                                {
                                    fechafinmax = ftemp;
                                }
                                else
                                {
                                    fechafinmax = Globales.fechaR;
                                    if (estado != 1 && fechafinmax > fechaegreso)
                                    {
                                        fechafinmax = fechaegreso;
                                    }
                                }
                            }
                            else if ((proyectamesi == 0 || (fechafinmax.Month == ftemp.Month && fechafinmax.Year == ftemp.Year && fechafinmax.Day > ftemp.Day)) && hist == 0)
                            {
                                fechafinmax = ftemp;
                            }
                        }
                        //Ultimos 6 meses                          
                        DataTable dmAnio;
                        //**********
                        dmAnio = ObtenerMesesPrestaciones(fechaingreso, fechafinmax);

                        DataRow[] drresult = null;
                        DataTable dm;

                        if (proyectamesi == 0)
                        {
                            drresult = dmAnio.Select("DIA=TDIASMES");
                        }
                        else//no se excluye meses incompletos para proyeccion en calculo de pasivo laboral
                        {
                            drresult = dmAnio.Select();
                        }

                        if (drresult.Length > 0)
                            dm = drresult.OrderByDescending(row => Convert.ToInt32(row["Anio"])).ThenByDescending(row => Convert.ToInt32(row["Mes"])).Take(6).CopyToDataTable();
                        else//***************
                            dm = dmAnio.Rows.Cast<DataRow>().OrderByDescending(row => Convert.ToInt32(row["Anio"])).ThenByDescending(row => Convert.ToInt32(row["Mes"])).CopyToDataTable();

                        if (dp.Rows.Count > 0)
                            TipoSalario = Convert.ToInt32(dp.Rows[0]["tiposalario"].ToString());
                        else//empleados que no se han reportado en planilla 
                        {
                            matrizliq.Rows.Add(Globales.fechaR.Month, "", DateTime.DaysInMonth(Globales.fechaR.Year, Globales.fechaR.Month), 0, 0, 0, 0, 0, Globales.fechaR.Year);
                        }
                        //Recorre las planillas del codEmpl para determinar si pertenecen a cada mes y asi asignar un Neto por Mes.                        
                        for (int i = 0; i < dm.Rows.Count; i++)
                        {
                            int MesCompleto = Convert.ToInt32(dm.Rows[i]["mes"].ToString().Trim());
                            string MesCompletoName = dm.Rows[i]["nombreMes"].ToString().Trim();
                            int Anio = Convert.ToInt32(dm.Rows[i]["Anio"].ToString().Trim());
                            int diasT = 0;
                            diasT = Convert.ToInt32(dm.Rows[i]["Dia"].ToString().Trim());//dias trabajados reportados en planilla
                            //***********
                            //if (fechafinmax.Month == MesCompleto && fechafinmax.Year == Anio)
                            //{
                            //    diasT = fechafinmax.Day;
                            //}                            
                            int TdiasMes = Convert.ToInt32(dm.Rows[i]["TdiasMes"].ToString().Trim());//dias del mes segun calendario

                            matrizliq.Rows.Add(MesCompleto, MesCompletoName, TdiasMes, 0, 0, 0, 0, 0, Anio);
                            //salario e incentivo acumulado en el mes
                            salario = 0.00;
                            incentivo = 0.00;
                            beneficio = 0.00;

                            DateTime SemanaInicio = DateTime.Now, SemanaFin = DateTime.Now;
                            string periodo = "", semana = "";
                            double diassemana = 0, incentivosemana = 0, salariosemana = 0, beneficiosemana = 0, salariosem = 0;

                            foreach (DataRow dr1 in dp.Rows)
                            {
                                SemanaInicio = Convert.ToDateTime(dr1["fechaini"].ToString().Trim());
                                SemanaFin = Convert.ToDateTime(dr1["fechafin"].ToString().Trim());
                                periodo = dr1["periodo"].ToString().Trim();
                                semana = dr1["semana"].ToString().Trim();
                                //variables para construir tabla de detalle de planillas por semanas (utilizada en pantalla de consultar pago por mes)
                                diassemana = 0; incentivosemana = 0; salariosemana = 0; beneficiosemana = 0; salariosem = 0;

                                if (TipoSalario == 1)//salario fijo                                
                                    salario = Convert.ToDouble(dr1["salario"]);

                                //semana pertence al mismo mes y anio
                                if ((SemanaInicio.Month == MesCompleto && SemanaInicio.Year == Anio) && (SemanaFin.Month == MesCompleto && SemanaFin.Year == Anio))
                                {
                                    diassemana = (SemanaFin - SemanaInicio).Days + 1;//7 dias   
                                    //******                            
                                    incentivosemana = Convert.ToDouble(dr1["Incentivo"].ToString().Trim());
                                    incentivo += incentivosemana;
                                    beneficiosemana = Convert.ToDouble(dr1["Beneficio"].ToString().Trim());
                                    beneficio += beneficiosemana;
                                    //salariosemana para fijos es salario mensual, variables salario de la semana
                                    salariosemana = Convert.ToDouble(dr1["salario"].ToString().Trim());
                                    //salariosem es para todos el salario de la semana
                                    salariosem = Convert.ToDouble(dr1["salariosem"].ToString().Trim());
                                    //salario mes historico por planilla
                                    salariomes = Convert.ToDouble(dr1["salariomensual"]);

                                    if (TipoSalario == 2)//variable
                                    {
                                        ///**********                                        
                                        salario += salariosemana;
                                        matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosemana, incentivosemana, beneficiosemana, diassemana, salariosemana.ToString("n2"), incentivosemana.ToString("n2"), beneficiosemana.ToString("n2"));
                                    }
                                    else//fijo
                                    {
                                        //salariosemana = salariosemana > salario ? salariosemana : salario;
                                        //salario = salariosemana;
                                        matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosem, incentivosemana, beneficiosemana, 0, 0, 0, 0);
                                    }
                                }
                                else
                                {
                                    if ((SemanaInicio.Month == MesCompleto && SemanaInicio.Year == Anio) || (SemanaFin.Month == MesCompleto && SemanaFin.Year == Anio))
                                    {
                                        //*****
                                        incentivosemana = Convert.ToDouble(dr1["Incentivo"].ToString().Trim());
                                        beneficiosemana = Convert.ToDouble(dr1["Beneficio"].ToString().Trim());
                                        salariosemana = Convert.ToDouble(dr1["salario"].ToString().Trim());
                                        salariosem = Convert.ToDouble(dr1["salariosem"].ToString().Trim());
                                        salariomes = Convert.ToDouble(dr1["salariomensual"]);

                                        if (TipoSalario == 2)//salario variable
                                        {
                                            int k = 0;
                                            for (int j = 0; j < 7; j++)
                                            {
                                                if (SemanaInicio.AddDays(j).Month == MesCompleto)
                                                    k++;
                                            }
                                            diassemana = k;
                                            incentivo += ((incentivosemana / 7) * k);
                                            beneficio += ((beneficiosemana / 7) * k);
                                            salario += ((salariosemana / 7) * k);

                                            matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosemana, incentivosemana, beneficiosemana, diassemana, ((salariosemana / 7) * diassemana).ToString("n2"), ((incentivosemana / 7) * diassemana).ToString("n2"), ((beneficiosemana / 7) * diassemana).ToString("n2"));
                                        }
                                        else//salario fijo
                                        {
                                            matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosem, incentivosemana, beneficiosemana, 0, 0, 0, 0);
                                        }
                                    }
                                    
                                }

                            }

                            if (diasT != TdiasMes && i == 0)
                            {
                                if (TipoSalario == 2 && proyectamesi == 1 && !pago)
                                {
                                    if (fechaingreso <= new DateTime(Anio, MesCompleto, 1))
                                    {
                                        salario = salario / (double)diasT * 30.0;
                                        incentivo = incentivo / (double)diasT * 30.0;
                                        beneficio = beneficio / (double)diasT * 30.0;
                                    }
                                    else
                                    {
                                        salario = salario / (double)diasT * (double)(30 - fechaingreso.Day + 1);
                                        incentivo = incentivo / (double)diasT * (double)(30 - fechaingreso.Day + 1);
                                        beneficio = beneficio / (double)diasT * (double)(30 - fechaingreso.Day + 1);
                                    }
                                }
                            }
                            else if (diasT != TdiasMes && i != 0)//meses incompletos antiguos se eliminan de la lista
                            {
                                matrizliq.Rows.RemoveAt(i);
                            }
                            double Ingreso = 0.0;
                            double DiasPromedio = 0.0;
                            int diasMes = 30;
                            bool nuevoing = false;
                            if (diasT != TdiasMes && i != 0)
                            {
                                continue;
                            }
                            if (TipoSalario == 2)
                            {
                                if (i == 0 && diasT != TdiasMes && fechaingreso > new DateTime(Anio, MesCompleto, 1))
                                {
                                    nuevoing = true;
                                    if (!(Math.Round(salario, 2) + Math.Round(incentivo, 2) + Math.Round(beneficio, 2) < salariomes))
                                    {
                                        diasMes = (pago ? diasT : (30 - fechaingreso.Day + 1));
                                    }
                                    else
                                    {
                                        salario = salariomes;
                                        diasMes = 30;
                                        incentivo = 0.0;
                                        beneficio = 0.0;
                                    }
                                }
                            }
                            else
                            {
                                incentivo = 0.0;
                                beneficio = 0.0;
                            }
                            if (!nuevoing && salario < salariomes)
                            {
                                salario = salariomes;
                            }
                            Ingreso = Math.Round(salario, 2) + Math.Round(incentivo, 2) + Math.Round(beneficio, 2);
                            DiasPromedio = Ingreso / (double)diasMes;
                            matrizliq.Rows[i]["Salario"] = Math.Round(salario, 2);
                            matrizliq.Rows[i]["Incentivo"] = Math.Round(incentivo, 2);
                            matrizliq.Rows[i]["Beneficio"] = Math.Round(beneficio, 2);
                            matrizliq.Rows[i]["Ingreso"] = Math.Round(Ingreso, 2);
                            matrizliq.Rows[i]["PromedioDias"] = Math.Round(DiasPromedio, 2);
                            if (Convert.ToDouble(matrizliq.Rows[i]["Salario"]) == 0.0)
                            {
                                matrizliq.Rows[i]["Salario"] = Math.Round(salariomensual, 2);
                                matrizliq.Rows[i]["Ingreso"] = Math.Round(salariomensual, 2);
                                matrizliq.Rows[i]["PromedioDias"] = Math.Round(salariomensual, 2) / 30.0;
                            }
                        }
                    }
                    ///Por ser quincenal y ser meses completos,no hay necesidad de desglozar los dias ,ver procedimiento spmatrizliqQuincenal.
                    if (deDatos.Rows[0][6].ToString() == "3")
                        matrizliq = spmatrizliqQuincenal(codEmpl);

                    DataTable dttemp = matrizliq;
                    DataTable dtParametro = calculosLiquidacion(dttemp, codEmpl, tperiodo, fechaingreso, fechaegreso, estado, salariomensual, diasVacaciones, DiasTrabajados, TipoSalario, idempresa);

                    if (tperiodo == 1)//si es liquidacion o pasivo laboral                                            
                        if (DiasTrabajados < 30)
                            matrizliq.Clear();

                    Liquidacion.Tables.Add(matrizliq.Copy());
                    Liquidacion.Tables.Add(dtParametro.Copy());
                    Liquidacion.Tables.Add(matrizdetallepago.Copy());
                }
                return Liquidacion;
            }
            catch (Exception ex)
            {
                DataSet dsTemporal = null;
                return dsTemporal;
            }
        }
        public DataTable ObtenerPlanillaMesProyecta(int codEmpl, int estado, DateTime fechaperiodo)
        {
            try
            {

                IUserDetail userDetail = UserDetailResolver.getUserDetail();


                //detalle de pago por smenas
                DataTable matrizdetallepago = new DataTable();
                matrizdetallepago.Columns.Add("mes");
                matrizdetallepago.Columns.Add("fechaini");
                matrizdetallepago.Columns.Add("fechafin");
                matrizdetallepago.Columns.Add("periodo");
                matrizdetallepago.Columns.Add("semana");
                matrizdetallepago.Columns.Add("salario");
                matrizdetallepago.Columns.Add("incentivo");
                matrizdetallepago.Columns.Add("beneficio");
                matrizdetallepago.Columns.Add("dias");
                matrizdetallepago.Columns.Add("salariodias");
                matrizdetallepago.Columns.Add("incentivodias");
                matrizdetallepago.Columns.Add("beneficiodias");

                DataTable deDatos = Dato_Liquidacion.spLiquidacionDatosEmp(codEmpl, 1, userDetail.getIDEmpresa());

                if (deDatos.Rows.Count != 0)//si el empleado existe
                {
                    DateTime fechaingreso = Convert.ToDateTime(deDatos.Rows[0]["fecha_ingreso"]);
                    DateTime fechaegreso = Convert.ToDateTime(deDatos.Rows[0]["fecha_egreso"]);
                    DateTime fechadefault = new DateTime(1900, 1, 1, 0, 0, 0);
                    //int estado= Convert.ToInt32(deDatos.Rows[0]["idestado"]);
                    double salariomensual = Convert.ToDouble(deDatos.Rows[0]["salariomensual"]);

                    if (deDatos.Rows[0]["idTipoSalario"].ToString() == "1" || deDatos.Rows[0]["idTipoSalario"].ToString() == "2")//Planilla Catorcenal fija y Variable
                    {
                        double salario = 0, incentivo = 0, beneficio = 0;
                        int TipoSalario = 0;

                        DataTable dm = new DataTable();
                        DateTime SemanaInicioP = DateTime.Now, SemanaFinP = DateTime.Now;

                        //fecha inicio
                        int result;
                        result = DateTime.Compare(fechaingreso, fechaperiodo);//1ero fecha del mes consultado

                        if (result >= 0)
                        {//mayor                        
                            SemanaInicioP = fechaingreso;
                        }
                        else
                        {//menor o igual                                                                                                 
                            SemanaInicioP = fechaperiodo;
                        }
                        //fecha fin
                        if (estado == 1)
                        {
                            SemanaFinP = Globales.fechaR;
                        }
                        else
                        {
                            result = DateTime.Compare(fechaegreso, Globales.fechaR);//fin fecha del mes consultado
                                                                                    //fecha fin
                            if (result >= 0)
                            {//mayor                            
                                SemanaFinP = Globales.fechaR;
                            }
                            else
                            {//menor o igual                                                                      
                                SemanaFinP = fechaegreso;
                            }
                        }
                        int diaspendientes = (SemanaFinP - SemanaInicioP).Days + 1;
                        //si hay dias sin pagar al corte
                        if (diaspendientes > 0)
                        {
                            dm = ObtenerMesesPrestaciones(SemanaInicioP, SemanaFinP);

                            DataTable dp = new DataTable();
                            //Obtengo las planillas registradas para codEmpl.                    
                            dp = Dato_Liquidacion.ObtenerPlanillasxMes(codEmpl, new DateTime(Globales.fechaR.Year, Globales.fechaR.Month, 1), Globales.fechaR, userDetail.getIDEmpresa());

                            if (dp.Rows.Count > 0)
                            {
                                TipoSalario = Convert.ToInt32(dp.Rows[0]["tiposalario"].ToString());

                                //Recorre las planillas del codEmpl para determinar si pertenecen a cada mes y asi asignar un Neto por Mes.                        
                                for (int i = 0; i < dm.Rows.Count; i++)
                                {
                                    int MesCompleto = Convert.ToInt32(dm.Rows[i]["mes"].ToString().Trim());
                                    string MesCompletoName = dm.Rows[i]["nombreMes"].ToString().Trim();
                                    int Anio = Convert.ToInt32(dm.Rows[i]["Anio"].ToString().Trim());
                                    int diasT = Convert.ToInt32(dm.Rows[i]["Dia"].ToString().Trim());//dias trabajados reportados en planilla
                                    int TdiasMes = Convert.ToInt32(dm.Rows[i]["TdiasMes"].ToString().Trim());//dias del mes segun calendario

                                    //salario e incentivo acumulado en el mes
                                    salario = 0.00;
                                    incentivo = 0.00;
                                    beneficio = 0.00;


                                    string periodo = "", semana = "";
                                    double diassemana = 0, incentivosemana = 0, salariosemana = 0, beneficiosemana = 0, salariosem = 0;
                                    DateTime SemanaInicio = DateTime.Now, SemanaFin = DateTime.Now;

                                    foreach (DataRow dr1 in dp.Rows)
                                    {
                                        SemanaInicio = Convert.ToDateTime(dr1["fechaini"].ToString().Trim());
                                        SemanaFin = Convert.ToDateTime(dr1["fechafin"].ToString().Trim());
                                        periodo = dr1["periodo"].ToString().Trim();
                                        semana = dr1["semana"].ToString().Trim();

                                        //variables para construir tabla de detalle de planillas por semanas (utilizada en pantalla de consultar pago por mes)
                                        incentivosemana = 0; salariosemana = 0; beneficiosemana = 0; salariosem = 0;

                                        if (TipoSalario == 1)//salario fijo                                
                                            salario = Convert.ToDouble(dr1["salario"]);

                                        //semana pertence al mismo mes y anio
                                        if ((SemanaInicio.Month == MesCompleto && SemanaInicio.Year == Anio) && (SemanaFin.Month == MesCompleto && SemanaFin.Year == Anio))
                                        {
                                            diassemana += (SemanaFin - SemanaInicio).Days + 1;//7 dias                               
                                            incentivosemana = Convert.ToDouble(dr1["Incentivo"].ToString().Trim());
                                            incentivo += incentivosemana;
                                            beneficiosemana = Convert.ToDouble(dr1["Beneficio"].ToString().Trim());
                                            beneficio += beneficiosemana;
                                            //salariosemana para fijos es salario mensual, variables salario de la semana
                                            salariosemana = Convert.ToDouble(dr1["salario"].ToString().Trim());
                                            //salariosem es para todos el salario de la semana
                                            salariosem = Convert.ToDouble(dr1["salariosem"].ToString().Trim());

                                            if (TipoSalario == 2)//variable
                                            {
                                                salario += salariosemana;
                                                // matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosemana, incentivosemana, beneficiosemana, diassemana, salariosemana.ToString("n2"), incentivosemana.ToString("n2"), beneficiosemana.ToString("n2"));
                                            }
                                            else//fijo
                                            {
                                                salariosemana = salariosemana > salario ? salariosemana : salario;
                                                salario = salariosemana;
                                                // matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosem, incentivosemana, beneficiosemana, 0, 0, 0, 0);
                                            }
                                        }
                                        else
                                        {
                                            if ((SemanaInicio.Month == MesCompleto && SemanaInicio.Year == Anio) || (SemanaFin.Month == MesCompleto && SemanaFin.Year == Anio))
                                            {
                                                incentivosemana = Convert.ToDouble(dr1["Incentivo"].ToString().Trim());
                                                beneficiosemana = Convert.ToDouble(dr1["Beneficio"].ToString().Trim());
                                                salariosemana = Convert.ToDouble(dr1["salario"].ToString().Trim());
                                                salariosem = Convert.ToDouble(dr1["salariosem"].ToString().Trim());

                                                if (TipoSalario == 2)//salario variable
                                                {
                                                    int k = 0;
                                                    for (int j = 0; j < 7; j++)
                                                    {
                                                        if (SemanaInicio.AddDays(j).Month == MesCompleto)
                                                            k++;
                                                    }
                                                    diassemana += k;
                                                    incentivo += ((incentivosemana / 7) * k);
                                                    beneficio += ((beneficiosemana / 7) * k);
                                                    salario += ((salariosemana / 7) * k);

                                                    // matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosemana, incentivosemana, beneficiosemana, diassemana, ((salariosemana / 7) * diassemana).ToString("n2"), ((incentivosemana / 7) * diassemana).ToString("n2"), ((beneficiosemana / 7) * diassemana).ToString("n2"));
                                                }
                                                else//salario fijo
                                                {
                                                    // matrizdetallepago.Rows.Add(MesCompleto, SemanaInicio.ToShortDateString(), SemanaFin.ToShortDateString(), periodo, semana, salariosem, incentivosemana, beneficiosemana, 0, 0, 0, 0);
                                                }
                                            }

                                        }

                                    }
                                    if (i == 0)//proyecta ultimo mes en caso que sea incompleto, reporte de pasivo laboral                                
                                    {
                                        //NUEVO SE AGREGO PARA REPORTE DE PLANILLAS POR MES                                   
                                        if (TipoSalario == 2)
                                        {
                                            // salario = (salario / diasT) * 30;
                                            incentivo = (incentivo / diassemana) * diaspendientes;
                                            beneficio = (beneficio / diassemana) * diaspendientes;

                                            //NUEVO SE AGREGO PARA REPORTE DE PLANILLAS POR MES                                    
                                            matrizdetallepago.Rows.Add(MesCompleto, SemanaInicioP.ToShortDateString(), SemanaFinP.ToShortDateString(), 0, 0, ((salariomensual / 30) * diaspendientes).ToString("n2"), incentivo.ToString("n2"), beneficio.ToString("n2"), diaspendientes, ((salariomensual / 30) * diaspendientes).ToString("n2"), incentivo.ToString("n2"), beneficio.ToString("n2"));
                                        }
                                        else //NUEVO SE AGREGO PARA REPORTE DE PLANILLAS POR MES
                                        {
                                            matrizdetallepago.Rows.Add(MesCompleto, SemanaInicioP.ToShortDateString(), SemanaFinP.ToShortDateString(), 0, 0, ((salariomensual / 30) * diaspendientes).ToString("n2"), incentivo.ToString("n2"), beneficio.ToString("n2"), diaspendientes, 0, 0, 0);
                                        }
                                    }

                                }
                            }
                            else//empleados que no se han reportado en planilla 
                            {
                                //NUEVO SE AGREGO PARA REPORTE DE PLANILLAS POR MES                               
                                matrizdetallepago.Rows.Add(Globales.fechaR.Month, SemanaInicioP.ToShortDateString(), SemanaFinP.ToShortDateString(), 0, 0, ((salariomensual / 30) * diaspendientes).ToString("n2"), 0, 0, diaspendientes, ((salariomensual / 30) * diaspendientes).ToString("n2"), 0, 0);

                            }
                        }


                    }

                }
                return matrizdetallepago;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable calculosLiquidacion(DataTable matrizliq, int codEmpleado, int tperiodo, DateTime fechaingreso, DateTime fechaegreso, int estado, double salariomensual, double diasVacaciones, double DiasTrabajados, int TipoSalario, int idempresa)
        {
            try
            {
                DataTable dfa = new DataTable();
                dfa = Dato_Factores.obtenerFactor(idempresa);
                double factorInss = Convert.ToDouble(dfa.Rows[1]["factor"].ToString());
                double factorInssPatronal = Convert.ToDouble(dfa.Rows[2]["factor"].ToString());
                double factorInatec = Convert.ToDouble(dfa.Rows[3]["factor"].ToString());
                double SalarioMaximo = 0.0;
                double SalarioPromedio = 0.0;
                if (matrizliq.Rows.Count > 0)
                {
                    SalarioMaximo = Convert.ToDouble(matrizliq.AsEnumerable().Max((DataRow row) => Convert.ToDouble(row["PromedioDias"])));
                    SalarioPromedio = Convert.ToDouble(matrizliq.AsEnumerable().Sum((DataRow row) => Convert.ToDouble(row["PromedioDias"]))) / (double)matrizliq.Rows.Count;
                }
                if (SalarioMaximo < salariomensual / 30.0)
                {
                    SalarioMaximo = salariomensual / 30.0;
                }
                if (SalarioPromedio < salariomensual / 30.0)
                {
                    SalarioPromedio = salariomensual / 30.0;
                }
                DataTable dtTemporal = matrizliq;
                double IngresoMaximoMes = 0.0;
                double SumaIngresos = 0.0;
                double IngresoPromedioMes = 0.0;
                if (matrizliq.Rows.Count > 0)
                {
                    dtTemporal = (from DataRow row in dtTemporal.Rows
                                  orderby Convert.ToDouble(row["Ingreso"]) descending
                                  select row).CopyToDataTable();
                    IngresoMaximoMes = Convert.ToDouble(dtTemporal.Rows[0]["Ingreso"].ToString());
                    SumaIngresos = Convert.ToDouble(matrizliq.AsEnumerable().Sum((DataRow row) => Convert.ToDouble(row["Ingreso"])));
                    IngresoPromedioMes = SumaIngresos / (double)matrizliq.Rows.Count;
                }
                if (IngresoMaximoMes < salariomensual)
                {
                    IngresoMaximoMes = salariomensual;
                }
                if (IngresoPromedioMes < salariomensual)
                {
                    IngresoPromedioMes = salariomensual;
                }
                double vacaciones = diasVacaciones * SalarioPromedio;
                decimal dimpuesto = default(decimal);
                decimal inssPatr = default(decimal);
                decimal inssVac = default(decimal);
                decimal inactecVac = default(decimal);
                if (vacaciones > 0.0)
                {
                    inssPatr = Convert.ToDecimal(vacaciones) * Convert.ToDecimal(factorInssPatronal);
                    inssVac = Convert.ToDecimal(vacaciones) * Convert.ToDecimal(factorInss) / 100m;
                    inactecVac = Convert.ToDecimal(vacaciones) * Convert.ToDecimal(factorInatec);
                }
                if (diasVacaciones > 0.0)
                {
                }
                double totalAguinaldo = 0.0;
                double indemnizacion = 0.0;
                double totalPagar = 0.0;
                int pagui = Dato_Liquidacion.ObtenerdiasAguinaldoPayEmp(codEmpleado, new DateTime(Globales.fechaR.Year - 1, 12, 1), new DateTime(Globales.fechaR.Year, 11, 30), idempresa);
                double diasAguinaldo = 0.0;
                double diasprestaciones = 0.0;
                int result = DateTime.Compare(fechaingreso, new DateTime(Globales.fechaR.Year - 1, 12, 1));
                if (estado == 1)
                {
                    if (pagui > 0)
                    {
                        diasAguinaldo = (Globales.fechaR - new DateTime(Globales.fechaR.Year, 12, 1)).Days + 1;
                        diasprestaciones = CalcularDiasPrestacion(new DateTime(Globales.fechaR.Year, 12, 1), Globales.fechaR, 0, 0);
                    }
                    else if (result > 0)
                    {
                        diasAguinaldo = (Globales.fechaR - fechaingreso).Days + 1;
                        diasprestaciones = CalcularDiasPrestacion(fechaingreso, Globales.fechaR, 0, 0);
                    }
                    else
                    {
                        diasAguinaldo = (Globales.fechaR - new DateTime(Globales.fechaR.Year - 1, 12, 1)).Days + 1;
                        diasprestaciones = CalcularDiasPrestacion(new DateTime(Globales.fechaR.Year - 1, 12, 1), Globales.fechaR, 0, 0);
                    }
                }
                else if (pagui > 0)
                {
                    diasAguinaldo = (fechaegreso - new DateTime(fechaegreso.Year, 12, 1)).Days + 1;
                    diasprestaciones = CalcularDiasPrestacion(new DateTime(fechaegreso.Year, 12, 1), fechaegreso, 0, 0);
                }
                else if (result > 0)
                {
                    diasAguinaldo = (fechaegreso - fechaingreso).Days + 1;
                    diasprestaciones = CalcularDiasPrestacion(fechaingreso, fechaegreso, 0, 0);
                }
                else
                {
                    diasAguinaldo = (fechaegreso - new DateTime(fechaegreso.Year - 1, 12, 1)).Days + 1;
                    diasprestaciones = CalcularDiasPrestacion(new DateTime(fechaegreso.Year - 1, 12, 1), fechaegreso, 0, 0);
                }
                if (diasAguinaldo >= 1.0 && diasAguinaldo <= 365.0)
                {
                    totalAguinaldo = diasprestaciones * SalarioMaximo;
                }
                else if (diasAguinaldo > 365.0)
                {
                    totalAguinaldo = 29.99999999999918 * SalarioMaximo;
                }
                double diasIndemnizacion = 0.0;
                diasIndemnizacion = CalcularDiasPrestacion(fechaingreso, Globales.fechaR, 1, 0);
                indemnizacion = diasIndemnizacion * SalarioPromedio;
                totalPagar = indemnizacion + totalAguinaldo + vacaciones;
               
                DataTable dtParametrosLiq = new DataTable();
                dtParametrosLiq.Columns.Add("fechaingreso");
                dtParametrosLiq.Columns.Add("salMensual");
                dtParametrosLiq.Columns.Add("salMayorDia");
                dtParametrosLiq.Columns.Add("salPromedioDia");
                dtParametrosLiq.Columns.Add("IndemnizacionDia");
                dtParametrosLiq.Columns.Add("Indemnizacion");
                dtParametrosLiq.Columns.Add("AguinaldoDia");
                dtParametrosLiq.Columns.Add("Aguinaldo");
                dtParametrosLiq.Columns.Add("vacacionesDia");
                dtParametrosLiq.Columns.Add("Vacaciones");
                dtParametrosLiq.Columns.Add("totalPagar");
                dtParametrosLiq.Columns.Add("salMayor");
                dtParametrosLiq.Columns.Add("salPromedio");
                dtParametrosLiq.Columns.Add("INSS");
                dtParametrosLiq.Columns.Add("IR");
                dtParametrosLiq.Columns.Add("Patronal_Vacaciones");
                dtParametrosLiq.Columns.Add("Inatec_Vacaciones");
                dtParametrosLiq.Columns.Add("HorasPend");
                dtParametrosLiq.Columns.Add("SalarioPend");
                dtParametrosLiq.Columns.Add("Observacion");
                dtParametrosLiq.Columns.Add("CodMotivo");
                dtParametrosLiq.Columns.Add("TipoSalario");
                decimal horaspend = default(decimal);
                decimal salariopend = default(decimal);
                string observacion = "";
                string codmotivo = "0";
                if (tperiodo == 1 && DiasTrabajados < 30.0)
                {
                    salariomensual = 0.0;
                    SalarioMaximo = 0.0;
                    SalarioPromedio = 0.0;
                    indemnizacion = 0.0;
                    diasprestaciones = 0.0;
                    totalAguinaldo = 0.0;
                    vacaciones = 0.0;
                    IngresoMaximoMes = 0.0;
                    IngresoPromedioMes = 0.0;
                    inssVac = default(decimal);
                    dimpuesto = default(decimal);
                    inssPatr = default(decimal);
                    inactecVac = default(decimal);
                }
                dtParametrosLiq.Rows.Add(fechaingreso.ToShortDateString(), salariomensual, SalarioMaximo, SalarioPromedio, diasIndemnizacion, indemnizacion, diasprestaciones, totalAguinaldo, diasVacaciones, vacaciones, totalPagar, IngresoMaximoMes, IngresoPromedioMes, inssVac, dimpuesto, inssPatr, inactecVac, horaspend, salariopend, observacion, codmotivo, TipoSalario);
                return dtParametrosLiq;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        } //Obtiene los dias calculados en base factor mes/dia parametro 12 meses anteriores
        public DataTable ObtenerMesesPrestaciones(DateTime ini, DateTime fin)
        {
            DataTable tmeses = new DataTable();
            tmeses.Columns.Add("Dia");
            tmeses.Columns.Add("Mes");
            tmeses.Columns.Add("NombreMes");
            tmeses.Columns.Add("Anio");
            tmeses.Columns.Add("TdiasMes");

            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            DateTime contador;
            int mes = 0, diasmescalendario = 0, year = ini.Year, result;
            double diasmes = 0;
            string mesnombre = "";

            try
            {
                contador = ini;
                while (contador <= fin)
                {
                    mes = contador.Month;
                    mesnombre = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtinfo.GetMonthName(mes));
                    diasmescalendario = DateTime.DaysInMonth(year, mes);
                    year = contador.Year;

                    result = DateTime.Compare(new DateTime(year, mes, diasmescalendario), fin);

                    if (result > 0)//fin de mes mayor a fecha fin
                        diasmes = (fin - contador).Days + 1;
                    else//menor o igual 
                        diasmes = (new DateTime(year, mes, diasmescalendario) - contador).Days + 1;

                    tmeses.Rows.Add(diasmes, mes, mesnombre, year, diasmescalendario);

                    contador = contador.AddDays(diasmes);
                }

                return tmeses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
       
        public double CalcularDiasPrestacion(DateTime fini, DateTime ffin, int prestacion, int meseq)
        {
            double diascalendario = 0, diasprestacion = 0;
            int diasmes = 0;

            if (meseq == 0)
            {
                diascalendario = (((ffin.Year - fini.Year) * 12 + (ffin.Month - fini.Month)) * 30);//+ ((ffin.Day - fini.Day) + 1));

                if (DateTime.DaysInMonth(fini.Year, fini.Month) == 31 && fini.Day != 1)
                {
                    diascalendario += ((ffin.Day - fini.Day) + 2);
                }
                else
                {
                    diascalendario += ((ffin.Day - fini.Day) + 1);
                }
            }
            else//dias acumulados en el mismo mes
            {
                if (fini.Year == ffin.Year && fini.Month == ffin.Month)//dias del mismo mes
                {
                    diasmes = DateTime.DaysInMonth(fini.Year, fini.Month);
                    if (fini.Day == 1 && (diasmes == ffin.Day))
                    {
                        diascalendario += 30;
                    }
                    else
                    {
                        if (ffin.Day == diasmes && ffin.Month == 2)
                        {
                            diascalendario += ((30 - fini.Day) + 1);
                        }
                        else
                        {
                            diascalendario += ((ffin.Day - fini.Day) + 1);
                        }

                    }
                }
            }

            //factores por rango dias acumulados
            if (prestacion == 0 || diascalendario <= 1080)//dias prestaciones o 3 anios de indemnizacion
            {
                diasprestacion += Math.Round(diascalendario * Convert.ToDouble(0.083333333333), 2);
            }
            else if (prestacion == 1 && diascalendario > 1080)//para el caso de indemnizacion de mas de 3 anios
            {
                if (diascalendario > 2160)//no se pagan mas de 6 anios
                {
                    diascalendario = 2160;
                }
                //por cada dia despues de los 3 anios, se multiplica por el factor equivalente a 20 dias por anio
                diasprestacion += Math.Round((diascalendario - 1080) * Convert.ToDouble(0.0555555555555556), 2) + Math.Round(1080 * Convert.ToDouble(0.083333333333), 2);
            }
            return diasprestacion;
        }
        public double CalcularDiasVacDescansadas(int codigo, DateTime fin, DateTime pini, DateTime pfin)
        {
            double descansadas = 0;
            DataTable desc = NPermisos.PermisosVacEmpleadoDetalleSel(codigo, fin, 1, 1).Tables[0];
            //DataRow[] detalle = desc.Select("(fechaini >= '" + pini + "' and fechaini <='" + pfin + "') or  (fechafin >= '" + pini + "' and fechafin <='" + pfin + "')");
            DataRow[] detalle = desc.Select("( '" + pini + "' <= fechafin and  '" + pfin + "' >= fechaini)");

            foreach (DataRow item in detalle)
            {
                DateTime fini = new DateTime(), ffin = new DateTime();

                if (Convert.ToDateTime(item[1]) >= pini)//fecha fin permiso menor al corte del mes
                {
                    fini = Convert.ToDateTime(item[1]);
                }
                else
                {
                    fini = pini;
                }
                if (Convert.ToDateTime(item[2]) <= pfin)
                {
                    ffin = Convert.ToDateTime(item[2]);
                }
                else
                {
                    ffin = pfin;
                }
                //factor cobrado en el historico
                double factor = Convert.ToDouble(item[5]) / Convert.ToDouble((Convert.ToDateTime(item[2]) - Convert.ToDateTime(item[1])).Days + 1);

                if (Convert.ToDouble(item[5]) > 0)
                {
                    descansadas += Convert.ToDouble((ffin - fini).Days + 1) * factor;
                }
                descansadas += Convert.ToDouble(item[6]) / 8.0;

            }

            return Math.Round(descansadas, 2);
        }
        public double CalcularDiasVacPagadas(int codigo, DateTime fin, int year, int month)
        {
            double pagadas = 0;
            DataTable pag = NPermisos.VacacionesPagadasxEmpDetalleSel(codigo, fin, 1).Tables[0];
            pagadas = pag.Rows.Cast<DataRow>().Where(row => (Convert.ToDateTime(row["feccerrado"])).Year == year && (Convert.ToDateTime(row["feccerrado"])).Month == month).Sum(r => Convert.ToDouble(r["diasVacacionesPagar"]));

            return pagadas;
        }
        public DataTable GetMonthYear(DateTime dtStart, DateTime dtEnd)
        {

            DataTable monthList = new DataTable();
            monthList.Columns.Add("col1");//mes año
            monthList.Columns.Add("col2");//mes
            monthList.Columns.Add("col3");//año
            monthList.Columns.Add("col4");//fecha completa

            //lista de meses desde inicio a fin
            DateTime dt = new DateTime();
            string fecha = "";
            for (dt = dtStart; dt <= dtEnd; dt = dt.AddMonths(1))
            {
                if (dt == dtStart)
                {
                    fecha = new DateTime(dt.Year, dt.Month, (DateTime.DaysInMonth(dt.Year, dt.Month))).ToShortDateString();
                }
                else
                {
                    fecha = dt.ToShortDateString();
                }
                monthList.Rows.Add(dt.ToString("MMMM yyyy"), dt.Month, dt.ToString("yyyy"), fecha);
            }
            if (true)
            {

            }
            //cuando finaliza poner la fecha fin dl ultimo mes
            DateTime ultmesini = new DateTime(dt.Year, dt.Month, 1);
            if (dtEnd >= ultmesini && dtEnd < dt)
            {
                monthList.Rows.Add(dtEnd.ToString("MMMM yyyy"), dtEnd.Month, dtEnd.ToString("yyyy"), dtEnd.ToShortDateString());
            }
            monthList.Rows[monthList.Rows.Count - 1]["col4"] = dtEnd.ToShortDateString();

            return monthList;
        }
        public DataTable CrearDetalleVacaciones(int codigo, DateTime dtStart, DateTime dtEnd)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("fecha");
            dt.Columns.Add("saldoini");
            dt.Columns.Add("acumuladas");
            dt.Columns.Add("descansadas");
            dt.Columns.Add("pagadas");
            dt.Columns.Add("saldoactual");
            dt.Columns.Add("observaciones");

            double saldoini = 0, descansadas = 0, pagadas = 0, saldoactual = 0, acumuladas = 0, acumuladast = 0, descansadast = 0, pagadast = 0;
            DateTime fecini = new DateTime();
            DateTime fecfin = new DateTime();

            DataTable monthList = GetMonthYear(dtStart, dtEnd);

            for (int i = 0; i < monthList.Rows.Count; i++)
            {
                int year = Convert.ToInt32(monthList.Rows[i]["col3"]);
                int month = Convert.ToInt32(monthList.Rows[i]["col2"]);
                string fecha = "";

                if (i == 0 || i == monthList.Rows.Count - 1)
                {
                    fecha = monthList.Rows[i]["col4"].ToString();
                }
                else
                {
                    fecha = monthList.Rows[i]["col1"].ToString();
                }

                if (i == 0)
                {
                    fecini = dtStart;
                    saldoini = 0;
                }
                else
                {
                    fecini = (new DateTime(year, month, 1));
                    saldoini = saldoactual;
                }

                if (i == monthList.Rows.Count - 1)
                {
                    fecfin = dtEnd;
                }
                else
                {
                    fecfin = (new DateTime(year, month, DateTime.DaysInMonth(year, month)));
                }

                acumuladas = CalcularDiasPrestacion(fecini, fecfin, 0, 1);
                descansadas = CalcularDiasVacDescansadas(codigo, dtEnd, fecini, fecfin);
                pagadas = CalcularDiasVacPagadas(codigo, dtEnd, year, month);

                saldoactual = saldoini + acumuladas - descansadas - pagadas;

                //Acumulados a la fecha
                acumuladast += acumuladas;
                descansadast += descansadas;
                pagadast += pagadas;

                dt.Rows.Add(fecha, saldoini.ToString("F2"), acumuladas.ToString("F2"), descansadas.ToString("F2"), pagadas.ToString("F2"), saldoactual.ToString("F2"), "");

            }
            dt.Rows.Add("Totales", "", acumuladast.ToString("F2"), descansadast.ToString("F2"), pagadast.ToString("F2"), saldoactual.ToString("F2"), "");

            return dt;
        }

        public decimal CalcularIRVacaciones(int codigo, decimal vacaciones, decimal inssvac)
        {
            decimal dimpuestos = default(decimal);
            Neg_IR NIR = new Neg_IR();
            int i = 0;
            if (i < Neg_IR.Globales.dtIRHistorico.Rows.Count)
            {
                for (; i < Neg_IR.Globales.dtIRHistorico.Rows.Count && codigo > Neg_IR.Globales.dtIRHistorico[i].codigo_empleado; i++)
                {
                }
                if (i < Neg_IR.Globales.dtIRHistorico.Rows.Count)
                {
                    if (codigo == Neg_IR.Globales.dtIRHistorico[i].codigo_empleado)
                    {
                        decimal ingresosdelperiodo = vacaciones - inssvac;
                        dimpuestos = NIR.ObtenerIR(Neg_IR.Globales.dtIRHistorico[i], ingresosdelperiodo, ocasional: true, 3);
                        i++;
                    }
                    else
                    {
                        dimpuestos = NIR.ObtenerIR(null, vacaciones - inssvac, ocasional: true, 3);
                    }
                }
            }
            return dimpuestos;
        }
        public DataTable CalcularDiasVacaciones(int codigo, DateTime fecingreso, int aplicaCorte, int idempresa)
        {
            decimal dvacacumuladas = 0, dvacdescansadas = 0, dsubsidios = 0, dvacpagadas = 0, saldovacaciones = 0;

            DataTable tvacaciones = new DataTable();
            tvacaciones.Columns.Add("vacacumuladas");
            tvacaciones.Columns.Add("vacdescansadas");
            tvacaciones.Columns.Add("dsubsidios");
            tvacaciones.Columns.Add("vacpagadas");
            tvacaciones.Columns.Add("saldovacaciones");

            //saldo de vacaciones = acumulado desde fecha_ingreso – dias permisos de vacaciones – días de vacaciones pagados en histórico

            //obtener vacaciones acumuladas
            dvacacumuladas = Convert.ToDecimal(CalcularDiasPrestacion(fecingreso, Globales.fechaR, 0, 0) < 0 ? 0 : CalcularDiasPrestacion(fecingreso, Globales.fechaR, 0, 0));
            //obtener vacaciones descansadas           
            dvacdescansadas = dp.PermisosVacEmpleadosxRangoSel(codigo, Globales.fechaR, aplicaCorte, idempresa);

            //obtener dias subsidios
            //DataTable sdet = NPermisos.SubsidiosEmpleadoDetalleSel(codigo,1);
            //dsubsidios = sdet.Rows.Cast<DataRow>().Sum(c=>Convert.ToDecimal(c["diasprestaciones"]));// Math.Round(NPermisos.SubsidiosEmpleadosxRangoSel(codigo, Globales.fechaR, aplicaCorte) * Convert.ToDecimal(0.083333333333), 2);

            //obtener vacaciones pagadas
            dvacpagadas = dp.obtenerVacacionesPagadasxEmp(codigo, Globales.fechaR, aplicaCorte, idempresa);

            saldovacaciones = dvacacumuladas - dvacdescansadas - dsubsidios - dvacpagadas;

            tvacaciones.Rows.Add(Math.Round(dvacacumuladas, 2), Math.Round(dvacdescansadas, 2), Math.Round(dsubsidios, 2), Math.Round(dvacpagadas, 2), Math.Round(saldovacaciones, 2));

            return tvacaciones;
        }

        public DataTable CalcularDiasVacacionesV14nal(int codigo, DateTime fecingreso, DateTime fechafinperiodo, int aplicaCorte, int idempresa)
        {
            decimal dvacacumuladas = 0, dvacdescansadas = 0, dsubsidios = 0, dvacpagadas = 0, saldovacaciones = 0;

            DataTable tvacaciones = new DataTable();
            tvacaciones.Columns.Add("vacacumuladas");
            tvacaciones.Columns.Add("vacdescansadas");
            tvacaciones.Columns.Add("dsubsidios");
            tvacaciones.Columns.Add("vacpagadas");
            tvacaciones.Columns.Add("saldovacaciones");

            //
            int bandera = 0;

            if (Globales.fechaR == Convert.ToDateTime("01/01/0001"))
            {
                Globales.fechaR = fechafinperiodo; // DateTime.Now;
                bandera = 2;//corte a la fecha actual
            }
            else
            {
                bandera = 1;//corte a la fecha de egreso   
            }

            //saldo de vacaciones = acumulado desde fecha_ingreso – dias permisos de vacaciones – días de vacaciones pagados en histórico

            //obtener vacaciones acumuladas
            dvacacumuladas = Convert.ToDecimal(CalcularDiasPrestacion(fecingreso, Globales.fechaR, 0, 0) < 0 ? 0 : CalcularDiasPrestacion(fecingreso, Globales.fechaR, 0, 0));
            //obtener vacaciones descansadas           
            dvacdescansadas = dp.PermisosVacEmpleadosxRangoSel(codigo, Globales.fechaR, aplicaCorte, idempresa);

            //obtener dias subsidios
            //DataTable sdet = NPermisos.SubsidiosEmpleadoDetalleSel(codigo,1);
            //dsubsidios = sdet.Rows.Cast<DataRow>().Sum(c=>Convert.ToDecimal(c["diasprestaciones"]));// Math.Round(NPermisos.SubsidiosEmpleadosxRangoSel(codigo, Globales.fechaR, aplicaCorte) * Convert.ToDecimal(0.083333333333), 2);

            //obtener vacaciones pagadas
            dvacpagadas = dp.obtenerVacacionesPagadasxEmp(codigo, Globales.fechaR, aplicaCorte, idempresa);

            saldovacaciones = dvacacumuladas - dvacdescansadas - dsubsidios - dvacpagadas;

            tvacaciones.Rows.Add(Math.Round(dvacacumuladas, 2), Math.Round(dvacdescansadas, 2), Math.Round(dsubsidios, 2), Math.Round(dvacpagadas, 2), Math.Round(saldovacaciones, 2));

            return tvacaciones;
        }

        public DataTable ObtenerSolvenciaEconomica(int codigo)
        {
            Dato_Informes Dato_Informes = new Dato_Informes();
            DataTable dt = Dato_Informes.DetalleSolvenciaxE(codigo, 1);
            DataSet dtCalculos = new DataSet();
            DataTable dtdatos = new DataTable();
            dtdatos.Columns.Add("Estado", typeof(string));
            dtdatos.Columns.Add("Codigo_empleado", typeof(int));
            dtdatos.Columns.Add("nombrecompleto", typeof(string));
            dtdatos.Columns.Add("nombre_depto", typeof(string));
            dtdatos.Columns.Add("Deduccion", typeof(decimal));
            dtdatos.Columns.Add("Adelanto", typeof(decimal));
            dtdatos.Columns.Add("Embargo", typeof(decimal));
            dtdatos.Columns.Add("Indemnizacion", typeof(decimal));
            dtdatos.Columns.Add("Aguinaldo", typeof(decimal));
            dtdatos.Columns.Add("Vacaciones", typeof(decimal));
            dtdatos.Columns.Add("TLiquidacion", typeof(decimal));
            decimal Indemnizacion = default(decimal);
            decimal Aguinaldo = default(decimal);
            decimal Vacaciones = default(decimal);
            decimal total = default(decimal);
            DataTable Empleados = (from DataRow c in dt.Rows
                                   group c by new
                                   {
                                       c2 = c["codigo_empleado"]
                                   } into grp
                                   select grp.First()).CopyToDataTable();
            DataTable tipoxEmp = (from DataRow c in dt.Rows
                                  group c by new
                                  {
                                      c2 = c["tipo"]
                                  } into grp
                                  select grp.First()).CopyToDataTable();
            decimal sumtotal = default(decimal);
            decimal sumporc = default(decimal);
            decimal irvac = default(decimal);
            Dato_Planilla Dato_Planilla = new Dato_Planilla();
            Dato_IR IR = new Dato_IR();
            Neg_Periodo neg_Periodo = new Neg_Periodo();
            DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
            DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);
            Neg_IR.Globales.dtIRHistorico = IR.ObtenerHistoricoIR(1, inicioano);
            foreach (DataRow emp in Empleados.Rows)
            {
                sumtotal = default(decimal);
                sumporc = default(decimal);
                Indemnizacion = default(decimal);
                Aguinaldo = default(decimal);
                Vacaciones = default(decimal);
                total = default(decimal);
                dtCalculos = ObtenerDatosLiquidacion(Convert.ToInt32(emp["codigo_empleado"]), 0, 0, 0.0, 0, 0,  false);
                if (dtCalculos != null && dtCalculos.Tables[1] != null && dtCalculos.Tables[1].Rows.Count > 0)
                {
                    Indemnizacion = Convert.ToDecimal(dtCalculos.Tables["Table2"].Rows[0]["Indemnizacion"]);
                    Aguinaldo = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Aguinaldo"]);
                    Vacaciones = Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["Vacaciones"]) - (Convert.ToDecimal(dtCalculos.Tables[1].Rows[0]["INSS"]) + irvac);
                    total = Indemnizacion + Aguinaldo + Vacaciones;
                }
                dtdatos.Rows.Add(emp["estado"].ToString(), Convert.ToInt32(emp["codigo_empleado"]), emp["nombrecompleto"].ToString(), emp["nombre_depto"].ToString(), 0, 0, 0, Indemnizacion, Aguinaldo, Vacaciones, total);
                foreach (DataRow tipo in tipoxEmp.Rows)
                {
                    sumtotal = (from r in dt.AsEnumerable()
                                where r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && !r.Field<bool>("porcentual") && !r.Field<bool>("recurrente")
                                select r).Sum((DataRow r) => r.Field<decimal>("debe"));
                    sumtotal += (from r in dt.AsEnumerable()
                                 where r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && !r.Field<bool>("porcentual") && r.Field<bool>("recurrente")
                                 select r).Sum((DataRow r) => r.Field<decimal>("valor"));
                    if (sumtotal < 0m)
                    {
                        sumtotal = default(decimal);
                    }
                    var Tipoporc = (from r in dt.AsEnumerable()
                                    where r.Field<int>("codigo_empleado") == Convert.ToInt32(emp["codigo_empleado"]) && r.Field<string>("tipo") == tipo["tipo"].ToString() && r.Field<bool>("porcentual")
                                    select new
                                    {
                                        tipo = r.Field<string>("tipo"),
                                        debe = r.Field<decimal>("debe"),
                                        valor = r.Field<decimal>("valor")
                                    }).ToList();
                    foreach (var Porcentaje in Tipoporc)
                    {
                        if (Porcentaje.debe == 0m)
                        {
                            sumporc += Porcentaje.valor * (Indemnizacion + Aguinaldo + Vacaciones) / 100m;
                        }
                        else
                        {
                            sumporc += Porcentaje.debe;
                        }
                    }
                    sumtotal += sumporc;
                    if (!(sumtotal > 0m))
                    {
                        continue;
                    }
                    dtdatos.AsEnumerable().ToList().ForEach(delegate (DataRow r)
                    {
                        if (r["Codigo_empleado"].ToString().Trim() == Convert.ToInt32(emp["codigo_empleado"]).ToString().Trim())
                        {
                            r[tipo["tipo"].ToString().Trim()] = sumtotal;
                        }
                    });
                }
            }
            return dtdatos;
        }
        public decimal GetMontoDisponible(string cod, int idempresa)
        {
            Dato_Liquidacion Dato_Liquidacion = new Dato_Liquidacion();
            Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
            Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
            decimal ingresoPromedio = default(decimal);
            decimal salproteccion = default(decimal);
            decimal disponible = default(decimal);
            decimal deducfijo = default(decimal);
            decimal deducporc = default(decimal);
            if (!int.TryParse(cod, out var codigo))
            {
                return -1m;
            }
            DataTable dtEmp = Dato_Liquidacion.spLiquidacionDatosEmp(codigo, 2, idempresa);
            if (dtEmp.Rows.Count > 0)
            {
                if (dtEmp.Rows[0]["idestado"].ToString().Trim() != "1")
                {
                    return 0m;
                }
                DataTable dtIngresos = Dato_Liquidacion.ObtenerIngresosxPlanillas(codigo, idempresa);
                if (dtIngresos.Rows.Count > 0)
                {
                    DataRow[] ingresocod = dtIngresos.Select("codigo_empleado=" + codigo);
                    ingresoPromedio = Convert.ToDecimal(ingresocod.Sum((DataRow row) => Convert.ToDecimal(row["ingresodif"]))) / (decimal)ingresocod.Count();
                    salproteccion = ingresoPromedio;
                    DataTable dtDeducciones = Neg_DevYDed.CalcularDetalleDeduccionesxEmp(codigo, 0, 0, ingresoPromedio, DateTime.Now, idempresa);
                    if (dtDeducciones.Rows.Count > 0)
                    {
                        deducfijo = (from r in dtDeducciones.AsEnumerable()
                                     where r["porcentual"].ToString() == "NO" && Convert.ToDecimal(r["Debe"]) > 0m && Convert.ToDecimal(r["valorCuotas"]) <= Convert.ToDecimal(r["Debe"])
                                     select r).Sum((DataRow r) => Convert.ToDecimal(r["valorCuotas"]));
                        deducfijo += (from r in dtDeducciones.AsEnumerable()
                                      where r["recurrente"].ToString() == "NO" && Convert.ToDecimal(r["Debe"]) > 0m && Convert.ToDecimal(r["valorCuotas"]) > Convert.ToDecimal(r["Debe"])
                                      select r).Sum((DataRow r) => Convert.ToDecimal(r["Debe"]));
                        deducporc = (from r in dtDeducciones.AsEnumerable()
                                     where r["recurrente"].ToString() == "SI" && r["porcentual"].ToString() == "SI" && Convert.ToDecimal(r["Debe"]) > 0m && Convert.ToDecimal(r["valorCuotas"]) <= Convert.ToDecimal(r["Debe"])
                                     select r).Sum((DataRow r) => Convert.ToDecimal(r["valorCuotas"])) * salproteccion / 100m;
                    }
                    disponible = ((salproteccion - deducfijo - deducporc > 1000m) ? 1000m : (salproteccion - deducfijo - deducporc));
                }
                else
                {
                    disponible = 500m;
                }
            }
            else
            {
                disponible = -1m;
            }
            return disponible;
        }
        public DataTable IngresosEgresosPlanillaxMes(DateTime fini, DateTime ffin, int todos, int depto1, int depto2,int xemp)
        {
            DataTable matrizliq = new DataTable();
            //Estructura de la tabla Meses.
            matrizliq.Columns.Add("MesNumero", typeof(int));
            matrizliq.Columns.Add("MesNombre", typeof(string));
            matrizliq.Columns.Add("codigo_depto", typeof(int));
            matrizliq.Columns.Add("nombre_depto", typeof(string));
            //si es por empleado 
            if (xemp == 2)
            {
                matrizliq.Columns.Add("codigo_empleado", typeof(int));
                matrizliq.Columns.Add("nombrecompleto", typeof(string));
                matrizliq.Columns.Add("cedula_identidad", typeof(string));
            }
            matrizliq.Columns.Add("id_tipo", typeof(int));
            matrizliq.Columns.Add("tipo", typeof(string));
            matrizliq.Columns.Add("nombrerubro", typeof(string));
            matrizliq.Columns.Add("valor", typeof(double));

            try
            {
                DataTable dmAnio;
                dmAnio = ObtenerMesesPrestaciones(fini, ffin);
                DataTable dm;

                dm = dmAnio;//.Rows.Cast<DataRow>().OrderByDescending(row => Convert.ToInt32(row["Anio"])).ThenByDescending(row => Convert.ToInt32(row["Mes"])).CopyToDataTable();

                //Obtengo las planillas registradas para codEmpl.      
                DataTable dpg = new DataTable();
                //DataTable dp = new DataTable();
                DataTable dpf = new DataTable();
                IUserDetail userDetail = UserDetailResolver.getUserDetail();

                dpg = Dato_Liquidacion.plnobtenerdetalleplanillasxemp(fini, ffin, depto1, depto2, todos, userDetail.getIDEmpresa());

                //if (todos == 0)//general o por departamentos
                //{
                //    dp = dpg.Select("codigo_depto>=" + depto1 + " and codigo_depto<=" + depto2).CopyToDataTable();
                //}
                //else
                //{
                //    dp = dpg;
                //}
                //agrupando por departamento ingreso y deducciones
                //DataTable rubro = Neg_DevYDed.plnObtenerRubros();//
                DataTable rubro = new DataTable();                            
                if (xemp == 2)//por empleado, solo los que retienen para reporte dgi
                {
                    rubro = dpg.Rows.Cast<DataRow>().Where(a => Convert.ToBoolean(a["aplicaINSS"]) == true).GroupBy(row => new { c2 = row["id_tipo"], c3 = row["id_tipo"], c4 = row["nombrerubro"] }).Select(grp => grp.First()).OrderBy(row => row["id_tipo"].ToString()).CopyToDataTable();
                }                
                else//si no es por empleado son todos los rubros.
                {
                    rubro = dpg.Rows.Cast<DataRow>().GroupBy(row => new { c2 = row["id_tipo"], c3 = row["id_tipo"], c4 = row["nombrerubro"] }).Select(grp => grp.First()).OrderBy(row => row["id_tipo"].ToString()).CopyToDataTable();
                }

                //departamentos ordenados
                DataTable sortedDT=new DataTable();

                if (xemp == 2)//por empleados
                {
                    sortedDT = dpg.Rows.Cast<DataRow>().GroupBy(row => new { c2 = row["codigo_empleado"] }).Select(grp => grp.First()).OrderBy(row => row["codigo_empleado"].ToString()).CopyToDataTable();
                }
                else if (xemp == 4)//por gerencia
                {
                    sortedDT = dpg.Rows.Cast<DataRow>().GroupBy(row => new { c2 = row["gerencia"] }).Select(grp => grp.First()).OrderBy(row => row["gerencia"].ToString()).CopyToDataTable();
                }
                else//departamentos
                {
                    DataTable ft = Neg_Catalogos.CargarProcesos().Tables[0];
                    DataView dv = ft.DefaultView;
                    dv.Sort = "codigo_depto asc";
                    sortedDT = dv.ToTable();
                }

                for (int i = 0; i < dm.Rows.Count; i++)
                {
                    int MesCompleto = Convert.ToInt32(dm.Rows[i]["mes"].ToString().Trim());
                    string MesCompletoName = dm.Rows[i]["nombreMes"].ToString().Trim();
                    int Anio = Convert.ToInt32(dm.Rows[i]["Anio"].ToString().Trim());
                    //int diasT = 0;
                    //diasT = Convert.ToInt32(dm.Rows[i]["Dia"].ToString().Trim());
                    int TdiasMes = Convert.ToInt32(dm.Rows[i]["TdiasMes"].ToString().Trim());//dias del mes segun calendario

                    DateTime SemanaInicio = DateTime.Now, SemanaFin = DateTime.Now;
                    //   double diassemana = 0, valorsemana = 0; q = 0;

                    DateTime fmesinicio = new DateTime(Anio, MesCompleto, 1);
                    DateTime fmesfin = new DateTime(Anio, MesCompleto, TdiasMes);
                    //variables para construir tabla de detalle de planillas por semanas (utilizada en pantalla de consultar pago por mes)
                    //  diassemana = 0; valorsemana = 0;

                    //solo registros correspondientes al mes
                    DataRow[] dp=null;
                    dp = dpg.Select("(fechaini >= '" + fmesinicio + "' and fechaini <='" + fmesfin + "') or  (fechafin >= '" + fmesinicio + "' and fechafin<='" + fmesfin + "')");

                    if (dp.Length>0)
                    {
                        dpf = dp.CopyToDataTable();
                        
                    }else
                    {
                        continue;
                    }
                    foreach (DataRow itemd in sortedDT.Rows)//se recorre cada departamento
                    {
                        foreach (DataRow itemr in rubro.Rows)//se recorre cada rubro 
                        {
                            double valor = 0;
                            int q = 0;
                            double valorsemana = 0;
                            DataRow[] drresult = null;
                            string condicion = "";

                            condicion = "id_tipo=" + itemr["id_tipo"].ToString() + " and nombrerubro='" + itemr["nombrerubro"].ToString() + "'";                            
                            
                            if (xemp == 2)//por empleados
                            {
                                condicion+= " and codigo_empleado=" + itemd["codigo_empleado"].ToString();
                            }
                            else if (xemp == 4)//por gerencia
                            {
                                condicion += " and gerencia='" + itemd["gerencia"].ToString()+"'";
                            }
                            else//por departamentos
                            {
                                condicion += " and codigo_depto=" + itemd["codigo_depto"].ToString();
                            }

                            drresult = dpf.Select(condicion);
                            if (drresult.Length > 0)
                            {
                                valor = 0; valorsemana = 0;
                                q = 0;
                                
                                while (q < drresult.Length)
                                {
                                    SemanaInicio = Convert.ToDateTime(drresult[q]["fechaini"].ToString().Trim());
                                    SemanaFin = Convert.ToDateTime(drresult[q]["fechafin"].ToString().Trim());

                                    //semana pertence al mismo mes y anio
                                    if ((SemanaInicio.Month == MesCompleto && SemanaInicio.Year == Anio) && (SemanaFin.Month == MesCompleto && SemanaFin.Year == Anio))
                                    {
                                        //diassemana = (SemanaFin - SemanaInicio).Days + 1;//7 dias   
                                        //******                            
                                        valorsemana = Convert.ToDouble(drresult[q]["valor"].ToString().Trim());
                                        valor += valorsemana;
                                    }
                                    else
                                    {
                                        // {
                                        if ((SemanaInicio.Month == MesCompleto && SemanaInicio.Year == Anio) || (SemanaFin.Month == MesCompleto && SemanaFin.Year == Anio))
                                        {
                                            //*****
                                            valorsemana = Convert.ToDouble(drresult[q]["valor"].ToString().Trim());
                                            int k = 0, j = 0;
                                            for (j = 0; j < 7; j++)
                                            {
                                                if (SemanaInicio.AddDays(j).Month == MesCompleto)
                                                {
                                                    k++;
                                                }
                                            }
                                            //diassemana = k;
                                            valor += ((valorsemana / 7) * k);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                    }
                                
                                    q++;
                                }
                                if (valor > 0)
                                {
                                    if (xemp == 2)//por empleados
                                    {
                                        matrizliq.Rows.Add(MesCompleto, MesCompletoName, Convert.ToInt32(itemd["codigo_depto"]), itemd["nombre_depto"], Convert.ToInt32(itemd["codigo_empleado"]), itemd["nombrecompleto"], itemd["cedula_identidad"], Convert.ToInt32(itemr["id_tipo"]), itemr["tipo"], itemr["nombrerubro"], valor.ToString("n2"));

                                    }
                                    else if (xemp == 4)//por gerencia
                                    {
                                        matrizliq.Rows.Add(MesCompleto, MesCompletoName, Convert.ToInt32(itemd["idgerencia"]), itemd["gerencia"], Convert.ToInt32(itemr["id_tipo"]), itemr["tipo"], itemr["nombrerubro"], valor.ToString("n2"));
                                    }
                                    else
                                    {
                                        matrizliq.Rows.Add(MesCompleto, MesCompletoName, Convert.ToInt32(itemd["codigo_depto"]), itemd["nombre_depto"], Convert.ToInt32(itemr["id_tipo"]), itemr["tipo"], itemr["nombrerubro"], valor.ToString("n2"));
                                    }
                                }
                            
                            }
                           
                        }
                    }

                }
                return matrizliq;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

