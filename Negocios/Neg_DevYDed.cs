using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Negocios
{
    public class Neg_DevYDed
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_DevYDed Dato_DevYDed = new Dato_DevYDed();
        Dato_Planilla Dato_Planilla = new Dato_Planilla();
        #endregion
        public DataSet Deducciones()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.SeleccionarDeducciones(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet Devengados()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.SeleccionarDevengados(userDetail.getIDEmpresa());
            return ds;
        }

        public bool DeduccionesEditar(string deduccion, bool activo, int idDeduc, bool aplicaagui, bool aplicavac, string prioridad, bool mostrarc, bool deducibruto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DeduccionesEditar(deduccion, activo, idDeduc, aplicaagui, aplicavac, prioridad, mostrarc, deducibruto, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeduccionesEliminar(int idDeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DeduccionesEliminar(idDeduc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeduccionesEmpleadoEliminar(int IdDeduccion, int codEmpleado, int periodo, int semana, int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DeduccionesEmpleadoEliminar(IdDeduccion, codEmpleado, periodo, semana, id, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IngresosEmpleadoEliminar(int ID, int codEmpleado, int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.IngresosEmpleadoEliminar(ID, codEmpleado, periodo, semana, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool DeduccioesEmpleadoEditar(int IdDeduccion, decimal total, int codEmpleado, int periodo, int semana, int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DeduccionesEmpleadoEditar(IdDeduccion, total, codEmpleado, periodo, semana, id, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //retornar cuota incompleta
        public bool plnRetornarCuotaIncompleta(int periodo, int id, int codigo, int semana, decimal egreso)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.plnRetornarCuotaIncompleta(periodo, id, codigo, semana, egreso, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool plnAplicarEgresoCuotaParc(int periodo, int id, int codigo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.plnAplicarEgresoCuotaParc(periodo, id, codigo, semana, userDetail.getIDEmpresa()))
            {
                return true;
            }
            return false;
        }
        public bool IngresosEmpleadoEditar(int ID, decimal total, int codEmpleado, int periodo, int semana, int ingresoAsociado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.IngresosEmpleadoEditar(ID, total, codEmpleado, periodo, semana, ingresoAsociado, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeduccionesAgregar(string deduccion, bool activo, bool aplicaagui, bool aplicavac, string prioridad, bool mostrarc, bool deducibruto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DeduccionesAgregar(deduccion, activo, aplicaagui, aplicavac, prioridad, mostrarc, deducibruto, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DevengadosAgregar(string devengado, bool inss, bool ir, bool liquidacion, bool aplicadeduc, bool mostrarc, bool deducibruto, int ingasoc, int deducasoc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DevengadosAgregar(devengado, inss, ir, liquidacion, aplicadeduc, mostrarc, deducibruto, ingasoc, deducasoc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool devengadosEditar(int Id, string deveng, bool inss, bool ir, bool Liq, bool aplicadeduc, bool mostrarc, bool deducibruto, int ingasoc, int deducasoc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.devengadosEditar(Id, deveng, inss, ir, Liq, aplicadeduc, mostrarc, deducibruto, ingasoc, deducasoc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DevengadosEliminar(int idDeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DevengadosEliminar(idDeduc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataSet cargarDeducciones()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.SeleccionarDeducciones(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet cargarDeduccionesEspeciales()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.SeleccionarDeduccionesEspeciales(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet cargarDeduccionesRecurrentes()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.SeleccionarDeduccionesRecurrentes(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet cargarIngresos(int filtro)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.cargarIngresos(filtro, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet cargarIngresosAplicaDia()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.cargarIngresosAplicaDia(userDetail.getIDEmpresa());
            return ds;
        }
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool AsignarDeduccionesEmpleado(int codEmpleado, int periodo, int tipoDeduccion, decimal total, decimal cuotas, string fecsol, string fecaut, string user, int porc, int recurrente, int periodovalidez, string factiva, string fexpira, string coment)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.AsignarDeduccionesOrdinariasEmpleado(codEmpleado, periodo, tipoDeduccion, total, cuotas, fecsol, fecaut, user, porc, recurrente, periodovalidez, factiva, fexpira, coment, userDetail.getIDEmpresa());

            return true;
        }
        public bool IngresosEspecialesEmpleadoIns(int codEmpleado, int periodo, int tipoIng, decimal total, string user, int recurrente, string coment)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.IngresosEspecialesEmpleadoIns(codEmpleado, periodo, tipoIng, total, user, recurrente, coment, userDetail.getIDEmpresa());

            return true;
        }
        public bool SalarioEspecialEmpleadoIns(int codEmpleado, decimal salariop, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.SalarioEspecialEmpleadoIns(codEmpleado, salariop, user, userDetail.getIDEmpresa());

            return true;
        }
        //DEDUCCIONES ESPECIALES FUERA DE PLANILLA
        public bool AsignardeduccionesEspecialesIns(int periodo, int tipoDeduccion, decimal total, decimal cuotas, string user, int porc, int recurrente, int tipo, int filtro1, int filtro2, int excemb, int excefec)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.AsignardeduccionesEspecialesIns(periodo, tipoDeduccion, total, cuotas, user, porc, recurrente, tipo, filtro1, filtro2, excemb, excefec, userDetail.getIDEmpresa());

            return true;
        }
        public bool AsignardeduccionesEspecialesUpd(int periodo, int tipoDeduccion, decimal total, decimal cuotas, string user, int porc, int recurrente, int tipo, int filtro1, int filtro2, int excemb, int excefec)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.AsignardeduccionesEspecialesUpd(periodo, tipoDeduccion, total, cuotas, user, porc, recurrente, tipo, filtro1, filtro2, excemb, excefec, userDetail.getIDEmpresa());

            return true;
        }
        public bool AsignarIngresosEspecialesIns(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.AsignarIngresosEspecialesIns(periodo, userDetail.getIDEmpresa());

            return true;
        }
        public bool DeduccionesExternasLogIns(int codEmpleado, decimal sobregiro, string tiposobregiro, int tipodeduc, string usuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.DeduccionesExternasLogIns(codEmpleado, sobregiro, tiposobregiro, tipodeduc, usuario, userDetail.getIDEmpresa());

            return true;
        }
        //CREADO POR GRETHEL TERCERO 24/10/2016
        public bool deduccionesSaldoPendEmpleadoIns(int codEmpleado, int periodo, int tipoDeduccion, decimal total, decimal cuotas, string fecsol, string fecaut, string user, int porc, int recurrente)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //Verifico si existe una deduccion para ese empleado y ese periodo
            //int deducucion = Dato_DevYDed.verificarExiste(codEmpleado, periodo, tipoDeduccion, userDetail.getIDEmpresa());

            Dato_DevYDed.deduccionesSaldoPendEmpleadoIns(codEmpleado, periodo, tipoDeduccion, total, cuotas, fecsol, fecaut, user, porc, recurrente, userDetail.getIDEmpresa());

            return true;
        }
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool AsignarDeduccionesRecurrentes(int codEmpleado, int tipoDeduccion, bool especial, decimal total, bool porcentual, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.AsignarDeduccionesRecurrentes(codEmpleado, tipoDeduccion, especial, total, porcentual, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public DataSet DeduccionesOrdinariasObtener(int codEmpleado, int mostrar, int saldo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.DeduccionesOrdinariasObtener(codEmpleado, mostrar, saldo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet IngresosEspecialesSel(int codEmpleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.IngresosEspecialesSel(codEmpleado, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable CalcularDetalleDeduccionesxEmp(int codigo, int vista, int saldo, decimal ingreso, DateTime fechacorte, int idempresa)
        {

            DataSet dd = Dato_DevYDed.DeduccionesOrdinariasObtener(codigo, vista, saldo, idempresa);//Obtengo el detalle de la deduccion  
            DataTable dtEgresos = new DataTable();
            dtEgresos.Columns.Add("id");
            dtEgresos.Columns.Add("deduccionNombre");
            dtEgresos.Columns.Add("valorCuotas");
            dtEgresos.Columns.Add("Total");
            dtEgresos.Columns.Add("Debe");
            dtEgresos.Columns.Add("porcentual");
            dtEgresos.Columns.Add("recurrente");
            dtEgresos.Columns.Add("tipoDeduc");
            dtEgresos.Columns.Add("fecreg");
            dtEgresos.Columns.Add("fechaultimopago");
            dtEgresos.Columns.Add("aplicavalidez");
            dtEgresos.Columns.Add("fechaexpira");
            dtEgresos.Columns.Add("fechaactiva");

            decimal monto = 0;

            for (int i = 0; i < dd.Tables[0].Rows.Count; i++)
            {
                if (!(dd.Tables[0].Rows[i]["porcentual"].ToString().ToLower() == "si"))
                {
                    monto = ((!(dd.Tables[0].Rows[i]["recurrente"].ToString().ToLower() == "si") || !(Convert.ToDecimal(dd.Tables[0].Rows[i]["debe"]) > 0m)) ? Convert.ToDecimal(dd.Tables[0].Rows[i]["Debe"]) : Convert.ToDecimal(dd.Tables[0].Rows[i]["valorCuotas"]));
                }
                else
                {
                    decimal cuotaporc = ingreso * Convert.ToDecimal(dd.Tables[0].Rows[i]["valorCuotas"]) / 100m;
                    if (Convert.ToDecimal(dd.Tables[0].Rows[i]["debe"]) == 0m)
                    {
                        monto = cuotaporc;
                    }
                    else if (Convert.ToDecimal(dd.Tables[0].Rows[i]["debe"]) > 0m)
                    {
                        monto = Convert.ToDecimal(dd.Tables[0].Rows[i]["debe"]);
                    }
                }
                if (Convert.ToInt32(dd.Tables[0].Rows[i]["deduccionIbruto"]) == 1)
                {
                    monto = default(decimal);
                }
                if (Convert.ToBoolean(dd.Tables[0].Rows[i]["aplicavalidez"]))
                {
                    DateTime factiva = Convert.ToDateTime(dd.Tables[0].Rows[i]["fechaactiva"]);
                    DateTime fexpira = Convert.ToDateTime(dd.Tables[0].Rows[i]["fechaexpira"]);
                    if (fechacorte >= factiva && fechacorte < fexpira)
                    {
                        dtEgresos.Rows.Add(dd.Tables[0].Rows[i]["id"].ToString(), dd.Tables[0].Rows[i]["deduccionNombre"].ToString(), dd.Tables[0].Rows[i]["valorCuotas"].ToString(), dd.Tables[0].Rows[i]["Total"].ToString(), monto.ToString(), dd.Tables[0].Rows[i]["porcentual"].ToString(), dd.Tables[0].Rows[i]["recurrente"].ToString(), dd.Tables[0].Rows[i]["tipoDeduc"].ToString(), dd.Tables[0].Rows[i]["fecreg"], dd.Tables[0].Rows[i]["fechaultimopago"], dd.Tables[0].Rows[i]["aplicavalidez"], dd.Tables[0].Rows[i]["fechaexpira"], dd.Tables[0].Rows[i]["fechaactiva"]);
                    }
                }
                else
                {
                    dtEgresos.Rows.Add(dd.Tables[0].Rows[i]["id"].ToString(), dd.Tables[0].Rows[i]["deduccionNombre"].ToString(), dd.Tables[0].Rows[i]["valorCuotas"].ToString(), dd.Tables[0].Rows[i]["Total"].ToString(), monto.ToString(), dd.Tables[0].Rows[i]["porcentual"].ToString(), dd.Tables[0].Rows[i]["recurrente"].ToString(), dd.Tables[0].Rows[i]["tipoDeduc"].ToString(), dd.Tables[0].Rows[i]["fecreg"], dd.Tables[0].Rows[i]["fechaultimopago"], dd.Tables[0].Rows[i]["aplicavalidez"], dd.Tables[0].Rows[i]["fechaexpira"], dd.Tables[0].Rows[i]["fechaactiva"]);
                }
            }
            return dtEgresos;
        }
                    
        public DataSet DeduccionesOrdinariasObtenerxTipo(int codEmpleado, int tipodeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.DeduccionesOrdinariasObtenerxTipo(codEmpleado, tipodeduc, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet IngresosPeriodoxTipoSel(int codEmpleado, int periodo, int semana, int iddeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.IngresosPeriodoxTipoSel(codEmpleado, periodo, semana, iddeduc, userDetail.getIDEmpresa());
            return ds;
        }
        //AGREGADO POR GRETHEL TERCERO 24/10/2016
        public DataSet ObtenerDeduccionesVigentes(int codigo, int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.ObtenerDeduccionesVigentes(codigo, periodo, userDetail.getIDEmpresa());
            return ds;
        }
        public bool IngresosPeriodoAplicaIBrutoSel(int codEmpleado, int periodo, int iddeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.IngresosPeriodoAplicaIBrutoSel(codEmpleado, periodo, iddeduc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool DeduccionOrdinariaEliminar(int ID)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.DeduccionOrdinariaEliminar(ID, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditarMarcasxEmpleado(int codigo, DateTime fecha, string horaE, string horaS, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.EditarMarcasxEmpleado(codigo, fecha, horaE, horaS, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool EditarDeduccionesxEmpleado(int ID, decimal total, decimal cuotas, string user)//(int codEmple, decimal cuotas, string tipoDeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //if (Dato_DevYDed.EditarDeduccionesxEmpleado(codEmple, cuotas, tipoDeduc, userDetail.getIDEmpresa()))
            if (Dato_DevYDed.EditarDeduccionesxEmpleado(ID, total, cuotas, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool EditarIngresosEspxEmpleado(int ID, decimal total,int estado,string user)//(int codEmple, decimal cuotas, string tipoDeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //if (Dato_DevYDed.EditarDeduccionesxEmpleado(codEmple, cuotas, tipoDeduc, userDetail.getIDEmpresa()))
            if (Dato_DevYDed.EditarIngresosEspxEmpleado(ID, total,  user,estado, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool IngresosEmpAplicaDeducIBrutoDel(int codigo, int periodo, int iddeduc)//(int codEmple, decimal cuotas, string tipoDeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //if (Dato_DevYDed.EditarDeduccionesxEmpleado(codEmple, cuotas, tipoDeduc, userDetail.getIDEmpresa()))
            if (Dato_DevYDed.IngresosEmpAplicaDeducIBrutoDel(codigo, periodo, iddeduc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool DeshabilitarDeuda(int ID)//(int codEmpleado, string idDeduccion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            //if (Dato_DevYDed.DeshabilitarDeuda(codEmpleado, idDeduccion, userDetail.getIDEmpresa()))
            if (Dato_DevYDed.DeshabilitarDeuda(ID, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        //MODIFICADO POR GRETHEL TERCERO 24/10/2016
        public bool AsignarIngresoOdeduccionPorEmpleado(int codEmpleado, int periodo, int semana, decimal valor, int tipoAsignacion, int IngrsDeduc, int tipoPlanilla, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.AsignarIngresoOdeduccionPorEmpleado(codEmpleado, periodo, semana, valor, tipoAsignacion, IngrsDeduc, userDetail.getIDEmpresa(), tipoPlanilla, user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataSet cargarIngresosLiq()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.cargarIngresosLiq(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet DeduccionEspecialesActivas()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.DeduccionEspecialesActivas(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet AdelantosEspecialesActivos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.AdelantosEspecialesActivos(userDetail.getIDEmpresa());
            return ds;
        }
        //AGREGADO POR WBRAVO
        public dsPlanilla.dtIngDedDataTable IngresosxPeriodoSel(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.IngresosxPeriodoSel(periodo, semana, userDetail.getIDEmpresa());
        }
        public dsPlanilla.dtIngDedDataTable IngresosxPeriodoSelAll(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.IngresosxPeriodoSelAll(periodo, userDetail.getIDEmpresa());
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPeriodoSelAll(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.EgresosxPeriodoSelAll(periodo, userDetail.getIDEmpresa());
        }
        public dsPlanilla.dtIngDedDataTable BeneficiosxPeriodoSel(int periodo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.BeneficiosxPeriodoSel(periodo, userDetail.getIDEmpresa());
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPeriodoSel(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.EgresosxPeriodoSel(periodo, semana, userDetail.getIDEmpresa());
        }
        public dsPlanilla.dtEgresosDataTable EgresosEspecialesxPeriodoSel(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.EgresosEspecialesxPeriodoSel(periodo, semana, userDetail.getIDEmpresa());
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPrestacionesSel(int periodo, int tprestacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.EgresosxPrestacionesSel(periodo, tprestacion, userDetail.getIDEmpresa());
        }
        public dsPlanilla.dtEgresosDataTable EgresosxPrestacionesSelxEmpleado(int periodo, int tprestacion, int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.EgresosxPrestacionesSelxEmpleado(periodo, tprestacion, codigo, userDetail.getIDEmpresa());
        }
        public DataSet verificarIngresocnDeduccionIBruto(int idDevengado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_DevYDed.verificarIngresocnDeduccionIBruto(idDevengado, userDetail.getIDEmpresa());
            return ds;
        }
        public int verificarDeduccionIBruto(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int existe = 0;
            existe = Dato_DevYDed.verificaDeduccionIBruto(codigo, userDetail.getIDEmpresa());
            return existe;
        }
        public int verificaDeduccionPrioridad(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int existe = 0;
            existe = Dato_DevYDed.verificaDeduccionPrioridad(codigo, userDetail.getIDEmpresa());
            return existe;
        }
        public int PlnValidarPersonalAplicaProteccion(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int existe = 0;
            return Dato_DevYDed.PlnValidarPersonalAplicaProteccion(codigo, userDetail.getIDEmpresa());
        }
        public int verificarAplicaDeduccionIBruto(int idDeduc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int existe = 0;
            existe = Dato_DevYDed.verificarAplicaDeduccionIBruto(idDeduc, userDetail.getIDEmpresa());
            return existe;
        }
        public bool InsertarIngrDeduc(int tipo, int codEmpleado, int nSemana, int idTipo, int periodo, decimal valor, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.InsertarIngrDeduc(tipo, codEmpleado, nSemana, idTipo, periodo, valor, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool PlnRegistrarRubrosIncentivoIns(int tipo, int codEmpleado, int nSemana, int idTipo, int periodo, decimal valor, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.PlnRegistrarRubrosIncentivoIns(tipo, codEmpleado, nSemana, idTipo, periodo, valor, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            return false;
        }
        public bool plnAplicarPlanillaIncentivo(int tipo, int nSemana, int idTipo, int periodo,  string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.plnAplicarPlanillaIncentivo(tipo,  nSemana, idTipo, periodo, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool ActualizarSalarioHorasExtras(int periodo, int nSemana, bool consolidar)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.ActualizarSalarioHorasExtras(periodo, nSemana, consolidar, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool IngresosAplicaIBrutoBakIns(int tipo, int codEmpleado, int nSemana, int idTipo, int periodo, decimal valor)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.IngresosAplicaIBrutoBakIns(tipo, codEmpleado, nSemana, idTipo, periodo, valor, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool ReestablecerIngresosIBruto(int codigo, int periodo, int iddeduc)
        {
            try
            {
                int existe = verificarAplicaDeduccionIBruto(iddeduc);
                if (existe > 0)
                {
                    DataTable dt1 = new DataTable();
                    DataSet ds = new DataSet();

                    if (!IngresosEmpAplicaDeducIBrutoDel(codigo, periodo, iddeduc))
                    {
                        throw new Exception("Error al eliminar deduccion con ingreso asociado");

                    }
                    if (!IngresosPeriodoAplicaIBrutoSel(codigo, periodo, iddeduc))
                    {
                        throw new Exception("Error al reestablecer ingreso aplicables a deduccion");

                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }
        public bool RegistrarIngresoEgresoAsociado(int periodo, int semana, DataTable dt1, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int codigo = 0;
            try
            {
                dsPlanilla.dtEgresosDataTable dtEgresos = EgresosEspecialesxPeriodoSel(periodo, semana);
                Neg_Periodo NPeriodo = new Neg_Periodo();
                dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(periodo);
                dsPlanilla.dtDeduccionCuotaDataTable dtIngDed = new dsPlanilla.dtDeduccionCuotaDataTable();
                decimal valorIngresoAsociado = 0;
                decimal totalIngresoAsociado = 0;
                for (int i = 0; i < dtEgresos.Rows.Count; i++)
                {
                    decimal valorEgreso = 0;
                    decimal valorIngresoR = 0;
                    decimal debe = 0;
                    decimal valorOriginal = 0;
                    for (int iG = 0; iG < dt1.Rows.Count; iG++)
                    {
                        int idDeduccionIBruto = dtEgresos[i].iddeduccionibruto;
                        if (Convert.ToInt32(dt1.Rows[iG][1]) == dtEgresos[i].codigo_empleado && dtEgresos[i].tipoingrdeduc == idDeduccionIBruto)
                        {
                            semana = Convert.ToInt32(dt1.Rows[iG][2]);
                            valorIngresoR = Math.Ceiling(Convert.ToDecimal(dt1.Rows[iG][5]));
                            valorEgreso = Convert.ToDecimal(dtEgresos[i].valor);
                            debe = dtEgresos[i].debe;
                            valorIngresoAsociado = (Convert.ToBoolean(dtEgresos[i].porcentual) ? (valorIngresoR * Convert.ToDecimal(valorEgreso / 100m)) : ((debe > 0m && valorIngresoR >= 100m && valorEgreso <= valorIngresoR && valorEgreso <= debe) ? valorEgreso : ((debe > 0m && valorIngresoR >= 100m && valorIngresoR <= valorEgreso && valorIngresoR <= debe) ? valorIngresoR : ((!(debe > 0m) || !(valorIngresoR >= 100m) || !(debe < valorEgreso)) ? default(decimal) : debe))));
                            if (!Convert.ToBoolean(dtEgresos[i].porcentual))
                            {
                                dtEgresos[i].debe = debe - valorIngresoAsociado;
                            }
                            dt1.Rows[iG][5] = Convert.ToDecimal(dt1.Rows[iG][5]) - valorIngresoAsociado;
                            totalIngresoAsociado += valorIngresoAsociado;
                            dsPlanilla.dtDeduccionCuotaRow NewdtIng = dtIngDed.NewdtDeduccionCuotaRow();
                            NewdtIng.id = 0;
                            NewdtIng.id_tipo = Convert.ToInt32(dt1.Rows[iG][0]);
                            NewdtIng.codigo_empleado = Convert.ToInt32(dt1.Rows[iG][1]);
                            NewdtIng.tipoingrdeduc = Convert.ToInt32(dt1.Rows[iG][3]);
                            NewdtIng.periodo = periodo;
                            NewdtIng.semana = semana;
                            NewdtIng.egreso = 0m;
                            NewdtIng.modalidad = "DU";
                            NewdtIng.saldo = 0m;
                            NewdtIng.ingresodisponible = 0m;
                            NewdtIng.valor = Math.Ceiling(Convert.ToDecimal(dt1.Rows[iG][5]));
                            dtIngDed.AdddtDeduccionCuotaRow(NewdtIng);
                        }
                        else if (dtEgresos[i].codigo_empleado < Convert.ToInt32(dt1.Rows[iG][1]))
                        {
                            break;
                        }
                    }
                    if (totalIngresoAsociado > 0m)
                    {
                        dsPlanilla.dtDeduccionCuotaRow NewdtIng = dtIngDed.NewdtDeduccionCuotaRow();
                        NewdtIng.id = dtEgresos[i].id;
                        NewdtIng.id_tipo = 1;
                        NewdtIng.codigo_empleado = dtEgresos[i].codigo_empleado;
                        NewdtIng.tipoingrdeduc = dtEgresos[i].idingresoasociado;
                        NewdtIng.periodo = periodo;
                        NewdtIng.semana = semana;
                        NewdtIng.egreso = 0m;
                        NewdtIng.modalidad = "DU";
                        NewdtIng.saldo = 0m;
                        NewdtIng.ingresodisponible = 0m;
                        NewdtIng.valor = totalIngresoAsociado;
                        dtIngDed.AdddtDeduccionCuotaRow(NewdtIng);
                        totalIngresoAsociado = 0;
                    }
                }
                return RegistrarIngresosAsocCuota(dtIngDed, user);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " " + codigo);
            }
        }
        public bool RegistrarIngresosAsocCuota(dsPlanilla.dtDeduccionCuotaDataTable dtIng, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                for (int i = 0; i < dtIng.Rows.Count; i++)
                {
                    if (dtIng[i].valor > 0 && dtIng[i].id!=0)
                    {
                        if (!Dato_Planilla.ActualizarEstadoCuentaDeudaEmp(dtIng[i].id, 2, dtIng[i].codigo_empleado, dtIng[i].tipoingrdeduc, dtIng[i].periodo, dtIng[i].valor, 1, userDetail.getIDEmpresa()))
                        {
                            throw new Exception("Hubo problemas al deducir cuota");
                        }
                    }
                    if (!InsertarIngrDeduc(dtIng[i].id_tipo, dtIng[i].codigo_empleado, dtIng[i].semana, dtIng[i].tipoingrdeduc, dtIng[i].periodo, dtIng[i].valor, user))
                    {
                        throw new Exception("Error al insertar ingreso");
                    }
                  
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
        public bool IngresosIncentivoIBrutoEliminar(int periodo, int semana, int tipoingr)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.IngresosIncentivoIBrutoEliminar(periodo, semana, tipoingr, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool IngresosIncentivoIBrutoEliminarxEmp(int periodo, int semana, int tipoingr, int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_DevYDed.IngresosIncentivoIBrutoEliminarxEmp(periodo, semana, tipoingr, codigo, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public DataTable ObtenerIngresocnDeduccionIBruto()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.ObtenerIngresocnDeduccionIBruto(userDetail.getIDEmpresa());
        }
        //AGREGADO PARA VAC FUERA DE PLANILLA
        public dsPlanilla.dtEgresosDataTable EgresosxPrestacionesFechaSelxEmpleado(string fecaut, int tprestacion, int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.EgresosxPrestacionesFechaSelxEmpleado(fecaut, tprestacion, codigo, userDetail.getIDEmpresa());
        }
        //nuevo
        public DataTable plnObtenerRubros()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.plnObtenerRubros(userDetail.getIDEmpresa());
        }
        public DataTable ObtenerDetalleHorasExtrasxFecha(int filtro,int periodo,DateTime fechaini,DateTime fechafin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_DevYDed.ObtenerDetalleHorasExtrasxFecha(filtro,periodo,fechaini,fechafin,userDetail.getIDEmpresa());
        }
        //procesar horas extas
        public void ProcesarHorasExtrasxPeriodo(int periodo,int semana,int tplanilla,DateTime fechainicio, DateTime fechafin)
        {
            try
            {
                Neg_Periodo Neg_Periodo = new Neg_Periodo();
                Neg_Planilla Neg_Planilla = new Neg_Planilla();
                //todos los registros con el periodo actual
                //en caso que la aprobacion sea diario y en fecha actual se debe cambiar rango fechas por cada semana del periodo
                DataTable dthe = ObtenerDetalleHorasExtrasxFecha(2, periodo, DateTime.Now, DateTime.Now);            
                DataRow[] dt;

                if (semana==1)
                {
                    dt = dthe.Select("fecha<='" + fechafin + "' and id_tipo = 1");//fechas anteriores y las que corresponden a la semana 
                }else
                {
                    dt = dthe.Select("fecha>='" + fechainicio + "' and fecha<='" + fechafin + "' and id_tipo = 1");//fechas periodo para la semana
                }
                DataTable tiempo = new DataTable();                          
                decimal horas = 0;
                int tipo = 0;

                if (dt.Length>0) //distinct empleado
                {
                    tiempo = dt.GroupBy(row => new { c2 = row["codigo_empleado"], c3 = row["tipoingrdeduc"] }).Select(grp => grp.First()).CopyToDataTable();
                }              
              
                foreach (DataRow dr in tiempo.Rows)
                {
                    if (Convert.ToInt32(dr["codigo_empleado"])== 868063)
                    {

                    }
                    horas = dt.Where(grp => Convert.ToInt32(grp["codigo_empleado"]) == Convert.ToInt32(dr["codigo_empleado"]) && Convert.ToInt32(grp["tipoingrdeduc"]) == Convert.ToInt32(dr["tipoingrdeduc"])).Sum(c => Convert.ToDecimal(c["tiempo"]));
                    tipo = Convert.ToInt32(dr["tipoingrdeduc"]);
                    if (!Neg_Planilla.insertarHorasExtras(Convert.ToInt32(dr["codigo_empleado"]), periodo, semana,tipo, Convert.ToDecimal(horas), tplanilla))
                    {
                        throw new Exception("Error al registrar horas extras del periodo por empleado");
                    }
                }
            }
            catch ( Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }
    }
}
