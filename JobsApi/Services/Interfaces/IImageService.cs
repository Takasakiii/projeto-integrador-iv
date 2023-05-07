using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IImageService
{
    Task<string> Create(IFormFile file);
    Task<ImageDto> GetById(string id);
    ValueTask Delete(string id);
}