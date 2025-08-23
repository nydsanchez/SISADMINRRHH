using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;
using System.Reflection;
using Datos;


namespace Negocios
{
    public class Neg_Incentivos : System.Web.UI.Page
    {
        #region VARIABLES PUBLICAS
        int NdiasL = 0, NdiasJ = 0;
        float TotalHorasLaboradas, TotalHoraSemana;
        DataTable Ndt = new DataTable();
        DataTable Ndtc1 = new DataTable();
        DataTable Ndtc2 = new DataTable();
        public const decimal MetaProduccionBase = 200m;
       
        public int idconstruccion = 0;
        public decimal dzpagar = 0;
        public DateTime fecha_producido = new DateTime();
        public decimal valor = default(decimal);
        #endregion

        #region CLASES OBJETOS

        public class IncentivoEmp
        {
            private string _modulo;

            public string Modulo
            {
                get { return _modulo; }
                set { _modulo = value; }
            }
            private decimal metasModulo;

            public decimal MetasModulo
            {
                get { return metasModulo; }
                set { metasModulo = value; }
            }
            private int _estilo;

            public int Estilo
            {
                get { return _estilo; }
                set { _estilo = value; }
            }
            private int _codigo;

            public int Codigo
            {
                get { return _codigo; }
                set { _codigo = value; }
            }
            private string _nombreCompleto;

            public string NombreCompleto
            {
                get { return _nombreCompleto; }
                set { _nombreCompleto = value; }
            }
            private string _construccion;

            public string Construccion
            {
                get { return _construccion; }
                set { _construccion = value; }
            }
            private decimal _produccion;

            public decimal Produccion
            {
                get { return _produccion; }
                set { _produccion = value; }
            }
            private string _proceso;

            public string Proceso
            {
                get { return _proceso; }
                set { _proceso = value; }
            }
            private decimal _meta;

            public decimal Meta
            {
                get { return _meta; }
                set { _meta = value; }
            }
            private decimal _incentivo;

            public decimal Incentivo
            {
                get { return _incentivo; }
                set { _incentivo = value; }
            }

            private decimal diasLaborales;

            public decimal DiasLaborales
            {
                get { return diasLaborales; }
                set { diasLaborales = value; }
            }
            private decimal diasLaborados;

            public decimal DiasLaborados
            {
                get { return diasLaborados; }
                set { diasLaborados = value; }
            }

            private decimal diasJustificados;

            public decimal DiasJustificados
            {
                get { return diasJustificados; }
                set { diasJustificados = value; }
            }

            private decimal diasInjustificados;

            public decimal DiasInjustificados
            {
                get { return diasInjustificados; }
                set { diasInjustificados = value; }
            }
            private decimal diasAusencias;

            public decimal DiasAusencias
            {
                get { return diasAusencias; }
                set { diasAusencias = value; }
            }

            private string operacion;

            public string Operacion
            {
                get { return operacion; }
                set { operacion = value; }
            }

            private decimal total;

            public decimal Total
            {
                get { return total; }
                set { total = value; }
            }


            private float horasaunsencia;

            public float Horasaunsencia
            {
                get { return horasaunsencia; }
                set { horasaunsencia = value; }
            }
            private float procAusencia;

            public float ProcAusencia
            {
                get { return procAusencia; }
                set { procAusencia = value; }
            }

            private decimal eficiencia;

            public decimal Eficiencia
            {
                get { return eficiencia; }
                set { eficiencia = value; }
            }
            private int amonestaciones;

            public int Amonestaciones
            {
                get { return amonestaciones; }
                set { amonestaciones = value; }
            }

            private int rechazosInternos;

            public int RechazosInternos
            {
                get { return rechazosInternos; }
                set { rechazosInternos = value; }
            }
            private int rechazos;

            public int Rechazos
            {
                get { return rechazos; }
                set { rechazos = value; }
            }
            private int rechazosExternos;

            public int RechazosExternos
            {
                get { return rechazosExternos; }
                set { rechazosExternos = value; }
            }
            private decimal deduccionPorcentual;

            public decimal DeduccionPorcentual
            {
                get { return deduccionPorcentual; }
                set { deduccionPorcentual = value; }
            }
            private decimal deduccionNumerica;

            public decimal DeduccionNumerica
            {
                get { return deduccionNumerica; }
                set { deduccionNumerica = value; }
            }

            private decimal ingresoPorcentual;

            public decimal IngresoPorcentual
            {
                get { return ingresoPorcentual; }
                set { ingresoPorcentual = value; }
            }
            private decimal ingresoNumerico;

            public decimal IngresoNumerico
            {
                get { return ingresoNumerico; }
                set { ingresoNumerico = value; }
            }


            private decimal ingreso;

            public decimal Ingreso
            {
                get { return ingreso; }
                set { ingreso = value; }
            }

            private decimal deduccion;

            public decimal Deduccion
            {
                get { return deduccion; }
                set { deduccion = value; }
            }

            private string detalleIngreso;

            public string DetalleIngreso
            {
                get { return detalleIngreso; }
                set { detalleIngreso = value; }
            }
            private string detalleEgreso;

            public string DetalleEgreso
            {
                get { return detalleEgreso; }
                set { detalleEgreso = value; }
            }

            private int tipoIngr;
            public int TipoIngr
            {
                get { return tipoIngr; }
                set { tipoIngr = value; }
            }

            private string comentario;

            public string Comentario
            {
                get { return comentario; }
                set { comentario = value; }
            }

            internal object[] GetCustomAttributes(bool p)
            {
                throw new NotImplementedException();
            }
        }
        public class Incentivos
        {
            private string construccion;

            public string Construccion
            {
                get { return construccion; }
                set { construccion = value; }
            }

            private decimal metadia;

            public decimal Metadia
            {
                get { return metadia; }
                set { metadia = value; }
            }
            private decimal meta5;

            public decimal Meta5
            {
                get { return meta5; }
                set { meta5 = value; }
            }
            private decimal incentivo;

            public decimal Incentivo
            {
                get { return incentivo; }
                set { incentivo = value; }
            }
            private int layout;

            public int Layout
            {
                get { return layout; }
                set { layout = value; }
            }
            private decimal monto;

            public decimal Monto
            {
                get { return monto; }
                set { monto = value; }
            }
            private string proceso;

            public string Proceso
            {
                get { return proceso; }
                set { proceso = value; }
            }

            private decimal eficiencia;

            public decimal Eficiencia
            {
                get { return eficiencia; }
                set { eficiencia = value; }
            }
        }

        public class ProdXmod
        {
            private string modulo;

            public string Modulo
            {
                get { return modulo; }
                set { modulo = value; }
            }
            private int coddep;

            public int Coddep
            {
                get { return coddep; }
                set { coddep = value; }
            }

            private string construccion;

            public string Construccion
            {
                get { return construccion; }
                set { construccion = value; }
            }
            private decimal produccion;

            public decimal Produccion
            {
                get { return produccion; }
                set { produccion = value; }
            }

            private int estiloP;

            public int EstiloP
            {
                get { return estiloP; }
                set { estiloP = value; }
            }


        }

        public class EmpleadoOp
        {
            private int codigo_empleado;

            public int Codigo_empleado
            {
                get { return codigo_empleado; }
                set { codigo_empleado = value; }
            }

            private string operacion;

            public string Operacion
            {
                get { return operacion; }
                set { operacion = value; }
            }
            private string descripcion;

            public string Descripcion
            {
                get { return descripcion; }
                set { descripcion = value; }
            }
            private bool multiTarea;

            public bool MultiTarea
            {
                get { return multiTarea; }
                set { multiTarea = value; }
            }
        }

        public class PIngDeducInc
        {
            private int id;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            private int idTipo;

            public int IdTipo
            {
                get { return idTipo; }
                set { idTipo = value; }
            }

            private string tipo;

            public string Tipo
            {
                get { return tipo; }
                set { tipo = value; }
            }

            private int idParametro;

            public int IdParametro
            {
                get { return idParametro; }
                set { idParametro = value; }
            }
            private string nombre;

            public string Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }

            private int tipocalculo;

            public int Tipocalculo
            {
                get { return tipocalculo; }
                set { tipocalculo = value; }
            }

            private string calc;

            public string Calc
            {
                get { return calc; }
                set { calc = value; }
            }

            private decimal valor;

            public decimal Valor
            {
                get { return valor; }
                set { valor = value; }
            }

            private string asignarValor;

            public string AsignarValor
            {
                get { return asignarValor; }
                set { asignarValor = value; }
            }

            private string condicionOP;

            public string CondicionOP
            {
                get { return condicionOP; }
                set { condicionOP = value; }
            }
            private int condicion;

            public int Condicion
            {
                get { return condicion; }
                set { condicion = value; }
            }

            private bool dependencias;

            public bool Dependencias
            {
                get { return dependencias; }
                set { dependencias = value; }
            }
            private bool variantes;

            public bool Variantes
            {
                get { return variantes; }
                set { variantes = value; }
            }

            private int idvariante;

            public int Idvariante
            {
                get { return idvariante; }
                set { idvariante = value; }
            }

            private string nombreV;

            public string NombreV
            {
                get { return nombreV; }
                set { nombreV = value; }
            }

        }
        public class IngDedDep
        {

            private int idIngDedP;

            public int IdIngDedP
            {
                get { return idIngDedP; }
                set { idIngDedP = value; }
            }

            private int idParamDep;

            public int IdParamDep
            {
                get { return idParamDep; }
                set { idParamDep = value; }
            }

            private string nombre;

            public string Nombre
            {
                get { return nombre; }
                set { nombre = value; }
            }

            private int idTipoCalc;

            public int IdTipoCalc
            {
                get { return idTipoCalc; }
                set { idTipoCalc = value; }
            }
            private decimal valor;

            public decimal Valor
            {
                get { return valor; }
                set { valor = value; }
            }

            private decimal condicion;

            public decimal Condicion
            {
                get { return condicion; }
                set { condicion = value; }
            }

            private string condicionOP;

            public string CondicionOP
            {
                get { return condicionOP; }
                set { condicionOP = value; }
            }

            private bool variante;

            public bool Variante
            {
                get { return variante; }
                set { variante = value; }
            }

            private string nombreV;

            public string NombreV
            {
                get { return nombreV; }
                set { nombreV = value; }
            }



        }

        public class DetalleComprobante
        {

            private dsPlanilla.dtIngresosDeduccIncentivosDataTable dtIDInce = new dsPlanilla.dtIngresosDeduccIncentivosDataTable();

            public dsPlanilla.dtIngresosDeduccIncentivosDataTable DtIDInce
            {
                get { return dtIDInce; }
                set { dtIDInce = value; }
            }

            private string codigo;

            public string Codigo
            {
                get { return codigo; }
                set { codigo = value; }
            }

            private int periodo;

            public int Periodo
            {
                get { return periodo; }
                set { periodo = value; }
            }

            private int semana;

            public int Semana
            {
                get { return semana; }
                set { semana = value; }
            }

            private string nombrecompleto;

            public string Nombrecompleto
            {
                get { return nombrecompleto; }
                set { nombrecompleto = value; }
            }

            private string modulo;

            public string Modulo
            {
                get { return modulo; }
                set { modulo = value; }
            }

            string operacion;

            public string Operacion
            {
                get { return operacion; }
                set { operacion = value; }
            }
            private int estilo;

            public int Estilo
            {
                get { return estilo; }
                set { estilo = value; }
            }

            private string contruccion;

            public string Contruccion
            {
                get { return contruccion; }
                set { contruccion = value; }
            }

            private decimal produccion;

            public decimal Produccion
            {
                get { return produccion; }
                set { produccion = value; }
            }

            private decimal metaalcanzada;

            public decimal Metaalcanzada
            {
                get { return metaalcanzada; }
                set { metaalcanzada = value; }
            }

            private decimal eficienciaalcanzada;

            public decimal Eficienciaalcanzada
            {
                get { return eficienciaalcanzada; }
                set { eficienciaalcanzada = value; }
            }

            private decimal incentivoMeta;

            public decimal IncentivoMeta
            {
                get { return incentivoMeta; }
                set { incentivoMeta = value; }
            }

            private decimal totalIngreso;

            public decimal TotalIngreso
            {
                get { return totalIngreso; }
                set { totalIngreso = value; }
            }

            private decimal totalEgreso;

            public decimal TotalEgreso
            {
                get { return totalEgreso; }
                set { totalEgreso = value; }
            }

            private decimal totalIncentivo;

            public decimal TotalIncentivo
            {
                get { return totalIncentivo; }
                set { totalIncentivo = value; }
            }

            private string usuarioGraba;

            public string UsuarioGraba
            {
                get { return usuarioGraba; }
                set { usuarioGraba = value; }
            }

            private DateTime fechagraba;

            public DateTime Fechagraba
            {
                get { return fechagraba; }
                set { fechagraba = value; }
            }

            private TimeSpan horaG;
            public TimeSpan HoraG
            {
                get { return horaG; }
                set { horaG = value; }
            }
        }
        #endregion

