using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface IWorkSkillRepository : IBaseRepository<WorkSkillModel>
{
    void DeleteRange(IEnumerable<WorkSkillModel> models);
    Task<IEnumerable<WorkSkillModel>> GetByWork(uint workId);
}