using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace AppVMC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var port = Environment.GetEnvironmentVariable("PORT");
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    if (!isDevelopment)
                    {
                        /*usado para deploy no Heroku*/
                        //webBuilder.UseStartup<Startup>()
                        //                       .UseKestrel((opt) => opt.AllowSynchronousIO = true)
                        //                       .UseUrls("http://*:" + port);
                        
                        /*sem Kestrel para ser usado no UOLHOST*/
                        webBuilder.UseStartup<Startup>();
                    }
                    else
                    {
                        webBuilder.UseStartup<Startup>()
                       .UseKestrel((opt) => opt.AllowSynchronousIO = true);
                    }
                });
        }
    }
}
