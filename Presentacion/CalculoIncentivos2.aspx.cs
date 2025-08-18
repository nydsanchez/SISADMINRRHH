using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using Negocios;
using Datos;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace NominaRRHH
{
    public partial class CalculoIncentivos2 : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Marca Neg_Marca = new Neg_Marca();
        Neg_Amonestaciones Neg_Amonestaciones = new Neg_Amonestaciones();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Empleados NegEmp = new Neg_Empleados();
        Neg_DevYDed NDevyDed = new Neg_DevYDed();
        #endregion


        DataTable dtDD = new DataTable();
        DataTable dtIngresosCargados = new DataTable();

        List<Neg_Incentivos.IncentivoEmp> ListIE = new List<Neg_Incentivos.IncentivoEmp>();
        List<Neg_Empleados> lt = new List<Neg_Empleados>(); //ESTA LISTA CONTIENE TODOS LOS EMPLEADOS DE LA PLANILLA
        List<Neg_Incentivos.PIngDeducInc> param = new List<Neg_Incentivos.PIngDeducInc>();
        List<Neg_Amonestaciones.Amonestaciones> la = new List<Neg_Amonestaciones.Amonestaciones>();
        List<Neg_Empleados> Empleados = new List<Neg_Empleados>();// ESTA ;OSTA CONTIENE SOLO LOS EMPLEADOS DE UN DEPARTAMENTO EN ESPECIFICO
        List<Neg_Incentivos.EmpleadoOp> EmpleadosOP = new List<Neg_Incentivos.EmpleadoOp>();// ESTA ;OSTA CONTIENE SOLO LOS EMPLEADOS DE UN DEPARTAMENTO EN ESPECIFICO




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["EMPLEADOS"] = ""; // VARIABLE QUE ALMACENARA LA TABLA DE TODOS LOS EMPLEADOS
                Session["PARAM"] = "";
                Session["AMONESTACIONES"] = "";
                Session["Ing"] = "";
                Session["EMPLEADOSOP"] = ""; // VARIABLE QUE ALMACENARA LA TABLA DE TODOS LOS EMPLEADOS y SUS OPERACIONES
                Session["PARAM"] = "";
                Session["dtcargado"] = "";
                Session["IngDed"] = "";

                panelinc.Visible = false;
                Button1.Visible = false;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
            if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                throw new Exception("Periodo Invalido");

            if (txtFechaIni.Text.Trim() == "" || txtFechaFin.Text.Trim() == "")
                throw new Exception("Favor Seleccione un Rango de Semana");

            dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(Convert.ToInt32(txtPeriodo.Text));
            if (dtPeriodo[0].cerrado == 1)
                throw new Exception("El periodo esta cerrado");

            string user = Convert.ToString(this.Page.Session["usuario"]);

            int codigo, amonestacion, diaslaborados, diasausencia, ausenciaj, ausenciaI;
            string modulo;
            decimal produccion, metaalcanzada, totalA, eficiencia, incentivoM, totalI, totalE, incentivo;
            if (gvING.Rows.Count > 0)
            {

                DataTable dt = new DataTable();

                dt.Columns.Add("Codigo_emp");
                dt.Columns.Add("Depto");
                dt.Columns.Add("Nombre_Completo");
                dt.Columns.Add("Tipo_Ingreso_Reportado");
                dt.Columns.Add("Monto_Reportado");
                dt.Columns.Add("Monto_Deducido");
                dt.Columns.Add("Detalle_Deduccion");
                dt.Columns.Add("Dias_Ausencia_Injustificada");
                dt.Columns.Add("Número_de_amonestaciones");
                dt.Columns.Add("Total_a_Recibir");
                dt.Columns.Add("Comentario");


                DataTable dt1 = new DataTable();
                //Estructura de la tabla Meses.
                dt1.Columns.Add("tipo");
                dt1.Columns.Add("codigoempleado");
                dt1.Columns.Add("semana");
                dt1.Columns.Add("idtipo");
                dt1.Columns.Add("periodo");
                dt1.Columns.Add("valor");


                // dsIncIE= DS INCENTIVOS CON INGRESOS Y DEDUCCIONES, ESTE DATATABLE GUARDA LOS TIPOS DE PENALIZACIONES APLICADOS AL TIPO DE INGRESO REPORTADO
                DataTable dtIngresosyDeducciones = new DataTable();

                dtIngresosyDeducciones.Columns.Add("Codigo", typeof(int));
                dtIngresosyDeducciones.Columns.Add("tipo", typeof(int));
                dtIngresosyDeducciones.Columns.Add("detalle", typeof(string));
                dtIngresosyDeducciones.Columns.Add("tipoCalc", typeof(int));
                dtIngresosyDeducciones.Columns.Add("Cantidad", typeof(decimal));
                dtIngresosyDeducciones.Columns.Add("Valor", typeof(decimal));
                dtIngresosyDeducciones.Columns.Add("Observacion", typeof(string));
                dtIngresosyDeducciones.Columns.Add("tipoIng", typeof(string));
                dtIngresosyDeducciones.Columns.Add("GeneradoSistema", typeof(bool));
                Session["IngDed"] = dtIngresosyDeducciones;

                DateTime fechaini = Convert.ToDateTime(txtFechaIni.Text);
                DateTime fechafin = Convert.ToDateTime(txtFechaFin.Text);
                IUserDetail userDetail = UserDetailResolver.getUserDetail();

                //filtro 1 liquidados y activos
                //filtro 2 solo activos
                lt = Neg_Marca.ObtenerHT(fechaini, fechafin, dtPeriodo[0].ubicacion,1 , userDetail.getIDEmpresa());
                Session["EMPLEADOS"] = lt;

                la = Neg_Amonestaciones.getamonestaciones(fechaini, fechafin);
                Session["AMONESTACIONES"] = la;

                EmpleadosOP = Neg_Incentivos.EmpleadoOperacion();
                Session["EMPLEADOSOP"] = EmpleadosOP;

                param = Neg_Incentivos.ParametosIngresosDeduccionesIncentivos(0);
                Session["PARAM"] = param;
                int periodo = int.Parse(txtPeriodo.Text);
                int semana = int.Parse(ddlTipo.SelectedValue);

                //wbravo
                dtIngresosCargados = (DataTable)Session["dtcargado"];

                Neg_DevYDed Neg_DevYDed = new Neg_DevYDed();
                DateTime iniweekend, finweekend;
                if ((int)fechafin.DayOfWeek == 5)
                {
                    iniweekend = fechafin.AddDays(1);//sabado
                    finweekend = iniweekend.AddDays(1);
                }
                else if ((int)fechafin.DayOfWeek == 6)
                {
                    iniweekend = fechafin;//sabado
                    finweekend = iniweekend.AddDays(1);
                }
                else
                {
                    iniweekend = fechafin.AddDays(-1);//sabado
                    finweekend = iniweekend;
                }
              
                //se obtiene las horas extras en fin de semana
                DataTable dtHRFindeSemana = Neg_DevYDed.ObtenerDetalleHorasExtrasxFecha(1, 0, fechaini, fechafin);
                DataTable dtIncDeducEmpleados = Neg_Incentivos.IncentivoIngDedccLOGxEmpleado(periodo, semana);
                foreach (DataRow row in dtIngresosCargados.Rows)
                {

                    if (row["Aprobado"].ToString() == "SI")
                    {
                        Neg_Empleados empleado = new Neg_Empleados();
                        int tipoing = int.Parse(row["idtipo"].ToString());
                        int codempleado = int.Parse(row["codigoEmpleado"].ToString());                        
                        string comentario = row["Comentario"].ToString();
                        incentivo = Convert.ToDecimal(row["valor"].ToString());
                        
                        empleado = lt.Where(x => x.codigo_empleado.Equals(codempleado)).SingleOrDefault();
                        if (empleado != null)
                        {
                            //wbravo2
                            tipoing = Neg_Incentivos.PlnObtenerIDRubroIncentivo(codempleado, tipoing, 1);//pasa el rubro para transformarlo.
                            //POR EL MOMENTO SE HARA ESTO DE ESTA FORMA, MIENTRAS SE MEJORA EL PROCESO
                            Neg_Incentivos.IncentivoEmp l = Neg_Incentivos.ProcesarIncentivos(empleado, "", "", EmpleadosOP, param, empleado.departamento, 0, 0, 0, incentivo, 0, 0, tipoing, comentario);

                            if (l != null)
                            {
                                ListIE.Add(l);
                                dt.Rows.Add(codempleado, empleado.departamento, empleado.nombrecompleto, tipoing, incentivo, l.Deduccion, l.DetalleEgreso, l.DiasInjustificados, l.Amonestaciones, l.Total, comentario);
                            }
                            Session["DTIncentivosSP"] = ListIE;

                        }

                        else
                        {

                        }
                    }

                }              

               

                DataTable dtfiltrartiposIng = dtIngresosCargados.Copy();
                DataView dtvfiltrartiposIng = dtfiltrartiposIng.DefaultView;
                


                var filtrartiposIng = (from r in dtfiltrartiposIng.AsEnumerable()
                                       select r[dtIngresosCargados.Columns[1].ToString()]).Distinct().ToList();

             
                decimal valor = 0, cantidad = 0;
                DataRow[] empleadoreg = null;
                DataRow[] empleadoingdeduc = null;
                dtIngresosyDeducciones = (DataTable)Session["IngDed"];
                foreach (var item in ListIE)
                {
                    valor = 0; 
                    codigo = int.Parse(item.Codigo.ToString());
                    modulo = item.Modulo.ToString();
                    amonestacion = int.Parse(item.Amonestaciones.ToString());
                    diaslaborados = int.Parse(item.DiasLaborados.ToString());
                    diasausencia = int.Parse(item.DiasAusencias.ToString());
                    ausenciaj = int.Parse(item.DiasJustificados.ToString());
                    ausenciaI = int.Parse(item.DiasInjustificados.ToString());

                    produccion = decimal.Parse(item.Produccion.ToString());
                    metaalcanzada = decimal.Parse(item.Meta.ToString());
                    totalA = decimal.Parse(item.Horasaunsencia.ToString());
                    eficiencia = decimal.Parse(item.Eficiencia.ToString());
                    incentivoM = decimal.Parse(item.Incentivo.ToString());
                    totalI = decimal.Parse(item.Ingreso.ToString());
                    totalE = decimal.Parse(item.Deduccion.ToString());
                    incentivo = decimal.Parse(item.Total.ToString());

                    if (codigo== 86220)
                    {

                    }
                    //obtener horas extras aplicadas al periodo, en jornada de fin de semana o en dia feriado
                    valor = 0; cantidad = 0;
                    empleadoreg = dtHRFindeSemana.AsEnumerable().Where(c => c.Field<int>("codigo_empleado") == codigo
                    && c.Field<int>("id_tipo") == 1 && (c.Field<int>("tipoingrdeduc") == 28) && c.Field<int>("periodo") == periodo).ToArray();
                    //si ya fueron descontadas en otro rubro
                    empleadoingdeduc = dtIncDeducEmpleados.AsEnumerable().Where(c => c.Field<int>("codigo") == codigo && c.Field<int>("IdTipoIng") != item.TipoIngr
                    && c.Field<int>("tipo") == 2 && c.Field<string>("detalle") == "HES" && c.Field<bool>("GeneradoSistema") == true).ToArray();
                    if (empleadoreg.Length > 0 && empleadoingdeduc.Length == 0)
                    {
                        valor = empleadoreg.Sum(c => c.Field<decimal>("valor"));
                        cantidad = empleadoreg.Sum(c => c.Field<decimal>("tiempo"));
                        totalE += valor;
                        incentivo -= valor;
                        dtIngresosyDeducciones.Rows.Add(codigo, 2, empleadoreg.FirstOrDefault()["nombrerubro"].ToString(), 1, cantidad, valor, "SISTEMA", item.TipoIngr);
                    }
                    //obtener horas extras aplicadas al periodo, en jornada de fin de semana o en dia feriado
                    valor = 0; cantidad = 0;
                    empleadoreg = dtHRFindeSemana.AsEnumerable().Where(c => c.Field<int>("codigo_empleado") == codigo
                    && c.Field<int>("id_tipo") == 1 && (c.Field<int>("tipoingrdeduc") == 32) && c.Field<int>("periodo") == periodo).ToArray();
                    //si ya fueron descontadas en otro rubro
                    empleadoingdeduc = dtIncDeducEmpleados.AsEnumerable().Where(c => c.Field<int>("codigo") == codigo && c.Field<int>("IdTipoIng") != item.TipoIngr
                    && c.Field<int>("tipo") == 2 && c.Field<string>("detalle").ToLower() == "hferiado (-)" && c.Field<bool>("GeneradoSistema") == true).ToArray();

                    if (empleadoreg.Length > 0 && empleadoingdeduc.Length == 0)
                    {
                        valor = empleadoreg.Sum(c => c.Field<decimal>("valor"));
                        cantidad = empleadoreg.Sum(c => c.Field<decimal>("tiempo"));
                        totalE += valor;
                        incentivo -= valor;

                        dtIngresosyDeducciones.Rows.Add(codigo, 2, empleadoreg.FirstOrDefault()["nombrerubro"].ToString(),1, cantidad, valor, "SISTEMA", item.TipoIngr);
                    }

                    incentivo = incentivo < 0 ? 0 : incentivo;
                    //if (item.TipoIngr==4 || item.TipoIngr==14)//viaticos o incentivos protegidos
                    //{              
                        //Neg_Incentivos.IncentivosHistoricoInsert(codigo, modulo, item.Estilo.ToString(), item.Operacion, item.Construccion, produccion, metaalcanzada, amonestacion, diaslaborados, diasausencia, ausenciaj, ausenciaI, totalA, eficiencia, incentivoM, totalI, totalE, incentivo, periodo, semana, user, item.TipoIngr, item.Comentario, false);
                    if (!Neg_Incentivos.PlnIncentivosxEmpleadoIns(periodo, semana, modulo.ToString(), codigo, "", item.Operacion, 0, 0, amonestacion, incentivoM, 0, totalE, incentivo, user, item.TipoIngr, item.Comentario, false))
                    {
                        throw new Exception("Error al insertar ingreso");
                    }
                    
                    //bloque de codigo que inserta ingresos asociados a deducirse del ingreso bruto de incentivos
                    //NDevyDed.IngresosIncentivoIBrutoEliminarxEmp(periodo, semana, item.TipoIngr, codigo);
                    //dt1.Rows.Add(1, codigo, semana, item.TipoIngr, periodo, Math.Ceiling(incentivo));
                    if (codigo == 0)
                    {
                        continue;
                    }
                  
                    if (!NDevyDed.InsertarIngrDeduc(1, codigo, semana, item.TipoIngr, periodo, Math.Round(incentivo), user))
                    //if (!InsertarIngrDeduc(Convert.ToInt32(item[0]), Convert.ToInt32(item[1]), semana, Convert.ToInt32(item[3]), periodo, valorOriginal, user))
                    {
                        throw new Exception("Error al insertar ingreso");
                    }
                    //SE INSERTA INGRESO RESPALDO SOLO PARA INGRESOS QUE APLICAN A DEDUCCION BASE
                    if (!NDevyDed.IngresosAplicaIBrutoBakIns(1, codigo, semana, item.TipoIngr, periodo, Math.Round(incentivo)))
                    {
                        throw new Exception("Error al insertar ingreso");
                    }
                }

                //dtInD = (DataTable)Session["IngDed"];
                foreach (DataRow dr in dtIngresosyDeducciones.Rows)
                {
                    Neg_Incentivos.IncentivoIngDedccLOGInsert(int.Parse(dr["Codigo"].ToString()), periodo, semana, int.Parse(dr["tipo"].ToString()), dr["detalle"].ToString(), int.Parse(dr["tipoCalc"].ToString()), decimal.Parse(dr["Cantidad"].ToString()), decimal.Parse(dr["Valor"].ToString()), dr["Observacion"].ToString(), int.Parse(dr["tipoIng"].ToString()), true);
                }
                ///partida de ingreso y deduccion wallmart
                //NDevyDed.RegistrarIngresoEgresoAsociado(periodo, dt1, user);

                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.DataSources.Clear();

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/ReporteInventivos2.rdlc");
                ReportDataSource source = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Add(source);
                ReportViewer1.LocalReport.Refresh();

            }

           

        } //aqui fin

        protected void btAlicarPlanilla_Click(object sender, EventArgs e)
        {

        }

        protected void btnCargarID_Click(object sender, EventArgs e)
        {
            if (txtPeriodo.Text != "")
            {
                if (txtFechaIni.Text != "" && txtFechaFin.Text != "")
                {

                    if (fuIngr.HasFile)
                    {

                        Button1.Visible = true;
                        CargarArchivo("fuIngr", "NG", "gvING");
                    }
                    else
                    {
                        alertValida.Visible = true;
                        lblAlert.Visible = true;
                        lblAlert.Text = "Favor Seleccione un archivo";
                        fuIngr.Focus();
                    }
                }
                else
                {
                    alertValida.Visible = true;
                    lblAlert.Visible = true;
                    lblAlert.Text = "Favor Seleccione un Rango de semana";
                    fuIngr.Focus();
                }
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Ingrese un Periodo";
                fuIngr.Focus();
            }
        }

        #region METODOS

        public void CargarArchivo(string nameFU, string NombreVS, string nombreGV)
        {

            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            FileUpload FU = (FileUpload)cph.FindControl(nameFU);
            GridView gv = (GridView)cph.FindControl(nombreGV);

            if (FU.HasFile)
            {

                string connectionString = "";
                string fileName = Path.GetFileName(FU.PostedFile.FileName);
                string fileExtension = Path.GetExtension(FU.PostedFile.FileName);
                string fileLocation = HttpContext.Current.Server.MapPath("..").ToString() + @"\Trash\" + fileName;
                FU.SaveAs(fileLocation);


                if (fileExtension == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (fileExtension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }



                //Create OleDB Connection and OleDb Command
                OleDbConnection con = new OleDbConnection(connectionString);
                OleDbCommand cmd = new OleDbCommand();


                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = con;

                OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);

                con.Open();

                DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                dAdapter.SelectCommand = cmd;
                dAdapter.Fill(dtDD);
                con.Close();

                dtIngresosCargados.Columns.Add("codigoEmpleado");
                dtIngresosCargados.Columns.Add("idtipo");
                dtIngresosCargados.Columns.Add("valor");
                dtIngresosCargados.Columns.Add("Comentario");
                dtIngresosCargados.Columns.Add("TipoIngreso");
                dtIngresosCargados.Columns.Add("Aprobado", typeof(string));
                DataTable TipodIngresos = Neg_Incentivos.plnDevengadoSelect();

                bool Correcto = false;

                if ((dtDD.Columns[0].ToString().ToLower().Trim() == "codigoEmpleado".ToLower().Trim())
                    && (dtDD.Columns[1].ToString().ToLower().Trim() == "idTipo".ToLower().Trim())
                    && (dtDD.Columns[2].ToString().Trim().ToLower() == "valor".ToLower().Trim())
                    && (dtDD.Columns[3].ToString().Trim().ToLower() == "Comentario".ToLower().Trim())
                   )
                {
                    Correcto = true;
                }

                if (Correcto)
                {
                    DataView dtwExcelRecords = new DataView();
                    dtwExcelRecords = dtDD.DefaultView;  //se obtiene el total de filas
                    dtwExcelRecords.RowFilter = "[" + dtDD.Columns[0] + "] is not null"; //se obtiene las filas con datos y evitar recorrer campos nulos

                    DataTable dt2 = dtwExcelRecords.ToTable();
                    if (dt2.Rows.Count > 0)
                    {
                        panelinc.Visible = true;
                        Button1.Visible = true;
                    }
                    foreach (DataRow item in dt2.Rows)
                    {
                        DataView dtvTipodIngresos = TipodIngresos.Copy().DefaultView;
                        dtvTipodIngresos.RowFilter = "idDevengado=" + int.Parse(item["idtipo"].ToString());

                        if (dtvTipodIngresos.Count > 0)
                        {
                            try
                            {
                                decimal evaluar = decimal.Parse(item["valor"].ToString());
                                dtIngresosCargados.Rows.Add(item["codigoEmpleado"].ToString(), item["idtipo"].ToString(), item["valor"].ToString(), item["Comentario"].ToString(), dtvTipodIngresos.ToTable().Rows[0][1], "SI");
                            }
                            catch
                            {
                                dtIngresosCargados.Rows.Add(item["codigoEmpleado"].ToString(), item["idtipo"].ToString(), "REVISAR EL VALOR ASIGNADO A ESTE CODIGO, YA QUE NO PUEDE SER PROCESADO COMO INCENTIVO", item["Comentario"].ToString(), dtvTipodIngresos.ToTable().Rows[0][1], "NO");
                            }

                            // item["Aprobado"] = "SI";
                        }
                        else
                        {
                            dtIngresosCargados.Rows.Add(item["codigoEmpleado"].ToString(), item["idtipo"].ToString(), item["valor"].ToString(), item["Comentario"].ToString(), "EL TIPO DE INGRESO NO EXISTE EN EL SISTEMA", "NO");
                            // item["Aprobado"] = "NO";
                        }

                    }

                    gvING.DataSource = dtIngresosCargados;
                    gvING.DataBind();

                    Session["dtcargado"] = dtIngresosCargados;
                    //Read Data from First Sheet
                    //connExcel.Open();
                    //cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                    //oda.SelectCommand = cmdExcel;



                    //oda.Fill(dtID);
                    //dtID.Columns.Add("Comentario", typeof(string));
                    //Session["IngDD"] = dtID;
                    //gv.DataSource = dtID;
                    //gv.Columns[5].Visible = false;


                    //connExcel.Close();
                    //gv.DataBind();

                }

            }
        }

        #endregion

        protected void gvING_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvING.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)Session["dtcargado"];

            gvING.DataSource = dt;
            gvING.DataBind();
        }
    }
}