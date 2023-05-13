using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IStatisticService
{
    Task<IEnumerable<UserSkillCountDto>> MostUsedSkills();
}