using JobsApi.Dtos;
using JobsApi.Services.Interfaces;
using Lina.DynamicServicesProvider.Attributes;

namespace JobsApi.Services;

[Service(typeof(IStatisticService))]
public class StatisticService : IStatisticService
{
    private readonly IUserSkillService _userSkillService;

    public StatisticService(IUserSkillService userSkillService)
    {
        _userSkillService = userSkillService;
    }

    public async Task<IEnumerable<UserSkillCountDto>> MostUsedSkills()
    {
        var userSkills = await _userSkillService.MostUsed();

        var skills = userSkills
            .GroupBy(x => x.SkillId)
            .Select(x => new UserSkillCountDto(x.First().Skill ?? "", x.Count()));

        return skills;
    }
}