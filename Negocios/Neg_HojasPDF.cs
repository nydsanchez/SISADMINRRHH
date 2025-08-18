using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;
using System.Reflection;
using Datos;
using System.Net.Mail;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;
using System.Configuration;
using iTextSharp.text.html.simpleparser;
using System.Net;
using System.Text;
using System.Globalization;

namespace Negocios
{
    public class Neg_HojasPDF
    {
        #region REFERENCIAS

        Neg_Incentivos Neg_Incentivos = new Neg_Incentivos();
        #endregion

        public string armarEstructuraHojaReconciliacion(List<Neg_Incentivos.DetalleComprobante> lista, int tipo, int parametro, DataTable dt, int periodo)
        {
            List<int> listaCodigo = new List<int>();
            DataTable dtcodigos = new DataTable();
            #region
            if (tipo == 1 || tipo == 4) //MODULOS o TODOS
            {
                dtcodigos = Neg_Incentivos.IncentivoHistoricoSelectCodigoEmpleados(periodo, tipo, parametro);

                foreach (DataRow row in dtcodigos.Rows)
                {
                    listaCodigo.Add(int.Parse(row["codigo_empleado"].ToString()));

                }

            }
            else if (tipo == 2) //CODIGO
            {
                listaCodigo.Add(parametro);
            }
            else if (tipo == 3)
            {
                foreach (DataRow row in dt.Rows)
                {
                    listaCodigo.Add(int.Parse(row["codigo"].ToString().ToLower()));
                }
            }
            #endregion
            #region Datos  Cabecera
            string Lcodigo = "Empleado:", Lcuenta = "Cuenta:", Ldepto = "Depto:", Lproduccion = "Producción Escaneada :", LContruccion = "Construcción:", LImpresion = "Impresión:", LHEx = "HorasE:", LSEx = "PagoE:", LSeguro = "Inss:", LCedula = "Cédula:", LCargo = "Operación:";
            char pad = ' ';
            Lcodigo.PadRight(8, pad);
            Lcuenta.PadRight(8, pad);
            Ldepto.PadRight(8, pad);
            Lproduccion.PadRight(8, pad);
            LContruccion.PadRight(8, pad);
            LImpresion.PadRight(8, pad);
            LHEx.PadRight(8, pad);
            LSEx.PadRight(8, pad);
            LSeguro.PadRight(8, pad);
            LCedula.PadRight(8, pad);
            LCargo.PadRight(8, pad);
            #endregion
            string htmlfinal = "", htmltmp = "";
            Neg_Periodo NPeriodo = new Neg_Periodo();
            dsPlanilla.dtPeriodoDataTable dtPeriodo = NPeriodo.PeriodoSel(lista[0].Periodo);
            if (dtPeriodo != null)
            {
                if (dtPeriodo.Rows.Count > 0)
                {
                    foreach (int item in listaCodigo)
                    {
                        int contadorfilas = 0;
                        string Cimpresion = System.DateTime.Now.Date.ToString("dd/MM/yyyy");

                        List<Neg_Incentivos.DetalleComprobante> Semanas = (from i in lista where i.Codigo.Equals(item.ToString()) orderby i.Semana ascending select i).ToList();
                        int contador = 2;
                        decimal totalIngreso = 0;
                        decimal totalEgreso = 0;
                        decimal Total = 0;
                        htmltmp = "";
                        foreach (var empleado in Semanas)
                        {

                            totalIngreso += empleado.IncentivoMeta + empleado.TotalIngreso;
                            totalEgreso += empleado.TotalEgreso;
                            Total += empleado.TotalIncentivo;

                            if (contador == 2)
                            {
                                contador--;
                                htmltmp += "<font size='24px'; align='center'; color=\"#000000\"><b><i>KAIZEN</i></b></font><br/>" +
                                                          "<font size='18px'; align='center'; color=\"#000000\"><b><i>Colilla de pago del periodo  " + empleado.Periodo + ", del " + Convert.ToDateTime(dtPeriodo.Rows[0]["fechaini"].ToString()).ToShortDateString() + " al " + Convert.ToDateTime(dtPeriodo.Rows[0]["fechafin2"].ToString()).ToShortDateString() + "</i></b></font><br/>" +
                                                          "<font size='4px'>" +
                                                              "<table border='0' cellpadding='0' cellspacing='0'>" +
                                                                  "<tr>" +
                                                                       "<td width='30%'>" + Ldepto + "&nbsp;" + empleado.Modulo + "</td>" +
                                                                       "<td width='50%' >" + Lcodigo + "&nbsp;" + empleado.Codigo + " - " + empleado.Nombrecompleto + "</td>" +
                                                                       "<td width='20%' >" + LImpresion + "&nbsp;" + Cimpresion + "</td>" +

                                                                  "</tr>" +
                                                                   "<tr>" +
                                                                           "<td width='30%'>&nbsp;&nbsp;</td>" +
                                                                           "<td width='50%' cellspan=2>" + LCargo + "&nbsp;" + empleado.Operacion + "</td>" +
                                                                            "<td width='30%'>&nbsp;&nbsp;</td>" +
                                                                      "</tr>" +
                                                                      "<tr>" +
                                                                           "<td width='30%' color='#FFFFFF'>_</td>" +
                                                                           "<td width='50%' color='#FFFFFF'>_</td>" +
                                                                           "<td width='20%' color='#FFFFFF'>_</td>" +
                                                                      "</tr>" +
                                                             "</table>" +
                                                         "</font>";

                                contadorfilas += 5;
                            }

                            htmltmp += "<font size='4px'>" +
                                         "<table border='0' cellpadding='0' cellspacing='0'>" +

                                              "<tr>" +
                                                  "<td width='30%'><b>Semana&nbsp;" + empleado.Semana + "</b>&nbsp;&nbsp;" + Lproduccion + "&nbsp;" + empleado.Produccion + "&nbsp; Eficiencia:" + empleado.Eficienciaalcanzada + "%</td>" +
                                                  "<td width='20%'>&nbsp;" + LContruccion + "&nbsp;" + empleado.Contruccion + "</td>" +
                                             "</tr>" +
                                        "</table>" +
                                    "</font>";

                            htmltmp += "<font size='4px'>" +
                               "<table width='100%' border=1>" +
                                   "<tr>" +
                                          "<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Ingresos</b></td>" +
                                          "<td COLSPAN='12' width='10%' style='margin-right:5px;' align='center'><b>Egresos</b></td>" +
                                    "</tr>" +
                               "</table>" +
                               "<table width='100%' cellpadding='0' cellspacing='0'>";
                            contadorfilas += 2;


                            #region SECCION DE INGRESOS Y DEDUCCIONES
                            DataTable IGDE = empleado.DtIDInce;
                            DataTable Ingresos = empleado.DtIDInce;
                            DataTable Deducciones = empleado.DtIDInce;

                            DataTable Ingresos2 = Ingresos.Copy();
                            DataTable Deducciones2 = Deducciones.Copy();

                            DataView Ingresosvw = Ingresos2.DefaultView;
                            DataView Deduccionesvw = Deducciones2.DefaultView;

                            Ingresosvw.RowFilter = "Tipo=1";
                            Deduccionesvw.RowFilter = "Tipo=2";


                            List<int> listafilas = new List<int>();

                            //VARIABLES PARA MANEJAR NUMERO DE FILAS POR DATATABLE
                            int filasIngreso = 0;
                            int filasDeducciones = 0;

                            //VARIABLES PARA MANEJAR NUMERO DE FILAS POR DATATABLE
                            int CONTADORfilasIngreso = 0;
                            int CONTADORfilasDeducciones = 0;



                            //VARIABLES PARA MANEJAR NUMERO DE REGISTROS
                            int totalRegistrosIngresos = 0;
                            int totalRegistrosDeducciones = 0;

                            if (Ingresosvw != null)
                            {
                                if (Ingresosvw.Count > 0)
                                {
                                    totalRegistrosIngresos = Ingresosvw.Count;
                                    //SI ES PAR
                                    if ((totalRegistrosIngresos % 2) == 0)
                                    {
                                        filasIngreso = totalRegistrosIngresos / 2;

                                    }
                                    else
                                    {
                                        filasIngreso = (totalRegistrosIngresos - 1) / 2;
                                    }

                                    listafilas.Add(filasIngreso);
                                }
                            }

                            if (Deduccionesvw != null)
                            {
                                if (Deduccionesvw.Count > 0)
                                {
                                    totalRegistrosDeducciones = Deduccionesvw.Count;

                                    //SI ES PAR
                                    if ((totalRegistrosDeducciones % 2) == 0)
                                    {
                                        filasDeducciones = totalRegistrosDeducciones / 2;
                                    }
                                    else
                                    {
                                        filasDeducciones = (totalRegistrosDeducciones - 1) / 2;
                                    }
                                    listafilas.Add(filasDeducciones);
                                }

                            }

                            int mayor = 0, contadorfilasid = 0;
                            foreach (int numfilas in listafilas)
                            {
                                if (contador == 0)
                                {
                                    mayor = numfilas;
                                }
                                else
                                {
                                    if (mayor < numfilas)
                                        mayor = numfilas;
                                }
                                contadorfilasid++;

                            }
                            string detalle = "";
                            if (mayor <= 0)
                            {

                                if (totalRegistrosIngresos > 0)
                                {

                                    if (CONTADORfilasIngreso < totalRegistrosIngresos)
                                    {
                                        contadorfilas += 1;

                                        if (Ingresosvw[CONTADORfilasIngreso]["tipoCalc"].ToString() == "1")
                                        {
                                            if (Ingresosvw[CONTADORfilasIngreso]["Observacion"].ToString().ToLower() == "op")
                                            {
                                                detalle = Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                            }
                                            else
                                            {
                                                detalle = Ingresosvw[CONTADORfilasIngreso]["cantidad"].ToString() + "% = " + Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                            }
                                        }
                                        else
                                        {
                                            detalle = Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                        }
                                        htmltmp += "<tr>" +
                                    "<td COLSPAN='3' width='10%'>" + Ingresosvw[CONTADORfilasIngreso]["detalle"] + "</td>" +
                                    "<td COLSPAN='3' width='10%'>" + detalle + "</td>" +
                                    "<td COLSPAN='3' width='10%'>Incentivo por Meta </td>" +
                                    "<td COLSPAN='3' width='10%'>" + empleado.IncentivoMeta + "</td>";

                                        if (CONTADORfilasDeducciones < totalRegistrosDeducciones)
                                        {
                                            if (Deduccionesvw[CONTADORfilasDeducciones]["tipoCalc"].ToString() == "1")
                                            {
                                                detalle = Deduccionesvw[CONTADORfilasDeducciones]["cantidad"].ToString() + "% = " + Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                            }
                                            else
                                            {
                                                detalle = Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                            }
                                            htmltmp += "<td COLSPAN='3' width='10%'>" + Deduccionesvw[CONTADORfilasDeducciones]["detalle"] + "</td>" +
                                                       "<td COLSPAN='3' width='10%'>" + detalle + "</td>" +
                                                       "<td COLSPAN='3' width='10%'></td>" +
                                                       "<td COLSPAN='3' width='10%'></td>" +
                                                "</tr>";
                                            CONTADORfilasDeducciones++;
                                        }
                                        else
                                        {
                                            htmltmp += "<td COLSPAN='3' width='10%'></td>" +
                                                       "<td COLSPAN='3' width='10%'></td>" +
                                                       "<td COLSPAN='3' width='10%'></td>" +
                                                       "<td COLSPAN='3' width='10%'></td>" +
                                                "</tr>";

                                        }
                                        CONTADORfilasIngreso++;
                                    }

                                }
                                else if (totalRegistrosIngresos == 0)
                                {
                                    contadorfilas += 1;
                                    htmltmp += "<tr>" +
                                   "<td COLSPAN='3' width='10%'></td>" +
                                   "<td COLSPAN='3' width='10%'></td>" +
                                   "<td COLSPAN='3' width='10%'>Incentivo por Meta </td>" +
                                   "<td COLSPAN='3' width='10%'>" + empleado.IncentivoMeta + "</td>";

                                    if (CONTADORfilasDeducciones < totalRegistrosDeducciones)
                                    {
                                        if (Deduccionesvw[CONTADORfilasDeducciones]["tipoCalc"].ToString() == "1")
                                        {
                                            detalle = Deduccionesvw[CONTADORfilasDeducciones]["cantidad"].ToString() + "% = " + Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                        }
                                        else
                                        {
                                            detalle = Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                        }
                                        htmltmp += "<td COLSPAN='3' width='10%'>" + Deduccionesvw[CONTADORfilasDeducciones]["detalle"] + "</td>" +
                                                   "<td COLSPAN='3' width='10%'>" + detalle + "</td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                            "</tr>";
                                        CONTADORfilasDeducciones++;
                                    }
                                    else
                                    {
                                        htmltmp += "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                            "</tr>";

                                    }
                                    CONTADORfilasIngreso++;

                                }

                            }
                            else
                            {
                                //SE RECORRE UNA A UNA LAS FILAS QUE PUEDEN EXISTIR PARA INGRESOS Y DEDUCCIONES
                                for (int i = 0; i < mayor; i++)
                                {
                                    contadorfilas += 1;
                                    htmltmp += "<tr>";
                                    for (int j = 0; j < 2; j++)
                                    {
                                        //si aun tiene filas por recorrer
                                        if (filasIngreso > 0)
                                        {
                                            if (Ingresosvw[CONTADORfilasIngreso]["tipoCalc"].ToString() == "1")
                                            {
                                                if (Ingresosvw[CONTADORfilasIngreso]["Observacion"].ToString().ToLower() == "op")
                                                {
                                                    detalle = Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                                }
                                                else
                                                {
                                                    detalle = Ingresosvw[CONTADORfilasIngreso]["cantidad"].ToString() + "% = " + Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                                }
                                            }
                                            else
                                            {
                                                detalle = Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                            }
                                            if (CONTADORfilasIngreso < totalRegistrosIngresos)
                                            {

                                                htmltmp += "<td COLSPAN='3' width='10%'>" + Ingresosvw[CONTADORfilasIngreso]["detalle"] + "</td>" +
                                                           "<td COLSPAN='3' width='10%'>" + detalle + "</td>";


                                                CONTADORfilasIngreso++;
                                            }

                                            if (j == 1)
                                            {
                                                filasIngreso--;
                                            }

                                        }
                                        else
                                        {
                                            if (i == 1)
                                            {
                                                if (j == 1)
                                                {
                                                    htmltmp += "<td COLSPAN='3' width='10%'>Incentivo por Meta </td>" +
                                                                "<td COLSPAN='3' width='10%'>" + empleado.IncentivoMeta + "</td>";
                                                }
                                                else
                                                {
                                                    htmltmp += "<td COLSPAN='3' width='10%'> </td>" +
                                                               "<td COLSPAN='3' width='10%'></td>";
                                                }
                                            }
                                            else
                                            {
                                                htmltmp += "<td COLSPAN='3' width='10%'></td>" +
                                                  "<td COLSPAN='3' width='10%'></td>";

                                            }


                                        }

                                    }

                                    for (int k = 0; k < 2; k++)
                                    {

                                        //si aun tiene filas por recorrer
                                        if (filasDeducciones > 0)
                                        {
                                            if (Deduccionesvw[CONTADORfilasDeducciones]["tipoCalc"].ToString() == "1")
                                            {
                                                detalle = Deduccionesvw[CONTADORfilasDeducciones]["cantidad"].ToString() + "% = " + Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                            }
                                            else
                                            {
                                                detalle = Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                            }
                                            if (CONTADORfilasDeducciones < totalRegistrosDeducciones)
                                            {
                                                htmltmp += "<td COLSPAN='3' width='10%'>" + Deduccionesvw[CONTADORfilasDeducciones]["detalle"] + "</td>" +
                                                           "<td COLSPAN='3' width='10%'>" + detalle + "</td>";
                                                CONTADORfilasDeducciones++;
                                            }

                                            if (k == 1)
                                            {
                                                filasDeducciones--;
                                            }

                                        }
                                        htmltmp += "<td COLSPAN='3' width='10%'></td>" +
                                                  "<td COLSPAN='3' width='10%'></td>";

                                    }
                                    htmltmp += "</tr>";
                                }

                                if (CONTADORfilasIngreso < totalRegistrosIngresos)
                                {
                                    contadorfilas += 1;
                                    if (Ingresosvw[CONTADORfilasIngreso]["tipoCalc"].ToString() == "1")
                                    {
                                        detalle = Ingresosvw[CONTADORfilasIngreso]["cantidad"].ToString() + "% = " + Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                    }
                                    else
                                    {
                                        detalle = Ingresosvw[CONTADORfilasIngreso]["valor"].ToString();
                                    }
                                    htmltmp += "<tr>" +
                                                    "<td COLSPAN='3' width='10%'>" + Ingresosvw[CONTADORfilasIngreso]["detalle"] + "</td>" +
                                                     "<td COLSPAN='3' width='10%'>" + detalle + "</td>" +
                                                     "<td COLSPAN='3' width='10%'>Incentivo por Meta </td>" +
                                                     "<td COLSPAN='3' width='10%'>" + empleado.IncentivoMeta + "</td>";

                                    if (CONTADORfilasDeducciones < totalRegistrosDeducciones)
                                    {
                                        if (Deduccionesvw[CONTADORfilasDeducciones]["tipoCalc"].ToString() == "1")
                                        {
                                            detalle = Deduccionesvw[CONTADORfilasDeducciones]["cantidad"].ToString() + "% = " + Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                        }
                                        else
                                        {
                                            detalle = Deduccionesvw[CONTADORfilasDeducciones]["valor"].ToString();
                                        }
                                        htmltmp += "<td COLSPAN='3' width='10%'>" + Deduccionesvw[CONTADORfilasDeducciones]["detalle"] + "</td>" +
                                                   "<td COLSPAN='3' width='10%'>" + detalle + "</td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                            "</tr>";
                                        CONTADORfilasDeducciones++;
                                    }
                                    else
                                    {
                                        htmltmp += "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                                   "<td COLSPAN='3' width='10%'></td>" +
                                            "</tr>";

                                    }
                                    CONTADORfilasIngreso++;
                                }
                                else
                                {
                                    htmltmp += "<tr>" +
                               "<td COLSPAN='3' width='10%'>Incentivo por Meta</td>" +
                               "<td COLSPAN='3' width='10%'>" + empleado.IncentivoMeta + "</td>" +
                               "<td COLSPAN='3' width='10%'> </td>" +
                               "<td COLSPAN='3' width='10%'></td>";
                                    htmltmp += "<td COLSPAN='3' width='10%'></td>" +
                                               "<td COLSPAN='3' width='10%'></td>" +
                                               "<td COLSPAN='3' width='10%'></td>" +
                                                "<td COLSPAN='3' width='10%'></td>" +
                                               "</tr>";

                                }
                            }
                            contadorfilas += 1;
                            htmltmp += "<tr>" +
                         "<td COLSPAN='3' width='10%'> </td>" +
                         "<td COLSPAN='3' width='10%'></td>" +
                         "<td COLSPAN='3' width='10%'></td>" +
                         "<td COLSPAN='3' width='10%'></td>" +
                         "<td COLSPAN='3' width='10%'></td>" +
                         "<td COLSPAN='3' width='10%'></td>" +
                         "<td COLSPAN='3' width='10%'></td>" +
                         "<td COLSPAN='3' width='10%'></td>" +
                      "</tr>";

                            htmltmp += "</table><table width='100%' cellpadding='0' cellspacing='0' align='right'><tr>" +
                                        "<td COLSPAN='6' width='10%'></td>" +
                                        "<td COLSPAN='3' width='10%'><b>Total Ing:</b></td>" +
                                        "<td COLSPAN='3' width='10%'>" + (empleado.IncentivoMeta + empleado.TotalIngreso).ToString() + "</td>" +
                                        "<td COLSPAN='6' width='10%'></td>" +
                                        "<td COLSPAN='3' width='10%'><b>Total Egr:</b></td>" +
                                        "<td COLSPAN='3' width='10%'>" + totalEgreso.ToString() + "</td></tr>" +
                                     "</table>";

                            contadorfilas += 1;
                            #endregion
                        }


                        htmltmp += "<table width='100%' cellpadding='0' cellspacing='0'>";
                        //SI EL CONTADOR ES MAYOR A 0 INDICA QUE NNO HUBIERON 2 SEMANAS
                        if (contador == 1)
                        {
                            htmltmp += "<tr style='color:white;background-color=white;font-color:white;'>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                "</tr>";

                            contadorfilas += 1;
                            contador--;
                        }
                        htmltmp += "</table></font>";
                        htmltmp += "<br/><table width='90%';font size='4px';align='center'>" +
                  "<tr>" +
                    "<td  width='2%'></td>" +
                    "<td  width='2%'></td>" +
                    "<td  width='2%'></td>" +
                    "<td  width='2%'></td>" +
                    "<td  width='2%'></td>" +
                    "<td  width='2%'></td>" +
                    "<td  width='2%'></td>" +
                    "<td  width='2%'></td>" +
                    "<td  width='8%';><b>Ingresos</b></td>" +
                    "<td  width='8%';><b>Egresos</b></td>" +
                    "<td  width='8%';><b>Neto</b></td>" +
                 "</tr>" +
                  "<tr>" +
                     "<td  width='2%'></td>" +
                     "<td  width='2%'></td>" +
                     "<td  width='2%'></td>" +
                     "<td  width='2%'></td>" +
                     "<td  width='2%'></td>" +
                     "<td  width='2%'></td>" +
                     "<td  width='2%'></td>" +
                     "<td  width='2%'></td>" +
                     "<td  width='8%';>" + totalIngreso + "</td>" +
                     "<td  width='8%';>" + totalEgreso + "</td>" +
                     "<td  width='8%';>" + Total + "</td>" +
                  "</tr>" +
                  "<tr style='color:white;background-color=white;font-color:white;'>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                    "<td COLSPAN='3' width='10%'>blanck</td>" +
                                "</tr>" +
                  "</table>";

                        contadorfilas += 3;
                        int faltante = 0;

                        //hay 2 filas
                        if (contador == 0)
                        {

                            faltante = 19 - contadorfilas;

                        }

                        else if (contador == 1)
                        {

                            faltante = 38 - contadorfilas;
                        }
                        for (int i = 0; i < faltante; i++)
                        {
                            htmltmp += "<br/>";
                        }


                        htmltmp += "<table width='90%';font size='4px';align='center'>" +
                                  "<tr><td>_</td></tr>" +
                                  "<tr><td>_</td></tr>" +
                                   "<tr><td>_</td></tr>" +
                                  "</table>";
                        contadorfilas += 3;
                        htmltmp += htmltmp;
                        htmlfinal += htmltmp;

                    }
                }
            }
            return htmlfinal;

        }
        public void armarEstructuraHojaInventivosPDF(string nombre, DataTable datos, DataTable ingDed)
        {
            if (datos != null)
            {
                if (datos.Rows.Count > 0)
                {

                    string html = "";

                    Document doc = new Document(PageSize.LETTER.Rotate(), 40, 40, 40, 40);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(nombre, FileMode.OpenOrCreate, FileAccess.ReadWrite));
                    doc.Open();

                    iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
                    iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);


                    DataTable datos2 = new DataTable();

                    datos2 = datos.Copy();
                    DataView dtvmodulos = datos2.DefaultView;

                    var Modulos = (from r in datos2.AsEnumerable()
                                   select r[datos.Columns[6].ToString()]).Distinct().ToList();
                    int contadorfilas = 0;
                    DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
                    foreach (var item in Modulos)
                    {

                        contadorfilas = 2;
                        DataTable dtprodModulo = Neg_Incentivos.PRODUCCIONXMODULOXDIA(Convert.ToDateTime(datos.Rows[0][2]), Convert.ToDateTime(datos.Rows[0][3]), int.Parse(item.ToString()));

                        int dia = 0, ano = 0, dia2 = 0, ano2 = 0;
                        string mes = "", mes2 = "";
                        dia = Convert.ToDateTime(datos.Rows[0][2]).Day;
                        dia2 = Convert.ToDateTime(datos.Rows[0][3]).Day;

                        mes = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtinfo.GetMonthName(Convert.ToDateTime(datos.Rows[0][2]).Month));
                        mes2 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dtinfo.GetMonthName(Convert.ToDateTime(datos.Rows[0][3]).Month));

