using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserModelToUserDto : DynamicMapperProfile<UserModel, UserDto>
{
    protected override void Map(IMappingExpression<UserModel, UserDto> mappingExpression)
    {
    }
}