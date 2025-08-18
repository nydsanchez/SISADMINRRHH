using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Datos;

namespace Negocios
{
    public class Neg_Retenciones
    {
        #region Referencias
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Factores Dato_Factores = new Dato_Factores();
        Dato_Retenciones Dato_Retenciones = new Dato_Retenciones();
        #endregion
        public DataTable retenciones()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Dato_Factores.SeleccionarRetencionesIR(userDetail.getIDEmpresa());
            return ds;
        }

        public bool EditarRetenciones(int idRenta, decimal desde, decimal hasta, decimal impBase, decimal porcentj, decimal sobExc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Retenciones.EditarRetenciones(idRenta, desde, hasta, impBase, porcentj, sobExc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

          
        }

        public bool AgregarRetenciones(decimal desde, decimal hasta, decimal impBase, decimal porcentj, decimal sobExc)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Retenciones.InsertarRetenciones(desde, hasta, impBase, porcentj, sobExc, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public bool EliminarRetenciones(int idRenta)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Retenciones.EliminarRetenciones(idRenta, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
