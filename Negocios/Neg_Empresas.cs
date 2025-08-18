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
    public class Neg_Empresas
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Empresas Dato_Empresas = new Dato_Empresas();
        #endregion
        public dsPlanilla.dtEmpresaDataTable ObtenerInfoDetEmpresas()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
        
            return Dato_Empresas.ObtenerDetalleEmpresas(userDetail.getIDEmpresa());
        }

        public DataSet CargarTipoNomina()
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataSet ds = new DataSet();
            ds = Dato_Empresas.SeleccionarTipoNomina(userDetail.getIDEmpresa());
            return ds;
        }

        public bool EditarEmpresa(string nombEmpAnt, string nombEmpresa, int pais, string idioma,
           int tipoNomina, decimal salarMin, decimal PorcSEmple, decimal PorcSEmpresa, decimal PorcEdcEmp,
            decimal SalarMaxSS, decimal MaxSSEmp, decimal MaxSSEmpr, decimal MinS4Sempl, decimal MinS5Sem,
            decimal ValorS4Sem, decimal ValorS5Sem, decimal MinS4Sempresa, decimal MinS5Sempresa, decimal ValorSS4Semprs,
            decimal ValorSS5Semprs, decimal FactCamb, string GerRrhh, string user, bool pagaAntig, bool RedondearPago, bool PromVac,int moneda)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if(Dato_Empresas.EditarEmpresa(nombEmpAnt, nombEmpresa, pais, idioma,
            tipoNomina, salarMin, PorcSEmple, PorcSEmpresa, PorcEdcEmp,
            SalarMaxSS, MaxSSEmp, MaxSSEmpr, MinS4Sempl, MinS5Sem,
            ValorS4Sem, ValorS5Sem, MinS4Sempresa, MinS5Sempresa, ValorSS4Semprs,
            ValorSS5Semprs, FactCamb, GerRrhh, user, pagaAntig, RedondearPago, PromVac,moneda, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AgregarEmpresa(int idemp, string nombEmpresa, int pais, 
           int tipoNomina, decimal salarMin, string GerRrhh, string user,  bool PromVac,int moneda)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empresas.AgregarEmpresa(idemp,nombEmpresa, pais, 
            tipoNomina, salarMin, GerRrhh, user,  PromVac,moneda, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarEmpresa(string nombEmpresa)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            if (Dato_Empresas.EliminarEmpresa(nombEmpresa, userDetail.getIDEmpresa()))
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
