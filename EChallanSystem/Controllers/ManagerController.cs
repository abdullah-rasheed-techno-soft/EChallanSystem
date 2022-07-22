using EChallanSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Controllers
{
    [Route("api/[controller]/[action]")] 
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ManagerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Manager>>> GetManagers()
        {
            return Ok(await _context.Managers.Include(c=>c.User).ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<ApplicationUser>>> GetManagersBy()
        {
            return Ok(await _context.ApplicationUsers.Where(c=>c.Role=="Manager").ToListAsync());
        }
        //[HttpGet]  
        //public async Task<ActionResult<TrafficWarden>> GetTrafficWardens()
        //{
        //    return Ok(await _context.TrafficWardens.Include(c => c.UserId).ToListAsync());
        //}
        //[HttpGet]
        //public async Task<ActionResult<Citizen>> GetCitizens()
        //{
        //    return Ok(await _context.Citizens.Include(c => c.UserId).ToListAsync());
        //}

    }
}
 