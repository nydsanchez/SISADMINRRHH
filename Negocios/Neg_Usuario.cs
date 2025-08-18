using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Datos;
using System.Data;

namespace Negocios
{
    public class Neg_Usuario
    {
        public List<UsuarioSession> VerificarUsuario(UsuarioSessionD usuario)
        {
            
            Dato_Usuario datoU = new Dato_Usuario();
            return datoU.ValidarUsuario(usuario);
        }

        public List<UsuarioSession> VerificarCredenciales(UsuarioSessionD usuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Usuario datoU = new Datos.Dato_Usuario();
            return datoU.ValidarCredenciales(usuario, userDetail.getIDEmpresa());
        }

        public DataTable Usuario_Select()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Usuario datoU = new Datos.Dato_Usuario();
            return datoU.Usuario_Select(userDetail.getIDEmpresa());
        }


        public bool usuarioInsert(string usuario, string pass, bool activo, string nombre, string apellido)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Usuario datoU = new Datos.Dato_Usuario();
            return datoU.UsuarioInsert(usuario, pass, activo, nombre, apellido, userDetail.getIDEmpresa());
        }

        public bool UsuarioUpdate(string usuario, bool activo, string nombre, string apellido, int idUsuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Usuario datoU = new Datos.Dato_Usuario();
            return datoU.UsuarioUpdate(usuario, activo, nombre, apellido, idUsuario, userDetail.getIDEmpresa());
        }

        public bool UsuarioUpdatePass(string usuario, string pass, bool activo, string nombre, string apellido, int idUsuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Usuario datoU = new Datos.Dato_Usuario();
            return datoU.UsuarioUpdatePass(usuario, pass, activo, nombre, apellido, idUsuario, userDetail.getIDEmpresa());
        }

        public int Usuario_SelectByMaxId()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Usuario datoU = new Datos.Dato_Usuario();
            return datoU.Usuario_SelectByMaxId(userDetail.getIDEmpresa());
        }
        public string EncriptarContraseña(string pass)
        {
            Datos.Dato_Usuario datoU = new Datos.Dato_Usuario();
            return datoU.EncriptarContraseña(pass);
        } 

    }
}