                        ano = Convert.ToDateTime(datos.Rows[0][2]).Year;
                        ano2 = Convert.ToDateTime(datos.Rows[0][3]).Year;
                        string fecha = "Semana del ";
                        if (mes == mes2)
                        {
                            fecha += dia.ToString() + " al " + dia2.ToString() + " de " + mes + " " + ano.ToString();
                        }
                        else
                        {
                            fecha += dia.ToString() + " de " +mes +" "+ano.ToString()+ " al " + dia2.ToString() + " de " + mes2 + " " + ano.ToString();
                        }

                        DataTable datos3 = new DataTable();

                        datos3 = datos.Copy();
                        DataView dtvEmpxModulo = datos3.DefaultView;

                        dtvEmpxModulo.RowFilter = "Modulo=" + int.Parse(item.ToString());

                        html += "<table width='100%'>"
                                                      + "<tr>"
                                                          + "<td align='center' border=1 colspan='22'> " + fecha + " </td>"
                                                      + "</tr>"
                                                      + "<tr>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Codigo Emp</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Módulo</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='2'> Nombre Completo</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Op</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Prod Lunes</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Prod Martes</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Prod Miérc.</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Prod Jueves</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Prod Viernes</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Prod Sábado</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='2'> Total DZ</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> DZ a Pagar</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Penaliz Rech.</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='2'> Penaliz Ausencias Inj</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='2'> Penaliz Amonestaciones</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='1'> Incent. Produc</td>"
                                                           + "<td size='2px' align='center' border=1 colspan='2'> FIRMA</td>"

