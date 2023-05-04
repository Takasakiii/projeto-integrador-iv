using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class SkillCreateDtoToSkillModel : DynamicMapperProfile<SkillCreateDto, SkillModel>
{
    protected override void Map(IMappingExpression<SkillCreateDto, SkillModel> mappingExpression)
    {
    }
}