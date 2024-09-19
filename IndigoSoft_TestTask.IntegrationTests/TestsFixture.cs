using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IndigoSoft_TestTask.IntegrationTests;

public class TestsFixture: IDisposable
{
    private readonly IndigoSoftTestTaskDbContext _dbContext;
    public IndigoSoftTestTaskDbContext DbContext { get => _dbContext; }
    public TestsFixture()
    {
        var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = (configuration.GetConnectionString(nameof(IndigoSoftTestTaskDbContext)));

        var optionsBuilder = new DbContextOptionsBuilder<IndigoSoftTestTaskDbContext>()
        .UseNpgsql(connectionString);

        _dbContext = new IndigoSoftTestTaskDbContext(optionsBuilder.Options);

        _dbContext.Database.EnsureDeleted();
        _dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}
