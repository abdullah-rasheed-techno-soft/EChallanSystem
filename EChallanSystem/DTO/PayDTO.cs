using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class PayDTO
    {
        [Required]
        public bool IsPaid { get; set; } = false;
    }
}
