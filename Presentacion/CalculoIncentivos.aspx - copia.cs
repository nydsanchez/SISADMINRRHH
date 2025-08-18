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
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace NominaRRHH.Presentacion
{
    public partial class CalculoIncentivos : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Marca Neg_Marca = new Neg_Marca();
        Neg_Amonestaciones Neg_Amonestaciones = new Neg_Amonestaciones();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        #endregion

        #region CLASES DE OBJETOS


        #endregion

        #region VARIABLES PUBLICAS
        // int diasL = 0, diasJ = 0;
        // decimal horaspcgsg;
        DataTable dt = new DataTable();
        DataTable dtID = new DataTable();
        DataTable dtDD = new DataTable();
        DataTable dtc1 = new DataTable();
        DataTable dtc2 = new DataTable();

        int codigo1 = 0, codigo2 = 0;
        decimal DZ = 0;
        string connectionString = "", observacionTDZ = "", observacionID = "";

        decimal ingresoNumero = 0, deduccionnumerico = 0;
        decimal ingresoporcentual = 0, deduccionporcentual = 0;
        int modulo = 0, estilo = 0;
        int Codigo_depto = 0;
        decimal MetaDias = 0, meta = 0, produccion = 0;
        decimal incentivo = 0, eficienciai = 0;
        List<Neg_Incentivos.IncentivoEmp> ListIE = new List<Neg_Incentivos.IncentivoEmp>();
        List<Neg_Incentivos.ProdXmod> prodcmod = new List<Neg_Incentivos.ProdXmod>();
        List<Neg_Incentivos.Incentivos> li = new List<Neg_Incentivos.Incentivos>();
        List<Neg_Empleados> lt = new List<Neg_Empleados>(); //ESTA LISTA CONTIENE TODOS LOS EMPLEADOS DE LA PLANILLA
        List<Neg_Empleados> Empleados = new List<Neg_Empleados>();// ESTA ;OSTA CONTIENE SOLO LOS EMPLEADOS DE UN DEPARTAMENTO EN ESPECIFICO
        List<Neg_Incentivos.EmpleadoOp> EmpleadosOP = new List<Neg_Incentivos.EmpleadoOp>();// ESTA ;OSTA CONTIENE SOLO LOS EMPLEADOS DE UN DEPARTAMENTO EN ESPECIFICO
        List<Neg_Incentivos.PIngDeducInc> param = new List<Neg_Incentivos.PIngDeducInc>();
        List<Neg_Amonestaciones.Amonestaciones> la = new List<Neg_Amonestaciones.Amonestaciones>();
        #endregion

        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["DTIncentivosSP"] = ""; //VARIABLE ALMACENARA INCENTIVOS PARA MODULOS QUE ALCANZARON META
                Session["datos"] = ""; // VARIABLE QUE ALMACENARA LOS DATOS DE PERSONAS A LAS QUE SE LE SUMARA Y RESTARA PRODUCCION
                Session["PRODUCCIONXMODULO"] = "";//VARIABLE QUE ALMACENARA LA PRODUCCION POR MODULO ENTRE EL RANGO DE FEVHA SELECCIONADO
                Session["TABLAINCENTIVOS"] = ""; //VARAIBLE QUE ALMACENARA LA TABLA DE INCENTIVOS EN GENERAL POR ESTILO
                Session["EMPLEADOS"] = ""; // VARIABLE QUE ALMACENARA LA TABLA DE TODOS LOS EMPLEADOS
                Session["EMPLEADOSOP"] = ""; // VARIABLE QUE ALMACENARA LA TABLA DE TODOS LOS EMPLEADOS y SUS OPERACIONES
                Session["PARAM"] = "";
                Session["AMONESTACIONES"] = "";
                Session["IngDD"] = "";
                Session["DiasMetasModulos"] = "";
                Session["IngDed"] = "";
                Session["DTTRaslados"] = "";

                pnlDZ.Visible = false;

                divID.Visible = false;
                divIDGrid.Visible = false;

                divDDZ.Visible = false;
                divDDZGrid.Visible = false;
                btAlicarPlanilla.Visible = false;

                panelDZ.Visible = false;
                panelID.Visible = false;


                //dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(lista[0].Periodo);
            }
        }

        protected void btnProcesar_Click(object sender, EventArgs e)
        {
            GenerarR(1);
            pnlDZ.Visible = false;
        }

        protected void btProcesarDZ_Click(object sender, EventArgs e)
        {
            if (fuDDZ.HasFile)
            {
                CargarArchivo("fuDDZ", "DZ", "gvReajusteDZ");
                alertValida.Visible = false;
                lblAlert.Visible = false;
                lblAlert.Text = "";
                fuDDZ.Focus();
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                fuDDZ.Focus();
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            GenerarR(2);
            pnlDZ.Visible = true;
        }
        #endregion

        #region METODOS

        public bool validar()
        {

            int c = 0;
            if (txtFechaIni.Text.Trim() == "" || txtFechaFin.Text.Trim() == "")
            {
                c = c + 1;
                txtFechaFin.Focus();
                lblAlert.Text = "Favor Ingrese una Fecha de Inicio y una fecha Fin";
            }

            else
            {
                if (Convert.ToDateTime(txtFechaIni.Text.Trim()) > Convert.ToDateTime(txtFechaFin.Text.Trim()))
                {
                    lblAlert.Text = "Rango de Fechas invalidos";
                    txtFechaIni.Focus();
                    c = c + 1;
                }

            }

            if (c > 0)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;

                return false;
            }
            else { return true; }

        }

        public void cargarReporte()
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();
            // ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PagoIncentivo.rdlc");

            ReportDataSource source = new ReportDataSource("DataSet1", ListIE);
            ReportViewer1.LocalReport.DataSources.Add(source);
            ReportViewer1.LocalReport.Refresh();
            Session["DTIncentivosSP"] = ListIE;//ESTA VARIALE DE SESSION ALMACENA LOS MONTOS DE INCENTIVOS POR MODULOS
        }
        public void GenerarR(int tipo)
        {
            #region DECLARACION VARIABLES

            DataTable ds = null;
            DataTable dsmodulo = new DataTable();
            string construccion, proceso;
            #endregion

            #region DATATABLE PARA REGISTRAR INCENTIVOS POR MODULOS
            DataTable dsInc = new DataTable();

            dsInc.Columns.Add("modulo", typeof(int));
            dsInc.Columns.Add("codigoDepto", typeof(int));
            dsInc.Columns.Add("estilo", typeof(int));
            dsInc.Columns.Add("construccion", typeof(string));
            dsInc.Columns.Add("produccion", typeof(string));
            dsInc.Columns.Add("proceso", typeof(string));
            dsInc.Columns.Add("meta", typeof(decimal));
            dsInc.Columns.Add("incentivo", typeof(decimal));
            dsInc.Columns.Add("Layout", typeof(int));
            dsInc.Columns.Add("MontoTotal", typeof(decimal));
            dsInc.Columns.Add("Eficiencia", typeof(decimal));
            dsInc.Columns.Add("MetasDias", typeof(decimal));

            #endregion

            #region DATATABLE PARA REGISTRAR INGRESOS EGRESOS POR EMPLEADO
            DataTable dsIncIE = new DataTable();

            dsIncIE.Columns.Add("Codigo", typeof(int));
            dsIncIE.Columns.Add("tipo", typeof(int));
            dsIncIE.Columns.Add("detalle", typeof(string));
            dsIncIE.Columns.Add("tipoCalc", typeof(int));
            dsIncIE.Columns.Add("Cantidad", typeof(decimal));
            dsIncIE.Columns.Add("Valor", typeof(decimal));
            dsIncIE.Columns.Add("Observacion", typeof(string));
            Session["IngDed"] = dsIncIE;

            #endregion

            //PARAMETROS DE FECHAS NECESARIOS
            DateTime fechaini = Convert.ToDateTime(txtFechaIni.Text);
            DateTime fechafin = Convert.ToDateTime(txtFechaFin.Text);

            #region SE OBTIENEN LAS LISTAS DE INCENTIVOS, LAS TABLAS DE PRODUCCION X MODULO Y LOS DATOS DE TODOS LOS EMPLEADOS

            dsmodulo = Neg_Incentivos.obtenerModulos();
            Session["DiasMetasModulos"] = dsmodulo;
            prodcmod = Neg_Incentivos.PRODUCCIONXMODULOList(fechaini, fechafin);
            Session["PRODUCCIONXMODULO"] = prodcmod;

            li = Neg_Incentivos.IncentivosList();
            Session["TABLAINCENTIVOS"] = li;

            lt = Neg_Marca.ObtenerEmpleadosParaIncentivosP(fechaini, fechafin, 3, 1);
            Session["EMPLEADOS"] = lt;

            EmpleadosOP = Neg_Incentivos.EmpleadoOperacion();
            Session["EMPLEADOSOP"] = EmpleadosOP;
            #endregion

            param = Neg_Incentivos.ParametosIngresosDeduccionesIncentivos();
            Session["PARAM"] = param;

            la = Neg_Amonestaciones.getamonestaciones(fechaini, fechafin);
            Session["AMONESTACIONES"] = la;

            if (dsmodulo.Rows.Count > 0)
            {
                int estilom = 0;
                decimal produccionm;
                string construccionm = "";

                foreach (DataRow item in dsmodulo.Rows)
                {
                    estilom = 0; meta = 0; produccionm = 0;
                    construccionm = "";
                    incentivo = 0;
                    eficienciai = 0;
                    MetaDias = 0;
                    List<Neg_Incentivos.ProdXmod> MOD = (List<Neg_Incentivos.ProdXmod>)(from i in prodcmod where i.Modulo.ToString().Trim().Equals(item["Modulo"].ToString().Trim()) select i).ToList();
                    if (MOD.Count > 0)
                    {
                        //SIN CONTIENE MAS DE 1 REGISTRO INDICA QUE TIENE PRODUCCION DE MAS DE UN ESTILO  CONTRUCCION
                        if (MOD.Count > 1)
                        {
                            Neg_Incentivos.ProdXmod PMOD = (Neg_Incentivos.ProdXmod)(from i in MOD orderby i.Produccion descending select i).FirstOrDefault();
                            produccionm = (from i in MOD select i.Produccion).Sum();
                            construccionm = PMOD.Construccion.ToString().Trim();
                            estilom = PMOD.EstiloP;
                        }
                        //SI SOLO CONTIENE UN REGISTRO
                        else
                        {
                            produccionm = MOD[0].Produccion;
                            construccionm = MOD[0].Construccion.ToString().Trim();
                            estilom = MOD[0].EstiloP;
                        }

                    }

                    li = (List<Neg_Incentivos.Incentivos>)Session["TABLAINCENTIVOS"];
                    Neg_Incentivos.Incentivos nuevoincentivo = (Neg_Incentivos.Incentivos)(from incentivos in li where ((incentivos.Meta5 / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString()))) <= produccionm && incentivos.Construccion.Trim().Equals(construccionm) orderby incentivos.Meta5 descending select incentivos).FirstOrDefault();
                    if (nuevoincentivo != null)
                    {
                        double metac = double.Parse(((nuevoincentivo.Meta5 / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString()))).ToString());
                        double incentivoc = double.Parse((((nuevoincentivo.Incentivo / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString())))).ToString());
                        double eficienciac = double.Parse(nuevoincentivo.Eficiencia.ToString());//double.Parse((((nuevoincentivo.Eficiencia / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString())))).ToString());
                        double monto = incentivoc * nuevoincentivo.Layout;


                        dsInc.Rows.Add(item["Modulo"].ToString().Trim(), item["codigoDepto"].ToString().Trim(), estilom, construccionm, produccionm, nuevoincentivo.Proceso.ToString().Trim(), metac, incentivoc, nuevoincentivo.Layout, monto, eficienciac, item["MetadDiasIncentivo"]);
                    }
                }

            }

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();
            //ds = Neg_Incentivos.MontoTotalIncentivos(fechaini, fechafin);
            ds = dsInc;

            if (tipo == 1)
            {
                if (ds.Rows.Count > 0)
                {
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/IncentivoLayout.rdlc");
                    ReportDataSource source = new ReportDataSource("DataSet1", ds);
                    ReportViewer1.LocalReport.DataSources.Add(source);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
            else if (tipo == 2)
            {

                //SE RECORRE CADA MODULO QUE ALCANZO META PARA OBTENER A SUS INTEGRANTES POR MODULOS
                foreach (DataRow row in ds.Rows)
                {
                    modulo = int.Parse(row["Modulo"].ToString());
                    estilo = int.Parse(row["estilo"].ToString());
                    Codigo_depto = int.Parse(row["codigoDepto"].ToString());
                    construccion = row["construccion"].ToString();
                    produccion = decimal.Parse(row["produccion"].ToString());
                    proceso = row["proceso"].ToString();
                    meta = decimal.Parse(row["meta"].ToString());
                    incentivo = decimal.Parse(row["Incentivo"].ToString());
                    eficienciai = decimal.Parse(row["Eficiencia"].ToString());
                    MetaDias = decimal.Parse(row["MetasDias"].ToString());
                    //SE OBTIENEN LOS EMPLEADOS QUE PERTENECEN A ESE MODULO
                    Empleados = lt.Where(x => x.codigo_depto.Equals(Codigo_depto)).ToList();

                    List<Neg_Incentivos.IncentivoEmp> l = Neg_Incentivos.ArmarDatosIncetivosxEmpleado(Empleados, construccion, proceso, EmpleadosOP, param, modulo, estilo, produccion, meta, incentivo, eficienciai, MetaDias);
                    foreach (var item in l)
                    {
                        ListIE.Add(item);
                    }
                    Session["DTIncentivosSP"] = ListIE;

                }

                bool mensaje = false;

                if (ListIE.Count > 0)
                {
                    if (cbDedDZ.Checked)
                    {
                        mensaje = true;
                        ProcesarDZ();
                    }
                    if (cbIngDed.Checked)
                    {
                        ProcesarIngDd();
                    }
                    if (!mensaje)
                    {
                        Label2.Visible = false;
                    }
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PagoIncentivo.rdlc");
                    ReportDataSource source = new ReportDataSource("DataSet1", ListIE);
                    ReportViewer1.LocalReport.DataSources.Add(source);
                    Session["DTIncentivosSP"] = ListIE;//ESTA VARIALE DE SESSION ALMACENA LOS MONTOS DE INCENTIVOS POR MODULOS
                    ReportViewer1.LocalReport.Refresh();
                    DataTable dtInD = (DataTable)Session["IngDed"];
                    btAlicarPlanilla.Visible = true;
                }
            }



        }
        #endregion

        protected void cbIngDed_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIngDed.Checked)
            {
                divID.Visible = true;
                divIDGrid.Visible = true;

            }
            else
            {
                divID.Visible = false;
                divIDGrid.Visible = false;

            }

        }

        protected void cbDedDZ_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDedDZ.Checked)
            {

                divDDZ.Visible = true;
                divDDZGrid.Visible = true;
            }
            else
            {
                divDDZ.Visible = false;
                divDDZGrid.Visible = false;
            }
        }

        protected void btnCargarID_Click(object sender, EventArgs e)
        {
            if (fuIngrDed.HasFile)
            {
                CargarArchivo("fuIngrDed", "IngDD", "gvINGDD");
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                fuDDZ.Focus();
            }
        }

        public void CargarArchivo(string nameFU, string NombreVS, string nombreGV)
        {

            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
            FileUpload FU = (FileUpload)cph.FindControl(nameFU);
            GridView gv = (GridView)cph.FindControl(nombreGV);

            if (FU.HasFile)
            {

                string FileName = Path.GetFileName(FU.PostedFile.FileName);
                string Extension = Path.GetExtension(FU.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FileName);
                string fileLocation = HttpContext.Current.Server.MapPath(".").ToString() + @"\Trash\" + FileName;
                FU.SaveAs(FilePath);

                if (Extension == ".xls")
                {

                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                }
                else if (Extension == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                connectionString = String.Format(connectionString, FilePath);
                OleDbConnection connExcel = new OleDbConnection(connectionString);
                OleDbCommand cmdExcel = new OleDbCommand();
                cmdExcel.CommandType = System.Data.CommandType.Text;
                cmdExcel.Connection = connExcel;
                OleDbDataAdapter oda = new OleDbDataAdapter();
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;

                if (NombreVS == "DZ")
                {

                    oda.Fill(dtDD);
                    dtDD.Columns.Add("Comentario", typeof(string));
                    Session["datos"] = dtDD;
                    gv.DataSource = dtDD;
                    gv.Columns[4].Visible = false;
                }

                else if (NombreVS == "IngDD")
                {

                    oda.Fill(dtID);
                    dtID.Columns.Add("Comentario", typeof(string));
                    Session["IngDD"] = dtID;
                    gv.DataSource = dtID;
                    gv.Columns[5].Visible = false;
                }

                connExcel.Close();
                gv.DataBind();



            }
        }
        // CODIGO DE CARGA Y PROCESO DE EXCEL DE dz

        public void ProcesarDZ()
        {
            // bool SumoyResto = false;
            int periodo = 0;

            if (txtPeriodo.Text != "")
            {
                periodo = int.Parse(txtPeriodo.Text);
            }
            int semana = int.Parse(ddlTipo.SelectedValue);
            if (Session["datos"] != "")
            {
                dt = (DataTable)Session["datos"];

                #region

                DataTable dtRegistroTrasDZ = new DataTable();

                dtRegistroTrasDZ.Columns.Add("Codigo", typeof(int));
                dtRegistroTrasDZ.Columns.Add("Cantidad", typeof(decimal));
                dtRegistroTrasDZ.Columns.Add("Operacion", typeof(int));
                Session["DTTRaslados"] = dtRegistroTrasDZ;
                #endregion
                #region

                if (dt.Rows.Count > 0)
                {
                    //Bind Data to GridView
                    // dt.Columns.Add("Comentario", typeof(string));
                    ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
                    int contadorop = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        contadorop = 0;
                        Session["contadorop"] = 0;
                        //comentario = "";
                        DZ = 0;

                        if (row["Codigosumar"].ToString() != "" && row["Codigorestar"].ToString() != "")
                        {
                            codigo1 = Convert.ToInt32(row["Codigosumar"].ToString());
                            codigo2 = Convert.ToInt32(row["Codigorestar"].ToString());
                            DZ = Convert.ToDecimal(row["DZ"].ToString());
                            observacionTDZ = row["observacion"].ToString();

                            //bool Resto = false;

                            //SE BUSCA POR CODIGO PARA SABER SI TIENE SIGNADO UN INCENTIVO
                            //CODIGO A SUMARLE
                            Neg_Incentivos.IncentivoEmp dt2 = (from myrow in ListIE where myrow.Codigo.Equals(codigo1) select myrow).SingleOrDefault();
                            //CODIGO A RESTARLE
                            Neg_Incentivos.IncentivoEmp dt1 = (from myrow in ListIE where myrow.Codigo.Equals(codigo2) select myrow).SingleOrDefault();
                            Session["Resto"] = false;

                            Neg_Incentivos.SumaREstaDZ(dt1, codigo1, codigo2, 2, DZ);
                            if (Convert.ToBoolean(Session["Resto"]))
                            {

                                row["Comentario"] = Neg_Incentivos.SumaREstaDZ(dt2, codigo1, codigo2, 1, DZ);
                                contadorop = int.Parse(Session["contadorop"].ToString());

                                if (contadorop == 2)
                                {
                                    dtRegistroTrasDZ.Rows.Add(codigo1, DZ, 1);
                                    dtRegistroTrasDZ.Rows.Add(codigo2, DZ, 2);
                                    Session["DTTRaslados"] = dtRegistroTrasDZ;

                                }
                            }
                            else
                            {
                                row["Comentario"] = "NO EXISTEN DZ PARA RESTAR NI SUMAR";
                            }
                        }

                        else
                        {
                            if (row["Codigorestar"].ToString() != "")
                            {

                                codigo2 = Convert.ToInt32(row["Codigorestar"].ToString());
                                DZ = Convert.ToDecimal(row["DZ"].ToString());
                                observacionTDZ = row["observacion"].ToString();
                                Neg_Incentivos.IncentivoEmp dt1 = (from myrow in ListIE where myrow.Codigo.Equals(codigo2) select myrow).SingleOrDefault();
                                Session["Resto"] = false;
                                Neg_Incentivos.SumaREstaDZ(dt1, 0, codigo2, 2, DZ);
                                if (Convert.ToBoolean(Session["Resto"]))
                                {
                                    dtRegistroTrasDZ.Rows.Add(codigo2, DZ, 2);
                                    Session["DTTRaslados"] = dtRegistroTrasDZ;
                                }

                            }
                        }

                    }

                    //  gvReajusteDZ.Caption = Path.GetFileName(FilePath);
                    gvReajusteDZ.Columns[4].Visible = true;
                    gvReajusteDZ.DataSource = dt;
                    gvReajusteDZ.DataBind();



                    Session["datos"] = dt; // ESTA VARIABLE DE SESSION ALMACENA LA TABLA DE DZ Y CODIGOS A SUMAR Y RESTAR
                    Session["DTIncentivosSP"] = ListIE;//ESTA VARIALE DE SESSION ALMACENA LOS MONTOS DE INCENTIVOS POR MODULOS

                    if (dt.Rows.Count > 0)
                    {
                        Neg_Incentivos.IncentivoHistoricoDZTrasDelete(periodo, semana);
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Codigosumar"].ToString() != "" && row["Codigorestar"].ToString() != "")
                            {
                                codigo1 = Convert.ToInt32(row["Codigosumar"].ToString());
                                codigo2 = Convert.ToInt32(row["Codigorestar"].ToString());
                            }
                            else
                            {
                                codigo1 = 0;
                                codigo2 = Convert.ToInt32(row["Codigorestar"].ToString());
                            }
                            DZ = Convert.ToDecimal(row["DZ"].ToString());
                            observacionTDZ = row["observacion"].ToString();
                            string comentario = row["Comentario"].ToString();
                            Neg_Incentivos.IncentivoHistoricoDZTrasInsert(periodo, semana, codigo2.ToString(), codigo1.ToString(), DZ, observacionTDZ.ToString(), comentario);

                        }
                    }


                }
            }
                #endregion
        }
        public void ProcesarIngDd()
        {
            if (Session["IngDD"] != "")
            {
                int tipoINGDD = 0, tipoCalculo = 0;
                decimal valor = 0, cantidad=0;
                string detalle = "";
                dt = (DataTable)Session["IngDD"];


                if (dt.Rows.Count > 0)
                {
                    //Bind Data to GridView
                    // dt.Columns.Add("Comentario", typeof(string));
                    ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;

                    //SE OBTIENEN PRIMERO LOS INGRESOS POR .5 0 .125
                    DataTable dtop = dt.Copy();
                    DataView dtview1 = dtop.DefaultView;
                    dtview1.RowFilter = "observacion='OP'";
                   
                    DataTable dt2 = dt.Copy();
                    DataView dtview = dt2.DefaultView;


                    dtview.Sort = "TipoIDD ASC";


                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Codigo"].ToString() != "")
                        {

                            try
                            {
                                

                                ingresoNumero = 0; deduccionnumerico = 0;
                                ingresoporcentual = 0; deduccionporcentual = 0;
                                //comentario = "";
                                DZ = 0;
                                codigo1 = Convert.ToInt32(row["Codigo"].ToString());
                                tipoINGDD = Convert.ToInt32(row["TipoIDD"].ToString());
                                tipoCalculo = Convert.ToInt32(row["TipoCal"].ToString());
                                valor = Convert.ToDecimal(row["Valor"].ToString());
                                observacionID = row["observacion"].ToString();
                                detalle = observacionID;
                                
                                if (observacionID == "")
                                    detalle = " otros " ;
                                Neg_Incentivos.IncentivoEmp dt1 = (from myrow in ListIE where myrow.Codigo.Equals(codigo1) select myrow).SingleOrDefault();

                                //SI EMPLEADO EXISTE O PERTENECE A UN MODULO QUE ALCANZO META
                                if (dt1 != null)
                                {
                                    DataTable dtInD = (DataTable)Session["IngDed"];
                                    if (observacionID.ToLower() == "op")
                                    {
                                        detalle = (valor / 100).ToString()+ dt1.Operacion.ToString();
                                    }
                                    //ES UN INGRESO
                                    if (tipoINGDD == 1)
                                    {
                                        //SI ES PORCENTUAL
                                        if (tipoCalculo == 1)
                                        {
                                            ingresoporcentual += valor;
                                            cantidad = ((dt1.Incentivo * ingresoporcentual) / 100);

                                        }
                                        else if (tipoCalculo == 2)
                                        {
                                            ingresoNumero += Convert.ToDecimal(valor.ToString());
                                            cantidad = ingresoNumero;
                                        }
                                    }

                                        //SI ES DEDUCCION
                                    else if (tipoINGDD == 2)
                                    {
                                        if (tipoCalculo == 1)
                                        {
                                            deduccionporcentual += valor;
                                            cantidad = ((dt1.Incentivo * deduccionporcentual) / 100);
                                        }
                                        else if (tipoCalculo == 2)
                                        {
                                            deduccionnumerico += Convert.ToDecimal(valor.ToString());
                                            cantidad = deduccionnumerico;
                                        }

                                    }


                                    dt1.IngresoNumerico = ingresoNumero;
                                    if (dt1.IngresoNumerico > 0)
                                    {
                                        dt1.DetalleIngreso += " Ingreso " + detalle + " de: C$" + ingresoNumero;
                                    }
                                    dt1.IngresoPorcentual = ingresoporcentual;

                                    if (dt1.IngresoPorcentual > 0)
                                    {
                                        dt1.DetalleIngreso += " Ingreso " + detalle + " de: " + ingresoporcentual + "% ";
                                    }
                                    dt1.DeduccionNumerica = deduccionnumerico;

                                    if (dt1.DeduccionNumerica > 0)
                                    {
                                        dt1.DetalleEgreso += " Egreso " + detalle + " de: C$" + deduccionnumerico;
                                    }
                                    dt1.DeduccionPorcentual = deduccionporcentual;

                                    if (dt1.DeduccionPorcentual > 0)
                                    {
                                        dt1.DetalleEgreso += " Egreso " + detalle + " de:" + deduccionporcentual + "% ";
                                    }


                                    dt1.Ingreso = dt1.Ingreso + (((dt1.Incentivo * ingresoporcentual) / 100) + ingresoNumero);
                                    decimal incentivoIngresos = dt1.Incentivo + dt1.Ingreso;
                                    dt1.Deduccion = dt1.Deduccion + (((incentivoIngresos * deduccionporcentual) / 100) + deduccionnumerico);


                                    dt1.Total = incentivoIngresos - dt1.Deduccion;

                                    if (dt1.Total < 0)
                                    {
                                        dt1.Total = 0;
                                    }

                                    dtInD.Rows.Add(codigo1, tipoINGDD, detalle, tipoCalculo, valor, cantidad, observacionID);
                                    Session["IngDed"] = dtInD;
                                }
                            }
                            catch
                            {

                            }
                        }

                    }

                    //  gvReajusteDZ.Caption = Path.GetFileName(FilePath);
                    gvINGDD.Columns[5].Visible = true;
                    gvINGDD.DataSource = dt;
                    gvINGDD.DataBind();
                    // btnProcesar.Visible = true;
                    Session["IngDD"] = dt; // ESTA VARIABLE DE SESSION ALMACENA LA TABLA DE DZ Y CODIGOS A SUMAR Y RESTAR
                    Session["DTIncentivosSP"] = ListIE;//ESTA VARIALE DE SESSION ALMACENA LOS MONTOS DE INCENTIVOS POR MODULOS
                }
            }

        }

        protected void btAlicarPlanilla_Click(object sender, EventArgs e)
        {
            int codigo, modulo, amonestacion, diaslaborados, diasausencia, ausenciaj, ausenciaI;
            decimal produccion, metaalcanzada, totalA, eficiencia, incentivoM, totalI, totalE, incentivo;
            if (txtPeriodo.Text != "")
            {
                int periodo = int.Parse(txtPeriodo.Text);
                int semana = int.Parse(ddlTipo.SelectedValue);
                Neg_Incentivos.IncentivosHistoricoDelete(periodo, semana);
                Neg_Incentivos.IncentivoIngDedccLOGDelete(periodo, semana);
                ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
                DataTable dtInD = (DataTable)Session["IngDed"];

                ///////////////////PROCESO DE REGISTRO DE INGRESOS Y DEDUCCIONES WALLMART
                DataTable dt;
                Neg_Planilla Neg_Planilla = new Neg_Planilla();
                Neg_DevYDed NDevyDed = new Neg_DevYDed();

                DataTable dt1 = new DataTable();
                //Estructura de la tabla Meses.
                dt1.Columns.Add("tipo");
                dt1.Columns.Add("codigoempleado");
                dt1.Columns.Add("semana");
                dt1.Columns.Add("idtipo");
                dt1.Columns.Add("periodo");
                dt1.Columns.Add("valor");

                decimal valorIngresoAsociado = 0;

                dt = Neg_Planilla.cargarUltPeriodoAbieCat(1);
                int periodopago = 0;
                if (dt.Rows.Count > 0)
                {
                    periodopago = Convert.ToInt32(dt.Rows[0]["nperiodo"].ToString());
                }
                else
                {
                    periodopago = 0;
                }
                int cont = 0;
                //////////////
                foreach (var item in ListIE)
                {                   
                    codigo = int.Parse(item.Codigo.ToString());
                    modulo = int.Parse(item.Modulo.ToString());
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
                    string user = Convert.ToString(this.Page.Session["usuario"]);

                    Neg_Incentivos.IncentivosHistoricoInsert(codigo, modulo, item.Estilo.ToString(), item.Operacion, item.Construccion, produccion, metaalcanzada, amonestacion, diaslaborados, diasausencia, ausenciaj, ausenciaI, totalA, eficiencia, incentivoM, totalI, totalE, incentivo, periodo, semana, user);

                    //bloque de codigo que inserta ingresos asociados a deducirse del ingreso bruto de incentivos
                    dt1.Rows.Add(1, codigo, semana, 4, periodopago, Math.Ceiling(incentivo));

                    valorIngresoAsociado = NDevyDed.RegistrarIngresoAsociado(Math.Ceiling(incentivo), dt1, cont, user);

                    if (!NDevyDed.InsertarIngrDeduc(1, codigo, semana, 4, periodopago, (Math.Ceiling(incentivo) - valorIngresoAsociado), user))
                    {
                        throw new Exception("Error al insertar registro de ingreso");
                    }
                    cont++;
                }

                foreach (DataRow dr in dtInD.Rows)
                {

                    Neg_Incentivos.IncentivoIngDedccLOGInsert(int.Parse(dr["Codigo"].ToString()), periodo, semana, int.Parse(dr["tipo"].ToString()), dr["detalle"].ToString(), int.Parse(dr["tipoCalc"].ToString()), decimal.Parse(dr["Cantidad"].ToString()), decimal.Parse(dr["Valor"].ToString()), dr["Observacion"].ToString());

                }

            }


        }

        protected void cbSeleccion_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSeleccion.Checked)
            {
                panelDZ.Visible = true;
                panelID.Visible = true;

            }
            else
            {
                panelDZ.Visible = false;
                panelID.Visible = false;

            }
        }
    }

}
