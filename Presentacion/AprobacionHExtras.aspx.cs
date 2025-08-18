using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocios;
using Datos;
using System.Data;


namespace NominaRRHH.Presentacion
{
    public partial class AprobacionHExtras : System.Web.UI.Page
    {
        //private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Catalogos Neg_Catalogos = new Neg_Catalogos();
        Neg_DevYDed Neg_DevYDed= new Neg_DevYDed();
        Neg_Marca Neg_Marca = new Neg_Marca();
        public string idUsuario;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                DateTime fini = DateTime.Now.Date;
                DateTime ffin = DateTime.Now.Date.AddDays(-1).Date;
                if ((int)DateTime.Now.DayOfWeek == 1)//lunes
                {
                    fini = DateTime.Now.AddDays(-3).Date;
                }
                else
                {
                    fini = DateTime.Now.AddDays(-1).Date;
                }
                txtFechaIni.Text = fini.ToShortDateString();
                txtFechaFin.Text = ffin.ToShortDateString();

                LimpiarSession();
            }
        }
        private void LimpiarSession()
        {
            Session["HorasExtrasAprob"] = null;
             Session["HorasExtrasRev"] = null;
        }

        private void ObtenerHorasExtras()
        {

            DataTable he = new DataTable();
            he = Neg_Marca.ObtenerHorasExtrasAprobacion(Convert.ToDateTime(txtFechaIni.Text), Convert.ToDateTime(txtFechaFin.Text),1,0,false);
            Session["HorasExtrasAprob"] = he;

        }
        private void ObtenerProcesos()
        {
            DataTable he = new DataTable();
            DataRow[] deptos = null;
            if (ChkEstatus.Checked)
            {
                he = Session["HorasExtrasRev"] as DataTable;
            }
            else
            {
                he = Session["HorasExtrasAprob"] as DataTable;
            }
            if (he != null)
            {
                deptos = he.AsEnumerable().GroupBy(c => c.Field<int>("codigo_depto")).Select(c => c.First()).ToArray();

                if (deptos.Length > 0)
                {
                    divfiltro.Visible = true;
                    this.ddlProceso.DataSource = deptos.CopyToDataTable();
                    this.ddlProceso.DataValueField = "codigo_depto";
                    this.ddlProceso.DataTextField = "nombre_depto";
                    this.ddlProceso.DataBind();
                }
                else
                {
                    divfiltro.Visible = false;
                }
            }
            else
            {
                divfiltro.Visible = false;
            }

        }
        private void MostrarRevisiones()
        {
            DataTable he = new DataTable();
            DataRow[] personal = null;
            he = Session["HorasExtrasAprob"] as DataTable;
            if (he != null)
            {
                personal = he.AsEnumerable().Where(c => c.Field<bool>("revision")).ToArray();

                if (personal.Length > 0)
                {
                    Session["HorasExtrasRev"] = personal.CopyToDataTable();
                }
                else
                {
                    Session["HorasExtrasRev"] = null;
                }
            }
          
        }
        private void FiltrarDatosxDepto()
        {
            DataTable he = new DataTable();
            DataRow[] personal = null;

            if (ChkEstatus.Checked)
            {
                he = Session["HorasExtrasRev"] as DataTable;
               

            }
            else
            {
                he = Session["HorasExtrasAprob"] as DataTable;
                

            }
            MostrarColumnasActivas();
            if (he != null)
            {
                if (ChkEstatus.Checked)
                {

                    personal = he.AsEnumerable().Where(c => c.Field<int>("codigo_depto") == Convert.ToInt32(ddlProceso.SelectedValue)).ToArray();

                }
                else
                {

                    personal = he.AsEnumerable().Where(c => c.Field<int>("codigo_depto") == Convert.ToInt32(ddlProceso.SelectedValue) && !c.Field<bool>("revision")).ToArray();

                }
                if (personal.Length > 0)
                {
                    
                    GVHEx.DataSource = personal.OrderBy(c => c.Field<DateTime>("fecha")).ThenBy(c => c.Field<int>("codigo_empleado")).CopyToDataTable();
                    GVHEx.DataBind();
                }
                else
                {
                    GVHEx.DataSource = null;
                    GVHEx.DataBind();
                }
            }
            else
            {
                GVHEx.DataSource = null;
                GVHEx.DataBind();
            }

        }
        void MostrarColumnasActivas()
        {
            if (ChkEstatus.Checked)
            {//4,5,8,10
             //e.Row.Cells[4].Visible = false;//hrini
                GVHEx.Columns[4].Visible = false;
                //e.Row.Cells[5].Visible = false;//hrfin
                GVHEx.Columns[5].Visible = false;
                //e.Row.Cells[9].Visible = false;//acum
                GVHEx.Columns[9].Visible = false;
                //e.Row.Cells[11].Visible = true;//comentario
                GVHEx.Columns[11].Visible = true;
            }
            else
            {
                //e.Row.Cells[4].Visible = true;//hrini
                GVHEx.Columns[4].Visible = true;
                //e.Row.Cells[5].Visible = true;//hrfin
                GVHEx.Columns[5].Visible = true;
                //e.Row.Cells[9].Visible = true;//acum
                GVHEx.Columns[9].Visible = true;
                //e.Row.Cells[11].Visible = false;//comentario
                GVHEx.Columns[11].Visible = false;
            }
        }
     
        protected void ddlProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarDatosxDepto();

        }
        protected void GVHEx_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.CompareTo("eliminar") == 0)
            {
                GridViewRow selectedRow = GVHEx.Rows[index];
                int ID = Convert.ToInt32(((TextBox)selectedRow.FindControl("txtId")).Text.Trim());


            }
            if (e.CommandName.CompareTo("editar") == 0)
            {
                GridViewRow selectedRow = GVHEx.Rows[index];
                int ID = Convert.ToInt32(((TextBox)selectedRow.FindControl("txtId")).Text.Trim());


                // }
                FiltrarDatosxDepto();

            }
        }

        protected void GVHEx_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string estatus = e.Row.Cells[10].Text.Trim();// 
                //if (estatus.ToLower() == "aprobado")
                //{
                //    CheckBox ChkBono = (e.Row.FindControl("ChkBono") as CheckBox);
                //    ChkBono.Enabled = false;

                //    CheckBox chkHE = (e.Row.FindControl("chkHE") as CheckBox);
                //    chkHE.Enabled = false;

                //}
                
                obtenerProcesos(e.Row);
                obtenerTiempoLibre(e.Row);
                obtenerTipoIngreso(e.Row);

                
            }
        }
        private void obtenerProcesos(GridViewRow row)
        {
            try
            {
                //Find the DropDownList in the Row
                DropDownList ddlDeptoAfecta = (row.FindControl("ddlDeptoAfecta") as DropDownList);
                GridViewRow rowH = GVHEx.HeaderRow;
                DropDownList ddlDeptoAfectaColumn = (rowH.FindControl("ddlDeptoAfectaColumn") as DropDownList);
                string lblDeptoAfecta = (row.FindControl("lblDeptoAfecta") as Label).Text;

                enlazarDropdownDepto(ddlDeptoAfecta);
                enlazarDropdownDepto(ddlDeptoAfectaColumn);

                ddlDeptoAfectaColumn.SelectedValue = ddlProceso.SelectedValue;
                if (lblDeptoAfecta != "0")
                {
                    //Select the Country of Customer in DropDownList               
                    ddlDeptoAfecta.Items.FindByValue(lblDeptoAfecta).Selected = true;
                }
                else
                {
                    ddlDeptoAfecta.SelectedValue = ddlProceso.SelectedValue;
                }
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        void enlazarDropdownDepto(DropDownList ddl)
        {
            DataTable departamentos = Neg_Catalogos.CargarProcesos().Tables[0];
            ddl.DataSource = departamentos;
            ddl.DataMember = "procesos";
            ddl.DataValueField = "codigo_depto";
            ddl.DataTextField = "nombre_depto";
            ddl.DataBind();
        }
        private void obtenerTipoIngreso(GridViewRow row)
        {
            try
            {
              
                //Find the DropDownList in the Row
                DropDownList ddlTipo = (row.FindControl("ddlTipo") as DropDownList);
                string lblTipo = (row.FindControl("lblTipo") as Label).Text;

                GridViewRow rowH = GVHEx.HeaderRow;
                DropDownList ddlTipoColumn = (rowH.FindControl("ddlTipoColumn") as DropDownList);

                enlazarDropdownIngreso(ddlTipo);
                enlazarDropdownIngreso(ddlTipoColumn);

                ddlTipoColumn.SelectedValue = "0";
                //Select the Country of Customer in DropDownList               
                ddlTipo.Items.FindByValue(lblTipo).Selected = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        void enlazarDropdownIngreso(DropDownList ddl)
        {
            DataTable ing = new DataTable();
            ing.Columns.Add("idDevengado");
            ing.Columns.Add("devengadoNombre");
            ing.Rows.Add(0, "No Pago");

            DataTable catalog = new DataTable();
            catalog = Neg_DevYDed.cargarIngresosAplicaDia().Tables[0];
            foreach (DataRow item in catalog.Rows)
            {
                ing.Rows.Add(item["idDevengado"].ToString(), item["devengadoNombre"].ToString());
            }
            ddl.DataSource = ing;
            ddl.DataValueField = "idDevengado";
            ddl.DataTextField = "devengadoNombre";
            ddl.DataBind();
        }
        private void obtenerTiempoLibre(GridViewRow row)
        {
            try
            {
                //Find the DropDownList in the Row
                DropDownList ddlTiempoLibre = (row.FindControl("ddlTiempoLibre") as DropDownList);
                string lblTiempoLibre = (row.FindControl("lblTiempoLibre") as Label).Text;

                GridViewRow rowH = GVHEx.HeaderRow;
                DropDownList ddlTiempoLibreColumn = (rowH.FindControl("ddlTiempoLibreColumn") as DropDownList);

                ddlTiempoLibreColumn.SelectedValue = "0.00";
                //Select the Country of Customer in DropDownList               
                //ddlTiempoLibre.Items.FindByValue(lblTiempoLibre).Selected = true;
                if (!string.IsNullOrEmpty(lblTiempoLibre))
                {
                    ddlTiempoLibre.Items.FindByValue(lblTiempoLibre).Selected = true;
                    //var item = ddlTiempoLibre.Items.FindByValue(lblTiempoLibre);
                    //if (item != null)
                    //{
                    //    item.Selected = true;
                    //}
                    //else
                    //{
                    //    // Manejar el caso donde no se encuentra el valor
                    //    Console.WriteLine("El valor no se encontró en ddlTiempoLibre.");
                    //}
                }
                else
                {
                    // Manejar el caso donde lblTiempoLibre es null o vacío
                    Console.WriteLine("lblTiempoLibre está vacío o es null.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        protected void ChkAllBono_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Boolean Mark = false;

                GridViewRow row = GVHEx.HeaderRow;
                CheckBox ChkAllBono = (CheckBox)row.FindControl("ChkAllBono");
                ChkAllBono.Focus();
                if (ChkAllBono.Checked == true)
                {
                    Mark = true;
                    ((CheckBox)row.FindControl("ChkAllHE")).Checked = false;
                }

                for (int i = 0; i < GVHEx.Rows.Count; i++)
                {
                    string estatus = GVHEx.Rows[i].Cells[10].Text.Trim();// 

                    if (estatus.ToLower() != "aprobado")
                    {
                        ((CheckBox)GVHEx.Rows[i].FindControl("chkBono")).Checked = Mark;
                        ((CheckBox)GVHEx.Rows[i].FindControl("chkHE")).Checked = false;
                    }


                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }

        protected void ChkAllHE_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Boolean Mark = false;

                GridViewRow row = GVHEx.HeaderRow;
                CheckBox ChkAllHE = (CheckBox)row.FindControl("ChkAllHE");
                ChkAllHE.Focus();

                if (((CheckBox)row.FindControl("ChkAllHE")).Checked == true)
                {
                    Mark = true;
                    ((CheckBox)row.FindControl("ChkAllBono")).Checked = false;
                }

                for (int i = 0; i < GVHEx.Rows.Count; i++)
                {
                    string estatus = GVHEx.Rows[i].Cells[10].Text.Trim();// 

                    if (estatus.ToLower() != "aprobado")
                    {
                        ((CheckBox)GVHEx.Rows[i].FindControl("chkHE")).Checked = Mark;
                        ((CheckBox)GVHEx.Rows[i].FindControl("chkBono")).Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
        protected void ChkBono_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            GridViewRow rowH = GVHEx.HeaderRow;

            int index = row.RowIndex;
            CheckBox ChkBono = (CheckBox)GVHEx.Rows[index].FindControl("ChkBono");
            ChkBono.Focus();
            
            if (ChkBono.Checked)
            {
                ((CheckBox)GVHEx.Rows[index].FindControl("ChkHE")).Checked = false;

                if (((CheckBox)rowH.FindControl("ChkAllHE")).Checked == true)
                {
                    ((CheckBox)rowH.FindControl("ChkAllHE")).Checked = false;
                }
            }

        }

        protected void ChkHE_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            GridViewRow rowH = GVHEx.HeaderRow;

            int index = row.RowIndex;
            CheckBox ChkHE = (CheckBox)GVHEx.Rows[index].FindControl("ChkHE");
            ChkHE.Focus();
           
            if (ChkHE.Checked)
            {
                ((CheckBox)GVHEx.Rows[index].FindControl("ChkBono")).Checked = false;
                if (((CheckBox)rowH.FindControl("ChkAllBono")).Checked == true)
                {
                    ((CheckBox)rowH.FindControl("ChkAllBono")).Checked = false;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtFechaIni.Text) || string.IsNullOrEmpty(txtFechaFin.Text))
                {
                    throw new Exception("Debe ingresar Fechas validas");
                }


                ActualizarGrid();
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
        void ActualizarGrid()
        {
            divChkDptoVac.Visible = true;
            ObtenerHorasExtras();
            if (ChkEstatus.Checked)
            {
                MostrarRevisiones();
            }
            ObtenerProcesos();
            FiltrarDatosxDepto();
           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Neg_Planilla Neg_Planilla = new Neg_Planilla();
                string user = Convert.ToString(this.Page.Session["usuario"]);
                //aqui recorrer la grid
                //bono checked tipoingrdeduc=18, insertarIngDeducxDia
                //HE checked tipoingrdeduc=1, insertarHorasExtrasxDia
                if (this.GVHEx.Rows.Count > 0)
                {
                    int codigo = 0;
                    DateTime fecha = new DateTime();
                    decimal horas = 0, tiempolibre = 0;
                    string estatus = "";
                    bool bonoOrig ,HEOrig;
                    int tipop = 0,tipoOrig=0,depto_afecta=0;
                    for (int i = 0; i < this.GVHEx.Rows.Count; i++)
                    {
                        GridViewRow selectedRow = this.GVHEx.Rows[i];
                        //se toma el valor de los campos de la tabla
                        codigo = Convert.ToInt32(selectedRow.Cells[0].Text.Trim());//                      
                        fecha = Convert.ToDateTime(selectedRow.Cells[3].Text.Trim());//
                        horas = Convert.ToDecimal(((TextBox)GVHEx.Rows[i].FindControl("txtHEPagar")).Text);//    
                        DropDownList ddlTipo = (GVHEx.Rows[i].FindControl("ddlTipo") as DropDownList);
                        tipop = Convert.ToInt32(ddlTipo.SelectedValue);
                        tipoOrig = Convert.ToInt32(GVHEx.DataKeys[i][2]);
                        //tiempolibre
                        DropDownList ddlTiempoLibre = (GVHEx.Rows[i].FindControl("ddlTiempoLibre") as DropDownList);
                        tiempolibre = Convert.ToDecimal(ddlTiempoLibre.SelectedValue);
                        //departamento afectado
                        DropDownList ddlDeptoAfecta = (GVHEx.Rows[i].FindControl("ddlDeptoAfecta") as DropDownList);
                        depto_afecta = Convert.ToInt32(ddlDeptoAfecta.SelectedValue) == Convert.ToInt32(ddlProceso.SelectedValue) ? 0 : Convert.ToInt32(ddlDeptoAfecta.SelectedValue);
                        //HEOrig = Convert.ToBoolean(GVHEx.DataKeys[i][3]);
                        //CheckBox chkBono = (CheckBox)GVHEx.Rows[i].FindControl("chkBono");
                        //CheckBox chkHE = (CheckBox)GVHEx.Rows[i].FindControl("chkHE");                       
                        //estatus = selectedRow.Cells[10].Text.Trim();

                        //if (estatus.ToLower()!="aprobado")
                        //{
                        if (tipoOrig != tipop)
                        {
                            if (!Neg_Planilla.EliminarIngDeducxDia(codigo, 0, 1, tipoOrig, fecha))
                            {
                                throw new Exception("Error eliminando ingreso por dia.");
                            }
                        }
                        if (tipop != 0)//se selecciono tipo d ingreso
                        {
                            if (tipop == 26 || tipop == 33)//bono cumplimiento o errores imputables sin calculo
                            {
                                if (!Neg_Planilla.insertarIngDeducxDia(codigo, 0, 1, tipop, fecha, horas, 0,tiempolibre,depto_afecta, user))
                                {
                                    throw new Exception("Error registrando bono dia.");
                                }
                            }
                            else 
                            {
                                //horas extras y bono variable
                                //if (!Neg_Planilla.insertarHorasExtrasxDia(codigo, 0,tipop, fecha, horas, tiempolibre, depto_afecta, user))
                                //{
                                //    throw new Exception("Error registrando HE dia.");
                                //}
                                Neg_Marca.VerificarTipoIngresoExcedente(fecha,0, codigo,tipop, horas, tiempolibre, depto_afecta, user);
                            }
                        }
                          
                                                       
                        //}
                        
                    }

                    // Recuperar el ID del usuario desde la sesión
                    object ob = this.Page.Session["usuario"];
                    
                    if (ob != null)
                    {
                        idUsuario = ob.ToString(); // Asegúrate de convertirlo al tipo correcto
                                                   // Ahora puedes usar idUsuario según sea necesario
                        ///Response.Write($"ID del Usuario: {idUsuario}");
                    }
                    else
                    {
                        Response.Write("No se encontró el ID del usuario en la sesión.");
                    }

                    // Log
                    //Log.Information("Datos del empleado con código {CodEmpleado} actualizados exitosamente.", codEmpleado);
                    //logger.Info("Empleados Actualizado. No. " + codigo + ", por el Usuario: " + idUsuario);

                    ActualizarGrid();
                    alertValida.Visible = false;
                    alertSucces.Visible = true;
                    LblSuccess.Visible = true;
                    LblSuccess.Text = "Los registros se han guardado correctamente.";
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }
        protected void ChkEstatus_CheckedChanged(object sender, EventArgs e)
        {
            MostrarRevisiones();
            ObtenerProcesos();
            FiltrarDatosxDepto();
        }

        protected void ddlTiempoLibre_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);
            
            int index = row.RowIndex;
            calcularHrsAPagar(index);
            
        }
        void calcularHrsAPagar(int index)
        {
            double horas = Convert.ToDouble(GVHEx.Rows[index].Cells[6].Text.Trim());//    
            DropDownList ddlTiempoLibre = (DropDownList)GVHEx.Rows[index].FindControl("ddlTiempoLibre");
            ddlTiempoLibre.Focus();
            double tiempo_libre = Convert.ToDouble(ddlTiempoLibre.SelectedValue);

            TextBox txtHEPagar = (TextBox)GVHEx.Rows[index].FindControl("txtHEPagar");
            double Apagar = horas - tiempo_libre;
            txtHEPagar.Text = Apagar < 0 ? "0" : Apagar.ToString();
            HiddenField hfhrvalidas = (HiddenField)GVHEx.Rows[index].FindControl("hfhrvalidas");
            hfhrvalidas.Value = Apagar < 0 ? "0" : Apagar.ToString();
        }
        protected void ddlDeptoAfectaColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow rowH = GVHEx.HeaderRow;
                DropDownList ddlDeptoAfectaColumn = (rowH.FindControl("ddlDeptoAfectaColumn") as DropDownList);
                ddlDeptoAfectaColumn.Focus();              

                for (int i = 0; i < GVHEx.Rows.Count; i++)
                {
                    ((DropDownList)GVHEx.Rows[i].FindControl("ddlDeptoAfecta")).SelectedValue = ddlDeptoAfectaColumn.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }

        protected void ddlTiempoLibreColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow rowH = GVHEx.HeaderRow;
                DropDownList ddlTiempoLibreColumn = (rowH.FindControl("ddlTiempoLibreColumn") as DropDownList);
                ddlTiempoLibreColumn.Focus();
              
                for (int i = 0; i < GVHEx.Rows.Count; i++)
                {
                    ((DropDownList)GVHEx.Rows[i].FindControl("ddlTiempoLibre")).SelectedValue = ddlTiempoLibreColumn.SelectedValue;
                    calcularHrsAPagar(i);
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }

        protected void ddlTipoColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GridViewRow rowH = GVHEx.HeaderRow;
                DropDownList ddlTipoColumn = (rowH.FindControl("ddlTipoColumn") as DropDownList);
                ddlTipoColumn.Focus();
               

                for (int i = 0; i < GVHEx.Rows.Count; i++)
                {
                    ((DropDownList)GVHEx.Rows[i].FindControl("ddlTipo")).SelectedValue = ddlTipoColumn.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                this.alertValida.Visible = true;
                this.lblAlert.Visible = true;
                this.lblAlert.Text = ex.Message;
            }
        }

        //protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DropDownList ddlTipo = null;
        //        GridViewRow row = ((GridViewRow)((DropDownList)sender).NamingContainer);

        //        int index = row.RowIndex;
        //        ddlTipo = (DropDownList)GVHEx.Rows[index].FindControl("ddlTipo");
        //        int tiposelected = Convert.ToInt32(ddlTipo.SelectedValue.Trim());
        //        int tipooriginal = Convert.ToInt32(GVHEx.DataKeys[index][2].ToString());

        //        //
        //    }
        //    catch (Exception ex)
        //    {
        //        this.alertValida.Visible = true;
        //        this.lblAlert.Visible = true;
        //        this.lblAlert.Text = ex.Message;
        //    }
        //}
    }
}