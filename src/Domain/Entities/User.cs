using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }

    }
}
