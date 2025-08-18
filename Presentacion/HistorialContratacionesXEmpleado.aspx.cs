using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Negocios;
using Datos;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NominaRRHH.Presentacion
{
    public partial class HistorialContratacionesXEmpleado : System.Web.UI.Page
    {
        #region REFERENCIAS
        //creado POR WENDY MEMBREÑO
        // 31 AGOSTO 2016
        Neg_Empleados Neg_Empleados = new Neg_Empleados();
        Neg_Catalogos nc = new Neg_Catalogos();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
        {
            if (txtdoc_ident.Text.Trim() != "")
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                int idEmpresa = userDetail.getIDEmpresa();
                DataTable dt = Neg_Empleados.HistorialContratacionXEmpleado(txtdoc_ident.Text, idEmpresa);
                GVDetNomEmpl.DataSource = dt;
                GVDetNomEmpl.DataBind();

            }
        }

        protected void GVDetNomEmpl_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Image imgfoto = e.Row.FindControl("imgfoto") as System.Web.UI.WebControls.Image;
               int codigo = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "codigo_empleado").ToString());
               byte[] foto = nc.cargarFoto(codigo);

               imgfoto.ImageUrl = "data:image/jpg;base64," + Convert.ToBase64String((byte[])foto);


            }

        }

      
    }
}