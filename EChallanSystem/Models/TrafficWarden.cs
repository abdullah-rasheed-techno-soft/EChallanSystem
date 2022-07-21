using System.ComponentModel.DataAnnotations;
namespace EChallanSystem.Models
{
    public class TrafficWarden
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; } 
        public ICollection<Challan>? Challans { get; set; }
    }
}
