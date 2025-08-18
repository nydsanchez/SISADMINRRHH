using NLog;
using System;
using System.Web;
using System.Web.Routing;

namespace NominaRRHH
{
    public class Global : HttpApplication
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        void Application_Start(object sender, EventArgs e)
        {


            // Configuración de NLog usando Setup
            var config = new NLog.Config.LoggingConfiguration();

            // Definir los targets
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                // Usar un patrón de nombre de archivo que incluya la fecha
                FileName = "logs/KRP_${shortdate}.txt", // Crea un archivo por día
                Layout = "${longdate} ${level} ${message}" // Formato del contenido del log
            };

            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Regla para escribir en los targets
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole);

            // Aplicar la configuración
            LogManager.Configuration = config;

            // Crear un logger
            var logger = LogManager.GetCurrentClassLogger();

            // Ejemplo de uso
            logger.Info("Aplicación iniciada.");



            // Code that runs on application startup
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin"));

            
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Cerrar el logger al finalizar la aplicación
            LogManager.Shutdown(); // Asegúrate de cerrar el logger al final
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Inicializa valores en la sesión si es necesario
            Session["Idusuario"] = null; // o algún valor predeterminado
        }

       

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
