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
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITrafficWardenRepository _trafficWardenRepository;
        private readonly IMapper _mapper;
        public ChallanController(IChallanRepository challanRepostiory,IMapper mapper, IVehicleRepository vehicleRepository, ITrafficWardenRepository trafficWardenRepository)
        {
            _challanRepository = challanRepostiory;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _trafficWardenRepository = trafficWardenRepository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChallanDTO>> GetChallan(int id)
        {
            Challan challan = await _challanRepository.GetChallan(id);
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
        public async Task<ActionResult<List<ChallanDTO>>> CreateChallan(int vehicleId,int trafficWardenId ,[FromBody]ChallanDTO newChallan)
        {
            if (newChallan == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = _vehicleRepository.VehicleExists(vehicleId);
            var trafficWarden = _trafficWardenRepository.TrafficWardenExists(trafficWardenId);
            if (!vehicle)
                return NotFound("Vehicle doesnt exist");
            if(!trafficWarden)
                return NotFound("Traffic warden doesnt exist");

            var challanMap = _mapper.Map<Challan>(newChallan);
            challanMap.Vehicle = await _vehicleRepository.GetVehicle(vehicleId);
            challanMap.TrafficWarden = await _trafficWardenRepository.GetTrafficWarden(trafficWardenId);
            await _challanRepository.CreateChallan(challanMap);
            return Ok("Challan successfully created");
        }
        [HttpPut("{id}")]
        public IActionResult PayChallan(int id)
        {
    
            
            if (!_challanRepository.ChallanExists(id))
            {
                return NotFound("This Challan doesnt exist");
            }
          
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
          
            if (!_challanRepository.PayChallan(id))
            {
                return NotFound("Challan has already been paid");
            }
            return Ok("Challan Paid Successfully");
        }
    }
}
