using EChallanSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.DTO
{
    public class TrafficWardenDTO
    {
        [Key]
        public int Id { get; set; }

    
        public ApplicationUserDTO? User { get; set; }
    }
}
