using AutoMapper;
using JobsApi.Dtos;
using JobsApi.Models;
using Lina.DynamicMapperConfiguration.Abstracts;

namespace JobsApi.Mappers;

public class UserUpdateDtoToUserModel : DynamicMapperProfile<UserUpdateDto, UserModel>
{
    protected override void Map(IMappingExpression<UserUpdateDto, UserModel> mappingExpression)
    {
    }
}