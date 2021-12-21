using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class QuestionOptionWordToVideoConfiguration : IEntityTypeConfiguration<QuestionOptionWordToVideoEntity>
    {
        public QuestionOptionWordToVideoConfiguration()
        {}

        public virtual void Configure(EntityTypeBuilder<QuestionOptionWordToVideoEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.WordToGuess)
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
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Question_Test");
        }
    }
}
