using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Negocios;

using iTextSharp.text.pdf.draw;


namespace NominaRRHH.Presentacion
{
    public partial class VCarta : System.Web.UI.Page
    {
        #region REFERENCIAS
        //MODIFICADO POR WENDY MEMBREÑO
        //SE CAMBIARON LOS METODOS STATIC
        // 31 AGOSTO 2016
        Neg_Informes Neg_Informes = new Neg_Informes();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Cargar();
            }
        }

        //        #region Otro Html
        //        private void Cargar()
        //        {
        //            string documento = "";
        //            documento = Convert.ToString(System.Guid.NewGuid());
        //            //documento=Convert.ToString(System.Guid.NewGuid());
        //            string pdfpath = HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\C"+documento+".pdf";
        //            //Server.MapPath("PDFs");
        //           //string imagepath = Server.MapPath("Columns");
        //            Document doc = new Document();
        //            string text = "";
        //            try
        //            {
        //                PdfWriter.GetInstance(doc, new FileStream(pdfpath, FileMode.Create));
        //                doc.Open();
        //                Paragraph heading = new Paragraph("Page Heading", new Font(Font.NORMAL, 28f, Font.BOLD));
        //                heading.SpacingAfter = 18f;
        //                doc.Add(heading);
        //                text = @"CONTRATO INDIVIDUAL DE TRABAJO POR TIEMPO INDETERMINADO La empresa denominada KAIZEN S.A, representada para los fines y efectos del presente Contrato de Trabajo por el Sr. Diego Andrés Jacir García Prieto,
        //mayor de edad, casado, Empresario, natural de la Republica de El Salvador y con domicilio y residencia en esta ciudad de Managua, identificado con cédula de residencia nicaragüense 
        //numero: 030720140010, actuando en nombre y representación de KAIZEN S.A del domicilio de Managua, quien en adelante y para los efectos del presente contrato se denominara
        //EL EMPLEADOR, y el Sr(a)__________________________, mayor de edad, estado civil_____________________________y con domicilio en ____________________________ , portador de 
        //la cédula de identidad número:_______________, quien en lo sucesivo se denominará EL TRABAJADOR, ambas partes en pleno uso y goce de nuestros derechos y facultades; bajo
        //";

        //                MultiColumnText columns = new MultiColumnText();

        //                //float left, float right, float gutterwidth, int numcolumns
        //                columns.AddRegularColumns(36f, doc.PageSize.Width - 36f, 24f, 2);
        //                Paragraph para = new Paragraph(text, new Font(Font.NORMAL, 8f));
        //                para.SpacingAfter = 9f;
        //                para.Alignment = Element.ALIGN_JUSTIFIED;
        //                for (int i = 0; i < 8; i++)
        //                {
        //                    columns.AddElement(para);
        //                }

        //                doc.Add(columns);

        //            }
        //            catch (Exception ex)
        //            {
        //                //Log(ex.Message);
        //            }
        //            finally
        //            {
        //                doc.Close();
        //            }

        //            CrearPDF(text);
        //        }
        //        #endregion

        #region Pdf
        private void CrearPDF(string html)
        {
            // string fileName = HttpContext.Current.Server.MapPath(".").ToString() + @"C:\Users\rmedrano\Documents\RandallMedrano\Sistema\NominaRRHH\NominaRRHH\Doc"".pdf";
            //string fileName = @"C:\Users\rmedrano\Documents\RandallMedrano\Sistema\NominaRRHH\NominaRRHH\Contrato.pdf";
            //string fileName = @"C:\inetpub\wwwroot\aspnet_client\TRASH\OrdenTrabajo_" + corte + "&nbsp" + seccion + ".pdf";
            string documento = "";
            documento = Convert.ToString(System.Guid.NewGuid());
            string fileName = HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\C" + documento + ".pdf";
            //probar establecer los margenes en 0
            //Document doc = new Document(PageSize.LETTER, 30, 0, 30, 20);

            Document doc = new Document(PageSize.LETTER, 5, 5, 0, 0);//Carta

            //Document doc = new Document(PageSize.LETTER, 30, 0, 0, 20);

            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite));
            doc.Open();

            iTextSharp.text.html.simpleparser.StyleSheet styles = new iTextSharp.text.html.simpleparser.StyleSheet();
            iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(doc);
            hw.Parse(new StringReader(html));
            doc.Close();
            writer.Close();
            ShowPdf(HttpContext.Current.Server.MapPath(".").ToString() + @"\..\Trash\C" + documento + ".pdf");
            //ShowPdf(@"C:\Users\rmedrano\Documents\RandallMedrano\Sistema\NominaRRHH\NominaRRHH\Contrato.pdf");  //OrdenTrabajo.pdf");//                 
            //ShowPdf(@"C:\inetpub\wwwroot\aspnet_client\TRASH\OrdenTrabajo_" + corte + "&nbsp" + seccion + ".pdf");  //OrdenTrabajo.pdf");
        }

        private void ShowPdf(string s)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "inline;filename=" + s);
            Response.ContentType = "application/pdf";
            Response.WriteFile(s);
            Response.Flush();
            Response.Clear();
        }
        #endregion
        #region Html Contrato
        private void CrearPlantilla(string Codigo, string Nombre, string Domicilio, string Cedula, string Estado, string Cargo, string Salario, string Año, string Mes, string Dia)
        {
            //            String html = string.Format("<p align='center' style='text-align: justify;'><strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;</strong><img align='center' src='http://s7.postimg.org/8hk9o10t7/kaizen2.png' alt='kaizen2' style='font-size: 10pt;'>&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<strong style='font-size: 10pt;'>CONSTANCIA &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</strong></p>" +

            //"<p align='center' style='text-align: justify;'>&nbsp;</p>" +

            //"<p style='text-align: justify;'>La suscrita Gerente de Desarrollo del Talento Humano de la Empresa <strong></strong>hace constar que el Sr (a)<strong></strong>quien se identifica con la cédula número<strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;001-260391-0004N </strong>es trabajador de esta empresa a partir&nbsp; del &nbsp;3de febrero del 2014&nbsp; hasta la fecha, desempeñando el cargo de Jefe de bodega de exportaciones cumpliendo responsablemente con las tareas asignadas .</p>" +

            //"<p style='text-align: justify;'>&nbsp;</p>" +

            //"<p style='text-align: justify;'>Se extiende la presente solicitud a la parte interesada y para los fines que estime conveniente, a los 25 días&nbsp; del mes de septiembre del año 2015.</p>"
            //      );
            string letra = "style='font-size: 7;'";
            String html = string.Format(

 "<p style='float: left; text-align: justify; width: 20%; font-size: 8pt;'><b>CONTRATO INDIVIDUAL DE TRABAJO POR TIEMPO INDETERMINADO</b><br />" +
"La empresa denominada KAIZEN S.A, representada para los fines y efectos del presente Contrato de Trabajo por el Sr. Edvin Giovany Samayoa Juarez, " +
"mayor de edad, casado, Empresario, natural de la Rep&uacute;blica de Guatemala y con domicilio y residencia en esta ciudad de Managua, identificado con c&eacute;dula " +
"de residencia nicarag&uuml;ense numero: 060620100018, actuando en nombre y representaci&oacute;n de KAIZEN S.A del domicilio de Diriamba, quien en adelante y para " +
"los efectos del presente contrato se denominar&aacute; <span " + letra + " > EL EMPLEADOR</span>, y el Sr(a) <span " + letra + " > " + Nombre.Trim() + " </span>, mayor de edad, estado civil <span " + letra + " > " + Estado + " </span> y con " +
"domicilio en <span " + letra + " > " + Domicilio.Trim() + " </span>, portador de la c&eacute;dula de identidad n&uacute;mero: <span " + letra + " > " + Cedula.Trim() + " </span>, quien en lo sucesivo se denominar&aacute; EL " +
"TRABAJADOR, ambas partes en pleno uso y goce de nuestros derechos y facultades; bajo el principio de la VOLUNTAD Y LIBRE CONTRATACION; hemos convenimos en celebrar " +
"el presente contrato individual de trabajo por tiempo indeterminado en virtud a lo establecido en nuestra Constituci&oacute;n Pol&iacute;tica y de conformidad con " +
"la legislaci&oacute;n laboral vigente ley 185 del C&oacute;digo del Trabajo en la Rep&uacute;blica de Nicaragua, el que se regir&aacute; por las siguientes cl&aacute;usulas: " +
"PRIMERA (RELACION DE TRABAJO): EL TRABAJADOR se obliga a prestar sus servicios para EL EMPLEADOR en el cargo de: <span " + letra + " > " + Cargo + " </span> , cuyas funciones se anexan y las que se entender&aacute;n " +
"incorporadas al presente contrato, el cual ha sido aceptado y firmado por ambas partes, debiendo tambi&eacute;n realizar las tareas afines con el cargo, de acuerdo a las " +
"instrucciones impartidas por el Empleador o el Representante en su caso. SEGUNDA (LUGAR DE LA PRESTACION DEL SERVICIO) EL TRABAJADOR cumplir&aacute; con sus obligaciones " +
"laborales, en las instalaciones f&iacute;sicas de las instalaciones de la Zona Franca KAIZEN S.A, con domicilio en el Km 45.5 Carretera Casares La Boquita, Diriamba, Carazo. " +
"Parque Industrial Jos&eacute; Ignacio Gonz&aacute;lez Morales. TERCERA (DURACION DEL CONTRATO). El presente contrato ser&aacute; por tiempo " +
"indeterminado, entrando en vigencia a partir del d&iacute;a <span " + letra + " > " + Dia + " </span> del mes de <span " + letra + " > " + Mes + " </span> del a&ntilde;o <span " + letra + " > " + Año + " </span> y aceptando que los primeros TREINTA DIAS, ser&aacute;n de prueba y que dentro de " +
"&eacute;ste periodo, cualquiera de las partes podr&aacute; ponerle fin a la relaci&oacute;n de trabajo, con justa causa o sin ella, sin ninguna responsabilidad para las " +
"mismas o resultado que se deriven de la evaluaci&oacute;n efectuada al TRABAJADOR con el objeto de constatar su desempe&ntilde;o en el cargo para el cual fue contratado," +
"sin m&aacute;s responsabilidad para la empresa que el pago del salario por los d&iacute;as laborados. CUARTA (HORARIO DE TRABAJO). La jornada ordinaria de trabajo " +
"ser&aacute; diurna de (48) cuarenta y ocho horas distribuidas as&iacute;: cinco (5) d&iacute;as de trabajo de nueve punto seis (9.6) horas discontinua siendo el horario de " +
"trabajo del siguiente: lun-jue 7:00 A.M. a 5:45 P.M. y vie 7:00 A.M. a 5:00 P.M., con un descanso de sesenta (60) minutos. &Uacute;nicamente podr&aacute;n ejecutarse trabajos extraordinarios cuando la empresa " +
"as&iacute; lo requiera, dada por escrito, por el empleador o jefe inmediato o cuando existan necesidades de casos de emergencias de producci&oacute;n y embarques por " +
"atrasos en recibir los materiales para operar, conforme el Arto. 59 del CT. Estando de acuerdo que la jornada extraordinaria ser&aacute; contrato especial entre las " +
"partes de conformidad a lo establecido en el Arto. 57 del CT<br />&nbsp; QUINTA (FORMA DE PAGO, PERIODO Y LUGAR DE PAGO) El trabajador recibir&aacute; por sus servicios una " +
"remuneraci&oacute;n mensual de C$ <span " + letra + " > " + Salario.Trim() + " </span> m&aacute;s viaticos, bonos u otra remuneraci&oacute;n adicional seg&uacute;n las pol&iacute;ticas internas establecidas por la empresa el cual ser&aacute; pagado en efectivo en las instalaciones f&iacute;sicas o transferencia bancaria electr&oacute;nica y en moneda " +
"de curso legal, los que ser&aacute;n pagados en cuotas semanales. Si el d&iacute;a de pago coincidiere con un d&iacute;a inh&aacute;bil el salario se " +
"pagar&aacute; el d&iacute;a anterior h&aacute;bil. SEXTA (DEBERES Y OBLIGACIONES DEL TRABAJADOR) Son obligaciones de EL TRABAJADOR las establecidas en este Contrato " +
"Individual de Trabajo, en el C&oacute;digo del Trabajo y espec&iacute;ficamente las contenidas en el Art. 18 de mismo as&iacute; como las que se llegaren a establecer " +
"Planes y Programas de trabajo; las funciones anexas y conexas a su cargo. A respectar y cumplir las Pol&iacute;ticas y Reglamentos Interno de Trabajo, Reglamento " +
"T&eacute;cnico Organizativo de Higiene y Seguridad Ocupacional del trabajo; as&iacute; como lo establecido en la Legislaci&oacute;n Laboral Vigente. Son " +
"obligaciones de EL EMPLEADOR las establecidas en este contrato Individual de Trabajo, en el C&oacute;digo de Trabajo y espec&iacute;ficamente las contenidas en el Arto." +
"17 del mismo. SEPTIMA (DE LAS MODIFICACIONES Y NORMAS APLICABLES) Se entender&aacute; parte integrante de este contrato, las modificaciones futuras que se consignen por " +
"escrito siempre que est&eacute;n autorizadas con la firma de ambas partes; adjunt&aacute;ndose la misma como un adendum. Tambi&eacute;n se entiende incluidos en este documento " +
"los derechos y obligaciones laborales establecidas en el C&oacute;digo del Trabajo. Asimismo, este contrato anula cualquier otro contrato individual de trabajo ya sea " +
"escrito o verbal, que haya estado vigente entre EL TRABAJADOR Y EL EMPLEADOR, pero no altera de manera alguna los derechos y prerrogativas que emanen de las leyes " +
"laborales. OCTAVA (CAUSAS DE TERMINACION DE CONTRATO). Son causas de terminaci&oacute;n de este contrato el incumplimiento de las obligaciones contenidas en el " +
"mismo, la violaci&oacute;n de las obligaciones establecidas en el C&oacute;digo del Trabajo, el incumplimiento de las obligaciones expresamente contempladas " +
"en el Reglamento Interno de Trabajo, cuando las mismas constituyan causal de despido, as&iacute; como las causales citadas en el Arto. 48 del referido C&oacute;digo. " +
"El presente contrato tambi&eacute;n podr&aacute; concluir por mutuo consentimiento y/o renuncia del trabajador con una anticipaci&oacute;n de quince (15) d&iacute;as. " +
"De igual forma EL EMPLEADOR puede darlo por concluido en cualquier tiempo, sin causa justificada, pagando a EL TRABAJADOR la indemnizaci&oacute;n por antig&uuml;edad " +
"establecida en el Arto. 45 del C&oacute;digo del Trabajo cuando esta corresponda. En fe de lo antes expuesto ratificamos y firmamos en el presente contrato individual " +
"de trabajo en dos tantos de un mismo tenor en la ciudad de Managua, a los d&iacute;as <span " + letra + " > " + Dia + " </span> del mes de <span " + letra + " > " + Mes + " </span> del a&ntilde;o <span " + letra + " > " + Año + " </span> . Para la firma del presente contrato " +
"individual de trabajo, por autorizaci&oacute;n expresa del Arto. 10 del C&oacute;digo de Trabajo y por delegaci&oacute;n del Gerente General, se autoriza al Gerente de " +
"Estrategia y Recursos Humanos para que en nombre de este, celebre y firma el presente contrato de trabajo.<br />" +
"EMPLEADOR  _________________________  TRABAJADOR  _________________________  </p>");

            //"<div style='float: right; text-align: justify; width: 20%; font-size: 8pt;'>QUINTA (FORMA DE PAGO, PERIODO Y LUGAR DE PAGO) El trabajador recibir&aacute; por sus servicios una " +
            //"remuneraci&oacute;n mensual de C$ "+Salario.Trim()+" el cual ser&aacute; pagado en efectivo en las instalaciones f&iacute;sicas o transferencia bancaria electr&oacute;nica y en moneda "+
            //"de curso legal, los que ser&aacute;n pagados en cuotas catorcenales. Si el d&iacute;a de pago coincidiere con un d&iacute;a inh&aacute;bil el salario se "+
            //"pagara el d&iacute;a anterior h&aacute;bil. SEXTA (DEBERES Y OBLIGACIONES DEL TRABAJADOR) Son obligaciones de EL TRABAJADOR las establecidas en este Contrato"+
            //"Individual de Trabajo, en el C&oacute;digo del Trabajo y espec&iacute;ficamente las contenidas en el Art. 18 de mismo as&iacute; como las que se llegaren a establecer "+
            //"Planes y Programas de trabajo; las funciones anexas y conexas a su cargo. A respectar y cumplir las Pol&iacute;ticas y Reglamentos Interno de Trabajo, Reglamento "+
            //"T&eacute;cnico Organizativo de Higiene y Seguridad Ocupacional del trabajo; as&iacute; como lo establecido en la Legislaci&oacute;n Laboral Vigente. Son"+
            //"obligaciones de EL EMPLEADOR las establecidas en este contrato Individual de Trabajo, en el C&oacute;digo de Trabajo y espec&iacute;ficamente las contenidas en el Arto."+
            //"17 del mismo. SEPTIMA (DE LAS MODIFICACIONES Y NORMAS APLICABLES) Se entender&aacute; parte integrante de este contrato, las modificaciones futuras que se consignen por"+
            //"escrito siempre que est&eacute;n autorizadas con la firma de ambas partes; adjunt&aacute;ndose la misma como un adendum. Tambi&eacute;n se entiende incluidos en este documento "+
            //"los derechos y obligaciones laborales establecidas en el C&oacute;digo del Trabajo. Asimismo, este contrato anula cualquier otro contrato individual de trabajo ya sea "+
            //"escrito o verbal, que haya estado vigente entre EL TRABAJADOR Y EL EMPLEADOR, pero no altera de manera alguna los derechos y prerrogativas que emanen de las leyes "+
            //"laborales. OCTAVA (CAUSAS DE TERMINACION DE CONTRATO). Son causas de terminaci&oacute;n de este contrato el incumplimiento de las obligaciones contenidas en el "+
            //"mismo, la violaci&oacute;n de las obligaciones establecidas en el C&oacute;digo del Trabajo, el incumplimiento de las obligaciones expresamente contempladas "+
            //"en el Reglamento Interno de Trabajo, cuando las mismas constituyan causal de despido, as&iacute; como las causales citadas en el Arto. 48 del referido C&oacute;digo."+
            //"El presente contrato tambi&eacute;n podr&aacute; concluir por mutuo consentimiento y/o renuncia del trabajador con una anticipaci&oacute;n de quince (15) d&iacute;as."+
            //"De igual forma EL EMPLEADOR puede darlo por concluido en cualquier tiempo, sin causa justificada, pagando a EL TRABAJADOR la indemnizaci&oacute;n por antig&uuml;edad"+
            //"establecida en el Arto. 45 del C&oacute;digo del Trabajo cuando esta corresponda. En fe de lo antes expuesto ratificamos y firmamos en el presente contrato individual"+
            //"de trabajo en dos tantos de un mismo tenor en la ciudad de Managua, a los d&iacute;as "+ Dia +" del mes de "+Mes+" del a&ntilde;o "+Año+" . Para la firma del presente contrato "+
            //"individual de trabajo, por autorizaci&oacute;n expresa del Arto. 10 del C&oacute;digo de Trabajo y por delegaci&oacute;n del Gerente General, se autoriza al Gerente de"+
            //"Desarrollo del Talento Humano para que en nombre de este, celebre y firma el presente contrato de trabajo.<br /><br />"+
            //"EMPLEADOR____________TRABAJADOR____________</div>"           );
            CrearPDF(html);
            #region
            //            String html = string.Format(
            //                "<p class='MsoNormal' align='center' style='text-align:center'><b><span style='font-size:12.0pt;mso-bidi-font-size:11.0pt'>CONTRATO " +
            //"INDIVIDUAL DE TRABAJO POR TIEMPO INDETERMINADO<o:p></o:p></span></b></p>" +

            //"<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;" +
            //"mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>La empresa denominada" +
            //"KAIZEN S.A, representada para los fines y efectos del presente <b>Contrato de Trabajo</b> por el Sr. Diego" +
            //"Andrés Jacir García Prieto, mayor de edad, casado, Empresario, natural de la" +
            //"Republica de El Salvador y con domicilio y residencia en esta ciudad de Managua," +
            //"identificado con cédula de residencia nicaragüense numero: 030720140010, actuando" +
            //"en nombre y representación de KAIZEN S.A del domicilio de Managua, quien en" +
            //"adelante y para los efectos del presente contrato se denominara EL EMPLEADOR, y" +
            //"el Sr(a)<b> "+Nombre.Trim()+"</b>," +
            //"mayor de edad, estado civil "+Estado.Trim()+"<b><u></u></b>y con domicilio en <u>"+Domicilio+"</u>," +
            //"portador de la cédula de identidad número:<b>"+Cedula.Trim()+"</b><u>,</u>" +
            //"quien en lo sucesivo se denominará EL TRABAJADOR, ambas partes en pleno uso y" +
            //"goce de nuestros derechos y facultades; bajo el principio de la VOLUNTAD Y" +
            //"LIBRE CONTRATACION; hemos convenimos en celebrar el presente contrato" +
            //"individual de trabajo por tiempo indeterminado en virtud a lo establecido en" +
            //"nuestra Constitución Política y de conformidad con la legislación laboral" +
            //"vigente ley 185 del Código del Trabajo en&nbsp;" +
            //"la Republica de Nicaragua, el que se regirá por las siguientes cláusulas:" +
            //"<u><o:p></o:p></u></span></p>" +

            //"<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;" +
            //"mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>PRIMERA</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>" +
            //"RELACION DE TRABAJO): EL TRABAJADOR se obliga a prestar sus servicios para EL" +
            //"EMPLEADOR en el cargo de:<b><u>"+Cargo+"</u></b><u>,</u>" +
            //"cuyas funciones se anexan y las que se entenderán incorporadas al presente" +
            //"contrato, el cual ha sido aceptado y firmado por ambas partes, debiendo también" +
            //"realizar las tareas afines con el cargo, de acuerdo a las instrucciones" +
            //"impartidas por el Empleador o el Representante en su caso.</span></p>" +

            //"<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;" +
            //"mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>SEGUNDA</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>" +
            //"(LUGAR DE LA PRESTACION DEL SERVICIO) EL TRABAJADOR cumplirá con sus" +
            //"obligaciones laborales, en las instalaciones físicas de las instalaciones de la" +
            //"Zona Franca KAIZEN S.A, con domicilio en el Km. 7.5 Carretera Panamericana, de" +
            //"donde fue la Kativo 500 metros al sur, complejo industrial “El Transito”," +
            //"Managua, Nicaragua.</span></p>" +

            //"<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;" +
            //"mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>TERCERA" +
            //"</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>(DURACION DEL CONTRATO). El presente contrato será por" +
            //"tiempo indeterminado, entrando en vigencia a partir del día <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>, del mes <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>del año <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>&nbsp;y aceptando que los primeros TREINTA DIAS," +
            //"serán de prueba y que dentro de éste periodo, cualquiera de las partes podrá" +
            //"ponerle fin a la relación de trabajo, con justa causa o sin ella, sin ninguna" +
            //"responsabilidad para las mismas o resultado que se deriven de la evaluación" +
            //"efectuada al TRABAJADOR con el objeto de constatar su desempeño en el cargo" +
            //"para el cual fue contratado, sin más responsabilidad para la empresa que el" +
            //"pago del salario por los días laborados.</span></p>" +
            //"<p class='MsoNormal' style='text-align:justify'><b><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span></b><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>CUARTA</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>" +
            //"(HORARIO DE TRABAJO). La jornada ordinaria de trabajo será diurna de (48) cuarenta" +
            //"y ocho horas distribuidas así: cinco (5) días de trabajo de nueve punto seis" +
            //"(9.6) horas discontinua siendo el horario de trabajo del siguiente: 7:00 A.M. a" +
            //"5:36 P.M., con un descanso de sesenta (60) minutos. Únicamente podrán" +
            //"ejecutarse trabajos extraordinarios cuando la empresa así lo requiera, dada por" +
            //"escrito, por el empleador o jefe inmediato o cuando existan necesidades de" +
            //"casos de emergencias de producción y embarques por atrasos en recibir los" +
            //"materiales para operar, conforme el Arto. 59 del CT. Estando de acuerdo que la" +
            //"jornada extraordinaria será contrato especial entre las partes de conformidad a" +
            //"lo establecido en el Arto. 57 del CT</span></p>" +
            //"<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;" +
            //"mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><span style='font-size: 9pt; font-family: Arial, sans-serif;'>&nbsp;</span><span style='font-size: 9pt; font-family: Arial, sans-serif;'>&nbsp;</span><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>QUINTA</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>" +
            //"(FORMA DE PAGO, PERIODO Y LUGAR DE PAGO)</span></p>" +
            //"<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;" +
            //"mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>El trabajador recibirá" +
            //"por sus servicios una remuneración mensual de C$<b> "+Salario.Trim()+" <u>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u>&nbsp;</b>el cual será pagado en efectivo en las" +
            //"instalaciones físicas o transferencia bancaria electrónica y en moneda de curso" +
            //"legal, los que serán pagados en cuotas catorcenales. Si el día de pago" +
            //"coincidiere con un día inhábil el salario se pagara el día anterior hábil.<o:p></o:p></span></p>" +
            //"<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;" +
            //"mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>SEXTA</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>" +
            //"(DEBERES Y OBLIGACIONES DEL TRABAJADOR) Son obligaciones de EL TRABAJADOR las" +
            //"establecidas en este Contrato Individual de Trabajo, en el Código del Trabajo y" +
            //"específicamente las contenidas en el Art. 18 de mismo así como las que se" +
            //"llegaren a establecer Planes y Programas de trabajo; las funciones anexas y conexas" +
            //"a su cargo. A respectar y cumplir las Políticas y Reglamentos Interno de" +
            //"Trabajo, Reglamento Técnico Organizativo de Higiene y Seguridad Ocupacional del" +
            //"trabajo; así como lo establecido en la Legislación Laboral Vigente.</span></p>"+

            //             "<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;"+
            //             "mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>Son obligaciones de EL"+
            //             "EMPLEADOR las establecidas en este contrato Individual de Trabajo, en el Código"+
            //             "de Trabajo y específicamente las contenidas en el Arto. 17 del mismo.</span></p>"+

            //             "<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;"+
            //             "mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>SEPTIMA"+
            //             "</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>(DE LAS MODIFICACIONES Y NORMAS APLICABLES) Se entenderá"+
            //             "parte integrante de este contrato, las modificaciones futuras que se consignen"+
            //             "por escrito siempre que estén autorizadas con la firma de ambas partes;"+
            //             "adjuntándose la misma como un adendum. También se entiende incluidos en este"+
            //             "documento los derechos y obligaciones laborales establecidas en el Código del"+
            //             "Trabajo. Asimismo, este contrato anula cualquier otro contrato individual de"+
            //             "trabajo ya sea escrito o verbal, que haya estado vigente entre EL TRABAJADOR Y"+
            //             "EL EMPLEADOR, pero no altera de manera alguna los derechos y prerrogativas que emanen"+
            //             "de las leyes laborales.</span></p>" +

            //            "<p class='MsoNormal' style='text-align:justify'><b><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span></b><b style='font-size: 10pt;'><span style='font-size:9.0pt;mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>OCTAVA</span></b><span style='font-size: 9pt; font-family: Arial, sans-serif;'>"+
            //            "(CAUSAS DE TERMINACION DE CONTRATO). Son causas de terminación de este contrato"+
            //            "el incumplimiento de las obligaciones contenidas en el mismo, la violación de"+
            //            "las obligaciones establecidas en el Código del Trabajo, el incumplimiento de"+
            //            "las obligaciones expresamente contempladas en el Reglamento Interno de Trabajo,"+
            //            "cuando las mismas constituyan causal de despido, así como las causales citadas"+
            //            "en el Arto. 48 del referido Código. El presente contrato también podrá concluir"+
            //            "por mutuo consentimiento y/o renuncia del trabajador con una anticipación de"+
            //            "quince (15) días. De igual forma EL EMPLEADOR puede darlo por concluido en"+
            //            "cualquier tiempo, sin causa justificada, pagando a EL TRABAJADOR la"+
            //            "indemnización por antigüedad establecida en el Arto. 45 del Código del Trabajo"+
            //            "cuando esta corresponda.</span></p>" +

            //            "<pclass='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;"+
            //                 "mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>En fe de lo antes"+
            //                 "expuesto ratificamos y firmamos en el presente contrato individual de trabajo"+
            //                 "en dos tantos de un mismo tenor en la ciudad de Managua, a los </span><u style='font-family: Arial, sans-serif; font-size: 9pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u><span style='font-family: Arial, sans-serif; font-size: 9pt;'>&nbsp;</span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>días del mes de ___________</span><u style='font-family: Arial, sans-serif; font-size: 9pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </u><span style='font-family: Arial, sans-serif; font-size: 9pt;'>&nbsp;</span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>del año </span><u style='font-family: Arial, sans-serif; font-size: 9pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</u><span style='font-family: Arial, sans-serif; font-size: 9pt;'>.</span></p>"+

            //            "<p class='MsoNormal' style='text-align:justify'><span style='font-family: Arial, sans-serif; font-size: 9pt;'>Para la firma del"+
            //                 "presente contrato individual de trabajo, por autorización expresa del Arto. 10"+
            //                 "del Código de Trabajo y por delegación del Gerente General, se autoriza al"+
            //                 "Gerente de Desarrollo del Talento Humano para que en nombre de este, celebre y"+
            //                 "firma el presente contrato de trabajo</span></p>"+

            //            "<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;"+
            //                 "mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;</span></p>"+

            //            "<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;"+
            //            "mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'>&nbsp;<span class='Apple-tab-span' style='white-space:pre'>						</span></span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>____________________</span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>&nbsp;&nbsp;&nbsp;&nbsp; </span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style='font-family: Arial, sans-serif; font-size: 9pt;'>____________________</span></p>"+

            //            "<p class='MsoNormal' style='text-align:justify'><span style='font-size:9.0pt;"+
            //                 "mso-bidi-font-size:9.5pt;font-family:&quot;Arial&quot;,sans-serif'><span class='Apple-tab-span' style='white-space:pre'>						</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EL"+
            //                 "EMPLEADOR&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; EL TRABAJADOR<o:p></o:p></span></p>");             
            //CrearPDF(html);
            #endregion
        }

        #endregion
        #region Eventos
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            GVBenEmpleado.DataSource = Neg_Informes.CargarEmpleadoContrato(Convert.ToInt32(TxtBuscar.Text.Trim()));
            GVBenEmpleado.DataBind();
        }
        protected void GVBenEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Codigo = "_____", Nombre = "_____", Direccion = "_____", Cedula = "_____", Estado = "_____", Cargo = "_____", Salario = "_____";
            string Dia = "", Mes = "", Año = "";
            DataSet ds = Neg_Informes.CargarEmpleadoContrato(Convert.ToInt32(TxtBuscar.Text.Trim()));
            if (ds.Tables != null)
            {
                Dia = ds.Tables[0].Rows[0][11].ToString();
                Mes = ds.Tables[0].Rows[0][10].ToString();
                Año = ds.Tables[0].Rows[0][9].ToString();
            }
            Codigo = GVBenEmpleado.SelectedDataKey["Codigo"].ToString();
            Nombre = GVBenEmpleado.Rows[GVBenEmpleado.SelectedIndex].Cells[1].Text.Trim();
            Direccion = GVBenEmpleado.Rows[GVBenEmpleado.SelectedIndex].Cells[2].Text.Trim();
            Cedula = GVBenEmpleado.Rows[GVBenEmpleado.SelectedIndex].Cells[3].Text.Trim();
            Estado = GVBenEmpleado.Rows[GVBenEmpleado.SelectedIndex].Cells[4].Text.Trim();
            Cargo = GVBenEmpleado.Rows[GVBenEmpleado.SelectedIndex].Cells[5].Text.Trim();
            Salario = GVBenEmpleado.Rows[GVBenEmpleado.SelectedIndex].Cells[6].Text.Trim();
            //Cargar();
            CrearPlantilla(Codigo, Nombre, Direccion, Cedula, Estado, Cargo, Salario, Año, Mes, Dia);

        }
        #endregion
    }
}