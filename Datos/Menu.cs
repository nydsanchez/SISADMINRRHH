using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Datos
{
    public class Menu
    {

    }
    public class MenuPerfil
    {
        private int _idMenu;

        public int IdMenu
        {
            get { return _idMenu; }
            set { _idMenu = value; }
        }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private string _formulario;

        public string Formulario
        {
            get { return _formulario; }
            set { _formulario = value; }
        }

        private int _padre;

        public int Padre
        {
            get { return _padre; }
            set { _padre = value; }
        }


        private int _orden;

        public int Orden
        {
            get { return _orden; }
            set { _orden = value; }
        }

        public MenuPerfil(int idMenu, string Nombre, string descripcion, int Padre, string formulario, int orden)
        {
            this.IdMenu = idMenu;
            this.Nombre = Nombre;
            this.Padre = Padre;
            this.Formulario = formulario;
            this.Orden = orden;
            this.Descripcion = descripcion;
        }
    }
}
