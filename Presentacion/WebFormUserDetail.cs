using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using Negocios;

namespace NominaRRHH.Presentacion
{
    public class WebFormUserDetail : IUserDetail
    {        

        public static void storeUserDetail(string user, int idEmpresa,int user_codempleado)
        {
            HttpContext.Current.Session["usuario"] = user;
            HttpContext.Current.Session["idEmpresa"] = idEmpresa;
            HttpContext.Current.Session["user_codempleado"] = user_codempleado;
        }

        public string getUser()
        {
            return (string) HttpContext.Current.Session["usuario"];
        }

        public int getIDEmpresa()
        {
            return Convert.ToInt32(HttpContext.Current.Session["idEmpresa"]); 
        }

        public int getUserCodEmpleado()
        {
            return Convert.ToInt32(HttpContext.Current.Session["user_codempleado"]);
        }
    }
}
