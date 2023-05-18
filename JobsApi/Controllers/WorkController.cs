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

[Route("api/works")]
[ApiController]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpGet("{id}")]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(WorkDto))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, type: typeof(ErrorDto))]
    public async Task<IActionResult> Get([FromRoute] uint id)
    {
        var work = await _workService.GetById(id);
        return Ok(work);
    }

    [HttpPost]
    [Authorize(Roles = nameof(UserDtoType.Professional))]
    [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(WorkDto))]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized)]
    [SwaggerResponse((int)HttpStatusCode.Forbidden, type: typeof(ErrorDto))]
    public async Task<IActionResult> Post([FromBody] WorkCreateDto workCreate)
    {
        var userId = User.GetId();

        var work = await _workService.Create(workCreate, userId);

        return CreatedAtAction(nameof(Get), new
        {
            work.Id
        }, work);
    }

    [HttpGet]
    [SwaggerResponse((int)HttpStatusCode.OK, type: typeof(IEnumerable<WorkDto>))]
    public async Task<IActionResult> List([FromQuery] WorkFilterDto workFilter)
    {
        var works = await _workService.List(workFilter);
        return Ok(works);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] uint id)
    {
        var userId = User.GetId();
        await _workService.Delete(id, userId);
        return Ok();
    }
}