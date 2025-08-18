using Datos;
using System;
using System.Data;

namespace Negocios
{
    public class Neg_Cargo
    {
        public DataTable PlnCargoSel()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int idEmpresa = userDetail.getIDEmpresa();
            return new Dato_Cargo().PlnCargosSel(idEmpresa);
        }

        public DataTable PlnCargoSel(int codigo_departamento)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int idEmpresa = userDetail.getIDEmpresa();
            return new Dato_Cargo().PlnCargosSel(idEmpresa,codigo_departamento);
        }

        public bool PlnCargoIns(string nombre_cargo, bool indirecto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int idEmpresa = userDetail.getIDEmpresa();
            string user = userDetail.getUser();
            return new Dato_Cargo().PlnCargoIns(nombre_cargo, indirecto, idEmpresa, user);
        }

        public bool PlnDepartamentoCargoIns(string codigo_cargo, int codigo_depto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int idEmpresa = userDetail.getIDEmpresa();
            return new Dato_Cargo().PlnDepartamentoCargoIns(codigo_cargo,codigo_depto,idEmpresa);
        }

        public bool PlnDepartamentoCargoDel(string codigo_cargo, int codigo_depto)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int idEmpresa = userDetail.getIDEmpresa();
            return new Dato_Cargo().PlnDepartamentoCargoDel(codigo_cargo, codigo_depto, idEmpresa);
        }

        public bool PlnCargoUpd(int codigo_cargo, string nombre_cargo, bool indirecto, bool activo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            int idEmpresa = userDetail.getIDEmpresa();
            string user = userDetail.getUser();
            return new Dato_Cargo().PlnCargoUpd(codigo_cargo, nombre_cargo, indirecto, activo, idEmpresa,user);
        }
    }
}
