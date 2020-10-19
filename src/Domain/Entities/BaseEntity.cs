using System;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public DateTime DateOfInsert { get; set; } = DateTime.Now;
        public DateTime? DateOfUpdate { get; set; }
        public DateTime? DateOfDelete { get; set; }
        public bool IsDeleted { get; set; }
    }
}
