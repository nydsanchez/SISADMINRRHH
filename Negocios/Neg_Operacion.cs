using Datos;
using System;
using System.Data;

namespace Negocios
{
    public class Neg_Operacion
    {
        public DataTable PlnOperacionSel()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int idEmpresa = userDetail.getIDEmpresa();
            return new Dato_Operacion().PlnOperacionSel(idEmpresa);
        }
        public bool PlnOperacionIns(string codigo_operacion, bool critica,string descripcion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            return new Dato_Operacion().PlnOperacionIns(codigo_operacion, critica, userDetail.getIDEmpresa(), userDetail.getUser(), descripcion);
        }

        public bool PlnOperacionUpd(string codigo_operacion, bool critica, string descripcion, bool activo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            return new Dato_Operacion().PlnOperacionUpd(codigo_operacion, critica, userDetail.getIDEmpresa(),activo, userDetail.getUser(), descripcion);
        }

        public bool PlnCargosOperacionIns(string codigo_operacion, string codigo_cargo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return new Dato_Operacion().PlnCargosOperacionIns(codigo_cargo, codigo_operacion, userDetail.getIDEmpresa());
        }

        public bool PlnCargosOperacioDel(string codigo_operacion, string codigo_cargo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return new Dato_Operacion().PlnCargosOperacionDel(codigo_cargo, codigo_operacion, userDetail.getIDEmpresa());
        }

        public DataTable PlnCargosOperacionSel(int codigo_cargo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            return new Dato_Operacion().PlnCargosOperacionSel(userDetail.getIDEmpresa(), codigo_cargo);
        }
    }
}
