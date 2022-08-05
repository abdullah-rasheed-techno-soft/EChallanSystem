using System.ComponentModel.DataAnnotations;

namespace EChallanSystem.Models
{
    public class Citizen
    {
        [Key]
        public int Id { get; set; }
       
        public  ApplicationUser? User { get; set; }


    }
}
