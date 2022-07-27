using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Repository.Implementation
{
    public class VehicleRepository:IVehicleRepository
    {
        private readonly AppDbContext _context;
        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Vehicle>> AddVehicle(Vehicle newVehicle)
        {
      

            
            _context.Vehicles.Add(newVehicle);
            _context.SaveChanges();
            return _context.Vehicles.ToList();
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            Vehicle vehicle = _context.Vehicles.FirstOrDefault(m => m.Id == id);

            return vehicle;
        }

        public async Task<List<Vehicle>> GetVehiclesByCitizenId(int id)
        {
            return _context.Vehicles.Where(b => b.CitizenId == id).ToList();
        }
        public bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(c => c.Id == id);
        }
    }
}
