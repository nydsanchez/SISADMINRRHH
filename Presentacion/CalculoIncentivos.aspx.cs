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

namespace NominaRRHH.Presentacion
{
    public partial class CalculoIncentivos : System.Web.UI.Page
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        Neg_Marca Neg_Marca = new Neg_Marca();
        Neg_Amonestaciones Neg_Amonestaciones = new Neg_Amonestaciones();
        Neg_Periodo Neg_Periodo = new Neg_Periodo();
        Neg_Empleados NegEmp = new Neg_Empleados();
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

        int totaldiasTrab;
        int NdiasL = 0, NdiasJ = 0;
        float TotalHorasLaboradas, TotalHoraSemana;

        int codigo1 = 0, codigo2 = 0;
        decimal DZ = 0;
        string connectionString = "", observacionTDZ = "", observacionID = "";
        decimal aqlmeta = 0, aqlsemana = 0;
        decimal ingresoNumero = 0, deduccionnumerico = 0;
        decimal ingresoporcentual = 0, deduccionporcentual = 0;
        string modulo = "0";
        int estilo = 0;
        int Codigo_depto = 0;
        decimal MetaDias = 0, meta = 0, produccion = 0, AQLPer = 0;
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
                //lt = Neg_Marca.ObtenerEmpleadosParaIncentivosP(Convert.ToDateTime("2019-06-24"), Convert.ToDateTime("2019-06-30"), 3, 1, 10);


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
                Session["Autorizaciones"] = "";
                Session["AQL"] = "";

                Session["FormatoCorrectoTD"] = "True";

                pnlDZ.Visible = false;

                divID.Visible = false;
                divIDGrid.Visible = false;

                divDDZ.Visible = false;
                divDDZGrid.Visible = false;
                btAlicarPlanilla.Visible = false;

                divAQL.Visible = false;
                divAQLGrid.Visible = false;

                panelDZ.Visible = false;
                panelID.Visible = false;
                pnlAQL.Visible = false;

                //dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(lista[0].Periodo);

                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.cargarUltPeriodoAbieCat(1, 1, 3);

                int periodo = 0;
                if (dtPeriodo.Rows.Count > 0)
                {
                    periodo = dtPeriodo[0].nperiodo;
                    Session["fechaini"] = dtPeriodo[0]["fechaini"].ToString();
                    Session["fechafin"] = dtPeriodo[0]["fechafin2"].ToString();
                    Session["ubicacion"] = dtPeriodo[0].ubicacion;
                }

