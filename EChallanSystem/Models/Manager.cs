using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.Models
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
