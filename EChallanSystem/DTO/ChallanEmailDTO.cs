using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class ChallanEmailDTO
    {
        [Key]
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }

        public int CitizenId { get; set; }
    }
}
