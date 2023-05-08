using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserSkillCreateDtoToUserSkillModel : DynamicMapperProfile<UserSkillCreateDto, UserSkillModel>
{
    protected override void Map(IMappingExpression<UserSkillCreateDto, UserSkillModel> mappingExpression)
    {
    }
}