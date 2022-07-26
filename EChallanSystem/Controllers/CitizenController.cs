using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizenRepository _citizenRepository;
        public CitizenController(ICitizenRepository citizenRepostiory)
        {
            _citizenRepository = citizenRepostiory;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Citizen>> GetCitizen(int id)
        {
            var citizen = await _citizenRepository.GetCitizen(id);
            if (citizen is null)
            {
                return NotFound("Citizens not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(citizen);
        }
        [HttpGet]
        public async Task<ActionResult<List<Citizen>>> GetCitizens()
        {
            var citizen = await _citizenRepository.GetCitizens();
            if (citizen is null)
            {
                return NotFound("Citizen not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(citizen);
        }
        [HttpPost]
        public async Task<ActionResult<List<Citizen>>> AddCitizen(Citizen newCitizen)
        {
            var citizen = await _citizenRepository.AddCitizen(newCitizen);
            return Ok(citizen);

        }
    }
}
