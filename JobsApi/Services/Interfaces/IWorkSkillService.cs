using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IWorkSkillService
{
    Task<WorkSkillDto> GetById(uint id);
    Task Create(IEnumerable<string> skills, uint workId);
}