using System;
using System.Reflection;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using PixelService.Context.ConfigEntity;

#nullable disable

namespace PixelService.Context.Models
{
    public partial class PixelContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PixelContext()
        {

        }

        public PixelContext(DbContextOptions<PixelContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Pixel> Pixels { get; set; }
        public virtual DbSet<PixelColor> PixelColors { get; set; }
        public virtual DbSet<PixelGroup> PixelGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //TODO: Перед запуском в прод изменить получение строки 
                optionsBuilder.UseNpgsql("Host=89.108.65.74;Port=5432;Username=postgres;Password=DarkSpore1;Database=Pixels");
            }

            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());
          

           

           
        }
    }
}
