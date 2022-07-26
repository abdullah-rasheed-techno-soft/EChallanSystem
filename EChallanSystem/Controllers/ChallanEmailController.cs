using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EChallanEmailSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ChallanEmailController : ControllerBase
    {
        private readonly IChallanEmailRepository _challanEmailRepository;
        private readonly ICitizenRepository _citizenRepository;
        private readonly IMapper _mapper;
        public ChallanEmailController(IChallanEmailRepository challanEmailRepostiory, IMapper mapper, ICitizenRepository citizenRepository)
        {
            _challanEmailRepository = challanEmailRepostiory;
            _mapper = mapper;
            _citizenRepository = citizenRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ChallanEmailDTO>>> GetChallanEmails()
        {
            var challanEmail = await _challanEmailRepository.GetChallanEmails();
            var challanEmailDto = _mapper.Map<List<ChallanEmailDTO>>(challanEmail);
            if (challanEmail is null)
            {
                return NotFound("Emails not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(challanEmailDto);
        }
        [HttpPost]
        public async Task<ActionResult<List<ChallanEmailDTO>>> SendChallanEmail(int citizenId,[FromBody]ChallanEmailDTO newChallanEmail)
        {
            if (newChallanEmail == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var citizen = _citizenRepository.CitizenExists(citizenId);
            if (!citizen)
                return NotFound("Citizen doesnt exist");
            var MailMap = _mapper.Map<ChallanEmail>(newChallanEmail);
            MailMap.Citizen = await _citizenRepository.GetCitizen(citizenId);

            await _challanEmailRepository.SendChallanEmail(MailMap);
            return Ok("Email successfully sent");

        }
  
    }
}
