using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PixelService.Context.Models;

namespace PixelService.Context
{
    public class PixelContext : DbContext
    {
	    private readonly string _connectionString;
	    private readonly IConfiguration _configuration;

        public PixelContext()
        {

        }

        public PixelContext(string connectionString)
        {
	        _connectionString = connectionString;
        }

        public PixelContext(DbContextOptions<PixelContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Pixel> Pixels { get; set; }
       // public virtual DbSet<PixelColor> PixelColors { get; set; }
        public virtual DbSet<PixelGroup> PixelGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //TODO: Перед запуском в прод изменить получение строки 
                //optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
                optionsBuilder.UseNpgsql(
	                "Host = localhost; Port = 5432; Username = postgres; Password = DarkSpore1; Database = Pixels");

            }

            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());

        }
    }
}