                                                       + "</tr>";


                        foreach (DataRow emp in dtvEmpxModulo.ToTable().Rows)
                        {
                            contadorfilas = contadorfilas + 2;
                            string porcetRechazo = "0%", porctAusencia = "0%", porcAmonestacion = "0%";
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
                            html += "<tr>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'>" + emp[4].ToString() + " </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + emp[6].ToString() + "  </td>"
                                                           + "<td  style='height:15px;' size='1px' align='left' border=1 colspan='2'>" + emp[5].ToString() + " </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + emp[7].ToString() + "  </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + dtprodModulo.Rows[0][1].ToString() + "</td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + dtprodModulo.Rows[0][2].ToString() + "  </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + dtprodModulo.Rows[0][3].ToString() + "  </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + dtprodModulo.Rows[0][4].ToString() + " </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + dtprodModulo.Rows[0][5].ToString() + "  </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + dtprodModulo.Rows[0][6].ToString() + " </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='2'> " + dtprodModulo.Rows[0][7].ToString() + "  </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + emp[10].ToString() + "   </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> " + porcetRechazo + " </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='2'> " + porctAusencia + "</td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='2'> " + porcAmonestacion + "</td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='1'> C$" + emp[16].ToString() + " </td>"
                                                           + "<td  size='1px' align='left' border=1 colspan='2'>    </td>"
                                                        + "</tr>";

                        }
                        html += "</table>";

