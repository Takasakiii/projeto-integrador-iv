using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IWorkService
{
    Task<WorkDto> GetById(uint id);
    Task<WorkDto> Create(WorkCreateDto workCreate, uint userId);
    Task<IEnumerable<WorkDto>> List(WorkFilterDto workFilter);
    ValueTask Delete(uint id, uint userId);
}