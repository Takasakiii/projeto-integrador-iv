using JobsApi.Dtos.User;

namespace JobsApi.Services.Interfaces;

public interface IJwtService
{
    string GenerateUserToken(UserDto user);
}