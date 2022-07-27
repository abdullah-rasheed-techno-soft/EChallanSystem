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
    public class TrafficWardenController : ControllerBase
    {
        private readonly ITrafficWardenRepository _trafficWardenRepository;
        private readonly IMapper _mapper;
        public TrafficWardenController(ITrafficWardenRepository trafficWardenRepostiory,IMapper mapper)
        {
            _trafficWardenRepository = trafficWardenRepostiory;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TrafficWardenDTO>> GetTrafficWarden(int id)
        {
            TrafficWarden trafficWarden = await _trafficWardenRepository.GetTrafficWarden(id);
            var trafficWardenDto = _mapper.Map<TrafficWardenDTO>(trafficWarden);
            if (trafficWarden is null)
            {
                return NotFound("Traffic Warden not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(trafficWardenDto);
        }
        [HttpGet]
        public async Task<ActionResult<List<TrafficWardenDTO>>> GetTrafficWardens()
        {
            List<TrafficWarden> trafficWarden = await _trafficWardenRepository.GetTrafficWardens();
            var trafficWardenDto = _mapper.Map<List<TrafficWardenDTO>>(trafficWarden);
            if (trafficWarden is null)
            {
                return NotFound("Traffic Warden not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(trafficWardenDto);
        }
        [HttpPost]
        public async Task<ActionResult<List<TrafficWardenDTO>>> AddTrafficWarden([FromBody]TrafficWardenDTO newTrafficWarden)
        {
            if (newTrafficWarden == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trafficWardenDto = _mapper.Map<TrafficWarden>(newTrafficWarden);
            await _trafficWardenRepository.AddTrafficWarden(trafficWardenDto);
            return Ok("Successfully created");

        }
    }
}
