using Autofac;

using GDI.Configuration;

using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;

using XperienceAdapter.Core.Configuration;

using XperienceCommunity.UrlRedirection;

namespace GDI
{
    public class Startup
    {
        private const string ConventionalRoutingControllers = "Error|Privacy|ImageUploader|MediaLibraryUploader|FormTest|Account|Profile";
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public IConfigurationSection Options { get; }
        public AutoFacConfig AutoFacConfig => new AutoFacConfig();
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
            Options = configuration.GetSection(nameof(XperienceOptions));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable desired Kentico Xperience features
            var kenticoServiceCollection = services.AddKentico(features =>
            {
                features.UsePageBuilder();
                features.UsePageRouting();
            }).SetAdminCookiesSameSiteNone();

            if (Environment.IsDevelopment())
            {
                kenticoServiceCollection.SetAdminCookiesSameSiteNone();
                kenticoServiceCollection.DisableVirtualContextSecurityForLocalhost();
            }
            services.AddAuthentication();
            services.AddControllersWithViews();
            services.Configure<XperienceOptions>(Options);
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddUrlRedirection();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/404");
            }
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/404";
                    await next();
                }
            });
            app.UseStaticFiles();
            app.UseKentico();
            app.UseCookiePolicy();
            app.UseCors();
            app.UseAuthentication();
            app.UseUrlRedirection();
            app.UseEndpoints(endpoints =>
            {
                endpoints.Kentico().MapRoutes();

                endpoints.MapControllerRoute(
                    name: "error",
                    pattern: "/error/{code}",
                    defaults: new { controller = "Error", action = "Index" },
                    constraints: new
                    {
                        controller = ConventionalRoutingControllers
                    });
                endpoints.MapControllerRoute(
                    name: "static",
                    pattern: "/{controller}/{action}/{id?}",
                    constraints: new
                    {
                        controller = ConventionalRoutingControllers
                    });

                endpoints.MapDefaultControllerRoute();
            });
        }

        private void RegisterInitializationHandler(ContainerBuilder builder) =>
        CMS.Base.ApplicationEvents.Initialized.Execute += (sender, eventArgs) => AutoFacConfig.ConfigureContainer(builder);
        public void ConfigureContainer(ContainerBuilder builder)
        {
            try
            {
                AutoFacConfig.ConfigureContainer(builder);
            }
            catch
            {
                RegisterInitializationHandler(builder);
            }
        }
    }
}
