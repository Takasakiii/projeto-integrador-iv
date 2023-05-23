using JobsApi.Dtos;
using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<UserModel>
{
    Task<UserModel?> GetByEmail(string email);
    Task<(IEnumerable<UserModel>, int)> Filter(UserFilterDto filter);
    new Task<UserModel?> GetByIdNoIncludes(uint id);
    Task<IEnumerable<SkillCountDto>> GetSkillCount();
}