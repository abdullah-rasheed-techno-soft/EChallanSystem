using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Helper;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizenRepository _citizenRepository;
        private readonly IApplicationUserRepo _applicationUserRepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public CitizenController(ICitizenRepository citizenRepostiory,IMapper mapper, IApplicationUserRepo applicationUserRepo, IConfiguration configuration)
        {
            _citizenRepository = citizenRepostiory;
            _mapper = mapper;
            _applicationUserRepo = applicationUserRepo;
            _configuration = configuration;
        }
        [HttpGet("{id}")] 
        public async Task<ActionResult<CitizenDTO>> GetCitizen(int id)
        {
            try
            {
                Citizen citizen = await _citizenRepository.GetCitizen(id);
                var citizenDto = _mapper.Map<CitizenDTO>(citizen);
                if (citizen is null)
                {
                    return NotFound("Citizens not found");
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(citizenDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<CitizenDTO>>> GetCitizens()
        {
            try
            {
                List<Citizen> citizen = await _citizenRepository.GetCitizens();
                var citizenDto = _mapper.Map<List<CitizenDTO>>(citizen);
                if (citizen is null)
                {
                    return NotFound("Citizen not found");
                }
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                return Ok(citizenDto);
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult<List<CitizenDTO>>> RegisterCitizen([FromBody]CitizenDTO newCitizen)
        {
            try
            {
                if (newCitizen == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var citizenDto = _mapper.Map<Citizen>(newCitizen);
                citizenDto.User.Role = "Citizen";
                await _citizenRepository.AddCitizen(citizenDto);
                return Ok("Successfully Created");
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> LoginCitizen([FromBody] LoginDTO citizenLogin)
        {
            try
            {
                if (citizenLogin == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var applicationUserDto = _mapper.Map<ApplicationUser>(citizenLogin);
                ApplicationUser check= await _applicationUserRepo.GetUserByEmail(citizenLogin.Email);
                if (check == null)
                {
                    return NotFound("Incorrect Email");
                }
                if (check.Role=="Citizen")
                {
                    if (check.Password == citizenLogin.Password)
                    {
                        string token = CreateToken(check);
                        return Ok(token);
                    }
                    else
                    {
                        return NotFound("Incorrect Password");
                    }
                }
                else
                {
                    return NotFound("Incorrect Email");

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        private string CreateToken(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role,user.Role),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
           
        }
    }
}
