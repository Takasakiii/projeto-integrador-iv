using JobsApi.Models;

namespace JobsApi.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<UserModel>
{
    Task<UserModel?> GetByEmail(string email);
}