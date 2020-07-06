using Cepres.Common;
using Cepres.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cepres.Service.API.Infrastructure
{
    public class DbStartup : ICepresStartup
    {
        public int Order => 10;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<Context>(optionsBuilder =>
            {
                optionsBuilder.UseLazyLoadingProxies();
            });
        }
    }
}
