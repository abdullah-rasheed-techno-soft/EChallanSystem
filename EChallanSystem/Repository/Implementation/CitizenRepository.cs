using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Repository.Implementation
{
    public class CitizenRepository:ICitizenRepository
    {
        private readonly AppDbContext _context;
        public CitizenRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Citizen>> AddCitizen(Citizen newCitizen)
        {
            _context.Citizens.Add(newCitizen);
            _context.SaveChanges();
            return _context.Citizens.ToList();
        }

        public async Task<Citizen> GetCitizen(int id)
        {
            var citizen = _context.Citizens.Include(c => c.User).Include(c=>c.Vehicle).Include(c=>c.ChallanEmails).FirstOrDefault(m => m.Id == id);

            return citizen;
        }

        public async Task<List<Citizen>> GetCitizens()
        {
            return _context.Citizens.Include(c => c.User).Include(d => d.Vehicle).Include(e => e.ChallanEmails).ToList();
        }
    }
}
