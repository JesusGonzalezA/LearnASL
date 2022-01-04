using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class QuestionOptionVideoToWordConfiguration : BaseQuestionConfiguration<QuestionOptionVideoToWordEntity>
    {
        public QuestionOptionVideoToWordConfiguration()
        {}

        public override void Configure(EntityTypeBuilder<QuestionOptionVideoToWordEntity> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.VideoToGuess)
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
