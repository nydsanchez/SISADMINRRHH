using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Datos;


namespace Negocios
{


    public class Neg_Amonestaciones
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016

        Datos_Amonestaciones Datos_Amonestaciones = new Datos_Amonestaciones();
        Dato_Empleados Dato_Empleados = new Dato_Empleados();

        #endregion

        public class Amonestaciones
        {
            
            private string nombrecompleto;

            public string Nombrecompleto
            {
                get { return nombrecompleto; }
                set { nombrecompleto = value; }
            }

            private int codigo_empleado;

            public int Codigo_empleado
            {
                get { return codigo_empleado; }
                set { codigo_empleado = value; }
            }

            private int cantidad;

            public int Cantidad
            {
                get { return cantidad; }
                set { cantidad = value; }
            }
            private int idParamPenalizacion;
            public int IdParamPenalizacion
            {
                get { return idParamPenalizacion; }
                set { idParamPenalizacion = value; }
            }
        }

        public class DetalleAmonestacion
        {

            private string nombreComplet;

            public string NombreComplet
            {
                get { return nombreComplet; }
                set { nombreComplet = value; }
            }
            private int codigoEmp;


            public int CodigoEmp
            {
                get { return codigoEmp; }
                set { codigoEmp = value; }
            }
            private string detalleFalta;

            public string DetalleFalta
            {
                get { return detalleFalta; }
                set { detalleFalta = value; }
            }
            private string codigo;

            public string Codigo
            {
                get { return codigo; }
                set { codigo = value; }
            }
            private DateTime fechaA;

            public DateTime FechaA
            {
                get { return fechaA; }
                set { fechaA = value; }
            }
            private string sancion;

            public string Sancion
            {
                get { return sancion; }
                set { sancion = value; }
            }
            private string observacion;

            public string Observacion
            {
                get { return observacion; }
                set { observacion = value; }
            }
            private DateTime fechagraba;

            public DateTime Fechagraba
            {
                get { return fechagraba; }
                set { fechagraba = value; }
            }
            private TimeSpan horagraba;

            public TimeSpan Horagraba
            {
                get { return horagraba; }
                set { horagraba = value; }
            }
            private string usuario;

            public string Usuario
            {
                get { return usuario; }
                set { usuario = value; }
            }

            private int idAmonestacion;

            public int IdAmonestacion
            {
                get { return idAmonestacion; }
                set { idAmonestacion = value; }
            }

            private int idSancion;

            public int IdSancion
            {
                get { return idSancion; }
                set { idSancion = value; }
            }

            private string nombreDepto;

            public string NombreDepto
            {
                get { return nombreDepto; }
                set { nombreDepto = value; }
            }
        }

        public DataTable ObtenerCatalogoAmonestaciones()
        {
           
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Datos_Amonestaciones.ObtenerCatalogoAmonestaciones(userDetail.getIDEmpresa());

            return ds;
        }

        public DataTable ObtenergrupoCatalogo(int grupo)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Datos_Amonestaciones.ObtenergrupoCatalogo(userDetail.getIDEmpresa(), grupo);

