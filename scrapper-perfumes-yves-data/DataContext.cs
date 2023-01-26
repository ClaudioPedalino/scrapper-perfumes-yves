using Microsoft.EntityFrameworkCore;
using scrapper_perfumes_yves_common.Configuration;
using scrapper_perfumes_yves_data.Models;

namespace scrapper_perfumes_yves_data
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationFile.GetConfiguration().DatabaseUrl;
            optionsBuilder.UseNpgsql($"{connectionString}");
        }
    }
}