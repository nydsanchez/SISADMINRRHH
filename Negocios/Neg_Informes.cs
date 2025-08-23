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

    public class Neg_Informes
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Informes di = new Dato_Informes();
        Dato_Factores Dato_Factores = new Dato_Factores();
        Dato_Planilla Dato_Planilla = new Dato_Planilla();
        Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        Notificacion noti = new Notificacion();

        #endregion
        #region Metodos
        public DataSet CargarCumpleañeros(int depto1, int depto2, string sexo, int mes)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarCumpleañeros(depto1, depto2, sexo, mes, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarTipoConcepto(int periodo, int semana, int IdTipo, int TipoIngDed)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarTipoConceptosSel(periodo, semana, IdTipo, TipoIngDed, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarDeptos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarDeptos(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarPlanillaTotal(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PlanillaSelTotal(periodo, semana, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarPlanillaConsolidada(int periodo, int semana, int todo, int pagoAdelantado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.PlanillaConsolidadaSel(periodo, semana, todo, pagoAdelantado, userDetail.getIDEmpresa());
        }
        public DataSet PlanillaResumenPeriodoCSel(int periodo, int periodo2, int todo, int pagoAdelantado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.PlanillaResumenPeriodoCSel(periodo, periodo2, todo, pagoAdelantado, userDetail.getIDEmpresa());
        }
        public DataSet PlnObtenerConsolidadoPlanillaxPeriodo(int periodo, int periodo2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.PlnObtenerConsolidadoPlanillaxPeriodo(periodo, periodo2, userDetail.getIDEmpresa());
        }
        public bool PlnConversionPeriodoConsolidado(int periodo, int periodo2, int operacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return di.PlnConversionPeriodoConsolidado(periodo, periodo2, operacion, userDetail.getIDEmpresa());            
        }
        public bool PlnPeriodosConsolidadosEqvIns(int periodo, int periodo2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return di.PlnPeriodosConsolidadosEqvIns(periodo, periodo2, userDetail.getIDEmpresa());
        }
        public DataSet PlanillaTotalSel(int periodo, int semana,int periodo2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PlanillaConsolidadaTotalSel(periodo, semana,periodo2, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet spTotalPlanillaxPeriodo(int periodo, int semana, int periodo2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.spTotalPlanillaxPeriodo(periodo, semana, periodo2, userDetail.getIDEmpresa());
        }

        public DataSet CargarEmpleado(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.EmpleadoSel(codigo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarLiquidacion(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.LiquidacionSel(codigo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarLiquidacionDetalle(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.LiquidacionDetalleSel(codigo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarLiquidacionEncabezado(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarLiquidacionEncabezado(codigo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet spMesesLiquidacion(int codigo, DateTime fechaliquidacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spMesesLiquidacion(codigo, fechaliquidacion, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet spLiquidacionDetallado(int codigo, DateTime fechaliquidacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spLiquidacionDetallado(codigo, fechaliquidacion, userDetail.getIDEmpresa());
            return ds;
        }


        public DataSet FiltrarEmpleado(int Codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.EmpleadoFiltrar(Codigo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet InsertarCodigo(int Codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.EmpleadoInsCodigo(Codigo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet spMasterTodos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spMasterTodos(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarEmpActivos(int filtro, int ubicacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.EmpleadoActivosSel(filtro, ubicacion, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable plnObtenerHistoricoIRrpt()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = di.plnObtenerHistoricoIRrpt(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet PlanillaVisionSel(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PlanillaVisionSel(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarPreplanilla(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PreplanillaSel(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarPrestamos(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarPrestamos(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarSemana1(int periodo, int tipo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.Semana1Sel(periodo, tipo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarSemana2(int periodo, int tipo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.Semana2Sel(periodo, tipo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarSemCaracteres(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.SemanaCaracteres(periodo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarSemEmp(int pEmpleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.Semana1SelEmp(pEmpleado, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarSemEmpIng(int pEmpleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.Semana1SelEmpIng(pEmpleado, userDetail.getIDEmpresa());
            return ds;
        }


        public DataSet CargarSem2Emp(int pEmpleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.Semana2SelEmp(pEmpleado, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarSem2EmpIng(int pEmpleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.Semana2SelEmpIng(pEmpleado, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargaCarnet()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargaCarnet(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarAtributos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargaAtributos(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet BuscarDedFijas(int pEmpleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.BuscarDeduccionesFijas(pEmpleado, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarFijas()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarDeduccionesFijas(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarPreplanillaIng(int periodo, int tipo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PreplanillaIngSel(periodo, tipo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarMarcasPlanilla(int periodo, int semana, int dept, int dept2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.MarcasSel(periodo, semana, dept, dept2, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarDatosEmpleado(int periodo, int tipo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarDatosEmp(periodo, tipo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarEmpleadoContrato(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.EmpleadoContratoSel(codigo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarIngresos(DateTime Inicio, DateTime Fin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.IngresosSel(Inicio, Fin, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarEgresos(DateTime Inicio, DateTime Fin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.EgresosSel(Inicio, Fin, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarPermisos(DateTime Inicio, DateTime Fin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PermisosSel(Inicio, Fin, userDetail.getIDEmpresa());
            return ds;
        }

        public void CargarMarcasHorasT(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            DataTable dp = new DataTable();
            DataTable df = new DataTable();
            DataTable dh = new DataTable();

            dh = Dato_Factores.obtenerTurnos(userDetail.getIDEmpresa());
            ds = di.MarcasHorasT(periodo, semana, userDetail.getIDEmpresa());
            dp = Dato_Planilla.ObtenerPermisosPeriodo(periodo, semana, userDetail.getIDEmpresa());
            decimal horasTurno = Convert.ToDecimal(dh.Rows[0]["horasTotalTurno"].ToString());
            decimal htMinimasDiurno = Convert.ToDecimal(dh.Rows[0]["horasMinimoSemana"].ToString());
            decimal horasCompletasSemana = Convert.ToDecimal(dh.Rows[0]["horasCompletasSemana"].ToString());
            foreach (DataRow dr in ds.Rows)
            {
                foreach (DataRow dt in dp.Rows)
                {
                    if (dr["Codigo"].ToString().ToUpper().Trim() == dt["codigo_empleado"].ToString().ToUpper().Trim()
                        && Convert.ToDecimal(dt["DiasVac"].ToString().ToUpper().Trim()) > 0 && Convert.ToInt32(dt["tipo"].ToString()) != 3
                         && Convert.ToDecimal(dr["HT"].ToString().ToUpper().Trim()) < 56)
                    {
                        decimal permisoVacac = Convert.ToDecimal(dt["DiasVac"].ToString().ToUpper().Trim());
                        permisoVacac = permisoVacac * horasTurno;
                        if (Convert.ToDecimal(dr["HT"].ToString().ToUpper().Trim()) + permisoVacac >= htMinimasDiurno)
                        {
                            decimal permiso = permisoVacac + 8;
                            if (permiso > horasCompletasSemana)
                                permiso = horasCompletasSemana;
                            if ((Convert.ToDecimal(dr["HT"].ToString().ToUpper().Trim()) + permiso) > horasCompletasSemana)
                                permiso = horasCompletasSemana - Convert.ToDecimal(dr["HT"].ToString().ToUpper().Trim());

                            di.InsertarMarcasMenores(Convert.ToInt32(dr["Codigo"].ToString().ToUpper().Trim()), dr["Nombre"].ToString().ToUpper().Trim(),
                            Convert.ToInt32(dr["periodo"].ToString().ToUpper().Trim()), Convert.ToInt32(dr["semana"].ToString().ToUpper().Trim()), (Convert.ToDecimal(dr["HT"].ToString().ToUpper().Trim()) + permiso), dr["nombre_depto"].ToString().ToUpper().Trim(), userDetail.getIDEmpresa());
                        }

                        else
                        {
                            decimal permiso = permisoVacac;
                            di.InsertarMarcasMenores(Convert.ToInt32(dr["Codigo"].ToString().ToUpper().Trim()), dr["Nombre"].ToString().ToUpper().Trim(),
                          Convert.ToInt32(dr["periodo"].ToString().ToUpper().Trim()), Convert.ToInt32(dr["semana"].ToString().ToUpper().Trim()), (Convert.ToDecimal(dr["HT"].ToString().ToUpper().Trim()) + permiso), dr["nombre_depto"].ToString().ToUpper().Trim(), userDetail.getIDEmpresa());
                        }

                    }
                }

            }

            obtenerMarcas(periodo, semana);

        }

        public DataTable obtenerMarcas(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable df = new DataTable();
            return df = di.obtenerMarcasMenores(periodo, semana, userDetail.getIDEmpresa());
        }

        public DataSet CargarConceptos(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ConceptosSel(periodo, semana, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarDenominaciones(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarEmpDenomicaciones(codigo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarPlanillaNegativos(int periodo, int semana, int tipoPlanilla)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PlanillaSelNegativos(periodo, semana, userDetail.getIDEmpresa(), tipoPlanilla);
            return ds;
        }
        public DataSet CargarPeriodo(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PeriodoFechas(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet LimpiarEmpTemporal()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.LimpiarTemporal(userDetail.getIDEmpresa());
            return ds;
        }
        #endregion

        public DataSet spDeducEspecialesSel(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spDeducEspecialesSel(codigo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet PeriodoDeptos(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PeriodoDepartamentos(codigo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet ObtenerEstructuraComprobantePago(string periodo, int periodo2, int semana2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.ObtenerEstructuraComprobantePago(periodo, periodo2, semana2, userDetail.getIDEmpresa());
        }
        public DataSet ObtenerEstructuraComprobanteIncentivo(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ObtenerEstructuraComprobanteIncentivo(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable PlnIncentivoPendPagarxPeriodoSel(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = di.PlnIncentivoPendPagarxPeriodoSel(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet ObtenerEncComprobantePago(string periodoBase, string periodo, int codigo, int semana, int semana2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.ObtenerEncComprobantePago(periodoBase, periodo, codigo, semana, semana2, userDetail.getIDEmpresa());
        }

        public DataSet ObtenerEncComprobanteIncentivo(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ObtenerEncComprobanteIncentivo(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet ObtenerEncComprobantePrestacion(int periodo, int codigo, int tperiodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ObtenerEncComprobantePrestacion(periodo, codigo, tperiodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet ObtenerEmpleadosPlanillaV14nal(string periodo, string periodo2, DateTime fechaini, DateTime fechafin, string depto, string codigo, bool all, bool efectivo, int filtroemail, bool periodoConsolida, bool AllEmpleados)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.ObtenerEmpleadosPlanillaV14nal(periodo, periodo2, fechaini, fechafin, depto, codigo, all, efectivo, userDetail.getIDEmpresa(), filtroemail, periodoConsolida, AllEmpleados);
        }

        public DataSet ObtenerEmpleadosPlanilla(string periodo, string periodo2, DateTime fechaini, DateTime fechafin, string depto, string codigo, bool all, bool efectivo, int filtroemail, bool periodoConsolida, bool AllEmpleados)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.ObtenerEmpleadosPlanilla(periodo, periodo2, fechaini, fechafin, depto, codigo, all, efectivo, userDetail.getIDEmpresa(), filtroemail, periodoConsolida);
        }

        public DataSet ObtenerEmpleadosPlanilla14nal(string periodo, string periodo2, DateTime fechaini, DateTime fechafin, string depto, string codigo, bool all, bool efectivo, int filtroemail, bool periodoConsolida, bool AllEmpleados)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.ObtenerEmpleadosPlanillaV14nal(periodo, periodo2, fechaini, fechafin, depto, codigo, all, efectivo, userDetail.getIDEmpresa(), filtroemail, periodoConsolida, AllEmpleados);
        }

        public DataTable PlnPeriodosConsolidadosEqvSel(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            return di.PlnPeriodosConsolidadosEqvSel(periodo, userDetail.getIDEmpresa());
        }
        public DataSet ObtenerEmpleadosPlanillaIncentivo(string periodo, string depto, string codigo, bool all)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ObtenerEmpleadosPlanillaIncentivo(periodo, depto, codigo, all, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet spLiquidacionDetalladoSel(DateTime Inicio, DateTime Fin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spLiquidacionDetalladoSel(Inicio, Fin, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet PermisosSel()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spTipoPermisoSel(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarPermisos(DateTime Inicio, DateTime Fin, int tipo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PermisosSel(Inicio, Fin, tipo, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet generarInformePersonasSinMarcaEntrada(DateTime fecha)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.generarInformePersonasSinMarcaEntrada(fecha, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarMarcasAusentes(DateTime inicio, int dept, int dept2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.MarcasAusentesSel(inicio, dept, dept2, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet spLiquidacionPendiente(int codigo, DateTime fechaliquidacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spLiquidacionPendiente(codigo, fechaliquidacion, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet spLiquidacionPendientesDeduc(int codigo, DateTime fechaliquidacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spLiquidacionPendientesDeduc(codigo, fechaliquidacion, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet plnDeduccionesPendPagoLiqSel(int codigo, DateTime fechaliquidacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.plnDeduccionesPendPagoLiqSel(codigo, fechaliquidacion, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarFotoDepto(int depto1, int depto2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.CargarFotoDepto(depto1, depto2, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet spSaldoVacacionesSel(int depto1, int depto2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spSaldoVacacionesSel(depto1, depto2, userDetail.getIDEmpresa());
            return ds;
        }

        public bool spObtenerMarcasBioAdmin_BDRRHH(DateTime Fecha)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (di.spObtenerMarcasBioAdmin_BDRRHH(Fecha, Convert.ToInt32(userDetail.getIDEmpresa())))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public DataSet spListarMarcas(DateTime FechaInicio, DateTime FechaFin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spListarMarcas(FechaInicio, FechaFin, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet obtenerPlanillaVacaciones(int periodo, int codigo, string fecini, string fecfin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.obtenerPlanillaVacaciones(periodo, codigo, fecini, fecfin, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable obtenerPlanillaAguinaldo(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = di.obtenerPlanillaAguinaldo(periodo,  userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable ObtenerDetalleIngresoMesAguinaldo(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = di.ObtenerDetalleIngresoMesAguinaldo(periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet PrestamosConsultarDetallexEmp(int codigo_empleado, int iddeduc, int mostrarcuotas, int pgpend)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PrestamosConsultarDetallexEmp(codigo_empleado, userDetail.getIDEmpresa(), iddeduc, mostrarcuotas, pgpend);
            return ds;
        }

        public DataSet BDRRHHDevolventes(int periodo, int deduccion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.BDRRHHDevolventes(periodo, userDetail.getIDEmpresa(), deduccion);
            return ds;
        }
        public DataSet spEmpleadosActivosxDepto(int depto1, int depto2)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spEmpleadosActivosxDepto(depto1, depto2, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet spEmpleadosActivosHistoricoSel(DateTime fechacorte)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.spEmpleadosActivosHistoricoSel(fechacorte, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet ObtenerEmpIndemnizacionMes(int mes, int anio)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ObtenerEmpIndemnizacionMes(mes, anio, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet ObtenerEmpLiqAntiguedad(int anio)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ObtenerEmpLiqAntiguedad(anio, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet PlnObtenerEmpleadosPlanillaMes(DateTime fechacorte)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PlnObtenerEmpleadosPlanillaMes(fechacorte, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet ObtenerEmpleadosEstatusEspecial()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.ObtenerEmpleadosEstatusEspecial(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet PrestamosConsultarDetalleCxEmp(int codigo_empleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.PrestamosConsultarDetalleCxEmp(codigo_empleado, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable spMarcasSelxFecha(DateTime FechaInicio, DateTime FechaFin, int filtro, int ubic, int depto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = di.spMarcasSelxFecha(FechaInicio, FechaFin, filtro, ubic, depto, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable MasterPComedor()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = di.MasterPComedor(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet AdelantosEspecialesDetalle(int codigo_empleado, int iddeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = di.AdelantosEspecialesDetalle(codigo_empleado, userDetail.getIDEmpresa(), iddeduc);
            return ds;
        }
        public DataTable DetalleSolvenciaxE(int codigo_empleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = di.DetalleSolvenciaxE(codigo_empleado, userDetail.getIDEmpresa());
            return ds;
        }
        string html = "", htmlv="", htmle = "", htmltmp = "";
        decimal ingresote = 0, egresote = 0, ingresosem = 0, egresosem = 0, horasbv=0;
        int d500 = 0, d200 = 0, d100 = 0, d50 = 0, d20 = 0, d10 = 0, d5 = 0, d1 = 0, d05 = 0, d025 = 0, d010 = 0, d005 = 0, d001 = 0;

        //metodos de generacion de comprobantes
        public string GenerarComprobantePeriodoPdf(DataTable dtEmpleadospln, string nperiodo, int periodo2, int semana2, int tipoplanilla, int tperiodo, int filtroemail, string encabezado, bool periodoConsolida)
        {
            try
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (dtEmpleadospln.Rows.Count > 0)
                {
                    DataTable emailTable = new DataTable();
                    emailTable.Columns.Add("correo");
                    emailTable.Columns.Add("adjunto");
                    DataSet dsComprobante = ObtenerEstructuraComprobantePago(nperiodo, periodo2, semana2);
                    DataTable dtdetalle = dsComprobante.Tables[0];
                    DataSet dsEncComprobante = new DataSet();
                    DataTable dtencabezado = new DataTable();
                    DataSet ds = null;
                    ds = CargarIngresoPeriodoIBruto(int.Parse(nperiodo), periodo2, 0);
                    string periodo3 = "";
                    string semana3 = "";
                    string Ccodigo = "";
                    string vacaciones = "";
                    string saldopr = "";
                    string montopr = "";
                    string ahorro = "";
                    string fechaini = "";
                    string fechafin = "";
                    string empresa = "";
                    string Cnombre = "";
                    string Ccuenta = "";
                    string Cdepto = "";
                    string Cimpresion = "";
                    string CSeguro = "";
                    string CCedula = "";
                    string CCargo = "";
                    string email = "";
                    string asunto = "";
                    DateTime fecha_ingreso = default(DateTime);

                    // TODO:VHPO
                    Neg_Periodo NPeriodo = new Neg_Periodo();
                    dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(Convert.ToInt32(nperiodo));
                    DateTime ini = dtPeriodo[0].fechaini;
                    DateTime fin = dtPeriodo[0].fechafin2;

                    DateTime ini2 = dtPeriodo[0].fechaini;
                    DateTime fin2 = dtPeriodo[0].fechafin2;

                    DataTable viaticosS1 = ObtenerPersonalPagoViatico(int.Parse(nperiodo), 1,  ini,  fin, 1, 0, "", dtEmpleadospln);
                    DataTable viaticosS2 = ObtenerPersonalPagoViatico(periodo2           , 1, ini2, fin2, 1, 0, "", dtEmpleadospln);

             
                    DataRow[] existeviaticopS1 = null;
                    DataRow[] existeviaticopS2 = null;

                    bool existeVS1 = false;
                    bool existeVS2 = false;

                    DataTable viaticopersonaS1 = new DataTable();
                    DataTable viaticopersonaS2 = new DataTable();

                    for (int i = 0; i < dtEmpleadospln.Rows.Count; i++)
                    {
                        d500 = 0;
                        d200 = 0;
                        d100 = 0;
                        d50 = 0;
                        d20 = 0;
                        d10 = 0;
                        d5 = 0;
                        d1 = 0;
                        d05 = 0;
                        d025 = 0;
                        d010 = 0;
                        d005 = 0;
                        d001 = 0;
                        ingresote = default(decimal);
                        egresote = default(decimal);
                        horasbv = default(decimal);
                        htmle = "";
                        htmltmp = "";
                        fechaini = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechaini"]).ToShortDateString();
                        fechafin = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechafin"]).ToShortDateString();
                        fecha_ingreso = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fecha_ingreso"]);
                        Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechafin"]);
                        email = dtEmpleadospln.Rows[i]["email"].ToString();
                        empresa = dtEmpleadospln.Rows[i]["empresa"].ToString();
                        periodo3 = dtEmpleadospln.Rows[i]["periodo"].ToString();
                        Ccodigo = dtEmpleadospln.Rows[i]["codigo"].ToString();
                        Cnombre = dtEmpleadospln.Rows[i]["nombre"].ToString();
                        Ccuenta = dtEmpleadospln.Rows[i]["cuenta"].ToString();
                        Cdepto = dtEmpleadospln.Rows[i]["depto"].ToString();
                        CSeguro = dtEmpleadospln.Rows[i]["numero_seguro"].ToString();
                        CCedula = dtEmpleadospln.Rows[i]["cedula_identidad"].ToString();
                        CCargo = dtEmpleadospln.Rows[i]["nombre_cargo"].ToString();
                        Cimpresion = DateTime.Now.Date.ToString("dd/MM/yyyy");


                        DataTable Datos = Neg_Liquidacion.CalcularDiasVacaciones(Convert.ToInt32(Ccodigo), fecha_ingreso, 1, userDetail.getIDEmpresa());

                        existeviaticopS1 = (from c in viaticosS1.AsEnumerable()
                                          where c.Field<int>("codigo_empleado") == int.Parse(Ccodigo)
                                          select c).ToArray();

                        existeviaticopS2 = (from c in viaticosS2.AsEnumerable()
                                            where c.Field<int>("codigo_empleado") == int.Parse(Ccodigo)
                                            select c).ToArray();

                        if (existeviaticopS1.Length != 0)
                        {
                            existeVS1 = true;
                            viaticopersonaS1 = existeviaticopS1.CopyToDataTable();
                        }
                        else
                        {
                            existeVS1 = false;
                            viaticopersonaS1 = new DataTable();
                        }

                        if (existeviaticopS2.Length != 0)
                        {
                            existeVS2 = true;
                            viaticopersonaS2 = existeviaticopS2.CopyToDataTable();
                        }
                        else
                        {
                            existeVS2 = false;
                            viaticopersonaS2 = new DataTable();
                        }

                        if (Datos != null)
                        {
                            vacaciones = Datos.Rows[0]["saldovacaciones"].ToString();
                        }

                        int Limite = 2;
                        Limite = ((tipoplanilla != 1) ? 1 : 2);
                        int bandera = 0;
                        int pagosemana = 0;
                        int semanalimite = 0;
                        for (int j = 0; j < Limite; j++)
                        {
                            if (tipoplanilla != 2 && tperiodo == 1)
                            {
                                semanalimite = j + 1;
                            }
                            else if (tipoplanilla == 2 && tperiodo == 1)
                            {
                                semanalimite = 0;
                                j++;
                            }
                            dsEncComprobante = ((!periodoConsolida || semanalimite != 2) ? ObtenerEncComprobantePago(periodo3, periodo3, Convert.ToInt32(dtEmpleadospln.Rows[i]["codigo"]), semanalimite, semanalimite) : ObtenerEncComprobantePago(periodo3, periodo2.ToString(), Convert.ToInt32(dtEmpleadospln.Rows[i]["codigo"]), semanalimite, 1));
                            if (dsEncComprobante.Tables.Count > 0 && dsComprobante.Tables[0].Rows.Count > 0)
                            {
                                dtencabezado = dsEncComprobante.Tables[0];
                            }
                            ingresosem = default(decimal);
                            egresosem = default(decimal);
                            foreach (DataRow drowS1 in dtencabezado.Rows)
                            {
                                semana3 = drowS1["nsemana"].ToString();
                                string salario = drowS1["salario"].ToString();
                                string horast = drowS1["horast"].ToString();
                                string extra = drowS1["extra"].ToString();
                                string horase = drowS1["horase"].ToString();
                                horasbv += Convert.ToDecimal(drowS1["horasbv"].ToString());
                                string bonovariable = drowS1["bonovariable"].ToString();
                                saldopr = drowS1["saldo"].ToString();
                                montopr = drowS1["totalpagar"].ToString();
                                ahorro = drowS1["ahorro"].ToString();
                                ingresote += ((salario != "") ? Convert.ToDecimal(salario) : 0m);
                                ingresosem += ((salario != "") ? Convert.ToDecimal(salario) : 0m);
                                string Lcodigo = "Empleado:";
                                string Lcuenta = "Cuenta:";
                                string Ldepto = "Depto:";
                                string LHora = "HorasT:";
                                string LSalario = "Salario:";
                                string LImpresion = "Impresion:";
                                string LHEx = "HorasE:";
                                string LSEx = "PagoE:";
                                string LSeguro = "Inss:";
                                string LCedula = "Cédula:";
                                string LCargo = "Cargo:";
                                char pad = ' ';
                                Lcodigo.PadRight(8, pad);
                                Lcuenta.PadRight(8, pad);
                                Ldepto.PadRight(8, pad);
                                LHora.PadRight(8, pad);
                                LSalario.PadRight(8, pad);
                                LImpresion.PadRight(8, pad);
                                LHEx.PadRight(8, pad);
                                LSEx.PadRight(8, pad);
                                LSeguro.PadRight(8, pad);
                                LCedula.PadRight(8, pad);
                                LCargo.PadRight(8, pad);
                                if (bandera == 0)
                                {
                                    bandera = 1;
                                    asunto = "Colilla de pago del periodo  " + encabezado + ", del " + fechaini + " al " + fechafin;
                                    htmltmp = htmltmp + "<font size='24px'; align='center'; color=\"#000000\"><b><i>" + empresa + "</i></b></font><br/><font size='24px'; align='center'; color=\"#000000\"><b><i>" + asunto + "</i></b></font><br/><font size='4px'><table border='0' cellpadding='0' cellspacing='0'><tr><td width='30%'>" + Ldepto + "&nbsp;" + Cdepto + "</td><td width='50%'>" + Lcodigo + "&nbsp;" + Ccodigo + " - " + Cnombre + "</td><td width='20%'>" + Lcuenta + "&nbsp;" + Ccuenta + "</td></tr><tr><td width='30%'>" + LCedula + "&nbsp;" + CCedula + "&nbsp;-&nbsp;" + LSeguro + "&nbsp;" + CSeguro + "</td><td width='50%'>" + LCargo + "&nbsp;" + CCargo + "</td><td width='20%'>" + LImpresion + "&nbsp;" + Cimpresion + "</td></tr><tr><td width='30%' color='#FFFFFF'>_</td><td width='50%' color='#FFFFFF'>_</td><td width='20%' color='#FFFFFF'>_</td></tr></table></font>";
                                }
                                pagosemana++;
                                htmltmp = htmltmp + "<font size='4px'><table border='0' cellpadding='0' cellspacing='0'><tr><td width='30%'><b>Semana&nbsp;" + semana3 + "</b>&nbsp;&nbsp;" + LHora + "&nbsp;" + horast + "&nbsp;&nbsp;" + LSalario + "&nbsp;" + salario + "</td><td width='50%'>" + ((horase != "") ? LHEx : "") + "&nbsp;" + horase + "</td><td width='20%'>&nbsp;</td></tr></table></font>";
                                CrearTablaSem(dtdetalle, Ccodigo, semana3, tipoplanilla, tperiodo, colillaYviatico: false);
                            }
                        }
                        if (dsEncComprobante.Tables.Count <= 0 || dsComprobante.Tables[0].Rows.Count <= 0)
                        {
                            continue;
                        }
                        decimal proteccion = default(decimal);
                        decimal bono = default(decimal);
                        decimal otros = default(decimal);
                        decimal ingresoespecial = default(decimal);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            proteccion = ds.Tables[0].Select($"[codigo]='{Ccodigo.Trim()}' and [tipoIngrDeduc]<>'{25}'").Sum((DataRow c) => Convert.ToDecimal(c["valor"]));
                            bono = ds.Tables[0].Select($"[codigo]='{Ccodigo.Trim()}' and [tipoIngrDeduc]='{25}'").Sum((DataRow c) => Convert.ToDecimal(c["valor"]));
                            ingresoespecial = ((proteccion == 0m) ? otros : proteccion);
                        }
                        string interpaginado = CrearFooter(semana3, montopr, saldopr, ahorro, vacaciones, pagosemana, tipoplanilla, tperiodo, fechafin, ingresoespecial, bono, horasbv.ToString("n2"), colillaYviatico: false);
                        htmle += htmle;
                        htmle += interpaginado;

                        // TODO:VHPO 
                        if (existeVS1)
                        {
                            if (Convert.ToInt32(nperiodo) == periodo2 && existeVS1 == true)
                            {
                                html += CrearHojaViaticoHTML(int.Parse(nperiodo), Convert.ToDateTime(fechaini), Convert.ToDateTime(fechafin), viaticopersonaS1, existeVS1);
                            }
                            else
                            {
                                // pasar fecha Inicio y fin semana 1 y 2, dataset viaticopersonaS1, viaticopersonaS2, existeVS1 y existeVS2
                                html += CrearHojaViaticoHTML14nal(int.Parse(nperiodo), Convert.ToDateTime(fechaini), Convert.ToDateTime(fechafin), viaticopersonaS1, existeVS1, periodo2, viaticopersonaS2, Convert.ToDateTime(fechaini), Convert.ToDateTime(fechafin), existeVS1);
                                                                
                            }
                              
                        }
                        else
                        {
                            html += CrearTablaVaciaViatico();
                        }

                        html += htmle;
                        if (email.IndexOf("@") > -1)
                        {
                            emailTable.Rows.Add(email, htmltmp);
                        }
                    }
                    if (filtroemail == 3)
                    {
                        EnviarCorreoColilla(asunto, emailTable);
                    }
                    return html;
                }
                throw new Exception("No existen datos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public string GenerarColilla_ViaticoPeriodoPdf(DataTable dtEmpleadospln, string nperiodo, DateTime ini, DateTime fin, int periodo2, int semana2, int tipoplanilla, int tperiodo, int filtroemail, string encabezado, bool periodoConsolida)
        {
            try
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (dtEmpleadospln.Rows.Count > 0)
                {
                    DataTable emailTable = new DataTable();
                    emailTable.Columns.Add("correo");
                    emailTable.Columns.Add("adjunto");
                    DataSet dsComprobante = ObtenerEstructuraComprobantePago(nperiodo, periodo2, semana2);
                    DataTable dtdetalle = dsComprobante.Tables[0];
                    DataSet dsEncComprobante = new DataSet();
                    DataTable dtencabezado = new DataTable();
                    DataTable viaticos = ObtenerPersonalPagoViatico(int.Parse(nperiodo), 1, ini, fin, 1, 0, "", dtEmpleadospln);
                    DataRow[] existeviaticop = null;
                    bool existeV = false;
                    DataTable viaticopersona = new DataTable();
                    DataSet ds = null;
                    ds = CargarIngresoPeriodoIBruto(int.Parse(nperiodo), periodo2, 0);
                    string periodo3 = "";
                    string semana3 = "";
                    string Ccodigo = "";
                    string vacaciones = "";
                    string saldopr = "";
                    string montopr = "";
                    string ahorro = "";
                    string fechaini = "";
                    string fechafin = "";
                    string empresa = "";
                    string Cnombre = "";
                    string Ccuenta = "";
                    string Cdepto = "";
                    string Cimpresion = "";
                    string CSeguro = "";
                    string CCedula = "";
                    string CCargo = "";
                    string email = "";
                    string asunto = "";
                    DateTime fecha_ingreso = default(DateTime);
                    html = "";
                    string stringMessage = "";
                    for (int i = 0; i < dtEmpleadospln.Rows.Count; i++)
                    {
                        d500 = 0;
                        d200 = 0;
                        d100 = 0;
                        d50 = 0;
                        d20 = 0;
                        d10 = 0;
                        d5 = 0;
                        d1 = 0;
                        d05 = 0;
                        d025 = 0;
                        d010 = 0;
                        d005 = 0;
                        d001 = 0;
                        ingresote = default(decimal);
                        egresote = default(decimal);
                        horasbv = default(decimal);
                        htmle = "";
                        htmltmp = "";
                        htmlv = "";
                        stringMessage = "";
                        fechaini = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechaini"]).ToShortDateString();
                        fechafin = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechafin"]).ToShortDateString();
                        fecha_ingreso = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fecha_ingreso"]);
                        Neg_Liquidacion.Globales.fechaR = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechafin"]);
                        email = dtEmpleadospln.Rows[i]["email"].ToString();
                        empresa = dtEmpleadospln.Rows[i]["empresa"].ToString();
                        periodo3 = dtEmpleadospln.Rows[i]["periodo"].ToString();
                        Ccodigo = dtEmpleadospln.Rows[i]["codigo"].ToString();
                        if (Ccodigo == "83761")
                        {
                        }
                        Cnombre = dtEmpleadospln.Rows[i]["nombre"].ToString();
                        Ccuenta = dtEmpleadospln.Rows[i]["cuenta"].ToString();
                        Cdepto = dtEmpleadospln.Rows[i]["depto"].ToString();
                        CSeguro = dtEmpleadospln.Rows[i]["numero_seguro"].ToString();
                        CCedula = dtEmpleadospln.Rows[i]["cedula_identidad"].ToString();
                        CCargo = dtEmpleadospln.Rows[i]["nombre_cargo"].ToString();
                        Cimpresion = DateTime.Now.Date.ToString("dd/MM/yyyy");
                        DataTable Datos = Neg_Liquidacion.CalcularDiasVacaciones(Convert.ToInt32(Ccodigo), fecha_ingreso, 1, userDetail.getIDEmpresa());
                        
                        existeviaticop = (from c in viaticos.AsEnumerable()
                                          where c.Field<int>("codigo_empleado") == int.Parse(Ccodigo)
                                          select c).ToArray();
                        if (existeviaticop.Length != 0)
                        {
                            existeV = true;
                            viaticopersona = existeviaticop.CopyToDataTable();
                        }
                        else
                        {
                            existeV = false;
                            viaticopersona = new DataTable();
                        }

                        if (Datos != null)
                        {
                            vacaciones = Datos.Rows[0]["saldovacaciones"].ToString();
                        }
                        int Limite = 2;
                        Limite = ((tipoplanilla != 1) ? 1 : 2);
                        int bandera = 0;
                        int pagosemana = 0;
                        int semanalimite = 0;
                        for (int j = 0; j < Limite; j++)
                        {
                            if (tipoplanilla != 2 && tperiodo == 1)
                            {
                                semanalimite = j + 1;
                            }
                            else if (tipoplanilla == 2 && tperiodo == 1)
                            {
                                semanalimite = 0;
                                j++;
                            }
                            dsEncComprobante = ((!periodoConsolida || semanalimite != 2) ? ObtenerEncComprobantePago(periodo3, periodo3, Convert.ToInt32(dtEmpleadospln.Rows[i]["codigo"]), semanalimite, semanalimite) : ObtenerEncComprobantePago(periodo3, periodo2.ToString(), Convert.ToInt32(dtEmpleadospln.Rows[i]["codigo"]), semanalimite, 1));
                            if (dsEncComprobante.Tables.Count > 0 && dsComprobante.Tables[0].Rows.Count > 0)
                            {
                                dtencabezado = dsEncComprobante.Tables[0];
                            }
                            ingresosem = default(decimal);
                            egresosem = default(decimal);
                            foreach (DataRow drowS1 in dtencabezado.Rows)
                            {
                                semana3 = drowS1["nsemana"].ToString();
                                string salario = drowS1["salario"].ToString();
                                string horast = drowS1["horast"].ToString();
                                string extra = drowS1["extra"].ToString();
                                string horase = drowS1["horase"].ToString();
                                string horasdv = drowS1["horasdv"].ToString();
                                horasbv += Convert.ToDecimal(drowS1["horasbv"].ToString());
                                string bonovariable = drowS1["bonovariable"].ToString();
                                saldopr = drowS1["saldo"].ToString();
                                montopr = drowS1["totalpagar"].ToString();
                                ahorro = drowS1["ahorro"].ToString();
                                ingresote += ((salario != "") ? Convert.ToDecimal(salario) : 0m);
                                ingresosem += ((salario != "") ? Convert.ToDecimal(salario) : 0m);
                                string Lcodigo = "Empleado:";
                                string Lcuenta = "Cuenta:";
                                string Ldepto = "Depto:";
                                string LHora = "HorasT:";
                                string LSalario = "Salario:";
                                string LImpresion = "Impresion:";
                                string LHEx = "HorasE:";
                                string LHDV = "HorasVD:";
                                string LSEx = "PagoE:";
                                string LSeguro = "Inss:";
                                string LCedula = "Cédula:";
                                string LCargo = "Cargo:";
                                char pad = ' ';
                                Lcodigo.PadRight(8, pad);
                                Lcuenta.PadRight(8, pad);
                                Ldepto.PadRight(8, pad);
                                LHora.PadRight(8, pad);
                                LSalario.PadRight(8, pad);
                                LImpresion.PadRight(8, pad);
                                LHEx.PadRight(8, pad);
                                LHDV.PadRight(8, pad);
                                LSEx.PadRight(8, pad);
                                LSeguro.PadRight(8, pad);
                                LCedula.PadRight(8, pad);
                                LCargo.PadRight(8, pad);
                                if (bandera == 0)
                                {
                                    bandera = 1;
                                    asunto = "Colilla de pago del periodo  " + encabezado + ", del " + fechaini + " al " + fechafin;
                                    htmltmp = htmltmp + "<font size='24px'; align='center'; color=\"#000000\"><b><i>" + empresa + "</i></b></font><br/><font size='24px'; align='center'; color=\"#000000\"><b><i>" + asunto + "</i></b></font><br/><font size='4px'><table border='0' cellpadding='0' cellspacing='0'><tr><td width='30%'>" + Ldepto + "&nbsp;" + Cdepto + "</td><td width='50%'>" + Lcodigo + "&nbsp;" + Ccodigo + " - " + Cnombre + "</td><td width='20%'>" + Lcuenta + "&nbsp;" + Ccuenta + "</td></tr><tr><td width='30%'>" + LCedula + "&nbsp;" + CCedula + "&nbsp;-&nbsp;" + LSeguro + "&nbsp;" + CSeguro + "</td><td width='50%'>" + LCargo + "&nbsp;" + CCargo + "</td><td width='20%'>" + LImpresion + "&nbsp;" + Cimpresion + "</td></tr><tr><td width='30%' color='#FFFFFF'>_</td><td width='50%' color='#FFFFFF'>_</td><td width='20%' color='#FFFFFF'>_</td></tr></table></font>";
                                }
                                pagosemana++;
                                htmltmp = htmltmp + "<font size='4px'><table border='0' cellpadding='0' cellspacing='0'><tr><td width='30%'><b>Semana&nbsp;" + semana3 + "</b>&nbsp;&nbsp;" + LHora + "&nbsp;" + horast + "&nbsp;&nbsp;" + LSalario + "&nbsp;" + salario + "</td><td width='50%'>" + ((horasdv != "") ? LHDV : "") + "&nbsp;" + horasdv + "&nbsp;&nbsp;" + ((horase != "") ? LHEx : "") + "&nbsp;" + horase + "</td><td width='20%'>&nbsp;</td></tr></table></font>";
                                CrearTablaSem(dtdetalle, Ccodigo, semana3, tipoplanilla, tperiodo, colillaYviatico: true);
                            }
                        }
                        if (dsEncComprobante.Tables.Count <= 0 || dsComprobante.Tables[0].Rows.Count <= 0)
                        {
                            continue;
                        }
                        decimal proteccion = default(decimal);
                        decimal bono = default(decimal);
                        decimal otros = default(decimal);
                        decimal ingresoespecial = default(decimal);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            proteccion = ds.Tables[0].Select($"[codigo]='{Ccodigo.Trim()}' and [tipoIngrDeduc]<>'{25}'").Sum((DataRow c) => Convert.ToDecimal(c["valor"]));
                            bono = ds.Tables[0].Select($"[codigo]='{Ccodigo.Trim()}' and [tipoIngrDeduc]='{25}'").Sum((DataRow c) => Convert.ToDecimal(c["valor"]));
                            ingresoespecial = ((proteccion == 0m) ? otros : proteccion);
                        }
                        string interpaginado = CrearFooter(semana3, montopr, saldopr, ahorro, vacaciones, pagosemana, tipoplanilla, tperiodo, fechafin, ingresoespecial, bono, horasbv.ToString("n2"), colillaYviatico: true);
                        
                        html += htmle;
                        stringMessage = htmltmp;
                        
                        if (existeV)
                        {
                            html += CrearHojaViaticoHTML(int.Parse(nperiodo), Convert.ToDateTime(fechaini), Convert.ToDateTime(fechafin), viaticopersona, existeV);
                        }
                        else
                        {
                            html += CrearTablaVaciaViatico();
                        }

                        stringMessage += htmlv;
                        html += htmle;
                        if (email.IndexOf("@") > -1)
                        {
                            emailTable.Rows.Add(email, stringMessage);
                        }
                    }
                    if (filtroemail == 3)
                    {
                        bool resultadoemail = EnviarCorreoColilla(asunto, emailTable);
                        if (resultadoemail)
                        {
                            Console.WriteLine("Correo enviado con éxito.");
                            //throw new Exception("Correo enviado con éxito.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo enviar el correo después de varios intentos.");
                            throw new Exception("No se pudo enviar el correo después de varios intentos.");
                        }
                    }
                    return html;
                }
                throw new Exception("No existen datos");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool EnviarCorreoColilla(string asunto, DataTable mensaje)
        {
            List<string> correos;
            bool resultado = false;
            foreach (DataRow item in mensaje.Rows)
            {
                correos = new List<string>();
                correos.Add(item["correo"].ToString());

                //TODO:VHPO 17/12/2024
               resultado = noti.EnviarNotificacionVariosDestinatarios(asunto, item["adjunto"].ToString(), correos);

                if (resultado)
                {
                    Console.WriteLine("Correo enviado con éxito.");
                }
                else
                {
                    Console.WriteLine("No se pudo enviar el correo después de varios intentos.");
                }
               
            }
            return resultado;
        }

        public bool EnviarCorreoColillaPDF(string asunto, string correo, string rutapdf)
        {
            //List<string> correos;
            //correos = new List<string>();
            //correos.Add(item["correo"].ToString());
            bool resultado = false;
            //foreach (DataRow item in mensaje.Rows)
            //{

            //}
            // Crear una lista de cadenas
            List<string> correos = new List<string>();
           
            // Agregar el correo a la lista
            correos.Add(correo);

            string mensaje = @"Por medio de la presente, y en cumplimiento con lo establecido en el Código del 
                               Trabajo de Nicaragua, específicamente en el artículo 42, que regula las obligaciones del empleador 
                               respecto a la remuneración y entrega de recibos, le informamos que su colilla se le entrega por esta via,
                               ver documendo adjunto PDF, " + asunto;

            //TODO:VHPO 26/12/2024
            resultado = noti.EnviarNotificacionVariosDestinatariosPDF(asunto, mensaje, correos, rutapdf, 3);

            if (resultado)
            {
                Console.WriteLine("Correo enviado con éxito.");
            }
            else
            {
                Console.WriteLine("No se pudo enviar el correo después de varios intentos.");
            }


            return resultado;
        }
       
        private void CrearTablaSem(DataTable dtdetalle, string Ccodigo, string semana, int tipoplanilla, int tperiodo, bool colillaYviatico)
        {
            int contador = 0;
            DataRow[] dtniveles = dtdetalle.Select($"[codigo]='{Ccodigo.Trim()}' and [nsemana]= '{semana}'");
            DataRow[] dtNivelesTotal = dtdetalle.Select($"[codigo]='{Ccodigo.Trim()}'");
            htmltmp += "<font size='4px'><table width='100%' border=1><tr><td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Ingresos</b></td><td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Egresos</b></td></tr></table><table width='100%' cellpadding='0' cellspacing='0'>";
            DataRow[] array = dtniveles;
            foreach (DataRow drowD in array)
            {
                string Ingreso = "";
                string ValorIngreso = "";
                string Egreso = "";
                string ValorEgreso = "";
                string Ingreso2 = "";
                string ValorIngreso2 = "";
                string Egreso2 = "";
                string ValorEgreso2 = "";
                contador++;
                Egreso = drowD["egreso"].ToString();
                ValorEgreso = drowD["valoregr"].ToString();
                Ingreso = drowD["ingreso"].ToString();
                ValorIngreso = drowD["valoring"].ToString();
                Egreso2 = drowD["egreso2"].ToString();
                ValorEgreso2 = drowD["valoregr2"].ToString();
                Ingreso2 = drowD["ingreso2"].ToString();
                ValorIngreso2 = drowD["valoring2"].ToString();
                ingresote += ((ValorIngreso != "") ? Convert.ToDecimal(ValorIngreso) : 0m) + ((ValorIngreso2 != "") ? Convert.ToDecimal(ValorIngreso2) : 0m);
                egresote += ((ValorEgreso != "") ? Convert.ToDecimal(ValorEgreso) : 0m) + ((ValorEgreso2 != "") ? Convert.ToDecimal(ValorEgreso2) : 0m);
                ingresosem += ((ValorIngreso != "") ? Convert.ToDecimal(ValorIngreso) : 0m) + ((ValorIngreso2 != "") ? Convert.ToDecimal(ValorIngreso2) : 0m);
                egresosem += ((ValorEgreso != "") ? Convert.ToDecimal(ValorEgreso) : 0m) + ((ValorEgreso2 != "") ? Convert.ToDecimal(ValorEgreso2) : 0m);
                htmltmp = htmltmp + "<tr><td COLSPAN='3' width='10%'>" + Ingreso + "</td><td COLSPAN='3' width='10%'>" + ValorIngreso + "</td><td COLSPAN='3' width='10%'>" + Ingreso2 + "</td><td COLSPAN='3' width='10%'>" + ValorIngreso2 + "</td><td COLSPAN='3' width='10%'>" + Egreso + "</td><td COLSPAN='3' width='10%'>" + ValorEgreso + "</td><td COLSPAN='3' width='10%'>" + Egreso2 + "</td><td COLSPAN='3' width='10%'>" + ValorEgreso2 + "</td></tr>";
            }
            htmltmp = htmltmp + "</table><table width='100%' cellpadding='0' cellspacing='0' align='right'><tr><td COLSPAN='6' width='10%'></td><td COLSPAN='3' width='10%'><b>Total Ing:</b></td><td COLSPAN='3' width='10%'>" + ingresosem + "</td><td COLSPAN='6' width='10%'></td><td COLSPAN='3' width='10%'><b>Total Egr:</b></td><td COLSPAN='3' width='10%'>" + egresosem + "</td></tr></table>";
            int LimiteEspacios = 0;
            if (tipoplanilla != 2 && tperiodo == 1)
            {
                LimiteEspacios = 4;
            }
            else if (tipoplanilla == 2 && tperiodo == 1)
            {
                LimiteEspacios = 13;
            }
            if (tperiodo == 5)
            {
                LimiteEspacios = 0;
            }
            else if (tperiodo == 3 || tperiodo == 4)
            {
                LimiteEspacios = 4;
            }
            htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";
            if (contador < LimiteEspacios)
            {
                for (int i = 0; i < LimiteEspacios - contador; i++)
                {
                    if (dtNivelesTotal.Count() <= 7)
                    {
                        htmltmp += "<tr style='color:white;background-color=white;font-color:white;'><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td></tr>";
                    }
                }
            }
            htmltmp += "</table>";
            htmltmp += "</font>";
        }
        private string CrearFooter(string semana, string montopr, string saldopr, string ahorro, string vacaciones, int pagosemana, int tipoplanilla, int tperiodo, string fechafin, decimal proteccion, decimal bono, string horasbv, bool colillaYviatico)
        {
            string interpaginado = "";
            if (tipoplanilla != 2 && tperiodo == 1 && pagosemana == 1 && !colillaYviatico)
            {
                int LimiteEspacios = 5;
                htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";
                for (int i = 0; i < LimiteEspacios; i++)
                {
                    htmltmp += "<tr style='color:white;background-color=white;font-color:white;'><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td><td COLSPAN='3' width='10%'>blanck</td></tr>";
                }
                htmltmp += "</table>";
            }
            if (semana != "1" || pagosemana == 1)
            {
                if (colillaYviatico)
                {
                    htmltmp += "<br/><table width='90%';font size='4px';align='center' cellpadding='0' cellspacing='0'>";
                }
                else
                {
                    htmltmp += "<br/><table width='90%';font size='4px';align='center'>";
                }
                htmltmp = htmltmp + "<tr><td width='14%' colspan='7'></td><td  width='4%';><b>Ingresos</b></td><td  width='4%';><b>Egresos</b></td><td  width='4%';><b>Neto</b></td></tr><tr><td width='14%' colspan='7'></td><td  width='4%';>" + ingresote + "</td><td  width='4%';>" + egresote + "</td><td  width='4%';>" + (ingresote - egresote) + "</td></tr></table>";
                string FilaDetalle = "_";
                string FilaVac = "_";
                string complementoDeuda = "";
                string complementoAhorro = "";
                if (vacaciones != "")
                {
                    FilaVac = "Vacaciones al " + fechafin + ": " + Math.Round(Convert.ToDouble(vacaciones), 2);
                }
                if (montopr != "")
                {
                    complementoDeuda = "Prestamos :&nbsp;" + montopr + " Saldo Pendiente :&nbsp;" + saldopr + "&nbsp;";
                }
                if (ahorro != "")
                {
                    complementoAhorro = "::: Ahorro cooperativa :&nbsp;" + ahorro;
                }
                FilaDetalle = complementoDeuda + complementoAhorro;
                if (colillaYviatico)
                {
                    htmltmp += "<table width='95%';font size='4px';align='center' cellpadding='0' cellspacing='0'>";
                }
                else
                {
                    htmltmp += "<table width='95%';font size='4px';align='center'>";
                }
                htmltmp = htmltmp + "<tr><td colspan='4'>" + FilaVac + "</td><td align='right'>VAT" + proteccion + "</td></tr><tr><td colspan='4' width='92%'>" + FilaDetalle + "</td><td align='right'>BV" + bono + ((horasbv != "") ? ("(" + horasbv + ")") : "") + "</td></tr></table>";
                if (ingresote != 0m)
                {
                    if (tipoplanilla != 2 && tperiodo == 1 && FilaDetalle == "_")
                    {
                        htmltmp += "<table width='90%';font size='4px';align='center';><tr><td  width='2%'>_</td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='2%'> </td><td  width='4%'> </td><td  width='4%'> </td><td  width='4%'> </td></tr></table>";
                    }
                    htmle = htmltmp;
                    interpaginado = ((!(FilaDetalle != "_") || colillaYviatico) ? "" : (interpaginado + "<table width='90%';font size='4px';align='center'><tr><td>_</td></tr><tr><td>_</td></tr></table>"));
                }
            }
            return interpaginado;
        }
        public string GenerarComprobantePrestacionPdf(DataTable dtEmpleadospln, string filtro, int periodo2, int semana2, int tipoplanilla, int tperiodo, int filtroemail)
        {
            //empleados pagados en planilla
            try
            {
                DataTable emailTable = new DataTable();
                emailTable.Columns.Add("correo");
                emailTable.Columns.Add("adjunto");

                if (dtEmpleadospln.Rows.Count > 0)
                {
                    //detalle de deducciones e ingresos de empleados por semana
                    DataSet dsComprobante = ObtenerEstructuraComprobantePago(filtro, periodo2, semana2);
                    DataTable dtdetalle = dsComprobante.Tables[0];

                    string periodo = "", Ccodigo = "", fechaini = "", fechafin = "", fechaingreso = "", empresa = "", Cnombre = "", Ccuenta = "", Cdepto = "", Cimpresion = "", CSeguro = "", CCedula = "", CCargo = "", email = "", asunto = "";
                    string diasprestacion = "", salprestacion = "", mes = "", anio = "";

                    for (int i = 0; i < dtEmpleadospln.Rows.Count; i++)
                    {
                        //Monedas General
                        d500 = 0; d200 = 0; d100 = 0; d50 = 0; d20 = 0; d10 = 0; d5 = 0; d1 = 0; d05 = 0; d025 = 0; d010 = 0; d005 = 0; d001 = 0;
                        ingresote = 0; egresote = 0;
                        htmle = ""; htmltmp = "";

                        fechaini = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechaini"]).ToShortDateString();
                        fechafin = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechafin"]).ToShortDateString();
                        fechaingreso = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fecha_ingreso"]).ToShortDateString();
                        empresa = dtEmpleadospln.Rows[i]["empresa"].ToString();
                        periodo = dtEmpleadospln.Rows[i]["periodo"].ToString();
                        email = dtEmpleadospln.Rows[i]["email"].ToString();
                        Ccodigo = dtEmpleadospln.Rows[i]["codigo"].ToString();
                        Cnombre = dtEmpleadospln.Rows[i]["nombre"].ToString();
                        Ccuenta = dtEmpleadospln.Rows[i]["cuenta"].ToString();
                        Cdepto = dtEmpleadospln.Rows[i]["depto"].ToString();
                        CSeguro = dtEmpleadospln.Rows[i]["numero_seguro"].ToString();
                        CCedula = dtEmpleadospln.Rows[i]["cedula_identidad"].ToString();
                        CCargo = dtEmpleadospln.Rows[i]["nombre_cargo"].ToString();
                        Cimpresion = System.DateTime.Now.Date.ToString("dd/MM/yyyy");

                        int bandera = 0;

                        ingresosem = 0; egresosem = 0;

                        #region Datos  Cabecera
                        string Lcodigo = "Empleado:", Lcuenta = "Cuenta:", Ldepto = "Depto:", LHora = "HorasT:", LSalario = "Salario:", LImpresion = "Impresion:", LHEx = "HorasE:", LSEx = "PagoE:", LSeguro = "Inss:", LCedula = "Cédula:", LCargo = "Cargo:";
                        char pad = ' ';
                        Lcodigo.PadRight(8, pad);
                        Lcuenta.PadRight(8, pad);
                        Ldepto.PadRight(8, pad);
                        LHora.PadRight(8, pad);
                        LSalario.PadRight(8, pad);
                        LImpresion.PadRight(8, pad);
                        LHEx.PadRight(8, pad);
                        LSEx.PadRight(8, pad);
                        LSeguro.PadRight(8, pad);
                        LCedula.PadRight(8, pad);
                        LCargo.PadRight(8, pad);
                        #endregion

                        if (bandera == 0)
                        {
                            bandera = 1;
                            asunto = "Colilla de pago de " + ObtenerNombrePeriodo(tperiodo) + " - Periodo del " + fechaini + " al " + fechafin;
                            htmltmp += "<font size='24px'; align='center'; color=\"#000000\"><b><i>" + empresa + "</i></b></font><br/>" +
                          "<font size='24px'; align='center'; color=\"#000000\"><b><i>Colilla de pago de " + ObtenerNombrePeriodo(tperiodo) + " - Periodo del " + fechaini + " al " + fechafin + "</i></b></font><br/>" +
                          "<font size='4px'>" +
                              "<table border='0' cellpadding='0' cellspacing='0'>" +
                                  "<tr>" +
                                       "<td width='30%'>" + Ldepto + "&nbsp;" + Cdepto + "</td>" +
                                       "<td width='50%'>" + Lcodigo + "&nbsp;" + Ccodigo + " - " + Cnombre + "</td>" +
                                       "<td width='20%'>" + Lcuenta + "&nbsp;" + Ccuenta + "</td>" +
                                  "</tr>" +
                                   "<tr>" +
                                           "<td width='30%'>" + LCedula + "&nbsp;" + CCedula + "&nbsp;-&nbsp;" + LSeguro + "&nbsp;" + CSeguro + "</td>" +
                                           "<td width='50%'>" + LCargo + "&nbsp;" + CCargo + "</td>" +
                                           "<td width='20%'>" + LImpresion + "&nbsp;" + Cimpresion + "</td>" +
                                      "</tr>" +
                                      "<tr>" +
                                           "<td width='30%' color='#FFFFFF'>_</td>" +
                                           "<td width='50%' color='#FFFFFF'>_</td>" +
                                           "<td width='20%' color='#FFFFFF'>_</td>" +
                                      "</tr>" +
                             "</table>" +
                         "</font>";
                            //encabezado de empleados por semana
                            DataSet dsEncComprobante = ObtenerEncComprobantePrestacion(Convert.ToInt32(filtro), Convert.ToInt32(Ccodigo), tperiodo);
                            DataTable dtencabezado = dsEncComprobante.Tables[0];
                            string Ldias = "Dias " + ObtenerNombrePeriodo(tperiodo) + ":", Lingreso = "Fecha Ingreso:";
                            Ldias.PadRight(8, pad);
                            Lingreso.PadRight(8, pad);
                            diasprestacion = dtencabezado.Rows[0]["diasprestacion"].ToString();
                            salprestacion = dtencabezado.Rows[0]["saldia"].ToString();
                            mes = dtencabezado.Rows[0]["mesnombre"].ToString();
                            anio = dtencabezado.Rows[0]["anio"].ToString();

                            htmltmp += "<font size='4px'>" +
                                          "<table cellpadding='0' cellspacing='0'>" +

                                               "<tr>" +
                                                   "<td width='30%'>" + Lingreso + "&nbsp;" + fechaingreso + "</td>" +
                                                   "<td width='30%'>" + Ldias + "&nbsp;" + diasprestacion + "</td>" +
                                              //"<td width='20%'>" + Lsalm + "&nbsp;" + salmayor + "</td>" +
                                              "</tr>" +
                                         "</table>" +
                                     "</font>";
                        }
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////                                                        

                        CrearTablaSem(dtdetalle, Ccodigo, "1", tipoplanilla, tperiodo,colillaYviatico:false);//Detalle de Conceptos.   

                        CrearFooterPrestacion(diasprestacion, mes, anio, salprestacion, tperiodo);

                        html += htmle;

                        if (email.IndexOf("@") > -1)
                        {
                            emailTable.Rows.Add(email, htmltmp);
                        }
                    }
                    if (filtroemail == 3)
                    {
                        EnviarCorreoColilla(asunto, emailTable);
                    }
                    return html;
                }
                else
                {
                    throw new Exception("No Existen Datos");
                }
              
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CrearFooterPrestacion(string dias, string mes, string anio, string salprestacion, int tperiodo)
        {
            //otros informativos
            string FilaDetalle = "_";
            char pad = ' ';
            string Lsalario = tperiodo == 5 ? "Mayor" : "Promedio";
            Lsalario.PadRight(8, pad);

            htmltmp += "<br/><p align='center'><em>Neto a Recibir C$ " + (ingresote - egresote) + "</em></p>";

            htmltmp += "<table width='30%' font size='4px';align='center'>" +
                   "<tr>" +
                      "<td  border='1'; colspan='2' align='center'>Salario " + Lsalario + " / Dia</td>" +
                  "</tr>" +
                   "<tr>";
            if (tperiodo == 5)
            {
                htmltmp += "<td border='1'; align='center'>" + mes + " / " + anio + "</td>" +
                      "<td border='1'; align='center'>" + salprestacion + "</td>";
            }
            else
            {
                htmltmp += "<td border='1'; colspan='2' align='center'>" + salprestacion + "</td>";
            }

            htmltmp += "</tr>" +
                   "</table>";

            htmltmp += tperiodo == 5 ? "<br/><p align='center'><em>Feliz Navidad les desea Kaizen, S.A !!!</em></p>" : "<br/><p align='center' color='#FFFFFF'><em>******************************************</em></p>";//color='#FFFFFF'
            int LimiteEspacios = 0;
            if (tperiodo == 5)
            {
                LimiteEspacios = 6;
            }
            else
            {
                LimiteEspacios = 4;
            }

            htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";

            for (int i = 0; i < LimiteEspacios; i++)
            {

                htmltmp += "<tr style='color:white;background-color=white;font-color:white;'>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
              "</tr>";

            }
            htmltmp += "</table>";


            ///////////////////////////////////////////////////////////////////////////


            if (ingresote != 0)//solo se imprimen pagos con ingresos y egresos !=0
            {

                if (FilaDetalle == "_")
                {
                    htmltmp += "<table width='90%';font size='4px';align='center';>" +//espacio en blanco
                      "<tr>" +
                       "<td  width='2%'>_</td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='2%'> </td>" +
                       "<td  width='4%'> </td>" +
                       "<td  width='4%'> </td>" +
                       "<td  width='4%'> </td>" +
                  "</tr>" +
                  "</table>";

                }

                htmle += htmltmp;
                htmle += htmle;
                if (FilaDetalle != "_")
                {
                    htmle += "<table width='90%';font size='4px';align='center'>" +
                  "<tr><td>_</td></tr>" +
                  "<tr><td>_</td></tr>" +
                  "</table>";
                }

            }
        }
        public string ObtenerNombrePeriodo(int tperiodo)
        {
            if (tperiodo == 1)//pago normal
            {
                return "Planilla Pago";
            }
            else if (tperiodo == 3 || tperiodo == 4)
            {
                return "Vacaciones";
            }
            else if (tperiodo == 5)
            {
                return "Aguinaldo";
            }
            return "";
        }
        public DataSet CargarIngresoPeriodoIBruto(int periodo, int periodo2, int tipoing)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            return di.CargarIngresoPeriodoIBruto(periodo, periodo2, tipoing, userDetail.getIDEmpresa());
        }
        public DataTable GenerarDetalleHorasExtrasRpt(string filtro, string fecha, string fecha2)
        {
            DataSet ds = new DataSet();
            Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
            try
            {
                DataTable design = new DataTable();
                design.Columns.Add("nombre_depto", typeof(string));
                if (filtro == "2")//por empleados
                {
                    design.Columns.Add("codigo_empleado", typeof(string));
                    design.Columns.Add("nombrecompleto", typeof(string));
                }
                design.Columns.Add("nombrerubro", typeof(string));
                design.Columns.Add("tiempo", typeof(decimal));
                design.Columns.Add("valor", typeof(decimal));
                design.Columns.Add("fecha", typeof(DateTime));

                if (string.IsNullOrEmpty(fecha) && string.IsNullOrEmpty(fecha2))
                {
                    throw new Exception("Debe ingresar rango de fecha valido");
                }
                DataTable rpt = Neg_DevYDed.ObtenerDetalleHorasExtrasxFecha(1, 0, Convert.ToDateTime(fecha), Convert.ToDateTime(fecha2));

                //obtener solo horas extras validas para el reporte
                DataRow[] HEvalid = null;                
                HEvalid = rpt.Select("periodo > 0 and id_tipo = 1 and tipoingrdeduc = 1");

                if (HEvalid.Length>0)
                {                    
                    DataTable deptosxfecha = new DataTable();

                    DataTable HE = new DataTable();
                    HE = HEvalid.CopyToDataTable();

                    if (filtro == "1")//por area
                    {
                        deptosxfecha = HE.AsEnumerable().GroupBy(row => new { c2 = row["nombre_depto"], c3 = row["fecha"] }).Select(grp => grp.First()).CopyToDataTable();
                    }
                    else if (filtro == "2")//por empleado
                    {
                        deptosxfecha = HE.AsEnumerable().GroupBy(row => new { c2 = row["nombre_depto"], c3 = row["fecha"], c1 = row["codigo_empleado"] }).Select(grp => grp.First()).CopyToDataTable();

                    }
                    else//por gerencia
                    {
                        deptosxfecha = HE.AsEnumerable().GroupBy(row => new { c2 = row["gerencia"], c3 = row["fecha"] }).Select(grp => grp.First()).CopyToDataTable();
                    }
                    decimal tiempo = 0, valor = 0;
                    string condicion = "";
                    DataRow[] filas;
                    foreach (DataRow dr in deptosxfecha.Rows)
                    {
                        if (filtro == "3")//por gerencia
                        {
                            condicion = "gerencia='" + dr["gerencia"].ToString() + "' and fecha='" + Convert.ToDateTime(dr["fecha"]) + "'";
                        }
                        else//por depto
                        {
                            condicion = "codigo_depto=" + Convert.ToInt32(dr["codigo_depto"]) + " and fecha='" + Convert.ToDateTime(dr["fecha"]) + "'";
                        }

                        if (filtro == "2")//por area
                        {
                            condicion += " and codigo_empleado = " + Convert.ToInt32(dr["codigo_empleado"]) + "";
                        }

                        filas = HE.Select(condicion);
                        tiempo = filas.Sum(c => Convert.ToDecimal(c["tiempo"]));
                        valor = filas.Sum(c => Convert.ToDecimal(c["valor"]));

                        if (filtro == "1")//por depto
                        {
                            int codigo_deptoC = 0;
                            string deptoC = "";
                            codigo_deptoC = Convert.ToInt32(dr["depto_afecta"]);
                            deptoC = codigo_deptoC == 0 ? dr["nombre_depto"].ToString() : dr["nombre_depto_afecta"].ToString();
                            design.Rows.Add(deptoC, dr["nombrerubro"].ToString(), tiempo, valor, Convert.ToDateTime(dr["fecha"]));
                        }
                        else if (filtro == "2")//por empleado
                        {
                            design.Rows.Add(dr["nombre_depto"].ToString(), dr["codigo_empleado"].ToString(), dr["nombrecompleto"].ToString(), dr["nombrerubro"].ToString(), tiempo, valor, Convert.ToDateTime(dr["fecha"]));
                        }
                        else//por gerencia
                        {
                            int codigo_deptoC = 0;
                            string gerenciaC = "";
                            codigo_deptoC = Convert.ToInt32(dr["depto_afecta"]);
                            gerenciaC = codigo_deptoC == 0 ? dr["gerencia"].ToString() : dr["gerencia_afecta"].ToString();
                            design.Rows.Add(gerenciaC, dr["nombrerubro"].ToString(), tiempo, valor, Convert.ToDateTime(dr["fecha"]));
                        }
                    }
                }
                return design;
                // MostrarReporte(design);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///comprobante incentivo
        public string GenerarComprobantIncentivoPdf(DataTable dtEmpleadospln, int nperiodo, int tipoplanilla, int tperiodo)
        {

            try
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                if (dtEmpleadospln.Rows.Count > 0)
                {


                    //detalle de deducciones e ingresos de empleados por semana
                    DataSet dsComprobante = ObtenerEstructuraComprobanteIncentivo(nperiodo);
                    DataTable dtdetalle = dsComprobante.Tables[0];

                    //obtener incentivo pendiente
                    DataTable pendiente = PlnIncentivoPendPagarxPeriodoSel(nperiodo);

                    //encabezado de empleados por semana
                    DataSet dsEncComprobante = new DataSet();
                    dsEncComprobante = ObtenerEncComprobanteIncentivo(nperiodo);
                    DataRow [] dtencabezado= null;

                    //ingresos no gravables deducidos de incentivo
                   

                    string periodo = "", semana = "", Ccodigo = "", fechaini = "", fechafin = "", empresa = "", Cnombre = "",Cdepto = "", Cimpresion = "";
               
                    for (int i = 0; i < dtEmpleadospln.Rows.Count; i++)
                    {
                        //Monedas General
                        d500 = 0; d200 = 0; d100 = 0; d50 = 0; d20 = 0; d10 = 0; d5 = 0; d1 = 0; d05 = 0; d025 = 0; d010 = 0; d005 = 0; d001 = 0;
                        ingresote = 0; egresote = 0;
                        htmle = ""; htmltmp = "";

                        fechaini = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechaini"]).ToShortDateString();
                        fechafin = Convert.ToDateTime(dtEmpleadospln.Rows[i]["fechafin"]).ToShortDateString();                     
                       
                        empresa = dtEmpleadospln.Rows[i]["empresa"].ToString();
                        periodo = dtEmpleadospln.Rows[i]["periodo"].ToString();

                        Ccodigo = dtEmpleadospln.Rows[i]["codigo_empleado"].ToString();
                        Cnombre = dtEmpleadospln.Rows[i]["nombre"].ToString();
                       
                        Cdepto = dtEmpleadospln.Rows[i]["depto"].ToString();
                       
                        Cimpresion = System.DateTime.Now.Date.ToString("dd/MM/yyyy");
                  

                        //por semana
                        int Limite = 2;
                        if (tipoplanilla == 1)//catorcenal
                            Limite = 2;
                        else
                            Limite = 1;//quincenal o mensual

                        int bandera = 0, pagosemana = 0;

                        int semanalimite = 0;
                        for (int j = 0; j < Limite; j++)
                        {

                            if (tipoplanilla != 2 && tperiodo == 1)//Planilla Catorcenal
                            {
                                semanalimite = (j + 1);
                            }
                            else if (tipoplanilla == 2 && tperiodo == 1)//Planilla Quincenal
                            {
                                semanalimite = 0;
                                j += 1;
                            }
                          

                            if (dsEncComprobante.Tables.Count > 0 && dsComprobante.Tables[0].Rows.Count > 0)
                            {
                                dtencabezado = dsEncComprobante.Tables[0].Select(string.Format("[codigo_empleado]='{0}' and [semana]= '{1}'", Ccodigo.Trim(), semanalimite)); 
                            }
                            ingresosem = 0; egresosem = 0;

                            foreach (DataRow drowS1 in dtencabezado)
                            {
                                ///datos de encabezado
                                semana = drowS1["semana"].ToString();
                                string dzpagar = drowS1["dzpagar"].ToString();
                                string bonoasistencia = drowS1["bonoasistencia"].ToString();
                                string incentivo = drowS1["incentivo"].ToString();
                                string deduccion = drowS1["deducciones"].ToString();
                                string total = drowS1["total"].ToString();
                          
                                #region Datos  Cabecera
                                string Lcodigo = "Empleado:", Lcuenta = "Cuenta:", Ldepto = "Depto:", LHora = "HorasT:", LSalario = "Salario:", LImpresion = "Impresion:", LHEx = "HorasE:", LSEx = "PagoE:", LSeguro = "Inss:", LCedula = "Cédula:", LCargo = "Cargo:";
                                string LBonoAsist = "Bono Asistencia: ", LDzPagar="Dz Pagar: ", LIncentivo="Monto Dz: ", LTotalIncentivo="Total Incentivo: ",  LDeducciones = "Deduccion:";
                                char pad = ' ';
                                Lcodigo.PadRight(8, pad);
                                Lcuenta.PadRight(8, pad);
                                Ldepto.PadRight(8, pad);
                                LHora.PadRight(8, pad);
                                LSalario.PadRight(8, pad);
                                LImpresion.PadRight(8, pad);
                                LHEx.PadRight(8, pad);
                                LSEx.PadRight(8, pad);
                                LSeguro.PadRight(8, pad);
                                LCedula.PadRight(8, pad);
                                LCargo.PadRight(8, pad);
                                LBonoAsist.PadRight(8, pad);
                                LDzPagar.PadRight(8, pad);
                                LIncentivo.PadRight(8, pad);
                                LDeducciones.PadRight(8, pad);
                                LTotalIncentivo.PadRight(8, pad);

                                #endregion

                                if (bandera == 0)
                                {
                                    bandera = 1;
                                    htmltmp = htmltmp + "<font size='12px'; align='center'; color=\"#000000\"><b><i>" + empresa + "</i></b></font><br/><font size='12px'; align='center'; color=\"#000000\"><b><i>Comprobante de produccion del periodo  " + periodo + ", del " + fechaini + " al " + fechafin + "</i></b></font><br/><font size='4px'><table border='0' cellpadding='0' cellspacing='0'><tr><td width='30%'>" + Ldepto + "&nbsp;" + Cdepto + "</td><td width='50%'>" + Lcodigo + "&nbsp;" + Ccodigo + " - " + Cnombre + "</td><td width='20%'>" + LImpresion + "&nbsp;" + Cimpresion + "</td></tr><tr><td width='30%' color='#FFFFFF'>_</td><td width='50%' color='#FFFFFF'>_</td><td width='20%' color='#FFFFFF'>_</td></tr></table></font>";
                                }
                                pagosemana++;
                                htmltmp = htmltmp + "<font size='4px'><table width='100%' border='0' cellpadding='0' cellspacing='0' style='font-size:6px;font-family;Arial;'><tr><td width='12%' colspan='2'>" + LDzPagar + "&nbsp;" + dzpagar + "</td><td width='12%' colspan='2'>" + LIncentivo + "&nbsp;" + incentivo + "</td><td width='12%' colspan='2'>" + LDeducciones + "&nbsp;" + deduccion + "</td><td width='12%' colspan='3'>" + LBonoAsist + "&nbsp;" + bonoasistencia + "</td><td width='12%' colspan='3'>" + LTotalIncentivo + "&nbsp;" + total + "</td><td width='50%' colspan='6'><b>Cortes Pendientes de pago</b></td></tr></table></font>";
                                CrearTablaIncentivoSem(dtdetalle, pendiente, Ccodigo, semana, tipoplanilla, tperiodo);
                                htmle += htmltmp;
                            }

                        }
                        
                        htmle += htmle;//dos copias del mismo codigo en la misma pagina
                        html += htmle;

                    }
                    return html;

                }
                else
                {
                    throw new Exception("No existen datos");

                }

               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void CrearTablaIncentivoSem(DataTable dtdetalle,DataTable pendiente, string Ccodigo, string semana, int tipoplanilla, int tperiodo)
        {
            DataRow[] dt1;           
            //se obtiene cada empleado por semana
            dt1 = dtdetalle.Select(string.Format("[codigo_empleado]='{0}' and [semana]= '{1}'", Ccodigo.Trim(), semana));
            int pago = dt1.Length;

            DataRow[] dt2;
            //se obtiene cada empleado por semana
            dt2 = pendiente.Select(string.Format("[codigo_empleado]='{0}' and [semana]= '{1}'", Ccodigo.Trim(), semana));          
            int pend = dt2.Length;

            int[] length = { pago, pend };
            int maxlenght = length.Max();

            int contador = 0;
            DataRow drowD;

            htmltmp += "<font size='4px'>" +
                                 "<table width='100%' border=1 style='font-size:5px;font-family;Arial;'>" +
                                     "<tr>" +//COLSPAN='12' width='10%' 
                                            "<th style='margin-right:5px;' align='center'><b>Producido</b></th>" +
                                             "<th style='margin-right:5px;' align='center'><b>#MM</b></th>" +
                                            "<th style='margin-right:5px;' align='center'><b>Dz Dia</b></th>" +
                                            "<th COLSPAN='2' style='margin-right:5px;' align='center'><b>Corte</b></th>" +
                                            //"<th COLSPAN='12' width='20%' style='margin-right:5px;' align='center'><b>Estilo</b></th>" +
                                            "<th style='margin-right:5px;' align='center'><b>Aprobado</b></th>" +                                            
                                            "<th style='margin-right:5px;' align='center'><b>Dz Pagar</b></th>" +
                                            "<th style='margin-right:5px;' align='center'><b>OQL</b></th>" +
                                            "<th style='margin-right:5px;' align='center'><b>Precio Dz</b></th>" +
                                            "<th style='margin-right:5px;' align='center'><b>Monto Dz</b></th>" +
                                            "<th style='color:white;background-color=white;font-color:white;'>espacio</th>" +
                                             //
                                             "<th style='margin-right:5px;' align='center'><b>Producido</b></th>" +
                                             "<th style='margin-right:5px;' align='center'><b>#MM</b></th>" +
                                            "<th style='margin-right:5px;' align='center'><b>Dz Dia</b></th>" +
                                            "<th COLSPAN='2' style='margin-right:5px;' align='center'><b>Corte</b></th>" +
                                            //"<th COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Estilo</b></th>" +
                                            "<th COLSPAN='2' style='margin-right:5px;' align='center'><b>Subestatus</b></th>" +
                                            "<th style='margin-right:5px;' align='center'><b>Dz Pagar</b></th>" +
                                      "</tr>" +
                                 "</table>" +
                                 "<table width='100%' cellpadding='0' cellspacing='0' style='font-size:5px;font-family;Arial;'>";

            //foreach (DataRow drowD in dtniveles)
            //{
            string produccion = "", aprobacion = "", corte = "", dzpagar = "", dzdia = "", precio = "", oql = "", monto = "", estilo = "", modulo = "";
            for (int i = 0; i < maxlenght; i++)
            {
                contador += 1;
               
                if (i < pago)
                {
                    drowD = dt1[i];
                    produccion = Convert.ToDateTime(drowD["fecha_producido"]).ToShortDateString();
                    modulo = drowD["modulo"].ToString();
                    aprobacion = Convert.ToDateTime(drowD["fecha_aprobado"]).ToShortDateString();
                    estilo = drowD["estilo"].ToString();
                    corte = drowD["corte"].ToString() + "_" + drowD["seccion"].ToString() + " / " + estilo;
                    dzpagar = drowD["docenaspagarprot"].ToString();
                    dzdia = drowD["docenasprodprot"].ToString();
                    precio = drowD["costo"].ToString();
                    oql = drowD["oql"].ToString();
                    monto = drowD["montodocenas"].ToString();
                }
                else
                {
                    produccion = ""; aprobacion = ""; corte = ""; dzpagar = ""; dzdia = ""; precio = ""; oql = ""; monto = ""; estilo = ""; modulo = "";
                }
               
                htmltmp += "<tr>" +//COLSPAN='3' width='10%'
                           "<td>" + produccion + "</td>" +
                             "<td>" + modulo + "</td>" +
                            "<td>" + dzdia + "</td>" +
                           "<td COLSPAN='2'>" + corte + "</td>" +
                           //"<td COLSPAN='3' width='20%'>" + estilo + "</td>" +
                           "<td>" + aprobacion + "</td>" +
                           "<td>" + dzpagar + "</td>" +
                           "<td>" + oql + "</td>" +
                           "<td>" + precio + "</td>" +
                           "<td style='border-right:solid black 1px;'>" + monto + "</td>"+
                           "<td style='color:white;background-color=white;font-color:white;border-left:solid black 1px;'>espacio</td>";
                //
                CrearTablaIncentivoPenSem(dt2, i, Ccodigo.Trim(), semana);
                htmltmp += "</tr>";
            }

            //}
            //totales por semana      
            htmltmp += "</table>";
            //htmltmp += "<table width='100%' cellpadding='0' cellspacing='0' align='right'><tr style='color:white;background-color=white;font-color:white;'>" +
            //                 "<td COLSPAN='6' width='10%'>blanck</td>" +
            //                 "<td COLSPAN='3' width='10%'>blanck</td>" +
            //                 "<td COLSPAN='3' width='10%'>blanck</td>" +
            //                 "<td COLSPAN='6' width='10%'>blanck</td>" +
            //                 "<td COLSPAN='3' width='10%'>blanck</td>" +
            //                 "<td COLSPAN='3' width='10%'>blanck</td></tr>" +
            //              "</table>";
            int LimiteEspacios = 15;            


            htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";

            if (contador < LimiteEspacios)
            {
                for (int i = 0; i < LimiteEspacios - contador; i++)
                {
                    //if (dtNivelesTotal.Count() <= 7)
                    //{
                    htmltmp += "<tr style='color:white;background-color=white;font-color:white;'>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "<td COLSPAN='3' width='10%'>blanck</td>" +
                             "</tr>";
                    // }
                }
        }
        htmltmp += "</table></font>";

            CrearFooterTablaIncentivo();

            //htmltmp += "<font size='4px'>" +
            //                    "<table border='0' cellpadding='0' cellspacing='0'>" +

            //                         "<tr>" +
            //                             "<td width='50%' colspan='12'><b>Produccion pendiente de pago</b></td>" +
            //                        "</tr>" +
            //                   "</table>" +
            //               "</font>";
            //CrearTablaIncentivoPenSem(pendiente,contador, Ccodigo, semana, tipoplanilla, tperiodo);//Detalle de Conceptos.

        }
        public void CrearFooterTablaIncentivo()
        {
           int LimiteEspacios = 8;

            htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";

            for (int i = 0; i < LimiteEspacios; i++)
            {

                htmltmp += "<tr style='color:white;background-color=white;font-color:white;'>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                     "<td COLSPAN='3' width='10%'>blanck</td>" +
              "</tr>";

            }
            htmltmp += "</table>";
        }
        private void CrearTablaIncentivoPenSem(DataRow[] dtniveles,int fila, string Ccodigo, string semana)
        {           
            string produccion = "", subestatus = "", corte = "", dzpagar = "", dzdia = "", estilo = "", modulo = "";

            if (fila < dtniveles.Length )
            {
                DataRow drowD = dtniveles[fila];
                produccion = Convert.ToDateTime(drowD["fecha_producido"]).ToShortDateString();
                modulo = drowD["modulo"].ToString();
                subestatus = drowD["subestatus"].ToString();
                estilo = drowD["estilo"].ToString();
                corte = drowD["corte"].ToString() + "_" + drowD["seccion"].ToString() + "/" + estilo;
                dzpagar = drowD["dzpagar"].ToString();
                dzdia = drowD["dzdia"].ToString();
            }
            else
            {
                produccion = ""; subestatus = ""; corte = ""; dzpagar = ""; dzdia = ""; estilo = ""; modulo = "";
            }
            //COLSPAN='3' width='10%'
            htmltmp += "<td>" + produccion + "</td>" +
                         "<td>" + modulo + "</td>" +
                         "<td>" + dzdia + "</td>" +
                        "<td COLSPAN='2'>" + corte + "</td>" +
                        //"<td>" + estilo + "</td>" +
                        "<td COLSPAN='2'>" + subestatus + "</td>" +
                        "<td>" + dzpagar + "</td>";

            //total denominaciones por periodo


        }
        private void CrearTablaIncentivoPenSem(DataTable dtdetalle,int filas, string Ccodigo, string semana, int tipoplanilla, int tperiodo)
        {
            DataRow[] dtniveles;
            int contador = 0;
            //se obtiene cada empleado por semana
            dtniveles = dtdetalle.Select(string.Format("[codigo_empleado]='{0}' and [semana]= '{1}'", Ccodigo.Trim(), semana));

            htmltmp += "<font size='4px'>" +
                                 "<table width='100%' border=1>" +
                                     "<tr>" +
                                            "<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Produccion</b></td>" +
                                             "<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>#MM</b></td>" +
                                            "<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Dz Dia</b></td>" +
                                            "<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Corte</b></td>" +
                                            //"<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Estilo</b></td>" +
                                            "<td COLSPAN='24' width='50%' style='margin-right:5px;' align='center'><b>Subestatus</b></td>" +
                                            "<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Dz Pagar</b></td>" +                                           
                                      "</tr>" +
                                 "</table>" +
                                 "<table width='100%' cellpadding='0' cellspacing='0'>";

            foreach (DataRow drowD in dtniveles)
            {
                string produccion = "", subestatus = "", corte = "", dzpagar = "", dzdia = "", estilo = "",modulo="";
                contador += 1;

                produccion = Convert.ToDateTime(drowD["fecha_producido"]).ToShortDateString();
                modulo = drowD["modulo"].ToString();
                subestatus = drowD["subestatus"].ToString();
                estilo = drowD["estilo"].ToString();
                corte = drowD["corte"].ToString() + "_" + drowD["seccion"].ToString() + " / " + estilo;              
                dzpagar = drowD["dzpagar"].ToString();
                dzdia = drowD["dzdia"].ToString();
              
                htmltmp += "<tr>" +
                           "<td COLSPAN='3' width='10%'>" + produccion + "</td>" +
                            "<td COLSPAN='3' width='10%'>" + modulo + "</td>" +
                            "<td COLSPAN='3' width='10%'>" + dzdia + "</td>" +
                           "<td COLSPAN='3' width='10%'>" + corte + "</td>" +
                           "<td COLSPAN='3' width='10%'>" + estilo + "</td>" +
                           "<td COLSPAN='6' width='50%'>" + subestatus + "</td>" +
                           "<td COLSPAN='3' width='10%'>" + dzpagar + "</td>" +                          
                        "</tr>";

                //total denominaciones por periodo

            }
            //totales por semana
            htmltmp += "</table>";
          
            int LimiteEspacios = 20;
           
            htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";

            //if (contador < LimiteEspacios)
            //{
                for (int i = 0; i < LimiteEspacios - contador; i++)
                {
                    //if (dtNivelesTotal.Count() <= 7)
                    //{
                        htmltmp += "<tr style='color:white;background-color=white;font-color:white;'>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                     "<td COLSPAN='3' width='10%'>blanck</td>" +
                                 "</tr>";
                   // }
                }
            //}
            htmltmp += "</table></font>";
            //aqui llamar funcion de lo pendiente
            //htmle += htmle;//dos copias del mismo codigo en la misma pagina
        }
        public string CrearHojaIncentivoHTML(int periodo,int semana, string modulo, DateTime fini)
        {
            try
            {

                //Encabezado de la orden de trabajo
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable pendiente = new DataTable();
                DataRow[] datosMod = null;
                DataRow[] pendSemMod = null;
                DataRow[] prodPago = null;
                DataRow[] eficMod = null;
                Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
                string splitmod = modulo.Split(' ')[1];

                dt1 = Neg_Incentivos.PlnObtenerEficienciaModuloByPeriodo(periodo, semana);
                eficMod = dt1.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == splitmod.Trim()).OrderBy(c => c.Field<DateTime>("fecha_producido")).ToArray();
                DateTime finioql = default(DateTime);
                DateTime ffinoql = default(DateTime);
                if (eficMod.Length > 0)
                {
                    fini = dt1.AsEnumerable().Min(c => c.Field<DateTime>("fecha_producido"));
                }
                finioql = fini.AddDays(-3.0);
                ffinoql = fini.AddDays(3.0);

                //PlnObtenerOQLModulosProd cambiar pasar rango fecha finioql , ffinoql
                DataTable oqlmod = Neg_Incentivos.PlnOqlPeriodoPagoSel(periodo);
                
                //DataTable oqlmod = Neg_Incentivos.PlnObtenerOQLModulosProd(finioql, ffinoql, 1);


                DataRow[] datosOQL = (from c in oqlmod.AsEnumerable()
                            where c.Field<string>("modulo").Trim() == splitmod.Trim()
                            select c).ToArray();


                decimal oql =0;
                string msjoql = "";
                msjoql = ((datosOQL.Length == 0) ? "-OQL" : (datosOQL.Select((DataRow c) => c.Field<decimal>("oql")).First().ToString("n2") + "%"));
                decimal montoAprobado = default(decimal);
                DataSet dsComprobante = ObtenerEstructuraComprobanteIncentivo(periodo);
                DataTable dtdetalle = dsComprobante.Tables[0];
                DataTable dtconsolidadocut = Neg_Incentivos.PlnPagoIncentivobyCutSel(periodo);
                prodPago = dtconsolidadocut.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == splitmod.Trim() && c.Field<int>("semana") == semana)
                    .GroupBy(row => new { c1 = row["fecha_producido"], c2 = row["corte"], c3 = row["seccion"] }).Select(c => c.FirstOrDefault()).OrderBy(c => c.Field<DateTime>("fecha_producido")).ToArray();
                if (prodPago.Length > 0)
                {
                    montoAprobado = Math.Round(prodPago.Where(c => c.Field<DateTime>("fecha_producido") >= fini).Sum(c => c.Field<decimal>("dzPagar") * c.Field<decimal>("costo")), 2);
                    //docenaspagarprot
                }
                decimal montoPend = 0;
                pendiente = PlnIncentivoPendPagarxPeriodoSel(periodo);
                pendSemMod = pendiente.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == splitmod.Trim() && c.Field<int>("semana") == semana)
                    .GroupBy(row => new { c1 = row["fecha_producido"], c2 = row["corte"], c3 = row["seccion"] }).Select(c => c.FirstOrDefault()).OrderBy(c => c.Field<DateTime>("fecha_producido")).ToArray();
                if (pendSemMod.Length > 0)
                {
                    montoPend = Math.Round(pendSemMod.Sum(c => c.Field<decimal>("dzpagar") * c.Field<decimal>("costo")), 2);

                }
                //wbravo
                //string htmlStyles = @"
                //    <style>
                //        body { font-family: Arial, sans-serif; font-size: 10px; }
                //        table { border-collapse: collapse; width: 95%; margin: 10px auto; }
                //        th, td { border: 1px solid #000; padding: 5px; text-align: center; }
                //        .title { text-align: center; font-size: 14px; color: #0000FF; margin-bottom: 20px; }
                //    </style>";

                dt2 = Neg_Incentivos.IncentivoHistoricoSelectDia(periodo, semana);
                datosMod = dt2.AsEnumerable().Where(c => c.Field<string>("modulo").Trim() == splitmod.Trim()).ToArray();
                DataTable dtIncDeducEmpleados = Neg_Incentivos.IncentivoIngDedccLOGxEmpleado(periodo, semana);
                //string html = "<font size='12px' color=\"#0000FF\"><b><i>Incentivo " + modulo + ", periodo " + periodo + " - COMPROBANTE DE INCENTIVOS" + "</i></b></font><br/></br>";
                string html =  @"<div style='text-align:center; margin-bottom:20px;'>
                    <font size='12px' color='#0000FF'>
                        <b><i>COMPROBANTE DE INCENTIVO</i></b>
                    </font><br/>
                    <font size='10px'>
                        <b>" + modulo + ", Período: " + periodo + ",  " + "" + @"</b>
                    </font>
                </div>";

                 //< b > " + modulo + ", Período: " + periodo + ", Semana: " + semana + @" </ b >
                   html = html + "<font size='8px'><table border='1' width='95%' style='margin-top:10px;margin-bottom:10px;'><tr><td COLSPAN='12' style='text-align:center;'>DETALLE PRODUCCION DEL MODULO</td> <td COLSPAN='3'>OQL: </td><td COLSPAN='3'> " + msjoql + "</td> </tr><tr><td COLSPAN='3'>Fecha</td><td COLSPAN='3'>Dz Aprobada</td><td COLSPAN='3'>Dz Pendiente</td><td COLSPAN='3'>Dz Proteccion</td><td COLSPAN='3'>Total</td><td COLSPAN='3'>Cumplimiento</td></tr>";
                decimal dzpagar = default(decimal);
                decimal dzpend = default(decimal);
                decimal dzprot = default(decimal);
                decimal dztotal = default(decimal);
                decimal efictotal = default(decimal);
                int conteodias = 0;
                if (eficMod.Length != 0)
                {
                    conteodias = eficMod.Length;
                    
                    foreach (DataRow dr4 in eficMod)
                    {
                        conteodias--;
                        if (Convert.ToDateTime(dr4["fecha_producido"]).DayOfWeek < DayOfWeek.Saturday)
                        {
                            dzpagar += Convert.ToDecimal(dr4["dzpagarDia"]);
                            dzpend += Convert.ToDecimal(dr4["dzpendDia"]);
                            dzprot += Convert.ToDecimal(dr4["dzprotDia"]);
                            dztotal += Convert.ToDecimal(dr4["dztotalDia"]);
                            efictotal = Convert.ToDecimal(dr4["eficienciaSem"]);
                        }
                        html = html + "<tr><td COLSPAN='3'>" + Convert.ToDateTime(dr4["fecha_producido"]).ToShortDateString() + "</td><td COLSPAN='3'>" + dr4["dzpagarDia"].ToString() + "</td><td COLSPAN='3'>" + dr4["dzpendDia"].ToString() + "</td><td COLSPAN='3'>" + dr4["dzprotDia"].ToString() + "</td><td COLSPAN='3'>" + dr4["dztotalDia"].ToString() + "</td><td COLSPAN='3'>" + dr4["eficienciaDia"].ToString() + "</td></tr>";
                        if (conteodias == 0)
                        {
                            html = html + "<tr><td COLSPAN='3'>Totales</td><td COLSPAN='3'>" + dzpagar + "</td><td COLSPAN='3'>" + dzpend + "</td><td COLSPAN='3'>" + dzprot + "</td><td COLSPAN='3'>" + dztotal + "</td><td COLSPAN='3'>" + efictotal + "</td></tr><tr><td COLSPAN='3'>Monto a Recibir</td><td COLSPAN='3'>" + montoAprobado + "</td><td COLSPAN='3'>" + montoPend + "</td><td COLSPAN='3'>_</td><td COLSPAN='3'>" + (montoAprobado + montoPend) + "</td><td COLSPAN='3'>_</td></tr>";
                        }
                    }
                }
                else
                {
                    html += "<tr><td colspan='18' style='text-align:center;'>NO HAY DATOS</td></tr>";
                }
                html += "</table></br>";
                html += "<table border='1' width='95%' style='margin-top:10px;margin-bottom:10px;'><tr><td colspan='24' style='text-align:center;'>DETALLE PRODUCCION APROBADA</td></tr><tr><td COLSPAN='3'>Fecha Prod.</td><td COLSPAN='6'>Corte/Sec</td><td COLSPAN='3'>Fecha Aprob.</td><td COLSPAN='3'>Dz Aprob + Prot</td><td COLSPAN='3'>OQL</td><td COLSPAN='3'>Precio</td><td COLSPAN='3'>Monto</td></tr>";
                decimal dzaprob = default(decimal);
                decimal montodzaprob = default(decimal);
                decimal costo = default(decimal);
                decimal dzaprocut = default(decimal);
                decimal montodzcutaprob = default(decimal);
                decimal dzaprobpend = default(decimal);
                decimal montodzapropend = default(decimal);
                int done = 0;
                if (prodPago.Length != 0)
                {
                    
                    foreach (DataRow dr3 in prodPago)
                    {
                        dzaprocut = Convert.ToDecimal(dr3["dzPagar"]);
                        costo = Convert.ToDecimal(dr3["costo"]);
                        montodzcutaprob = Convert.ToDecimal(dr3["montoPagar"]);
                        dzaprob = Math.Round(prodPago.Where((DataRow c) => c.Field<DateTime>("fecha_producido") >= fini).Sum((DataRow c) => c.Field<decimal>("dzPagar")), 2);
                        montodzaprob = Math.Round(prodPago.Where((DataRow c) => c.Field<DateTime>("fecha_producido") >= fini).Sum((DataRow c) => c.Field<decimal>("montoPagar")), 2);
                        if (Convert.ToDateTime(dr3["fecha_producido"]) >= fini && done == 0)
                        {
                            done = 1;
                            dzaprobpend = Math.Round(prodPago.Where((DataRow c) => c.Field<DateTime>("fecha_producido") < fini).Sum((DataRow c) => c.Field<decimal>("dzPagar")), 2);
                            montodzapropend = Math.Round(prodPago.Where((DataRow c) => c.Field<DateTime>("fecha_producido") < fini).Sum((DataRow c) => c.Field<decimal>("montoPagar")), 2);
                            if (dzaprobpend > 0m)
                            {
                                html = html + "<tr><td COLSPAN='12'>Total</td><td COLSPAN='3'>" + dzaprobpend + "</td><td COLSPAN='6'>_</td><td COLSPAN='3'>" + montodzapropend + "</td></tr>";
                            }
                        }
                        html = html + "<tr><td COLSPAN='3'>" + Convert.ToDateTime(dr3["fecha_producido"]).ToShortDateString() + "</td><td COLSPAN='6'>" + dr3["corte"].ToString() + "_" + dr3["seccion"].ToString() + "/" + dr3["estilo"].ToString() + "</td><td COLSPAN='3'>" + Convert.ToDateTime(dr3["fecha_aprobado"]).ToShortDateString() + "</td><td COLSPAN='3'>" + dzaprocut + "</td><td COLSPAN='3'>" + dr3["oql"].ToString() + "</td><td COLSPAN='3'>" + dr3["costo"].ToString() + "</td><td COLSPAN='3'>" + montodzcutaprob + "</td></tr>";
                    }
                    html = html + "<tr><td COLSPAN='12'>Total</td><td COLSPAN='3'>" + dzaprob + "</td><td COLSPAN='6'>_</td><td COLSPAN='3'>" + montodzaprob + "</td></tr>";
                    if (dzaprobpend > 0m)
                    {
                        html = html + "<tr><td COLSPAN='12'>GRAN TOTAL</td><td COLSPAN='3'>" + (dzaprob + dzaprobpend) + "</td><td COLSPAN='6'>_</td><td COLSPAN='3'>" + (montodzaprob + montodzapropend) + "</td></tr>";
                    }
                }
                else
                {
                    html += "<tr><td colspan='24' style='text-align:center;'>NO HAY DATOS</td></tr>";
                }
                html += "</table></br>";
                html += "<table border='1' width='95%' style='margin-top:10px;margin-bottom:10px;font-size:8px;'><tr><td colspan='51' style='text-align:center;'>DETALLE DE PAGO AL PERSONAL - PRODUCCION APROBADA</td></tr><tr><td COLSPAN='24'></td><td COLSPAN='15'>Ingresos</td><td COLSPAN='9'>Egresos</td><td COLSPAN='3'></td></tr><tr><td COLSPAN='3'>Codigo</td><td COLSPAN='9'>Nombre Completo</td><td COLSPAN='3'>Op.</td><td COLSPAN='3'>Hrs Prod.</td><td COLSPAN='3'>Dz Aprob + Prot</td><td COLSPAN='3'># Amon.</td><td COLSPAN='3'>Monto Dz</td><td COLSPAN='3'>Bono Asist.</td><td COLSPAN='3'>Bono Viernes.</td><td COLSPAN='3'>Ad/Tr</td><td COLSPAN='3'>Op.Cr</td><td COLSPAN='3'>Amon.</td><td COLSPAN='3'>HES</td><td COLSPAN='3'>Dz Menos</td><td COLSPAN='3'>Total</td></tr>";
                decimal dzpagaremp = default(decimal);
                decimal montodz = default(decimal);
                decimal deduc = default(decimal);
                decimal bono = default(decimal);
                decimal montototal = default(decimal);
                decimal hrst = default(decimal);
                decimal hrsp = default(decimal);
                decimal hrspend = default(decimal);
                decimal dzadicional = default(decimal);

                decimal dzmenos = default(decimal);
                decimal amonestaciones = default(decimal);
                decimal hes = default(decimal);
                decimal bonocalidad = default(decimal); 
                decimal bonoOperCrit = default(decimal); 
                decimal bonoviernes = default(decimal);

                DataRow[] fechapagadas = null;
                DataRow[] empleadoreg = null;
                DataRow[] empleadoingdeduc = null;
                if (datosMod.Length != 0)
                {
                    
                    foreach (DataRow dr1 in datosMod)
                    {
                        fechapagadas = (from c in dtdetalle.AsEnumerable()
                                        where c.Field<int>("codigo_empleado") == Convert.ToInt32(dr1["codigo_empleado"]) && c.Field<DateTime>("fecha_producido") >= fini
                                        select c).ToArray();
                        hrsp = (from row in fechapagadas
                                group row by new
                                {
                                    c1 = row["fecha_producido"]
                                } into c
                                select c.FirstOrDefault()).Sum((DataRow c) => c.Field<decimal>("horas"));
                        IEnumerable<DateTime> excluir = fechapagadas.Select((DataRow c) => c.Field<DateTime>("fecha_producido")).ToArray().Distinct();
                        hrspend = (from c in pendiente.AsEnumerable()
                                   where c.Field<int>("codigo_empleado") == Convert.ToInt32(dr1["codigo_empleado"]) && c.Field<DateTime>("fecha_producido") >= fini && !excluir.Contains(c.Field<DateTime>("fecha_producido"))
                                   select c into row
                                   group row by new
                                   {
                                       c1 = row["fecha_producido"]
                                   } into c
                                   select c.FirstOrDefault()).Sum((DataRow c) => c.Field<decimal>("horas"));
                        hrst = hrsp + hrspend;
                        empleadoingdeduc = (from c in dtIncDeducEmpleados.AsEnumerable()
                                            where c.Field<int>("codigo") == Convert.ToInt32(dr1["codigo_empleado"]) && (c.Field<int>("IdTipoIng") == 4 || c.Field<int>("IdTipoIng") == 29)
                                            select c).ToArray();
                        empleadoreg = (from c in empleadoingdeduc.AsEnumerable()
                                       where c.Field<int>("tipo") == 1 && c.Field<string>("detalle") == "DocenasAdicionales" 
                                       select c).ToArray();
                        dzadicional = empleadoreg.Sum((DataRow c) => c.Field<decimal>("valor"));
                       
                        empleadoreg = (from c in empleadoingdeduc.AsEnumerable()
                                       where c.Field<int>("tipo") == 1 && c.Field<string>("detalle") == "BonoViernes"
                                       select c).ToArray();
                        bonoviernes = empleadoreg.Sum((DataRow c) => c.Field<decimal>("valor"));
                        empleadoreg = (from c in empleadoingdeduc.AsEnumerable()
                                       where c.Field<int>("tipo") == 1 && c.Field<string>("detalle") == "BonoCalidad"
                                       select c).ToArray();
                        bonocalidad = empleadoreg.Sum((DataRow c) => c.Field<decimal>("valor"));
                       
                        empleadoreg = (from c in empleadoingdeduc.AsEnumerable()
                                       where c.Field<int>("tipo") == 1 && c.Field<string>("detalle") == "OpCriticaYTransporte"
                                       select c).ToArray();
                        bonoOperCrit = empleadoreg.Sum((DataRow c) => c.Field<decimal>("valor"));

                        
                        empleadoreg = (from c in empleadoingdeduc.AsEnumerable()
                                       where c.Field<int>("tipo") == 2 && (c.Field<string>("detalle") == "Amonestaciones" || c.Field<string>("detalle") == "Rechazos") && c.Field<bool>("GeneradoSistema")
                                       select c).ToArray();
                        amonestaciones = empleadoreg.Sum((DataRow c) => c.Field<decimal>("valor"));
                        empleadoreg = (from c in empleadoingdeduc.AsEnumerable()
                                       where c.Field<int>("tipo") == 2 && (c.Field<string>("detalle") == "HES" || c.Field<string>("detalle").ToLower() == "hferiado (-)") && c.Field<bool>("GeneradoSistema")
                                       select c).ToArray();
                        hes = empleadoreg.Sum((DataRow c) => c.Field<decimal>("valor"));
                        empleadoreg = (from c in empleadoingdeduc.AsEnumerable()
                                       where c.Field<int>("tipo") == 2 && c.Field<string>("detalle") == "DocenasMenos"
                                       select c).ToArray();
                        dzmenos = empleadoreg.Sum((DataRow c) => c.Field<decimal>("valor"));
                        dzpagaremp += Convert.ToDecimal(dr1["dzpagar"]);
                        montodz += Convert.ToDecimal(dr1["incentivo"]);
                        deduc += Convert.ToDecimal(dr1["deducciones"]);
                        bono += Convert.ToDecimal(dr1["bonoasistencia"]);
                        montototal += Convert.ToDecimal(dr1["TotalIncentivo"]);
                        html = html + "<tr><td COLSPAN='3'>" + dr1["codigo_empleado"].ToString() + "</td><td COLSPAN='9'>" + dr1["nombrecompleto"].ToString().Split(' ')[0] + " " + dr1["nombrecompleto"].ToString().Split(' ')[2];
                        html = html + "</td><td COLSPAN='3'>" + dr1["operacion"].ToString() + "</td><td COLSPAN='3'>" + hrst + "</td><td COLSPAN='3'>" + ((int)((decimal)dr1["dzpagar"]));
                        html = html + "</td><td COLSPAN='3'>" + dr1["amonestaciones"].ToString() + "</td><td COLSPAN='3'>" + decimal.ToInt32(((decimal)dr1["incentivo"])).ToString();
                        html = html + "</td><td COLSPAN='3' align='right'>" + decimal.ToInt32(((decimal)dr1["bonoasistencia"])).ToString() + "</td><td COLSPAN='3' align='right'>" + ((int)bonoviernes);
                        html = html + "</td><td COLSPAN='3' align='right'>" + ((int)dzadicional) + "</td><td COLSPAN='3' align='right'>" + ((int)bonoOperCrit);
                        html = html + "</td><td COLSPAN='3' align='right'>" + amonestaciones + "</td><td COLSPAN='3'>" + hes + "</td><td COLSPAN='3' align='right'>" + dzmenos;
                        html = html + "</td><td COLSPAN='3' align='right'>" + decimal.ToInt32(((decimal)dr1["TotalIncentivo"])).ToString() + "</td></tr>";
                    }
                    html = html + "<tr><td COLSPAN='24'>Totales</td><td COLSPAN='3'>" + montodz + "</td><td COLSPAN='3'>" + bono + "</td><td COLSPAN='3'>_</td><td COLSPAN='3'>_</td><td COLSPAN='3'>" + 0 + "</td><td COLSPAN='3'>_</td><td COLSPAN='3'>_</td><td COLSPAN='3'>" + montototal + "</td></tr>";
                }
                else
                {
                    html += "<tr><td colspan='48' style='text-align:center;'>NO HAY DATOS</td></tr>";
                }
                html += "</table></br>";
                html += "<table border='1' width='95%' style='margin-top:10px;margin-bottom:10px;'><tr><td colspan='36' style='text-align:center;'>DETALLE PRODUCCION PENDIENTE DE PAGO - ESTATUS</td></tr><tr><td COLSPAN='3'>Fecha</td><td COLSPAN='6'>Corte/Seccion</td><td COLSPAN='9'>Color</td><td COLSPAN='6'>Subestatus</td><td COLSPAN='3'>Dz Pend + Prot</td><td COLSPAN='3'>OQL</td><td COLSPAN='3'>Precio</td><td COLSPAN='3'>Monto</td></tr>";
                decimal dzpendpagar = default(decimal);
                decimal montodzcutpend = default(decimal);
                decimal dzpendcut = default(decimal);
                decimal montodzpend = default(decimal);
                decimal dzantpendpagar = default(decimal);
                decimal montodzpendant = default(decimal);
                int done2 = 0;
                if (pendSemMod.Length != 0)
                {
                    DataRow[] array4 = pendSemMod;
                    foreach (DataRow dr2 in array4)
                    {
                        dzpendcut = Convert.ToDecimal(dr2["dzpagar"]);
                        costo = Convert.ToDecimal(dr2["costo"]);
                        montodzcutpend = Math.Round(dzpendcut * costo, 2);
                        dzpendpagar = Math.Round(pendSemMod.Where((DataRow c) => c.Field<DateTime>("fecha_producido") >= fini).Sum((DataRow c) => c.Field<decimal>("dzpagar")), 2);
                        montodzpend = Math.Round(pendSemMod.Where((DataRow c) => c.Field<DateTime>("fecha_producido") >= fini).Sum((DataRow c) => c.Field<decimal>("dzpagar") * c.Field<decimal>("costo")), 2);
                        if (Convert.ToDateTime(dr2["fecha_producido"]) >= fini && done2 == 0)
                        {
                            done2 = 1;
                            dzantpendpagar = Math.Round(pendSemMod.Where((DataRow c) => c.Field<DateTime>("fecha_producido") < fini).Sum((DataRow c) => c.Field<decimal>("dzpagar")), 2);
                            montodzpendant = Math.Round(pendSemMod.Where((DataRow c) => c.Field<DateTime>("fecha_producido") < fini).Sum((DataRow c) => c.Field<decimal>("dzpagar") * c.Field<decimal>("costo")), 2);
                            if (dzantpendpagar > 0m)
                            {
                                html = html + "<tr><td COLSPAN='24'>Total</td><td COLSPAN='3'>" + dzantpendpagar + "</td><td COLSPAN='6'>_</td><td COLSPAN='3'>" + montodzpendant + "</td></tr><tr>";
                            }
                        }
                        html = html + "<tr><td COLSPAN='3'>" + Convert.ToDateTime(dr2["fecha_producido"]).ToShortDateString() + "</td><td COLSPAN='6'>" + dr2["corte"].ToString() + "_" + dr2["seccion"].ToString() + "/" + dr2["estilo"].ToString() + "</td><td COLSPAN='9'>" + dr2["color"].ToString() + "</td><td COLSPAN='6'>" + dr2["subestatus"].ToString() + "</td><td COLSPAN='3'>" + dzpendcut + "</td><td COLSPAN='3'>" + dr2["oql"].ToString() + "</td><td COLSPAN='3'>" + dr2["costo"].ToString() + "</td><td COLSPAN='3'>" + montodzcutpend + "</td></tr>";
                    }
                    html = html + "<tr><td COLSPAN='24'>Total</td><td COLSPAN='3'>" + dzpendpagar + "</td><td COLSPAN='6'>_</td><td COLSPAN='3'>" + montodzpend + "</td></tr><tr>";
                    if (dzantpendpagar > 0m)
                    {
                        html = html + "<tr><td COLSPAN='24'>Total</td><td COLSPAN='3'>" + (dzpendpagar + dzantpendpagar) + "</td><td COLSPAN='6'>_</td><td COLSPAN='3'>" + (montodzpend + montodzpendant) + "</td></tr><tr>";
                    }
                }
                else
                {
                    html += "<tr><td colspan='36' style='text-align:center;'>NO HAY DATOS</td></tr>";
                }
                return html + "</table></font>";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string CrearHojaViaticoHTML(int periodo, DateTime ini, DateTime fin, DataTable detalleemp, bool colillaYviatico)
        {
            try
            {
                string Ccodigo = "";
                string Cnombre = "";
                string Cimpresion = "";
                string Cdepto = "";
                string ocultarFilas = "style='color:white;background-color=white;font-color:white;'";
                DataRow[] empleado = (from c in detalleemp.AsEnumerable()
                                      group c by new
                                      {
                                          c2 = c["codigo_empleado"]
                                      } into grp
                                      select grp.First() into c
                                      orderby c.Field<string>("nombre_depto")
                                      select c).ToArray();
                DataRow[] array = empleado;
                foreach (DataRow dr in array)
                {
                    htmltmp = "";
                    Ccodigo = dr["codigo_empleado"].ToString();
                    Cnombre = dr["nombrecompleto"].ToString();
                    Cdepto = dr["nombre_depto"].ToString();
                    Cimpresion = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    decimal granTotal = default(decimal);

                    DataTable desgloce = (from c in detalleemp.AsEnumerable()
                                          where c.Field<int>("codigo_empleado") == Convert.ToInt32(Ccodigo)
                                          select c).CopyToDataTable();

                    granTotal = desgloce.AsEnumerable().Sum((DataRow c) => c.Field<decimal>("total"));
                    ocultarFilas = ((granTotal > 0m && colillaYviatico) ? "" : ocultarFilas);
                    if (!(granTotal == 0m) || colillaYviatico)
                    {
                        string Lcodigo = "Empleado:";
                        string LImpresion = "Impresion:";
                        string LDepto = "Departamento:";
                        char pad = ' ';
                        Lcodigo.PadRight(8, pad);
                        LDepto.PadRight(8, pad);
                        htmltmp = htmltmp + "<font size='24px'; align='center'; color=\"#000000\"><b><i>Colilla de Viatico de Alimentacion y Transporte</i></b></font><br/><font size='4px'><table border='0' cellpadding='0' cellspacing='0'><tr><td width='50%'>" + LDepto + "&nbsp;" + Cdepto + "</td><td >" + LImpresion + "&nbsp;" + Cimpresion + "</td></tr><tr><td width='50%'>" + Lcodigo + "&nbsp;" + Ccodigo + " - " + Cnombre + "</td><td >&nbsp;</td></tr>";
                        if (periodo < 400)
                        {
                            htmltmp = htmltmp + "<tr><td width='50%'>S" + Math.Round(Convert.ToDecimal(dr["saldo"])) + "</td><td >&nbsp;</td></tr>";
                        }
                        htmltmp += "</table></font>";
                        CrearTablaSemViatico(periodo, desgloce, ini.ToShortDateString(), fin.ToShortDateString(), colillaYviatico);
                        htmlv += htmltmp;
                        if (!colillaYviatico)
                        {
                            htmlv += "<table width='100%' cellpadding='0' cellspacing='0'><tr style='color:white;background-color=white;font-color:white;'><td COLSPAN='42'>blanck</td></tr></table>";
                        }
                    }
                }
                return htmlv;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string CrearHojaViaticoHTML14nal(int periodo, DateTime ini, DateTime fin, DataTable detalleemp, bool colillaYviatico, int periodo2, DataTable detalleemp2, DateTime ini2, DateTime fin2, bool colillaYviatico2)
        {
            try
            {
                string Ccodigo = "";
                string Cnombre = "";
                string Cimpresion = "";
                string Cdepto = "";
                string ocultarFilas = "style='color:white;background-color=white;font-color:white;'";
                DataRow[] empleado = (from c in detalleemp.AsEnumerable()
                                      group c by new
                                      {
                                          c2 = c["codigo_empleado"]
                                      } into grp
                                      select grp.First() into c
                                      orderby c.Field<string>("nombre_depto")
                                      select c).ToArray();
                DataRow[] array = empleado;
                foreach (DataRow dr in array)
                {
                    htmltmp = "";
                    Ccodigo = dr["codigo_empleado"].ToString();
                    Cnombre = dr["nombrecompleto"].ToString();
                    Cdepto = dr["nombre_depto"].ToString();
                    Cimpresion = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    decimal granTotal = default(decimal);
                    DataTable desgloce = (from c in detalleemp.AsEnumerable()
                                          where c.Field<int>("codigo_empleado") == Convert.ToInt32(Ccodigo)
                                          select c).CopyToDataTable();
                    granTotal = desgloce.AsEnumerable().Sum((DataRow c) => c.Field<decimal>("total"));
                    ocultarFilas = ((granTotal > 0m && colillaYviatico) ? "" : ocultarFilas);
                    if (!(granTotal == 0m) || colillaYviatico)
                    {
                        string Lcodigo = "Empleado:";
                        string LImpresion = "Impresion:";
                        string LDepto = "Departamento:";
                        char pad = ' ';
                        Lcodigo.PadRight(8, pad);
                        LDepto.PadRight(8, pad);
                        htmltmp = htmltmp + "<font size='24px'; align='center'; color=\"#000000\"><b><i>Colilla de Viatico de Alimentacion y Transporte</i></b></font><br/><font size='4px'><table border='0' cellpadding='0' cellspacing='0'><tr><td width='50%'>" + LDepto + "&nbsp;" + Cdepto + "</td><td >" + LImpresion + "&nbsp;" + Cimpresion + "</td></tr><tr><td width='50%'>" + Lcodigo + "&nbsp;" + Ccodigo + " - " + Cnombre + "</td><td >&nbsp;</td></tr>";
                        if (periodo < 400)
                        {
                            htmltmp = htmltmp + "<tr><td width='50%'>S" + Math.Round(Convert.ToDecimal(dr["saldo"])) + "</td><td >&nbsp;</td></tr>";
                        }
                        htmltmp += "</table></font>";
                        CrearTablaSemViatico14nal(periodo, desgloce, detalleemp2, ini.ToShortDateString(), fin.ToShortDateString(), colillaYviatico);
                        htmlv += htmltmp;
                        if (!colillaYviatico)
                        {
                            htmlv += "<table width='100%' cellpadding='0' cellspacing='0'><tr style='color:white;background-color=white;font-color:white;'><td COLSPAN='42'>blanck</td></tr></table>";
                        }
                    }
                }
                return htmlv;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ObtenerPersonalPagoViatico(int periodo, int semana, DateTime ini, DateTime fin, int filtro, int valorFiltro, string modulo, DataTable codigos)
        {
            try
            {
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable pendiente = new DataTable();
                DataRow[] datosInc = null;
                DataRow[] datosMarca = null;
                int[] codemp = null;
                Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
                Neg_Marca Neg_Marca = new Neg_Marca();
                DataTable dtInD = new DataTable();
                dt2 = ((periodo >= 400) ? Neg_Incentivos.ObtenerIncentivoPlantaVATSel(periodo, semana) : Neg_Incentivos.ObtenerIncentivoPlantaSel(periodo, semana));
                dtInD = Neg_Marca.ObtenerMarcasHorasOficial(ini, fin, 2, 3, 0);
                switch (filtro)
                {
                    case 3:
                        datosInc = (from c in dt2.AsEnumerable()
                                    where c.Field<string>("modulo").Trim() == valorFiltro.ToString()
                                    select c).ToArray();
                        break;
                    case 2:
                        datosInc = ((valorFiltro <= 0) ? dt2.AsEnumerable().ToArray() : (from c in dt2.AsEnumerable()
                                                                                         where c.Field<int>("codigo_empleado") == valorFiltro
                                                                                         select c).ToArray());
                        break;
                    default:
                        codemp = (from u in codigos.AsEnumerable()
                                  select u.Field<int>("codigo_empleado")).ToArray();
                        datosInc = (from c in dt2.AsEnumerable()
                                    where codemp.Contains(c.Field<int>("codigo_empleado"))
                                    select c).ToArray();
                        break;
                }
                if (filtro > 1)
                {
                    codemp = datosInc.Select((DataRow u) => u.Field<int>("codigo_empleado")).ToArray();
                }
                datosMarca = (from c in dtInD.AsEnumerable()
                              where codemp.Contains(c.Field<int>("codigo_empleado"))
                              select c).ToArray();
                DataTable detalleemp = new DataTable();

                return Neg_Incentivos.ObtenerDesgloceViatico(datosInc, ini, datosMarca, periodo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // TODO: VHPO
        // Se  crea colilla de pago de viaticos y transporte
        private void CrearTablaSemViatico(int periodo, DataTable desgloce, string ini, string fin, bool colillaYviatico)
        {
            if (colillaYviatico)
            {
                htmltmp += "<font size='4px'><table width='100%' border=1 cellpadding='0' cellspacing='0'>";
            }
            else
            {
                htmltmp += "<font size='4px'><table width='100%' border=1>";
            }
            for (int j = 0; j < desgloce.Rows.Count; j++)
            {
                DataRow dr = desgloce.Rows[j];
                if (j == 0)
                {
                    htmltmp += "<tr><td COLSPAN='3' ROWSPAN='2' width='10%' align='center'>Fecha</td><td COLSPAN='6' width='10%' align='center'>Desayuno</td><td COLSPAN='6' width='10%' align='center'>Almuerzo</td><td COLSPAN='6' width='10%' align='center'>Refrigerio</td><td COLSPAN='6' width='10%' align='center'>Cena</td><td COLSPAN='3' width='10%' align='center'>Transporte</td><td COLSPAN='3' width='10%' align='center'>Total</td></tr>";
                    htmltmp += "<tr><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%'></td></tr>";
                }
                htmltmp = htmltmp + "<tr><td COLSPAN='3' width='10%'>" + Convert.ToDateTime(dr["fecha"]).ToShortDateString() + "</td><td COLSPAN='3' width='10%' align='center'>" + ((j < desgloce.Rows.Count - 1 && Convert.ToDecimal(dr["desayuno"]) > 0m) ? "1" : "") + "</td><td COLSPAN='3' width='10%' align='center'>" + dr["desayuno"]?.ToString() + "</td><td COLSPAN='3' width='10%' align='center'>" + ((j < desgloce.Rows.Count - 1 && Convert.ToDecimal(dr["almuerzo"]) > 0m) ? "1" : "") + "</td><td COLSPAN='3' width='10%' align='center'>" + dr["almuerzo"]?.ToString() + "</td><td COLSPAN='3' width='10%' align='center'>" + ((j < desgloce.Rows.Count - 1 && Convert.ToDecimal(dr["refrigerio"]) > 0m) ? "1" : "") + "</td><td COLSPAN='3' width='10%' align='center'>" + dr["refrigerio"]?.ToString() + "</td><td COLSPAN='3' width='10%' align='center'>" + ((j < desgloce.Rows.Count - 1 && Convert.ToDecimal(dr["cena"]) > 0m) ? "1" : "") + "</td><td COLSPAN='3' width='10%' align='center'>" + dr["cena"]?.ToString() + "</td><td COLSPAN='3' width='10%' align='center'>" + dr["transporte"]?.ToString() + "</td><td COLSPAN='3' width='10%' align='center'>" + dr["total"]?.ToString() + "</td></tr>";
            }
            htmltmp += "</table>";
            htmltmp += ObtenerFormatoTotalViatico(periodo, desgloce, colillaYviatico);
            htmltmp = htmltmp + "<table width='95%';font size='4px';align='center' cellpadding='0' cellspacing='0'><tr><td colspan='42'>Confirmo que los datos arriba detallados corresponden al uso del viatico de alimentacion y transporte otorgado para el periodo comprendido entre el " + ini + " al " + fin + "</td></tr></table>";
            if (!colillaYviatico)
            {
                htmltmp += "<table width='95%';font size='4px';border='0' align='right' >";
            }
            else
            {
                htmltmp += "<table width='95%';font size='4px';border='0' align='right' cellpadding='0' cellspacing='0'>";
            }
            string datofecha = "<td align='left'>________________________</td>";
            if (periodo >= 400)
            {
                datofecha = "<td align='left' style='text-decoration: underline;'>" + Convert.ToDateTime(fin).AddDays(1.0).ToShortDateString() + "</td>";
            }
            htmltmp = htmltmp + "<tr><td>Fecha:</td>" + datofecha + "<td>Recibe Conforme F.:</td><td align='left'>________________________</td></tr></table>";
            int LimiteEspacios = 6;
            if (colillaYviatico)
            {
                LimiteEspacios = 2;
            }
            htmltmp += "<p></p><p></p><p></p><p></p>";
            htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";
            for (int i = 0; i < LimiteEspacios; i++)
            {
                htmltmp += "<tr style='color:white;background-color=white;font-color:white;'><td COLSPAN='42'>blanck</td></tr>";
            }
            htmltmp += "</table></font>";
        }


        // temporal pasar a catorcenal
        private void CrearTablaSemViatico14nal(int periodo, DataTable desgloce, DataTable desgloce2, string ini, string fin, bool colillaYviatico)
        {
            if (colillaYviatico)
            {
                htmltmp += "<font size='4px'><table width='100%' border=1 cellpadding='0' cellspacing='0'>";
            }
            else
            {
                htmltmp += "<font size='4px'><table width='100%' border=1>";
            }

            // Encabezado de las tablas
            htmltmp += "<tr>";
            htmltmp += "<td colspan='3' align='center'>Fecha</td>";
            htmltmp += "<td colspan='6' align='center'>Desayuno</td>";
            htmltmp += "<td colspan='6' align='center'>Almuerzo</td>";
            htmltmp += "<td colspan='6' align='center'>Refrigerio</td>";
            htmltmp += "<td colspan='6' align='center'>Cena</td>";
            htmltmp += "<td colspan='3' align='center'>Transporte</td>";
            htmltmp += "<td colspan='3' align='center'>Total</td>";

            // Encabezado para desgloce2
            htmltmp += "<td colspan='3' align='center'>Fecha</td>";
            htmltmp += "<td colspan='6' align='center'>Desayuno</td>";
            htmltmp += "<td colspan='6' align='center'>Almuerzo</td>";
            htmltmp += "<td colspan='6' align='center'>Refrigerio</td>";
            htmltmp += "<td colspan='6' align='center'>Cena</td>";
            htmltmp += "<td colspan='3' align='center'>Transporte</td>";
            htmltmp += "<td colspan='3' align='center'>Total</td>";

            htmltmp += "</tr>";

            // Segunda fila del encabezado
            htmltmp += "<tr>";
            for (int i = 0; i < 2; i++) // Dos secciones: desgloce y desgloce2
            {
                for (int j = 0; j < 4; j++) // Cantidad y C$ para cada comida
                {
                    for (int k = 0; k < 2; k++) // Cantidad y C$ (Cantidad primero)
                    {
                        if (j == 0) // Cantidad
                            htmltmp += "<td colspan='3' align='center'>Cantidad</td>";
                        else // C$
                            htmltmp += "<td colspan='3' align='center'>C$</td>";
                    }
                }
                // Transporte y Total
                htmltmp += "<td colspan='3' align='center'>Cantidad</td><td colspan='3' align='center'>C$</td>";
            }

            htmltmp += "</tr>";

            // Datos de desgloce
            for (int j = 0; j < desgloce.Rows.Count; j++)
            {
                DataRow dr = desgloce.Rows[j];
                htmltmp += "<tr><td>" + Convert.ToDateTime(dr["fecha"]).ToShortDateString() + "</td>" +
                           "<td>" + ((Convert.ToDecimal(dr["desayuno"]) > 0m) ? "1" : "") + "</td>" +
                           "<td>" + dr["desayuno"]?.ToString() + "</td>" +
                           // ... Agrega el resto de las columnas como en tu código original
                           "</tr>";

                // Datos de desgloce2
                if (j < desgloce2.Rows.Count)
                {
                    DataRow dr2 = desgloce2.Rows[j];
                    htmltmp += "<tr><td>" + Convert.ToDateTime(dr2["fecha"]).ToShortDateString() + "</td>" +
                               "<td>" + ((Convert.ToDecimal(dr2["desayuno"]) > 0m) ? "1" : "") + "</td>" +
                               "<td>" + dr2["desayuno"]?.ToString() + "</td>" +
                               // ... Agrega el resto de las columnas como en tu código original
                               "</tr>";
                }
                else
                {
                    // Si no hay más filas en desgloce2, agrega celdas vacías para mantener la alineación
                    htmltmp += "<tr><td></td><td></td><td></td></tr>";
                }
            }

            // Cierra la tabla y añade el formato total y demás elementos como en tu código original.

            htmltmp += "</table>";
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="periodo"></param>
        /// <param name="desgloce"></param>
        /// <param name="colillaYviatico"></param>
        /// <returns></returns>
        private string ObtenerFormatoTotalViatico(int periodo, DataTable desgloce, bool colillaYviatico)
        {
            string cadena = "";
            cadena = (colillaYviatico ? (cadena + "<table width='95%';font size='4px';border='1' align='right' cellpadding='0' cellspacing='0'>") : (cadena + "<table width='95%';font size='4px';border='0' align='right' >"));
            cadena = ((periodo >= 400) ? (cadena + "<tr><td COLSPAN='27'></td><td COLSPAN='6' align='right'>Subtotal </td><td COLSPAN='6' align='left'>C$</td><td COLSPAN='3' width='10%'>" + (Convert.ToDecimal(desgloce.Rows[0]["viatico_total"]) - Convert.ToDecimal(desgloce.Rows[0]["saldo"])) + "</td></tr><tr><td COLSPAN='33' align='right'>Reembolso sujeto a rendición </td><td COLSPAN='6' align='left'>C$</td><td COLSPAN='3' width='10%'>" + Convert.ToDecimal(desgloce.Rows[0]["saldo"]) + "</td></tr><tr><td COLSPAN='27'></td><td COLSPAN='6' align='right'>TOTAL </td><td COLSPAN='6' align='left'>C$</td><td COLSPAN='3' width='10%'>" + Convert.ToDecimal(desgloce.Rows[0]["viatico_total"]) + "</td></tr>") : (cadena + "<tr><td COLSPAN='36'></td><td COLSPAN='3'>TOTAL</td><td COLSPAN='3' width='10%'>" + (Convert.ToDecimal(desgloce.Rows[0]["viatico_total"]) - Convert.ToDecimal(desgloce.Rows[0]["saldo"])) + "</td></tr>"));
            return cadena + "</table>";
        }
        private string CrearTablaVaciaViatico()
        {
            htmlv = "";
            htmltmp = "";
            htmltmp += "<font size='24px'; align='center'; color=\"#FFFFFF\"><b><i>Colilla de Viatico de Alimentacion y Transporte</i></b></font><br/><font size='4px'><table border='0' cellpadding='0' cellspacing='0' style='color:white;background-color=white;font-color:white;'><tr><td width='50%'>Departamento:&nbsp;-</td><td >Impresion:&nbsp;-</td></tr><tr><td width='50%'>Codigo:&nbsp;-</td><td >&nbsp;</td></tr><tr><td width='50%'>S0</td><td >&nbsp;</td></tr></table></font>";
            htmltmp += "<font size='4px'><table width='100%' cellpadding='0' cellspacing='0' style='color:white;background-color=white;font-color:white;'>";
            for (int j = 0; j < 7; j++)
            {
                if (j == 0)
                {
                    htmltmp += "<tr><td COLSPAN='3' ROWSPAN='2' width='10%' align='center'>Fecha</td><td COLSPAN='6' width='10%' align='center'>Desayuno</td><td COLSPAN='6' width='10%' align='center'>Almuerzo</td><td COLSPAN='6' width='10%' align='center'>Refrigerio</td><td COLSPAN='6' width='10%' align='center'>Cena</td><td COLSPAN='3' width='10%' align='center'>Transporte</td><td COLSPAN='3' width='10%' align='center'>Total</td></tr>";
                    htmltmp += "<tr><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>Cantidad</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%' align='center'>C$</td><td COLSPAN='3' width='10%'></td></tr>";
                }
                htmltmp += "<tr><td COLSPAN='3' width='10%'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td><td COLSPAN='3' width='10%' align='center'>-</td></tr>";
            }
            htmltmp += "</table>";
            htmltmp += "<table width='95%';font size='4px';border='0' align='right' cellpadding='0' cellspacing='0' style='color:white;background-color=white;font-color:white;'>";
            htmltmp += "<tr><td COLSPAN='36'></td><td COLSPAN='3'>TOTAL</td><td COLSPAN='3' width='10%'>-</td></tr></table>";
            htmltmp += "<table width='95%';font size='4px';align='center' cellpadding='0' cellspacing='0' style='color:white;background-color=white;font-color:white;'><tr><td colspan='42'>Confirmo que los datos arriba detallados corresponden al uso del viatico de alimentacion y transporte otorgado para el periodo comprendido entre el dd/MM/yyyy al dd/MM/yyyy</td></tr></table>";
            htmltmp += "<table width='95%';font size='4px';border='0' align='right' cellpadding='0' cellspacing='0' style='color:white;background-color=white;font-color:white;'>";
            htmltmp += "<tr><td>Fecha:</td><td align='left'>_______________________</td><td>Recibe Conforme F.:</td><td align='left'>________________________</td></tr></table>";
            int LimiteEspacios = 2;
            htmltmp += "<p></p><p></p><p></p><p></p>";
            htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";
            for (int i = 0; i < LimiteEspacios; i++)
            {
                htmltmp += "<tr style='color:white;background-color=white;font-color:white;'><td COLSPAN='42'>blanck</td></tr>";
            }
            htmltmp += "</table></font>";
            htmlv += htmltmp;
            return htmlv;
        }
    }
}
