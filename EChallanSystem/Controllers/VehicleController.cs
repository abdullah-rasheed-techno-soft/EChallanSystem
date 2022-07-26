using AutoMapper;
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
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicle(id);
            if (vehicle is null)
            {
                return NotFound("Vehicle not found");
            }
            return Ok(vehicle);
        }
        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetVehicles()
        {
            var vehicle = await _vehicleRepository.GetVehicles();
            if (vehicle is null)
            {
                return NotFound("Vehicle not found");
            }
            return Ok(vehicle);
        }
        [HttpPost]
        public async Task<ActionResult<List<Vehicle>>> AddVehicle(int citizenId, [FromBody]Vehicle newVehicle)
        {
            var vehicle = await _vehicleRepository.AddVehicle(newVehicle);
            return Ok(vehicle);

        }
    }
}
