using System.Collections.Generic;
using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class QuestionOptionVideoToWordConfiguration : IEntityTypeConfiguration<QuestionOptionVideoToWordEntity>
    {
        public QuestionOptionVideoToWordConfiguration()
        {}

        public virtual void Configure(EntityTypeBuilder<QuestionOptionVideoToWordEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.VideoToGuess)
                    .HasMaxLength(20)
                    .IsRequired();
            builder.Property(p => p.PossibleAnswer0)
                    .HasMaxLength(20)
                    .IsRequired();
            builder.Property(p => p.PossibleAnswer1)
                    .HasMaxLength(20)
                    .IsRequired();
            builder.Property(p => p.PossibleAnswer2)
                    .HasMaxLength(20)
                    .IsRequired();
            builder.Property(p => p.PossibleAnswer3)
                    .HasMaxLength(20)
                    .IsRequired();
            builder.Property(p => p.UserAnswer)
                    .HasDefaultValue(null)
                    .IsRequired();
            builder.Property(p => p.CorrectAnswer)
                    .IsRequired();
            builder.Property(p => p.TestId)
                    .IsRequired();
            builder.HasOne(d => d.Test)
                .WithMany(p => (ICollection<QuestionOptionVideoToWordEntity>)p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_QuestionOptionVideoToWord_Test");
        }
    }
}
