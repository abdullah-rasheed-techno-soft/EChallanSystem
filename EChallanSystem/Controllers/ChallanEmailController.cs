using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
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

  
        [HttpPost]
        public  IActionResult SendChallanEmail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("ladarius25@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("ladarius25@ethereal.email"));
            email.Subject = "Test Email";
            email.Body = new TextPart(TextFormat.Plain)
            {
                Text = body
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ladarius25@ethereal.email", "M1KqBSgtnZ7nZhUpa4");
            smtp.Send(email);
            smtp.Disconnect(true);
            return Ok("Email for Challan sent successfully");
        }

    }
}
