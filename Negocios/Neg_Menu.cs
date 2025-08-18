using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using Datos;
using System.Data.SqlClient;
namespace Negocios
{
    public class Neg_Menu
    {
        #region
        //MOFIFICACION HECHA POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        Dato_Menu Dato_Menu = new Dato_Menu();
        #endregion
        public List<MenuPerfil> MenuxPerfil(int perfil)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Menu datoM = new Dato_Menu();
            return datoM.MenuxPerfil(perfil, userDetail.getIDEmpresa());
        }

        public int ObtenerPerfilxUsuario(int idusuario)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Menu datom = new Dato_Menu();
            return datom.ObtenerPerfilxUsuario(idusuario, userDetail.getIDEmpresa());
        }

        public List<MenuPerfil> MenuObtenerItems()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Menu datoM = new Datos.Dato_Menu();
            return datoM.MenuObtenerItems(userDetail.getIDEmpresa());
        }

        public List<int> ObtenerItemsxPerfil(int IdPerfil)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Datos.Dato_Menu dm = new Datos.Dato_Menu();
            return dm.ObtenerItemsxPerfil(IdPerfil, userDetail.getIDEmpresa());
        }

        public  DataSet cargarEmpresas()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Menu.cargarEmpresas();
            return ds;
        }

        public  void ObtenerCadena(int idEmpresa)
        {
            Dato_Menu.ObtenerCadena(idEmpresa);
        }

    }
}
