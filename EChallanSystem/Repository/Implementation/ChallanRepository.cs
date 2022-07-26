using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Repository.Implementation
{
    public class ChallanRepository:IChallanRepository
    {
        private readonly AppDbContext _context;
        public ChallanRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Challan>> CreateChallan(Challan newChallan)
        {
            _context.Challans.Add(newChallan);
            _context.SaveChanges();
            return _context.Challans.ToList();
        }

        public async Task<Challan> GetChallan(int id)
        {
            var challan = _context.Challans.Include(c => c.Vehicle).ThenInclude(d=>d.Citizen).Include(c => c.TrafficWarden).ThenInclude(d=>d.User).FirstOrDefault(m => m.Id == id);

            return challan;
        }

        public async Task<List<Challan>> GetChallans()
        {
            return _context.Challans.Include(c => c.Vehicle).ThenInclude(d => d.Citizen).Include(c => c.TrafficWarden).ThenInclude(d => d.User).ToList();
        }
        public bool PayChallan(int id,Challan challan)
        {
            _context.Update(challan);
            return Save();
        }
        public bool Save()
        {
            var saved=_context.SaveChanges();
            return saved > 0 ? true:false;
        }
        public bool ChallanExists(int id)
        {
            return _context.Challans.Any(c => c.Id == id);
        }
    }
}

