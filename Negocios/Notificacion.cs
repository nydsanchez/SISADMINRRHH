using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using System.IO;

namespace Negocios
{
    public class Notificacion
    {
        public bool EnviarNotificacionVariosDestinatarios(string Mensaje, string Asunto, List<String> correos, string Attachments)
        {
            MailMessage objEmail = new MailMessage();
            //string destinatario = grupo + "@crp7.com";
            //cambiad info@kaizen... for administrador@...
            objEmail.From = new MailAddress("soporte@kaizen.com.ni");
            foreach (var item in correos)
            {
                objEmail.To.Add(new MailAddress(item));
            }

            //objEmail.Bcc.Add("wmembreno@kaizen.com.ni");
            objEmail.Priority = MailPriority.Normal;
            objEmail.Subject = Asunto;
            objEmail.Body = Mensaje;

            if (Attachments != "")
                objEmail.Attachments.Add(new Attachment(Attachments));



            objEmail.IsBodyHtml = true;
            SmtpClient objSmtp = new SmtpClient();
            //objSmtp.Host = "mail.crp7.com";
            //objSmtp.Port = 2552;
            objSmtp.Host = "kaizen.com.ni";
            objSmtp.Port = 25;
            //objSmtp.Credentials = new System.Net.NetworkCredential("infor", "Kaizen2015");
            objSmtp.Credentials = new System.Net.NetworkCredential("soporte@kaizen.com.ni", "Ma$7er.21");
            try
            {
                objSmtp.Send(objEmail);
                return true;
            }
            catch 
            {
                return false;
            }
        }
        // TODO:VHPO 17/12/2024
        public bool EnviarNotificacionVariosDestinatarios(string Asunto, string Mensaje, List<string> correos, int intentosMax = 3)
        {
            int intentos = 0;
            bool enviado = false;

            while (intentos < intentosMax && !enviado)
            {
                try
                {
                    // TODO: VHPO 18/12/2024
                    // Configurar el objeto SmtpClient 2552
                    SmtpClient objSmtp = new SmtpClient("kaizen.com.ni", 25)
                    {
                        EnableSsl =  false, // Ajusta esto según lo requiera tu servidor SMTP false Kaizen2015
                        Credentials = new System.Net.NetworkCredential("soporte", "Ma$7er.21")
                    };

                    // Configurar el mensaje de correo
                    MailMessage objEmail = new MailMessage
                    {
                        From = new MailAddress("soporte@kaizen.com.ni"),
                        Subject = Asunto,
                        Body = Mensaje,
                        IsBodyHtml = true
                    };

                    // Agregar destinatarios
                    foreach (var correo in correos)
                    {
                        if (!string.IsNullOrEmpty(correo) && IsValidEmail(correo))
                        {
                           objEmail.To.Add(new MailAddress(correo));
                        }
                        else
                        {
                            Console.WriteLine($"Correo inválido: {correo}");
                            //throw new FormatException($"Correo inválido: {correo}");
                        }

                        //objEmail.To.Add(new MailAddress(correo));
                    }

                    //objEmail.To.Add(new MailAddress("ijimenez@kaizen.com.ni"));

                    // Crear vista alternativa HTML
                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Mensaje, Encoding.UTF8, MediaTypeNames.Text.Html);
                    objEmail.AlternateViews.Add(htmlView);

                    // Enviar correo
                    objSmtp.Send(objEmail);
                    

                    // Si llega aquí, el envío fue exitoso
                    enviado = true;
                }
                catch (SmtpException ex)
                {
                    intentos++;
                    Console.WriteLine($"Error al enviar correo. Reintento {intentos}/{intentosMax}. Detalle: {ex.Message}");
                    System.Threading.Thread.Sleep(5000); // Espera de 5 segundos antes de reintentar
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                    break; // Sale del bucle si ocurre un error diferente
                }
            }

            return enviado;
        }


