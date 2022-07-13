using Application;
using Domain;
using Infra;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Tests.Integration
{
    public static class TestServiceCollecionFactory
    {
        public static IServiceCollection BuildIntegrationTestInfrastructure(string dbConnectionString)
        {
            var services = new ServiceCollection();
            services.AddTestDatabase(dbConnectionString);
            services.AddTestLogs();
            services.AddDomain();
            services.AddApplication();
            services.AddInfra();
            services.AddLogging();
            ConfigureCulture();

            return services;
        }

        private static void ConfigureCulture()
        {
            var ptBrCulture = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = ptBrCulture;
            CultureInfo.DefaultThreadCurrentUICulture = ptBrCulture;
        }
    }
}