                //  txtPeriodo.Text = periodo.ToString();

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
            //ESTA VARIABLE ES PARA VALIDAR QUE SI SE CARGÓ UN EXCEL DE DEDUCCION DE DZ, HAYAN ESPECIFICADO LA FECHA Y SEA UNA FECHA VALIDA
            bool procesar = true;
            //SE CONSULTA SI SE SELECCIONO LA CARGA DE ARCHIVOS CON DEDUCCION DE DZ
            if (cbDedDZ.Checked)
            {
                //SI LA VARIABLE DE SESSION ES FALSE, INDICA QUE HUBO UNA O MAS FECHAS EN MAL FORMATO O FILAS SIN FECHA ESPECIFICADA
                if (!Convert.ToBoolean(Session["FormatoCorrectoTD"].ToString()))
                {
                    //SE NIEGO EL DERECHO A PROCESAR LA PLANILLA DE INCENTIVOS HASTA QUE SE REALICE LA CORRECCION O SE DECIDA NO CARGAR 
                    procesar = Convert.ToBoolean(Session["FormatoCorrectoTD"].ToString());
                }
            }
            if (procesar)
            {
                GenerarR(2);
                pnlDZ.Visible = true;
            }
        }
        #endregion

        #region METODOS

        //public bool validar()
        //{

        //    int c = 0;
        //    if (txtFechaIni.Text.Trim() == "" || txtFechaFin.Text.Trim() == "")
        //    {
        //        c = c + 1;
        //        txtFechaFin.Focus();
        //        lblAlert.Text = "Favor Ingrese una Fecha de Inicio y una fecha Fin";
        //    }

        //    else
        //    {
        //        if (Convert.ToDateTime(txtFechaIni.Text.Trim()) > Convert.ToDateTime(txtFechaFin.Text.Trim()))
        //        {
        //            lblAlert.Text = "Rango de Fechas invalidos";
        //            txtFechaIni.Focus();
        //            c = c + 1;
        //        }

        //    }

        //    if (c > 0)
        //    {
        //        alertValida.Visible = true;
        //        lblAlert.Visible = true;

        //        return false;
        //    }
        //    else { return true; }

        //}

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

        protected void gvReajusteDZ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }

        protected void gvReajusteDZ_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dt = (DataTable)Session["datos"];
            gvReajusteDZ.PageIndex = e.NewPageIndex;
            gvReajusteDZ.DataBind();
            gvReajusteDZ.DataSource = dt;
            gvReajusteDZ.DataBind();
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
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

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {

                divAQL.Visible = true;
                divAQLGrid.Visible = true;
            }
            else
            {

                divAQL.Visible = false;
                divAQLGrid.Visible = false;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (fuAQL.HasFile)
            {
                CargarArchivo("fuAQL", "AQL", "gvAQL");
            }
            else
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = "Favor Seleccione un archivo";
                fuAQL.Focus();

            }
        }

        public void GenerarR(int tipo)
        {
            #region DECLARACION VARIABLES
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = null;
            DataTable dsmodulo = new DataTable();
            string construccion, proceso;
            #endregion

            #region DATATABLE PARA REGISTRAR INCENTIVOS POR MODULOS
            DataTable dsInc = new DataTable();

            dsInc.Columns.Add("modulo", typeof(string));
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
            dsInc.Columns.Add("AQLMeta", typeof(decimal));
            dsInc.Columns.Add("AQLSemana", typeof(decimal));

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

            //  System.Globalization.CultureInfo MyCultureInfo = new System.Globalization.CultureInfo("en-US");
            DateTime fechaini, fechafin;
            if (txtPeriodo.Text != "")
            {
                lblAlert.Text = "";
                lblAlert.Visible = false;
                dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(int.Parse(txtPeriodo.Text));

                if (dtPeriodo.Rows.Count > 0)
                {
                    lblAlert.Text = "";
                    lblAlert.Visible = false;

                    if (ddlTipo.SelectedValue.ToString() == "1")
                    {
                        //fechaini = Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini"].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-Us").DateTimeFormat);
                        fechaini = Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini"].ToString());
                        fechafin = Convert.ToDateTime(dtPeriodo.Rows[0]["fechafin"].ToString());
                        //    fechafin = Convert.ToDateTime(dtPeriodo.Rows[0]["fechafin"].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-Us").DateTimeFormat);

                    }
                    else
                    {
                        // fechaini = Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini2"].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-Us").DateTimeFormat);
                        //// Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini2"].ToString());
                        // fechafin = Convert.ToDateTime(dtPeriodo.Rows[0]["fechafin2"].ToString(), System.Globalization.CultureInfo.GetCultureInfo("en-Us").DateTimeFormat);
                        // //Convert.ToDateTime(dtPeriodo.Rows[0]["fechafin2"].ToString());

                        fechaini = Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini2"].ToString());
                        fechafin = Convert.ToDateTime(dtPeriodo.Rows[0]["fechafin2"].ToString());
                    }




                    //PARAMETROS DE FECHAS NECESARIOS


                    #region SE OBTIENEN LAS LISTAS DE INCENTIVOS, LAS TABLAS DE PRODUCCION X MODULO Y LOS DATOS DE TODOS LOS EMPLEADOS





                    dsmodulo = Neg_Incentivos.obtenerModulos();
                    Session["DiasMetasModulos"] = dsmodulo;
                    prodcmod = Neg_Incentivos.PRODUCCIONXMODULOList(fechaini, fechafin);
                    Session["PRODUCCIONXMODULO"] = prodcmod;

                    li = Neg_Incentivos.IncentivosList();
                    Session["TABLAINCENTIVOS"] = li;

                    //lt = Neg_Marca.ObtenerEmpleadosParaIncentivosP(fechaini, fechafin, 3, 1, userDetail.getIDEmpresa());
                    //Session["EMPLEADOS"] = lt;

                    EmpleadosOP = Neg_Incentivos.EmpleadoOperacion();
                    Session["EMPLEADOSOP"] = EmpleadosOP;
                    #endregion

                    param = Neg_Incentivos.ParametosIngresosDeduccionesIncentivos(0);
                    Session["PARAM"] = param;

                    la = Neg_Amonestaciones.getamonestaciones(fechaini, fechafin);
                    Session["AMONESTACIONES"] = la;

                    DataTable todosemple = NegEmp.pln_empleadosHistoricoALL(3, fechaini, 1);
                    Session["todosemp"] = todosemple;



                    if (dsmodulo.Rows.Count > 0)
                    {
                        int estilom = 0;
                        decimal produccionm;
                        string construccionm = "";
                        //DataTable dtAQL = (DataTable)Session["AQL"];
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
                            produccionm = Neg_Incentivos.redondeo(produccionm);

                            li = (List<Neg_Incentivos.Incentivos>)Session["TABLAINCENTIVOS"];
                            Neg_Incentivos.Incentivos nuevoincentivo = (Neg_Incentivos.Incentivos)(from incentivos in li where ((incentivos.Meta5 / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString()))) <= produccionm && incentivos.Construccion.Trim().Equals(construccionm) orderby incentivos.Meta5 descending select incentivos).FirstOrDefault();
                            if (nuevoincentivo != null)
                            {

                                double metac = double.Parse(((nuevoincentivo.Meta5 / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString()))).ToString());
                                double incentivoc = double.Parse((((nuevoincentivo.Incentivo / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString())))).ToString());
                                double eficienciac = double.Parse(nuevoincentivo.Eficiencia.ToString());//double.Parse((((nuevoincentivo.Eficiencia / 5) * (decimal.Parse(item["MetadDiasIncentivo"].ToString())))).ToString());
                                double monto = incentivoc * nuevoincentivo.Layout;



                                dsInc.Rows.Add(item["Modulo"].ToString().Trim(), item["codigoDepto"].ToString().Trim(), estilom, construccionm, produccionm, nuevoincentivo.Proceso.ToString().Trim(), metac, incentivoc, nuevoincentivo.Layout, monto, eficienciac, item["MetadDiasIncentivo"], aqlmeta, aqlsemana);

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
                        // DataTable obtenerpermisos = np.obtenerpermisos(fechaini, fechafin);
                        //SE RECORRE CADA MODULO QUE ALCANZO META PARA OBTENER A SUS INTEGRANTES POR MODULOS



                        foreach (DataRow row in ds.Rows)
                        {
                            modulo = row["Modulo"].ToString();
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

                            decimal AQLSemana = decimal.Parse(row["AQLSemana"].ToString());
                            decimal AQLMeta = decimal.Parse(row["AQLMeta"].ToString());

                            if (AQLSemana > 0)
                            {
                                AQLPer = AQLMeta / AQLSemana;
                            }
                            else
                            {
                                AQLPer = 0;
                            }

                            List<Neg_Incentivos.IncentivoEmp> l = Neg_Incentivos.ArmarDatosIncetivosxEmpleado(Empleados, construccion, proceso, EmpleadosOP, param, modulo, estilo, produccion, meta, incentivo, eficienciai, MetaDias, AQLPer);
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


                            DataTable dtEmpIncFijo = Neg_Incentivos.EmpleadosPagosXOperacionGetPagossFijos();
                            Neg_Incentivos.GenerarPagosProteccion(dtEmpIncFijo, int.Parse(ddlTipo.SelectedValue.ToString()), fechaini, fechafin, userDetail.getIDEmpresa(), int.Parse(txtPeriodo.Text)); //DataTable dtEmpIncFijo, int semana, DateTime fechaini, DateTime fechafin, int idempresa)
                            ListIE = (List<Neg_Incentivos.IncentivoEmp>)Session["DTIncentivosSP"];


                            if (cbIngDed.Checked)
                            {
                                ProcesarIngDd();
                            }
                            
                            #region
                            //List<int> EmpleadosIncentivosFijos = new List<int>();
                            //List<Neg_Empleados> lt1 = new List<Neg_Empleados>(); //ESTA LISTA CONTIENE TODOS LOS EMPLEADOS DE LA PLANILLA
                            //lt1 = Neg_Marca.ObtenerHT(fechaini, fechafin, 3, 1, userDetail.getIDEmpresa());
                            //foreach (DataRow item in dtEmpIncFijo.Rows)
                            //{
                            //    //DataView dtvEmplInF = dtEmpIncFijo.Copy().DefaultView;

                            //    //for (int i = 0; i < ListIE.Count; i++)
                            //    //{

                            //    Neg_Incentivos.IncentivoEmp empprueba = (from myrow in ListIE where myrow.Codigo.Equals(int.Parse(item["codigo"].ToString())) select myrow).SingleOrDefault();
                            //    if (empprueba != null)
                            //    {
                            //        foreach (Neg_Incentivos.IncentivoEmp fila in ListIE.Where(w => w.Codigo.Equals ( int.Parse(item["codigo"].ToString()))))
                            //        {
                            //            empprueba.Incentivo = decimal.Parse(item["Semana1"].ToString());
                            //            empprueba.IngresoNumerico = decimal.Parse(item["Semana" + ddlTipo.SelectedValue.ToString().Trim()].ToString());
                            //        }
                            //    }

                            //    else
                            //    {
                            //        Neg_Incentivos.IncentivoEmp emp2 = new Neg_Incentivos.IncentivoEmp();
                            //        Neg_Empleados empleado2 = new Neg_Empleados();
                            //        empleado2 = lt1.Where(x => x.codigo_empleado.Equals(int.Parse(item["codigo"].ToString()))).SingleOrDefault();
                            //        if (empleado2!=null)
                            //        {
                            //            string dep = empleado2.departamento;
                            //            bool esint = false;
                            //            foreach (var ch in dep)
                            //            {
                            //                if (Char.IsNumber(ch))
                            //                {
                            //                    esint = true;
                            //                }
                            //            }

                            //            if (esint)
                            //            {
                            //                string dep2 = dep;
                            //                if (dep.Length > 2)
                            //                {
                            //                    dep2 = dep.Substring(7, 2);
                            //                    emp2.Modulo = int.Parse(dep2);
                            //                }
                            //                else
                            //                {
                            //                    emp2.Modulo = int.Parse(dep);
                            //                }

                            //            }
                            //            else
                            //            {
                            //                emp2.Modulo = 0;
                            //            }
                            //            emp2.NombreCompleto = empleado2.nombrecompleto;
                            //            emp2.Estilo = 0;
                            //            emp2.Codigo = int.Parse(item["codigo"].ToString());
                            //            emp2.Construccion = "";
                            //            emp2.Produccion = 0;
                            //            emp2.Proceso = "";
                            //            emp2.Meta = 0;
                            //            emp2.DetalleIngreso = item["TipoPreferencia"].ToString();
                            //            emp2.Incentivo = decimal.Parse(item["Semana"+ddlTipo.SelectedValue.ToString()].ToString());

                            //            DataTable dtHorasT = empleado2.dtHorasT;
                            //            //validacion para no incluir dias con marcas pero fuera del turno
                            //            DataTable diasLaborados = dtHorasT.Select("horasturno>0").CopyToDataTable();
                            //            totaldiasTrab = diasLaborados.Rows.Count;
                            //            Neg_Incentivos.obtenerDiasTrabYAusenciasJ(diasLaborados, Convert.ToDateTime(empleado2.fecheingreso));
                            //            emp2.DiasLaborales = totaldiasTrab;
                            //            emp2.DiasLaborados = NdiasL;
                            //            emp2.DiasJustificados = NdiasJ;
                            //            emp2.DiasAusencias = (totaldiasTrab - NdiasL);
                            //            emp2.Operacion = "";
                            //            emp2.Horasaunsencia = float.Parse("48") - TotalHoraSemana;
                            //            emp2.Eficiencia = 0;
                            //            emp2.Amonestaciones = 0;
                            //            emp2.DiasInjustificados = emp2.DiasAusencias - emp2.DiasJustificados;
                            //            emp2.MetasModulo = 0;
                            //            emp2.TipoIngr = 4;
                            //            emp2.Comentario = "GENERADO DESDE SISTEMA";

                            //            if (emp2.Horasaunsencia < 0)
                            //            {
                            //                emp2.Horasaunsencia = 0;
                            //            }
                            //            Neg_Amonestaciones.Amonestaciones amone = (Neg_Amonestaciones.Amonestaciones)(from tabla in la where tabla.Codigo_empleado.Equals(int.Parse(item["codigo"].ToString())) select tabla).SingleOrDefault();

                            //            if (amone != null)
                            //            {
                            //                emp2.Amonestaciones = amone.Cantidad;
                            //            }


                            //            Neg_Incentivos.IncentivoEmp empIngD = Neg_Incentivos.IngresosDeduccion(emp2, param, 0, 0, 0, 0);

                            //            emp2.DetalleIngreso = emp2.DetalleIngreso;
                            //            emp2.DetalleEgreso = emp2.DetalleEgreso;
                            //            emp2.IngresoNumerico = emp2.IngresoNumerico;
                            //            emp2.IngresoPorcentual = emp2.IngresoPorcentual;
                            //            emp2.DeduccionNumerica = emp2.DeduccionNumerica;
                            //            emp2.DeduccionPorcentual = emp2.DeduccionPorcentual;

                            //            emp2.Ingreso = emp2.Ingreso;
                            //            emp2.Deduccion = emp2.Deduccion;

                            //            emp2.Total = emp2.Total;


                            //            ListIE.Add(emp2);
                            //        }


                            //    }
                            //    // }


                            //}
                            #endregion
                          


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
                else
                {
                    lblAlert.Text = "NO SE HA CREADO EL PERIDO";
                    lblAlert.Visible = true;
                }
            }
            else
            {
                lblAlert.Text = "DIGITE UN PERIODO";
                lblAlert.Visible = true;
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
                fuIngrDed.Focus();
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
                    int contadorMalFormato = 0;
                    oda.Fill(dtDD);
                    connExcel.Close();

                    dtID = dtDD.Copy();
                    dtID.Columns.Add("Comentario", typeof(string));
                    dtID.Columns.Add("Aprobado", typeof(string));
                    if ((dtDD.Columns[0].ToString().ToLower().Trim() == "Codigosumar".ToLower().Trim())
                                        && (dtDD.Columns[1].ToString().ToLower().Trim() == "Codigorestar".ToLower().Trim())
                                        && (dtDD.Columns[2].ToString().Trim().ToLower() == "dz".ToLower().Trim())
                                        && (dtDD.Columns[3].ToString().ToLower().Trim() == "observacion".ToLower().Trim())
                                       )
                    //&& (dtDD.Columns[4].ToString().ToLower().Trim() == "Fecha".ToLower().Trim())

                    {

                        foreach (DataRow item in dtID.Rows)
                        {
                            item["Aprobado"] = "SI";
                            //    try
                            //    {
                            //        if (item["Fecha"].ToString() == "")
                            //        {
                            //            contadorMalFormato++;
                            //            item["Comentario"] = "NO SE ESPECIFICO UNA FECHA.";
                            //            item["Aprobado"] = "NO";
                            //        }
                            //        else
                            //        {
                            //            DateTime fechap = DateTime.Parse(item["Fecha"].ToString());
                            //            DateTime fechaini = DateTime.Parse(txtFechaIni.Text);
                            //            DateTime fechafin = DateTime.Parse(txtFechaFin.Text);
                            //            if (fechap >= fechaini && fechap <= fechafin)
                            //            {

                            //                item["Aprobado"] = "SI";
                            //            }
                            //            else
                            //            {
                            //                item["Comentario"] = "FECHA NO ESTA EN EL RANGO DE LA SEMANA QUE SE REGISTRA";
                            //                item["Aprobado"] = "NO";
                            //            }

                            //        }
                            //    }
                            //    catch
                            //    {
                            //        contadorMalFormato++;

                            //        item["Comentario"] = "LA FECHA NO ES UN FORMATO VALIDO, DEBE SER AÑO-MES'DIA";
                            //        item["Aprobado"] = "NO";
                            //    }

                        }
                        if (contadorMalFormato > 0)
                        {
                            Session["FormatoCorrectoTD"] = "False";
                        }

                        Session["datos"] = dtID;
                        gv.DataSource = dtID;

                    }



                }

                else if (NombreVS == "IngDD")
                {

                    oda.Fill(dtDD);
                    connExcel.Close();

                    dtID = dtDD.Copy();

                    if ((dtDD.Columns[0].ToString().ToLower().Trim() == "Codigo".ToLower().Trim())
                                   && (dtDD.Columns[1].ToString().Trim().ToLower() == "valor".ToLower().Trim())
                        && (dtDD.Columns[2].ToString().Trim().ToLower() == "TipoCal".ToLower().Trim())
                        && (dtDD.Columns[3].ToString().Trim().ToLower() == "observacion".ToLower().Trim()))

                    {
                        dtID.Columns.Add("Comentario", typeof(string));
                        dtID.Columns.Add("Aprobado", typeof(string));
                        Session["IngDD"] = dtID;
                        gv.DataSource = dtID;

                    }


                }

                else if (NombreVS == "AQL")
                {

                    oda.Fill(dtDD);
                    connExcel.Close();

                    dtID = dtDD.Copy();

                    if ((dtDD.Columns[0].ToString().ToLower().Trim() == "Modulo".ToLower().Trim())
                                   && (dtDD.Columns[1].ToString().Trim().ToLower() == "AQL".ToLower().Trim())
                        && (dtDD.Columns[2].ToString().Trim().ToLower() == "AQLMeta".ToLower().Trim()))

                    {
                        dtID.Columns.Add("Total", typeof(string));
                        Session["AQL"] = dtID;
                        gv.DataSource = dtID;

                    }


                }

                //else if (NombreVS == "Autorizacion")
                //{

                //    oda.Fill(dtID);
                //    Session["Autorizaciones"] = dtID;
                //    gv.DataSource = dtID;
                //    gv.Columns[5].Visible = false;
                //}

                gv.DataBind();



            }
        }

        //public void CargarArchivo(string nameFU, string NombreVS, string nombreGV)
        //{

        //    ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("MainContent");
        //    FileUpload FU = (FileUpload)cph.FindControl(nameFU);
        //    GridView gv = (GridView)cph.FindControl(nombreGV);

        //    if (FU.HasFile)
        //    {

        //        string FileName = Path.GetFileName(FU.PostedFile.FileName);
        //        string Extension = Path.GetExtension(FU.PostedFile.FileName);
        //        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
        //        string FilePath = Server.MapPath(FileName);
        //        string fileLocation = HttpContext.Current.Server.MapPath(".").ToString() + @"\Trash\" + FileName;
        //        FU.SaveAs(FilePath);

        //        if (Extension == ".xls")
        //        {

        //            connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

        //        }
        //        else if (Extension == ".xlsx")
        //        {
        //            connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //        }
        //        connectionString = String.Format(connectionString, FilePath);
        //        OleDbConnection connExcel = new OleDbConnection(connectionString);
        //        OleDbCommand cmdExcel = new OleDbCommand();
        //        cmdExcel.CommandType = System.Data.CommandType.Text;
        //        cmdExcel.Connection = connExcel;
        //        OleDbDataAdapter oda = new OleDbDataAdapter();
        //        connExcel.Open();
        //        DataTable dtExcelSchema;
        //        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //        string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //        connExcel.Close();

        //        //Read Data from First Sheet
        //        connExcel.Open();
        //        cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
        //        oda.SelectCommand = cmdExcel;

        //        if (NombreVS == "DZ")
        //        {
        //            int contadorMalFormato = 0;
        //            oda.Fill(dtDD);
        //            connExcel.Close();

        //            dtID = dtDD.Copy();
        //            dtID.Columns.Add("Comentario", typeof(string));
        //            dtID.Columns.Add("Aprobado", typeof(string));
        //            if ((dtDD.Columns[0].ToString().ToLower().Trim() == "Codigosumar".ToLower().Trim())
        //                                && (dtDD.Columns[1].ToString().ToLower().Trim() == "Codigorestar".ToLower().Trim())
        //                                && (dtDD.Columns[2].ToString().Trim().ToLower() == "dz".ToLower().Trim())
        //                                && (dtDD.Columns[3].ToString().ToLower().Trim() == "observacion".ToLower().Trim())
        //                               )
        //            //&& (dtDD.Columns[4].ToString().ToLower().Trim() == "Fecha".ToLower().Trim())

        //            {

        //                foreach (DataRow item in dtID.Rows)
        //                {
        //                    item["Aprobado"] = "SI";
        //                    //    try
        //                    //    {
        //                    //        if (item["Fecha"].ToString() == "")
        //                    //        {
        //                    //            contadorMalFormato++;
        //                    //            item["Comentario"] = "NO SE ESPECIFICO UNA FECHA.";
        //                    //            item["Aprobado"] = "NO";
        //                    //        }
        //                    //        else
        //                    //        {
        //                    //            DateTime fechap = DateTime.Parse(item["Fecha"].ToString());
        //                    //            DateTime fechaini = DateTime.Parse(txtFechaIni.Text);
        //                    //            DateTime fechafin = DateTime.Parse(txtFechaFin.Text);
        //                    //            if (fechap >= fechaini && fechap <= fechafin)
        //                    //            {

        //                    //                item["Aprobado"] = "SI";
        //                    //            }
        //                    //            else
        //                    //            {
        //                    //                item["Comentario"] = "FECHA NO ESTA EN EL RANGO DE LA SEMANA QUE SE REGISTRA";
        //                    //                item["Aprobado"] = "NO";
        //                    //            }

        //                    //        }
        //                    //    }
        //                    //    catch
        //                    //    {
        //                    //        contadorMalFormato++;

        //                    //        item["Comentario"] = "LA FECHA NO ES UN FORMATO VALIDO, DEBE SER AÑO-MES'DIA";
        //                    //        item["Aprobado"] = "NO";
        //                    //    }

        //                }
        //                if (contadorMalFormato > 0)
        //                {
        //                    Session["FormatoCorrectoTD"] = "False";
        //                }

        //                Session["datos"] = dtID;
        //                gv.DataSource = dtID;

        //            }



        //        }

        //        else if (NombreVS == "IngDD")
        //        {

        //            oda.Fill(dtDD);
        //            connExcel.Close();

        //            dtID = dtDD.Copy();

        //            if ((dtDD.Columns[0].ToString().ToLower().Trim() == "Codigo".ToLower().Trim())
        //                           && (dtDD.Columns[1].ToString().Trim().ToLower() == "PorcetanjeDeduc".ToLower().Trim()))
        //            // && (dtDD.Columns[1].ToString().Trim().ToLower() == "NumRechazos".ToLower().Trim())

        //            {
        //                dtID.Columns.Add("Comentario", typeof(string));
        //                dtID.Columns.Add("Aprobado", typeof(string));
        //                Session["IngDD"] = dtID;
        //                gv.DataSource = dtID;

        //            }


        //        }

        //        //else if (NombreVS == "Autorizacion")
        //        //{

        //        //    oda.Fill(dtID);
        //        //    Session["Autorizaciones"] = dtID;
        //        //    gv.DataSource = dtID;
        //        //    gv.Columns[5].Visible = false;
        //        //}

        //        gv.DataBind();



        //    }
        //}
        // CODIGO DE CARGA Y PROCESO DE EXCEL DE dz

        public void ProcesarDZ()
        {
            List<Neg_Empleados> EmpleadosPDZ = new List<Neg_Empleados>();// ESTA ;OSTA CONTIENE SOLO LOS EMPLEADOS DE UN DEPARTAMENTO EN ESPECIFICO

            DataTable todosemple = (DataTable)Session["todosemp"];
            Session["todosemp"] = todosemple;
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
                        //if (row["Aprobado"].ToString() == "SI")
                        //{
                        contadorop = 0;
                        Session["contadorop"] = 0;
                        //comentario = "";
                        DZ = 0;


                        //DataTable dtAutorizacion = (DataTable)Session["Autorizaciones"];
                        codigo1 = Convert.ToInt32(row["Codigosumar"].ToString());
                        codigo2 = Convert.ToInt32(row["Codigorestar"].ToString());
                        List<Neg_Incentivos.EmpleadoOp> EmpleadosOP = (List<Neg_Incentivos.EmpleadoOp>)Session["EMPLEADOSOP"];
                        Neg_Incentivos.EmpleadoOp ESuma = (Neg_Incentivos.EmpleadoOp)(from e in EmpleadosOP where e.Codigo_empleado.Equals(codigo1) select e).SingleOrDefault();
                        Neg_Incentivos.EmpleadoOp EResta = (Neg_Incentivos.EmpleadoOp)(from e in EmpleadosOP where e.Codigo_empleado.Equals(codigo2) select e).SingleOrDefault();
                        bool OPAprobada = false;


                        string OP1;
                        string OP2;
                        String MENSAJE = "";
                        try
                        {
                            OP1 = ESuma.Operacion.Trim();
                            OP2 = EResta.Operacion.Trim();

                            //SE CONSULTA SI LAS OPERACIONES SON IGUALES, LA CUAL ES LA CONDICION PARA PODER HACER EL TRASLADO
                            if (OP1 == OP2)
                            {
                                OPAprobada = true;

                            }
                            else
                            {
                                //SI NO SON IGUALES, SE CONSULTA SI EL OPERARIO AL QUE LE PRETENDER HACER EL TRASLADO ES UN MULTITAREA
                                if (ESuma.MultiTarea)
                                {
                                    OPAprobada = true;
                                }

                            }

                        }
                        catch
                        {

                        }

                        if (OPAprobada)
                        {
                            //bool TrasladoAprobado = false;
                            bool TrasladoAprobado = true;

                            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                            //EmpleadosPDZ = lt.Where(x => x.codigo_empleado.Equals(codigo2)).ToList();
                            //DateTime fecha = Convert.ToDateTime(row["fecha"].ToString());
                            //if (EmpleadosPDZ.Count > 0)
                            //{
                            //    foreach (var item2 in EmpleadosPDZ)
                            //    {
                            //        DataTable diasLaborados = item2.dtHorasT;
                            //        if (diasLaborados.Rows.Count > 0)
                            //        {

                            //            DataView dtvdiasLaborados = diasLaborados.Copy().DefaultView;

                            //            var dia = from myRow in dtvdiasLaborados.ToTable().AsEnumerable()
                            //                      where myRow.Field<DateTime>("fecha") == fecha
                            //                      select myRow;
                            //            foreach (var dias in dia)
                            //            {
                            //                //SE OBTIENEN LAS HORAS QUE LABORO EN ESE DIA
                            //                double horast = dias.Field<double>("horast");
                            //                double horasnt = dias.Field<double>("horasv") + dias.Field<double>("horascg") + dias.Field<double>("horassg") + dias.Field<double>("horass");
                            //                //SI NO TRABAJO EL TURNO COMPLETO, QUIERE DECIR QUE ALGUN OTRO EMPLEADO SI PUDO HABER CUBIERTO SU OPERACION
                            //                if (horast < (dias.Field<double>("horasturno") - 0.3))
                            //                {
                            //                    TrasladoAprobado = true;
                            //                }
                            //                else
                            //                {
                            //                    //SI EL OPERARIO TIENE COMPLETO SU HORAS LABORALES, PERO TIENE ALGUN PERMISO YA SEA CON GOCE O SIN GOCE QUE INDIQUE QUE SE AUSENTO DENTRO DE SU HORARIO LABORAL
                            //                    if (horasnt > 0)
                            //                    {
                            //                        TrasladoAprobado = true;
                            //                    }
                            //                    else
                            //                    {
                            //                        TrasladoAprobado = false;
                            //                        row["Comentario"] = "NO SE PUEDE HACER EL TRASLADO PORQUE EL OPERARIO SI TRABAJO SUS HORAS COMPLETAS";
                            //                    }
                            //                }

                            //            }

                            //        }

                            //    }


                            //}



                            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                            if (TrasladoAprobado)
                            {

                                if (row["Codigosumar"].ToString() != "" && row["Codigorestar"].ToString() != "")
                                {


                                    codigo1 = Convert.ToInt32(row["Codigosumar"].ToString());
                                    codigo2 = Convert.ToInt32(row["Codigorestar"].ToString());



                                    DZ = Convert.ToDecimal(row["DZ"].ToString());
                                    observacionTDZ = row["observacion"].ToString();



                                    //SE BUSCA POR CODIGO PARA SABER SI TIENE SIGNADO UN INCENTIVO
                                    //CODIGO A SUMARLE

                                    Neg_Incentivos.IncentivoEmp dt2 = new Neg_Incentivos.IncentivoEmp();
                                    if (codigo1 != 0)
                                    {

                                        dt2 = (from myrow in ListIE where myrow.Codigo.Equals(codigo1) select myrow).SingleOrDefault();
                                    }
                                    else
                                    {
                                        dt2 = null;
                                    }
                                    //CODIGO A RESTARLE

                                    Neg_Incentivos.IncentivoEmp dt1 = new Neg_Incentivos.IncentivoEmp();
                                    if (codigo2 != 0)
                                    {
                                        dt1 = (from myrow in ListIE where myrow.Codigo.Equals(codigo2) select myrow).SingleOrDefault();
                                    }
                                    else
                                    {
                                        dt1 = null;
                                    }
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
                                        else
                                        {
                                            dtRegistroTrasDZ.Rows.Add(codigo1, DZ, 1);
                                            dtRegistroTrasDZ.Rows.Add(codigo2, DZ, 2);
                                            Session["DTTRaslados"] = dtRegistroTrasDZ;
                                        }
                                    }
                                    else
                                    {
                                        row["Comentario"] = "NO EXISTEN DZ PARA RESTAR Y POR LO TANTO NO SE LE PUEDE SUMAR";
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
                        }

                        else
                        {
                            row["Comentario"] = "NO ES PERMITIDO EL TRASLADO ENTRE OPERACIONES DISTINTAS, NI A OPERARIOS QUE NO SON MULTITAREA";
                        }
                        //}
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

        public void ProcesarAQL()
        {
            if (Session["AQL"] != "")
            {
                dt = (DataTable)Session["AQL"];
                if (dt.Rows.Count > 0)
                {
                }
            }

        }
        public void ProcesarIngDd()
        {

            if (Session["IngDD"] != "")
            {
                int tipoINGDD = 0, tipoCalculo = 0;
                decimal valor = 0, cantidad = 0;
                string detalle = "";
                dt = (DataTable)Session["IngDD"];


                if (dt.Rows.Count > 0)
                {
                    //Bind Data to GridView
                    // dt.Columns.Add("Comentario", typeof(string));
                    ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;

                    //SE OBTIENEN PRIMERO LOS INGRESOS POR .5 0 .125
                    //DataTable dtop = dt.Copy();
                    //DataView dtview1 = dtop.DefaultView;
                    //dtview1.RowFilter = "observacion='OP'";

                    //DataTable dt2 = dt.Copy();
                    //DataView dtview = dt2.DefaultView;


                    //dtview.Sort = "TipoIDD ASC";


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


                                tipoINGDD = 2;
                                // tipoCalculo = 1;
                                //tipoINGDD = Convert.ToInt32(row["TipoIDD"].ToString());
                                tipoCalculo = Convert.ToInt32(row["TipoCal"].ToString());
                                valor = Convert.ToDecimal(row["valor"].ToString());
                                observacionID = row["observacion"].ToString();
                                // observacionID = "Rechazo";
                                detalle = observacionID;

                                if (observacionID == "")
                                    detalle = " otros ";
                                Neg_Incentivos.IncentivoEmp dt1 = (from myrow in ListIE where myrow.Codigo.Equals(codigo1) select myrow).SingleOrDefault();

                                //SI EMPLEADO EXISTE O PERTENECE A UN MODULO QUE ALCANZO META
                                if (dt1 != null)
                                {


                                    DataTable dtInD = (DataTable)Session["IngDed"];
                                    if (observacionID.ToLower() == "op")
                                    {
                                        detalle = (valor / 100).ToString() + dt1.Operacion.ToString();
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

                                    DataTable dtEmpIncFijo = Neg_Incentivos.EmpleadosPagosXOperacionGetPagossFijos();
                                    DataView dtvEmpIncFijo = dtEmpIncFijo.Copy().DefaultView;

                                    dtvEmpIncFijo.RowFilter = "codigo=" + codigo1;

                                    if (dtvEmpIncFijo.ToTable().Rows.Count > 0)
                                    {
                                        dt1.IngresoNumerico = 0;
                                        dt1.Ingreso = 0;
                                        //dt1.Incentivo = 0;
                                    }
                                    else
                                    {
                                        dt1.IngresoNumerico = ingresoNumero;
                                    }

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
                    //gvINGDD.Columns[5].Visible = true;
                    gvINGDD.DataSource = dt;
                    gvINGDD.DataBind();
                    // btnProcesar.Visible = true;
                    Session["IngDD"] = dt; // ESTA VARIABLE DE SESSION ALMACENA LA TABLA DE DZ Y CODIGOS A SUMAR Y RESTAR
                    Session["DTIncentivosSP"] = ListIE;//ESTA VARIALE DE SESSION ALMACENA LOS MONTOS DE INCENTIVOS POR MODULOS
                }
            }

        }



        //public void ProcesarIngDd()
        //{

        //    if (Session["IngDD"] != "")
        //    {
        //        int tipoINGDD = 0, tipoCalculo = 0;
        //        decimal valor = 0, cantidad = 0;
        //        string detalle = "";
        //        dt = (DataTable)Session["IngDD"];


        //        if (dt.Rows.Count > 0)
        //        {
        //            //Bind Data to GridView
        //            // dt.Columns.Add("Comentario", typeof(string));
        //            ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;

        //            //SE OBTIENEN PRIMERO LOS INGRESOS POR .5 0 .125
        //            //DataTable dtop = dt.Copy();
        //            //DataView dtview1 = dtop.DefaultView;
        //            //dtview1.RowFilter = "observacion='OP'";

        //            //DataTable dt2 = dt.Copy();
        //            //DataView dtview = dt2.DefaultView;


        //            //dtview.Sort = "TipoIDD ASC";


        //            foreach (DataRow row in dt.Rows)
        //            {
        //                if (row["Codigo"].ToString() != "")
        //                {

        //                    try
        //                    {

        //                        ingresoNumero = 0; deduccionnumerico = 0;
        //                        ingresoporcentual = 0; deduccionporcentual = 0;
        //                        //comentario = "";
        //                        DZ = 0;
        //                        codigo1 = Convert.ToInt32(row["Codigo"].ToString());
        //                        tipoINGDD = 2;
        //                        tipoCalculo = 1;
        //                        //tipoINGDD = Convert.ToInt32(row["TipoIDD"].ToString());
        //                        //tipoCalculo = Convert.ToInt32(row["TipoCal"].ToString());
        //                        valor = Convert.ToDecimal(row["PorcetanjeDeduc"].ToString());
        //                        //observacionID = row["observacion"].ToString();
        //                        observacionID = "Rechazo";
        //                        detalle = observacionID;

        //                        if (observacionID == "")
        //                            detalle = " otros ";
        //                        Neg_Incentivos.IncentivoEmp dt1 = (from myrow in ListIE where myrow.Codigo.Equals(codigo1) select myrow).SingleOrDefault();

        //                        //SI EMPLEADO EXISTE O PERTENECE A UN MODULO QUE ALCANZO META
        //                        if (dt1 != null)
        //                        {
        //                            DataTable dtInD = (DataTable)Session["IngDed"];
        //                            if (observacionID.ToLower() == "op")
        //                            {
        //                                detalle = (valor / 100).ToString() + dt1.Operacion.ToString();
        //                            }
        //                            //ES UN INGRESO
        //                            if (tipoINGDD == 1)
        //                            {
        //                                //SI ES PORCENTUAL
        //                                if (tipoCalculo == 1)
        //                                {
        //                                    ingresoporcentual += valor;
        //                                    cantidad = ((dt1.Incentivo * ingresoporcentual) / 100);

        //                                }
        //                                else if (tipoCalculo == 2)
        //                                {
        //                                    ingresoNumero += Convert.ToDecimal(valor.ToString());
        //                                    cantidad = ingresoNumero;
        //                                }
        //                            }

        //                            //SI ES DEDUCCION
        //                            else if (tipoINGDD == 2)
        //                            {
        //                                if (tipoCalculo == 1)
        //                                {
        //                                    deduccionporcentual += valor;
        //                                    cantidad = ((dt1.Incentivo * deduccionporcentual) / 100);
        //                                }
        //                                else if (tipoCalculo == 2)
        //                                {
        //                                    deduccionnumerico += Convert.ToDecimal(valor.ToString());
        //                                    cantidad = deduccionnumerico;
        //                                }

        //                            }


        //                            dt1.IngresoNumerico = ingresoNumero;
        //                            if (dt1.IngresoNumerico > 0)
        //                            {
        //                                dt1.DetalleIngreso += " Ingreso " + detalle + " de: C$" + ingresoNumero;
        //                            }
        //                            dt1.IngresoPorcentual = ingresoporcentual;

        //                            if (dt1.IngresoPorcentual > 0)
        //                            {
        //                                dt1.DetalleIngreso += " Ingreso " + detalle + " de: " + ingresoporcentual + "% ";
        //                            }
        //                            dt1.DeduccionNumerica = deduccionnumerico;

        //                            if (dt1.DeduccionNumerica > 0)
        //                            {
        //                                dt1.DetalleEgreso += " Egreso " + detalle + " de: C$" + deduccionnumerico;
        //                            }
        //                            dt1.DeduccionPorcentual = deduccionporcentual;

        //                            if (dt1.DeduccionPorcentual > 0)
        //                            {
        //                                dt1.DetalleEgreso += " Egreso " + detalle + " de:" + deduccionporcentual + "% ";
        //                            }


        //                            dt1.Ingreso = dt1.Ingreso + (((dt1.Incentivo * ingresoporcentual) / 100) + ingresoNumero);
        //                            decimal incentivoIngresos = dt1.Incentivo + dt1.Ingreso;
        //                            dt1.Deduccion = dt1.Deduccion + (((incentivoIngresos * deduccionporcentual) / 100) + deduccionnumerico);


        //                            dt1.Total = incentivoIngresos - dt1.Deduccion;

        //                            if (dt1.Total < 0)
        //                            {
        //                                dt1.Total = 0;
        //                            }

        //                            dtInD.Rows.Add(codigo1, tipoINGDD, detalle, tipoCalculo, valor, cantidad, observacionID);
        //                            Session["IngDed"] = dtInD;
        //                        }
        //                    }
        //                    catch
        //                    {

        //                    }
        //                }

        //            }

        //            //  gvReajusteDZ.Caption = Path.GetFileName(FilePath);
        //            //gvINGDD.Columns[5].Visible = true;
        //            gvINGDD.DataSource = dt;
        //            gvINGDD.DataBind();
        //            // btnProcesar.Visible = true;
        //            Session["IngDD"] = dt; // ESTA VARIABLE DE SESSION ALMACENA LA TABLA DE DZ Y CODIGOS A SUMAR Y RESTAR
        //            Session["DTIncentivosSP"] = ListIE;//ESTA VARIALE DE SESSION ALMACENA LOS MONTOS DE INCENTIVOS POR MODULOS
        //        }
        //    }

        //}

        protected void btAlicarPlanilla_Click(object sender, EventArgs e)
        {
            int codigo, modulo, amonestacion, diaslaborados, diasausencia, ausenciaj, ausenciaI;
            decimal produccion, metaalcanzada, totalA, eficiencia, incentivoM, totalI, totalE, incentivo;
            try
            {
                if (txtPeriodo.Text != "")
                {

                    //MODIFICAR SP PARA ELIMINAR SOLO LOS TIPOS INGRESOS QUE SE ESTEN PROCESANDO EN ESTA PANTALLA
                    if (txtPeriodo.Text.Trim() == "0" || txtPeriodo.Text.Trim() == "")
                        throw new Exception("Periodo Invalido");

                    int periodo = int.Parse(txtPeriodo.Text);
                    int semana = int.Parse(ddlTipo.SelectedValue);

                    dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.PeriodoSel(periodo);

                    if (dtPeriodo[0].cerrado == 1)
                        throw new Exception("El periodo esta cerrado");

                    Neg_Incentivos.IncentivosHistoricoDelete(periodo, semana, 4, true);
                    Neg_Incentivos.IncentivoIngDedccLOGDelete(periodo, semana, 4, true);
                    ListIE = Session["DTIncentivosSP"] as List<Neg_Incentivos.IncentivoEmp>;
                    DataTable dtInD = (DataTable)Session["IngDed"];
                    string user = Convert.ToString(this.Page.Session["usuario"]);

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

                    //int periodopago = 0;

                    //dsPlanilla.dtPeriodoDataTable dtPeriodo = Neg_Periodo.cargarUltPeriodoAbieCat(1,1,0);
                    //if (dtPeriodo.Rows.Count > 0)
                    //{
                    //    periodopago = dtPeriodo[0].nperiodo;
                    //}
                    //else
                    //{
                    //    periodopago = 0;
                    //}


                    foreach (var item in ListIE)
                    {
                        if (int.Parse(item.Codigo.ToString()) == 309963)
                        {

                        }
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

                        Neg_Incentivos.IncentivosHistoricoInsert(codigo, modulo, item.Estilo.ToString(), item.Operacion, item.Construccion, produccion, metaalcanzada, amonestacion, diaslaborados, diasausencia, ausenciaj, ausenciaI, totalA, eficiencia, incentivoM, totalI, totalE, incentivo, periodo, semana, user, 4, item.Comentario, true);

                        DataTable dtSumIncenivos = Neg_Incentivos.IncentivosHistoricoGetXEmpleado(semana, periodo, codigo.ToString(), 4);

                        if (dtSumIncenivos.Rows.Count > 0)
                        {
                            incentivo = Convert.ToDecimal(dtSumIncenivos.Rows[0][0]);
                        }


                        //bloque de codigo que inserta ingresos asociados a deducirse del ingreso bruto de incentivos
                        //NDevyDed.IngresosIncentivoIBrutoEliminarxEmp(periodo, semana, 4, codigo);
                        //dt1.Rows.Add(1, codigo, semana, 4, periodo, Math.Ceiling(incentivo));
                        if (codigo == 0)
                        {
                            continue;
                        }
                        if (!NDevyDed.InsertarIngrDeduc(1, codigo, semana, 4, periodo, Math.Ceiling(incentivo), user))
                        //if (!InsertarIngrDeduc(Convert.ToInt32(item[0]), Convert.ToInt32(item[1]), semana, Convert.ToInt32(item[3]), periodo, valorOriginal, user))
                        {
                            throw new Exception("Error al insertar ingreso");
                        }
                        //SE INSERTA INGRESO RESPALDO SOLO PARA INGRESOS QUE APLICAN A DEDUCCION BASE
                        if (!NDevyDed.IngresosAplicaIBrutoBakIns(1, codigo, semana, 4, periodo, Math.Ceiling(incentivo)))
                        {
                            throw new Exception("Error al insertar ingreso");
                        }
                    }

                    foreach (DataRow dr in dtInD.Rows)
                    {
                        Neg_Incentivos.IncentivoIngDedccLOGInsert(int.Parse(dr["Codigo"].ToString()), periodo, semana, int.Parse(dr["tipo"].ToString()), dr["detalle"].ToString(), int.Parse(dr["tipoCalc"].ToString()), decimal.Parse(dr["Cantidad"].ToString()), decimal.Parse(dr["Valor"].ToString()), dr["Observacion"].ToString(), 4, true);
                    }

                    if (Session["AQL"] != null)
                    {
                        if (Session["AQL"] != "")
                        {
                            DataTable dtAQLs = (DataTable)Session["AQL"]; //

                            foreach (DataRow mod in dtAQLs.Rows)
                            {
                                Neg_Incentivos.IncentivoAQLxModuloInsert(semana, periodo, mod[0].ToString(), decimal.Parse(mod[1].ToString()), decimal.Parse(mod[2].ToString()), (decimal.Parse(mod[1].ToString()) / decimal.Parse(mod[2].ToString())) * 100);
                            }
                        }
                    }

                    //NDevyDed.IngresosIncentivoIBrutoEliminar(periodo, semana, 4);
                    //dt1 = dt1.Rows.Cast<DataRow>().OrderBy(row => Convert.ToInt32(row["codigoempleado"])).CopyToDataTable();
                    //NDevyDed.RegistrarIngresoEgresoAsociado(periodo, dt1, user);
                }
            }
            catch (Exception ex)
            {
                alertValida.Visible = true;
                lblAlert.Visible = true;
                lblAlert.Text = ex.Message;
            }

        }

        protected void cbSeleccion_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSeleccion.Checked)
            {
                panelDZ.Visible = true;
                panelID.Visible = true;
                pnlAQL.Visible = true;
            }
            else
            {
                panelDZ.Visible = false;
                panelID.Visible = false;
                pnlAQL.Visible = false;
            }
        }
    }

}
