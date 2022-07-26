using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ChallanController : ControllerBase
    {
        private readonly IChallanRepository _challanRepository;
        private readonly IMapper _mapper;
        public ChallanController(IChallanRepository challanRepostiory,IMapper mapper)
        {
            _challanRepository = challanRepostiory;
              _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Challan>> GetChallan(int id)
        {
            var challan = await _challanRepository.GetChallan(id);
            if (challan is null)
            {
                return NotFound("Challans not found");
            }
            return Ok(challan);
        }
        [HttpGet]
        public async Task<ActionResult<List<Challan>>> GetChallans()
        {
            var challan = await _challanRepository.GetChallans();
            if (challan is null)
            {
                return NotFound("Challan not found");
            }
            return Ok(challan);
        }
        [HttpPost]
        public async Task<ActionResult<List<Challan>>> CreateChallan(Challan newChallan)
        {
            var challan = await _challanRepository.CreateChallan(newChallan);
            return Ok(challan);

        }
        [HttpPut]
        public IActionResult PayChallan(int id,[FromBody] ChallanDTO challan)
        {
            if (challan == null)
                return BadRequest(ModelState);
            
            if (!_challanRepository.ChallanExists(id))
            {
                return NotFound();
            }
          
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var payChallan = _mapper.Map<Challan>(challan);
            if (_challanRepository.PayChallan(id,payChallan))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
