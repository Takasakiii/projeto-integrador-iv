using JobsApi.Database;
using JobsApi.Dtos;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(IUserSkillRepository))]
public class UserSkillRepository : BaseRepository<UserSkillModel>, IUserSkillRepository
{
    private readonly AppDbContext _context;

    public UserSkillRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserSkillModel>> Filter(UserSkillFilterDto filter)
    {
        return await _context.UsersSkills.Where(x => !filter.UserId.HasValue || x.UserId == filter.UserId.Value)
            .ToListAsync();
    }
}