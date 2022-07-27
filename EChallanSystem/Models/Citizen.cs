using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.Models
{
    public class Citizen
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<Vehicle>? Vehicle { get; set; }

    }
}
