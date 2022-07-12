namespace Infra.Database.Contexts
{
    public interface IScopedDatabaseContext
    {
        DatabaseContext Context { get; }
    }
}
