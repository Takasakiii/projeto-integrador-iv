using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using JobsApi.Dtos;
using JobsApi.Extensions;
using JobsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
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

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(UserDto))]
    [SwaggerResponse((int)HttpStatusCode.Conflict)]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(IEnumerable<ValidationFailure>))]
    public async Task<IActionResult> Create([FromBody] UserCreateDto userCreate)
    {
        var user = await _userService.Create(userCreate);
        return Ok(user);
    }
}