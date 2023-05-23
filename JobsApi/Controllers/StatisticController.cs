using System.Net;
using JobsApi.Dtos;
using JobsApi.Services.Interfaces;
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

    [HttpGet("most-used-language")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<UserSkillCountDto>))]
    public async Task<IActionResult> GetMostUsed()
    {
        var skills = await _statisticService.MostUsedSkills();
        return Ok(skills);
    }

    [HttpGet("average-value-language")]
    public async Task<IActionResult> GetAverageValue()
    {
        var result = await _statisticService.ExpectedValuePerLanguage();
        return Ok(result);
    }

    [HttpGet("amount-skill-user")]
    public async Task<IActionResult> GetSkillCount()
    {
        var result = await _statisticService.AmountOfSkillsPerUser();
        return Ok(result);
    }

    [HttpGet("job-level-count")]
    public async Task<IActionResult> GetJobLevelCount()
    {
        var result = await _statisticService.GetJobLevelCount();
        return Ok(result);
    }

    [HttpGet("less-used-language")]
    public async Task<IActionResult> GetLessUsed()
    {
        
        var skills = await _statisticService.LessUsedSkills();
        return Ok(skills);
    }
}