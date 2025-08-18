using System;
using System.Data;
using System.Web.Http;
using Negocios;
using Datos;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Inss")]
    public class InssController : ApiController
    {
        // GET: Inns
        public string GetInss(int year1, int month1, int days1, int year2, int month2, int days2,string param)
        {
            Neg_INSS NegInns = new Negocios.Neg_INSS();
            DateTime fechainicio;
            DateTime fechafin;

            fechainicio = new DateTime(year1, month1, days1);
            fechafin = new DateTime(year2, month2, days2);

            dsInss.dtInssDataTable dt = NegInns.InssCatorcenal_DataTable(fechainicio, fechafin);

            if (param == "html")
            return NegInns.InnsCatorcenal_HTML(dt);

            if (param == "xml")
                return NegInns.InnsCatorcenal_XML(dt);

            if (param == "text")
                return NegInns.InnsCatorcenal_Text(dt);

            return "error";
        }

        public string GetInss(int year1, int month1, int days1, int year2, int month2, int days2)
        {
            Neg_INSS NegInns = new Negocios.Neg_INSS();
            DateTime fechainicio;
            DateTime fechafin;

            fechainicio = new DateTime(year1, month1, days1);
            fechafin = new DateTime(year2, month2, days2);

            dsInss.dtInssDataTable dt = NegInns.InssCatorcenal_DataTable(fechainicio, fechafin);

            return NegInns.InnsCatorcenal_All(dt);
        }
    }
}