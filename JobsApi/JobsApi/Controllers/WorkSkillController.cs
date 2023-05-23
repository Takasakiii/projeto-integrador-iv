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
}