using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Datos;
using System.Web;

namespace Negocios
{
    public class Neg_PllanillaQuincenal
    {
        #region
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Dato_Planilla Dato_Planilla = new Dato_Planilla();
        Dato_Factores Dato_Factores = new Dato_Factores();
        #endregion

        public bool InsertarIngrDeducQuincenales(int tipo, int codEmpleado, int idTipo, int periodo, decimal valor, string user, int tPlanilla)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            if (Dato_Planilla.InsertarIngrDeducQuincenales(tipo, codEmpleado, idTipo, periodo, valor, user, tPlanilla, userDetail.getIDEmpresa()))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public int procesarPlanillaQuincenal(int periodo, int tipoPlanilla, int tPlanilla, string user, int ubicacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            DataTable ds = new DataTable();
            DataTable df = new DataTable();
            DataTable di = new DataTable();
            DataTable de = new DataTable();
            DataTable dp = new DataTable();
            DataTable dh = new DataTable();
            DataTable dir = new DataTable();
            DataTable dret = new DataTable();
            //Set de Datos para Deducciones
            DataTable dt = new DataTable();
            DataTable dd = new DataTable();

            df = Dato_Factores.obtenerFactor(userDetail.getIDEmpresa());
            ds = Dato_Planilla.obtenerEmpleadosPlanillaQuincenal(periodo, tipoPlanilla, ubicacion, userDetail.getIDEmpresa());
            di = Dato_Planilla.obtenerIngresosQuincenalesASumar(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            //dp = Dato_Planilla.ObtenerPermisosPlanillaQuinc(periodo, tipoPlanilla);
            dh = Dato_Factores.obtenerTurnos(userDetail.getIDEmpresa());
            decimal factorInss = Convert.ToDecimal(df.Rows[1]["factor"].ToString());
            decimal factorPatronal = Convert.ToDecimal(df.Rows[2]["factor"].ToString());
            decimal factorInatec = Convert.ToDecimal(df.Rows[3]["factor"].ToString());
            decimal horasCompletasSemana = Convert.ToDecimal(dh.Rows[0]["horasCompletasSemana"].ToString());
            decimal horasTurno = Convert.ToDecimal(dh.Rows[0]["horasTotalTurno"].ToString());
            decimal htMinimasDiurno = Convert.ToDecimal(dh.Rows[0]["horasMinimoSemana"].ToString());

            foreach (DataRow dr in ds.Rows)
            {
                //Ingresos Extras
                foreach (DataRow dr1 in di.Rows)
                {
                    if (dr["codigo_empleado"].ToString().ToUpper().Trim() == dr1["codigo_empleado"].ToString().ToUpper().Trim() && dr1["aplicaINSS"].ToString() == "True" && dr1["aplicaIR"].ToString() == "True")
                    {
                        decimal diasPago = 0;
                        //Si Ingreso con el periodo iniciado
                        if (Convert.ToDateTime(dr["fecha_ingreso"]) > Convert.ToDateTime(dr["fechaini"]))
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fecha_ingreso"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }
                        else
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fechaini"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }

                        decimal quincenaPago = (Convert.ToDecimal(dr["salariomensual"]) / 30) * diasPago;
                        decimal inssQuincena = (quincenaPago * factorInss) / 100;
                        decimal netoQuincena = quincenaPago - inssQuincena;
                        decimal inssPatronal = quincenaPago * factorPatronal;
                        decimal inatec = quincenaPago * factorInatec;
                  
                        Dato_Planilla.insertarCalculosPlanillaQuincenal(1, Convert.ToInt32(dr["codigo_empleado"]), Convert.ToDateTime(dr["fechaini"]), Convert.ToDateTime(dr["fechafin2"]),
                            Convert.ToInt32(dr["mesplanilla"]), dr["nombrecompleto"].ToString(), Convert.ToDecimal(dr["salariomensual"]), 120, quincenaPago, inssQuincena,
                            netoQuincena, Convert.ToDecimal(dr1["valor"]), periodo, 0, inssPatronal, inatec, dr["codigo_depto"].ToString(), tPlanilla, user, tipoPlanilla, userDetail.getIDEmpresa());
                    }
                    if (dr["codigo_empleado"].ToString().ToUpper().Trim() == dr1["codigo_empleado"].ToString().ToUpper().Trim() && dr1["aplicaINSS"].ToString() == "False" && dr1["aplicaIR"].ToString() == "False")
                    {
                        decimal diasPago = 0;
                        //Si Ingreso con el periodo iniciado
                        if (Convert.ToDateTime(dr["fecha_ingreso"]) > Convert.ToDateTime(dr["fechaini"]))
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fecha_ingreso"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }
                        else
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fechaini"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }

                        decimal quincenaPago = (Convert.ToDecimal(dr["salariomensual"]) / 30) * diasPago;
                        decimal inssQuincena = (quincenaPago * factorInss) / 100;
                        decimal netoQuincena = quincenaPago - inssQuincena;
                        decimal inssPatronal = quincenaPago * factorPatronal;
                        decimal inatec = quincenaPago * factorInatec;

                        Dato_Planilla.insertarCalculosPlanillaQuincenal(5, Convert.ToInt32(dr["codigo_empleado"]), Convert.ToDateTime(dr["fechaini"]), Convert.ToDateTime(dr["fechafin2"]),
                         Convert.ToInt32(dr["mesplanilla"]), dr["nombrecompleto"].ToString(), Convert.ToDecimal(dr["salariomensual"]), 120, quincenaPago, inssQuincena,
                         netoQuincena, Convert.ToDecimal(dr1["valor"]), periodo, 0, inssPatronal, inatec, dr["codigo_depto"].ToString(), tPlanilla, user, tipoPlanilla, userDetail.getIDEmpresa());
                    }
                }

                //No hay Ingresos extras
                decimal diasPago2 = 0;
                //Si Ingreso con el periodo iniciado
                if (Convert.ToDateTime(dr["fecha_ingreso"]) > Convert.ToDateTime(dr["fechaini"]))
                {
                    TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fecha_ingreso"]));
                    diasPago2 = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                }
                else
                {
                    TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fechaini"]));
                    diasPago2 = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                }
               
                decimal quincenaPago2 = (Convert.ToDecimal(dr["salariomensual"]) / 30) * diasPago2;
                decimal inssQuincena2 = (quincenaPago2 * factorInss) / 100;
                decimal netoQuincena2 = quincenaPago2 - inssQuincena2;
                decimal inssPatronal2 = quincenaPago2 * factorPatronal;
                decimal inatec2 = quincenaPago2 * factorInatec;

                Dato_Planilla.insertarCalculosPlanillaQuincenal(0, Convert.ToInt32(dr["codigo_empleado"]), Convert.ToDateTime(dr["fechaini"]), Convert.ToDateTime(dr["fechafin2"]),
                        Convert.ToInt32(dr["mesplanilla"]), dr["nombrecompleto"].ToString(), Convert.ToDecimal(dr["salariomensual"]), 120, quincenaPago2, inssQuincena2,
                        netoQuincena2, 0, periodo, 0, inssPatronal2, inatec2, dr["codigo_depto"].ToString(), tPlanilla, user, tipoPlanilla, userDetail.getIDEmpresa());
              
            }

            //CALCULO DE IR
            dir = Dato_Planilla.ObtenerPlanillaQuincenal(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            dret = Dato_Factores.SeleccionarRetencionesIR(userDetail.getIDEmpresa());

            decimal irDesdT1 = Convert.ToDecimal(dret.Rows[0]["rentadesde"].ToString());
            decimal irHastT1 = Convert.ToDecimal(dret.Rows[0]["rentahasta"].ToString());
            decimal irSobrExcesT1 = Convert.ToDecimal(dret.Rows[0]["SobreExceso"].ToString());
            decimal irDesdT2 = Convert.ToDecimal(dret.Rows[1]["rentadesde"].ToString());
            decimal irHastT2 = Convert.ToDecimal(dret.Rows[1]["rentahasta"].ToString());
            decimal ImpBaseT2 = Convert.ToDecimal(dret.Rows[1]["ImpuestoBase"].ToString());
            decimal irSobrExcesT2 = Convert.ToDecimal(dret.Rows[1]["SobreExceso"].ToString());
            decimal irDesdT3 = Convert.ToDecimal(dret.Rows[2]["rentadesde"].ToString());
            decimal irHastT3 = Convert.ToDecimal(dret.Rows[2]["rentahasta"].ToString());
            decimal ImpBaseT3 = Convert.ToDecimal(dret.Rows[2]["ImpuestoBase"].ToString());
            decimal irSobrExcesT3 = Convert.ToDecimal(dret.Rows[2]["SobreExceso"].ToString());
            decimal irDesdT4 = Convert.ToDecimal(dret.Rows[3]["rentadesde"].ToString());
            decimal irHastT4 = Convert.ToDecimal(dret.Rows[3]["rentahasta"].ToString());
            decimal ImpBaseT4 = Convert.ToDecimal(dret.Rows[3]["ImpuestoBase"].ToString());
            decimal irSobrExcesT4 = Convert.ToDecimal(dret.Rows[3]["SobreExceso"].ToString());

            foreach (DataRow dr3 in dir.Rows)
            {
                int codigo = Convert.ToInt32(dr3["codigo_empleado"]);
                decimal ingresosDeducInss = Convert.ToDecimal(dr3["ingresos"].ToString()) - Convert.ToDecimal(dr3["dsegurosocial"].ToString());
                decimal iR = 0;
                iR = ingresosDeducInss * 24;
                if (iR < irDesdT1)
                    iR = 0;
                else
                {
                    //primerRango
                    if (iR >= irDesdT1 && iR <= irHastT1)
                        iR = ((((iR - irSobrExcesT1) * 0.15M) / 24));
                    //segundoRango
                    if (iR >= irDesdT2 && iR <= irHastT2)
                        iR = (((((iR - irSobrExcesT2) * 0.20M) + ImpBaseT2) / 24));
                    //tercerRango
                    if (iR >= irDesdT3 && iR <= irHastT3)
                        iR = (((((iR - irSobrExcesT3) * 0.25M) + ImpBaseT3) / 24));
                    //cuartoRango
                    if (iR >= irDesdT4 && iR <= irHastT4)
                        iR = (((((iR - irSobrExcesT4) * 0.30M) + ImpBaseT4) / 24));

                    Dato_Planilla.insertarCalculoIRPlanillaQuincenal(Convert.ToInt32(dr3["codigo_empleado"]),
                        iR, periodo, userDetail.getIDEmpresa());
                }
            }

            //DEDUCCIONES
            dd = Dato_Planilla.ObtenerDeduccionesQuincenales(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            dt = Dato_Planilla.ObtenerPlanillaQuincenal(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            int bandera = 0;
            foreach (DataRow dr4 in dt.Rows)
            {
                decimal deduccion = 0;
                foreach (DataRow dr2 in dd.Rows)
                {
                    //Si la deduccion no es porcentual al ingreso pension alimenticia
                    if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 1 &&
                        Convert.ToInt32(dr2["id_Tipo"].ToString().ToUpper().Trim()) == 3 && dr2["porcentual"].ToString().Trim() == "False")
                    {
                        bandera = 1;
                        deduccion = Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim());
                    }

                     //Si la deduccion es porcentual al ingreso pension alimenticia
                    else if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 1 &&
                    Convert.ToInt32(dr2["id_Tipo"].ToString().ToUpper().Trim()) == 3 && dr2["porcentual"].ToString().Trim() == "True")
                    {
                        bandera = 1;
                        deduccion = ((Convert.ToDecimal(dr4["ingresos"].ToString().ToUpper().Trim()) * Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim())) / 100);
                    }

                    //Si la deduccion no es porcentual al ingreso, embargo por banco
                    else if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 2
                        && dr2["porcentual"].ToString().Trim() == "False")
                    {
                        bandera = 1;
                        deduccion = Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim());
                    }

                    //Si la deduccion es porcentual al ingreso, embargo por banco
                    else if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 2 &&
                    dr2["porcentual"].ToString().Trim() == "True")
                    {
                        bandera = 1;
                        deduccion = ((Convert.ToDecimal(dr4["ingresos"].ToString().ToUpper().Trim()) * Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim())) / 100);
                    }

