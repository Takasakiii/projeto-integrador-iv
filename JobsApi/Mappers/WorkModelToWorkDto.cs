using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class WorkModelToWorkDto : DynamicMapperProfile<WorkModel, WorkDto>
{
    protected override void Map(IMappingExpression<WorkModel, WorkDto> mappingExpression)
    {
        mappingExpression
            .ForMember(x => x.Skills, y => y.MapFrom(z => ConvertSkillsArrToString(z)));
    }

    private static IEnumerable<string?> ConvertSkillsArrToString(WorkModel model)
    {
        if (model.Skills is null) return Enumerable.Empty<string?>();
        var skillsString = model.Skills.Select(x => x.Skill?.Name).Where(x => x != null);
        return skillsString;
    }
}