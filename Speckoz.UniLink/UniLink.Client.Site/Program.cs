using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Syncfusion.Licensing;

namespace UniLink.Client.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SyncfusionLicenseProvider.RegisterLicense("MjM3OTA3QDMxMzgyZTMxMmUzMEcySTY3NmlzZGlFcjhnaGpPMWwvbEZtbUdzZldKZUF3QVlaWW1Ha0tTZ2s9");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}