        public bool EnviarNotificacionVariosDestinatariosPDF(string Asunto, string Mensaje, List<string> correos, string rutaTemporal, int intentosMax = 3)
        {
            int intentos = 0;
            bool enviado = false;

            while (intentos < intentosMax && !enviado)
            {
                try
                {
                    // Configurar el objeto SmtpClient
                    SmtpClient objSmtp = new SmtpClient("kaizen.com.ni", 25)
                    {
                        EnableSsl = true,
                        Credentials = new System.Net.NetworkCredential("soporte@kaizen.com.ni", "Ma$7er.21")
                    };

                    // Configurar el mensaje de correo
                    MailMessage objEmail = new MailMessage
                    {
                        From = new MailAddress("soporter@kaizen.com.ni"),
                        Subject = Asunto,
                        Body = Mensaje,
                        IsBodyHtml = true
                    };

                    // Agregar destinatarios
                    foreach (var correo in correos)
                    {
                        if (!string.IsNullOrEmpty(correo) && IsValidEmail(correo))
                        {
                            objEmail.To.Add(new MailAddress(correo));
                   
                            //objEmail.To.Add(new MailAddress("vporras@kaizen.com.ni"));
                        }
                        else
                        {
                            Console.WriteLine($"Correo inválido: {correo}");
                        }
                    }

                    // Generar el archivo PDF desde Crystal Reports
                    string rutaPDF = rutaTemporal;

                    //string rutaPDF = Path.Combine(rutaTemporal, $"Reporte_{Guid.NewGuid()}.pdf");
                    //reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, rutaPDF);

                    // Adjuntar el archivo PDF
                    Attachment attachment = new Attachment(rutaPDF, MediaTypeNames.Application.Pdf);
                    objEmail.Attachments.Add(attachment);

                    // Enviar correo
                    objSmtp.Send(objEmail);

                    // Si llega aquí, el envío fue exitoso
                    enviado = true;

                    // Limpiar el objeto MailMessage
                    objEmail.Dispose(); // Libera los recursos utilizados por MailMessage

                    // Eliminar el archivo temporal
                    if (File.Exists(rutaPDF))
                    {
                        File.Delete(rutaPDF);
                    }
                }
                catch (SmtpException ex)
                {
                    intentos++;
                    Console.WriteLine($"Error al enviar correo. Reintento {intentos}/{intentosMax}. Detalle: {ex.Message}");
                    System.Threading.Thread.Sleep(5000); // Espera de 5 segundos antes de reintentar
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                    break;
                }
            }

            return enviado;
        }

