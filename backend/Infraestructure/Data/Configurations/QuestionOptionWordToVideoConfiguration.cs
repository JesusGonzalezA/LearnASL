using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class QuestionOptionWordToVideoConfiguration : BaseQuestionConfiguration<QuestionOptionWordToVideoEntity>
    {
        public QuestionOptionWordToVideoConfiguration()
        {}

        public override void Configure(EntityTypeBuilder<QuestionOptionWordToVideoEntity> builder)
        {
            base.Configure(builder);

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
            builder.Property(p => p.UserAnswer);
            builder.Property(p => p.CorrectAnswer)
                    .IsRequired();
        }
    }
}
