using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Role { get; set; }
    }
    //public enum Roles
    //{
    //    Manager,
    //    TrafficWarden,
    //    Citizen
    //}
}
