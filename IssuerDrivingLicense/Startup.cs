using IssuerDrivingLicense.Persistence;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace IssuerDrivingLicense;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        services.Configure<CredentialSettings>(Configuration.GetSection("CredentialSettings"));
        services.AddScoped<DriverLicenseService>();
        services.AddScoped<IssuerService>();

        services.AddDatabaseDeveloperPageExceptionFilter();
        services.AddDbContext<DrivingLicenseDbContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));

        services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });

        services.AddDistributedMemoryCache();

        services.AddRazorPages()
            .AddMvcOptions(options => { })
            .AddMicrosoftIdentityUI();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSecurityHeaders(SecurityHeadersDefinitions
           .GetHeaderPolicyCollection(env.IsDevelopment()));

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapControllers();
        });
    }
}
