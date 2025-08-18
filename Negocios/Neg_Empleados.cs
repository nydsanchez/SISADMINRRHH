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
    public class Neg_Empleados
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Empleados Dato_Empleados = new Dato_Empleados();
        public string departamento;
        public string cargo;
        public string nombrecompleto;
        public string turno;
        public double horast = 0;
        public double horasv = 0;
        public double horascg = 0;
        public double horassg = 0;
        public double horass = 0;
        public double horasapagar = 0;
        public double horasturno = 0; 
        public int codigo_empleado = 0;
        public decimal salariomensual = 0;
        public int codigo_depto = 0;
        public int codigo_cargo = 0;
        public decimal saldo_vacaciones = 0;
        public int moneda;
        public int estado;
        public bool flexitime = false;
        public int tipocontrato;
        public int tiposalario;
        public bool p;
        public DateTime fecheingreso = DateTime.Now;
        public bool opera_simultaneo = false;
        public double horasincompletas = 0;
        public double horasvariables = 0;
        public string operacion = "";

        public bool marca;

        public dsPlanilla.dtHorasTDataTable dtHorasT = new dsPlanilla.dtHorasTDataTable();
        #endregion

        public  DataTable ObtenerInfoDetEmpleado(string codigoEmp)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            int codEmple = Convert.ToInt32(codigoEmp);
            ds = Dato_Empleados.ObtenerDetalleEmpleados(codEmple, userDetail.getIDEmpresa());
            return ds;
        }
        public string SelNombreCompleto(string codigoEmp)
        {
            string nombre;
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int codEmple = Convert.ToInt32(codigoEmp);
            nombre = Dato_Empleados.SelNombreCompleto(codEmple, userDetail.getIDEmpresa());
            return nombre;
        }
        public DataTable HistorialContratacionXEmpleado(string doc_identidad, int idEmpresa)
        {
           
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds=Dato_Empleados.HistorialContratacionXEmpleado(doc_identidad, idEmpresa);
            return ds;
        }

        public void SaveFile(string name, byte[] data, int numemp, int tipo, int idEmpresa)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Empleados.SaveFile(name, data, numemp, tipo,idEmpresa);
        }

       public  DataTable ObtenerInfoDetEmpleadoxNombre(string nombreEmp)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Empleados.ObtenerDetalleEmpleadoxNombre(nombreEmp, userDetail.getIDEmpresa());
            return ds;
        }

       public bool InfoEmpleadoEditar(int codEmpleado,  string primerNomb, string segundoNomb, string primerApellido, string segundApellido, string nombreCompleto, string sexo,
        int pais, int departamento, int municipio, DateTime fechaNac, string numCedula, DateTime emiteCed, DateTime venceCed, string operacion, int nivelAcademico, string numInss, int ubicacion, int proceso,
            int cargo, DateTime primerIngreso, int estadoEmpl, DateTime fechaIngr, DateTime fechaEgrs, int moneda, decimal salario, string cuentaContable, decimal subsidio, string cuentaBanc, decimal incentivo,
            int turno, int tipoContrato, int tipoSalario, int liquidado, bool marca, bool ganaExtras, string telefono, string celular, string correo, int estadoCivil, int tipoCasa, string viveCon,
            int domiciMunc, string direccion, string nombreEmerg, string telfEmerg, string parentesco, string direcEmerg, string nombPadre, string numCedPadre, string vivePadre,
            string nombMadre, string numCedMadre, string viveMadre, string nombEsps, string numCedEsps, string nombreHijo1, string lugarNacH1, DateTime fechaNacH1, string sexoH1, string nombreHijo2, string lugarNacH2,
            DateTime fechaNacH2, string sexoH2, string nombreHijo3, string lugarNacH3, DateTime fechaNacH3, string sexoH3, string nombreHijo4, string lugarNacH4, DateTime fechaNacH4, string sexoH4, string empAnterior1, 
            string verfEmpAnt1, string observEmpAnt1, decimal ultimoSalarioEmp1, string verfUltSalaEm1, string observSalAntEm1, string cargoEmp1, string verfCargoEmp1, string observUltCargoEmp1, string motivoSalidaEmp1,
            string verfMotvSal1, string observMotvSal1, DateTime fechaIngEmp1, string verfFechaIngEm1, string observFechaIngEm1, DateTime fechaEgrEmp1, string verfFechEgrEm1, string obsercFechEgrEm1, string empAnterior2, 
            decimal UltSalarioEmp2, string ultCargoEmp2, string motSalidEmp2, DateTime fechaIngEmp2, DateTime fechaEgrsEmp2, string referenciaPers, string verfRefPers, string observRefPers, string parentsRefPers, string verfParentRef,
            string observRefParnRef, string numTelfRefPers, string verfNumTelfRef, string observNumTelRef, string tiempoConcRef, string verfTiempCncRef, string observTiempCncRef, string padeceEnfermedad, string tipoEnfermedad,
            string enfermedadDesde, string descEstudios, string centroEstudios, 
            //string fechIngHistoEgr, string fechaEgrsHistoEg, string ubicHistoEgrs, string deptoHistoEgrs, string cargoHistoEgrs, string motvHistoEgrs, 
            string nombreJefe,string observacion, string user, bool pagoVacacion, bool discapacidad,string cuentaMall,bool flexitime, bool credito, int domicdepto,bool multitask)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empleados.EditarEmpleado(codEmpleado, primerNomb, segundoNomb, primerApellido, segundApellido, nombreCompleto, sexo, pais, departamento, municipio,
                fechaNac, numCedula, emiteCed, venceCed, operacion, nivelAcademico, numInss, ubicacion, proceso, cargo, primerIngreso, estadoEmpl, fechaIngr, fechaEgrs,
                moneda, salario, cuentaContable, subsidio, cuentaBanc, incentivo, turno, tipoContrato, tipoSalario, liquidado, marca, ganaExtras ,telefono, celular, correo, 
                estadoCivil, tipoCasa, viveCon, domiciMunc, direccion, nombreEmerg, telfEmerg, parentesco, direcEmerg, nombPadre, numCedPadre, vivePadre, 
                nombMadre, numCedMadre, viveMadre, nombEsps, numCedEsps, nombreHijo1, lugarNacH1, fechaNacH1, sexoH1, nombreHijo2, lugarNacH2, fechaNacH2, sexoH2, nombreHijo3,
                lugarNacH3, fechaNacH3, sexoH3, nombreHijo4, lugarNacH4, fechaNacH4, sexoH4, empAnterior1, verfEmpAnt1, observEmpAnt1, ultimoSalarioEmp1, verfUltSalaEm1, observSalAntEm1, 
                cargoEmp1, verfCargoEmp1, observUltCargoEmp1, motivoSalidaEmp1, verfMotvSal1, observMotvSal1, fechaIngEmp1, verfFechaIngEm1, observFechaIngEm1, fechaEgrEmp1, verfFechEgrEm1, 
                obsercFechEgrEm1, empAnterior2, UltSalarioEmp2, ultCargoEmp2, motSalidEmp2, fechaIngEmp2, fechaEgrsEmp2, referenciaPers, verfRefPers, observRefPers, parentsRefPers, verfParentRef, 
                observRefParnRef, numTelfRefPers, verfNumTelfRef, observNumTelRef, tiempoConcRef, verfTiempCncRef, observTiempCncRef, padeceEnfermedad, tipoEnfermedad, enfermedadDesde, descEstudios,
                centroEstudios, 
                //fechIngHistoEgr, fechaEgrsHistoEg, ubicHistoEgrs, deptoHistoEgrs, cargoHistoEgrs, motvHistoEgrs, 
                nombreJefe, observacion, userDetail.getIDEmpresa(), user,pagoVacacion, discapacidad,cuentaMall,flexitime,credito,domicdepto,multitask
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
        }


        public bool InfoEmpleadoAgregar(string primerNomb, string segundoNomb, string primerApellido, string segundApellido, string nombreCompleto, string sexo,
         int pais, int departamento, int municipio, DateTime fechaNac, string numCedula, DateTime emiteCed, DateTime venceCed, string operacion, int nivelAcademico, string numInss, int ubicacion, int proceso, int cargo,
             DateTime primerIngreso, int estadoEmpl, DateTime fechaIngr, DateTime fechaEgrs, int moneda, decimal salario, string cuentaContable, decimal subsidio, string cuentaBanc, decimal incentivo,
             int turno, int tipoContrato, int tipoSalario, int liquidado, bool marca, bool ganaExtras, string telefono, string celular, string correo, int estadoCivil, int tipoCasa, string viveCon,
             int domiciMunc, string direccion, string nombreEmerg, string telfEmerg, string parentesco, string direcEmerg, string nombPadre, string numCedPadre, string vivePadre,
             string nombMadre, string numCedMadre, string viveMadre, string nombEsps, string numCedEsps, string nombreHijo1, string lugarNacH1, DateTime fechaNacH1, string sexoH1, string nombreHijo2, string lugarNacH2,
             DateTime fechaNacH2, string sexoH2, string nombreHijo3, string lugarNacH3, DateTime fechaNacH3, string sexoH3, string nombreHijo4, string lugarNacH4, DateTime fechaNacH4, string sexoH4, string empAnterior1,
             string verfEmpAnt1, string observEmpAnt1, decimal ultimoSalarioEmp1, string verfUltSalaEm1, string observSalAntEm1, string cargoEmp1, string verfCargoEmp1, string observUltCargoEmp1, string motivoSalidaEmp1,
             string verfMotvSal1, string observMotvSal1, DateTime fechaIngEmp1, string verfFechaIngEm1, string observFechaIngEm1, DateTime fechaEgrEmp1, string verfFechEgrEm1, string obsercFechEgrEm1, string empAnterior2,
             decimal UltSalarioEmp2, string ultCargoEmp2, string motSalidEmp2, DateTime fechaIngEmp2, DateTime fechaEgrsEmp2, string referenciaPers, string verfRefPers, string observRefPers, string parentsRefPers, string verfParentRef,
             string observRefParnRef, string numTelfRefPers, string verfNumTelfRef, string observNumTelRef, string tiempoConcRef, string verfTiempCncRef, string observTiempCncRef, string padeceEnfermedad, string tipoEnfermedad,
             string enfermedadDesde, string descEstudios, string centroEstudios, 
            // string fechIngHistoEgr, string fechaEgrsHistoEg, string ubicHistoEgrs, string deptoHistoEgrs, string cargoHistoEgrs, string motvHistoEgrs, 
             string nombreJefe,string observacion, string user, bool pagoVacacion, bool discapacidad, string cuentaMall,bool flexitime,bool credito,int domicdepto,bool multitask)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Empleados.AgregarEmpleado(primerNomb, segundoNomb, primerApellido, segundApellido, nombreCompleto, sexo, pais, departamento, municipio,
                fechaNac, numCedula, emiteCed, venceCed, operacion, nivelAcademico, numInss, ubicacion, proceso, cargo, primerIngreso, estadoEmpl, fechaIngr, fechaEgrs,
                moneda, salario, cuentaContable, subsidio, cuentaBanc, incentivo, turno, tipoContrato, tipoSalario, liquidado, marca, ganaExtras, telefono, celular, correo,
                estadoCivil, tipoCasa, viveCon, domiciMunc, direccion, nombreEmerg, telfEmerg, parentesco, direcEmerg, nombPadre, numCedPadre, vivePadre,
                nombMadre, numCedMadre, viveMadre, nombEsps, numCedEsps, nombreHijo1, lugarNacH1, fechaNacH1, sexoH1, nombreHijo2, lugarNacH2, fechaNacH2, sexoH2, nombreHijo3,
                lugarNacH3, fechaNacH3, sexoH3, nombreHijo4, lugarNacH4, fechaNacH4, sexoH4, empAnterior1, verfEmpAnt1, observEmpAnt1, ultimoSalarioEmp1, verfUltSalaEm1, observSalAntEm1,
                cargoEmp1, verfCargoEmp1, observUltCargoEmp1, motivoSalidaEmp1, verfMotvSal1, observMotvSal1, fechaIngEmp1, verfFechaIngEm1, observFechaIngEm1, fechaEgrEmp1, verfFechEgrEm1,
                obsercFechEgrEm1, empAnterior2, UltSalarioEmp2, ultCargoEmp2, motSalidEmp2, fechaIngEmp2, fechaEgrsEmp2, referenciaPers, verfRefPers, observRefPers, parentsRefPers, verfParentRef,
                observRefParnRef, numTelfRefPers, verfNumTelfRef, observNumTelRef, tiempoConcRef, verfTiempCncRef, observTiempCncRef, padeceEnfermedad, tipoEnfermedad, enfermedadDesde, descEstudios,
                centroEstudios, //fechIngHistoEgr, fechaEgrsHistoEg, ubicHistoEgrs, deptoHistoEgrs, cargoHistoEgrs, motvHistoEgrs,
                nombreJefe, observacion, userDetail.getIDEmpresa(), user,pagoVacacion,discapacidad,cuentaMall,flexitime,credito,domicdepto,multitask
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
            
        }

       public bool InfoEmpleadoEliminar(int codEmpleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Empleados.EliminarEmpleado(codEmpleado, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {

                return false;
            }
            try
            {
                return true;
            }
            catch (SystemException)
            {

                return false;
            }

            return true;
        }



       public string obtenerUltimoEmpleadoAgregado()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            string codigo = Dato_Empleados.obtenerUltimoEmpleadoAgregado(userDetail.getIDEmpresa());
            return codigo;
        }

        public bool CambioSalarioGuardar(int codigo, int codubicacion, int coddepto, decimal salarioant, decimal salarionew,            
            string observacion, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empleados.CambioSalarioGuardar(codigo, codubicacion, coddepto, salarioant, salarionew, observacion, userDetail.getIDEmpresa(), user
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public bool plnHistorialEgresosIns(int codigo, DateTime fechaingreso, DateTime fechaegreso, bool renovacion, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Empleados.plnHistorialEgresosIns(codigo, fechaingreso, fechaegreso, renovacion, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            return false;
        }
        public bool plnEstadoEmpleadoUpd(int codigo, DateTime fechaingreso, DateTime fechaegreso, int estado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empleados.plnEstadoEmpleadoUpd(codigo, fechaingreso, fechaegreso,estado, userDetail.getIDEmpresa()
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public bool plnSalarioMinEmpleadoUpd(int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Empleados.plnSalarioMinEmpleadoUpd(codigo, userDetail.getIDEmpresa()))
            {
                return true;
            }
            return false;
        }
        public DataTable pln_empleadosHistoricoALL(int idempresa, DateTime fecha, int filtro)
        {
            Datos.Dato_Empleados datoI = new Datos.Dato_Empleados();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return datoI.pln_empleadosHistoricoALL(userDetail.getIDEmpresa(), fecha, filtro);
        }

        public DataSet HistorialSalariosXEmpleado(int codigoEmp)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Empleados.HistorialSalariosXEmpleado(codigoEmp, userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet HistorialEgresosXEmpleado(int codigoEmp)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Empleados.HistorialEgresosXEmpleado(codigoEmp, userDetail.getIDEmpresa());
            return ds;
        }
        public bool CuentaEmpleadoUpd(int codigo, string cuenta, int tipo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empleados.CuentaEmpleadoUpd(codigo,cuenta ,tipo,userDetail.getIDEmpresa()
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        //NUEVOS SP PARA CALCULO INCENTIVOS PRODUCCION DIARIA APROBADA
        
         public DataTable pln_empleadosHistoricoSelectxDia(DateTime fechaini,DateTime fechafin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_Empleados.pln_empleadosHistoricoSelectxDia(userDetail.getIDEmpresa(),fechaini,fechafin);
            
        }
        public bool PlnTrasladoEmpleadosIns(DateTime fecha, int codigo, int codigo_depto, string justificacion, string operacion, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Empleados.PlnTrasladoEmpleadosIns(fecha, codigo, codigo_depto, justificacion, operacion, user, userDetail.getIDEmpresa()))
            {
                return true;
            }
            return false;
        }
        public bool PlnEmpleadoOperaSimultaneoIns(int codigo, int codigo_depto,decimal porcentaje,int codigo_deptoc,int desactivar, string user)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empleados.PlnEmpleadoOperaSimultaneoIns(codigo, codigo_depto, porcentaje,codigo_deptoc,desactivar, user, userDetail.getIDEmpresa()
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public bool PlnEmpleadoOperaSimultaneoDel()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empleados.PlnEmpleadoOperaSimultaneoDel( userDetail.getIDEmpresa()
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public bool PlnEmpleadosHistUpd(int codigo, DateTime fecha, int codigo_depto, int codigo_deptoc, decimal porcentaje, string operacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Empleados.PlnEmpleadosHistUpd(codigo, fecha, codigo_depto, codigo_deptoc, porcentaje, operacion, userDetail.getIDEmpresa()))
            {
                return true;
            }
            return false;
        }

        public bool PlnTrasladoEmpleadosDel(DateTime fecha, int codigo, int codigo_depto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empleados.PlnTrasladoEmpleadosDel(fecha, codigo,codigo_depto, userDetail.getIDEmpresa()
            ))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
        public DataTable PlnTrasladoEmpleadosSel(DateTime fechaini, DateTime fechafin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_Empleados.PlnTrasladoEmpleadosSel(fechaini, fechafin, userDetail.getIDEmpresa());

        }
        public DataTable plnObtenerEmpleadosOpSimultaneo(DateTime fechaini, DateTime fechafin, int codigo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_Empleados.plnObtenerEmpleadosOpSimultaneo(fechaini, fechafin,codigo, userDetail.getIDEmpresa());

        }
        public DataTable PlnDeptoEmpHistoricoSel(int codigo,DateTime fechaini, DateTime fechafin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return Dato_Empleados.PlnDeptoEmpHistoricoSel(codigo,fechaini, fechafin, userDetail.getIDEmpresa());

        }

        public string RevertirBaja(int codigo_empleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return new Dato_Empleados().RevertirBaja(codigo_empleado, userDetail.getIDEmpresa());
        }
    }

    public enum Estado
    {
        LiquidadosYActivos = 1,
        SoloActivos
    }
}
