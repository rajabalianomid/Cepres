using Autofac;
using Cepres.Common;
using Cepres.Common.Data;
using Cepres.Data;
using Cepres.Data.Repositories;
using Cepres.Services.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cepres.Service.API.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => 0;

        public void Register(ContainerBuilder builder, CepresConfig config)
        {
            builder.Register(context => new Context(context.Resolve<DbContextOptions<Context>>())).As<IDbContext>().InstancePerLifetimeScope();
            if (config.UserEF)
            {
                builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
                builder.RegisterType<IPatientMetaDataRepository>().AsSelf().InstancePerRequest();
            }
            else
            {
                builder.RegisterGeneric(typeof(StaticSqlRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
                builder.RegisterType<PatientMetaDataRepository>().As<IPatientMetaDataRepository>().InstancePerLifetimeScope();
                builder.RegisterType<PatientRepository>().As<IPatientRepository>().InstancePerLifetimeScope();
                builder.RegisterType<RecordRepository>().As<IRecordRepository>().InstancePerLifetimeScope();
            }
            builder.RegisterType<PatientService>().As<IPatientService>().InstancePerLifetimeScope();
            builder.RegisterType<PatientMetaDataService>().As<IPatientMetaDataService>().InstancePerLifetimeScope();
            builder.RegisterType<RecordService>().As<IRecordService>().InstancePerLifetimeScope();
            builder.RegisterType<FileManagerProvider>().As<IFileManagerProvider>().InstancePerDependency();
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
        }
    }
}
