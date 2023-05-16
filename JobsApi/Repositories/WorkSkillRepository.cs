using JobsApi.Database;
using JobsApi.Models;
using JobsApi.Repositories.Interfaces;
using Lina.DynamicServicesProvider.Attributes;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Repositories;

[Repository(typeof(IWorkSkillRepository))]
public class WorkSkillRepository : BaseRepository<WorkSkillModel>, IWorkSkillRepository
{
    private readonly AppDbContext _context;

    public WorkSkillRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkSkillModel>> GetByWork(uint workId)
    {
        return await _context.WorksSkills.Where(x => x.WorkId == workId).ToListAsync();
    }

    public void DeleteRange(IEnumerable<WorkSkillModel> models)
    {
        _context.WorksSkills.RemoveRange(models);
    }
}