using EChallanSystem.Models;

namespace EChallanSystem.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetVehiclesByCitizenId(int id);
        Task<Vehicle> GetVehicle(int id);
        bool VehicleExists(int id);
        Task<List<Vehicle>> AddVehicle(Vehicle newVehicle);
    }
}
