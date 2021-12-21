using System;
using Core.Entities.Tests;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Configurations
{
    public abstract class BaseTestConfiguration<T> : IEntityTypeConfiguration<TestEntity<T> > where T : BaseQuestionEntity
    {
        public BaseTestConfiguration()
        {
        }

        public virtual void Configure(EntityTypeBuilder<TestEntity<T>> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.UserGuid)
                    .IsRequired();

            builder.Property(p => p.Difficulty)
                    .HasConversion(
                        x => x.ToString(),
                        x => (Difficulty)Enum.Parse(typeof(Difficulty), x)
                    );

            builder.Property(p => p.NumberOfQuestions)
                    .IsRequired();

            builder.Property(p => p.IsErrorTest)
                    .IsRequired();
        }
    }
}
