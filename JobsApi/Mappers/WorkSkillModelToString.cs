using AutoMapper;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class WorkSkillModelToString : DynamicMapperProfile<WorkSkillModel, string>
{
    protected override void Map(IMappingExpression<WorkSkillModel, string> mappingExpression)
    {
        mappingExpression.ConvertUsing(x => x.Skill!.Name);
    }
}