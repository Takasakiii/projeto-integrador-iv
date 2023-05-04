using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class SkillModelToSkillDto : DynamicMapperProfile<SkillModel, SkillDto>
{
    protected override void Map(IMappingExpression<SkillModel, SkillDto> mappingExpression)
    {
    }
}