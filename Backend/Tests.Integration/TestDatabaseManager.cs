using Infra.Database;
using Infra.Database.Common;
using Infra.Database.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;


namespace Tests.Integration
{
    public static class TestDatabaseManager
    {
        public static void RebuildDatabase(IServiceProvider serviceProvider)
        {
            var databaseContext = serviceProvider.GetService<DatabaseContext>();
            databaseContext!.Database.EnsureDeleted();
            databaseContext.Database.Migrate();
        }

        public static void TruncateAllTables(IServiceProvider serviceProvider)
        {
            var databaseContext = serviceProvider.GetService<DatabaseContext>();

            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Pessoa DROP CONSTRAINT FK_Pessoa_Cidade_CidadeId");

            var tableNames = GetTableNames();
            foreach (var tableName in tableNames)
            {
                databaseContext!.Database.ExecuteSqlRaw($"TRUNCATE TABLE {tableName};");
            }

            databaseContext!.Database.ExecuteSqlRaw($"ALTER TABLE Pessoa ADD CONSTRAINT FK_Pessoa_Cidade_CidadeId FOREIGN KEY(ID) REFERENCES Cidade (ID)");
        }

        private static IEnumerable GetTableNames()
        {
            var tableNames = typeof(PessoaMapping).Assembly
                .GetTypes()
                .Where(x => x.IsSubclassOfRawGeneric(typeof(BaseMapping<>)))
                .Where(x => x.IsAbstract is false)
                .Select(x => Activator.CreateInstance(x))
                .Select(x => x.GetType().GetProperty(nameof(PessoaMapping.TableName)).GetValue(x))
                .ToList();
            return tableNames;
        }
    }

    public static class TypeExtensions
    {
        public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}
