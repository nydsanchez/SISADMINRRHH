using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Negocios
{
    public class Globales
    {
        private decimal inssLab;

        public decimal InssLab
        {
            get { return inssLab; }
            set { inssLab = value; }
        }
        private decimal inssPatron;

        public decimal InssPatron
        {
            get { return inssPatron; }
            set { inssPatron = value; }
        }
        private decimal ir;

        public decimal Ir
        {
            get { return ir; }
            set { ir = value; }
        }
        private decimal hPend;

        public decimal HPend
        {
            get { return hPend; }
            set { hPend = value; }
        }
        private decimal horas;

        public decimal Horas
        {
            get { return horas; }
            set { horas = value; }
        }
        private DateTime fechaR;

        public DateTime FechaR
        {
            get { return fechaR; }
            set { fechaR = value; }
        }
    }
}
