using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Utilidades
{
    public class SerilogLoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public SerilogLoggerService()
        {
            _logger = Log.Logger;
        }

        public void Information(string message)
        {
            _logger.Information(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception, string message)
        {
            _logger.Error(exception, message);
        }
    }
}
