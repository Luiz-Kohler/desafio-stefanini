using Application.Common.UnitOfWork;
using Domain.IRepositories;
using Infra.Database;
using Infra.Database.Common;
using Infra.Database.Contexts;
using Infra.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            AddRepositories(services);
            AddMySql(services);
            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ICidadesRepository, CidadesRepository>();
            services.AddScoped<IPessoasRepository, PessoasRepository>();
        }

        private static void AddMySql(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();

            var serviceProvider = services.BuildServiceProvider();
            var connectionString = serviceProvider
                .GetRequiredService<IConnectionStringFactory>()
                .GetConnectionString();

            services.AddDbContextPool<DatabaseContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IScopedDatabaseContext, ScopedDatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
