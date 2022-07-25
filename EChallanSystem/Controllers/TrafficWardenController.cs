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
        public TrafficWardenController(ITrafficWardenRepository trafficWardenRepostiory)
        {
            _trafficWardenRepository = trafficWardenRepostiory;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TrafficWarden>> GetTrafficWarden(int id)
        {
            var trafficWarden = await _trafficWardenRepository.GetTrafficWarden(id);
            if (trafficWarden is null)
            {
                return NotFound("TrafficWardens not found");
            }
            return Ok(trafficWarden);
        }
        [HttpGet]
        public async Task<ActionResult<List<TrafficWarden>>> GetTrafficWardens()
        {
            var trafficWarden = await _trafficWardenRepository.GetTrafficWardens();
            if (trafficWarden is null)
            {
                return NotFound("TrafficWarden not found");
            }
            return Ok(trafficWarden);
        }
        [HttpPost]
        public async Task<ActionResult<List<TrafficWarden>>> AddTrafficWarden(TrafficWarden newTrafficWarden)
        {
            var trafficWarden = await _trafficWardenRepository.AddTrafficWarden(newTrafficWarden);
            return Ok(trafficWarden);

        }
    }
}
