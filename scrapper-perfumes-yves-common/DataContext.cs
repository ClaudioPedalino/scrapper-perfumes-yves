using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using scrapper_perfumes_yves_common.Models;
using scrapper_perfumes_yves_common.ServiceConfiguration;

namespace scrapper_perfumes_yves_common
{
    public sealed class DataContext : DbContext
    {
        private readonly IOptionsSnapshot<Configuration> _configuration;

        public DataContext(IOptionsSnapshot<Configuration> configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Product> Products { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = _configuration.Value;

            if (configuration.DataConfiguration.DatabaseEnabled)
            {
                var connectionString = configuration.DataConfiguration.DatabaseUrl;
                optionsBuilder.UseNpgsql($"{connectionString}");
            }
            else // TODO: use sql lite - configuration.DataConfiguration.FileEnabled
            {

            }
        }
    }
}