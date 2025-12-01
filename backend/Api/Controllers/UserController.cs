using Core.Application.Dtos.Responses;
using Core.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    
    [Authorize(Policy = "AdminOrUser")]
    [HttpGet(ApiEndpoints.Users.GetUserById)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        var foundUser = await userService.GetByIdAsync(id);
        return Ok(foundUser);
    }
    
}