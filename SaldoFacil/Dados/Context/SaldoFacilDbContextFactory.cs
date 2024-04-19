using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace SaldoFacil.Dados.Context
{
    public class SaldoFacilDbContextFactory : IDesignTimeDbContextFactory<SaldoFacilDbContext>
    {
        public SaldoFacilDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SaldoFacilDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SaldoFacilDb"));

            return new SaldoFacilDbContext(optionsBuilder.Options);
        }
    }
}
