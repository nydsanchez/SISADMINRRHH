using System;
using System.Data;
using Negocios;

namespace NominaRRHH
{   
    public partial class EmpleadosSinMarcaEntradaXDia : System.Web.UI.Page
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
                CargarDptos();               
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtFecha.Text != "")
            {
                IUserDetail userDetail = UserDetailResolver.getUserDetail();
                int id = Convert.ToInt32(userDetail.getIDEmpresa());

                DateTime fechaini = Convert.ToDateTime(txtFecha.Text);
                ltDatosHtml.InnerHtml = new Neg_Marca().ReportedeMarcasSinEntrada(fechaini,fechaini,5,3, int.Parse(ddldepto1.SelectedValue.ToString()), int.Parse(ddldepto2.SelectedValue.ToString()));                
            }
        }

        private void CargarDptos()
        {
            DataTable ft = Neg_Catalogos.CargarProcesos().Tables[0];
            DataView dv = ft.DefaultView;
            dv.Sort = "codigo_depto asc";
            DataTable sortedDT = dv.ToTable();

            ddldepto1.DataSource = sortedDT;
            this.ddldepto1.DataMember = "procesos";
            this.ddldepto1.DataValueField = "codigo_depto";
            ddldepto1.DataTextField = "nombre_depto";
            this.ddldepto1.DataBind();

            ddldepto2.DataSource = sortedDT;
            this.ddldepto2.DataMember = "procesos";
            this.ddldepto2.DataValueField = "codigo_depto";
            ddldepto2.DataTextField = "nombre_depto";
            this.ddldepto2.DataBind();
        }
        
    }
}