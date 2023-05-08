using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobsApi.Dtos;
using JobsApi.Extensions;
using JobsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobsApi.Controllers;

[Route("api/work-skills")]
[ApiController]
public class WorkSkillController : ControllerBase
{
    private readonly IWorkSkillService _workSkillService;

    public WorkSkillController(IWorkSkillService workSkillService)
    {
        _workSkillService = workSkillService;
    }

    [HttpGet("{id}")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(WorkSkillDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ErrorDto))]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var workSkill = await _workSkillService.GetById(id);
        return Ok(workSkill);
    }

    [HttpPost]
    [Authorize]
    [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(WorkSkillDto))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, type: typeof(ErrorDto))]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, type: typeof(ErrorDto))]
    [SwaggerResponse((int)HttpStatusCode.Conflict, type: typeof(ErrorDto))]
    public async Task<IActionResult> Post([FromBody] WorkSkillCreateDto workSkillCreate)
    {
        var userId = User.GetId();
        var workSkill = await _workSkillService.Create(workSkillCreate, userId);
        return CreatedAtAction(nameof(Get), new
        {
            workSkill.Id
        }, workSkill);
    }
}