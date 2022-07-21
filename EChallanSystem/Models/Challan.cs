using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.Models
{
    public class Challan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Fine { get; set; }
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }  
        public bool IsPaid { get; set; }= false;
        public int TrafficWardenId { get; set; }
        public TrafficWarden? Warden { get; set; }

    }
}
