using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserModelToUserDto : DynamicMapperProfile<UserModel, UserDto>
{
    protected override void Map(IMappingExpression<UserModel, UserDto> mappingExpression)
    {
        mappingExpression.ForMember(x => x.Works, y => y.MapFrom(z => z.Works));
    }
}