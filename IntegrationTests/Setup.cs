using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public abstract class Setup
{
  protected AppDbContext _dbContext;

  protected Setup()
  {
    var options = CreateNewContextOptions();

    _dbContext = new AppDbContext(options);
  }

  protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
  {
    var serviceProvider = new ServiceCollection()
      .AddEntityFrameworkInMemoryDatabase()
      .BuildServiceProvider();

    var builder = new DbContextOptionsBuilder<AppDbContext>();
    builder.UseInMemoryDatabase("cdr_api")
      .UseInternalServiceProvider(serviceProvider);

    return builder.Options;
  }

  protected InMemoryCallRecordRepository GetRepository()
  {
    return new InMemoryCallRecordRepository(_dbContext);
  }
}
