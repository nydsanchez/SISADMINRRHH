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
    public class Neg_Feriados
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Feriados Dato_Feriados = new Dato_Feriados();
        #endregion
        public dsPlanilla.dtFeriadosDataTable diasFeriados(DateTime fecini, DateTime fecfin, int userDetail)
        {
            //IUserDetail userDetail = UserDetailResolver.getUserDetail();
            
            return Dato_Feriados.SeleccionarDiasFeriados(fecini, fecfin, userDetail);
        }


        public bool AgregardiasFeriados(DateTime fecha, string descripcion, decimal cantidadDias)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Feriados.InsertardiasFeriados(fecha, descripcion, cantidadDias, userDetail.getIDEmpresa());

            try
            {

            }
            catch (SystemException)
            {

                return false;
            }
            return true;
        }

        public bool EditarFeriados(DateTime fecha, string descripcion, decimal cantidadDias, DateTime nfecha, string ndesc, decimal ncant)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Feriados.EditardiasFeriados(fecha, descripcion, cantidadDias, nfecha, ndesc, ncant, userDetail.getIDEmpresa());

            try
            {

            }
            catch (SystemException)
            {

                return false;
            }
            return true;
        }

        public bool EliminarFeriados(DateTime nfecha, string ndesc, decimal ncant)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Feriados.EliminardiasFeriados(nfecha, ndesc, ncant, userDetail.getIDEmpresa());

            try
            {

            }
            catch (SystemException)
            {

                return false;
            }
            return true;
        }
    }
}
