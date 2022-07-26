﻿using AutoMapper;
using EChallanSystem.DTO;
using EChallanSystem.Helper;
using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly ICitizenRepository _citizenRepository;
        private readonly IMapper _mapper;
        public CitizenController(ICitizenRepository citizenRepostiory,IMapper mapper)
        {
            _citizenRepository = citizenRepostiory;
            _mapper = mapper;

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CitizenDTO>> GetCitizen(int id)
        {
            var citizen = await _citizenRepository.GetCitizen(id);
            var citizenDto = _mapper.Map<CitizenDTO>(citizen);
            if (citizen is null)
            {
                return NotFound("Citizens not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(citizenDto);
        }
        [HttpGet]
        public async Task<ActionResult<List<CitizenDTO>>> GetCitizens()
        {
            var citizen = await _citizenRepository.GetCitizens();
            var citizenDto = _mapper.Map<List<CitizenDTO>>(citizen);
            if (citizen is null)
            {
                return NotFound("Citizen not found");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(citizenDto);
        }
        [HttpPost]
        public async Task<ActionResult<List<CitizenDTO>>> AddCitizen([FromBody]CitizenDTO newCitizen)
        {
            if (newCitizen == null)
                return BadRequest(ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var citizenDto = _mapper.Map<Citizen>(newCitizen);
            await _citizenRepository.AddCitizen(citizenDto);
            return Ok("Successfully Created");

            

        }
    }
}
