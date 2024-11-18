using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using Serilog;
using System;
using System.IO;

namespace Transversal.Utilidades
{
    public static class LoggingConfig
    {
        public static void ConfigureLogging()
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app_log.txt");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day) // Log en la raíz de la app
                .CreateLogger();
        }

        public static ILogger GetLogger()
        {
            return Log.Logger;
        }
    }

}
