using Domain.Common.Environments;

namespace Infra.Database.Common
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private readonly IEnvironmentVariables _environmentVariables;

        public ConnectionStringFactory(IEnvironmentVariables environmentVariables)
        {
            _environmentVariables = environmentVariables;
        }

        public string GetConnectionString()
        {
            return _environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.DBConnection)
                ?? "Data Source = sqldata,1433; Initial Catalog = master; Persist Security Info = True; User ID = sa; Password = Stefanini@123";
        }
    }
}
