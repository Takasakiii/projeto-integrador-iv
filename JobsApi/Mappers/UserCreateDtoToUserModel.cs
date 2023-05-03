using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserCreateDtoToUserModel : DynamicMapperProfile<UserCreateDto, UserModel>
{
    protected override void Map(IMappingExpression<UserCreateDto, UserModel> mappingExpression)
    {
    }
}