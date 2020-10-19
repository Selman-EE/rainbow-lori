using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Surname).HasMaxLength(200).IsRequired();
            builder.Property(t => t.Password).HasMaxLength(32).IsRequired();
            builder.Property(t => t.EmailAddress).HasMaxLength(256).IsRequired();
            builder.Property(t => t.Username).HasMaxLength(50).IsRequired();
        }
    }
}