        public bool EnviarNotificacionVariosDestinatariosv1(string Asunto, string Mensaje, List<string> correos)
        {
            MailMessage objEmail = new MailMessage();

            try
            {
                // Configurar vista alternativa HTML
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                    Mensaje, Encoding.UTF8, MediaTypeNames.Text.Html);

                // Configurar dirección de origen
                objEmail.From = new MailAddress("soporte@kaizen.com.ni");

                // Validar correos
                if (correos == null || correos.Count == 0)
                {
                    throw new ArgumentException("La lista de correos está vacía o es nula.");
                }

                // Agregar destinatarios
                foreach (var correo in correos)
                {
                    //if (!string.IsNullOrEmpty(correo) && IsValidEmail(correo))
                    //{
                    //    //objEmail.To.Add(new MailAddress(correo));
                        
                    //}
                    //else
                    //{
                    //    throw new FormatException($"Correo inválido: {correo}");
                    //}
                }

                objEmail.To.Add(new MailAddress("ijimenez@kaizen.com.ni"));
                //objEmail.To.Add(new MailAddress("vporras@kaizen.com.ni"));

                // Configuración del correo
                objEmail.AlternateViews.Add(htmlView);
                objEmail.Priority = MailPriority.Normal;
                objEmail.Subject = Asunto;
                objEmail.Body = Mensaje;
                objEmail.IsBodyHtml = true;

                // Configuración del servidor SMTP
                SmtpClient objSmtp = new SmtpClient

                // cambiado por 12 marzo 2025
                //{
                //    Host = "mail.crp7.com",
                //    Port = 2552,
                //    Credentials = new System.Net.NetworkCredential("infor", "Kaizen2015"),
                //    EnableSsl = false // Ajusta según la configuración de tu servidor
                //};

                // nueva configuracion al 12 marzo 2025
                {
                    Host = "kaizen.com.ni",
                    Port = 25,
                    Credentials = new System.Net.NetworkCredential("soporte@kaizen.com.ni", "Ma$7er.21"),
                    EnableSsl = false
                };

                // Configuración del servidor SMTP con SSL
                //SmtpClient objSmtp = new SmtpClient
                //{
                //    Host = "mail.crp7.com", // Configura tu servidor Exchange
                //    Port = 587,             // Puerto común para SMTP con SSL/TLS (puedes probar con 465 también)
                //    EnableSsl = true,       // Habilitar SSL
                //    Credentials = new System.Net.NetworkCredential("infor", "Kaizen2015"),
                //    DeliveryMethod = SmtpDeliveryMethod.Network
                //};

                // Enviar correo
                objSmtp.Send(objEmail);
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"Error SMTP: {ex.Message}");
                return false;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error en formato: {ex.Message}");
                return false;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error en argumento: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
                return false;
            }
            finally
            {
                objEmail.Dispose();
            }
        }

        /// <summary>
        /// Método auxiliar para validar correos electrónicos
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool EnviarCorreoConReintento(string asunto, string mensaje, List<string> correos, int intentosMax = 3)
        {
            int intentos = 0;
            bool enviado = false;

            while (intentos < intentosMax && !enviado)
            {
                // se cambio el smtp y las credenciales 12 marzo 2025
                try
                {
                    SmtpClient objSmtp = new SmtpClient("kaizen.com.ni", 25)
                    {
                        EnableSsl = false,
                        Credentials = new System.Net.NetworkCredential("soporte@kaizen.com.ni", "Ma$7er.21")
                    };

                    MailMessage objEmail = new MailMessage
                    {
                        From = new MailAddress("soporte@kaizen.com.ni"),
                        Subject = asunto,
                        Body = mensaje,
                        IsBodyHtml = true
                    };

                    foreach (var correo in correos)
                        objEmail.To.Add(new MailAddress(correo));

                    objSmtp.Send(objEmail);
                    enviado = true; // Si llega aquí, se envió correctamente
                }
                catch (SmtpException ex)
                {
                    intentos++;
                    Console.WriteLine($"Error al enviar correo. Reintento {intentos}/{intentosMax}. Detalle: {ex.Message}");
                    System.Threading.Thread.Sleep(5000); // Espera de 5 segundos antes de reintentar
                }
            }

            return enviado;
        }


        // old metodo envio email
        public bool EnviarNotificacionVariosDestinatariosOld(string Asunto, string Mensaje, List<String> correos)
        {
            MailMessage objEmail = new MailMessage();

            AlternateView htmlView =
            AlternateView.CreateAlternateViewFromString(Mensaje,
                             Encoding.UTF8,
                             MediaTypeNames.Text.Html);

            // Creamos el recurso a incrustar. Observad
            // que el ID que le asignamos (arbitrario) está
            // referenciado desde el código HTML como origen
            // de la imagen (resaltado en amarillo)...

            //LinkedResource imagen1 =
            //    new LinkedResource(HttpContext.Current.Server.MapPath("~/Recursos/Imagenes/header.jpg"),
            //                        MediaTypeNames.Image.Jpeg);
            //imagen1.ContentId = "imagen1";

            //// Lo incrustamos en la vista HTML...

            //LinkedResource imagen2 =
            //    new LinkedResource(HttpContext.Current.Server.MapPath("~/Recursos/Imagenes/kaizen.png"),
            //                        MediaTypeNames.Image.Jpeg);
            //imagen2.ContentId = "imagen2";

            //// Lo incrustamos en la vista HTML...

            //LinkedResource imagen3 =
            //    new LinkedResource(HttpContext.Current.Server.MapPath("~/Recursos/Imagenes/5s.png"),
            //                        MediaTypeNames.Image.Jpeg);
            //imagen3.ContentId = "imagen3";

            //// Lo incrustamos en la vista HTML...

            //htmlView.LinkedResources.Add(imagen1);
            //htmlView.LinkedResources.Add(imagen2);
            //htmlView.LinkedResources.Add(imagen3);


            //string destinatario = grupo + "@crp7.com";

            objEmail.From = new MailAddress("infor@kaizen.com.ni");
            foreach (var item in correos)
            {
                objEmail.To.Add(new MailAddress(item));
            }

            objEmail.AlternateViews.Add(htmlView);
            //objEmail.Bcc.Add("gtercero@kaizen.com.ni");
            objEmail.Priority = MailPriority.Normal;
            objEmail.Subject = Asunto;

            objEmail.Body = Mensaje;

            objEmail.IsBodyHtml = true;
            SmtpClient objSmtp = new SmtpClient();
            objSmtp.Host = "mail.crp7.com";
            objSmtp.Port = 2552;
            //objSmtp.Host = "localhost";//para pruebas locales
            objSmtp.Credentials = new System.Net.NetworkCredential("infor", "Kaizen2015");

            try
            {
                objSmtp.Send(objEmail);
                return true;
            }
            catch (SystemException)
            {
                return false;
            }
        }
    }
}
