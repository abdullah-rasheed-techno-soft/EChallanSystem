using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class ChallanDTO
    {

        public int VehicleId { get; set; }
    
        [Required]
        public double Fine { get; set; }
       
    }
}
