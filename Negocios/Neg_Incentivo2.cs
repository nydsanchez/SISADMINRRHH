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
    public class Neg_Incentivo2
    {
        
        public string Codigo;
        public int Periodo;

        public int Semana;

        public string Nombrecompleto;

        public int Modulo;

        public int Estilo;

        public string Contruccion;
        public decimal Produccion;

        public decimal Metaalcanzada;

        public decimal Eficienciaalcanzada;

        public decimal IncentivoMeta;

        public decimal TotalIngreso;

        public decimal TotalEgreso;

        public decimal TotalIncentivo;

        public string UsuarioGraba;

        public DateTime Fechagraba;
        public TimeSpan HoraG;
        public dsPlanilla.dtIngresosDeduccIncentivosDataTable dtIDInce = new dsPlanilla.dtIngresosDeduccIncentivosDataTable();




        public List<Neg_Incentivo2> IncentivoHistoricoDT(int periodo, int semana, DataTable dt)
        {
            Datos.Dato_Incentivos datoI = new Datos.Dato_Incentivos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();

            DataTable dtID = new DataTable();
            //  DataTable dt = datoI.IncentivoHistoricoSelect(userDetail.getIDEmpresa(), periodo, semana);
            List<Neg_Incentivo2> lista = new List<Neg_Incentivo2>();
            foreach (DataRow row in dt.Rows)
            {
                Neg_Incentivo2 objeto = new Neg_Incentivo2();
                objeto.Codigo = row["codigo_empleado"].ToString();
                objeto.Periodo = int.Parse(row["periodo"].ToString());
                objeto.Semana = int.Parse(row["semana"].ToString());
                objeto.Nombrecompleto = row["nombrecompleto"].ToString();
                objeto.Modulo = int.Parse(row["Modulo"].ToString());
                objeto.Estilo = int.Parse(row["Estilo"].ToString());
                objeto.Contruccion = row["Construccion"].ToString();
                objeto.Produccion = decimal.Parse(row["Produccion"].ToString());
                objeto.Metaalcanzada = decimal.Parse(row["metaAlcanzada"].ToString());
                objeto.Eficienciaalcanzada = decimal.Parse(row["EficienciaAlcanzada"].ToString());
                objeto.IncentivoMeta = decimal.Parse(row["IncentivoMeta"].ToString());
                objeto.TotalEgreso = decimal.Parse(row["TotalIngreso"].ToString());
                objeto.TotalEgreso = decimal.Parse(row["TotalDeducciones"].ToString());
                objeto.TotalIncentivo = decimal.Parse(row["TotalIncentivo"].ToString());
                objeto.UsuarioGraba = row["UsuarioGraba"].ToString();
                objeto.Fechagraba = DateTime.Parse(row["Fechagraba"].ToString());
                objeto.HoraG = TimeSpan.Parse(row["HoraGraba"].ToString());

                dtID = datoI.IncentivoIngDedccLOGSelect(userDetail.getIDEmpresa(), periodo, semana, int.Parse(objeto.Codigo));
                if (dtID != null)
                {
                    if (dtID.Rows.Count > 0)
                    {
                         //dsPlanilla.dtHorasTRow HT = NEmpleado.dtHorasT.NewdtHorasTRow();
                        dsPlanilla.dtIngresosDeduccIncentivosRow IG = objeto.dtIDInce.NewdtIngresosDeduccIncentivosRow();
                    }
                }
                lista.Add(objeto);
            }
            return lista;
        }

    }
}
