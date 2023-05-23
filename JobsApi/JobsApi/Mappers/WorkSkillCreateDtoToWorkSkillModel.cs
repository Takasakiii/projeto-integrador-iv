using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class WorkSkillCreateDtoToWorkSkillModel : DynamicMapperProfile<WorkSkillCreateDto, WorkSkillModel>
{
    protected override void Map(IMappingExpression<WorkSkillCreateDto, WorkSkillModel> mappingExpression)
    {
    }
}