                    //Demas deducciones
                    else if (Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) > 2 && dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim()
                        && Convert.ToInt32(dr2["periodo"].ToString().ToUpper().Trim()) == Convert.ToInt32(dr4["periodo"].ToString().ToUpper().Trim()))
                    {
                        bandera = 1;
                        deduccion = Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim());

                        //salarioNeto -= deducir;
                    }

                    else bandera = 0;
                    if (bandera == 1)
                    {
                        bandera = 0;
                        Dato_Planilla.aplicarDeduccionesQuincenales(Convert.ToInt32(dr4["periodo"].ToString().ToUpper().Trim()),
                        Convert.ToInt32(dr4["codigo_empleado"].ToString().ToUpper().Trim()), deduccion, Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()), tipoPlanilla, userDetail.getIDEmpresa());
                    }
                }

            }

            //Desglose de monedas
            dt = Dato_Planilla.ObtenerPlanillaQuincenal(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            foreach (DataRow dr4 in dt.Rows)
            {
                decimal restante1 = 0;
                decimal restante2 = 0;
                decimal restante3 = 0;
                decimal restante4 = 0;
                decimal restante5 = 0;
                decimal restante6 = 0;
                decimal restante7 = 0;
                decimal restante8 = 0;
                decimal restante9 = 0;
                decimal restante10 = 0;
                decimal restante11 = 0;
                decimal restante12 = 0;
                decimal restante13 = 0;
                int codEmpleado = Convert.ToInt32(dr4["codigo_empleado"].ToString().ToUpper().Trim());
                decimal salarioNeto = Math.Truncate(Convert.ToDecimal(dr4["neto"].ToString().ToUpper().Trim()));
                decimal decimales = Convert.ToDecimal(dr4["neto"].ToString().ToUpper().Trim()) - salarioNeto;

                int d500 = Convert.ToInt32(Math.Truncate(salarioNeto / 500));
                if (d500 >= 1)
                    restante1 = d500 * 500;
                else
                    restante1 = salarioNeto;

                int d200 = Convert.ToInt32(Math.Truncate((salarioNeto - restante1) / 200));
                if (d200 >= 1)
                {
                    restante2 = d200 * 200;
                    restante2 += restante1;
                }
                else
                    restante2 = restante1;

                int d100 = Convert.ToInt32(Math.Truncate((salarioNeto - restante2) / 100));
                if (d100 >= 1)
                {
                    restante3 = d100 * 100;
                    restante3 += restante2;
                }
                else
                    restante3 = restante2;

                int d50 = Convert.ToInt32(Math.Truncate((salarioNeto - restante3) / 50));
                if (d50 >= 1)
                {
                    restante4 = d50 * 50;
                    restante4 += restante3;
                }
                else
                    restante4 = restante3;
                int d20 = Convert.ToInt32(Math.Truncate((salarioNeto - restante4) / 20));
                if (d20 >= 1)
                {
                    restante5 = d20 * 20;
                    restante5 += restante4;
                }
                else
                    restante5 = restante4;
                int d10 = Convert.ToInt32(Math.Truncate((salarioNeto - restante5) / 10));
                if (d10 >= 1)
                {
                    restante6 = d10 * 10;
                    restante6 += restante5;
                }
                else
                    restante6 = restante5;
                int d5 = Convert.ToInt32(Math.Truncate((salarioNeto - restante6) / 5));
                if (d5 >= 1)
                {
                    restante7 = d5 * 5;
                    restante7 += restante6;
                }
                else
                    restante7 = restante6;
                int d1 = Convert.ToInt32(Math.Truncate((salarioNeto - restante7) / 1));
                if (d1 >= 1)
                {
                    restante8 = d1 * 1;
                    restante8 += restante7;
                }
                else
                    restante8 = restante7;

                int d05 = Convert.ToInt32(Math.Truncate(decimales / 0.5M));
                if (d05 <= 1)
                {
                    restante9 = d05 * 0.5M;
                }
                else
                    restante9 = decimales;

                int d025 = Convert.ToInt32(Math.Truncate((decimales - restante9) / 0.25M));
                if (d025 >= 1)
                {
                    restante10 = d025 * 0.25M;
                    restante10 += restante9;
                }
                else
                    restante10 = restante9;

                int d010 = Convert.ToInt32(Math.Truncate((decimales - restante10) / 0.10M));
                if (d010 >= 1)
                {
                    restante11 = d010 * 0.10M;
                    restante11 += restante10;
                }
                else
                {
                    restante11 = restante10;
                }

                int d005 = Convert.ToInt32(Math.Truncate((decimales - restante11) / 0.05M));
                if (d005 >= 1)
                {
                    restante12 = d005 * 0.05M;
                    restante12 += restante11;
                }
                else
                {
                    restante12 = restante11;
                }

                int d001 = Convert.ToInt32(Math.Truncate((decimales - restante12) / 0.01M));
                if (d001 >= 1)
                {
                    restante13 = d001 * 0.01M;
                }
                Dato_Planilla.distribuirDenominacionesMonedaQuincenal(d500, d200, d100, d50, d20, d10, d5, d1, d05, d025, d010, d005, d001, codEmpleado, periodo, tipoPlanilla, userDetail.getIDEmpresa());
            }
            int a = 0;
                return a;
        }

        public int ReprocesarPlanillaQuincenal(int periodo, int tipoPlanilla, int tPlanilla, string user, int ubicacion)
        {
            IUserDetail userDetail = UserDetailResolver.getUserDetail();
            Dato_Planilla.EliminarPlanillaQuincenal(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            DataTable ds = new DataTable();
            DataTable df = new DataTable();
            DataTable di = new DataTable();
            DataTable de = new DataTable();
            DataTable dp = new DataTable();
            DataTable dh = new DataTable();
            DataTable dir = new DataTable();
            DataTable dret = new DataTable();
            //Set de Datos para Deducciones
            DataTable dt = new DataTable();
            DataTable dd = new DataTable();

            df = Dato_Factores.obtenerFactor(userDetail.getIDEmpresa());
            ds = Dato_Planilla.obtenerEmpleadosPlanillaQuincenal(periodo, tipoPlanilla, ubicacion, userDetail.getIDEmpresa());
            di = Dato_Planilla.obtenerIngresosQuincenalesASumar(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            //dp = Dato_Planilla.ObtenerPermisosPlanillaQuinc(periodo, tipoPlanilla);
            dh = Dato_Factores.obtenerTurnos(userDetail.getIDEmpresa());
            decimal factorInss = Convert.ToDecimal(df.Rows[1]["factor"].ToString());
            decimal factorPatronal = Convert.ToDecimal(df.Rows[2]["factor"].ToString());
            decimal factorInatec = Convert.ToDecimal(df.Rows[3]["factor"].ToString());
            decimal horasCompletasSemana = Convert.ToDecimal(dh.Rows[0]["horasCompletasSemana"].ToString());
            decimal horasTurno = Convert.ToDecimal(dh.Rows[0]["horasTotalTurno"].ToString());
            decimal htMinimasDiurno = Convert.ToDecimal(dh.Rows[0]["horasMinimoSemana"].ToString());

            foreach (DataRow dr in ds.Rows)
            {
                //Ingresos Extras
                foreach (DataRow dr1 in di.Rows)
                {
                    if (dr["codigo_empleado"].ToString().ToUpper().Trim() == dr1["codigo_empleado"].ToString().ToUpper().Trim() && dr1["aplicaINSS"].ToString() == "True" && dr1["aplicaIR"].ToString() == "True")
                    {
                        decimal diasPago = 0;
                        //Si Ingreso con el periodo iniciado
                        if (Convert.ToDateTime(dr["fecha_ingreso"]) > Convert.ToDateTime(dr["fechaini"]))
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fecha_ingreso"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }
                        else
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fechaini"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }

                        decimal quincenaPago = (Convert.ToDecimal(dr["salariomensual"]) / 30) * diasPago;
                        decimal inssQuincena = (quincenaPago * factorInss) / 100;
                        decimal netoQuincena = quincenaPago - inssQuincena;
                        decimal inssPatronal = quincenaPago * factorPatronal;
                        decimal inatec = quincenaPago * factorInatec;

                        Dato_Planilla.insertarCalculosPlanillaQuincenal(1, Convert.ToInt32(dr["codigo_empleado"]), Convert.ToDateTime(dr["fechaini"]), Convert.ToDateTime(dr["fechafin2"]),
                            Convert.ToInt32(dr["mesplanilla"]), dr["nombrecompleto"].ToString(), Convert.ToDecimal(dr["salariomensual"]), 120, quincenaPago, inssQuincena,
                            netoQuincena, Convert.ToDecimal(dr1["valor"]), periodo, 0, inssPatronal, inatec, dr["codigo_depto"].ToString(), tPlanilla, user, tipoPlanilla, userDetail.getIDEmpresa());
                    }
                    if (dr["codigo_empleado"].ToString().ToUpper().Trim() == dr1["codigo_empleado"].ToString().ToUpper().Trim() && dr1["aplicaINSS"].ToString() == "False" && dr1["aplicaIR"].ToString() == "False")
                    {
                        decimal diasPago = 0;
                        //Si Ingreso con el periodo iniciado
                        if (Convert.ToDateTime(dr["fecha_ingreso"]) > Convert.ToDateTime(dr["fechaini"]))
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fecha_ingreso"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }
                        else
                        {
                            TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fechaini"]));
                            diasPago = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                        }

                        decimal quincenaPago = (Convert.ToDecimal(dr["salariomensual"]) / 30) * diasPago;
                        decimal inssQuincena = (quincenaPago * factorInss) / 100;
                        decimal netoQuincena = quincenaPago - inssQuincena;
                        decimal inssPatronal = quincenaPago * factorPatronal;
                        decimal inatec = quincenaPago * factorInatec;

                        Dato_Planilla.insertarCalculosPlanillaQuincenal(5, Convert.ToInt32(dr["codigo_empleado"]), Convert.ToDateTime(dr["fechaini"]), Convert.ToDateTime(dr["fechafin2"]),
                         Convert.ToInt32(dr["mesplanilla"]), dr["nombrecompleto"].ToString(), Convert.ToDecimal(dr["salariomensual"]), 120, quincenaPago, inssQuincena,
                         netoQuincena, Convert.ToDecimal(dr1["valor"]), periodo, 0, inssPatronal, inatec, dr["codigo_depto"].ToString(), tPlanilla, user, tipoPlanilla, userDetail.getIDEmpresa());
                    }
                }

                //No hay Ingresos extras
                decimal diasPago2 = 0;
                //Si Ingreso con el periodo iniciado
                if (Convert.ToDateTime(dr["fecha_ingreso"]) > Convert.ToDateTime(dr["fechaini"]))
                {
                    TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fecha_ingreso"]));
                    diasPago2 = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                }
                else
                {
                    TimeSpan diasPagar = (Convert.ToDateTime(dr["fechafin2"]) - Convert.ToDateTime(dr["fechaini"]));
                    diasPago2 = Convert.ToDecimal(diasPagar.TotalDays) + 1;
                }
                decimal quincenaPago2 = (Convert.ToDecimal(dr["salariomensual"]) / 30) * diasPago2;
                decimal inssQuincena2 = (quincenaPago2 * factorInss) / 100;
                decimal netoQuincena2 = quincenaPago2 - inssQuincena2;
                decimal inssPatronal2 = quincenaPago2 * factorPatronal;
                decimal inatec2 = quincenaPago2 * factorInatec;
               

                Dato_Planilla.insertarCalculosPlanillaQuincenal(0, Convert.ToInt32(dr["codigo_empleado"]), Convert.ToDateTime(dr["fechaini"]), Convert.ToDateTime(dr["fechafin2"]),
                        Convert.ToInt32(dr["mesplanilla"]), dr["nombrecompleto"].ToString(), Convert.ToDecimal(dr["salariomensual"]), 120, quincenaPago2, inssQuincena2,
                        netoQuincena2, 0, periodo, 0, inssPatronal2, inatec2, dr["codigo_depto"].ToString(), tPlanilla, user, tipoPlanilla, userDetail.getIDEmpresa());

            }

            //CALCULO DE IR
            dir = Dato_Planilla.ObtenerPlanillaQuincenal(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            dret = Dato_Factores.SeleccionarRetencionesIR(userDetail.getIDEmpresa());

            decimal irDesdT1 = Convert.ToDecimal(dret.Rows[0]["rentadesde"].ToString());
            decimal irHastT1 = Convert.ToDecimal(dret.Rows[0]["rentahasta"].ToString());
            decimal irSobrExcesT1 = Convert.ToDecimal(dret.Rows[0]["SobreExceso"].ToString());
            decimal irDesdT2 = Convert.ToDecimal(dret.Rows[1]["rentadesde"].ToString());
            decimal irHastT2 = Convert.ToDecimal(dret.Rows[1]["rentahasta"].ToString());
            decimal ImpBaseT2 = Convert.ToDecimal(dret.Rows[1]["ImpuestoBase"].ToString());
            decimal irSobrExcesT2 = Convert.ToDecimal(dret.Rows[1]["SobreExceso"].ToString());
            decimal irDesdT3 = Convert.ToDecimal(dret.Rows[2]["rentadesde"].ToString());
            decimal irHastT3 = Convert.ToDecimal(dret.Rows[2]["rentahasta"].ToString());
            decimal ImpBaseT3 = Convert.ToDecimal(dret.Rows[2]["ImpuestoBase"].ToString());
            decimal irSobrExcesT3 = Convert.ToDecimal(dret.Rows[2]["SobreExceso"].ToString());
            decimal irDesdT4 = Convert.ToDecimal(dret.Rows[3]["rentadesde"].ToString());
            decimal irHastT4 = Convert.ToDecimal(dret.Rows[3]["rentahasta"].ToString());
            decimal ImpBaseT4 = Convert.ToDecimal(dret.Rows[3]["ImpuestoBase"].ToString());
            decimal irSobrExcesT4 = Convert.ToDecimal(dret.Rows[3]["SobreExceso"].ToString());

            foreach (DataRow dr3 in dir.Rows)
            {
                int codigo = Convert.ToInt32(dr3["codigo_empleado"]);
                decimal ingresosDeducInss = Convert.ToDecimal(dr3["ingresos"].ToString()) - Convert.ToDecimal(dr3["dsegurosocial"].ToString());
                decimal iR = 0;
                iR = ingresosDeducInss * 24;
                if (iR < irDesdT1)
                    iR = 0;
                else
                {
                    //primerRango
                    if (iR >= irDesdT1 && iR <= irHastT1)
                        iR = ((((iR - irSobrExcesT1) * 0.15M) / 24));
                    //segundoRango
                    if (iR >= irDesdT2 && iR <= irHastT2)
                        iR = (((((iR - irSobrExcesT2) * 0.20M) + ImpBaseT2) / 24));
                    //tercerRango
                    if (iR >= irDesdT3 && iR <= irHastT3)
                        iR = (((((iR - irSobrExcesT3) * 0.25M) + ImpBaseT3) / 24));
                    //cuartoRango
                    if (iR >= irDesdT4 && iR <= irHastT4)
                        iR = (((((iR - irSobrExcesT4) * 0.30M) + ImpBaseT4) / 24));

                    Dato_Planilla.insertarCalculoIRPlanillaQuincenal(Convert.ToInt32(dr3["codigo_empleado"]),
                        iR, periodo, userDetail.getIDEmpresa());
                }
            }

            //DEDUCCIONES
            dd = Dato_Planilla.ObtenerDeduccionesQuincenales(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            dt = Dato_Planilla.ObtenerPlanillaQuincenal(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            int bandera = 0;
            foreach (DataRow dr4 in dt.Rows)
            {
                decimal deduccion = 0;
                foreach (DataRow dr2 in dd.Rows)
                {
                    //Si la deduccion no es porcentual al ingreso pension alimenticia
                    if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 1 &&
                        Convert.ToInt32(dr2["id_Tipo"].ToString().ToUpper().Trim()) == 3 && dr2["porcentual"].ToString().Trim() == "False")
                    {
                        bandera = 1;
                        deduccion = Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim());
                    }

                     //Si la deduccion es porcentual al ingreso pension alimenticia
                    else if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 1 &&
                    Convert.ToInt32(dr2["id_Tipo"].ToString().ToUpper().Trim()) == 3 && dr2["porcentual"].ToString().Trim() == "True")
                    {
                        bandera = 1;
                        deduccion = ((Convert.ToDecimal(dr4["ingresos"].ToString().ToUpper().Trim()) * Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim())) / 100);
                    }

                    //Si la deduccion no es porcentual al ingreso, embargo por banco
                    else if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 2
                        && dr2["porcentual"].ToString().Trim() == "False")
                    {
                        bandera = 1;
                        deduccion = Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim());
                    }

                    //Si la deduccion es porcentual al ingreso, embargo por banco
                    else if (dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim() && Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) == 2 &&
                    dr2["porcentual"].ToString().Trim() == "True")
                    {
                        bandera = 1;
                        deduccion = ((Convert.ToDecimal(dr4["ingresos"].ToString().ToUpper().Trim()) * Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim())) / 100);
                    }

                    //Demas deducciones
                    else if (Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()) > 2 && dr2["codigo_empleado"].ToString().ToUpper().Trim() == dr4["codigo_empleado"].ToString().ToUpper().Trim()
                        && Convert.ToInt32(dr2["periodo"].ToString().ToUpper().Trim()) == Convert.ToInt32(dr4["periodo"].ToString().ToUpper().Trim()))
                    {
                        bandera = 1;
                        deduccion = Convert.ToDecimal(dr2["valor"].ToString().ToUpper().Trim());

                        //salarioNeto -= deducir;
                    }

                    else bandera = 0;
                    if (bandera == 1)
                    {
                        bandera = 0;
                        Dato_Planilla.aplicarDeduccionesQuincenales(Convert.ToInt32(dr4["periodo"].ToString().ToUpper().Trim()),
                        Convert.ToInt32(dr4["codigo_empleado"].ToString().ToUpper().Trim()), deduccion, Convert.ToInt32(dr2["tipoIngrDeduc"].ToString().ToUpper().Trim()), tipoPlanilla, userDetail.getIDEmpresa());
                    }
                }
            }

            //Desglose de monedas
            dt = Dato_Planilla.ObtenerPlanillaQuincenal(periodo, tipoPlanilla, userDetail.getIDEmpresa());
            foreach (DataRow dr4 in dt.Rows)
            {
                decimal restante1 = 0;
                decimal restante2 = 0;
                decimal restante3 = 0;
                decimal restante4 = 0;
                decimal restante5 = 0;
                decimal restante6 = 0;
                decimal restante7 = 0;
                decimal restante8 = 0;
                decimal restante9 = 0;
                decimal restante10 = 0;
                decimal restante11 = 0;
                decimal restante12 = 0;
                decimal restante13 = 0;
                int codEmpleado = Convert.ToInt32(dr4["codigo_empleado"].ToString().ToUpper().Trim());
                decimal salarioNeto = Math.Truncate(Convert.ToDecimal(dr4["neto"].ToString().ToUpper().Trim()));
                decimal decimales = Convert.ToDecimal(dr4["neto"].ToString().ToUpper().Trim()) - salarioNeto;

                int d500 = Convert.ToInt32(Math.Truncate(salarioNeto / 500));
                if (d500 >= 1)
                    restante1 = d500 * 500;
                else
                    restante1 = salarioNeto;

                int d200 = Convert.ToInt32(Math.Truncate((salarioNeto - restante1) / 200));
                if (d200 >= 1)
                {
                    restante2 = d200 * 200;
                    restante2 += restante1;
                }
                else
                    restante2 = restante1;

                int d100 = Convert.ToInt32(Math.Truncate((salarioNeto - restante2) / 100));
                if (d100 >= 1)
                {
                    restante3 = d100 * 100;
                    restante3 += restante2;
                }
                else
                    restante3 = restante2;

                int d50 = Convert.ToInt32(Math.Truncate((salarioNeto - restante3) / 50));
                if (d50 >= 1)
                {
                    restante4 = d50 * 50;
                    restante4 += restante3;
                }
                else
                    restante4 = restante3;
                int d20 = Convert.ToInt32(Math.Truncate((salarioNeto - restante4) / 20));
                if (d20 >= 1)
                {
                    restante5 = d20 * 20;
                    restante5 += restante4;
                }
                else
                    restante5 = restante4;
                int d10 = Convert.ToInt32(Math.Truncate((salarioNeto - restante5) / 10));
                if (d10 >= 1)
                {
                    restante6 = d10 * 10;
                    restante6 += restante5;
                }
                else
                    restante6 = restante5;
                int d5 = Convert.ToInt32(Math.Truncate((salarioNeto - restante6) / 5));
                if (d5 >= 1)
                {
                    restante7 = d5 * 5;
                    restante7 += restante6;
                }
                else
                    restante7 = restante6;
                int d1 = Convert.ToInt32(Math.Truncate((salarioNeto - restante7) / 1));
                if (d1 >= 1)
                {
                    restante8 = d1 * 1;
                    restante8 += restante7;
                }
                else
                    restante8 = restante7;

                int d05 = Convert.ToInt32(Math.Truncate(decimales / 0.5M));
                if (d05 <= 1)
                {
                    restante9 = d05 * 0.5M;
                }
                else
                    restante9 = decimales;

                int d025 = Convert.ToInt32(Math.Truncate((decimales - restante9) / 0.25M));
                if (d025 >= 1)
                {
                    restante10 = d025 * 0.25M;
                    restante10 += restante9;
                }
                else
                    restante10 = restante9;

                int d010 = Convert.ToInt32(Math.Truncate((decimales - restante10) / 0.10M));
                if (d010 >= 1)
                {
                    restante11 = d010 * 0.10M;
                    restante11 += restante10;
                }
                else
                {
                    restante11 = restante10;
                }

                int d005 = Convert.ToInt32(Math.Truncate((decimales - restante11) / 0.05M));
                if (d005 >= 1)
                {
                    restante12 = d005 * 0.05M;
                    restante12 += restante11;
                }
                else
                {
                    restante12 = restante11;
                }

                int d001 = Convert.ToInt32(Math.Truncate((decimales - restante12) / 0.01M));
                if (d001 >= 1)
                {
                    restante13 = d001 * 0.01M;
                }
                Dato_Planilla.distribuirDenominacionesMonedaQuincenal(d500, d200, d100, d50, d20, d10, d5, d1, d05, d025, d010, d005, d001, codEmpleado, periodo, tipoPlanilla, userDetail.getIDEmpresa());
            }
            int a = 0;
            return a;
        }
    }
}


