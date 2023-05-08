using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IWorkSkillService
{
    Task<WorkSkillDto> Create(WorkSkillCreateDto workSkillCreate, uint userId);
    Task<WorkSkillDto> GetById(uint id);
}