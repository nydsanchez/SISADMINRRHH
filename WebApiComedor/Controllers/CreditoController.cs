using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using Negocios;
using System;
using Datos;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Script.Serialization;

namespace WebApiComedor.Controllers
{
    [RoutePrefix("api/Credito")]
    public class CreditoController : ApiController
    {
       
        // GET api/Credito/GetSolvenciaEconomica?codigo=866328
        [HttpGet]
        [Route("SolvenciaEconomica")]       
        public string GetSolvenciaEconomica(int codigo)
        {
            Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();

            return JsonConvert.SerializeObject(Neg_Liquidacion.ObtenerSolvenciaEconomica(codigo), Formatting.None);
        }
      
        [Route("IngresosxPlanilla")]
        public string GetIngresosxPlanilla(int codigo)
        {
            Dato_Liquidacion Dato_Liquidacion = new Dato_Liquidacion();

            return JsonConvert.SerializeObject(Dato_Liquidacion.ObtenerIngresosxPlanillas(codigo,1), Formatting.None);
        }
       
        [Route("DeduccionesEmp")]
        public string GetDeduccionesEmp(int codigo,decimal ingreso)
        {
            Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
            // vhpo-fecha-revisar
            return JsonConvert.SerializeObject(Neg_DevYDed.CalcularDetalleDeduccionesxEmp(codigo, 0, 0, ingreso, DateTime.Now , 1), Formatting.None);
        }
        [Route("PeriodoActual")]
        public string GetPeriodoActual()//string parametro)
        {
            Dato_Periodo Dato_Periodo = new Dato_Periodo();
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            ////aqui obtener tipo planilla de la empresa
            //Dato_Catalogos Dato_Catalogos = new Dato_Catalogos();
            //Dato_Empleados dato_Empleados = new Dato_Empleados();
            //DataTable DetEmpleados = dato_Empleados.ObtenerDetalleEmpleados(Convert.ToInt32(codigo),idempresa);
            //int cod_ubicacion = Convert.ToInt32(DetEmpleados.Rows[0]["codigo_ubicacion"]);
            //DataTable ubicacion = Dato_Catalogos.seleccionarUbicacionesxCod(cod_ubicacion, idempresa);
            //int tplanilla = Convert.ToInt32(ubicacion.Rows[0]["tplanilla"]);
    
            /////
            DataTable periodo = Dato_Periodo.SeleccionarPeriodoCat(1, 4, 3, 1);
            DataTable periodocerrado = Dato_Periodo.SeleccionarPeriodoCerrado(1, 4, 3, 1);
            DateTime pini = new DateTime();
            DateTime pfin = new DateTime();
            DateTime fini = new DateTime();
            DateTime ffin = new DateTime();
            DataTable result = new DataTable();
            result.Columns.Add("fechaini");
            result.Columns.Add("fechafin");

            //parametro = jsSerializer.DeserializeObject(parametro).ToString();

            //if (parametro == "")//(periodo.Rows.Count == 0 && periodocerrado.Rows.Count == 0)//no hay periodos registrados
            //{
            //    result.Rows.Add("", "");
            //    return JsonConvert.SerializeObject(result, Formatting.None);
            //}

            DateTime fechaactual = DateTime.Now;//string.IsNullOrEmpty(parametro) ? DateTime.Now : Convert.ToDateTime(parametro);
            if (periodo.Rows.Count==0 && periodocerrado.Rows.Count > 0)//no se ha creado el proximo periodo y solo se conoce el ultimo cerrado
            {
                pini = Convert.ToDateTime(periodocerrado.Rows[0]["fechafin2"]).AddDays(1);
                pfin = Convert.ToDateTime(periodocerrado.Rows[0]["fechafin2"]).AddDays(14);

            } else if (periodo.Rows.Count>0)//si esta abierto periodo
            {
                pini = Convert.ToDateTime(periodo.Rows[0]["fechaini"]);
                pfin = Convert.ToDateTime(periodo.Rows[0]["fechafin2"]);
            }
            double diasp = (pfin - pini).Days + 1;
            if (fechaactual < pini)
            {
                fini = pini.AddDays(diasp * -1);
                ffin = pini.AddDays(-1);
            }else if(fechaactual >= pini && fechaactual <= pfin)
            {
                fini = pini;
                ffin = pfin;
            }else if (fechaactual > pfin)
            {
                fini = pfin.AddDays(1);
                ffin = pfin.AddDays(diasp);
            }
            result.Rows.Add(fini,ffin);

            return JsonConvert.SerializeObject(result, Formatting.None);
        }
        //public string GetSolvenciaEconomica(int codigo)
        //{
        //    Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();
        //    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

