using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Message")]
    public class Message : BaseEntity
    {
        public int Id { get; set; }
        public Guid ChatId { get; set; }
        public int SenderUserId { get; set; }
        public string SenderName { get; set; }
        public string SenderUsername { get; set; }
        public string SenderEmailAddress { get; set; }
        public int ReceiverUserId { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverUsername { get; set; }
        public string ReceiverEmailAddress { get; set; }
        public string Text { get; set; }
    }
}
