using System.Data;
using System.Web.Http;
using Negocios;

namespace WebApi.Controllers
{
    [RoutePrefix("api/Jerarquia")]
    public class JerarquiaController : ApiController
    {
        public DataTable GetObtenerDepartamentosSubordinados(string idempleado, int idempresa, int iddepartamento)
        {
            Neg_Jerarquia x = new Neg_Jerarquia();
            return x.ObtenerDeptoSubordinados(idempleado,idempresa);
        }

        //JerarquiaObtenerSubordinadosPorDepto
        public DataTable GetObtenerSubordinados(string idempleado, int idempresa)
        {
            Neg_Jerarquia x = new Neg_Jerarquia();
            return x.ObtenerSubordinados(idempleado, idempresa);            
        }

        public DataTable GetObtenerSubordinadosporDepto(int iddepartamento, int idempresa)
        {
            Neg_Jerarquia x = new Neg_Jerarquia();
            return x.ObtenerSubordinadosPorDepto(iddepartamento, idempresa);
        }

    }
}
