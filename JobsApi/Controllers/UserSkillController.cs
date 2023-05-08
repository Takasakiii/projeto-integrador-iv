using System.Net;
using FluentValidation.Results;
using JobsApi.Dtos;
using JobsApi.Extensions;
using JobsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobsApi.Controllers;

[Route("api/user-skills")]
[ApiController]
public class UserSkillController : ControllerBase
{
    private readonly IUserSkillService _userSkillService;

    public UserSkillController(IUserSkillService userSkillService)
    {
        _userSkillService = userSkillService;
    }

    [HttpGet("{id}")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(UserSkillDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ErrorDto))]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var userSkill = await _userSkillService.GetById(id);
        return Ok(userSkill);
    }

    [HttpPost]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(UserSkillDto))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, type: typeof(ErrorDto))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, type: typeof(ErrorDto))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(IEnumerable<ValidationFailure>))]
    public async Task<IActionResult> Post([FromBody] UserSkillCreateDto userSkillCreate)
    {
        var userId = User.GetId();
        var userSkill = await _userSkillService.Create(userSkillCreate, userId);

        return CreatedAtAction(nameof(Get), new
        {
            userSkill.Id
        }, userSkill);
    }

    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<UserSkillDto>))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, type: typeof(IEnumerable<ValidationFailure>))]
    public async Task<IActionResult> List([FromQuery] UserSkillFilterDto filter)
    {
        var userSkill = await _userSkillService.Filter(filter);

        return Ok(userSkill);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        var userId = User.GetId();
        await _userSkillService.Delete(id, userId);
            
        return Ok();
    }
}