                        int faltante = 0;
                        faltante = 36 - contadorfilas;

                        for (int i = 0; i < faltante; i++)
                        {
                            html += "<br/>";
                        }

                        //if (contadorfilas < 17)
                        //{
                        //    for (int i = contadorfilas; i < 18; i++)
                        //    {
                        //        html += "<p><br /><br /></p>";

                        //    }
                        //}
                    }






                    hw.Parse(new StringReader(html));
                    doc.Close();
                    writer.Close();


                }
            }


        }

        public void ShowPdf(string path)
        {

            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + path);
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.WriteFile(path);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
        }

        public void ConfigTabla(PdfPTable tableprincipal, int alineacion, bool locked, bool border)
        {
            tableprincipal.HorizontalAlignment = alineacion;                 //le indicamos la alineación horizontal
            tableprincipal.LockedWidth = locked;                       //enllavamos el ancho de la tabla

            if (!border)
                tableprincipal.DefaultCell.Border = Rectangle.NO_BORDER;
        }

        private static void addCell(PdfPTable table, string text, string colortexto, bool colorFondotexto, int rowspan, int colspan, int alig, int fontT, int border, bool conborde, int tamaletra)
        {
            //definimos el tipo de fuente a util0ar
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

            BaseColor color;
            if (colortexto == "black")
                color = iTextSharp.text.BaseColor.BLACK;
            else if (colortexto == "red")
                color = iTextSharp.text.BaseColor.RED;
            else
                color = new BaseColor(40, 43, 97);
            //se define el color y estilo de fuente
            Font font = new Font(bfTimes, tamaletra, fontT, color);
            Chunk Ctexto = new Chunk(text + Environment.NewLine, font);

            PdfPCell cell = new PdfPCell(new Phrase(Ctexto));
            cell.Rowspan = rowspan;
            cell.Colspan = colspan;

            if (conborde)
                cell.Border = border;

            cell.HorizontalAlignment = alig;  //alineación 0=Left, 1=Centre, 2=Right

            cell.VerticalAlignment = Element.ALIGN_MIDDLE;

            //if (colorFondotexto)
            //    cell.BackgroundColor = new BaseColor(97, 180, 244);


            table.AddCell(cell);
        }
        private void addTable(PdfPTable table, string param, int border, bool conborde)
        {
            string parametros = param;
            string[] p = parametros.Split('#');
            string Texto, ColorTexto, ColorFondoT, roswspan, colspan;
            int Alig, fontT, c = 0, c2 = 0, c3 = 0, tama = 0;

            c = p.Count();
            c2 = c;
            c2 = c2 / 8;

            for (int i = 0; i < c2; i++)
            {
                Texto = p[c3];
                c3 = c3 + 1;

                ColorTexto = p[c3];
                c3 = c3 + 1;

                ColorFondoT = p[c3];
                c3 = c3 + 1;

                roswspan = p[c3];
                c3 = c3 + 1;

                colspan = p[c3];
                c3 = c3 + 1;

                Alig = int.Parse(p[c3].ToString());
                c3 = c3 + 1;

                fontT = int.Parse(p[c3].ToString());
                c3 = c3 + 1;


                tama = int.Parse(p[c3].ToString());
                c3 = c3 + 1;

                addCell(table, Texto, ColorTexto, Convert.ToBoolean(ColorFondoT), int.Parse(roswspan), int.Parse(colspan), Alig, fontT, border, conborde, tama);
            }

        }

        private void espacioenblanco(PdfPTable table, int iteracion)
        {
            string cadena = "";
            for (int i = 0; i <= iteracion; i++)
            {
                cadena = "" + "  #black#false#1#1#1#0#10";
                addTable(table, cadena, 0, false);
            }

        }

    }
}
