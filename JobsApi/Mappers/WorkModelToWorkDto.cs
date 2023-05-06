using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class WorkModelToWorkDto : DynamicMapperProfile<WorkModel, WorkDto>
{
    protected override void Map(IMappingExpression<WorkModel, WorkDto> mappingExpression)
    {
    }
}