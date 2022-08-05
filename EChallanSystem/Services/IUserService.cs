using EChallanSystem.Models;

namespace EChallanSystem.Services
{
    public interface IUserService
    {
        Task<string> RegisterManagerAsync(RegisterModel model);
    }
}
