using EChallanSystem.Models;

namespace EChallanSystem.Repository.IServices
{
    public interface IManagerRepository
    {
        Task<List<Manager>> GetManagers();
        Task<Manager> GetManager(int id);
        Task<List<Manager>> AddManager(Manager newManager);
    }
}
