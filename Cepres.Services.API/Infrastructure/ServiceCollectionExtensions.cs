using Cepres.Common;
using Cepres.Data;
using Cepres.Services.API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cepres.Service.API.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            //add Config configuration parameters
            CepresConfig config = services.ConfigureStartupConfig<CepresConfig>(configuration.GetSection("Cepres"));
            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            CommonHelper.DefaultFileProvider = new FileManagerProvider(hostingEnvironment);

            IMvcCoreBuilder mvcCoreBuilder = services.AddMvcCore();

            //create, initialize and configure the engine
            IEngine engine = EngineContext.Create();
            IServiceProvider serviceProvider = engine.ConfigureServices(services, configuration);
            SqlServerDataProvider.InitializeDatabase(config.DataConnectionString);


            return serviceProvider;
        }
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            //create instance of config
            TConfig config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }
        public static void AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<Context>(optionsBuilder =>
            {
                CepresConfig CepresConfig = services.BuildServiceProvider().GetRequiredService<CepresConfig>();
                DbContextOptionsBuilder dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
                dbContextOptionsBuilder.UseSqlServer(CepresConfig.DataConnectionString);
            });
        }
        public static void AddBlobServiceClient(this IServiceCollection services)
        {
            //services.AddConnections(c => 
            //{ 
            //    c.AddBlobServiceClient();
            //});
        }
    }
}
