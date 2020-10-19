using Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Chat")]
    public class Chat
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public ChatType ChatType { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}
