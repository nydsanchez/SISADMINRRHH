using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;



namespace Negocios
{
    public class Neg_Perfil
    {
        public DataTable Perfil_Select()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Perfil dp = new Datos.Dato_Perfil();
            return dp.Perfil_Select(userDetail.getIDEmpresa());
        }

        public bool PerfilInsert(bool activo, string NombreRol)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Perfil dp = new Datos.Dato_Perfil();
            return dp.PerfilInsert(activo, NombreRol, userDetail.getIDEmpresa());

        }

        public bool PerfilUpdate(int IdPerfil, bool Activo, string Descripcion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Perfil dp = new Datos.Dato_Perfil();
            return dp.PerfilUpdate(IdPerfil, Activo, Descripcion, userDetail.getIDEmpresa());

        }

        public int Perfil_SelectByMaxId()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Perfil datoU = new Datos.Dato_Perfil();
            return datoU.Perfil_SelectByMaxId(userDetail.getIDEmpresa());
        }
    }
}