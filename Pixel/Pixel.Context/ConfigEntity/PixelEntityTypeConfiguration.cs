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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
	            .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId).IsRequired();

        }
    }
}
