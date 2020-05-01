using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace UniLink.Client.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjQ3MjU1QDMxMzgyZTMxMmUzMExya2UyTUdUQmV5cXgyeCtTVFQrVXRtb0JLcDNsQy9UeXg5dlB3eTdoMGs9");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}