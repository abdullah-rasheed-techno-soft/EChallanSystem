using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class PayDTO
    {
        [Required]
        [Range(10,500000,ErrorMessage ="Fine Range should be between 10 and 500000")]
        public bool IsPaid { get; set; } = false;
    }
}
