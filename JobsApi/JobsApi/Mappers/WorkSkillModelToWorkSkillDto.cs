using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class WorkSkillModelToWorkSkillDto : DynamicMapperProfile<WorkSkillModel, WorkSkillDto>
{
    protected override void Map(IMappingExpression<WorkSkillModel, WorkSkillDto> mappingExpression)
    {
    }
}