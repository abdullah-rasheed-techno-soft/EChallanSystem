using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.Models
{
    public class ChallanEmail
    {
        [Key]
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }

        public int CitizenId { get; set; }
        public Citizen? Citizen { get; set; } = null;
    }
}
