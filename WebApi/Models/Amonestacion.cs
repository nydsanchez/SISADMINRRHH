using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Datos;


namespace WebApi.Models
{
    public class Amonestacion
    {
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


    }
}