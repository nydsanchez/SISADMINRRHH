using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocios
{
    public class Neg_Jerarquia
    {
        Datos.Dato_Jerarquia J;

        public Neg_Jerarquia()
        {
            J = new Datos.Dato_Jerarquia();
        }

        public System.Data.DataTable ObtenerDeptoSubordinados(string codigo_empleado,int idempresa)
        {
            System.Data.DataTable dt = J.ObtenerDeptoSubordinados(codigo_empleado,idempresa);
            dt.TableName = "Departamento";
            return dt;
        }
        
        public System.Data.DataTable ObtenerSubordinados(string codigo_empleado, int idempresa)
        {
            System.Data.DataTable dt = J.ObtenerSubordinados(codigo_empleado, idempresa);
            dt.TableName = "Empleado";
            return dt;
        }

        public System.Data.DataTable ObtenerSubordinadosPorDepto(int iddepartamento, int idempresa)
        {
            System.Data.DataTable dt = J.ObtenerSubordinadosPorDepto(iddepartamento, idempresa);
            dt.TableName = "Empleado";
            return dt;
        }
    }
}
