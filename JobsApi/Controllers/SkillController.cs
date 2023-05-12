using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobsApi.Dtos;
using JobsApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobsApi.Controllers;

[Route("api/skills")]
[ApiController]
public class SkillController : ControllerBase
{
    private readonly ISkillService _skillService;

    public SkillController(ISkillService skillService)
    {
        _skillService = skillService;
    }

    [HttpGet("{id}")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(SkillDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ErrorDto))]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var skill = await _skillService.GetById(id);
        return Ok(skill);
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(SkillDto))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, type: typeof(ErrorDto))]
    public async Task<IActionResult> Post([FromBody] SkillCreateDto skillCreate)
    {
        var skill = await _skillService.Create(skillCreate);
        return CreatedAtAction(nameof(Get), new
        {
            skill.Id
        }, skill);
    }

    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<SkillDto>))]
    public async Task<IActionResult> List()
    {
        var skills = await _skillService.List();
        return Ok(skills);
    }
}