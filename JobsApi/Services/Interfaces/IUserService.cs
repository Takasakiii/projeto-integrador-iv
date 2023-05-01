using JobsApi.Dtos.Auth;
using JobsApi.Dtos.User;

namespace JobsApi.Services.Interfaces;

public interface IUserService
{
    ValueTask CreateAdminUser();
    Task<JwtDto> Login(LoginDto login);
    Task<UserDto> GetById(uint id);
}