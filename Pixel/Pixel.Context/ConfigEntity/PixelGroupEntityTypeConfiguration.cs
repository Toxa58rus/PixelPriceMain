﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PixelService.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelService.Context.ConfigEntity
{
    public class PixelGroupEntityTypeConfiguration : IEntityTypeConfiguration<PixelGroup>
    {
        public void Configure(EntityTypeBuilder<PixelGroup> builder)
        {

            builder.HasKey(x=>x.Id);

            builder.ToTable("PixelGroups");

            builder.HasIndex(e => e.Id)
	            .IsUnique();

            builder.Property(e => e.Id)
	            .ValueGeneratedOnAdd();
        }
    }
}
