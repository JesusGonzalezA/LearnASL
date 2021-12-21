using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class OptionWordToVideoQuestionConfiguration : IEntityTypeConfiguration<OptionWordToVideoQuestionEntity>
    {
        public OptionWordToVideoQuestionConfiguration()
        {
        }

        public virtual void Configure(EntityTypeBuilder<OptionWordToVideoQuestionEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.WordToGuess)
                    .HasMaxLength(20)
                    .IsRequired();

            builder.Property(p => p.PossibleAnswers[0])
                    .HasColumnName("PossibleAnswer0");
            builder.Property(p => p.PossibleAnswers[1])
                    .HasColumnName("PossibleAnswer1");
            builder.Property(p => p.PossibleAnswers[2])
                    .HasColumnName("PossibleAnswer2");
            builder.Property(p => p.PossibleAnswers[3])
                    .HasColumnName("PossibleAnswer3");

            builder.Property(p => p.IndexOfUserAnswer)
                    .IsRequired();

            builder.Property(p => p.IndexOfCorrectAnswer)
                    .IsRequired();
        }
    }
}
