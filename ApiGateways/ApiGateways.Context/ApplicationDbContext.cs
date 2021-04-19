using ApiGateways.Common.Models.User;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace ApiGateways.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            var npgsqlOptions = options.FindExtension<NpgsqlOptionsExtension>();

            if (npgsqlOptions != null)
            {
                _connectionString = npgsqlOptions.ConnectionString;
            }
        }

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        public DbSet<Users> Users { get; set; }
    }
}
