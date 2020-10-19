using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Blocking")]
    public class Blocking : BaseEntity
    {
        public int Id { get; set; }
        public int BlockedUserId { get; set; }
        public int ObstructionistUserId { get; set; }
        public bool Status { get; set; }
    }
}
