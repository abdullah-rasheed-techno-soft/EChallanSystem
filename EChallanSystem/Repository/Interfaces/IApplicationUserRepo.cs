using EChallanSystem.Models;

namespace EChallanSystem.Repository.Interfaces
{
    public interface IApplicationUserRepo
    {
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
