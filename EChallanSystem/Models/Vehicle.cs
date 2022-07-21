using System.ComponentModel.DataAnnotations;
namespace EChallanSystem.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30), MinLength(2)]
        public string? Name { get; set; }
        public ICollection<Challan>? Challans { get; set; }
        public int CitizenId { get; set; } = 0;
        public Citizen? Citizen { get; set; } = null;

    }
}
