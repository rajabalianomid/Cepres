﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cepres.Common.Data
{
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// SQL Server specific extension method for Microsoft.EntityFrameworkCore.DbContextOptionsBuilder
        /// </summary>
        /// <param name="optionsBuilder">Database context options builder</param>
        /// <param name="services">Collection of service descriptors</param>
        public static void UseSqlServerWithLazyLoading(this DbContextOptionsBuilder optionsBuilder, IServiceCollection services)
        {
            CepresConfig config = services.BuildServiceProvider().GetRequiredService<CepresConfig>();
            DbContextOptionsBuilder dbContextOptionsBuilder = optionsBuilder.UseLazyLoadingProxies();
            dbContextOptionsBuilder.UseSqlServer(config.DataConnectionString);
        }
    }
}
