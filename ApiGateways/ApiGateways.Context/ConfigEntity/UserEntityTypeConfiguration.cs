using ApiGateways.Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiGateways.Context.ConfigEntity
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
	        builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

        }
    }
}
