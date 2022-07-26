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
        public ChallanEmailController(IChallanEmailRepository challanEmailRepostiory)
        {
            _challanEmailRepository = challanEmailRepostiory;
        }
    
        [HttpGet]
        public async Task<ActionResult<List<ChallanEmail>>> GetChallanEmails()
        {
            var challanEmail = await _challanEmailRepository.GetChallanEmails();
            if (challanEmail is null)
            {
                return NotFound("Challan Email not found");
            }
            return Ok(challanEmail);
        }
        [HttpPost]
        public async Task<ActionResult<List<ChallanEmail>>> SendChallanEmail(ChallanEmail newChallanEmail)
        {
            var challanEmail = await _challanEmailRepository.SendChallanEmail(newChallanEmail);
            return Ok(challanEmail);

        }
  
    }
}
