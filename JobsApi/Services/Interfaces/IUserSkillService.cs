using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IUserSkillService
{
    Task<UserSkillDto> Create(UserSkillCreateDto userSkillCreate, uint userId);
    Task<UserSkillDto> GetById(uint id);
    Task<IEnumerable<UserSkillDto>> Filter(UserSkillFilterDto filter);
    ValueTask Delete(uint id, uint userId);
}