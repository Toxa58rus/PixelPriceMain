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
    public class PixelColorEntityTypeConfiguration : IEntityTypeConfiguration<PixelColor>
    {
        public void Configure(EntityTypeBuilder<PixelColor> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired();

            builder.HasIndex(e => e.Id, "PixelIndexColorId")
	            .IsUnique();

            builder.ToTable("PixelColor");
           
        }
    }
}
