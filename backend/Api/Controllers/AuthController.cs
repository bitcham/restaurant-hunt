using Asp.Versioning;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
public class AuthController(ILogger<AuthController> _logger, IAuthService authService) : ControllerBase
{
    
    [HttpPost(ApiEndpoints.Auth.Register)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<UserResponse>> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.Register(request, cancellationToken);
        
        return CreatedAtAction(
            actionName: nameof(UserController.GetUserById), 
            controllerName: "User", 
            routeValues: new { id = result.Id, version = "1.0" },
            value: result
        );
    }

    [HttpPost(ApiEndpoints.Auth.Login)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.Login(request, cancellationToken);
        
        return Ok(result);
    }
    
}