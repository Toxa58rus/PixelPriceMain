using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Context.Models;

namespace UserService.Context.ConfigEntity
{
    public class ImageUserEntityTypeConfiguration : IEntityTypeConfiguration<ImageUser>
    {
        public void Configure(EntityTypeBuilder<ImageUser> builder)
        {
	        builder.ToTable("ImageUsers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();
        }
    }
}
