using Common;
using Common.Models.Pixels;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace Pixel.Context
{
    public class PixelDbContext : DbContext
    {
        private readonly string _connectionString;

        public PixelDbContext(DbContextOptions<PixelDbContext> options) : base(options)
        {
            var npgsqlOptions = options.FindExtension<NpgsqlOptionsExtension>();

            if (npgsqlOptions != null)
            {
                _connectionString = npgsqlOptions.ConnectionString;
            }
        }

        public PixelDbContext()
        {
        }

        public PixelDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    optionsBuilder.UseNpgsql(SettingsConfigHelper.AppSetting("ConnectionStrings", "Default"));
                }
                else
                {
                    optionsBuilder.UseNpgsql(_connectionString);
                }
            }
        }

        public DbSet<Pixels> Pixels { get; set; }
        public DbSet<PixelGroup> PixelGroup { get; set; }
        public DbSet<PixelColor> PixelColor { get; set; }
    }
}
