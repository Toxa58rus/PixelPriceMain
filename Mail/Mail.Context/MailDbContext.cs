using Common;
using Common.Models.Mail;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;

namespace Mail.Context
{
    public class MailDbContext:DbContext
    {
        private readonly string _connectionString;

        public MailDbContext(DbContextOptions<MailDbContext> options) : base(options)
        {
            var npgsqlOptions = options.FindExtension<NpgsqlOptionsExtension>();

            if (npgsqlOptions != null)
            {
                _connectionString = npgsqlOptions.ConnectionString;
            }
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
                    optionsBuilder.UseNpgsql(SettingsConfigHelper.AppSetting("ConnectionStrings", "Default"));
                }
                else
                {
                    optionsBuilder.UseNpgsql(_connectionString);
                }
            }
        }

        public DbSet<MailModel> Mails { get; set; }
      
    }
}
