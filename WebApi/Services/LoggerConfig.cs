using Microsoft.Extensions.Configuration;
using Serilog;

namespace EonixWebApi.Web.Services
{
    internal static class LoggerConfig
    {
        internal static void ConfigureLogging(IConfiguration configuration)
        {
            //var logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration);
            //Log.Logger = logger.CreateLogger();
        }
    }
}
