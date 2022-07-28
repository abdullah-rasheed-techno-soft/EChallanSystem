using EChallanSystem.Models;
using EChallanSystem.Repository.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerRepository _managerRepository;
        public ManagerController(IManagerRepository managerRepostiory)
        {
            _managerRepository = managerRepostiory;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Manager>> GetManager(int id)
        {
            try
            {
                Manager manager = await _managerRepository.GetManager(id);
                if (manager is null)
                {
                    return NotFound("Manager not found");
                }
                return Ok(manager);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<Manager>>> GetManagers()
        {
            try
            {
                List<Manager> manager = await _managerRepository.GetManagers();
                if (manager is null)
                {
                    return NotFound("Manager not found");
                }
                return Ok(manager);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<Manager>>> AddManager(Manager newManager)
        {
            try
            {
                List<Manager> manager = await _managerRepository.AddManager(newManager);
                if (!ModelState.IsValid) return BadRequest();
                return Created("Successfully added ", manager);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        //[HttpGet]
        //public async Task<ActionResult<List<Manager>>> GetManagers()
        //{
        //    return Ok(await _context.Managers.Include(c=>c.User).ToListAsync());
        //}
        //[HttpGet]
        //public async Task<ActionResult<List<ApplicationUser>>> GetManagersBy()
        //{
        //    return Ok(await _context.ApplicationUsers.Where(c=>c.Role=="Manager").ToListAsync());
        //}
        //[HttpGet]
        //public async Task<ActionResult<TrafficWarden>> GetTrafficWardens()
        //{
        //    return Ok(await _context.TrafficWardens.Include(c => c.User).ToListAsync());
        //}
        //[HttpGet]
        //public async Task<ActionResult<Citizen>> GetCitizens()
        //{
        //    return Ok(await _context.Citizens.Include(c => c.User).ToListAsync());
        //}

    }
}
 