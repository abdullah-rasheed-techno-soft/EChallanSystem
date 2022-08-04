using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using EChallanSystem.Repository.IServices;
using EChallanSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICitizenRepository _citizenRepository;
        private readonly IApplicationUserRepo _applicationUserRepo;
        private readonly ITrafficWardenRepository _trafficWardenRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AuthController(ICitizenRepository citizenRepostiory, IMapper mapper, IApplicationUserRepo applicationUserRepo, IConfiguration configuration, IUserService userService, ITrafficWardenRepository trafficWardenRepository,IManagerRepository managerRepository)
        {
            _managerRepository=managerRepository;
            _citizenRepository = citizenRepostiory;
            _mapper = mapper;
            _userService = userService;
            _applicationUserRepo = applicationUserRepo;
            _configuration = configuration;
            _trafficWardenRepository = trafficWardenRepository;
        }
        [HttpGet]
        public ActionResult<string> GetName()
        {
            var userName = _userService.GetName();
            if (userName.IsNullOrEmpty())
            {
                return Ok("Not Logged In");
            }
            return Ok(userName);
        }
        [HttpPost]

        public async Task<ActionResult<List<CitizenDTO>>> RegisterCitizen([FromBody] CitizenDTO newCitizen)
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
                ApplicationUser check = await _applicationUserRepo.GetUserByEmail(citizenLogin.Email);
                if (check == null)
                {
                    return NotFound("Incorrect Email");
                }
                if (check.Role == "Citizen")
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
        [HttpPost]
        public async Task<ActionResult<List<TrafficWardenDTO>>> RegisterTrafficWarden([FromBody] TrafficWardenDTO newTrafficWarden)
        {
            try
            {
                if (newTrafficWarden == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var wardenDto = _mapper.Map<TrafficWarden>(newTrafficWarden);
                wardenDto.User.Role = "TrafficWarden";
                await _trafficWardenRepository.AddTrafficWarden(wardenDto);
                return Ok("Successfully Created");
            }
            catch (Exception ex)
            {
                throw new Exception("Exception occured ", ex);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> LoginTrafficWarden([FromBody] LoginDTO wardenLogin)
        {
            try
            {
                if (wardenLogin == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var applicationUserDto = _mapper.Map<ApplicationUser>(wardenLogin);
                ApplicationUser check = await _applicationUserRepo.GetUserByEmail(wardenLogin.Email);
                if (check == null)
                {
                    return NotFound("Incorrect Email");
                }
                if (check.Role == "TrafficWarden")
                {
                    if (check.Password == wardenLogin.Password)
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
        [HttpPost]
        public async Task<ActionResult<ApplicationUser>> LoginManager([FromBody] LoginDTO managerLogin)
        {
            try
            {
                if (managerLogin == null)
                    return BadRequest(ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var applicationUserDto = _mapper.Map<ApplicationUser>(managerLogin);
                ApplicationUser check = await _applicationUserRepo.GetUserByEmail(managerLogin.Email);
                if (check == null)
                {
                    return NotFound("Incorrect Email");
                }
                if (check.Role == "Manager")
                {
                    if (check.Password == managerLogin.Password)
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
