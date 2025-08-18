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
    public class Neg_Planilla
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Planilla Dato_Planilla = new Dato_Planilla();
        Dato_Factores Dato_Factores = new Dato_Factores();
        Neg_DevYDed IngryDeduc = new Neg_DevYDed();
        Neg_Comedor Neg_Comedor = new Neg_Comedor();
        dsPlanilla.dtDeduccionCuotaDataTable dtDeduccionCuota;
        dsPlanilla.dtIngrDeducDataTable dtIngrDeduc;
        public int id = 0;
        public int id_tipo = 0;
        public int codigo_empleado = 0;
        public int nsemana = 0;
        public int ultimoperiodo = 0;
        public int tipoingrdeduc = 0;
        public decimal valor = 0;
        public int porcentual = 0;
        public string modalidad = "";
        public int recurrente = 0;
        public int idprioridad = 0;
        public decimal debe = 0;
        public int pagopendiente = 0;
        public decimal ultimacuota = 0;
        public int deduccionibruto = 0;

        public decimal montosincancelar = 0;

        #endregion
        public class Globales
        {
            public int codigo;
        }

        public DataTable obtenerDetalleHorasLab(DateTime fechaini, DateTime fechafin, int CodEmp)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Planilla.obtenerDetalleHorasLab(fechaini, fechafin, CodEmp, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable ObtenerPermisosxEmpleadoR(DateTime fechaini, DateTime fechafin, int CodEmp)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Planilla.ObtenerPermisosxEmpleadoR(fechaini, fechafin, CodEmp, userDetail.getIDEmpresa());
            return ds;
        }

        public bool insertarHorasExtras(int codEmpleado, int periodo, int semana,int tipo, decimal horas, int tplanilla)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Planilla.insertarHorasExtras(codEmpleado, periodo, semana,tipo, horas, userDetail.getIDEmpresa(), tplanilla))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool insertarHorasExtrasxDia(int codEmpleado, int periodo,int tipoIngrDeduc, DateTime fecha, decimal horas,decimal tiempolibre,int depto_afecta, string usuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Planilla.insertarHorasExtrasxDia(codEmpleado, periodo, tipoIngrDeduc, fecha, horas, tiempolibre, depto_afecta, usuario, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool insertarIngDeducxDia(int codEmpleado, int periodo, int idtipo, int tipoingdeduc, DateTime fecha, decimal horas, decimal valor,decimal tiempolibre,int depto_afecta, string usuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Planilla.insertarIngDeducxDia(codEmpleado, periodo,idtipo,tipoingdeduc, fecha, horas,valor, tiempolibre, depto_afecta, usuario, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool EliminarIngDeducxDia(int codEmpleado, int periodo, int idtipo, int tipoingdeduc, DateTime fecha)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Planilla.EliminarIngDeducxDia(codEmpleado, periodo, idtipo, tipoingdeduc, fecha, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ActualizarIngDeducxDia(int codEmpleado, int periodo, int idtipo, int tipoingdeduc, DateTime fecha,string comentario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Planilla.ActualizarIngDeducxDia(codEmpleado,  periodo, idtipo, tipoingdeduc, fecha, comentario,userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool AplicarIngDeducxDia(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Planilla.AplicarIngDeducxDia(periodo, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

         public void GenerarProcesoPlanilla(int periodo, string user)
        {
            Neg_Periodo Neg_Periodo = new Neg_Periodo();
            Neg_DevYDed NDevyDed = new Neg_DevYDed();
            dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(periodo);
            int ciclo = 0;
            if (!dtPeriodo[0].consolidar && dtPeriodo[0].tplanilla == 1)
            {
                // si es catorcenal procesa cada semana
                ciclo = 2;
            }
            else
            {
                ciclo = 1;
            }
            Boolean fin = false;
            //configuracion deducciones fuera de planilla
            int tipodeduc = 22;
            decimal cuota = 90, total = 0;
            int recurrente = 1;
            int porcentual = 1;

            NDevyDed.AsignarIngresosEspecialesIns(periodo);

            for (int i = 0; i < ciclo; i++)
            {
                fin = ((ciclo == i + 1) ? true : false);
                string planilla = calculoIngresosDeducciones(dtPeriodo[0].nperiodo, i + 1, user, fin);
            }
        }

        public string calculoIngresosDeducciones(int periodo, int semana, string user, bool fin)
        {
            try
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                Neg_Periodo NPeriodo = new Neg_Periodo();
                Neg_DevYDed NDevyDed = new Neg_DevYDed();
                Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
                Neg_Informes NInformes = new Neg_Informes();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(periodo);
                if (dtPeriodo[0].cerrado == 1)
                {
                    return "";
                }
                DateTime fechainicio = DateTime.Now;
                DateTime fechafin = DateTime.Now;
                if (!dtPeriodo[0].consolidar && dtPeriodo[0].tplanilla == 1)
                {
                    if (semana == 1)
                    {
                        fechainicio = dtPeriodo[0].fechaini;
                        fechafin = dtPeriodo[0].fechafin;
                    }
                    else
                    {
                        fechainicio = dtPeriodo[0].fechaini2;
                        fechafin = dtPeriodo[0].fechafin2;
                    }
                }
                else
                {
                    fechainicio = dtPeriodo[0].fechaini;
                    fechafin = dtPeriodo[0].fechafin2;
                }
                dsPlanilla.dtIngDedDataTable dtIngresos = new dsPlanilla.dtIngDedDataTable();
                dsPlanilla.dtEgresosDataTable dtEgresos = new dsPlanilla.dtEgresosDataTable();
                NDevyDed.ProcesarHorasExtrasxPeriodo(periodo, semana, dtPeriodo[0].tplanilla, fechainicio, fechafin);
                Neg_Comedor.ProcesarDeduccionComedorxPeriodo(periodo, semana, fechainicio.ToShortDateString(), fechafin.ToShortDateString(), user);
                if (!dtPeriodo[0].consolidar)
                {
                    Dato_Planilla.EliminarPlanilla(periodo, semana, userDetail.getIDEmpresa());
                    dtIngresos = NDevyDed.IngresosxPeriodoSel(periodo, semana);
                    dtEgresos = NDevyDed.EgresosxPeriodoSel(periodo, semana);
                }
                else
                {
                    Dato_Planilla.EliminarPlanilla(periodo, userDetail.getIDEmpresa());
                    dtIngresos = NDevyDed.IngresosxPeriodoSelAll(periodo);
                    dtEgresos = NDevyDed.EgresosxPeriodoSelAll(periodo);
                }
                Neg_Empresas NEmpresas = new Neg_Empresas();
                dsPlanilla.dtEmpresaDataTable DetEmpresas = NEmpresas.ObtenerInfoDetEmpresas();
                Neg_IR NIR = new Neg_IR();
                DataTable df = new DataTable();
                Neg_Periodo neg_Periodo = new Neg_Periodo();
                DataTable periodoFiscal = neg_Periodo.PlnPeriodoFiscalSel();
                DateTime inicioano = Convert.ToDateTime(periodoFiscal.Rows[0]["fechaini"]);            
                df = Dato_Factores.obtenerFactor(userDetail.getIDEmpresa());
                decimal factorInss = Convert.ToDecimal(df.Rows[1]["factor"].ToString());
                decimal factorPatronal = Convert.ToDecimal(df.Rows[2]["factor"].ToString());
                decimal factorInatec = Convert.ToDecimal(df.Rows[3]["factor"].ToString());
                dsPlanilla.dtPlanillaDataTable dtPlanilla = new dsPlanilla.dtPlanillaDataTable();
                dtDeduccionCuota = new dsPlanilla.dtDeduccionCuotaDataTable();
                dtIngrDeduc = new dsPlanilla.dtIngrDeducDataTable();
                dsPlanilla.dtIRHistoricoDataTable dtIRHistorico = NIR.ObtenerHistoricoIR(inicioano);
                int q = 0;
                int e = 0;
                int k = 0;
                int j = 0;
                decimal ingresosgrabables = default(decimal);
                decimal ingresosnograbables = default(decimal);
                decimal egresoplanilla = default(decimal);
                decimal ingresosnogrvdeduc = default(decimal);
                decimal ingresosnocontar = default(decimal);
                decimal beneficio = default(decimal);
                decimal ingresomenosimpuesto = default(decimal);
                decimal ingresodisponible = default(decimal);
                int bandera = 0;
                decimal ingresovacaciones = default(decimal);
                decimal ingresosalario = default(decimal);
                decimal salariomensual = default(decimal);
                decimal salariopromedio = default(decimal);
                decimal vacdesc = default(decimal);
                decimal vacpag = default(decimal);
                decimal horastrabajadas = default(decimal);
                decimal horastr = default(decimal);
                decimal horascgs = default(decimal);
                decimal ingresohcgs = default(decimal);
                decimal ingresoht = default(decimal);
                DateTime fechadefault = new DateTime(1900, 1, 1, 0, 0, 0);
                DateTime fechagraba = DateTime.Now;
                Neg_Marca NMarca = new Neg_Marca();
                List<Neg_Empleados> Empleados = NMarca.ObtenerHT(periodo, semana, dtPeriodo[0].ubicacion, userDetail.getIDEmpresa());
                DataSet pasivo = new DataSet();
                bool existepasivo = false;
                for (int i = 0; i < Empleados.Count; i++)
                {
                    dsPlanilla.dtPlanillaRow NewPlanilla = dtPlanilla.NewdtPlanillaRow();
                    salariomensual = default(decimal);
                    ingresovacaciones = default(decimal);
                    salariopromedio = default(decimal);
                    existepasivo = false;
                    vacdesc = Convert.ToDecimal(Empleados[i].horasv);
                    if (DetEmpresas.Rows.Count > 0)
                    {
                        salariomensual = ((Empleados[i].moneda == Convert.ToInt32(DetEmpresas[0].moneda)) ? Empleados[i].salariomensual : (Empleados[i].salariomensual * dtPeriodo[0].factor));
                        if (DetEmpresas[0].promvac && vacdesc > 0m)
                        {
                            horastrabajadas = Convert.ToDecimal(Empleados[i].horasapagar) - vacdesc;
                            Neg_Liquidacion.Globales.fechaR = fechainicio.AddDays(-1.0);
                            if (Empleados[i].tiposalario != 1)
                            {
                                pasivo = Neg_Liquidacion.ObtenerDatosLiquidacion(Empleados[i].codigo_empleado, 0, 0, 0.0, 0, 0, pago: true);
                                if (pasivo != null && pasivo.Tables.Count > 0 && pasivo.Tables[0].Rows.Count > 0)
                                {
                                    existepasivo = true;
                                    salariopromedio = Convert.ToDecimal(pasivo.Tables[1].Rows[0]["salPromedioDia"]);
                                }
                            }
                            if (!existepasivo || Empleados[i].tiposalario == 1)
                            {
                                salariopromedio = salariomensual / 30m;
                            }
                            ingresovacaciones = vacdesc * (salariopromedio / 8m);
                        }
                        else
                        {
                            horastrabajadas = Convert.ToDecimal(Empleados[i].horasapagar);
                        }
                    }
                    horastrabajadas = ((horastrabajadas < 0m) ? 0m : horastrabajadas);
                    NewPlanilla.codigo_empleado = Empleados[i].codigo_empleado;
                    NewPlanilla.fechaini = fechainicio;
                    NewPlanilla.fechafin = fechafin;
                    NewPlanilla.messemana = fechafin.Month;
                    NewPlanilla.semana = semana;
                    NewPlanilla.nombre = Empleados[i].nombrecompleto;
                    NewPlanilla.periodo = periodo;
                    NewPlanilla.anio = fechafin.Year;
                    NewPlanilla.dimpuestos = 0m;
                    NewPlanilla.salariomensual = salariomensual;
                    NewPlanilla.horast = 0m;
                    NewPlanilla.ingresos = 0m;
                    NewPlanilla.egresos = 0m;
                    NewPlanilla.dsegurosocial = 0m;
                    NewPlanilla.neto = 0m;
                    NewPlanilla.tperiodo = dtPeriodo[0].tperiodo;
                    NewPlanilla.tplanilla = dtPeriodo[0].tplanilla;
                    NewPlanilla.fechagraba = fechagraba;
                    NewPlanilla.nombre_depto = Empleados[i].departamento;
                    NewPlanilla.usuariograba = user;
                    NewPlanilla.codigo_cargo = Empleados[i].codigo_cargo;
                    NewPlanilla.codigo_depto = Empleados[i].codigo_depto;
                    NewPlanilla.saldo_vacaciones = Empleados[i].saldo_vacaciones;
                    NewPlanilla.nombre_cargo = Empleados[i].cargo;
                    NewPlanilla.horast = horastrabajadas;
                    NewPlanilla.ingresosnograbables = 0m;
                    NewPlanilla.p = Empleados[i].p;
                    ingresosgrabables = default(decimal);
                    ingresosalario = default(decimal);
                    ingresosnograbables = default(decimal);
                    egresoplanilla = default(decimal);
                    ingresosnogrvdeduc = default(decimal);
                    ingresosnocontar = default(decimal);
                    montosincancelar = default(decimal);
                    beneficio = default(decimal);
                    ingresohcgs = default(decimal);
                    ingresoht = default(decimal);
                    if (Empleados[i].tipocontrato == 7)
                    {
                        ingresosgrabables = default(decimal);
                        ingresosalario = default(decimal);
                        ingresoht = default(decimal);
                        ingresohcgs = default(decimal);
                    }
                    else
                    {
                        ingresosalario = horastrabajadas * (salariomensual * 0.0041095890411m);
                        ingresosgrabables = ingresosalario + ingresovacaciones;
                        if (DetEmpresas[0].promvac && vacdesc > 0m)
                        {
                            dsPlanilla.dtIngrDeducRow newingrdeduc = dtIngrDeduc.NewdtIngrDeducRow();
                            newingrdeduc.id = 0;
                            newingrdeduc.id_tipo = 1;
                            newingrdeduc.codigo_empleado = Empleados[i].codigo_empleado;
                            newingrdeduc.nsemana = semana;
                            newingrdeduc.tipoingrdeduc = 17;
                            newingrdeduc.periodo = periodo;
                            newingrdeduc.valor = ingresovacaciones;
                            newingrdeduc.tiempo = vacdesc;
                            newingrdeduc.tplanilla = dtPeriodo[0].tplanilla;
                            dtIngrDeduc.AdddtIngrDeducRow(newingrdeduc);
                        }
                    }
                    NewPlanilla.salario = ingresosalario;
                    q = 0;
                    int banderav = 0;
                    for (; q < dtIngresos.Rows.Count; q++)
                    {
                        if (Empleados[i].codigo_empleado == dtIngresos[q].codigo_empleado && dtIngresos[q].id_tipo == 1)
                        {
                            if (dtIngresos[q].aplicainss && dtIngresos[q].aplicair)
                            {
                                ingresosgrabables += dtIngresos[q].valor;
                                continue;
                            }
                            ingresosnograbables += (dtIngresos[q].mostrarc ? dtIngresos[q].valor : 0m);
                            if (dtIngresos[q].aplicadeduc)
                            {
                                ingresosnogrvdeduc += dtIngresos[q].valor;
                            }
                            if (dtIngresos[q].regEspecial)
                            {
                                ingresosnocontar += dtIngresos[q].valor;
                            }
                        }
                        else if (Empleados[i].codigo_empleado < dtIngresos[q].codigo_empleado)
                        {
                            break;
                        }
                    }
                    NewPlanilla.ingresos = ingresosgrabables;
                    if (Empleados[i].tipocontrato == 1 || Empleados[i].tipocontrato == 5)
                    {
                        NewPlanilla.dsegurosocial = factorInss / 100m * ingresosgrabables;
                        NewPlanilla.dseguropatronal = ingresosgrabables * factorPatronal;
                    }
                    NewPlanilla.deducacion = ingresosgrabables * factorInatec;
                    NewPlanilla.ingresosnograbables = ingresosnograbables;
                    NewPlanilla.egresos += NewPlanilla.dsegurosocial;
                    k = 0;
                    //if (Empleados[i].codigo_empleado == 309811)
                    //{
                    //}
                    if (Empleados[i].tipocontrato == 1 || Empleados[i].tipocontrato == 5)
                    {
                        if (k < dtIRHistorico.Rows.Count)
                        {
                            // ESTABLECE EL FLUJO de codigo para empleado.
                            for (; k < dtIRHistorico.Rows.Count && Empleados[i].codigo_empleado > dtIRHistorico[k].codigo_empleado; k++)
                            {
                            }

                            //bool islog = false;
                            //if (Empleados[i].codigo_empleado == 868664)
                            //{
                            //    islog = true;
                            //}

                            if (k < dtIRHistorico.Rows.Count)
                            {
                                if (Empleados[i].codigo_empleado == dtIRHistorico[k].codigo_empleado)
                                {
                                    decimal ingresosdelperiodo = ingresosgrabables - NewPlanilla.dsegurosocial;
                                    //NewPlanilla.dimpuestos = NIR.ObtenerIR(dtIRHistorico[k], ingresosdelperiodo, ocasional: false, dtPeriodo[0].tperiodo);
                                    NewPlanilla.dimpuestos = NIR.ObtenerIR2025(dtIRHistorico[k], (ingresosdelperiodo- ingresovacaciones), ocasional: false, dtPeriodo[0].tperiodo, inicioano, Empleados[i].codigo_empleado, fechainicio, ingresovacaciones);
                                    k++;
                                }
                            }
                            else
                            {
                                decimal ingresosdelperiodo = ingresosgrabables - NewPlanilla.dsegurosocial;
                                //NewPlanilla.dimpuestos = NIR.ObtenerIR(null, ingresosgrabables - NewPlanilla.dsegurosocial, ocasional: false, dtPeriodo[0].tperiodo);
                                NewPlanilla.dimpuestos = NIR.ObtenerIR2025(null, (ingresosdelperiodo - ingresovacaciones), ocasional: false, dtPeriodo[0].tperiodo, inicioano, Empleados[i].codigo_empleado, fechainicio, ingresovacaciones);
                            }
                        }
                        else
                        {
                            decimal ingresosdelperiodo = ingresosgrabables - NewPlanilla.dsegurosocial;
                            //NewPlanilla.dimpuestos = NIR.ObtenerIR(null, ingresosgrabables - NewPlanilla.dsegurosocial, ocasional: false, dtPeriodo[0].tperiodo);
                            NewPlanilla.dimpuestos = NIR.ObtenerIR2025(null, (ingresosdelperiodo - ingresovacaciones), ocasional: false, dtPeriodo[0].tperiodo, inicioano, Empleados[i].codigo_empleado, fechainicio, ingresovacaciones);
                        }
                    }
                    else
                    {
                        decimal factorServicios = default(decimal);
                        factorServicios = ((Empleados[i].tipocontrato != 3) ? Convert.ToDecimal(df.Rows[5]["factor"].ToString()) : Convert.ToDecimal(df.Rows[4]["factor"].ToString()));
                        NewPlanilla.dimpuestos = ingresosgrabables * factorServicios;
                    }
                    NewPlanilla.egresos += NewPlanilla.dimpuestos;

                    int existe = NDevyDed.verificaDeduccionPrioridad(Empleados[i].codigo_empleado);

                    e = 0;
                    ingresomenosimpuesto = NewPlanilla.ingresos - NewPlanilla.egresos + ingresosnogrvdeduc;
                    ingresodisponible = ingresomenosimpuesto;
                    if (Empleados[i].estado != 1 || existe > 0)
                    {
                        ingresodisponible = ingresomenosimpuesto;
                    }
                    for (; e < dtEgresos.Rows.Count; e++)
                    {
                        if (Empleados[i].codigo_empleado == dtEgresos[e].codigo_empleado && dtEgresos[e].id_tipo != 1 && dtEgresos[e].deduccionibruto == 0)
                        {
                            Neg_Planilla deduccion = new Neg_Planilla();
                            deduccion.id = dtEgresos[e].id;
                            deduccion.id_tipo = dtEgresos[e].id_tipo;
                            deduccion.codigo_empleado = dtEgresos[e].codigo_empleado;
                            deduccion.nsemana = dtEgresos[e].nsemana;
                            deduccion.ultimoperiodo = dtEgresos[e].periodo;
                            deduccion.tipoingrdeduc = dtEgresos[e].tipoingrdeduc;
                            deduccion.valor = dtEgresos[e].valor;
                            deduccion.porcentual = dtEgresos[e].porcentual;
                            deduccion.modalidad = dtEgresos[e].modalidad;
                            deduccion.recurrente = dtEgresos[e].recurrente;
                            deduccion.idprioridad = dtEgresos[e].idprioridad;
                            deduccion.debe = dtEgresos[e].debe;
                            deduccion.pagopendiente = dtEgresos[e].pagopendiente;
                            deduccion.ultimacuota = dtEgresos[e].ultimacuota;
                            egresoplanilla += procesarDeducciones(deduccion, periodo, semana, Math.Round(ingresodisponible - egresoplanilla, 2), Math.Round(NewPlanilla.ingresos + ingresosnogrvdeduc - ingresosnocontar, 2), "g", 0, bandera, dtDeduccionCuota);
                        }
                        else if (Empleados[i].codigo_empleado < dtEgresos[e].codigo_empleado)
                        {
                            break;
                        }
                    }
                    NewPlanilla.egresos += egresoplanilla;
                    NewPlanilla.neto = NewPlanilla.ingresos - NewPlanilla.egresos + ingresosnograbables;
                    if ((NewPlanilla.ingresos > 0m || NewPlanilla.ingresosnograbables > 0m) && bandera == 0 && !NewPlanilla.p)
                    {
                        dtPlanilla.AdddtPlanillaRow(NewPlanilla);
                    }
                }
                Dato_Planilla D_Planilla = new Dato_Planilla();
                D_Planilla.PlnplanillasInsBulk(userDetail.getIDEmpresa(), dtPlanilla);
                RegistrarIngresoVacDesc(dtIngrDeduc, user);
                RegistrarDeduccionCuotaEC(dtDeduccionCuota, periodo, semana, fin);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "T";
        }
        //AGREGADO POR GRETHEL TERCERO 28/11/16
        public decimal procesarDeducciones(Neg_Planilla deduccion, int periodo, int semana, decimal ingresosdisponible, decimal ingreso, string proceso, int existe, int reproceso, dsPlanilla.dtDeduccionCuotaDataTable dtDeduccionCuota)
	{
		Neg_DevYDed NDevyDed = new Neg_DevYDed();
		int bandera = 0;
		try
		{
			decimal egreso = default(decimal);
			decimal nosaldo = default(decimal);
			decimal valor = default(decimal);
			if (deduccion.codigo_empleado == 310146)
			{
			}
			valor = ((deduccion.recurrente == 1 || deduccion.porcentual == 1 || (deduccion.debe > 0m && deduccion.valor <= deduccion.debe && (deduccion.ultimacuota == 0m || deduccion.ultimacuota == deduccion.valor || deduccion.ultimoperiodo < periodo))) ? deduccion.valor : ((deduccion.recurrente == 0 && deduccion.debe > 0m && deduccion.valor > deduccion.debe) ? deduccion.debe : ((deduccion.recurrente != 0 || !(deduccion.debe > 0m) || !(deduccion.ultimacuota < deduccion.valor)) ? deduccion.ultimacuota : (deduccion.valor - deduccion.ultimacuota))));
			if ((semana != 1 && deduccion.modalidad == "DU") || (semana == 1 && deduccion.id != 0 && deduccion.modalidad == "DU"))
			{
				return 0m;
			}
			if (Convert.ToDecimal(ingresosdisponible) > 0m)
			{
				if (deduccion.porcentual == 1)
				{
					decimal cuotaporc = Convert.ToDecimal(ingreso) * valor / 100m;
					if (deduccion.debe == 0m || cuotaporc <= deduccion.debe)
					{
						egreso = cuotaporc;
					}
					else if (deduccion.debe > 0m && cuotaporc > deduccion.debe)
					{
						egreso = deduccion.debe;
					}
				}
				else
				{
					egreso = valor;
				}
				if (egreso > ingresosdisponible)
				{
					nosaldo = egreso - ingresosdisponible;
					egreso = ingresosdisponible;
					if (deduccion.id > 0 && deduccion.modalidad != "DU" && deduccion.pagopendiente == 0)
					{
						if (ingresosdisponible < valor * 0.5m)
						{
							nosaldo = default(decimal);
							egreso = default(decimal);
						}
						else
						{
							egreso = valor * 0.5m;
							nosaldo = egreso;
						}
					}
				}
				if (nosaldo > 0m && semana != 1)
				{
					montosincancelar += nosaldo;
					bandera = 1;
				}
			}
			else if (valor > 0m && semana != 1)
			{
				montosincancelar += valor;
				bandera = 1;
			}
			dsPlanilla.dtDeduccionCuotaRow NewDeduccionCuota = dtDeduccionCuota.NewdtDeduccionCuotaRow();
			NewDeduccionCuota.id = deduccion.id;
			NewDeduccionCuota.id_tipo = deduccion.id_tipo;
			NewDeduccionCuota.codigo_empleado = deduccion.codigo_empleado;
			NewDeduccionCuota.tipoingrdeduc = deduccion.tipoingrdeduc;
			NewDeduccionCuota.periodo = periodo;
			NewDeduccionCuota.semana = deduccion.nsemana;
			NewDeduccionCuota.modalidad = deduccion.modalidad;
			NewDeduccionCuota.ingresodisponible = ingresosdisponible;
			NewDeduccionCuota.pagopendiente = deduccion.pagopendiente;
			NewDeduccionCuota.egreso = egreso;
			NewDeduccionCuota.saldo = nosaldo;
			NewDeduccionCuota.valor = valor;
			dtDeduccionCuota.AdddtDeduccionCuotaRow(NewDeduccionCuota);
			return egreso;
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public void RegistrarDeduccionCuotaEC(dsPlanilla.dtDeduccionCuotaDataTable dtDeduccionCuota, int periodo, int semana, bool fin)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		decimal montoaplicado = default(decimal);
		decimal montosaldo = default(decimal);
		int bandera = 0;
		try
		{
			Neg_Periodo NPeriodo = new Neg_Periodo();
			dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(periodo);
			bool aplicacuotaparc = false;
			decimal cuotapagar = default(decimal);
			decimal cuotaporc = default(decimal);
			for (int i = 0; i < dtDeduccionCuota.Rows.Count; i++)
			{
				if (dtDeduccionCuota[i].codigo_empleado == 310146 && fin)
				{
				}
				cuotapagar = dtDeduccionCuota[i].egreso;
				cuotaporc = default(decimal);
				aplicacuotaparc = false;
				if (dtPeriodo[0].tperiodo == 1 && fin && dtDeduccionCuota[i].saldo > 0m && dtDeduccionCuota[i].id > 0 && dtDeduccionCuota[i].modalidad != "DU" && dtDeduccionCuota[i].pagopendiente == 0)
				{
					if (!IngryDeduc.plnRetornarCuotaIncompleta(periodo, dtDeduccionCuota[i].id, dtDeduccionCuota[i].codigo_empleado, semana, dtDeduccionCuota[i].egreso))
					{
						throw new Exception("Hubo problemas al modificar deduccion aplicada por falta de ingreso");
					}
					if (!(dtDeduccionCuota[i].egreso >= dtDeduccionCuota[i].valor * 0.5m) || !(dtDeduccionCuota[i].egreso < dtDeduccionCuota[i].valor))
					{
						cuotaporc = default(decimal);
						aplicacuotaparc = false;
						continue;
					}
					cuotaporc = dtDeduccionCuota[i].valor * 0.5m;
					aplicacuotaparc = true;
				}
				if (dtDeduccionCuota[i].ingresodisponible > 0m || aplicacuotaparc)
				{
					if (dtDeduccionCuota[i].egreso > 0m && dtDeduccionCuota[i].egreso <= dtDeduccionCuota[i].ingresodisponible && dtDeduccionCuota[i].modalidad != "DU")
					{
						cuotapagar = (aplicacuotaparc ? cuotaporc : dtDeduccionCuota[i].egreso);
						if (!Dato_Planilla.DeduccionCuotaPlanillaEmpIns(dtDeduccionCuota[i].id, dtDeduccionCuota[i].id_tipo, dtDeduccionCuota[i].codigo_empleado, dtDeduccionCuota[i].tipoingrdeduc, periodo, semana, cuotapagar, userDetail.getIDEmpresa()))
						{
							throw new Exception("Hubo problemas al insertar cuota del empleado");
						}
						if (dtDeduccionCuota[i].modalidad == "DC" && !Dato_Planilla.ActualizarEstadoCuentaDeudaEmp(dtDeduccionCuota[i].id, dtDeduccionCuota[i].id_tipo, dtDeduccionCuota[i].codigo_empleado, dtDeduccionCuota[i].tipoingrdeduc, periodo, cuotapagar, 0, userDetail.getIDEmpresa()))
						{
							throw new Exception("Hubo problemas al deducir cuota en deuda del empleado");
						}
						if (aplicacuotaparc && !IngryDeduc.plnAplicarEgresoCuotaParc(periodo, dtDeduccionCuota[i].id, dtDeduccionCuota[i].codigo_empleado, semana))
						{
							throw new Exception("Hubo problemas al modificar deduccion aplicada por falta de ingreso");
						}
					}
					else if (dtDeduccionCuota[i].saldo > 0m && dtDeduccionCuota[i].modalidad == "DU")
					{
						montoaplicado = dtDeduccionCuota[i].egreso;
						montosaldo = dtDeduccionCuota[i].saldo;
						bandera = 1;
					}
				}
				else if (dtDeduccionCuota[i].valor > 0m && dtDeduccionCuota[i].modalidad == "DU")
				{
					montoaplicado = default(decimal);
					montosaldo = dtDeduccionCuota[i].valor;
					bandera = 1;
				}
				if (bandera == 1)
				{
					if (!IngryDeduc.DeduccioesEmpleadoEditar(dtDeduccionCuota[i].tipoingrdeduc, montoaplicado, dtDeduccionCuota[i].codigo_empleado, periodo, dtDeduccionCuota[i].semana, dtDeduccionCuota[i].id))
					{
						throw new Exception("Hubo problemas al modificar deduccion aplicada por falta de ingreso");
					}
					if (dtDeduccionCuota[i].id == 0 && !IngryDeduc.deduccionesSaldoPendEmpleadoIns(dtDeduccionCuota[i].codigo_empleado, periodo, dtDeduccionCuota[i].tipoingrdeduc, montosaldo, montosaldo, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortDateString(), "SYSTEM", 0, 0))
					{
						throw new Exception("Hubo problemas al registrar saldo deduccion por falta de ingreso");
					}
					bandera = 0;
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public void RegistrarIngresoVacDesc(dsPlanilla.dtIngrDeducDataTable dtIngrDeduc, string user)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		try
		{
			for (int i = 0; i < dtIngrDeduc.Rows.Count; i++)
			{
				if (!Dato_Planilla.PlnIngresoVacDescIns(dtIngrDeduc[i].id, dtIngrDeduc[i].id_tipo, dtIngrDeduc[i].codigo_empleado, dtIngrDeduc[i].nsemana, dtIngrDeduc[i].tipoingrdeduc, dtIngrDeduc[i].periodo, dtIngrDeduc[i].valor, dtIngrDeduc[i].tiempo, user, dtIngrDeduc[i].tplanilla, userDetail.getIDEmpresa()))
				{
					throw new Exception("Hubo problemas al insertar registro de vacacion del empleado");
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public void RegistrarDeduccionCuotaEC(dsPlanilla.dtDeduccionCuotaDataTable dtDeduccionCuota, string fecaut)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		try
		{
			for (int i = 0; i < dtDeduccionCuota.Rows.Count; i++)
			{
				if (dtDeduccionCuota[i].ingresodisponible > 0m && dtDeduccionCuota[i].egreso > 0m && dtDeduccionCuota[i].egreso <= dtDeduccionCuota[i].ingresodisponible && dtDeduccionCuota[i].modalidad != "DU")
				{
					if (!Dato_Planilla.DeduccionCuotaPrestacionesEmpIns(dtDeduccionCuota[i].id, dtDeduccionCuota[i].id_tipo, dtDeduccionCuota[i].codigo_empleado, dtDeduccionCuota[i].tipoingrdeduc, fecaut, dtDeduccionCuota[i].egreso, userDetail.getUser(), userDetail.getIDEmpresa()))
					{
						throw new Exception("Hubo problemas al insertar cuota del empleado");
					}
					if (dtDeduccionCuota[i].modalidad == "DC" && !Dato_Planilla.ActualizarEstadoCuentaDeudaEmp(dtDeduccionCuota[i].id, dtDeduccionCuota[i].id_tipo, dtDeduccionCuota[i].codigo_empleado, dtDeduccionCuota[i].tipoingrdeduc, 0, dtDeduccionCuota[i].egreso, 0, userDetail.getIDEmpresa()))
					{
						throw new Exception("Hubo problemas al deducir cuota en deuda del empleado");
					}
				}
			}
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	public DataSet obtenerDetalleDeduccionesPorEmpleado(int periodo, int semana, int codEmpleado, int tipoPlanilla)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		DataSet ds = new DataSet();
		return Dato_Planilla.obtenerDetalleDeduccionesPorEmpleado(periodo, semana, codEmpleado, userDetail.getIDEmpresa(), tipoPlanilla);
	}

	public DataSet obtenerDetalleIngresosPorEmpleado(int periodo, int semana, int codEmpleado, int tipoPlanilla)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		DataSet ds = new DataSet();
		return Dato_Planilla.obtenerDetalleIngresosPorEmpleado(periodo, semana, codEmpleado, userDetail.getIDEmpresa(), tipoPlanilla);
	}

	public DataSet obtenerDetalleMarcas(int periodo, int semana, int CodEmp)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		DataSet ds = new DataSet();
		return Dato_Planilla.obtenerDetalleMarcas(periodo, semana, CodEmp, userDetail.getIDEmpresa());
	}

	public bool EliminarPlanilla(int nperiodo, int semana)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		if (Dato_Planilla.EliminarPlanilla(nperiodo, semana, userDetail.getIDEmpresa()))
		{
			return true;
		}
		return false;
	}

	public bool EliminarPlanillaPorEmpleado(int nperiodo, int semana, int codigo)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		if (Dato_Planilla.EliminarPlanillaPorEmpleado(nperiodo, semana, codigo, userDetail.getIDEmpresa()))
		{
			return true;
		}
		return false;
	}

	public bool InsertarMarcaPorEmpleado(int codEmpl, DateTime fecha, string horaE, string horaS, string user)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		if (Dato_Planilla.InsertarMarcaPorEmpleado(codEmpl, fecha, horaE, horaS, userDetail.getIDEmpresa(), user))
		{
			return true;
		}
		return false;
	}

	public DataSet cargarTiposPlanilla()
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		DataSet ds = new DataSet();
		return Dato_Planilla.cargarTiposPlanilla(userDetail.getIDEmpresa());
	}

	public DataSet obtenerDetalleDeduccionesPorEmpleadoAll(int periodo, int codEmpleado, int tipoPlanilla)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		DataSet ds = new DataSet();
		return Dato_Planilla.obtenerDetalleDeduccionesPorEmpleadoAll(periodo, codEmpleado, tipoPlanilla, userDetail.getIDEmpresa());
	}

	public DataSet obtenerDetalleIngresosPorEmpleadoAll(int periodo, int codEmpleado, int tipoPlanilla)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		DataSet ds = new DataSet();
		return Dato_Planilla.obtenerDetalleIngresosPorEmpleadoAll(periodo, codEmpleado, tipoPlanilla, userDetail.getIDEmpresa());
	}

	public DataTable desgloceMoneda(DataTable dt)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		DataTable deno = Dato_Planilla.ObtenerDenominaciones(userDetail.getIDEmpresa());
		DataTable desgloce = new DataTable();
		desgloce.Columns.Add("codigo", typeof(int));
		desgloce.Columns.Add("denominacion", typeof(decimal));
		desgloce.Columns.Add("nbilletes", typeof(int));
		int nbillete = 0;
		decimal restante = default(decimal);
		foreach (DataRow dr4 in dt.Rows)
		{
			int codEmpleado = Convert.ToInt32(dr4["codigo"].ToString().ToUpper().Trim());
			decimal salarioNeto = Convert.ToDecimal(dr4["neto"]);
			restante = default(decimal);
			for (int i = 0; i < deno.Rows.Count; i++)
			{
				nbillete = Convert.ToInt32(Math.Truncate((salarioNeto - restante) / Convert.ToDecimal(deno.Rows[i]["denominacion"])));
				if (nbillete >= 1)
				{
					restante += (decimal)nbillete * Convert.ToDecimal(deno.Rows[i]["denominacion"]);
				}
				DataRow dx = desgloce.NewRow();
				dx["codigo"] = codEmpleado;
				dx["denominacion"] = Convert.ToDecimal(deno.Rows[i]["denominacion"].ToString());
				dx["nbilletes"] = nbillete;
				desgloce.Rows.Add(dx);
			}
		}
		return desgloce;
	}

	public void desgloceMoneda(DataTable dt, int periodo, int semana)
	{
		IUserDetail userDetail = UserDetailResolver.getUserDetail();
		foreach (DataRow dr4 in dt.Rows)
		{
			decimal restante1 = default(decimal);
			decimal restante6 = default(decimal);
			decimal restante7 = default(decimal);
			decimal restante8 = default(decimal);
			decimal restante9 = default(decimal);
			decimal restante10 = default(decimal);
			decimal restante11 = default(decimal);
			decimal restante12 = default(decimal);
			decimal restante13 = default(decimal);
			decimal restante2 = default(decimal);
			decimal restante3 = default(decimal);
			decimal restante4 = default(decimal);
			decimal restante5 = default(decimal);
			int codEmpleado = Convert.ToInt32(dr4["codigo_empleado"].ToString().ToUpper().Trim());
			decimal salarioNeto = Math.Truncate(Convert.ToDecimal(dr4["neto"].ToString().ToUpper().Trim()));
			decimal decimales = Convert.ToDecimal(dr4["neto"].ToString().ToUpper().Trim()) - salarioNeto;
			int d13 = Convert.ToInt32(Math.Truncate(salarioNeto / 500m));
			restante1 = ((d13 < 1) ? salarioNeto : ((decimal)(d13 * 500)));
			int d10 = Convert.ToInt32(Math.Truncate((salarioNeto - restante1) / 200m));
			if (d10 >= 1)
			{
				restante6 = d10 * 200;
				restante6 += restante1;
			}
			else
			{
				restante6 = restante1;
			}
			int d8 = Convert.ToInt32(Math.Truncate((salarioNeto - restante6) / 100m));
			if (d8 >= 1)
			{
				restante7 = d8 * 100;
				restante7 += restante6;
			}
			else
			{
				restante7 = restante6;
			}
			int d12 = Convert.ToInt32(Math.Truncate((salarioNeto - restante7) / 50m));
			if (d12 >= 1)
			{
				restante8 = d12 * 50;
				restante8 += restante7;
			}
			else
			{
				restante8 = restante7;
			}
			int d9 = Convert.ToInt32(Math.Truncate((salarioNeto - restante8) / 20m));
			if (d9 >= 1)
			{
				restante9 = d9 * 20;
				restante9 += restante8;
			}
			else
			{
				restante9 = restante8;
			}
			int d7 = Convert.ToInt32(Math.Truncate((salarioNeto - restante9) / 10m));
			if (d7 >= 1)
			{
				restante10 = d7 * 10;
				restante10 += restante9;
			}
			else
			{
				restante10 = restante9;
			}
			int d11 = Convert.ToInt32(Math.Truncate((salarioNeto - restante10) / 5m));
			if (d11 >= 1)
			{
				restante11 = d11 * 5;
				restante11 += restante10;
			}
			else
			{
				restante11 = restante10;
			}
			int d6 = Convert.ToInt32(Math.Truncate((salarioNeto - restante11) / 1m));
			if (d6 >= 1)
			{
				restante12 = d6;
				restante12 += restante11;
			}
			else
			{
				restante12 = restante11;
			}
			int d5 = Convert.ToInt32(Math.Truncate(decimales / 0.5m));
			restante13 = ((d5 > 1) ? decimales : ((decimal)d5 * 0.5m));
			int d4 = Convert.ToInt32(Math.Truncate((decimales - restante13) / 0.25m));
			if (d4 >= 1)
			{
				restante2 = (decimal)d4 * 0.25m;
				restante2 += restante13;
			}
			else
			{
				restante2 = restante13;
			}
			int d3 = Convert.ToInt32(Math.Truncate((decimales - restante2) / 0.10m));
			if (d3 >= 1)
			{
				restante3 = (decimal)d3 * 0.10m;
				restante3 += restante2;
			}
			else
			{
				restante3 = restante2;
			}
			int d2 = Convert.ToInt32(Math.Truncate((decimales - restante3) / 0.05m));
			if (d2 >= 1)
			{
				restante4 = (decimal)d2 * 0.05m;
				restante4 += restante3;
			}
			else
			{
				restante4 = restante3;
			}
			int d = Convert.ToInt32(Math.Truncate((decimales - restante4) / 0.01m));
			if (d >= 1)
			{
				restante5 = (decimal)d * 0.01m;
			}
			Dato_Planilla.distribuirDenominacionesMoneda(d13, d10, d8, d12, d9, d7, d11, d6, d5, d4, d3, d2, d, codEmpleado, periodo, semana, userDetail.getIDEmpresa());
		}
	}

    }
}
