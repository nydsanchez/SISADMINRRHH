using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.OleDb;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using Datos;

using Negocios;

namespace NominaRRHH.Presentacion
{
    public partial class ReportPlanillaRevision : System.Web.UI.Page
    {
        #region REFERENCIAS
        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pnlModulo.Visible = false;
                pnlCodigo.Visible = false;
                Session["Filtro"] = "TODOS";
                Session["Filtro2"] = "C/INCENTIVO";
                pnlcheck.Visible = false;
                Session["dtInc"] = "";

            }

        }

        protected void rbllistImpresion_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (rbllistImpresion.SelectedValue == "1")
            {
                Session["Filtro"] = "MODULO";


                pnlModulo.Visible = true;
                pnlCodigo.Visible = false;
                //if (ddltipor.SelectedValue == "1")
                //{
                //    ddlProceso.DataTextField = "ModuloNombre";
                //    ddlProceso.DataValueField = "MODULO";
                //    ddlProceso.DataSource = CargarDll();
                //}
                //if (ddltipor.SelectedValue == "2")
                //{
                //    ddlProceso.DataTextField = "ModuloNombre";
                //    ddlProceso.DataValueField = "MODULO";
                //    ddlProceso.DataSource = cargardllMSI();
                //}

                //ddlProceso.DataBind();
            }
            else if (rbllistImpresion.SelectedValue == "2")
            {
                Session["Filtro"] = "RANGO";
                pnlCodigo.Visible = true;
                pnlModulo.Visible = false;
            }
            else
            {
                Session["Filtro"] = "TODOS";
                pnlCodigo.Visible = false;
                pnlModulo.Visible = false;
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            DataTable dtEmpIncFijo = Neg_Incentivos.EmpleadosPagosXOperacionGetPagossFijos();
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable dsInc = new DataTable();

            dsInc.Columns.Add("Codigo_emp", typeof(int));
            dsInc.Columns.Add("Deptartamento", typeof(string));
            dsInc.Columns.Add("Nombre_Completo", typeof(string));
            dsInc.Columns.Add("OP", typeof(string));
            dsInc.Columns.Add("Prod_Lunes", typeof(decimal));
            dsInc.Columns.Add("Prod_Martes", typeof(decimal));
            dsInc.Columns.Add("Prod_Miercoles", typeof(decimal));
            dsInc.Columns.Add("Prod_Jueves", typeof(decimal));
            dsInc.Columns.Add("Prod_Viernes", typeof(decimal));
            dsInc.Columns.Add("Prod_Sabado", typeof(decimal));
            dsInc.Columns.Add("Prod_Domingo", typeof(decimal));
            dsInc.Columns.Add("Prod_Total", typeof(decimal));
            dsInc.Columns.Add("DZRestadas", typeof(decimal));
            dsInc.Columns.Add("DZSumadas", typeof(decimal));
            dsInc.Columns.Add("Prod_TotalPagar", typeof(decimal));
            dsInc.Columns.Add("Rechazo", typeof(string));
            dsInc.Columns.Add("Ausencias", typeof(string));
            dsInc.Columns.Add("Amonestaciones", typeof(string));
            dsInc.Columns.Add("Eficiencia", typeof(string));
            dsInc.Columns.Add("Pago_Sistema", typeof(string));
            dsInc.Columns.Add("Pago_Manual", typeof(string));
            dsInc.Columns.Add("Incentivo", typeof(decimal));
            dsInc.Columns.Add("AQL", typeof(decimal));
            dsInc.Columns.Add("AQLMETA", typeof(decimal));
            dsInc.Columns.Add("Total", typeof(decimal));
            dsInc.Columns.Add("Deduccion_Detalle", typeof(string));
            dsInc.Columns.Add("DeduccionTotal", typeof(decimal));
            dsInc.Columns.Add("IncenttivoProteccion", typeof(decimal));





            Session["dtInc"] = dsInc;
            if (txtperiodo.Text != "")
            {
                List<string> modulosCI = new List<string>();
                List<string> modulosSI = new List<string>();


                int periodo = int.Parse(txtperiodo.Text);
                int semana = int.Parse(ddlTipo.SelectedValue.ToString());
                Dato_Empleados DEmpleado = new Dato_Empleados();

                DataTable dtInfon = Neg_Incentivos.IncentivoHistoricoSelect(periodo, semana);

                if (dtInfon != null)
                {
                    if (dtInfon.Rows.Count > 0)
                    {
                        DataTable dtEmpleado = DEmpleado.pln_empleadosHistoricoALL(userDetail.getIDEmpresa(), Convert.ToDateTime(dtInfon.Rows[0][2]), 1); //Ordenado por codigo_empleado
                        DataTable dtproduccionxdiaMod = Neg_Incentivos.CosProduccionporDiaRangoFecha(Convert.ToDateTime(dtInfon.Rows[0][2]), Convert.ToDateTime(dtInfon.Rows[0][3]));
                        DataTable dtAQL = Neg_Incentivos.IncentivoAQLxModuloSelectSemanaPEriodo(semana, periodo);
                        DataTable dttrasladoDZ = Neg_Incentivos.TrasladoDZEfectivos(periodo, semana);
                        DataTable dtIncDeducEmpleados = Neg_Incentivos.IncentivoIngDedccLOGxEmpleado(periodo, semana);
                        List<string> modulo = moduloList();

                        int codigo = 0;



                        foreach (string mod in modulo)
                        {
                            decimal prodLunes = 0, prodMartes = 0, prodMiercoles = 0, prodJueves = 0, prodViernes = 0, prodSabado = 0, prodDomingo = 0, prodTotal = 0;

                            decimal aql = 0, aqlmeta = 0, porce = 0;
                            if (mod.ToString().Trim() != "00")
                            {
                                //PRODUCCION POR MODULO
                                if (dtproduccionxdiaMod.Rows.Count > 0)
                                {
                                    DataView dtProd = dtproduccionxdiaMod.Copy().DefaultView;
                                    dtProd.RowFilter = "Modulo='" + mod.ToString().Trim() + "'";

                                    DataView dtvAQL = dtAQL.Copy().DefaultView;

                                    if (dtAQL.Rows.Count > 0)
                                    {
                                        dtvAQL.RowFilter = "Modulo=" + int.Parse(mod.ToString().Trim());

                                        if (dtvAQL.ToTable().Rows.Count > 0)
                                        {
                                            aql = decimal.Parse(dtvAQL[0][3].ToString());
                                            aqlmeta = decimal.Parse(dtvAQL[0][4].ToString());

                                            if (aql > 0)
                                            {
                                                porce = (aqlmeta / aql) * 100;
                                            }


                                            if (porce > 100)
                                            {
                                                porce = 100;
                                            }
                                        }


                                    }

                                    if (dtProd.ToTable().Rows.Count > 0)
                                    {
                                        // prodLunes = 0,prodMartes = 0, prodMiercoles = 0, prodJueves = 0,prodViernes = 0, prodSabado = 0, prodDomingo = 0, prodTotal = 0
                                        prodLunes = decimal.Parse(dtProd[0]["Lunes"].ToString());
                                        prodMartes = decimal.Parse(dtProd[0]["Martes"].ToString());
                                        prodMiercoles = decimal.Parse(dtProd[0]["Miercoles"].ToString());
                                        prodJueves = decimal.Parse(dtProd[0]["Jueves"].ToString());
                                        prodViernes = decimal.Parse(dtProd[0]["Viernes"].ToString());
                                        prodSabado = decimal.Parse(dtProd[0]["Sabado"].ToString());
                                        prodDomingo = decimal.Parse(dtProd[0]["Domingo"].ToString());
                                        prodTotal = decimal.Parse(dtProd[0]["TotalDZ"].ToString());

                                        DataView dtvEmpleado2 = dtEmpleado.Copy().DefaultView;
                                        dtvEmpleado2.RowFilter = "Modulo=" + int.Parse(mod.ToString().Trim());
                                        if (dtvEmpleado2.ToTable().Rows.Count > 0)
                                        {
                                            foreach (DataRow emp in dtvEmpleado2.ToTable().Rows)
                                            {

                                                bool IncFijo = false;

                                                decimal dzSum = 0, dzRestadas = 0, pagoSistema = 0, pagoManual = 0, pagoTotal = 0, eficiencia = 0, incentivoproteccion = 0, totalDeduccion = 0;
                                                string nombre = "", op = "", porcAmonestacion = "", porcetRechazo = "", porctAusencia = "", detalleDeduccion = "";


                                                int codigo_empleado = int.Parse(emp[0].ToString());


                                                if (codigo_empleado == 866369)
                                                {
                                                }


                                                nombre = emp["nombre"].ToString();
                                                op = emp["operacion"].ToString();

                                                //if (op.Trim() != "IF")
                                                //{

                                                DataView dtvInfon = dtInfon.Copy().DefaultView;
                                                dtvInfon.RowFilter = "codigo_empleado=" + codigo_empleado;
                                                //SI EL EMPLEADO ALCANZO INCENTIVO
                                                if (dtvInfon.ToTable().Rows.Count > 0)
                                                {

                                                    DataView dtGeneradoSistema = dtvInfon.ToTable().DefaultView;
                                                    DataView dtGeneradoManual = dtvInfon.ToTable().DefaultView;


                                                    dtGeneradoSistema.RowFilter = "GeneradoSistema=" + true;
                                                    dtGeneradoManual.RowFilter = "GeneradoSistema=" + false;


                                                    if (dtGeneradoSistema.ToTable().Rows.Count > 0)
                                                    {


                                                        pagoSistema = dtGeneradoSistema.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("IncentivoMeta"))).Sum();
                                                        pagoTotal += dtGeneradoSistema.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("TotalIncentivo"))).Sum();

                                                        DataView dtVEmpIncFijo = dtEmpIncFijo.Copy().DefaultView;
                                                        dtVEmpIncFijo.RowFilter = "codigo=" + codigo_empleado;

                                                        if (dtVEmpIncFijo.ToTable().Rows.Count > 0)
                                                        {
                                                            incentivoproteccion = pagoSistema;
                                                            pagoSistema = 0;
                                                            IncFijo = true;

                                                        }


                                                    }
                                                    if (dtGeneradoManual.ToTable().Rows.Count > 0) { pagoManual = dtGeneradoManual.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("IncentivoMeta"))).Sum(); pagoTotal += dtGeneradoManual.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("TotalIncentivo"))).Sum(); }




                                                    DataView dtvTrasladosDZResta = dttrasladoDZ.Copy().DefaultView;
                                                    DataView dtvTrasladosDZSuma = dttrasladoDZ.Copy().DefaultView;

                                                    dtvTrasladosDZResta.RowFilter = "codigoOrigen='" + codigo_empleado + "'";
                                                    dtvTrasladosDZSuma.RowFilter = "codigoDestino='" + codigo_empleado + "'";

                                                    if (dtvTrasladosDZResta.ToTable().Rows.Count > 0) { dzRestadas = dtvTrasladosDZResta.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("DZ"))).Sum(); }
                                                    if (dtvTrasladosDZSuma.ToTable().Rows.Count > 0) { dzSum = dtvTrasladosDZSuma.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("DZ"))).Sum(); }

                                                    DataView ingDedview = dtIncDeducEmpleados.Copy().DefaultView;

                                                    ingDedview.RowFilter = "codigo='" + codigo_empleado + "'";
                                                    if (ingDedview.ToTable().Rows.Count > 0)
                                                    {
                                                        DataTable ingDedemp = ingDedview.ToTable();
                                                        for (int i = 0; i < 4; i++)
                                                        {

                                                            DataTable ingDedemp2 = ingDedemp.Copy();
                                                            DataView ingDedempview = ingDedemp2.DefaultView;
                                                            if (i == 0)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'Rechazo'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    porcetRechazo = ingDedempview[0][4].ToString() + "%";
                                                                }
                                                            }
                                                            if (i == 1)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'DiasInjustificados'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    porctAusencia = ingDedempview[0][4].ToString() + "%";
                                                                }
                                                            }
                                                            if (i == 2)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'Amonestaciones'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    porcAmonestacion = ingDedempview[0][4].ToString() + "%";
                                                                }
                                                            }
                                                            if (i == 3)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'Eficiencia'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    if (IncFijo)
                                                                    {
                                                                        eficiencia = 0;
                                                                    }
                                                                    else
                                                                    {
                                                                        eficiencia = Convert.ToDecimal(ingDedempview[0][4].ToString());
                                                                    }

                                                                }
                                                            }

                                                        }


                                                        DataView ingDedempviewOtras = ingDedemp.Copy().DefaultView;
                                                        ingDedempviewOtras.RowFilter = "codigo='" + codigo_empleado + "' and tipo=2 and (detalle<> 'Eficiencia' or detalle<> 'Amonestaciones' or detalle<> 'DiasInjustificados')";
                                                        if (ingDedempviewOtras.ToTable().Rows.Count > 0)
                                                        {
                                                            int c = 0;
                                                            foreach (DataRow item in ingDedempviewOtras.ToTable().Rows)
                                                            {
                                                                c++;
                                                                totalDeduccion += Convert.ToDecimal(item["valor"].ToString());


                                                                if (ingDedempviewOtras.ToTable().Rows.Count > 1)
                                                                {

                                                                    detalleDeduccion += item["detalle"].ToString();

                                                                    if (c < ingDedempviewOtras.ToTable().Rows.Count)
                                                                    {
                                                                        detalleDeduccion += ",";
                                                                    }
                                                                }
                                                                else
                                                                { detalleDeduccion += item["detalle"].ToString(); }
                                                            }


                                                        }

                                                    }




                                                    dsInc.Rows.Add(codigo_empleado, mod.ToString().Trim(), nombre, op, prodLunes, prodMartes, prodMiercoles, prodJueves, prodViernes, prodSabado, prodDomingo, prodTotal, dzRestadas, dzSum, (prodTotal + dzSum) - dzRestadas, porcetRechazo, porctAusencia, porcAmonestacion, eficiencia, pagoSistema, pagoManual, pagoTotal, aql, aqlmeta, porce, detalleDeduccion, totalDeduccion, incentivoproteccion);

                                                }
                                                //NO ALCANZO INCENTIVO
                                                else
                                                {
                                                    DataView dtvTrasladosDZResta = dttrasladoDZ.Copy().DefaultView;
                                                    DataView dtvTrasladosDZSuma = dttrasladoDZ.Copy().DefaultView;

                                                    dtvTrasladosDZResta.RowFilter = "codigoOrigen='" + codigo_empleado + "'";
                                                    dtvTrasladosDZSuma.RowFilter = "codigoDestino='" + codigo_empleado + "'";

                                                    if (dtvTrasladosDZResta.ToTable().Rows.Count > 0) { dzRestadas = dtvTrasladosDZResta.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("DZ"))).Sum(); }
                                                    if (dtvTrasladosDZSuma.ToTable().Rows.Count > 0) { dzSum = dtvTrasladosDZSuma.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("DZ"))).Sum(); }

                                                    DataView ingDedview = dtIncDeducEmpleados.Copy().DefaultView;

                                                    ingDedview.RowFilter = "codigo='" + codigo_empleado + "'";
                                                    if (ingDedview.ToTable().Rows.Count > 0)
                                                    {
                                                        DataTable ingDedemp = ingDedview.ToTable();
                                                        for (int i = 0; i < 4; i++)
                                                        {

                                                            DataTable ingDedemp2 = ingDedemp.Copy();
                                                            DataView ingDedempview = ingDedemp2.DefaultView;
                                                            if (i == 0)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'Rechazo'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    porcetRechazo = ingDedempview[0][4].ToString() + "%";
                                                                }
                                                            }
                                                            if (i == 1)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'DiasInjustificados'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    porctAusencia = ingDedempview[0][4].ToString() + "%";
                                                                }
                                                            }
                                                            if (i == 2)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'Amonestaciones'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    porcAmonestacion = ingDedempview[0][4].ToString() + "%";
                                                                }
                                                            }
                                                            if (i == 3)
                                                            {
                                                                ingDedempview.RowFilter = "detalle= 'Eficiencia'";
                                                                if (ingDedempview.ToTable().Rows.Count > 0)
                                                                {
                                                                    eficiencia = Convert.ToDecimal(ingDedempview[0][4].ToString());

                                                                }
                                                            }

                                                        }

                                                        DataView ingDedempviewOtras = ingDedemp.Copy().DefaultView;
                                                        ingDedempviewOtras.RowFilter = "codigo='" + codigo_empleado + "' and tipo=2 and (detalle<> 'Eficiencia' or detalle<> 'Amonestaciones' or detalle<> 'DiasInjustificados')";
                                                        if (ingDedempviewOtras.ToTable().Rows.Count > 0)
                                                        {
                                                            int c = 0;
                                                            foreach (DataRow item in ingDedempviewOtras.ToTable().Rows)
                                                            {
                                                                c++;
                                                                totalDeduccion += Convert.ToDecimal(item["valor"].ToString());


                                                                if (ingDedempviewOtras.ToTable().Rows.Count > 1)
                                                                {

                                                                    detalleDeduccion += item["detalle"].ToString();

                                                                    if (c < ingDedempviewOtras.ToTable().Rows.Count)
                                                                    {
                                                                        detalleDeduccion += ",";
                                                                    }
                                                                }
                                                                else
                                                                { detalleDeduccion += item["detalle"].ToString(); }
                                                            }


                                                        }

                                                    }
                                                    dsInc.Rows.Add(codigo_empleado, mod.ToString().Trim(), nombre, op, prodLunes, prodMartes, prodMiercoles, prodJueves, prodViernes, prodSabado, prodDomingo, prodTotal, dzRestadas, dzSum, (prodTotal + dzSum) - dzRestadas, porcetRechazo, porctAusencia, porcAmonestacion, 0, pagoSistema, pagoManual, pagoTotal, aql, aqlmeta, porce, detalleDeduccion, totalDeduccion, 0);

                                                }


                                                //  }




                                            }
                                        }


                                    }


                                }

                            }
                            else
                            {
                                bool IncFijo = false;
                                DataView dtvEmpleado2 = dtInfon.Copy().DefaultView;
                                dtvEmpleado2.RowFilter = "operacion='NA' and Estilo=0";
                                if (dtvEmpleado2.ToTable().Rows.Count > 0)
                                {
                                    foreach (DataRow emp in dtvEmpleado2.ToTable().Rows)
                                    {
                                        decimal dzSum = 0, dzRestadas = 0, pagoSistema = 0, pagoManual = 0, pagoTotal = 0, eficiencia = 0, incentivoproteccion = 0, totalDeduccion = 0;
                                        string nombre = "", op = "", porcAmonestacion = "", porcetRechazo = "", porctAusencia = "", detalleDeduccion = "";

                                        string dep = emp["NombreDepto"].ToString();
                                        int codigo_empleado = int.Parse(emp["codigo_empleado"].ToString());


                                        if (codigo_empleado == 866369)
                                        {
                                        }


                                        nombre = emp["nombrecompleto"].ToString();
                                        op = emp["operacion"].ToString();

                                        //if (op.Trim() != "IF")
                                        //{

                                        DataView dtvInfon = dtInfon.Copy().DefaultView;
                                        dtvInfon.RowFilter = "codigo_empleado=" + codigo_empleado;


                                        DataView dtGeneradoSistema = dtvInfon.ToTable().DefaultView;
                                        DataView dtGeneradoManual = dtvInfon.ToTable().DefaultView;


                                        dtGeneradoSistema.RowFilter = "GeneradoSistema=" + true;
                                        dtGeneradoManual.RowFilter = "GeneradoSistema=" + false;


                                        if (dtGeneradoSistema.ToTable().Rows.Count > 0)
                                        {


                                            pagoSistema = dtGeneradoSistema.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("IncentivoMeta"))).Sum();
                                            pagoTotal += dtGeneradoSistema.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("TotalIncentivo"))).Sum();

                                            DataView dtVEmpIncFijo = dtEmpIncFijo.Copy().DefaultView;
                                            dtVEmpIncFijo.RowFilter = "codigo=" + codigo_empleado;

                                            if (dtVEmpIncFijo.ToTable().Rows.Count > 0)
                                            {
                                                incentivoproteccion = pagoSistema;
                                                pagoSistema = 0;
                                                IncFijo = true;

                                            }


                                        }
                                        if (dtGeneradoManual.ToTable().Rows.Count > 0) { pagoManual = dtGeneradoManual.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("IncentivoMeta"))).Sum(); pagoTotal += dtGeneradoManual.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("TotalIncentivo"))).Sum(); }




                                        DataView dtvTrasladosDZResta = dttrasladoDZ.Copy().DefaultView;
                                        DataView dtvTrasladosDZSuma = dttrasladoDZ.Copy().DefaultView;

                                        dtvTrasladosDZResta.RowFilter = "codigoOrigen='" + codigo_empleado + "'";
                                        dtvTrasladosDZSuma.RowFilter = "codigoDestino='" + codigo_empleado + "'";

                                        if (dtvTrasladosDZResta.ToTable().Rows.Count > 0) { dzRestadas = dtvTrasladosDZResta.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("DZ"))).Sum(); }
                                        if (dtvTrasladosDZSuma.ToTable().Rows.Count > 0) { dzSum = dtvTrasladosDZSuma.ToTable().AsEnumerable().Select(r => Convert.ToInt32(r.Field<decimal>("DZ"))).Sum(); }

                                        DataView ingDedview = dtIncDeducEmpleados.Copy().DefaultView;

                                        ingDedview.RowFilter = "codigo='" + codigo_empleado + "'";
                                        if (ingDedview.ToTable().Rows.Count > 0)
                                        {
                                            DataTable ingDedemp = ingDedview.ToTable();
                                            for (int i = 0; i < 4; i++)
                                            {

                                                DataTable ingDedemp2 = ingDedemp.Copy();
                                                DataView ingDedempview = ingDedemp2.DefaultView;
                                                if (i == 0)
                                                {
                                                    ingDedempview.RowFilter = "detalle= 'Rechazo'";
                                                    if (ingDedempview.ToTable().Rows.Count > 0)
                                                    {
                                                        porcetRechazo = ingDedempview[0][4].ToString() + "%";
                                                    }
                                                }
                                                if (i == 1)
                                                {
                                                    ingDedempview.RowFilter = "detalle= 'DiasInjustificados'";
                                                    if (ingDedempview.ToTable().Rows.Count > 0)
                                                    {
                                                        porctAusencia = ingDedempview[0][4].ToString() + "%";
                                                    }
                                                }
                                                if (i == 2)
                                                {
                                                    ingDedempview.RowFilter = "detalle= 'Amonestaciones'";
                                                    if (ingDedempview.ToTable().Rows.Count > 0)
                                                    {
                                                        porcAmonestacion = ingDedempview[0][4].ToString() + "%";
                                                    }
                                                }
                                                if (i == 3)
                                                {
                                                    ingDedempview.RowFilter = "detalle= 'Eficiencia'";
                                                    if (ingDedempview.ToTable().Rows.Count > 0)
                                                    {
                                                        if (IncFijo)
                                                        {
                                                            eficiencia = 0;
                                                        }
                                                        else
                                                        {
                                                            eficiencia = Convert.ToDecimal(ingDedempview[0][4].ToString());
                                                        }

                                                    }
                                                }

                                            }


                                            DataView ingDedempviewOtras = ingDedemp.Copy().DefaultView;
                                            ingDedempviewOtras.RowFilter = "codigo='" + codigo_empleado + "' and tipo=2 and (detalle<> 'Eficiencia' or detalle<> 'Amonestaciones' or detalle<> 'DiasInjustificados')";
                                            if (ingDedempviewOtras.ToTable().Rows.Count > 0)
                                            {
                                                int c = 0;
                                                foreach (DataRow item in ingDedempviewOtras.ToTable().Rows)
                                                {
                                                    c++;
                                                    totalDeduccion += Convert.ToDecimal(item["valor"].ToString());


                                                    if (ingDedempviewOtras.ToTable().Rows.Count > 1)
                                                    {

                                                        detalleDeduccion += item["detalle"].ToString();

                                                        if (c < ingDedempviewOtras.ToTable().Rows.Count)
                                                        {
                                                            detalleDeduccion += ",";
                                                        }
                                                    }
                                                    else
                                                    { detalleDeduccion += item["detalle"].ToString(); }
                                                }


                                            }

                                        }




                                        dsInc.Rows.Add(codigo_empleado, dep.ToString().Trim(), nombre, op, prodLunes, prodMartes, prodMiercoles, prodJueves, prodViernes, prodSabado, prodDomingo, prodTotal, dzRestadas, dzSum, (prodTotal + dzSum) - dzRestadas, porcetRechazo, porctAusencia, porcAmonestacion, eficiencia, pagoSistema, pagoManual, pagoTotal, aql, aqlmeta, porce, detalleDeduccion, totalDeduccion, incentivoproteccion);




                                    }
                                }

                            }
                        }


                        //DataView dtvempleadosNoProduccion = dtInfon.Copy().DefaultView;

                        //dtvempleadosNoProduccion.RowFilter = "GeneradoSistema=" + false + " and operacion='IF'";

                        //if (dtvempleadosNoProduccion.ToTable().Rows.Count > 0)
                        //{
                        //    foreach (DataRow itempem in dtvempleadosNoProduccion.ToTable().Rows)
                        //    {
                        //        string nombre = "", op = "", depto = "", porcAmonestacion = "", porcetRechazo = "", porctAusencia = "", detalleDeduccion = "";
                        //        int codig0 = 0;
                        //        decimal totalDeduccion = 0;

                        //        nombre = itempem["nombrecompleto"].ToString();
                        //        depto = itempem["NombreDepto"].ToString();
                        //        codigo = int.Parse(itempem["codigo_empleado"].ToString());
                        //        op = (itempem["operacion"].ToString().Trim());

                        //        if (codigo == 866369)
                        //        {
                        //        }

                        //        if (itempem["operacion"].ToString().Trim() == "IF")
                        //        {
                        //            if (depto.Length > 2)
                        //            {
                        //                string dep = itempem["NombreDepto"].ToString();

                        //                depto = dep.Substring(7, 2);
                        //            }

                        //        }

                        //        decimal pagoManual = Convert.ToDecimal(itempem["IncentivoMeta"].ToString());
                        //        decimal totalpago = Convert.ToDecimal(itempem["TotalIncentivo"].ToString());

                        //        DataView ingDedview = dtIncDeducEmpleados.Copy().DefaultView;

                        //        ingDedview.RowFilter = "codigo='" + codigo + "'";
                        //        if (ingDedview.ToTable().Rows.Count > 0)
                        //        {
                        //            DataTable ingDedemp = ingDedview.ToTable();
                        //            for (int i = 0; i < 3; i++)
                        //            {

                        //                DataTable ingDedemp2 = ingDedemp.Copy();
                        //                DataView ingDedempview = ingDedemp2.DefaultView;
                        //                if (i == 0)
                        //                {
                        //                    ingDedempview.RowFilter = "detalle= 'Rechazo'";
                        //                    if (ingDedempview.ToTable().Rows.Count > 0)
                        //                    {
                        //                        porcetRechazo = ingDedempview[0][4].ToString() + "%";
                        //                    }
                        //                }
                        //                if (i == 1)
                        //                {
                        //                    ingDedempview.RowFilter = "detalle= 'DiasInjustificados'";
                        //                    if (ingDedempview.ToTable().Rows.Count > 0)
                        //                    {
                        //                        porctAusencia = ingDedempview[0][4].ToString() + "%";
                        //                    }
                        //                }
                        //                if (i == 2)
                        //                {
                        //                    ingDedempview.RowFilter = "detalle= 'Amonestaciones'";
                        //                    if (ingDedempview.ToTable().Rows.Count > 0)
                        //                    {
                        //                        porcAmonestacion = ingDedempview[0][4].ToString() + "%";
                        //                    }
                        //                }

                        //            }

                        //            DataView ingDedempviewOtras = ingDedemp.Copy().DefaultView;
                        //            ingDedempviewOtras.RowFilter = "codigo='" + codigo + "' and tipo=2 and (detalle<> 'Eficiencia' or detalle<> 'Amonestaciones' or detalle<> 'DiasInjustificados')";
                        //            if (ingDedempviewOtras.ToTable().Rows.Count > 0)
                        //            {
                        //                int c = 0;
                        //                foreach (DataRow item in ingDedempviewOtras.ToTable().Rows)
                        //                {
                        //                    c++;
                        //                    totalDeduccion += Convert.ToDecimal(item["valor"].ToString());


                        //                    if (ingDedempviewOtras.ToTable().Rows.Count > 1)
                        //                    {

                        //                        detalleDeduccion += item["detalle"].ToString();

                        //                        if (c < ingDedempviewOtras.ToTable().Rows.Count)
                        //                        {
                        //                            detalleDeduccion += ",";
                        //                        }
                        //                    }
                        //                    else
                        //                    { detalleDeduccion += item["detalle"].ToString(); }
                        //                }


                        //            }

                        //        }


                        //        dsInc.Rows.Add(codigo, depto, nombre, op, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, porcetRechazo, porctAusencia, porcAmonestacion, 0, 0, pagoManual, totalpago, 0, 0, 0, detalleDeduccion, totalDeduccion, 0);
                        //    }
                        //}

                        //DataView dtvempleadosNoProduccion2 = dtInfon.Copy().DefaultView;
                        //dtvempleadosNoProduccion2.RowFilter = "GeneradoSistema=" + false + " and operacion='NA'";
                        //if (dtvempleadosNoProduccion2.ToTable().Rows.Count > 0)
                        //{
                        //    foreach (DataRow itempem in dtvempleadosNoProduccion2.ToTable().Rows)
                        //    {
                        //        string nombre = "", op = "", depto = "", porcAmonestacion = "", porcetRechazo = "", porctAusencia = "", detalleDeduccion = "";
                        //        int codig0 = 0;
                        //        decimal totalDeduccion = 0;

                        //        nombre = itempem["nombrecompleto"].ToString();
                        //        depto = itempem["NombreDepto"].ToString();
                        //        codigo = int.Parse(itempem["codigo_empleado"].ToString());

                        //        if (codigo == 866369)
                        //        {
                        //        }

                        //        op = (itempem["operacion"].ToString().Trim());
                        //        if (itempem["operacion"].ToString() == "IF")
                        //        {
                        //            if (depto.Length > 2)
                        //            {
                        //                string dep = itempem["NombreDepto"].ToString();

                        //                depto = dep.Substring(7, 2);
                        //            }
                        //        }

                        //        decimal pagoManual = Convert.ToDecimal(itempem["IncentivoMeta"].ToString());
                        //        decimal totalpago = Convert.ToDecimal(itempem["TotalIncentivo"].ToString());


                        //        DataView ingDedview = dtIncDeducEmpleados.Copy().DefaultView;

                        //        ingDedview.RowFilter = "codigo='" + codigo + "'";
                        //        if (ingDedview.ToTable().Rows.Count > 0)
                        //        {
                        //            DataTable ingDedemp = ingDedview.ToTable();
                        //            for (int i = 0; i < 3; i++)
                        //            {

                        //                DataTable ingDedemp2 = ingDedemp.Copy();
                        //                DataView ingDedempview = ingDedemp2.DefaultView;
                        //                if (i == 0)
                        //                {
                        //                    ingDedempview.RowFilter = "detalle= 'Rechazo'";
                        //                    if (ingDedempview.ToTable().Rows.Count > 0)
                        //                    {
                        //                        porcetRechazo = ingDedempview[0][4].ToString() + "%";
                        //                    }
                        //                }
                        //                if (i == 1)
                        //                {
                        //                    ingDedempview.RowFilter = "detalle= 'DiasInjustificados'";
                        //                    if (ingDedempview.ToTable().Rows.Count > 0)
                        //                    {
                        //                        porctAusencia = ingDedempview[0][4].ToString() + "%";
                        //                    }
                        //                }
                        //                if (i == 2)
                        //                {
                        //                    ingDedempview.RowFilter = "detalle= 'Amonestaciones'";
                        //                    if (ingDedempview.ToTable().Rows.Count > 0)
                        //                    {
                        //                        porcAmonestacion = ingDedempview[0][4].ToString() + "%";
                        //                    }
                        //                }

                        //            }

                        //            DataView ingDedempviewOtras = ingDedemp.Copy().DefaultView;
                        //            ingDedempviewOtras.RowFilter = "codigo='" + codigo + "' and tipo=2 and (detalle<> 'Eficiencia' or detalle<> 'Amonestaciones' or detalle<> 'DiasInjustificados')";
                        //            if (ingDedempviewOtras.ToTable().Rows.Count > 0)
                        //            {
                        //                int c = 0;
                        //                foreach (DataRow item in ingDedempviewOtras.ToTable().Rows)
                        //                {
                        //                    c++;
                        //                    totalDeduccion += Convert.ToDecimal(item["valor"].ToString());


                        //                    if (ingDedempviewOtras.ToTable().Rows.Count > 1)
                        //                    {

                        //                        detalleDeduccion += item["detalle"].ToString();

                        //                        if (c < ingDedempviewOtras.ToTable().Rows.Count)
                        //                        {
                        //                            detalleDeduccion += ",";
                        //                        }
                        //                    }
                        //                    else
                        //                    { detalleDeduccion += item["detalle"].ToString(); }
                        //                }


                        //            }

                        //        }


                        //        dsInc.Rows.Add(codigo, depto, nombre, op, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, porcetRechazo, porctAusencia, porcAmonestacion, 0, 0, pagoManual, totalpago, 0, 0, 0, detalleDeduccion, totalDeduccion, 0);
                        //    }
                        //}


                    }
                }
            }


            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.DataSources.Clear();

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("../Reportes/PlanillaInventivoFormatoINGRV.rdlc");
            ReportDataSource source = new ReportDataSource("DataSet1", dsInc);
            ReportViewer1.LocalReport.DataSources.Add(source);
            ReportViewer1.LocalReport.Refresh();
        }

        public DataTable ArmarTabla(List<string> Modulos, DataTable datos, DataTable ingDed, DateTime fechaini, DateTime fechafin, int tipofiltro, DataTable dtempleadosg)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            DataTable dsInc = (DataTable)Session["dtInc"];


            int dia = 0, ano = 0, dia2 = 0, ano2 = 0;
            string mes = "", mes2 = "";
            dia = fechaini.Day;
            dia2 = fechafin.Day;

            mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtinfo.GetMonthName(fechaini.Month));
            mes2 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtinfo.GetMonthName(fechafin.Month));

            ano = fechaini.Year;
            ano2 = fechafin.Year;
            string fecha = "Semana del ";
            if (mes == mes2)
            {
                fecha += dia.ToString() + " al " + dia2.ToString() + " de " + mes + " " + ano.ToString();
            }
            else
            {
                fecha += dia.ToString() + " de " + mes + " " + ano.ToString() + " al " + dia2.ToString() + " de " + mes2 + " " + ano.ToString();
            }
            foreach (var item in Modulos)
            {
                DataTable dtprodModulo = Neg_Incentivos.PRODUCCIONXMODULOXDIA(fechaini, fechafin, int.Parse(item.ToString()));
                string porcetRechazo = "0%", porctAusencia = "0%", porcAmonestacion = "0%";
                DataTable datos3 = new DataTable();

                DataView dtvEmpxModulo = new DataView();

                if (tipofiltro == 1)
                {
                    datos3 = datos.Copy();
                    dtvEmpxModulo = datos3.DefaultView;
                    dtvEmpxModulo.RowFilter = "Modulo=" + int.Parse(item.ToString());
                    foreach (DataRow emp in dtvEmpxModulo.ToTable().Rows)
                    {
                        porcetRechazo = "0%"; porctAusencia = "0%"; porcAmonestacion = "0%";
                        DataTable ingDed2 = ingDed.Copy();
                        DataView ingDed2view = ingDed2.DefaultView;

                        ingDed2view.RowFilter = "codigo='" + emp[4].ToString() + "'";
                        if (ingDed2view.ToTable().Rows.Count > 0)
                        {
                            DataTable ingDedemp = ingDed2view.ToTable();
                            for (int i = 0; i < 3; i++)
                            {

                                DataTable ingDedemp2 = ingDedemp.Copy();
                                DataView ingDedempview = ingDedemp2.DefaultView;
                                if (i == 0)
                                {
                                    ingDedempview.RowFilter = "detalle= 'Rechazo'";
                                    if (ingDedempview.ToTable().Rows.Count > 0)
                                    {
                                        porcetRechazo = ingDedempview[0][4].ToString() + "%";
                                    }
                                }
                                if (i == 1)
                                {
                                    ingDedempview.RowFilter = "detalle= 'DiasInjustificados'";
                                    if (ingDedempview.ToTable().Rows.Count > 0)
                                    {
                                        porctAusencia = ingDedempview[0][4].ToString() + "%";
                                    }
                                }
                                if (i == 2)
                                {
                                    ingDedempview.RowFilter = "detalle= 'Amonestaciones'";
                                    if (ingDedempview.ToTable().Rows.Count > 0)
                                    {
                                        porcAmonestacion = ingDedempview[0][4].ToString() + "%";
                                    }
                                }

                            }

                        }
                        else
                        {
                            porcetRechazo = "0%";
                            porctAusencia = "0%";
                            porcAmonestacion = "0%";
                        }


                        dsInc.Rows.Add(emp[4].ToString(), emp[6].ToString(), emp[5].ToString(), emp[7].ToString(), dtprodModulo.Rows[0][1].ToString(), dtprodModulo.Rows[0][2].ToString(), dtprodModulo.Rows[0][3].ToString(), dtprodModulo.Rows[0][4].ToString(), dtprodModulo.Rows[0][5].ToString(), dtprodModulo.Rows[0][6].ToString(), dtprodModulo.Rows[0][7].ToString(), emp[10].ToString(), porcetRechazo, porctAusencia, porcAmonestacion, "C$" + emp[16].ToString(), "");

                    }
                }
                if (tipofiltro == 2)
                {
                    datos3 = dtempleadosg.Copy();
                    dtvEmpxModulo = datos3.DefaultView;
                    dtvEmpxModulo.RowFilter = "Modulo=" + int.Parse(item.ToString());
                    foreach (DataRow emp in dtvEmpxModulo.ToTable().Rows)
                    {
                        porcetRechazo = "0%"; porctAusencia = "0%"; porcAmonestacion = "0%";
                        bool produccion = false;
                        if (dtprodModulo != null)
                        {
                            if (dtprodModulo.Rows.Count > 0)
                            {
                                produccion = true;
                            }
                        }

                        if (produccion)
                        {
                            dsInc.Rows.Add(emp[3].ToString(), emp[14].ToString(), emp[10].ToString(), emp[16].ToString(), dtprodModulo.Rows[0][1].ToString(), dtprodModulo.Rows[0][2].ToString(), dtprodModulo.Rows[0][3].ToString(), dtprodModulo.Rows[0][4].ToString(), dtprodModulo.Rows[0][5].ToString(), dtprodModulo.Rows[0][6].ToString(), dtprodModulo.Rows[0][7].ToString(), "0.00", porcetRechazo, porctAusencia, porcAmonestacion, "C$00.0", "");
                        }
                        else
                        {
                            dsInc.Rows.Add(emp[3].ToString(), emp[14].ToString(), emp[11].ToString(), emp[16].ToString(), 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, porcetRechazo, porctAusencia, porcAmonestacion, "C$0.00", "");
                        }

                    }
                }

            }

            Session["dtInc"] = dsInc;
            return dsInc;
        }
        public DataTable CargarDll()
        {
            DataTable Modulos = new DataTable();
            if (txtperiodo.Text != "")
            {
                int periodo = int.Parse(txtperiodo.Text);
                Modulos = Neg_Incentivos.IncentivoModulosConMeta(periodo);
            }
            return Modulos;
        }

        public DataTable cargardllMSI()
        {
            List<string> modulosCI = new List<string>();
            List<string> modulosSI = new List<string>();
            List<string> modulos = moduloList();
            DataTable dtmodulosSI = new DataTable();

            dtmodulosSI.Columns.Add("ModuloNombre", typeof(string));
            dtmodulosSI.Columns.Add("MODULO", typeof(int));
            DataTable Modulos = new DataTable();
            if (txtperiodo.Text != "")
            {
                int periodo = int.Parse(txtperiodo.Text);
                Modulos = Neg_Incentivos.IncentivoModulosConMeta(periodo);
            }

            var modulo = ((from r in Modulos.AsEnumerable()
                           select r[Modulos.Columns[1].ToString()].ToString()).Distinct()).ToList();

            modulosCI = modulo.Select(s => (string)s).ToList();

            foreach (string m in modulos)
            {
                if (!modulosCI.Contains(m.Trim()))
                {
                    dtmodulosSI.Rows.Add("MODULO " + m.Trim(), int.Parse(m.Trim()));
                }

            }
            return dtmodulosSI;
        }

        protected void ddltipor_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (ddltipor.SelectedValue == "1")
            //{

            pnlcheck.Visible = true;
            //}
            //if (ddltipor.SelectedValue == "2")
            //{
            //    pnlcheck.Visible = true;
            //}
            //if (ddltipor.SelectedValue == "3")
            //{
            //    pnlcheck.Visible = false;
            //}

            //ddlProceso.Items.Clear();
            //if (ddltipor.SelectedValue == "1")
            //{

            //    ddlProceso.DataTextField = "ModuloNombre";
            //    ddlProceso.DataValueField = "MODULO";

            //    ddlProceso.DataSource = CargarDll();
            //}
            //if (ddltipor.SelectedValue == "2")
            //{
            //    ddlProceso.DataTextField = "ModuloNombre";
            //    ddlProceso.DataValueField = "MODULO";
            //    ddlProceso.DataSource = cargardllMSI();
            //}
            //ddlProceso.DataBind();
        }

        public List<string> moduloList()
        {
            DataTable dtmodulos = Neg_Incentivos.CosModulosSel();

            List<string> listamo = dtmodulos.Rows.OfType<DataRow>()
           .Select(dr => dr.Field<string>("idmodulo")).ToList();

            return listamo;
        }

        protected void txtperiodo_TextChanged(object sender, EventArgs e)
        {

            if (txtperiodo.Text != "")
            {
                pnlcheck.Visible = true;
            }
        }


    }
}