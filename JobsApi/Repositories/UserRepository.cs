using JobsApi.Database;
using JobsApi.Dtos;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Lina.UtilsExtensions;
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

    public async Task<(IEnumerable<UserModel>, int)> Filter(UserFilterDto filter)
    {
        var query = _context.Users.Where(x => !filter.Type.HasValue || (UserModelType)filter.Type.Value == x.Type);

        var users = await query
            .Paginate(filter.Page ?? 1, filter.PageSize ?? 30)
            .ToListAsync();
        var count = await query.CountAsync();

        return (users, count);
    }
}