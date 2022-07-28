using EChallanSystem.Models;

namespace EChallanSystem.Repository.Interfaces
{
    public interface IChallanRepository
    {
        Task<List<Challan>> GetChallanByVehicleId(int id);
        Task<List<Challan>> GetChallanByWardenId(int id);
        Task<Challan> GetChallanById(int id);
        Task<List<Challan>> CreateChallan(Challan newChallan);

        bool PayChallan(int id);
        bool ChallanExists(int id);
        bool Save();
    }
}
