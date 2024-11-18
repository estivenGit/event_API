using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Utilidades
{
    public interface ILoggerService
    {
        void Information(string message);
        void Error(string message);
        void Error(Exception exception, string message);
    }
}
