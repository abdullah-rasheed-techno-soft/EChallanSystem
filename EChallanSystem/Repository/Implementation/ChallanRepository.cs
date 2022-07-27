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
            Challan challan = _context.Challans.FirstOrDefault(m => m.Id == id);

            return challan;
        }

        public async Task<List<Challan>> GetChallans()
        {
            return _context.Challans.ToList();
        }
        public bool PayChallan(int id)
        {
            Challan UpdateChallan = _context.Challans.FirstOrDefault(m=>m.Id==id);
            if (UpdateChallan.IsPaid==false)
            {
                UpdateChallan.IsPaid = true;
                _context.Update(UpdateChallan);
                return Save();

            }
            return false;
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

