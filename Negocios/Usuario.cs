using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaEntidad
{
    public class UsuarioSession
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

        public UsuarioSession(int IdUsuario, string Usuarios, string Pass, Boolean Activo, string Nombre, string Apellido)
        {
            this.Usuarios = Usuarios;
            this.Pass = Pass;
            this.IdUsuario = IdUsuario;
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
