using JobsApi.Dtos;

namespace JobsApi.Services.Interfaces;

public interface IUserService
{
    Task<JwtDto> Login(LoginDto login);
    Task<UserDto> GetById(uint id);
    Task<UserDto> Create(UserCreateDto userCreate);
    Task<IEnumerable<UserDto>> List(UserFilterDto userFilter);
    Task<UserDto> Update(UserUpdateDto userUpdate, uint userId, uint requestUserId);
    Task<IEnumerable<SkillCountDto>> GetSkillCount();
}