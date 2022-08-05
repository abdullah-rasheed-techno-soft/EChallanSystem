using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("/[controller]")]
[ApiController]
public class SecuredController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSecuredData()
    {
        return Ok("This Secured Data is available only for Authenticated Users.");
    }
}