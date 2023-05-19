using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class JobCreateDtoToJobModel : DynamicMapperProfile<JobCreateDto, JobModel>
{
    protected override void Map(IMappingExpression<JobCreateDto, JobModel> mappingExpression)
    {
        mappingExpression.ForMember(x => x.Skills, y => y.Ignore());
        mappingExpression.ForCtorParam(nameof(JobModel.UserId), x => x.MapFrom(_ => 0));
    }
}