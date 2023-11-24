using KompInvest.Data;
//using KompInvest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendGrid;
using System;

namespace KompInvest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Build the connection string using the DATABASE_URL environment variable
            var connectionString = GetHerokuConnectionString() ??
                                   Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            // Configure Identity with custom options
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                // Add more custom options here if needed
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            //services.AddHostedService<RoleInitializer>();

            services.AddControllersWithViews();

            services.AddSingleton(x => new SendGridClient(Configuration["SendGrid:ApiKey"]));

            services.AddSession();

            services.AddRazorPages();
        }
        private string GetHerokuConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // Check if a DATABASE_URL is provided, which indicates we are running on Heroku
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                // Parse the connection string
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');

                // Build the connection string using Npgsql format
                return $"Host={databaseUri.Host};Database={databaseUri.LocalPath.TrimStart('/')};" +
                       $"Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=True";
            }

            return null;
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //SeedDatabase(serviceProvider);


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
        //private void SeedDatabase(IServiceProvider serviceProvider)
        //{
        //    using (var scope = serviceProvider.CreateScope())
        //    {
        //        var scopedServices = scope.ServiceProvider;
        //        try
        //        {
        //            SeedData.Initialize(scopedServices).Wait();
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exceptions, you might want to log this
        //            var logger = scopedServices.GetRequiredService<ILogger<Startup>>();
        //            logger.LogError(ex, "An error occurred seeding the DB.");
        //        }
        //    }
        //}
    }
}

