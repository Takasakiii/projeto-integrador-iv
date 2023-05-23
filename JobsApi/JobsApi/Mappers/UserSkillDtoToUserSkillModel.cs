using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserSkillDtoToUserSkillModel : DynamicMapperProfile<UserSkillDto, UserSkillModel>
{
    protected override void Map(IMappingExpression<UserSkillDto, UserSkillModel> mappingExpression)
    {
    }
}