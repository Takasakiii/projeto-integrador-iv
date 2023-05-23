using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IJobService
{
    Task<JobDto> Create(JobCreateDto jobCreate, uint userId);
    ValueTask Delete(uint id, uint userId);
    Task<IEnumerable<JobDto>> List();
    Task<IEnumerable<JobLevelCountDto>> GetJobLevelCount();
}