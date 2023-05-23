using JobsApi.Dtos;
using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface IJobRepository : IBaseRepository<JobModel>
{
    Task<IEnumerable<JobModel>> ListWithIncludes();
    Task<IEnumerable<JobLevelCountDto>> GetJobLevelCount();
}