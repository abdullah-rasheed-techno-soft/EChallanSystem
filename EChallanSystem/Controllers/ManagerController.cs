using EChallanSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ManagerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Manager>> Get()
        {
            return Ok(await _context.Managers.ToListAsync());
        }
       
    }
}
 