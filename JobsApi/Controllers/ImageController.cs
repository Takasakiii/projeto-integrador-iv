using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JobsApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace JobsApi.Controllers;

[Route("api/images")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpGet("{id}")]
    [SwaggerResponse((int)HttpStatusCode.OK)]
    [SwaggerResponse((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var image = await _imageService.GetById(id);

        return File(image.Data, image.Mime);
    }

    [HttpPost]
    [SwaggerResponse((int)HttpStatusCode.Created, type: typeof(string))]
    public async Task<IActionResult> Create(IFormFile file)
    {
        var image = await _imageService.Create(file);
        return CreatedAtAction(nameof(Get), new
        {
            Id = image
        }, image);
    }
}