using ApiGateways.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiGateways.Context.ConfigEntity
{
    public class LogsEntityTypeConfiguration : IEntityTypeConfiguration<Logs>
    {
        public void Configure(EntityTypeBuilder<Logs> builder)
        {
	        builder.HasNoKey();

            builder.ToTable("Logs");

            builder.Property(x => x.Application);

            builder.Property(x => x.Callsite);
            
            builder.Property(x => x.Exception);
            
            builder.Property(x => x.Level);
            
            builder.Property(x => x.Logged);
            
            builder.Property(x => x.Logger);
            
            builder.Property(x => x.Message);

        }
    }
}
