using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Data;

using Negocios;


namespace WebApi.Controllers
{
    [Route("api/EmpleadosHorasLaborales")]
    public class EmpleadosHorasLaboralesController : ApiController
    {
        #region referencias
        Negocios.Neg_Marca marcas = new Negocios.Neg_Marca();
        AmonestacionesController amone = new AmonestacionesController();
        #endregion

        [HttpGet]


        public DataTable GetReporData(int year1, int month1, int days1, int year2, int month2, int days2, int tipo, string nombre)
        {
            DataTable dt = new DataTable("tabla");

            dt.Columns.Add("CodigoEmpleado", typeof(String));
            dt.Columns.Add("Nombre", typeof(String));
            dt.Columns.Add("Departamento", typeof(String));
            dt.Columns.Add("vac", typeof(String));
            dt.Columns.Add("vacT", typeof(String));
            dt.Columns.Add("PCG", typeof(String));
            dt.Columns.Add("PCGT", typeof(String));
            dt.Columns.Add("PSG", typeof(String));
            dt.Columns.Add("PSGT", typeof(String));
            dt.Columns.Add("S", typeof(String));
            dt.Columns.Add("ST", typeof(String));
            dt.Columns.Add("AIT", typeof(String));
            dt.Columns.Add("DI", typeof(String));
            dt.Columns.Add("Amonestacion", typeof(String));


            Negocios.Neg_Marca marcas = new Negocios.Neg_Marca();
            DateTime fecha1;
            DateTime fecha2;


            fecha1 = new DateTime(year1, month1, days1);
            fecha2 = new DateTime(year2, month2, days2);

            List<Negocios.Neg_Empleados> LTE = marcas.ObtenerHT(fecha1, fecha2, 1, 4,tipo);
            List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> La = amone.GetAmonestaciones(year1, month1, days1, year2, month2, days2,tipo);
            
            foreach (var item in LTE)
            {
                if (item.estado == 1 && item.codigo_depto != 5 && item.codigo_depto != 4)
                {
                    if (item.flexitime == false)
                    {
                        DataTable diasLaborados = item.dtHorasT;
                        int vacaciones = 0, horascg = 0, horassgoce = 0, horass = 0, amonestaciones = 0;
                        double vacacionesmin = 0, horascgmin = 0, horassgocemin = 0, horassmin = 0, hI = 0, DI = 0;
                        List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> Amonestacion;
                        if (diasLaborados != null)
                        {
                            if (diasLaborados.Rows.Count > 0)
                            {
                                foreach (DataRow dias in diasLaborados.Rows)
                                {
                                    Amonestacion = (from i in La where i.Codigo.Trim().Equals(item.codigo_empleado.ToString().Trim()) && (i.Codigo == "37A" || i.Codigo == "38A") select i).ToList();
                                    if (Amonestacion.Count > 0)
                                    {

                                        amonestaciones = Amonestacion.Count;
                                    }

                                    else
                                    {
                                        amonestaciones = 0;
                                    }


                                    vacacionesmin += Math.Round(Convert.ToDouble(dias[6]), 2);
                                    horascgmin += Math.Round(Convert.ToDouble(dias[7]), 2);
                                    horassgocemin += Math.Round(Convert.ToDouble(dias[8]), 2);
                                    horassmin += Math.Round(Convert.ToDouble(dias[9]), 2);

                                    if (Convert.ToInt32(dias[6]) > 0)
                                    {
                                        vacaciones++;
                                    }

                                    if (Convert.ToInt32(dias[7]) > 0)
                                    {
                                        horascg++;
                                    }

                                    if (Convert.ToInt32(dias[8]) > 0)
                                    {
                                        horassgoce++;
                                    }

                                    if (Convert.ToInt32(dias[9]) > 0)
                                    {
                                        horass++;
                                    }

                                    hI = Math.Round(item.horasturno - (item.horascg + item.horassg + item.horasv + item.horast + item.horass), 2);
                                    if (hI >= item.horasturno)
                                    {
                                        DI = 1;
                                    }
                                }

                            }
                        }

                        if ((vacaciones + horascg + horassgoce + horass + hI) > 0)
                        {
                            dt.Rows.Add(item.codigo_empleado.ToString(), item.nombrecompleto, item.departamento, vacaciones.ToString(), vacacionesmin.ToString(), horascg.ToString(), horascgmin.ToString(), horassgoce.ToString(), horassgocemin.ToString(), horass.ToString(), horassmin.ToString(), hI.ToString(), DI.ToString(), amonestaciones);
                        }

                    }
                }
            }



            return dt;
        }

