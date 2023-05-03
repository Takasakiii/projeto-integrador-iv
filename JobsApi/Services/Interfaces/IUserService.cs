using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IUserService
{
    ValueTask CreateAdminUser();
    Task<JwtDto> Login(LoginDto login);
    Task<UserDto> GetById(uint id);
    Task<UserDto> Create(UserCreateDto userCreate);
    Task<IEnumerable<UserDto>> List(UserFilterDto userFilter);
}