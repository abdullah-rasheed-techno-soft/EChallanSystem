using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using EChallanSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ChallanController : ControllerBase
    {
        private readonly ILogger<ChallanController> _logger;
        private readonly IChallanRepository _challanRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICitizenRepository _citizenRepository;
        private readonly ITrafficWardenRepository _trafficWardenRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        public ChallanController(IChallanRepository challanRepostiory,IMapper mapper, IVehicleRepository vehicleRepository, ITrafficWardenRepository trafficWardenRepository, IEmailService emailService, ICitizenRepository citizenRepository, ILogger<ChallanController> logger)
        {
            _challanRepository = challanRepostiory;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
            _trafficWardenRepository = trafficWardenRepository;
            _emailService = emailService;
            _citizenRepository = citizenRepository;
            _logger = logger;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ChallanDTO>> GetChallanById(int id)
        {
            try
            {
                _logger.LogInformation($"Getting Challan by Id {id} invoked");
                Challan challan = await _challanRepository.GetChallanById(id);
                var challanDto = _mapper.Map<ChallanDTO>(challan);
             
                if (challan is null)
                {
                    _logger.LogError($"Challan with id {id} not found");
                    return NotFound("Challan not found");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError(" Model state not valid");

                    return BadRequest(ModelState);
                }
                _logger.LogInformation($"Challan data with id {id} recevied");

                return Ok(challanDto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Stopped program because of some exception");
                throw new Exception("Exception occured ",ex);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ChallanDTO>>> GetChallanByVehicleId(int id)
        {
            try
            {
                _logger.LogInformation($"Getting Challan by Vehicle Id {id} invoked");

                var vehicle = _vehicleRepository.VehicleExists(id);
                if (!vehicle)
                {
                    _logger.LogError($"Challan with Id {id} not found");
                    return NotFound("The vehicle does not exist");
                }
                List<Challan> challan = await _challanRepository.GetChallanByVehicleId(id);
                var challanDto = _mapper.Map<List<ChallanDTO>>(challan);
                if (!challan.Any())
                {
                    _logger.LogError("The vehicle does not have any challans");

                    return NotFound("The vehicle does not have any challans");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogError(" Model state not valid");
                    return BadRequest(ModelState);

                }
                return Ok(challanDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Stopped program because of some exception");

                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ChallanDTO>>> GetChallanByWardenId(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<ChallanDTO>>> CreateChallan([FromBody]ChallanDTO newChallan)
        {
            try
            {
              
                if (newChallan == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                Vehicle vehicle = await _vehicleRepository.GetVehicle(newChallan.VehicleId);

                var trafficWarden = _trafficWardenRepository.TrafficWardenExists(newChallan.TrafficWardenId);
                if (vehicle is null)
                    return NotFound("Vehicle doesnt exist");
                if (!trafficWarden)
                    return NotFound("Traffic warden doesnt exist");

                var challanMap = _mapper.Map<Challan>(newChallan);
                challanMap.Vehicle = await _vehicleRepository.GetVehicle(newChallan.VehicleId);
                challanMap.TrafficWarden = await _trafficWardenRepository.GetTrafficWarden(newChallan.TrafficWardenId);
                await _challanRepository.CreateChallan(challanMap);
                EmailDTO request = new EmailDTO();
                Citizen citizen = await _citizenRepository.GetCitizen(vehicle.CitizenId);
                request.To = "mamb.technosoftteamleader@gmail.com";
                request.Subject = "Challan ";
                request.Body = $"Your Vehicle {vehicle.Name} has violated the rule. You have to pay {newChallan.Fine} rupees";
                _emailService.SendMail(request);
                return Ok("Challan successfully created and Email sent to its owner");
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpPut("{id}")]
        public IActionResult PayChallan(int id)
        {

            try 
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
               catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
    }
}
