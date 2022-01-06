﻿using System;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class LearntWordConfiguration : IEntityTypeConfiguration<LearntWordEntity>
    {
        public LearntWordConfiguration()
        { }

        public void Configure(EntityTypeBuilder<LearntWordEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(k => k.DatasetItemEntityId)
                    .IsRequired();
            builder.Property(p => p.UserId)
                    .IsRequired();

            builder.HasOne(d => d.DatasetItem)
                .WithMany()
                .HasForeignKey(d => d.DatasetItemEntityId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LearntWord_DatasetItem");

            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_LearntWord_User");
        }
    }
}
