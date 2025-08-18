using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocios;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Marcas")]
    public class MarcasController : ApiController
    {
        // GET: api/Marcas/5
        public string GetReporteMarcasPrueba(int uno)
        {
            Neg_Marca x = new Neg_Marca();
            DateTime fechainicio = new DateTime(2018, 10, 15);
            DateTime fechafin = new DateTime(2018, 10, 21);

            return x.ObtenerReportedeMarcas(fechainicio, fechafin, 5, 3, 1, 150);
        }

        public string GetMarcasdeBioadmin(int year, int month, int day, int idempresa)
        {
            Neg_Marca _Marca = new Neg_Marca();
            DateTime fecha = new DateTime(year, month, day);
            _Marca.spObtenerMarcas(fecha,idempresa);
            return "Ok";
        }

        public string GetReporteMarcas(int ano1, int mes1, int dia1, int ano2, int mes2, int dia2, int tipo, int ubicacion)
        {
            Neg_Marca x = new Neg_Marca();
            DateTime fechainicio = new DateTime(ano1, mes1, dia1);
            DateTime fechafin = new DateTime(ano2, mes2, dia2);
            x.ObtenerReportedeMarcas(fechainicio, fechafin, tipo, ubicacion);
            return "ok";
        }

        public string GetReporteMarcas(int ano1, int mes1, int dia1, int ano2, int mes2, int dia2, int tipo, int ubicacion, int iddepartamento)
        {
            Neg_Marca x = new Neg_Marca();
            DateTime fechainicio = new DateTime(ano1, mes1, dia1);
            DateTime fechafin = new DateTime(ano2, mes2, dia2);
            return x.ObtenerReportedeMarcas(fechainicio, fechafin,tipo, ubicacion ,iddepartamento, iddepartamento);
        }

        public string GetReporteMarcasRango(int ano1, int mes1, int dia1, int ano2, int mes2, int dia2, int tipo, int ubicacion, int iddepartamentoini, int iddepartamentofin)
        {
            Neg_Marca x = new Neg_Marca();
            DateTime fechainicio = new DateTime(ano1, mes1, dia1);
            DateTime fechafin = new DateTime(ano2, mes2, dia2);

            return x.ObtenerReportedeMarcas(fechainicio, fechafin, tipo, ubicacion, iddepartamentoini, iddepartamentofin);
        }

        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Marcas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Marcas/5
        public void Delete(int id)
        {
        }
    }
}
