using System;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ErrorWordConfiguration : IEntityTypeConfiguration<ErrorWordEntity>
    {
        public ErrorWordConfiguration()
        { }

        public void Configure(EntityTypeBuilder<ErrorWordEntity> builder)
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
                .HasConstraintName("FK_ErrorWord_DatasetItem");

            builder.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ErrorWord_User");
        }
    }
}
