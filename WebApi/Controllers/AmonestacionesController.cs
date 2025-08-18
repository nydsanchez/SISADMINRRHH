using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Data;
using Negocios;

namespace WebApi.Controllers
{
    [Route("api/Amonestaciones")]
    public class AmonestacionesController : ApiController
    {

        Neg_Amonestaciones negA = new Neg_Amonestaciones();
        public List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> GetAmonestaciones(int year1, int month1, int days1, int year2, int month2, int days2, int userDetail)
        {


            DateTime fecha1 = new DateTime(year1, month1, days1);
            DateTime fecha2 = new DateTime(year2, month2, days2);

            fecha1 = fecha1.Date;
            fecha2 = fecha2.Date;
            DataTable dt = new DataTable("tabla");

            List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> LA = negA.getamonestacionesByRango(fecha1, fecha2, 0, false, true,userDetail);
            return LA;

        }
    }
}
