using AppNomesBr.Infrastructure.IoC;
using AppNomesBr.Pages;
using AppNomesBr.Domain.Interfaces.Repositories; // Interface
using AppNomesBr.Infrastructure.Repositories; // Classe Repository
using AppNomesBr.Domain.Interfaces.Services; // Interface do Serviço
using AppNomesBr.Domain.Services; // Classe do Serviço
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AppNomesBr
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var startup = new Startup();
            builder.Configuration.AddConfiguration(startup.Configuration);
            builder.Services.AddLogging();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registra os serviços e repositórios
            NativeInjector.RegisterServices(builder.Services, startup.Configuration);
            RegisterPages(builder.Services);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        /// <summary>
        /// Método responsável por fazer a injeção de dependência das páginas da aplicação.
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterPages(IServiceCollection services)
        {
            #region Transient

            services.AddTransient<RankingNomesBrasileiros>();
            services.AddTransient<NovaConsultaNome>();
            services.AddTransient<INomesBrRepository, NomesBrRepository>(); // Registre o repositório aqui
            services.AddTransient<INomesBrService, NomesBrService>(); // Registre o serviço aqui

            #endregion

            #region Scoped

            // Se houver serviços que precisam ser gerenciados por escopo, registre-os aqui.

            #endregion
        }
    }

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream stream = assembly.GetManifestResourceStream("AppNomesBr.appsettings.json") ?? Stream.Null;
            Configuration = new ConfigurationBuilder()
                .AddJsonStream(stream).Build();
        }
    }
}
