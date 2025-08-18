using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Negocios;


namespace WebApi.Controllers
{
    [RoutePrefix("api/Departamento")]
    public class UnidadOrganizativaController : ApiController
    {
        // GET: UnidadOrganizativa
        public string GetCambiarDepartamentoaEmpleado(int codigo_empleado, int codigo_departamento)
        {
            return "Ok";
        }
        
    }
}