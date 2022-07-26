using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class ChallanDTO
    {
        [Key]
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int TrafficWardenId { get; set; }
        [Required]
        public double Fine { get; set; }
        public bool IsPaid { get; set; } = false;
    }
}
