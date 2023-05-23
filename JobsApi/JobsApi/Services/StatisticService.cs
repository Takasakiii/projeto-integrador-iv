using JobsApi.Dtos;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IStatisticService))]
public class StatisticService : IStatisticService
{
    private readonly IUserSkillService _userSkillService;
    private readonly IUserService _userService;
    private readonly IJobService _jobService;

    public StatisticService(IUserSkillService userSkillService, IUserService userService, IJobService jobService)
    {
        _userSkillService = userSkillService;
        _userService = userService;
        _jobService = jobService;
    }

    public async Task<IEnumerable<UserSkillCountDto>> MostUsedSkills()
    {
        var userSkills = await _userSkillService.MostUsed();

        var skills = userSkills
            .GroupBy(x => x.SkillId)
            .Select(x => new UserSkillCountDto(x.First().Skill ?? "", x.Count()))
            .OrderByDescending(x => x.Count)
            .Take(10);

        return skills;
    }

    public async Task<IEnumerable<ExpectedValueLanguageDto>> ExpectedValuePerLanguage()
    {
        var userSkills = await _userSkillService.MostUsed();

        var skills = userSkills
            .GroupBy(x => x.SkillId);

        var skillGroups = skills as IGrouping<uint, UserSkillDto>[] ?? skills.ToArray();
        
        var result = new List<ExpectedValueLanguageDto>(skillGroups.Length);

        foreach (var skillGroup in skillGroups)
        {
            long value = 0;

            foreach (var userSkill in skillGroup)
            {
                var user = await _userService.GetById(userSkill.UserId);
                if (user.ExpectedValue != null) value += (long)user.ExpectedValue;
            }

            value = value / skillGroup.Count();
            
            result.Add(new ExpectedValueLanguageDto(skillGroup.First().Skill ?? "", value));
        }

        return result.OrderByDescending(x => x.Value).Take(10);
    }

    public async Task<IEnumerable<SkillCountDto>> AmountOfSkillsPerUser()
    {
        var skillCount = await _userService.GetSkillCount();

        return skillCount.OrderByDescending(x => x.Count).Take(10);
    }

    public async Task<IEnumerable<JobLevelCountDto>> GetJobLevelCount()
    {
        var result = await _jobService.GetJobLevelCount();

        return result;
    }
    
    public async Task<IEnumerable<UserSkillCountDto>> LessUsedSkills()
    {
        var userSkills = await _userSkillService.LessUsed();

        var skills = userSkills
            .GroupBy(x => x.SkillId)
            .Select(x => new UserSkillCountDto(x.First().Skill ?? "", x.Count()))
            .OrderByDescending(x => x.Count)
            .Take(10);

        return skills;
    }
}