using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class JobSkillModelToJobSkillDto : DynamicMapperProfile<JobSkillModel, JobSkillDto>
{
    protected override void Map(IMappingExpression<JobSkillModel, JobSkillDto> mappingExpression)
    {
        mappingExpression.ForCtorParam(nameof(JobSkillDto.Skill),
            x => x.MapFrom(y => y.Skill != null ? y.Skill.Name : ""));
    }
}