using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using provider.InfraStructure.Log;

namespace provider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Logger logger = NLog.LogManager.Setup()
               .SetupExtensions(s => s.AutoLoadAssemblies(false).RegisterLayoutRenderer<ElapsedTimeLayoutRenderer>("elapsed-time")                                                               
               )
               .RegisterNLogWeb()
               .LoadConfigurationFromFile("nlog.config")
               .GetCurrentClassLogger();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseNLog();
    }
}
