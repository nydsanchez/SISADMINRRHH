using System;
using System.Web.Http;
using Negocios;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Eficiencia")]
    public class EficienciaController : ApiController
    {
        public string GetEficienciaxModulo(int anoi, int mesi, int diai, int anof, int mesf, int diaf,int idempresa)
        {
            Neg_Eficiencia x = new Neg_Eficiencia();
            DateTime fechainicio = new DateTime(anoi, mesi, diai);
            DateTime fechafin = new DateTime(anof, mesf, diaf);
            string respuestaHtml = new Neg_Eficiencia().EficienciaPorModuloHTML(fechainicio, fechafin,idempresa);
            return respuestaHtml;
        }
    }
}