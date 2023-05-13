using System.Collections;
using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(ISkillRepository))]
public class SkillRepository : BaseRepository<SkillModel>, ISkillRepository
{
    private readonly AppDbContext _context;

    public SkillRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SkillModel>> List()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<SkillModel?> GetByName(string name)
    {
        return await _context.Skills.FirstOrDefaultAsync(x => x.Name == name);
    }
}