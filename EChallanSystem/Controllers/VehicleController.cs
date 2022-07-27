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
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICitizenRepository _citizenRepository;
        private readonly IMapper _mapper;
        public VehicleController(IVehicleRepository vehicleRepostiory, ICitizenRepository citizenRepository,IMapper mapper)
        {
            _vehicleRepository = vehicleRepostiory;
            _citizenRepository = citizenRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDTO>> GetVehicleBySpecificId(int id)
        {
            Vehicle vehicle = await _vehicleRepository.GetVehicle(id);
            var vehicleDto = _mapper.Map<VehicleDTO>(vehicle);
            if (vehicle is null)
            {
                return NotFound("Vehicle not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(vehicleDto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<VehicleDTO>>> GetVehiclesByCitizenId(int id)
        {
            var citizen = _citizenRepository.CitizenExists(id);
            if (!citizen)
            {
                return NotFound("The Citizen does not exist");
            }
            List<Vehicle> vehicle = await _vehicleRepository.GetVehiclesByCitizenId(id);
            var vehicleDto = _mapper.Map<List<VehicleDTO>>(vehicle);
            if (vehicle is null)
            {
                return NotFound("This Citizen does not have any Vehicles");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(vehicleDto);
        }
        [HttpPost]
        public async Task<ActionResult<List<VehicleDTO>>> AddVehicle(int citizenId, [FromBody]VehicleDTO newVehicle)
        {
            if (newVehicle == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var citizen = _citizenRepository.CitizenExists(citizenId);
            if (!citizen)
                return NotFound("Citizen doesnt exist");
            var vehicleMap=_mapper.Map<Vehicle>(newVehicle);
            vehicleMap.Citizen =await _citizenRepository.GetCitizen(citizenId);

            await _vehicleRepository.AddVehicle(vehicleMap);
            return Ok("Vehicle successfully added");
        }
    }
}
