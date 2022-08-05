using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class ApplicationUserDTO
    {

        [Required]
        public string? UserName { get; set; }
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter valid email")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
