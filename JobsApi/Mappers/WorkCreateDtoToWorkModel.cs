using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class WorkCreateDtoToWorkModel : DynamicMapperProfile<WorkCreateDto, WorkModel>
{
    protected override void Map(IMappingExpression<WorkCreateDto, WorkModel> mappingExpression)
    {
    }
}