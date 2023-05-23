using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IJobSkillService
{
    ValueTask Create(IEnumerable<JobSkillDto> skills, uint jobId);
    ValueTask Clear(uint jobId);
}