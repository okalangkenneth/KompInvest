using KompInvest.Data;
using KompInvest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SendGrid;
using System;
using System.Threading.Tasks;

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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            // Check if a DATABASE_URL is provided, which indicates we are running on Heroku
            if (!string.IsNullOrEmpty(databaseUrl))
            {
                // Parse the connection string
                var databaseUri = new Uri(databaseUrl);
                var userInfo = databaseUri.UserInfo.Split(':');

                // Build the connection string using Npgsql format
                connectionString = $"Host={databaseUri.Host};Database={databaseUri.LocalPath.TrimStart('/')};" +
                    $"Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=True";
            }

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

            services.AddHostedService<RoleInitializer>();

            services.AddControllersWithViews();

            services.AddSingleton(x => new SendGridClient(Configuration["SendGrid:ApiKey"]));

            services.AddSession();

            services.AddRazorPages();
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
    }
}

