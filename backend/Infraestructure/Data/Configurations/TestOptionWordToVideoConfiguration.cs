using System;
using System.Collections.Generic;
using Core.Entities.Tests;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class TestOptionWordToVideoConfiguration : IEntityTypeConfiguration<TestOptionWordToVideoEntity>
    {
        public TestOptionWordToVideoConfiguration()
        { }

        public virtual void Configure(EntityTypeBuilder<TestOptionWordToVideoEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Difficulty)
                    .HasConversion(
                        x => x.ToString(),
                        x => (Difficulty)Enum.Parse(typeof(Difficulty), x)
                    );
            builder.Property(p => p.NumberOfQuestions)
                    .IsRequired();
            builder.Property(p => p.IsErrorTest)
                    .IsRequired();
            builder.Property(p => p.UserId)
                    .IsRequired();
            builder.HasOne(d => d.User)
                .WithMany(p => p.TestsOptionWordToVideo)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TestOptionWordToVideo_User");
        }
    }
}

