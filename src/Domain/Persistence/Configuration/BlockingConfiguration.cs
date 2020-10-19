using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Domain.Persistence.Configuration
{
    public class BlockingConfiguration : IEntityTypeConfiguration<Blocking>
    {
        public void Configure(EntityTypeBuilder<Blocking> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.BlockedUserId).IsRequired();
            builder.Property(t => t.ObstructionistUserId).IsRequired();
        }
    }
}