            return ds;
        }



        public List<Amonestaciones> getamonestaciones(DateTime fechaini, DateTime fechafin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Datos_Amonestaciones.getamonestaciones(userDetail.getIDEmpresa(), fechaini, fechafin);

            List<Amonestaciones> li = new List<Amonestaciones>();
            foreach (DataRow row in ds.Rows)
            {
                Amonestaciones i = new Amonestaciones();
                i.Nombrecompleto = row["nombrecompleto"].ToString();
                i.Codigo_empleado = int.Parse(row["codigo_empleado"].ToString());
                i.Cantidad = int.Parse(row["total"].ToString());
                i.IdParamPenalizacion = int.Parse(row["idparampenalizacion"].ToString());
                li.Add(i);

            }
            return li;
        }
        //getAmonestacionesAplicarInc

        public List<Amonestaciones> getAmonestacionesAplicarInc(DateTime fechaini, DateTime fechafin)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Datos_Amonestaciones.getAmonestacionesAplicarInc(userDetail.getIDEmpresa(), fechaini, fechafin);

            List<Amonestaciones> li = new List<Amonestaciones>();
            foreach (DataRow row in ds.Rows)
            {
                Amonestaciones i = new Amonestaciones();
                i.Nombrecompleto = row["nombrecompleto"].ToString();
                i.Codigo_empleado = int.Parse(row["codigo_empleado"].ToString());
                i.Cantidad = int.Parse(row["total"].ToString());
                i.IdParamPenalizacion = int.Parse(row["idparampenalizacion"].ToString());
                li.Add(i);

            }
            return li;
        }
        public List<DetalleAmonestacion> getamonestacionesByRango(DateTime fechaini, DateTime fechafin, int codigoE, bool bycodigo, bool byrango, int userDetail)
        {
            //IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            ds = Datos_Amonestaciones.getamonestacionesByRango(userDetail, fechaini, fechafin, codigoE, bycodigo, byrango);

            List<DetalleAmonestacion> li = new List<DetalleAmonestacion>();
            foreach (DataRow row in ds.Rows)
            {
                DetalleAmonestacion i = new DetalleAmonestacion();
                i.NombreComplet = row["nombrecompleto"].ToString();
                i.CodigoEmp = int.Parse(row["codigo_empleado"].ToString());
                i.DetalleFalta = row["DetalleFalta"].ToString();
                i.Codigo =row["Codigo"].ToString();
                i.FechaA = Convert.ToDateTime(row["fechaAmonestacion"].ToString());
                i.Sancion = row["Sancion"].ToString();
                i.Observacion = row["observacion"].ToString();
                i.Fechagraba = Convert.ToDateTime(row["fechaGraba"].ToString());
                i.Horagraba = TimeSpan.Parse(row["horaGraba"].ToString());
                i.Usuario = row["usuario"].ToString();
                i.IdAmonestacion = Convert.ToInt32(row["idAmonestacion"].ToString());
                i.IdSancion = Convert.ToInt32(row["idSancion"].ToString());
                i.NombreDepto = row["nombre_depto"].ToString();
                li.Add(i);

            }
            return li;
        }

        public bool InsertarAmonestaciones(int codigoEmpleado, int idAmonestacion, string sancion, DateTime fechaAMonestacion, string Observacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.InsertarAmonestaciones(userDetail.getIDEmpresa(), codigoEmpleado, idAmonestacion, sancion, fechaAMonestacion, userDetail.getUser(), Observacion))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool AmonestacionesDelete(int codigoEmpleado, int idAmonestacion, string sancion, DateTime fechaAMonestacion, DateTime fechaReg, TimeSpan horaR)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.AmonestacionesDelete(userDetail.getIDEmpresa(), codigoEmpleado, idAmonestacion, sancion, fechaAMonestacion, fechaReg, horaR, userDetail.getUser()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool AmonestacionesLOGInsertar(int codigoEmpleado, int idAmonestacion, int idAmonestacionnew, string sancion, string sancionnew, DateTime fechaAMonestacion, DateTime fechaAMonestacionnew, string Observacion, string accion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.AmonestacionesLOGInsertar(userDetail.getIDEmpresa(), codigoEmpleado, idAmonestacion, idAmonestacionnew, sancion, sancionnew, fechaAMonestacion, fechaAMonestacionnew, userDetail.getUser(), Observacion, accion))
            {
                return true;
            }
            else
            {
                return false;
            }


        }


        public bool UpdateAmonestaciones(int codigoEmpleado, int idAmonestacion, int idAmonestacionnew, string sancion, string sancionnew, DateTime fechaAMonestacion, DateTime fechaAMonestacionnew, DateTime fecharegistro, TimeSpan horaregistro, string Observacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.UpdateAmonestaciones(userDetail.getIDEmpresa(), codigoEmpleado, idAmonestacion, idAmonestacionnew, sancion, sancionnew, fechaAMonestacion, fechaAMonestacionnew, fecharegistro, horaregistro, userDetail.getUser(), Observacion))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool CatalogoAmonestacionesInsertar(string DetalleF, string codigF, string nivelF, string sancion)
        {

            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.CatalogoAmonestacionesInsertar(userDetail.getIDEmpresa(),DetalleF,codigF,nivelF,sancion))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool CatalogoAmonestacionesUpdate(string DetalleF,int IdA, string codigF, string nivelF, string sancion)
        {

            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.CatalogoAmonestacionesUpdate(userDetail.getIDEmpresa(),IdA, DetalleF, codigF, nivelF, sancion))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CatalogoInsert(int IdG, string Descripcion)
        {

            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.CatalogoInsert(userDetail.getIDEmpresa(), IdG, Descripcion))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool updategrupoCatalogo(int IdG, string Descripcion,int iddes)
        {

            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Amonestaciones i = new Amonestaciones();
            if (Datos_Amonestaciones.updategrupoCatalogo(userDetail.getIDEmpresa(), IdG, Descripcion, iddes))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
