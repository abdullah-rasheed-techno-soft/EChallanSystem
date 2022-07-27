using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using EChallanSystem.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;


namespace EChallanEmailSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ChallanEmailController : ControllerBase
    {

        //private readonly ICitizenRepository _citizenRepository;
        //private readonly IMapper _mapper;
        //public ChallanEmailController( IMapper mapper, ICitizenRepository citizenRepository)
        //{

        //    _mapper = mapper;
        //    _citizenRepository = citizenRepository;
        //}
        private readonly IEmailService _emailService;
        public ChallanEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public  IActionResult SendChallanEmail(EmailDTO request)
        {
            _emailService.SendMail(request);  
    
            return Ok("Email for Challan sent successfully");
        }

    }
}
