using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public UserConfiguration()
        {}

        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            builder.Property(p => p.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            builder.Property(p => p.TokenEmailConfirmation)
                    .IsUnicode(false);
            builder.Property(p => p.TokenPasswordRecovery)
                    .IsUnicode(false);
            builder.Property(p => p.ConfirmedEmail)
                    .IsRequired();
        }
    }
}
