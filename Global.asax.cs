using NLog;
using System;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace task2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureLogger();
        }

        static void ConfigureLogger()
        {
            var config = new NLog.Config.LoggingConfiguration();
            // Targets where to log to: File and Console.
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = $"{HostingEnvironment.MapPath("~/Logs/")}{DateTime.Now.ToShortDateString()}-{DateTime.Now.Hour}.{DateTime.Now.Minute}.{DateTime.Now.Second}.txt"
            };
            logfile.ArchiveOldFileOnStartup = false;
            // Rules for mapping loggers to targets.           
            config.AddRule(LogLevel.Debug, LogLevel.Debug, logfile);
            config.AddRule(LogLevel.Error, LogLevel.Error, logfile);
            config.AddRule(LogLevel.Info, LogLevel.Info, logfile);

            // Apply config           
            LogManager.Configuration = config;
        }
    }
}
