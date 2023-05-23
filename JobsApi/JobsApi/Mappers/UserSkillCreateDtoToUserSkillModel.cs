using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserSkillCreateDtoToUserSkillModel : DynamicMapperProfile<UserSkillCreateDto, UserSkillModel>
{
    protected override void Map(IMappingExpression<UserSkillCreateDto, UserSkillModel> mappingExpression)
    {
        mappingExpression.ForCtorParam(nameof(UserSkillModel.UserId), x => x.MapFrom(_ => 0));
        mappingExpression.ForCtorParam(nameof(UserSkillModel.SkillId), x => x.MapFrom(_ => 0));
        mappingExpression.ForMember(x => x.Skill, y => y.Ignore());
    }
}