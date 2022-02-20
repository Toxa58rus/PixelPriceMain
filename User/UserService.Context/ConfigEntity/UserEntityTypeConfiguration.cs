using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Context.Models;

namespace UserService.Context.ConfigEntity
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
