using AppVMC.Data;
using AppVMC.Services;
using AppVMC.ViewModels;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using MatBlazor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace AppVMC
{
    public class Startup
    {
        public static string _database;
        public static string _domainPath;

        public static bool _envProdution;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _database = Configuration.GetSection("LiteDbDatabase").GetSection("FilePath").Value;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddRazorPages();
            services.AddOptions();
            services.AddDbContext<DatabaseContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DBTVMCUolConnection"))
               .EnableDetailedErrors()
              );

            services.AddServerSideBlazor();
            services.AddBlazoredModal();
            services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.TopFullWidth;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 3000;
                config.ShowProgressBar = true;
                config.ShowTransitionDuration = 2000;
            });
            services.AddSweetAlert2(options =>
            {
                options.SetThemeForColorSchemePreference(ColorScheme.Dark, SweetAlertTheme.Dark);
                options.Theme = SweetAlertTheme.Dark;
            });
            services.AddSingleton<GlobalStateChange>();
           // services.AddScoped<ToastService>();
            services.AddTransient(typeof(IServiceBase<>), typeof(ServiceBaseLiteDB<>));
            services.AddTransient<IMensalidadeService, MensalidadeService>();
            services.AddTransient<IAfiliadoService, AfiliadoService>();
            services.AddTransient<ICaixaService, CaixaService>();
            services.AddTransient<ILancamentoService, LancamentoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Definindo a cultura padr�o: pt-BR
            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            _envProdution = env.IsProduction();

            if (env.IsDevelopment())
            {

                _domainPath = "/";
                app.UseDeveloperExceptionPage();
            }
            else
            {
                _domainPath="/terreiro/";

                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UsePathBase(_domainPath);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //if (env.IsDevelopment())
                //{
                //    endpoints.MapBlazorHub();
                //}
                //else
                //{
                //    endpoints.MapBlazorHub($"{_domainPath}_blazor");
                //}
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

            });
        }
    }
}