        public string GetReporteHTML(int year1, int month1, int days1, int year2, int month2, int days2)
        {
            DataTable dt = new DataTable("tabla");

            dt.Columns.Add("semana", typeof(int));
            dt.Columns.Add("fecha", typeof(DateTime));
            dt.Columns.Add("CodigoEmpleado", typeof(String));
            dt.Columns.Add("Nombre", typeof(String));
            dt.Columns.Add("Departamento", typeof(String));
            dt.Columns.Add("vac", typeof(String));
            dt.Columns.Add("vacT", typeof(String));
            dt.Columns.Add("PCG", typeof(String));
            dt.Columns.Add("PCGT", typeof(String));
            dt.Columns.Add("PSG", typeof(String));
            dt.Columns.Add("PSGT", typeof(String));
            dt.Columns.Add("S", typeof(String));
            dt.Columns.Add("ST", typeof(String));
            dt.Columns.Add("AIT", typeof(String));
            dt.Columns.Add("DI", typeof(String));
            dt.Columns.Add("Amonestacion", typeof(String));


            Negocios.Neg_Marca marcas = new Negocios.Neg_Marca();
            DateTime fecha1;
            DateTime fecha2;


            fecha1 = new DateTime(year1, month1, days1);
            fecha2 = new DateTime(year2, month2, days2);

            List<Negocios.Neg_Empleados> LTE = marcas.ObtenerHT(fecha1, fecha2, 1, 4, 1);
            List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> La = amone.GetAmonestaciones(year1, month1, days1, year2, month2, days2, 1);
            int personas = 0;
            foreach (var item in LTE)
            {

                if (item.estado == 1 && item.codigo_depto != 5 && item.codigo_depto != 4)
                {
                    if (item.flexitime == false)
                    {
                        DataTable diasLaborados = item.dtHorasT;

                        List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> Amonestacion;
                        if (diasLaborados != null)
                        {
                            if (diasLaborados.Rows.Count > 0)
                            {

                                int contador = 0;
                                foreach (DataRow dias in diasLaborados.Rows)
                                {
                                    contador++;
                                    if ((Convert.ToDateTime(dias[0].ToString()).Date <= fecha2.Date))
                                    {
                                        int vacaciones = 0, horascg = 0, horassgoce = 0, horass = 0, amonestaciones = 0;
                                        double vacacionesmin = 0, horascgmin = 0, horassgocemin = 0, horassmin = 0, hI = 0, DI = 0, HTrabajadas = 0, HTurno = 0;
                                        Amonestacion = (from i in La where i.Codigo.Trim().Equals(item.codigo_empleado.ToString().Trim()) && (i.Codigo == "37A" || i.Codigo == "38A") select i).ToList();
                                        if (Amonestacion.Count > 0)
                                        {
                                            foreach (var Amon in Amonestacion)
                                            {
                                                if (Convert.ToDateTime(Amon.FechaA).Date == Convert.ToDateTime(dias[0].ToString()).Date)
                                                {
                                                    amonestaciones = 1;
                                                }
                                            }
                                        }


                                        vacacionesmin += Math.Round(Convert.ToDouble(dias[6]), 2);
                                        horascgmin += Math.Round(Convert.ToDouble(dias[7]), 2);
                                        horassgocemin += Math.Round(Convert.ToDouble(dias[8]), 2);
                                        horassmin += Math.Round(Convert.ToDouble(dias[9]), 2);

                                        if (Convert.ToInt32(dias[6]) > 0)
                                        {
                                            vacaciones++;
                                        }

                                        if (Convert.ToInt32(dias[7]) > 0)
                                        {
                                            horascg++;
                                        }

                                        if (Convert.ToInt32(dias[8]) > 0)
                                        {
                                            horassgoce++;
                                        }

                                        if (Convert.ToInt32(dias[9]) > 0)
                                        {
                                            horass++;
                                        }

                                        HTrabajadas = Math.Round(Convert.ToDouble(dias[5]), 2);
                                        HTurno = Math.Round(Convert.ToDouble(dias[13]), 2);
                                        hI = Math.Round(HTurno - (horascgmin + horassgocemin + vacacionesmin + HTrabajadas + horassmin), 2);
                                        if (hI < 0)
                                            hI = 0;

                                        if (hI >= HTurno)
                                        {
                                            DI = 1;
                                        }

                                        if ((vacaciones + horascg + horassgoce + horass + hI) > 0)
                                        {
                                            if (contador == 1)
                                            {
                                                personas++;
                                            }
                                            dt.Rows.Add(Convert.ToInt32(dias[14].ToString()), Convert.ToDateTime(dias[0].ToString()).Date, item.codigo_empleado.ToString(), item.nombrecompleto, item.departamento, vacaciones.ToString(), vacacionesmin.ToString(), horascg.ToString(), horascgmin.ToString(), horassgoce.ToString(), horassgocemin.ToString(), horass.ToString(), horassmin.ToString(), hI.ToString(), DI.ToString(), amonestaciones);
                                        }
                                    }
                                }

                            }
                        }



                    }
                }
            }




            //DateTime fecha1;
            //DateTime fecha2;

            //fecha1 = new DateTime(year1, month1, days1);
            //fecha2 = new DateTime(year2, month2, days2);

            //DataTable dt = new DataTable();
            string html = "";
            //dt = GetReporData(year1, month1, days1, year2, month2, days2, tipo);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    // if (item.estado == 1 && item.codigo_depto!=5 && item.codigo_depto != 4)
                    List<Negocios.Neg_Empleados> listEV = (from i in LTE where i.estado == 1 && i.codigo_depto != 4 && i.codigo_depto != 5 select i).ToList();
                    int empleados = listEV.Count;
                    int ausentes = 0;
                    DataTable dtausentes = dt;
                    DataView dtvausentes = dtausentes.DefaultView;
                    dtvausentes.RowFilter = "DI>=1 or vacT>=9.6 or PCGT>=9.6 or PSGT>=9.6 or ST>=9.6 or AIT>=9.6 ";
                    html = "<H1 style='text-align:center; '> REPORTE AUSENTISMO DEL DIA " + fecha1.ToShortDateString() + " AL DIA " + fecha2.ToShortDateString() + "</H1>"
                         + "<br/>"
                        + "<H3 style='text-align:center; '> PORCENTAJE DE AUSENTISMO " + ((personas * 100) / empleados).ToString() + "% </H1>"
                        + "<br/>"
                        + "<table  width='90 % '  border='1' cellspacing='0' cellpadding='0' style='size:8px' align='center'>"
                            + "<thead>"
                        + "<tr style='color: white; background: rgb(66,136,216);'>"
                         + "<th>"
                        + " <span>Semana<span>"
                        + "</th>"
                         + "<th>"
                        + " <span>Fecha<span>"
                        + "</th>"
                        + "<th>"
                        + " <span>Codigo<span>"
                        + "</th>"
                         + "<th>"
                        + " <span>Nombre<span>"
                        + "</th>"
                        + "<th>"
                        + " <span>Departamento<span>"
                        + "</th>"
                         + "<th>"
                        + " <span>Incidencias Vac.<span>"
                        + "</th>"
                         + "<th>"
                        + " <span>Tiempo Vac.<span>"
                        + "</th>"
                        + "<th>"
                       + " <span>Incidencias Permiso C/Goce<span>"
                       + "</th>"
                        + "<th>"
                       + " <span>Tiempo Permiso C/Goce<span>"
                       + "</th>"
                        + "<th>"
                        + " <span>Incidencias Permiso S/Goce<span>"
                        + "</th>"
                        + "<th>"
                        + " <span>Tiempo Permiso S/Goce<span>"
                        + "</th>"
                         + "<th>"
                        + " <span>Incidencias Sub.<span>"
                        + "</th>"
                        + "<th>"
                        + " <span>Tiempo Sub.<span>"
                        + "</th>"
                        + "<th>"
                        + " <span>Tiempo Inj.<span>"
                        + "</th>"
                         + "<th>"
                        + " <span>Dias Inj<span>"
                        + "</th>"
                        + "<th>"
                        + " <span>Amon.<span>"
                        + "</th>"
                        + "</thead>"
                        + "<tbody>";



                    foreach (DataRow fila in dt.Rows)
                    {
                        html += "<tr>"
                                + "<td align='center'>" + fila[0].ToString() + "</td>"
                                + "<td align='center'>" + fila[1].ToString() + "</td>"
                                + "<td align='center'>" + fila[2].ToString() + "</td>"
                                + "<td align='center'>" + fila[3].ToString() + "</td>"
                                + "<td align='center'>" + fila[4].ToString() + "</td>"
                                + "<td align='center'>" + fila[5].ToString() + "</td>"
                                + "<td align='center'>" + fila[6].ToString() + "</td>"
                                + "<td align='center'>" + fila[7].ToString() + "</td>"
                                + "<td align='center'>" + fila[8].ToString() + "</td>"
                                + "<td align='center'>" + fila[9].ToString() + "</td>"
                                + "<td align='center'>" + fila[10].ToString() + "</td>"
                                + "<td align='center'>" + fila[11].ToString() + "</td>"
                                + "<td align='center'>" + fila[12].ToString() + "</td>"
                                + "<td align='center'>" + fila[13].ToString() + "</td>"
                                + "<td align='center'>" + fila[14].ToString() + "</td>"
                                + "<td align='center'>" + fila[15].ToString() + "</td>"
                                + "</tr>";
                    }

                    html += "</tbody>"
                                + "</table>";
                }
            }

