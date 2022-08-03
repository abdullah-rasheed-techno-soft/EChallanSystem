using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class LoginDTO
    {

     
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please Enter valid email")]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
