using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Repository.Implementation
{
    public class ChallanEmailRepository : IChallanEmailRepository
    {
        private readonly AppDbContext _context;
        public ChallanEmailRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ChallanEmail>> SendChallanEmail(ChallanEmail newChallanEmail)
        {
            _context.ChallanEmails.Add(newChallanEmail);
            _context.SaveChanges();
            return _context.ChallanEmails.ToList();
        }



        public async Task<List<ChallanEmail>> GetChallanEmails()
        {
            return _context.ChallanEmails.ToList();
        }
    }
}
