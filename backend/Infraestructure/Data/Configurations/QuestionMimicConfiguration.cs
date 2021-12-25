using Core.Entities.Tests;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public class QuestionMimicConfiguration : BaseQuestionConfiguration<QuestionMimicEntity>
    {
        public QuestionMimicConfiguration()
        {}

        public override void Configure(EntityTypeBuilder<QuestionMimicEntity> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.WordToGuess)
                    .IsRequired();
            builder.Property(p => p.VideoHelp)
                    .IsRequired();
            builder.Property(p => p.VideoUser)
                    .IsRequired();
            builder.Property(p => p.IsCorrect)
                    .IsRequired();
        }
    }
}
