using EChallanSystem.Models;
using EChallanSystem.Services;
using Microsoft.AspNetCore.Mvc;

[Route("/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpPost("RegisterManager")]
    public async Task<ActionResult> RegisterManagerAsync(RegisterModel model)
    {
        var result = await _userService.RegisterManagerAsync(model);
        return Ok(result);
    }
}