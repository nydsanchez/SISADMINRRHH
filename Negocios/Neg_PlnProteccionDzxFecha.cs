using System;
using System.Data;
using Datos;

namespace Negocios
{
    /// <summary>
    /// Protege docenas de personas que perdieron horas laborales.
    /// </summary>
    public class Neg_PlnProteccionDzxFecha
    {
        public DataTable Select(DateTime fechaini, DateTime fechafin, int idEmpresa)
        {
            return new Dato_PlnProteccionDzxFecha().Select(fechaini, fechafin, idEmpresa);
        }

        public string Insert(DateTime fechaini, DateTime fechafin,int codigo_empleado, int idEmpresa)
        {
            return new Dato_PlnProteccionDzxFecha().Insert(fechaini, fechafin,codigo_empleado, idEmpresa);
        }
    }
}
