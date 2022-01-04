using System.Collections.Generic;
using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public abstract class BaseQuestionConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseQuestionEntity
    {
        public BaseQuestionConfiguration()
        { }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.TestId)
                    .IsRequired();

            builder.HasOne(d => d.Test)
                .WithMany()
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Question_Test_" + GetType().Name);
        }
    }
}
