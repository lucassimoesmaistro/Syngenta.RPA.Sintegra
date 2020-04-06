using Serilog;
using Serilog.Events;
using System;

namespace Syngenta.Common.Log
{
    public class Base
    {
        public static Serilog.ILogger Logar;

        public Base(string applicationName)
        {
            string nomeArquivoLog = $"../Logs/{DateTime.Now.ToString("yyyyMMddHHmmss")}_{applicationName.Replace(".", "_")}.log";
            Logar = new LoggerConfiguration()
                          .WriteTo
                            .Logger(lc => lc.Filter
                                            .ByIncludingOnly(e => e.Level == LogEventLevel.Information 
                                                            || e.Level == LogEventLevel.Debug
                                                            || e.Level == LogEventLevel.Error)
                                            .WriteTo.RollingFile(nomeArquivoLog))
                          .CreateLogger();

        }

    }
}
