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

[Route("api/statistics")]
[ApiController]
public class StatisticController : ControllerBase
{
    private readonly IStatisticService _statisticService;

    public StatisticController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    [HttpGet("most-used")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<UserSkillCountDto>))]
    public async Task<IActionResult> GetMostUsed()
    {
        var skills = await _statisticService.MostUsedSkills();
        return Ok(skills);
    }
}