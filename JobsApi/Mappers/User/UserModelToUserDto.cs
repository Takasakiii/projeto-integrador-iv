using AutoMapper;
using JobsApi.Dtos.User;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers.User;

public class UserModelToUserDto : DynamicMapperProfile<UserModel, UserDto>
{
    protected override void Map(IMappingExpression<UserModel, UserDto> mappingExpression)
    {
    }
}