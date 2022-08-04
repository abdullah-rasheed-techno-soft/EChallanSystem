using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Helper;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using EChallanSystem.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public CitizenController(ICitizenRepository citizenRepostiory,IMapper mapper, IApplicationUserRepo applicationUserRepo, IConfiguration configuration,IUserService userService)
        {
            _citizenRepository = citizenRepostiory;
            _mapper = mapper;
            _userService = userService;
            _applicationUserRepo = applicationUserRepo;
            _configuration = configuration;
        }
        [HttpGet("{id}"),Authorize(Roles="Manager")] 
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
     
        [HttpGet, Authorize(Roles = "Manager")]
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

    }
}
