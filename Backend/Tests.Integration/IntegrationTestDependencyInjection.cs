using Application.Common.UnitOfWork;
using Domain.Common;
using Domain.Common.Environments;
using Infra.Database;
using Infra.Database.Common;
using Infra.Database.Contexts;
using Infra.Database.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Tests.Integration
{
    public static class IntegrationTestDependencyInjection
    {
        public static void AddTestLogs(this IServiceCollection services)
        {
            services.AddSingleton(Substitute.For<ILoggerFactory>());
            foreach (var loggerType in GetLoggers().Value)
            {
                services.AddSingleton(loggerType,
                    Substitute.For(typesToProxy: new[] { loggerType }, constructorArguments: Array.Empty<object>()));
            }
        }

        private static Lazy<IEnumerable<Type>> GetLoggers() => new(() =>
        {
            var assemblies = new[]
            {
            typeof(BaseEntity).Assembly,
            typeof(UnitOfWorkBehavior<,>).Assembly,
            typeof(BaseRepository<>).Assembly,
        };
            var loggerTypes = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract)
                .SelectMany(x => x.GetConstructors())
                .SelectMany(x => x.GetParameters())
                .Select(x => x.ParameterType)
                .Where(x => x.IsGenericType && typeof(ILogger).IsAssignableFrom(x))
                .Where(x => !x.ContainsGenericParameters)
                .ToArray();

            var requestLoggerTypes = typeof(UnitOfWorkBehavior<,>).Assembly
                .GetTypes()
                .Where(x => typeof(IBaseRequest).IsAssignableFrom(x))
                .Select(x => typeof(ILogger<>).MakeGenericType(x))
                .ToArray();

            return loggerTypes.Union(requestLoggerTypes);
        });

        public static void AddTestDatabase(this IServiceCollection services, string dbConnection)
        {
            var environmentVariables = Substitute.For<IEnvironmentVariables>();
            environmentVariables.GetEnvironmentVariable(Arg.Any<string>()).Returns((string)null);
            var config = new[]
            {
                ("DBConnection", dbConnection),
            };

            foreach (var (variable, value) in config)
            {
                environmentVariables.GetEnvironmentVariable(variable).Returns(value);
            }

            services.AddScoped<IScopedDatabaseContext, ScopedDatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(environmentVariables);
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();

            services.AddDbContextPool<DatabaseContext>(opt => opt.UseSqlServer(dbConnection));
        }
    }
}
