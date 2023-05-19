using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class JobModelToJobDto : DynamicMapperProfile<JobModel, JobDto>
{
    protected override void Map(IMappingExpression<JobModel, JobDto> mappingExpression)
    {
    }
}