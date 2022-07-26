using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class ChallanEmailDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Subject is required")]
        public string? Subject { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string? Message { get; set; }

        public int CitizenId { get; set; }
    }
}
