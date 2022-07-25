using EChallanSystem.Models;

namespace EChallanSystem.Repository.Interfaces
{
    public interface ITrafficWardenRepository
    {
        Task<List<TrafficWarden>> GetTrafficWardens();
        Task<TrafficWarden> GetTrafficWarden(int id);
        Task<List<TrafficWarden>> AddTrafficWarden(TrafficWarden newTrafficWarden);
    }
}
