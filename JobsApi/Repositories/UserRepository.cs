using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(IUserRepository))]
public class UserRepository : BaseRepository<UserModel>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserModel?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }
}