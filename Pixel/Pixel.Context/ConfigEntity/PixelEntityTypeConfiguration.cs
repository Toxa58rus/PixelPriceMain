using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelService.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelService.Context.ConfigEntity
{
    public sealed class PixelEntityTypeConfiguration : IEntityTypeConfiguration<Pixel>
    {
        public void Configure(EntityTypeBuilder<Pixel> builder)
        {
            builder.ToTable("Pixels");

           
            
            builder.HasIndex(e => e.GroupId, "PixelIndexGroupId")
	            .IsUnique(false);

            builder.HasIndex(e => e.Id, "PixelIndexId")
                .IsUnique();

            builder.HasIndex(e => e.UserId, "PixelIndexUserId")
                .IsUnique(false);

            builder.Property(e => e.GroupId).IsRequired();

            builder.Property(e => e.UserId).IsRequired();

        }
    }
}
