using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace taskAPI.Model
{
    public sealed class ToDoContextFactory : IDesignTimeDbContextFactory<ToDoContext>
    {
        private const string ConnectionString = "Host=postgres;Port=5432;UserId=postgres;Password=5koNorJ7WVqprgESuS;Database=todoAPI;CommandTimeout=300;";
        
        public ToDoContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ToDoContext> optionsBuilder = new();
            optionsBuilder.UseNpgsql(ConnectionString,
                b => b.EnableRetryOnFailure()
                    .MigrationsAssembly("taskAPI")
                    .MigrationsHistoryTable("__MigrationHistory", ToDoContext.SchemaName));
            return new ToDoContext(optionsBuilder.Options);
        }
    }
}