using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface IJobSkillRepository : IBaseRepository<JobSkillModel>
{
    Task<IEnumerable<JobSkillModel>> GetByJob(uint jobId);
    void DeleteRange(IEnumerable<JobSkillModel> models);
}