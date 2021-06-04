using Common;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace Chat.Context
{
    public class ChatDbContext : DbContext
    {
        private readonly string _connectionString;

        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
            var npgsqlOptions = options.FindExtension<NpgsqlOptionsExtension>();

            if (npgsqlOptions != null)
            {
                _connectionString = npgsqlOptions.ConnectionString;
            }
        }

        public ChatDbContext()
        {
        }

        public ChatDbContext(string connectionString)
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
    }
}
