using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Text.RegularExpressions;
using System.Data;
using Negocios;

using Microsoft.Reporting.WebForms;

namespace NominaRRHH
{
   
    public partial class ReporteEficienciaAPI : System.Web.UI.Page
    {

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_Informes Neg_Informes = new Neg_Informes();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                //CargarDptos();                
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text != "" && TxtFecha2.Text != "")
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                int id = Convert.ToInt32(userDetail.getIDEmpresa());

                System.Data.DataTable x = new System.Data.DataTable();

                DateTime fechaini = Convert.ToDateTime(txtFecha.Text);
                DateTime fechafin = Convert.ToDateTime(TxtFecha2.Text);

                ltDatosHtml.InnerHtml = new Neg_Eficiencia().EficienciaPorModuloHTML(fechaini, fechafin, id);
            }
        }
       
    }
}