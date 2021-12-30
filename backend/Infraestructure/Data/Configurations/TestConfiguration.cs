using System;
using Core.Entities.Tests;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public TestConfiguration()
        {}

        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Difficulty)
                    .HasConversion(
                        x => x.ToString(),
                        x => (Difficulty)Enum.Parse(typeof(Difficulty), x)
                    );
            builder.Property(p => p.TestType)
                    .HasConversion(
                        x => x.ToString(),
                        x => (TestType)Enum.Parse(typeof(TestType), x)
                    );
            builder.Property(p => p.UserId)
                    .IsRequired();

            builder.HasOne(d => d.User)
                .WithMany(d => d.Tests)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Test_User");
        }
    }
}
