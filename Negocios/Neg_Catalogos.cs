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
    public class Neg_Catalogos
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Empleados Dato_Empleados = new Dato_Empleados();
        Dato_Catalogos Dato_Catalogos = new Dato_Catalogos();
        Dato_Factores Dato_Factores = new Dato_Factores();
        
        #endregion
        public DataSet CargarPaises()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarPaises(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarDepartamentos(int idpadre)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarDepartamentos(idpadre,userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable PlnObtenerDeptosSuburdinados(int codigo_empleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Catalogos.PlnObtenerDeptosSuburdinados(codigo_empleado, userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarMunicipios(int idpadre)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarMunicipios(idpadre,userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarOperaciones(int codigo_cargo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarOperaciones(codigo_cargo,userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarNivelAcademico()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarNivelAcademico(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarUbicaciones()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarUbicaciones(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarEmpresas()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarEmpresas(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet CargarProcesos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarProcesos(userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable ObtenerTipoContratosActivo()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            return Dato_Catalogos.ObtenerTipoContratosActivo(userDetail.getIDEmpresa());
        }
        public DataSet CargarMonedas()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarMonedas(userDetail.getIDEmpresa());
            return ds;
        }

        public byte[] cargarFoto(int codEmple)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            byte[] byteArray = new byte[0];
            byteArray = Dato_Catalogos.SeleccionarFoto(codEmple, userDetail.getIDEmpresa());
            return byteArray;
        }

        public DataSet CargarEstadoCivil()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarEstadoCivil(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarTipoCasa()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarTipoCasa(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarEstadoEmpleado()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarEstadoEmpleado(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarTipoContrato()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarTipoContrato(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarTipoSalario()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarTipoSalario(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarTurno()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarTurno(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarCargo(int codigo_depto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarCargo(codigo_depto,userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet cargarGrupos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarGrupos(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet SeleccionarGrupos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.seleccionarGruposCatalogos(userDetail.getIDEmpresa());
            return ds;
        }

        public DataSet CargarMeses()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.SeleccionarMeses(userDetail.getIDEmpresa());
            return ds;
        }


        public bool AgregarAGrupo(int idGrupo, string descripcion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.InsertarAGrupo(idGrupo, descripcion, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool EditarGrupos(int idGrupo, int antGrupo, string descripcion, string antDescrip)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.EditarGrupo(idGrupo, antGrupo, descripcion, antDescrip, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarGrupos(int antGrupo, string antDescrip)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.EliminarGrupo(antGrupo, antDescrip, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditarFactor(decimal factor, bool activo, int id)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.EditarFactor(factor, activo, id, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable obtenerFactor()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Factores.obtenerFactor(userDetail.getIDEmpresa());
            return ds;
        }
        public DataSet cargarTipoPermisos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.cargarTipoPermisos(userDetail.getIDEmpresa());
            return ds;
        }

        public DataTable selecionarUbicaciones()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Catalogos.selecionarUbicaciones(userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable seleccionarUbicacionesxCod(int codigo_ubicacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Catalogos.seleccionarUbicacionesxCod(codigo_ubicacion,userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable selecionarDepartamentos()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Catalogos.selecionarDepartamentos(userDetail.getIDEmpresa());
            return ds;
        }

        public DataTable selecionarDepartamentos(int idempresa)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Catalogos.selecionarDepartamentos(idempresa);
            return ds;
        }

        public bool agregarUbicaciones(int idEmp, string ubicacion, string telefono, string nPatronal,
            string nRuc, string departamento, string municipio, string direccion, int tplanilla)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.agregarUbicaciones(idEmp, ubicacion, telefono, nPatronal,
                nRuc, departamento, municipio, direccion,tplanilla, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool editarUbicaciones(int idEmp, string ubicacion, string telefono, string nPatronal,
            string nRuc, string departamento, string municipio, string direccion, int idUbicacion,int tplanilla)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.editarUbicaciones(idEmp, ubicacion, telefono, nPatronal,
                nRuc, departamento, municipio, direccion, idUbicacion,tplanilla, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlnDepartamentosIns(string departamento, int centroCosto, bool directo, int idpadre,int idjefe,string jefe,int omitirpl)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.PlnDepartamentosIns(departamento, centroCosto, directo,idpadre,idjefe,jefe,omitirpl, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlnDepartamentosUpd(string departamento, int centroCosto, bool directo, int codigoDepto,int idpadre,int idjefe,string jefe, int omitirpl,bool activo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Catalogos.PlnDepartamentosUpd(departamento, centroCosto, directo, codigoDepto,idpadre, idjefe,jefe,omitirpl,activo, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataSet SeleccionarCatalogosxGrupos(int grupo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Catalogos.seleccionarCatalogosxGrupo(grupo, userDetail.getIDEmpresa());
            return ds;
        }
        public DataTable PlnObtenerRubrosViativo()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            return Dato_Catalogos.PlnObtenerRubrosViativo(userDetail.getIDEmpresa());
        }
    }
}
