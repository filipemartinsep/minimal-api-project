using Microsoft.EntityFrameworkCore;
using minimal_api.Domain.Entities;

namespace minimal_api.Infrastructure.Db
{
  public class DatabaseContext : DbContext
  {
    private readonly IConfiguration _configurationAppSettings;

    public DatabaseContext(IConfiguration configurationAppSettings)
    {
      _configurationAppSettings = configurationAppSettings;
    }

    public DbSet<Administrator> Administrators { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          if(!optionsBuilder.IsConfigured)
          {
            var stringConnection = _configurationAppSettings.GetConnectionString("mysql")?.ToString();
            if(!string.IsNullOrEmpty(stringConnection))
            {
              optionsBuilder.UseMySql(
                stringConnection,
                ServerVersion.AutoDetect(stringConnection)
              );
            }
          }
        }
    }
}