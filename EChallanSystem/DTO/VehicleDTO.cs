using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class VehicleDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Vehicle name is required")]
        [MaxLength(30,ErrorMessage ="Max Length should not exceed 30"), MinLength(2)]
        public string? Name { get; set; }


        public int CitizenId { get; set; }
    
    }
}
