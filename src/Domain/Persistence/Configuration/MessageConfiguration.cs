using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Persistence.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.SenderUserId).IsRequired();
            builder.Property(t => t.SenderName).IsRequired();
            builder.Property(t => t.SenderUsername).IsRequired();
            builder.Property(t => t.SenderEmailAddress).IsRequired();
            builder.Property(t => t.ReceiverUserId).IsRequired();
            builder.Property(t => t.ReceiverName).IsRequired();
            builder.Property(t => t.ReceiverUsername).IsRequired();
            builder.Property(t => t.ReceiverEmailAddress).IsRequired();
            builder.Property(t => t.Text).HasMaxLength(5000).IsRequired();
        }
    }
}
