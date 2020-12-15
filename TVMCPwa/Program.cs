
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TVMCPwa.Data;
using TVMCPwa.Services;
using TVMCPwa.ViewModels;

namespace TVMCPwa
{
    public class Program
    {
        public IConfiguration Configuration { get; }
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddDbContext<DatabaseContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("DB4FreeConnection")).EnableDetailedErrors());

           
            builder.Services.AddBlazoredModal();
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.TopCenter;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 3000;
                config.ShowProgressBar = true;
                config.ShowTransitionDuration = 2000;
            });
            builder.Services.AddSweetAlert2(options =>
            {
                options.SetThemeForColorSchemePreference(ColorScheme.Dark, SweetAlertTheme.Dark);
                options.Theme = SweetAlertTheme.Dark;
            });
            builder.Services.AddSingleton<GlobalStateChange>();
            builder.Services.AddTransient(typeof(IServiceBase<>), typeof(ServiceBase<>));
            builder.Services.AddTransient<IMensalidadeService, MensalidadeService>();
            builder.Services.AddTransient<IAfiliadoService, AfiliadoService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
