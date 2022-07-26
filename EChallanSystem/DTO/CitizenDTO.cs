using EChallanSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class CitizenDTO
    {
        [Key]
        public int Id { get; set; }
       
        public ApplicationUser? User { get; set; }
    }
}
