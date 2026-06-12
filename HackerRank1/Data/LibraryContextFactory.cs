using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LibraryService.WebAPI.Data
{
    public class LibraryContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddUserSecrets(typeof(LibraryContextFactory).Assembly)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = DatabaseConnection.Resolve(configuration);
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}
