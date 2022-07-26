using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class VehicleDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30), MinLength(2)]
        public string? Name { get; set; }


        public int CitizenId { get; set; }
    
    }
}
