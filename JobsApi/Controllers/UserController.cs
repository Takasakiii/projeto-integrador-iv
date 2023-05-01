using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobsApi.Dtos.User;
using JobsApi.Extensions;
using JobsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobsApi.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("@me")]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(UserDto))]
    public async Task<IActionResult> Me()
    {
        var id = User.GetId();
        var user = await _userService.GetById(id);

        return Ok(user);
    }
}