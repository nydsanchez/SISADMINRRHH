using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Datos
{
    public class UsuarioSession
    {
        private int _IdUsuario;
        private string _Usuarios;
        private string _pass;
        private string _nombre;
        private string _apellido;
        private Boolean _activo;
        private int _Codigo_Empleado;

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string Usuarios
        {
            get { return _Usuarios; }
            set { _Usuarios = value; }
        }
        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        public Boolean Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        public int Codigo_Empleado
        {
            get { return _Codigo_Empleado; }
            set { _Codigo_Empleado = value; }
        }

        public UsuarioSession(int IdUsuario, string Usuarios, string Pass, Boolean Activo, string Nombre, string Apellido,int Codigo_Empleado)
        {
            this.Usuarios = Usuarios;
            this.Pass = Pass;
            this.IdUsuario = IdUsuario;
            this.Codigo_Empleado = Codigo_Empleado;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
        }
    }

    public class UsuarioSessionD
    {
        private int _IdUsuario;
        private string _Usuarios;
        private string _pass;
        private string _nombre;
        private string _apellido;
        private Boolean _activo;

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string Usuarios
        {
            get { return _Usuarios; }
            set { _Usuarios = value; }
        }
        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        public Boolean Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
    }

    public class Usuario
    {

        private int _IdUsuario;
        private string _Usuarios;
        private string _pass;
        private string _nombre;
        private string _apellido;
        private Boolean _activo;

        public int IdUsuario
        {
            get { return _IdUsuario; }
            set { _IdUsuario = value; }
        }
        public string Usuarios
        {
            get { return _Usuarios; }
            set { _Usuarios = value; }
        }
        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }
        public Boolean Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        public Usuario(string Usuarios, string Pass)
        {
            this.Usuarios = Usuarios;
            this.Pass = Pass;
        }
    }

}
