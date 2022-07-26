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
        public async Task<List<Vehicle>> AddVehicle(int citizenId,Vehicle newVehicle)
        {
            var citizen=_context.Citizens.Where(a=>a.Id==citizenId).FirstOrDefault();

            
            _context.Vehicles.Add(newVehicle);
            _context.SaveChanges();
            return _context.Vehicles.ToList();
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            var vehicle = _context.Vehicles.Include(c => c.Citizen).Include(d=>d.Challans).FirstOrDefault(m => m.Id == id);

            return vehicle;
        }

        public async Task<List<Vehicle>> GetVehicles()
        {
            return _context.Vehicles.Include(c => c.Citizen).Include(d => d.Challans).ToList();
        }
    }
}
