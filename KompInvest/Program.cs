using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace KompInvest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            webBuilder.ConfigureKestrel(serverOptions =>
            {
                // Bind to the port provided by Heroku
                int port = int.Parse(Environment.GetEnvironmentVariable("PORT") ?? "5000");
                serverOptions.ListenAnyIP(port);
            });
        });

    }
}
