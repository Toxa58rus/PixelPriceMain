using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;

namespace Mail.Context
{
    public class MailDbContext:DbContext
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        
        
        public MailDbContext(DbContextOptions<MailDbContext> options, IConfiguration configuration) : base(options)
        {
            var npgsqlOptions = options.FindExtension<NpgsqlOptionsExtension>();

            if (npgsqlOptions != null)
            {
                _connectionString = npgsqlOptions.ConnectionString;
            }

            this._configuration = configuration;
        }

        public MailDbContext()
        {
        }

        public MailDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
                }
                else
                {
                    optionsBuilder.UseNpgsql(_connectionString);
                }
            }
        }


        public DbSet<Domain.Model.DB.Mail> Mail { get; set; }
    }
}
