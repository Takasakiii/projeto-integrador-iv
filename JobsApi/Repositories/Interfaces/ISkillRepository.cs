using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface ISkillRepository : IBaseRepository<SkillModel>
{
    Task<IEnumerable<SkillModel>> List();
    Task<SkillModel?> GetByName(string name);
}