        //    return jsSerializer.Serialize(Neg_Liquidacion.ObtenerSolvenciaEconomica(codigo));//GenerarTabla(Neg_Liquidacion.ObtenerSolvenciaEconomica(codigo));
        //}
        [Route("MontoDisponible")]
        public string GetMontoDisponible(string codigo)
        {
            Dato_Liquidacion Dato_Liquidacion = new Dato_Liquidacion();
            Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
            Neg_Liquidacion Neg_Liquidacion = new Neg_Liquidacion();

            //DataTable dtIngresos = Dato_Liquidacion.ObtenerIngresosxPlanillas(codigo, 1);
            //decimal ingresoPromedio = 0, salproteccion = 0, disponible = 0, deducfijo = 0, deducporc = 0;

            //if (dtIngresos.Rows.Count > 0)//personal que todavia no tiene historico de planillas se les pone credito por defecto
            //{
            //    DataRow[] ingresocod = dtIngresos.Select("codigo_empleado=" + codigo);
            //    ingresoPromedio = Convert.ToDecimal(ingresocod.Sum(row => Convert.ToDecimal(row["ingresodif"]))) / ingresocod.Count();
            //    salproteccion = ingresoPromedio - (ingresoPromedio * 0.25M);//proteccion 

            //    DataTable dtDeducciones = Neg_DevYDed.CalcularDetalleDeduccionesxEmp(codigo, 0, 0, salproteccion, 1);
            //    if (dtDeducciones.Rows.Count > 0)
            //    {
            //        //deducfijo = Convert.ToDecimal(dtDeducciones.Select("porcentual='NO'").Sum(row => Convert.ToDecimal(row["valorCuotas"])));
            //        //deducporc = (Convert.ToDecimal(dtDeducciones.Select("porcentual='SI'").Sum(row => Convert.ToDecimal(row["valorCuotas"]))) * salproteccion) / 100;

            //        ///aqui
            //        ///
            //        //deducfijo = dtDeducciones.AsEnumerable().Where(r => r.Field<bool>("porcentual") == false && r.Field<decimal>("valorCuotas") <= r.Field<decimal>("debe")).Sum(r => r.Field<decimal>("valorCuotas"));
            //        deducfijo = Convert.ToDecimal(dtDeducciones.Select("recurrente='SI' or (porcentual='NO' and debe > 0 and valorcuota <= debe)").Sum(row => Convert.ToDecimal(row["valorCuotas"])));
            //        deducfijo += Convert.ToDecimal(dtDeducciones.Select("recurrente='NO' and porcentual='NO' and debe > 0 and valorcuota > debe)").Sum(row => Convert.ToDecimal(row["debe"])));
            //        deducporc = (Convert.ToDecimal(dtDeducciones.Select("recurrente='SI' or (porcentual='SI' and debe > 0 and valorcuota <= debe)").Sum(row => Convert.ToDecimal(row["valorCuotas"]))) * salproteccion) / 100;
            //        deducporc += Convert.ToDecimal(dtDeducciones.Select("recurrente='NO' and porcentual='SI' and debe > 0 and valorcuota > debe)").Sum(row => Convert.ToDecimal(row["debe"])));
            //        //deducfijo += dtDeducciones.AsEnumerable().Where(r => r.Field<bool>("porcentual") == false && r.Field<decimal>("valorCuotas") <= r.Field<decimal>("debe")).Sum(r => r.Field<decimal>("valorCuotas"));

            //    }
            //    disponible = (salproteccion - deducfijo - deducporc);//(((salproteccion - deducfijo - deducporc) - (dtCredito - consumo))/14)*9;
            //}
            //else//aquellos sin ingresos se les pone por defecto
            //{
            //    disponible = 2000;//por catorcena
            //}

            return JsonConvert.SerializeObject(Neg_Liquidacion.GetMontoDisponible(codigo,1), Formatting.None);
        }

        public List<Dictionary<string, object>> DataTableToJSON(DataTable table)
        {
           
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;

            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return parentRow;
        }
        
    }
}