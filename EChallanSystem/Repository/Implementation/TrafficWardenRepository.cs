using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Repository.Implementation
{
    public class TrafficWardenRepository:ITrafficWardenRepository
    {
        private readonly AppDbContext _context;
        public TrafficWardenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<TrafficWarden>> AddTrafficWarden(TrafficWarden newTrafficWarden)
        {
            _context.TrafficWardens.Add(newTrafficWarden);
            _context.SaveChanges();
            return _context.TrafficWardens.ToList();
        }

        public async Task<TrafficWarden> GetTrafficWarden(int id)
        {
            TrafficWarden trafficWarden = _context.TrafficWardens.Include(c => c.User).Include(d=>d.Challans).FirstOrDefault(m => m.Id == id);

            return trafficWarden;
        }

        public async Task<List<TrafficWarden>> GetTrafficWardens()
        {
            return _context.TrafficWardens.Include(c => c.User).Include(d => d.Challans).ToList();
        }
        public bool TrafficWardenExists(int id)
        {
            return _context.Citizens.Any(c => c.Id == id);
        }
    }
}
