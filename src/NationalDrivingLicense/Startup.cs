using NationalDrivingLicense.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Fido2NetLib;
using System;
using NationalDrivingLicense.Services;

namespace NationalDrivingLicense
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
            services.AddHttpClient();

            services.Configure<AzureDidConfiguration>(Configuration.GetSection("AzureDidConfiguration"));
            services.AddScoped<AzureADCredentialsService>();

            services.AddScoped<DriverLicenseService>();

            services.AddDistributedMemoryCache();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>(
                    options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<Fifo2UserTwoFactorTokenProvider>("FIDO2");

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IUserClaimsPrincipalFactory<IdentityUser>,
                AdditionalUserClaimsPrincipalFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TwoFactorEnabled",
                    x => x.RequireClaim("amr", "mfa")
                );
            });

            services.AddControllers()
               .AddNewtonsoftJson();

            services.AddRazorPages();

            services.Configure<Fido2Configuration>(Configuration.GetSection("fido2"));
            services.AddScoped<Fido2Storage>();
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.Name = "__Host-Session";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
