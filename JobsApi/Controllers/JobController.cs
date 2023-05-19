using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JobsApi.Dtos;
using JobsApi.Extensions;
using JobsApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobsApi.Controllers;

[Route("api/jobs")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] JobCreateDto jobCreate)
    {
        var userId = User.GetId();
        var job = await _jobService.Create(jobCreate, userId);

        return Ok(job);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        var userId = User.GetId();
        await _jobService.Delete(id, userId);
        return Ok();
    }
}