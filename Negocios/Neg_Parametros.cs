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
    public class Neg_Parametros
    {
      
        #region REFERENCIAS
        Dato_Parametros Dato_Parametros = new Dato_Parametros();
        IUserDetail userDetail = UserDetailResolver.getUserDetail();
        public Dictionary<string, int> diccionario = new Dictionary<string, int>();
        #endregion

        

        public DataTable ParametrosSelect(int idgrupo)
        {
            DataTable ds = new DataTable();
            ds = Dato_Parametros.ParametrosSelect(userDetail.getIDEmpresa(), idgrupo);
            return ds;
        }

        public bool ParametrosInsert(int idgrupo, string nombre,decimal valor, string descripcion)
        {
            Boolean hecho;
            hecho = Dato_Parametros.ParametrosInsert(userDetail.getIDEmpresa(), idgrupo, nombre, valor,descripcion);
            return hecho;
        }
        public bool ParametrosUpdate(int id, int idgrupo, string nombre,decimal valor, string descripcion)
        {
            Boolean hecho;
            hecho = Dato_Parametros.ParametrosUpdate(userDetail.getIDEmpresa(), id, idgrupo, nombre,valor, descripcion);
            return hecho;
        }

        public bool ParametrosDelete(int id, int idgrupo)
        {
            Boolean hecho;
            hecho = Dato_Parametros.ParametrosDelete(userDetail.getIDEmpresa(), id, idgrupo);
            return hecho;
        }

        public DataTable ParametrosGroupSelect()
        {
            DataTable ds = new DataTable();
            ds = Dato_Parametros.ParametrosGroupSelect(userDetail.getIDEmpresa());
            return ds;
        }

        public bool ParametrosGrupoInsert(string nombre, string descripcion)
        {
            Boolean hecho;
            hecho = Dato_Parametros.ParametrosGrupoInsert(userDetail.getIDEmpresa(), nombre, descripcion);
            return hecho;
        }

        public bool ParametrosGrupoUpdate(int id, string nombre, string descripcion)
        {
            Boolean hecho;
            hecho = Dato_Parametros.ParametrosGrupoUpdate(userDetail.getIDEmpresa(), id, nombre, descripcion);
            return hecho;
        }

        public bool ParametrosGrupoDelete(int id)
        {
            Boolean hecho;
            hecho = Dato_Parametros.ParametrosGrupoDelete(userDetail.getIDEmpresa(), id);
            return hecho;

        }
        public void ParametrosGrupoObtenerGruposyParametros()
        {
            Dato_Parametros.ParametrosGrupoObtenerGruposyParametros(userDetail.getIDEmpresa());
        }

        public Dictionary<string, int> ParametrosGrupoObtenerGrupos()
        {
           
            Dictionary<string, int> g = Dato_Parametros.ParametrosGrupoObtenerGrupos();
            return g;
        }
        public Dictionary<string, int> ObtenerParametrosxGrupo(int idgrupo)
        {
            return  Dato_Parametros.ObtenerParametrosxGrupo(idgrupo);
        }

        public decimal obtenerValorParametro(int parametro)
        {
            return Dato_Parametros.obtenerValorParametro(parametro);
        }

        public int obtenerIdXNombre( Dictionary<string, int> dicionario, string nombre)
        {
            return (from d in dicionario
                    where d.Key.ToUpper().Contains(nombre.ToUpper())
                               select d.Value).SingleOrDefault();
        }
       
    }
}
