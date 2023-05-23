using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IStatisticService
{
    Task<IEnumerable<UserSkillCountDto>> MostUsedSkills();
    Task<IEnumerable<ExpectedValueLanguageDto>> ExpectedValuePerLanguage();
    Task<IEnumerable<SkillCountDto>> AmountOfSkillsPerUser();
    Task<IEnumerable<JobLevelCountDto>> GetJobLevelCount();
    Task<IEnumerable<UserSkillCountDto>> LessUsedSkills();
}