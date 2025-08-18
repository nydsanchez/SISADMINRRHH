using Datos;

namespace Negocios
{
    /// <summary>
    /// Protege docenas de personas que perdieron horas laborales.
    /// </summary>
    public class Neg_Cooperativa
    {
        //public DataTable SelectAhorro(int codigo, int idEmpresa)
        //{
        //    return new Neg_DevYDed().DeshabilitarDeuda(codigo);
        //}

        public string LimpiarSaldo(int codigo_empleado)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();            
            return new Dato_Cooperativa().LimpiarSaldo(codigo_empleado, userDetail.getIDEmpresa());
        }
    }
}
