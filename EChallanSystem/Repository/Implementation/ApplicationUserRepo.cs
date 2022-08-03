using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;

namespace EChallanSystem.Repository.Implementation
{
    public class ApplicationUserRepo:IApplicationUserRepo
    {
        private readonly AppDbContext _context;
        public ApplicationUserRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            ApplicationUser user = _context.ApplicationUsers.FirstOrDefault(m => m.Email == email);
            return user;
        }
        
    }
}
