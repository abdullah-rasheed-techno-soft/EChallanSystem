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
        public async Task<ActionResult<ChallanDTO>> GetChallanBySpecificId(int id)
        {
            Challan challan = await _challanRepository.GetChallanBySpecificId(id);
            var challanDto = _mapper.Map<ChallanDTO>(challan);
            if (challan is null)
            {
                return NotFound("Challan not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(challanDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ChallanDTO>>> GetChallanByVehicleId(int id)
        {
            var vehicle =_vehicleRepository.VehicleExists(id);
            if (!vehicle)
            {
                return NotFound("The vehicle does not exist");
            }
            List<Challan> challan = await _challanRepository.GetChallanByVehicleId(id);
            var challanDto = _mapper.Map<List<ChallanDTO>>(challan);
            if (!challan.Any())
            {
                return NotFound("The vehicle does not have any challans");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(challanDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ChallanDTO>>> GetChallanByWardenId(int id)
        {
            var warden = _trafficWardenRepository.TrafficWardenExists(id);
            if (!warden)
            {
                return NotFound("The traffic warden does not exist");
            }
            List<Challan> challan = await _challanRepository.GetChallanByWardenId(id);
            var challanDto = _mapper.Map<List<ChallanDTO>>(challan);
            if (!challan.Any())
            {
                return NotFound("This traffic warden does not have any challans");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(challanDto);
        }
        [HttpPost]
        public async Task<ActionResult<List<ChallanDTO>>> CreateChallan([FromBody]ChallanDTO newChallan)
        {
            if (newChallan == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = _vehicleRepository.VehicleExists(newChallan.VehicleId);
            var trafficWarden = _trafficWardenRepository.TrafficWardenExists(newChallan.TrafficWardenId);
            if (!vehicle)
                return NotFound("Vehicle doesnt exist");
            if(!trafficWarden)
                return NotFound("Traffic warden doesnt exist");

            var challanMap = _mapper.Map<Challan>(newChallan);
            challanMap.Vehicle = await _vehicleRepository.GetVehicle(newChallan.VehicleId);
            challanMap.TrafficWarden = await _trafficWardenRepository.GetTrafficWarden(newChallan.TrafficWardenId);
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