            return html;
        }

        public DataTable GetReporDataPorFecha(int year1, int month1, int days1, int year2, int month2, int days2, int tipo)
        {
            DataTable dt = new DataTable("tabla");

            dt.Columns.Add("semana", typeof(string));
            dt.Columns.Add("fecha", typeof(DateTime));
            dt.Columns.Add("CodigoEmpleado", typeof(String));
            dt.Columns.Add("Nombre", typeof(String));
            dt.Columns.Add("Departamento", typeof(String));
            dt.Columns.Add("vac", typeof(String));
            dt.Columns.Add("vacT", typeof(String));
            dt.Columns.Add("PCG", typeof(String));
            dt.Columns.Add("PCGT", typeof(String));
            dt.Columns.Add("PSG", typeof(String));
            dt.Columns.Add("PSGT", typeof(String));
            dt.Columns.Add("S", typeof(String));
            dt.Columns.Add("ST", typeof(String));
            dt.Columns.Add("AIT", typeof(String));
            dt.Columns.Add("DI", typeof(String));
            dt.Columns.Add("Amonestacion", typeof(String));


            Negocios.Neg_Marca marcas = new Negocios.Neg_Marca();
            DateTime fecha1;
            DateTime fecha2;


            fecha1 = new DateTime(year1, month1, days1);
            fecha2 = new DateTime(year2, month2, days2);

            List<Negocios.Neg_Empleados> LTE = marcas.ObtenerHT(fecha1, fecha2, 1, 4, tipo);
            List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> La = amone.GetAmonestaciones(year1, month1, days1, year2, month2, days2, tipo);
            int personas = 0;
            foreach (var item in LTE)
            {

                if (item.estado == 1 && item.codigo_depto != 5 && item.codigo_depto != 4)
                {
                    if (item.flexitime == false)
                    {
                        DataTable diasLaborados = item.dtHorasT;

                        List<Negocios.Neg_Amonestaciones.DetalleAmonestacion> Amonestacion;
                        if (diasLaborados != null)
                        {
                            if (diasLaborados.Rows.Count > 0)
                            {

                                int contador = 0;
                                foreach (DataRow dias in diasLaborados.Rows)
                                {
                                    contador++;
                                    if ((Convert.ToDateTime(dias[0].ToString()).Date <= fecha2.Date))
                                    {
                                        int vacaciones = 0, horascg = 0, horassgoce = 0, horass = 0, amonestaciones = 0;
                                        double vacacionesmin = 0, horascgmin = 0, horassgocemin = 0, horassmin = 0, hI = 0, DI = 0, HTrabajadas = 0, HTurno = 0;
                                        Amonestacion = (from i in La where i.Codigo.Trim().Equals(item.codigo_empleado.ToString().Trim()) && (i.Codigo == "37A" || i.Codigo == "38A") select i).ToList();
                                        if (Amonestacion.Count > 0)
                                        {
                                            foreach (var Amon in Amonestacion)
                                            {
                                                if (Convert.ToDateTime(Amon.FechaA).Date == Convert.ToDateTime(dias[0].ToString()).Date)
                                                {
                                                    amonestaciones = 1;
                                                }
                                            }
                                        }


                                        vacacionesmin += Math.Round(Convert.ToDouble(dias[6]), 2);
                                        horascgmin += Math.Round(Convert.ToDouble(dias[7]), 2);
                                        horassgocemin += Math.Round(Convert.ToDouble(dias[8]), 2);
                                        horassmin += Math.Round(Convert.ToDouble(dias[9]), 2);

                                        if (Convert.ToInt32(dias[6]) > 0)
                                        {
                                            vacaciones++;
                                        }

                                        if (Convert.ToInt32(dias[7]) > 0)
                                        {
                                            horascg++;
                                        }

                                        if (Convert.ToInt32(dias[8]) > 0)
                                        {
                                            horassgoce++;
                                        }

                                        if (Convert.ToInt32(dias[9]) > 0)
                                        {
                                            horass++;
                                        }

                                        HTrabajadas = Math.Round(Convert.ToDouble(dias[5]), 2);
                                        HTurno = Math.Round(Convert.ToDouble(dias[13]), 2);
                                        hI = Math.Round(HTurno - (horascgmin + horassgocemin + vacacionesmin + HTrabajadas + horassmin), 2);
                                        if (hI < 0)
                                            hI = 0;

                                        if (hI >= HTurno)
                                        {
                                            DI = 1;
                                        }

                                        if ((vacaciones + horascg + horassgoce + horass + hI) > 0)
                                        {
                                            if (contador == 1)
                                            {
                                                personas++;
                                            }
                                            dt.Rows.Add(dias[14].ToString(), Convert.ToDateTime(dias[0].ToString()).Date, item.codigo_empleado.ToString(), item.nombrecompleto, item.departamento, vacaciones.ToString(), vacacionesmin.ToString(), horascg.ToString(), horascgmin.ToString(), horassgoce.ToString(), horassgocemin.ToString(), horass.ToString(), horassmin.ToString(), hI.ToString(), DI.ToString(), amonestaciones);
                                        }
                                    }
                                }

                            }
                        }

                    }
                }
            }

            return dt;
        }

        public string GetReporteMarcas(int ano1, int mes1, int dia1, int ano2, int mes2, int dia2, int tipo, int ubicacion)
        {
            Neg_Marca x = new Neg_Marca();
            DateTime fechainicio = new DateTime(ano1, mes1, dia1);
            DateTime fechafin = new DateTime(ano2, mes2, dia2);
            x.ObtenerReportedeMarcas(fechainicio, fechafin, tipo, ubicacion);

            return "ok";
        }

    }
}
