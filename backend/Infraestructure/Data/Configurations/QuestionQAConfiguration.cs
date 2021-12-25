using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class QuestionQaConfiguration : BaseQuestionConfiguration<QuestionQAEntity>
    {
        public QuestionQaConfiguration()
        { }

        public override void Configure(EntityTypeBuilder<QuestionQAEntity> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.WordToGuess)
                    .IsRequired();
            builder.Property(p => p.VideoUser);
            builder.Property(p => p.IsCorrect)
                    .IsRequired();
        }
    }
}
