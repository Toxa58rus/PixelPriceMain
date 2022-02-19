using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Reflection;
using ApiGateways.Context.Models;

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

        public ApiGatewaysDbContext()
        {
	     
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
	        modelBuilder.Entity<Logs>().HasNoKey();
	        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetEntryAssembly());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Logs> Logs { set; get; }
    }
}
