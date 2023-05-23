using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class WorkCreateDtoToWorkModel : DynamicMapperProfile<WorkCreateDto, WorkModel>
{
    protected override void Map(IMappingExpression<WorkCreateDto, WorkModel> mappingExpression)
    {
        mappingExpression.ForCtorParam(nameof(WorkModel.UserId), x => x.MapFrom(_ => 0));
        mappingExpression.ForMember(x => x.Skills, y => y.Ignore());
    }
}