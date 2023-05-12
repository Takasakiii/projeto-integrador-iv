using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserSkillModelToUserSkillDto : DynamicMapperProfile<UserSkillModel, UserSkillDto>
{
    protected override void Map(IMappingExpression<UserSkillModel, UserSkillDto> mappingExpression)
    {
        mappingExpression.ForMember(x => x.Skill, y => y.MapFrom(z => z.Skill!.Name));
    }
}