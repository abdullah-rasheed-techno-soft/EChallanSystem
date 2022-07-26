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
        public async Task<ActionResult<ChallanDTO>> GetChallan(int id)
        {
            var challan = await _challanRepository.GetChallan(id);
            var challanDto = _mapper.Map<ChallanDTO>(challan);
            if (challan is null)
            {
                return NotFound("Challan not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(challanDto);
        }
        [HttpGet]
        public async Task<ActionResult<List<ChallanDTO>>> GetChallans()
        {
            var challan = await _challanRepository.GetChallans();
            var challanDto = _mapper.Map<List<ChallanDTO>>(challan);
            if (challan is null)
            {
                return NotFound("Challans not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(challanDto);
        }
        [HttpPost]
        public async Task<ActionResult<List<Challan>>> CreateChallan(Challan newChallan)
        {
            var challan = await _challanRepository.CreateChallan(newChallan);
            return Ok(challan);

        }
        [HttpPut("{id}")]
        public IActionResult PayChallan(int id,[FromBody] PayDTO challan)
        {
            if (challan == null)
                return BadRequest(ModelState);
            
            if (!_challanRepository.ChallanExists(id))
            {
                return NotFound("This Challan doesnt exist");
            }
          
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var payChallan = _mapper.Map<Challan>(challan);
            if (!_challanRepository.PayChallan(id,payChallan))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Challan Paid Successfully");
        }
    }
}
