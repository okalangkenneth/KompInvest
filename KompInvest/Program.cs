using KompInvest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

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
                    // Heroku dynamically assigns a port
                    var port = Environment.GetEnvironmentVariable("PORT");
                    if (!string.IsNullOrEmpty(port))
                    {
                        serverOptions.ListenAnyIP(int.Parse(port));
                    }
                });
            });
}
