using Common.Models.User;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;

namespace ApiGateways.Context
{
    public class ApiGatewaysDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApiGatewaysDbContext(DbContextOptions<ApiGatewaysDbContext> options) : base(options)
        {
            var npgsqlOptions = options.FindExtension<NpgsqlOptionsExtension>();

            if (npgsqlOptions != null)
            {
                _connectionString = npgsqlOptions.ConnectionString;
            }
        }

        public ApiGatewaysDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
            optionsBuilder.LogTo(Console.WriteLine);
        }

        public DbSet<Users> Users { get; set; }
    }
}
