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
            .Where(x => !filter.SkillId.HasValue || x.SkillId == filter.SkillId.Value)
            .ToListAsync();
    }

    public async Task<UserSkillModel?> GetByUserAndSkill(uint userId, uint skillId)
    {
        return await _context.UsersSkills.FirstOrDefaultAsync(x => x.UserId == userId && x.SkillId == skillId);
    }

    public async Task<IEnumerable<UserSkillModel>> GetMostUsed()
    {
        return await _context.UsersSkills.Include(x => x.Skill)
            .GroupBy(x => x.UserId)
            .Select(x => x.OrderByDescending(y => y.Years).ToList())
            .Select(x => x.First())
            .ToListAsync();
    }
    
    public async Task<IEnumerable<UserSkillModel>> GetLessUsed()
    {
        return await _context.UsersSkills.Include(x => x.Skill)
            .GroupBy(x => x.UserId)
            .Select(x => x.OrderByDescending(y => y.Years).ToList())
            .Select(x => x.Last())
            .ToListAsync();
    }
}