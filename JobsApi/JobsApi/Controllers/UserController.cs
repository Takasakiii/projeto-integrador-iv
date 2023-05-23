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

    [HttpGet("{id}")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(UserDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ErrorDto))]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var user = await _userService.GetById(id);

        return Ok(user);
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(UserDto))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, type: typeof(ErrorDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(IEnumerable<ValidationFailure>))]
    public async Task<IActionResult> Create([FromBody] UserCreateDto userCreate)
    {
        var user = await _userService.Create(userCreate);
        return CreatedAtAction(nameof(Get), new
        {
            user.Id
        }, user);
    }

    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<UserDto>))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(IEnumerable<ValidationFailure>))]
    public async Task<IActionResult> List([FromQuery] UserFilterDto userFilter)
    {
        var users = await _userService.List(userFilter);
        return Ok(users);
    }

    [HttpPut("{id}")]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(UserDto))]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, type: typeof(ErrorDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ErrorDto))]
    public async Task<IActionResult> Update([FromBody] UserUpdateDto userUpdate, [FromRoute] uint id)
    {
        var userId = User.GetId();
        var user = await _userService.Update(userUpdate, id, userId);

        return Ok(user);
    }
}