        public DataTable MontoTotalIncentivos(DateTime fechaini, DateTime fechafin)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.MontoTotalIncentivos(fechaini, fechafin);
        }

        public DataTable PRODUCCIONXMODULO(DateTime fechaini, DateTime fechafin)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.PRODUCCIONXMODULO(fechaini, fechafin);
        }

        public DataTable PRODUCCIONXMODULOXDIA(DateTime fechaini, DateTime fechafin, int modulo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.PRODUCCIONXMODULOXDIA(fechaini, fechafin, modulo);
        }
        public DataTable CosProduccionporDiaRangoFecha(DateTime fechaini, DateTime fechafin)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.CosProduccionporDiaRangoFecha(fechaini, fechafin);
        }

        public DataTable TrasladoDZEfectivos(int periodo, int semana)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.TrasladoDZEfectivos(periodo, semana);
        }

        public DataTable incentivos()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.incentivos();
        }

        public DataTable obtenerModulos()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.obtenerModulos();
        }

        public List<ProdXmod> PRODUCCIONXMODULOList(DateTime fechaini, DateTime fechafin)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            DataTable ds = datoI.PRODUCCIONXMODULO(fechaini, fechafin);
            List<ProdXmod> lpm = new List<ProdXmod>();
            foreach (DataRow row in ds.Rows)
            {
                ProdXmod pm = new ProdXmod();

                pm.Modulo = row["modulo"].ToString();
                pm.Coddep = int.Parse(row["codigoDepto"].ToString());
                pm.EstiloP = int.Parse(row["estilo"].ToString());
                pm.Construccion = row["construccion"].ToString();
                pm.Produccion = decimal.Parse(row["produccion"].ToString());

                lpm.Add(pm);
            }
            return lpm;
        }
        public List<Incentivos> IncentivosList()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();

            DataTable ds = datoI.incentivos();
            List<Incentivos> li = new List<Incentivos>();
            foreach (DataRow row in ds.Rows)
            {
                Incentivos i = new Incentivos();
                i.Construccion = row["construcion"].ToString();
                i.Metadia = decimal.Parse(row["MetaDia"].ToString());
                i.Meta5 = decimal.Parse(row["meta_wk_5"].ToString());
                i.Incentivo = decimal.Parse(row["incentivo_wk_5"].ToString());
                i.Layout = int.Parse(row["layout"].ToString());
                i.Monto = decimal.Parse(row["Monto"].ToString());
                i.Proceso = row["Proceso"].ToString();
                i.Eficiencia = decimal.Parse(row["eficiencia"].ToString());
                li.Add(i);

            }
            return li;
        }
        public List<EmpleadoOp> EmpleadoOperacion()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();

            DataTable ds = datoI.EmpleadoOperacion();
            List<EmpleadoOp> li = new List<EmpleadoOp>();
            foreach (DataRow row in ds.Rows)
            {
                EmpleadoOp i = new EmpleadoOp();
                i.Codigo_empleado = int.Parse(row["codigo_empleado"].ToString());
                i.Operacion = row["operacion"].ToString();
                i.Descripcion = row["descripcion"].ToString();
                i.MultiTarea = Convert.ToBoolean(row["multitarea"].ToString());

                li.Add(i);

            }
            return li;
        }

        public List<PIngDeducInc> ParametosIngresosDeduccionesIncentivos(int tipoDeduccion)
        {

            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();

            DataTable ds = datoI.ParametosIngresosDeduccionesIncentivos(tipoDeduccion);
            List<PIngDeducInc> li = new List<PIngDeducInc>();
            foreach (DataRow row in ds.Rows)
            {
                PIngDeducInc i = new PIngDeducInc();

                i.Id = int.Parse(row["id"].ToString());
                i.IdTipo = int.Parse(row["idTipo"].ToString());
                i.Tipo = row["tipo"].ToString();
                i.Nombre = row["Nombre"].ToString();
                i.IdParametro = int.Parse(row["idParametro"].ToString());
                i.Tipocalculo = int.Parse(row["IdTipoCalculo"].ToString());
                i.Calc = row["calc"].ToString();
                i.Valor = decimal.Parse(row["valor"].ToString());
                i.Condicion = int.Parse(row["condicion"].ToString());
                i.Dependencias = bool.Parse(row["Dependencias"].ToString());
                i.Variantes = bool.Parse(row["Variantes"].ToString());
                i.Idvariante = int.Parse(row["idVariante"].ToString());
                i.NombreV = row["NombreVariante"].ToString();
                i.CondicionOP = row["CondicionOp"].ToString();
                i.AsignarValor = row["AsignarValor"].ToString();

                li.Add(i);


            }
            return li;
        }
        public List<IngDedDep> ParametosIngresosDeduccionesDepencias(int id)
        {

            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();

            DataTable ds = datoI.ParametosIngresosDeduccionesDepencias(id);
            List<IngDedDep> li = new List<IngDedDep>();
            foreach (DataRow row in ds.Rows)
            {
                IngDedDep i = new IngDedDep();

                i.IdIngDedP = int.Parse(row["IdIngresDeducP"].ToString());
                i.IdParamDep = int.Parse(row["IdParamDependecia"].ToString());
                i.Nombre = row["Nombre"].ToString();
                i.IdTipoCalc = int.Parse(row["IdTipoCalculo"].ToString());
                i.Valor = decimal.Parse(row["valor"].ToString());
                i.Condicion = decimal.Parse(row["condicion"].ToString());
                i.Variante = bool.Parse(row["variante"].ToString());
                i.NombreV = row["NombreVariante"].ToString();
                i.CondicionOP = row["CondicionOp"].ToString();

                li.Add(i);


            }
            return li;
        }
        public List<IncentivoEmp> ArmarDatosIncetivosxEmpleado(List<Neg_Empleados> Empleados, string construccion, string proceso, List<Neg_Incentivos.EmpleadoOp> EmpleadosOP, List<Neg_Incentivos.PIngDeducInc> param, string modulo, int estilo, decimal produccion, decimal meta, decimal incentivo, decimal eficienciai, decimal Metadias, decimal AQLPer)
        {
            System.Globalization.CultureInfo MyCultureInfo = new System.Globalization.CultureInfo("en-US");
            //List<IncentivoEmp> ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
            List<Neg_Incentivos.IncentivoEmp> ListIE = new List<Neg_Incentivos.IncentivoEmp>();
            decimal ingresoNumero = 0, deduccionnumerico = 0;
            decimal ingresoporcentual = 0, deduccionporcentual = 0;

            string nombre;
            decimal totaldiasTrab = 0;
            int codigo;

            EmpleadosOP = (List<Neg_Incentivos.EmpleadoOp>)Session["EMPLEADOSOP"];
            foreach (var item in Empleados)
            {

                IncentivoEmp emp = new IncentivoEmp();
                codigo = int.Parse(item.codigo_empleado.ToString());

                nombre = item.nombrecompleto.ToString();

                List<Neg_Amonestaciones.Amonestaciones> la = (List<Neg_Amonestaciones.Amonestaciones>)Session["AMONESTACIONES"];

                Neg_Incentivos.EmpleadoOp EO = (Neg_Incentivos.EmpleadoOp)(from e in EmpleadosOP where e.Codigo_empleado.Equals(item.codigo_empleado) select e).SingleOrDefault();
                if (EO != null)
                {
                    if (EO.Operacion.Trim().ToUpper() != "CMD")
                    {
                        int suma;
                        ingresoNumero = 0; deduccionnumerico = 0;
                        ingresoporcentual = 0; deduccionporcentual = 0;

                        DataTable dtHorasT = item.dtHorasT;
                        //validacion para no incluir dias con marcas pero fuera del turno
                        DataTable diasLaborados = dtHorasT.Select("horasturno>0").CopyToDataTable();
                        totaldiasTrab = diasLaborados.Rows.Count;
                        obtenerDiasTrabYAusenciasJ(diasLaborados, Convert.ToDateTime(item.fecheingreso));

                        if (EO.Codigo_empleado == 871062)
                        {

                        }

                        emp.Modulo = modulo;
                        emp.Estilo = estilo;
                        emp.Codigo = codigo;
                        emp.NombreCompleto = nombre;
                        emp.Construccion = construccion;
                        emp.Produccion = produccion;
                        emp.Proceso = proceso;
                        emp.Meta = meta;
         //prueba apara quitar el incentivo para if
                    if (EO.Operacion.Trim().ToUpper() == "IF")
                           {
                            emp.Incentivo = IncentivoConAQL((incentivo / 5) * totaldiasTrab, modulo);
                            }
                            else
                        {
                            emp.Incentivo = (incentivo / 5) * totaldiasTrab;
                        }

                        
                        
                        emp.DiasLaborales = totaldiasTrab;
                        emp.DiasLaborados = NdiasL;
                        emp.DiasJustificados = NdiasJ;
                        emp.DiasAusencias = (totaldiasTrab - NdiasL);
                        emp.Operacion = EO.Operacion;
                        emp.Horasaunsencia = float.Parse("48") - TotalHoraSemana;
                        emp.Eficiencia = eficienciai;
                        emp.Amonestaciones = 0;
                        emp.DiasInjustificados = emp.DiasAusencias - emp.DiasJustificados;
                        emp.MetasModulo = Metadias;
                        emp.TipoIngr = 4;
                        emp.Comentario = "GENERADO DESDE SISTEMA";


                        if (emp.Horasaunsencia < 0)
                        {
                            emp.Horasaunsencia = 0;
                        }
                        Neg_Amonestaciones.Amonestaciones amone = (Neg_Amonestaciones.Amonestaciones)(from i in la where i.Codigo_empleado.Equals(emp.Codigo) select i).SingleOrDefault();

                        if (amone != null)
                        {
                            emp.Amonestaciones = amone.Cantidad;
                        }

                        IncentivoEmp emp2 = IngresosDeduccion(emp, param, ingresoNumero, deduccionnumerico, ingresoporcentual, deduccionporcentual);

                        emp.DetalleIngreso = emp2.DetalleIngreso;
                        emp.DetalleEgreso = emp2.DetalleEgreso;
                        emp.IngresoNumerico = emp2.IngresoNumerico;
                        emp.IngresoPorcentual = emp2.IngresoPorcentual;
                        emp.DeduccionNumerica = emp2.DeduccionNumerica;
                        emp.DeduccionPorcentual = emp2.DeduccionPorcentual;

                        emp.Ingreso = emp2.Ingreso;
                        emp.Deduccion = emp2.Deduccion;
                        emp.Total = emp2.Total;

                        ListIE.Add(emp);
                    }

                }
            }

            Session["DTIncentivosSP"] = ListIE;
            return ListIE;
        }
        public IncentivoEmp ArmarDatosIncetivosEmpleado(Neg_Empleados Empleados, string construccion, string proceso, List<Neg_Incentivos.EmpleadoOp> EmpleadosOP, List<Neg_Incentivos.PIngDeducInc> param, string modulo, int estilo, decimal produccion, decimal meta, decimal incentivo, decimal eficienciai, decimal Metadias)
        {
            List<IncentivoEmp> ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
            Neg_Incentivos.IncentivoEmp empleado = new Neg_Incentivos.IncentivoEmp();
            decimal ingresoNumero = 0, deduccionnumerico = 0;
            decimal ingresoporcentual = 0, deduccionporcentual = 0;

            string nombre;
            decimal totaldiasTrab = 0;
            int codigo;

            EmpleadosOP = (List<Neg_Incentivos.EmpleadoOp>)Session["EMPLEADOSOP"];

            IncentivoEmp emp = new IncentivoEmp();
            codigo = int.Parse(Empleados.codigo_empleado.ToString());

            nombre = Empleados.nombrecompleto.ToString();

            List<Neg_Amonestaciones.Amonestaciones> la = (List<Neg_Amonestaciones.Amonestaciones>)Session["AMONESTACIONES"];

            Neg_Incentivos.EmpleadoOp EO = (Neg_Incentivos.EmpleadoOp)(from e in EmpleadosOP where e.Codigo_empleado.Equals(Empleados.codigo_empleado) select e).SingleOrDefault();
            if (EO != null)
            {
                if (EO.Operacion.Trim().ToUpper() != "CMD")
                {

                    ingresoNumero = 0; deduccionnumerico = 0;
                    ingresoporcentual = 0; deduccionporcentual = 0;

                    DataTable diasLaborados = Empleados.dtHorasT;
                    DataView dtvdiaslaborados = diasLaborados.Copy().DefaultView;
                    dtvdiaslaborados.RowFilter = "horasturno >0";

                    if (EO.Codigo_empleado == 871062)
                    {

                    }

                    totaldiasTrab = dtvdiaslaborados.Count;
                    obtenerDiasTrabYAusenciasJ(diasLaborados, Convert.ToDateTime(Empleados.fecheingreso));

                    emp.Modulo = modulo;
                    emp.Estilo = estilo;
                    emp.Codigo = codigo;
                    emp.NombreCompleto = nombre;
                    emp.Construccion = construccion;
                    emp.Produccion = produccion;
                    emp.Proceso = proceso;
                    emp.Meta = meta;

                     if (EO.Operacion.Trim().ToUpper() == "IF")
                    {
                        emp.Incentivo = IncentivoConAQL((incentivo / 5) * totaldiasTrab, modulo);
                    }
                    else
                    {
                     emp.Incentivo = (incentivo / 5) * totaldiasTrab;   
                    }
                                    
                    emp.DiasLaborales = totaldiasTrab;
                    emp.DiasLaborados = NdiasL;
                    emp.DiasJustificados = NdiasJ;
                    emp.DiasAusencias = (totaldiasTrab - NdiasL);
                    emp.Operacion = EO.Operacion;
                    emp.Horasaunsencia = float.Parse("48") - TotalHoraSemana;
                    emp.Eficiencia = eficienciai;
                    emp.Amonestaciones = 0;
                    emp.DiasInjustificados = emp.DiasAusencias - emp.DiasJustificados;
                    emp.MetasModulo = Metadias;
                    emp.Comentario = "GENERADO DESDE SISTEMA";

                    if (emp.Horasaunsencia < 0)
                    {
                        emp.Horasaunsencia = 0;
                    }
                    Neg_Amonestaciones.Amonestaciones amone = (Neg_Amonestaciones.Amonestaciones)(from i in la where i.Codigo_empleado.Equals(emp.Codigo) select i).SingleOrDefault();

                    if (amone != null)
                    {
                        emp.Amonestaciones = amone.Cantidad;
                    }

                    /////QUEDA PENDIENTE OBTENER EL NUMERO DE  RECHAZOS


                    //SE RECORREN TODOS LOS PARAMETROS CONFIGURADOS EN EL SISTEMA

                    IncentivoEmp emp2 = IngresosDeduccion(emp, param, ingresoNumero, deduccionnumerico, ingresoporcentual, deduccionporcentual);

                    emp.DetalleIngreso = emp2.DetalleIngreso;
                    emp.DetalleEgreso = emp2.DetalleEgreso;
                    emp.IngresoNumerico = emp2.IngresoNumerico;
                    emp.IngresoPorcentual = emp2.IngresoPorcentual;
                    emp.DeduccionNumerica = emp2.DeduccionNumerica;
                    emp.DeduccionPorcentual = emp2.DeduccionPorcentual;

                    emp.Ingreso = emp2.Ingreso;
                    emp.Deduccion = emp2.Deduccion;

                    emp.Total = emp2.Total;
                }
            }

            Session["DTIncentivosSP"] = ListIE;
            return emp;
        }

        public IncentivoEmp ProcesarIncentivos(Neg_Empleados Empleados, string construccion, string proceso, List<Neg_Incentivos.EmpleadoOp> EmpleadosOP, List<Neg_Incentivos.PIngDeducInc> param, string modulo, int estilo, decimal produccion, decimal meta, decimal incentivo, decimal eficienciai, decimal Metadias, int tipoing, string comentario)
        {
            List<IncentivoEmp> ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
            Neg_Incentivos.IncentivoEmp empleado = new Neg_Incentivos.IncentivoEmp();
            decimal ingresoNumero = 0, deduccionnumerico = 0;
            decimal ingresoporcentual = 0, deduccionporcentual = 0;

            string nombre;
            decimal totaldiasTrab = 0;
            int codigo;

            IncentivoEmp emp = new IncentivoEmp();
            codigo = int.Parse(Empleados.codigo_empleado.ToString());

            nombre = Empleados.nombrecompleto.ToString();

            List<Neg_Amonestaciones.Amonestaciones> la = (List<Neg_Amonestaciones.Amonestaciones>)Session["AMONESTACIONES"];

            Neg_Incentivos.EmpleadoOp EO = (Neg_Incentivos.EmpleadoOp)(from e in EmpleadosOP where e.Codigo_empleado.Equals(Empleados.codigo_empleado) select e).SingleOrDefault();
            if (EO != null)
            {

                ingresoNumero = 0; deduccionnumerico = 0;
                ingresoporcentual = 0; deduccionporcentual = 0;

                DataTable diasLaborados = Empleados.dtHorasT;

                DataTable diasLaborados2 = diasLaborados;
                DataView dtvdiaslaborados = diasLaborados2.DefaultView;
                dtvdiaslaborados.RowFilter = "horasturno >0";

                totaldiasTrab = dtvdiaslaborados.Count;
                obtenerDiasTrabYAusenciasJ(diasLaborados, Empleados.fecheingreso);

                emp.TipoIngr = tipoing;
                emp.Comentario = comentario;
                emp.Modulo = modulo;
                emp.Estilo = estilo;
                emp.Codigo = codigo;
                emp.NombreCompleto = nombre;
                emp.Construccion = construccion;
                emp.Produccion = produccion;
                emp.Proceso = proceso;
                emp.Meta = meta;
                emp.Incentivo = incentivo;
                emp.DiasLaborales = totaldiasTrab;
                emp.DiasLaborados = NdiasL;
                emp.DiasJustificados = NdiasJ;
                if (!Empleados.flexitime)
                {
                    emp.DiasAusencias = (totaldiasTrab - NdiasL);
                    emp.DiasInjustificados = emp.DiasAusencias - emp.DiasJustificados;
                }
                else
                {
                    emp.DiasAusencias = 0;
                    emp.DiasInjustificados = 0;
                }
                emp.Operacion = EO.Operacion;
                emp.Horasaunsencia = float.Parse("48") - TotalHoraSemana;
                emp.Eficiencia = eficienciai;
                emp.Amonestaciones = 0;
                emp.Rechazos = 0;

                emp.MetasModulo = Metadias;

                if (emp.Horasaunsencia < 0)
                {
                    emp.Horasaunsencia = 0;
                }


                List<Neg_Amonestaciones.Amonestaciones> amone = (from i in la where i.Codigo_empleado.Equals(emp.Codigo) select i).ToList();

                if (amone != null)
                {
                    foreach (var ta in amone)
                    {
                        if (ta.IdParamPenalizacion == 2)//amonestacion 10%
                        {
                            emp.Amonestaciones = ta.Cantidad;
                        }
                        else//rechazos 20%
                        {
                            emp.Rechazos = ta.Cantidad;
                        }
                    }

                }


                IncentivoEmp emp2 = CalculoIngresosDeduccion(emp, param, ingresoNumero, deduccionnumerico, ingresoporcentual, deduccionporcentual);

                emp.DetalleIngreso = emp2.DetalleIngreso;
                emp.DetalleEgreso = emp2.DetalleEgreso;
                emp.IngresoNumerico = emp2.IngresoNumerico;
                emp.IngresoPorcentual = emp2.IngresoPorcentual;
                emp.DeduccionNumerica = emp2.DeduccionNumerica;
                emp.DeduccionPorcentual = emp2.DeduccionPorcentual;

                emp.Ingreso = emp2.Ingreso;
                emp.Deduccion = emp2.Deduccion;

                emp.Total = emp2.Total;

                Session["DTIncentivosSP"] = ListIE;
                return emp;


            }


            // Session["DTIncentivosSP"] = ListIE;
            return emp;

        }


        public void obtenerDiasTrabYAusenciasJ(DataTable diasLaborados, DateTime fechaIngreso)
        {

            System.Globalization.CultureInfo MyCultureInfo = new System.Globalization.CultureInfo("en-US");

            decimal horasJ = 0;
            decimal horasd = 0;
            TotalHorasLaboradas = 0; TotalHoraSemana = 0;
            NdiasL = 0; NdiasJ = 0;
            foreach (DataRow dia in diasLaborados.Rows)
            {
                if (decimal.Parse(dia["horasturno"].ToString()) > 0)
                {
                    decimal ht = 0;
                    try
                    {
                        if (decimal.Parse(dia["horast"].ToString()) > 0)
                        {
                            ht = decimal.Parse(dia["horast"].ToString());
                        }
                    }
                    catch
                    {

                        ht = 0;


                    }

                    horasd = ht + decimal.Parse(dia["horascg"].ToString());

                    if (horasd == 0)
                    {
                        if (fechaIngreso >= DateTime.Parse(dia["fecha"].ToString()))
                        {
                            NdiasJ = NdiasJ + 1;
                        }
                        else if (double.Parse(dia["horassg"].ToString()) >= 9.6)
                        {
                            NdiasJ = NdiasJ + 1;
                            horasJ = decimal.Parse(dia["horassg"].ToString());
                        }
                        else if (double.Parse(dia["horass"].ToString()) >= 9.6)
                        {
                            NdiasJ++;
                            horasJ = decimal.Parse(dia["horass"].ToString());
                        }
                        else
                        {

                            horasJ = decimal.Parse(dia["horasv"].ToString()) + decimal.Parse(dia["horascg"].ToString());
                            // + decimal.Parse(dia["horassg"].ToString())
                            if (horasJ > 0)
                            {
                                NdiasJ = NdiasJ + 1;
                            }
                        }
                    }
                    else if (decimal.Parse(dia["horasturno"].ToString()) == decimal.Parse(dia["horascg"].ToString()))
                    {
                        NdiasJ++;
                    }
                    else
                    {
                        NdiasL = NdiasL + 1;
                    }

                    TotalHorasLaboradas += (float.Parse(dia["horast"].ToString()));
                    TotalHoraSemana += float.Parse(dia["horast"].ToString()) + (float.Parse(dia["horasv"].ToString()) + float.Parse(dia["horascg"].ToString()) + float.Parse(dia["horassg"].ToString()) + float.Parse(dia["horass"].ToString()));
                }
            }

        }


        public void ActualizarMeta(Neg_Incentivos.IncentivoEmp dtISP, decimal DZ, int tipo, List<Neg_Incentivos.PIngDeducInc> param)
        {
            //TIPO=1 SUMA
            //TIPO=2 RESTA
            //OPERACION=1 ACTUALIZAR
            //OPERACION=2 ELIMINAR
            decimal DOC = 0;
            string construccion = "";
            decimal ingresoNumero = 0, deduccionnumerico = 0;
            decimal ingresoporcentual = 0, deduccionporcentual = 0;



            if (tipo == 1)
            {
                DOC = Convert.ToDecimal(dtISP.Produccion.ToString()) + DZ;

            }
            else if (tipo == 2)
            {
                DOC = Convert.ToDecimal(dtISP.Produccion.ToString()) - DZ;
            }
            construccion = dtISP.Construccion.ToString();
            List<Incentivos> li = (List<Neg_Incentivos.Incentivos>)Session["TABLAINCENTIVOS"];
            List<IncentivoEmp> ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
            List<ProdXmod> prodcmod = (List<Neg_Incentivos.ProdXmod>)Session["PRODUCCIONXMODULO"];

            Neg_Incentivos.Incentivos nuevoincentivo = (Neg_Incentivos.Incentivos)(from incentivos in li where incentivos.Construccion.Equals(construccion) && ((incentivos.Meta5 / 5) * dtISP.MetasModulo) <= DOC orderby incentivos.Meta5 descending select incentivos).FirstOrDefault();

            //SI EL OBJETO nuevoincentivo ES != DE NULL, INDICA QUE SI HAY INCENTIVO PARA ESA PRODUCCION

            for (int i = 0; i < ListIE.Count; i++)
            {
                Neg_Incentivos.IncentivoEmp emp = ListIE[i];
                if (emp.Codigo.Equals(dtISP.Codigo))
                {
                    if (nuevoincentivo != null)
                    {

                        ingresoNumero = 0; deduccionnumerico = 0;
                        ingresoporcentual = 0; deduccionporcentual = 0;
                        emp.Produccion = DOC;
                        emp.Meta = decimal.Parse(((nuevoincentivo.Meta5 / 5) * (emp.MetasModulo)).ToString());
                        emp.Eficiencia = decimal.Parse(nuevoincentivo.Eficiencia.ToString());// decimal.Parse((((nuevoincentivo.Eficiencia / 5) * (emp.MetasModulo))).ToString());

                        if (emp.Codigo == 871062)
                        {

                        }
                        if (emp.Operacion.Trim().ToUpper() == "IF")
                       {
                           emp.Incentivo = IncentivoConAQL(decimal.Parse((((nuevoincentivo.Incentivo / 5) * (emp.MetasModulo))).ToString()), emp.Modulo);
                       }
                       else
                       {
                           emp.Incentivo = decimal.Parse((((nuevoincentivo.Incentivo / 5) * (emp.MetasModulo))).ToString());
                       }

                        //emp.Incentivo = decimal.Parse((((nuevoincentivo.Incentivo / 5) * (emp.MetasModulo))).ToString());
                        emp.DetalleEgreso = "";
                        emp.DetalleIngreso = "";
                        IngresosDeduccion(emp, param, ingresoNumero, deduccionnumerico, ingresoporcentual, deduccionporcentual);
                    }
                    else
                    {
                        ListIE.Remove(emp);

                    }
                }
            }

            Session["DTIncentivosSP"] = ListIE;
        }

        public void BuscarProdud(int cod, decimal DZ, string modulo, int estilo, decimal produccion, decimal meta, decimal incentivo, decimal eficienciai, List<Neg_Incentivos.EmpleadoOp> EmpleadosOP, List<Neg_Incentivos.PIngDeducInc> param, List<Neg_Empleados> Empleados, int Codigo_depto)
        {
            //SE OBTIENE LA TABLA DE TODOS LOS EMPLEADOS CON INCENTIVOS
            List<IncentivoEmp> ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
            //SE OBTIENE LA PRODUCCION DE TODOS LOS MODULOS
            List<ProdXmod> prodcmod = (List<Neg_Incentivos.ProdXmod>)Session["PRODUCCIONXMODULO"];

            DataTable dt = (DataTable)Session["DiasMetasModulos"];


            //SE OBTIENEN TODOS LOS EMPLEADOS
            List<Neg_Empleados> lt = (List<Neg_Empleados>)Session["EMPLEADOS"];


            //SE OBTIENE DE TODOS LOS EMPLEADOS EL EMPLEADO AL QUE SE LE SUMARA LA PRODUCCION
            Neg_Empleados objeEmp = (Neg_Empleados)(from emp in lt where emp.codigo_empleado.Equals(cod) select emp).SingleOrDefault();
            if (objeEmp != null)
            {
                List<Incentivos> li = (List<Neg_Incentivos.Incentivos>)Session["TABLAINCENTIVOS"];
                List<Neg_Incentivos.ProdXmod> produccionM = (List<Neg_Incentivos.ProdXmod>)(from prod in prodcmod where prod.Coddep.Equals(objeEmp.codigo_depto) select prod).ToList();
                foreach (Neg_Incentivos.ProdXmod p in produccionM)
                {
                    DataTable dt2 = dt.Copy();
                    DataView dtv = dt2.DefaultView;
                    dtv.RowFilter = "Modulo=" + int.Parse(p.Modulo);
                    if (dtv != null)
                    {
                        if (dtv.Count > 0)
                        {
                            Neg_Incentivos.Incentivos nuevoincentivo = (Neg_Incentivos.Incentivos)(from incentivos in li where ((incentivos.Meta5 / 5) * (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString()))) <= (p.Produccion + DZ) && incentivos.Construccion.Equals(p.Construccion) orderby incentivos.Meta5 descending select incentivos).FirstOrDefault();
                            if (nuevoincentivo != null)
                            {
                                modulo = p.Modulo;
                                estilo = p.EstiloP;
                                Codigo_depto = objeEmp.codigo_empleado;
                                produccion = Convert.ToDecimal(p.Produccion.ToString()) + DZ;
                                meta = ((nuevoincentivo.Meta5 / 5) * (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString())));
                                incentivo = (((nuevoincentivo.Incentivo / 5) * (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString()))));


                                decimal monto = incentivo * nuevoincentivo.Layout;


                                decimal eficienciac = nuevoincentivo.Eficiencia;


                                ListIE.Add(ArmarDatosIncetivosEmpleado(objeEmp, p.Construccion, nuevoincentivo.Proceso, EmpleadosOP, param, modulo, estilo, produccion, decimal.Parse(meta.ToString()), incentivo, eficienciac, (int.Parse(dtv[0]["MetadDiasIncentivo"].ToString()))));

                                Session["DTIncentivosSP"] = ListIE;
                            }

                        }
                    }


                }

            }

        }

        public IncentivoEmp IngresosDeduccion(IncentivoEmp emp, List<Neg_Incentivos.PIngDeducInc> param, decimal ingresoNumero, decimal deduccionnumerico, decimal ingresoporcentual, decimal deduccionporcentual)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("tipo", typeof(int));
            dt.Columns.Add("detalle", typeof(string));
            dt.Columns.Add("tipoCalc", typeof(int));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("Valor", typeof(decimal));

            DataTable dtInD = (DataTable)Session["IngDed"];
            param = (List<Neg_Incentivos.PIngDeducInc>)Session["PARAM"];


            DataTable dtOP = EmpleadosPagosXOperacionSelect();
            decimal valorOP = 0;
            DataView dtvOP = dtOP.Copy().DefaultView;
            dtvOP.RowFilter = "codigo=" + emp.Codigo;
            if (dtvOP.Count > 0)
            {
                valorOP = ((emp.Incentivo * (decimal.Parse(dtvOP.ToTable().Rows[0][1].ToString()) * 100)) / 100);
                emp.DetalleIngreso += " PagoOperacion : C$" + valorOP.ToString();
                dtInD.Rows.Add(int.Parse(emp.Codigo.ToString()), 1, "PagoOperacion", 2, decimal.Parse(dtvOP.ToTable().Rows[0][1].ToString()), valorOP, "SISTEMA");

            }


            foreach (var p in param)
            {
                decimal valorParametro = 0, valorDependencia = 0;

                string nombrecapoO = "";
                //SI TIENE VARIANTE INDICA QUE SE BUSCARA UN NOMBRE DE COLUMNA ESPECIFICO
                if (p.Variantes)
                {
                    nombrecapoO = p.NombreV;
                }
                else
                {
                    nombrecapoO = p.Nombre;

                }

                //VARABLE PARA BUSCAR EL NOMBRE DEL PARAMETRO EN LAS COLUMNAS DE OBJETO
                //////////////////////////////////////////////////////////////////////////////
                Type CampoParametro = typeof(IncentivoEmp);
                PropertyInfo PropInfoCampoParametro;
                PropInfoCampoParametro = CampoParametro.GetProperty(nombrecapoO);

                /////////////////////////////////////////////////////////////////////////////

                //VARABLE PARA BUSCAR EL NOMBRE DE LA DEPENDENCIA EN LAS COLUMNAS DE OBJETO
                //////////////////////////////////////////////////////////////////////////////
                PropertyInfo ProInfoCampoDependencia;
                Type CampoDependenci = typeof(IncentivoEmp);
                //////////////////////////////////////////////////////////////////////////////


                //SI SE ENCUENTRA UN NOMBRE DE COLUMNA DEL OBJETO IGUAL AL NOMBRE DEL PARAMETRO CONFIGURABLE
                if (PropInfoCampoParametro != null)
                {
                    bool cumpleDependencias = true;

                    //SI EL PARAMETRO TIENE DEPENDENCIA PARA CUMPLIRSE
                    if (p.Dependencias)
                    {
                        List<IngDedDep> ldep = ParametosIngresosDeduccionesDepencias(p.Id);

                        valorParametro = 0; valorDependencia = 0;
                        //CONTADOR DE CUANTAS DEPENDENCIAS SE INCUMPLEN
                        int contDep = 0;
                        //SE RECORREN LAS DEPENDENIAS ENCONTRADAS
                        foreach (var d in ldep)
                        {
                            string nombrecapo = "";
                            //SI TIENE VARIANTE INDICA QUE SE BUSCARA UN NOMBRE DE COLUMNA ESPECIFICO
                            if (d.Variante)
                            {
                                nombrecapo = d.NombreV;
                                //Horasaunsencia 
                                //Horasaunsencia
                            }
                            //SI NO TIENE VARIANTE INDICA QUE SE BUSCARA EL NOMBRE DE COLUMNA DEL PARAMETRO
                            else
                            {
                                nombrecapo = d.Nombre;
                            }

                            ProInfoCampoDependencia = CampoDependenci.GetProperty(nombrecapo);

                            if (ProInfoCampoDependencia != null)
                            {
                                valorDependencia = decimal.Parse(ProInfoCampoDependencia.GetValue(emp, null).ToString());

                                PropInfoCampoParametro = CampoParametro.GetProperty(nombrecapo);
                                valorParametro = decimal.Parse(PropInfoCampoParametro.GetValue(emp, null).ToString());

                                if (d.CondicionOP.ToLower() == "mayor")
                                {
                                    if (!(valorDependencia >= d.Condicion))
                                    {
                                        contDep++;
                                    }
                                }
                                else if (d.CondicionOP.ToLower() == "menor")
                                {
                                    if (!(valorDependencia <= d.Condicion))
                                    {
                                        contDep++;
                                    }

                                }

                            }

                        }
                        if (contDep > 0)
                        {
                            cumpleDependencias = false;
                        }

                    }


                    if (cumpleDependencias)
                    {
                        bool cumplecondicion = true;
                        PropInfoCampoParametro = CampoParametro.GetProperty(nombrecapoO);
                        valorParametro = decimal.Parse(PropInfoCampoParametro.GetValue(emp, null).ToString());
                        if (p.Condicion > 0)
                        {

                            if (p.CondicionOP.ToLower() == "mayor")
                            {
                                if (!(valorParametro >= p.Condicion))
                                {
                                    cumplecondicion = false;
                                }
                            }
                            else if (p.CondicionOP.ToLower() == "menor")
                            {
                                if (!(valorParametro <= p.Condicion))
                                {
                                    cumplecondicion = false;
                                }

                            }

                            //SI SE CUMPLE LA CONDICION DEL PARAMETRO
                            if (cumplecondicion)
                            {
                                decimal valorPor = 0, valorNum = 0, Valor = 0;
                                Valor = p.Valor;

                                //TIPO DE CALCULO ES %
                                if (p.Tipocalculo == 1)
                                {
                                    if (p.AsignarValor.ToLower() == "individual")
                                    {
                                        valorPor = valorParametro * Valor;
                                    }
                                    else
                                    {
                                        valorPor = Valor;
                                    }
                                }

                                //TIPO DE CALCULO ES NUMERICO
                                else
                                {
                                    if (p.AsignarValor.ToLower() == "individual")
                                    {
                                        valorNum = valorParametro * Valor;
                                    }
                                    else
                                    {
                                        valorNum = Valor;
                                    }

                                }


                                /////////////////////////////////////////INGRESO/////////////////////////////////////////////////////////////
                                if (p.IdTipo == 1)
                                {

                                    ingresoporcentual += valorPor;
                                    decimal valoIE = 0;
                                    if (valorNum > 0)
                                    {
                                        if (nombrecapoO == "Eficiencia")
                                        {
                                            valorNum = (valorNum / 5) * emp.MetasModulo;
                                        }
                                        ingresoNumero += valorNum;
                                        emp.DetalleIngreso += " " + nombrecapoO + " : C$" + valorNum.ToString();
                                        string query = "Codigo=" + int.Parse(emp.Codigo.ToString()) + " and  tipo =" + 1 + " and detalle='" + nombrecapoO + "' and tipoCalc=" + 2;
                                        var rows = dtInD.Select(query);
                                        foreach (var row in rows)
                                            row.Delete();

                                        dtInD.Rows.Add(int.Parse(emp.Codigo.ToString()), 1, nombrecapoO, 2, valorNum, valorNum, "SISTEMA");
                                    }
                                    if (valorPor > 0)
                                    {
                                        valoIE = ((emp.Incentivo * valorPor) / 100);
                                        emp.DetalleIngreso += " " + nombrecapoO + " : " + valorPor.ToString() + "% ";
                                        dt.Rows.Add(int.Parse(emp.Codigo.ToString()), 1, nombrecapoO, 1, valorPor, valoIE);
                                    }



                                }
                                ///////////////////////////////////////////DEDUCCION/////////////////////////////////////////////////////////
                                else
                                {

                                    deduccionnumerico += valorNum;
                                    deduccionporcentual += valorPor;
                                    decimal valoIE = 0;
                                    if (valorNum > 0)
                                    {

                                        emp.DetalleEgreso += " " + nombrecapoO + " : C$" + valorNum.ToString();

                                        string query = "tipo =" + 2 + " and detalle='" + nombrecapoO + "' and tipoCalc=" + 2;
                                        var rows = dtInD.Select(query);
                                        foreach (var row in rows)
                                            row.Delete();
                                        dtInD.Rows.Add(int.Parse(emp.Codigo.ToString()), 2, nombrecapoO, 2, valorNum, valorNum, "SISTEMA");
                                    }
                                    if (valorPor > 0)
                                    {
                                        valoIE = ((emp.Incentivo * valorPor) / 100);
                                        emp.DetalleEgreso += " " + nombrecapoO + " : " + valorPor.ToString() + "% ";

                                        dt.Rows.Add(int.Parse(emp.Codigo.ToString()), 2, nombrecapoO, 1, valorPor, valoIE);
                                    }

                                }
                            }
                        }


                    }
                }

            } ///FIN LISTA PARAMETROS
            emp.IngresoNumerico = ingresoNumero + valorOP;
            emp.IngresoPorcentual = ingresoporcentual;
            emp.DeduccionNumerica = deduccionnumerico;
            emp.DeduccionPorcentual = deduccionporcentual;

            emp.Ingreso = ((emp.Incentivo * emp.IngresoPorcentual) / 100) + emp.IngresoNumerico;
            decimal incentivoIngresos = emp.Incentivo + emp.Ingreso;


            foreach (DataRow dr in dt.Rows)
            {
                int codigo = int.Parse(dr["Codigo"].ToString());
                int tipo = int.Parse(dr["tipo"].ToString());
                string detalle = dr["detalle"].ToString();
                int tipoC = int.Parse(dr["tipoCalc"].ToString());
                decimal cantidad = decimal.Parse(dr["Cantidad"].ToString());

                decimal valoIE = ((incentivoIngresos * cantidad) / 100);

                string query = "Codigo=" + codigo + " and tipo =" + tipo + " and detalle='" + detalle + "' and tipoCalc=" + tipoC;
                var rows = dtInD.Select(query);
                foreach (var row in rows)
                    row.Delete();

                dtInD.Rows.Add(codigo, tipo, detalle, tipoC, cantidad, valoIE, "SISTEMA");
            }


            emp.Deduccion = ((incentivoIngresos * emp.DeduccionPorcentual) / 100) + emp.DeduccionNumerica;


            emp.Total = incentivoIngresos - emp.Deduccion;
            if (emp.Total < 0)
            {
                emp.Total = 0;
            }

            Session["IngDed"] = dtInD;
            return emp;
        }

        public IncentivoEmp CalculoIngresosDeduccion(IncentivoEmp emp, List<Neg_Incentivos.PIngDeducInc> param, decimal ingresoNumero, decimal deduccionnumerico, decimal ingresoporcentual, decimal deduccionporcentual)
        {

            DataTable dt = new DataTable();

            dt.Columns.Add("Codigo", typeof(int));
            dt.Columns.Add("tipo", typeof(int));
            dt.Columns.Add("detalle", typeof(string));
            dt.Columns.Add("tipoCalc", typeof(int));
            dt.Columns.Add("Cantidad", typeof(decimal));
            dt.Columns.Add("Valor", typeof(decimal));


            DataTable dtInD = (DataTable)Session["IngDed"];
            param = (List<Neg_Incentivos.PIngDeducInc>)Session["PARAM"];

            foreach (var p in param)
            {
                decimal valorParametro = 0, valorDependencia = 0;

                string nombrecapoO = "";
                //SI TIENE VARIANTE INDICA QUE SE BUSCARA UN NOMBRE DE COLUMNA ESPECIFICO
                if (p.Variantes)
                {
                    nombrecapoO = p.NombreV;
                }
                else
                {
                    nombrecapoO = p.Nombre;

                }
                if (!(nombrecapoO.Trim() != ""))
                {
                    continue;
                }
                //if (nombrecapoO == "Amonestaciones" || nombrecapoO == "DiasInjustificados" || nombrecapoO == "Rechazos")
                //{
                //VARABLE PARA BUSCAR EL NOMBRE DEL PARAMETRO EN LAS COLUMNAS DE OBJETO
                //////////////////////////////////////////////////////////////////////////////
                Type CampoParametro = typeof(IncentivoEmp);
                PropertyInfo PropInfoCampoParametro;
                PropInfoCampoParametro = CampoParametro.GetProperty(nombrecapoO);

                /////////////////////////////////////////////////////////////////////////////

                //VARABLE PARA BUSCAR EL NOMBRE DE LA DEPENDENCIA EN LAS COLUMNAS DE OBJETO
                //////////////////////////////////////////////////////////////////////////////
                PropertyInfo ProInfoCampoDependencia;
                Type CampoDependenci = typeof(IncentivoEmp);
                //////////////////////////////////////////////////////////////////////////////


                //SI SE ENCUENTRA UN NOMBRE DE COLUMNA DEL OBJETO IGUAL AL NOMBRE DEL PARAMETRO CONFIGURABLE
                if (PropInfoCampoParametro != null)
                {
                    bool cumpleDependencias = true;

                    //SI EL PARAMETRO TIENE DEPENDENCIA PARA CUMPLIRSE
                    if (p.Dependencias)
                    {
                        List<IngDedDep> ldep = ParametosIngresosDeduccionesDepencias(p.Id);

                        valorParametro = 0; valorDependencia = 0;
                        //CONTADOR DE CUANTAS DEPENDENCIAS SE INCUMPLEN
                        int contDep = 0;
                        //SE RECORREN LAS DEPENDENIAS ENCONTRADAS
                        foreach (var d in ldep)
                        {
                            string nombrecapo = "";
                            //SI TIENE VARIANTE INDICA QUE SE BUSCARA UN NOMBRE DE COLUMNA ESPECIFICO
                            if (d.Variante)
                            {
                                nombrecapo = d.NombreV;
                                //Horasaunsencia 
                                //Horasaunsencia
                            }
                            //SI NO TIENE VARIANTE INDICA QUE SE BUSCARA EL NOMBRE DE COLUMNA DEL PARAMETRO
                            else
                            {
                                nombrecapo = d.Nombre;
                            }

                            ProInfoCampoDependencia = CampoDependenci.GetProperty(nombrecapo);

                            if (ProInfoCampoDependencia != null)
                            {
                                valorDependencia = decimal.Parse(ProInfoCampoDependencia.GetValue(emp, null).ToString());

                                PropInfoCampoParametro = CampoParametro.GetProperty(nombrecapo);
                                valorParametro = decimal.Parse(PropInfoCampoParametro.GetValue(emp, null).ToString());

                                if (d.CondicionOP.ToLower() == "mayor")
                                {
                                    if (!(valorDependencia >= d.Condicion))
                                    {
                                        contDep++;
                                    }
                                }
                                else if (d.CondicionOP.ToLower() == "menor")
                                {
                                    if (!(valorDependencia <= d.Condicion))
                                    {
                                        contDep++;
                                    }

                                }

                            }

                        }
                        if (contDep > 0)
                        {
                            cumpleDependencias = false;
                        }

                    }

                    if (cumpleDependencias)
                    {
                        bool cumplecondicion = true;
                        PropInfoCampoParametro = CampoParametro.GetProperty(nombrecapoO);
                        valorParametro = decimal.Parse(PropInfoCampoParametro.GetValue(emp, null).ToString());
                        if (p.Condicion > 0)
                        {

                            if (p.CondicionOP.ToLower() == "mayor")
                            {
                                if (!(valorParametro >= p.Condicion))
                                {
                                    cumplecondicion = false;
                                }
                            }
                            else if (p.CondicionOP.ToLower() == "menor")
                            {
                                if (!(valorParametro <= p.Condicion))
                                {
                                    cumplecondicion = false;
                                }

                            }

                            //SI SE CUMPLE LA CONDICION DEL PARAMETRO
                            if (cumplecondicion)
                            {
                                decimal valorPor = 0, valorNum = 0, Valor = 0;
                                Valor = p.Valor;

                                //TIPO DE CALCULO ES %
                                if (p.Tipocalculo == 1)
                                {
                                    if (p.AsignarValor.ToLower() == "individual")
                                    {
                                        valorPor = valorParametro * Valor;
                                    }
                                    else
                                    {
                                        valorPor = Valor;
                                    }


                                }

                                //TIPO DE CALCULO ES NUMERICO
                                else
                                {
                                    if (p.AsignarValor.ToLower() == "individual")
                                    {
                                        valorNum = valorParametro * Valor;
                                    }
                                    else
                                    {
                                        valorNum = Valor;
                                    }

                                }


                                /////////////////////////////////////////INGRESO/////////////////////////////////////////////////////////////
                                if (p.IdTipo == 1)
                                {

                                    ingresoporcentual += valorPor;
                                    decimal valoIE = 0;
                                    if (valorNum > 0)
                                    {
                                        if (nombrecapoO == "Eficiencia")
                                        {
                                            valorNum = (valorNum / 5) * emp.MetasModulo;
                                        }
                                        ingresoNumero += valorNum;
                                        emp.DetalleIngreso += " " + nombrecapoO + " : C$" + valorNum.ToString();
                                        string query = "Codigo=" + int.Parse(emp.Codigo.ToString()) + " and  tipo =" + 1 + " and detalle='" + nombrecapoO + "' and tipoCalc=" + 2;
                                        var rows = dtInD.Select(query);
                                        foreach (var row in rows)
                                            row.Delete();

                                        dtInD.Rows.Add(int.Parse(emp.Codigo.ToString()), 1, nombrecapoO, 2, valorNum, valorNum, "SISTEMA", true);
                                    }
                                    if (valorPor > 0)
                                    {
                                        valoIE = ((emp.Incentivo * valorPor) / 100);
                                        emp.DetalleIngreso += " " + nombrecapoO + " : " + valorPor.ToString() + "% ";
                                        dt.Rows.Add(int.Parse(emp.Codigo.ToString()), 1, nombrecapoO, 1, valorPor, valoIE);
                                    }

                                }
                                ///////////////////////////////////////////DEDUCCION/////////////////////////////////////////////////////////
                                else
                                {

                                    deduccionnumerico += valorNum;
                                    deduccionporcentual += valorPor;
                                    decimal valoIE = 0;
                                    if (valorNum > 0)
                                    {

                                        emp.DetalleEgreso += " " + nombrecapoO + " : C$" + valorNum.ToString();

                                        string query = "tipo =" + 2 + " and detalle='" + nombrecapoO + "' and tipoCalc=" + 2;
                                        var rows = dtInD.Select(query);
                                        foreach (var row in rows)
                                            row.Delete();
                                        dtInD.Rows.Add(int.Parse(emp.Codigo.ToString()), 2, nombrecapoO, 2, valorNum, valorNum, "SISTEMA", true);
                                    }
                                    if (valorPor > 0)
                                    {
                                        valoIE = ((emp.Incentivo * valorPor) / 100);
                                        emp.DetalleEgreso += " " + nombrecapoO + " : " + valorPor.ToString() + "% ";

                                        dt.Rows.Add(int.Parse(emp.Codigo.ToString()), 2, nombrecapoO, 1, valorPor, valoIE);
                                    }

                                }
                            }
                        }


                    }
                }
                // }

            } ///FIN LISTA PARAMETROS
            emp.IngresoNumerico = ingresoNumero;
            emp.IngresoPorcentual = ingresoporcentual;
            emp.DeduccionNumerica = deduccionnumerico;
            emp.DeduccionPorcentual = deduccionporcentual;

            emp.Ingreso = ((emp.Incentivo * emp.IngresoPorcentual) / 100) + emp.IngresoNumerico;
            decimal incentivoIngresos = emp.Incentivo + emp.Ingreso;

            foreach (DataRow dr in dt.Rows)
            {
                int codigo = int.Parse(dr["Codigo"].ToString());
                int tipo = int.Parse(dr["tipo"].ToString());
                string detalle = dr["detalle"].ToString();
                int tipoC = int.Parse(dr["tipoCalc"].ToString());
                decimal cantidad = decimal.Parse(dr["Cantidad"].ToString());

                decimal valoIE = ((incentivoIngresos * cantidad) / 100);

                string query = "Codigo=" + codigo + " and tipo =" + tipo + " and detalle='" + detalle + "' and tipoCalc=" + tipoC;
                var rows = dtInD.Select(query);
                foreach (var row in rows)
                    row.Delete();

                dtInD.Rows.Add(codigo, tipo, detalle, tipoC, cantidad, valoIE, "SISTEMA", emp.TipoIngr, true);
            }


            emp.Deduccion = ((incentivoIngresos * emp.DeduccionPorcentual) / 100) + emp.DeduccionNumerica;


            emp.Total = incentivoIngresos - emp.Deduccion;
            if (emp.Total < 0)
            {
                emp.Total = 0;
            }

            Session["IngDed"] = dtInD;

            return emp;
        }


        public bool IncentivosHistoricoInsert(int codigo, int modulo, string estilo, string operacion, string construccion, decimal produccion, decimal metaAlcanzada, int amonestacion, decimal diasLaborados, decimal diasAusencia, decimal AusenciaJustificada, decimal AusenciaInJustificada, decimal TotalAusencia, decimal Eficiencia, decimal IncentivoMeta, decimal totalIngreso, decimal totalEgreso, decimal totalIncentivo, int periodo, int semana, string user, int idtipoIng, string comentario, bool GeneradoSistema)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivosHistoricoInsert(userDetail.getIDEmpresa(), codigo, modulo, estilo, operacion, construccion, produccion, metaAlcanzada, amonestacion, diasLaborados, diasAusencia, AusenciaJustificada, AusenciaInJustificada, TotalAusencia, Eficiencia, IncentivoMeta, totalIngreso, totalEgreso, totalIncentivo, periodo, semana, user, idtipoIng, comentario, GeneradoSistema))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }

        public DataTable IncentivoHistoricoSelect(int periodo, int semana)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoHistoricoSelect(userDetail.getIDEmpresa(), periodo, semana);
        }
        public DataTable IncentivoHistoricoSelectDia(int periodo, int semana)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoHistoricoSelectDia(userDetail.getIDEmpresa(), periodo, semana);
        }

        public DataTable PlnConsultarIncentivoDetallexEmp(int periodo, int semana, int codigo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.PlnConsultarIncentivoDetallexEmp(periodo, semana, codigo, userDetail.getIDEmpresa());
        }

        public DataTable PlnConsultarSubCategoriaIncxEmp(int periodo, int semana, int codigo, int idtipoing)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.PlnConsultarSubCategoriaIncxEmp(periodo, semana, codigo, idtipoing, userDetail.getIDEmpresa());
        }

        public DataTable IncentivoConsolidadoSelect(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoConsolidadoSelect(userDetail.getIDEmpresa(), periodo, semana);
        }

        public DataTable IncentivoConsolidadoSelectxPeriodo(int periodo, int periodo2, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoConsolidadoSelectxPeriodo(userDetail.getIDEmpresa(), periodo, periodo2, semana);
        }

        public DataTable ObtenerIncentivoPlantaSel(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.ObtenerIncentivoPlantaSel(userDetail.getIDEmpresa(), periodo, semana);
        }

        public DataTable ObtenerIncentivoPlantaVATSel(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.ObtenerIncentivoPlantaVATSel(userDetail.getIDEmpresa(), periodo, semana);
        }
        public DataTable IncentivoIngDedExcepcionesSel(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoIngDedExcepcionesSel(userDetail.getIDEmpresa(), periodo, semana);
        }
        public DataTable IncentivoIngDedccLOGxEmpleado(int periodo, int semana)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoIngDedccLOGxEmpleado(userDetail.getIDEmpresa(), periodo, semana);
        }
        public DataTable IncentivoHistoricoSelectModulo(int periodo, int semana, int modulo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoHistoricoSelectModulo(userDetail.getIDEmpresa(), periodo, semana, modulo);
        }

        public DataTable IncentivoHistoricoSelectCodigo(int periodo, int semana, int codigo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoHistoricoSelectCodigo(userDetail.getIDEmpresa(), periodo, semana, codigo);
        }

        public DataTable IncentivoIngDedccLOGSelect(int periodo, int semana, int codigo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoIngDedccLOGSelect(userDetail.getIDEmpresa(), periodo, semana, codigo);
        }
        public DataTable IncentivoModulosConMeta(int periodo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoModulosConMeta(userDetail.getIDEmpresa(), periodo);
        }

        public DataTable IncentivoHistoricoSelectCodigoEmpleados(int periodo, int tipo, int param)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivoHistoricoSelectCodigoEmpleados(userDetail.getIDEmpresa(), periodo, tipo, param);
        }

        public List<DetalleComprobante> IncentivoHistoricoDT(int periodo, int semana, DataTable dt)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            DataTable dtID = new DataTable();
            List<DetalleComprobante> lista = new List<DetalleComprobante>();
            foreach (DataRow row in dt.Rows)
            {
                DetalleComprobante objeto = new DetalleComprobante();
                objeto.Codigo = row["codigo_empleado"].ToString();
                objeto.Periodo = int.Parse(row["periodo"].ToString());
                objeto.Semana = int.Parse(row["semana"].ToString());
                objeto.Nombrecompleto = row["nombrecompleto"].ToString();
                objeto.Modulo = row["Modulo"].ToString();
                objeto.Estilo = int.Parse(row["Estilo"].ToString());
                objeto.Contruccion = row["Construccion"].ToString();
                objeto.Produccion = decimal.Parse(row["Produccion"].ToString());
                objeto.Metaalcanzada = decimal.Parse(row["metaAlcanzada"].ToString());
                objeto.Eficienciaalcanzada = decimal.Parse(row["EficienciaAlcanzada"].ToString());
                objeto.IncentivoMeta = decimal.Parse(row["IncentivoMeta"].ToString());
                objeto.TotalIngreso = decimal.Parse(row["TotalIngreso"].ToString());
                objeto.TotalEgreso = decimal.Parse(row["TotalDeducciones"].ToString());
                objeto.TotalIncentivo = decimal.Parse(row["TotalIncentivo"].ToString());
                objeto.UsuarioGraba = row["UsuarioGraba"].ToString();
                objeto.Fechagraba = DateTime.Parse(row["Fechagraba"].ToString());
                objeto.HoraG = TimeSpan.Parse(row["HoraGraba"].ToString());
                objeto.Operacion = row["operacion"].ToString();
                dtID = datoI.IncentivoIngDedccLOGSelect(userDetail.getIDEmpresa(), periodo, objeto.Semana, int.Parse(objeto.Codigo));

                foreach (DataRow rowID in dtID.Rows)
                {
                    dsPlanilla.dtIngresosDeduccIncentivosRow ID = objeto.DtIDInce.NewdtIngresosDeduccIncentivosRow();
                    ID.codigo = rowID["codigo"].ToString();
                    ID.periodo = rowID["periodo"].ToString();
                    ID.semana = rowID["semana"].ToString();
                    ID.tipo = rowID["tipo"].ToString();
                    ID.detalle = rowID["detalle"].ToString();
                    ID.tipoCalc = rowID["tipoCalc"].ToString();
                    ID.cantidad = rowID["cantidad"].ToString();
                    ID.valor = rowID["valor"].ToString();
                    ID.Observacion = rowID["Observacion"].ToString();

                    objeto.DtIDInce.AdddtIngresosDeduccIncentivosRow(ID);

                }



                lista.Add(objeto);
            }
            return lista;
        }
        public string SumaREstaDZ(Neg_Incentivos.IncentivoEmp dt, int codigosuma, int codigoresta, int op, decimal DZ)
        {
            //DataTable dtAutorizado = (DataTable)Session["Autorizaciones"];

            string mensaje = "";
            List<Neg_Incentivos.PIngDeducInc> param = (List<Neg_Incentivos.PIngDeducInc>)Session["PARAM"];
            //PRODUCCION DE LOS MODULOS
            List<Neg_Incentivos.ProdXmod> prodcmod = (List<Neg_Incentivos.ProdXmod>)Session["PRODUCCIONXMODULO"];
            List<Neg_Incentivos.EmpleadoOp> EmpleadosOP = (List<Neg_Incentivos.EmpleadoOp>)Session["EMPLEADOSOP"];
            List<IncentivoEmp> ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
            //EMPLEADOS
            List<Neg_Empleados> lt = (List<Neg_Empleados>)Session["EMPLEADOS"];
            List<Incentivos> li = (List<Neg_Incentivos.Incentivos>)Session["TABLAINCENTIVOS"];
            //SE OBTIENE LA INFORMACION DEL EMPLEADO
            // int codigo = 0;
            int codigo = 0;
            string operacionSuma = "", operacionResta = "";

            if (op == 1)
            {
                codigo = codigosuma;
            }
            else
            {
                codigo = codigoresta;
            }


            DataTable dtTraslado = (DataTable)Session["DTTRaslados"];
            DataTable dtTrasladoS = dtTraslado.Copy();
            DataTable dtTrasladoR = dtTraslado.Copy();
            DataView dtvTrasladoS = dtTrasladoS.DefaultView;
            DataView dtvTrasladoR = dtTrasladoR.DefaultView;

            dtvTrasladoS.RowFilter = "Codigo=" + codigo + " and Operacion=1";
            dtvTrasladoR.RowFilter = "Codigo=" + codigo + " and Operacion=2";
            decimal suma = dtvTrasladoS.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("Cantidad"))).Sum();
            decimal resta = dtvTrasladoR.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("Cantidad"))).Sum();
            decimal total = 0;
            int codigodepto = 0;
            bool empexist = false;
            //string 

            Neg_Empleados objeEmp = new Neg_Empleados();
            if (op == 1)
            {

                objeEmp = (Neg_Empleados)(from emp in lt where emp.codigo_empleado.Equals(codigosuma) select emp).SingleOrDefault();
                if (objeEmp != null)
                {
                    empexist = true;
                    codigodepto = objeEmp.codigo_depto;
                }

            }
            else
            {

                objeEmp = (Neg_Empleados)(from emp in lt where emp.codigo_empleado.Equals(codigoresta) select emp).SingleOrDefault();

                if (objeEmp == null)
                {
                    DataTable todosemple = (DataTable)Session["todosemp"];
                    Session["todosemp"] = todosemple;
                    DataTable todosemple2 = todosemple.Copy();
                    DataView todosemplev = todosemple2.DefaultView;
                    todosemplev.RowFilter = "codigo_empleado=" + codigoresta;
                    //el empleado no recibio pago pero si esta activo
                    if (todosemplev.ToTable().Rows.Count > 0)
                    {
                        //868783
                        codigodepto = int.Parse(todosemplev.ToTable().Rows[0][0].ToString());
                        empexist = true;
                    }
                    else { empexist = false; }
                }
                else
                {
                    codigodepto = objeEmp.codigo_depto;
                    empexist = true;
                }
            }


            DataTable dtmeta = (DataTable)Session["DiasMetasModulos"];

            //SI SE ENCUENTRA INFORMACION DEL CODIGO, SE PROCEDE
            if (empexist)
            {
                //YA QUE EL EMPLEADO EXISTE, SE BUSCA EN LA TABLA DE MODULOS CON META
                if (dt != null)
                {
                    if (op == 1)
                    {
                        if (Convert.ToBoolean(Session["Resto"]))
                        {
                            ActualizarMeta(dt, DZ, op, param);
                            Session["contadorop"] = int.Parse(Session["contadorop"].ToString()) + 1;
                        }
                        mensaje = "";
                    }
                    else
                    {

                        total = (dt.Produccion + suma);
                        if (total >= DZ)
                        {

                            ActualizarMeta(dt, DZ, op, param);
                            Session["Resto"] = true;
                            mensaje = "";
                            Session["contadorop"] = int.Parse(Session["contadorop"].ToString()) + 1;
                        }
                        else
                        {
                            mensaje = "Codigo a Restar ya no tiene Produccion disponible";
                        }
                    }

                }
                //SI NO EXISTE EN LA TABLA DE INCENTIVOS CON META, ENTONCES SOLO SE VERIFICA SI TUVO PRODUCCION,
                //PARA RESTARSELA Y SUMARLA A OTRO CODIGO, PERO NO SE LE BUSCA INCENTIVO PORQUE SI NO TUVO CON SU PRODUCCION MENOS QUE TENGA SI SE LE RESTA
                else
                {
                    //SE CONSULTA SI TIENE PRODUCCION SU MODULO
                    List<Neg_Incentivos.ProdXmod> produccionM = (List<Neg_Incentivos.ProdXmod>)(from prod in prodcmod where prod.Coddep.Equals(codigodepto) select prod).ToList();
                    DataTable dt2 = dtmeta.Copy();
                    DataView dtv = dt2.DefaultView;
                    //SI EL MODULO EN EL QUE TRABAJO TIENE PRODUCCION ESCANEADA
                    if ((produccionM != null) && (produccionM.Count > 0))
                    {
                        decimal produccionm;
                        string construccionm;
                        int estilom;
                        string modulo;
                        //SIN CONTIENE MAS DE 1 REGISTRO INDICA QUE TIENE PRODUCCION DE MAS DE UN ESTILO  CONTRUCCION
                        if (produccionM.Count > 1)
                        {
                            Neg_Incentivos.ProdXmod PMOD = (Neg_Incentivos.ProdXmod)(from i in produccionM orderby i.Produccion descending select i).FirstOrDefault();
                            produccionm = (from i in produccionM select i.Produccion).Sum();
                            construccionm = PMOD.Construccion.ToString().Trim();
                            estilom = PMOD.EstiloP;
                            modulo = PMOD.Modulo;
                        }
                        //SI SOLO CONTIENE UN REGISTRO
                        else
                        {
                            produccionm = produccionM[0].Produccion;
                            construccionm = produccionM[0].Construccion.ToString().Trim();
                            estilom = produccionM[0].EstiloP;
                            modulo = produccionM[0].Modulo;
                        }

                        dtv.RowFilter = "Modulo=" + modulo;
                        if (op == 1)
                        {
                            if (Convert.ToBoolean(Session["Resto"]))
                            {

                                if (dtv != null)
                                {
                                    if (dtv.Count > 0)
                                    {
                                        Neg_Incentivos.Incentivos nuevoincentivo = (Neg_Incentivos.Incentivos)(from incentivos in li where ((incentivos.Meta5 / 5) * (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString()))) <= (produccionm + DZ + suma) && incentivos.Construccion.Equals(construccionm) orderby incentivos.Meta5 descending select incentivos).FirstOrDefault();
                                        if (nuevoincentivo != null)
                                        {

                                            // Codigo_depto = objeEmp.codigo_empleado;
                                            decimal produccion = Convert.ToDecimal(produccionm) + DZ + suma;
                                            decimal meta = ((nuevoincentivo.Meta5 / 5) * (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString())));
                                            decimal incentivo = (((nuevoincentivo.Incentivo / 5) * (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString()))));

                                            decimal eficienciac = nuevoincentivo.Eficiencia;


                                            ListIE.Add(ArmarDatosIncetivosEmpleado(objeEmp, construccionm, nuevoincentivo.Proceso, EmpleadosOP, param, modulo, estilom, produccion, decimal.Parse(meta.ToString()), incentivo, eficienciac, (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString()))));
                                            Session["DTIncentivosSP"] = ListIE;
                                            mensaje = "";
                                            Session["contadorop"] = int.Parse(Session["contadorop"].ToString()) + 1;
                                        }
                                        else
                                        {
                                            // dtRegistroTrasDZ.Rows.Add(codigo1, DZ, 1);
                                            mensaje = "";
                                        }
                                    }
                                    else
                                    {
                                        mensaje = "NO SE ENCONTRARON DIAS METAS PARA EL MODULO " + modulo;
                                    }

                                }
                                else
                                {
                                    mensaje = "NO SE ENCONTRARON DIAS METAS PARA EL MODULO " + modulo;
                                }
                            }
                            else
                            {
                                mensaje = "NO SE PUEDE SUMAR PRODUCCION PORQUE EL CODIGO AL QUE SE LE RESTA NO SE LE ENCONTRO PRODUCCION ESCANEADA";
                            }
                        }
                        else
                        {

                            //LA OPERACION ES RESTA, ENTONCES SE VERIFICA QUE EL MODULO PRODUJO LO SUFICIENTE COMO PARA PODER RESTARLE LAS DZ QUE SE NECESITAN
                            if ((produccionm + suma) - resta >= DZ)
                            {
                                Session["Resto"] = true;
                                Session["contadorop"] = int.Parse(Session["contadorop"].ToString()) + 1;

                            }
                            else
                            {
                                Session["Resto"] = false;
                            }
                            mensaje = "";
                        }


                    }
                    //NO TIENE PRODUCCION ASIGNADA, PERO SI ES SUMA SE DEBE HACER LA OPERACION YA QUE PUEDE SER EL CASO DE UN EMPLEADO QUE PERTENECE A UN MODULO
                    //DESARMADO Y EL APOYO EN OTRO
                    else
                    {
                        if (op == 1)
                        {
                            //SI SE RESTO INDICA QUE EL EMPLEADO DEL QUE PROVIENEN LAS DZ SI SON VALIDAS
                            if (Convert.ToBoolean(Session["Resto"]))
                            {
                                Neg_Empleados objeEmp2 = (Neg_Empleados)(from emp in lt where emp.codigo_empleado.Equals(codigoresta) select emp).SingleOrDefault();


                                if (objeEmp2 != null)
                                {
                                    decimal meta = 0;
                                    //se obtiene la produccion del empleado de donde proviene las dz
                                    List<Neg_Incentivos.ProdXmod> produccionM2 = (List<Neg_Incentivos.ProdXmod>)(from prod in prodcmod where prod.Coddep.Equals(objeEmp2.codigo_depto) select prod).ToList();
                                    DataTable dt2p = dtmeta.Copy();
                                    DataView dtv2 = dt2p.DefaultView;
                                    if (produccionM2 != null)
                                    {
                                        decimal produccionm;
                                        string construccionm;
                                        int estilom;
                                        int modulo;


                                        //SIN CONTIENE MAS DE 1 REGISTRO INDICA QUE TIENE PRODUCCION DE MAS DE UN ESTILO  CONTRUCCION
                                        if (produccionM2.Count > 1)
                                        {
                                            Neg_Incentivos.ProdXmod PMOD = (Neg_Incentivos.ProdXmod)(from i in produccionM2 orderby i.Produccion descending select i).FirstOrDefault();
                                            produccionm = DZ;
                                            construccionm = PMOD.Construccion.ToString().Trim();
                                            estilom = PMOD.EstiloP;
                                            modulo = int.Parse(PMOD.Modulo);
                                        }
                                        //SI SOLO CONTIENE UN REGISTRO
                                        else
                                        {
                                            produccionm = DZ;
                                            construccionm = produccionM2[0].Construccion.ToString().Trim();
                                            estilom = produccionM2[0].EstiloP;
                                            modulo = int.Parse(produccionM2[0].Modulo);
                                        }

                                        dtv.RowFilter = "Modulo=" + modulo;
                                        if (dtv != null)
                                        {
                                            if (dtv.Count > 0)
                                            {
                                                meta = decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString());
                                            }
                                        }


                                        Neg_Incentivos.Incentivos nuevoincentivo = (Neg_Incentivos.Incentivos)(from incentivos in li where ((incentivos.Meta5 / 5) * (meta)) <= (DZ) && incentivos.Construccion.Equals(construccionm) orderby incentivos.Meta5 descending select incentivos).FirstOrDefault();
                                        if (nuevoincentivo != null)
                                        {

                                            // Codigo_depto = objeEmp.codigo_empleado;
                                            decimal produccion = DZ;
                                            decimal meta2 = ((nuevoincentivo.Meta5 / 5) * (decimal.Parse(dtv[0]["MetadDiasIncentivo"].ToString())));
                                            decimal incentivo = (((nuevoincentivo.Incentivo / 5) * (meta)));

                                            List<Neg_Empleados> listaEmp = new List<Neg_Empleados>();
                                            listaEmp.Add(objeEmp);


                                            decimal eficienciac = nuevoincentivo.Eficiencia;

                                            string moduloempsuma = "0";
                                            DataTable dtmodulosuma = PlnDEpartamentosObtenerModulo(codigodepto);
                                            if (dtmodulosuma.Rows.Count > 0)
                                            {
                                                moduloempsuma = dtmodulosuma.Rows[0][0].ToString();
                                            }

                                            ListIE.Add(ArmarDatosIncetivosEmpleado(objeEmp, construccionm, nuevoincentivo.Proceso, EmpleadosOP, param, moduloempsuma, estilom, produccion, decimal.Parse(meta.ToString()), incentivo, eficienciac, (meta)));
                                            Session["DTIncentivosSP"] = ListIE;
                                            Session["contadorop"] = int.Parse(Session["contadorop"].ToString()) + 1;
                                        }
                                        mensaje = "";
                                    }
                                    else
                                    {
                                        Session["Resto"] = false;
                                        mensaje = "NO SE PUEDEN SUMAR DZ QUE NO EXISTEN PARA EL CODIGO A RESTAR";
                                    }

                                }
                                else
                                {
                                    Session["Resto"] = false;
                                    mensaje = "NO SE PUEDEN SUMAR DZ QUE NO EXISTEN PARA EL CODIGO A RESTAR";


                                }
                            }
                            else
                            {
                                mensaje = "NO SE PUEDEN SUMAR DZ QUE NO EXISTEN PARA EL CODIGO A RESTAR";

                            }
                        }
                        else
                        {

                            Session["Resto"] = false;
                            mensaje = "EL CODIGO A RESTAR NO TIENE PRODUCCION REGISTRADA";

                        }
                    }
                }

            }
            else
            {
                if (op == 1)
                {
                    mensaje = "EL CODIGO A SUMAR NO PERTENECE A UN EMPLEADO EXISTENTE";
                }

                else
                {
                    mensaje = "EL CODIGO A RESTAR NO PERTENECE A UN EMPLEADO EXISTENTE";
                }


            }
            return mensaje;

        }
        public bool IncentivoHistoricoDZTrasInsert(int periodo, int semana, string codigoOrigen, string codigoDestino, decimal Dz, string observacion, string comentario)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivoHistoricoDZTrasInsert(userDetail.getIDEmpresa(), periodo, semana, codigoOrigen, codigoDestino, Dz, observacion, comentario))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }

        public bool IncentivoHistoricoDZTrasDelete(int periodo, int semana)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivoHistoricoDZTrasDelete(userDetail.getIDEmpresa(), periodo, semana))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }
        public DataTable CosModulosSel()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            return datoI.CosModulosSel();
        }

        public DataTable PlnDEpartamentosObtenerModulo(int codigoDEpto)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.PlnDEpartamentosObtenerModulo(userDetail.getIDEmpresa(), codigoDEpto);
        }
        public decimal redondeo(decimal cantidad)
        {
            decimal b = (cantidad % 1);

            if (Convert.ToDouble(b) < 0.5)
                cantidad = Math.Round(cantidad);
            else if (Convert.ToDouble(b) >= 0.5)
                cantidad = Math.Ceiling(cantidad);

            return cantidad;
        }

        public bool IncentivosTrasladosEspecialesAutorizadosInsert(int periodo, int semana, string codigo, string OP, string UsuarioAutoriza)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivosTrasladosEspecialesAutorizadosInsert(userDetail.getIDEmpresa(), periodo, semana, codigo, OP, UsuarioAutoriza))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }

        public DataTable EmpleadosPagosXOperacionSelect()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            return datoI.EmpleadosPagosXOperacionSelect(userDetail.getIDEmpresa());
        }

        public DataTable plnDevengadoSelect()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            return datoI.plnDevengadoSelect(userDetail.getIDEmpresa());
        }

        public decimal IncentivoConAQL(decimal incentivo, string modulo)
        {
            decimal aqlmeta = 0, aqlsemana = 0;
            DataTable dtAQL = new DataTable();
            try
            {
                dtAQL = (DataTable)Session["AQL"];
                DataRow[] dtvAQL = dtAQL.Copy().Select("Modulo='" + modulo + "'");
                if (dtvAQL.Length > 0)
                {
                    DataTable dtAQLD = new DataTable();
                    dtAQLD = dtvAQL.CopyToDataTable<DataRow>();

                    aqlmeta = decimal.Parse(dtAQLD.Rows[0][2].ToString());
                    aqlsemana = decimal.Parse(dtAQLD.Rows[0][1].ToString());
                }
            }
            catch
            {

            }


            decimal Per = 0;
            if (aqlsemana > 0)
            {
                Per = (aqlmeta / aqlsemana) * 100;
                if (Per > 100)
                {
                    Per = 100;
                }
            }
            if (Per >= 85)
            {
                incentivo = (incentivo * Per) / 100;
            }
            else
            {
                incentivo = 0;
            }

            return incentivo;
        }


        public bool IncentivoAQLxModuloInsert(int semana, int periodo, string modulo, decimal AQL, decimal AQL_Meta, decimal Porcentaje)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivoAQLxModuloInsert(userDetail.getIDEmpresa(), semana, periodo, modulo, AQL, AQL_Meta, Porcentaje))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }

        public DataTable IncentivoAQLxModuloSelect(int semana, int periodo, int modulo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            return datoI.IncentivoAQLxModuloSelect(userDetail.getIDEmpresa(), semana, periodo, modulo);
        }

        public DataTable IncentivoAQLxModuloSelectSemanaPEriodo(int semana, int periodo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            return datoI.IncentivoAQLxModuloSelectSemanaPEriodo(userDetail.getIDEmpresa(), semana, periodo);
        }
        public DataTable IncentivosHistoricoGetXEmpleado(int semana, int periodo, string codigo, int idtipo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivosHistoricoGetXEmpleado(userDetail.getIDEmpresa(), semana, periodo, codigo, idtipo);

        }


        public bool IncentivosHistoricoDelete(int periodo, int semana, int tipoIng, bool GeneradoSistema)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivosHistoricoDelete(userDetail.getIDEmpresa(), periodo, semana, tipoIng, GeneradoSistema))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }

        public bool IncentivoIngDedccLOGDelete(int periodo, int semana, int tipoIng, bool GeneradoSistema)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivoIngDedccLOGDelete(userDetail.getIDEmpresa(), periodo, semana, tipoIng, GeneradoSistema))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }


        /////////////////////////////////////NUEVO///////////////////////////

        public DataTable IncentivosHistoricoGetXEmpleado(int semana, int periodo, string codigo)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.IncentivosHistoricoGetXEmpleado(userDetail.getIDEmpresa(), semana, periodo, codigo);


        }
        ////////////////////////////////////////////////////////////////////////////////////////	
        public bool IncentivoIngDedccLOGInsert(int codigo, int periodo, int semana, int tipo, string detalle, int tipocalc, decimal cantidad, decimal valor, string observacion, int tipoing, bool GeneradoSistema)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivoIngDedccLOGInsert(userDetail.getIDEmpresa(), codigo, periodo, semana, tipo, detalle, tipocalc, cantidad, valor, observacion, tipoing, GeneradoSistema))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }

        public DataTable EmpleadosPagosXOperacionGetPagossFijos()
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            return datoI.EmpleadosPagosXOperacionGetPagossFijos(userDetail.getIDEmpresa());
        }

        public void GenerarPagosProteccion(DataTable dtEmpIncFijo, int semana, DateTime fechaini, DateTime fechafin, int idempresa, int periodo)
        {
            List<Neg_Incentivos.IncentivoEmp> ListIE = new List<Neg_Incentivos.IncentivoEmp>();
            ListIE = (List<Neg_Incentivos.IncentivoEmp>)Session["DTIncentivosSP"];
            List<Neg_Empleados> lt1 = new List<Neg_Empleados>(); //ESTA LISTA CONTIENE TODOS LOS EMPLEADOS DE LA PLANILLA
            List<Neg_Amonestaciones.Amonestaciones> la = new List<Neg_Amonestaciones.Amonestaciones>();
            la = (List<Neg_Amonestaciones.Amonestaciones>)Session["AMONESTACIONES"];

            List<Neg_Empleados> lt = new List<Neg_Empleados>();
            lt = (List<Neg_Empleados>)Session["EMPLEADOS"];


            List<Neg_Incentivos.PIngDeducInc> param = new List<Neg_Incentivos.PIngDeducInc>();
            param = (List<Neg_Incentivos.PIngDeducInc>)Session["PARAM"];

            Neg_Marca nm = new Neg_Marca();
            lt1 = nm.ObtenerHT(fechaini, fechafin, 3, 1, idempresa);

            foreach (DataRow item in dtEmpIncFijo.Rows)

            {
                //DataView dtvEmplInF = dtEmpIncFijo.Copy().DefaultView;

                //for (int i = 0; i < ListIE.Count; i++)
                //{

                Neg_Incentivos.IncentivoEmp empprueba = (from myrow in ListIE where myrow.Codigo.Equals(int.Parse(item["codigo"].ToString())) select myrow).SingleOrDefault();
                if (empprueba != null)
                {
                    foreach (Neg_Incentivos.IncentivoEmp fila in ListIE.Where(w => w.Codigo.Equals(int.Parse(item["codigo"].ToString()))))
                    {
                        IncentivoIngDedccLOGDeletexEmpleado(periodo, semana, 4, true, int.Parse(item["codigo"].ToString()), 0);

                        empprueba.Incentivo = decimal.Parse(item["Semana" + semana.ToString()].ToString());
                        empprueba.IngresoNumerico = decimal.Parse(item["Semana" + semana.ToString()].ToString());
                        empprueba.Total = decimal.Parse(item["Semana" + semana.ToString()].ToString());
                        empprueba.DetalleIngreso = item["TipoPreferencia"].ToString();
                        empprueba.IngresoPorcentual = 0;
                        empprueba.Ingreso = decimal.Parse(item["Semana" + semana.ToString()].ToString()) - empprueba.Deduccion;
                        empprueba.Eficiencia = 0;

                        IncentivoEmp empIngD = IngresosDeduccion(empprueba, param, 0, 0, 0, 0);

                        empprueba.DetalleEgreso = empIngD.DetalleEgreso;
                        empprueba.DeduccionNumerica = empIngD.DeduccionNumerica;
                        empprueba.DeduccionPorcentual = empIngD.DeduccionPorcentual;
                        empprueba.Deduccion = empIngD.Deduccion;

                        empprueba.Total = empprueba.Incentivo - empprueba.Deduccion;

                        IncentivoIngDedccLOGDeletexEmpleado(periodo, semana, 4, true, int.Parse(item["codigo"].ToString()), 1);

                    }
                }

                else
                {
                    Neg_Incentivos.IncentivoEmp emp2 = new Neg_Incentivos.IncentivoEmp();
                    Neg_Empleados empleado2 = new Neg_Empleados();
                    empleado2 = lt.Where(x => x.codigo_empleado.Equals(int.Parse(item["codigo"].ToString()))).SingleOrDefault();
                    if (empleado2 == null)
                    {
                        empleado2 = lt1.Where(x => x.codigo_empleado.Equals(int.Parse(item["codigo"].ToString()))).SingleOrDefault();
                    }
                    if (empleado2 != null)
                    {
                        string dep = empleado2.departamento;
                        bool esint = false;
                        foreach (var ch in dep)
                        {
                            if (Char.IsNumber(ch))
                            {
                                esint = true;
                            }
                        }

                        if (esint)
                        {
                            string dep2 = dep;
                            if (dep.Length > 2)
                            {
                                dep2 = dep.Substring(7, 2);
                                emp2.Modulo = dep2;
                            }
                            else
                            {
                                emp2.Modulo = dep;
                            }

                        }
                        else
                        {
                            emp2.Modulo = "0";
                        }

                        emp2.NombreCompleto = empleado2.nombrecompleto;
                        emp2.Estilo = 0;
                        emp2.Codigo = int.Parse(item["codigo"].ToString());
                        emp2.Construccion = "";
                        emp2.Produccion = 0;
                        emp2.Proceso = "";
                        emp2.Meta = 0;
                        emp2.DetalleIngreso = item["TipoPreferencia"].ToString();
                        emp2.Incentivo = decimal.Parse(item["Semana" + semana.ToString()].ToString());
                        emp2.Eficiencia = 0;

                        DataTable dtHorasT = empleado2.dtHorasT;
                        //validacion para no incluir dias con marcas pero fuera del turno
                        DataTable diasLaborados = dtHorasT.Select("horasturno>0").CopyToDataTable();
                        int totaldiasTrab = diasLaborados.Rows.Count;

                        obtenerDiasTrabYAusenciasJ(diasLaborados, Convert.ToDateTime(empleado2.fecheingreso));
                        emp2.DiasLaborales = totaldiasTrab;
                        emp2.DiasLaborados = NdiasL;
                        emp2.DiasJustificados = NdiasJ;
                        emp2.DiasAusencias = (totaldiasTrab - NdiasL);
                        //emp2.Operacion = "";
                        emp2.Horasaunsencia = float.Parse("48") - TotalHoraSemana;
                        emp2.Eficiencia = 0;
                        emp2.Amonestaciones = 0;
                        emp2.DiasInjustificados = emp2.DiasAusencias - emp2.DiasJustificados;
                        emp2.MetasModulo = 0;
                        emp2.TipoIngr = 4;
                        emp2.Comentario = "GENERADO DESDE SISTEMA";
                        emp2.Operacion = empleado2.departamento;

                        if (emp2.Horasaunsencia < 0)
                        {
                            emp2.Horasaunsencia = 0;
                        }
                        Neg_Amonestaciones.Amonestaciones amone = (Neg_Amonestaciones.Amonestaciones)(from tabla in la where tabla.Codigo_empleado.Equals(int.Parse(item["codigo"].ToString())) select tabla).SingleOrDefault();

                        if (amone != null)
                        {
                            emp2.Amonestaciones = amone.Cantidad;
                        }


                        IncentivoEmp empIngD = IngresosDeduccion(emp2, param, 0, 0, 0, 0);

                        //emp2.DetalleIngreso = "";
                        emp2.DetalleEgreso = empIngD.DetalleEgreso;
                        emp2.IngresoNumerico = emp2.Incentivo;
                        emp2.IngresoPorcentual = 0;
                        emp2.DeduccionNumerica = empIngD.DeduccionNumerica;
                        emp2.DeduccionPorcentual = empIngD.DeduccionPorcentual;

                        emp2.Ingreso = emp2.Incentivo;
                        emp2.Deduccion = empIngD.Deduccion;

                        emp2.Total = emp2.Incentivo - emp2.Deduccion;


                        ListIE.Add(emp2);
                        Session["DTIncentivosSP"] = ListIE;

                        DataTable dtInD = (DataTable)Session["IngDed"];
                    }
                    else
                    { }


                }
                // }


            }


        }

        public bool IncentivoIngDedccLOGDeletexEmpleado(int periodo, int semana, int tipoIng, bool GeneradoSistema, int codigo, int idtipoProceso)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.IncentivoIngDedccLOGDeletexEmpleado(userDetail.getIDEmpresa(), periodo, semana, tipoIng, GeneradoSistema, codigo, idtipoProceso))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (SystemException)
            {

                return false;
            }

        }
        //NUEVOS SP CALCULO INCENTIVOS POR DIA
        public DataTable PlnObtenerTablaIncentivo()
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerTablaIncentivo();
        }

        public DataTable PlnModuloIncentivoAreaSel()
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnModuloIncentivoAreaSel();
        }

        public DataTable PlnObtenerLayoutxEstilo()
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerLayoutxEstilo();
        }

        public DataTable PlnPagoIncentivobyCutSel(int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnPagoIncentivobyCutSel(periodo);
        }

        public DataTable PlnParametroCalculoIncEmpSel(int periodo, int semana)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnParametroCalculoIncEmpSel(periodo, semana, userDetail.getIDEmpresa());
        }

        public DataTable PlnConfigPeriodoIncentivoSel(DateTime fechaini, DateTime fechafin, int p)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();


            return datoI.PlnConfigPeriodoIncentivoSel(fechaini, fechafin, userDetail.getIDEmpresa());
        }

        public DataTable PlnObtenerOperacionCriticaSel()
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.PlnObtenerOperacionCriticaSel(userDetail.getIDEmpresa());
        }

        public DataTable PlnObtenerProteccionModulo(DateTime fechaini, DateTime fechafin)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerProteccionModulo(fechaini, fechafin);
        }

        public DataTable PlnObtenerPersonalIncFueraLayout(int periodo, int filtro)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.PlnObtenerPersonalIncFueraLayout(periodo, filtro, userDetail.getIDEmpresa());
        }

        public DataTable PlnObtenerEficienciaModuloHist(DateTime fechaini, DateTime fechafin)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerEficienciaModuloHist(fechaini, fechafin);
        }

        public DataTable PlnObtenerEficienciaModuloByPeriodo(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerEficienciaModuloByPeriodo(periodo, semana);
        }

        public DataTable PlnObtenerRangoOql()
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerRangoOql();
        }

        public DataTable PlnTablaOperacionCriticaSel()
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnTablaOperacionCriticaSel();
        }

        public DataTable PlnObtenerRangoAdicionales()
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerRangoAdicionales();
        }

        public DataTable PlnObtenerProduccionAprobadaByPeriodo(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int vista, int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerProduccionAprobadaByPeriodo(fechaini, fechafin, corteaprobacion, vista, periodo);
        }

        public DataTable PlnObtenerProdPendienteByPeriodo(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerProdPendienteByPeriodo(fechaini, fechafin, corteaprobacion, periodo);
        }

        public DataTable PlnObtenerProduccionByPeriodo(DateTime fechaini, DateTime fechafin)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerProduccionByPeriodo(fechaini, fechafin);
        }

        public DataTable PlnObtenerProduccionDetalleByPeriodo(DateTime fechaini, DateTime fechafin)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerProduccionDetalleByPeriodo(fechaini, fechafin);
        }

        public DataTable PlnObtenerProduccionBono(DateTime fechaini, DateTime fechafin)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerProduccionBono(fechaini, fechafin);
        }

        public DataTable PlnObtenerIngresosPTByPeriodo(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerIngresosPTByPeriodo(fechaini, fechafin, corteaprobacion, periodo);
        }

        public DataTable PlnObtenerOQLModulosProd(DateTime fechaini, DateTime fechafin, int tipo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerOQLModulosProd(fechaini, fechafin, tipo);
        }

        public DataTable PlnOqlPeriodoPagoSel(int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnOqlPeriodoPagoSel(periodo);
        }

        public DataTable PlnObtenerIncidenciasCortesProd(DateTime fechaini, DateTime fechafin)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            return datoI.PlnObtenerIncidenciasCortesProd(fechaini, fechafin);
        }

        public decimal PlnObtenerEficienciaDist(DataRow[] source, DateTime fc, bool hist)
        {
            decimal eficiencia = default(decimal);
            decimal eficienciaSemana = default(decimal);
            DataRow[] metaSemAlc = null;
            DataRow[] sinMeta = null;
            DataRow[] eficienciaSem = null;
            Neg_Marca neg_Marca = new Neg_Marca();
            string modulo = "";
            DataTable areaMod = Session["ModuloArea"] as DataTable;
            DataRow[] areaModList = null;
            decimal docenassem = default(decimal);
            decimal eficienciaMin = default(decimal);
            eficienciaSem = ((!hist) ? source : source.Where((DataRow c) => c.Field<int>("nsemana") == neg_Marca.GetIso8601WeekOfYear(fc)).ToArray());
            modulo = eficienciaSem.Select((DataRow c) => c.Field<string>("modulo")).FirstOrDefault();
            docenassem = eficienciaSem.Select((DataRow c) => c.Field<decimal>("dzSem")).FirstOrDefault();
            areaModList = (from c in areaMod.AsEnumerable()
                           where c.Field<string>("modulo").Trim() == modulo && c.Field<int>("area") == 2
                           select c).ToArray();
            if (areaModList.Length != 0 && docenassem < 880.0m)
            {
                eficienciaMin = 87.10m;
            }
            else
            {
                eficienciaMin = 70m;
            }
            metaSemAlc = eficienciaSem.Where((DataRow c) => c.Field<decimal>("eficienciaSem") >= eficienciaMin).ToArray();
            if (metaSemAlc.Length != 0)
            {
                eficienciaSemana = eficienciaSem.Select((DataRow c) => c.Field<decimal>("eficienciaSem")).FirstOrDefault();
                return (eficienciaSemana >= eficienciaMin) ? eficienciaSemana : 0m;
            }
            return eficiencia;
        }

        public DataSet PlnObtenerEficienciaMod(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int periodo, int semana)
        {
            DataSet ds = new DataSet();
            DataTable dtprod = new DataTable();
            dtprod.Columns.Add("modulo", typeof(string));
            dtprod.Columns.Add("diasSem", typeof(int));
            dtprod.Columns.Add("eficienciaSem", typeof(decimal));
            dtprod.Columns.Add("dzSem", typeof(decimal));
            dtprod.Columns.Add("factorDia", typeof(decimal));
            dtprod.Columns.Add("fecha_producido", typeof(DateTime));
            dtprod.Columns.Add("eficienciaDia", typeof(decimal));
            dtprod.Columns.Add("dzpagarDia", typeof(decimal));
            dtprod.Columns.Add("dzpendDia", typeof(decimal));
            dtprod.Columns.Add("dzprotDia", typeof(decimal));
            dtprod.Columns.Add("dztotalDia", typeof(decimal));
            dtprod.Columns.Add("diferenciaDia", typeof(decimal));
            dtprod.Columns.Add("periodo", typeof(int));
            dtprod.Columns.Add("semana", typeof(int));
            dtprod.Columns.Add("dzprodRep", typeof(decimal));
            dtprod.Columns.Add("dzprodAdic", typeof(decimal));
            dtprod.Columns.Add("dzprodCump", typeof(decimal));
            dtprod.Columns.Add("eficienciaSemReal", typeof(decimal));
            DataTable dtModCalidad = new DataTable();
            dtModCalidad.Columns.Add("modulo", typeof(string));
            dtModCalidad.Columns.Add("IncidenciaInternaC", typeof(decimal));
            dtModCalidad.Columns.Add("IncidenciaExternaC", typeof(decimal));
            dtModCalidad.Columns.Add("TotalIncidencia", typeof(decimal));
            decimal docenasperiodo = default(decimal);
            decimal docenaspagarDia = default(decimal);
            decimal docenaspendDia = default(decimal);
            decimal docenasprotDia = default(decimal);
            decimal docenastotalDia = default(decimal);
            decimal docenasprodDia = default(decimal);
            decimal docenasprodRep = default(decimal);
            decimal docenasprodAdic = default(decimal);
            decimal docenasprodCump = default(decimal);
            decimal docenasPermitidas = default(decimal);
            decimal dias = 0;
            DataRow[] datoModR = null;
            DataRow[] semanaProd = null;
            DataRow[] datoModPend = null;
            DataRow[] datoModAprob = null;
            decimal eficienciaSem = default(decimal);
            decimal eficienciaDia = default(decimal);
            decimal eficienciaSemReal = default(decimal);
            decimal factorDia = default(decimal);
            string modulo = "";
            decimal metamaxDia = default(decimal);
            int incidencia_internaC = 0;
            int incidencia_externaC = 0;
            DataRow[] calidadseccion = null;
            DataTable config = Session["tablaConfig"] as DataTable;
            dias = ((config.Rows.Count <= 0) ? 5 : Convert.ToInt32(config.Rows[0]["diasPagar"]));
            try//wb3
            {
                DataTable pend = PlnObtenerProdPendienteByPeriodo(fechaini, fechafin, corteaprobacion, periodo);//Pendiente de aprobacion
                //DataTable dtmod = plnObtenerProdAPagarxMod(fechaini, fechafin.AddDays(1.0), corteaprobacion, 2, periodo).Tables[1];//Producion semanal
                DataTable dtmod = plnObtenerProdAPagarxMod(fechaini, fechafin, corteaprobacion, 2, periodo).Tables[1];//Producion semanal

                Session["produccionPeriodo"] = dtmod;
                DataTable aprobado = PlnObtenerIngresosPTByPeriodo(fechaini, fechafin, corteaprobacion, periodo);//Produccion aprobada
                DataTable IncidenciasCalidad = PlnObtenerIncidenciasCortesProd(fechaini, fechafin);//Consulta el historico de subestatus que no pagan bonos de calidad
                DataRow[] modulos = (from c in dtmod.AsEnumerable()//obtiene todo los modulos
                                     group c by new
                                     {
                                         c2 = c["modulo"]
                                     } into grp
                                     select grp.First()).ToArray();
                DataRow[] array = modulos;
                foreach (DataRow item2 in array)//Recorre los modulos.
                {
                    docenaspendDia = default(decimal);
                    incidencia_internaC = 0;
                    incidencia_externaC = 0;
                    modulo = item2["modulo"].ToString().Trim();
                    //****
                    //decimal dzdesde = item2["dzdesde"] != DBNull.Value ? Convert.ToDecimal(item2["dzdesde"]) : 0;
                    //decimal dzhasta = item2["dzhasta"] != DBNull.Value ? Convert.ToDecimal(item2["dzhasta"]) : 0;
                    //****
                    // PlnObtenerIngresosPTByPeriodo o Produccion aprobada
                    datoModAprob = (from a in aprobado.AsEnumerable()
                                    where a.Field<string>("modulo").Trim() == modulo
                                    select a).ToArray();
                    if (pend != null && pend.Rows.Count > 0)
                    {
                        datoModPend = (from a in pend.AsEnumerable()
                                       where a.Field<string>("modulo").Trim() == modulo
                                       select a).ToArray();
                    }

                    if (IncidenciasCalidad.Rows.Count > 0)
                    {
                        calidadseccion = (from c in IncidenciasCalidad.AsEnumerable()
                                          where Convert.ToDateTime(c.Field<string>("fecha").Trim()) >= fechaini && Convert.ToDateTime(c.Field<string>("fecha").Trim()) <= fechafin.AddDays(1.0) && c.Field<string>("modulo").Trim() == modulo
                                          select c).ToArray();
                        incidencia_internaC = calidadseccion.Where((DataRow c) => c.Field<int>("codigo_subestatus") == 52).Count();
                        incidencia_externaC = calidadseccion.Where((DataRow c) => c.Field<int>("codigo_subestatus") == 41).Count();
                    }

                    dtModCalidad.Rows.Add(modulo, incidencia_internaC, incidencia_externaC, incidencia_internaC + incidencia_externaC);

                    datoModR = (from a in dtmod.AsEnumerable()
                                where a.Field<string>("modulo") == modulo
                                select a into c
                                group c by c.Field<DateTime>("fecha_producido") into c
                                select c.First()).ToArray();//obtiene lo que se produjo en la semana

                    semanaProd = (Convert.ToBoolean(config.Rows[0]["incluyeFinSem"]) ? datoModR.ToArray() : datoModR.Where((DataRow c) => c.Field<DateTime>("fecha_producido").DayOfWeek >= DayOfWeek.Monday && c.Field<DateTime>("fecha_producido").DayOfWeek <= DayOfWeek.Friday).ToArray());

                    docenasperiodo = semanaProd.Sum((DataRow r) => r.Field<decimal>("docenasprodprot"));//sum para totalizar las docenas.


                    //obtiene la tabla de incentivo del modulo
                    // filtra por el modulo todos los registro de la tabla PlnTablaIncentivo * Parametros de Incentivos
                    // Esta tabla se cambia manualmente desde el menu del sistema en Incentivos
                    DataRow[] tablaincxconst = PlnObtenerTablaIncentivo(semanaProd, 1, 2, 0, modulo, docenasperiodo);
                    eficienciaSem = default(decimal);
                    eficienciaSemReal = default(decimal);
                    decimal prueba = docenasperiodo / dias;
                    if (tablaincxconst != null && tablaincxconst.Length != 0)
                    {
                        var filaValida = tablaincxconst.AsEnumerable()
                            .Where(c => prueba >= c.Field<decimal>("dzdesde") && prueba <= c.Field<decimal>("dzhasta"))
                            .FirstOrDefault();
                        valor = 200;
                        if (filaValida != null)
                        {
                            // Haz algo con 'filaValida', que es un DataRow
                            valor = filaValida.Field<decimal>("metamaxDia");
                            Console.WriteLine($"Valor obtenido: {valor}");
                        }
                        
                        metamaxDia = valor; // 200;                     

                        eficienciaSem = Math.Round(docenasperiodo / (metamaxDia * (decimal)dias) * 100m, 2);
                        eficienciaSemReal = eficienciaSem;
                    }

                    factorDia = default(decimal);

                    // Produccion Semanal o plnObtenerProdAPagarxMod
                    // elemento representa un dia
                    DataRow[] array2 = datoModR;

                    // Calculos 
                    foreach (DataRow dr in array2)
                    {
                        eficienciaDia = default(decimal);
                        // evalua en PlnObtenerIngresosPTByPeriodo o Produccion aprobada
                        // por la fecha de producido suma el total del campo Dz_a_Pagar
                        docenaspagarDia = datoModAprob.Where((DataRow c) => c.Field<DateTime>("fecha_producido") == Convert.ToDateTime(dr["fecha_producido"])).Sum((DataRow r) => r.Field<decimal>("Dz_a_Pagar"));
                        docenasprodDia = Convert.ToDecimal(dr["docenasprod"]);
                        docenasprotDia = Convert.ToDecimal(dr["docenasproteccion"]);
                        docenastotalDia = Convert.ToDecimal(dr["docenasprodprot"]);
                        if (datoModPend != null)
                        {
                            docenaspendDia = datoModPend.Where((DataRow c) => c.Field<DateTime>("fecha_producido") == Convert.ToDateTime(dr["fecha_producido"])).Sum((DataRow r) => r.Field<decimal>("Dz_a_Pagar"));
                        }
                        DataRow[] fechas = datoModR.Where((DataRow c) => c.Field<DateTime>("fecha_producido") == Convert.ToDateTime(dr["fecha_producido"])).ToArray();
                        eficienciaDia = Math.Round(docenastotalDia / metamaxDia, 2) * 100m;
                        dtprod.Rows.Add(modulo, dias, eficienciaSem, docenasperiodo, factorDia, Convert.ToDateTime(dr["fecha_producido"]), eficienciaDia, docenaspagarDia, docenaspendDia, docenasprotDia, docenastotalDia, 0, periodo, semana, docenasprodRep, docenasprodAdic, docenasprodCump, eficienciaSemReal);
                    }
                }
                ds.Tables.Add(dtprod);
                ds.Tables.Add(dtModCalidad);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static bool PlnPagoIncentivoCortes(DataTable dtcut, string comentario, string user)
        {
            using (SqlConnection conn = new CnBD().GetConecctionC())
            {
                try
                {
                    conn.Open();

                   foreach (DataRow row in dtcut.Rows)
                    {
                        
                        string corte = row["corte"].ToString();
                        int seccion = Convert.ToInt32(row["seccion"].ToString());

                        using (SqlCommand cmd = new SqlCommand("PlnPagoIncentivoCortesNLIns", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@corte", corte);
                            cmd.Parameters.AddWithValue("@seccion", seccion);
                            cmd.Parameters.AddWithValue("@comentario", comentario);
                            cmd.Parameters.AddWithValue("@usuario", user);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (SystemException)
                {
                    return false;
                }
            } 

            return true; 
        }

        public DataTable PlnObtenerOQLMod(int periodo, DateTime fechaini, DateTime fechafin)
        {
            DataTable dtOQLMod = new DataTable();
            dtOQLMod.Columns.Add("periodo", typeof(int));
            dtOQLMod.Columns.Add("modulo", typeof(string));
            dtOQLMod.Columns.Add("oql", typeof(decimal));
            dtOQLMod.Columns.Add("monto", typeof(decimal));
            //dtOQLMod.Columns.Add("eficiencia", typeof(decimal));
            DataTable eficienciaMod = Session["eficienciaMod"] as DataTable;
            DataTable lo = Session["tablaOQL"] as DataTable;
            DataRow[] modulosprod = null;
            DataRow[] datooqlmod = null;
            DataRow[] datooqlantmod = null;
            DataRow[] rangooql = null; //no
            string modulo = "";
            decimal monto = default(decimal);
            decimal oql = default(decimal);
            decimal eficiencia = default(decimal);
            try
            {
                modulosprod = (from c in eficienciaMod.AsEnumerable()
                               group c by c.Field<string>("modulo") into c
                               select c.FirstOrDefault()).ToArray();


                DataTable oqlmod = PlnObtenerOQLModulosProd(fechaini, fechafin, 1);
                DataTable oqlantmod = PlnObtenerOQLModulosProd(fechaini.AddDays(-7.0), fechafin.AddDays(-7.0), 1);

                int exists = 0;
                DataRow[] array = modulosprod;


                foreach (DataRow item2 in array)
                {
                    exists = 0;
                    monto = default(decimal);
                    modulo = item2["modulo"].ToString().Trim();

                    datooqlmod = (from c in oqlmod.AsEnumerable()
                                  where c.Field<string>("modulo").Trim() == modulo.Trim()
                                  select c).ToArray();


                    datooqlantmod = (from c in oqlantmod.AsEnumerable()
                                     where c.Field<string>("modulo").Trim() == modulo.Trim()
                                     select c).ToArray();
                    if (datooqlmod.Length != 0)
                    {
                        exists = 1;
                        oql = decimal.Parse(datooqlmod[0][5].ToString());
                    }
                    else if (datooqlantmod.Length != 0)
                    {
                        exists = 1;
                        oql = decimal.Parse(datooqlantmod[0][5].ToString());
                    }
                    if (exists != 1)
                    {
                        continue;
                    }
                    //cambiar para que sea igual a los operarios
                    //Tabla de Eficiencia: lo
                    eficiencia = decimal.Parse(item2["eficienciaSem"].ToString());
                    rangooql = (from c in lo.AsEnumerable()
                                    // where eficiencia >= Convert.ToDecimal(c["eficienciadesde"]) &&
                                    //eficiencia <= Convert.ToDecimal(c["eficienciahasta"]) &&
                                    //oql >= Convert.ToDecimal(c["oqldesde"]) &&
                                    // oql <= Convert.ToDecimal(c["oqlhasta"])

                                where Convert.ToDecimal(c["eficienciadesde"]) <= eficiencia &&
                                Convert.ToDecimal(c["eficienciahasta"]) > eficiencia &&
                                Convert.ToDecimal(c["oqldesde"]) <= oql &&
                                Convert.ToDecimal(c["oqlhasta"]) > oql
                                select c).ToArray();
                    // OQL. Aqui se llena la tabla que se usa para pago en los calculos
                    if (rangooql != null && rangooql.Length != 0)
                    {
                        monto = rangooql.Select((DataRow c) => c.Field<decimal>("monto")).First();
                    }
                    dtOQLMod.Rows.Add(periodo, modulo, oql, monto);
                }
                return dtOQLMod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal PlnAsignarIngresoOpCritica(DataRow[] datoempR,  decimal dzEficiencia, string operacion)
        {
            DataTable OpCrit = Session["OpCrit"] as DataTable;
            DataTable tablaOpC = Session["tablaOpC"] as DataTable;
            DataTable areaMod = Session["ModuloArea"] as DataTable;
            DataRow[] aplicaoperacion = null;
            DataRow[] datooRango = null;
            decimal monto = default(decimal);
            try
            {
                aplicaoperacion = (from c in OpCrit.AsEnumerable()
                                   where c.Field<string>("operacion").Trim() == operacion.Trim() 
                                   select c).ToArray();
                if (aplicaoperacion.Length != 0)
                {
                    
                    DataRow[] layoutCheck = null;
                    int invalido = 0;


                    datooRango = (from c in tablaOpC.AsEnumerable()
                                  where Convert.ToDecimal(c["eficienciadesde"]) <= dzEficiencia && dzEficiencia <= Convert.ToDecimal(c["eficienciahasta"])
                                  select c).ToArray();
                    if (datooRango.Length != 0)
                    {
                        layoutCheck = datoempR.Where((DataRow c) => c.Field<bool>("excedeLayout")).ToArray();
                        if (layoutCheck != null && layoutCheck.Length != 0)
                        {
                            invalido = 1;
                        }
                        if (invalido == 0)
                        {
                            return datooRango.Select((DataRow c) => c.Field<decimal>("montoOpCritica")).FirstOrDefault();
                        }
                    }
                }
                return monto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public decimal PlnAsignarDocenasAdicionales(DataRow[] datoempR)
        {
            DataTable tablaAdc = Session["tablaAdicional"] as DataTable;
            DataRow[] datooRango = null;
            decimal monto = default(decimal);
            try
            {
                decimal docenasadic = (from c in datoempR
                                       group c by c.Field<DateTime>("fecha") into c
                                       select c.First() into r
                                       select r.Field<decimal>("docenasprodprot")).FirstOrDefault();
                datooRango = (from c in tablaAdc.AsEnumerable()
                              where Convert.ToDecimal(c["dzdesde"]) <= docenasadic && Convert.ToDecimal(c["dzhasta"]) >= docenasadic
                              select c).ToArray();
                if (datooRango.Length != 0)
                {
                    return datooRango.Select((DataRow c) => c.Field<decimal>("monto")).FirstOrDefault();
                }
                return monto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PlnObtenerAdicionalesMod(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion)
        {
            DataTable dtAdicMod = new DataTable();
            dtAdicMod.Columns.Add("modulo", typeof(string));
            dtAdicMod.Columns.Add("DocenasAdicionales", typeof(decimal));
            dtAdicMod.Columns.Add("monto", typeof(decimal));
            DataTable eficienciaMod = Session["eficienciaMod"] as DataTable;
            DataTable lo = Session["tablaAdicional"] as DataTable;
            DataRow[] modulosprod = null;
            string modulo = "";
            decimal monto = default(decimal);
            decimal dzprodAdic = default(decimal);
            try
            {
                modulosprod = (from c in eficienciaMod.AsEnumerable()
                               group c by c.Field<string>("modulo") into c
                               select c.FirstOrDefault()).ToArray();
                DataRow[] array = modulosprod;
                foreach (DataRow item2 in array)
                {
                    dzprodAdic = decimal.Parse(item2["dzprodAdic"].ToString());
                    modulo = item2["modulo"].ToString().Trim();
                    if (dzprodAdic > 0m)
                    {
                        monto = (from c in lo.AsEnumerable()
                                 where Convert.ToDecimal(c["dzdesde"]) <= dzprodAdic && Convert.ToDecimal(c["dzhasta"]) >= dzprodAdic
                                 select c.Field<decimal>("monto")).First();
                    }
                    dtAdicMod.Rows.Add(modulo, dzprodAdic, monto);
                }
                return dtAdicMod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataRow[] PlnObtenerTablaIncentivo(DataRow[] source, int campos, int vista, int idconstruccion, string modulo, decimal docenasperiodo)
        {
            int maxconstdia = 0;
            int maxconstModulo = 0;
            DataTable li = new DataTable();
            li = Session["tablaIncentivos"] as DataTable;
            DataTable areaMod = Session["ModuloArea"] as DataTable;
            DataRow[] areaModList = null;
            DataRow[] areaModTabla = null;
            DataRow[] tablaincxconst = null;
            string constr = "";
            string dz = "";
            DataTable config = Session["tablaConfig"] as DataTable;
            decimal metamaxSem = default(decimal);
            decimal eficienciaSem = default(decimal);
            try
            {
                decimal dias = 0;
                dias = ((config.Rows.Count <= 0) ? 5 : Convert.ToDecimal(config.Rows[0]["diasPagar"]));
                if (campos == 1)
                {
                    constr = "idconstruccion";
                    dz = "docenasprodprot";
                }
                else
                {
                    constr = "idconstrincent";
                    if (vista != 2)
                    {
                        dz = "Dz_a_Pagar";
                    }
                    else
                    {
                        dz = "Dz_Producidas";
                    }
                }
                if (idconstruccion == 0)
                {
                    List<Neg_Incentivos> result = (from c in source.Where((DataRow c) => c.Field<int>(constr) > 0).AsEnumerable()
                                                   group c by new
                                                   {
                                                       c1 = c[constr]
                                                   } into cl
                                                   select new Neg_Incentivos
                                                   {
                                                       idconstruccion = cl.First().Field<int>(constr),
                                                       dzpagar = cl.Sum((DataRow c) => c.Field<decimal>(dz))
                                                   }).ToList();
                    if (result.Count > 0)
                    {
                        maxconstdia = result.OrderByDescending((Neg_Incentivos c) => c.dzpagar).Take(1).First()
                            .idconstruccion;
                    }
                }
                else
                {
                    maxconstdia = idconstruccion;
                }
                areaModTabla = (from c in areaMod.AsEnumerable()
                                where c.Field<string>("modulo").Trim() == modulo
                                select c).ToArray();
                areaModList = areaModTabla.Where((DataRow c) => c.Field<int>("area") == 2).ToArray();
                if (areaModTabla != null && areaModTabla.Length != 0)
                {
                    maxconstModulo = Convert.ToInt32(areaModTabla.FirstOrDefault()["idConstIncent"]);
                    if (maxconstModulo > 0)
                    {
                        maxconstdia = maxconstModulo;
                    }
                }

                // Aqui se calcula eficiencia
                tablaincxconst = (from c in li.AsEnumerable()
                                  where c.Field<int>("idConstIncent") == maxconstdia && c.Field<int>("idArea") == 1
                                  select c).ToArray();
                if (tablaincxconst != null && tablaincxconst.Length != 0)
                {
                      /*TODO:VHPO
                    
                      // metamaxSem = tablaincxconst.Max((DataRow c) => c.Field<decimal>("dzdesde"));
                      //eficienciaSem = Math.Round(docenasperiodo / (metamaxSem * (decimal)dias) * 100m, 2);*/
                        var fila = tablaincxconst.FirstOrDefault(c => c.Field<decimal>("eficienciadesde") == 100);
                        decimal metaxSem = fila != null ? fila.Field<decimal>("DzDesde") : 1000m;       
                        
                         eficienciaSem = Math.Round ((docenasperiodo / metaxSem) * 100);

                       /*  //hacer condicion de pagar o no bonoasistencia si asistencia >= (48m - 0.5) && eficienciasemana > 90 buscar la tabla y mandar el bono de asistencia
                        o retornar buscar la fila que coincida con la eficiencia
                     */

                                      
                    if (eficienciaSem < 90m && areaModList != null && areaModList.Length != 0)
                    {
                        tablaincxconst = (from c in li.AsEnumerable()
                                          where c.Field<int>("idArea") == 2
                                          select c).ToArray();
                    }
                }
                return tablaincxconst; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DateTime> PlnObtenerRangoFechas(DateTime inicio, DateTime fin, DataRow[] fechas)
        {
            List<DateTime> newDate = new List<DateTime>();
            newDate = (from offset in Enumerable.Range(0, 1 + fin.Subtract(inicio).Days)
                       select inicio.AddDays(offset)).ToList();
            fechas = fechas.Where((DataRow c) => c.Field<DateTime>("fecha_producido") < inicio).ToArray();
            DataRow[] array = fechas;
            foreach (DataRow item in array)
            {
                newDate.Add(Convert.ToDateTime(item["fecha_producido"]));
            }
            return newDate;
        }

        public DataSet plnObtenerProdAPagarxMod(DateTime fechaini, DateTime fechafin, DateTime corteaprobacion, int vista, int periodo)
        {
            DataTable dtInD = new DataTable();
            dtInD.Columns.Add("modulo", typeof(string));
            dtInD.Columns.Add("fecha_producido", typeof(DateTime));
            dtInD.Columns.Add("idconstruccion", typeof(int));
            dtInD.Columns.Add("construccion", typeof(string));
            dtInD.Columns.Add("docenasprod", typeof(decimal));
            dtInD.Columns.Add("docenaspagar", typeof(decimal));
            dtInD.Columns.Add("docenasproteccion", typeof(decimal));
            dtInD.Columns.Add("docenasprodprot", typeof(decimal));
            dtInD.Columns.Add("docenaspagarprot", typeof(decimal));
            dtInD.Columns.Add("montodocenas", typeof(decimal));
            DataTable dtprod = new DataTable();
            dtprod.Columns.Add("fecha_producido", typeof(DateTime));
            dtprod.Columns.Add("fecha_aprobado", typeof(DateTime));
            dtprod.Columns.Add("modulo", typeof(string));
            dtprod.Columns.Add("corte", typeof(string));
            dtprod.Columns.Add("seccion", typeof(int));
            dtprod.Columns.Add("nsecciones", typeof(int));
            dtprod.Columns.Add("estilo", typeof(int));
            dtprod.Columns.Add("color", typeof(string));
            dtprod.Columns.Add("subestatus", typeof(string));
            dtprod.Columns.Add("idconstruccion", typeof(int));
            dtprod.Columns.Add("construccion", typeof(string));
            dtprod.Columns.Add("oql", typeof(decimal));
            dtprod.Columns.Add("docenasprod", typeof(decimal));
            dtprod.Columns.Add("docenaspagar", typeof(decimal));
            dtprod.Columns.Add("docenasproteccion", typeof(decimal));
            dtprod.Columns.Add("docenasprotxsec", typeof(decimal));
            dtprod.Columns.Add("docenasprodprot", typeof(decimal));
            dtprod.Columns.Add("docenaspagarprot", typeof(decimal));
            dtprod.Columns.Add("costo", typeof(decimal));
            dtprod.Columns.Add("montodocenas", typeof(decimal));
            dtprod.Columns.Add("IncidenciaInternaC", typeof(decimal));
            dtprod.Columns.Add("IncidenciaExternaC", typeof(decimal));
            DataTable prodcmod = new DataTable();
            DataTable PTmod = new DataTable();
            DataRow[] fechas = null;
            DataRow[] modulos = null;
            DataRow[] prodxdia = null;
            DataRow[] ptxdia = null;
            DataRow[] construcciones = null;
            DataRow[] tablainc = null;
            DataRow[] PTxconst = null;
            DataRow[] tablaincxconst = null;
            DataRow[] proteccionxmod = null;
            DataRow[] protDistDia = null;
            DataRow[] protDistDiaHist = null;
            string modulo = "";
            decimal pagoconst = default(decimal);
            decimal pagocutsec = default(decimal);
            decimal oql = default(decimal);
            decimal costodc = default(decimal);
            int idoql = 1;
            decimal totalptxconst = default(decimal);
            decimal totalprodxdia = default(decimal);
            decimal totalprodxSem = default(decimal);
            decimal metadia100 = default(decimal);
            decimal proteccionpagar = default(decimal);
            decimal protecciondocenas = default(decimal);
            decimal balanceprod = default(decimal);
            decimal prodprot = default(decimal);
            decimal pagarprot = default(decimal);
            decimal proteccionxsec = default(decimal);
            int secciones = 0;
            decimal dzEficiencia = default(decimal);
            decimal dzSem = default(decimal);
            int idconstruccion = 0;
            string construccion = "";
            DataSet ds = new DataSet();

            try
            {
                DataTable eficienciaMod = Session["eficienciaMod"] as DataTable;
                DataTable eficienciaModHist = Session["eficienciaModHist"] as DataTable;
                DataTable config = Session["tablaConfig"] as DataTable;
                if (vista != 2)
                {
                    prodcmod = PlnObtenerProduccionAprobadaByPeriodo(fechaini, fechafin, corteaprobacion, vista, periodo);
                    PTmod = ((vista != 1) ? PlnObtenerProdPendienteByPeriodo(fechaini, fechafin, corteaprobacion, periodo) : PlnObtenerIngresosPTByPeriodo(fechaini, fechafin, corteaprobacion, periodo));//Es lo mismo
                }//wbravo1
                else if (vista == 2)
                {
                    prodcmod = PlnObtenerProduccionByPeriodo(fechaini, fechafin);//produccion total
                    PTmod = PlnObtenerProduccionDetalleByPeriodo(fechaini, fechafin);//detalle de la produccion total
                }

                DateTime fechainiprot = fechaini;
                DataRow[] fechaprot = (from c in PTmod.AsEnumerable()
                                       group c by c.Field<DateTime>("fecha_producido") into g
                                       select g.First()).ToArray();
                if (fechaprot.Length != 0)
                {
                    fechainiprot = Convert.ToDateTime(fechaprot.OrderBy((DataRow f) => f.Field<DateTime>("fecha_producido")).Take(1).First()["fecha_producido"]);
                }

                DataTable prot = PlnObtenerProteccionModulo(fechainiprot, fechafin);//proteccion en docenas

                if (vista != 3 || (vista == 3 && PTmod.Rows.Count > 0))
                {
                    modulos = (from c in PTmod.AsEnumerable()
                               group c by new
                               {
                                   c2 = c["modulo"]
                               } into grp
                               select grp.First()).ToArray();
                    DataRow[] array = modulos;
                    foreach (DataRow item in array)
                    {
                        modulo = item["modulo"].ToString().Trim();
                        if (modulo == "04")
                        {
                            modulo = item["modulo"].ToString().Trim();
                        }
                        List<DateTime> newDate = new List<DateTime>();
                        fechas = (from d in PTmod.AsEnumerable()
                                  where d.Field<string>("modulo").Trim() == modulo
                                  select d into c
                                  group c by new
                                  {
                                      c2 = c["fecha_producido"]
                                  } into grp
                                  select grp.First()).ToArray();
                        newDate = PlnObtenerRangoFechas(fechaini, fechafin, fechas);
                        foreach (DateTime fc in newDate)
                        {
                            if (!Convert.ToBoolean(config.Rows[0]["incluyeFinSem"]) && vista != 2 && (fc.DayOfWeek < DayOfWeek.Monday || fc.DayOfWeek > DayOfWeek.Friday))
                            {
                                continue;
                            }
                            pagoconst = default(decimal);
                            costodc = default(decimal);
                            metadia100 = default(decimal);
                            dzSem = default(decimal);
                            construccion = "";
                            idconstruccion = 0;
                            proteccionpagar = default(decimal);
                            proteccionxsec = default(decimal);
                            protecciondocenas = default(decimal);
                            prodprot = default(decimal);
                            pagarprot = default(decimal);
                            balanceprod = default(decimal);
                            secciones = 0;
                            dzEficiencia = default(decimal);
                            prodxdia = (from c in prodcmod.AsEnumerable()
                                        where c.Field<DateTime>("fecha_producido") == fc && c.Field<string>("modulo").Trim() == modulo
                                        select c).ToArray();
                            totalprodxdia = Math.Round(prodxdia.Sum((DataRow r) => r.Field<decimal>("Dz_Producidas")), 2);
                            ptxdia = (from c in PTmod.AsEnumerable()
                                      where c.Field<DateTime>("fecha_producido") == fc && c.Field<string>("modulo").Trim() == modulo
                                      select c).ToArray();
                            secciones = ((prodxdia.Length == 0) ? 1 : prodxdia.Sum((DataRow r) => r.Field<int>("secciones")));
                            if (vista == 2)
                            {
                                if (prodxdia.Length != 0)
                                {
                                    tablainc = PlnObtenerTablaIncentivo(prodxdia, 2, 2, 0, modulo, totalprodxdia);
                                    //metadia100 = tablainc.Max((DataRow c) => c.Field<decimal>("dzdesde"));

                                    metadia100 = tablainc
                                                      .Where(c => c.Field<decimal>("EficienciaDesde") == 100)
                                                      .Select(c => c.Field<decimal>("DzDesde"))
                                                      .FirstOrDefault();

                                }
                                proteccionxmod = (from c in prot.AsEnumerable()
                                                  where c.Field<DateTime>("fecha") == fc && c.Field<string>("modulo").Trim() == modulo
                                                  select c).ToArray();
                                if (proteccionxmod.Length != 0)
                                {
                                    balanceprod = (from c in proteccionxmod.AsEnumerable()
                                                   where c.Field<string>("problema").Trim().ToLower()
                                                       .IndexOf("bal") > -1
                                                   select c).Sum((DataRow r) => r.Field<decimal>("dz"));
                                    protecciondocenas = (from c in proteccionxmod.AsEnumerable()
                                                         where c.Field<string>("problema").Trim().ToLower()
                                                             .IndexOf("bal") < 0
                                                         select c).Sum((DataRow r) => r.Field<decimal>("dz"));
                                    proteccionpagar = balanceprod;
                                    prodprot = totalprodxdia + proteccionpagar;
                                    if (prodprot < metadia100)
                                    {
                                        if (prodprot + protecciondocenas > metadia100)
                                        {
                                            proteccionpagar = proteccionpagar + metadia100 - prodprot;
                                        }
                                        else
                                        {
                                            proteccionpagar += protecciondocenas;
                                        }
                                    }
                                    else if (balanceprod == 0m)
                                    {
                                        proteccionpagar = default(decimal);
                                        proteccionxsec = default(decimal);
                                    }
                                }
                                proteccionxsec = Math.Round(proteccionpagar / (decimal)secciones, 2);//Se desgrana en secciones la proteccion
                                prodprot = Math.Round(totalprodxdia + proteccionpagar, 2);
                            }
                            else
                            {
                                protDistDia = (from c in eficienciaMod.AsEnumerable()
                                               where c.Field<string>("modulo").Trim() == modulo.Trim() && c.Field<DateTime>("fecha_producido") == fc
                                               select c).ToArray();
                                if (protDistDia.Length != 0)
                                {
                                    dzSem = protDistDia.Select((DataRow c) => c.Field<decimal>("dzSem")).FirstOrDefault();
                                    dzEficiencia = protDistDia.Select((DataRow c) => c.Field<decimal>("eficienciaSem")).FirstOrDefault();
                                    proteccionpagar = protDistDia.Select((DataRow c) => c.Field<decimal>("dzprotDia")).FirstOrDefault();
                                }
                                else
                                {
                                    protDistDiaHist = (from c in eficienciaModHist.AsEnumerable()
                                                       where c.Field<string>("modulo").Trim() == modulo.Trim() && c.Field<DateTime>("fecha_producido") == fc
                                                       select c).ToArray();
                                    if (protDistDiaHist.Length != 0)
                                    {
                                        dzSem = protDistDiaHist.Select((DataRow c) => c.Field<decimal>("dzSem")).FirstOrDefault();
                                        dzEficiencia = protDistDiaHist.Select((DataRow c) => c.Field<decimal>("eficienciaSem")).FirstOrDefault();
                                        proteccionpagar = protDistDiaHist.Select((DataRow c) => c.Field<decimal>("dzprotDia")).FirstOrDefault();
                                    }
                                }
                                proteccionxsec = Math.Round(proteccionpagar / (decimal)secciones, 2);
                                prodprot = Math.Round(totalprodxdia + proteccionpagar, 2);
                            }
                            if (ptxdia.Length != 0)
                            {
                                construcciones = (from c in ptxdia
                                                  group c by new
                                                  {
                                                      c1 = c["idconstrincent"]
                                                  } into grp
                                                  select grp.First()).ToArray();
                                DataRow[] array2 = construcciones;

                                foreach (DataRow item2 in array2)
                                {
                                    pagoconst = default(decimal);
                                    PTxconst = (from c in ptxdia.AsEnumerable()
                                                where c.Field<int>("idconstrincent") == Convert.ToInt32(item2["idconstrincent"])
                                                select c).ToArray();
                                    totalptxconst = PTxconst.Sum((DataRow r) => r.Field<decimal>("Dz_a_Pagar"));
                                    pagarprot = Math.Round(totalptxconst + proteccionpagar, 2);
                                    tablaincxconst = PlnObtenerTablaIncentivo(PTxconst, 1, vista, Convert.ToInt32(item2["idconstrincent"]), modulo, dzSem);
                                    tablainc = tablaincxconst.Where((DataRow c) => c.Field<decimal>("eficienciaDesde") <= dzEficiencia && c.Field<decimal>("eficienciaHasta") > dzEficiencia).ToArray();
                                    DataRow[] array3 = PTxconst;
                                    foreach (DataRow dr in array3)
                                    {
                                        decimal dzpagar = default(decimal);
                                        string corte = "";
                                        string subestatus = "";
                                        string color = "";
                                        int seccion = 0;
                                        int estilo = 0;
                                        oql = ((vista != 3) ? Convert.ToDecimal(dr["oql"]) : default(decimal));
                                        costodc = ((tablainc.Length == 0) ? default(decimal) : Convert.ToDecimal(tablainc.Where((DataRow c) => c.Field<int>("idrangooql") == idoql).FirstOrDefault()["costodz"]));
                                        corte = dr["corte"].ToString();
                                        seccion = Convert.ToInt32(dr["seccion"]);
                                        estilo = Convert.ToInt32(dr["estilo"]);
                                        color = dr["color"].ToString();
                                        dzpagar = Math.Round(Convert.ToDecimal(dr["Dz_a_Pagar"]), 2);
                                        DateTime fechapt = Convert.ToDateTime(dr["fecha_aprobado"]);
                                        if (vista == 3)
                                        {
                                            subestatus = dr["subestatus"].ToString();
                                        }
                                        pagocutsec = Math.Round((dzpagar + proteccionxsec) * costodc, 2);
                                        pagoconst += pagocutsec;
                                        dtprod.Rows.Add(fc, fechapt, modulo, corte, seccion, secciones, estilo, color, subestatus, Convert.ToInt32(item2["idconstrincent"]), item2["construccionincentivo"].ToString(), oql, totalprodxdia, dzpagar, proteccionpagar, proteccionxsec, prodprot, dzpagar + proteccionxsec, costodc, pagocutsec, 0, 0);//Produccion x modulo detallando secciones
                                    }
                                    if (tablainc.Length != 0)
                                    {
                                        dtInD.Rows.Add(modulo, fc, Convert.ToInt32(item2["idconstrincent"]), item2["construccionincentivo"].ToString(), totalprodxdia, totalptxconst, proteccionpagar, prodprot, pagarprot, pagoconst);//Produccion x modulo
                                    }
                                    else
                                    {
                                        dtInD.Rows.Add(modulo, fc, Convert.ToInt32(item2["idconstrincent"]), item2["construccionincentivo"].ToString(), totalprodxdia, totalptxconst, proteccionpagar, prodprot, pagarprot, 0);
                                    }
                                }
                            }
                            else
                            {
                                if (vista == 3)
                                {
                                    continue;
                                }
                                if (tablainc != null && tablainc.Length != 0)
                                {
                                    idconstruccion = tablainc.Select((DataRow c) => c.Field<int>("idconstincent")).FirstOrDefault();
                                    construccion = tablainc.Select((DataRow c) => c.Field<string>("construccion")).FirstOrDefault();
                                    tablaincxconst = tablainc.Where((DataRow c) => c.Field<decimal>("eficienciaDesde") <= dzEficiencia && c.Field<decimal>("eficienciaHasta") > dzEficiencia).ToArray();
                                    costodc = tablaincxconst.Select((DataRow c) => c.Field<decimal>("costoDz")).FirstOrDefault();
                                }
                                if (prodxdia.Length != 0)
                                {
                                    dtprod.Rows.Add(fc, new DateTime(1900, 1, 1), modulo, "", 0, secciones, 0, "", "", idconstruccion, construccion, 0, totalprodxdia, 0, proteccionpagar, 0, prodprot, 0, costodc, 0, 0, 0);
                                }
                                else if (proteccionpagar > 0m)
                                {
                                    pagoconst = Math.Round(proteccionpagar * costodc, 2);
                                    dtprod.Rows.Add(fc, new DateTime(1900, 1, 1), modulo, "", 0, secciones, 0, "", "", idconstruccion, construccion, 0, proteccionpagar, proteccionpagar, proteccionpagar, proteccionpagar, proteccionpagar, proteccionpagar, costodc, pagoconst, 0, 0);
                                }
                                dtInD.Rows.Add(modulo, fc, 0, "", 0, 0, 0, 0, 0, 0);
                            }
                        }
                    }
                }
                ds.Tables.Add(dtInD);
                ds.Tables.Add(dtprod);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet ProcesarIncentivosxDia(DataTable pagomod, int periodo, int semana, int vista)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Neg_Marca Neg_Marca = new Neg_Marca();
            Neg_Amonestaciones Neg_Amonestaciones = new Neg_Amonestaciones();
            Neg_Empleados Neg_Empleados = new Neg_Empleados();
            DataTable dt = new DataTable();
            DataRow[] fechas = (from c in pagomod.AsEnumerable()
                                group c by c.Field<DateTime>("fecha_producido") into g
                                select g.First()).ToArray();
            DateTime fechaini = Convert.ToDateTime(fechas.OrderBy((DataRow f) => f.Field<DateTime>("fecha_producido")).Take(1).First()["fecha_producido"]);
            DateTime fechafin = Convert.ToDateTime(fechas.OrderByDescending((DataRow f) => f.Field<DateTime>("fecha_producido")).Take(1).First()["fecha_producido"]);

            DataTable todosempxdia = Neg_Empleados.pln_empleadosHistoricoSelectxDia(fechaini, fechafin);
            DataTable pdz = PlnProteccionDzxFechaSel(fechaini, fechafin);
            DataTable lt = Session["HORASSEMANA"] as DataTable;
            decimal dzprod = default(decimal);
            decimal dzpt = default(decimal);
            decimal dzproteccion = default(decimal);
            decimal dzprodprot = default(decimal);
            decimal costo = default(decimal);
            decimal dzpgprot = default(decimal);
            decimal dzproteccionconst = default(decimal);
            int idconstruccion = 0;
            int estilo = 0;
            string construccion = "";
            decimal montodz = default(decimal);
            DataTable dthoras = new DataTable();
            DataRow[] layoutConstr = null;
            DataRow[] pdzemp = null;
            string user = Convert.ToString(Page.Session["usuario"]);
            DataRow[] empleados = null;
            DataTable dtInD = new DataTable();
            dtInD.Columns.Add("modulo", typeof(string));
            dtInD.Columns.Add("fecha", typeof(DateTime));
            dtInD.Columns.Add("asistencia", typeof(string));
            dtInD.Columns.Add("horas", typeof(decimal));
            dtInD.Columns.Add("codigo_empleado", typeof(int));
            dtInD.Columns.Add("nombrecompleto", typeof(string));
            dtInD.Columns.Add("operacion", typeof(string));
            dtInD.Columns.Add("idconstruccion", typeof(int));
            dtInD.Columns.Add("construccion", typeof(string));
            dtInD.Columns.Add("dzprod", typeof(decimal));
            dtInD.Columns.Add("dzpt", typeof(decimal));
            dtInD.Columns.Add("docenasproteccion", typeof(decimal));
            dtInD.Columns.Add("docenasprotxsec", typeof(decimal));
            dtInD.Columns.Add("docenasprodprot", typeof(decimal));
            dtInD.Columns.Add("docenaspagarprot", typeof(decimal));
            dtInD.Columns.Add("costo", typeof(decimal));
            dtInD.Columns.Add("montodz", typeof(decimal));
            dtInD.Columns.Add("total", typeof(decimal));
            dtInD.Columns.Add("opera_simultaneo", typeof(bool));
            dtInD.Columns.Add("excedeLayout", typeof(bool));
            DataTable dtprod = new DataTable();
            dtprod.Columns.Add("id", typeof(int));
            dtprod.Columns.Add("periodo", typeof(int));
            dtprod.Columns.Add("semana", typeof(int));
            dtprod.Columns.Add("modulo", typeof(string));
            dtprod.Columns.Add("fecha_producido", typeof(DateTime));

            if (vista != 3)
            {
                dtprod.Columns.Add("asistencia", typeof(string));
                dtprod.Columns.Add("horas", typeof(decimal));
                dtprod.Columns.Add("codigo_empleado", typeof(int));
                dtprod.Columns.Add("nombrecompleto", typeof(string));
                dtprod.Columns.Add("operacion", typeof(string));
                dtprod.Columns.Add("corte", typeof(string));
                dtprod.Columns.Add("seccion", typeof(int));
                dtprod.Columns.Add("estilo", typeof(int));
                dtprod.Columns.Add("color", typeof(string));
                dtprod.Columns.Add("construccion", typeof(string));
                dtprod.Columns.Add("fecha_aprobado", typeof(DateTime));
                dtprod.Columns.Add("oql", typeof(decimal));
                dtprod.Columns.Add("dzdia", typeof(decimal));
                dtprod.Columns.Add("dzpagar", typeof(decimal));
                dtprod.Columns.Add("dzproteccion", typeof(decimal));
                dtprod.Columns.Add("dzprotxsec", typeof(decimal));
                dtprod.Columns.Add("dzprodprot", typeof(decimal));
                dtprod.Columns.Add("dzpgprot", typeof(decimal));
                dtprod.Columns.Add("costo", typeof(decimal));
                dtprod.Columns.Add("montopagar", typeof(decimal));
                dtprod.Columns.Add("fechagraba", typeof(DateTime));
                dtprod.Columns.Add("usuariograba", typeof(string));
            }
            else
            {
                dtprod.Columns.Add("horas", typeof(decimal));
                dtprod.Columns.Add("codigo_empleado", typeof(int));
                dtprod.Columns.Add("corte", typeof(string));
                dtprod.Columns.Add("seccion", typeof(int));
                dtprod.Columns.Add("estilo", typeof(int));
                dtprod.Columns.Add("color", typeof(string));
                dtprod.Columns.Add("construccion", typeof(string));
                dtprod.Columns.Add("subestatus", typeof(string));
                dtprod.Columns.Add("dzprod", typeof(decimal));
                dtprod.Columns.Add("dzpagar", typeof(decimal));
                dtprod.Columns.Add("oql", typeof(decimal));
                dtprod.Columns.Add("costo", typeof(decimal));
                dtprod.Columns.Add("montodocenas", typeof(decimal));
                dtprod.Columns.Add("fechagraba", typeof(DateTime));
                dtprod.Columns.Add("usuariograba", typeof(string));
            }

            DataTable LayoutxEstilo = Session["LayoutxEstilo"] as DataTable;
            DataTable layoutdiamod = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                DataRow[] prod = (from c in pagomod.AsEnumerable()
                                  group c by new
                                  {
                                      c2 = c["modulo"],
                                      c3 = c["fecha_producido"],
                                      c1 = c["idconstruccion"]
                                  } into grp
                                  select grp.First()).ToArray();
                DataRow[] array = prod;
                foreach (DataRow mod in array)
                {
                    string modulo = mod["modulo"].ToString().Trim();
                    DateTime fecha = Convert.ToDateTime(mod["fecha_producido"]);
                    idconstruccion = int.Parse(mod["idconstruccion"].ToString());
                    estilo = int.Parse(mod["estilo"].ToString());
                    construccion = mod["construccion"].ToString();
                    dzprod = Math.Round(Convert.ToDecimal(mod["docenasprod"]), 2);
                    dzprodprot = Math.Round(Convert.ToDecimal(mod["docenasprodprot"]), 2);
                    dzproteccion = Math.Round(Convert.ToDecimal(mod["docenasproteccion"]), 2);
                    costo = Math.Round(Convert.ToDecimal(mod["costo"]), 6);
                    DataRow[] diaprodxconst = (from c in pagomod.AsEnumerable()
                                               where c.Field<string>("modulo") == modulo && c.Field<DateTime>("fecha_producido") == fecha && c.Field<int>("idconstruccion") == idconstruccion
                                               select c).ToArray();
                    dzpt = Math.Round(diaprodxconst.Sum((DataRow r) => r.Field<decimal>("docenaspagar")), 2);
                    dzpgprot = Math.Round(diaprodxconst.Sum((DataRow r) => r.Field<decimal>("docenaspagarprot")), 2);
                    montodz = Math.Round(diaprodxconst.Sum((DataRow r) => r.Field<decimal>("montodocenas")), 2);
                    dzproteccionconst = Math.Round(diaprodxconst.Sum((DataRow r) => r.Field<decimal>("docenasprotxsec")), 2);
                    DataRow[] empxdia = (from c in todosempxdia.AsEnumerable()
                                         where c.Field<DateTime>("diatrabajo") == fecha
                                         select c).ToArray();//Todos los empleados de un dia
                    DataRow[] empDiaMod = empxdia.Where((DataRow c) => c.Field<string>("nombre_depto").Trim().IndexOf(modulo) > -1).ToArray();//De un modulo
                    int[] codemp = empDiaMod.Select((DataRow u) => u.Field<int>("codigo_empleado")).ToArray();
                    empleados = (from c in lt.AsEnumerable()
                                 where codemp.Contains(c.Field<int>("codigo_empleado")) && c.Field<DateTime>("fecha") == fecha
                                 select c).ToArray();//De un empleado
                    DataTable conteopmoddia = ObtenerConteoLayoutxOperacionDia(empleados, empDiaMod, fecha, modulo);//ver el layout
                    DataRow[] array2 = empleados;
                    foreach (DataRow i in array2)
                    {
                        int codigo = Convert.ToInt32(i["codigo_empleado"]);
                        decimal horaspagar = default(decimal);
                        decimal total = default(decimal);
                        decimal horasturno = default(decimal);
                        decimal montodzproporcional = default(decimal);
                        decimal porcentaje_opera = 1m;
                        bool opera_simultaneo = false;
                        bool excedeLayout = false;
                        string operacion = "";
                        decimal conteo_operacion = default(decimal);
                        decimal layout = default(decimal);
                        horaspagar = Math.Round(Convert.ToDecimal(i["horas"]), 2);
                        horasturno = Math.Round(Convert.ToDecimal(i["horasturno"]), 2);
                        operacion = (from x in empDiaMod
                                     where x.Field<int>("codigo_empleado") == codigo
                                     select x into c
                                     select c.Field<string>("operacion")).FirstOrDefault().Trim();
                        dzprod = Math.Round(Convert.ToDecimal(mod["docenasprod"]), 2);//lo total
                        dzprodprot = Math.Round(Convert.ToDecimal(mod["docenasprodprot"]), 2);//lo producido mas lo protegido
                        dzpt = Math.Round(diaprodxconst.Sum((DataRow r) => r.Field<decimal>("docenaspagar")), 2);//lo aprobado
                        dzpgprot = Math.Round(diaprodxconst.Sum((DataRow r) => r.Field<decimal>("docenaspagarprot")), 2);//lo aprobado mas lo protegido
                        montodz = Math.Round(diaprodxconst.Sum((DataRow r) => r.Field<decimal>("montodocenas")), 2);//lo aprobado protegido en dinero

                        if (operacion.Trim().ToLower() == "if")
                        {
                            montodz *= 0.5m;
                        }

                        DataRow[] empSimultaneo = empxdia.Where((DataRow c) => c.Field<int>("codigo_empleado") == codigo && c.Field<bool>("opera_simultaneo")).ToArray();
                        porcentaje_opera = (from x in empDiaMod
                                            where x.Field<int>("codigo_empleado") == codigo
                                            select x into c
                                            select c.Field<decimal>("porcentaje_opera")).FirstOrDefault();
                        if (empSimultaneo.Length != 0)
                        {
                            dzprod = default(decimal);
                            dzprodprot = default(decimal);
                            string[] modopsim = null;
                            opera_simultaneo = true;
                            modopsim = empSimultaneo.Select((DataRow c) => c.Field<string>("nombre_depto").ToString().Substring(6)
                                .Trim()).ToArray();
                            decimal porctotal = Math.Round(empSimultaneo.Sum((DataRow c) => c.Field<decimal>("porcentaje_opera")), 2);
                            DataRow[] resultmod = (from c in pagomod.AsEnumerable()
                                                   where modopsim.Contains(c.Field<string>("modulo")) && c.Field<DateTime>("fecha_producido") == fecha
                                                   select c).ToArray();
                            decimal dzprodmod = Math.Round(resultmod.Sum((DataRow c) => c.Field<decimal>("docenasprod")), 2);
                            decimal dzprodprotmod = Math.Round(resultmod.Sum((DataRow c) => c.Field<decimal>("docenasprodprot")), 2);
                            dzprod = Math.Round(dzprodmod * porctotal, 2);
                            dzprodprot = Math.Round(dzprodprotmod * porctotal, 2);//aplica el porcentaje al simultaneo
                        }
                        dzpt = Math.Round(dzpt * porcentaje_opera, 2);
                        dzpgprot = Math.Round(dzpgprot * porcentaje_opera, 2);
                        montodz = Math.Round(montodz * porcentaje_opera, 2);
                        conteo_operacion = (from x in conteopmoddia.AsEnumerable()
                                            where x.Field<string>("operacion").Trim() == operacion.Trim()
                                            select x.Field<decimal>("conteo")).FirstOrDefault();
                        layoutConstr = (from x in LayoutxEstilo.AsEnumerable()
                                        where x.Field<int>("estilo") == estilo
                                        select x).ToArray();
                        layout = (from x in layoutConstr.AsEnumerable()
                                  where x.Field<string>("operacion").Trim() == operacion
                                  select x into c
                                  select c.Field<decimal>("layout")).FirstOrDefault();
                        if (conteo_operacion > layout && layout > 0m)
                        {
                            excedeLayout = true;
                            montodz = Math.Round(montodz * layout / conteo_operacion, 2);
                        }
                        else if (conteo_operacion == layout && layout > 0m && porcentaje_opera < 1m)
                        {
                            excedeLayout = true;
                        }

                        if (estilo == 6411)
                        {
                            excedeLayout = true;
                        }

                        if (horaspagar == 0m)
                        {
                            total = default(decimal);
                        }
                        else if (horaspagar >= horasturno || horasturno == 0m)
                        {
                            total = montodz;
                        }
                        else
                        {
                            montodzproporcional = Math.Round(horaspagar * montodz / horasturno, 2);
                            total = montodzproporcional;
                        }
                        if (vista == 1)
                        {
                            pdzemp = (from c in pdz.AsEnumerable()
                                      where c.Field<int>("codigo_empleado") == codigo && c.Field<DateTime>("fechaini") <= Convert.ToDateTime(i["fecha"]) && c.Field<DateTime>("fechafin") >= Convert.ToDateTime(i["fecha"])
                                      select c).ToArray();
                            if (pdzemp.Length != 0)
                            {
                                total = montodz;
                            }
                        }
                        if (horaspagar > 0m)
                        {
                            dtInD.Rows.Add(modulo, Convert.ToDateTime(i["fecha"]), "SI", horaspagar, codigo, Convert.ToString(i["nombrecompleto"]), operacion, idconstruccion, construccion, dzprod, dzpt, dzproteccion, dzproteccionconst, dzprodprot, dzpgprot, costo, total, total, opera_simultaneo, excedeLayout);//Registro a nivel de dia
                        }
                        else
                        {
                            dtInD.Rows.Add(modulo, Convert.ToDateTime(i["fecha"]), "NO", 0, codigo, Convert.ToString(i["nombrecompleto"]), operacion, 0, "", 0, 0, 0, 0, 0, 0, 0, total, total, opera_simultaneo, false);
                        }
                        DataRow[] array3 = diaprodxconst;
                        //Detalle de pago por seccion
                        foreach (DataRow cut in array3)
                        {
                            decimal montodocenas = default(decimal);
                            decimal dzpagar_cut = default(decimal);
                            decimal dzprotsec_cut = default(decimal);
                            decimal dzpgprot_cut = default(decimal);
                            dzpagar_cut = Math.Round(Convert.ToDecimal(cut["docenaspagar"]), 2);
                            dzpgprot_cut = Math.Round(Convert.ToDecimal(cut["docenaspagarprot"]), 2);
                            dzprotsec_cut = Math.Round(Convert.ToDecimal(cut["docenasprotxsec"]), 2);
                            montodocenas = Math.Round(Convert.ToDecimal(cut["montodocenas"]), 2);

                            if (operacion.Trim().ToLower() == "if")
                            {
                                montodocenas *= 0.5m;
                            }

                            if (conteo_operacion > layout && layout > 0m)
                            {
                                montodocenas = Math.Round(montodocenas * layout / conteo_operacion, 2);
                            }
                            dzpagar_cut = Math.Round(dzpagar_cut * porcentaje_opera, 2);
                            dzpgprot_cut = Math.Round(dzpgprot_cut * porcentaje_opera, 2);
                            dzprotsec_cut = Math.Round(dzprotsec_cut * porcentaje_opera, 2);
                            montodocenas = Math.Round(montodocenas * porcentaje_opera, 2);
                            total = ((horaspagar == 0m) ? default(decimal) : ((!(horaspagar >= horasturno) && !(horasturno == 0m)) ? Math.Round(horaspagar * montodocenas / horasturno, 2) : montodocenas));
                            if (horaspagar == 0m && horasturno > 0m)
                            {
                                if (vista != 3)
                                {
                                    dtprod.Rows.Add(0, periodo, semana, modulo, Convert.ToDateTime(i["fecha"]), "NO", 0, codigo, Convert.ToString(i["nombrecompleto"]), operacion, cut["corte"].ToString(), Convert.ToInt32(cut["seccion"]), Convert.ToInt32(cut["estilo"]), cut["color"].ToString(), construccion, Convert.ToDateTime(cut["fecha_aprobado"]), 0, 0, 0, 0, 0, 0, 0, 0, total, DateTime.Now, user);
                                }
                                else
                                {
                                    dtprod.Rows.Add(0, periodo, semana, modulo, Convert.ToDateTime(i["fecha"]), 0, codigo, cut["corte"].ToString(), Convert.ToInt32(cut["seccion"]), Convert.ToInt32(cut["estilo"]), cut["color"].ToString(), construccion, cut["subestatus"].ToString(), 0, 0, 0, 0, total, DateTime.Now, user);
                                }
                            }
                            else if (vista != 3)
                            {
                                dtprod.Rows.Add(0, periodo, semana, modulo, Convert.ToDateTime(i["fecha"]), "SI", horaspagar, codigo, Convert.ToString(i["nombrecompleto"]), operacion, cut["corte"].ToString(), Convert.ToInt32(cut["seccion"]), Convert.ToInt32(cut["estilo"]), cut["color"].ToString(), construccion, Convert.ToDateTime(cut["fecha_aprobado"]), Convert.ToDecimal(cut["oql"]), dzprod, dzpagar_cut, dzproteccion, dzprotsec_cut, dzprodprot, dzpgprot_cut, Convert.ToDecimal(cut["costo"]), total, DateTime.Now, user);
                            }
                            else
                            {
                                dtprod.Rows.Add(0, periodo, semana, modulo, Convert.ToDateTime(i["fecha"]), horaspagar, codigo, cut["corte"].ToString(), Convert.ToInt32(cut["seccion"]), Convert.ToInt32(cut["estilo"]), cut["color"].ToString(), construccion, cut["subestatus"].ToString(), dzprodprot, dzpgprot_cut, Convert.ToDecimal(cut["oql"]), Convert.ToDecimal(cut["costo"]), total, DateTime.Now, user);
                            }
                        }
                    }
                }
                ds.Tables.Add(dtInD);
                ds.Tables.Add(dtprod);
                return ds;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObtenerHorasTrabajadasInc(DateTime fechaini, DateTime fechafin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Neg_Marca Neg_Marca = new Neg_Marca();
            DataTable dt = new DataTable();
            List<Neg_Empleados> empleados = Neg_Marca.ObtenerHT(fechaini, fechafin, 3, 1, userDetail.getIDEmpresa());
            DataTable dtInD = new DataTable();
            dtInD.Columns.Add("fecha", typeof(DateTime));
            dtInD.Columns.Add("asistencia", typeof(string));
            dtInD.Columns.Add("horas", typeof(decimal));
            dtInD.Columns.Add("horasturno", typeof(decimal));
            dtInD.Columns.Add("codigo_empleado", typeof(int));
            dtInD.Columns.Add("nombrecompleto", typeof(string));
            dtInD.Columns.Add("operacion", typeof(string));
            string asistencia = "NO";
            try
            {
                foreach (Neg_Empleados i in empleados)
                {
                    if (i.codigo_empleado == 870994)
                    {
                    }
                    foreach (DataRow item in i.dtHorasT.Rows)
                    {
                        decimal horastrabajadas = default(decimal);
                        decimal horascgoce = default(decimal);
                        decimal horasturno = default(decimal);
                        decimal horaspagar = default(decimal);
                        decimal horasdia = default(decimal);
                        horasturno = Math.Round(Convert.ToDecimal(item["horasturno"]), 2);
                        horastrabajadas = Math.Round(Convert.ToDecimal(item["horast"]), 2);
                        if (horasturno == 0m)
                        {
                            TimeSpan horae = TimeSpan.Parse(item["horae"].ToString());
                            horasdia = Convert.ToDecimal(TimeSpan.Parse(item["horas"].ToString()).TotalHours - horae.TotalHours);
                            if (horasdia > 0m)
                            {
                                horastrabajadas = horasdia;
                            }
                        }
                        horascgoce = Math.Round(Convert.ToDecimal(item["horascg"]), 2);
                        horaspagar = horastrabajadas + horascgoce;
                        if (!(horascgoce == horasturno) || !(horascgoce > 0m) || !(horasturno > 0m))
                        {
                            if (horastrabajadas == 0m && horasturno > 0m)
                            {
                                asistencia = "NO";
                                horaspagar = default(decimal);
                            }
                            else if (horastrabajadas > 0m)
                            {
                                asistencia = "SI";
                            }
                            dtInD.Rows.Add(Convert.ToDateTime(item["fecha"]), asistencia, horaspagar, horasturno, i.codigo_empleado, i.nombrecompleto, i.operacion);
                        }
                    }
                }
                return dtInD;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private decimal PlnAsignarIngresoCumplimientoOQL(DataTable CumplimientoOQL, DataRow[] datoempR, DataRow[] opera)
        {
            try
            {
                decimal porcentajemonto = 1m;
                decimal montooql = default(decimal);
                string[] modulos = null;
                DataRow[] oqlmod = null;
                if (opera.Length != 0)
                {
                    porcentajemonto = 0.5m;
                    modulos = (from c in (from c in opera
                                          group c by new
                                          {
                                              c1 = c["modulo"]
                                          } into grp
                                          select grp.First()).Take(2)
                               select c.Field<string>("modulo")).ToArray();
                }
                else
                {
                    modulos = (from c in (from r in datoempR.Distinct()
                                          orderby r.Field<decimal>("docenasprodprot") descending
                                          select r).Take(1)
                               select c.Field<string>("modulo")).ToArray();
                }
                if (modulos.Length != 0)
                {

                    oqlmod = (from c in CumplimientoOQL.AsEnumerable()
                              where modulos.Contains(c.Field<string>("modulo"))
                              select c).ToArray();
                    if (oqlmod.Length != 0)
                    {
                        return oqlmod.Sum((DataRow c) => c.Field<decimal>("monto")) * porcentajemonto;
                    }
                }
                return montooql;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private decimal PlnAsignarIngresoBonoCalidad(string[] moduloCumplimientoS, DataRow[] datoempR, DataTable CalidadMod, DataRow[] tablainc)
        {
            try
            {
                string[] moduloBonoC = null;
                string[] moduloProtegerBonoC = null;
                decimal horasmodulo = default(decimal);
                decimal diasmodulo = default(decimal);
                string[] moduloparcial = null;
                DataRow[] incidenciascalidad = null;
                //DataRow[] ModProtegercalidad = null;
                decimal porcentajebonoOp = 1m;
                decimal porcentajebonoMod = 1m;
                decimal bonocalidad = default(decimal);
                incidenciascalidad = (from c in CalidadMod.AsEnumerable()
                                      where moduloCumplimientoS.Contains(c.Field<string>("modulo")) && c.Field<decimal>("TotalIncidencia") >= 1m//>
                                      select c).ToArray();
                string[] moduloIncidencia = incidenciascalidad.Select((DataRow c) => c.Field<string>("modulo")).Distinct().ToArray();
                moduloBonoC = moduloCumplimientoS.Except(moduloIncidencia).ToArray();
                //ModProtegercalidad = (from c in CalidadMod.AsEnumerable()
                //                      where moduloCumplimientoS.Contains(c.Field<string>("modulo")) && c.Field<decimal>("TotalIncidencia") == 1m
                //                      select c).ToArray();
                //string[] moduloFaltaPermitida = ModProtegercalidad.Select((DataRow c) => c.Field<string>("modulo")).Distinct().ToArray();
                if (moduloBonoC.Length != 0)
                {
                    moduloparcial = (from c in datoempR
                                     where moduloBonoC.Contains(c.Field<string>("modulo")) && c.Field<bool>("opera_simultaneo")
                                     select c.Field<string>("modulo")).Distinct().ToArray();
                    if (moduloparcial.Length == 1)
                    {
                        porcentajebonoOp = 0.5m;
                    }
                    //moduloProtegerBonoC = (from c in datoempR
                    //                       where moduloFaltaPermitida.Contains(c.Field<string>("modulo"))
                    //                       select c.Field<string>("modulo")).Distinct().ToArray();
                    //if (moduloProtegerBonoC.Length == 1)
                    //{
                    //    porcentajebonoMod = 0.5m;
                    //}
                    //else if (moduloProtegerBonoC.Length > 1)
                    //{
                    //    porcentajebonoMod = default(decimal);
                    //}
                    diasmodulo = (decimal)(from c in datoempR
                                           where moduloBonoC.Contains(c.Field<string>("modulo")) && c.Field<bool>("opera_simultaneo")
                                           group c by c.Field<DateTime>("fecha") into c
                                           select c.First()).Count() * porcentajebonoOp;
                    diasmodulo += (decimal)(from c in datoempR
                                            where moduloBonoC.Contains(c.Field<string>("modulo")) && !c.Field<bool>("opera_simultaneo")
                                            group c by c.Field<DateTime>("fecha") into c
                                            select c.First()).Count();
                    if (tablainc != null && tablainc.Length != 0)
                    {
                        bonocalidad = tablainc.Max((DataRow c) => c.Field<decimal>("bonoCalidad")) * diasmodulo / 5m;
                        return bonocalidad * porcentajebonoMod;
                    }
                }
                return bonocalidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private DataTable ObtenerConteoLayoutxOperacionDia(DataRow[] lt, DataRow[] empModDia, DateTime fecha, string modulo)
        {
            try
            {
                DataTable dtInD = new DataTable();
                dtInD.Columns.Add("modulo", typeof(string));
                dtInD.Columns.Add("fecha", typeof(DateTime));
                dtInD.Columns.Add("operacion", typeof(string));
                dtInD.Columns.Add("conteo", typeof(decimal));
                int[] empleados = null;
                decimal conteodia = default(decimal);
                DataRow[] op = (from c in empModDia
                                group c by new
                                {
                                    c2 = c["operacion"]
                                } into grp
                                select grp.First()).ToArray();
                DataRow[] array = op;
                foreach (DataRow emp in array)
                {
                    conteodia = default(decimal);
                    int[] codemp = (from x in empModDia
                                    where x.Field<string>("operacion").Trim() == emp["operacion"].ToString().Trim()
                                    select x into u
                                    select u.Field<int>("codigo_empleado")).ToArray();
                    empleados = (from c in lt.AsEnumerable()
                                 where codemp.Contains(c.Field<int>("codigo_empleado")) && c.Field<decimal>("horas") > 0m
                                 select c into u
                                 select u.Field<int>("codigo_empleado")).ToArray();
                    conteodia = empModDia.Where((DataRow c) => empleados.Contains(c.Field<int>("codigo_empleado"))).Sum((DataRow c) => c.Field<decimal>("porcentaje_opera"));
                    dtInD.Rows.Add(modulo, fecha, emp["operacion"].ToString().Trim(), conteodia);
                }
                return dtInD;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ObtenerIncentivoTotal(DateTime ini, DateTime fin, int periodo, int semana, int calculo)
        {
            DataTable dtInD = new DataTable();
            dtInD.Columns.Add("Codigo", typeof(int));
            dtInD.Columns.Add("tipo", typeof(int));
            dtInD.Columns.Add("detalle", typeof(string));
            dtInD.Columns.Add("tipoCalc", typeof(int));
            dtInD.Columns.Add("Cantidad", typeof(decimal));
            dtInD.Columns.Add("Valor", typeof(decimal));
            dtInD.Columns.Add("Observacion", typeof(string));
            dtInD.Columns.Add("tipoIng", typeof(string));
            dtInD.Columns.Add("GeneradoSistema", typeof(bool));
            Session["IngDed"] = dtInD;
            DataTable planilla = new DataTable();
            planilla.Columns.Add("periodo", typeof(int));
            planilla.Columns.Add("semana", typeof(int));
            planilla.Columns.Add("modulo", typeof(string));
            planilla.Columns.Add("codigo_empleado", typeof(int));
            planilla.Columns.Add("nombrecompleto", typeof(string));
            planilla.Columns.Add("operacion", typeof(string));
            planilla.Columns.Add("dzpagar", typeof(decimal));
            planilla.Columns.Add("bonoasistencia", typeof(decimal));
            planilla.Columns.Add("amonestaciones", typeof(int));
            planilla.Columns.Add("incentivo", typeof(decimal));
            planilla.Columns.Add("otrosIngresos", typeof(decimal));
            planilla.Columns.Add("deducciones", typeof(decimal));
            planilla.Columns.Add("total", typeof(decimal));
            planilla.Columns.Add("usuariograba", typeof(string));
            planilla.Columns.Add("fechagraba", typeof(string));
            planilla.Columns.Add("idtipoing", typeof(int));
            planilla.Columns.Add("comentario", typeof(string));
            planilla.Columns.Add("generadosistema", typeof(bool));
            DataRow[] datoempR = null;
            DataRow[] datoempAdc = null;
            DataRow[] semanaProd = null;
            decimal docenasperiodo = default(decimal);
            decimal horasperiodo = default(decimal);
            decimal bonoasistencia = default(decimal);
            decimal operacioncritica = default(decimal);
            decimal bonocalidad = default(decimal);
            decimal incentivo = default(decimal);
            decimal incentivorango = default(decimal);
            decimal totalinc = default(decimal);
            decimal dzpagar = default(decimal);
            decimal deducciones = default(decimal);
            decimal ingresos = default(decimal);
            decimal bonoasistenciafijo = default(decimal);
            decimal incentivofijo = default(decimal);
            decimal diasperiodo = default(decimal);
            decimal valor = default(decimal);
            decimal cantidad = default(decimal);
            decimal dias = 0;
            int amonestaciones = 0;
            DataTable config = new DataTable();
            string user = Convert.ToString(Page.Session["usuario"]);
            int codigo = 0;
            try
            {
                config = Session["tablaConfig"] as DataTable;
                dias = ((config.Rows.Count <= 0) ? 5 : Convert.ToInt32(config.Rows[0]["diasPagar"])); //convert.Decimal
                DataTable lt = Session["HORASSEMANA"] as DataTable;
                DataTable incdiario = Session["INCENTIVOSDIARIO"] as DataTable;
                DataTable dtmod = Session["produccionPeriodo"] as DataTable;
                DataTable incrango = ProcesarIncentivosxDia(dtmod, periodo, semana, 1).Tables[0];
                DataTable eficienciaMod = new DataTable();
                eficienciaMod = Session["eficienciaMod"] as DataTable;
                DataTable CalidadMod = new DataTable();
                CalidadMod = Session["CalidadMod"] as DataTable;
                DataTable CumplimientoOQL = Session["CumplimientoOQL"] as DataTable;//quitar
                decimal bonustiempo = 0.5m;
                decimal horasemlab = 48m;
                Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
                DateTime finweekend = ((fin.DayOfWeek == DayOfWeek.Friday) ? fin.AddDays(2.0) : ((fin.DayOfWeek != DayOfWeek.Saturday) ? fin : fin.AddDays(1.0)));
                DataRow[] empincfijo = null;
                DataTable incfijo = PlnObtenerProteccionIncentivoFijo(periodo);
                DataTable rpt = Neg_DevYDed.ObtenerDetalleHorasExtrasxFecha(1, 0, ini, finweekend);
                DataRow[] IngrDeducInc = null;
                DataRow[] empleadoreg = null;
                DataTable dtIncDeducEmpleados = IncentivoIngDedccLOGxEmpleado(periodo, semana);
                DataRow[] ingdeducexcept = null;//pro
                DataTable dtIncDeducExcepciones = IncentivoIngDedExcepcionesSel(periodo, semana);
                //TODO:VHPO
                //DataTable oqlmod = PlnObtenerOQLModulosProd(ini, fin, 1);
                if (calculo == 1)
                {
                    IncentivoIngDedccLOGDelete(periodo, semana, 0, GeneradoSistema: true);
                }
                int[] codigo_empleado = ObtenerAsociadosIncentivoTotal(incrango, incdiario, incfijo, dtIncDeducEmpleados);
                DataRow[] emp = (from c in lt.AsEnumerable()
                                 where codigo_empleado.Contains(c.Field<int>("codigo_empleado"))
                                 group c by c.Field<int>("codigo_empleado") into grp
                                 select grp.First()).ToArray();
                string modulo = "";
                string operacion = "";
                DataRow[] array = emp;
                foreach (DataRow item in array)
                {
                    IngrDeducInc = null;
                    datoempR = null;
                    empincfijo = null;
                    bonoasistencia = default(decimal);
                    bonocalidad = default(decimal);
                    //operacioncritica = default(decimal);
                    incentivo = default(decimal);
                    incentivorango = default(decimal);
                    amonestaciones = 0;
                    deducciones = default(decimal);
                    ingresos = default(decimal);
                    totalinc = default(decimal);
                    horasperiodo = default(decimal);
                    docenasperiodo = default(decimal);
                    diasperiodo = default(decimal);
                    valor = default(decimal);
                    cantidad = default(decimal);
                    List<decimal> bonolist = new List<decimal>();
                    codigo = Convert.ToInt32(item["codigo_empleado"]);

                    int rubroP = PlnObtenerIDRubroIncentivo(codigo, 4, 1);//Si tiene embargo no se protege.
                    datoempR = (from a in incrango.AsEnumerable()
                                where a.Field<int>("codigo_empleado") == codigo
                                select a into c
                                orderby c.Field<DateTime>("fecha") descending
                                select c).ToArray();
                    empincfijo = (from c in incfijo.AsEnumerable()
                                  where c.Field<int>("codigo_empleado") == codigo
                                  select c).ToArray();
                    IngrDeducInc = (from c in dtIncDeducEmpleados.AsEnumerable()
                                    where c.Field<int>("codigo") == codigo && c.Field<int>("IdTipoIng") == rubroP && !c.Field<bool>("GeneradoSistema")
                                    select c).ToArray();
                    int existe = 0;
                    existe = datoempR.Length + empincfijo.Length + IngrDeducInc.Length;
                    if ((datoempR == null && empincfijo == null && IngrDeducInc == null) || existe == 0)
                    {
                        continue;
                    }
                    DataRow[] datoempI = (from d in incdiario.AsEnumerable()
                                          where d.Field<int>("codigo_empleado") == codigo
                                          select d).ToArray();
                    dzpagar = datoempI.Sum((DataRow r) => r.Field<decimal>("docenaspagarprot"));
                    DataRow[] jornada = (from c in lt.AsEnumerable()
                                         where c.Field<int>("codigo_empleado") == codigo && c.Field<DateTime>("fecha") >= ini && c.Field<DateTime>("fecha") <= fin && c.Field<decimal>("horas") > 0m
                                         select c).ToArray();
                    horasperiodo = jornada.Sum((DataRow r) => r.Field<decimal>("horas"));
                    diasperiodo = jornada.Count();
                    if (empincfijo.Length != 0)
                    {
                        bool prot_porcentual = true;
                        decimal prot_porcentaje = 1m;
                        bonoasistenciafijo = empincfijo.Select((DataRow c) => c.Field<decimal>("bonoasistencia_fijo")).First();
                        incentivofijo = empincfijo.Select((DataRow c) => c.Field<decimal>("incentivo_fijo")).First();
                        prot_porcentual = empincfijo.Select((DataRow c) => c.Field<bool>("porcentual")).First();
                        prot_porcentaje = empincfijo.Select((DataRow c) => c.Field<decimal>("porcentaje")).First();

                        if (prot_porcentual)
                        {
                            incentivo = Math.Round(incentivofijo * (decimal)dias / 5m * prot_porcentaje);
                            bonoasistencia = Math.Round(bonoasistenciafijo * (decimal)dias / 5m * prot_porcentaje);
                        }
                        else
                        {
                            incentivo = Math.Round(incentivofijo * (decimal)dias / 5m);
                            bonoasistencia = Math.Round(bonoasistenciafijo * (decimal)dias / 5m);
                        }
                        incentivo = incentivo * diasperiodo / (decimal)dias;
                        bonoasistencia = bonoasistencia * diasperiodo / (decimal)dias;
                        incentivorango = incentivo;

                        dtInD.Rows.Add(codigo, 1, "Proteccion", 1, prot_porcentaje * 100m, incentivo, "SISTEMA", rubroP, true);
                        dtInD.Rows.Add(codigo, 1, "BonoAsistencia", 1, bonoasistencia, bonoasistencia, "SISTEMA", rubroP, true);
                    }
                    else
                    {
                        incentivo = datoempI.Sum((DataRow r) => r.Field<decimal>("total"));
                        incentivorango = datoempI.Where((DataRow c) => c.Field<DateTime>("fecha") >= ini && c.Field<DateTime>("fecha") <= fin).Sum((DataRow r) => r.Field<decimal>("total"));
                        bonoasistencia = default(decimal);
                    }
                    decimal metaSem100 = default(decimal);
                    decimal dzEficiencia = default(decimal);
                    DataRow[] tablainc = null;
                    DataRow[] ModEfic = null;

                    if (datoempR.Length != 0)
                    {

                        if (!Convert.ToBoolean(config.Rows[0]["incluyeFinSem"]))
                        {
                            datoempAdc = datoempR.Where((DataRow c) => c.Field<DateTime>("fecha").DayOfWeek == DayOfWeek.Sunday || c.Field<DateTime>("fecha").DayOfWeek == DayOfWeek.Saturday).ToArray();
                            datoempR = datoempR.Where((DataRow c) => c.Field<DateTime>("fecha").DayOfWeek >= DayOfWeek.Monday && c.Field<DateTime>("fecha").DayOfWeek <= DayOfWeek.Friday).ToArray();
                        }
                        semanaProd = (from c in datoempR
                                      group c by c.Field<DateTime>("fecha") into c
                                      select c.First()).ToArray();
                        docenasperiodo = semanaProd.Sum((DataRow r) => r.Field<decimal>("docenasprodprot"));
                        modulo = datoempR.Select((DataRow c) => c.Field<string>("modulo")).FirstOrDefault();
                        operacion = datoempR.Select((DataRow c) => c.Field<string>("operacion")).FirstOrDefault();
                        string[] distribucionMod = datoempR.Select((DataRow c) => c.Field<string>("modulo")).Distinct().ToArray();//Todos los modulos en los que estuvo el empleado
                        DataRow[] existeSimultaneo = datoempR.Where((DataRow c) => c.Field<bool>("opera_simultaneo")).ToArray();
                        ModEfic = (from c in eficienciaMod.AsEnumerable()
                                   where distribucionMod.Contains(c.Field<string>("modulo"))
                                   select c).ToArray();
                        DataRow[] tablaincxconst = PlnObtenerTablaIncentivo(semanaProd, 1, 1, 0, modulo, docenasperiodo);

                        //debug manual
                        int veces = 0;
                        if (modulo.Contains("11"))
                        {
                            veces = 1;
                        }

                        dzEficiencia = (docenasperiodo / (200 * dias)) * 100;
                        // Declarar e inicializar tablaincX
                        DataRow tablaincX = null;
                        if (tablaincxconst != null && tablaincxconst.Length != 0)
                        {
                            
                           var filaValida = tablaincxconst.AsEnumerable()
                                .Where(c => dzEficiencia >= c.Field<decimal>("EficienciaDesde") && dzEficiencia <= c.Field<decimal>("EficienciaHasta"))
                                .FirstOrDefault();

                            tablainc = tablaincxconst.Where((DataRow c) => c.Field<decimal>("eficienciaDesde") <= dzEficiencia && 
                            c.Field<decimal>("eficienciaHasta") >= dzEficiencia).ToArray();
                                                          
                            if (existeSimultaneo.Length != 0 && ModEfic.Length != 0)
                            {
                                dzEficiencia = (from c in ModEfic.OrderByDescending((DataRow c) => c.Field<decimal>("eficienciaSem")).Take(1)
                                                select c.Field<decimal>("eficienciaSem")).First();
                            }

                            tablaincX = tablaincxconst.AsEnumerable()
                                            .Where(c => dzEficiencia >= c.Field<decimal>("EficienciaDesde") && dzEficiencia <= c.Field<decimal>("EficienciaHasta"))
                                            .FirstOrDefault();
                        }
                        int excp = 0;
                        
                        if (tablaincX != null) 
                        {
                            bool asistenciaPerfecta = horasemlab >= (horasperiodo - 0.5m); // máximo 30 min perdidos

                            if (empincfijo.Length == 0 && asistenciaPerfecta)
                            {
                                // Procesar el resultado encontrado
                                bonoasistencia = tablaincX.Field<decimal>("bonoasist"); // Acceder 
                            }

                            else
                            {
                                bonoasistencia = 0m; // no aplica bono
                            }
                            
                            if (IngrDeducInc != null)
                            {
                                ingdeducexcept = (from c in IngrDeducInc.AsEnumerable()
                                                  where c.Field<string>("detalle") == "OpCriticaYTransporte"
                                                  select c).ToArray();
                                excp = ingdeducexcept.Length;
                            }
                            //revisar para OPC

                            /*
                             condicion para las OpC
                            - operaciones: PM, PC, PT, RF y UH
                            - eficiencia >= 90%
                            - asistencia perfecta
                            - layout completo
                             */
                            if (excp == 0 && diasperiodo >= (decimal)dias)
                            {
                                decimal montoOpC = PlnAsignarIngresoOpCritica(semanaProd, dzEficiencia, operacion);
                                if (montoOpC > 0m)
                                {
                                    montoOpC = Math.Round(montoOpC * (decimal)dias / 5m, 2);
                                    ingresos += montoOpC;
                                    dtInD.Rows.Add(codigo, 1, "OpCriticaYTransporte", 1, montoOpC, montoOpC, "SISTEMA", rubroP, true);
                                }
                            }
                        }
                        if (operacion.Trim().ToUpper() == "IF")
                        {
                            //oqlmod
                            //int Veces = 0;
                            //if (codigo == 873675)
                            //{
                            //    Veces = 1;
                            //}

                            //QUITAR Y LAMAR A LOS BONOS
                            decimal montooql = PlnAsignarIngresoCumplimientoOQL(CumplimientoOQL, datoempR, existeSimultaneo);
                            if (montooql > 0m)
                            {
                                montooql = Math.Round(montooql * (decimal)dias / 5m, 2) * diasperiodo / (decimal)dias;
                                incentivorango += montooql;
                                incentivo += montooql;
                                dtInD.Rows.Add(codigo, 1, "PorcentajeOql", 1, montooql, montooql, "SISTEMA", rubroP, true);
                            }
                        }


                        excp = 0;
                        if (IngrDeducInc != null)
                        {
                            ingdeducexcept = (from c in IngrDeducInc.AsEnumerable()
                                              where c.Field<string>("detalle") == "DocenasAdicionales"
                                              select c).ToArray();


                            excp = ingdeducexcept.Length;
                        }
                        if (excp == 0 && datoempAdc != null && datoempAdc.Length != 0)
                        {
                            //adicionales
                            decimal montoDzAd = PlnAsignarDocenasAdicionales(datoempAdc);
                            if (montoDzAd > 0m)
                            {
                                ingresos += montoDzAd;
                                dtInD.Rows.Add(codigo, 1, "DocenasAdicionales", 1, montoDzAd, montoDzAd, "SISTEMA", rubroP, true);
                            }
                        }
                        excp = 0;
                        if (IngrDeducInc != null)
                        {
                            ingdeducexcept = (from c in IngrDeducInc.AsEnumerable()
                                              where c.Field<string>("detalle") == "BonoCalidad"
                                              select c).ToArray();
                            excp = ingdeducexcept.Length;
                        }
                        if (excp == 0)
                        {
                            string[] moduloCumplimientoS = null;
                            moduloCumplimientoS = (from c in ModEfic
                                                   where c.Field<decimal>("eficienciaSemReal") >= 70m
                                                   select c.Field<string>("modulo")).Distinct().ToArray();
                            if (moduloCumplimientoS.Length != 0)
                            {
                                bonocalidad = PlnAsignarIngresoBonoCalidad(moduloCumplimientoS, datoempR, CalidadMod, tablainc);

                                if (bonocalidad > 0m)
                                {
                                    ingresos += bonocalidad;
                                    dtInD.Rows.Add(codigo, 1, "BonoCalidad", 1, bonocalidad, bonocalidad, "SISTEMA", rubroP, true);
                                }
                            }
                        }
                    }
                    else
                    {
                        modulo = "60";//wbravo
                        operacion = item["operacion"].ToString();
                    }
                    if (IngrDeducInc != null)
                    {
                        DataRow[] array2 = IngrDeducInc;
                        foreach (DataRow ie in array2)
                        {
                            valor = default(decimal);
                            cantidad = default(decimal);
                            string campoafecta = ie["campoafecta"].ToString().Trim().ToLower();
                            string detalle = ie["detalle"].ToString().Trim();
                            int tipo = Convert.ToInt32(ie["tipo"].ToString());
                            valor = Convert.ToDecimal(ie["valor"].ToString());
                            cantidad = Convert.ToDecimal(ie["cantidad"].ToString());
                            if (campoafecta == "bonoasistencia" && tipo == 1)
                            {
                                bonoasistencia = valor;
                                continue;
                            }
                            if (campoafecta == "incentivo" && tipo == 1)
                            {
                                incentivorango += valor;
                                incentivo += valor;
                                continue;
                            }
                            if (campoafecta == "otrosingresos" && tipo == 1)
                            {
                                ingresos += valor;
                                continue;
                            }
                            if (detalle.ToLower() == "docenasmenos")
                            {
                                valor = ((datoempI.Length == 0) ? default(decimal) : (datoempI.Max((DataRow c) => c.Field<decimal>("costo")) * cantidad));
                                dtInD.Rows.Add(codigo, 2, detalle, 1, cantidad, valor, "SISTEMA", rubroP, false);
                            }
                            deducciones += valor;
                        }
                    }
                    totalinc += bonoasistencia;
                    totalinc += incentivo;
                    totalinc += ingresos;
                    IncentivoEmp empinc = new IncentivoEmp();
                    empinc = ObtenerMontoAmonestaciones(ini, fin, codigo, incentivorango, rubroP);
                    amonestaciones = empinc.Amonestaciones + empinc.Rechazos;
                    deducciones += Math.Round(empinc.Deduccion, 2);
                    valor = default(decimal);
                    cantidad = default(decimal);
                    empleadoreg = (from c in rpt.AsEnumerable()
                                   where c.Field<int>("codigo_empleado") == codigo && c.Field<int>("id_tipo") == 1 && (c.Field<int>("tipoingrdeduc") == 28 || c.Field<int>("tipoingrdeduc") == 32) && c.Field<int>("periodo") == periodo
                                   select c).ToArray();
                    if (empleadoreg.Length != 0)
                    {
                        DataRow[] tiporubro = (from c in empleadoreg
                                               group c by c.Field<string>("nombrerubro") into c
                                               select c.First()).ToArray();
                        DataRow[] array3 = tiporubro;
                        foreach (DataRow hes in array3)
                        {
                            string nombrerubro = hes["nombrerubro"].ToString().Trim();
                            cantidad = empleadoreg.Where((DataRow c) => c.Field<string>("nombrerubro").Trim() == nombrerubro).Sum((DataRow r) => r.Field<decimal>("tiempo"));
                            valor = empleadoreg.Where((DataRow c) => c.Field<string>("nombrerubro").Trim() == nombrerubro).Sum((DataRow r) => r.Field<decimal>("valor"));
                            deducciones += valor;
                            dtInD.Rows.Add(codigo, 2, nombrerubro, 1, cantidad, valor, "SISTEMA", rubroP, true);
                        }
                    }
                    totalinc -= deducciones;
                    incentivo = ((incentivo < 0m) ? 0m : incentivo);
                    totalinc = ((totalinc < 0m) ? 0m : totalinc);
                    planilla.Rows.Add(periodo, semana, modulo, codigo, item["nombrecompleto"].ToString(), operacion, dzpagar, bonoasistencia, amonestaciones, incentivo, ingresos, deducciones, Math.Round(totalinc), user, DateTime.Now, rubroP, "", true);
                }
                Session["IngDed"] = dtInD;
                return planilla;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + codigo);
            }
        }

        private IncentivoEmp ObtenerMontoAmonestaciones(DateTime ini, DateTime fin, int codigo, decimal incentivorango, int rubroP)
        {
            List<PIngDeducInc> param = new List<PIngDeducInc>();
            param = ParametosIngresosDeduccionesIncentivos(0);
            Session["PARAM"] = param;
            Neg_Amonestaciones Neg_Amonestaciones = new Neg_Amonestaciones();
            List<Neg_Amonestaciones.Amonestaciones> la = new List<Neg_Amonestaciones.Amonestaciones>();
            la = Neg_Amonestaciones.getAmonestacionesAplicarInc(ini, fin);
            Session["AMONESTACIONES"] = la;
            IncentivoEmp empinc = new IncentivoEmp();
            IncentivoEmp emp2 = new IncentivoEmp();
            empinc.Codigo = codigo;
            empinc.Incentivo = incentivorango;
            empinc.Amonestaciones = 0;
            empinc.Rechazos = 0;
            empinc.TipoIngr = rubroP;
            List<Neg_Amonestaciones.Amonestaciones> amone = la.Where((Neg_Amonestaciones.Amonestaciones i) => i.Codigo_empleado.Equals(empinc.Codigo)).ToList();
            if (amone != null)
            {
                foreach (Neg_Amonestaciones.Amonestaciones ta in amone)
                {
                    if (ta.IdParamPenalizacion == 2)
                    {
                        empinc.Amonestaciones = ta.Cantidad;
                    }
                    else
                    {
                        empinc.Rechazos = ta.Cantidad;
                    }
                }
            }
            if ((empinc.Amonestaciones > 0 || empinc.Rechazos > 0) && empinc.Incentivo > 0m)
            {
                emp2 = CalculoIngresosDeduccion(empinc, param, 0m, 0m, 0m, 0m);
            }
            return emp2;
        }

        private int[] ObtenerAsociadosIncentivoTotal(DataTable dprod, DataTable dpg, DataTable dprot, DataTable ding)
        {
            // Initialize empty DataTables to prevent NullReferenceException
            dprod = dprod ?? new DataTable();
            dpg = dpg ?? new DataTable();
            dprot = dprot ?? new DataTable();
            ding = ding ?? new DataTable();

            int[] codigos = null;
            int[] lista1 = null;
            int[] lista2 = null;
            int[] lista3 = null;
            int[] lista4 = null;
            lista1 = (from c in dprod.AsEnumerable()
                      select c.Field<int>("codigo_empleado")).ToArray();
            lista4 = (from c in dpg.AsEnumerable()
                      select c.Field<int>("codigo_empleado")).Except(lista1).ToArray();
            lista2 = (from c in dprot.AsEnumerable()
                      select c.Field<int>("codigo_empleado")).ToArray();
            lista3 = (from c in ding.AsEnumerable()
                      select c.Field<int>("codigo")).ToArray();
            codigos = lista1.Concat(lista4).ToArray();
            codigos = codigos.Concat(lista2).ToArray();
            codigos = codigos.Concat(lista3).ToArray();
            return codigos.Distinct().ToArray();
        }

        public DataTable ObtenerDesgloceViatico(DataRow[] datosemp, DateTime ini, DataRow[] datosmarca, int periodo)
        {
            Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
            DataTable rubros = Neg_Catalogos.PlnObtenerRubrosViativo();
            DateTime fecha = default(DateTime);
            DataTable dtInD = new DataTable();
            DataTable temporal = new DataTable();
            temporal.Columns.Add("codigo_empleado", typeof(int));
            temporal.Columns.Add("nombrecompleto", typeof(string));
            temporal.Columns.Add("nombre_depto", typeof(string));
            temporal.Columns.Add("fecha", typeof(DateTime));
            temporal.Columns.Add("viatico_total", typeof(decimal));

            DataRow[] empFecha = null;
            int ncolumnRubro = 0;
            decimal saldo = default(decimal);
            decimal cuota = default(decimal);
            decimal viatico_total = default(decimal);
            int countemp = 0;
            foreach (DataRow emp in datosemp)
            {
                viatico_total = Math.Round(Convert.ToDecimal(emp["TotalIncentivo"]));
                saldo = viatico_total;
                fecha = ini;
                if (saldo == 0m)
                {
                    continue;
                }
                for (int i = 0; i < 7; i++)
                {
                    DataRow newRow = temporal.NewRow();
                    newRow[0] = emp["codigo_empleado"].ToString();
                    newRow[1] = emp["nombrecompleto"].ToString();
                    newRow[2] = emp["nombre_depto"].ToString();
                    newRow[3] = fecha;
                    newRow[4] = viatico_total;

                    temporal.Rows.Add(newRow);
                    fecha = fecha.AddDays(1.0);
                }
                ncolumnRubro = 4;
                for (int j = 0; j < rubros.Rows.Count; j++)
                {
                    string rubroViatico = "";
                    decimal valorIni = default(decimal);
                    decimal valorFin = default(decimal);
                    decimal cuotaEstablecida = default(decimal);
                    rubroViatico = rubros.Rows[j]["rubroViatico"].ToString();
                    valorIni = decimal.Parse(rubros.Rows[j]["valorIni"].ToString());
                    valorFin = decimal.Parse(rubros.Rows[j]["valorFin"].ToString());
                    cuotaEstablecida = decimal.Parse(rubros.Rows[j]["cuotaEstablecida"].ToString());
                    if (countemp == 0)
                    {
                        temporal.Columns.Add(rubroViatico);
                    }
                    ncolumnRubro++;
                    foreach (DataRow item in temporal.Rows)
                    {
                        cuota = default(decimal);
                        empFecha = datosmarca.Where((DataRow c) => c.Field<int>("codigo_empleado") == int.Parse(emp["codigo_empleado"].ToString()) && Convert.ToDateTime(c.Field<string>("fecha")) == Convert.ToDateTime(item["fecha"])).ToArray();
                        if ((Convert.ToDateTime(item["fecha"]).DayOfWeek == DayOfWeek.Saturday || Convert.ToDateTime(item["fecha"]).DayOfWeek == DayOfWeek.Sunday) && rubroViatico.ToLower().IndexOf("cena") > -1)
                        {
                            item[ncolumnRubro] = 0;
                        }
                        else if (empFecha.Length != 0)
                        {
                            TimeSpan horae = TimeSpan.Parse(empFecha.FirstOrDefault()["entrada"].ToString());
                            double horasdia = TimeSpan.Parse(empFecha.FirstOrDefault()["salida"].ToString()).TotalHours - horae.TotalHours;
                            if (horasdia > 0.0)
                            {
                                if (saldo >= cuotaEstablecida && valorIni == valorFin && cuotaEstablecida > 0m)
                                {
                                    cuota = cuotaEstablecida;
                                }
                                else
                                {
                                    if (saldo > 0m && saldo < cuotaEstablecida && valorIni == valorFin && cuotaEstablecida > 0m)
                                    {
                                        item[ncolumnRubro] = 0;
                                        continue;
                                    }
                                    if (saldo > 0m)
                                    {
                                        if (saldo < valorIni)
                                        {
                                            item[ncolumnRubro] = 0;
                                            continue;
                                        }
                                        cuota = ((!(saldo >= valorIni) || !(saldo <= valorFin)) ? valorFin : saldo);
                                    }
                                }
                                item[ncolumnRubro] = cuota;
                                saldo -= cuota;
                            }
                            else
                            {
                                item[ncolumnRubro] = 0;
                            }
                        }
                        else
                        {
                            item[ncolumnRubro] = 0;
                        }
                    }
                }
                if (countemp == 0)
                {
                    dtInD = temporal.Clone();
                    dtInD.Columns.Add("total", typeof(decimal));
                    dtInD.Columns.Add("saldo", typeof(decimal));
                    dtInD.Columns.Add("periodo", typeof(int)); //vhpo
                }
                foreach (DataRow dr2 in temporal.Rows)
                {
                    DataRow dr = dtInD.NewRow();
                    dr[0] = dr2[0];
                    dr[1] = dr2[1];
                    dr[2] = dr2[2];
                    dr[3] = dr2[3];
                    dr[4] = dr2[4];
                    dr[5] = dr2[5];
                    dr[6] = dr2[6];
                    dr[7] = dr2[7];
                    dr[8] = dr2[8];
                    dr[9] = dr2[9];
                    dr[10] = Convert.ToDecimal(dr2[5]) + Convert.ToDecimal(dr2[6]) + Convert.ToDecimal(dr2[7]) + Convert.ToDecimal(dr2[8]) + Convert.ToDecimal(dr2[9]);
                    dr[11] = saldo;
                    dr[12] = periodo;
                    dtInD.Rows.Add(dr);
                }
                temporal.Rows.Clear();
                countemp++;
            }
            return dtInD;
        }

        public bool PlnIncentivosxDPlanillaIns(DataTable dt, int periodo, int semana)
        {
            string user = Convert.ToString(Page.Session["usuario"]);
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                Dato_Incentivos dato_Incentivos = new Dato_Incentivos();
                dato_Incentivos.PlnPagoIncentivoEmpleadoIns(userDetail.getIDEmpresa(), dt);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnIncentivosxEmpleadoIns(int periodo, int semana, string modulo, int codigo, string nombre, string operacion, decimal dzpagar, decimal bonoasistencia, int amonestaciones, decimal incentivo, decimal otrosingresos, decimal deducciones, decimal total, string user, int idtipoing, string comentario, bool generadosistema)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                Dato_Incentivos dato_Incentivos = new Dato_Incentivos();
                if (!dato_Incentivos.PlnPagoIncentivoEmpleadoIns(periodo, semana, modulo, codigo, nombre, operacion, dzpagar, bonoasistencia, amonestaciones, incentivo, otrosingresos, deducciones, total, user, idtipoing, comentario, generadosistema, userDetail.getIDEmpresa()))
                {
                    throw new Exception("Error al insertar total incentivo por empleado");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnIngresoPlanillaIns(DataTable dt, int periodo, int semana, bool upd)
        {
            try
            {
                string user = Convert.ToString(Page.Session["usuario"]);
                Neg_DevYDed NDevyDed = new Neg_DevYDed();
                DataTable dtIncDeducEmpleados = IncentivoIngDedccLOGxEmpleado(periodo, semana);
                DataRow[] Deduc = null;
                DataRow[] Ing = null;
                DataRow[] empleadodeduc = null;
                DataRow[] empleadoing = null;
                Deduc = (from c in dtIncDeducEmpleados.AsEnumerable()
                         where c.Field<int>("tipo") == 2 && (c.Field<string>("detalle") == "HES" || c.Field<string>("detalle").ToLower() == "hferiado (-)") && c.Field<bool>("GeneradoSistema")
                         select c).ToArray();
                Ing = (from c in dtIncDeducEmpleados.AsEnumerable()
                       where c.Field<int>("tipo") == 1 && (c.Field<string>("detalle") == "DocenasAdicionales" || c.Field<string>("detalle") == "OpCriticaYTransporte")
                       select c).ToArray();
                decimal montoRubro = default(decimal);
                decimal incReal = default(decimal);
                decimal adicional = default(decimal);
                decimal total = default(decimal);
                decimal hes = default(decimal);
                decimal viatico = default(decimal);
                decimal opCriticaYTr = default(decimal);
                foreach (DataRow item in dt.Rows)
                {
                    string detalle = "";
                    int rubroSP = 0;
                    int rubroP = 0;
                    montoRubro = default(decimal);
                    incReal = default(decimal);
                    adicional = default(decimal);
                    total = default(decimal);
                    hes = default(decimal);
                    viatico = default(decimal);
                    opCriticaYTr = default(decimal);
                    int codempleado = int.Parse(item["codigo_empleado"].ToString());
                    int idtipoing = int.Parse(item["idtipoing"].ToString());
                    empleadoing = null;
                    empleadodeduc = null;
                    if (Ing != null)
                    {
                        empleadoing = Ing.Where((DataRow c) => c.Field<int>("codigo") == codempleado && c.Field<int>("idtipoing") == idtipoing).ToArray();
                    }
                    if (Deduc != null)
                    {
                        empleadodeduc = Deduc.Where((DataRow c) => c.Field<int>("codigo") == codempleado && c.Field<int>("idtipoing") == idtipoing).ToArray();
                    }
                    opCriticaYTr = empleadoing.Where((DataRow c) => c.Field<string>("detalle") == "OpCriticaYTransporte").Sum((DataRow c) => c.Field<decimal>("valor"));
                    total = Convert.ToDecimal(item["total"]) - opCriticaYTr;
                    incReal = total;
                    DataRow[] array = empleadoing;
                    foreach (DataRow dr in array)
                    {
                        detalle = dr["detalle"].ToString();
                        montoRubro = default(decimal);
                        if (detalle.Trim().ToLower() == "docenasadicionales")
                        {
                            rubroSP = PlnObtenerIDRubroIncentivo(codempleado, idtipoing, 2);
                            adicional = dr.Field<decimal>("valor");
                            viatico = Convert.ToDecimal(item["incentivo"]) + Convert.ToDecimal(item["bonoasistencia"]) + (Convert.ToDecimal(item["otrosingresos"]) - adicional - opCriticaYTr);
                            if (adicional > 0m)
                            {
                                hes = empleadodeduc.Sum((DataRow c) => c.Field<decimal>("valor"));
                                montoRubro = adicional - hes;
                                incReal = ((!(montoRubro < 0m)) ? (total - montoRubro) : ((!(viatico > 0m)) ? default(decimal) : total));
                                montoRubro = ((montoRubro < 0m) ? 0m : montoRubro);
                                incReal = ((incReal < 0m) ? 0m : incReal);
                            }
                            else
                            {
                                if (upd)
                                {
                                    montoRubro = default(decimal);
                                }
                                incReal = total;
                            }
                        }
                        else
                        {
                            rubroSP = PlnObtenerIDRubroIncentivo(codempleado, idtipoing, 3);
                            montoRubro = dr.Field<decimal>("valor");
                        }
                        if (!NDevyDed.PlnRegistrarRubrosIncentivoIns(1, codempleado, semana, rubroSP, periodo, montoRubro, user))
                        {
                            throw new Exception("Error al insertar ingreso");
                        }
                    }
                    rubroP = PlnObtenerIDRubroIncentivo(codempleado, idtipoing, 1);
                    if (!NDevyDed.PlnRegistrarRubrosIncentivoIns(1, codempleado, semana, rubroP, periodo, incReal, user))
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

        public int PlnObtenerIDRubroIncentivo(int codigo_empleado, int idtipo, int rubro)
        {
            Neg_DevYDed NDevyDed = new Neg_DevYDed();
            int id = idtipo;
            if (NDevyDed.PlnValidarPersonalAplicaProteccion(codigo_empleado) == 0)
            {
                if (rubro == 1)
                {
                    if (idtipo == 4)
                    {
                        id = 29;
                    }
                    else if (idtipo == 14)
                    {
                        id = 30;
                    }
                }
                else if (rubro == 2)
                {
                    id = 35;
                }
                else
                {
                    id = 37;
                }

            }
            else
            {
                if (rubro == 2)
                {
                    id = 34;
                }
                else if (rubro == 3)
                {
                    id = 38;
                }


            }
            return id;
        }

        public bool PlnPagoIncentivoByCutIns(DataTable dt, int periodo, int semana)
        {
            Neg_DevYDed NDevyDed = new Neg_DevYDed();
            Dato_Incentivos datoI = new Dato_Incentivos();
            string user = Convert.ToString(Page.Session["usuario"]);
            try
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (!datoI.PlnPagoIncentivoByCutIns(Convert.ToDateTime(item["fecha_producido"]), Convert.ToDateTime(item["fecha_aprobado"]), item["modulo"].ToString(), item["corte"].ToString(), Convert.ToInt32(item["seccion"]), Convert.ToInt32(item["estilo"]), Convert.ToInt32(item["idconstruccion"]), Convert.ToDecimal(item["oql"]), Convert.ToDecimal(item["docenasprodprot"]), Convert.ToDecimal(item["docenaspagarprot"]), Convert.ToDecimal(item["costo"]), periodo, semana, Convert.ToDecimal(item["montodocenas"]), user))
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

        public bool PlnPagoIncentivoEmpleadoByCutIns(DataTable dt, int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                datoI.PlnPagoIncentivoEmpleadoByCutIns(userDetail.getIDEmpresa(), dt);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnIncentivoPendPagarxPeriodoIns(DataTable dt, int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                datoI.PlnIncentivoPendPagarxPeriodoIns(userDetail.getIDEmpresa(), dt);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IncentivoIngDedccLOGInsert(DataTable dt, int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    IncentivoIngDedccLOGInsert(int.Parse(dr["Codigo"].ToString()), periodo, semana, int.Parse(dr["tipo"].ToString()), dr["detalle"].ToString(), int.Parse(dr["tipoCalc"].ToString()), decimal.Parse(dr["Cantidad"].ToString()), decimal.Parse(dr["Valor"].ToString()), "", int.Parse(dr["tipoIng"].ToString()), bool.Parse(dr["GeneradoSistema"].ToString()));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnProteccionModuloIns(DateTime fecha, string modulo, string problema, decimal dz, string observacion, string user)
        {
            Neg_DevYDed NDevyDed = new Neg_DevYDed();
            Dato_Incentivos datoI = new Dato_Incentivos();
            try
            {
                if (!datoI.PlnProteccionModuloIns(fecha, modulo, problema, dz, observacion, user))
                {
                    throw new Exception("Error al insertar ingreso");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnPagoIncentivoDel(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.PlnPagoIncentivoDel(periodo, semana, userDetail.getIDEmpresa()))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnPagoIncentivobyCutDel(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.PlnPagoIncentivobyCutDel(periodo, semana))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnOqlPeriodoPagoDel(int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.PlnOqlPeriodoPagoDel(periodo))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnEficienciaModDel(int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (datoI.PlnEficienciaModDel(periodo))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PlnObtenerProteccionIncentivoFijo(int periodo)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.PlnObtenerProteccionIncentivoFijo(periodo, userDetail.getIDEmpresa());
        }

        public bool plnExcepcionesCalculoInc(int periodo, int semana)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                if (!datoI.plnExcepcionesCalculoInc(periodo, semana, userDetail.getIDEmpresa()))
                {
                    throw new Exception("Error al procesar excepcion de calculo.");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool PlnPagoIncentivoxRubroEmpleadoDel(int periodo, int semana, int codEmpleado, int ingresoAsociado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Incentivos datoI = new Dato_Incentivos();
            if (datoI.PlnPagoIncentivoxRubroEmpleadoDel(periodo, semana, codEmpleado, ingresoAsociado, userDetail.getIDEmpresa()))
            {
                return true;
            }
            return false;
        }

        public bool PlnProteccionIncentivoFijoIns(int codigo, decimal bonoasistencia, decimal incentivo, decimal porcentaje, int repeticiones, bool recurrente, bool estado, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            try
            {
                Dato_Incentivos dato_Incentivos = new Dato_Incentivos();
                if (!dato_Incentivos.PlnProteccionIncentivoFijoIns(codigo, bonoasistencia, incentivo, porcentaje, repeticiones, recurrente, estado, user, userDetail.getIDEmpresa()))
                {
                    throw new Exception("Error al insertar total incentivo por empleado");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PlnProteccionDzxFechaSel(DateTime fechaini, DateTime fechafin)
        {
            Dato_Incentivos datoI = new Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.PlnProteccionDzxFechaSel(fechaini, fechafin, userDetail.getIDEmpresa());
        }
    }

}


