using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Log
{
    public interface ILogger
    {
        void Log_Info(Log_M log_M);
        void Log_Warn(Log_M log_M);
        void Log_Error(Log_M log_M);
        void Log_Fatal(Log_M log_M);
    }
}
