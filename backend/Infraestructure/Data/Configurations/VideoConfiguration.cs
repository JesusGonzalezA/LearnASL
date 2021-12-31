﻿using System;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class VideoConfiguration : IEntityTypeConfiguration<VideoEntity>
    {
        public VideoConfiguration()
        { }

        public void Configure(EntityTypeBuilder<VideoEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(k => k.Index)
                    .IsRequired();
            builder.Property(p => p.VideoFilename)
                    .IsRequired()
                    .HasMaxLength(20);
            builder.Property(p => p.Word)
                    .IsRequired()
                    .HasMaxLength(20);
            builder.Property(p => p.Difficulty)
                    .HasConversion(
                        x => x.ToString(),
                        x => (Difficulty)Enum.Parse(typeof(Difficulty), x)
                    );
        }
    }
}
