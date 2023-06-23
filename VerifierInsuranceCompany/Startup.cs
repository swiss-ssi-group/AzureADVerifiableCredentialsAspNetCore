using IssuerDrivingLicense;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace VerifierInsuranceCompany;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<VerifierService>();
        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        services.Configure<CredentialSettings>(Configuration.GetSection("CredentialSettings"));
        services.AddHttpClient();
        services.AddDistributedMemoryCache();

        services.AddRazorPages();
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
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapRazorPages();
        });
    }
}
