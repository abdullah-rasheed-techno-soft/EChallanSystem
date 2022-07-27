using EChallanSystem.Models;
using EChallanSystem.Repository.IServices;
using Microsoft.EntityFrameworkCore;
namespace EChallanSystem.Repository.Implementation
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly AppDbContext _context;
        public ManagerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Manager>> AddManager(Manager newManager)
        {
            _context.Managers.Add(newManager);
            _context.SaveChanges();
            return _context.Managers.ToList();
        }

        public async Task<Manager> GetManager(int id)
        {
            Manager manager=_context.Managers.Include(c=>c.User).FirstOrDefault(m => m.Id == id);
           
            return manager;
        }

        public async Task<List<Manager>> GetManagers()
        {
            return _context.Managers.Include(c => c.User).ToList();
        }
    }
}
