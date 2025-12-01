using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ILogger<AuthController> _logger, IAuthService authService) : ControllerBase
{
    
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<UserResponse>> Register(RegisterUserRequest request)
    {
        var result = await authService.Register(request);
        
        return CreatedAtAction(nameof(Register), result);
    }
    
}