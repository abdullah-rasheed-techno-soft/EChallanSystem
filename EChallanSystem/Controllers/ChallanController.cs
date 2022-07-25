﻿using EChallanSystem.Models;
using EChallanSystem.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EChallanSystem.Controllers
{
    [Route("/[controller]/[action]")]
    [ApiController]
    public class ChallanController : ControllerBase
    {
        private readonly IChallanRepository _challanRepository;
        public ChallanController(IChallanRepository challanRepostiory)
        {
            _challanRepository = challanRepostiory;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Challan>> GetChallan(int id)
        {
            var challan = await _challanRepository.GetChallan(id);
            if (challan is null)
            {
                return NotFound("Challans not found");
            }
            return Ok(challan);
        }
        [HttpGet]
        public async Task<ActionResult<List<Challan>>> GetChallans()
        {
            var challan = await _challanRepository.GetChallans();
            if (challan is null)
            {
                return NotFound("Challan not found");
            }
            return Ok(challan);
        }
        [HttpPost]
        public async Task<ActionResult<List<Challan>>> AddChallan(Challan newChallan)
        {
            var challan = await _challanRepository.AddChallan(newChallan);
            return Ok(challan);

        }
    }
}