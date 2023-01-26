using Microsoft.EntityFrameworkCore;
using scrapper_perfumes_yves_data.Models;
using System;
using System.Threading.Channels;

namespace scrapper_perfumes_yves_data
{
    public class DataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "holis";
            optionsBuilder.UseNpgsql($"{connectionString}");
        }
    }
}