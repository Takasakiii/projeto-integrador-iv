using JobsApi.Dtos;
using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface IUserSkillRepository : IBaseRepository<UserSkillModel>
{
    Task<IEnumerable<UserSkillModel>> Filter(UserSkillFilterDto filter);
    Task<UserSkillModel?> GetByUserAndSkill(uint userId, uint skillId);
    Task<IEnumerable<UserSkillModel>> GetMostUsed();
    Task<IEnumerable<UserSkillModel>> GetLessUsed();
}