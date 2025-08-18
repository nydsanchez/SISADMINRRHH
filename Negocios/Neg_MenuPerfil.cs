using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Negocios
{
    public class Neg_MenuPerfil
    {
        public bool MenuPerfilDelete(int IdPerfil)
        {
           IUserDetail userDetail = UserDetailResolver.getUserDetail();
           Datos.Dato_MenuPerfil dmp = new Datos.Dato_MenuPerfil();
           return dmp.MenuPerfilDelete(IdPerfil, userDetail.getIDEmpresa());
        }

        public bool MenuPerfilInsert(int IdMenu,int IdPerfil)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_MenuPerfil dmp = new Datos.Dato_MenuPerfil();
            return dmp.MenuPerfilInsert(IdMenu, IdPerfil, userDetail.getIDEmpresa());
        }
    }
}