using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;
using System.Reflection;
using Datos;


namespace Negocios
{
    public class Neg_Comedor : System.Web.UI.Page
    {
        #region
           
        Dato_Comedor Dato_Comedor = new Dato_Comedor();
        Neg_DevYDed NDevyDed = new Neg_DevYDed();
        #endregion
        public decimal CreditoPeriodoSel(string codigo, string fecini, string fecfin)
        {
            Datos.Dato_Comedor datoI = new Datos.Dato_Comedor();
            return datoI.CreditoPeriodoSel(codigo,fecini, fecfin);
        }
        public DataTable spCreditoSemanaSel(string codigo)
        {
            Dato_Comedor datoI = new Dato_Comedor();
            return datoI.spCreditoSemanaSel(codigo);
        }
        public bool ProcesarDeduccionComedorxPeriodo(int periodo, int semana, string fini, string ffin, string user)
        {
            try
            {
                DataTable dtc = new DataTable();
                dtc = Dato_Comedor.CreditoEmpresaSel(fini, ffin);

                if (dtc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtc.Rows)
                    {
                        //validar si es codigo empleado numerico
                        int codigo_empleado = 0;
                        if (int.TryParse(dr["codempleado"].ToString(), out codigo_empleado))
                        {
                            if (!NDevyDed.InsertarIngrDeduc(2, codigo_empleado, semana, 4, periodo, Convert.ToDecimal(dr["Total"]), user))
                            {
                                throw new Exception("Error al insertar ingreso");
                            }
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }




}
