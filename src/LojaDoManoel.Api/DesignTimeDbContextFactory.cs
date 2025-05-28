using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LojaDoManoel.Api.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LojaDoManoelDbContext>
    {
        public LojaDoManoelDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LojaDoManoelDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            builder.UseSqlServer(connectionString);

            return new LojaDoManoelDbContext(builder.Options);
        }
    }
}