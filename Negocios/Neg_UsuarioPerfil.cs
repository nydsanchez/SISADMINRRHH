using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Negocios
{
    public class Neg_UsuarioPerfil
    {

        public DataTable UsuarioPerfil_Select()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_UsuarioPerfil dup = new Datos.Dato_UsuarioPerfil();
            return dup.UsuarioPerfil_Select(userDetail.getIDEmpresa());
        }
        public bool UsuarioPerfilInsert(int IdUsuario, int IdPerfil)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_UsuarioPerfil datoU = new Datos.Dato_UsuarioPerfil();
            return datoU.UsuarioPerfilInsert(IdUsuario, IdPerfil, userDetail.getIDEmpresa());
        }
        public bool UsuarioPerfilDelete(int IdUsuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_UsuarioPerfil datoU = new Datos.Dato_UsuarioPerfil();
            return datoU.UsuarioPerfilDelete(IdUsuario, userDetail.getIDEmpresa());
        }
    }
}