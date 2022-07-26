using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class ChallanEmailDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Message { get; set; }

        public int CitizenId { get; set; }
    }
}
